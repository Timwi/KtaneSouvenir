using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHereditaryBaseNotation
{
    [SouvenirQuestion("What was the given number in {0}?", TwoColumns4Answers, ExampleAnswers = ["12", "33", "46", "112", "356"])]
    InitialNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("hereditaryBaseNotationModule", "Hereditary Base Notation", typeof(SHereditaryBaseNotation), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessHereditaryBaseNotation(ModuleData module)
    {
        var comp = GetComponent(module, "hereditaryBaseNotationScript");
        var mthNumberToBaseNString = GetMethod<string>(comp, "numberToBaseNString", numParameters: 2);

        yield return WaitForSolve;

        var baseN = GetIntField(comp, "baseN").Get(3, 7);
        var upperBound = new[] { 19682, 60000, 80000, 100000, 100000 }[baseN - 3];
        var initialNum = GetIntField(comp, "initialNumber").Get(1, upperBound);

        var answer = mthNumberToBaseNString.Invoke(baseN, initialNum).ToString();
        var invalidAnswer = new HashSet<string> { answer };

        // Generate fake options in the same base of the answer
        while (invalidAnswer.Count() < 4)
            invalidAnswer.Add(mthNumberToBaseNString.Invoke(baseN, Rnd.Range(1, upperBound + 1)).ToString());

        yield return question(SHereditaryBaseNotation.InitialNumber).Answers(answer, preferredWrong: invalidAnswer.ToArray());
    }
}