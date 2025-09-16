using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SReorderedKeys
{
    [SouvenirQuestion("What color was this key in the {1} stage of {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    KeyColor,

    [SouvenirQuestion("What color was the label of this key in the {1} stage of {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    LabelColor,

    [SouvenirQuestion("What was the label of this key in the {1} stage of {0}?", ThreeColumns6Answers, UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    Label,

    [SouvenirQuestion("Which key was the pivot in the {1} stage of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "OrderedKeysSprites")]
    Pivot
}

public partial class SouvenirModule
{
    [SouvenirHandler("reorderedKeys", "Reordered Keys", typeof(SReorderedKeys), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessReorderedKeys(ModuleData module)
    {
        var comp = GetComponent(module, "ReorderedKeysScript");
        var stages = new int[2][][];
        var pivots = new int[2] { -1, -1 };
        var fldStage = GetIntField(comp, "stage");
        var fldResets = GetIntField(comp, "resetCount");
        var info = GetArrayField<int[]>(comp, "info").Get(expectedLength: 6, validator: a => a.Length != 4 ? "expected inner array length of 4" : null);
        var fldPivot = GetIntField(comp, "pivot");
        var fldSolved = GetField<bool>(comp, "moduleSolved"); // The module also adds a reset when solving which must be ignored
        void getInfo()
        {
            var stage = fldStage.Get(min: 1, max: 2);
            stages[stage - 1] = info.Select(a => a.ToArray()).ToArray();
            pivots[stage - 1] = fldPivot.Get(min: 0, max: 5);
        }
        getInfo();
        var resets = fldResets.Get(min: 0);

        while (module.Unsolved)
        {
            var newReset = fldResets.Get(min: resets);
            if (newReset != resets && !fldSolved.Get())
            {
                if (newReset != resets + 1)
                    throw new AbandonModuleException($"I missed something (I noticed at reset {newReset})");
                resets = newReset;
                getInfo();
            }
            yield return null;
        }

        if (stages.Any(s => s is null) || pivots.Any(p => p is -1))
            throw new AbandonModuleException($"I missed a stage: ({stages.Stringify()}), ({pivots.Stringify()})");

        var colors = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };
        for (var stageIx = 0; stageIx < stages.Length; stageIx++)
        {
            for (var keyIx = 0; keyIx < stages[stageIx].Length; keyIx++)
            {
                yield return question(SReorderedKeys.KeyColor, args: [Ordinal(stageIx + 1)], questionSprite: OrderedKeysSprites[keyIx]).Answers(colors[stages[stageIx][keyIx][0]]);
                yield return question(SReorderedKeys.LabelColor, args: [Ordinal(stageIx + 1)], questionSprite: OrderedKeysSprites[keyIx]).Answers(colors[stages[stageIx][keyIx][2]]);
                yield return question(SReorderedKeys.Label, args: [Ordinal(stageIx + 1)], questionSprite: OrderedKeysSprites[keyIx]).Answers((stages[stageIx][keyIx][1] + 1).ToString());
            }
            yield return question(SReorderedKeys.Pivot, args: [Ordinal(stageIx + 1)]).Answers(OrderedKeysSprites[pivots[stageIx]]);
        }
    }
}
