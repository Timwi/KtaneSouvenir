using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SBigCircle
{
    [SouvenirQuestion("Which direction was the circle spinning in {0}?", OneColumn2Answers, "clockwise", "counterclockwise", TranslateAnswers = true)]
    SpinDirection
}

public partial class SouvenirModule
{
    [SouvenirHandler("BigCircle", "Big Circle", typeof(SBigCircle), "Quinn Wuest")]
    [SouvenirManualQuestion("Which direction was the circle spinning?")]
    private IEnumerator<SouvenirInstruction> ProcessBigCircle(ModuleData module)
    {
        var comp = GetComponent(module, "TheBigCircle");
        yield return WaitForSolve;

        var isCounterClockwise = GetField<bool>(comp, "_rotateCounterClockwise").Get();
        var answers = SBigCircle.SpinDirection.GetAnswers();
        yield return question(SBigCircle.SpinDirection).Answers(isCounterClockwise ? answers[1] : answers[0]);
    }
}
