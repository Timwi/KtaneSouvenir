using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDisorderedKeys
{
    [SouvenirQuestion("What was the missing information for this key in {0}?", OneColumn3Answers, "Key color", "Label color", "Label", UsesQuestionSprite = true, TranslateAnswers = true)]
    MissingInfo,

    [SouvenirQuestion("What was the unrevealed key color for this key in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, TranslateAnswers = true)]
    UnrevealedKeyColor,

    [SouvenirQuestion("What was the unrevealed label color for this key in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, TranslateAnswers = true)]
    UnrevealedLabelColor,

    [SouvenirQuestion("What was the unrevealed label for this key in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true)]
    [AnswerGenerator.Integers(1, 6)]
    UnrevealedKeyLabel,

    [SouvenirQuestion("What was the revealed key color for this key in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, TranslateAnswers = true)]
    RevealedKeyColor,

    [SouvenirQuestion("What was the revealed label color for this key in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, TranslateAnswers = true)]
    RevealedLabelColor,

    [SouvenirQuestion("What was the revealed label for this key in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true)]
    [AnswerGenerator.Integers(1, 6)]
    RevealedLabel
}

public partial class SouvenirModule
{
    [SouvenirHandler("disorderedKeys", "Disordered Keys", typeof(SDisorderedKeys), "Hawker")]
    [SouvenirManualQuestion("What were the missing information and the revealed/unrevealed key color, label, and label color of each key?")]
    private IEnumerator<SouvenirInstruction> ProcessDisorderedKeys(ModuleData module)
    {
        var comp = GetComponent(module, "DisorderedKeysScript");
        var fldMissing = GetArrayField<int>(comp, "missing");
        var fldInfo = GetArrayField<int[]>(comp, "info");
        var fldQuirk = GetArrayField<int>(comp, "quirk");
        var colorList = GetStaticField<string[]>(comp.GetType(), "colourList").Get(v => v.Length != 6 ? "expected length 6" : null);

        // These variables are populated by GetInfo() below
        int[] missing = null;
        int[][] info = null;
        int[] quirks = null;
        var unrevealedKeyColors = new string[6];
        var unrevealedLabels = new string[6];
        var unrevealedLabelColors = new string[6];
        var recompute = true;

        module.Module.OnStrike += () =>
        {
            recompute = true;
            return false;
        };

        while (module.Unsolved)
        {
            yield return null;
            if (recompute)
            {
                missing = fldMissing.Get(expectedLength: 6, validator: number => number is < 0 or > 2 ? "expected range 0–2 inclusively" : null).ToArray();
                info = fldInfo.Get(expectedLength: 6, validator: arr => arr.Length != 3 ? "expected length 3" : null).ToArray();
                quirks = fldQuirk.Get(expectedLength: 6).ToArray();

                for (var keyIndex = 0; keyIndex < 6; keyIndex++)
                {
                    unrevealedKeyColors[keyIndex] = missing[keyIndex] == 0 ? "missing" : colorList[info[keyIndex][0]];
                    unrevealedLabelColors[keyIndex] = missing[keyIndex] == 1 ? "missing" : colorList[info[keyIndex][1]];
                    unrevealedLabels[keyIndex] = missing[keyIndex] == 2 ? "missing" : (info[keyIndex][2] + 1).ToString();
                }

                recompute = false;
            }
        }
        var missingStrArr = new[] { "Key color", "Label color", "Label" };
        
        var oneFirstKey = quirks.Count(x => x == 2) == 1;
        var oneLastKey = quirks.Count(x => x == 3) == 1;

        for (var keyIndex = 0; keyIndex < 6; keyIndex++)
        {
            yield return question(SDisorderedKeys.MissingInfo, questionSprite: OrderedKeysSprites[keyIndex]).Answers(missingStrArr[missing[keyIndex]]);

            if (missing[keyIndex] != 0)   // Key color
                yield return question(SDisorderedKeys.UnrevealedKeyColor, questionSprite: OrderedKeysSprites[keyIndex]).Answers(unrevealedKeyColors[keyIndex]);
            if (missing[keyIndex] != 1)     // Label color
                yield return question(SDisorderedKeys.UnrevealedLabelColor, questionSprite: OrderedKeysSprites[keyIndex]).Answers(unrevealedLabelColors[keyIndex]);
            if (missing[keyIndex] != 2)     // Label
                yield return question(SDisorderedKeys.UnrevealedKeyLabel, questionSprite: OrderedKeysSprites[keyIndex]).Answers(unrevealedLabels[keyIndex]);

            // If not a sequential nor false key, ask about reavealed key info
            if (quirks[keyIndex] < 4)
            {
                // If only one first or last key, don't ask about revealed key info
                if (!((quirks[keyIndex] == 2 && oneFirstKey) || (quirks[keyIndex] == 3 && oneLastKey)))
                {
                    yield return question(SDisorderedKeys.RevealedKeyColor, questionSprite: OrderedKeysSprites[keyIndex]).Answers(colorList[info[keyIndex][0]]);
                    yield return question(SDisorderedKeys.RevealedLabelColor, questionSprite: OrderedKeysSprites[keyIndex]).Answers(colorList[info[keyIndex][1]]);
                    yield return question(SDisorderedKeys.RevealedLabel, questionSprite: OrderedKeysSprites[keyIndex]).Answers((info[keyIndex][2] + 1).ToString());
                }
            }
        }
    }
}
