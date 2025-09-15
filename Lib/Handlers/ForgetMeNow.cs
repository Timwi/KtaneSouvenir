using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SForgetMeNow
{
    [SouvenirQuestion("What was the {1} displayed digit in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    DisplayedDigits
}

public partial class SouvenirModule
{
    [SouvenirHandler("ForgetMeNow", "Forget Me Now", typeof(SForgetMeNow), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessForgetMeNow(ModuleData module)
    {
        var comp = GetComponent(module, "ForgetMeNow");

        yield return WaitForSolve;

        var displayedDigits = GetArrayField<int>(comp, "displayDigits").Get(expectedLength: Bomb.GetSolvableModuleNames().Count, validator: d => d is < 0 or > 9 ? "expected range 0-9" : null);
        addQuestions(module, displayedDigits.Select((d, ix) => makeQuestion(SForgetMeNow.DisplayedDigits, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { d.ToString() })));
    }
}