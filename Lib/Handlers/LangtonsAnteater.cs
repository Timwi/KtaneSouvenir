using System.Collections.Generic;
using System.Linq;
using Souvenir;

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
        if (initialBlacks.Count >= 5)
            yield return question(SLangtonsAnteater.InitialState, args: ["white"]).Answers(initialWhites.Select(pos => new Coord(5, 5, pos)).ToArray());
        if (initialWhites.Count >= 5)
            yield return question(SLangtonsAnteater.InitialState, args: ["black"]).Answers(initialBlacks.Select(pos => new Coord(5, 5, pos)).ToArray());
    }
}