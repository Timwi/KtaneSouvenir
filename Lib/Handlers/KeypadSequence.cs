using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SKeypadSequence
{
    [SouvenirQuestion("What was this key’s label on the {1} panel in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Labels
}

public partial class SouvenirModule
{
    private static Sprite[] _keypadSequenceSprites;

    [SouvenirHandler("keypadSeq", "Keypad Sequence", typeof(SKeypadSequence), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessKeypadSequence(ModuleData module)
    {
        var comp = GetComponent(module, "KeypadSeqScript");

        _keypadSequenceSprites ??= GetArrayField<Material>(comp, "symbols", true)
            .Get(expectedLength: 36).Select(m => ((Texture2D) m.mainTexture).Recolor().ToSprite()).ToArray();

        yield return WaitForSolve;

        var symbols = GetArrayField<int>(comp, "symbselect").Get(expectedLength: 16, validator: v => v is < 0 or > 35 ? "Expected range 0–35" : null);

        for (var p = 0; p < 4; p++)
            for (var i = 0; i < p; i++)
                yield return question(SKeypadSequence.Labels, args: [Ordinal(p + 1)], questionSprite: Sprites.GenerateGridSprite(2, 2, i))
                    .Answers(_keypadSequenceSprites[symbols[4 * p + i]], all: _keypadSequenceSprites);
    }
}
