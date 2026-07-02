using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAlfaBravo
{
    [Question("What was the last digit on the small display in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
    Digit
}

public partial class SouvenirModule
{
    [Handler("alfa_bravo", "Alfa-Bravo", typeof(SAlfaBravo), "NickLatkovich")]
    [ManualQuestion("What was the last digit on the small display?")]
    private IEnumerator<SouvenirInstruction> ProcessAlfaBravo(ModuleData module)
    {
        var comp = GetComponent(module, "AlfaBravoModule");
        yield return WaitForSolve;

        yield return question(SAlfaBravo.Digit).Answers(GetProperty<int>(comp, "souvenirDisplayedDigit", true).Get().ToString());
    }
}
