using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SBlindArrows
{
    [Question("What was the color of the {1} LED in the {2} stage of {0}?", ThreeColumns6Answers, "Red", "Black", "Orange", "Yellow", "Green", "Cyan", "Magenta", "White", TranslateAnswers = true, TranslateArguments = [true, false], Arguments = ["top", QandA.Ordinal, "right", QandA.Ordinal], ArgumentGroupSize = 2)]
    QLEDs,

    [Question("What was the position of the arrow in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, AnswerType = InfoType.Sprites)]
    QPositions,

    [Question("What was the direction of the arrow in the {1} stage of {0}?", ThreeColumns6Answers, "↑", "↖", "←", "↙", "↓", "↘", "→", "↗", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QDirections,

    [Discriminator("the Blind Arrows where the color of the {0} LED in the {1} stage was {2}", TranslateArguments = [true, false, true], Arguments = ["top", QandA.Ordinal, "red", "right", QandA.Ordinal, "black", "top", QandA.Ordinal, "orange", "right", QandA.Ordinal, "yellow", "top", QandA.Ordinal, "green", "right", QandA.Ordinal, "cyan", "top", QandA.Ordinal, "magenta", "right", QandA.Ordinal, "white"], ArgumentGroupSize = 3)]
    DLEDs,

    [Discriminator("the Blind Arrows where the arrow in the {0} stage was here", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, QuestionExtraType = InfoType.Sprites)]
    DPositions,

    [Discriminator("the Blind Arrows where the arrow in the {0} stage was {1}", Arguments = [QandA.Ordinal, "↗", QandA.Ordinal, "↙"], ArgumentGroupSize = 2)]
    DDirections
}

public partial class SouvenirModule
{
    [Handler("blindArrows", "Blind Arrows", typeof(SBlindArrows), "Infusion")]
    [ManualQuestion("What were the colors of the LEDs in the first two stages?")]
    [ManualQuestion("What was the position of the arrow in the first two stages?")]
    [ManualQuestion("What was the direction of the arrow in the first two stages?")]
    private IEnumerator<SouvenirInstruction> ProcessBlindArrows(ModuleData module)
    {
        if (Bomb.IsIndicatorPresent(Indicator.BOB))
            yield return legitimatelyNoQuestion(module, "The unicorn rule applied.");

        var comp = GetComponent(module, "blindArrowsScript");
        var fldStage = GetIntField(comp, "Stage");
        var colors = GetArrayField<Material>(comp, "LEDOptions", isPublic: true).Get(expectedLength: 8);
        var fldTopLed = GetIntField(comp, "LEDIndex1");
        var fldRightLed = GetIntField(comp, "LEDIndex2");
        var fldPosition = GetIntField(comp, "ButtonIndex");
        var fldDirection = GetIntField(comp, "RotationStore");

        var ledColors = new[] { new string[2], new string[2] };
        var allPositions = new int[2];
        var allDirections = new int[2];

        while (module.Unsolved)
        {
            var stage = fldStage.Get(min: 0, max: 2);
            if (stage < 2)
            {
                ledColors[0][stage] = colors[fldTopLed.Get(min: 0, max: 7)].name;
                ledColors[1][stage] = colors[fldRightLed.Get(min: 0, max: 7)].name;
                allPositions[stage] = fldPosition.Get(min: 0, max: 24);
                allDirections[stage] = fldDirection.Get(min: 0, max: 7);
            }
            yield return null;
        }

        var directions = new string[8] { "↑", "↖", "←", "↙", "↓", "↘", "→", "↗" };
        var leds = new string[2] { "top", "right" };

        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                yield return new Discriminator(SBlindArrows.DLEDs, $"led{j}-color{i}", ledColors[j][i], args: [leds[j], Ordinal(i + 1), ledColors[j][i].ToLowerInvariant()]);
                yield return question(SBlindArrows.QLEDs, args: [leds[j], Ordinal(i + 1)]).AvoidDiscriminators($"led{j}-color{i}").Answers(ledColors[j][i]);
            }
            yield return new Discriminator(SBlindArrows.DPositions, $"position{i}", allPositions[i], args: [Ordinal(i + 1)], questionExtra: Sprites.GenerateGridSprite(5, 5, allPositions[i]));
            yield return new Discriminator(SBlindArrows.DDirections, $"direction{i}", allDirections[i], args: [Ordinal(i + 1), directions[allDirections[i]]]);
            yield return question(SBlindArrows.QPositions, args: [Ordinal(i + 1)]).AvoidDiscriminators($"position{i}").Answers(new Coord(5, 5, allPositions[i]));
            yield return question(SBlindArrows.QDirections, args: [Ordinal(i + 1)]).AvoidDiscriminators($"direction{i}").Answers(directions[allDirections[i]]);
        }
    }
}
