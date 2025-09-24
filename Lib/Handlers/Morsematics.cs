using System.Collections.Generic;
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

        for (var i = 0; i < 3; i++)
            yield return question(SMorsematics.ReceivedLetters, args: [Ordinal(i + 1)]).Answers(chars[i], preferredWrong: chars);
    }
}