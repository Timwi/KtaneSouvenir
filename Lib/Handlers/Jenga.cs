using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SJenga
{
    [SouvenirQuestion("Which symbol was on the first correctly pulled block in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "JengaSprites")]
    FirstBlock
}

public partial class SouvenirModule
{
    [SouvenirHandler("jenga", "Jenga", typeof(SJenga), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessJenga(ModuleData module)
    {
        var comp = GetComponent(module, "JengaModule");
        var fldCorrect = GetIntField(comp, "correct", isPublic: true);
        var fldSprites = GetArrayField<Sprite>(comp, "Characters", isPublic: true);

        KMSelectable mostRecentSelectable = null;
        KMSelectable firstCorrectSelectable = null;
        var allSelectables = GetArrayField<KMSelectable>(comp, "JengaPiece", isPublic: true).Get(expectedLength: 20);
        for (var i = 0; i < 20; i++)
        {
            var ix = i;
            allSelectables[ix].OnInteract += delegate ()
            {
                mostRecentSelectable = allSelectables[ix];
                return false;
            };
        }
        var currentCorrect = fldCorrect.Get();
        while (fldCorrect.Get() > 0)
        {
            if (firstCorrectSelectable == null && fldCorrect.Get() != currentCorrect)
                firstCorrectSelectable = mostRecentSelectable;
            currentCorrect = fldCorrect.Get();
            yield return null;
        }
        yield return WaitForSolve;

        var sprites = fldSprites.Get(expectedLength: 20);
        var spritesOnFirstPress = firstCorrectSelectable.GetComponentsInChildren<SpriteRenderer>().Select(x => x.sprite).ToArray(); //Always 2 sprites
        var prettyLookingSouvSprites = spritesOnFirstPress.Select(spr => JengaSprites[Array.IndexOf(sprites, spr)]).ToArray();
        addQuestion(module, Question.JengaFirstBlock,
            correctAnswers: prettyLookingSouvSprites,
            preferredWrongAnswers: JengaSprites);
    }
}