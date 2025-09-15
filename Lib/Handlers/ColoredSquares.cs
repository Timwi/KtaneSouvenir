using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SColoredSquares
{
    [SouvenirQuestion("What was the first color group in {0}?", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true)]
    FirstGroup
}

public partial class SouvenirModule
{
    [SouvenirHandler("ColoredSquaresModule", "Colored Squares", typeof(SColoredSquares), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessColoredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "ColoredSquaresModule");
        yield return WaitForSolve;
        addQuestion(module, Question.ColoredSquaresFirstGroup, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor").Get().ToString() });
    }
}