using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SBlindArrows
{
	[Question("What was the color of the {1} LED in the {2} stage of {0}?", ThreeColumns6Answers, "Red", "Black", "Orange", "Yellow", "Green", "Cyan", "Magenta", "White", TranslateAnswers = true, TranslateArguments = [true, false], Arguments = ["top", QandA.Ordinal, "right", QandA.Ordinal], ArgumentGroupSize = 2)]
    QLEDs,

    [Question("What was the position of the arrow in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-E", "1-5")]
    QPositions,

    [Question("What was the direction of the arrow in the {1} stage of {0}?", ThreeColumns6Answers, "N", "NW", "W", "SW", "S", "SE", "E", "NE", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QDirections,

    [Discriminator("the Blind Arrows where the color of the {0} LED in the {1} stage was {2}", TranslateArguments = [true, false, true], Arguments = ["top", QandA.Ordinal, "Red", "right", QandA.Ordinal, "Cyan"], ArgumentGroupSize = 3)]
    DLEDs,

    [Discriminator("the Blind Arrows where the arrow in the {0} stage was at {1}", Arguments = [QandA.Ordinal, "A1", QandA.Ordinal, "E5"], ArgumentGroupSize = 2)]
    DPositions,

    [Discriminator("the Blind Arrows where the arrow in the {0} stage was pointing {1}", TranslateArguments = [false, true], Arguments = [QandA.Ordinal, "NE", QandA.Ordinal, "SW"], ArgumentGroupSize = 2)]
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
        var comp = GetComponent(module, "blindArrowsScript");
        var fldStage = GetField<int>(comp, "Stage");
        var fldColors = GetArrayField<Material>(comp, "LEDOptions", isPublic: true);
        var fldTopLED = GetField<int>(comp, "LEDIndex1");
        var fldRightLED = GetField<int>(comp, "LEDIndex2");
        var fldPosition = GetField<int>(comp, "ButtonIndex");
        var fldDirection = GetField<int>(comp, "RotationStore");
        var positions = new string[25] { "A1", "B1", "C1", "D1", "E1", "A2", "B2", "C2", "D2", "E2", "A3", "B3", "C3", "D3", "E3", "A4", "B4", "C4", "D4", "E4", "A5", "B5", "C5", "D5", "E5" };
        var directions = new string[8] { "N", "NW", "W", "SW", "S", "SE", "E", "NE" };
        var LEDs = new string[2] { "top", "right" };

        var LEDColors = new string[2][];
        LEDColors[0] = new string[3];
        LEDColors[1] = new string[3];
        var allPositions = new int[2];
        var allDirections = new int[2];

        while (module.Unsolved)
        {
            var stage = fldStage.Get();
            if (stage < 2)
            {
                var colors = fldColors.Get();
                LEDColors[0][stage] = colors[fldTopLED.Get()].name;
                LEDColors[1][stage] = colors[fldRightLED.Get()].name;
                allPositions[stage] = fldPosition.Get();
                allDirections[stage] = fldDirection.Get();
            }
            yield return null;
        }

        if (Bomb.IsIndicatorPresent(Indicator.BOB))
            yield return legitimatelyNoQuestion(module, "The unicorn rule applied.");

        for (var i = 0; i < 2; i++)
        {
            for (var j = 0; j < 2; j++)
            {
                yield return new Discriminator(SBlindArrows.DLEDs, $"led{j}-color{i}", LEDColors[j][i], args: [LEDs[j], Ordinal(i + 1), LEDColors[j][i]]);
                yield return question(SBlindArrows.QLEDs, args: [LEDs[j], Ordinal(i + 1)]).AvoidDiscriminators($"led{j}-color{i}").Answers(LEDColors[j][i]);
            }
            yield return new Discriminator(SBlindArrows.DPositions, $"position{i}", allPositions[i], args: [Ordinal(i + 1), positions[allPositions[i]]]);
            yield return new Discriminator(SBlindArrows.DDirections, $"direction{i}", allDirections[i], args: [Ordinal(i + 1), directions[allDirections[i]]]);
            yield return question(SBlindArrows.QPositions, args: [Ordinal(i + 1)]).AvoidDiscriminators($"position{i}").Answers(positions[allPositions[i]]);
            yield return question(SBlindArrows.QDirections, args: [Ordinal(i + 1)]).AvoidDiscriminators($"direction{i}").Answers(directions[allDirections[i]]);
        }
    }
}