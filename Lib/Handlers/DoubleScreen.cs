using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SDoubleScreen
{
    [SouvenirQuestion("What color was the {1} screen in the {2} stage of {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", TranslateAnswers = true, Arguments = ["top", QandA.Ordinal, "bottom", QandA.Ordinal], ArgumentGroupSize = 2, TranslateFormatArgs = [true, false])]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("doubleScreenModule", "Double Screen", typeof(SDoubleScreen), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessDoubleScreen(ModuleData module)
    {
        var comp = GetComponent(module, "DoubleScreenScript");

        List<(int Top, int Bottom)> stages = new();
        module.Module.OnStrike += () => { stages.Clear(); return false; };

        yield return null;  // Ensures that the module’s Start() method has run
        var stageCount = GetField<int>(comp, "stageCount").Get(v => v is < 2 or > 3 ? $"Bad stage count {v}" : null);
        var screen = GetArrayField<GameObject>(comp, "screens", isPublic: true).Get(expectedLength: 2)[0];
        var colors = GetArrayField<int>(comp, "colors");

        var newStage = true;
        while (module.Unsolved)
        {
            if (newStage && screen.activeSelf)
            {
                newStage = false;
                var col = colors.Get(expectedLength: 2, validator: i => i is < 0 or > 3 ? $"Bad color {i}" : null);
                stages.Add((col[0], col[1]));
            }
            else if (!newStage && !screen.activeSelf)
                newStage = true;

            // Screens are off for 0.2s between stages and only turn back on after stage generation.
            yield return new WaitForSeconds(.1f);
        }

        if (stages.Count != stageCount)
            throw new AbandonModuleException($"Expected {stageCount} stages but found {stages.Count}.");

        var colorNames = new string[] { "Red", "Yellow", "Green", "Blue" };
        addQuestions(module, stages.SelectMany((s, i) => new QandA[] {
                makeQuestion(Question.DoubleScreenColors, module, correctAnswers: new[] { colorNames[s.Top] }, formatArgs: new[] { "top", Ordinal(i + 1) }),
                makeQuestion(Question.DoubleScreenColors, module, correctAnswers: new[] { colorNames[s.Bottom] }, formatArgs: new[] { "bottom", Ordinal(i + 1) })
        }));
    }
}