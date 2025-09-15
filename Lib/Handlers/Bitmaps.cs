using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBitmaps
{
    [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", ThreeColumns6Answers, TranslateArguments = [true, true], Arguments = ["white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right"], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 16)]
    Question
}

public partial class SouvenirModule
{
    [SouvenirHandler("BitmapsModule", "Bitmaps", typeof(SBitmaps), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessBitmaps(ModuleData module)
    {
        var comp = GetComponent(module, "BitmapsModule");
        yield return WaitForSolve;

        var bitmap = GetArrayField<bool[]>(comp, "_bitmap").Get(expectedLength: 8, validator: arr => arr.Length != 8 ? "expected length 8" : null);
        var qCounts = new int[4];
        for (var x = 0; x < 8; x++)
            for (var y = 0; y < 8; y++)
                if (bitmap[x][y])
                    qCounts[(y / 4) * 2 + (x / 4)]++;

        var preferredWrongAnswers = qCounts.SelectMany(i => new[] { i, 16 - i }).Distinct().Select(i => i.ToString()).ToArray();

        addQuestions(module,
            makeQuestion(Question.Bitmaps, module, formatArgs: new[] { "white", "top left" }, correctAnswers: new[] { qCounts[0].ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, module, formatArgs: new[] { "white", "top right" }, correctAnswers: new[] { qCounts[1].ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, module, formatArgs: new[] { "white", "bottom left" }, correctAnswers: new[] { qCounts[2].ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, module, formatArgs: new[] { "white", "bottom right" }, correctAnswers: new[] { qCounts[3].ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, module, formatArgs: new[] { "black", "top left" }, correctAnswers: new[] { (16 - qCounts[0]).ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, module, formatArgs: new[] { "black", "top right" }, correctAnswers: new[] { (16 - qCounts[1]).ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, module, formatArgs: new[] { "black", "bottom left" }, correctAnswers: new[] { (16 - qCounts[2]).ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, module, formatArgs: new[] { "black", "bottom right" }, correctAnswers: new[] { (16 - qCounts[3]).ToString() }, preferredWrongAnswers: preferredWrongAnswers));
    }
}
