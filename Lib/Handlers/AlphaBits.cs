using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAlphaBits
{
    [SouvenirQuestion("What character was displayed on the {1} screen on the {2} in {0}?", ThreeColumns6Answers, TranslateArguments = [false, true], Type = AnswerType.DynamicFont, Arguments = [QandA.Ordinal, "left", QandA.Ordinal, "right"], ArgumentGroupSize = 2)]
    [AnswerGenerator.Strings("0-9A-V")]
    DisplayedCharacters
}

public partial class SouvenirModule
{
    [SouvenirHandler("alphaBits", "Alpha-Bits", typeof(SAlphaBits), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessAlphaBits(ModuleData module)
    {
        var comp = GetComponent(module, "AlphaBitsScript");

        var textMeshes = new[] { "displayTL", "displayML", "displayBL", "displayTR", "displayMR", "displayBR" }.Select(fieldName => GetField<TextMesh>(comp, fieldName, isPublic: true).Get()).ToArray();
        var font = textMeshes[0].font;
        var fontTexture = textMeshes[0].GetComponent<MeshRenderer>().sharedMaterial.mainTexture;
        var displayedCharacters = textMeshes.Select(textMesh => textMesh.text.Trim()).ToArray();

        if (displayedCharacters.Any(ch => !(ch.Length == 1 && (ch[0] >= 'A' && ch[0] <= 'V' || ch[0] >= '0' && ch[0] <= '9'))))
            throw new AbandonModuleException($"The displayed characters are {displayedCharacters.Select(str => $"“{str}”").JoinString(", ")} (expected six single-character strings 0–9/A–V each).");

        yield return WaitForSolve;

        for (var displayIx = 0; displayIx < 6; displayIx++)
            yield return question(SAlphaBits.DisplayedCharacters, args: [new[] { "top", "middle", "bottom" }[displayIx % 3], new[] { "left", "right" }[displayIx / 3]])
                .Answers(displayedCharacters[displayIx], preferredWrong: displayedCharacters, info: new TextAnswerInfo(font, fontTexture));
    }
}
