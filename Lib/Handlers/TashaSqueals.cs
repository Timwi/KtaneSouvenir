using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STashaSqueals
{
    [SouvenirQuestion("What was the {1} flashed color in {0}?", TwoColumns4Answers, "Pink", "Green", "Yellow", "Blue", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("tashaSqueals", "Tasha Squeals", typeof(STashaSqueals), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessTashaSqueals(ModuleData module)
    {
        var comp = GetComponent(module, "tashaSquealsScript");

        var colors = GetStaticField<string[]>(comp.GetType(), "colorNames").Get(ar => ar.Length != 4 ? "expected length 4" : null).ToArray();
        var sequence = GetArrayField<int>(comp, "flashing").Get(expectedLength: 5, validator: val => val < 0 || val >= colors.Length ? $"expected range 0–{colors.Length - 1}" : null);

        for (var i = 0; i < colors.Length; i++)
            colors[i] = char.ToUpperInvariant(colors[i][0]) + colors[i].Substring(1);

        yield return WaitForSolve;

        for (var i = 0; i < 5; i++)
            yield return question(STashaSqueals.Colors, args: [Ordinal(i + 1)]).Answers(colors[sequence[i]]);
    }
}
