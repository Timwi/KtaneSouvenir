using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SGameOfLifeCruel
{
    [Question("Which of these was a color combination that occurred in {0}?", TwoColumns4Answers, ExampleAnswers = ["Red/Orange", "Orange/Yellow", "Yellow/Green", "Green/Blue"], TranslatableStrings = ["Solid {0}", "{0}/{1}", "Black", "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Brown"])]
    Colors
}

public partial class SouvenirModule
{
    [Handler("GameOfLifeCruel", "Game of Life Cruel", typeof(SGameOfLifeCruel), "GhostSalt")]
    [ManualQuestion("Which color combinations occurred?")]
    private IEnumerator<SouvenirInstruction> ProcessGameOfLifeCruel(ModuleData module)
    {
        var comp = GetComponent(module, "GameOfLifeCruel");
        yield return WaitForSolve;

        var colors = new[] { "Black", null, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Brown" }
            .Select(str => str == null ? null : TranslateQuestionString(SGameOfLifeCruel.Colors, str)).ToArray();
        var colors1 = GetArrayField<int>(comp, "BtnColor1init").Get();
        var colors2 = GetArrayField<int>(comp, "BtnColor2init").Get();

        var correctAnswers = (
            from ix in Enumerable.Range(0, 48)
            let i = Math.Min(colors1[ix], colors2[ix])
            let j = Math.Max(colors1[ix], colors2[ix])
            where (i > 1 || j > 1) && i != 1 && j != 1
            select string.Format(TranslateQuestionString(SGameOfLifeCruel.Colors, i == j ? "Solid {0}" : "{0}/{1}"), colors[i], colors[j])).ToArray();

        if (correctAnswers.Length == 0)
            yield return legitimatelyNoQuestion(module, "There were no colored squares.");

        var allAnswers = (
            from i in Enumerable.Range(0, 9)
            where i != 1
            from j in Enumerable.Range(i, 9 - i)
            where j != 1 && (i > 1 || j > 1)
            select string.Format(TranslateQuestionString(SGameOfLifeCruel.Colors, i == j ? "Solid {0}" : "{0}/{1}"), colors[i], colors[j])).ToArray();
        UnityEngine.Debug.Log($"♦ {allAnswers.JoinString("\n")}");

        yield return question(SGameOfLifeCruel.Colors).Answers(correctAnswers, all: allAnswers);
    }
}
