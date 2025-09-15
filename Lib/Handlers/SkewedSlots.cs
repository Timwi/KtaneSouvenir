using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSkewedSlots
{
    [SouvenirQuestion("What were the original numbers in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 999, "000")]
    OriginalNumbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("SkewedSlotsModule", "Skewed Slots", typeof(SSkewedSlots), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSkewedSlots(ModuleData module)
    {
        var comp = GetComponent(module, "SkewedModule");
        var fldNumbers = GetArrayField<int>(comp, "Numbers");
        var fldModuleActivated = GetField<bool>(comp, "moduleActivated");

        var originalNumbers = new List<string>();

        while (true)
        {
            // Skewed Slots sets moduleActivated to false while the slots are spinning.
            // If there was a correct answer, it will set solved to true, otherwise it will set moduleActivated to true.
            while (!fldModuleActivated.Get() && module.Unsolved)
                yield return new WaitForSeconds(.1f);

            if (module.IsSolved)
                break;

            // Get the current original digits.
            originalNumbers.Add(fldNumbers.Get(expectedLength: 3, validator: n => n is < 0 or > 9 ? "expected range 0–9" : null).JoinString());

            // When the user presses anything, Skewed Slots sets moduleActivated to false while the slots are spinning.
            while (fldModuleActivated.Get())
                yield return new WaitForSeconds(.1f);
        }

        yield return question(SSkewedSlots.OriginalNumbers).Answers(originalNumbers.Last(), preferredWrong: originalNumbers.Take(originalNumbers.Count - 1).ToArray());
    }
}