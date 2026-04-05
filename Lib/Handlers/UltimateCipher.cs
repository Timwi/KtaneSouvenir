using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUltimateCipher
{
    [Question("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["ACCESS", "EMPIRE", "EXPEND", "INDUCE", "LOCATE", "MELODY", "SPIRIT", "STOLEN", "VESSEL", "WIGGLE"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [Handler("ultimateCipher", "Ultimate Cipher", typeof(SUltimateCipher), "BigCrunch22")]
    [ManualQuestion("What was on each screen?")]
    private IEnumerator<SouvenirInstruction> ProcessUltimateCipher(ModuleData module) => processColoredCiphers(module, "ultimateCipher", SUltimateCipher.Screen);
}