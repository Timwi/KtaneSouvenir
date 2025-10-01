using System.Collections.Generic;
using Souvenir;
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
        var hasEnteredStage2 = GetField<bool>(comp, "stage2");

        var startingPos = (int?) null;

        while (module.Unsolved)
        {
            yield return null;
            var maze = GetField<object>(comp, "maze").Get();

            if (hasEnteredStage2.Get() && startingPos == null)
                startingPos = GetIntField(maze, "_curPos").Get(min: 0, max: 48);

            else if (!hasEnteredStage2.Get() && startingPos != null)
                startingPos = null;
        }

        if (startingPos == null)
            throw new AbandonModuleException("The starting position can't be found!");

        yield return question(SUndertunneling.PositionInMazeAfterPhaseOne).Answers(new Coord(7, 7, startingPos.Value));
    }
}
