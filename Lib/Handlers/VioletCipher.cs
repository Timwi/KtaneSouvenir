using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SVioletCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["DISMAY", "FRIDGE", "GALLON", "JAMMER", "KIDNEY", "RITUAL", "TRIPOD", "VIKING", "YEANED", "ZIPPER"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("violetCipher", "Violet Cipher", typeof(SVioletCipher), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessVioletCipher(ModuleData module) => processColoredCiphers(module, "violetCipher", Question.VioletCipherScreen);
}