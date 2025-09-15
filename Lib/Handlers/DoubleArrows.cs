using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDoubleArrows
{
    [SouvenirQuestion("What was the starting position in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 81, "00")]
    Start,

    [SouvenirQuestion("Which direction in the grid did the {1} arrow move in {0}?", TwoColumns4Answers, "Up", "Right", "Left", "Down", Arguments = ["inner up", "inner down", "inner left", "inner right", "outer up", "outer down", "outer left", "outer right"], TranslateAnswers = true, ArgumentGroupSize = 1, TranslateArguments = [true])]
    Movement,

    [SouvenirQuestion("Which {1} arrow moved {2} in the grid in {0}?", TwoColumns4Answers, "Up", "Right", "Left", "Down", Arguments = ["inner", "up", "outer", "up", "inner", "down", "outer", "down", "inner", "left", "outer", "left", "inner", "right", "outer", "right"], TranslateAnswers = true, ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    Arrow
}

public partial class SouvenirModule
{
    [SouvenirHandler("doubleArrows", "Double Arrows", typeof(SDoubleArrows), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleArrows(ModuleData module)
    {
        var comp = GetComponent(module, "DoubleArrowsScript");
        var fldPresses = GetField<int>(comp, "pressCount");
        var display = GetField<TextMesh>(comp, "disp", true).Get();
        var start = "";

        while (module.Unsolved)
        {
            if (display.text.Length == 2)
                start = display.text; // This resets on a strike.
            yield return new WaitForSeconds(.1f);
        }

        var qs = new List<QandA>(17) { makeQuestion(Question.DoubleArrowsStart, module, correctAnswers: new[] { start }) };
        var callib = GetArrayField<int[]>(comp, "callib").Get(expectedLength: 2);
        var dirs = new[] { "Left", "Up", "Right", "Down" };
        for (var i = 0; i < 8; i++)
        {
            qs.Add(makeQuestion(Question.DoubleArrowsMovement, module, formatArgs: new[] { $"{(i < 4 ? "inner" : "outer")} {dirs[i % 4].ToLowerInvariant()}" }, correctAnswers: new[] { dirs[callib[i / 4][i % 4]] }));
            qs.Add(makeQuestion(Question.DoubleArrowsArrow, module, formatArgs: new[] { i < 4 ? "inner" : "outer", dirs[callib[i / 4][i % 4]].ToLowerInvariant() }, correctAnswers: new[] { dirs[i % 4] }));
        }

        addQuestions(module, qs);
    }
}