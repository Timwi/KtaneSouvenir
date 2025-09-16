using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCreation
{
    [SouvenirQuestion("What were the weather conditions on the {1} day in {0}?", TwoColumns4Answers, "Clear", "Heat Wave", "Meteor Shower", "Rain", "Windy", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Weather
}

public partial class SouvenirModule
{
    [SouvenirHandler("CreationModule", "Creation", typeof(SCreation), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessCreation(ModuleData module)
    {
        var comp = GetComponent(module, "CreationModule");
        var fldDay = GetIntField(comp, "Day");
        var fldWeather = GetField<string>(comp, "Weather");

        var weatherNames = SCreation.Weather.GetAnswers();

        yield return WaitForActivate;

        var currentDay = fldDay.Get(min: 1, max: 1);
        var currentWeather = fldWeather.Get(cw => !weatherNames.Contains(cw) ? "unknown weather" : null);
        var allWeather = new List<string>();
        while (true)
        {
            while (fldDay.Get() == currentDay && module.Unsolved && currentWeather == fldWeather.Get())
                yield return new WaitForSeconds(0.1f);

            if (module.IsSolved)
                break;

            if (fldDay.Get() <= currentDay)
                allWeather.Clear();
            else
                allWeather.Add(currentWeather);

            currentDay = fldDay.Get(min: 1, max: 6);
            currentWeather = fldWeather.Get(cw => !weatherNames.Contains(cw) ? "unknown weather" : null);
        }

        for (var i = 0; i < allWeather.Length; i++)
            yield return question(SCreation.Weather, args: [Ordinal(i + 1)]).Answers(allWeather[i]);
    }
}