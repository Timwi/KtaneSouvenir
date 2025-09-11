using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMazeScrambler
{
    [SouvenirQuestion("What was the starting position on {0}?", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    Start,
    
    [SouvenirQuestion("What was the goal on {0}?", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    Goal,
    
    [SouvenirQuestion("Which of these positions was a maze marking on {0}?", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    Indicators
}

public partial class SouvenirModule
{
    [SouvenirHandler("MazeScrambler", "Maze Scrambler", typeof(SMazeScrambler), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessMazeScrambler(ModuleData module)
    {
        var comp = GetComponent(module, "MazeScrambler");

        var ind1X = GetIntField(comp, "IDX1").Get(min: 0, max: 2);
        var ind1Y = GetIntField(comp, "IDY1").Get(min: 0, max: 2);
        var ind2X = GetIntField(comp, "IDX2").Get(min: 0, max: 2);
        var ind2Y = GetIntField(comp, "IDY2").Get(min: 0, max: 2);
        var startX = GetIntField(comp, "StartX").Get(min: 0, max: 2);
        var startY = GetIntField(comp, "StartY").Get(min: 0, max: 2);
        var goalX = GetIntField(comp, "GoalX").Get(min: 0, max: 2);
        var goalY = GetIntField(comp, "GoalY").Get(min: 0, max: 2);

        yield return WaitForSolve;

        var positionNames = new[] { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };

        addQuestions(module,
            makeQuestion(Question.MazeScramblerStart, module, correctAnswers: new[] { positionNames[startY * 3 + startX] }, preferredWrongAnswers: new[] { positionNames[goalY * 3 + goalX] }),
            makeQuestion(Question.MazeScramblerGoal, module, correctAnswers: new[] { positionNames[goalY * 3 + goalX] }, preferredWrongAnswers: new[] { positionNames[startY * 3 + startX] }),
            makeQuestion(Question.MazeScramblerIndicators, module, correctAnswers: new[] { positionNames[ind1Y * 3 + ind1X], positionNames[ind2Y * 3 + ind2X] }, preferredWrongAnswers: positionNames));
    }
}