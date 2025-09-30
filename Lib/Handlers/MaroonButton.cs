using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMaroonButton
{
    [SouvenirQuestion("What was A in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    A
}

public partial class SouvenirModule
{
    [SouvenirHandler("MaroonButtonModule", "Maroon Button", typeof(SMaroonButton), "Anonymous", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessMaroonButton(ModuleData module)
    {
        var comp = GetComponent(module, "MaroonButtonScript");
        var sprites = GetArrayField<Texture>(comp, "FlagTextures", isPublic: true).Get(expectedLength: 20)
            .Select(tx => ((Texture2D) tx).ToSprite(1800f, new Vector2(.2f, .5f))).ToArray();

        yield return WaitForSolve;

        var ans = GetField<int>(comp, "solveFlag").Get(v => v is < 0 or > 19 ? $"Bad flag index {v}" : null);
        var solveParent = GetField<Transform>(comp, "SolveParent", isPublic: true).Get();
        for (var i = 0; i < solveParent.childCount; i++)
            solveParent.GetChild(i).gameObject.SetActive(false);
        yield return question(SMaroonButton.A).Answers(sprites[ans], all: sprites);
    }
}
