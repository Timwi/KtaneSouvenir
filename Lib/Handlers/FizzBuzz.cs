using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFizzBuzz
{
    [SouvenirQuestion("What was the {1} digit on the {2} display of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, "top", QandA.Ordinal, "middle", QandA.Ordinal, "bottom"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    [AnswerGenerator.Integers(0, 9)]
    DisplayedNumbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("fizzBuzzModule", "FizzBuzz", typeof(SFizzBuzz), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessFizzBuzz(ModuleData module)
    {
        var comp = GetComponent(module, "FizzBuzzModule");
        var labels = GetArrayField<TextMesh>(comp, "Labels", isPublic: true).Get(expectedLength: 3);
        var solutions = GetArrayField<int[]>(comp, "Solutions").Get(expectedLength: 2, nullContentAllowed: true);

        while (solutions.GetValue(0) is null)
            yield return null; // Don't wait 0.1 seconds to make sure we get the labels before they are changed.

        var labelTexts = labels.Select(t => t.text).ToArray();
        var displayedDigits = new List<string>();
        foreach (var text in labelTexts)
        {
            var match = Regex.Match(text, @"^(?:[RGBYW]\s)?(\d{7})$");
            if (!match.Success)
                throw new AbandonModuleException($"Unexpected display value: “{text}”.");
            displayedDigits.Add(match.Groups[1].Value);
        }

        yield return WaitForSolve;

        var qs = new List<QandA>();
        var displays = new[] { "top", "middle", "bottom" };
        for (var pos = 0; pos < 3; pos++)
        {
            if (labelTexts[pos] != labels[pos].text)
            {
                for (var digit = 0; digit < 6; digit++)
                    qs.Add(makeQuestion(Question.FizzBuzzDisplayedNumbers, module, formatArgs: new[] { Ordinal(digit + 1), displays[pos] }, correctAnswers: new[] { displayedDigits[pos][digit].ToString() }));
                if (!labels[pos].text.ToLowerInvariant().Contains("buzz")) // Do not ask about the last digit if the answer was buzz because there are only two possible correct answers.
                    qs.Add(makeQuestion(Question.FizzBuzzDisplayedNumbers, module, formatArgs: new[] { "7th", displays[pos] }, correctAnswers: new[] { displayedDigits[pos][6].ToString() }));
            }
        }
        if (qs.Count == 0)
            legitimatelyNoQuestion(module, "All of the numbers remained on the display.");
        else
            addQuestions(module, qs);
    }
}