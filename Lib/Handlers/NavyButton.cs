using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNavyButton
{
    [SouvenirQuestion("Which Greek letter appeared on {0} (case-sensitive)?", ThreeColumns6Answers, "Α", "Β", "Γ", "Δ", "Ε", "Ζ", "Η", "Θ", "Ι", "Κ", "Λ", "Μ", "Ν", "Ξ", "Ο", "Π", "Ρ", "Σ", "Τ", "Υ", "Φ", "Χ", "Ψ", "Ω", "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω", TranslatableStrings = ["the Navy Button that had a {0} on it", "the Navy Button that had an {0} on it", "the Navy Button where the (0-indexed) column of the given was {0}", "the Navy Button where the (0-indexed) row of the given was {0}", "the Navy Button where the value of the given was {0}"])]
    GreekLetters,

    [SouvenirQuestion("What was the {1} of the given in {0}?", TwoColumns4Answers, "0", "1", "2", "3", Arguments = ["(0-indexed) column", "(0-indexed) row", "value"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Given
}

public partial class SouvenirModule
{
    [SouvenirHandler("NavyButtonModule", "Navy Button", typeof(SNavyButton), "Timwi", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessNavyButton(ModuleData module)
    {
        var comp = GetComponent(module, "NavyButtonScript");
        var puzzle = GetField<object>(comp, "_puzzle").Get();

        var allGreekLetters = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψω";
        var vowelGreekLetters = "ΑΕΗΙΟΥΩαεηιουω";

        var hasGreekLetters = GetProperty<int[]>(puzzle, "GreekLetterIxs", isPublic: true)
            .Get(validator: arr => arr.Any(v => v < 0 || v >= allGreekLetters.Length) ? $"expected range 0–{allGreekLetters.Length - 1}" : null);
        var givenIndex = GetProperty<int>(puzzle, "GivenIndex", isPublic: true).Get(validator: v => v is < 0 or >= 16 ? "expected range 0–15" : null);
        var givenValue = GetProperty<int>(puzzle, "GivenValue", isPublic: true).Get(validator: v => v is < 0 or >= 4 ? "expected range 0–3" : null);
        _navyButtonInfos.Add((hasGreekLetters, givenIndex % 4, givenIndex / 4, givenValue));

        yield return WaitForSolve;

        var candidateDiscriminators = new List<(string format, string name)> { (null, null) };
        void addCandidateDiscriminator<T>(Func<(int[] greekLetters, int col, int row, int val), T> getter, T value, string name, string format, object arg)
        {
            if (_navyButtonInfos.Count(tup => getter(tup).Equals(value)) == 1)
                candidateDiscriminators.Add((string.Format(translateString(Question.NavyButtonGreekLetters, format), arg), name));
        }
        addCandidateDiscriminator(tup => tup.col, givenIndex % 4, "col", "the Navy Button where the (0-indexed) column of the given was {0}", givenIndex % 4);
        addCandidateDiscriminator(tup => tup.row, givenIndex / 4, "row", "the Navy Button where the (0-indexed) row of the given was {0}", givenIndex / 4);
        addCandidateDiscriminator(tup => tup.val, givenValue, "val", "the Navy Button where the value of the given was {0}", givenValue);
        for (var grLtrIx = 0; grLtrIx < allGreekLetters.Length; grLtrIx++)
            addCandidateDiscriminator(tup => tup.greekLetters.Contains(grLtrIx), true, "ltr", vowelGreekLetters.Contains(allGreekLetters[grLtrIx]) ? "the Navy Button that had an {0} on it" : "the Navy Button that had a {0} on it", allGreekLetters[grLtrIx]);

        addQuestions(module,
            makeQuestion(Question.NavyButtonGreekLetters, module, formattedModuleName: candidateDiscriminators.Where(tup => tup.name != "ltr").PickRandom().format,
                correctAnswers: hasGreekLetters.Select(ix => "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψω"[ix].ToString()).ToArray()),
            makeQuestion(Question.NavyButtonGiven, module, formattedModuleName: candidateDiscriminators.Where(tup => tup.name != "col").PickRandom().format, formatArgs: new[] { "(0-indexed) column" }, correctAnswers: new[] { (givenIndex % 4).ToString() }),
            makeQuestion(Question.NavyButtonGiven, module, formattedModuleName: candidateDiscriminators.Where(tup => tup.name != "row").PickRandom().format, formatArgs: new[] { "(0-indexed) row" }, correctAnswers: new[] { (givenIndex / 4).ToString() }),
            makeQuestion(Question.NavyButtonGiven, module, formattedModuleName: candidateDiscriminators.Where(tup => tup.name != "val").PickRandom().format, formatArgs: new[] { "value" }, correctAnswers: new[] { givenValue.ToString() }));
    }
}