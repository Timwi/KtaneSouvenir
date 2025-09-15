using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonSounds
{
    [SouvenirQuestion("Which sample button sounded {1} in the final sequence in {0}?", TwoColumns4Answers, "red", "blue", "yellow", "green", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    FlashingColors
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonSounds", "Simon Sounds", typeof(SSimonSounds), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSounds(ModuleData module)
    {
        var comp = GetComponent(module, "simonSoundsScript");
        yield return WaitForSolve;

        var colorNames = new[] { "red", "blue", "yellow", "green" };
        var flashed = GetArrayField<List<int>>(comp, "stage").Get(ar => ar == null ? "contains null" : ar.Any(list => list.Last() < 0 || list.Last() >= colorNames.Length) ? "expected last value in range 0–3" : null);
        for (var stage = 0; stage < flashed.Length; stage++)
            yield return question(SSimonSounds.FlashingColors, args: [Ordinal(stage + 1)]).Answers(colorNames[flashed[stage].Last()]);
    }
}