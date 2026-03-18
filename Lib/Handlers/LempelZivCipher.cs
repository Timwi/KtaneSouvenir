using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLempelZivCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("LempelZivCipherModule", "Lempel-Ziv Cipher", typeof(SLempelZivCipher), "Timwi")]
    [SouvenirManualQuestion("What was on each screen?")]
    private IEnumerator<SouvenirInstruction> ProcessLempelZivCipher(ModuleData module) =>
        processCompressionCiphers(module, "LempelZivCipherModule", SLempelZivCipher.Screen);
}
