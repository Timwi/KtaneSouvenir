using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SChess
{
    [SouvenirQuestion("What was the {1} coordinate in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("a-f", "1-6")]
    Coordinate
}

public partial class SouvenirModule
{
    [SouvenirHandler("ChessModule", "Chess", typeof(SChess), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessChess(ModuleData module)
    {
        var comp = GetComponent(module, "ChessBehaviour");
        var fldIndexSelected = GetArrayField<int>(comp, "indexSelected"); // this contains both the coordinates and the solution

        yield return WaitForActivate;

        var indexSelected = fldIndexSelected.Get(expectedLength: 7, validator: b => b / 10 < 0 || b / 10 >= 6 || b % 10 < 0 || b % 10 >= 6 ? "unexpected value" : null);

        yield return WaitForSolve;

        addQuestions(module, Enumerable.Range(0, 6).Select(i => makeQuestion(SChess.Coordinate, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { "" + ((char) (indexSelected[i] / 10 + 'a')) + (indexSelected[i] % 10 + 1) })));
    }
}