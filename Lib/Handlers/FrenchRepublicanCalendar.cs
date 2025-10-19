using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SFrenchRepublicanCalendar
{
    [SouvenirQuestion("What was the color of the LED in {0}?", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", TranslateAnswers = true)]
    LEDColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("FrenchRepublicanCalendar", "French Republican Calendar", typeof(SFrenchRepublicanCalendar), "KiloBites", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessFrenchRepublicanCalendar(ModuleData module)
    {
        var comp = GetComponent(module, "FrenchRepublicanCalendarScript");

        yield return WaitForSolve;

        var ledIx = GetIntField(comp, "ledIndex", isPublic: true).Get(min: 0, max: 3);
        var colorNames = new[] { "Red", "Yellow", "Green", "Blue" };
        yield return question(SFrenchRepublicanCalendar.LEDColor).Answers(colorNames[ledIx]);
    }
}
