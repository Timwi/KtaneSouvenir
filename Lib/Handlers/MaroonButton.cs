using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMaroonButton
{
    [SouvenirQuestion("What was A in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "MaroonButtonSprites")]
    A
}

public partial class SouvenirModule
{
    [SouvenirHandler("MaroonButtonModule", "Maroon Button", typeof(SMaroonButton), "Anonymous", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessMaroonButton(ModuleData module)
    {
        var comp = GetComponent(module, "MaroonButtonScript");
        yield return WaitForSolve;

        var ans = GetField<int>(comp, "solveFlag").Get(v => v is < 0 or > 19 ? $"Bad flag index {v}" : null);
        var solveParent = GetField<Transform>(comp, "SolveParent", isPublic: true).Get();
        for (var i = 0; i < solveParent.childCount; i++)
            solveParent.GetChild(i).gameObject.SetActive(false);
        yield return question(SMaroonButton.A).Answers(MaroonButtonSprites[ans]);
    }
}