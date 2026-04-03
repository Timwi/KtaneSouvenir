using System.Collections.Generic;
using Souvenir;

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
    [SouvenirManualQuestion("What was the rule number?")]
    private IEnumerator<SouvenirInstruction> ProcessRule(ModuleData module)
    {
        var comp = GetComponent(module, "TheRuleScript");

        yield return WaitForSolve;

        var rowButtons = GetArrayField<KMSelectable>(comp, "rowButtons", isPublic: true).Get();
        var submitButton = GetField<KMSelectable>(comp, "buttonS", isPublic: true).Get();

        foreach (var b in rowButtons)
            b.OnInteract = delegate () { return false; };
        submitButton.OnInteract = delegate () { submitButton.AddInteractionPunch(); return false; };

        yield return question(SRule.Number).Answers(GetIntField(comp, "ruleNumber").Get(min: 0, max: 15).ToString());
    }
}