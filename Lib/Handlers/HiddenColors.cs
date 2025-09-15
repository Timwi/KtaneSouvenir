using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SHiddenColors
{
    [SouvenirQuestion("What was the color of the main LED in {0}?", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Magenta", "White", TranslateAnswers = true)]
    LED
}

public partial class SouvenirModule
{
    [SouvenirHandler("lgndHiddenColors", "Hidden Colors", typeof(SHiddenColors), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessHiddenColors(ModuleData module)
    {
        var comp = GetComponent(module, "HiddenColorsScript");

        var ledcolors = new[] { "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Magenta", "White" };
        var ledcolor = GetIntField(comp, "LEDColor").Get(min: 0, max: 7);
        var colors = GetArrayField<Material>(comp, "buttonColors", isPublic: true).Get();
        var led = GetField<Renderer>(comp, "LED", isPublic: true).Get();

        yield return WaitForSolve;

        if (colors.Length == 9)
            led.material = colors[8];
        yield return question(SHiddenColors.LED).Answers(ledcolors[ledcolor]);
    }
}