using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SShiftedMaze
{
    [Question("What color was the {1} marker in {0}?", TwoColumns4Answers, "White", "Blue", "Yellow", "Magenta", "Green", TranslateAnswers = true, Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Colors
}

public partial class SouvenirModule
{
    [Handler("shiftedMaze", "Shifted Maze", typeof(SShiftedMaze), "Timwi")]
    [ManualQuestion("What were the colors of the markers?")]
    private IEnumerator<SouvenirInstruction> ProcessShiftedMaze(ModuleData module)
    {
        var comp = GetComponent(module, "shiftedMazeScript");
        var expectedCBTexts = new[] { "W", "B", "Y", "M", "G" };
        var colorNames = new[] { "White", "Blue", "Yellow", "Magenta", "Green" };
        var cornerNames = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };

        var colorblindTexts = GetArrayField<TextMesh>(comp, "colorblindTexts", isPublic: true).Get(expectedLength: 4).Select(c => c.text).ToArray();
        var invalid = colorblindTexts.IndexOf(c => !expectedCBTexts.Contains(c));
        if (invalid != -1)
            throw new AbandonModuleException($"Found unexpected color text: “{colorblindTexts[invalid]}”.");

        yield return WaitForSolve;
        for (var corner = 0; corner < 4; corner++)
            yield return question(SShiftedMaze.Colors, args: [cornerNames[corner]]).Answers(colorNames[Array.IndexOf(expectedCBTexts, colorblindTexts[corner])]);
    }
}
