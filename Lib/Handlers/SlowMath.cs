using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSlowMath
{
    [SouvenirQuestion("What was the last triplet of letters in {0}?", ThreeColumns6Answers, ExampleAnswers = ["ABC", "DEG", "KNP", "STX", "ZAB", "CDE", "GKN", "PST", "XZA", "BCD"])]
    [AnswerGenerator.Strings(3, "ABCDEGKNPSTXZ")]
    LastLetters
}

public partial class SouvenirModule
{
    [SouvenirHandler("SlowMathModule", "Slow Math", typeof(SSlowMath), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessSlowMath(ModuleData module)
    {
        var comp = GetComponent(module, "SlowMathScript");
        yield return WaitForSolve;

        var ogLetters = GetListField<string>(comp, "_chosenLetters").Get(minLength: 3, maxLength: 5);
        addQuestion(module, Question.SlowMathLastLetters, correctAnswers: new[] { ogLetters.Last() });
    }
}