using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMafia
{
    [SouvenirQuestion("Who was a player, but not the Godfather, in {0}?", ThreeColumns6Answers, "Rob", "Tim", "Mary", "Briane", "Hunter", "Macy", "John", "Will", "Lacy", "Claire", "Kenny", "Rick", "Walter", "Bonnie", "Luke", "Bill", "Sarah", "Larry", "Kate", "Stacy", "Diane", "Mac", "Jim", "Clyde", "Tommy", "Lenny", "Molly", "Benny", "Phil", "Bob", "Gary", "Ted", "Kim", "Nate", "Cher", "Ron", "Thomas", "Sam", "Duke", "Jack", "Ed", "Ronny", "Terry", "Claira", "Nick", "Cob", "Ash", "Don", "Jerry", "Simon")]
    Players
}

public partial class SouvenirModule
{
    [SouvenirHandler("MafiaModule", "Mafia", typeof(SMafia), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMafia(ModuleData module)
    {
        var comp = GetComponent(module, "MafiaModule");
        yield return WaitForSolve;

        var godfather = GetField<object>(comp, "_godfather").Get();
        var suspects = GetField<Array>(comp, "_suspects").Get(ar => ar.Length != 8 ? "expected length 8" : null);
        addQuestion(module, Question.MafiaPlayers, correctAnswers: suspects.Cast<object>().Select(obj => obj.ToString()).Except(new[] { godfather.ToString() }).ToArray());
    }
}