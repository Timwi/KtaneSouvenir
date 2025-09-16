using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEtterna
{
    [SouvenirQuestion("What was the beat for the {1} arrow from the bottom in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 32)]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("etterna", "Etterna", typeof(SEtterna), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessEtterna(ModuleData module)
    {
        var comp = GetComponent(module, "Etterna");
        yield return WaitForSolve;

        var correct = GetArrayField<byte>(comp, "correct").Get(expectedLength: 4, validator: b => b is > 32 or 0 ? "expected 1–32" : null);
        for (var ix = 0; ix < correct.Length; ix++)
            yield return question(SEtterna.Number, args: [Ordinal(ix + 1)]).Answers(correct[ix].ToString());
    }
}