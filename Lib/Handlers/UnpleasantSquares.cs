using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUnpleasantSquares
{
    [SouvenirQuestion("What was the color of this square in {0}?", TwoColumns4Answers, "Red", "Yellow", "Jade", "Azure", "Violet", TranslateAnswers = true, UsesQuestionSprite = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("unpleasantSquares", "Unpleasant Squares", typeof(SUnpleasantSquares), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessUnpleasantSquares(ModuleData module)
    {
        var comp = GetComponent(module, "UnSqScript");
        yield return WaitForSolve;
        var subGrid = GetField<int[,]>(comp, "subgrid").Get();
        var colorNames = new string[] { "Red", "Yellow", "Jade", "Azure", "Violet", };
        for (var x = 0; x < 5; x++)
            for (var y = 0; y < 5; y++)
            {
                var p = x * 5 + y;
                if (p == 12)
                    continue;
                var coord = new Coord(5, 5, p);
                yield return question(SUnpleasantSquares.Color, questionSprite: Sprites.GenerateGridSprite(coord)).Answers(colorNames[subGrid[x, y]]);
            }
    }
}