using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonStages
{
    [SouvenirQuestion("What color was the indicator in the {1} stage in {0}?", ThreeColumns6Answers, "red", "blue", "yellow", "orange", "magenta", "green", "pink", "lime", "cyan", "white", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Indicator,

    [SouvenirQuestion("Which color flashed {1} in the {2} stage in {0}?", ThreeColumns6Answers, "red", "blue", "yellow", "orange", "magenta", "green", "pink", "lime", "cyan", "white", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Flashes
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonStages", "Simon Stages", typeof(SSimonStages), "Espik")]
    private IEnumerator<SouvenirInstruction> ProcessSimonStages(ModuleData module)
    {
        var comp = GetComponent(module, "SimonStagesHandler");
        var indicatorList = GetMethod<List<string>>(comp, "grabIndicatorColorsAll", numParameters: 0, isPublic: true);
        var flashList = GetMethod<List<string>>(comp, "grabSequenceColorsOneStage", numParameters: 1, isPublic: true);

        yield return WaitForSolve;

        var indicators = indicatorList.Invoke();
        var stage1Flash = flashList.Invoke(1);
        var stage2Flash = flashList.Invoke(2);
        var stage3Flash = flashList.Invoke(3);
        var stage4Flash = flashList.Invoke(4);
        var stage5Flash = flashList.Invoke(5);

        for (var i = 0; i < indicators.Count; i++)
            yield return question(SSimonStages.Indicator, args: [Ordinal(i + 1)]).Answers(indicators[i]);
        for (var i = 0; i < stage1Flash.Count; i++)
            yield return question(SSimonStages.Flashes, args: [Ordinal(i + 1), Ordinal(1)]).Answers(stage1Flash[i]);
        for (var i = 0; i < stage2Flash.Count; i++)
            yield return question(SSimonStages.Flashes, args: [Ordinal(i + 1), Ordinal(2)]).Answers(stage2Flash[i]);
        for (var i = 0; i < stage3Flash.Count; i++)
            yield return question(SSimonStages.Flashes, args: [Ordinal(i + 1), Ordinal(3)]).Answers(stage3Flash[i]);
        for (var i = 0; i < stage4Flash.Count; i++)
            yield return question(SSimonStages.Flashes, args: [Ordinal(i + 1), Ordinal(4)]).Answers(stage4Flash[i]);
        for (var i = 0; i < stage5Flash.Count; i++)
            yield return question(SSimonStages.Flashes, args: [Ordinal(i + 1), Ordinal(5)]).Answers(stage5Flash[i]);
    }
}
