using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRPSJudging
{
    [SouvenirQuestion("Which round did the {1} team {2} in {0}?", ThreeColumns6Answers, ExampleAnswers = ["first", "second", "third", "fourth", "fifth", "sixth"], Arguments = ["red", "win", "blue", "win", "red", "lose", "blue", "lose"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    Winner,

    [SouvenirQuestion("Which round was a draw in {0}?", ThreeColumns6Answers, ExampleAnswers = ["first", "second", "third", "fourth", "fifth", "sixth"])]
    Draw
}

public partial class SouvenirModule
{
    [SouvenirHandler("RPSJudging", "RPS Judging", typeof(SRPSJudging), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessRPSJudging(ModuleData module)
    {
        var comp = GetComponent(module, "RPSJudgingScript");
        const string moduleId = "RPSJudging";

        const int OutcomeRed = -1;
        const int OutcomeDraw = 0;
        const int OutcomeBlue = 1;
        var outcomes = new[] { OutcomeRed, OutcomeDraw, OutcomeBlue };

        while (!_noUnignoredModulesLeft)
            yield return null;

        var leftDisplays = GetListField<int>(comp, "LeftDisplays").Get(minLength: 0, validator: v => v is < 0 or > 100 ? "Expected range [0, 101]" : null);
        var rightDisplays = GetListField<int>(comp, "RightDisplays").Get(expectedLength: leftDisplays.Count, validator: v => v is < 0 or > 100 ? "Expected range [0, 101]" : null);

        _rpsJudgingDisplays.Add((leftDisplays, rightDisplays));
        if (_rpsJudgingDisplays[0].blue.Count != leftDisplays.Count)
            throw new AbandonModuleException("There were inconsistent stage counts among modules.");
        if (leftDisplays.Count == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        yield return null;

        if (_rpsJudgingDisplays.Count != _moduleCounts[moduleId])
            throw new AbandonModuleException("The number of displays did not match the number of RPS Judging modules.");

        bool pickAnswers(List<int> valid, out string[] correct, out string[] incorrect)
        {
            correct = valid.Select(x => Ordinal(x + 1)).ToArray();
            incorrect = Enumerable.Range(0, leftDisplays.Count).Except(valid).Select(x => Ordinal(x + 1)).ToArray();
            return correct.Length > 0 && incorrect.Length > 0;
        }

        static int matchup(int blue, int red) => blue == red ? OutcomeDraw : (red - blue + 101) % 101 < 51 ? OutcomeBlue : OutcomeRed;

        bool pickUniqueStage(int outcomeToAvoid, out string format)
        {
            if (_moduleCounts[moduleId] == 1)
            {
                format = null;
                return true;
            }

            var options = outcomes.Where(oc => oc != outcomeToAvoid).SelectMany(outcome =>
                Enumerable
                    .Range(0, leftDisplays.Count)
                    .Where(stage => matchup(leftDisplays[stage], rightDisplays[stage]) == outcome && _rpsJudgingDisplays.Count(d => matchup(d.blue[stage], d.red[stage]) == outcome) == 1)
                    .Select(stage => (outcome, stage)))
                .ToArray();

            if (options.Length == 0)
            {
                format = null;
                return false;
            }

            format = (options.PickRandom(), UnityEngine.Random.Range(0, 2) != 0) switch
            {
                ((OutcomeRed, var stage), var b) => string.Format(
                    translateString(Question.RPSJudgingWinner, "the RPS Judging where the {0} team {1} the {2} round"),
                    translateFormatArg(Question.RPSJudgingWinner, b ? "blue" : "red"), translateFormatArg(Question.RPSJudgingWinner, b ? "lost" : "won"), Ordinal(stage + 1)),
                ((OutcomeDraw, var stage), _) => string.Format(
                    translateString(Question.RPSJudgingWinner, "the RPS Judging with a draw in the {0} round"),
                    Ordinal(stage + 1)),
                ((OutcomeBlue, var stage), var b) => string.Format(
                    translateString(Question.RPSJudgingWinner, "the RPS Judging where the {0} team {1} the {2} round"),
                    translateFormatArg(Question.RPSJudgingWinner, b ? "blue" : "red"), translateFormatArg(Question.RPSJudgingWinner, b ? "won" : "lost"), Ordinal(stage + 1)),
                _ => throw new InvalidOperationException()
            };
            return true;
        }

        IEnumerable<QandA> makeQuestions()
        {
            List<int> blueWins = new(), redWins = new(), draws = new();
            for (var stage = 0; stage < leftDisplays.Count; stage++)
                (matchup(leftDisplays[stage], rightDisplays[stage]) switch
                {
                    OutcomeRed => redWins,
                    OutcomeDraw => draws,
                    OutcomeBlue => blueWins,
                    _ => throw new InvalidOperationException()
                }).Add(stage);

            if (draws.Count > 0 && pickUniqueStage(OutcomeDraw, out var format) && pickAnswers(draws, out var correct, out var incorrect))
                yield return makeQuestion(Question.RPSJudgingDraw, moduleId, 1, formattedModuleName: format, correctAnswers: correct, preferredWrongAnswers: incorrect);
            if (blueWins.Count > 0 && pickUniqueStage(OutcomeBlue, out format) && pickAnswers(blueWins, out correct, out incorrect))
            {
                yield return makeQuestion(Question.RPSJudgingWinner, moduleId, 1, formattedModuleName: format, formatArgs: new[] { "blue", "win" }, correctAnswers: correct, preferredWrongAnswers: incorrect);
                yield return makeQuestion(Question.RPSJudgingWinner, moduleId, 1, formattedModuleName: format, formatArgs: new[] { "red", "lose" }, correctAnswers: correct, preferredWrongAnswers: incorrect);
            }
            if (redWins.Count > 0 && pickUniqueStage(OutcomeRed, out format) && pickAnswers(redWins, out correct, out incorrect))
            {
                yield return makeQuestion(Question.RPSJudgingWinner, moduleId, 1, formattedModuleName: format, formatArgs: new[] { "blue", "lose" }, correctAnswers: correct, preferredWrongAnswers: incorrect);
                yield return makeQuestion(Question.RPSJudgingWinner, moduleId, 1, formattedModuleName: format, formatArgs: new[] { "red", "win" }, correctAnswers: correct, preferredWrongAnswers: incorrect);
            }
        }

        var qs = makeQuestions().ToArray();
        if (qs.Length == 0)
            yield return legitimatelyNoQuestion(module, $"There were not enough stages at which this one (#{GetIntField(comp, "moduleId").Get()}) had a unique result.");
        addQuestions(module, qs);
    }
}
