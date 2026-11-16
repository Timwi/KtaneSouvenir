using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SNotXRay
{
    [Question("What did the scanner color change to in {0}?", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", TranslateAnswers = true)]
    ScannerColor,

    [Question("What was the goal symbol in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, SpriteFieldName = "XRaySprites")]
    GoalSymbol
}

public partial class SouvenirModule
{
    [Handler("NotXRayModule", "Not X-Ray", typeof(SNotXRay), "Espik")]
    [ManualQuestion("What did the scanner color change to?")]
    [ManualQuestion("What was the goal symbol?")]
    private IEnumerator<SouvenirInstruction> ProcessNotXRay(ModuleData module)
    {
        var comp = GetComponent(module, "NotXRayModule");
        yield return WaitForSolve;

        var allColors = SNotXRay.ScannerColor.GetAnswers();
        var scannerColor = GetField<object>(comp, "_scannerColor").Get(v => v == null ? "did not expected null" : !allColors.Contains(v.ToString()) ? "expected " + allColors.JoinString(", ") : null);

        yield return question(SNotXRay.ScannerColor).Answers(scannerColor.ToString());

        var symbolsRules = GetField<object>(comp, "_rules").Get();
        var invalidCells = new int[] { 0, 6, 24, 42, 48 };

        var table = GetIntField(comp, "_table").Get(0, 7);
        var solutionCell = GetIntField(comp, "_solutionCell").Get();

        var symbols = GetProperty<IList[]>(symbolsRules, "Tables", isPublic: true).Get();
        var fldSymbolIndex = GetProperty<int>(symbols[0][0], "Index", isPublic: true);
        var fldSymbolFlipped = GetProperty<bool>(symbols[0][0], "Flipped", isPublic: true);

        var allSymbolIndices = new int[8][];
        var allSymbolFlipped = new bool[8][];

        for (var i = 0; i < allSymbolIndices.Length; i++)
        {
            allSymbolIndices[i] = symbols[i].Cast<object>().Select(x => fldSymbolIndex.GetFrom(x)).ToArray();
            allSymbolFlipped[i] = symbols[i].Cast<object>().Select(x => fldSymbolFlipped.GetFrom(x)).ToArray();
        }

        var availableSprites = new HashSet<Sprite>();
        var flippedSprites = new Sprite[XRaySprites.Length];

        for (var i = 0; i < allSymbolIndices.Length; i++)
        {
            for (var j = 0; j < 49; j++)
            {
                if (!invalidCells.Contains(j))
                {
                    var foundSprite = XRaySprites[allSymbolIndices[i][j]];

                    if (allSymbolFlipped[i][j])
                    {
                        var flippedSprite = Sprite.Create(foundSprite.texture, foundSprite.rect, new Vector2(0, .5f), foundSprite.pixelsPerUnit);
                        // Hacky way to communicate to Souvenir to display a sprite vertically flipped
                        flippedSprite.name = $"Souvenir_FlipY_{foundSprite.name}";
                        flippedSprites[allSymbolIndices[i][j]] = flippedSprite;
                        availableSprites.Add(flippedSprite);
                    }
                    else
                        availableSprites.Add(foundSprite);
                }
            }
        }

        var chosenSprite = (allSymbolFlipped[table][solutionCell] ? flippedSprites : XRaySprites)[allSymbolIndices[table][solutionCell]];

        yield return question(SNotXRay.GoalSymbol).Answers(chosenSprite, all: availableSprites.ToArray());
    }
}
