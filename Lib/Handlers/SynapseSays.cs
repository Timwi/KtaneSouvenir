using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSynapseSays
{
    [SouvenirQuestion("What number was displayed in the {1} stage of {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 4)]
    Displays,

    [SouvenirQuestion("What color flashed {1} in the {2} stage of {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Flashes,

    [SouvenirQuestion("What color was in the {1} position of the {2} stage of {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Positions
}

public partial class SouvenirModule
{
    [SouvenirHandler("synapseSays", "Synapse Says", typeof(SSynapseSays), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessSynapseSays(ModuleData module)
    {
        var comp = GetComponent(module, "SynapseSaysScript");
        var fldStage = GetIntField(comp, "stage");
        var fldPositions = GetListField<int>(comp, "barrange");
        var fldFlashes = GetField<int[,,]>(comp, "seq");
        var fldDisplayText = GetField<TextMesh>(comp, "display", isPublic: true);

        var positions = new int[5][] { new int[4], new int[4], new int[4], new int[4], new int[4] };
        var allFlashes = new int[5][] { new int[4], new int[4], new int[4], new int[4], new int[4] };
        var displays = new string[5];

        var stage = -1;
        while (module.Unsolved)
        {
            if (fldStage.Get() != stage)
            {
                stage = fldStage.Get();
                var flashes = fldFlashes.Get();
                positions[stage] = fldPositions.Get().ToArray();
                for (var i = 0; i < 4; i++)
                    allFlashes[stage][i] = flashes[stage, i, 1];
                if (fldDisplayText.Get().text is var disp && disp != "")
                    displays[stage] = disp;
            }
            yield return null;
        }

        var colorNames = new string[] { "Red", "Yellow", "Green", "Blue" };
        for (var st = 0; st < 5; st++)
        {
            yield return question(SSynapseSays.Displays, args: [Ordinal(st + 1)]).Answers(displays[st]);
            for (var ix = 0; ix < 4; ix++)
            {
                yield return question(SSynapseSays.Flashes, args: [Ordinal(ix + 1), Ordinal(st + 1)]).Answers(colorNames[allFlashes[st][ix]]);
                yield return question(SSynapseSays.Positions, args: [Ordinal(ix + 1), Ordinal(st + 1)]).Answers(colorNames[positions[st][ix]]);
            }
        }
    }
}