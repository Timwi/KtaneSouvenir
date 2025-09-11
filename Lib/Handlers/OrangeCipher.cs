using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SOrangeCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["FORMAL", "FREEZE", "GLANCE", "JACKED", "JAMMED", "JAMMER", "NECTAR", "NEEDED", "QUEENS", "UTOPIA"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateFormatArgs = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("orangeCipher", "Orange Cipher", typeof(SOrangeCipher), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessOrangeCipher(ModuleData module) => processColoredCiphers(module, "orangeCipher", Question.OrangeCipherScreen)
}