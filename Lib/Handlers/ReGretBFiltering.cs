using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SReGretBFiltering
{
    [Question("Which calculation was used for the {1} stage of {0}?", ThreeColumns6Answers, "+", "×", "÷", "⊻", "∧", "∨", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Operator
}

public partial class SouvenirModule
{
    [Handler("regretbFiltering", "ReGret-B Filtering", typeof(SReGretBFiltering), "Anonymous")]
    [ManualQuestion("Which calculations were used?")]
    private IEnumerator<SouvenirInstruction> ProcessReGretBFiltering(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "regretScript");
        var operators = GetArrayField<int>(comp, "operators").Get(expectedLength: 3, validator: v => v is > 5 or < 0 ? "Out of range [0, 5]" : null);

        for (var i = 0; i < operators.Length; i++)
            yield return question(SReGretBFiltering.Operator, args: [Ordinal(i + 1)]).Answers(SReGretBFiltering.Operator.GetAnswers()[operators[i]]);
    }
}