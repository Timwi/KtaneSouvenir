using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SHyperForget
{
    [SouvenirQuestion("What was the rotation for the {1} stage in {0}?", ThreeColumns6Answers, "XY", "XZ", "XW", "YX", "YZ", "YW", "ZX", "ZY", "ZW", "WX", "WY", "WZ", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslatableStrings = ["the HyperForget whose rotation in the {1} stage was {0}"])]
    Rotations,

    [SouvenirDiscriminator("the HyperForget whose rotation in the {1} stage was {0}", Arguments = ["XY", QandA.Ordinal, "XZ", QandA.Ordinal, "XW", QandA.Ordinal, "YX", QandA.Ordinal, "YZ", QandA.Ordinal, "YW", QandA.Ordinal, "ZX", QandA.Ordinal, "ZY", QandA.Ordinal, "ZW", QandA.Ordinal, "WX", QandA.Ordinal, "WY", QandA.Ordinal, "WZ", QandA.Ordinal], ArgumentGroupSize = 2)]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("HyperForget", "HyperForget", typeof(SHyperForget), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessHyperForget(ModuleData module)
    {
        var comp = GetComponent(module, "HyperForget");

        yield return null;

        if (module.IsSolved)
            yield return legitimatelyNoQuestion(module, "No question for HyperForget because there were no stages.");

        var rots = GetListField<string>(comp, "rotationList").Get(minLength: 1);

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);

        var currentStage = GetField<int>(comp, "currentStage").Get();
        if (currentStage < 1)
            yield return legitimatelyNoQuestion(module, "No question for HyperForget because not enough stages were shown.");

        for (var stage = 0; stage < currentStage; stage++)
        {
            yield return new Discriminator(SHyperForget.Discriminator, $"stage{stage}", rots[stage], [rots[stage], Ordinal(stage + 1)]);
            yield return question(SHyperForget.Rotations, args: [Ordinal(stage + 1)]).Answers(rots[stage]);
        }
    }
}
