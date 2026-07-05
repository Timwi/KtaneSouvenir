using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SPlayfairCipher
{
    [Question("What was the {1} letter of the encrypted message in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letters,

    [Question("What color was the screen in {0}?", TwoColumns4Answers, "Magenta", "Blue", "Orange", "Yellow", TranslateAnswers = true)]
    ScreenColor
}

public partial class SouvenirModule
{
    [Handler("Playfair", "Playfair Cipher", typeof(SPlayfairCipher), "Espik")]
    [ManualQuestion("What was the encrypted message?")]
    [ManualQuestion("What color was the screen?")]
    private IEnumerator<SouvenirInstruction> ProcessPlayfairCipher(ModuleData module)
    {
        var comp = GetComponent(module, "playFair");
        var screenText = GetField<TextMesh>(comp, "ScreenText", isPublic: true).Get();

        var staticMsgs = new[] { "WRONG", "OKAY", "PARSING" };
        var message = "";

        while (!module.IsSolved)
        {
            if (!staticMsgs.Contains(screenText.text))
                message = screenText.text.Substring(0, 6); // Removes the ? at the end

            yield return null;
        }

        var screenColor = GetIntField(comp, "textcolor").Get();

        for (var i = 0; i < 6; i++)
            yield return question(SPlayfairCipher.Letters, args: [Ordinal(i + 1)]).Answers(message[i].ToString());

        yield return question(SPlayfairCipher.ScreenColor).Answers(SPlayfairCipher.ScreenColor.GetAnswers()[screenColor]);
    }
}
