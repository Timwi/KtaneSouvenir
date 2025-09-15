using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLondonUnderground
{
    [SouvenirQuestion("Where did the {1} journey on {0} {2}?", OneColumn4Answers, ExampleAnswers = ["Great Portland Street", "High Street Kensington", "King's Cross St. Pancras", "Mornington Crescent", "Shepherd's Bush Market", "Tottenham Court Road", "Walthamstow Central", "White City/Wood Lane"], TranslateArguments = [false, true], Arguments = [QandA.Ordinal, "depart from", QandA.Ordinal, "arrive to"], ArgumentGroupSize = 2)]
    Stations
}

public partial class SouvenirModule
{
    [SouvenirHandler("londonUnderground", "London Underground", typeof(SLondonUnderground), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessLondonUnderground(ModuleData module)
    {
        var comp = GetComponent(module, "londonUndergroundScript");
        var fldStage = GetIntField(comp, "levelsPassed");
        var fldDepartureStation = GetField<string>(comp, "departureStation");
        var fldDestinationStation = GetField<string>(comp, "destinationStation");
        var fldDepartureOptions = GetArrayField<string>(comp, "departureOptions");
        var fldDestinationOptions = GetArrayField<string>(comp, "destinationOptions");

        var mustReevaluate = false;
        module.Module.OnStrike += delegate { mustReevaluate = true; return false; };

        var departures = new List<string>();
        var destinations = new List<string>();
        var extraOptions = new HashSet<string>();
        var lastStage = -1;
        while (module.Unsolved)
        {
            var stage = fldStage.Get();
            if (stage != lastStage || mustReevaluate)
            {
                if (mustReevaluate)
                {
                    // The player got a strike and the module reset
                    departures.Clear();
                    destinations.Clear();
                    extraOptions.Clear();
                    mustReevaluate = false;
                }
                departures.Add(fldDepartureStation.Get());
                destinations.Add(fldDestinationStation.Get());

                foreach (var option in fldDepartureOptions.Get())
                    extraOptions.Add(option);
                foreach (var option in fldDestinationOptions.Get())
                    extraOptions.Add(option);
                lastStage = stage;
            }
            yield return null;
        }
        var primary = departures.Union(destinations).ToArray();
        if (primary.Length < 4)
            primary = primary.Union(extraOptions).ToArray();

        addQuestions(module,
            departures.Select((dep, ix) => makeQuestion(Question.LondonUndergroundStations, module, formatArgs: new[] { Ordinal(ix + 1), "depart from" }, correctAnswers: new[] { dep }, preferredWrongAnswers: primary)).Concat(
            destinations.Select((dest, ix) => makeQuestion(Question.LondonUndergroundStations, module, formatArgs: new[] { Ordinal(ix + 1), "arrive to" }, correctAnswers: new[] { dest }, preferredWrongAnswers: primary))));
    }
}