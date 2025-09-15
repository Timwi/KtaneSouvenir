using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRGBMaze
{
    [SouvenirQuestion("Where was the {1} key in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-H", "1-8")]
    Keys,

    [SouvenirQuestion("Which maze number was the {1} maze in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Number,

    [SouvenirQuestion("What was the exit coordinate in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-H", "1-8")]
    Exit
}

public partial class SouvenirModule
{
    [SouvenirHandler("rgbMaze", "RGB Maze", typeof(SRGBMaze), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessRGBMaze(ModuleData module)
    {
        var comp = GetComponent(module, "RGBMazeScript");
        yield return WaitForSolve;

        var keyPos = GetArrayField<int[]>(comp, "keylocations").Get(expectedLength: 3, validator: key => key.Length != 2 ? "expected length 2" : key.Any(number => number is < 0 or > 7) ? "expected range 0–7" : null);
        var mazeNum = GetArrayField<int[]>(comp, "mazenumber").Get(expectedLength: 3, validator: maze => maze.Length != 2 ? "expected length 2" : maze[0] is < 0 or > 9 ? "expected maze[0] in range 0–9" : null);
        var exitPos = GetArrayField<int>(comp, "exitlocation").Get(expectedLength: 3);

        if (exitPos[1] < 0 || exitPos[1] > 7 || exitPos[2] < 0 || exitPos[2] > 7)
            throw new AbandonModuleException($"‘exitPos’ contains invalid coordinate: ({exitPos[2]},{exitPos[1]})");

        string[] colors = { "red", "green", "blue" };

        for (var index = 0; index < 3; index++)
        {
            yield return question(SRGBMaze.Keys, args: [colors[index]]).Answers("ABCDEFGH"[keyPos[index][1]] + (keyPos[index][0] + 1).ToString());
            yield return question(SRGBMaze.Number, args: [colors[index]]).Answers(mazeNum[index][0].ToString());
        }

        yield return question(SRGBMaze.Exit).Answers("ABCDEFGH"[exitPos[2]] + (exitPos[1] + 1).ToString());
    }
}