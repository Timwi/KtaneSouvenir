using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMaze
{
    [SouvenirQuestion("In which {1} was the starting position in {0}, counting from the {2}?", ThreeColumns6Answers, TranslateArguments = [true, true], Arguments = ["column", "left", "row", "top"], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(1, 6)]
    StartingPosition
}

public partial class SouvenirModule
{
    [SouvenirHandler("Maze", "Maze", typeof(SMaze), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessMaze(ModuleData module)
    {
        var comp = GetComponent(module, "InvisibleWallsComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        var currentCell = GetProperty<object>(comp, "CurrentCell", isPublic: true).Get();  // Need to get the current cell at the start.
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        yield return question(SMaze.StartingPosition, args: ["column", "left"]).Answers((GetIntField(currentCell, "X", true).Get() + 1).ToString());
        yield return question(SMaze.StartingPosition, args: ["row", "top"]).Answers((GetIntField(currentCell, "Y", true).Get() + 1).ToString());
    }
}
