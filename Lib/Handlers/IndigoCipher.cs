using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SIndigoCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["BEAVER", "INDENT", "LONELY", "PILLAR", "REFUGE", "RIPPED", "STOLEN", "TUMBLE", "WHIMSY", "WYVERN"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("indigoCipher", "Indigo Cipher", typeof(SIndigoCipher), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessIndigoCipher(ModuleData module) => processColoredCiphers(module, "indigoCipher", Question.IndigoCipherScreen);
}