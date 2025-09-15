using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonServes
{
    [SouvenirQuestion("Who flashed {1} in course {2} of {0}?", ThreeColumns6Answers, "Riley", "Brandon", "Gabriel", "Veronica", "Wendy", "Kayle", Arguments = [QandA.Ordinal, "1", QandA.Ordinal, "2", QandA.Ordinal, "3"], ArgumentGroupSize = 2)]
    Flash,

    [SouvenirQuestion("Which item was not served in course {1} of {0}?", OneColumn4Answers, "Baked Batterys", "Bamboozling Waffles", "Big Boom Tortellini", "Blast Shrimps", "Blastwave Compote", "Bomb Brûlée", "Boolean Waffles", "Boom Lager Beer", "Caesar Salad", "Centurion Wings", "Colored Spare Ribs", "Cruelo Juice", "Defuse Juice", "Defuse au Chocolat", "Deto Bull", "Edgework Toast", "Forget Cocktail", "Forghetti Bombognese", "Indicator Tar Tar", "Morse Soup", "NATO Shrimps", "Not Ice Cream", "Omelette au Bombage", "Simon’s Special Mix", "Solve Cake", "Status Light Rolls", "Strike Pie", "Tasha’s Drink", "Ticking Timecakes", "Veggie Blast Plate", "Wire Shake", "Wire Spaghetti", Arguments = ["1", "2", "3"], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Food
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonServes", "Simon Serves", typeof(SSimonServes), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessSimonServes(ModuleData module)
    {
        // Constants
        var names = new[] { "Riley", "Brandon", "Gabriel", "Veronica", "Wendy", "Kayle" };
        var foodCourse = Ut.NewArray(
            new[] { "Cruelo Juice", "Defuse Juice", "Simon’s Special Mix", "Boom Lager Beer", "Forget Cocktail", "Wire Shake", "Deto Bull", "Tasha’s Drink" },
            new[] { "Caesar Salad", "Edgework Toast", "Ticking Timecakes", "Big Boom Tortellini", "Status Light Rolls", "Blast Shrimps", "Morse Soup", "Boolean Waffles" },
            new[] { "Forghetti Bombognese", "NATO Shrimps", "Wire Spaghetti", "Indicator Tar Tar", "Centurion Wings", "Colored Spare Ribs", "Omelette au Bombage", "Veggie Blast Plate" },
            new[] { "Strike Pie", "Blastwave Compote", "Not Ice Cream", "Defuse au Chocolat", "Solve Cake", "Baked Batterys", "Bamboozling Waffles", "Bomb Brûlée" });

        // Reflection
        var comp = GetComponent(module, "simonServesScript");
        var fldStage = GetIntField(comp, "stage");
        var fldFood = GetArrayField<int>(comp, "foods");
        var fldBlinkingOrder = GetArrayField<int>(comp, "blinkingOrder");

        // Variables
        var currentStage = fldStage.Get(min: -1, max: 5);
        var flashOrder = new string[][] { null, null, null, null };
        var foodDisplayed = new string[][] { null, null, null, null };

        while (module.Unsolved)
        {
            var stage = fldStage.Get(min: -1, max: 5);
            if (currentStage != stage)
            {
                currentStage = stage;
                if (stage is >= 0 and < 4)
                {
                    var flashes = fldBlinkingOrder.Get(expectedLength: 7, validator: i => i is < 0 or > 6 ? "expected range 0–6" : null);
                    flashOrder[stage] = flashes.Take(6).Select(flash => names[flash]).ToArray();
                    var food = fldFood.Get(expectedLength: 6, validator: i => i is < 0 or > 7 ? "expected range 0–7" : null);
                    foodDisplayed[stage] = food.Select(i => foodCourse[stage][i]).ToArray();
                }
            }
            yield return null;  // no ‘WaitForSeconds’ here to make absolutely sure we don’t miss a stage
        }

        var qs = new List<QandA>();
        for (var stage = 0; stage < 4; stage++)
        {
            for (var flashIx = 0; flashIx < 6; flashIx++)
                qs.Add(makeQuestion(Question.SimonServesFlash, module, formatArgs: new[] { Ordinal(flashIx + 1), (stage + 1).ToString() }, correctAnswers: new[] { flashOrder[stage][flashIx] }));
            qs.Add(makeQuestion(Question.SimonServesFood, module, formatArgs: new[] { (stage + 1).ToString() }, allAnswers: foodCourse[stage], correctAnswers: foodCourse[stage].Except(foodDisplayed[stage]).ToArray()));
        }
        addQuestions(module, qs);
    }
}