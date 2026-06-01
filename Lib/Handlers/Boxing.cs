using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBoxing
{
    [Question("What was {1}’s strength rating on {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", Arguments = ["Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander"], ArgumentGroupSize = 1)]
    StrengthByContestant,

    [Question("Which contestant had strength rating {1} on {0}?", TwoColumns4Answers, ExampleAnswers = ["Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander"], Arguments = ["0", "1", "2", "3", "4"], ArgumentGroupSize = 1)]
    ContestantByStrength,

    [Question("Which contestant appeared on {0}?", TwoColumns4Answers, ExampleAnswers = ["Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander"])]
    QNames,

    [Discriminator("the Boxing that had {0} as a contestant?", Arguments = ["Muhammad", "Mike", "Floyd", "Joe"], ArgumentGroupSize = 1)]
    DNames
}

public partial class SouvenirModule
{
    [Handler("boxing", "Boxing", typeof(SBoxing), "Timwi")]
    [ManualQuestion("Which contestants were shown?")]
    [ManualQuestion("Who had which punch strength rating?")]
    private IEnumerator<SouvenirInstruction> ProcessBoxing(ModuleData module)
    {
        var comp = GetComponent(module, "boxing");
        yield return WaitForSolve;

        var possibleNames = GetStaticField<string[]>(comp.GetType(), "possibleNames").Get();
        var contestantStrengths = GetArrayField<int>(comp, "contestantStrengths").Get(expectedLength: 5);
        var contestantIndices = GetArrayField<int>(comp, "contestantIndices").Get(expectedLength: 5, validator: v => v < 0 || v >= possibleNames.Length ? "out of range" : null);
        for (var ct = 0; ct < 5; ct++)
        {
            yield return question(SBoxing.StrengthByContestant, args: [possibleNames[contestantIndices[ct]]]).Answers(contestantStrengths[ct].ToString());
            yield return question(SBoxing.ContestantByStrength, args: [contestantStrengths[ct].ToString()]).Answers(possibleNames[contestantIndices[ct]], preferredWrong: possibleNames);
        }
        yield return question(SBoxing.QNames).Answers(contestantIndices.Select(ix => possibleNames[ix]).ToArray(), preferredWrong: possibleNames);

        foreach (var ix in contestantIndices.Distinct())
            yield return new Discriminator(SBoxing.DNames, $"cn-{ix}", args: [possibleNames[ix]], avoidAnswers: [possibleNames[ix]]);
    }
}
