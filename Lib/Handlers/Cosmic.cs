using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCosmic
{
    [SouvenirQuestion("What was the number initially shown in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9999)]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("CosmicModule", "Cosmic", typeof(SCosmic), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessCosmic(ModuleData module)
    {
        var comp = GetComponent(module, "CosmicModule");
        var answer = GetField<TextMesh>(comp, "DisplayText", isPublic: true).Get().text;

        yield return WaitForSolve;

        yield return question(SCosmic.Number).Answers(answer);
    }
}