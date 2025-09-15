using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGameOfLifeCruel
{
    [SouvenirQuestion("Which of these was a color combination that occurred in {0}?", TwoColumns4Answers, ExampleAnswers = ["Red/Orange", "Orange/Yellow", "Yellow/Green", "Green/Blue"])]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("GameOfLifeCruel", "Game of Life Cruel", typeof(SGameOfLifeCruel), "GhostSalt")]
    private IEnumerator<SouvenirInstruction> ProcessGameOfLifeCruel(ModuleData module)
    {
        var comp = GetComponent(module, "GameOfLifeCruel");
        yield return WaitForSolve;

        var colors = new[] { "Black", "White", "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Brown" };
        var colors1 = GetArrayField<int>(comp, "BtnColor1init").Get();
        var colors2 = GetArrayField<int>(comp, "BtnColor2init").Get();

        var answersList = new List<string>();
        for (var i = 0; i < 9; i++)
            if (i != 1)
                for (var j = 0; j < 9; j++)
                    if (j != 1 && (i > 1 || j > 1))
                        answersList.Add(colors[i] == colors[j] ? $"Solid {colors[i]}" : $"{colors[i]}/{colors[j]}");
        var allAnswers = answersList.ToArray();
        var correctAnswers = Enumerable.Range(0, 48).Where(i => (colors1[i] > 1 || colors2[i] > 1) && colors1[i] != 1 && colors2[i] != 1)
            .SelectMany(i => colors1[i] == colors2[i] ? new[] { $"Solid {colors[colors1[i]]}" } : new[] { $"{colors[colors1[i]]}/{colors[colors2[i]]}", $"{colors[colors2[i]]}/{colors[colors1[i]]}" })
            .Distinct().ToArray();

        if (correctAnswers.Length == 0)
            yield return legitimatelyNoQuestion(module, "There were no colored squares.");

        addQuestion(module, Question.GameOfLifeCruelColors, correctAnswers: correctAnswers, preferredWrongAnswers: allAnswers);
    }
}