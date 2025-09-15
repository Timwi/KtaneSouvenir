using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFlashingArrows
{
    [SouvenirQuestion("What number was displayed on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99)]
    DisplayedValue,
    
    [SouvenirQuestion("What color flashed {1} black on the relevant arrow in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White", Arguments = ["before", "after"], ArgumentGroupSize = 1, TranslateAnswers = true, TranslateArguments = [true])]
    ReferredArrow
}

public partial class SouvenirModule
{
    [SouvenirHandler("flashingArrowsModule", "Flashing Arrows", typeof(SFlashingArrows), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessFlashingArrows(ModuleData module)
    {
        var comp = GetComponent(module, "FlashingArrowsScript");

        yield return WaitForSolve;

        var colorReference = GetArrayField<string>(comp, "debugColors").Get(expectedLength: 7);
        var displayedValue = GetField<int>(comp, "displayNumber").Get(num => num is < 0 or >= 100 ? "Expected the displayed value to be within 0 and 99 inclusive." : null);
        var idxReferencedArrow = GetField<int>(comp, "idxReferencedArrow").Get(num => num is < 0 or >= 4 ? "Expected the value to be within 0 and 3 inclusive." : null);
        var idxFlashedArrows = GetArrayField<int[]>(comp, "idxColorFlashingArrows").Get(expectedLength: 4);
        var arrowSet = idxFlashedArrows[idxReferencedArrow];
        var idxBlack = Array.IndexOf(arrowSet, -1);
        var colorAfterBlack = arrowSet[(idxBlack + 1) % 3];
        var colorBeforeBlack = arrowSet[(idxBlack + 2) % 3];

        addQuestions(module,
            makeQuestion(Question.FlashingArrowsDisplayedValue, module, correctAnswers: new[] { displayedValue.ToString() }),
            makeQuestion(Question.FlashingArrowsReferredArrow, module, formatArgs: new[] { "before" }, correctAnswers: new[] { colorReference[colorBeforeBlack] }, preferredWrongAnswers: colorReference),
            makeQuestion(Question.FlashingArrowsReferredArrow, module, formatArgs: new[] { "after" }, correctAnswers: new[] { colorReference[colorAfterBlack] }, preferredWrongAnswers: colorReference));
    }
}