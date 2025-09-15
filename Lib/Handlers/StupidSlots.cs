using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SStupidSlots
{
    [SouvenirQuestion("What was the value of the {1} arrow in {0}?", ThreeColumns6Answers, Arguments = ["top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(-30, 30)]
    Values
}

public partial class SouvenirModule
{
    [SouvenirHandler("stupidSlots", "Stupid Slots", typeof(SStupidSlots), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessStupidSlots(ModuleData module)
    {
        var comp = GetComponent(module, "StupidSlotsScript");
        yield return WaitForSolve;

        var values = GetArrayField<int>(comp, "allValues").Get(expectedLength: 6);
        var validPositions = Enumerable.Range(0, 6).Where(x => values[x] != 0);
        var posNames = new[] { "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right" };
        foreach (var pos in validPositions)
            yield return question(SStupidSlots.Values, args: [posNames[pos]]).Answers(values[pos].ToString());
    }
}