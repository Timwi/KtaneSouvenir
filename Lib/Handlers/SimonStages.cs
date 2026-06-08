using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonStages
{
    [Question("What color was the indicator in the {1} stage in {0}?", ThreeColumns6Answers, "red", "blue", "yellow", "orange", "magenta", "green", "pink", "lime", "cyan", "white", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Indicator
}

public partial class SouvenirModule
{
    [Handler("simonStages", "Simon Stages", typeof(SSimonStages), "Espik")]
    [ManualQuestion("What color was the indicator in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessSimonStages(ModuleData module)
    {
        var comp = GetComponent(module, "SimonStagesHandler");
        var indicatorList = GetMethod<List<string>>(comp, "grabIndicatorColorsAll", numParameters: 0, isPublic: true);

        yield return WaitForSolve;

        var indicators = indicatorList.Invoke([]);

        for (var i = 0; i < indicators.Count; i++)
            yield return question(SSimonStages.Indicator, args: [Ordinal(i + 1)]).Answers(indicators[i]);
    }
}
