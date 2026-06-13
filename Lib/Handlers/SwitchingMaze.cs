using System.Collections.Generic;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSwitchingMaze
{
    [Question("What was the seed in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/")]
    Seed,

    [Question("What was the starting maze color in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Cyan", "Yellow", "Black", "White", "Gray", "Orange", "Pink", "Brown", TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [Handler("MazeSwitching", "Switching Maze", typeof(SSwitchingMaze), "BigCrunch22")]
    [ManualQuestion("What was the seed?")]
    [ManualQuestion("What was the starting maze color?")]
    private IEnumerator<SouvenirInstruction> ProcessSwitchingMaze(ModuleData module)
    {
        var comp = GetComponent(module, "SwitchingMazeScript");
        var seedTextMesh = GetField<TextMesh>(comp, "Seedling", isPublic: true).Get();
        var fldNumberBasis = GetField<int>(comp, "NumberBasis");
        var fldCoordinates = GetArrayField<int[]>(comp, "Copper");

        var matchingCoordinates = false;

        yield return WaitForActivate;

        var seed = seedTextMesh.text;
        var numberBasis = fldNumberBasis.Get();

        var coordinates = fldCoordinates.Get(expectedLength: 3);
        matchingCoordinates = coordinates[0][0] == coordinates[1][0] && coordinates[0][1] == coordinates[1][1];

        var hadStrike = false;
        module.Module.OnStrike += delegate { hadStrike = true; return false; };

        while (module.Unsolved)
        {
            if (hadStrike)
            {
                seed = seedTextMesh.text;
                numberBasis = fldNumberBasis.Get();
                coordinates = fldCoordinates.Get(expectedLength: 3);
                matchingCoordinates = coordinates[0][0] == coordinates[1][0] && coordinates[0][1] == coordinates[1][1];
                hadStrike = false;
            }
            yield return null;
        }

        var seedSplit = Regex.Replace(seed, " ", "").Split(':');
        var colorsOfTheMaze = GetArrayField<string>(comp, "ColorsOfMaze").Get();

        yield return question(SSwitchingMaze.Seed).Answers(seedSplit[1]);

        if (!matchingCoordinates)
            yield return question(SSwitchingMaze.Color).Answers(colorsOfTheMaze[numberBasis], preferredWrong: colorsOfTheMaze);
    }
}
