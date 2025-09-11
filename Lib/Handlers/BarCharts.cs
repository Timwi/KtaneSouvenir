using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBarCharts
{
    [SouvenirQuestion("What was the category of {0}?", OneColumn4Answers, null, ExampleAnswers = ["Non-Percussion Instruments", "European Capital Cities", "Cast of Star Trek: TOS", "Percussion Instruments", "Zodiac Signs", "20th Century Composers"])]
    Category,
    
    [SouvenirQuestion("What was the unit of {0}?", ThreeColumns6Answers, "Popularity", "Frequency", "Responses", "Occurrences", "Density", "Magnitude")]
    Unit,
    
    [SouvenirQuestion("What was the label of the {1} bar in {0}?", TwoColumns4Answers, null, ExampleAnswers = ["Glockenspiel", "C.Discharge", "Shakespeare", "Sagittarius", "Malted Milk", "Venting Gas"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Label,
    
    [SouvenirQuestion("What was the color of the {1} bar in {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Color,
    
    [SouvenirQuestion("What was the position of the {1} bar in {0}?", TwoColumns4Answers, TranslateFormatArgs = [true], Arguments = ["shortest", "second shortest", "second tallest", "tallest"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Ordinal(1, 4)]
    Height
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSBarCharts", "Bar Charts", typeof(SBarCharts), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessBarCharts(ModuleData module)
    {
        var comp = GetComponent(module, "BarChartsScript");
        yield return WaitForSolve;

        var allVariableSets = GetStaticField<object[]>(comp.GetType(), "AllVariableSets").Get().ToArray();
        var chosenSet = GetField<object>(comp, "ChosenSet").Get();
        var fldName = GetField<string>(allVariableSets[0], "Name", isPublic: true);
        var fldVariables = GetArrayField<string>(allVariableSets[0], "Variables", isPublic: true);
        var barColours = GetField<IList>(comp, "BarColours").Get();
        var chosenUnit = GetField<Enum>(comp, "yAxisLabel").Get();
        var relevantLabels = fldVariables.GetFrom(chosenSet);
        var heightOrderIndices = GetListField<float>(comp, "HeightOrder").Get(expectedLength: 4).Select(Mathf.RoundToInt).ToList();
        var labels = GetArrayField<Text>(comp, "BarTextRends", true).Get(expectedLength: 4).Select(t => t.text).ToArray();
        var heightArr = new[] { "shortest", "second shortest", "second tallest", "tallest" };
        var allCategories = new List<string>();
        var allLabels = new List<string>();

        foreach (var variableSet in allVariableSets)
        {
            allCategories.Add(fldName.GetFrom(variableSet));
            allLabels.AddRange(fldVariables.GetFrom(variableSet));
        }

        var qs = new List<QandA>
        {
            makeQuestion(Question.BarChartsCategory, module, correctAnswers: new[] { fldName.GetFrom(chosenSet) }, allAnswers: allCategories.ToArray()),
            makeQuestion(Question.BarChartsUnit, module, correctAnswers: new[] { chosenUnit.ToString() })
        };

        for (var i = 0; i < 4; i++)
        {
            var correctHeightPos = heightOrderIndices.IndexOf(i + 1) + 1;
            qs.AddRange(new[] {
                makeQuestion(Question.BarChartsLabel, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { labels[i] }, allAnswers: relevantLabels),
                makeQuestion(Question.BarChartsColor, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { barColours[i].ToString() }),
                makeQuestion(Question.BarChartsHeight, module, formatArgs: new[] { heightArr[i] }, correctAnswers: new[] { Ordinal(correctHeightPos) })
            });
        }

        addQuestions(module, qs);
    }
}