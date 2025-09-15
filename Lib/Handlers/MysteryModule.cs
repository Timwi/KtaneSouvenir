using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMysteryModule
{
    [SouvenirQuestion("Which module was the first requested to be solved by {0}?", OneColumn4Answers, ExampleAnswers = ["Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox", "Rock-Paper-Scissors-L.-Sp."])]
    FirstKey,

    [SouvenirQuestion("Which module was hidden by {0}?", OneColumn4Answers, ExampleAnswers = ["Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox"])]
    HiddenModule
}

public partial class SouvenirModule
{
    [SouvenirHandler("mysterymodule", "Mystery Module", typeof(SMysteryModule), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMysteryModule(ModuleData module)
    {
        var comp = GetComponent(module, "MysteryModuleScript");
        var fldKeyModules = GetListField<KMBombModule>(comp, "keyModules");
        var fldMystifiedModule = GetField<KMBombModule>(comp, "mystifiedModule");
        var fldAnimating = GetField<bool>(comp, "animating");
        var fldFailsolve = GetField<bool>(comp, "failsolve");

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

        yield return WaitForSolve;

        // Do not ask questions during the solve animation, since Mystery Module modifies the scaling of this module.
        while (fldAnimating.Get())
            yield return new WaitForSeconds(.1f);

        addQuestions(module,
            makeQuestion(Question.MysteryModuleFirstKey, module, correctAnswers: new[] { keyModule.ModuleDisplayName }, preferredWrongAnswers: Bomb.GetSolvableModuleNames().ToArray()),
            makeQuestion(Question.MysteryModuleHiddenModule, module, correctAnswers: new[] { mystifiedModule.ModuleDisplayName }, preferredWrongAnswers: Bomb.GetSolvableModuleNames().ToArray()));

        if (mystifiedModule == Module)
            _avoidQuestions--;
    }
}