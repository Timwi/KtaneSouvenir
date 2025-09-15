using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMorsematics
{
    [SouvenirQuestion("What was the {1} received letter in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    ReceivedLetters
}

public partial class SouvenirModule
{
    [SouvenirHandler("MorseV2", "Morsematics", typeof(SMorsematics), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMorsematics(ModuleData module)
    {
        var comp = GetComponent(module, "AdvancedMorse");
        var chars = GetArrayField<string>(comp, "DisplayCharsRaw").Get(expectedLength: 3);

        yield return WaitForSolve;

        addQuestions(module, Enumerable.Range(0, 3).Select(i => makeQuestion(Question.MorsematicsReceivedLetters, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { chars[i] }, preferredWrongAnswers: chars)));
    }
}