using System.Collections.Generic;
using Souvenir;
using Souvenir.Reflection;
using static Souvenir.AnswerLayout;

public enum SUndertunneling
{
    [SouvenirQuestion("What was the position in the maze after the first phase in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(7, 7)]
    PositionInMazeAfterPhaseOne
}

public partial class SouvenirModule
{
    [SouvenirHandler("undertunneling", "Undertunneling", typeof(SUndertunneling), "KiloBites")]
    private IEnumerator<SouvenirInstruction> ProcessUndertunneling(ModuleData module)
    {
        var comp = GetComponent(module, "UndertunnelingScript");
        var fldIsInStage2 = GetField<bool>(comp, "stage2");
        var fldMaze = GetField<object>(comp, "maze");

        int? startingPos = null;
        IntFieldInfo fldCurPos = null;

        while (module.Unsolved)
        {
            yield return null;

            if (fldIsInStage2.Get() && startingPos == null)
            {
                var maze = fldMaze.Get();
                startingPos = (fldCurPos ??= GetIntField(maze, "_curPos")).GetFrom(maze, min: 0, max: 48);
            }
            else if (!fldIsInStage2.Get())
                startingPos = null;
        }

        if (startingPos == null)
            throw new AbandonModuleException("Undertunneling was solved without reaching stage 2.");

        yield return question(SUndertunneling.PositionInMazeAfterPhaseOne).Answers(new Coord(7, 7, startingPos.Value));
    }
}
