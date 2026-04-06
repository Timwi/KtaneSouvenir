using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SObjectShows
{
    [Question("Which of these was a contestant, but not the winner, on {0}?", OneColumn4Answers, ExampleAnswers = ["Battleship", "Big Circle", "Jack O’ Lantern", "Lego", "Moon", "Radio", "Combination Lock", "Cookie Jar", "Fidget Spinner"])]
    Contestants
}

public partial class SouvenirModule
{
    [Handler("objectShows", "Object Shows", typeof(SObjectShows), "Timwi")]
    [ManualQuestion("What contestants were shown?")]
    private IEnumerator<SouvenirInstruction> ProcessObjectShows(ModuleData module)
    {
        var comp = GetComponent(module, "objectShows");
        yield return WaitForSolve;

        var contestants = GetField<IList>(comp, "solution").Get(lst => lst.Count != 5 ? "expected length 5" : null);
        var fldId = GetField<int>(contestants[0], "id", isPublic: true);
        var allContestantNames = GetStaticField<string[]>(comp.GetType(), "characterNames").Get(v => v.Length != 30 ? "expected length 30" : null);
        var contestantNames = Enumerable.Range(0, contestants.Count)
            .Select(ix => allContestantNames[fldId.GetFrom(contestants[ix], v => v is < 0 or >= 30 ? "expected range 0–29" : null)])
            .ToArray();
        yield return question(SObjectShows.Contestants).Answers(contestantNames, preferredWrong: allContestantNames);
    }
}
