using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SOldFogey
{
    [SouvenirQuestion("What was the initial color of the status light in {0}?", ThreeColumns6Answers, "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", "White", TranslateAnswers = true)]
    StartingColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("oldFogey", "Old Fogey", typeof(SOldFogey), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessOldFogey(ModuleData module)
    {
        var comp = GetComponent(module, "OldFogey");

        yield return WaitForSolve;

        var startingColor = GetMethod<string>(comp, "GetStartingColor", 0).Invoke();
        yield return question(SOldFogey.StartingColor).Answers(startingColor);
    }
}