using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SStability
{
    [SouvenirQuestion("What was the color of the {1} lit LED in {0}?", TwoColumns4Answers, "Red", "Yellow", "Blue", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    LedColors,

    [SouvenirQuestion("What was the identification number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9999, "0000")]
    IdNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("stabilityModule", "Stability", typeof(SStability), "NickLatkovich")]
    private IEnumerator<SouvenirInstruction> ProcessStability(ModuleData module)
    {
        var colorNames = new[] { "Red", "Yellow", "Blue" };

        var comp = GetComponent(module, "StabilityScript");
        yield return WaitForSolve;

        var litLedStates = GetArrayField<int>(comp, "ledStates").Get().Where(l => l != 5).ToArray();
        for (var i = 0; i < litLedStates.Length; i++)
            yield return question(SStability.LedColors, args: [Ordinal(i + 1)]).Answers(colorNames[litLedStates[i]]);

        if (litLedStates.Length > 3)
            yield return question(SStability.IdNumber).Answers(GetField<string>(comp, "idNumber").Get());
    }
}