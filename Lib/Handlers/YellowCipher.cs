using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SYellowCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["ALTHOUGH", "BUSINESS", "CHILDREN", "DIRECTOR", "EXCHANGE", "FUNCTION", "GUIDANCE", "HOSPITAL", "INDUSTRY", "JUNCTION", "KEYBOARD", "LANGUAGE", "MATERIAL", "NUMEROUS", "OFFERING", "POSSIBLE", "QUESTION", "RESEARCH", "SOFTWARE", "TOGETHER", "ULTIMATE", "VALUABLE", "WIRELESS", "XENOLITH", "YOURSELF", "ZUCCHINI"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("yellowCipher", "Yellow Cipher", typeof(SYellowCipher), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessYellowCipher(ModuleData module) => processColoredCiphers(module, "yellowCipher", Question.YellowCipherScreen);
}