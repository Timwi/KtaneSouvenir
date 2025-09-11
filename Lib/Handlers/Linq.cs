using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLinq
{
    [SouvenirQuestion("What was the {1} function in {0}?", ThreeColumns6Answers, "First", "Last", "Min", "Max", "Distinct", "Skip", "SkipLast", "Take", "TakeLast", "ElementAt", "Except", "Intersect", "Concat", "Append", "Prepend", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslatableStrings = ["the Linq whose {0} function was {1}"])]
    Function
}

public partial class SouvenirModule
{
    [SouvenirHandler("Linq", "Linq", typeof(SLinq), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessLinq(ModuleData module)
    {
        var comp = GetComponent(module, "LinqScript");

        var select = GetField<object>(comp, "select").Get();
        var functions = GetField<Array>(select, "functions").Get(ar =>
                ar.Length != 3 ? "expected length 3" :
                ar.OfType<object>().Any(v => !Question.LinqFunction.GetAnswers().Contains(v.ToString())) ? "contains unknown function" : null)
            .OfType<object>().Select(v => v.ToString()).ToArray();
        _linqFunctions.Add(functions);

        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (var i = 0; i < functions.Length; i++)
        {
            string format = null;
            var stages = Enumerable.Range(0, 2).Where(s => s != i && _linqFunctions.Count(f => f[s] == functions[s]) == 1).ToArray();
            if (stages.Any() && UnityEngine.Random.Range(0, 2) != 0)
            {
                var stage = stages.PickRandom();
                format = string.Format(translateString(Question.LinqFunction, "the Linq whose {0} function was {1}"), Ordinal(stage + 1), functions[stage]);
            }
            qs.Add(makeQuestion(Question.LinqFunction, module, formattedModuleName: format, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { functions[i].ToString() }));
        }

        addQuestions(module, qs);
    }
}