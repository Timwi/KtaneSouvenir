using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SQuintuples
{
    [SouvenirQuestion("What was the {1} digit in the {2} slot in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    Numbers,

    [SouvenirQuestion("What color was the {1} digit in the {2} slot in {0}?", TwoColumns4Answers, "red", "blue", "orange", "green", "pink", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Colors,

    [SouvenirQuestion("How many numbers were {1} in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["red", "blue", "orange", "green", "pink"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 25)]
    ColorCounts
}

public partial class SouvenirModule
{
    [SouvenirHandler("quintuples", "Quintuples", typeof(SQuintuples), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessQuintuples(ModuleData module)
    {
        var comp = GetComponent(module, "quintuplesScript");
        yield return WaitForSolve;

        var numbers = GetArrayField<int>(comp, "cyclingNumbers", isPublic: true).Get(expectedLength: 25, validator: n => n is < 1 or > 10 ? "expected range 1–10" : null);
        var colors = GetArrayField<string>(comp, "chosenColorsName", isPublic: true).Get(expectedLength: 25);
        var colorCounts = GetArrayField<int>(comp, "numberOfEachColour", isPublic: true).Get(expectedLength: 5, validator: cc => cc is < 0 or > 25 ? "expected range 0–25" : null);
        var colorNames = GetArrayField<string>(comp, "potentialColorsName", isPublic: true).Get(expectedLength: 5);

        addQuestions(module,
            numbers.Select((n, ix) => makeQuestion(Question.QuintuplesNumbers, module, formatArgs: new[] { Ordinal(ix % 5 + 1), Ordinal(ix / 5 + 1) }, correctAnswers: new[] { (n % 10).ToString() })).Concat(
            colors.Select((color, ix) => makeQuestion(Question.QuintuplesColors, module, formatArgs: new[] { Ordinal(ix % 5 + 1), Ordinal(ix / 5 + 1) }, correctAnswers: new[] { color }))).Concat(
            colorCounts.Select((cc, ix) => makeQuestion(Question.QuintuplesColorCounts, module, formatArgs: new[] { colorNames[ix] }, correctAnswers: new[] { cc.ToString() }))));
    }
}