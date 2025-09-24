using System;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SBigCircle
{
    [SouvenirQuestion("What color was {1} in the solution to {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("BigCircle", "Big Circle", typeof(SBigCircle), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessBigCircle(ModuleData module)
    {
        var comp = GetComponent(module, "TheBigCircle");
        yield return WaitForSolve;

        var source = GetField<Array>(comp, "_currentSolution").Get(v => v.Length != 3 ? "expected length 3" : null);
        for (var ix = 0; ix < source.Length; ix++)
            yield return question(SBigCircle.Colors, args: [Ordinal(ix + 1)]).Answers(source.GetValue(ix).ToString());
    }
}
