using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SChess
{
    [SouvenirQuestion("What was the {1} coordinate in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("a-f", "1-6")]
    QCoordinate,

    [SouvenirDiscriminator("the Chess where the {1} coordinate was {0}", Arguments = ["a1", QandA.Ordinal], ArgumentGroupSize = 2)]
    DCoordinate
}

public partial class SouvenirModule
{
    [SouvenirHandler("ChessModule", "Chess", typeof(SChess), "Timwi")]
    [SouvenirManualQuestion("What were the coordinates?")]
    private IEnumerator<SouvenirInstruction> ProcessChess(ModuleData module)
    {
        var comp = GetComponent(module, "ChessBehaviour");
        var fldIndexSelected = GetArrayField<int>(comp, "indexSelected"); // this contains both the coordinates and the solution

        yield return WaitForActivate;

        var indexSelected = fldIndexSelected.Get(expectedLength: 7, validator: b => b / 10 < 0 || b / 10 >= 6 || b % 10 < 0 || b % 10 >= 6 ? "unexpected value" : null);

        yield return WaitForSolve;

        for (var i = 0; i < 6; i++)
        {
            var coordStr = "" + ((char)(indexSelected[i] / 10 + 'a')) + (indexSelected[i] % 10 + 1);
            yield return new Discriminator(SChess.DCoordinate, $"coord-{i}", args: [coordStr, Ordinal(i + 1)]);
            yield return question(SChess.QCoordinate, args: [Ordinal(i + 1)])
                .AvoidDiscriminators($"coord-{i}")
                .Answers(coordStr);
        }
    }
}