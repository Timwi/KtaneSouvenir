using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SStability
{
    [SouvenirQuestion("What was the color of the {1} lit LED in {0}?", TwoColumns4Answers, "Red", "Yellow", "Blue", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    LedColors,
    
    [SouvenirQuestion("What was the identification number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9999, "0000")]
    IdNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("stabilityModule", "Stability", typeof(SStability), "NickLatkovich")]
    private IEnumerator<SouvenirInstruction> ProcessStability(ModuleData module)
    {
        var colorNames = new[] { "Red", "Yellow", "Blue" };

        var comp = GetComponent(module, "StabilityScript");
        yield return WaitForSolve;

        var qs = new List<QandA>();

        var litLedStates = GetArrayField<int>(comp, "ledStates").Get().Where(l => l != 5).ToArray();
        for (var i = 0; i < litLedStates.Length; i++)
            qs.Add(makeQuestion(Question.StabilityLedColors, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { colorNames[litLedStates[i]] }));

        if (litLedStates.Length > 3)
            qs.Add(makeQuestion(Question.StabilityIdNumber, module, correctAnswers: new[] { GetField<string>(comp, "idNumber").Get() }));

        addQuestions(module, qs);
    }
}