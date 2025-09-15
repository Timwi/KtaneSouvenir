using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SConnectionCheck
{
    [SouvenirQuestion("What pair of numbers was present in {0}?", ThreeColumns6Answers, ExampleAnswers = ["2 1", "3 1", "4 1", "5 1", "6 1", "7 1", "8 1", "1 2", "3 2", "4 2", "5 2", "6 2", "7 2", "8 2", "1 3", "2 3", "4 3", "5 3", "6 3", "7 3", "8 3", "1 4", "2 4", "3 4", "5 4", "6 4", "7 4", "8 4", "1 5", "2 5", "3 5", "4 5", "6 5", "7 5", "8 5", "1 6", "2 6", "3 6", "4 6", "5 6", "7 6", "8 6", "1 7", "2 7", "3 7", "4 7", "5 7", "6 7", "8 7", "1 8", "2 8", "3 8", "4 8", "5 8", "6 8", "7 8"], ArgumentGroupSize = 1)]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("graphModule", "Connection Check", typeof(SConnectionCheck), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessConnectionCheck(ModuleData module)
    {
        var comp = GetComponent(module, "GraphModule");

        var valid = new float[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        var queries = GetArrayField<Vector2>(comp, "Queries")
            .Get(expectedLength: 4, validator: v =>
            !valid.Contains(v.x) ? $"x out of bounds (got: {v.x})" :
            !valid.Contains(v.y) ? $"y out of bounds (got: {v.y})" :
            v.y <= v.x ? $"y less than or equal to x (got: {v.x} {v.y})" : null);

        var allDigits = queries.SelectMany(v => new[] { (int) v.x, (int) v.y }).GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        for (var i = 1; i <= 8; i++)
            allDigits.TryAdd(i, 0);
        _connectionCheckDigitCounts.Add(allDigits);

        yield return WaitForSolve;

        IEnumerable<string> wrongAnswers()
        {
            for (var i = 1; i <= 8; i++)
            {
                for (var j = i + 1; j <= 8; j++)
                {
                    if (!queries.Any(q => q.x == i && q.y == j))
                    {
                        yield return $"{i} {j}";
                        yield return $"{j} {i}";
                    }
                }
            }
        }
        var wrong = wrongAnswers().ToArray();
        IEnumerable<QandA> qs()
        {
            foreach (var q in queries)
            {
                string format = null;
                var myWrong = wrong;
                var candidates = Enumerable.Range(1, 8).Where(i => q.x != i && q.y != i && _connectionCheckDigitCounts.Count(d => d[i] == allDigits[i]) == 1).ToArray();
                if (candidates.Any() && UnityEngine.Random.Range(0, 3) != 0)
                {
                    var which = candidates.PickRandom();
                    var count = allDigits[which];
                    var phrase = new[] { "the Connection Check with no {0}s", "the Connection Check with one {0}", "the Connection Check with two {0}s", "the Connection Check with three {0}s", "the Connection Check with four {0}s" }[count];
                    format = string.Format(translateString(Question.ConnectionCheckNumbers, phrase), which);
                    if (count == 0)
                        myWrong = myWrong.Where(s => !s.Contains(which.ToString())).ToArray();
                }
                yield return makeQuestion(Question.ConnectionCheckNumbers, module, formattedModuleName: format, correctAnswers: new[] { $"{q.x} {q.y}", $"{q.y} {q.x}" }, preferredWrongAnswers: myWrong);
            }
        }
        addQuestions(module, qs());

        var L = GetArrayField<GameObject>(comp, "L", true).Get(expectedLength: 4);
        var R = GetArrayField<GameObject>(comp, "R", true).Get(expectedLength: 4);
        IEnumerator removeDisplays()
        {
            foreach (var num in Enumerable.Range(0, 4).SelectMany(i => new[] { L[i], R[i] }))
            {
                num.GetComponentInChildren<TextMesh>().text = "!";
                yield return new WaitForSeconds(.1f);
            }
        }
        StartCoroutine(removeDisplays());
    }
}
