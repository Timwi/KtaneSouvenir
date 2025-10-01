using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SPatternRecognition
{
    [SouvenirQuestion("What was the repeating pattern in {0}?", ThreeColumns6Answers, ExampleAnswers = new[] { "● ● ● ● ▬", "● ● ● ▬ ▬", "● ● ▬ ▬ ▬", "● ● ▬ ● ▬", "● ▬ ● ▬ ▬", "● ▬ ▬ ▬ ▬" })]
    Pattern,
    [SouvenirQuestion("What frequency was available to transmit in {0}?", ThreeColumns6Answers, ExampleAnswers = new[] { "500", "502", "602", "525", "537", "585", "527", "597" })]
    Freq
}

public partial class SouvenirModule
{
    [SouvenirHandler("patternRecognition", "Pattern Recognition", typeof(SPatternRecognition), "KiloBites")]
    private IEnumerator<SouvenirInstruction> ProcessPatternRecognition(ModuleData module)
    {
        var comp = GetComponent(module, "PatternRecognitionScript");
        yield return WaitForSolve;

        var patterns = GetArrayField<object>(comp, "patterns").Get(expectedLength: 6);
        var patternStrs = patterns.Select(x => GetField<string>(x, "Pattern", isPublic: true).Get().Select(y => (y == '-' ? "▬" : "●") + " ").JoinString("")).ToArray();

        var patternIx = GetIntField(comp, "selectedPattern").Get(min: 0, max: 5);

        var availableFreq = GetListField<int>(comp, "frequencies").Get(expectedLength: 8);
        var otherFreqs = patterns.SelectMany(x => GetArrayField<int>(x, "Frequencies", isPublic: true).Get(expectedLength: 3)).Where(x => !availableFreq.Contains(x)).ToArray();

        yield return question(SPatternRecognition.Pattern).Answers(patternStrs[patternIx], preferredWrong: patternStrs);

        foreach (var freq in availableFreq)
            yield return question(SPatternRecognition.Freq).Answers(freq.ToString(), preferredWrong: otherFreqs.Select(x => x.ToString()).ToArray());
    }
}
