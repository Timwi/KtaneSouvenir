using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLangtonsAnteater
{
    [SouvenirQuestion("Which of these squares was initially {1} in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = ["black", "white"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Grid(5, 5)]
    InitialState
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSLangtonsAnteater", "Langton’s Anteater", typeof(SLangtonsAnteater), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessLangtonsAnteater(ModuleData module)
    {
        var comp = GetComponent(module, "LangtonsAnteaterScript");

        var initialStates = GetArrayField<bool>(comp, "Board").Get(expectedLength: 25);
        var initialWhites = new List<int>();
        var initialBlacks = new List<int>();
        for (var pos = 0; pos < 25; pos++)
            (initialStates[pos] ? initialBlacks : initialWhites).Add(pos);

        if (initialBlacks.Count == 0 || initialWhites.Count == 0)
            yield return legitimatelyNoQuestion(module.Module, "the module generated 25 cells of the same colour.");

        yield return WaitForSolve;

        var qs = new List<QandA>();
        if (initialBlacks.Count >= 5)
            qs.Add(makeQuestion(Question.LangtonsAnteaterInitialState, module, formatArgs: new[] { "white" }, correctAnswers: initialWhites.Select(pos => new Coord(5, 5, pos)).ToArray()));
        if (initialWhites.Count >= 5)
            qs.Add(makeQuestion(Question.LangtonsAnteaterInitialState, module, formatArgs: new[] { "black" }, correctAnswers: initialBlacks.Select(pos => new Coord(5, 5, pos)).ToArray()));
        addQuestions(module, qs);
    }
}