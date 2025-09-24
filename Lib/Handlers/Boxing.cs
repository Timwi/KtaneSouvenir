using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBoxing
{
    [SouvenirQuestion("What was {1}’s strength rating on {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", Arguments = ["Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander"], ArgumentGroupSize = 1)]
    StrengthByContestant,

    [SouvenirQuestion("What was the {1} of the contestant with strength rating {2} on {0}?", TwoColumns4Answers, ExampleAnswers = ["Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander"], TranslateArguments = [true, false], Arguments = ["first name", "0", "first name", "1", "first name", "2", "last name", "0", "last name", "1", "last name", "2", "substitute’s first name", "0", "substitute’s first name", "1", "substitute’s first name", "2", "substitute’s last name", "0", "substitute’s last name", "1", "substitute’s last name", "2"], ArgumentGroupSize = 2)]
    ContestantByStrength,

    [SouvenirQuestion("Which {1} appeared on {0}?", TwoColumns4Answers, ExampleAnswers = ["Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander"], Arguments = ["contestant’s first name", "contestant’s last name", "substitute’s first name", "substitute’s last name"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Names
}

public partial class SouvenirModule
{
    [SouvenirHandler("boxing", "Boxing", typeof(SBoxing), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessBoxing(ModuleData module)
    {
        var comp = GetComponent(module, "boxing");
        yield return WaitForSolve;

        var possibleNames = GetStaticField<string[]>(comp.GetType(), "possibleNames").Get();
        var possibleSubstituteNames = GetStaticField<string[]>(comp.GetType(), "possibleSubstituteNames").Get();
        var possibleLastNames = GetStaticField<string[]>(comp.GetType(), "possibleLastNames").Get();
        var contestantStrengths = GetArrayField<int>(comp, "contestantStrengths").Get(expectedLength: 5);
        var contestantIndices = GetArrayField<int>(comp, "contestantIndices").Get(expectedLength: 5, validator: v => v < 0 || v >= possibleNames.Length ? "out of range" : null);
        var lastNameIndices = GetArrayField<int>(comp, "lastNameIndices").Get(expectedLength: 5, validator: v => v < 0 || v >= possibleLastNames.Length ? "out of range" : null);
        var substituteIndices = GetArrayField<int>(comp, "substituteIndices").Get(expectedLength: 5, validator: v => v < 0 || v >= possibleSubstituteNames.Length ? "out of range" : null);
        var substituteLastNameIndices = GetArrayField<int>(comp, "substituteLastNameIndices").Get(expectedLength: 5, validator: v => v < 0 || v >= possibleLastNames.Length ? "out of range" : null);
        for (var ct = 0; ct < 5; ct++)
        {
            yield return question(SBoxing.StrengthByContestant, args: [possibleNames[contestantIndices[ct]]]).Answers(contestantStrengths[ct].ToString());
            yield return question(SBoxing.ContestantByStrength, args: ["first name", contestantStrengths[ct].ToString()]).Answers(possibleNames[contestantIndices[ct]], preferredWrong: possibleNames);
            yield return question(SBoxing.ContestantByStrength, args: ["last name", contestantStrengths[ct].ToString()]).Answers(possibleLastNames[lastNameIndices[ct]], preferredWrong: possibleLastNames);
            yield return question(SBoxing.ContestantByStrength, args: ["substitute’s first name", contestantStrengths[ct].ToString()]).Answers(possibleSubstituteNames[substituteIndices[ct]], preferredWrong: possibleSubstituteNames);
            yield return question(SBoxing.ContestantByStrength, args: ["substitute’s last name", contestantStrengths[ct].ToString()]).Answers(possibleLastNames[substituteLastNameIndices[ct]], preferredWrong: possibleLastNames);
        }
        yield return question(SBoxing.Names, args: ["contestant’s first name",]).Answers(contestantIndices.Select(ix => possibleNames[ix]).ToArray(), preferredWrong: possibleNames);
        yield return question(SBoxing.Names, args: ["contestant’s last name"]).Answers(lastNameIndices.Select(ix => possibleLastNames[ix]).ToArray(), preferredWrong: possibleLastNames);
        yield return question(SBoxing.Names, args: ["substitute’s first name"]).Answers(substituteIndices.Select(ix => possibleSubstituteNames[ix]).ToArray(), preferredWrong: possibleSubstituteNames);
        yield return question(SBoxing.Names, args: ["substitute’s last name"]).Answers(substituteLastNameIndices.Select(ix => possibleLastNames[ix]).ToArray(), preferredWrong: possibleLastNames);
    }
}