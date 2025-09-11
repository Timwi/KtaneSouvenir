using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPickupIdentification
{
    [SouvenirQuestion("What pickup was shown in the {1} stage of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Item
}

public partial class SouvenirModule
{
    [SouvenirHandler("PickupIdentification", "Pickup Identification", typeof(SPickupIdentification), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessPickupIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "PickupIdentificationScript");

        yield return WaitForSolve;

        var allSprites = GetArrayField<Sprite>(comp, "SeedPacketIdentifier", isPublic: true).Get(expectedLength: 719).TranslateSprites(166).ToArray();
        var chosen = GetArrayField<int>(comp, "Unique").Get(expectedLength: 3, validator: v => v is < 0 or >= 719 ? "Expected pickup number 0–718" : null);

        addQuestions(module, chosen.Select((sprite, stage) => makeQuestion(Question.PickupIdentificationItem, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { allSprites[sprite] }, allAnswers: allSprites)));
    }
}