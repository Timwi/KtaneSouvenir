using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCode
{
    [SouvenirQuestion("What was the displayed number in {0}?", ThreeColumns6Answers, null)]
    [AnswerGenerator.Integers(999, 9999)]
    DisplayNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("theCodeModule", "Code", typeof(SCode), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessCode(ModuleData module)
    {
        var comp = GetComponent(module, "TheCodeModule");
        var fldCode = GetIntField(comp, "moduleNumber");
        var fldResetBtn = GetField<KMSelectable>(comp, "ButtonR", isPublic: true);
        var fldSubmitBtn = GetField<KMSelectable>(comp, "ButtonS", isPublic: true);

        var code = fldCode.Get(min: 999, max: 9999);

        yield return WaitForSolve;

        // Block the submit/reset buttons
        fldResetBtn.Get().OnInteract = delegate { return false; };
        fldSubmitBtn.Get().OnInteract = delegate { return false; };

        yield return question(SCode.DisplayNumber).Answers(code.ToString());
    }
}