using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSetTheory
{
    [SouvenirQuestion("What equation was shown in the {1} stage of {0}?", OneColumn4Answers, ExampleAnswers = ["(A ∩ B)", "(A ∪ B)", "(!B ∆ !A)", "(B ∩ !A)", "(!(C − B) ∪ !A)", "((B ∩ A) − C)", "(!(B ∪ A) ∆ (C ∩ !B))", "((A − !C) ∩ !(B ∪ !C))"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Equations
}

public partial class SouvenirModule
{
    [SouvenirHandler("SetTheory", "S.E.T. Theory", typeof(SSetTheory), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSetTheory(ModuleData module)
    {
        var comp = GetComponent(module, "SetTheoryScript");
        var fldEquations = GetField<Array>(comp, "_equations");
        var mthGenerate = GetMethod<object>(comp, "GenerateEquationForStage", 1);

        yield return WaitForSolve;

        var equations = fldEquations.Get(v => v.Length != 4 ? "expected length 4" : null).Cast<object>().Select(eq => eq.ToString()).ToArray();
        addQuestions(module, Enumerable.Range(0, 4).Select(stage =>
        {
            var wrongAnswers = new HashSet<string> { equations[stage] };
            while (wrongAnswers.Count < 4)
                wrongAnswers.Add(mthGenerate.Invoke(stage).ToString());
            return makeQuestion(Question.SetTheoryEquations, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { equations[stage] }, preferredWrongAnswers: wrongAnswers.ToArray());
        }));
    }
}