using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRedCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["EATING", "GOBLET", "INCOME", "INSIDE", "MARKED", "POWDER", "STRING", "WIZARD", "WOBBLE", "YELLOW"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("redCipher", "Red Cipher", typeof(SRedCipher), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessRedCipher(ModuleData module) => processColoredCiphers(module, "redCipher", Question.RedCipherScreen);
}