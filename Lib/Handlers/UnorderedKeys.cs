using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUnorderedKeys
{
    [SouvenirQuestion("What color was this key in the {1} stage of {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    KeyColor,

    [SouvenirQuestion("What color was the label of this key in the {1} stage of {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    LabelColor,

    [SouvenirQuestion("What was the label of this key in the {1} stage of {0}?", ThreeColumns6Answers, UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    Label
}

public partial class SouvenirModule
{
    [SouvenirHandler("unorderedKeys", "Unordered Keys", typeof(SUnorderedKeys), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessUnorderedKeys(ModuleData module)
    {
        var comp = GetComponent(module, "UnorderedKeysScript");
        var stages = new List<int[][]>(2);
        var fldResetCount = GetField<int>(comp, "resetCount");
        var fldInfo = GetArrayField<int[]>(comp, "info");
        var fldPressed = GetArrayField<bool>(comp, "alreadypressed");
        int[][] getInfo()
        {
            var info = fldInfo.Get(expectedLength: 6, validator: a => a.Length != 3 ? "expected inner array length of 3" : null).Select(ar => ar.ToArray()).ToArray();
            var pressed = fldPressed.Get(expectedLength: 7).ToArray();
            return info.Select((a, i) => pressed[i] ? null : a).ToArray();
        }
        stages.Add(getInfo());
        var resets = 0;

        while (module.Unsolved)
        {
            var newReset = fldResetCount.Get();
            if (newReset != resets)
            {
                if (newReset != resets + 1)
                    throw new AbandonModuleException($"I missed a stage (I noticed at stage {newReset})");
                resets = newReset;
                stages.Add(getInfo());
            }
            yield return null;
        }

        var colors = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };
        addQuestions(module, stages.Take(stages.Count - 1).SelectMany((stage, stageIx) => stage.SelectMany((key, keyIx) => key is null ? Enumerable.Empty<QandA>() : Ut.NewArray(
                makeQuestion(Question.UnorderedKeysKeyColor, module, OrderedKeysSprites[keyIx], formatArgs: new[] { Ordinal(stageIx + 1) }, correctAnswers: new[] { colors[key[0]] }),
                makeQuestion(Question.UnorderedKeysLabelColor, module, OrderedKeysSprites[keyIx], formatArgs: new[] { Ordinal(stageIx + 1) }, correctAnswers: new[] { colors[key[1]] }),
                makeQuestion(Question.UnorderedKeysLabel, module, OrderedKeysSprites[keyIx], formatArgs: new[] { Ordinal(stageIx + 1) }, correctAnswers: new[] { (key[2] + 1).ToString() })))));
    }
}