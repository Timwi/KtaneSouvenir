using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SOffWhiteCipher
{
    [Question("What was on the top display in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    QTopDisplay,

    [Question("What was on the middle display in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(4, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    QMiddleDisplay,

    [Question("What was on the bottom display in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(3, "012")]
    QBottomDisplay,

    [Discriminator("the Off-White Cipher that had {0} on the top display", Arguments = ["AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS"], ArgumentGroupSize = 1)]
    DTopDisplay,

    [Discriminator("the Off-White Cipher that had {0} on the middle display", Arguments = ["ABCD", "EFGH", "IJKL", "MNOP", "QRST", "UVWX"], ArgumentGroupSize = 1)]
    DMiddleDisplay,

    [Discriminator("the Off-White Cipher that had {0} on the bottom display", Arguments = ["000", "012", "101", "121", "210", "222"], ArgumentGroupSize = 1)]
    DBottomDisplay
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

        yield return new Discriminator(SOffWhiteCipher.DTopDisplay, "top", displayedInfo[0], args: [displayedInfo[0]]);
        yield return new Discriminator(SOffWhiteCipher.DMiddleDisplay, "middle", displayedInfo[1], args: [displayedInfo[1]]);
        yield return new Discriminator(SOffWhiteCipher.DBottomDisplay, "bottom", displayedInfo[2], args: [displayedInfo[2]]);

        yield return question(SOffWhiteCipher.QTopDisplay).AvoidDiscriminators(SOffWhiteCipher.DTopDisplay).Answers(displayedInfo[0]);
        yield return question(SOffWhiteCipher.QMiddleDisplay).AvoidDiscriminators(SOffWhiteCipher.DMiddleDisplay).Answers(displayedInfo[1]);
        yield return question(SOffWhiteCipher.QBottomDisplay).AvoidDiscriminators(SOffWhiteCipher.DBottomDisplay).Answers(displayedInfo[2]);
    }
}
