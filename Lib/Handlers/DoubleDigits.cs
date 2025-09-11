using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDoubleDigits
{
    [SouvenirQuestion("What was the digit on the {1} display in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    Displays
}

public partial class SouvenirModule
{
    [SouvenirHandler("doubleDigitsModule", "Double Digits", typeof(SDoubleDigits), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleDigits(ModuleData module)
    {
        var comp = GetComponent(module, "DoubleDigitsScript");
        yield return WaitForSolve;

        var d = GetArrayField<int>(comp, "digits").Get();
        var digits = Enumerable.Range(0, d.Length).Select(str => d[str].ToString()).ToArray();

        addQuestions(module,
            makeQuestion(Question.DoubleDigitsDisplays, module, formatArgs: new[] { "left" }, correctAnswers: new[] { digits[0] }),
            makeQuestion(Question.DoubleDigitsDisplays, module, formatArgs: new[] { "right" }, correctAnswers: new[] { digits[1] }));
    }
}