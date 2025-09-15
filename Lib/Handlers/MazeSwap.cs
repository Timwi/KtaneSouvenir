using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMazeSwap
{
    [SouvenirQuestion("Where was the {1} position in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = ["starting", "goal"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Grid(6, 6)]
    Position
}

public partial class SouvenirModule
{
    [SouvenirHandler("mazeSwap", "Maze Swap", typeof(SMazeSwap), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessMazeSwap(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "MazeSwapScript");
        foreach (var tri in GetArrayField<GameObject>(comp, "triangleObj", true).Get(expectedLength: 36))
            tri.SetActive(false);
        foreach (var cell in GetArrayField<TextMesh>(comp, "grid", true).Get(expectedLength: 36))
        {
            cell.text = "0";
            cell.color = new Color32(64, 64, 64, 255);
        }

        var start = GetIntField(comp, "start").Get(min: 0, max: 35);
        var goal = GetIntField(comp, "goal").Get(min: 0, max: 35);

        addQuestions(module,
            makeQuestion(Question.MazeSwapPosition, module, correctAnswers: new[] { new Coord(6, 6, start) }, formatArgs: new[] { "starting" }),
            makeQuestion(Question.MazeSwapPosition, module, correctAnswers: new[] { new Coord(6, 6, goal) }, formatArgs: new[] { "goal" }));
    }
}