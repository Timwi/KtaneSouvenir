using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SColorsMaximization
{
    [SouvenirQuestion("How many buttons were {1} in {0}?", ThreeColumns6Answers, Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(0, 11)]
    ColorCount
}

public partial class SouvenirModule
{
    [SouvenirHandler("colors_maximization", "Colors Maximization", typeof(SColorsMaximization), "NickLatkovich")]
    private IEnumerator<SouvenirInstruction> ProcessColorsMaximization(ModuleData module)
    {
        var comp = GetComponent(module, "ColorsMaximizationModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
            yield return legitimatelyNoQuestion(module, "The module was force-solved.");

        var colorNameDic = GetStaticField<Dictionary<Color, string>>(comp.GetType(), "colorNames", true).Get();
        var colorNames = colorNameDic.Values.ToArray();
        var allColors = GetStaticField<Color[]>(comp.GetType(), "allColors").Get();

        var questions = new List<QandA>();
        foreach (var color in allColors)
            questions.Add(makeQuestion(Question.ColorsMaximizationColorCount, module,
                formatArgs: new[] { colorNameDic[color] },
                correctAnswers: new[] { GetField<Dictionary<Color, int>>(comp, "countOfColor").Get()[color].ToString() }));

        addQuestions(module, questions);
    }
}