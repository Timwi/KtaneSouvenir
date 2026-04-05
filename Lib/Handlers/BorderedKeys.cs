using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SBorderedKeys
{
    [Question("What was this key’s border color when it was pressed in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, TranslateAnswers = true)]
    BorderColor,

    [Question("What was the digit displayed when this key was pressed in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true)]
    [AnswerGenerator.Integers(1, 6)]
    Digit,

    [Question("What was this key’s key color when it was pressed in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, TranslateAnswers = true)]
    KeyColor,

    [Question("What was this key’s label when it was pressed in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true)]
    [AnswerGenerator.Integers(1, 6)]
    Label,

    [Question("What was this key’s label color when it was pressed in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, TranslateAnswers = true)]
    LabelColor
}

public partial class SouvenirModule
{
    [Handler("borderedKeys", "Bordered Keys", typeof(SBorderedKeys), "Hawker")]
    [ManualQuestion("What were the border color, displayed digit, key color, label and label color when you pressed each key?")]
    private IEnumerator<SouvenirInstruction> ProcessBorderedKeys(ModuleData module)
    {
        var comp = GetComponent(module, "BorderedKeysScript");
        var colors = GetArrayField<string>(comp, "colourList").Get(expectedLength: 6);
        var allButtons = GetListField<KMSelectable>(comp, "keys", true).Get(expectedLength: 7).ToList();
        var keys = allButtons.Take(6).ToList();
        var fldInfo = GetArrayField<int[]>(comp, "info");

        var keysColors = new string[6];
        var labelColors = new string[6];
        var borderColors = new string[6];
        var labels = new string[6];
        var digits = new string[6];
        Exception exception = null;

        foreach (var key in keys)
        {
            key.OnInteract += delegate ()
            {
                if (exception != null)
                    return false;
                try
                {
                    var info = fldInfo.Get(expectedLength: 6, validator: arr => arr.Length != 5 ? "expected length of 5" : null);
                    var index = keys.IndexOf(key);
                    keysColors[index] = colors[info[index][0]];
                    labelColors[index] = colors[info[index][1]];
                    borderColors[index] = colors[info[index][2]];
                    labels[index] = (info[index][3] + 1).ToString();
                    digits[index] = info[index][4].ToString();
                    return false;
                }
                catch (AbandonModuleException ex)
                {
                    exception = ex;
                    return false;
                }
            };
        }

        yield return WaitForSolve;
        if (exception != null)
            throw exception;
        for (var keyIndex = 0; keyIndex < keys.Count; keyIndex++)
        {
            if (borderColors[keyIndex] != null)
            {
                yield return question(SBorderedKeys.BorderColor, questionSprite: OrderedKeysSprites[keyIndex]).Answers(borderColors[keyIndex]);
                yield return question(SBorderedKeys.Digit, questionSprite: OrderedKeysSprites[keyIndex]).Answers(digits[keyIndex]);
                yield return question(SBorderedKeys.KeyColor, questionSprite: OrderedKeysSprites[keyIndex]).Answers(keysColors[keyIndex]);
                yield return question(SBorderedKeys.Label, questionSprite: OrderedKeysSprites[keyIndex]).Answers(labels[keyIndex]);
                yield return question(SBorderedKeys.LabelColor, questionSprite: OrderedKeysSprites[keyIndex]).Answers(labelColors[keyIndex]);
            }
        }
    }
}
