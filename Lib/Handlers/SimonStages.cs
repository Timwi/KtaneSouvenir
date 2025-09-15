using System.Collections.Generic;
using System.Linq;
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

        addQuestions(module, indicators.Select((ans, i) => makeQuestion(Question.SimonStagesIndicator, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { ans }))
            .Concat(stage1Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, module, formatArgs: new[] { Ordinal(i + 1), "first" }, correctAnswers: new[] { ans })))
            .Concat(stage2Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, module, formatArgs: new[] { Ordinal(i + 1), "second" }, correctAnswers: new[] { ans })))
            .Concat(stage3Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, module, formatArgs: new[] { Ordinal(i + 1), "third" }, correctAnswers: new[] { ans })))
            .Concat(stage4Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, module, formatArgs: new[] { Ordinal(i + 1), "4th" }, correctAnswers: new[] { ans })))
            .Concat(stage5Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, module, formatArgs: new[] { Ordinal(i + 1), "5th" }, correctAnswers: new[] { ans }))));
    }
}