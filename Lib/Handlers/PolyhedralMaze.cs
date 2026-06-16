using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SPolyhedralMaze
{
    [Question("What was the starting position in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 61)]
    StartPosition
}

public partial class SouvenirModule
{
    [Handler("PolyhedralMazeModule", "Polyhedral Maze", typeof(SPolyhedralMaze), "Timwi")]
    [ManualQuestion("What was the starting position?")]
    private IEnumerator<SouvenirInstruction> ProcessPolyhedralMaze(ModuleData module)
    {
        var comp = GetComponent(module, "PolyhedralMazeModule");

        yield return WaitForSolve;
        yield return question(SPolyhedralMaze.StartPosition).Answers(GetIntField(comp, "_startFace").Get().ToString());
    }
}
