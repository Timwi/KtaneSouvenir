using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSnooker
{
    [SouvenirQuestion("How many red balls were there at the start of {0}?", ThreeColumns3Answers, "8", "9", "10")]
    Reds
}

public partial class SouvenirModule
{
    [SouvenirHandler("snooker", "Snooker", typeof(SSnooker), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessSnooker(ModuleData module)
    {
        var comp = GetComponent(module, "snookerScript");
        var fldActiveReds = GetIntField(comp, "activeReds");
        var activeReds = 0;

        var getNewValue = true;
        module.Module.OnStrike += delegate { getNewValue = true; return true; };

        while (module.Unsolved)
        {
            if (getNewValue)
            {
                activeReds = fldActiveReds.Get(min: 8, max: 10);
                getNewValue = false;
            }
            yield return null;
        }
        yield return new WaitForSeconds(.1f);

        yield return question(SSnooker.Reds).Answers(activeReds.ToString());
    }
}