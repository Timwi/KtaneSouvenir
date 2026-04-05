using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SOffWhiteCipher
{
    [Question("What was on the top display in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    TopDisplay,

    [Question("What was on the middle display in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(4, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    MiddleDisplay,

    [Question("What was on the bottom display in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(3, "012")]
    BottomDisplay
}

public partial class SouvenirModule
{
    [Handler("offWhiteCipher", "Off-White Cipher", typeof(SOffWhiteCipher), "KiloBites")]
    [ManualQuestion("What was on each display?")]
    private IEnumerator<SouvenirInstruction> ProcessOffWhiteCipher(ModuleData module)
    {
        var comp = GetComponent(module, "OffWhiteCipherScript");
        yield return WaitForSolve;

        var displayedInfo = GetArrayField<string>(comp, "displayedWords").Get(expectedLength: 3);

        yield return question(SOffWhiteCipher.TopDisplay).Answers(displayedInfo[0]);
        yield return question(SOffWhiteCipher.MiddleDisplay).Answers(displayedInfo[1]);
        yield return question(SOffWhiteCipher.BottomDisplay).Answers(displayedInfo[2]);
    }
}
