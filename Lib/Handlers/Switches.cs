using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSwitches
{
    [SouvenirQuestion("What was the initial position of the switches in {0}?", ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings(5, 'Q', 'R')]
    InitialPosition
}

public partial class SouvenirModule
{
    [SouvenirHandler("switchModule", "Switches", typeof(SSwitches), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "SwitchModule");
        var mthCurConfig = GetMethod<object>(comp, "GetCurrentConfiguration", 0);
        var switches = GetArrayField<MonoBehaviour>(comp, "Switches", isPublic: true).Get(expectedLength: 5);

        // The special font Souvenir uses to display switch states uses Q for up and R for down
        var initialState = switches.Select(sw => sw.GetComponent<Animator>().GetBool("Up") ? "Q" : "R").JoinString();

        yield return WaitForSolve;

        yield return question(SSwitches.InitialPosition).Answers(initialState);
    }
}