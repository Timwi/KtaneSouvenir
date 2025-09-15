using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

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

        var qs = new List<QandA>();
        for (var ct = 0; ct < 5; ct++)
        {
            qs.Add(makeQuestion(Question.BoxingStrengthByContestant, module, formatArgs: new[] { possibleNames[contestantIndices[ct]] }, correctAnswers: new[] { contestantStrengths[ct].ToString() }));
            qs.Add(makeQuestion(Question.BoxingContestantByStrength, module, formatArgs: new[] { "first name", contestantStrengths[ct].ToString() }, correctAnswers: new[] { possibleNames[contestantIndices[ct]] }, preferredWrongAnswers: possibleNames));
            qs.Add(makeQuestion(Question.BoxingContestantByStrength, module, formatArgs: new[] { "last name", contestantStrengths[ct].ToString() }, correctAnswers: new[] { possibleLastNames[lastNameIndices[ct]] }, preferredWrongAnswers: possibleLastNames));
            qs.Add(makeQuestion(Question.BoxingContestantByStrength, module, formatArgs: new[] { "substitute’s first name", contestantStrengths[ct].ToString() }, correctAnswers: new[] { possibleSubstituteNames[substituteIndices[ct]] }, preferredWrongAnswers: possibleSubstituteNames));
            qs.Add(makeQuestion(Question.BoxingContestantByStrength, module, formatArgs: new[] { "substitute’s last name", contestantStrengths[ct].ToString() }, correctAnswers: new[] { possibleLastNames[substituteLastNameIndices[ct]] }, preferredWrongAnswers: possibleLastNames));
        }
        qs.Add(makeQuestion(Question.BoxingNames, module, formatArgs: new[] { "contestant’s first name", }, correctAnswers: contestantIndices.Select(ix => possibleNames[ix]).ToArray(), preferredWrongAnswers: possibleNames));
        qs.Add(makeQuestion(Question.BoxingNames, module, formatArgs: new[] { "contestant’s last name" }, correctAnswers: lastNameIndices.Select(ix => possibleLastNames[ix]).ToArray(), preferredWrongAnswers: possibleLastNames));
        qs.Add(makeQuestion(Question.BoxingNames, module, formatArgs: new[] { "substitute’s first name" }, correctAnswers: substituteIndices.Select(ix => possibleSubstituteNames[ix]).ToArray(), preferredWrongAnswers: possibleSubstituteNames));
        qs.Add(makeQuestion(Question.BoxingNames, module, formatArgs: new[] { "substitute’s last name" }, correctAnswers: substituteLastNameIndices.Select(ix => possibleLastNames[ix]).ToArray(), preferredWrongAnswers: possibleLastNames));
        addQuestions(module, qs);
    }
}