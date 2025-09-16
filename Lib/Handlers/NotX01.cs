using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotX01
{
    [SouvenirQuestion("Which sector value {1} present on {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    SectorValues
}

public partial class SouvenirModule
{
    [SouvenirHandler("notX01", "Not X01", typeof(SNotX01), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNotX01(ModuleData module)
    {
        var comp = GetComponent(module, "NX01Script");
        yield return WaitForSolve;

        var nums = GetArrayField<int>(comp, "nums").Get();
        var numsStr = nums.Select(i => i.ToString()).ToArray();
        var numsNotPresent = Enumerable.Range(1, 20).Except(nums).Select(i => i.ToString()).ToArray();

        yield return question(SNotX01.SectorValues, args: ["was"]).Answers(numsStr, preferredWrong: numsNotPresent);
        yield return question(SNotX01.SectorValues, args: ["was not"]).Answers(numsNotPresent, preferredWrong: numsStr);
    }
}