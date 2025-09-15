using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGreenArrows
{
    [SouvenirQuestion("What was the last number on the display on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99, "00")]
    LastScreen
}

public partial class SouvenirModule
{
    [SouvenirHandler("greenArrowsModule", "Green Arrows", typeof(SGreenArrows), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessGreenArrows(ModuleData module)
    {
        var comp = GetComponent(module, "GreenArrowsScript");
        var fldNumDisplay = GetField<GameObject>(comp, "numDisplay", isPublic: true);
        var fldStreak = GetIntField(comp, "streak");
        var fldAnimating = GetField<bool>(comp, "isanimating");

        string numbers = null;
        var activated = false;
        while (module.Unsolved)
        {
            var streak = fldStreak.Get();
            var animating = fldAnimating.Get();
            if (streak == 6 && !animating && !activated)
            {
                var numDisplay = fldNumDisplay.Get();
                numbers = numDisplay.GetComponent<TextMesh>().text;
                if (numbers == null)
                    throw new AbandonModuleException("numDisplay TextMesh text was null.");
                activated = true;
            }
            if (streak == 0)
                activated = false;
            yield return new WaitForSeconds(.1f);
        }

        if (!int.TryParse(numbers, out var number))
            throw new AbandonModuleException($"The screen is not an integer: “{number}”.");
        if (number is < 0 or > 99)
            throw new AbandonModuleException($"The number on the screen is out of range: number = {number}, expected 0-99");

        yield return question(SGreenArrows.LastScreen).Answers(number.ToString("00"));
    }
}