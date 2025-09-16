using System.Collections.Generic;
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

        for (var i = 0; i < 6; i++)
            yield return question(SChess.Coordinate, args: [Ordinal(i + 1)]).Answers("" + ((char) (indexSelected[i] / 10 + 'a')) + (indexSelected[i] % 10 + 1));
    }
}