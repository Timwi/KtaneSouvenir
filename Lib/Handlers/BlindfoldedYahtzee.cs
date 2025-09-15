using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBlindfoldedYahtzee
{
    [SouvenirQuestion("What roll did the module claim in the {1} stage of {0}?", TwoColumns4Answers, "Yahtzee", "Large Straight", "Small Straight", "Full House", "Four of a Kind", "Chance", "Three of a Kind", "1s", "2s", "3s", "4s", "5s", "6s", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Claim
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSBlindfoldedYahtzee", "Blindfolded Yahtzee", typeof(SBlindfoldedYahtzee), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessBlindfoldedYahtzee(ModuleData module)
    {
        var comp = GetComponent(module, "BlindfoldedYahtzeeScript");
        var fldExpecting = GetField<bool>(comp, "ExpectingInput");
        var fldPrev = GetListField<bool>(comp, "PrevAnswers");
        var fldCategory = GetField<string>(comp, "CategoryName");
        var stages = new string[5];

        while (module.Unsolved)
        {
            if (fldExpecting.Get())
                stages[fldPrev.Get(minLength: 0, maxLength: 4).Count] = fldCategory.Get();
            yield return null;
        }

        var map = new Dictionary<string, string>()
        {
            ["Yahtzee"] = "Yahtzee",
            ["large straight"] = "Large Straight",
            ["small straight"] = "Small Straight",
            ["full house"] = "Full House",
            ["four of a kind"] = "Four of a Kind",
            ["chance"] = "Chance",
            ["three of a kind"] = "Three of a Kind",
            ["1s"] = "1s",
            ["2s"] = "2s",
            ["3s"] = "3s",
            ["4s"] = "4s",
            ["5s"] = "5s",
            ["6s"] = "6s",
        };

        var all = stages.Select(s => map[s]).ToArray();
        addQuestions(module, stages.Select((s, i) =>
            makeQuestion(Question.BlindfoldedYahtzeeClaim, module,
                correctAnswers: new[] { map[s] },
                formatArgs: new[] { Ordinal(i + 1) },
                preferredWrongAnswers: all)));
    }
}