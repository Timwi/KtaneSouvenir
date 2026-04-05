using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SYellowHuffmanCipher
{
    [Question("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [Handler("YellowHuffmanCipherModule", "Yellow Huffman Cipher", typeof(SYellowHuffmanCipher), "Timwi")]
    [ManualQuestion("What was on each screen?")]
    private IEnumerator<SouvenirInstruction> ProcessYellowHuffmanCipher(ModuleData module) =>
        processCompressionCiphers(module, "YellowHuffmanCipherModule", SYellowHuffmanCipher.Screen);
}
