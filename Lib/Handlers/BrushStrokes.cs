using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBrushStrokes
{
    [SouvenirQuestion("What was the color of the middle contact point in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Cyan", "Sky", "Blue", "Purple", "Magenta", "Brown", "White", "Gray", "Black", "Pink", TranslateAnswers = true)]
    MiddleColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("brushStrokes", "Brush Strokes", typeof(SBrushStrokes), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessBrushStrokes(ModuleData module)
    {
        var comp = GetComponent(module, "BrushStrokesScript");
        yield return WaitForSolve;

        var colorNames = GetStaticField<string[]>(comp.GetType(), "colorNames").Get();
        var colors = GetArrayField<int>(comp, "colors").Get(expectedLength: 9);

        if (colors[4] < 0 || colors[4] >= colorNames.Length)
            throw new AbandonModuleException($"‘colors[4]’ pointed to illegal color: {colors[4]}.");

        addQuestion(module, Question.BrushStrokesMiddleColor, correctAnswers: new[] { char.ToUpperInvariant(colorNames[colors[4]][0]) + colorNames[colors[4]].Substring(1) });
    }
}