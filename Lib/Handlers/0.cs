using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S0
{
    [SouvenirQuestion("What was the initially displayed number in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Integers(100000000, 999999999)]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("0", "0", typeof(S0), "Anonymous")]
    private IEnumerator<SouvenirInstruction> Process0(ModuleData module)
    {
        var comp = GetComponent(module, "pruzZero");
        var solution = GetField<string>(comp, "number").Get(v => v.Length != 9 || !v.All("0123456789".Contains) ? "Expected 9 digits" : null);

        yield return WaitForSolve;

        addQuestion(module, Question._0Number, correctAnswers: new[] { solution });
    }
}
