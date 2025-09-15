using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SKnowYourWay
{
    [SouvenirQuestion("Which way was the arrow pointing in {0}?", TwoColumns4Answers, "Up", "Down", "Left", "Right", TranslateAnswers = true)]
    Arrow,

    [SouvenirQuestion("Which LED was green in {0}?", TwoColumns4Answers, "Top", "Bottom", "Right", "Left", TranslateAnswers = true)]
    Led
}

public partial class SouvenirModule
{
    [SouvenirHandler("KnowYourWay", "Know Your Way", typeof(SKnowYourWay), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessKnowYourWay(ModuleData module)
    {
        var comp = GetComponent(module, "KnowYourWayScript");

        yield return WaitForSolve;

        var ledIndex = GetIntField(comp, "LEDLoc").Get(min: 0, max: 3);
        var arrowIndex = GetIntField(comp, "ArrowLoc").Get(min: 0, max: 3);
        GetArrayField<GameObject>(comp, "Bars", isPublic: true)
            .Get(expectedLength: 4)[ledIndex]
            .GetComponent<MeshRenderer>().material = GetArrayField<Material>(comp, "LEDs", isPublic: true).Get(expectedLength: 2)[0];

        yield return question(SKnowYourWay.Arrow).Answers(new[] { "Up", "Left", "Down", "Right" }[arrowIndex]);
        yield return question(SKnowYourWay.Led).Answers(new[] { "Top", "Left", "Bottom", "Right" }[ledIndex]);
    }
}