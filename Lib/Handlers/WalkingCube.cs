using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWalkingCube
{
    [SouvenirQuestion("Which of these cells was part of the cube’s path in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    Path
}

public partial class SouvenirModule
{
    [SouvenirHandler("WalkingCubeModule", "Walking Cube", typeof(SWalkingCube), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessWalkingCube(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "WalkingCubeModule");
        var path = GetArrayField<int[]>(comp, "_solutionSymbols").Get(expectedLength: 4, validator: v => v is not { Length: 4 } ? "Expected length 4" : v.Any(i => i is < -1) ? "Expected all positive" : null);
        var sol = Enumerable.Range(0, 16).Where(i => path[i / 4][i % 4] is not -1).Select(i => Sprites.GenerateGridSprite(4, 4, i)).ToArray();
        yield return question(SWalkingCube.Path).Answers(sol);
    }
}