using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S0
{
    [Question("What was the {1} digit in the initially displayed number in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    QNumber,

    [Discriminator("the 0 where its {0} digit was {1}", Arguments = [QandA.Ordinal, "0", QandA.Ordinal, "1", QandA.Ordinal, "2", QandA.Ordinal, "3", QandA.Ordinal, "4", QandA.Ordinal, "5", QandA.Ordinal, "6", QandA.Ordinal, "7", QandA.Ordinal, "8", QandA.Ordinal, "9"], ArgumentGroupSize = 2)]
    DNumber
}

public partial class SouvenirModule
{
    [Handler("0", "0", typeof(S0), "Anonymous")]
    [ManualQuestion("What was the starting number?")]
    private IEnumerator<SouvenirInstruction> Process0(ModuleData module)
    {
        var comp = GetComponent(module, "pruzZero");
        var solution = GetField<string>(comp, "number").Get(v => v.Length != 9 || !v.All("0123456789".Contains) ? "Expected 9 digits" : null);

        yield return WaitForSolve;

        for (var digit = 0; digit < 9; digit++)
        {
            yield return new Discriminator(S0.DNumber, $"zero-{digit}", solution[digit], args: [Ordinal(digit + 1), solution[digit].ToString()]);
            yield return question(S0.QNumber, args: [Ordinal(digit + 1)]).AvoidDiscriminators($"zero-{digit}").Answers(solution[digit].ToString());
        }
    }
}
