using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCrypticKeypad
{
    [SouvenirQuestion("What was the label of the {1} key in {0}?", ThreeColumns6Answers, Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    [AnswerGenerator.Strings("A-Z")]
    Labels,
    
    [SouvenirQuestion("Which cardinal direction was the {1} key rotated to in {0}?", TwoColumns4Answers, "North", "East", "South", "West", TranslateAnswers = true, Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    Rotations
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSCrypticKeypad", "Cryptic Keypad", typeof(SCrypticKeypad), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessCrypticKeypad(ModuleData module)
    {
        var comp = GetComponent(module, "CrypticKeypadScript");
        yield return WaitForSolve;

        var letters = GetArrayField<string>(comp, "Letters2").Get();
        var rotations = GetArrayField<int>(comp, "Rotations").Get();

        var qs = new List<QandA>();
        var directions = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };
        var cardinalDirections = new[] { "North", "East", "South", "West" };
        for (var i = 0; i < 4; i++)
        {
            qs.Add(makeQuestion(Question.CrypticKeypadLabels, module, formatArgs: new[] { directions[i] }, correctAnswers: new[] { letters[i] }));
            qs.Add(makeQuestion(Question.CrypticKeypadRotations, module, formatArgs: new[] { directions[i] }, correctAnswers: new[] { cardinalDirections[rotations[i]] }));
        }
        addQuestions(module, qs);
    }
}