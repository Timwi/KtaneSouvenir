using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFastPlayfairCipher
{
    [SouvenirQuestion("What was the last displayed message in {0}?", ThreeColumns6Answers, ExampleAnswers = ["CT", "DK", "SA", "SG", "SH", "TG", "TZ", "FP", "JA", "ZB"])]
    [AnswerGenerator.Strings("A-WYZ")]
    [AnswerGenerator.Strings("A-WYZ", "A-WYZ")]
    LastMessage
}

public partial class SouvenirModule
{
    [SouvenirHandler("FastPlayfairCipher", "Fast Playfair Cipher", typeof(SFastPlayfairCipher), "Dani was here")]
    private IEnumerator<SouvenirInstruction> ProcessFastPlayfairCipher(ModuleData module)
    {
        var comp = GetComponent(module, "FastPlayfairCipher");
        var fldScreen = GetField<TextMesh>(comp, "DisplayedMessage", isPublic: true);
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var wrongAnswers = new HashSet<string>();
        string letters = null;
        while (module.Unsolved)
        {
            letters = fldScreen.Get().text;
            if (letters.Length is not 1 and not 2)
                throw new AbandonModuleException($"The screen contains something other than one or two characters: “{letters}” ({letters.Length} characters).");
            wrongAnswers.Add(letters);
            yield return new WaitForSeconds(.1f);
        }
        if (letters == null)
            throw new AbandonModuleException("No letters were extracted before the module was solved.");
        addQuestion(module, Question.FastPlayfairCipherLastMessage, correctAnswers: new[] { letters }, preferredWrongAnswers: wrongAnswers.ToArray());
    }
}