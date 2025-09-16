using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SButtonSequence
{
    [SouvenirQuestion("How many of the buttons in {0} were {1}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["red", "blue", "yellow", "white"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 12)]
    sColorOccurrences
}

public partial class SouvenirModule
{
    [SouvenirHandler("buttonSequencesModule", "Button Sequence", typeof(SButtonSequence), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessButtonSequence(ModuleData module)
    {
        var comp = GetComponent(module, "ButtonSequencesModule");
        yield return WaitForSolve;

        var panelInfo = GetField<Array>(comp, "PanelInfo").Get(arr =>
            arr.Rank != 2 ? $"has rank {arr.Rank}, expected 2" :
            arr.GetLength(1) != 3 ? $"GetLength(1) == {arr.GetLength(1)}, expected 3" :
            Enumerable.Range(0, arr.GetLength(0)).Any(x => Enumerable.Range(0, arr.GetLength(1)).Any(y => arr.GetValue(x, y) == null)) ? "contains null" : null);

        var obj = panelInfo.GetValue(0, 0);
        var fldColor = GetIntField(obj, "color", isPublic: true);
        var colorNames = GetStaticField<string[]>(comp.GetType(), "ColorNames").Get();
        var colorOccurrences = new Dictionary<int, int>();
        for (var i = panelInfo.GetLength(0) - 1; i >= 0; i--)
            for (var j = 0; j < 3; j++)
                colorOccurrences.IncSafe(fldColor.GetFrom(panelInfo.GetValue(i, j), v => v < 0 || v >= colorNames.Length ? $"out of range; colorNames.Length={colorNames.Length} ([{colorNames.JoinString(", ")}])" : null));

        addQuestions(module, colorOccurrences.Select(kvp =>
            makeQuestion(SButtonSequence.sColorOccurrences, module,
                formatArgs: new[] { colorNames[kvp.Key].ToLowerInvariant() },
                correctAnswers: new[] { kvp.Value.ToString() },
                preferredWrongAnswers: colorOccurrences.Values.Select(v => v.ToString()).ToArray())));
    }
}