using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SKeypadMagnified
{
    [SouvenirQuestion("What was the position of the LED in {0}?", TwoColumns4Answers, "Top-left", "Top-right", "Bottom-left", "Bottom-right", TranslateAnswers = true)]
    LED
}

public partial class SouvenirModule
{
    [SouvenirHandler("keypadMagnified", "Keypad Magnified", typeof(SKeypadMagnified), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessKeypadMagnified(ModuleData module)
    {
        var comp = GetComponent(module, "KeypadMagnifiedScript");

        var LEDPos = GetIntField(comp, "chosenPosition").Get(min: 0, max: 3);
        yield return WaitForSolve;

        var posNames = new[] { "Top-left", "Top-right", "Bottom-left", "Bottom-right" };
        addQuestion(module, Question.KeypadMagnifiedLED, correctAnswers: new[] { posNames[LEDPos] });
    }
}