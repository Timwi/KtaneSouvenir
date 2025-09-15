using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBulb
{
    [SouvenirQuestion("Was the bulb initially lit in {0}?", TwoColumns2Answers, "Yes", "No")]
    InitialState
}

public partial class SouvenirModule
{
    [SouvenirHandler("TheBulbModule", "Bulb", typeof(SBulb), "Quinn Wuest", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessBulb(ModuleData module)
    {
        var comp = GetComponent(module, "TheBulbModule");
        yield return WaitForSolve;
        addQuestion(module, Question.BulbInitialState, correctAnswers: new[] { GetField<bool>(comp, "_initiallyOn").Get() ? "Yes" : "No" });
    }
}