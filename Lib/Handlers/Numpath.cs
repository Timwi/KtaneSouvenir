using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNumpath
{
    [Question("What was the color of the number on {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", TranslateAnswers = true)]
    Color,

    [Question("What was the number displayed on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 9)]
    Digit
}

public partial class SouvenirModule
{
    [Handler("numpath", "Numpath", typeof(SNumpath), "tandyCake")]
    [ManualQuestion("What was the number and its color?")]
    private IEnumerator<SouvenirInstruction> ProcessNumpath(ModuleData module)
    {
        var comp = GetComponent(module, "NumpathScript");
        var disp = GetField<TextMesh>(comp, "screen", isPublic: true).Get().text;
        var color = GetIntField(comp, "colorIndex").Get();

        yield return WaitForSolve;

        var colorNames = new[] { "Red", "Green", "Blue", "Yellow", "Purple", "Orange" };
        yield return question(SNumpath.Color).Answers(colorNames[color]);
        yield return question(SNumpath.Digit).Answers(disp);
    }
}