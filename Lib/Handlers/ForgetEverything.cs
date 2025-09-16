using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SForgetEverything
{
    [SouvenirQuestion("What was the {1} displayed digit in the first stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    QStageOneDisplay,

    [SouvenirDiscriminator("the Forget Everything whose {0} displayed digit in that stage was {1}", Arguments = [QandA.Ordinal, "1", QandA.Ordinal, "2"], ArgumentGroupSize = 2)]
    DStageOneDisplay
}

public partial class SouvenirModule
{
    [SouvenirHandler("HexiEvilFMN", "Forget Everything", typeof(SForgetEverything), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessForgetEverything(ModuleData module)
    {
        var comp = GetComponent(module, "EvilMemory");

        yield return WaitForActivate;
        yield return null; // Wait one extra frame to ensure DialDisplay is set.

        var allDisplays = GetArrayField<int[]>(comp, "DialDisplay").Get(nullAllowed: true);
        if (allDisplays is null)
            yield return legitimatelyNoQuestion(module, "There were no stages.");
        if (allDisplays.Length < 1)
            throw new AbandonModuleException("‘DialDisplay’ had length 0, when I expected length at least 1.");

        var myFirstDisplay = allDisplays.First();
        if (myFirstDisplay.Length != 10)
            throw new AbandonModuleException($"First element of ‘DialDisplay’ had length {myFirstDisplay.Length}, when I expected length 10.");

        yield return WaitForUnignoredModules;

        var stageOrdering = GetArrayField<int>(comp, "StageOrdering").Get();
        var myIgnoredList = GetStaticField<string[]>(comp.GetType(), "ignoredModules", isPublic: true).Get();
        if (Array.IndexOf(stageOrdering, 0) + 1 > Bomb.GetSolvableModuleNames().Count(x => !myIgnoredList.Contains(x)))
            yield return legitimatelyNoQuestion(module, "Stage one was not displayed before non-ignored modules were solved.");

        for (var pos = 0; pos < myFirstDisplay.Length; pos++)
        {
            yield return new Discriminator(SForgetEverything.DStageOneDisplay, $"digit{pos}", myFirstDisplay[pos], [Ordinal(pos + 1), myFirstDisplay[pos].ToString()]);
            yield return question(SForgetEverything.QStageOneDisplay, args: [Ordinal(pos + 1)]).AvoidDiscriminators($"digit{pos}").Answers(myFirstDisplay[pos].ToString());
        }
    }
}
