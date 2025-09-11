using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBooleanWires
{
    [SouvenirQuestion("Which operator did you submit in the {1} stage of {0}?", TwoColumns4Answers, "OR", "XOR", "AND", "NAND", "NOR", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    EnteredOperators
}

public partial class SouvenirModule
{
    [SouvenirHandler("booleanWires", "Boolean Wires", typeof(SBooleanWires), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessBooleanWires(ModuleData module)
    {
        var comp = GetComponent(module, "BooleanWiresScript");

        yield return WaitForSolve;

        var operators = GetListField<string>(comp, "Entered", isPublic: true).Get(expectedLength: 10);
        var qs = new List<QandA>();
        for (var pos = 0; pos < 5; pos++)
            qs.Add(makeQuestion(Question.BooleanWiresEnteredOperators, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { operators[2 * pos] }));
        addQuestions(module, qs);
    }
}