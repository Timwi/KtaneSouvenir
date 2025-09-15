using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SChineseCounting
{
    [SouvenirQuestion("What color was the {1} LED in {0}?", TwoColumns4Answers, "White", "Red", "Green", "Orange", TranslateAnswers = true, TranslateArguments = [true], Arguments = ["left", "right"], ArgumentGroupSize = 1)]
    LED
}

public partial class SouvenirModule
{
    [SouvenirHandler("chineseCounting", "Chinese Counting", typeof(SChineseCounting), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessChineseCounting(ModuleData module)
    {
        var comp = GetComponent(module, "chineseCounting");
        yield return WaitForSolve;

        var ledIndices = GetArrayField<int>(comp, "ledIndices").Get(expectedLength: 2, validator: ix => ix is < 0 or > 3 ? "expected range 0–3" : null);
        var ledColors = new[] { "White", "Red", "Green", "Orange" };

        addQuestions(module,
          makeQuestion(Question.ChineseCountingLED, module, formatArgs: new[] { "left" }, correctAnswers: new[] { ledColors[ledIndices[0]] }),
          makeQuestion(Question.ChineseCountingLED, module, formatArgs: new[] { "right" }, correctAnswers: new[] { ledColors[ledIndices[1]] }));
    }
}