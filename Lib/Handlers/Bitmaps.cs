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

        yield return question(SBitmaps.Question, args: ["white", "top left"]).Answers(qCounts[0].ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["white", "top right"]).Answers(qCounts[1].ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["white", "bottom left"]).Answers(qCounts[2].ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["white", "bottom right"]).Answers(qCounts[3].ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["black", "top left"]).Answers((16 - qCounts[0]).ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["black", "top right"]).Answers((16 - qCounts[1]).ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["black", "bottom left"]).Answers((16 - qCounts[2]).ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["black", "bottom right"]).Answers((16 - qCounts[3]).ToString(), preferredWrong: preferredWrongAnswers);
    }
}
