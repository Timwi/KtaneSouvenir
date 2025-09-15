using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SVaricoloredSquares
{
    [SouvenirQuestion("What was the initially pressed color on {0}?", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true)]
    InitialColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("VaricoloredSquaresModule", "Varicolored Squares", typeof(SVaricoloredSquares), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessVaricoloredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "VaricoloredSquaresModule");

        yield return WaitForSolve;

        addQuestion(module, Question.VaricoloredSquaresInitialColor, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor").Get().ToString() });
    }
}