using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SButton
{
    [SouvenirQuestion("What color did the light glow in {0}?", TwoColumns4Answers, "red", "blue", "yellow", "white", TranslateAnswers = true)]
    LightColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("BigButton", "Button", typeof(SButton), "Andrio Celos", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessButton(ModuleData module)
    {
        var comp = GetComponent(module, "ButtonComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);
        var propLightColor = GetProperty<object>(comp, "IndicatorColor", true);
        var ledOff = GetField<GameObject>(comp, "LED_Off", true).Get();

        var color = -1;
        while (!fldSolved.Get())
        {
            color = ledOff.activeSelf ? -1 : (int) propLightColor.Get();
            yield return new WaitForSeconds(.1f);
        }
        module.SolveIndex = _modulesSolved.IncSafe("BigButton");
        if (color < 0)
            yield return legitimatelyNoQuestion(module, "The button was tapped (or I missed the light color).");

        var answer = color switch
        {
            0 => "red",
            1 => "blue",
            2 => "yellow",
            3 => "white",
            _ => throw new AbandonModuleException($"IndicatorColor is out of range ({color})."),
        };
        addQuestion(module, Question.ButtonLightColor, correctAnswers: new[] { answer });
    }
}