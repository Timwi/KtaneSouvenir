using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SReformedRoleReversal
{
    [Question("What was the base of the seed in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(20, 62)]
    Base,

    [Question("What was the lookup number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    Lookup
}

public partial class SouvenirModule
{
    [Handler("ReformedRoleReversal", "Reformed Role Reversal", typeof(SReformedRoleReversal), "Espik")]
    [ManualQuestion("What base was the seed in?")]
    [ManualQuestion("What was the lookup number?")]
    private IEnumerator<SouvenirInstruction> ProcessReformedRoleReversal(ModuleData module)
    {
        var comp = GetComponent(module, "ReformedRoleReversal");

        yield return WaitForActivate;

        var init = GetField<object>(comp, "Init").Get();
        var manual = GetField<object>(init, "Manual").Get();
        var tutorial = GetField<IList>(manual, "tutorial").Get(x => x.Count != 8 ? "expected length 8" : null);
        var propText = GetProperty<string>(tutorial[0], "Text", isPublic: true);
        var tutorialMessages = tutorial.Cast<object>().Select(x => propText.GetFrom(x)).ToArray();

        var foundBase = 0;
        var foundLookup = "";

        // Entry 1 contains the base
        var seedMessage = tutorialMessages[1];

        if (seedMessage.Substring(0, 65) == "Look around the screen and locate the seed. Convert it from Base-")
            foundBase = int.Parse(seedMessage.Substring(65, 2));

        else
            throw new AbandonModuleException($"Invalid string found for tutorial screen 1.");

        // Entry 2 contains the lookup
        var lookupMessage = tutorialMessages[2];

        if (lookupMessage.Substring(lookupMessage.Length - 40, 40) == ", convert digits to colors to get wires.")
            foundLookup = lookupMessage.Substring(lookupMessage.Length - 41, 1);

        else
            throw new AbandonModuleException($"Invalid string found for tutorial screen 2.");

        var preferredWrongBases = Enumerable.Range(20, 43).Where(ix => ix >= Math.Max(20, foundBase - 5) && ix <= Math.Min(62, foundBase + 5)).Select(ix => ix.ToString()).ToArray();

        yield return WaitForSolve;

        yield return question(SReformedRoleReversal.Base).Answers(foundBase.ToString(), preferredWrong: preferredWrongBases);
        yield return question(SReformedRoleReversal.Lookup).Answers(foundLookup);
    }
}
