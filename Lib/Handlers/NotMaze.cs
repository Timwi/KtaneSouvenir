using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotMaze
{
    [SouvenirQuestion("What was the starting distance in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 9)]
    StartingDistance
}

public partial class SouvenirModule
{
    [SouvenirHandler("NotMaze", "Not Maze", typeof(SNotMaze), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessNotMaze(ModuleData module)
    {
        var component = GetComponent(module, "NotMaze");
        yield return WaitForSolve;

        addQuestion(module, Question.NotMazeStartingDistance, correctAnswers: new[] { GetIntField(component, "distance").Get().ToString() });
    }
}