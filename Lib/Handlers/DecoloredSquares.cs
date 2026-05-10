using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDecoloredSquares
{
    [Question("What was the starting {1} defining color in {0}?", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["column", "row"], ArgumentGroupSize = 1)]
    QStartingPos,

    [Discriminator("the Decolored Squares where was the starting {1} defining color was {0}", Arguments = ["White", "column", "Red", "column", "Blue", "column", "Green", "row", "Yellow", "row", "Magenta", "row"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    DStartingPos
}

public partial class SouvenirModule
{
    [Handler("DecoloredSquaresModule", "Decolored Squares", typeof(SDecoloredSquares), "luisdiogo98")]
    [ManualQuestion("What were the colors defining the starting row and column?")]
    private IEnumerator<SouvenirInstruction> ProcessDecoloredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "DecoloredSquaresModule");
        yield return WaitForSolve;

        var colColor = GetField<string>(comp, "_color1").Get();
        var rowColor = GetField<string>(comp, "_color2").Get();

        yield return new Discriminator(SDecoloredSquares.DStartingPos, "col", colColor, [colColor, "column"]);
        yield return question(SDecoloredSquares.QStartingPos, args: ["column"]).AvoidDiscriminators("col").Answers(colColor);
        yield return new Discriminator(SDecoloredSquares.DStartingPos, "row", rowColor, [rowColor, "row"]);
        yield return question(SDecoloredSquares.QStartingPos, args: ["row"]).AvoidDiscriminators("row").Answers(rowColor);
    }
}
