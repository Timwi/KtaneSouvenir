using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SVisualImpairment
{
    [SouvenirQuestion("What was the desired color in the {1} stage on {0}?", TwoColumns4Answers, "Blue", "Green", "Red", "White", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("visual_impairment", "Visual Impairment", typeof(SVisualImpairment), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessVisualImpairment(ModuleData module)
    {
        var comp = GetComponent(module, "VisualImpairment");
        var fldRoundsFinished = GetIntField(comp, "roundsFinished");
        var fldColor = GetIntField(comp, "color");
        var fldPicture = GetArrayField<string>(comp, "picture");

        // Wait for the first picture to be assigned
        while (fldPicture.Get(nullAllowed: true) == null)
            yield return new WaitForSeconds(.1f);

        var stageCount = GetIntField(comp, "stageCount").Get(min: 2, max: 3);
        var colorsPerStage = new int[stageCount];
        var colorNames = new[] { "Blue", "Green", "Red", "White" };

        while (module.Unsolved)
        {
            var newStage = fldRoundsFinished.Get();
            if (newStage >= stageCount)
                break;

            var newColor = fldColor.Get(min: 0, max: 3);
            colorsPerStage[newStage] = newColor;
            yield return new WaitForSeconds(.1f);
        }

        for (var ix = 0; ix < colorsPerStage.Length; ix++)
            yield return question(SVisualImpairment.Colors, args: [Ordinal(ix + 1)]).Answers(colorNames[colorsPerStage[ix]]);
    }
}
