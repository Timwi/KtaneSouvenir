using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonStates
{
    [SouvenirQuestion("Which {1} in the {2} stage in {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", "Red, Yellow", "Red, Green", "Red, Blue", "Yellow, Green", "Yellow, Blue", "Green, Blue", "all 4", "none", TranslateAnswers = true, TranslateArguments = [true, false], Arguments = ["color(s) flashed", QandA.Ordinal, "color(s) didn’t flash", QandA.Ordinal], ArgumentGroupSize = 2)]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("SimonV2", "Simon States", typeof(SSimonStates), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimonStates(ModuleData module)
    {
        var comp = GetComponent(module, "AdvancedSimon");
        var fldPuzzleDisplay = GetArrayField<bool[]>(comp, "PuzzleDisplay");

        bool[][] puzzleDisplay;
        while ((puzzleDisplay = fldPuzzleDisplay.Get(nullAllowed: true)) == null)
            yield return new WaitForSeconds(.1f);

        if (puzzleDisplay.Length != 4 || puzzleDisplay.Any(arr => arr.Length != 4))
            throw new AbandonModuleException($"‘PuzzleDisplay’ has an unexpected length or value: [{puzzleDisplay.Select(arr => arr == null ? "null" : "[" + arr.JoinString(", ") + "]").JoinString("; ")}]");

        var colorNames = new[] { "Red", "Yellow", "Green", "Blue" };

        yield return WaitForSolve;
        // Consistency check
        if (fldPuzzleDisplay.Get(nullAllowed: true) != null)
            throw new AbandonModuleException($"‘PuzzleDisplay’ was expected to be null when the module solved, but wasn’t.");
        for (var i = 0; i < 3; i++)     // Do not ask about fourth stage because it can sometimes be solved without waiting for the flashes
        {
            var c = puzzleDisplay[i].Count(b => b);
            if (c != 3)
                yield return question(SSimonStates.Display, args: ["color(s) flashed", Ordinal(i + 1)]).Answers(c == 4 ? "all 4" : puzzleDisplay[i].Select((v, j) => v ? colorNames[j] : null).Where(x => x != null).JoinString(", "));
            if (c != 1)
                yield return question(SSimonStates.Display, args: ["color(s) didn’t flash", Ordinal(i + 1)]).Answers(c == 4 ? "none" : puzzleDisplay[i].Select((v, j) => v ? null : colorNames[j]).Where(x => x != null).JoinString(", "));
        }
    }
}