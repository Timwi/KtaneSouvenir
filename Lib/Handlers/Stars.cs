using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SStars
{
    [SouvenirQuestion("What was the digit in the center of {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    Center
}

public partial class SouvenirModule
{
    [SouvenirHandler("stars", "Stars", typeof(SStars), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessStars(ModuleData module)
    {
        var comp = GetComponent(module, "Stars2Script");
        var originalNumber = GetField<TextMesh>(comp, "Number", isPublic: true).Get().text;

        yield return WaitForSolve;

        yield return question(SStars.Center).Answers(originalNumber);
    }
}