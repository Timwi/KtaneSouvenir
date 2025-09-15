using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SForgetThis
{
    [SouvenirQuestion("What color was the LED in the {1} stage of {0}?", ThreeColumns6Answers, "Cyan", "Magenta", "Yellow", "Black", "White", "Green", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors,

    [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", ThreeColumns6Answers, Type = AnswerType.AsciiMazeFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0-9A-Z")]
    Digits
}

public partial class SouvenirModule
{
    [SouvenirHandler("forgetThis", "Forget This", typeof(SForgetThis), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessForgetThis(ModuleData module)
    {
        var comp = GetComponent(module, "ForgetThis");
        const string moduleId = "forgetThis";

        if (GetField<bool>(comp, "autoSolved").Get())
            yield return legitimatelyNoQuestion(module, "It solved itself due to a lack of stages.");

        var myColors = GetListField<int>(comp, "stageColors").Get();
        var myDigits = GetListField<int>(comp, "stageNumbers").Get();

        if (myColors.Count != myDigits.Count)
            throw new AbandonModuleException($"The number of colors ({myColors.Count}) did not match the number of digits ({myDigits.Count})");
        if (_ftColors.Any() && _ftColors.Last().Count != myColors.Count)
            throw new AbandonModuleException("The number of colors was not consistent across all Forget This modules.");
        if (_ftDigits.Any() && _ftDigits.Last().Count != myDigits.Count)
            throw new AbandonModuleException("The number of digits was not consistent across all Forget This modules.");

        _ftColors.Add(myColors);
        _ftDigits.Add(myDigits);

        yield return WaitForUnignoredModules;

        var displayedStagesCount = GetIntField(comp, "curStageNum").Get(min: 0, max: myColors.Count);

        var allColors = new[] { "Cyan", "Magenta", "Yellow", "Black", "White", "Green" };
        var base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var chosenStage = Rnd.Range(0, displayedStagesCount);

        string formattedName = null;
        if (_moduleCounts[moduleId] > 1)
        {
            for (var stage = 0; stage < displayedStagesCount; stage++)
            {
                if (stage == chosenStage)
                    continue;
                var formatCandidates = new List<string>();
                if (_ftColors.Count(c => c[stage] == myColors[stage]) == 1)
                    formatCandidates.Add(string.Format(translateString(SForgetThis.Colors, "the Forget This whose LED was {0} in the {1} stage"), translateAnswer(SForgetThis.Colors, allColors[myColors[stage]]), Ordinal(stage + 1)));
                if (_ftDigits.Count(d => d[stage] == myDigits[stage]) == 1)
                    formatCandidates.Add(string.Format(translateString(SForgetThis.Colors, "the Forget This which displayed {0} in the {1} stage"), base36[myDigits[stage]], Ordinal(stage + 1)));
                if (formatCandidates.Count > 0)
                {
                    formattedName = formatCandidates.PickRandom();
                    break;
                }
            }
            if (formattedName == null)
                yield return legitimatelyNoQuestion(module, $"There were not enough stages in which this one (#{GetIntField(comp, "_moduleId").Get()}) was unique.");
        }
        formattedName ??= _translation?.Translate(SForgetThis.Colors).ModuleName ?? "Forget This";
        yield return question(SForgetThis.Colors, args: [Ordinal(chosenStage + 1)]).Answers(allColors[myColors[chosenStage]]);
        yield return question(SForgetThis.Digits, args: [Ordinal(chosenStage + 1)]).Answers(base36[myDigits[chosenStage]].ToString());
    }
}
