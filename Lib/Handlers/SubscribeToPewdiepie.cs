using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSubscribeToPewdiepie
{
    [SouvenirQuestion("How many subscribers does {1} have in {0}?", TwoColumns4Answers, Arguments = ["PewDiePie", "T-Series"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    [AnswerGenerator.Integers(10000000, 99999999)]
    SubCount
}

public partial class SouvenirModule
{
    [SouvenirHandler("subscribeToPewdiepie", "Subscribe to Pewdiepie", typeof(SSubscribeToPewdiepie), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessSubscribeToPewdiepie(ModuleData module)
    {
        var comp = GetComponent(module, "subscribeToPewdiepieScript");

        var pewdiepieNumber = GetField<int>(comp, "startingPewdiepie").Get();
        var tSeriesNumber = GetField<int>(comp, "startingTSeries").Get();

        yield return WaitForSolve;

        addQuestions(module,
           makeQuestion(Question.SubscribeToPewdiepieSubCount, module, formatArgs: new[] { "PewDiePie" }, correctAnswers: new[] { pewdiepieNumber.ToString() }),
           makeQuestion(Question.SubscribeToPewdiepieSubCount, module, formatArgs: new[] { "T-Series" }, correctAnswers: new[] { tSeriesNumber.ToString() }));
    }
}