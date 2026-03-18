using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBitmaps
{
    [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", ThreeColumns6Answers, TranslateArguments = [true, true], Arguments = ["white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right"], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 16)]
    Question,

    [SouvenirDiscriminator("the Bitmap where the {2} pixel count in the {1} quadrant was {0}", Arguments = ["1", "top left", "white", "2", "top right", "white", "3", "bottom left", "white", "4", "bottom right", "white", "5", "top left", "black", "6", "top right", "black", "7", "bottom left", "black", "8", "bottom right", "black"], ArgumentGroupSize = 3, TranslateArguments = [false, true, true])]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("BitmapsModule", "Bitmaps", typeof(SBitmaps), "Timwi")]
    [SouvenirManualQuestion("How many pixels were black/white in each quadrant?")]
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

        // Questions
        yield return question(SBitmaps.Question, args: ["white", "top left"]).AvoidDiscriminators("q1w", "q1b").Answers(qCounts[0].ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["white", "top right"]).AvoidDiscriminators("q2w", "q2b").Answers(qCounts[1].ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["white", "bottom left"]).AvoidDiscriminators("q3w", "q3b").Answers(qCounts[2].ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["white", "bottom right"]).AvoidDiscriminators("q4w", "q4b").Answers(qCounts[3].ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["black", "top left"]).AvoidDiscriminators("q1w", "q1b").Answers((16 - qCounts[0]).ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["black", "top right"]).AvoidDiscriminators("q2w", "q2b").Answers((16 - qCounts[1]).ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["black", "bottom left"]).AvoidDiscriminators("q3w", "q3b").Answers((16 - qCounts[2]).ToString(), preferredWrong: preferredWrongAnswers);
        yield return question(SBitmaps.Question, args: ["black", "bottom right"]).AvoidDiscriminators("q4w", "q4b").Answers((16 - qCounts[3]).ToString(), preferredWrong: preferredWrongAnswers);

        // Discriminators
        yield return new Discriminator(SBitmaps.Discriminator, "q1w", qCounts[0], args: [qCounts[0].ToString(), "top left", "white"]);
        yield return new Discriminator(SBitmaps.Discriminator, "q2w", qCounts[1], args: [qCounts[1].ToString(), "top right", "white"]);
        yield return new Discriminator(SBitmaps.Discriminator, "q3w", qCounts[2], args: [qCounts[2].ToString(), "bottom left", "white"]);
        yield return new Discriminator(SBitmaps.Discriminator, "q4w", qCounts[3], args: [qCounts[3].ToString(), "bottom right", "white"]);
        yield return new Discriminator(SBitmaps.Discriminator, "q1b", 16 - qCounts[0], args: [(16 - qCounts[0]).ToString(), "top left", "black"]);
        yield return new Discriminator(SBitmaps.Discriminator, "q2b", 16 - qCounts[1], args: [(16 - qCounts[1]).ToString(), "top right", "black"]);
        yield return new Discriminator(SBitmaps.Discriminator, "q3b", 16 - qCounts[2], args: [(16 - qCounts[2]).ToString(), "bottom left", "black"]);
        yield return new Discriminator(SBitmaps.Discriminator, "q4b", 16 - qCounts[3], args: [(16 - qCounts[3]).ToString(), "bottom right", "black"]);
    }
}
