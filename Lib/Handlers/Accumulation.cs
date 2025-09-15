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

        yield return question(SAccumulation.BorderColor).Answers(colorNames[borderIx]);
        yield return question(SAccumulation.BackgroundColor, args: [Ordinal(1)]).Answers(bgNames[0], preferredWrong: bgNames);
        yield return question(SAccumulation.BackgroundColor, args: [Ordinal(2)]).Answers(bgNames[1], preferredWrong: bgNames);
        yield return question(SAccumulation.BackgroundColor, args: [Ordinal(3)]).Answers(bgNames[2], preferredWrong: bgNames);
        yield return question(SAccumulation.BackgroundColor, args: [Ordinal(4)]).Answers(bgNames[3], preferredWrong: bgNames);
        yield return question(SAccumulation.BackgroundColor, args: [Ordinal(5)]).Answers(bgNames[4], preferredWrong: bgNames);
    }
}
