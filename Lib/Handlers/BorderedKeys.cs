using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBorderedKeys
{
    [SouvenirQuestion("What was the {1} key’s border color when it was pressed in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    BorderColor,

    [SouvenirQuestion("What was the digit displayed when the {1} key was pressed in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    Digit,

    [SouvenirQuestion("What was the {1} key’s key color when it was pressed in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    KeyColor,

    [SouvenirQuestion("What was the {1} key’s label when it was pressed in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    Label,

    [SouvenirQuestion("What was the {1} key’s label color when it was pressed in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    LabelColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("borderedKeys", "Bordered Keys", typeof(SBorderedKeys), "Hawker")]
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

        var qs = new List<QandA>();
        for (var keyIndex = 0; keyIndex < keys.Count; keyIndex++)
        {
            if (borderColors[keyIndex] != null)
            {
                var formatArgs = new[] { Ordinal(keyIndex + 1) };
                qs.AddRange(new[] {
                    makeQuestion(Question.BorderedKeysBorderColor, module, formatArgs: formatArgs, correctAnswers: new[] { borderColors[keyIndex] }),
                    makeQuestion(Question.BorderedKeysDigit, module, formatArgs: formatArgs, correctAnswers: new[] { digits[keyIndex] }),
                    makeQuestion(Question.BorderedKeysKeyColor, module, formatArgs: formatArgs, correctAnswers: new[] { keysColors[keyIndex] }),
                    makeQuestion(Question.BorderedKeysLabel, module, formatArgs: formatArgs, correctAnswers: new[] { labels[keyIndex] }),
                    makeQuestion(Question.BorderedKeysLabelColor, module, formatArgs: formatArgs, correctAnswers: new[] { labelColors[keyIndex] }),
                });
            }
        }
        addQuestions(module, qs);
    }
}