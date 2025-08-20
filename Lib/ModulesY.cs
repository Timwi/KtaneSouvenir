using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessYahtzee(ModuleData module)
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

        addQuestion(module, Question.YahtzeeInitialRoll, correctAnswers: new[] { result });
    }

    private IEnumerator<YieldInstruction> ProcessYellowArrows(ModuleData module)
    {
        var comp = GetComponent(module, "YellowArrowsScript");
        yield return WaitForSolve;

        var letterIndex = GetIntField(comp, "_displayedLetterIx").Get(min: 0, max: 25);
        addQuestion(module, Question.YellowArrowsStartingRow, correctAnswers: new[] { ((char) ('A' + letterIndex)).ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessYellowButton(ModuleData module)
    {
        var comp = GetComponent(module, "YellowButtonScript");
        yield return WaitForSolve;

        var sqs = GetArrayField<MeshRenderer>(comp, "ColorSquares", isPublic: true).Get(expectedLength: 8);
        var colorNames = Question.YellowButtonColors.GetAttribute().AllAnswers;
        addQuestions(module, sqs.Select((sq, ix) =>
        {
            var m = Regex.Match(sq.sharedMaterial.name, @"^Color([0-5])$");
            return !m.Success
                ? throw new AbandonModuleException($"Expected material name “Color0–5”, got: “{sq.sharedMaterial.name}”")
                : makeQuestion(Question.YellowButtonColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { colorNames[int.Parse(m.Groups[1].Value)] });
        }));
    }

    private IEnumerator<YieldInstruction> ProcessYellowButtont(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "yellow");
        var text = GetField<TextMesh>(comp, "DisplayText", true).Get();
        var ans = text.text;
        if (!ans.Contains('.'))
            throw new AbandonModuleException($"Expected a filename with a dot, got {ans}");

        yield return null; // Wait one frame to allow other Souvenirs to also grab the text
        text.text = "";

        var names = GetArrayField<string>(comp, "names").Get(expectedLength: 4031);
        var extensions = new[] {
            new[] { "JPG", "JPEG", "SVG", "PNG" },
            new[] { "MP4", "AVI", "MKV", "WMV" },
            new[] { "MP3", "WAV", "OGG", "WMA" },
            new[] { "CS", "TXT", "JSON", "CSV", "DOC", "DOCX" },
            new[] { "EXE" },
            new[] { "ISO", "XYZ", "RET", "MAE" }
        };

        var ext = ans.Split('.').Last();
        var extIx = extensions.IndexOf(a => a.Contains(ext));
        var chosenNames = names.OrderRandomly().Take(6).ToArray();
        var answers = Enumerable
            .Range(0, 6)
            .Except(new[] { extIx })
            .Select(i => $"{names[i]}.{extensions[i].PickRandom()}")
            .Concat(new[] { ans })
            .ToArray();

        addQuestion(module, Question.YellowButtontFilename, correctAnswers: new[] { ans }, allAnswers: answers);
    }

    private IEnumerator<YieldInstruction> ProcessYellowCipher(ModuleData module) => processColoredCiphers(module, "yellowCipher", Question.YellowCipherScreen);
}
