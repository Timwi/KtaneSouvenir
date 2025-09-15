using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBobBarks
{
    [SouvenirQuestion("What was the {1} indicator label in {0}?", ThreeColumns6Answers, "BOB", "CAR", "CLR", "IND", "FRK", "FRQ", "MSA", "NSA", "SIG", "SND", "TRN", "BUB", "DOG", "ETC", "KEY", TranslateArguments = [true], Arguments = ["top left", "top right", "bottom left", "bottom right"], ArgumentGroupSize = 1)]
    Indicators,

    [SouvenirQuestion("Which button flashed {1} in sequence in {0}?", TwoColumns4Answers, "top left", "top right", "bottom left", "bottom right", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Positions
}

public partial class SouvenirModule
{
    [SouvenirHandler("ksmBobBarks", "Bob Barks", typeof(SBobBarks), "Kaito Sinclaire")]
    private IEnumerator<SouvenirInstruction> ProcessBobBarks(ModuleData module)
    {
        var comp = GetComponent(module, "BobBarks");
        var fldIndicators = GetArrayField<int>(comp, "assigned");
        var fldFlashes = GetArrayField<int>(comp, "stages");

        yield return WaitForSolve;

        string[] validDirections = { "top left", "top right", "bottom left", "bottom right" };
        string[] validLabels = { "BOB", "CAR", "CLR", "IND", "FRK", "FRQ", "MSA", "NSA", "SIG", "SND", "TRN", "BUB", "DOG", "ETC", "KEY" };

        var indicators = fldIndicators.Get(expectedLength: 4, validator: idn => idn < 0 || idn >= validLabels.Length ? $"expected 0–{validLabels.Length - 1}" : null);
        var flashes = fldFlashes.Get(expectedLength: 5, validator: fn => fn < 0 || fn >= validDirections.Length ? $"expected 0–{validDirections.Length - 1}" : null);

        // To provide preferred wrong answers, mostly.
        string[] labelsOnModule = { validLabels[indicators[0]], validLabels[indicators[1]], validLabels[indicators[2]], validLabels[indicators[3]] };

        addQuestions(module,
            Enumerable.Range(0, 4).Select(ix => makeQuestion(Question.BobBarksIndicators, module,
                correctAnswers: new[] { labelsOnModule[ix] },
                formatArgs: new[] { validDirections[ix] },
                preferredWrongAnswers: labelsOnModule.Except(new[] { labelsOnModule[ix] }).ToArray()
            )).Concat(
            Enumerable.Range(0, 5).Select(ix => makeQuestion(Question.BobBarksPositions, module,
                correctAnswers: new[] { validDirections[flashes[ix]] },
                formatArgs: new[] { Ordinal(ix + 1) }))
            ));
    }
}