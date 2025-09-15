using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonShuffles
{
    [SouvenirQuestion("What was the {1} flash of the {2} stage of {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Magenta", "White", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Flashes
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSSimonShuffles", "Simon Shuffles", typeof(SSimonShuffles), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessSimonShuffles(ModuleData module)
    {
        var comp = GetComponent(module, "SimonShufflesScript");
        var stageComp = GetIntField(comp, "Stage");
        var flashesComp = GetListField<int>(comp, "FlashingSequence");
        var colourNames = GetArrayField<string>(comp, "ColourNames").Get();
        var flashesArr = new List<int>[3];
        while (module.Unsolved)
        {
            var stage = stageComp.Get();
            var flashes = flashesComp.Get();
            flashesArr[stage] = flashes.ToList();
            yield return null;
        }

        for (var s = 0; s < 3; s++)
            for (var f = 0; f < flashesArr[s].Count; f++)
                yield return question(SSimonShuffles.Flashes, args: [Ordinal(f + 1), Ordinal(s + 1)]).Answers(colourNames[flashesArr[s][f]]);
    }
}