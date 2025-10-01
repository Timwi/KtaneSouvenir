using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SPatternRecognition
{
    [SouvenirQuestion("What was the repeating pattern in {0}?", ThreeColumns6Answers, ExampleAnswers = ["● ● ● ● ▬", "● ● ● ▬ ▬", "● ● ▬ ▬ ▬", "● ● ▬ ● ▬", "● ▬ ● ▬ ▬", "● ▬ ▬ ▬ ▬"])]
    Pattern
}

public partial class SouvenirModule
{
    [SouvenirHandler("patternRecognition", "Pattern Recognition", typeof(SPatternRecognition), "KiloBites")]
    private IEnumerator<SouvenirInstruction> ProcessPatternRecognition(ModuleData module)
    {
        var comp = GetComponent(module, "PatternRecognitionScript");
        yield return WaitForSolve;

        var patterns = GetArrayField<object>(comp, "patterns").Get(expectedLength: 6);
        var fldPattern = GetField<string>(patterns[0], "Pattern", isPublic: true);
        var patternStrs = patterns.Select(x => fldPattern.Get().Select(y => (y == '-' ? "▬" : "●")).JoinString(" ")).ToArray();

        yield return question(SPatternRecognition.Pattern).Answers(patternStrs[GetIntField(comp, "selectedPattern").Get(min: 0, max: 5)], preferredWrong: patternStrs);
    }
}
