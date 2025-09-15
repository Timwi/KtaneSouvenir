using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SProceduralMaze
{
    [SouvenirQuestion("What was the initial seed in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings("6*0-1")]
    InitialSeed
}

public partial class SouvenirModule
{
    [SouvenirHandler("ProceduralMaze", "Procedural Maze", typeof(SProceduralMaze), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessProceduralMaze(ModuleData module)
    {
        var comp = GetComponent(module, "ProceduralMazeModule");
        yield return WaitForSolve;

        var initialSeed = GetField<string>(comp, "_initialSeed").Get();

        StartCoroutine(GetMethod<IEnumerator>(GetField<object>(comp, "_mazeRenderer").Get(), "HideRings", 0, true).Invoke());
        addQuestion(module, Question.ProceduralMazeInitialSeed, correctAnswers: new[] { initialSeed });
    }
}