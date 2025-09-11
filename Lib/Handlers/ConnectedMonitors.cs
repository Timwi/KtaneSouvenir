using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SConnectedMonitors
{
    [SouvenirQuestion("What number was initially displayed on this screen in {0}?", ThreeColumns6Answers, UsesQuestionSprite = true)]
    [AnswerGenerator.Integers(0, 99)]
    Number,
    
    [SouvenirQuestion("What colour was the indicator on this screen in {0}?", ThreeColumns6Answers, "Red", "Orange", "Green", "Blue", "Purple", "White", UsesQuestionSprite = true, TranslateAnswers = true)]
    SingleIndicator,
    
    [SouvenirQuestion("What colour was the {1} indicator on this screen in {0}?", ThreeColumns6Answers, "Red", "Orange", "Green", "Blue", "Purple", "White", UsesQuestionSprite = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    OrdinalIndicator
}

public partial class SouvenirModule
{
    [SouvenirHandler("ConnectedMonitorsModule", "Connected Monitors", typeof(SConnectedMonitors), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessConnectedMonitors(ModuleData module)
    {
        var comp = GetComponent(module, "ConnectedMonitorsScript");

        yield return null; // Wait for monitors to become available
        var monitors = GetField<IList>(comp, "_monitors").Get(l => l.Count != 15 ? $"Bad monitor list length {l}" : null);
        var displayProp = GetProperty<int>(monitors[0], "DisplayValue", isPublic: true);
        // Grab screen values before they change from the blue-green rule
        var displays = monitors.Cast<object>().Select(m => displayProp.GetFrom(m, v => v is > 99 or < 0 ? $"Bad monitor value {v}" : null)).ToArray();

        yield return WaitForSolve;

        var indsProp = GetProperty<IList>(monitors[0], "Indicators", isPublic: true);
        PropertyInfo<object> colorProp = null;
        IEnumerable<QandA> processMonitor(object mon, int ix)
        {
            yield return makeQuestion(Question.ConnectedMonitorsNumber, module, questionSprite: ConnectedMonitorsSprites[ix],
                    correctAnswers: new[] { displays[ix].ToString() });
            var inds = indsProp.GetFrom(mon, validator: v => v.Count is > 3 or < 0 ? $"Bad indicator count {v.Count} (Monitor {ix})" : null);
            foreach (var q in inds.Cast<object>().Select((ind, indIx) =>
                    makeQuestion(inds.Count == 1 ? Question.ConnectedMonitorsSingleIndicator : Question.ConnectedMonitorsOrdinalIndicator, module, questionSprite: ConnectedMonitorsSprites[ix],
                    correctAnswers: new[] { (colorProp ??= GetProperty<object>(ind, "Color", isPublic: true)).GetFrom(ind, v => (int) v is < 0 or > 5 ? $"Bad indicator color {v} (Monitor {ix}) (Indicator {indIx})" : null).ToString() },
                    formatArgs: new[] { Ordinal(indIx + 1) })))
                yield return q;
        }
        addQuestions(module, monitors.Cast<object>().SelectMany(processMonitor));
    }
}