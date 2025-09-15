using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SUltraStores
{
    [SouvenirQuestion("What was the {1} rotation in the {2} stage of {0}?", ThreeColumns6Answers, ExampleAnswers = ["UZ", "VU", "WV", "YU", "YW", "YX"], Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    SingleRotation,

    [SouvenirQuestion("What was the {1} rotation in the {2} stage of {0}?", TwoColumns4Answers, ExampleAnswers = ["(XU, VY, WZ)", "(XY, VZ, UW)", "(XZ, YV, WU)", "(YX, UZ, VW)"], Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    MultiRotation
}

public partial class SouvenirModule
{
    [SouvenirHandler("UltraStores", "UltraStores", typeof(SUltraStores), "Marksam")]
    private IEnumerator<SouvenirInstruction> ProcessUltraStores(ModuleData module)
    {
        var comp = GetComponent(module, "UltraStoresScript");
        var fldStage = GetIntField(comp, "stage");
        var fldRotations = GetListField<string>(comp, "rotlist");
        var possibleRotations = GetArrayField<List<string>[]>(comp, "rotations").Get();
        var prevStage = 0;

        var rotations = new List<List<string>> { fldRotations.Get().ToList() };

        while (module.Unsolved)
        {
            var nextStage = fldStage.Get();
            if (nextStage != prevStage)
            {
                rotations.Add(fldRotations.Get().ToList());
                prevStage = nextStage;
            }
            yield return new WaitForSeconds(.1f);
        }

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < i + 3; j++)
            {
                var possibleWrong = possibleRotations[rotations[i][j].Count(ch => ch == ',')].SelectMany(x => x).ToArray();
                yield return question(rotations[i][j].Split(',').Length - 1 == 0 ? SUltraStores.SingleRotation : SUltraStores.MultiRotation, args: [Ordinal(j + 1), Ordinal(i + 1)])
                    .Answers(rotations[i][j], preferredWrong: possibleWrong);
            }
        }
    }
}
