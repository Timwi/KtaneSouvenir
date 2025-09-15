using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotTheButton
{
    [SouvenirQuestion("What colors did the light glow in {0}?", ThreeColumns6Answers, "white", "red", "yellow", "green", "blue", "white/red", "white/yellow", "white/green", "white/blue", "red/yellow", "red/green", "red/blue", "yellow/green", "yellow/blue", "green/blue", TranslateAnswers = true)]
    LightColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("NotButton", "Not The Button", typeof(SNotTheButton), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessNotTheButton(ModuleData module)
    {
        var comp = GetComponent(module, "NotButton");
        var propLightColour = GetProperty<object>(comp, "LightColour", isPublic: true); // actual type is an enum

        var lightColor = 0;
        while (module.Unsolved)
        {
            lightColor = (int) propLightColour.Get();   // casting boxed enum value to int
            yield return null;  // Don’t wait for .1 seconds so we don’t miss it
        }

        if (lightColor == 0)
            yield return legitimatelyNoQuestion(module, "The strip didn’t light up (or I missed the light color).");

        var strings = Question.NotTheButtonLightColor.GetAnswers();
        if (lightColor <= 0 || lightColor > strings.Length)
            throw new AbandonModuleException($"‘LightColour’ is out of range ({lightColor}).");
        yield return question(SNotTheButton.LightColor).Answers(strings[lightColor - 1]);
    }
}