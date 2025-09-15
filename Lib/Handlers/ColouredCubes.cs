using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SColouredCubes
{
    [SouvenirQuestion("What was the colour of this {1} in the {2} stage of {0}?", ThreeColumns6Answers, "Black", "Indigo", "Blue", "Forest", "Teal", "Azure", "Green", "Jade", "Cyan", "Maroon", "Plum", "Violet", "Olive", "Grey", "Maya", "Lime", "Mint", "Aqua", "Red", "Rose", "Magenta", "Orange", "Salmon", "Pink", "Yellow", "Cream", "White", Arguments = ["cube", QandA.Ordinal, "stage light", QandA.Ordinal], ArgumentGroupSize = 2, UsesQuestionSprite = true, TranslateAnswers = true, TranslateArguments = [true, false])]
    Colours
}

public partial class SouvenirModule
{
    [SouvenirHandler("ColouredCubes", "Coloured Cubes", typeof(SColouredCubes), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessColouredCubes(ModuleData module)
    {
        var comp = GetComponent(module, "ColouredCubesModule");

        var screenText = GetField<string>(GetField<object>(comp, "Screen", isPublic: true).Get(), "_defaultText");

        var cubes = GetField<Array>(comp, "Cubes", isPublic: true).Get(arr => arr.Length != 9 ? "expected length 9" : null);
        var fldCubePosition = GetIntField(cubes.GetValue(0), "_position");
        var fldCubeColour = GetField<string>(cubes.GetValue(0), "_colourName");
        var allCubeColours = GetField<Dictionary<string, string>>(cubes.GetValue(0), "TernaryColourValuesToName").Get().Values.ToArray();

        var stageLights = GetField<Array>(comp, "StageLights", isPublic: true).Get(arr => arr.Length != 3 ? "expected length 3" : null);
        var fldStageLightNumber = GetIntField(stageLights.GetValue(0), "_stageNumber");
        var fldStageLightColour = GetField<string>(stageLights.GetValue(0), "_colourName");
        var allStageLightColours = GetStaticField<Dictionary<string, string>>(stageLights.GetValue(0).GetType(), "BinaryColourValuesToName").Get().Values.ToArray();

        var cubeColours = new string[3, 9];
        var stageLightColours = new string[2, 3];

        for (var nextStage = 1; nextStage <= 3; nextStage++)
        {
            while (screenText.Get(nullAllowed: true) != $"Stage {nextStage}")
                yield return null; // Do not wait 0.1 seconds to make sure we get the correct colours.
            foreach (var cube in cubes)
            {
                var position = fldCubePosition.GetFrom(cube, min: 0, max: 8);
                cubeColours[nextStage - 1, position] = fldCubeColour.GetFrom(cube, col => !allCubeColours.Contains(col) ? $"invalid cube colour ‘{col}’" : null);
            }
            if (nextStage != 3)
            {
                foreach (var light in stageLights)
                {
                    var number = 3 - fldStageLightNumber.GetFrom(light, min: 1, max: 3);
                    stageLightColours[nextStage - 1, number] = fldStageLightColour.GetFrom(light, col => !allStageLightColours.Contains(col) ? $"invalid stage light colour ‘{col}’" : null);
                }
            }
        }

        yield return WaitForSolve;
        for (var stage = 0; stage < 3; stage++)
        {
            for (var ix = 0; ix < 9; ix++)
                yield return question(SColouredCubes.Colours, args: ["cube", Ordinal(stage + 1)], questionSprite: Sprites.GenerateGridSprite(3, 3, ix)).Answers(cubeColours[stage, ix], preferredWrong: allCubeColours);
            if (stage < 2)
                for (var ix = 0; ix < 3; ix++)
                    yield return question(SColouredCubes.Colours, args: ["stage light", Ordinal(stage + 1)], questionSprite: Sprites.GenerateGridSprite(1, 3, ix)).Answers(stageLightColours[stage, ix], preferredWrong: allStageLightColours);
        }
    }
}