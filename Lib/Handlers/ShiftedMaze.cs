using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SShiftedMaze
{
    [SouvenirQuestion("What color was the {1} marker in {0}?", TwoColumns4Answers, "White", "Blue", "Yellow", "Magenta", "Green", TranslateAnswers = true, Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("shiftedMaze", "Shifted Maze", typeof(SShiftedMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessShiftedMaze(ModuleData module)
    {
        var comp = GetComponent(module, "shiftedMazeScript");
        var expectedCBTexts = new[] { "W", "B", "Y", "M", "G" };
        var colorNames = new[] { "White", "Blue", "Yellow", "Magenta", "Green" };
        var cornerNames = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };

        var colorblindTexts = GetArrayField<TextMesh>(comp, "colorblindTexts", isPublic: true).Get(expectedLength: 4).Select(c => c.text).ToArray();
        var invalid = colorblindTexts.IndexOf(c => !expectedCBTexts.Contains(c));
        yield return invalid != -1
            ? throw new AbandonModuleException($"Found unexpected color text: “{colorblindTexts[invalid]}”.")
            : (YieldInstruction) WaitForSolve;
        addQuestions(module, Enumerable.Range(0, 4).Select(corner => makeQuestion(Question.ShiftedMazeColors, module,
            formatArgs: new[] { cornerNames[corner] }, correctAnswers: new[] { colorNames[Array.IndexOf(expectedCBTexts, colorblindTexts[corner])] })));
    }
}