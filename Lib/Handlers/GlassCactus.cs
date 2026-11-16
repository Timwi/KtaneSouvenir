using System;
using System.Collections;
using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGlassCactus
{
    [Question("Which direction was the breeze rotating in {0}?", OneColumn2Answers, "Clockwise", "Counter-Clockwise", TranslateAnswers = true)]
    ClockDirection,

    [Question("What was the {1} breeze movement in {0}?", ThreeColumns6Answers, "Up", "Up-Right", "Up-Left", "Right", "Left", "Down", "Down-Right", "Down-Left", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    BreezeMovements,

    [Question("Which colour was the {1} present in {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Yellow", TranslateAnswers = true, Arguments = ["most", "second-most", "second-least", "least"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    ColourOccurrence,

    [Discriminator("the Glass Cactus where the breeze rotated {0}", Arguments = ["clockwise", "counter-clockwise"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    ClockDirectionDiscriminator,

    [Discriminator("the Glass Cactus where the {0} breeze movement was {1}", Arguments = [QandA.Ordinal, "up", QandA.Ordinal, "up-right", QandA.Ordinal, "up-left", QandA.Ordinal, "right", QandA.Ordinal, "left", QandA.Ordinal, "down", QandA.Ordinal, "down-right", QandA.Ordinal, "down-left"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    BreezeMovementsDiscriminator,

    [Discriminator("the Glass Cactus where the {0} present colour was {1}", Arguments = ["most", "red", "second-most", "green", "second-least", "blue", "least", "yellow"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    ColourOccurrenceDiscriminator
}

public partial class SouvenirModule
{
    [Handler("GlassCactus", "Glass Cactus", typeof(SGlassCactus), "thunder725")]
    [ManualQuestion("Which direction was the breeze rotating?")]
    [ManualQuestion("What were the breeze's movements?")]
    [ManualQuestion("Which colour was the least/most and second-least/most present?")]
    private IEnumerator<SouvenirInstruction> ProcessGlassCactus(ModuleData module)
    {
        var comp = GetComponent(module, "GlassCactus");

        yield return WaitForSolve;

        var clockDirection = GetField<Enum>(comp, "selectedBreezeRotation")
            .Get(validator: x => x.ToString() is "Clockwise" or "CounterClockwise" ? null : "Expected Clockwise or CounterClockwise")
            .ToString();
        if (clockDirection == "CounterClockwise")
            clockDirection = "Counter-Clockwise";

        yield return question(SGlassCactus.ClockDirection)
            .AvoidDiscriminators([SGlassCactus.ClockDirectionDiscriminator])
            .Answers(clockDirection);

        yield return new Discriminator(SGlassCactus.ClockDirectionDiscriminator, "clockDir", clockDirection, [clockDirection]);

        var breezeMovements = GetField<IList>(comp, "breezeMovementSequence").Get(validator: v => v.Count != 5 ? "Expected length 5" : null);

        for (var i = 0; i < breezeMovements.Count; i++)
        {
            var _movement = breezeMovements[i].ToString();
            switch (_movement)
            {
                case "UpLeft": _movement = "Up-Left"; break;
                case "UpRight": _movement = "Up-Right"; break;
                case "DownLeft": _movement = "Down-Left"; break;
                case "DownRight": _movement = "Down-Right"; break;
            }

            yield return question(SGlassCactus.BreezeMovements, args: [Ordinal(i + 1)])
                .AvoidDiscriminators($"breeze{i}")
                .Answers(_movement);

            yield return new Discriminator(SGlassCactus.BreezeMovementsDiscriminator, $"breeze{i}", _movement, [Ordinal(i + 1), _movement.ToLower()]);
        }

        // Which occurrence Most / Second-Most / Second-Least / Least is asked (and becomes Souvenir-allowed) depends on Rule Seed
        var firstAllowedOccurrenceEnum = GetField<Enum>(comp, "firstColourOccurrenceRule")
            .Get(validator: x => x.ToString() is "Least" or "SecondLeast" or "SecondMost" or "Most" ? null : "Expected Least, SecondLeast, SecondMost or Most.");
        var secondAllowedOccurrenceEnum = GetField<Enum>(comp, "secondColourOccurrenceRule")
            .Get(validator: x => x.ToString() is "Least" or "SecondLeast" or "SecondMost" or "Most" ? null : "Expected Least, SecondLeast, SecondMost or Most.");
        var coloursPerOccurrence = GetField<IDictionary>(comp, "colourOccurrenceHierarchy")
            .Get(validator: x => x.Count != 4 ? "Expected 4 entries in the dictionary" : null);

        var firstOccurrenceColour = coloursPerOccurrence[firstAllowedOccurrenceEnum].ToString();
        var secondOccurrenceColour = coloursPerOccurrence[secondAllowedOccurrenceEnum].ToString();

        var firstAllowedOccurrence = firstAllowedOccurrenceEnum.ToString();
        var secondAllowedOccurrence = secondAllowedOccurrenceEnum.ToString();

        if (firstOccurrenceColour is "Red" or "Green" or "Blue" or "Yellow")
        {
            if (firstAllowedOccurrence == "SecondLeast")
                firstAllowedOccurrence = "Second-Least";
            else if (firstAllowedOccurrence == "SecondMost")
                firstAllowedOccurrence = "Second-Most";

            firstAllowedOccurrence = firstAllowedOccurrence.ToLowerInvariant();

            yield return question(SGlassCactus.ColourOccurrence, args: [firstAllowedOccurrence]).AvoidDiscriminators("firstOccurrence").Answers(firstOccurrenceColour);
            yield return new Discriminator(SGlassCactus.ColourOccurrenceDiscriminator, "firstOccurrence", firstOccurrenceColour, [firstAllowedOccurrence, firstOccurrenceColour.ToLower()]);
        }

        if (secondOccurrenceColour is "Red" or "Green" or "Blue" or "Yellow")
        {
            if (secondAllowedOccurrence == "SecondLeast")
                secondAllowedOccurrence = "Second-Least";
            else if (secondAllowedOccurrence == "SecondMost")
                secondAllowedOccurrence = "Second-Most";

            secondAllowedOccurrence = secondAllowedOccurrence.ToLowerInvariant();

            yield return question(SGlassCactus.ColourOccurrence, args: [secondAllowedOccurrence]).AvoidDiscriminators("secondOccurrence").Answers(secondOccurrenceColour);
            yield return new Discriminator(SGlassCactus.ColourOccurrenceDiscriminator, "secondOccurrence", secondOccurrenceColour, [secondAllowedOccurrence, secondOccurrenceColour.ToLower()]);
        }

    }
}
