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
        addQuestions(module,
            seq[0].Select((c, i) => makeQuestion(Question.NotColourFlashInitialWord, module, correctAnswers: new[] { Question.NotColourFlashInitialWord.GetAnswers()[c] }, formatArgs: new[] { Ordinal(i + 1) }))
            .Concat(seq[1].Select((c, i) => makeQuestion(Question.NotColourFlashInitialColour, module, correctAnswers: new[] { Question.NotColourFlashInitialColour.GetAnswers()[c] }, formatArgs: new[] { Ordinal(i + 1) }))));
    }
}