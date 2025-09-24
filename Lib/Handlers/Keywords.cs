using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SKeywords
{
    [SouvenirQuestion("What were the first four letters on the display in {0}?", ThreeColumns6Answers, ExampleAnswers = ["abvo", "pola", "drea", "buew", "utre", "oidy"])]
    DisplayedKey
}

public partial class SouvenirModule
{
    [SouvenirHandler("xtrkeywords", "Keywords", typeof(SKeywords), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessKeywords(ModuleData module)
    {
        var comp = GetComponent(module, "keywordsScript");

        yield return WaitForSolve;

        var consonants = "bcdfghjklmnpqrstvwxyz".ToCharArray();
        var vowels = "aeiou".ToCharArray();
        var displayedKey = GetField<string>(comp, "displayKey").Get(v => v.Length < 4 ? "expected length at least 4" : null).Substring(0, 4);
        if (displayedKey.Count(x => consonants.Contains(x)) != 2 || displayedKey.Count(x => vowels.Contains(x)) != 2)
            throw new AbandonModuleException($"‘displayKey’ had an unexpected value of “{displayedKey}” when I expected a string starting with two consonants and two vowels in any order.");

        var possibleAnswers = new HashSet<string>() { displayedKey };
        while (possibleAnswers.Count < 6)
            possibleAnswers.Add(consonants.Shuffle().Take(2).Concat(vowels.Shuffle().Take(2)).ToArray().Shuffle().JoinString());

        yield return question(SKeywords.DisplayedKey).Answers(displayedKey, preferredWrong: possibleAnswers.ToArray());
    }
}