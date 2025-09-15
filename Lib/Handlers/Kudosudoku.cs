using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SKudosudoku
{
    [SouvenirQuestion("Which square was {1} in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, TranslateArguments = [true], Arguments = ["pre-filled", "not pre-filled"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(4, 4)]
    Prefilled
}

public partial class SouvenirModule
{
    [SouvenirHandler("KudosudokuModule", "Kudosudoku", typeof(SKudosudoku), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessKudosudoku(ModuleData module)
    {
        var comp = GetComponent(module, "KudosudokuModule");
        var shown = GetArrayField<bool>(comp, "_shown").Get(expectedLength: 16).ToArray();  // Take a copy of the array because the module changes it

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.KudosudokuPrefilled, module, formatArgs: new[] { "pre-filled" },
                correctAnswers: Enumerable.Range(0, 16).Where(ix => shown[ix]).Select(coord => new Coord(4, 4, coord)).ToArray()),
            makeQuestion(Question.KudosudokuPrefilled, module, formatArgs: new[] { "not pre-filled" },
                correctAnswers: Enumerable.Range(0, 16).Where(ix => !shown[ix]).Select(coord => new Coord(4, 4, coord)).ToArray()));
    }
}