using System.Collections.Generic;
using System.Drawing;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCalendar
{
    [SouvenirQuestion("What was the holiday in {0}?", OneColumn4Answers, "April Fools’", "Australia Day", "Bastille Day", "Christmas Eve", "Cinco de Mayo", "Day of German Unity", "Day of the Dead", "Earth Day", "Epiphany", "Golden Week", "Groundhog Day", "Guy Fawkes Night", "Kwanzaa", "Republic Day", "Saint Patrick’s Day", "Valentine’s Day", "Veterans Day", "World Braille Day")]
    Holiday
}

public partial class SouvenirModule
{
    [SouvenirHandler("calendar", "Calendar", typeof(SCalendar), "Espik")]
    private IEnumerator<SouvenirInstruction> ProcessCalendar(ModuleData module)
    {
        var comp = GetComponent(module, "calendar");

        var allHolidays = SCalendar.Holiday.GetAnswers(); // We do not get the names directly from the module due to typos, wrong apostrophes, and various inconsistencies from the manual
        var holidayMonths = new Dictionary<int, int>(); // Used to note the months the holidays are in

        holidayMonths.Add(0, 3); // April Fools’ - April
        holidayMonths.Add(1, 0); // Australia Day - January
        holidayMonths.Add(2, 6); // Bastille Day - July
        holidayMonths.Add(3, 11); // Christmas Eve - December
        holidayMonths.Add(4, 4); // Cinco de Mayo - May
        holidayMonths.Add(5, 9); // Day of German Unity - October
        holidayMonths.Add(6, 9); // Day of the Dead - October
        holidayMonths.Add(7, 3); // Earth Day - April
        holidayMonths.Add(8, 0); // Epiphany - January
        holidayMonths.Add(10, 1); // Groundhog Day - Feburary
        holidayMonths.Add(11, 10); // Guy Fawkes Night - November
        holidayMonths.Add(13, 5); // Republic Day - June
        holidayMonths.Add(14, 2); // Saint Patrick’s Day - March
        holidayMonths.Add(15, 1); // Valentine’s Day - Feburary
        holidayMonths.Add(16, 10); // Veterans Day - November
        holidayMonths.Add(17, 0); // World Braille Day - January

        yield return WaitForSolve;

        var holiday = GetIntField(comp, "holiday").Get(min: 0, max: 17);

        // Checks if the holiday is in the submitted month. If it is, we don't ask a question
        var correctMonth = GetIntField(comp, "correctMonthIndex").Get(min: 0, max: 11);
        var valid = true;

        if (holiday == 9 && (correctMonth == 3 || correctMonth == 4)) // Golden Week - April & May
            valid = false;
        else if (holiday == 12 && (correctMonth == 11 || correctMonth == 0)) // Kwanzaa - December & January
            valid = false;
        else if (holidayMonths[holiday] == correctMonth) // Every other holiday
            valid = false;

        if (!valid)
            yield return legitimatelyNoQuestion(module, "The holiday is present in the submitted month.");

        yield return question(SCalendar.Holiday).Answers(allHolidays[holiday]);
    }
}
