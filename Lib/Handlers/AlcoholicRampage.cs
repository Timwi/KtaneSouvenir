using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SAlcoholicRampage
{
    [Question("Who was the {1} mercenary displayed in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QMercenaries,

    [Discriminator("the Alcoholic Rampage where the {0} mercenary was this", UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DMercenaries
}

public partial class SouvenirModule
{
    [Handler("alcoholicRampageModule", "Alcoholic Rampage", typeof(SAlcoholicRampage), "Quinn Wuest")]
    [ManualQuestion("Which mercenaries were displayed?")]
    private IEnumerator<SouvenirInstruction> ProcessAlcoholicRampage(ModuleData module)
    {
        var comp = GetComponent(module, "AlcoholicRampageScript");
        var fldStage = GetIntField(comp, "stage");
        var fldChosenMerc = GetIntField(comp, "chosenMerc");
        var mercs = new int[] { -1, -1, -1 };

        var mercIcons = GetArrayField<Sprite>(comp, "mercIcons", isPublic: true).Get(expectedLength: 8).TranslateSprites(800f).ToArray();

        while (fldStage.Get() != 3)
        {
            mercs[fldStage.Get()] = fldChosenMerc.Get();
            yield return null;
        }

        yield return WaitForSolve;

        for (var s = 0; s < 3; s++)
        {
            if (mercs[s] == -1)
                throw new AbandonModuleException($"Did not get a mercenary number for stage {s + 1}.");
            yield return question(SAlcoholicRampage.QMercenaries, args: [Ordinal(s + 1)]).AvoidDiscriminators($"merc-{s}").Answers(mercIcons[mercs[s]], all: mercIcons);
            yield return new Discriminator(SAlcoholicRampage.DMercenaries, $"merc-{s}", mercs[s], args: [Ordinal(s + 1)], questionSprite: mercIcons[mercs[s]]);
        }
    }
}
