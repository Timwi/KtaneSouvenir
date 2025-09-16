using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SColorDecoding
{
    [SouvenirQuestion("Which color {1} in the {2}-stage indicator pattern in {0}?", TwoColumns4Answers, "Green", "Purple", "Red", "Blue", "Yellow", TranslateAnswers = true, TranslateArguments = [true, false], Arguments = ["appeared", QandA.Ordinal, "did not appear", QandA.Ordinal], ArgumentGroupSize = 2)]
    IndicatorColors,

    [SouvenirQuestion("What was the {1}-stage indicator pattern in {0}?", TwoColumns4Answers, "Checkered", "Horizontal", "Vertical", "Solid", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    IndicatorPattern
}

public partial class SouvenirModule
{
    [SouvenirHandler("Color Decoding", "Color Decoding", typeof(SColorDecoding), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessColorDecoding(ModuleData module)
    {
        var comp = GetComponent(module, "ColorDecoding");
        var fldInputButtons = GetArrayField<KMSelectable>(comp, "InputButtons", isPublic: true);
        var fldStageNum = GetIntField(comp, "stagenum");
        var fldIndicator = GetField<object>(comp, "indicator");
        var indicatorGrid = GetArrayField<GameObject>(comp, "IndicatorGrid", isPublic: true).Get();

        var patterns = new Dictionary<int, string>();
        var colors = new Dictionary<int, string[]>();
        var isSolved = false;
        var isAbandoned = false;

        var inputButtons = fldInputButtons.Get();
        var origInteract = inputButtons.Select(ib => ib.OnInteract).ToArray();
        object lastIndicator = null;

        var colorNameMapping = new Dictionary<string, string>
        {
            { "R", "Red" },
            { "G", "Green" },
            { "B", "Blue" },
            { "Y", "Yellow" },
            { "P", "Purple" }
        };

        var update = new Action(() =>
        {
            // We mustn’t throw an exception during the module’s button handler
            try
            {
                var ind = fldIndicator.Get();
                if (ReferenceEquals(ind, lastIndicator))
                    return;
                lastIndicator = ind;
                var indColors = GetField<IList>(ind, "indicator_colors").Get(
                    v => v.Count == 0 ? "no indicator colors" :
                    v.Cast<object>().Any(col => !colorNameMapping.ContainsKey(col.ToString())) ? "color is not in the color name mapping" : null);
                var stageNum = fldStageNum.Get();
                var patternName = GetField<object>(ind, "pattern").Get().ToString();
                patterns[stageNum] = patternName.Substring(0, 1) + patternName.Substring(1).ToLowerInvariant();
                colors[stageNum] = indColors.Cast<object>().Select(obj => colorNameMapping[obj.ToString()]).ToArray();
            }
            catch (AbandonModuleException amex)
            {
                Debug.Log($"<Souvenir #{_moduleId}> Abandoning Color Decoding because: {amex.Message}");
                isAbandoned = true;
            }
        });
        update();

        foreach (var i in Enumerable.Range(0, inputButtons.Length))    // Do not use ‘for’ loop as the loop variable is captured by a lambda
        {
            inputButtons[i].OnInteract = delegate
            {
                var ret = origInteract[i]();
                if (isSolved || isAbandoned)
                    return ret;

                if (fldStageNum.Get() >= 3)
                {
                    for (var j = 0; j < indicatorGrid.Length; j++)
                        indicatorGrid[j].GetComponent<MeshRenderer>().material.color = Color.black;
                    isSolved = true;
                }
                else
                    update();

                return ret;
            };
        }

        while (!isSolved && !isAbandoned)
            yield return new WaitForSeconds(.1f);

        for (var ix = 0; ix < inputButtons.Length; ix++)
            inputButtons[ix].OnInteract = origInteract[ix];

        if (isAbandoned)
            throw new AbandonModuleException("See error logged earlier.");

        if (Enumerable.Range(0, 3).Any(k => !patterns.ContainsKey(k) || !colors.ContainsKey(k)))
            throw new AbandonModuleException($"I have a discontinuous set of stages: {patterns.Keys.JoinString(", ")}/{colors.Keys.JoinString(", ")}.");

        for (var stage = 0; stage < 3; stage++)
        {
            if (colors[stage].Length <= 2)
                yield return question(SColorDecoding.IndicatorColors, args: ["appeared", Ordinal(stage + 1)]).Answers(colors[stage]);
            else if (colors[stage].Length >= 3)
                yield return question(SColorDecoding.IndicatorColors, args: ["did not appear", Ordinal(stage + 1)]).Answers(colorNameMapping.Values.Except(colors[stage]).ToArray());
            yield return question(SColorDecoding.IndicatorPattern, args: [Ordinal(stage + 1)]).Answers(patterns[stage]);
        }
    }
}
