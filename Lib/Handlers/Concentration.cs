using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SConcentration
{
    [SouvenirQuestion("What number began here in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true)]
    [AnswerGenerator.Integers(1, 15)]
    StartingDigit
}

public partial class SouvenirModule
{
    [SouvenirHandler("ConcentrationModule", "Concentration", typeof(SConcentration), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessConcentration(ModuleData module)
    {
        var comp = GetComponent(module, "ConcentrationModule");
        const string moduleId = "ConcentrationModule";

        yield return null; // The module waits one frame after Start(), so we wait two (Souvenir already waits one frame on its own)

        var stage = GetArrayField<int>(comp, "_initialOrder").Get(expectedLength: 15, validator: i => i is < 0 or > 14 ? "Out of range 0-14" : null);
        if (stage.Distinct().Count() != 15)
            throw new AbandonModuleException($"Unexpected duplicate numbers. {stage.JoinString(" ")}");
        _concentrationStages.Add(stage);

        var swapCount = GetIntField(comp, "_lastStage").Get(0, 106) - 1;

        var swappedPositions = GetArrayField<(int one, int two)>(comp, "_swaps")
            .Get(expectedLength: Math.Max(swapCount, 0), validator: t => t.one is < 0 or > 14 ? "First out of range 0-14" : t.two <= t.one || t.two > 14 ? "Second out of range (first+1)-14" : null)
            .SelectMany(t => new[] { t.one, t.two })
            .Distinct()
            .ToList();

        yield return null; // Wait for other instances of this handler to find the first stage.

        yield return WaitForUnignoredModules;

        if (swapCount < 1)
            yield return legitimatelyNoQuestion(module, "No swaps occurred.");

        var validUnique = Enumerable
            .Range(0, 14)
            .Where(ix => _concentrationStages.Count(s => s[ix] == stage[ix]) == 1)
            .ToArray();

        if (validUnique.Length is 0)
        {
            var id = GetIntField(comp, "_moduleId").Get(min: 0);
            yield return legitimatelyNoQuestion(module, $"No position was unique for this one (#{id}).");
        }

        if (validUnique.Length == 1 && swappedPositions.Contains(validUnique[0]))
            swappedPositions.Remove(validUnique[0]); // swappedPositions cannot have a single element
        foreach (var ix in swappedPositions)
        {
            var unique = validUnique.Except([ix]).PickRandom();
            var moduleName = string.Format(translateString(SConcentration.StartingDigit, "the Concentration which began with {1} in the {0} position (in reading order)"), Ordinal(unique + 1), stage[unique] + 1);
            yield return question(SConcentration.StartingDigit, questionSprite: Sprites.GenerateGridSprite(3, 5, ix)).Answers((stage[ix] + 1).ToString());
        }
    }
}
