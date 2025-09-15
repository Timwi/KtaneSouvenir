using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SJuxtacoloredSquares
{
    [SouvenirQuestion("What was the color of this square in {0}?", ThreeColumns6Answers, "Red", "Blue", "Yellow", "Green", "Magenta", "Orange", "Cyan", "Purple", "Chestnut", "Brown", "Mauve", "Azure", "Jade", "Forest", "Gray", "Black", TranslateAnswers = true, UsesQuestionSprite = true)]
    ColorsByPosition,

    [SouvenirQuestion("Which square was {1} in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = ["red", "blue", "yellow", "green", "magenta", "orange", "cyan", "purple", "chestnut", "brown", "mauve", "azure", "jade", "forest", "gray", "black"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Grid(4, 4)]
    PositionsByColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("JuxtacoloredSquaresModule", "Juxtacolored Squares", typeof(SJuxtacoloredSquares), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessJuxtacoloredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "JuxtacoloredSquaresModule");

        var fldColors = GetField<Array>(comp, "_colors");
        var colors = fldColors.Get(); // Prevent compiler from complaining about this being an unassigned local variable.
        var needToUpdate = true;
        module.Module.OnStrike += () => { needToUpdate = true; return false; };

        while (module.Unsolved)
        {
            if (needToUpdate)
            {
                yield return null; // Wait for fldColors to update.
                colors = fldColors.Get(arr => arr.Length != 16 ? "expected length 16" : null).Clone() as Array;
                needToUpdate = false;
            }
            yield return null; // Do not wait .1 seconds to make sure we get get the colors before any squares are pressed.
        }
        yield return WaitForSolve;
        for (var pos = 0; pos < 16; pos++)
        {
            var colorName = colors.GetValue(pos).ToString();
            if (colorName == "DarkBlue")
                colorName = "Blue";
            var coordinate = new Coord(4, 4, pos);
            yield return question(SJuxtacoloredSquares.ColorsByPosition, questionSprite: Sprites.GenerateGridSprite(coordinate)).Answers(colorName);
            yield return question(SJuxtacoloredSquares.PositionsByColor, args: [colorName.ToLowerInvariant()]).Answers(coordinate);
        }
    }
}