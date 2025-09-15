using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCheapCheckout
{
    [SouvenirQuestion("What was {1} in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["the paid amount", "the first paid amount", "the second paid amount"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(5, 50, "$0\".00\"")]
    Paid
}

public partial class SouvenirModule
{
    [SouvenirHandler("CheapCheckoutModule", "Cheap Checkout", typeof(SCheapCheckout), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessCheapCheckout(ModuleData module)
    {
        var comp = GetComponent(module, "CheapCheckoutModule");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var paids = new List<decimal> { GetField<decimal>(comp, "Display").Get() };
        var paid = GetField<decimal>(comp, "Paid").Get();
        if (paid != paids[0])
            paids.Add(paid);

        yield return WaitForSolve;

        addQuestions(module, paids.Select((p, i) => makeQuestion(Question.CheapCheckoutPaid, module,
            formatArgs: new[] { paids.Count == 1 ? "the paid amount" : i == 0 ? "the first paid amount" : "the second paid amount" },
            correctAnswers: new[] { "$" + p.ToString("N2") })));
    }
}