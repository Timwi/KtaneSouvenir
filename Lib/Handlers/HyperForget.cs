using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SHyperForget
{
    [SouvenirQuestion("What was the rotation for the {1} stage in {0}?", ThreeColumns6Answers, "XY", "XZ", "XW", "YX", "YZ", "YW", "ZX", "ZY", "ZW", "WX", "WY", "WZ", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslatableStrings = ["the HyperForget whose rotation in the {1} stage was {0}"])]
    Rotations
}

public partial class SouvenirModule
{
    [SouvenirHandler("HyperForget", "HyperForget", typeof(SHyperForget), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessHyperForget(ModuleData module)
    {
        var comp = GetComponent(module, "HyperForget");
        const string moduleId = "HyperForget";

        yield return null;

        if (module.IsSolved)
        {
            _hyperForgetStages.Add(new());
            yield return legitimatelyNoQuestion(module, "No question for HyperForget because there were no stages.");
        }

        var rots = GetListField<string>(comp, "rotationList").Get(minLength: 1);
        _hyperForgetStages.Add(rots);

        yield return null;

        if (_hyperForgetStages.Select(s => s.Count).Distinct().Count() is not 1)
            throw new AbandonModuleException($"Expected consistent stage counts among HyperForget modules, got {_hyperForgetStages.Select(s => s.Count).JoinString(", ")}");

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);

        if (_hyperForgetStages.Count != _moduleCounts[moduleId])
            throw new AbandonModuleException("The number of handlers did not match the number of HyperForget modules.");

        var currentStage = GetField<int>(comp, "currentStage").Get();
        if (currentStage < 1)
            yield return legitimatelyNoQuestion(module, "No question for HyperForget because not enough stages were shown.");

        if (_moduleCounts[moduleId] == 1)
        {
            // Only one HyperForget: No need for the disambiguation phrase
            addQuestions(module, rots.Take(currentStage).Select((rot, ix) => makeQuestion(Question.HyperForgetRotations, moduleId, 1, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { rot })));
            yield break;
        }

        var uniqueStages = Enumerable.Range(1, currentStage).Where(stage => _hyperForgetStages.Count(display => display[stage - 1] == rots[stage - 1]) == 1).Take(2).ToArray();
        if (uniqueStages.Length == 0 || currentStage == 1)
        {
            var id = GetField<int>(comp, "moduleId").Get();
            yield return legitimatelyNoQuestion(module, $"No question for HyperForget #{id} because there are not enough stages at which this one had a unique rotation.");
        }

        var qs = new List<QandA>();
        for (var stage = 0; stage < currentStage; stage++)
        {
            var uniqueStage = uniqueStages.FirstOrDefault(s => s - 1 != stage);
            if (uniqueStage != 0)
            {
                qs.Add(makeQuestion(Question.HyperForgetRotations, moduleId, 0,
                    formattedModuleName: string.Format(translateString(Question.HyperForgetRotations, "the HyperForget whose rotation in the {1} stage was {0}"), rots[uniqueStage - 1], Ordinal(uniqueStage)),
                    formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { rots[stage] }));
            }
        }
        addQuestions(module, qs);
    }
}