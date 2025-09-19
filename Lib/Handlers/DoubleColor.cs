using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDoubleColor
{
    [SouvenirQuestion("What was the screen color on the {1} stage of {0}?", TwoColumns4Answers, "Green", "Blue", "Red", "Pink", "Yellow", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("doubleColor", "Double Color", typeof(SDoubleColor), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleColor(ModuleData module)
    {
        var comp = GetComponent(module, "doubleColor");
        var fldColor = GetIntField(comp, "screenColor");
        var fldStage = GetIntField(comp, "stageNumber");

        yield return WaitForActivate;

        var color1 = fldColor.Get(min: 0, max: 4);
        var stage = fldStage.Get(min: 1, max: 1);
        var submitBtn = GetField<KMSelectable>(comp, "submit", isPublic: true).Get();

        var prevInteract = submitBtn.OnInteract;
        submitBtn.OnInteract = delegate
        {
            var ret = prevInteract();
            stage = fldStage.Get();
            if (stage == 1)  // This means the user got a strike. Need to retrieve the new first stage color
                // We mustn’t throw an exception inside of the button handler, so don’t check min/max values here
                color1 = fldColor.Get();
            return ret;
        };

        yield return WaitForSolve;

        // Check the value of color1 because we might have reassigned it inside the button handler
        if (color1 is < 0 or > 4)
            throw new AbandonModuleException($"First stage color has unexpected value: {color1} (expected 0 to 4).");

        var color2 = fldColor.Get(min: 0, max: 4);

        var colorNames = new[] { "Green", "Blue", "Red", "Pink", "Yellow" };

        yield return question(SDoubleColor.Colors, args: ["first"]).Answers(colorNames[color1]);
        yield return question(SDoubleColor.Colors, args: ["second"]).Answers(colorNames[color2]);
    }
}
