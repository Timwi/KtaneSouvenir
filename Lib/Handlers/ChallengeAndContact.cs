using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SChallengeAndContact
{
    [SouvenirQuestion("What was the {1} displayed letter in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('a', 'z')]
    Letters
}

public partial class SouvenirModule
{
    [SouvenirHandler("challengeAndContact", "Challenge & Contact", typeof(SChallengeAndContact), "Espik")]
    private IEnumerator<SouvenirInstruction> ProcessChallengeAndContact(ModuleData module)
    {
        var comp = GetComponent(module, "moduleScript");

        yield return WaitForSolve;

        var allLetters = GetArrayField<string>(comp, "displayedLetters").Get(expectedLength: 3);

        yield return question(SChallengeAndContact.Letters, args: [Ordinal(1)]).Answers(allLetters[0], preferredWrong: allLetters);
        yield return question(SChallengeAndContact.Letters, args: [Ordinal(2)]).Answers(allLetters[1], preferredWrong: allLetters);
        yield return question(SChallengeAndContact.Letters, args: [Ordinal(3)]).Answers(allLetters[2], preferredWrong: allLetters);
    }
}
