using System.Collections;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SRegularCrazyTalk
{
    [SouvenirQuestion("What was the displayed digit that corresponded to the solution phrase in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    Digit,

    [SouvenirQuestion("What was the embellishment of the solution phrase in {0}?", OneColumn4Answers, "[PHRASE]", "It says: [PHRASE]", "Quote: [PHRASE] End quote", "“[PHRASE]”", "It says: “[PHRASE]”", "“It says: [PHRASE]”", TranslateAnswers = true)]
    Modifier
}

public partial class SouvenirModule
{
    [SouvenirHandler("RegularCrazyTalkModule", "Regular Crazy Talk", typeof(SRegularCrazyTalk), "Espik")]
    private IEnumerator<SouvenirInstruction> ProcessRegularCrazyTalk(ModuleData module)
    {
        var comp = GetComponent(module, "RegularCrazyTalkModule");
        yield return WaitForSolve;

        var phrases = GetField<IList>(comp, "_phraseActions").Get();
        var selected = GetField<int>(comp, "_selectedPhraseIx").Get();

        var selectedPhrase = phrases[selected];
        var phraseText = GetField<string>(selectedPhrase, "Phrase", isPublic: true).Get(v => string.IsNullOrEmpty(v) ? "‘Phrase’ is empty" : null);
        var displayDigit = GetField<int>(selectedPhrase, "ExpectedDigit", isPublic: true).Get();

        var modifier = "[PHRASE]";

        if (phraseText.Length >= 10 && phraseText.Substring(0, 10) == "It says: “") modifier = "It says: “[PHRASE]”";
        else if (phraseText.Length >= 9 && phraseText.Substring(0, 9) == "“It says:") modifier = "“It says: [PHRASE]”";
        else if (phraseText.Length >= 8 && phraseText.Substring(0, 8) == "It says:") modifier = "It says: [PHRASE]";
        else if (phraseText.Length >= 6 && phraseText.Substring(0, 6) == "Quote:") modifier = "Quote: [PHRASE] End quote";
        else if (phraseText.Substring(0, 1) == "“") modifier = "“[PHRASE]”";

        yield return question(SRegularCrazyTalk.Digit).Answers(displayDigit.ToString());
        yield return question(SRegularCrazyTalk.Modifier).Answers(modifier);
    }
}