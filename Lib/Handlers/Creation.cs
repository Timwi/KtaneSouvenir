using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCreation
{
    [Question("What were the weather conditions on the first day in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites, SpriteFieldName = "CreationSprites")]
    Weather
}

public partial class SouvenirModule
{
    [Handler("CreationModule", "Creation", typeof(SCreation), "CaitSith2")]
    [ManualQuestion("What was the weather condition on the first day?")]
    private IEnumerator<SouvenirInstruction> ProcessCreation(ModuleData module)
    {
        var comp = GetComponent(module, "CreationModule");
        var fldDay = GetIntField(comp, "Day");
        var fldWeather = GetField<string>(comp, "Weather");

        var weatherNames = new string[] { "Clear", "Heat Wave", "Meteor Shower", "Rain", "Windy" };

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

        if (allWeather[0] == "Clear" && GetComponent<KMBombInfo>().GetBatteryHolderCount() >= 3)
            yield return legitimatelyNoQuestion(module, "This specific answer can be reverse engineered.");

        yield return question(SCreation.Weather).Answers(CreationSprites.First(spr => spr.name == allWeather[0]), all: CreationSprites);
    }
}
