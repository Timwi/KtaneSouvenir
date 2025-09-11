using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCalendar
{
    [SouvenirQuestion("What was the LED color in {0}?", TwoColumns4Answers, "Green", "Yellow", "Red", "Blue", TranslateAnswers = true)]
    LedColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("calendar", "Calendar", typeof(SCalendar), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessCalendar(ModuleData module)
    {
        var comp = GetComponent(module, "calendar");
        var fldLightsOn = GetField<bool>(comp, "_lightsOn");

        while (!fldLightsOn.Get())
            yield return new WaitForSeconds(.1f);

        var colorblindText = GetField<TextMesh>(comp, "colorblindText", isPublic: true).Get(v => v.text == null ? "text is null" : null);

        yield return WaitForSolve;

        addQuestion(module, Question.CalendarLedColor, correctAnswers: new[] { colorblindText.text });
    }
}