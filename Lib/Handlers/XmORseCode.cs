using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SXmORseCode
{
    [SouvenirQuestion("What word did you decrypt in {0}?", ThreeColumns6Answers, "ADMIT", "AWARD", "BANJO", "BRAVO", "CHILL", "CYCLE", "DECOR", "DISCO", "EERIE", "ERUPT", "FEWER", "FUZZY", "GERMS", "GUSTO", "HAULT", "HEXED", "ICHOR", "INFER", "JEWEL", "KTANE", "LADLE", "LYRIC", "MANGO", "MUTED", "NERDS", "NIXIE", "OOZED", "OXIDE", "PARTY", "PURSE", "QUEST", "RETRO", "ROUGH", "SCOWL", "SIXTH", "THANK", "TWINE", "UNBOX", "USHER", "VIBES", "VOICE", "WHIZZ", "WRUNG", "XENON", "YOLKS", "ZILCH")]
    Word,

    [SouvenirQuestion("What was the {1} displayed letter (in reading order) in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-Z")]
    DisplayedLetters
}

public partial class SouvenirModule
{
    [SouvenirHandler("xmorse", "XmORse Code", typeof(SXmORseCode), "shortc1rcuit")]
    private IEnumerator<SouvenirInstruction> ProcessXmORseCode(ModuleData module)
    {
        var comp = GetComponent(module, "XmORseCode");

        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        yield return WaitForSolve;

        var displayLetters = GetArrayField<int>(comp, "displayed").Get(expectedLength: 5, validator: number => number is < 0 or > 25 ? "expected range 0–25" : null);
        var words = Question.XmORseCodeWord.GetAnswers();
        var answerWord = words[GetIntField(comp, "answer").Get(validator: number => number is < 0 or > 45 ? "expected range 0–45" : null)];
        for (var i = 0; i < 5; i++)
            yield return question(SXmORseCode.DisplayedLetters, args: [Ordinal(i + 1)]).Answers(alphabet.Substring(displayLetters[i], 1), preferredWrong: displayLetters.Select(x => alphabet.Substring(x, 1)).ToArray());
        yield return question(SXmORseCode.Word).Answers(answerWord);
    }
}