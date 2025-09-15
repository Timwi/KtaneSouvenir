using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRecoloredSwitches
{
    [SouvenirQuestion("What was the color of the {1} LED in {0}?", TwoColumns4Answers, "red", "green", "blue", "cyan", "orange", "purple", "white", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    LedColors
}

public partial class SouvenirModule
{
    [SouvenirHandler("R4YRecoloredSwitches", "Recolored Switches", typeof(SRecoloredSwitches), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessRecoloredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "Recolored_Switches");

        yield return WaitForSolve;

        var colorNames = new Dictionary<char, string>
        {
            { 'R', "red" },
            { 'G', "green" },
            { 'B', "blue" },
            { 'C', "cyan" },
            { 'O', "orange" },
            { 'P', "purple" },
            { 'W', "white" }
        };
        var ledColors = GetField<StringBuilder>(comp, "LEDsColorsString").Get(sb => sb.Length != 10 ? "expected length 10" : Enumerable.Range(0, 10).Any(ix => !colorNames.ContainsKey(sb[ix])) ? $"expected {colorNames.Keys.JoinString()}" : null);
        addQuestions(module, Enumerable.Range(0, 10).Select(ix => makeQuestion(SRecoloredSwitches.LedColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { colorNames[ledColors[ix]] })));
    }
}