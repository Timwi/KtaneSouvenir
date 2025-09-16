using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SForgetMeNot
{
    [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Question,

    [SouvenirDiscriminator("the Forget Me Not which displayed a {0} in the {1} stage", Arguments = ["1", QandA.Ordinal, "2", QandA.Ordinal], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("MemoryV2", "Forget Me Not", typeof(SForgetMeNot), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessForgetMeNot(ModuleData module)
    {
        var comp = GetComponent(module, "AdvancedMemory");

        var fldDisplayedDigits = GetArrayField<int>(comp, "Display");
        yield return WaitForActivate;
        yield return null; // Wait one frame to make sure the Display field has been set.

        var myDisplay = fldDisplayedDigits.Get(minLength: 0, validator: d => d is < 0 or > 9 ? "expected range 0-9" : null);
        if (myDisplay.Length == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        yield return WaitForUnignoredModules;

        var myIgnoredList = GetStaticField<string[]>(comp.GetType(), "ignoredModules", isPublic: true).Get();
        var displayedStageCount = Bomb.GetSolvedModuleNames().Count(x => !myIgnoredList.Contains(x));

        for (var stage = 0; stage < myDisplay.Length && stage < displayedStageCount; stage++)
        {
            yield return new Discriminator(SForgetMeNot.Discriminator, $"stage{stage}", myDisplay[stage], [myDisplay[stage].ToString(), Ordinal(stage + 1)]);
            yield return question(SForgetMeNot.Question, args: [Ordinal(stage + 1)]).AvoidDiscriminators($"stage{stage}").Answers(myDisplay[stage].ToString());
        }
    }
}
