using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFlashingLights
{
    [SouvenirQuestion("How many times did the {1} LED flash {2} on {0}?", ThreeColumns6Answers, TranslateArguments = [true, true], Arguments = ["top", "cyan", "top", "green", "top", "red", "top", "purple", "top", "orange", "bottom", "cyan", "bottom", "green", "bottom", "red", "bottom", "purple", "bottom", "orange"], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 12)]
    LEDFrequency
}

public partial class SouvenirModule
{
    [SouvenirHandler("flashingLights", "Flashing Lights", typeof(SFlashingLights), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessFlashingLights(ModuleData module)
    {
        var comp = GetComponent(module, "doubleNegativesScript");
        yield return WaitForSolve;

        var topColors = GetListField<int>(comp, "selectedColours").Get(expectedLength: 12);
        var bottomColors = GetListField<int>(comp, "selectedColours2").Get(expectedLength: 12);
        var colorNames = new[] { "cyan", "green", "red", "purple", "orange" };
        var topTotals = Enumerable.Range(1, 5).Select(num => topColors.Count(x => x == num)).ToArray();
        var bottomTotals = Enumerable.Range(1, 5).Select(num => bottomColors.Count(x => x == num)).ToArray();
        for (var i = 0; i < 5; i++)
        {
            yield return question(SFlashingLights.LEDFrequency, args: ["top", colorNames[i]]).Answers(topTotals[i].ToString(), preferredWrong: [bottomTotals[i].ToString()]);
            yield return question(SFlashingLights.LEDFrequency, args: ["bottom", colorNames[i]]).Answers(bottomTotals[i].ToString(), preferredWrong: [topTotals[i].ToString()]);
        }
    }
}