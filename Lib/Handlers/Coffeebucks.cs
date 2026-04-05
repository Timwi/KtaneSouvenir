using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCoffeebucks
{
    [Question("What was the last customer’s preferred sugar content in {0}?", OneColumn4Answers, "Sugar is murder", "Just a bit", "Loads", "Diabetic in-waiting")]
    Sugar,

    [Question("What was the last customer’s preferred time of day in {0}?", TwoColumns4Answers, "Morning", "Lunchtime", "Afternoon", "Evening")]
    Time,

    [Question("What was the last customer’s preferred stress-level in {0}?", TwoColumns4Answers, "Calm", "Agitated", "Stressed", "Murderous")]
    Stress,

    [Question("What was the last customer’s preferred size in {0}?", TwoColumns4Answers, "Short", "Tall", "Grande", "Venti")]
    Size
}

public partial class SouvenirModule
{
    [Handler("coffeebucks", "Coffeebucks", typeof(SCoffeebucks), "Espik")]
    [ManualQuestion("What was the last customer’s preferred sugar content, time of day, stress level, and size?")]
    private IEnumerator<SouvenirInstruction> ProcessCoffeebucks(ModuleData module)
    {
        var comp = GetComponent(module, "coffeebucksScript");

        yield return WaitForSolve;

        var allPreferences = GetArrayField<string>(comp, "selectedPreferences", isPublic: true).Get(expectedLength: 4);

        // These are split into separate questions due to the Sugar question needing a different answer layout
        yield return question(SCoffeebucks.Sugar).Answers(allPreferences[0].Replace("\n", " "));
        yield return question(SCoffeebucks.Time).Answers(allPreferences[1]);
        yield return question(SCoffeebucks.Stress).Answers(allPreferences[2]);
        yield return question(SCoffeebucks.Size).Answers(allPreferences[3]);
    }
}
