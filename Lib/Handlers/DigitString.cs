using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDigitString
{
    [SouvenirQuestion("What was the initial number in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings("1-9", "6*0-9", "1-9")]
    InitialNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("digitString", "Digit String", typeof(SDigitString), "GoodHood")]
    private IEnumerator<SouvenirInstruction> ProcessDigitString(ModuleData module)
    {
        var comp = GetComponent(module, "digitString");
        yield return WaitForSolve;

        var storedInitialString = GetField<string>(comp, "shownString").Get(x => x.Length != 8 ? "Expected length 8" : null);

        yield return question(SDigitString.InitialNumber).Answers(storedInitialString);
    }
}