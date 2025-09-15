using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLiteralMaze
{
    [SouvenirQuestion("Which letter was in this position in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letter
}

public partial class SouvenirModule
{
    [SouvenirHandler("literalMaze", "Literal Maze", typeof(SLiteralMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessLiteralMaze(ModuleData module)
    {
        var comp = GetComponent(module, "literalMazeScript");
        yield return WaitForSolve;

        var mazeString = GetField<string>(comp, "mazeString").Get(str => str.Length != 16 ? "expected length 16" : null);
        var cleartext = GetField<char[]>(comp, "cleartext").Get(ar => ar.Length != mazeString.Distinct().Count() ? "expected length of ‘cleartext’ to match number of distinct letters in ‘mazeString’" : null);
        if (mazeString.Any(ch => ch - 'a' < 0 && ch - 'a' >= cleartext.Length))
            throw new AbandonModuleException($"‘mazeString’ ({mazeString}) contains unexpected character (expected a–{(char) ('a' + cleartext.Length - 1)}).");
        var qs = new List<QandA>();
        for (var cell = 0; cell < 16; cell++)
            qs.Add(makeQuestion(Question.LiteralMazeLetter, module, Sprites.GenerateGridSprite(4, 4, cell), correctAnswers: new[] { cleartext[mazeString[cell] - 'a'].ToString() }));
        addQuestions(module, qs);
    }
}