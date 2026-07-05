using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SXRay
{
    [Question("Which symbol was scanned in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, SpriteFieldName = "XRaySprites")]
    Symbol
}

public partial class SouvenirModule
{
    [Handler("XRayModule", "X-Ray", typeof(SXRay), "Espik")]
    [ManualQuestion("What symbols were scanned?")]
    private IEnumerator<SouvenirInstruction> ProcessXRay(ModuleData module)
    {
        var comp = GetComponent(module, "XRayModule");
        yield return WaitForSolve;

        var symbolsRules = GetField<object>(comp, "_rules").Get();

        var indexCol = GetIntField(comp, "_col").Get();
        var indexRow = GetIntField(comp, "_row").Get();
        var index3x3 = GetIntField(comp, "_3x3").Get();

        var symbolsCol = GetProperty<IList>(symbolsRules, "Columns", isPublic: true).Get();
        var symbolsRow = GetProperty<IList>(symbolsRules, "Rows", isPublic: true).Get();
        var symbols3x3 = GetProperty<IList>(symbolsRules, "Table3x3", isPublic: true).Get();

        var fldSymbolIndex = GetProperty<int>(symbolsCol[0], "Index", isPublic: true);
        var fldSymbolFlipped = GetProperty<bool>(symbolsCol[0], "Flipped", isPublic: true);

        var indicesCol = symbolsCol.Cast<object>().Select(x => fldSymbolIndex.GetFrom(x)).ToArray();
        var flippedCol = symbolsCol.Cast<object>().Select(x => fldSymbolFlipped.GetFrom(x)).ToArray();

        var indicesRow = symbolsRow.Cast<object>().Select(x => fldSymbolIndex.GetFrom(x)).ToArray();
        var indices3x3 = symbols3x3.Cast<object>().Select(x => fldSymbolIndex.GetFrom(x)).ToArray();

        var chosenSprites = new HashSet<Sprite>();
        var availableSprites = new HashSet<Sprite>();
        var flippedSprites = new Sprite[XRaySprites.Length];

        for (var i = 0; i < indicesCol.Length; i++)
        {
            var foundSprite = XRaySprites[indicesCol[i]];

            if (flippedCol[i])
            {
                var flippedSprite = Sprite.Create(foundSprite.texture, foundSprite.rect, new Vector2(0, .5f), foundSprite.pixelsPerUnit);
                // Hacky way to communicate to Souvenir to display a sprite vertically flipped
                flippedSprite.name = $"Souvenir_FlipY_{foundSprite.name}";
                flippedSprites[indicesCol[i]] = flippedSprite;
                availableSprites.Add(flippedSprite);
            }
            else
                availableSprites.Add(foundSprite);
        }

        for (var i = 0; i < indicesRow.Length; i++)
            availableSprites.Add(XRaySprites[indicesRow[i]]);

        for (var i = 0; i < indices3x3.Length; i++)
            availableSprites.Add(XRaySprites[indices3x3[i]]);

        chosenSprites.Add((flippedCol[indexCol] ? flippedSprites : XRaySprites)[indicesCol[indexCol]]);
        chosenSprites.Add(XRaySprites[indicesRow[indexRow]]);
        chosenSprites.Add(XRaySprites[indices3x3[index3x3]]);

        yield return question(SXRay.Symbol).Answers(chosenSprites.ToArray(), all: availableSprites.ToArray());
    }
}
