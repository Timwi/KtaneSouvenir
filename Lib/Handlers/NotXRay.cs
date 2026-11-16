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

        var solutionTable = GetIntField(comp, "_table").Get(0, 7);
        var solutionCell = GetIntField(comp, "_solutionCell").Get();

        var tableSymbols = GetProperty<IList[]>(symbolsRules, "Tables", isPublic: true).Get();
        var fldSymbolIndex = GetProperty<int>(tableSymbols[0][0], "Index", isPublic: true);
        var fldSymbolFlipped = GetProperty<bool>(tableSymbols[0][0], "Flipped", isPublic: true);

        var validCells = new int[] { 1, 2, 3, 4, 5, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 43, 44, 45, 46, 47 };
        var availableSprites = new HashSet<Sprite>();
        Sprite answerSprite = null;

        for (var table = 0; table < 8; table++)
            foreach (var cell in validCells)
            {
                var ix = fldSymbolIndex.GetFrom(tableSymbols[table][cell]);
                var sprite = fldSymbolFlipped.GetFrom(tableSymbols[table][cell]) ? GetFlippedXRaySprite(ix) : XRaySprites[ix];
                availableSprites.Add(sprite);
                if (table == solutionTable && cell == solutionCell)
                    answerSprite = sprite;
            }

        yield return question(SNotXRay.GoalSymbol).Answers(answerSprite, all: availableSprites.ToArray());
    }
}
