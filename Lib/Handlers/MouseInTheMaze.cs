using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMouseInTheMaze
{
    [SouvenirQuestion("Which color sphere was the goal in {0}?", TwoColumns4Answers, "white", "green", "blue", "yellow", TranslateAnswers = true)]
    Sphere,

    [SouvenirQuestion("What color was the torus in {0}?", TwoColumns4Answers, "white", "green", "blue", "yellow", TranslateAnswers = true)]
    Torus
}

public partial class SouvenirModule
{
    [SouvenirHandler("MouseInTheMaze", "Mouse in the Maze", typeof(SMouseInTheMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMouseInTheMaze(ModuleData module)
    {
        var comp = GetComponent(module, "MouseInTheMazeModule");
        var sphereColors = GetArrayField<int>(comp, "_sphereColors").Get(expectedLength: 4);

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var goalPos = GetIntField(comp, "_goalPosition").Get(min: 0, max: 3);
        var torusColor = GetIntField(comp, "_torusColor").Get(min: 0, max: 3);
        var goalColor = sphereColors[goalPos];
        yield return goalColor is < 0 or > 3
            ? throw new AbandonModuleException($"Unexpected color (torus={torusColor}; goal={goalColor})")
            : (YieldInstruction) WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.MouseInTheMazeSphere, module, correctAnswers: new[] { new[] { "white", "green", "blue", "yellow" }[goalColor] }),
            makeQuestion(Question.MouseInTheMazeTorus, module, correctAnswers: new[] { new[] { "white", "green", "blue", "yellow" }[torusColor] }));
    }
}