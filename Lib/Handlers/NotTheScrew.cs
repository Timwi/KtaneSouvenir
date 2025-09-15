using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotTheScrew
{
    [SouvenirQuestion("What was the initial position in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(6, 4)]
    InitialPosition
}

public partial class SouvenirModule
{
    [SouvenirHandler("notTheScrew", "Not The Screw", typeof(SNotTheScrew), "GhostSalt")]
    private IEnumerator<SouvenirInstruction> ProcessNotTheScrew(ModuleData module)
    {
        var comp = GetComponent(module, "NotTheScrewModule");
        var position = GetField<int>(comp, "_curPos").Get();

        yield return WaitForSolve;

        yield return question(SNotTheScrew.InitialPosition).Answers(new Coord(6, 4, position));
    }
}