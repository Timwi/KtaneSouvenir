using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAlphabetNumbers
{
    [SouvenirQuestion("Which of these numbers was on one of the buttons in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 32)]
    DisplayedNumbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("alphabetNumbers", "Alphabet Numbers", typeof(SAlphabetNumbers), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessAlphabetNumbers(ModuleData module)
    {
        var comp = GetComponent(module, "alphabeticalOrderScript");

        var fldStageNumber = GetIntField(comp, "stage");
        var labels = GetArrayField<object>(comp, "buttons", isPublic: true).Get(expectedLength: 6).Select(b => GetField<TextMesh>(b, "text", isPublic: true).Get());
        var stageOptionCounts = new[] { 22, 28, 28, 32 };
        var allOptions = Enumerable.Range(1, 32).Select(pos => pos.ToString());
        var displayedNumberSets = new List<string[]>();

        var fldLevelOrdered = GetField<IList>(comp, "levelOrdered", isPublic: true);
        while (fldLevelOrdered.Get().Count == 0) // Make sure labels have been set for the first time.
            yield return null; // Don’t wait .1 seconds so that we are absolutely sure we get the right stage.

        var qs = new List<QandA>();
        do
        {
            while (fldStageNumber.Get(min: displayedNumberSets.Count - 1, max: displayedNumberSets.Count) < displayedNumberSets.Count)
                yield return null; // Don’t wait .1 seconds so that we are absolutely sure we get the right stage.
            displayedNumberSets.Add(labels.Select(l => l.text).ToArray());
        }
        while (displayedNumberSets.Count < 4);

        yield return WaitForSolve;

        addQuestions(module, displayedNumberSets.Select((numArr, stage) => makeQuestion(Question.AlphabetNumbersDisplayedNumbers, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: displayedNumberSets[stage], preferredWrongAnswers: allOptions.Take(stageOptionCounts[stage]).ToArray())));
    }
}