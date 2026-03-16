using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBlindMaze
{
    [SouvenirQuestion("What color was the {1} button in {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Gray", "Yellow", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["north", "east", "west", "south"], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("BlindMaze", "Blind Maze", typeof(SBlindMaze), "Timwi")]
    [SouvenirManualQuestion("What colors were the buttons?")]
    private IEnumerator<SouvenirInstruction> ProcessBlindMaze(ModuleData module)
    {
        var comp = GetComponent(module, "BlindMaze");
        yield return WaitForSolve;

        var buttonColors = GetArrayField<int>(comp, "buttonColors").Get(expectedLength: 4, validator: bc => bc is < 0 or > 4 ? "expected 0–4" : null);

        var colorNames = new[] { "Red", "Green", "Blue", "Gray", "Yellow" };
        var buttonNames = new[] { "north", "east", "south", "west" };

        for (var ix = 0; ix < buttonColors.Length; ix++)
            yield return question(SBlindMaze.Colors, args: [buttonNames[ix]]).Answers(colorNames[buttonColors[ix]]);
    }
}
