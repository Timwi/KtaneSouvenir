using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAlphaBits
{
    [Question("What character was displayed on the {1} screen in {0}?", ThreeColumns6Answers, AnswerType = InfoType.DynamicFont, Arguments = ["top-left", "middle-left", "bottom-left", "top-right", "middle-right", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Strings("0-9A-V")]
    DisplayedCharacters,

    [Discriminator("the Alpha-Bits whose {0} screen showed this", Arguments = ["top-left", "middle-left", "bottom-left", "top-right", "middle-right", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true], QuestionExtraType = InfoType.Sprites)]
    Discriminator
}

public partial class SouvenirModule
{
    [Handler("alphaBits", "Alpha-Bits", typeof(SAlphaBits), "Timwi")]
    [ManualQuestion("What characters were displayed on each screen?")]
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

        var displayNames = new[] { "top-left", "middle-left", "bottom-left", "top-right", "middle-right", "bottom-right" };
        for (var displayIx = 0; displayIx < 6; displayIx++)
        {
            yield return question(SAlphaBits.DisplayedCharacters, args: [displayNames[displayIx]]).AvoidDiscriminators($"display-{displayIx}")
                .Answers(displayedCharacters[displayIx], preferredWrong: displayedCharacters, info: new TextAnswerInfo(font, fontTexture));
            yield return new Discriminator(SAlphaBits.Discriminator, $"display-{displayIx}", displayedCharacters[displayIx], [displayNames[displayIx]],
                new QuestionExtraText(displayedCharacters[displayIx], font, fontTexture));
        }
    }
}
