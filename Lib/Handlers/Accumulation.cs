using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAccumulation
{
    [SouvenirQuestion("What was the border color in {0}?", ThreeColumns6Answers, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow", TranslateAnswers = true)]
    BorderColor,

    [SouvenirQuestion("What was the background color on the {1} stage in {0}?", ThreeColumns6Answers, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    BackgroundColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("accumulation", "Accumulation", typeof(SAccumulation), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessAccumulation(ModuleData module)
    {
        var comp = GetComponent(module, "accumulationScript");

        yield return WaitForSolve;

        var colorNames = new Dictionary<int, string> {
            { 9, "Blue" },
            { 23, "Brown" },
            { 4, "Green" },
            { 15, "Grey" },
            { 26, "Lime" },
            { 2, "Orange" },
            { 8, "Pink" },
            { 17, "Red" },
            { 11, "White" },
            { 10, "Yellow" }
        };

        var borderIx = GetIntField(comp, "borderValue").Get(v => !colorNames.ContainsKey(v) ? "value is not in the dictionary" : null);
        var bgNames = GetArrayField<Material>(comp, "chosenBackgroundColours", isPublic: true)
            .Get(expectedLength: 5)
            .Select(x => char.ToUpperInvariant(x.name[0]) + x.name.Substring(1))
            .ToArray();

        addQuestions(module,
            makeQuestion(Question.AccumulationBorderColor, module, correctAnswers: new[] { colorNames[borderIx] }),
            makeQuestion(Question.AccumulationBackgroundColor, module, formatArgs: new[] { "first" }, correctAnswers: new[] { bgNames[0] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, module, formatArgs: new[] { "second" }, correctAnswers: new[] { bgNames[1] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, module, formatArgs: new[] { "third" }, correctAnswers: new[] { bgNames[2] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { bgNames[3] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { bgNames[4] }, preferredWrongAnswers: bgNames));
    }
}