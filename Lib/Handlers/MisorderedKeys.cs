using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SMisorderedKeys
{
    [Question("What color was this key in {0}?", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta", TranslateAnswers = true, QuestionExtraType = InfoType.Sprites)]
    Colors,

    [Question("What was the label of this key in {0}?", ThreeColumns6Answers, ExampleAnswers = ["41", "434", "63265", "4431", "116", "6346", "41523", "4", "665", "52123", "4", "41"], QuestionExtraType = InfoType.Sprites)]
    [AnswerGenerator.MisorderedKeys]
    Labels,

    [Question("What color was the label of this key in {0}?", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta", TranslateAnswers = true, QuestionExtraType = InfoType.Sprites)]
    LabelColors,

    [Question("Which key was K in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, SpriteFieldName = "OrderedKeysSprites")]
    K
}

public partial class SouvenirModule
{
    [Handler("misorderedKeys", "Misordered Keys", typeof(SMisorderedKeys), "Timwi")]
    [ManualQuestion("What were the labels, their colors, and the colors of the keys?")]
    [ManualQuestion("Which key was K?")]
    private IEnumerator<SouvenirInstruction> ProcessMisorderedKeys(ModuleData module)
    {
        var comp = GetComponent(module, "MisorderedKeysScript");
        var fldInfo = GetArrayField<int[]>(comp, "info");
        var fldLabelList = GetArrayField<List<string>>(comp, "labelList");
        var fldBlackKey = GetField<int>(comp, "blackkey");

        yield return WaitForActivate;

        var blackKey = fldBlackKey.Get();

        int[][] info = null;
        List<string>[] labelList = null;
        while (!module.IsSolved)
        {
            var newInfo = fldInfo.Get(expectedLength: 6, validator: arr => arr == null ? "null" : arr.Length != 4 ? "expected length 4" : null);
            var newLabelList = fldLabelList.Get(expectedLength: 6, validator: lst => lst.Count is < 1 or > 6 ? "expected length 1–6" : lst.Any(v => v.Length != 1 || v[0] is < '1' or > '6') ? "expected single digits 1–6" : null);
            if (info == null || labelList == null || Enumerable.Range(0, newInfo.Length).Any(ix => !newInfo[ix].SequenceEqual(info[ix])) || !newLabelList.SequenceEqual(labelList))
            {
                // Take copies of the information
                info = newInfo.Select(arr => arr.ToArray()).ToArray();
                labelList = newLabelList.Select(arr => arr.ToList()).ToArray();
            }
            yield return null;
        }

        var colors = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };
        for (var key = 0; key < 6; key++)
        {
            yield return question(SMisorderedKeys.Colors, questionExtra: OrderedKeysSprites[key]).Answers(colors[info[key][0]]);
            yield return question(SMisorderedKeys.Labels, questionExtra: OrderedKeysSprites[key]).Answers(labelList[key].JoinString());
            yield return question(SMisorderedKeys.LabelColors, questionExtra: OrderedKeysSprites[key]).Answers(colors[info[key][1]]);
        }
        yield return question(SMisorderedKeys.K).Answers(OrderedKeysSprites[blackKey], all: OrderedKeysSprites);
    }
}
