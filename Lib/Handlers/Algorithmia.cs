using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SAlgorithmia
{
    [SouvenirQuestion("Which position was the {1} position in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = ["starting", "goal"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Grid(4, 4)]
    QPositions,

    [SouvenirQuestion("What was the color of the colored bulb in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Yellow", "Magenta")]
    QColor,

    [SouvenirQuestion("Which number was present in the seed in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99, "00")]
    QSeed,

    [SouvenirDiscriminator("the Algorithmia where this was the {0} position", UsesQuestionSprite = true, Arguments = ["starting", "goal"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DPositions,

    [SouvenirDiscriminator("the Algorithmia whose colored bulb was {0}", Arguments = ["red", "green", "blue", "cyan", "yellow", "magenta"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DColor,

    [SouvenirDiscriminator("the Algorithmia that had a {0} in the seed", Arguments = ["01", "02", "03", "10", "37", "42", "47", "55", "78"], ArgumentGroupSize = 1)]
    DSeed
}

public partial class SouvenirModule
{
    [SouvenirHandler("algorithmia", "Algorithmia", typeof(SAlgorithmia), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessAlgorithmia(ModuleData module)
    {
        var comp = GetComponent(module, "AlgorithmiaScript");

        var startingPos = GetIntField(comp, "currentPos").Get(min: 0, max: 15);
        var goalPos = GetIntField(comp, "goalPos").Get(min: 0, max: 15);

        yield return WaitForSolve;

        var fldColor = GetField<object>(comp, "mazeAlg");
        var color = fldColor.Get(col => (int) col is < 0 or >= 6 ? "expected in range 0-5" : null).ToString();

        var fldSeed = GetField<object>(comp, "seed");
        var prpSeedValues = GetProperty<int[]>(fldSeed.Get(), "values", true);
        var seedVals = prpSeedValues.Get(arr =>
            arr.Length != 10 ? "expected length 10" :
            arr.Any(val => val is < 0 or > 99) ? "expected in range 0-99" :
            null);

        yield return question(SAlgorithmia.QPositions, args: ["starting"]).AvoidDiscriminators(SAlgorithmia.DPositions)
            .Answers(new Coord(4, 4, startingPos), preferredWrong: [new Coord(4, 4, goalPos)]);
        yield return question(SAlgorithmia.QPositions, args: ["goal"]).AvoidDiscriminators(SAlgorithmia.DPositions)
            .Answers(new Coord(4, 4, goalPos), preferredWrong: [new Coord(4, 4, startingPos)]);
        yield return question(SAlgorithmia.QColor).AvoidDiscriminators(SAlgorithmia.DColor).Answers(color);
        yield return question(SAlgorithmia.QSeed).AvoidDiscriminators(SAlgorithmia.DSeed).Answers(seedVals.Select(x => x.ToString("00")).ToArray());

        yield return new Discriminator(SAlgorithmia.DPositions, "start", startingPos, args: ["starting"], questionSprite: Sprites.GenerateGridSprite(new Coord(4, 4, startingPos)));
        yield return new Discriminator(SAlgorithmia.DPositions, "goal", goalPos, args: ["goal"], questionSprite: Sprites.GenerateGridSprite(new Coord(4, 4, goalPos)));
        yield return new Discriminator(SAlgorithmia.DColor, "color", color, args: [color.ToLowerInvariant()]);

        foreach (var val in seedVals.Distinct())
            yield return new Discriminator(SAlgorithmia.DSeed, $"seed-has-{val}", args: [val.ToString("00")]);
    }
}
