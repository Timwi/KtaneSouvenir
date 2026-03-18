using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SArithmeticCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("ArithmeticCipherModule", "Arithmetic Cipher", typeof(SArithmeticCipher), "Timwi")]
    [SouvenirManualQuestion("What was on each screen?")]
    private IEnumerator<SouvenirInstruction> ProcessArithmeticCipher(ModuleData module) =>
        processCompressionCiphers(module, "ArithmeticCipherModule", SArithmeticCipher.Screen);
}
