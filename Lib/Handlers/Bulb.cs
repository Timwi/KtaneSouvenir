using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBulb
{
    [Question("Was the bulb initially lit in {0}?", TwoColumns2Answers, "Yes", "No", TranslateAnswers = true)]
    InitialState
}

public partial class SouvenirModule
{
    [Handler("TheBulbModule", "Bulb", typeof(SBulb), "Quinn Wuest", AddThe = true)]
    [ManualQuestion("Was the bulb initially lit?")]
    private IEnumerator<SouvenirInstruction> ProcessBulb(ModuleData module)
    {
        var comp = GetComponent(module, "TheBulbModule");
        yield return WaitForSolve;
        yield return question(SBulb.InitialState).Answers(GetField<bool>(comp, "_initiallyOn").Get() ? "Yes" : "No");
    }
}
