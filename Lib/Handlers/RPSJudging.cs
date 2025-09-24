using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SRPSJudging
{
    [SouvenirQuestion("Which round did the {1} team {2} in {0}?", ThreeColumns6Answers, ExampleAnswers = [QandA.Ordinal], Arguments = ["red", "win", "blue", "win", "red", "lose", "blue", "lose"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    QWinner,

    [SouvenirQuestion("Which round was a draw in {0}?", ThreeColumns6Answers, ExampleAnswers = [QandA.Ordinal])]
    QDraw,

    [SouvenirDiscriminator("the RPS Judging where the {0} team {1} the {2} round", Arguments = ["red", "won", QandA.Ordinal, "red", "lost", QandA.Ordinal, "blue", "won", QandA.Ordinal, "blue", "lost", QandA.Ordinal], ArgumentGroupSize = 3, TranslateArguments = [true, true, false])]
    DWinner,

    [SouvenirDiscriminator("the RPS Judging with a draw in the {0} round", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    DDraw
}

public partial class SouvenirModule
{
    [SouvenirHandler("RPSJudging", "RPS Judging", typeof(SRPSJudging), "Anonymous", IsBossModule = true)]
    private IEnumerator<SouvenirInstruction> ProcessRPSJudging(ModuleData module)
    {
        var comp = GetComponent(module, "RPSJudgingScript");

        const int OutcomeRed = -1;
        const int OutcomeDraw = 0;
        const int OutcomeBlue = 1;
        var outcomes = new[] { OutcomeRed, OutcomeDraw, OutcomeBlue };

        while (!_noUnignoredModulesLeft)
            yield return null;

        var leftDisplays = GetListField<int>(comp, "LeftDisplays").Get(minLength: 0, validator: v => v is < 0 or > 100 ? "Expected range [0, 101]" : null);
        var rightDisplays = GetListField<int>(comp, "RightDisplays").Get(expectedLength: leftDisplays.Count, validator: v => v is < 0 or > 100 ? "Expected range [0, 101]" : null);

        if (leftDisplays.Count == 0)
            yield return legitimatelyNoQuestion(module, "There were no stages.");

        yield return null;

        bool pickAnswers(List<int> valid, out string[] correct, out string[] incorrect)
        {
            correct = valid.Select(x => Ordinal(x + 1)).ToArray();
            incorrect = Enumerable.Range(0, leftDisplays.Count).Except(valid).Select(x => Ordinal(x + 1)).ToArray();
            return correct.Length > 0 && incorrect.Length > 0;
        }

        static int matchup(int blue, int red) => blue == red ? OutcomeDraw : (red - blue + 101) % 101 < 51 ? OutcomeBlue : OutcomeRed;

        List<int> blueWins = [], redWins = [], draws = [];
        for (var stage = 0; stage < leftDisplays.Count; stage++)
        {
            switch (matchup(leftDisplays[stage], rightDisplays[stage]))
            {
                case OutcomeRed:
                    redWins.Add(stage);
                    yield return new Discriminator(SRPSJudging.DWinner, $"red-win-{stage}", args: ["red", "won", Ordinal(stage + 1)], avoidAnswers: [Ordinal(stage + 1)]);
                    yield return new Discriminator(SRPSJudging.DWinner, $"blue-lose-{stage}", args: ["blue", "lost", Ordinal(stage + 1)], avoidAnswers: [Ordinal(stage + 1)]);
                    break;

                case OutcomeDraw:
                    draws.Add(stage);
                    yield return new Discriminator(SRPSJudging.DDraw, $"draw-{stage}", args: [Ordinal(stage + 1)], avoidAnswers: [Ordinal(stage + 1)]);
                    break;

                case OutcomeBlue:
                    blueWins.Add(stage);
                    yield return new Discriminator(SRPSJudging.DWinner, $"blue-win-{stage}", args: ["blue", "won", Ordinal(stage + 1)], avoidAnswers: [Ordinal(stage + 1)]);
                    yield return new Discriminator(SRPSJudging.DWinner, $"red-lose-{stage}", args: ["red", "lost", Ordinal(stage + 1)], avoidAnswers: [Ordinal(stage + 1)]);
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        var any = false;
        if (draws.Count > 0 && pickAnswers(draws, out var correct, out var incorrect))
        {
            yield return question(SRPSJudging.QDraw).Answers(correct, preferredWrong: incorrect);
            any = true;
        }
        if (blueWins.Count > 0 && pickAnswers(blueWins, out correct, out incorrect))
        {
            yield return question(SRPSJudging.QWinner, args: ["blue", "win"]).Answers(correct, preferredWrong: incorrect);
            yield return question(SRPSJudging.QWinner, args: ["red", "lose"]).Answers(correct, preferredWrong: incorrect);
            any = true;
        }
        if (redWins.Count > 0 && pickAnswers(redWins, out correct, out incorrect))
        {
            yield return question(SRPSJudging.QWinner, args: ["blue", "lose"]).Answers(correct, preferredWrong: incorrect);
            yield return question(SRPSJudging.QWinner, args: ["red", "win"]).Answers(correct, preferredWrong: incorrect);
            any = true;
        }

        if (!any)
            yield return legitimatelyNoQuestion(module, "Every stage had the same outcome.");
    }
}
