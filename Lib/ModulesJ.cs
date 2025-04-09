using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessJenga(ModuleData module)
    {
        var comp = GetComponent(module, "JengaModule");
        var fldCorrect = GetIntField(comp, "correct", isPublic: true);
        var fldSprites = GetArrayField<Sprite>(comp, "Characters", isPublic: true);

        KMSelectable mostRecentSelectable = null;
        KMSelectable firstCorrectSelectable = null;
        KMSelectable[] allSelectables = GetArrayField<KMSelectable>(comp, "JengaPiece", isPublic: true).Get(expectedLength: 20);
        for (int i = 0; i < 20; i++)
        {
            int ix = i;
            allSelectables[ix].OnInteract += delegate ()
            {
                mostRecentSelectable = allSelectables[ix];
                return false;
            };
        }
        int currentCorrect = fldCorrect.Get();
        while (fldCorrect.Get() > 0)
        {
            if (firstCorrectSelectable == null && fldCorrect.Get() != currentCorrect)
                firstCorrectSelectable = mostRecentSelectable;
            currentCorrect = fldCorrect.Get();
            yield return null;
        }
        yield return WaitForSolve;

        Sprite[] sprites = fldSprites.Get(expectedLength: 20);
        Sprite[] spritesOnFirstPress = firstCorrectSelectable.GetComponentsInChildren<SpriteRenderer>().Select(x => x.sprite).ToArray(); //Always 2 sprites
        Sprite[] prettyLookingSouvSprites = spritesOnFirstPress.Select(spr => JengaSprites[Array.IndexOf(sprites, spr)]).ToArray();
        addQuestion(module, Question.JengaFirstBlock,
            correctAnswers: prettyLookingSouvSprites,
            preferredWrongAnswers: JengaSprites);
    }

    private IEnumerator<YieldInstruction> ProcessJewelVault(ModuleData module)
    {
        var comp = GetComponent(module, "jewelWheelsScript");

        var wheels = GetArrayField<KMSelectable>(comp, "wheels", isPublic: true).Get(expectedLength: 4);
        var assignedWheels = GetListField<KMSelectable>(comp, "assignedWheels").Get(expectedLength: 4);

        yield return WaitForSolve;

        addQuestions(module, assignedWheels.Select((aw, ix) => makeQuestion(Question.JewelVaultWheels, module, formatArgs: new[] { "ABCD".Substring(ix, 1) }, correctAnswers: new[] { (Array.IndexOf(wheels, aw) + 1).ToString() })));
    }

    private IEnumerator<YieldInstruction> ProcessJumbleCycle(ModuleData module)
    {
        var qs = new[] { Question.JumbleCycleDialDirections, Question.JumbleCycleDialLabels };
        return ProcessSpeakingEvilCycle(module, "JumbleCycleScript", qs);
    }

    private IEnumerator<YieldInstruction> ProcessJuxtacoloredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "JuxtacoloredSquaresModule");

        var fldColors = GetField<Array>(comp, "_colors");
        var colors = fldColors.Get(); // Prevent compiler from complaining about this being an unassigned local variable.
        var needToUpdate = true;
        module.Module.OnStrike += () => { needToUpdate = true; return false; };

        while (module.Unsolved)
        {
            if (needToUpdate)
            {
                yield return null; // Wait for fldColors to update.
                colors = fldColors.Get(arr => arr.Length != 16 ? "expected length 16" : null).Clone() as Array;
                needToUpdate = false;
            }
            yield return null; // Do not wait .1 seconds to make sure we get get the colors before any squares are pressed.
        }
        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (int pos = 0; pos < 16; pos++)
        {
            var colorName = colors.GetValue(pos).ToString();
            if (colorName == "DarkBlue")
                colorName = "Blue";
            var coordinate = new Coord(4, 4, pos);
            qs.Add(makeQuestion(Question.JuxtacoloredSquaresColorsByPosition, module, questionSprite: Sprites.GenerateGridSprite(coordinate), correctAnswers: new[] { colorName }));
            qs.Add(makeQuestion(Question.JuxtacoloredSquaresPositionsByColor, module, formatArgs: new[] { colorName.ToLowerInvariant() }, correctAnswers: new[] { coordinate }));
        }
        addQuestions(module, qs);
    }
}
