using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SiPhone
{
    [SouvenirQuestion("What was the {1} PIN digit in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Digits
}

public partial class SouvenirModule
{
    [SouvenirHandler("iPhone", "iPhone", typeof(SiPhone), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessiPhone(ModuleData module)
    {
        var comp = GetComponent(module, "iPhoneScript");
        var digits = GetListField<string>(comp, "pinDigits", isPublic: true).Get(expectedLength: 4);

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.iPhoneDigits, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { digits[0] }, preferredWrongAnswers: new[] { digits[1], digits[2], digits[3] }),
            makeQuestion(Question.iPhoneDigits, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { digits[1] }, preferredWrongAnswers: new[] { digits[0], digits[2], digits[3] }),
            makeQuestion(Question.iPhoneDigits, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { digits[2] }, preferredWrongAnswers: new[] { digits[1], digits[0], digits[3] }),
            makeQuestion(Question.iPhoneDigits, module, formatArgs: new[] { Ordinal(4) }, correctAnswers: new[] { digits[3] }, preferredWrongAnswers: new[] { digits[1], digits[2], digits[0] }));
    }
}