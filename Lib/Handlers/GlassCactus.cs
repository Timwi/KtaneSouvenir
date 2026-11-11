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
    ColourOccurence,

    [Discriminator("the Glass Cactus where the breeze rotated {0}", Arguments = ["clockwise", "counter-clockwise"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    ClockDirectionDiscriminator,

    [Discriminator("the Glass Cactus where the {0} breeze movement was {1}", Arguments = [QandA.Ordinal, "up", QandA.Ordinal,  "up-right", QandA.Ordinal, "up-left", QandA.Ordinal, "right", QandA.Ordinal, "left", QandA.Ordinal, "down", QandA.Ordinal, "down-right", QandA.Ordinal, "down-left"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    BreezeMovementsDiscriminator,

    [Discriminator("the Glass Cactus where the {0} present colour was {1}", Arguments = ["most", "red", "second-most", "green", "second-least", "blue", "least", "yellow"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    ColourOccurenceDiscriminator
}

public partial class SouvenirModule
{
    [Handler("GlassCactus", "Glass Cactus", typeof(SGlassCactus), "thunder725")]
    [ManualQuestion("Which direction was the breeze rotating?")]
    [ManualQuestion("What were the breeze's movements?")]
    [ManualQuestion("Which colour was the least/most present?")]
    private IEnumerator<SouvenirInstruction> ProcessGlassCactus(ModuleData module)
    {
        var comp = GetComponent(module, "GlassCactus");

        yield return WaitForSolve;



        string clockDirection = GetField<Enum>(comp, "selectedBreezeRotation")
            .Get(validator: x => x.ToString() == "Clockwise" ? null : 
                x.ToString() == "CounterClockwise" ? null : 
                "Expected Clockwise or CounterClockwise")
            .ToString();
        if (clockDirection == "CounterClockwise") { clockDirection = "Counter-Clockwise"; }


        yield return question(SGlassCactus.ClockDirection)
            .AvoidDiscriminators([SGlassCactus.ClockDirectionDiscriminator])
            .Answers(clockDirection);

        yield return new Discriminator(SGlassCactus.ClockDirectionDiscriminator, "clockDir", clockDirection, [clockDirection]);


        IList breezeMovements = GetField<IList>(comp, "breezeMovementSequence")
            .Get(validator: v => v.Count != 5 ? "Expected List of length 5" : null);

        for (int i = 0; i  < breezeMovements.Count; i++)
        {
            string _movement = breezeMovements[i].ToString();
            switch(_movement)
            {
                case "UpLeft": _movement = "Up-Left"; break;
                case "UpRight": _movement = "Up-Right"; break;
                case "DownLeft": _movement = "Down-Left"; break;
                case "DownRight": _movement = "Down-Right"; break;
            }

            yield return question(SGlassCactus.BreezeMovements, args: [Ordinal(i + 1)])
                .AvoidDiscriminators([$"Breeze{i}"])
                .Answers(_movement);

            yield return new Discriminator(SGlassCactus.BreezeMovementsDiscriminator, $"Breeze{i}", _movement, [Ordinal(i + 1), _movement.ToLower()]);
        }


        // Which occurence Most / Second-Most / Second-Least / Least is asked (and becomes Souvenir-allowed) depends on Rule Seed
        Enum firstAllowedOccurenceEnum = GetField<Enum>(comp, "firstColourOccurenceRule")
            .Get(validator: x => x.ToString() == "Least" ? null :
            x.ToString() == "SecondLeast" ? null :
            x.ToString() == "SecondMost" ? null :
            x.ToString() == "Most" ? null : "Expected Least, SecondLeast, SecondMost or Most.");
        Enum secondAllowedOccurenceEnum = GetField<Enum>(comp, "secondColourOccurenceRule")
            .Get(validator: x => x.ToString() == "Least" ? null :
            x.ToString() == "SecondLeast" ? null :
            x.ToString() == "SecondMost" ? null :
            x.ToString() == "Most" ? null : "Expected Least, SecondLeast, SecondMost or Most.");
        IDictionary coloursPerOccurence = GetField<IDictionary>(comp, "colourOccurenceHierarchy")
            .Get(validator: x => x.Count != 4 ? "Expected 4 entries in the Dictionary" : null);
        
        string firstOccurenceColour = coloursPerOccurence[firstAllowedOccurenceEnum].ToString();
        string secondOccurenceColour = coloursPerOccurence[secondAllowedOccurenceEnum].ToString();

        string firstAllowedOccurence = firstAllowedOccurenceEnum.ToString();
        string secondAllowedOccurence = secondAllowedOccurenceEnum.ToString();

        if (firstOccurenceColour == "Red" || firstOccurenceColour == "Green" || firstOccurenceColour == "Blue" || firstOccurenceColour == "Yellow")
        {
            if (firstAllowedOccurence == "SecondLeast") { firstAllowedOccurence = "Second-Least"; }
            else if (firstAllowedOccurence == "SecondMost") { firstAllowedOccurence = "Second-Most"; }

            firstAllowedOccurence = firstAllowedOccurence.ToLower();

            yield return question(SGlassCactus.ColourOccurence, args: [firstAllowedOccurence])
                .AvoidDiscriminators(["firstOccurence"])
                .Answers(firstOccurenceColour);

            yield return new Discriminator(SGlassCactus.ColourOccurenceDiscriminator, "firstOccurence", firstOccurenceColour, [firstAllowedOccurence, firstOccurenceColour.ToLower()]);
        }


        if (secondOccurenceColour == "Red" || secondOccurenceColour == "Green" || secondOccurenceColour == "Blue" || secondOccurenceColour == "Yellow")
        {
            if (secondAllowedOccurence == "SecondLeast") { secondAllowedOccurence = "Second-Least"; }
            else if (secondAllowedOccurence == "SecondMost") { secondAllowedOccurence = "Second-Most"; }

            secondAllowedOccurence = secondAllowedOccurence.ToLower();

            yield return question(SGlassCactus.ColourOccurence, args: [secondAllowedOccurence])
                .AvoidDiscriminators(["secondOccurence"])
                .Answers(secondOccurenceColour);

            yield return new Discriminator(SGlassCactus.ColourOccurenceDiscriminator, "secondOccurence", secondOccurenceColour, [secondAllowedOccurence, secondOccurenceColour.ToLower()]);
        }

    }
}
