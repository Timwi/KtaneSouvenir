using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SPlanets
{
    [SouvenirQuestion("What was the color of the {1} strip (from the top) in {0}?", ThreeColumns6Answers, "Aqua", "Blue", "Green", "Lime", "Orange", "Red", "Yellow", "White", "Off", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Strips,

    [SouvenirQuestion("What was the planet shown in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "PlanetsSprites")]
    Planet
}

public partial class SouvenirModule
{
    [SouvenirHandler("planets", "Planets", typeof(SPlanets), "KingSlendy")]
    private IEnumerator<SouvenirInstruction> ProcessPlanets(ModuleData module)
    {
        var comp = GetComponent(module, "planetsModScript");
        var planetShown = GetIntField(comp, "planetShown").Get(0, 9);
        var stripColors = GetArrayField<int>(comp, "stripColours").Get(expectedLength: 5, validator: x => x is < 0 or > 8 ? "expected range 0–8" : null);
        var preferredWrong = DateTime.Now is { Month: 4, Day: 1 } ? PlanetsSprites : PlanetsSprites.Take(PlanetsSprites.Length - 2).ToArray();

        yield return WaitForSolve;

        var stripNames = new[] { "Aqua", "Blue", "Green", "Lime", "Orange", "Red", "Yellow", "White", "Off" };
        for (var count = 0; count < stripColors.Length; count++)
            yield return question(SPlanets.Strips, args: [Ordinal(count + 1)])
                .Answers(stripNames[stripColors[count]]);

        yield return question(SPlanets.Planet).Answers(PlanetsSprites[planetShown], preferredWrong: preferredWrong);
    }
}
