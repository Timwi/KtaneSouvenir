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
    [SouvenirHandler("keypadSeq", "Keypad Sequence", typeof(SKeypadSequence), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessKeypadSequence(ModuleData module)
    {
        var comp = GetComponent(module, "KeypadSeqScript");
        _keypadSequenceSprites ??= GetArrayField<Material>(comp, "symbols", true)
            .Get(expectedLength: 36).Select(m => ((Texture2D) m.mainTexture).Recolor().ToSprite()).ToArray();

        yield return WaitForSolve;

        var symbols = GetArrayField<int>(comp, "symbselect").Get(expectedLength: 16, validator: v => v is < 0 or > 35 ? "Expected range 0–35" : null);

        addQuestions(module, Enumerable.Range(0, 4).SelectMany(p =>
            symbols.Skip(4 * p).Take(4).Select((s, i) =>
                makeQuestion(SKeypadSequence.Labels, module,
                    correctAnswers: new[] { _keypadSequenceSprites[s] },
                    formatArgs: new[] { Ordinal(p + 1) },
                    questionSprite: Sprites.GenerateGridSprite(2, 2, i),
                    allAnswers: _keypadSequenceSprites))));
    }
}