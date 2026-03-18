using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SMaritimeSemaphore
{
    [SouvenirQuestion("In which position was the dummy in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Ordinal(6)]
    Dummy,

    [SouvenirQuestion("Which letter was shown {2} in the {1} position in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, "as a maritime flag", QandA.Ordinal, "in semaphore"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    [AnswerGenerator.Strings('A', 'Z')]
    Letter
}

public partial class SouvenirModule
{
    [SouvenirHandler("MaritimeSemaphoreModule", "Maritime Semaphore", typeof(SMaritimeSemaphore), "Timwi")]
    [SouvenirManualQuestion("Where was the dummy?")]
    [SouvenirManualQuestion("Which letters were signaled?")]
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

        yield return question(SMaritimeSemaphore.Dummy).Answers(Ordinal(dummyPos + 1));

        var fldLeft = GetField<int>(flags[0], "LeftMaritime", isPublic: true);
        var fldRight = GetField<int>(flags[0], "RightMaritime", isPublic: true);
        var fldSemaphore = GetField<int>(flags[0], "Semaphore", isPublic: true);

        var solutionPos = GetIntField(comp, "_solution").Get(0, 5); // The solution page is still visible after solve, so avoid asking about it
        var letters = Enumerable.Range(0, 6).Except([dummyPos, solutionPos]).Select(i => (
            Ordinal: Ordinal(i + 1),
            LeftFlag: ((char) ('A' + fldLeft.GetFrom(flags[i], i => i is < 0 or > 25 ? $"Left letter {i} out of range 0-25" : null))).ToString(), // The five non-dummy flags must not have numbers
            RightFlag: ((char) ('A' + fldRight.GetFrom(flags[i], i => i is < 0 or > 25 ? $"Right letter {i} out of range 0-25" : null))).ToString(),
            Semaphore: ((char) ('A' + fldSemaphore.GetFrom(flags[i], i => i is < 0 or > 25 ? $"Semaphore letter {i} out of range 0-25" : null))).ToString()
        )).ToArray();
        var preferred = letters.SelectMany(l => new[] { l.LeftFlag, l.RightFlag, l.Semaphore }).Distinct().ToArray();

        for (var i = 0; i < 4; i++)
        {
            yield return question(SMaritimeSemaphore.Letter, args: [letters[i].Ordinal, "as a maritime flag"]).Answers([letters[i].LeftFlag, letters[i].RightFlag], preferredWrong: preferred);
            yield return question(SMaritimeSemaphore.Letter, args: [letters[i].Ordinal, "in semaphore"]).Answers(letters[i].Semaphore, preferredWrong: preferred);
        }
    }
}
