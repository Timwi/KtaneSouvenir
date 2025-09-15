using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SVaricolourFlash
{
    [SouvenirQuestion("What was the word of the {1} goal in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "White", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Words,

    [SouvenirQuestion("What was the color of the {1} goal in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "White", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("varicolourFlash", "Varicolour Flash", typeof(SVaricolourFlash), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessVaricolourFlash(ModuleData module)
    {
        var comp = GetComponent(module, "VCFScript");
        var fldStage = GetIntField(comp, "stage");
        var fldGoal = GetArrayField<int>(comp, "sequence");

        var words = new int[4];
        var colors = new int[4];
        var names = new[] { "Red", "Green", "Blue", "Magenta", "Yellow", "White" };
        while (module.Unsolved)
        {
            var s = fldStage.Get(min: 0, max: 5);
            if (s < 4)
            {
                var goal = fldGoal.Get(expectedLength: 5)[4];
                if (goal is < 0 or >= 36)
                    throw new AbandonModuleException($"‘sequence[4]’ has value {goal} (expected 0–35).");
                words[s] = goal / 6;
                colors[s] = goal % 6;
            }
            yield return new WaitForSeconds(0.1f);
        }

        for (var ix = 0; ix < words.Length; ix++)
            yield return question(SVaricolourFlash.Words, args: [Ordinal(ix + 1)]).Answers(names[words[ix]]);
        for (var ix = 0; ix < colors.Length; ix++)
            yield return question(SVaricolourFlash.Colors, args: [Ordinal(ix + 1)]).Answers(names[colors[ix]]);
    }
}
