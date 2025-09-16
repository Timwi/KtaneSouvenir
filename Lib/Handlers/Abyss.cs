using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAbyss
{
    [SouvenirQuestion("What was the {1} character displayed on {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(1, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")]
    Seed
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSAbyss", "Abyss", typeof(SAbyss), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessAbyss(ModuleData module)
    {
        var comp = GetComponent(module, "AbyssScript");
        yield return WaitForSolve;
        var seedAbyss = GetField<string>(comp, "SeedVar").Get();
        for (var idx = 0; idx < seedAbyss.Length; idx++)
            yield return question(SAbyss.Seed, args: [Ordinal(idx + 1)]).Answers(seedAbyss[idx].ToString());
    }
}