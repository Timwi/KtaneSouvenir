using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessYahtzee(KMBombModule module)
    {
        var comp = GetComponent(module, "YahtzeeModule");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };

        // This array only changes its contents, it’s never reassigned, so we only need to get it once
        var diceValues = GetArrayField<int>(comp, "_diceValues").Get();

        while (diceValues.Any(v => v == 0))
            yield return new WaitForSeconds(.1f);

        string result;

        // Capture the first roll
        if (Enumerable.Range(1, 6).Any(i => diceValues.Count(val => val == i) == 5))
        {
            Debug.LogFormat("[Souvenir #{0}] No question for Yahtzee because the first roll was a Yahtzee.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            result = null;  // don’t yield break here because we need to know when the module is solved in case there are multiple Yahtzees on the bomb
        }
        else if (diceValues.Contains(2) && diceValues.Contains(3) && diceValues.Contains(4) && diceValues.Contains(5) && (diceValues.Contains(1) || diceValues.Contains(6)))
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
        {
            Debug.LogFormat("[Souvenir #{0}] No question for Yahtzee because the first roll was nothing.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            result = null;
        }

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Yahtzee);

        if (result != null)
            addQuestion(module, Question.YahtzeeInitialRoll, correctAnswers: new[] { result });
    }

    private IEnumerable<object> ProcessYellowArrows(KMBombModule module)
    {
        var comp = GetComponent(module, "YellowArrowsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_YellowArrows);

        int letterIndex = GetIntField(comp, "letindex").Get(min: 0, max: 25);
        addQuestion(module, Question.YellowArrowsStartingRow, correctAnswers: new[] { ((char) ('A' + letterIndex)).ToString() });
    }

    private IEnumerable<object> ProcessYellowButton(KMBombModule module)
    {
        var comp = GetComponent(module, "YellowButtonScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_YellowButton);

        var sqs = GetArrayField<MeshRenderer>(comp, "ColorSquares", isPublic: true).Get(expectedLength: 8);
        var colorNames = _attributes[Question.YellowButtonColors].AllAnswers;
        addQuestions(module, sqs.Select((sq, ix) =>
        {
            var m = Regex.Match(sq.sharedMaterial.name, @"^Color([0-5])$");
            if (!m.Success)
                throw new AbandonModuleException("Expected material name “Color0–5”, got: “{0}”", sq.sharedMaterial.name);
            return makeQuestion(Question.YellowButtonColors, _YellowButton, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colorNames[int.Parse(m.Groups[1].Value)] });
        }));
    }

    private IEnumerable<object> ProcessYellowCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "yellowCipher", Question.YellowCipherScreen, _YellowCipher);
    }
}