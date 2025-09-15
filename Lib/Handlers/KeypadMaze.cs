using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SKeypadMaze
{
    [SouvenirQuestion("Which of these cells was yellow in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(6, 6)]
    Yellow
}

public partial class SouvenirModule
{
    [SouvenirHandler("KeypadMaze", "Keypad Maze", typeof(SKeypadMaze), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessKeypadMaze(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "KeypadMaze");
        var yellow = GetArrayField<int>(comp, "yellow", true).Get(expectedLength: 5, validator: v => v is < 0 or > 35 ? "Expected range 0–35" : null);

        yield return question(SKeypadMaze.Yellow).Answers(yellow.Take(4).Select(i => new Coord(6, 6, i)).ToArray());
    }
}