using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHomophones
{
    [SouvenirQuestion("What was the {1} displayed phrase in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, ExampleAnswers = ["i", "C", "L", "1", "sees", "leemer", "aye-aye", "One"])]
    DisplayedPhrases
}

public partial class SouvenirModule
{
    [SouvenirHandler("homophones", "Homophones", typeof(SHomophones), "VFlyer")]
    private IEnumerator<SouvenirInstruction> ProcessHomophones(ModuleData module)
    {
        var comp = GetComponent(module, "HomophonesScript");
        yield return WaitForSolve;

        var selectedWords = GetArrayField<string>(comp, "selectedWords", isPublic: true).Get(expectedLength: 4);

        // Set up a trick to prevent the answer from being obvious
        var allIWords = GetArrayField<string>(comp, "iWords").Get(expectedLength: 10);
        var allLWords = GetArrayField<string>(comp, "lWords").Get(expectedLength: 10);
        var allCWords = GetArrayField<string>(comp, "cWords").Get(expectedLength: 10);
        var allOneWords = GetArrayField<string>(comp, "oneWords").Get(expectedLength: 10);

        var possibleQuestions = new List<QandA>();

        for (var i = 0; i < selectedWords.Length; i++)
        {
            var thisWord = selectedWords[i];
            if (allCWords.Contains(thisWord))
                possibleQuestions.Add(makeQuestion(Question.HomophonesDisplayedPhrases, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { thisWord }, preferredWrongAnswers: selectedWords.Union(allCWords).ToArray()));
            else if (allLWords.Contains(thisWord))
                possibleQuestions.Add(makeQuestion(Question.HomophonesDisplayedPhrases, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { thisWord }, preferredWrongAnswers: selectedWords.Union(allLWords).ToArray()));
            else if (allIWords.Contains(thisWord))
                possibleQuestions.Add(makeQuestion(Question.HomophonesDisplayedPhrases, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { thisWord }, preferredWrongAnswers: selectedWords.Union(allIWords).ToArray()));
            else if (allOneWords.Contains(thisWord))
                possibleQuestions.Add(makeQuestion(Question.HomophonesDisplayedPhrases, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { thisWord }, preferredWrongAnswers: selectedWords.Union(allOneWords).ToArray()));
            else
                throw new AbandonModuleException($"The given phrase “{thisWord}” is not one of the possible words that can be found in Homophones.");
        }

        addQuestions(module, possibleQuestions);
    }
}