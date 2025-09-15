using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCoffeebucks
{
    [SouvenirQuestion("What was the last served coffee in {0}?", OneColumn4Answers, "Twix Frappuccino", "The Blue Drink", "Matcha & Espresso Fusion", "Caramel Snickerdoodle Macchiato", "Liquid Cocaine", "S’mores Hot Chocolate", "The Pink Drink", "Grasshopper Frappuccino")]
    Coffee
}

public partial class SouvenirModule
{
    [SouvenirHandler("coffeebucks", "Coffeebucks", typeof(SCoffeebucks), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessCoffeebucks(ModuleData module)
    {
        var comp = GetComponent(module, "coffeebucksScript");

        yield return WaitForSolve;

        var coffees = GetArrayField<string>(comp, "coffeeOptions", isPublic: true).Get();
        var currCoffee = GetIntField(comp, "startCoffee").Get(min: 0, max: coffees.Length - 1);

        for (var i = 0; i < coffees.Length; i++)
            coffees[i] = coffees[i].Replace("\n", " ");

        yield return question(SCoffeebucks.Coffee).Answers(coffees[currCoffee], preferredWrong: coffees);
    }
}