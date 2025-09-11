using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBlindMaze
{
    [SouvenirQuestion("What color was the {1} button in {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Gray", "Yellow", TranslateAnswers = true, TranslateFormatArgs = [true], Arguments = ["north", "east", "west", "south"], ArgumentGroupSize = 1)]
    Colors,
    
    [SouvenirQuestion("Which maze did you solve {0} on?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    Maze
}

public partial class SouvenirModule
{
    [SouvenirHandler("BlindMaze", "Blind Maze", typeof(SBlindMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessBlindMaze(ModuleData module)
    {
        var comp = GetComponent(module, "BlindMaze");
        yield return WaitForSolve;

        // Despite the name “currentMaze”, this field actually contains the number of solved modules when Blind Maze was solved
        var numSolved = GetIntField(comp, "currentMaze").Get(v => v < 0 ? "negative" : null);
        var lastDigit = GetIntField(comp, "LastDigit").Get(min: 0, max: 9);
        var buttonColors = GetArrayField<int>(comp, "buttonColors").Get(expectedLength: 4, validator: bc => bc is < 0 or > 4 ? "expected 0–4" : null);

        var colorNames = new[] { "Red", "Green", "Blue", "Gray", "Yellow" };
        var buttonNames = new[] { "north", "east", "south", "west" };

        addQuestions(module,
            buttonColors.Select((col, ix) => makeQuestion(Question.BlindMazeColors, module, formatArgs: new[] { buttonNames[ix] }, correctAnswers: new[] { colorNames[col] }))
                .Concat(new[] { makeQuestion(Question.BlindMazeMaze, module, correctAnswers: new[] { ((numSolved + lastDigit) % 10).ToString() }) }));
    }
}