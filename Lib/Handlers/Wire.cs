using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWire
{
    [SouvenirQuestion("What was the color of the {1} dial in {0}?", ThreeColumns6Answers, "blue", "green", "grey", "orange", "purple", "red", TranslateAnswers = true, TranslateFormatArgs = [true], Arguments = ["top", "bottom-left", "bottom-right"], ArgumentGroupSize = 1)]
    DialColors,
    
    [SouvenirQuestion("What was the displayed number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    DisplayedNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("wire", "Wire", typeof(SWire), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessWire(ModuleData module)
    {
        var comp = GetComponent(module, "wireScript");
        yield return WaitForSolve;

        var dials = GetArrayField<Renderer>(comp, "renderers", isPublic: true).Get(expectedLength: 3);
        addQuestions(module,
            makeQuestion(Question.WireDialColors, module, formatArgs: new[] { "top" }, correctAnswers: new[] { dials[0].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDialColors, module, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { dials[1].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDialColors, module, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { dials[2].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDisplayedNumber, module, correctAnswers: new[] { GetIntField(comp, "displayedNumber").Get().ToString() }));
    }
}