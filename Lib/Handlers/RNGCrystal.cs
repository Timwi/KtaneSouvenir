using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRNGCrystal
{
    [SouvenirQuestion("Which bit had a tap in {0} (the output after shifting is at bit 0)?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 23)]
    Taps
}

public partial class SouvenirModule
{
    [SouvenirHandler("rngCrystal", "RNG Crystal", typeof(SRNGCrystal), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessRNGCrystal(ModuleData module)
    {
        var comp = GetComponent(module, "RngCrystalScript");
        yield return WaitForSolve;

        var style = GetField<object>(comp, "_style").Get(v => (int) v is < 1 or > 3 ? $"Unexpected solve style {v}" : null);
        if ((int) style != 2)
            yield return legitimatelyNoQuestion(module, $"The module was solved via luck or the autosolver. ({style})");

        var taps = GetField<int>(comp, "_taps").Get();
        var degree = GetStaticMethod<int>(comp.GetType(), "LfsrPolynomialDegree", 1).Invoke(taps);
        if (degree is < 17 or > 23)
            throw new AbandonModuleException($"Bad register size {degree}");

        var allPossible = Enumerable.Range(0, degree).ToArray();
        var answers = allPossible.Where(i => ((1 << i) & taps) != 0).ToArray();

        yield return question(SRNGCrystal.Taps).Answers(answers.Select(i => i.ToString()).ToArray(), all: allPossible.Select(i => i.ToString()).ToArray());
    }
}