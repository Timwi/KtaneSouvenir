using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDecoloredSquares
{
    [Question("What was the starting {1} defining color in {0}?", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["column", "row"], ArgumentGroupSize = 1)]
    StartingPos
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

        yield return question(SDecoloredSquares.StartingPos, args: ["column"]).Answers(colColor);
        yield return question(SDecoloredSquares.StartingPos, args: ["row"]).Answers(rowColor);
    }
}