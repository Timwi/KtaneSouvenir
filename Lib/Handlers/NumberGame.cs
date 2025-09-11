using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNumberGame
{
    [SouvenirQuestion("What was the maximum number in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Integers(10000000, 99999999)]
    Maximum
}

public partial class SouvenirModule
{
    [SouvenirHandler("TheNumberGame", "Number Game", typeof(SNumberGame), "Anonymous", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessNumberGame(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "TheNumberGameScript");
        var num = GetIntField(comp, "RandomMaxNumber").Get(min: 10000000, max: 99999999);

        addQuestion(module, Question.NumberGameMaximum, correctAnswers: new[] { num.ToString() });
    }
}