using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAbyss
{
    [Question("What was the {1} character displayed on {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(1, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")]
    QSeed,

    [Discriminator("the Abyss whose {0} character was {1}", Arguments = [QandA.Ordinal, "A", QandA.Ordinal, "B", QandA.Ordinal, "C"], ArgumentGroupSize = 2)]
    DSeed
}

public partial class SouvenirModule
{
    [Handler("GSAbyss", "Abyss", typeof(SAbyss), "VFlyer")]
    [ManualQuestion("What were the characters displayed?")]
    private IEnumerator<SouvenirInstruction> ProcessAbyss(ModuleData module)
    {
        var comp = GetComponent(module, "AbyssScript");
        yield return WaitForSolve;
        var seedAbyss = GetField<string>(comp, "SeedVar").Get();
        for (var idx = 0; idx < seedAbyss.Length; idx++)
        {
            yield return question(SAbyss.QSeed, args: [Ordinal(idx + 1)]).Answers(seedAbyss[idx].ToString());
            yield return new Discriminator(SAbyss.DSeed, $"seed-{idx}", seedAbyss[idx].ToString(), args: [Ordinal(idx + 1), seedAbyss[idx].ToString()]);
        }
    }
}
