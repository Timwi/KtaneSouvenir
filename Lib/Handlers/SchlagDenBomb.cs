using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSchlagDenBomb
{
    [SouvenirQuestion("What was the contestant’s name in {0}?", TwoColumns4Answers, "Ron", "Don", "Julia", "Cory", "Greg", "Paula", "Val", "Lisa", "Ozy", "Ozzy", "Elsa", "Cori", "Harry", "Gale", "Daniel", "Albert", "Spike", "Tommy", "Greta", "Tina", "Rob", "Edgar", "Julia", "Peter", "Millie", "Isolde", "Eris")]
    ContestantName,

    [SouvenirQuestion("What was the contestant’s score in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 75)]
    ContestantScore,

    [SouvenirQuestion("What was the bomb’s score in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 75)]
    BombScore
}

public partial class SouvenirModule
{
    [SouvenirHandler("qSchlagDenBomb", "Schlag den Bomb", typeof(SSchlagDenBomb), "JerryEris")]
    private IEnumerator<SouvenirInstruction> ProcessSchlagDenBomb(ModuleData module)
    {
        var comp = GetComponent(module, "qSchlagDenBomb");
        yield return WaitForSolve;

        var contestantName = GetField<string>(comp, "contestantName").Get();
        var contestantScore = GetIntField(comp, "scoreC").Get(min: 0, max: 75);
        var bombScore = GetIntField(comp, "scoreB").Get(min: 0, max: 75);

        yield return question(SSchlagDenBomb.ContestantName).Answers(contestantName);
        yield return question(SSchlagDenBomb.ContestantScore).Answers(contestantScore.ToString(), preferredWrong: Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(0, 75).ToString()).Distinct().Take(6).ToArray());
        yield return question(SSchlagDenBomb.BombScore).Answers(bombScore.ToString(), preferredWrong: Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(0, 75).ToString()).Distinct().Take(6).ToArray());
    }
}