using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNavigationDetermination
{
    [SouvenirQuestion("What was the color of the maze in {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", TranslateAnswers = true)]
    Color,

    [SouvenirQuestion("What was the label of the maze in {0}?", TwoColumns4Answers, "A", "B", "C", "D")]
    Label
}

public partial class SouvenirModule
{
    [SouvenirHandler("NavigationDeterminationModule", "Navigation Determination", typeof(SNavigationDetermination), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNavigationDetermination(ModuleData module)
    {
        var comp = GetComponent(module, "NavigationDeterminationScript");
        yield return WaitForSolve;

        var chosenPath = GetField<object>(comp, "_chosenPath").Get();
        var mazeNum = GetIntField(chosenPath, "MazeNum").Get(min: 0, max: 15);

        var maze = GetArrayField<object>(comp, "_mazes").Get(expectedLength: 16).GetValue(mazeNum);
        var color = GetField<int>(maze, "Color").Get();
        var label = GetField<char>(maze, "Label").Get();

        var colors = new string[] { "Red", "Yellow", "Green", "Blue" };

        addQuestions(module,
            makeQuestion(Question.NavigationDeterminationColor, module, correctAnswers: new[] { colors[color] }),
            makeQuestion(Question.NavigationDeterminationLabel, module, correctAnswers: new[] { label.ToString() })
        );
    }
}