using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWire
{
    [SouvenirQuestion("What was the color of the {1} dial in {0}?", ThreeColumns6Answers, "blue", "green", "grey", "orange", "purple", "red", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["top", "bottom-left", "bottom-right"], ArgumentGroupSize = 1)]
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
        yield return question(SWire.DialColors, args: ["top"]).Answers(dials[0].material.mainTexture.name.Replace("Mat", ""));
        yield return question(SWire.DialColors, args: ["bottom-left"]).Answers(dials[1].material.mainTexture.name.Replace("Mat", ""));
        yield return question(SWire.DialColors, args: ["bottom-right"]).Answers(dials[2].material.mainTexture.name.Replace("Mat", ""));
        yield return question(SWire.DisplayedNumber).Answers(GetIntField(comp, "displayedNumber").Get().ToString());
    }
}