using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCrazyMaze
{
    [SouvenirQuestion("What was the {1} location in {0}?", ThreeColumns6Answers, Arguments = ["starting", "goal"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    [AnswerGenerator.Strings("A-Z", "A-Z")]
    StartOrGoal
}

public partial class SouvenirModule
{
    [SouvenirHandler("CrazyMazeModule", "Crazy Maze", typeof(SCrazyMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessCrazyMaze(ModuleData module)
    {
        var comp = GetComponent(module, "CrazyMazeScript");
        var fldStart = GetIntField(comp, "_startingCell");
        var fldGoal = GetIntField(comp, "_goalCell");
        var fldCellLetters = GetArrayField<string>(comp, "_cellLetters");

        yield return WaitForSolve;

        var cellLetters = fldCellLetters.Get(expectedLength: 26 * 26);
        var start = cellLetters[fldStart.Get(min: 0, max: 26 * 26 - 1)];
        var goal = cellLetters[fldGoal.Get(min: 0, max: 26 * 26 - 1)];
        addQuestions(module,
            makeQuestion(Question.CrazyMazeStartOrGoal, module, formatArgs: new[] { "starting" }, correctAnswers: new[] { start }, preferredWrongAnswers: new[] { goal }),
            makeQuestion(Question.CrazyMazeStartOrGoal, module, formatArgs: new[] { "goal" }, correctAnswers: new[] { goal }, preferredWrongAnswers: new[] { start }));
    }
}