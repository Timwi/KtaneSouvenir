using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNegativity
{
    [SouvenirQuestion("In base 10, what was the value submitted in {0}?", ThreeColumns6Answers, ExampleAnswers = ["0", "9990", "-9990", "-1234", "5678", "-90"])]
    SubmittedValue,
    
    [SouvenirQuestion("Excluding 0s, what was the submitted balanced ternary in {0}?", TwoColumns4Answers, ExampleAnswers = ["+-", "-++", "++-+-", "++++-", "-----", "+-----++++"])]
    SubmittedTernary
}

public partial class SouvenirModule
{
    [SouvenirHandler("Negativity", "Negativity", typeof(SNegativity), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessNegativity(ModuleData module)
    {
        var comp = GetComponent(module, "NegativityScript");
        yield return WaitForSolve;

        var convertedNums = GetArrayField<int>(comp, "NumberingConverted").Get();
        var expectedTotal = GetField<int>(comp, "Totale").Get();
        var submittedTernary = GetField<string>(comp, "Tables").Get(str => str.Any(ch => !"+-".Contains(ch)) ? "At least 1 character from the submitted ternary is not familar. (Accepted: '+','-')" : null);

        // Generate possible incorrect answers for this module
        var incorrectValues = new HashSet<int>();
        while (incorrectValues.Count < 5)
        {
            var sumPossible = 0;
            for (var i = 0; i < convertedNums.Length; i++)
            {
                var aValue = convertedNums[i];
                if (Rnd.Range(0, 2) != 0)
                    sumPossible += aValue;
                else
                    sumPossible -= aValue;
            }
            if (sumPossible != expectedTotal)
                incorrectValues.Add(sumPossible);
        }

        var incorrectSubmittedTernary = new HashSet<string>();
        while (incorrectSubmittedTernary.Count < 5)
        {
            var onePossible = "";
            var wantedLength = Rnd.Range(Mathf.Max(2, submittedTernary.Length - 1), Mathf.Min(11, Mathf.Max(submittedTernary.Length + 1, 5)));
            for (var x = 0; x < wantedLength; x++)
                onePossible += "+-".PickRandom();
            if (onePossible != submittedTernary)
                incorrectSubmittedTernary.Add(onePossible);
        }

        addQuestions(module,
            makeQuestion(Question.NegativitySubmittedValue, module, formatArgs: null, correctAnswers: new[] { expectedTotal.ToString() }, preferredWrongAnswers: incorrectValues.Select(a => a.ToString()).ToArray()),
            makeQuestion(Question.NegativitySubmittedTernary, module, formatArgs: null, correctAnswers: new[] { string.IsNullOrEmpty(submittedTernary) ? "(empty)" : submittedTernary }, preferredWrongAnswers: incorrectSubmittedTernary.ToArray()));
    }
}