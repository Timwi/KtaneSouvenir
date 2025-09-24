using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLinq
{
    [SouvenirQuestion("What was the {1} function in {0}?", ThreeColumns6Answers, "First", "Last", "Min", "Max", "Distinct", "Skip", "SkipLast", "Take", "TakeLast", "ElementAt", "Except", "Intersect", "Concat", "Append", "Prepend", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Function,

    [SouvenirDiscriminator("the Linq whose {0} function was {1}", Arguments = [QandA.Ordinal, "First", QandA.Ordinal, "Last", QandA.Ordinal, "Min", QandA.Ordinal, "Max"], ArgumentGroupSize = 2)]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("Linq", "Linq", typeof(SLinq), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessLinq(ModuleData module)
    {
        var comp = GetComponent(module, "LinqScript");

        var select = GetField<object>(comp, "select").Get();
        var functions = GetField<Array>(select, "functions")
            .Get(ar => ar.Length != 3 ? "expected length 3" : ar.Cast<object>().Any(v => !SLinq.Function.GetAnswers().Contains(v.ToString())) ? "contains unknown function" : null)
            .Cast<object>().Select(v => v.ToString()).ToArray();

        for (var stage = 0; stage < 3; stage++)
            yield return new Discriminator(SLinq.Discriminator, $"stage{stage}", functions[stage], [Ordinal(stage + 1), functions[stage]]);

        yield return WaitForSolve;

        for (var stage = 0; stage < 3; stage++)
            yield return question(SLinq.Function, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"stage{stage}").Answers(functions[stage]);
    }
}
