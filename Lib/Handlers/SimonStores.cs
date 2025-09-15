using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonStores
{
    [SouvenirQuestion("Which color {1} {2} in the final sequence of {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", TranslateAnswers = true, TranslateArguments = [true, false], Arguments = ["flashed", QandA.Ordinal, "was among the colors flashed", QandA.Ordinal], ArgumentGroupSize = 2)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonStores", "Simon Stores", typeof(SSimonStores), "kavinkul")]
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
        for (var i = 0; i < 5; i++)
            yield return question(SSimonStores.Colors, args: [flashSequences[i].Length == 1 ? "flashed" : "was among the colors flashed", Ordinal(i + 1)]).Answers(flashSequences[i].Select(ch => colorNames[ch]).ToArray());
    }
}