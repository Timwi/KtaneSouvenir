using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDisorderedKeys
{
    [SouvenirQuestion("What was the missing information for the {1} key in {0}?", OneColumn3Answers, "Key color", "Label color", "Label", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    MissingInfo,

    [SouvenirQuestion("What was the unrevealed key color for the {1} key in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    UnrevealedKeyColor,

    [SouvenirQuestion("What was the unrevealed label color for the {1} key in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    UnrevealedLabelColor,

    [SouvenirQuestion("What was the unrevealed label for the {1} key in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    UnrevealedKeyLabel,

    [SouvenirQuestion("What was the revealed key color for the {1} key in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    RevealedKeyColor,

    [SouvenirQuestion("What was the revealed label color for the {1} key in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    RevealedLabelColor,

    [SouvenirQuestion("What was the revealed label for the {1} key in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    RevealedLabel
}

public partial class SouvenirModule
{
    [SouvenirHandler("disorderedKeys", "Disordered Keys", typeof(SDisorderedKeys), "Hawker")]
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

        var qs = new List<QandA>();
        var missingStrArr = new[] { "Key color", "Label color", "Label" };

        for (var keyIndex = 0; keyIndex < 6; keyIndex++)
        {
            var formatArgs = new[] { Ordinal(keyIndex + 1) };
            qs.Add(makeQuestion(Question.DisorderedKeysMissingInfo, module, formatArgs: formatArgs, correctAnswers: new[] { missingStrArr[missing[keyIndex]] }));

            if (missing[keyIndex] != 0)   // Key color
                qs.Add(makeQuestion(Question.DisorderedKeysUnrevealedKeyColor, module, formatArgs: formatArgs, correctAnswers: new[] { unrevealedKeyColors[keyIndex] }));
            if (missing[keyIndex] != 1)     // Label color
                qs.Add(makeQuestion(Question.DisorderedKeysUnrevealedLabelColor, module, formatArgs: formatArgs, correctAnswers: new[] { unrevealedLabelColors[keyIndex] }));
            if (missing[keyIndex] != 2)     // Label
                qs.Add(makeQuestion(Question.DisorderedKeysUnrevealedKeyLabel, module, formatArgs: formatArgs, correctAnswers: new[] { unrevealedLabels[keyIndex] }));

            // If not a sequential nor false key, ask about reavealed key info
            if (quirks[keyIndex] < 4)
            {
                qs.Add(makeQuestion(Question.DisorderedKeysRevealedKeyColor, module, formatArgs: formatArgs, correctAnswers: new[] { colorList[info[keyIndex][0]] }));
                qs.Add(makeQuestion(Question.DisorderedKeysRevealedLabelColor, module, formatArgs: formatArgs, correctAnswers: new[] { colorList[info[keyIndex][1]] }));
                qs.Add(makeQuestion(Question.DisorderedKeysRevealedLabel, module, formatArgs: formatArgs, correctAnswers: new[] { (info[keyIndex][2] + 1).ToString() }));
            }
        }
        addQuestions(module, qs);
    }
}