using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SColourFlash
{
    [SouvenirQuestion("What was the color of the last word in the sequence in {0}?", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Magenta", "White", TranslateAnswers = true)]
    LastColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("ColourFlash", "Colour Flash", typeof(SColourFlash), "LotsOfS")]
    private IEnumerator<SouvenirInstruction> ProcessColourFlash(ModuleData module)
    {
        var comp = GetComponent(module, "ColourFlashModule");

        yield return WaitForSolve;

        var display = GetField<TextMesh>(comp, "Indicator", isPublic: true).Get();

        if (display.text.Length == 0)
            yield return legitimatelyNoQuestion(module, "The module was submitted while the display was blank, and therefore it could be determined that the last word's color was white.");

        var fldColorSequence = GetArrayField<object>(comp, "_colourSequence").Get(ar => ar.Length != 8 ? "expected length 8" : null);
        var colorValue = GetField<object>(fldColorSequence.GetValue(7), "ColourValue", isPublic: true).Get();

        yield return question(SColourFlash.LastColor).Answers(colorValue.ToString());
    }
}