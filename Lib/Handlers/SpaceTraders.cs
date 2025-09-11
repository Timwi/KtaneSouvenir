using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSpaceTraders
{
    [SouvenirQuestion("What was the maximum tax amount per vessel in {0}?", ThreeColumns6Answers, "0 GCr", "1 GCr", "2 GCr", "3 GCr", "4 GCr", "5 GCr")]
    MaxTax
}

public partial class SouvenirModule
{
    [SouvenirHandler("space_traders", "Space Traders", typeof(SSpaceTraders), "NickLatkovich")]
    private IEnumerator<SouvenirInstruction> ProcessSpaceTraders(ModuleData module)
    {
        var comp = GetComponent(module, "SpaceTradersModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
            yield return legitimatelyNoQuestion(module, "The module was force-solved.");
        if (GetProperty<int>(comp, "maxPossibleTaxAmount", true).Get() < 4)
            yield return legitimatelyNoQuestion(module, "All paths from the solar system are too short.");

        addQuestion(module, Question.SpaceTradersMaxTax, correctAnswers: new[] { GetProperty<int>(comp, "maxTax", true).Get().ToString() + " GCr" });
    }
}