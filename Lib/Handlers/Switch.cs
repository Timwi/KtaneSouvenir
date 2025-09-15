using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSwitch
{
    [SouvenirQuestion("What color was the {1} LED on the {2} flip of {0}?", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", TranslateAnswers = true, TranslateArguments = [true, false], Arguments = ["top", QandA.Ordinal, "bottom", QandA.Ordinal], ArgumentGroupSize = 2)]
    InitialColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("BigSwitch", "Switch", typeof(SSwitch), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessSwitch(ModuleData module)
    {
        var comp = GetComponent(module, "Switch");
        var fldBottomColor = GetIntField(comp, "BottomColor");
        var fldTopColor = GetIntField(comp, "TopColor");
        var fldFirstSuccess = GetField<bool>(comp, "FirstSuccess");

        var colorNames = new[] { "red", "orange", "yellow", "green", "blue", "purple" };

        var topColor1 = fldTopColor.Get();
        var bottomColor1 = fldBottomColor.Get();
        var topColor2 = -1;
        var bottomColor2 = -1;

        var switchSelectable = GetField<KMSelectable>(comp, "FlipperSelectable", isPublic: true).Get();

        var prevInteract = switchSelectable.OnInteract;
        switchSelectable.OnInteract = delegate
        {
            var ret = prevInteract();

            // Only access bool and int fields in this button handler, so no exceptions are thrown
            var firstSuccess = fldFirstSuccess.Get();
            if (!firstSuccess)  // This means the user got a strike. Need to retrieve the new colors
            {
                topColor1 = fldTopColor.Get();
                bottomColor1 = fldBottomColor.Get();
            }
            else if (module.Unsolved)
            {
                topColor2 = fldTopColor.Get();
                bottomColor2 = fldBottomColor.Get();
            }
            return ret;
        };

        yield return WaitForSolve;

        if (topColor1 < 1 || topColor1 > 6 || bottomColor1 < 1 || bottomColor1 > 6 || topColor2 < 1 || topColor2 > 6 || bottomColor2 < 1 || bottomColor2 > 6)
            throw new AbandonModuleException($"topColor1/bottomColor1/topColor2/bottomColor2 have unexpected values: {topColor1}, {bottomColor1}, {topColor2}, {bottomColor2} (expected 1–6).");

        addQuestions(module,
            makeQuestion(Question.SwitchInitialColor, module, formatArgs: new[] { "top", "first" }, correctAnswers: new[] { colorNames[topColor1 - 1] }),
            makeQuestion(Question.SwitchInitialColor, module, formatArgs: new[] { "bottom", "first" }, correctAnswers: new[] { colorNames[bottomColor1 - 1] }),
            makeQuestion(Question.SwitchInitialColor, module, formatArgs: new[] { "top", "second" }, correctAnswers: new[] { colorNames[topColor2 - 1] }),
            makeQuestion(Question.SwitchInitialColor, module, formatArgs: new[] { "bottom", "second" }, correctAnswers: new[] { colorNames[bottomColor2 - 1] }));
    }
}