using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMatchRefereeing
{
    [SouvenirQuestion("Which planet was present in the {1} stage of {0}?", TwoColumns4Answers, Type = AnswerType.Sprites, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Planet
}

public partial class SouvenirModule
{
    [SouvenirHandler("matchRefereeing", "Match Refereeing", typeof(SMatchRefereeing), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessMatchRefereeing(ModuleData module)
    {
        var comp = GetComponent(module, "MeteoRefereeingScript");
        yield return WaitForSolve;

        var planetsArr = GetField<Array>(comp, "planetsUsed").Get(x => x.Length != 3 ? "expected length 3" : null);
        var planetsUsed = new List<Sprite[]>();
        var planetImages = GetArrayField<Sprite>(comp, "planetImages", isPublic: true).Get().Select(sprite => sprite.TranslateSprite(280)).ToArray();

        for (var stage = 0; stage < 3; stage++)
        {
            if (planetsArr.GetValue(stage) is not Array innerArr)
                throw new AbandonModuleException($"Abandoning Match Refereeing because planetsUsed[{stage}] is not an array.");
            else if (innerArr.Length != 2 + stage)
                throw new AbandonModuleException($"Abandoning Match Refereeing because planetsUsed[{stage}] has unexpected length {innerArr.Length}. Expected {2 + stage}.");
            planetsUsed.Add(innerArr.Cast<object>().Select(pl =>
            {
                var intValue = (int) pl;
                return intValue < 0 || intValue >= planetImages.Length
                    ? throw new AbandonModuleException($"Abandoning Match Refereeing because planetsUsed[{stage}] contains value {pl} with integer value {intValue}, which is outside the range for planetImages (0–{planetImages.Length - 1}).")
                    : planetImages[intValue];
            }).ToArray());
        }

        var allPlanetsUsed = planetsUsed.SelectMany(x => x).ToArray();

        var qs = new List<QandA>();
        for (var stage = 0; stage < 2; stage++)
            qs.Add(makeQuestion(Question.MatchRefereeingPlanet, module,
                formatArgs: new[] { Ordinal(stage + 1) },
                correctAnswers: planetsUsed[stage],
                preferredWrongAnswers: allPlanetsUsed,
                allAnswers: planetImages));
        addQuestions(module, qs);
    }
}