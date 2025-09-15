using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum S7
{
    [SouvenirQuestion("What was the {1} channel’s initial value in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(-9, 9)]
    InitialValues,
    
    [SouvenirQuestion("What LED color was shown in stage {1} of {0}?", TwoColumns4Answers, "red", "blue", "green", "white", Arguments = ["0", "1", "2", "3"], ArgumentGroupSize = 1, TranslateAnswers = true)]
    LedColors
}

public partial class SouvenirModule
{
    [SouvenirHandler("7", "7", typeof(S7), "VFlyer")]
    private IEnumerator<SouvenirInstruction> Process7(ModuleData module)
    {
        var comp = GetComponent(module, "SevenHandler");
        yield return WaitForSolve;

        var allDisplayedValues = GetListField<int[]>(comp, "displayedValues")
            .Get(stg => stg.Any(a => a.Length != 3) ? "at least 1 stage’s array does not have exactly a length of 3" : null);

        // Check if all of the stages have exactly 3 sets of values.
        var allIdxDisplayedOperators = GetListField<int>(comp, "idxOperations").Get(
            idx => !idx.Skip(1).All(a => a is >= 0 and <= 3) ? "After stage 0, at least 1 stage does not have a valid index between 0 and 3 inclusive" : // Check after stage 0 if all indexes are within 0-3 inclusive
            !(idx.First() == -1) ? "Stage 0 does not have an index of -1." : // Then check if stage 0 has an idx of -1.
            null);

        var allQuestions = new List<QandA>();

        var colorReference = new[] { "red", "green", "blue", "white" };

        for (var x = 0; x < allDisplayedValues.Count; x++)
        {
            if (x == 0) // Stage 0 is denoted as the initial stage on this module.
            {
                for (var y = 0; y < 3; y++)
                    allQuestions.Add(makeQuestion(Question._7InitialValues, module, formatArgs: new[] { colorReference[y] }, correctAnswers: new[] { allDisplayedValues[x][y].ToString() }));
            }
            else
                allQuestions.Add(makeQuestion(Question._7LedColors, module, formatArgs: new[] { x.ToString() }, correctAnswers: new[] { colorReference[allIdxDisplayedOperators[x]] }, preferredWrongAnswers: colorReference));
        }

        addQuestions(module, allQuestions.ToArray());
    }
}
