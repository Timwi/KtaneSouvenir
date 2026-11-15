using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SWumbo
{
    [Question("What was the number in {0}?", OneColumn4Answers, ExampleAnswers = ["30030", "813244863240810000", "0", "376639725", "27081081027000", "901800900"])]
    Number
}

public partial class SouvenirModule
{
    [Handler("wumbo", "Wumbo", typeof(SWumbo), "Anonymous")]
    [ManualQuestion("What was the number?")]
    private IEnumerator<SouvenirInstruction> ProcessWumbo(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "wumboScript");
        var num = GetField<ulong>(comp, "number").Get(v => v is < 0uL or > 813244863240810000uL ? "Out of range [0, 813244863240810000]" : null);

        static IEnumerable<string> AnswerGenerator()
        {
            var primes = new[] { 2uL, 3uL, 5uL, 7uL, 11uL, 13uL };
            while (true)
            {
                ulong n = 1;
                for (var i = 0; i < primes.Length; i++)
                    for (int j = 0, c = Random.Range(0, 5); j < c; j++)
                        n *= primes[i];
                yield return n.ToString();
            }
        }

        yield return question(SWumbo.Number).Answers(num.ToString(), answerGenerator: AnswerGenerator());
    }
}
