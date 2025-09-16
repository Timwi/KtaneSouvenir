using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SMisterSoftee
{
    [SouvenirQuestion("Where was the SpongeBob Bar on {0}?", ThreeColumns6Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    SpongebobPosition,

    [SouvenirQuestion("Which treat was present on {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "MisterSofteeSprites")]
    TreatsPresent
}

public partial class SouvenirModule
{
    [SouvenirHandler("misterSoftee", "Mister Softee", typeof(SMisterSoftee), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessMisterSoftee(ModuleData module)
    {
        var comp = GetComponent(module, "misterSoftee");
        yield return WaitForSolve;

        var iceCreams = GetArrayField<int>(comp, "iceCreamsPresent").Get();
        var iceCreamNames = GetStaticField<string[]>(comp.GetType(), "iceCreamNames").Get();
        var ix = Array.IndexOf(iceCreams, 14);
        var directions = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        var displayedIceCreamSprites = iceCreams.Where(x => x != 14).Select(index => MisterSofteeSprites.First(sprite => sprite.name == iceCreamNames[index])).ToArray();
        yield return question(SMisterSoftee.SpongebobPosition).Answers(directions[ix]);
        yield return question(SMisterSoftee.TreatsPresent).Answers(displayedIceCreamSprites);
    }
}