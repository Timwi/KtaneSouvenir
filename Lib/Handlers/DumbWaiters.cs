using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDumbWaiters
{
    [SouvenirQuestion("Which player {1} present in {0}?", OneColumn4Answers, ExampleAnswers = ["Arceus", "Danny7007", "EpicToast", "eXish", "Fang", "Makebao", "MCD573", "Mr. Peanut", "Mythers", "Xmaster"], Arguments = ["was", "was not"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    PlayerAvailable
}

public partial class SouvenirModule
{
    [SouvenirHandler("dumbWaiters", "Dumb Waiters", typeof(SDumbWaiters), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessDumbWaiters(ModuleData module)
    {
        var comp = GetComponent(module, "dumbWaiters");
        yield return WaitForSolve;

        var players = GetStaticField<string[]>(comp.GetType(), "names").Get();
        var playersAvaiable = GetArrayField<int>(comp, "presentPlayers").Get();
        var availablePlayers = playersAvaiable.Select(ix => players[ix]).ToArray();

        addQuestions(module,
           makeQuestion(Question.DumbWaitersPlayerAvailable, module, formatArgs: new[] { "was" }, correctAnswers: availablePlayers, preferredWrongAnswers: players),
           makeQuestion(Question.DumbWaitersPlayerAvailable, module, formatArgs: new[] { "was not" }, correctAnswers: players.Where(a => !availablePlayers.Contains(a)).ToArray(), preferredWrongAnswers: players));

    }
}