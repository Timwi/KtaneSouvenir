using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using Souvenir.Reflection;
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
        for (var ix = 0; ix < monitors.Count; ix++)
        {
            yield return question(SConnectedMonitors.Number, questionSprite: ConnectedMonitorsSprites[ix]).Answers(displays[ix].ToString());
            var inds = indsProp.GetFrom(monitors[ix], validator: v => v.Count is > 3 or < 0 ? $"Bad indicator count {v.Count} (Monitor {ix})" : null);
            for (var indIx = 0; indIx < inds.Count; indIx++)
            {
                colorProp ??= GetProperty<object>(inds[indIx], "Color", isPublic: true);
                yield return question(
                        inds.Count == 1 ? SConnectedMonitors.SingleIndicator : SConnectedMonitors.OrdinalIndicator,
                        args: [Ordinal(indIx + 1)],
                        questionSprite: ConnectedMonitorsSprites[ix])
                    .Answers(colorProp.GetFrom(inds[indIx], v => (int) v is < 0 or > 5 ? $"Bad indicator color {v} (Monitor {ix}) (Indicator {indIx})" : null).ToString());
            }
        }
    }
}
