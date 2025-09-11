using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SFindTheDate
{
    [SouvenirQuestion("What was the month displayed in the {1} stage of {0}?", TwoColumns4Answers, "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Month,
    
    [SouvenirQuestion("What was the day displayed in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 31)]
    Day,
    
    [SouvenirQuestion("What was the year displayed in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 2899, "000")]
    Year
}

public partial class SouvenirModule
{
    [SouvenirHandler("DateFinder", "Find The Date", typeof(SFindTheDate), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessFindTheDate(ModuleData module)
    {
        var comp = GetComponent(module, "DateFinder");
        var fldStage = GetIntField(comp, "count");
        var fldDate = GetIntField(comp, "day");
        var fldMonth = GetField<string>(comp, "month");
        var fldYear = GetIntField(comp, "year");
        var fldCentury = GetIntField(comp, "century");

        var dateArr = new int[3];
        var yearArr = new string[3];
        var monthArr = new string[3];
        var currentStage = -1;

        while (module.Unsolved)
        {
            var newStage = fldStage.Get();
            if (currentStage != newStage)
            {
                currentStage = newStage;
                dateArr[newStage] = fldDate.Get();
                monthArr[newStage] = fldMonth.Get();
                yearArr[newStage] = "" + fldCentury.Get() + fldYear.Get();
            }
            yield return null;
        }

        var qs = new List<QandA>();

        for (var i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.FindTheDateMonth, module,
            formatArgs: new[] { Ordinal(i + 1) },
            correctAnswers: new[] { monthArr[i] }));

            qs.Add(makeQuestion(Question.FindTheDateDay, module,
            formatArgs: new[] { Ordinal(i + 1) },
            correctAnswers: new[] { dateArr[i].ToString() }));

            qs.Add(makeQuestion(Question.FindTheDateYear, module,
            formatArgs: new[] { Ordinal(i + 1) },
            correctAnswers: new[] { yearArr[i] }));
        }

        addQuestions(module, qs);
    }
}