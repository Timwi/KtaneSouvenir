using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNumpath
{
    [SouvenirQuestion("What was the color of the number on {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", TranslateAnswers = true)]
    Color,

    [SouvenirQuestion("What was the number displayed on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 9)]
    Digit
}

public partial class SouvenirModule
{
    [SouvenirHandler("numpath", "Numpath", typeof(SNumpath), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessNumpath(ModuleData module)
    {
        var comp = GetComponent(module, "NumpathScript");
        var disp = GetField<TextMesh>(comp, "screen", isPublic: true).Get().text;
        var color = GetIntField(comp, "colorIndex").Get();

        yield return WaitForSolve;

        var colorNames = new[] { "Red", "Green", "Blue", "Yellow", "Purple", "Orange" };
        addQuestions(module,
            makeQuestion(Question.NumpathColor, module, correctAnswers: new[] { colorNames[color] }),
            makeQuestion(Question.NumpathDigit, module, correctAnswers: new[] { disp }));
    }
}