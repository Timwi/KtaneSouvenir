using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SLevenshteinDistance
{
    [Question("Which of these words was displayed in {0}?", ThreeColumns6Answers, ExampleAnswers = ["ABSENT", "BALANCE", "CAMPAIGN", "DEAD", "EXACT", "FEBRUARY"])]
    QDisplays
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
            yield return question(SLevenshteinDistance.QDisplays).Answers(possibleWords[fldWordIndices[i]], preferredWrong: possibleWords);
    }
}
