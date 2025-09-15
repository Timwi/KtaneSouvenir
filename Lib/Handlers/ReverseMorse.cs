using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SReverseMorse
{
    [SouvenirQuestion("What was the {1} character in the {2} message of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    [AnswerGenerator.Strings("A-Z0-9")]
    Characters
}

public partial class SouvenirModule
{
    [SouvenirHandler("reverseMorse", "Reverse Morse", typeof(SReverseMorse), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessReverseMorse(ModuleData module)
    {
        var comp = GetComponent(module, "reverseMorseScript");
        var message1 = GetListField<string>(comp, "selectedLetters1", isPublic: true).Get(expectedLength: 6);
        var message2 = GetListField<string>(comp, "selectedLetters2", isPublic: true).Get(expectedLength: 6);

        yield return WaitForSolve;
        for (var i = 0; i < 6; i++)
        {
            yield return question(SReverseMorse.Characters, args: [Ordinal(i + 1), "first"]).Answers(message1[i], preferredWrong: message1.ToArray());
            yield return question(SReverseMorse.Characters, args: [Ordinal(i + 1), "second"]).Answers(message2[i], preferredWrong: message2.ToArray());
        }
    }
}