using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SMisterSoftee
{
    [Question("Where was the SpongeBob Bar on {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    SpongebobPosition,

    [Question("Which treat was present on {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    TreatsPresent
}

public partial class SouvenirModule
{
    [Handler("misterSoftee", "Mister Softee", typeof(SMisterSoftee), "TasThiluna")]
    [ManualQuestion("Where was the SpongeBob Bar?")]
    [ManualQuestion("Which treats were present?")]
    private IEnumerator<SouvenirInstruction> ProcessMisterSoftee(ModuleData module)
    {
        var comp = GetComponent(module, "misterSoftee");
        yield return WaitForSolve;

        var iceCreams = GetArrayField<int>(comp, "iceCreamsPresent").Get();
        var iceCreamNames = GetStaticField<string[]>(comp.GetType(), "iceCreamNames").Get();
        var sprites = GetArrayField<Texture>(comp, "iceCreamTextures", isPublic: true)
            .Get(expectedLength: 15, validator: tex => tex is Texture2D ? null : "expected Texture2D instances")
            .Where(tex => tex.name != "SpongeBob Bar")
            .Select(tex => ((Texture2D) tex).ToSprite(1500f))
            .ToArray();
        var ix = Array.IndexOf(iceCreams, 14);
        var displayedIceCreamSprites = iceCreams.Where(x => x != 14).Select(index => sprites.First(sprite => sprite.name == iceCreamNames[index])).ToArray();
        yield return question(SMisterSoftee.SpongebobPosition).Answers(new Coord(3, 3, ix));
        yield return question(SMisterSoftee.TreatsPresent).Answers(displayedIceCreamSprites, all: sprites);
    }
}
