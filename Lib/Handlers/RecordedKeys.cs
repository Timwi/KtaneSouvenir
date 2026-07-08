using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SRecordedKeys
{
    [Question("What color was this key when calculating the value of A used to solve the {1} stage of {0}?", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta", TranslateAnswers = true, QuestionExtraType = InfoType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    AColors,

    [Question("What was the label of this key when calculating the value of A used to solve the {1} stage of {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", QuestionExtraType = InfoType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ALabels,

    [Question("What color was the label of this key when calculating the value of A used to solve the {1} stage of {0}?", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta", TranslateAnswers = true, QuestionExtraType = InfoType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ALabelColors,

    [Question("Which sound was played by this key when calculating the value of A used to solve the {1} stage of {0}?", ThreeColumns6Answers, QuestionExtraType = InfoType.Sprites, AnswerType = InfoType.Audio, ForeignAudioID = "recordedKeys", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ASounds,

    [Question("What color was this key when calculating the value of B used to solve the {1} stage of {0}?", TwoColumns2Answers, "Black", "White", TranslateAnswers = true, QuestionExtraType = InfoType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    BColors,

    [Question("What was the label of this key when calculating the value of B used to solve the {1} stage of {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", QuestionExtraType = InfoType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    BLabels,

    [Question("What color was the label of this key when calculating the value of B used to solve the {1} stage of {0}?", TwoColumns2Answers, "Black", "White", TranslateAnswers = true, QuestionExtraType = InfoType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    BLabelColors
}

public partial class SouvenirModule
{
    [Handler("recordedKeys", "Recorded Keys", typeof(SRecordedKeys), "Timwi")]
    [ManualQuestion("What were the labels, their colors, and the colors of the keys in each stage used to calculate both A and B?")]
    [ManualQuestion("Which sounds were played by the keys in each stage used to calculate A?")]
    private IEnumerator<SouvenirInstruction> ProcessRecordedKeys(ModuleData module)
    {
        var comp = GetComponent(module, "RecordedKeysScript");
        var fldInfo = GetArrayField<int[]>(comp, "info");
        var fldStage = GetIntField(comp, "stage");
        var fldInputMode = GetField<bool>(comp, "inputMode");
        var fldSoundList = GetArrayField<string[]>(comp, "soundList");

        yield return WaitForActivate;

        // “A” is the colorful (and soundful) stage where you calculate the value A
        // “B” is the black-and-white stage where you calculate the value B and also submit the input
        var infoA = new int[2][][];
        var soundsA = new int[2][];
        var infoB = new int[2][][];
        var curStage = -1;
        var curInputMode = false;

        while (!module.IsSolved)
        {
            var newStage = fldStage.Get(min: 1, max: 2);
            var newInputMode = fldInputMode.Get();

            if (newStage != curStage || newInputMode != curInputMode)
            {
                curStage = newStage;
                curInputMode = newInputMode;

                (curInputMode ? infoB : infoA)[curStage - 1] = fldInfo.Get(expectedLength: 6, validator: arr => arr == null ? "null" : arr.Length != 4 ? "expected length 4" : null)
                    .Select(arr => arr.ToArray()).ToArray();  // Take a copy of the array

                // Obtain the sounds in an A-stage
                if (!curInputMode)
                    soundsA[curStage - 1] = fldSoundList
                        .Get(expectedLength: 2, validator:
                            arr => arr.Length != 6 ? "expected length 6" :
                            arr.Any(str => !Regex.IsMatch(str, @"^Glockenspiel[1-6]$")) ? "expected ^Glockenspiel[1-6]$" : null)[1]
                        .Select(v => int.Parse(v.Substring("Glockenspiel".Length)) - 1)
                        .ToArray();

                // If we are in Stage 1 A, the defuser can solve Stages 1+2 B using the same A.
                // We deal with this by pretending that the same A-stage happened twice
                if (!curInputMode && curStage == 1)
                {
                    infoA[1] = infoA[0];
                    soundsA[1] = soundsA[0];
                }
            }
            yield return null;
        }

        var aColors = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };
        var bColors = new[] { "White", "Black" };
        var soundClips = Enumerable.Range(1, 6).Select(i => Sounds.GetForeignClip("recordedKeys", $"Glockenspiel{i}")).ToArray();
        for (var stage = 0; stage < 1; stage++)
            for (var key = 0; key < 6; key++)
            {
                yield return question(SRecordedKeys.AColors, args: [Ordinal(stage + 1)], questionExtra: OrderedKeysSprites[key]).Answers(aColors[infoA[stage][key][0]]);
                yield return question(SRecordedKeys.ALabels, args: [Ordinal(stage + 1)], questionExtra: OrderedKeysSprites[key]).Answers((infoA[stage][key][1] + 1).ToString());
                yield return question(SRecordedKeys.ALabelColors, args: [Ordinal(stage + 1)], questionExtra: OrderedKeysSprites[key]).Answers(aColors[infoA[stage][key][2]]);
                yield return question(SRecordedKeys.ASounds, args: [Ordinal(stage + 1)], questionExtra: OrderedKeysSprites[key]).Answers(soundClips[soundsA[stage][key]], all: soundClips);
                yield return question(SRecordedKeys.BColors, args: [Ordinal(stage + 1)], questionExtra: OrderedKeysSprites[key]).Answers(bColors[infoB[stage][key][0]]);
                yield return question(SRecordedKeys.BLabels, args: [Ordinal(stage + 1)], questionExtra: OrderedKeysSprites[key]).Answers(infoB[stage][key][1].ToString());
                yield return question(SRecordedKeys.BLabelColors, args: [Ordinal(stage + 1)], questionExtra: OrderedKeysSprites[key]).Answers(bColors[infoB[stage][key][2]]);
            }
    }
}
