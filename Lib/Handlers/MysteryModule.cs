using System.Collections;
using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SMysteryModule
{
    [Question("Which module was the first requested to be solved by {0}?", OneColumn4Answers, ExampleAnswers = ["Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox", "Rock-Paper-Scissors-L.-Sp."])]
    FirstKey
}

public partial class SouvenirModule
{
    [Handler("mysterymodule", "Mystery Module", typeof(SMysteryModule), "Timwi")]
    [ManualQuestion("Which module was the first requested to be solved?")]
    private IEnumerator<SouvenirInstruction> ProcessMysteryModule(ModuleData module)
    {
        var comp = GetComponent(module, "MysteryModuleScript");
        var fldKeyModules = GetListField<KMBombModule>(comp, "keyModules");
        var fldMystifiedModule = GetField<KMBombModule>(comp, "mystifiedModule");
        var fldAnimating = GetField<bool>(comp, "animating");
        var fldFailsolve = GetField<bool>(comp, "failsolve");

        var failsafeButton = GetField<KMSelectable>(comp, "Failswitch", isPublic: true).Get();

        while (fldKeyModules.Get(nullAllowed: true) == null && !fldFailsolve.Get())
            yield return null;
        while (fldMystifiedModule.Get(nullAllowed: true) == null && !fldFailsolve.Get())
            yield return null;

        if (fldFailsolve.Get())
            yield return legitimatelyNoQuestion(module, "No module was hidden.");

        var keyModule = fldKeyModules.Get(ar => ar.Count == 0 ? "empty" : null)[0];
        var mystifiedModule = fldMystifiedModule.Get();

        // Do not ask questions while Souvenir is hidden by Mystery Module.
        if (mystifiedModule == Module)
            _avoidQuestions++;

        // Detect if Mystery Module was solved by using the failsafe button
        var failsafeWasUsed = false;
        var failsafeOldInteract = failsafeButton.OnInteract;
        failsafeButton.OnInteract = delegate
        {
            var ret = failsafeOldInteract();
            if (!module.Unsolved)
                failsafeWasUsed = true;
            return ret;
        };

        try
        {
            yield return WaitForSolve;

            if (failsafeWasUsed)
                yield return legitimatelyNoQuestion(module, "The failswitch was used.");

            // Do not ask questions during the solve animation, since Mystery Module modifies the scaling of this module.
            while (fldAnimating.Get())
                yield return new WaitForSeconds(.1f);

            yield return question(SMysteryModule.FirstKey).Answers(keyModule.ModuleDisplayName, preferredWrong: Bomb.GetSolvableModuleNames().ToArray());
        }
        finally
        {
            if (mystifiedModule == Module)
                _avoidQuestions--;
        }
    }
}
