using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SAmusementParks
{
    [SouvenirQuestion("Which ride was available in {0}?", OneColumn4Answers, "Carousel", "Drop Tower", "Enterprise", "Ferris Wheel", "Ghost Train", "Inverted Coaster", "Junior Coaster", "Launched Coaster", "Log Flume", "Omnimover", "Pirate Ship", "River Rapids", "Safari", "Star Flyer", "Top Spin", "Tourbillon", "Vintage Cars", "Walkthrough", "Wooden Coaster")]
    Rides
}

public partial class SouvenirModule
{
    [SouvenirHandler("amusementParks", "Amusement Parks", typeof(SAmusementParks), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessAmusementParks(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "amusementParksScript");
        var avail = GetField<IEnumerable>(comp, "ridesAvailable").Get(v => v.Cast<object>().Count() != 3 ? "Expected length 3" : null).Cast<object>().ToArray();
        var correct = GetField<object>(comp, "correctInvestment").Get();

        var fldName = GetField<string>(avail[0], "name", true);
        var options = avail.Select(r => fldName.GetFrom(r, v => !Question.AmusementParksRides.GetAnswers().Contains(v) ? $"Unknown ride type {v}" : null));
        var correctName = fldName.GetFrom(correct, v => !Question.AmusementParksRides.GetAnswers().Contains(v) ? $"Unknown ride type {v}" : null);

        addQuestion(module, Question.AmusementParksRides,
            correctAnswers: options.Except(new[] { correctName }).ToArray(),
            allAnswers: Question.AmusementParksRides.GetAnswers().Except(new[] { correctName }).ToArray());
    }
}