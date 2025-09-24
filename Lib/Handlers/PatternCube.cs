using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPatternCube
{
    [SouvenirQuestion("Which symbol was highlighted in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "PatternCubeSprites")]
    HighlightedSymbol
}

public partial class SouvenirModule
{
    [SouvenirHandler("PatternCubeModule", "Pattern Cube", typeof(SPatternCube), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessPatternCube(ModuleData module)
    {
        var comp = GetComponent(module, "PatternCubeModule");
        var selectableSymbolObjects = GetArrayField<MeshRenderer>(comp, "_selectableSymbolObjs").Get(expectedLength: 5);
        var placeableSymbolObjects = GetArrayField<MeshRenderer>(comp, "_placeableSymbolObjs").Get(expectedLength: 6);
        var highlightPos = GetIntField(comp, "_highlightedPosition").Get(min: 0, max: 4);

        yield return WaitForSolve;

        var symbols = selectableSymbolObjects.Concat(placeableSymbolObjects.Where(r => r.gameObject.activeSelf))
            .Select(r => PatternCubeSprites[int.Parse(r.sharedMaterial.mainTexture.name.Substring(6))]).ToArray();
        yield return question(SPatternCube.HighlightedSymbol).Answers(symbols[highlightPos], preferredWrong: symbols);
    }
}
