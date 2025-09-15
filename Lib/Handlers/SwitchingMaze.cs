using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSwitchingMaze
{
    [SouvenirQuestion("What was the seed in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/")]
    Seed,

    [SouvenirQuestion("What was the starting maze color in {0}?", ThreeColumns6Answers, "Blue", "Cyan", "Magenta", "Orange", "Red", "White", TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("MazeSwitching", "Switching Maze", typeof(SSwitchingMaze), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessSwitchingMaze(ModuleData module)
    {
        var comp = GetComponent(module, "SwitchingMazeScript");
        var seedTextMesh = GetField<TextMesh>(comp, "Seedling", isPublic: true).Get();
        var fldNumberBasis = GetField<int>(comp, "NumberBasis");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var seed = seedTextMesh.text;
        var numberBasis = fldNumberBasis.Get();

        var hadStrike = false;
        module.Module.OnStrike += delegate { hadStrike = true; return false; };

        while (module.Unsolved)
        {
            if (hadStrike)
            {
                seed = seedTextMesh.text;
                numberBasis = fldNumberBasis.Get();
                hadStrike = false;
            }
            yield return null;
        }

        var seedSplit = Regex.Replace(seed, " ", "").Split(':');
        var colorsOfTheMaze = GetArrayField<string>(comp, "ColorsOfMaze").Get();

        addQuestions(module,
            makeQuestion(Question.SwitchingMazeSeed, module, formatArgs: null, correctAnswers: new[] { seedSplit[1] }),
            makeQuestion(Question.SwitchingMazeColor, module, formatArgs: null, correctAnswers: new[] { colorsOfTheMaze[numberBasis] }, preferredWrongAnswers: colorsOfTheMaze));
    }
}