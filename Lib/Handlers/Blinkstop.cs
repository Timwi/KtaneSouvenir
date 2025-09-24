using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBlinkstop
{
    [SouvenirQuestion("How many times did the LED flash in {0}?", ThreeColumns6Answers, "30", "33", "37", "39", "42", "44", "47", "51", "55", "59")]
    NumberOfFlashes,

    [SouvenirQuestion("Which color did the LED flash the fewest times in {0}?", TwoColumns4Answers, "Purple", "Cyan", "Yellow", "Multicolor", TranslateAnswers = true)]
    FewestFlashedColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("blinkstopModule", "Blinkstop", typeof(SBlinkstop), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessBlinkstop(ModuleData module)
    {
        var comp = GetComponent(module, "BlinkstopScript");

        yield return WaitForSolve;

        var flashes = GetArrayField<char>(comp, "prevledcols").Get(arr =>
            !SBlinkstop.NumberOfFlashes.GetAnswers().Contains(arr.Length.ToString()) ? "unexpected flash count" :
            arr.Any(f => !"PMYC".Contains(f)) ? "expected only P, M, Y, or C flash values" : null);
        var leastFlashedColour = new[] { "Multicolor", "Purple", "Yellow", "Cyan" }.OrderBy(col => flashes.Count(f => f == col[0])).First();

        yield return question(SBlinkstop.NumberOfFlashes).Answers(flashes.Length.ToString());
        yield return question(SBlinkstop.FewestFlashedColor).Answers(leastFlashedColour);
    }
}