using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPatternCube
{
    [SouvenirQuestion("Which symbol was highlighted in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    HighlightedSymbol
}

public partial class SouvenirModule
{
    [SouvenirHandler("PatternCubeModule", "Pattern Cube", typeof(SPatternCube), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessPatternCube(ModuleData module)
    {
        var comp = GetComponent(module, "PatternCubeModule");
        var highlightedSymbol = GetIntField(comp, "_highlightedSymbol").Get(min: 0, max: 119);
        var symbolsUsed = GetListField<int>(comp, "_symbolIxs").Get(expectedLength: 11, validator: v => v is >= 0 and < 120 ? null : "expected range 0–120");
        var symbolSprites = GetArrayField<Texture>(comp, "SymbolTextures", isPublic: true).Get(expectedLength: 120)
            .Select((tx, ix) => symbolsUsed.Contains(ix) ? Sprites.Recolor((Texture2D) tx).ToSprite() : null).ToArray();

        yield return WaitForSolve;

        yield return question(SPatternCube.HighlightedSymbol).Answers(symbolSprites[highlightedSymbol], all: symbolsUsed.Select(symIx => symbolSprites[symIx]).ToArray());
    }
}
