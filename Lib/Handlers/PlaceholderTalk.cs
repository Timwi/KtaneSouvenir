using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPlaceholderTalk
{
    [SouvenirQuestion("What was the first half of the first phrase in {0}?", TwoColumns4Answers, ExampleAnswers = ["", "IS IN THE", "IS THE", "IS IN UH", "IS", "IS AT", "IS INN", "IS THE IN", "IN IS", "IS IN.", "IS IN", "THE", "FIRST-", "IN", "UH IS IN", "AT", "LAST-", "UH", "KEYBORD", "A"])]
    FirstPhrase,
    
    [SouvenirQuestion("What was the last half of the first phrase in {0}?", TwoColumns4Answers, ExampleAnswers = ["", "FIRST POS.", "SECOND POS.", "THIRD POS.", "FOURTH POS.", "FIFTH POS.", "MILLIONTH POS.", "BILLIONTH POS.", "LAST POS.", "AN ANSWER"])]
    Ordinal
}

public partial class SouvenirModule
{
    [SouvenirHandler("placeholderTalk", "Placeholder Talk", typeof(SPlaceholderTalk), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessPlaceholderTalk(ModuleData module)
    {
        var comp = GetComponent(module, "placeholderTalk");
        yield return WaitForSolve;

        var answer = GetField<byte>(comp, "answerId").Get(b => b is < 0 or > 16 ? "expected range 0–16" : null) + 1;
        var firstPhrase = GetArrayField<string>(comp, "firstPhrase").Get();
        var firstString = GetField<string>(comp, "firstString").Get(str => !firstPhrase.Contains(str) ? $"expected string to be contained in “{firstPhrase}” (‘firstPhrase’)" : null);
        var ordinals = GetArrayField<string>(comp, "ordinals").Get();
        var currentOrdinal = GetField<string>(comp, "currentOrdinal").Get(str => !ordinals.Contains(str) ? $"expected string to be contained in “{ordinals}” (‘ordinals’)" : null);

        var qs = new List<QandA>();

        qs.Add(makeQuestion(Question.PlaceholderTalkFirstPhrase, module, correctAnswers: new[] { firstString }, preferredWrongAnswers: firstPhrase));
        qs.Add(makeQuestion(Question.PlaceholderTalkOrdinal, module, correctAnswers: new[] { currentOrdinal }, preferredWrongAnswers: ordinals));
        addQuestions(module, qs);
    }
}