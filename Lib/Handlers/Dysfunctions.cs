using System.Collections.Generic;
using System.Linq;
using Souvenir;
using System.Text.RegularExpressions;
using static Souvenir.AnswerLayout;


public enum SDysfunctions
{
    [Question("What was the {1} factor in {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, ExampleAnswers = ["0000", "1111", "2222", "3333", "4444", "5555"])]
    [AnswerGenerator.Strings(4, "012345")]
    Factors,
    [Question("What was the digit restriction in {0}?", TwoColumns2Answers, "+", "-")]
    DigitRestriction, 
    [Question("What was the divisibility restriction in {0}?", TwoColumns4Answers, ExampleAnswers = ["00", "11", "22", "33", "44", "55"])]
    [AnswerGenerator.Strings(2, "012345")]
    DivisibilityRestriction

}

public partial class SouvenirModule
{
    [Handler("lvDysfunctions", "Dysfunctions", typeof(SDysfunctions), "LivreVerte")]
    [ManualQuestion("What were the factors and restrictions?")]
    private IEnumerator<SouvenirInstruction> ProcessDysfunctions(ModuleData module)
    {

        var comp = GetComponent(module, "DysfunctionsScript");
        var factors = GetArrayField<string>(comp, "Souv").Get(expectedLength: 2);
        var digitRest = GetIntField(comp, "sign").Get();
        string sign;
        var divisibility = GetIntField(comp, "restriction").Get();
        var divRest = ((divisibility/6)).ToString()+(divisibility%6).ToString();
        if(digitRest == 0){
            sign = "-";
        } else {
            sign = "+";
        }
        yield return WaitForSolve;

        yield return question(SDysfunctions.Factors, args: [Ordinal(1)]).Answers(factors[0]);
        yield return question(SDysfunctions.Factors, args: [Ordinal(2)]).Answers(factors[1]);
        yield return question(SDysfunctions.DigitRestriction).Answers(sign);
        yield return question(SDysfunctions.DivisibilityRestriction).Answers(divRest);
    }
}
