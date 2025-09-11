using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SRedButtont
{
    [SouvenirQuestion("What was the word before “SUBMIT” in {0}?", TwoColumns4Answers, AddThe = false, ExampleAnswers = ["ABACUS", "BABBLE", "CABLES", "DABBLE", "EAGLES", "FABLED", "HABITS", "IAMBIC"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("redbuttont", "Red Button’t", typeof(SRedButtont), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessRedButtont(ModuleData module)
    {
        var comp = GetComponent(module, "BaseButtonScript");
        yield return WaitForSolve;

        GetField<TextMesh>(comp, "DisplayText", isPublic: true).Get().gameObject.SetActive(false);
        var allWords = GetArrayField<string>(comp, "keyword").Get(expectedLength: 4027, validator: s => s.Length != 6 ? "expected word length 6" : null);
        var word = GetField<string>(comp, "selectkeyword", isPublic: true).Get(s => !allWords.Contains(s) ? "expected valid word" : null);
        addQuestion(module, Question.RedButtontWord, correctAnswers: new[] { word }, allAnswers: allWords);
    }
}