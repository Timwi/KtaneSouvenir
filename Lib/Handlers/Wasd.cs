using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SWasd
{
    [SouvenirQuestion("What was the location displayed in {0}?", ThreeColumns6Answers, "Bank", "Grocery", "School", "Gym", "Home", "Mall", "Cafe", "Park", "Office")]
    DisplayedLocation
}

public partial class SouvenirModule
{
    [SouvenirHandler("wasdModule", "WASD", typeof(SWasd), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessWasd(ModuleData module)
    {
        var comp = GetComponent(module, "WasdModule");
        var display = GetArrayField<TextMesh>(comp, "DisplayTexts", isPublic: true).Get(minLength: 1).First();
        var displayedLocation = display.text;

        yield return WaitForSolve;
        display.text = "";

        yield return question(SWasd.DisplayedLocation).Answers(displayedLocation);
    }
}