using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STwoBits
{
    [SouvenirQuestion("What was the {1} correct query response from {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 99, "00")]
    QResponse,

    [SouvenirDiscriminator("the Two Bits where the {0} correct query was {1}", Arguments = [QandA.Ordinal, "00"], ArgumentGroupSize = 2)]
    DResponse
}

public partial class SouvenirModule
{
    [SouvenirHandler("TwoBits", "Two Bits", typeof(STwoBits), "Timwi")]
    [SouvenirManualQuestion("What were the correct three query responses?")]
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
        var responses = new[] { firstResponse.ToString("00"), secondResponse.ToString("00"), thirdResponse.ToString("00"), zerothNumCode.ToString("00") };

        for (int qIx = 0; qIx < 3; qIx++)
        {
            yield return new Discriminator(STwoBits.DResponse, $"query-{qIx}", args: [Ordinal(qIx + 1), responses[qIx]]);
            yield return question(STwoBits.QResponse, args: [Ordinal(qIx + 1)])
                .AvoidDiscriminators($"query-{qIx}")
                .Answers(responses[qIx], preferredWrong: responses);
        }
    }
}