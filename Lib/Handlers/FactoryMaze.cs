using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFactoryMaze
{
    [SouvenirQuestion("What room did you start in in {0}?", OneColumn4Answers, "Bathroom", "Assembly Line", "Cafeteria", "Room A9", "Broom Closet", "Basement", "Copy Room", "Unnecessarily Long-Named Room", "Library", "Break Room", "Empty Room with Two Doors", "Arcade", "Classroom", "Module Testing Room", "Music Studio", "Computer Room", "Infirmary", "Bomb Room", "Space", "Storage Room", "Lounge", "Conference Room", "Kitchen", "Incinerator")]
    StartRoom
}

public partial class SouvenirModule
{
    [SouvenirHandler("factoryMaze", "Factory Maze", typeof(SFactoryMaze), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessFactoryMaze(ModuleData module)
    {
        var comp = GetComponent(module, "FactoryMazeScript");
        yield return WaitForSolve;

        var usedRooms = GetArrayField<string>(comp, "usedRooms").Get(expectedLength: 5).ToArray();
        var startRoom = GetIntField(comp, "startRoom").Get(0, usedRooms.Length - 1);

        for (var i = usedRooms.Length - 1; i >= 0; --i)
            usedRooms[i] = usedRooms[i].Replace('\n', ' ');

        yield return question(SFactoryMaze.StartRoom).Answers(usedRooms[startRoom], preferredWrong: usedRooms);
    }
}