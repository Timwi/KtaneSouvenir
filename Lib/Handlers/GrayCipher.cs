using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGrayCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["ASSUME", "EMBRYO", "GAMBIT", "LAMENT", "LEARNT", "NEBULA", "NEEDED", "OBJECT", "PHOTON", "QUARRY"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("grayCipher", "Gray Cipher", typeof(SGrayCipher), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessGrayCipher(ModuleData module) => processColoredCiphers(module, "grayCipher", SGrayCipher.Screen);
}