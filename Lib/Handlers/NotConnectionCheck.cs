using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotConnectionCheck
{
    [Question("What symbol flashed on the {1} button in {0}?", ThreeColumns6Answers, "+", "-", ".", ":", "/", "_", "=", ",", Arguments = ["top left", "top right", "bottom left", "bottom right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Flashes
}

public partial class SouvenirModule
{
    [Handler("notConnectionCheck", "Not Connection Check", typeof(SNotConnectionCheck), "Quinn Wuest")]
    [ManualQuestion("What were the flashing symbols?")]
    private IEnumerator<SouvenirInstruction> ProcessNotConnectionCheck(ModuleData module)
    {
        var comp = GetComponent(module, "NCCScript");
        yield return WaitForSolve;
        var positions = new[] { "top left", "top right", "bottom left", "bottom right" };

        // Flashes
        var ops = GetArrayField<int>(comp, "ops").Get();
        var puncMarkNames = new[] { "+", "-", ".", ":", "/", "_", "=", "," };
        var puncMarks = Enumerable.Range(0, ops.Length).Select(i => puncMarkNames[ops[i]]).ToArray();
        for (var p = 0; p < 4; p++)
            yield return question(SNotConnectionCheck.Flashes, args: [positions[p]]).Answers(puncMarks[p]);
    }
}
