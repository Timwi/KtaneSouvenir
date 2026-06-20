using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SGreatVoid
{
    [Question("What was the {1} symbol in {0}?", ThreeColumns6Answers, AnswerType = InfoType.DynamicFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 6)]
    Symbol,

    [Question("What was the {1} color in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "Cyan", "White", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Color
}

public partial class SouvenirModule
{
    [Handler("greatVoid", "Great Void", typeof(SGreatVoid), "Marksam", AddThe = true)]
    [ManualQuestion("What were the symbols and colors?")]
    private IEnumerator<SouvenirInstruction> ProcessGreatVoid(ModuleData module)
    {
        var comp = GetComponent(module, "TheGreatVoid");
        var fldDigits = GetArrayField<int>(comp, "Displays");
        var fldColors = GetArrayField<int>(comp, "ColorNums");

        // Get font from module
        var textMesh = GetArrayField<TextMesh>(comp, "Text", isPublic: true).Get(expectedLength: 6)[0];
        var info = new TextAnswerInfo(textMesh.font, textMesh.GetComponent<MeshRenderer>().sharedMaterial.mainTexture);

        yield return WaitForSolve;

        var colorNames = new[] { "Red", "Green", "Blue", "Magenta", "Yellow", "Cyan", "White" };
        for (var i = 0; i < 6; i++)
        {
            yield return question(SGreatVoid.Symbol, args: [Ordinal(i + 1)]).Answers(fldDigits.Get()[i].ToString(), info: info);
            yield return question(SGreatVoid.Color, args: [Ordinal(i + 1)]).Answers(colorNames[fldColors.Get()[i]]);
        }
    }
}
