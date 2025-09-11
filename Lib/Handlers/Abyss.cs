using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

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
        addQuestions(module, seedAbyss.Select((aChar, idx) => makeQuestion(Question.AbyssSeed, module, formatArgs: new[] { Ordinal(idx + 1) }, correctAnswers: new[] { aChar.ToString() })));
    }
}