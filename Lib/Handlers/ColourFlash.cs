using System.Collections.Generic;
using System.Linq;
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

        var fldColorSequence = GetArrayField<object>(comp, "_colourSequence").Get(ar => ar.Length != 8 ? "expected length 8" : null);
        var colorValue = GetField<object>(fldColorSequence.GetValue(7), "ColourValue", isPublic: true).Get();

        addQuestion(module, Question.ColourFlashLastColor, correctAnswers: new[] { colorValue.ToString() });
    }
}