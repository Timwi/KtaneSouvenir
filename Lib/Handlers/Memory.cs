using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SMemory
{
    [SouvenirQuestion("What was the displayed number in the {1} stage of {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 4)]
    QDisplay,

    [SouvenirDiscriminator("the Memory that displayed a {0} in the {1} stage", Arguments = ["1", QandA.Ordinal], ArgumentGroupSize = 2)]
    DDisplay
}

public partial class SouvenirModule
{
    [SouvenirHandler("Memory", "Memory", typeof(SMemory), "Andrio Celos")]
    [SouvenirManualQuestion("What was the display in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessMemory(ModuleData module)
    {
        var comp = GetComponent(module, "MemoryComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        module.SolveIndex = module.Info.NumSolved++;

        var displaySequence = GetProperty<string>(comp, "DisplaySequence", true).Get();
        for (var stage = 0; stage < 4; stage++)
        {
            yield return new Discriminator(SMemory.DDisplay, $"display-{stage}", displaySequence[stage].ToString(), args: [displaySequence[stage].ToString(), Ordinal(stage + 1)]);
            yield return question(SMemory.QDisplay, args: [Ordinal(stage + 1)])
                .AvoidDiscriminators($"display-{stage}")
                .Answers(displaySequence[stage].ToString());
        }
    }
}
