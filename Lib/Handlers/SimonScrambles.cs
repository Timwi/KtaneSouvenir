using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonScrambles
{
    [SouvenirQuestion("What color flashed {1} in {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Yellow", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonScrambles", "Simon Scrambles", typeof(SSimonScrambles), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessSimonScrambles(ModuleData module)
    {
        var comp = GetComponent(module, "simonScramblesScript");
        yield return WaitForSolve;

        var sequence = GetArrayField<int>(comp, "sequence").Get(expectedLength: 10);
        var colors = GetArrayField<string>(comp, "colorNames").Get(expectedLength: 4);

        if (sequence[9] < 0 || sequence[9] >= colors.Length)
            throw new AbandonModuleException($"‘sequence[9]’ points to illegal color: {sequence[9]} (expected 0-3).");

        for (var ix = 0; ix < sequence.Length; ix++)
            yield return question(SSimonScrambles.Colors, args: [Ordinal(ix + 1)]).Answers(colors[sequence[ix]]);
    }
}