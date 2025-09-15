using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SOrderedKeys
{
    [SouvenirQuestion("What color was this key in the {1} stage of {0}?", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta", TranslateAnswers = true, UsesQuestionSprite = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Colors,

    [SouvenirQuestion("What was the label of this key in the {1} stage of {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", UsesQuestionSprite = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Labels,

    [SouvenirQuestion("What color was the label of this key in the {1} stage of {0}?", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta", TranslateAnswers = true, UsesQuestionSprite = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    LabelColors
}

public partial class SouvenirModule
{
    [SouvenirHandler("orderedKeys", "Ordered Keys", typeof(SOrderedKeys), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessOrderedKeys(ModuleData module)
    {
        var comp = GetComponent(module, "OrderedKeysScript");
        var fldInfo = GetArrayField<int[]>(comp, "info");
        var fldStage = GetIntField(comp, "stage");

        var curStage = 0;
        var moduleData = new int[3][][];

        while (module.Unsolved)
        {
            var info = fldInfo.Get(expectedLength: 6, validator: arr => arr == null ? "null" : arr.Length != 4 ? "expected length 4" : null);
            var newStage = fldStage.Get(min: 1, max: 3);
            if (curStage != newStage || moduleData[newStage - 1] == null || !Enumerable.Range(0, 6).All(ix => info[ix].SequenceEqual(moduleData[newStage - 1][ix])))
            {
                curStage = newStage;
                moduleData[curStage - 1] = info.Select(arr => arr.ToArray()).ToArray(); // Take a copy of the array.
            }
            yield return new WaitForSeconds(.1f);
        }

        var colors = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };
        for (var stage = 0; stage < 3; stage++)
        {
            for (var key = 0; key < 6; key++)
            {
                yield return question(SOrderedKeys.Colors, args: [Ordinal(stage + 1)], questionSprite: OrderedKeysSprites[key]).Answers(colors[moduleData[stage][key][0]]);
                yield return question(SOrderedKeys.Labels, args: [Ordinal(stage + 1)], questionSprite: OrderedKeysSprites[key]).Answers((moduleData[stage][key][3] + 1).ToString());
                yield return question(SOrderedKeys.LabelColors, args: [Ordinal(stage + 1)], questionSprite: OrderedKeysSprites[key]).Answers(colors[moduleData[stage][key][1]]);
            }
        }
    }
}