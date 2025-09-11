using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SColorOneTwo
{
    [SouvenirQuestion("What color was the {1} LED in {0}?", TwoColumns4Answers, "Red", "Blue", "Green", "Yellow", TranslateAnswers = true, Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("colorOneTwo", "Color One Two", typeof(SColorOneTwo), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessColorOneTwo(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "colorOneTwoScript");
        var left = GetIntField(comp, "leftLEDColor").Get(0, 3);
        var right = GetIntField(comp, "rightLEDColor").Get(0, 3);
        var colors = new[] { "Red", "Blue", "Green", "Yellow" };
        addQuestions(module,
            makeQuestion(Question.ColorOneTwoColor, module, formatArgs: new[] { "left" }, correctAnswers: new[] { colors[left] }),
            makeQuestion(Question.ColorOneTwoColor, module, formatArgs: new[] { "right" }, correctAnswers: new[] { colors[right] }));
    }
}