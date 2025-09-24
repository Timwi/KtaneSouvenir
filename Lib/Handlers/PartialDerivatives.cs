using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SPartialDerivatives
{
    [SouvenirQuestion("What was the LED color in the {1} stage of {0}?", ThreeColumns6Answers, "blue", "green", "orange", "purple", "red", "yellow", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    LedColors,

    [SouvenirQuestion("What was the {1} term in {0}?", TwoColumns4Answers, ExampleAnswers = ["−5x⁴z³", "8x⁴z⁴", "4xy³z²", "−3x⁴z", "3x⁵y⁵z³"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Terms
}

public partial class SouvenirModule
{
    [SouvenirHandler("partialDerivatives", "Partial Derivatives", typeof(SPartialDerivatives), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessPartialDerivatives(ModuleData module)
    {
        var comp = GetComponent(module, "PartialDerivativesScript");
        var fldLeds = GetArrayField<int>(comp, "ledIndex");

        var display = GetField<TextMesh>(comp, "display", isPublic: true).Get();
        var terms = display.text.Split('\n').Select(term => Regex.Replace(Regex.Replace(term.Trim(), @"^(f.*?=|\+) ", ""), @"^- ", "−")).ToArray();
        if (terms.Length != 3)
            throw new AbandonModuleException($"The display does not appear to contain three terms: “{_moduleId}”");

        static string writeTerm(int coeff, bool negative, int[] exps)
        {
            if (coeff == 0)
                return "0";

            var term = negative ? "−" : "";
            if (coeff > 1)
                term += coeff;
            for (var j = 0; j < 3; j++)
            {
                if (exps[j] != 0)
                {
                    term += "xyz"[j];
                    if (exps[j] > 1)
                        term += "²³⁴⁵"[exps[j] - 2];
                }
            }
            return term;
        }

        var wrongAnswers = new HashSet<string>();
        while (wrongAnswers.Count < 3)
        {
            var exps = new int[3];
            for (var j = 0; j < 3; j++)
                exps[j] = Rnd.Range(0, 6);
            if (exps.All(e => e == 0))
                exps[Rnd.Range(0, 3)] = Rnd.Range(1, 6);
            var wrongTerm = writeTerm(Rnd.Range(1, 10), Rnd.Range(0, 2) != 0, exps);
            if (!terms.Contains(wrongTerm))
                wrongAnswers.Add(wrongTerm);
        }

        yield return WaitForSolve;

        var leds = fldLeds.Get(expectedLength: 3, validator: l => l is < 0 or > 5 ? "expected range 0–5" : null);
        var colorNames = new[] { "blue", "green", "orange", "purple", "red", "yellow" };
        for (var stage = 0; stage < 3; stage++)
            yield return question(SPartialDerivatives.LedColors, args: [Ordinal(stage + 1)]).Answers(colorNames[leds[stage]]);
        for (var term = 0; term < 3; term++)
            yield return question(SPartialDerivatives.Terms, args: [Ordinal(term + 1)]).Answers(terms[term], preferredWrong: wrongAnswers.ToArray());
    }
}
