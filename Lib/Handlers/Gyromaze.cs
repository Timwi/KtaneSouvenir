using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGyromaze
{
    [SouvenirQuestion("What color was the {1} LED in {0}?", TwoColumns4Answers, "Red", "Blue", "Green", "Yellow", Arguments = ["top", "bottom"], ArgumentGroupSize = 1, TranslateAnswers = true, TranslateArguments = [true])]
    LEDColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("gyromaze", "Gyromaze", typeof(SGyromaze), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessGyromaze(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "GyromazeScript");

        var leds = GetArrayField<MeshRenderer>(comp, "leds", true).Get(expectedLength: 2);
        foreach (var l in leds)
            l.material.color = Color.black;

        var colorNames = new[] { "Red", "Blue", "Green", "Yellow" };
        var endPos = GetIntField(comp, "endPos").Get(0, 15);

        addQuestions(module,
            makeQuestion(Question.GyromazeLEDColor, module, correctAnswers: new[] { colorNames[endPos % 4] }, formatArgs: new[] { "top" }),
            makeQuestion(Question.GyromazeLEDColor, module, correctAnswers: new[] { colorNames[endPos / 4] }, formatArgs: new[] { "bottom" })
            );
    }
}