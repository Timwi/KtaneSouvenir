using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SEncryptedHangman
{
    [SouvenirQuestion("What module name was encrypted by {0}?", OneColumn4Answers, ExampleAnswers = ["Anagrams", "Word Scramble", "Two Bits", "Switches", "Lights Out", "Emoji Math", "Math", "Semaphore", "Piano Keys", "Colour Flash"])]
    Module,
    
    [SouvenirQuestion("What method of encryption was used by {0}?", OneColumn4Answers, "Caesar Cipher", "Atbash Cipher", "Rot-13 Cipher", "Affine Cipher", "Modern Cipher", "Vigenère Cipher", "Playfair Cipher", TranslateAnswers = true)]
    EncryptionMethod
}

public partial class SouvenirModule
{
    [SouvenirHandler("encryptedHangman", "Encrypted Hangman", typeof(SEncryptedHangman), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessEncryptedHangman(ModuleData module)
    {
        var comp = GetComponent(module, "HangmanScript");
        var fldInAnimation = GetField<bool>(comp, "inAnimation", isPublic: true);
        var fldUncipheredAnswer = GetField<string>(comp, "uncipheredanswer", isPublic: true);
        var fldAnswerDisp = GetField<TextMesh>(comp, "AnswerDisp", isPublic: true);

        yield return WaitForSolve;

        // Dirty trick to circumvent the solve animation
        fldUncipheredAnswer.Set("");

        // Wait for the solve animation to finish (it still does one iteration despite the above trick)
        while (fldInAnimation.Get())
            yield return new WaitForSeconds(.1f);

        fldAnswerDisp.Get().text = "G O O D\nJ O B";

        var moduleName = GetField<string>(comp, "moduleName", isPublic: true).Get(v => v.Length == 0 ? "empty string" : null);
        var wrongModuleNames = Bomb.GetSolvableModuleNames().Distinct().ToList();
        // If there are less than 4 eligible modules, fill the remaining spaces with random other modules.
        if (wrongModuleNames.Count < 4)
            wrongModuleNames.AddRange(Ut.Attributes.Select(a => a.Value.ModuleNameWithThe).Distinct());

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.EncryptedHangmanModule, module, correctAnswers: new[] { moduleName }, preferredWrongAnswers: wrongModuleNames.ToArray()));

        var encryptionMethodNames = new[] { "Caesar Cipher", "Playfair Cipher", "Rot-13 Cipher", "Atbash Cipher", "Affine Cipher", "Modern Cipher", "Vigenère Cipher" };
        var encryptionMethod = GetIntField(comp, "encryptionMethod").Get(0, encryptionMethodNames.Length - 1);
        qs.Add(makeQuestion(Question.EncryptedHangmanEncryptionMethod, module, correctAnswers: new[] { encryptionMethodNames[encryptionMethod] }));

        addQuestions(module, qs);
    }
}