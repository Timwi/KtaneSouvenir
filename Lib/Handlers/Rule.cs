using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SRule
{
    [SouvenirQuestion("What was the rule number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 15)]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("theRule", "Rule", typeof(SRule), "TasThiluna", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessRule(ModuleData module)
    {
        var comp = GetComponent(module, "TheRuleScript");

        yield return WaitForSolve;

        addQuestion(module, Question.RuleNumber, correctAnswers: new[] { GetIntField(comp, "ruleNumber").Get(min: 0, max: 15).ToString() });
    }
}