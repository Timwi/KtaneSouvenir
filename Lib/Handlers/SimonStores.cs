using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonStores
{
    [SouvenirQuestion("Which color flashed {1} in the final sequence of {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    QSingleColor,

    [SouvenirQuestion("Which color was among the colors that flashed {1} in the final sequence of {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    QMultipleColors,

    [SouvenirDiscriminator("the Simon Stores where {0} flashed {1} in the final sequence", Arguments = ["red", QandA.Ordinal, "green", QandA.Ordinal, "blue", QandA.Ordinal, "cyan", QandA.Ordinal, "magenta", QandA.Ordinal, "yellow", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DSingleColor,

    [SouvenirDiscriminator("the Simon Stores where {0} was among the colors that flashed {1} in the final sequence", Arguments = ["red", QandA.Ordinal, "green", QandA.Ordinal, "blue", QandA.Ordinal, "cyan", QandA.Ordinal, "magenta", QandA.Ordinal, "yellow", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DMultipleColors
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonStores", "Simon Stores", typeof(SSimonStores), "kavinkul")]
    [SouvenirManualQuestion("Which colors flashed in the final sequence?")]
    private IEnumerator<SouvenirInstruction> ProcessSimonStores(ModuleData module)
    {
        var comp = GetComponent(module, "SimonStoresScript");
        yield return WaitForSolve;

        var flashSequences = GetListField<string>(comp, "flashingColours").Get();
        var colors = "RGBCMY";

        foreach (var flash in flashSequences)
        {
            var set = new HashSet<char>();
            if (flash.Length < 1 || flash.Length > 3 || flash.Any(color => !set.Add(color) || !colors.Contains(color)))
                throw new AbandonModuleException($"'flashingColours' contains value with duplicated colors, invalid color, or unexpected length (expected: 1-3): [flash: {flash}, length: {flash.Length}]");
        }

        var colorNames = new Dictionary<char, string> {
            { 'R', "Red" },
            { 'G', "Green" },
            { 'B', "Blue" },
            { 'C', "Cyan" },
            { 'M', "Magenta" },
            { 'Y', "Yellow" }
        };
        for (var stage = 0; stage < 5; stage++)
        {
            var single = flashSequences[stage].Length == 1;
            foreach (var flash in flashSequences[stage])
                yield return new Discriminator(single ? SSimonStores.DSingleColor : SSimonStores.DMultipleColors, $"flash-{stage}-{flash}", colorNames[flash], args: [colorNames[flash], Ordinal(stage + 1)], avoidAnswers: [colorNames[flash]]);
            yield return question(single ? SSimonStores.QSingleColor : SSimonStores.QMultipleColors, args: [Ordinal(stage + 1)]).Answers(flashSequences[stage].Select(ch => colorNames[ch]).ToArray());
        }
    }
}
