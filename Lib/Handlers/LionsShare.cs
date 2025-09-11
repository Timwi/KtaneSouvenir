using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLionsShare
{
    [SouvenirQuestion("Which year was displayed on {0}?", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16")]
    Year,
    
    [SouvenirQuestion("Which lion was present but removed in {0}?", TwoColumns4Answers, ExampleAnswers = ["Taka", "Mufasa", "Uru", "Ahadi", "Zama", "Mohatu", "Kion", "Kiara", "Kopa", "Kovu", "Vitani", "Nuka", "Mheetu", "Zira", "Nala", "Simba", "Sarabi", "Sarafina"])]
    RemovedLions
}

public partial class SouvenirModule
{
    [SouvenirHandler("LionsShareModule", "Lion’s Share", typeof(SLionsShare), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessLionsShare(ModuleData module)
    {
        var comp = GetComponent(module, "LionsShareModule");
        var yearText = GetField<TextMesh>(comp, "Year", isPublic: true).Get().text;
        yield return !int.TryParse(yearText, out var year) || year < 1 || year > 16
            ? throw new AbandonModuleException($"Expected year number between 1 and 16; got: {yearText}")
            : (YieldInstruction) WaitForSolve;
        var lionNames = GetArrayField<string>(comp, "_lionNames").Get(minLength: 2);
        var correctPortions = GetArrayField<int>(comp, "_correctPortions").Get(expectedLength: lionNames.Length);
        var removedLions = Enumerable.Range(0, lionNames.Length).Where(ix => correctPortions[ix] == 0).Select(ix => lionNames[ix]).ToArray();
        var allLionNames = GetListField<string>(comp, "_allLionNames").Get(expectedLength: 35);

        var qs = new List<QandA> { makeQuestion(Question.LionsShareYear, module, correctAnswers: new[] { yearText }) };
        if (removedLions.Length > 0)
            qs.Add(makeQuestion(Question.LionsShareRemovedLions, module, correctAnswers: removedLions, preferredWrongAnswers: allLionNames.ToArray()));
        addQuestions(module, qs);
    }
}