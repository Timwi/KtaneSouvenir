using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCornflowerCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("cornflowerCipher", "Cornflower Cipher", typeof(SCornflowerCipher), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessCornflowerCipher(ModuleData module) => processColoredCiphers(module, "cornflowerCipher", Question.CornflowerCipherScreen);
}