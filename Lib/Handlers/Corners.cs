using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCorners
{
    [SouvenirQuestion("What was the color of the {1} corner in {0}?", TwoColumns4Answers, "red", "green", "blue", "yellow", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["top-left", "top-right", "bottom-right", "bottom-left"], ArgumentGroupSize = 1)]
    Colors,

    [SouvenirQuestion("How many corners in {0} were {1}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", TranslateArguments = [true], Arguments = ["red", "green", "blue", "yellow"], ArgumentGroupSize = 1)]
    ColorCount
}

public partial class SouvenirModule
{
    [SouvenirHandler("CornersModule", "Corners", typeof(SCorners), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessCorners(ModuleData module)
    {
        var comp = GetComponent(module, "CornersModule");
        yield return WaitForSolve;

        var colorNames = new[] { "red", "green", "blue", "yellow" };
        var cornerNames = new[] { "top-left", "top-right", "bottom-right", "bottom-left" };

        var clampColors = GetArrayField<int>(comp, "_clampColors").Get(expectedLength: 4, validator: v => v < 0 || v >= colorNames.Length ? $"expected 0–{colorNames.Length - 1}" : null);
        var qs = new List<QandA>();
        qs.AddRange(cornerNames.Select((corner, cIx) => makeQuestion(Question.CornersColors, module, formatArgs: new[] { corner }, correctAnswers: new[] { colorNames[clampColors[cIx]] })));
        qs.AddRange(colorNames.Select((col, colIx) => makeQuestion(Question.CornersColorCount, module, formatArgs: new[] { col }, correctAnswers: new[] { clampColors.Count(cc => cc == colIx).ToString() })));
        addQuestions(module, qs);
    }
}