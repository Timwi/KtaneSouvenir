using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSafetySquare
{
    [SouvenirQuestion("What was the digit displayed on the {1} diamond in {0}?", TwoColumns4Answers, Arguments = ["red", "yellow", "blue"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(0, 4)]
    Digits,

    [SouvenirQuestion("What was the special rule displayed on the white diamond in {0}?", OneColumn4Answers, "No special rule", "Reacts with water", "Simple asphyxiant", "Oxidizer", TranslateAnswers = true)]
    SpecialRule
}

public partial class SouvenirModule
{
    [SouvenirHandler("safetySquare", "Safety Square", typeof(SSafetySquare), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessSafetySquare(ModuleData module)
    {
        var comp = GetComponent(module, "SafetySquareScript");

        var specialRules = new Dictionary<string, string>
        {
            [" "] = "No special rule",
            ["W"] = "Reacts with water",
            ["SA"] = "Simple asphyxiant",
            ["OX"] = "Oxidizer"
        };
        var colors = new[] { "red", "yellow", "blue" };
        var digits = colors.Select(col => GetField<TextMesh>(comp, $"{col}Text", isPublic: true).Get(mesh => mesh.text.Length != 1 || !"01234".Contains(mesh.text) ? $"text value was \"{mesh.text}\", but expected a single digit from 0-4." : null).text).ToArray();
        var symbol = GetField<TextMesh>(comp, "whiteText", isPublic: true).Get(mesh => !specialRules.ContainsKey(mesh.text) ? $"text value was \"{mesh.text}\", but expected one of {specialRules.Keys.JoinString(", ", "\"", "\"", " or ")}" : null).text;

        yield return WaitForSolve;

        for (var ix = 0; ix < colors.Length; ix++)
            yield return question(SSafetySquare.Digits, args: [colors[ix]]).Answers(digits[ix]);
        yield return question(SSafetySquare.SpecialRule).Answers(specialRules[symbol]);
    }
}
