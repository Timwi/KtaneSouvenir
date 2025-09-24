using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBlueCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["ANCHOR", "ATTAIN", "DECIDE", "JAILOR", "LIGHTS", "OFFERS", "POETIC", "UNISON", "VECTOR", "VISION"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("blueCipher", "Blue Cipher", typeof(SBlueCipher), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessBlueCipher(ModuleData module) => processColoredCiphers(module, "blueCipher", SBlueCipher.Screen);
}