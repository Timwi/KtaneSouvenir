using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWhiteCipher
{
    [Question("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["ATTEND", "BREATH", "CRUNCH", "EFFECT", "JAILED", "JUMPER", "PLASMA", "UPROAR", "VERTEX", "VIEWED"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    QScreen,

    [Discriminator("the White Cipher that had {0} on the {1} screen on page {2}", Arguments = ["AMBUSH", "top", "1", "BANZAI", "middle", "1", "BIGGER", "bottom", "1", "GAMBLE", "top", "2", "KETOSE", "middle", "2", "OCULUS", "bottom", "2"], ArgumentGroupSize = 3, TranslateArguments = [false, true, false])]
    DScreen
}

public partial class SouvenirModule
{
    [Handler("whiteCipher", "White Cipher", typeof(SWhiteCipher), "BigCrunch22")]
    [ManualQuestion("What was on each screen?")]
    private IEnumerator<SouvenirInstruction> ProcessWhiteCipher(ModuleData module) => processColoredCiphers(module, "whiteCipher", SWhiteCipher.QScreen, SWhiteCipher.DScreen);
}
