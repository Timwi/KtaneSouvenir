using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SForgetMeNot
{
    [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslatableStrings = ["the Forget Me Not which displayed a {0} in the {1} stage"])]
    [AnswerGenerator.Integers(0, 9)]
    DisplayedDigits
}

public partial class SouvenirModule
{
    [SouvenirHandler("MemoryV2", "Forget Me Not", typeof(SForgetMeNot), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessForgetMeNot(ModuleData module)
    {
        var comp = GetComponent(module, "AdvancedMemory");
        const string moduleId = "MemoryV2";

        var fldDisplayedDigits = GetArrayField<int>(comp, "Display");
        yield return WaitForActivate;
        yield return null; // Wait one frame to make sure the Display field has been set.

        var myDisplay = fldDisplayedDigits.Get(minLength: 0, validator: d => d is < 0 or > 9 ? "expected range 0-9" : null);
        if (_forgetMeNotDisplays.Any() && myDisplay.Length != _forgetMeNotDisplays[0].Length)
            throw new AbandonModuleException("The number of stages in each ‘Display’ is inconsistent.");
        _forgetMeNotDisplays.Add(myDisplay);

        if (myDisplay.Length == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        yield return WaitForUnignoredModules;

        var myIgnoredList = GetStaticField<string[]>(comp.GetType(), "ignoredModules", isPublic: true).Get();
        var displayedStageCount = Bomb.GetSolvedModuleNames().Count(x => !myIgnoredList.Contains(x));

        if (_forgetMeNotDisplays.Count != _moduleCounts[moduleId])
            throw new AbandonModuleException("The number of displays did not match the number of Forget Me Not modules.");

        if (_moduleCounts[moduleId] == 1)
            addQuestions(module, myDisplay.Take(displayedStageCount).Select((digit, ix) => makeQuestion(Question.ForgetMeNotDisplayedDigits, moduleId, 1, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { digit.ToString() })));
        else
        {
            var uniqueStages = Enumerable.Range(1, displayedStageCount).Where(stage => _forgetMeNotDisplays.Count(display => display[stage - 1] == myDisplay[stage - 1]) == 1).Take(2).ToArray();
            if (uniqueStages.Length == 0 || displayedStageCount == 1)
                yield return legitimatelyNoQuestion(module, $"There are not enough stages at which this one (#{(GetIntField(comp, "thisLoggingID", isPublic: true).Get())}) had a unique displayed number.");

            var qs = new List<QandA>();
            for (var stage = 0; stage < displayedStageCount; stage++)
            {
                var uniqueStage = uniqueStages.FirstOrDefault(s => s != stage + 1);
                if (uniqueStage != 0)
                {
                    qs.Add(makeQuestion(Question.ForgetMeNotDisplayedDigits, moduleId, 0,
                        formattedModuleName: string.Format(translateString(Question.ForgetMeNotDisplayedDigits, "the Forget Me Not which displayed a {0} in the {1} stage"), myDisplay[uniqueStage - 1], Ordinal(uniqueStage)),
                        formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { myDisplay[stage].ToString() }));
                }
            }
            addQuestions(module, qs);
        }
    }
}
