using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SParity
{
    [SouvenirQuestion("What was shown on the display on {0}?", ThreeColumns6Answers, ExampleAnswers = ["A1", "B2", "C3", "D4", "E5", "F6"])]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("parity", "Parity", typeof(SParity), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessParity(ModuleData module)
    {
        var comp = GetComponent(module, "ParityScript");
        yield return WaitForSolve;

        var text = GetField<string>(comp, "_displayedText").Get();
        var pairs = new List<string>();
        for (var i = 0; i < 26; i++)
            for (var j = 0; j < 10; j++)
                pairs.Add("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[i].ToString() + j.ToString());
        yield return question(SParity.Display).Answers(text, preferredWrong: pairs.ToArray());
    }
}