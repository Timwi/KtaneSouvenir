using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SChallengeAndContact
{
    [SouvenirQuestion("What was the {1} submitted answer in {0}?", TwoColumns4Answers, ExampleAnswers = ["Accumulation", "Coffeebucks", "Perplexing", "Zoo", "Sunstone", "Bob"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Answers
}

public partial class SouvenirModule
{
    [SouvenirHandler("challengeAndContact", "Challenge & Contact", typeof(SChallengeAndContact), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessChallengeAndContact(ModuleData module)
    {
        var comp = GetComponent(module, "moduleScript");
        var fldAnswers = GetArrayField<string>(comp, "answers");
        var fldFirstSet = GetArrayField<string>(comp, "possibleFirstAnswers");
        var fldSecondSet = GetArrayField<string>(comp, "possibleSecondAnswers");
        var fldThirdSet = GetArrayField<string>(comp, "possibleFinalAnswers");

        yield return WaitForSolve;

        var answers = fldAnswers.Get(expectedLength: 3);
        var firstSet = fldFirstSet.Get();
        var secondSet = fldSecondSet.Get();
        var thirdSet = fldThirdSet.Get();

        var allAnswers = new string[firstSet.Length + secondSet.Length + thirdSet.Length];
        firstSet.CopyTo(allAnswers, 0);
        secondSet.CopyTo(allAnswers, firstSet.Length);
        thirdSet.CopyTo(allAnswers, firstSet.Length + secondSet.Length);

        for (var i = 0; i < answers.Length; i++)
            answers[i] = char.ToUpperInvariant(answers[i][0]) + answers[i].Substring(1);
        for (var i = 0; i < allAnswers.Length; i++)
            allAnswers[i] = char.ToUpperInvariant(allAnswers[i][0]) + allAnswers[i].Substring(1);

        addQuestions(module,
            makeQuestion(Question.ChallengeAndContactAnswers, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { answers[0] }, preferredWrongAnswers: allAnswers.Where(x => x[0] == answers[0][0]).ToArray()),
            makeQuestion(Question.ChallengeAndContactAnswers, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { answers[1] }, preferredWrongAnswers: allAnswers.Where(x => x[0] == answers[1][0]).ToArray()),
            makeQuestion(Question.ChallengeAndContactAnswers, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { answers[2] }, preferredWrongAnswers: allAnswers.Where(x => x[0] == answers[2][0]).ToArray()));
    }
}