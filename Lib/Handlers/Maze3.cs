using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMaze3
{
    [SouvenirQuestion("What was the color of the starting face in {0}?", ThreeColumns6Answers, "Red", "Blue", "Yellow", "Green", "Magenta", "Orange", TranslateAnswers = true)]
    StartingFace
}

public partial class SouvenirModule
{
    [SouvenirHandler("maze3", "Maze³", typeof(SMaze3), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessMaze3(ModuleData module)
    {
        var comp = GetComponent(module, "maze3Script");
        var node = GetIntField(comp, "node").Get(min: 0, max: 53);

        yield return WaitForSolve;

        var colors = new[] { "Red", "Blue", "Yellow", "Green", "Magenta", "Orange" };
        addQuestion(module, Question.Maze3StartingFace, correctAnswers: new[] { colors[node / 9] });
    }
}