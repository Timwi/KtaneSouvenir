using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNotPerspectivePegs
{
    [SouvenirQuestion("What was the position of the {1} flashing peg on {0}?", ThreeColumns6Answers, "top", "top-right", "bottom-right", "bottom-left", "top-left", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Position,
    
    [SouvenirQuestion("From what perspective did the {1} peg flash on {0}?", ThreeColumns6Answers, "top", "top-right", "bottom-right", "bottom-left", "top-left", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Perspective,
    
    [SouvenirQuestion("What was the color of the {1} flashing peg on {0}?", ThreeColumns6Answers, "blue", "green", "purple", "red", "yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("NotPerspectivePegsModule", "Not Perspective Pegs", typeof(SNotPerspectivePegs), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNotPerspectivePegs(ModuleData module)
    {
        var comp = GetComponent(module, "NotPerspectivePegsScript");
        yield return WaitForSolve;
        var posNames = new[] { "top", "top-right", "bottom-right", "bottom-left", "top-left" };
        // Peg position
        var positions = GetArrayField<int>(comp, "_flashPegPosition").Get();
        var qs = new List<QandA>();
        for (var i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.NotPerspectivePegsPosition, module, formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { posNames[positions[i]] }));
        // Peg perspective
        var perspectives = GetArrayField<int>(comp, "_flashPegPerspective").Get();
        for (var i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.NotPerspectivePegsPerspective, module, formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { posNames[perspectives[i]] }));
        // Peg color
        var colors = GetArrayField<int>(comp, "_flashPegColor").Get();
        var colorNames = new[] { "blue", "green", "purple", "red", "yellow" };
        for (var i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.NotPerspectivePegsColor, module, formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { colorNames[colors[i]] }));
        addQuestions(module, qs);
    }
}