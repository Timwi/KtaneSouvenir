using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SForgetEverything
{
    [SouvenirQuestion("What was the {1} displayed digit in the first stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    StageOneDisplay
}

public partial class SouvenirModule
{
    [SouvenirHandler("HexiEvilFMN", "Forget Everything", typeof(SForgetEverything), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessForgetEverything(ModuleData module)
    {
        var comp = GetComponent(module, "EvilMemory");
        const string moduleId = "HexiEvilFMN";

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
        _feFirstDisplays.Add(myFirstDisplay);

        yield return WaitForUnignoredModules;

        var stageOrdering = GetArrayField<int>(comp, "StageOrdering").Get();
        var myIgnoredList = GetStaticField<string[]>(comp.GetType(), "ignoredModules", isPublic: true).Get();
        if (Array.IndexOf(stageOrdering, 0) + 1 > Bomb.GetSolvableModuleNames().Count(x => !myIgnoredList.Contains(x)))
            yield return legitimatelyNoQuestion(module, "Stage one was not displayed before non-ignored modules were solved.");

        if (_feFirstDisplays.Count != _moduleCounts[moduleId])
            throw new AbandonModuleException($"The number of displays ({_feFirstDisplays.Count}) did not match the number of Forget Everything modules ({_moduleCounts[moduleId]}).");

        if (_moduleCounts[moduleId] == 1)
        {
            module.SolveIndex = 1;
            addQuestions(module, myFirstDisplay.Select((digit, pos) => makeQuestion(Question.ForgetEverythingStageOneDisplay, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { digit.ToString() })));
        }
        else
        {
            var uniquePositions = Enumerable.Range(0, 10).Where(pos => _feFirstDisplays.Count(dis => dis[pos] == myFirstDisplay[pos]) == 1).Take(2).ToArray();
            if (!uniquePositions.Any())
                yield return legitimatelyNoQuestion(module, $"This one (#{GetIntField(comp, "thisLoggingID", isPublic: true)}) had a non-unique first stage.");
            var qs = new List<QandA>();
            for (var pos = 0; pos < 10; pos++)
            {
                if (uniquePositions.Any(p => p != pos))
                {
                    var reference = uniquePositions.Where(p => p != pos).PickRandom();
                    qs.Add(makeQuestion(Question.ForgetEverythingStageOneDisplay, moduleId, 0,
                        formattedModuleName: string.Format(translateString(Question.ForgetEverythingStageOneDisplay, "the Forget Everything whose {0} displayed digit in that stage was {1}"), Ordinal(reference + 1), myFirstDisplay[reference]),
                        formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { myFirstDisplay[pos].ToString() }));
                }
            }
            addQuestions(module, qs);
        }
    }
}
