using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAccumulation
{
    [SouvenirQuestion("What was the border color in {0}?", ThreeColumns6Answers, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow", TranslateAnswers = true)]
    QBorderColor,

    [SouvenirQuestion("What was the background color in the {1} stage in {0}?", ThreeColumns6Answers, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QBackgroundColor,

    [SouvenirDiscriminator("the Accumulation whose border was {0}", Arguments = ["blue", "brown", "green", "grey", "lime", "orange", "pink", "red", "white", "yellow"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DBorderColor,

    [SouvenirDiscriminator("the Accumulation whose background in the {1} stage was {0}", Arguments = ["blue", QandA.Ordinal, "brown", QandA.Ordinal, "green", QandA.Ordinal, "grey", QandA.Ordinal, "lime", QandA.Ordinal, "orange", QandA.Ordinal, "pink", QandA.Ordinal, "red", QandA.Ordinal, "white", QandA.Ordinal, "yellow", QandA.Ordinal], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DBackgroundColor
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

        yield return question(SAccumulation.QBorderColor).AvoidDiscriminators(SAccumulation.DBorderColor).Answers(colorNames[borderIx]);
        yield return new Discriminator(SAccumulation.DBorderColor, "border", borderIx, args: [colorNames[borderIx].ToLowerInvariant()]);

        for (int stage = 0; stage < 5; stage++)
        {
            yield return question(SAccumulation.QBackgroundColor, args: [Ordinal(stage + 1)])
                .AvoidDiscriminators(SAccumulation.DBackgroundColor).Answers(bgNames[stage], preferredWrong: bgNames);
            yield return new Discriminator(SAccumulation.DBackgroundColor, $"bg-{stage}", bgNames[stage], args: [bgNames[stage].ToLowerInvariant(), Ordinal(stage + 1)]);
        }
    }
}
