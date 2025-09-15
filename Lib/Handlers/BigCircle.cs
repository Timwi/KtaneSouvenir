using System.Collections.Generic;
using System.Linq;
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

        addQuestions(module, GetField<Array>(comp, "_currentSolution").Get(v => v.Length != 3 ? "expected length 3" : null).Cast<object>()
            .Select((color, ix) => makeQuestion(Question.BigCircleColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { color.ToString() })));
    }
}