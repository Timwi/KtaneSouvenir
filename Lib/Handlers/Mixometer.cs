using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMixometer
{
    [SouvenirQuestion("What was the position of the submit button in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Ordinal(5)]
    SubmitButton
}

public partial class SouvenirModule
{
    [SouvenirHandler("mixometer", "Mixometer", typeof(SMixometer), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessMixometer(ModuleData module)
    {
        var comp = GetComponent(module, "Mixometer");
        yield return WaitForSolve;
        var i_buttons = GetArrayField<int[]>(comp, "i_buttons").Get(expectedLength: 5);
        var submitButtonIndex = i_buttons.IndexOf(x => x.Length == 0);
        if (submitButtonIndex < 0)
            throw new AbandonModuleException($"expected ‘i_buttons’ to contain an empty array, but got: {i_buttons.Stringify()}");
        yield return question(SMixometer.SubmitButton).Answers(Ordinal(submitButtonIndex + 1));
    }
}