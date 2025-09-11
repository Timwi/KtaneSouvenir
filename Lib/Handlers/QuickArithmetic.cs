using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SQuickArithmetic
{
    [SouvenirQuestion("What was the {1} color in the primary sequence in {0}?", ThreeColumns6Answers, "red", "blue", "green", "yellow", "white", "black", "orange", "pink", "purple", "cyan", "brown", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors,
    
    [SouvenirQuestion("What was the {1} digit in the {2} sequence in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, "primary", QandA.Ordinal, "secondary"], ArgumentGroupSize = 2, TranslateFormatArgs = [false, true])]
    [AnswerGenerator.Integers(0, 9)]
    PrimSecDigits
}

public partial class SouvenirModule
{
    [SouvenirHandler("QuickArithmetic", "Quick Arithmetic", typeof(SQuickArithmetic), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessQuickArithmetic(ModuleData module)
    {
        var comp = GetComponent(module, "QuickArithmetic");
        yield return WaitForSolve;

        var seqColors = GetArrayField<int>(comp, "ColorSequence").Get(expectedLength: 8);
        var primSeqDigits = GetArrayField<int>(comp, "LeftSequenceN").Get(expectedLength: 8);
        var secSeqDigits = GetArrayField<int>(comp, "RightSequence").Get(expectedLength: 8);
        var colorRef = new[] { "red", "blue", "green", "yellow", "white", "black", "orange", "pink", "purple", "cyan", "brown" };
        var allQuestions = new List<QandA>();
        for (var x = 0; x < 8; x++)
        {
            allQuestions.Add(makeQuestion(Question.QuickArithmeticColors, module, formatArgs: new[] { Ordinal(x + 1) }, correctAnswers: new[] { colorRef[seqColors[x]] }, preferredWrongAnswers: colorRef));
            allQuestions.Add(makeQuestion(Question.QuickArithmeticPrimSecDigits, module, formatArgs: new[] { Ordinal(x + 1), "primary" }, correctAnswers: new[] { primSeqDigits[x].ToString() }));
            allQuestions.Add(makeQuestion(Question.QuickArithmeticPrimSecDigits, module, formatArgs: new[] { Ordinal(x + 1), "secondary" }, correctAnswers: new[] { secSeqDigits[x].ToString() }));
        }

        addQuestions(module, allQuestions);
    }
}