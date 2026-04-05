using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SHunting
{
    [Question("Which of these symbols was displayed in the {1} stage of {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "HuntingSprites", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DisplayedSymbols
}

public partial class SouvenirModule
{
    [Handler("hunting", "Hunting", typeof(SHunting), "Quinn Wuest")]
    [ManualQuestion("Which pictograms were displayed?")]
    private IEnumerator<SouvenirInstruction> ProcessHunting(ModuleData module)
    {
        var comp = GetComponent(module, "hunting");
        var fldStage = GetIntField(comp, "stage");
        var fldDisplayedClues = GetField<TextMesh[]>(comp, "cluesTextMesh", isPublic: true);

        yield return WaitForActivate;

        var hasRowFirst = new bool[4];
        var clues = new Sprite[4][] { new Sprite[2], new Sprite[2], new Sprite[2], new Sprite[2] };
        var stringIxs = "oWzAMUfH";

        while (module.Unsolved)
        {
            var stage = fldStage.Get() - 1;
            var displayedClues = fldDisplayedClues.Get();
            clues[stage][0] = HuntingSprites[stringIxs.IndexOf(displayedClues[0].text)];
            clues[stage][1] = HuntingSprites[stringIxs.IndexOf(displayedClues[1].text)];
            yield return new WaitForSeconds(.1f);
        }
        for (int st = 0; st < 4; st++)
            yield return question(SHunting.DisplayedSymbols, args: [Ordinal(st + 1)]).Answers(clues[st], all: HuntingSprites);
    }
}
