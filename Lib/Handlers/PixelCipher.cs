using System;
using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPixelCipher
{
    [Question("What was the {1} displayed number in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 25)]
    Numbers
}

public partial class SouvenirModule
{
    [Handler("pixelcipher", "Pixel Cipher", typeof(SPixelCipher), "Espik")]
    [ManualQuestion("What were the displayed numbers?")]
    private IEnumerator<SouvenirInstruction> ProcessPixelCipher(ModuleData module)
    {
        var comp = GetComponent(module, "pixelcipherScript");
        yield return WaitForSolve;

        var numbersString = GetField<String>(comp, "displayedString").Get();
        var numbers = numbersString.Split(' ');

        for (var i = 0; i < 10; i++)
            yield return question(SPixelCipher.Numbers, args: [Ordinal(i + 1)]).Answers(numbers[i], preferredWrong: numbers);
    }
}
