using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SObjectShows
{
    [SouvenirQuestion("Which of these was a contestant on {0}?", TwoColumns4Answers, ExampleAnswers = ["Battleship", "Big Circle", "Jack O’ Lantern", "Lego", "Moon", "Radio", "Combination Lock", "Cookie Jar", "Fidget Spinner"])]
    Contestants
}

public partial class SouvenirModule
{
    [SouvenirHandler("objectShows", "Object Shows", typeof(SObjectShows), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessObjectShows(ModuleData module)
    {
        var comp = GetComponent(module, "objectShows");
        yield return WaitForSolve;

        var contestantsPresent = GetField<IList>(comp, "contestantsPresent").Get(lst => lst.Count != 6 ? "expected length 6" : null);
        var fldId = GetField<int>(contestantsPresent[0], "id", isPublic: true);
        var allContestantNames = GetStaticField<string[]>(comp.GetType(), "characterNames").Get(v => v.Length != 30 ? "expected length 30" : null);
        var contestantNames = Enumerable.Range(0, contestantsPresent.Count).Select(ix => allContestantNames[fldId.GetFrom(contestantsPresent[ix], v => v is < 0 or >= 30 ? "expected range 0–29" : null)]).ToArray();
        yield return question(SObjectShows.Contestants).Answers(contestantNames, preferredWrong: allContestantNames);
    }
}