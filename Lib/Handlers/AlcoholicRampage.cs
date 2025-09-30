using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SAlcoholicRampage
{
    [SouvenirQuestion("Who was the {1} mercenary displayed in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Mercenaries
}

public partial class SouvenirModule
{
    [SouvenirHandler("alcoholicRampageModule", "Alcoholic Rampage", typeof(SAlcoholicRampage), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessAlcoholicRampage(ModuleData module)
    {
        var comp = GetComponent(module, "AlcoholicRampageScript");
        var fldStage = GetIntField(comp, "stage");
        var fldChosenMerc = GetIntField(comp, "chosenMerc");
        var mercs = new int[3];

        var mercIcons = GetArrayField<Sprite>(comp, "mercIcons", isPublic: true).Get(expectedLength: 8).TranslateSprites(800f).ToArray();

        while (fldStage.Get() != 3)
        {
            mercs[fldStage.Get()] = fldChosenMerc.Get();
            yield return null;
        }

        yield return WaitForSolve;
        for (var s = 0; s < 3; s++)
            yield return question(SAlcoholicRampage.Mercenaries, args: [Ordinal(s + 1)]).Answers(mercIcons[mercs[s]], all: mercIcons);
    }
}
