using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotColourFlash
{
    [SouvenirQuestion("What was {1} in the displayed word sequence in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "White", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    InitialWord,

    [SouvenirQuestion("What was {1} in the displayed colour sequence in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "White", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    InitialColour
}

public partial class SouvenirModule
{
    [SouvenirHandler("notColourFlash", "Not Colour Flash", typeof(SNotColourFlash), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessNotColourFlash(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "NCFScript");
        var seq = GetArrayField<int[]>(comp, "seq").Get(expectedLength: 4, validator: v => v is not { Length: 6 } ? "Expected length 6" : v.Any(i => i is < 0 or > 5) ? "Expected within range [0, 5]" : null);
        for (var i = 0; i < seq[0].Length; i++)
            yield return question(SNotColourFlash.InitialWord, args: [Ordinal(i + 1)]).Answers(SNotColourFlash.InitialWord.GetAnswers()[seq[0][i]]);
        for (var i = 0; i < seq[1].Length; i++)
            yield return question(SNotColourFlash.InitialColour, args: [Ordinal(i + 1)]).Answers(SNotColourFlash.InitialColour.GetAnswers()[seq[1][i]]);
    }
}
