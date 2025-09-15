using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SYahtzee
{
    [SouvenirQuestion("What was the initial roll on {0}?", TwoColumns4Answers, "Yahtzee", "large straight", "small straight", "four of a kind", "full house", "three of a kind", "two pairs", "pair", TranslateAnswers = true)]
    InitialRoll
}

public partial class SouvenirModule
{
    [SouvenirHandler("YahtzeeModule", "Yahtzee", typeof(SYahtzee), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessYahtzee(ModuleData module)
    {
        var comp = GetComponent(module, "YahtzeeModule");

        // This array only changes its contents, it’s never reassigned, so we only need to get it once
        var diceValues = GetArrayField<int>(comp, "_diceValues").Get();

        while (diceValues.Any(v => v == 0))
            yield return new WaitForSeconds(.1f);

        string result = null;

        // Capture the first roll
        if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 5))
            yield return legitimatelyNoQuestion(module, "The first roll was a Yahtzee.");

        if (diceValues.Contains(2) && diceValues.Contains(3) && diceValues.Contains(4) && diceValues.Contains(5) && (diceValues.Contains(1) || diceValues.Contains(6)))
            result = "large straight";
        else if (diceValues.Contains(3) && diceValues.Contains(4) && (
            (diceValues.Contains(1) && diceValues.Contains(2)) ||
            (diceValues.Contains(2) && diceValues.Contains(5)) ||
            (diceValues.Contains(5) && diceValues.Contains(6))))
            result = "small straight";
        else if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 4))
            result = "four of a kind";
        else if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 3) && Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 2))
            result = "full house";
        else if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 3))
            result = "three of a kind";
        else if (Enumerable.Range(1, 6).Count(i => diceValues.Count(val => val == i) == 2) == 2)
            result = "two pairs";
        else if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 2))
            result = "pair";
        else
            yield return legitimatelyNoQuestion(module, "The first roll was nothing.");

        yield return WaitForSolve;

        yield return question(SYahtzee.InitialRoll).Answers(result);
    }
}