using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotMorseCode
{
    [Question("What was the {1} correct word you submitted in {0}?", ThreeColumns6Answers, ExampleAnswers = ["shelf", "pounds", "sister", "beef", "yeast", "drive"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Word
}

public partial class SouvenirModule
{
    [Handler("NotMorseCode", "Not Morse Code", typeof(SNotMorseCode), "Andrio Celos")]
    [ManualQuestion("What was the sequence of words you submitted?")]
    private IEnumerator<SouvenirInstruction> ProcessNotMorseCode(ModuleData module)
    {
        var component = GetComponent(module, "NotMorseCode");
        yield return WaitForSolve;

        var words = GetArrayField<string>(component, "words").Get();
        var channels = GetArrayField<int>(component, "correctChannels").Get();
        var columns = GetStaticField<string[][]>(component.GetType(), "defaultColumns").Get();

        for (var stage = 0; stage < 5; stage++)
            yield return question(SNotMorseCode.Word, args: [Ordinal(stage + 1)]).Answers(words[channels[stage]], preferredWrong: words.Concat(Enumerable.Range(0, 50).Select(_ => columns.PickRandom().PickRandom())).Except([words[channels[stage]]]).Distinct().Take(8).ToArray());
    }
}