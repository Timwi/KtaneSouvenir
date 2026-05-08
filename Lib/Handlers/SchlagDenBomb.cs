using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;
using Rnd = UnityEngine.Random;

public enum SSchlagDenBomb
{
    [Question("What was the contestant’s name in {0}?", TwoColumns4Answers, "Albert", "Cori", "Cory", "Daniel", "Don", "Edgar", "Elsa", "Eris", "Gale", "Greg", "Greta", "Harry", "Isolde", "Julia", "Julie", "Lisa", "Millie", "Ozy", "Ozzy", "Paula", "Peter", "Rob", "Ron", "Spike", "Tina", "Tommy", "Val")]
    ContestantName,

    [Question("What was the contestant’s score in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 75)]
    ContestantScore,

    [Question("What was the bomb’s score in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 75)]
    BombScore
}

public partial class SouvenirModule
{
    [Handler("qSchlagDenBomb", "Schlag den Bomb", typeof(SSchlagDenBomb), "JerryEris")]
    [ManualQuestion("What were the contestant’s name and both scores?")]
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
