using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBrownCipher
{
    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", TwoColumns4Answers, ExampleAnswers = ["AROUND", "JUKING", "OCELOT", "PARDON", "SCHOOL", "SOCCER", "SPRING", "TIMING", "VALVES", "VORTEX"], Arguments = ["top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    Screen
}

public partial class SouvenirModule
{
    [SouvenirHandler("brownCipher", "Brown Cipher", typeof(SBrownCipher), "Marksam")]
    private IEnumerator<SouvenirInstruction> ProcessBrownCipher(ModuleData module) => processColoredCiphers(module, "brownCipher", Question.BrownCipherScreen);
}