using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SNavyButton
{
    [Question("Which Greek letter appeared on {0} (case-sensitive)?", ThreeColumns6Answers, "Α", "Β", "Γ", "Δ", "Ε", "Ζ", "Η", "Θ", "Ι", "Κ", "Λ", "Μ", "Ν", "Ξ", "Ο", "Π", "Ρ", "Σ", "Τ", "Υ", "Φ", "Χ", "Ψ", "Ω", "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω")]
    QGreekLetters,

    [Question("Which number appeared in the sequence on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(6, 60)]
    QNumbers,

    [Discriminator("the Navy Button that had a {0} on it", Arguments = ["Β", "Γ", "Δ", "Ζ", "Θ", "Κ", "Λ", "Μ", "Ν", "Ξ", "Π", "Ρ", "Σ", "Τ", "Φ", "Χ", "Ψ", "β", "γ", "δ", "ζ", "θ", "κ", "λ", "μ", "ν", "ξ", "π", "ρ", "σ", "τ", "φ", "χ", "ψ"], ArgumentGroupSize = 1)]
    DGreekLettersNV,

    [Discriminator("the Navy Button that had an {0} on it", Arguments = ["Α", "Ε", "Η", "Ι", "Ο", "Υ", "Ω", "α", "ε", "η", "ι", "ο", "υ", "ω"], ArgumentGroupSize = 1)]
    DGreekLettersV,

    [Discriminator("the Navy Button that had the number {0} in the sequence", Arguments = ["1", "2", "3"], ArgumentGroupSize = 1)]
    DNumbers
}

public partial class SouvenirModule
{
    [Handler("NavyButtonModule", "Navy Button", typeof(SNavyButton), "Timwi", AddThe = true)]
    [ManualQuestion("Which Greek letters appeared?")]
    [ManualQuestion("What numbers appeared in the sequence?")]
    private IEnumerator<SouvenirInstruction> ProcessNavyButton(ModuleData module)
    {
        var comp = GetComponent(module, "NavyButtonScript");
        var puzzle = GetField<object>(comp, "_puzzle").Get();

        var allGreekLetters = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψω";
        var vowelGreekLetters = "ΑΕΗΙΟΥΩαεηιουω";

        var hasGreekLetters = GetProperty<int[]>(puzzle, "GreekLetterIxs", isPublic: true)
            .Get(validator: arr => arr.Any(v => v < 0 || v >= allGreekLetters.Length) ? $"expected range 0–{allGreekLetters.Length - 1}" : null);

        var seqNumbers = GetProperty<int[]>(puzzle, "Numbers", isPublic: true).Get();

        yield return WaitForSolve;

        // Questions
        yield return question(SNavyButton.QGreekLetters).AvoidDiscriminators(SNavyButton.DGreekLettersV, SNavyButton.DGreekLettersNV)
            .Answers(hasGreekLetters.Select(ix => "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψω"[ix].ToString()).ToArray());
        yield return question(SNavyButton.QNumbers).AvoidDiscriminators(SNavyButton.DNumbers).Answers(seqNumbers.Select(ix => ix.ToString()).ToArray());
        
        // Discriminators
        for (var grLtrIx = 0; grLtrIx < allGreekLetters.Length; grLtrIx++)
            yield return new Discriminator(
                vowelGreekLetters.Contains(allGreekLetters[grLtrIx]) ? SNavyButton.DGreekLettersV : SNavyButton.DGreekLettersNV,
                $"ltr-{allGreekLetters[grLtrIx]}", args: [allGreekLetters[grLtrIx].ToString()]);
        foreach (var n in seqNumbers.Distinct())
            yield return new Discriminator(SNavyButton.DNumbers, $"num-{n}", args: [n.ToString()]);
    }
}
