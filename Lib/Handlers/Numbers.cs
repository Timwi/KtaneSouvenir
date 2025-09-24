using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNumbers
{
    [SouvenirQuestion("What two-digit number was given in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99, "00")]
    TwoDigit
}

public partial class SouvenirModule
{
    [SouvenirHandler("Numbers", "Numbers", typeof(SNumbers), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessNumbers(ModuleData module)
    {
        var comp = GetComponent(module, "WAnumbersScript");
        yield return WaitForSolve;

        var numberValue1 = GetField<int>(comp, "numberValue1").Get();
        var numberValue2 = GetField<int>(comp, "numberValue2").Get();
        var answer = numberValue1.ToString() + numberValue2.ToString();
        yield return question(SNumbers.TwoDigit).Answers(answer);
    }
}