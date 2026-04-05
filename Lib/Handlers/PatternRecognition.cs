using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SPatternRecognition
{
    [Question("What was the repeating pattern in {0}?", ThreeColumns6Answers, ExampleAnswers = ["● ● ● ● ▬", "● ● ● ▬ ▬", "● ● ▬ ▬ ▬", "● ● ▬ ● ▬", "● ▬ ● ▬ ▬", "● ▬ ▬ ▬ ▬"])]
    Pattern
}

public partial class SouvenirModule
{
    [Handler("patternRecognition", "Pattern Recognition", typeof(SPatternRecognition), "KiloBites")]
    [ManualQuestion("What was the repeating pattern?")]
    private IEnumerator<SouvenirInstruction> ProcessPatternRecognition(ModuleData module)
    {
        var comp = GetComponent(module, "PatternRecognitionScript");
        yield return WaitForSolve;

        var patterns = GetArrayField<string>(comp, "logPatterns").Get(expectedLength: 6);
        var selectedPatternIx = GetIntField(comp, "selectedPattern").Get(min: 0, max: 5);

        yield return question(SPatternRecognition.Pattern).Answers(patterns[selectedPatternIx], preferredWrong: patterns);
    }
}
