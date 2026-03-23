using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonStores
{
    [SouvenirQuestion("Which color {2} {1} in the final sequence of {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", Arguments = [QandA.Ordinal, "flashed", "flashed ({3})", "flashed ({4})", QandA.Ordinal, "was among the colors that flashed", "was among the colors that flashed ({3})", "was among the colors that flashed ({4})"], ArgumentGroupSize = 4, TranslateAnswers = true, TranslateArguments = [false, true, true, true], ReferenceDocumentation = true)]
    QFlashes,

    [SouvenirDiscriminator("the Simon Stores where {0} {2} {1} in the final sequence", Arguments = ["red", QandA.Ordinal, "flashed", "flashed ({3})", "flashed ({4})", "green", QandA.Ordinal, "flashed", "flashed ({3})", "flashed ({4})", "blue", QandA.Ordinal, "flashed", "flashed ({3})", "flashed ({4})", "cyan", QandA.Ordinal, "was among the colors that flashed", "was among the colors that flashed ({3})", "was among the colors that flashed ({4})", "magenta", QandA.Ordinal, "was among the colors that flashed", "was among the colors that flashed ({3})", "was among the colors that flashed ({4})", "yellow", QandA.Ordinal, "was among the colors that flashed", "was among the colors that flashed ({3})", "was among the colors that flashed ({4})"], ArgumentGroupSize = 5, TranslateArguments = [true, false, true, true, true], ReferenceDocumentation = true)]
    DFlashes
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
            var flashStr = flashSequences[stage].Length == 1 ? "flashed" : "was among the colors that flashed";
            foreach (var flash in flashSequences[stage])
                yield return new Discriminator(SSimonStores.DFlashes, $"flash-{stage}-{flash}", colorNames[flash], args: [colorNames[flash], Ordinal(stage + 1), flashStr, $"{flashStr} ({{3}})", $"{flashStr} ({{4}})"], avoidAnswers: [colorNames[flash]]);
            yield return question(SSimonStores.QFlashes, args: [Ordinal(stage + 1), flashStr, $"{flashStr} ({{3}})", $"{flashStr} ({{4}})"]).Answers(flashSequences[stage].Select(ch => colorNames[ch]).ToArray());
        }
    }
}
