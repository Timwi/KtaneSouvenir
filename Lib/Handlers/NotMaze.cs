using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotMaze
{
    [Question("What was the starting distance in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 9)]
    StartingDistance
}

public partial class SouvenirModule
{
    [Handler("NotMaze", "Not Maze", typeof(SNotMaze), "Andrio Celos")]
    [ManualQuestion("What was the starting distance?")]
    private IEnumerator<SouvenirInstruction> ProcessNotMaze(ModuleData module)
    {
        var component = GetComponent(module, "NotMaze");
        yield return WaitForSolve;

        yield return question(SNotMaze.StartingDistance).Answers(GetIntField(component, "distance").Get().ToString());
    }
}