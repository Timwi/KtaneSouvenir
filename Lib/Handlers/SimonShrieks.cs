using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonShrieks
{
    [SouvenirQuestion("How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    FlashingButton
}

public partial class SouvenirModule
{
    [SouvenirHandler("SimonShrieksModule", "Simon Shrieks", typeof(SSimonShrieks), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimonShrieks(ModuleData module)
    {
        var comp = GetComponent(module, "SimonShrieksModule");
        yield return WaitForSolve;

        var arrow = GetIntField(comp, "_arrow").Get(min: 0, max: 6);
        var flashingButtons = GetArrayField<int>(comp, "_flashingButtons").Get(expectedLength: 8, validator: b => b is < 0 or > 6 ? "expected range 0–6" : null);
        for (var i = 0; i < flashingButtons.Length; i++)
            yield return question(SSimonShrieks.FlashingButton, args: [Ordinal(i + 1)]).Answers(((flashingButtons[i] + 7 - arrow) % 7).ToString());
    }
}