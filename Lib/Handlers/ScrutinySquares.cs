using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SScrutinySquares
{
    [SouvenirQuestion("What was the modified property of the first display in {0}?", OneColumn4Answers, "Word", "Color around word", "Color of background", "Color of word", TranslateAnswers = true)]
    FirstDifference
}

public partial class SouvenirModule
{
    [SouvenirHandler("scrutinySquares", "Scrutiny Squares", typeof(SScrutinySquares), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessScrutinySquares(ModuleData module)
    {
        var comp = GetComponent(module, "ScrutinySquaresScript");
        yield return WaitForSolve;

        var pathCells = GetField<IList>(comp, "pathCells").Get();
        var direction = GetField<Enum>(pathCells[0], "direction", isPublic: true).Get();
        var answer = direction.ToString() switch
        {
            "Up" => "Word",
            "Left" => "Color around word",
            "Right" => "Color of background",
            "Down" => "Color of word",
            _ => throw new AbandonModuleException($"Unexpected value of ‘direction’: {direction}")
        };
        addQuestion(module, Question.ScrutinySquaresFirstDifference, correctAnswers: new[] { answer });
    }
}