using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SHinges
{
    [SouvenirQuestion("Which of these hinges was initially {1} {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "HingesSprites", Arguments = ["present on", "absent from"], ArgumentGroupSize = 1, TranslateArguments = [true], TranslatableStrings = ["the Hinges where this hinge was initally present", "the Hinges where this hinge was initally absent"])]
    Initial,

    [SouvenirDiscriminator("the Hinges where this hinge was initally {0}", UsesQuestionSprite = true, Arguments = ["present", "absent"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("hinges", "Hinges", typeof(SHinges), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessHinges(ModuleData module)
    {
        var comp = GetComponent(module, "Hinges");
        var initialHingesStatus = GetArrayField<int>(comp, "hingeStatus").Get(expectedLength: 8, validator: i => i is not 0 and not 1 ? "expected value 0 or 1" : null).ToArray();

        var absentHinges = new List<Sprite>();
        var presentHinges = new List<Sprite>();
        for (var pos = 0; pos < 8; pos++)
        {
            var present = initialHingesStatus[pos] == 1;
            (present ? presentHinges : absentHinges).Add(HingesSprites[pos]);
            if ((pos, present) is ( < 4, true) or (4, _) or ( > 4, false))
                yield return new Discriminator(SHinges.Discriminator, $"hinge{pos}", present, args: [present ? "present" : "absent"], questionSprite: HingesSprites[pos], avoidAnswers: [HingesSprites[pos]]);
        }

        yield return WaitForSolve;

        // There are eight hinges in total, so at least one question will generate.
        if (presentHinges.Count is <= 4 and >= 1)
            yield return question(SHinges.Initial, args: ["present on"]).Answers(presentHinges.ToArray());
        if (absentHinges.Count is <= 4 and >= 1)
            yield return question(SHinges.Initial, args: ["absent from"]).Answers(absentHinges.ToArray());
    }
}
