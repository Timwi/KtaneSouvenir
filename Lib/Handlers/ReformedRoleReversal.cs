using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SReformedRoleReversal
{
    [SouvenirQuestion("Which condition was the solving condition in {0}?", ThreeColumns6Answers, "second", "third", "4th", "5th", "6th", "7th", "8th", TranslateAnswers = true)]
    Condition,

    [SouvenirQuestion("What color was the {1} wire in {0}?", ThreeColumns6Answers, "Navy", "Lapis", "Blue", "Sky", "Teal", "Plum", "Violet", "Purple", "Magenta", "Lavender", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Wire
}

public partial class SouvenirModule
{
    [SouvenirHandler("ReformedRoleReversal", "Reformed Role Reversal", typeof(SReformedRoleReversal), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessReformedRoleReversal(ModuleData module)
    {
        var comp = GetComponent(module, "ReformedRoleReversal");
        var init = GetField<object>(comp, "Init").Get();
        var handleManual = GetField<object>(init, "Manual").Get();
        var fldIndex = GetArrayField<int>(handleManual, "SouvenirIndex");
        var fldWires = GetArrayField<int>(handleManual, "SouvenirWires");

        yield return WaitForSolve;

        var index = fldIndex.Get(expectedLength: 2);
        var wires = fldWires.Get(minLength: 3, maxLength: 9, validator: i => i is < 0 or > 9 ? "expected value 0–9" : null);

        var colors = new[] { "Navy", "Lapis", "Blue", "Sky", "Teal", "Plum", "Violet", "Purple", "Magenta", "Lavender" };
        yield return question(SReformedRoleReversal.Condition).Answers(Ordinal(index[1] + 1));
        for (var ix = 0; ix < wires.Length; ix++)
            yield return question(SReformedRoleReversal.Wire, args: [Ordinal(ix + 1)]).Answers(colors[wires[ix]]);
    }
}