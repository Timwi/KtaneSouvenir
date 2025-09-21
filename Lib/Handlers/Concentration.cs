using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SConcentration
{
    [SouvenirQuestion("What number began here in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true)]
    [AnswerGenerator.Integers(1, 15)]
    StartingDigit,

    [SouvenirDiscriminator("the Concentration which began with {1} in the {0} position (in reading order)", Arguments = [QandA.Ordinal, "1", QandA.Ordinal, "2", QandA.Ordinal, "3"], ArgumentGroupSize = 2)]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("ConcentrationModule", "Concentration", typeof(SConcentration), "Anonymous", IsBossModule = true)]
    private IEnumerator<SouvenirInstruction> ProcessConcentration(ModuleData module)
    {
        var comp = GetComponent(module, "ConcentrationModule");

        yield return null; // The module waits one frame after Start(), so we wait two (Souvenir already waits one frame on its own)

        var stage = GetArrayField<int>(comp, "_initialOrder").Get(expectedLength: 15, validator: i => i is < 0 or > 14 ? "Out of range 0–14" : null);
        if (stage.Distinct().Count() != 15)
            throw new AbandonModuleException($"Unexpected duplicate numbers. {stage.Stringify()}");

        var swapCount = GetIntField(comp, "_lastStage").Get(0, 106) - 1;

        if (swapCount < 1)
            yield return legitimatelyNoQuestion(module, "No swaps occurred.");

        var swappedPositions = new HashSet<int>(GetArrayField<(int one, int two)>(comp, "_swaps")
            .Get(expectedLength: Math.Max(swapCount, 0), validator: t => t.one is < 0 or > 14 ? "First out of range 0–14" : t.two <= t.one || t.two > 14 ? "Second out of range (first+1)–14" : null)
            .SelectMany(t => new[] { t.one, t.two }));

        yield return WaitForUnignoredModules;

        foreach (var ix in swappedPositions)
            yield return question(SConcentration.StartingDigit, questionSprite: Sprites.GenerateGridSprite(3, 5, ix)).AvoidDiscriminators($"pos-{ix}").Answers((stage[ix] + 1).ToString());
        for (var ix = 0; ix < 15; ix++)
            yield return new Discriminator(SConcentration.Discriminator, $"pos-{ix}", stage[ix], args: [Ordinal(ix + 1), (stage[ix] + 1).ToString()]) { Priority = swappedPositions.Contains(ix) ? 0 : 1 };
    }
}
