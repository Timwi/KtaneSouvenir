using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STwoBits
{
    [SouvenirQuestion("What was the {1} correct query response from {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 99, "00")]
    Response
}

public partial class SouvenirModule
{
    [SouvenirHandler("TwoBits", "Two Bits", typeof(STwoBits), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessTwoBits(ModuleData module)
    {
        var comp = GetComponent(module, "TwoBitsModule");

        yield return WaitForSolve;

        var queryLookups = GetField<Dictionary<int, string>>(comp, "queryLookups").Get();
        var queryResponses = GetField<Dictionary<string, int>>(comp, "queryResponses").Get();

        var zerothNumCode = GetIntField(comp, "firstQueryCode").Get();
        var zerothLetterCode = queryLookups[zerothNumCode];
        var firstResponse = queryResponses[zerothLetterCode];
        var firstLookup = queryLookups[firstResponse];
        var secondResponse = queryResponses[firstLookup];
        var secondLookup = queryLookups[secondResponse];
        var thirdResponse = queryResponses[secondLookup];
        var preferredWrongAnswers = new[] { zerothNumCode.ToString("00"), firstResponse.ToString("00"), secondResponse.ToString("00"), thirdResponse.ToString("00") };

        addQuestions(module,
            makeQuestion(Question.TwoBitsResponse, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { firstResponse.ToString("00") }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.TwoBitsResponse, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { secondResponse.ToString("00") }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.TwoBitsResponse, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { thirdResponse.ToString("00") }, preferredWrongAnswers: preferredWrongAnswers));
    }
}