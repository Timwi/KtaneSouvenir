using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SLevenshteinDistance
{
	[Question("What was the word in the {1} display of {0}?", ThreeColumns6Answers, ExampleAnswers = ["ABSENT", "BALANCE", "CAMPAIGN", "DEAD", "EXACT", "FEBRUARY"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    QDisplays,

    [Discriminator("the Levenshtein Distance that had {0} in the {1} display", Arguments = ["ABSENT", QandA.Ordinal, "BALANCE", QandA.Ordinal, "CAMPAIGN", QandA.Ordinal, "DEAD", QandA.Ordinal, "EXACT", QandA.Ordinal, "FEBRUARY", QandA.Ordinal], ArgumentGroupSize = 2)]
    DDisplays
}

public partial class SouvenirModule
{
	[Handler("levenshteinDistance", "Levenshtein Distance", typeof(SLevenshteinDistance), "Infusion")]
	[ManualQuestion("What were the words displayed?")]
	private IEnumerator<SouvenirInstruction> ProcessLevenshteinDistance(ModuleData module)
    {
        var comp = GetComponent(module, "levenshteinDistanceScript");
        var possibleWords = GetArrayField<string>(comp, "possibleWords").Get();
        var fldWordIndices = GetArrayField<int>(comp, "chosenWordsIndeces").Get(expectedLength: 2);

        yield return WaitForSolve;

        for (var i = 0; i < 2; i++)
        {
            yield return new Discriminator(SLevenshteinDistance.DDisplays, $"display{i}", fldWordIndices[i], args: [possibleWords[fldWordIndices[i]], Ordinal(i + 1)]);
            yield return question(SLevenshteinDistance.QDisplays, args: [Ordinal(i + 1)]).AvoidDiscriminators($"display{i}").Answers(possibleWords[fldWordIndices[i]], preferredWrong: possibleWords);
        }
    }
}