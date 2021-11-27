using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{

    private IEnumerable<object> ProcessJenga(KMBombModule module)
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
        _modulesSolved.IncSafe(_Jenga);

        Sprite[] sprites = fldSprites.Get(expectedLength: 20);
        Sprite[] spritesOnFirstPress = firstCorrectSelectable.GetComponentsInChildren<SpriteRenderer>().Select(x => x.sprite).ToArray(); //Always 2 sprites
        Sprite[] prettyLookingSouvSprites = spritesOnFirstPress.Select(spr => JengaSprites[Array.IndexOf(sprites, spr)]).ToArray();
        addQuestion(module, Question.JengaFirstBlock,
            correctAnswers: prettyLookingSouvSprites,
            preferredWrongAnswers: JengaSprites);
    }

    private IEnumerable<object> ProcessJewelVault(KMBombModule module)
    {
        var comp = GetComponent(module, "jewelWheelsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        var wheels = GetArrayField<KMSelectable>(comp, "wheels", isPublic: true).Get(expectedLength: 4);
        var assignedWheels = GetListField<KMSelectable>(comp, "assignedWheels").Get(expectedLength: 4);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_JewelVault);

        addQuestions(module, assignedWheels.Select((aw, ix) => makeQuestion(Question.JewelVaultWheels, _JewelVault, formatArgs: new[] { "ABCD".Substring(ix, 1) }, correctAnswers: new[] { (Array.IndexOf(wheels, aw) + 1).ToString() })));
    }

    private IEnumerable<object> ProcessJumbleCycle(KMBombModule module)
    {
        return processSpeakingEvilCycle2(module, "JumbleCycleScript", Question.JumbleCycleWord, _JumbleCycle);
    }
}