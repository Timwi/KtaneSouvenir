using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWhiteCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["ATTEND", "BREATH", "CRUNCH", "EFFECT", "JAILED", "JUMPER", "PLASMA", "UPROAR", "VERTEX", "VIEWED"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("whiteCipher", "White Cipher", typeof(SWhiteCipher), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessWhiteCipher(ModuleData module) => processColoredCiphers(module, "whiteCipher", Question.WhiteCipherScreen);
}