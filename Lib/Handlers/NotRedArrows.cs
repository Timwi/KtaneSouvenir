using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotRedArrows
{
    [SouvenirQuestion("What was the starting number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(10, 99, 1, "00")]
    Start
}

public partial class SouvenirModule
{
    [SouvenirHandler("notRedArrowsModule", "Not Red Arrows", typeof(SNotRedArrows), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessNotRedArrows(ModuleData module)
    {
        var comp = GetComponent(module, "NotRedArrowsScript");
        var startNumber = -1;
        module.Module.OnActivate += () => startNumber = GetField<int>(comp, "currentNumber").Get(v => v is < 10 or > 99 ? "expected 10–99" : null);
        yield return WaitForSolve;

        if (startNumber == -1)
            throw new AbandonModuleException("Failed to capture the starting number.");

        yield return question(SNotRedArrows.Start).Answers(startNumber.ToString("00"));
    }
}