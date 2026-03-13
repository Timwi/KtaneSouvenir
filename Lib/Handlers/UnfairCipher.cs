using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SUnfairCipher
{
    [SouvenirQuestion("What was the {1} letter of the encrypted message in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letters
}

public partial class SouvenirModule
{
    [SouvenirHandler("unfairCipher", "Unfair Cipher", typeof(SUnfairCipher), "Espik")]
    private IEnumerator<SouvenirInstruction> ProcessUnfairCipher(ModuleData module)
    {
        var comp = GetComponent(module, "unfairCipherScript");

        // The module doesn't store the message in any variables, so we have to get it from the screen itself
        var textDisplay = GetField<TextMesh>(comp, "screen", isPublic: true).Get();

        yield return WaitForSolve;

        var message = textDisplay.text;

        for (var i = 0; i < 12; i++)
            yield return question(SUnfairCipher.Letters, args: [Ordinal(i + 1)]).Answers(message[i].ToString());
    }
}
