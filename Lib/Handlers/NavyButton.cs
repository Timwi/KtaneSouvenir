using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SNavyButton
{
    [SouvenirQuestion("Which Greek letter appeared on {0} (case-sensitive)?", ThreeColumns6Answers, "Α", "Β", "Γ", "Δ", "Ε", "Ζ", "Η", "Θ", "Ι", "Κ", "Λ", "Μ", "Ν", "Ξ", "Ο", "Π", "Ρ", "Σ", "Τ", "Υ", "Φ", "Χ", "Ψ", "Ω", "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω")]
    QGreekLetters,

    [SouvenirQuestion("What was the {1} of the given in {0}?", TwoColumns4Answers, "0", "1", "2", "3", Arguments = ["(0-indexed) column", "(0-indexed) row", "value"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    QGiven,

    [SouvenirDiscriminator("the Navy Button that had a {0} on it", Arguments = ["Β", "Γ", "Δ", "Ζ", "Θ", "Κ", "Λ", "Μ", "Ν", "Ξ", "Π", "Ρ", "Σ", "Τ", "Φ", "Χ", "Ψ", "β", "γ", "δ", "ζ", "θ", "κ", "λ", "μ", "ν", "ξ", "π", "ρ", "σ", "τ", "φ", "χ", "ψ"], ArgumentGroupSize = 1)]
    DGreekLettersNV,

    [SouvenirDiscriminator("the Navy Button that had an {0} on it", Arguments = ["Α", "Ε", "Η", "Ι", "Ο", "Υ", "Ω", "α", "ε", "η", "ι", "ο", "υ", "ω"], ArgumentGroupSize = 1)]
    DGreekLettersV,

    [SouvenirDiscriminator("the Navy Button where the {0} of the given was {1}", Arguments = ["(0-indexed) column", "1", "(0-indexed) row", "1", "value", "1"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    DGiven
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

        yield return WaitForSolve;

        yield return new Discriminator(SNavyButton.DGiven, "given-v", givenValue, ["value", givenValue.ToString()]);
        yield return new Discriminator(SNavyButton.DGiven, "given-c", givenValue, ["(0-indexed) column", (givenIndex % 4).ToString()]);
        yield return new Discriminator(SNavyButton.DGiven, "given-r", givenValue, ["(0-indexed) row", (givenIndex / 4).ToString()]);
        for (var grLtrIx = 0; grLtrIx < allGreekLetters.Length; grLtrIx++)
            yield return new Discriminator(
                vowelGreekLetters.Contains(allGreekLetters[grLtrIx]) ? SNavyButton.DGreekLettersV : SNavyButton.DGreekLettersNV,
                $"ltr-{allGreekLetters[grLtrIx]}", args: [allGreekLetters[grLtrIx].ToString()]);

        yield return question(SNavyButton.QGreekLetters).AvoidDiscriminators(SNavyButton.DGreekLettersV, SNavyButton.DGreekLettersNV)
            .Answers(hasGreekLetters.Select(ix => "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψω"[ix].ToString()).ToArray());
        yield return question(SNavyButton.QGiven, args: ["(0-indexed) column"]).AvoidDiscriminators("given-c").Answers((givenIndex % 4).ToString());
        yield return question(SNavyButton.QGiven, args: ["(0-indexed) row"]).AvoidDiscriminators("given-r").Answers((givenIndex / 4).ToString());
        yield return question(SNavyButton.QGiven, args: ["value"]).AvoidDiscriminators("given-v").Answers(givenValue.ToString());
    }
}
