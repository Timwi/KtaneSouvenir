using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessJewelVault(KMBombModule module)
    {
        var comp = GetComponent(module, "jewelWheelsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        var wheels = GetArrayField<KMSelectable>(comp, "wheels", isPublic: true).Get(expectedLength: 4);
        var assignedWheels = GetListField<KMSelectable>(comp, "assignedWheels").Get(expectedLength: 4);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_JewelVault);

        addQuestions(module, assignedWheels.Select((aw, ix) => makeQuestion(Question.JewelVaultWheels, _JewelVault, new[] { "ABCD".Substring(ix, 1) }, new[] { (Array.IndexOf(wheels, aw) + 1).ToString() })));
    }

    private IEnumerable<object> ProcessJumbleCycle(KMBombModule module)
    {
        return processSpeakingEvilCycle2(module, "JumbleCycleScript", Question.JumbleCycleWord, _JumbleCycle);
    }
}