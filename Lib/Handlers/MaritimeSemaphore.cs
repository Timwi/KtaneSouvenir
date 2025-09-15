using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMaritimeSemaphore
{
    [SouvenirQuestion("In which position was the dummy in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Ordinal(6)]
    Dummy,

    [SouvenirQuestion("Which letter was shown by the {2} in the {1} position in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, "left flag", QandA.Ordinal, "right flag", QandA.Ordinal, "semaphore"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    [AnswerGenerator.Strings('A', 'Z')]
    Letter
}

public partial class SouvenirModule
{
    [SouvenirHandler("MaritimeSemaphoreModule", "Maritime Semaphore", typeof(SMaritimeSemaphore), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessMaritimeSemaphore(ModuleData module)
    {
        yield return WaitForSolve;
        var comp = GetComponent(module, "MaritimeSemaphoreModule");
        var flags = GetField<IList>(comp, "_flagInfos").Get(l => l.Count != 6 ? "Expected length 6" : null);

        var isDummy = GetProperty<bool>(flags[0], "IsDummy", isPublic: true);
        var dummyPos = -1;
        for (var i = 0; i < 6; i++)
        {
            if (isDummy.GetFrom(flags[i]))
            {
                if (dummyPos != -1)
                    throw new AbandonModuleException($"Multiple dummy flags {dummyPos}, {i}");
                dummyPos = i;
            }
        }
        if (dummyPos == -1)
            throw new AbandonModuleException("No dummy flag");

        var questions = new List<QandA>(13) { makeQuestion(Question.MaritimeSemaphoreDummy, module, correctAnswers: new[] { Ordinal(dummyPos + 1) }) };

        var left = GetField<int>(flags[0], "LeftMaritime", isPublic: true);
        var right = GetField<int>(flags[0], "RightMaritime", isPublic: true);
        var semaphore = GetField<int>(flags[0], "Semaphore", isPublic: true);

        var solutionPos = GetIntField(comp, "_solution").Get(0, 5); // The solution page is still visible after solve, so avoid asking about it
        var letters = Enumerable.Range(0, 6).Except(new[] { dummyPos, solutionPos }).Select(i => (
            Ordinal: Ordinal(i + 1),
            LeftFlag: left.GetFrom(flags[i], i => i is < 0 or > 25 ? $"Left letter {i} out of range 0-25" : null), // The five non-dummy flags must not have numbers
            RightFlag: right.GetFrom(flags[i], i => i is < 0 or > 25 ? $"Right letter {i} out of range 0-25" : null),
            Semaphore: semaphore.GetFrom(flags[i], i => i is < 0 or > 25 ? $"Semaphore letter {i} out of range 0-25" : null)
        )).ToArray();

        for (var i = 0; i < 4; i++)
        {
            questions.Add(makeQuestion(Question.MaritimeSemaphoreLetter, module,
                formatArgs: new[] { letters[i].Ordinal, "left flag" }, correctAnswers: new[] { ((char) ('A' + letters[i].LeftFlag)).ToString() }));
            questions.Add(makeQuestion(Question.MaritimeSemaphoreLetter, module,
                formatArgs: new[] { letters[i].Ordinal, "right flag" }, correctAnswers: new[] { ((char) ('A' + letters[i].RightFlag)).ToString() }));
            questions.Add(makeQuestion(Question.MaritimeSemaphoreLetter, module,
                formatArgs: new[] { letters[i].Ordinal, "semaphore" }, correctAnswers: new[] { ((char) ('A' + letters[i].Semaphore)).ToString() }));
        }

        addQuestions(module, questions);
    }
}