using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

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

        addQuestion(module, Question.DigitStringInitialNumber, correctAnswers: new[] { storedInitialString });
    }
}