using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLiteralMaze
{
    [Question("Which letter was in this position in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letter
}

public partial class SouvenirModule
{
    [Handler("literalMaze", "Literal Maze", typeof(SLiteralMaze), "Timwi")]
    [ManualQuestion("Which letter was in each position?")]
    private IEnumerator<SouvenirInstruction> ProcessLiteralMaze(ModuleData module)
    {
        var comp = GetComponent(module, "literalMazeScript");
        yield return WaitForSolve;

        var mazeString = GetField<string>(comp, "mazeString").Get(str => str.Length != 16 ? "expected length 16" : null);
        var cleartext = GetField<char[]>(comp, "cleartext").Get(ar => ar.Length != mazeString.Distinct().Count() ? "expected length of ‘cleartext’ to match number of distinct letters in ‘mazeString’" : null);
        if (mazeString.Any(ch => ch - 'a' < 0 && ch - 'a' >= cleartext.Length))
            throw new AbandonModuleException($"‘mazeString’ ({mazeString}) contains unexpected character (expected a–{(char) ('a' + cleartext.Length - 1)}).");
        for (var cell = 0; cell < 16; cell++)
            yield return question(SLiteralMaze.Letter, questionSprite: Sprites.GenerateGridSprite(4, 4, cell)).Answers(cleartext[mazeString[cell] - 'a'].ToString());
    }
}