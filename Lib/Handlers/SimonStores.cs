using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonStores
{
    [SouvenirQuestion("Which color {1} {2} in the final sequence of {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", TranslateAnswers = true, TranslateArguments = [true, false], Arguments = ["flashed", QandA.Ordinal, "was among the colors that flashed", QandA.Ordinal], ArgumentGroupSize = 2)]
    QFlashes,

    [SouvenirDiscriminator("the Simon Stores where {0} {1} {2} in the final sequence", Arguments = ["red", "flashed", QandA.Ordinal, "green", "flashed", QandA.Ordinal, "blue", "flashed", QandA.Ordinal, "cyan", "was among the colors that flashed", QandA.Ordinal, "magenta", "was among the colors that flashed", QandA.Ordinal, "yellow", "was among the colors that flashed", QandA.Ordinal], ArgumentGroupSize = 3, TranslateArguments = [true, true, false])]
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
                yield return new Discriminator(SSimonStores.DFlashes, $"flash-{stage}-{flash}", colorNames[flash], args: [colorNames[flash], flashStr, Ordinal(stage+1)], avoidAnswers: [colorNames[flash]]);
            yield return question(SSimonStores.QFlashes, args: [flashStr, Ordinal(stage + 1)]).Answers(flashSequences[stage].Select(ch => colorNames[ch]).ToArray());
        }
    }
}