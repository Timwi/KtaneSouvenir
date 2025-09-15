using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFaultyRGBMaze
{
    [SouvenirQuestion("Where was the {1} key in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-G", "1-7")]
    Keys,

    [SouvenirQuestion("Which maze number was the {1} maze in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0-9a-f")]
    Number,

    [SouvenirQuestion("What was the exit coordinate in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-G", "1-7")]
    Exit
}

public partial class SouvenirModule
{
    [SouvenirHandler("faultyrgbMaze", "Faulty RGB Maze", typeof(SFaultyRGBMaze), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessFaultyRGBMaze(ModuleData module)
    {
        var comp = GetComponent(module, "FaultyRGBMazeScript");
        yield return WaitForSolve;

        var keyPos = GetArrayField<int[]>(comp, "keylocations").Get(expectedLength: 3, validator: key => key.Length != 2 ? "expected length 2" : key.Any(number => number is < 0 or > 6) ? "expected range 0–6" : null);
        var mazeNum = GetArrayField<int[]>(comp, "mazenumber").Get(expectedLength: 3, validator: maze => maze.Length != 2 ? "expected length 2" : maze[0] is < 0 or > 15 ? "expected range 0–15" : null);
        var exitPos = GetArrayField<int>(comp, "exitlocation").Get(expectedLength: 3);

        if (exitPos[1] < 0 || exitPos[1] > 6 || exitPos[2] < 0 || exitPos[2] > 6)
            throw new AbandonModuleException($"‘exitPos’ contains invalid coordinate: ({exitPos[2]},{exitPos[1]})");

        string[] colors = { "red", "green", "blue" };

        for (var index = 0; index < 3; index++)
        {
            yield return question(SFaultyRGBMaze.Keys, args: [colors[index]]).Answers("ABCDEFG"[keyPos[index][1]] + (keyPos[index][0] + 1).ToString());
            yield return question(SFaultyRGBMaze.Number, args: [colors[index]]).Answers("0123456789abcdef"[mazeNum[index][0]].ToString());
        }

        yield return question(SFaultyRGBMaze.Exit).Answers("ABCDEFG"[exitPos[2]] + (exitPos[1] + 1).ToString());
    }
}