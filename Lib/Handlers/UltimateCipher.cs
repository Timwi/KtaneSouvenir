using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUltimateCipher
{
    [Question("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["ACCESS", "EMPIRE", "EXPEND", "INDUCE", "LOCATE", "MELODY", "SPIRIT", "STOLEN", "VESSEL", "WIGGLE"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    QScreen,

    [Discriminator("the Ultimate Cipher that had {0} on the {1} screen on page {2}", Arguments = ["AMBUSH", "top", "1", "BANZAI", "middle", "1", "BIGGER", "bottom", "1", "GAMBLE", "top", "2", "KETOSE", "middle", "2", "OCULUS", "bottom", "2"], ArgumentGroupSize = 3, TranslateArguments = [false, true, false])]
    DScreen
}

public partial class SouvenirModule
{
    [Handler("ultimateCipher", "Ultimate Cipher", typeof(SUltimateCipher), "BigCrunch22")]
    [ManualQuestion("What was on each relevant screen?")]
    private IEnumerator<SouvenirInstruction> ProcessUltimateCipher(ModuleData module) => processColoredCiphers(module, "ultimateCipher", SUltimateCipher.QScreen, SUltimateCipher.DScreen);
}
