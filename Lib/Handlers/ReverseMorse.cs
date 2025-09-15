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

        var qs = new List<QandA>();
        for (var i = 0; i < 6; i++)
        {
            qs.Add(makeQuestion(Question.ReverseMorseCharacters, module, formatArgs: new[] { Ordinal(i + 1), "first" }, correctAnswers: new[] { message1[i] }, preferredWrongAnswers: message1.ToArray()));
            qs.Add(makeQuestion(Question.ReverseMorseCharacters, module, formatArgs: new[] { Ordinal(i + 1), "second" }, correctAnswers: new[] { message2[i] }, preferredWrongAnswers: message2.ToArray()));
        }
        addQuestions(module, qs);
    }
}