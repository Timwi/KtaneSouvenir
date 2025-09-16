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

        yield return question(SiPhone.Digits, args: [Ordinal(1)]).Answers(digits[0], preferredWrong: [digits[1], digits[2], digits[3]]);
        yield return question(SiPhone.Digits, args: [Ordinal(2)]).Answers(digits[1], preferredWrong: [digits[0], digits[2], digits[3]]);
        yield return question(SiPhone.Digits, args: [Ordinal(3)]).Answers(digits[2], preferredWrong: [digits[1], digits[0], digits[3]]);
        yield return question(SiPhone.Digits, args: [Ordinal(4)]).Answers(digits[3], preferredWrong: [digits[1], digits[2], digits[0]]);
    }
}