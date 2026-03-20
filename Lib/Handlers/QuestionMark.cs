using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SQuestionMark
{
    [SouvenirQuestion("Which of these symbols was part of the flashing sequence in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "QuestionMarkSprites")]
    FlashedSymbols
}

public partial class SouvenirModule
{
    [SouvenirHandler("Questionmark", "Question Mark", typeof(SQuestionMark), "Kuro")]
    [SouvenirManualQuestion("What were the flashing symbols?")]
    private IEnumerator<SouvenirInstruction> ProcessQuestionMark(ModuleData module)
    {
        var comp = GetComponent(module, "Questionmark");

        yield return WaitForSolve;

        var flashedSpritePool = GetArrayField<int>(comp, "spritePool").Get(expectedLength: 4);

        var allSprites = GetArrayField<Sprite>(comp, "itemSprites", isPublic: true).Get(expectedLength: 15);
        var currentSprite = GetField<SpriteRenderer>(comp, "moduleSprite", isPublic: true).Get().sprite;
        var currentSpriteIndex = Array.IndexOf(allSprites, currentSprite);

        var preferredWrongAns = Enumerable.Range(0, QuestionMarkSprites.Length).Except([currentSpriteIndex]).Select(ix => QuestionMarkSprites[ix]).ToArray();

        yield return question(SQuestionMark.FlashedSymbols).Answers(flashedSpritePool.Except([currentSpriteIndex]).Select(ix => QuestionMarkSprites[ix]).ToArray(), all: preferredWrongAns);
    }
}
