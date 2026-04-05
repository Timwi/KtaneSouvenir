using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLEDs
{
    [Question("What was the initial color of the changed LED in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Black", "White", TranslateAnswers = true)]
    OriginalColor
}

public partial class SouvenirModule
{
    [Handler("leds", "LEDs", typeof(SLEDs), "tandyCake")]
    [ManualQuestion("What was the initial color of the changed LED?")]
    private IEnumerator<SouvenirInstruction> ProcessLEDs(ModuleData module)
    {
        var comp = GetComponent(module, "LEDsScript");
        yield return WaitForSolve;

        var fldInitColor = GetField<object>(comp, "colorChangedTo");
        var fldActualColor = GetField<object>(comp, "currentDisplayOnChanged");

        var initStr = fldInitColor.Get(col => (int) col is > 7 or < 0 ? "expected value 0-7" : null).ToString();
        var actualStr = fldActualColor.Get(col => (int) col is > 7 or < 0 ? "expected value 0-7" : null).ToString();

        yield return question(SLEDs.OriginalColor).Answers(initStr, preferredWrong: [actualStr]);
    }
}