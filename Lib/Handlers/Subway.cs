using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSubway
{
    [SouvenirQuestion("Which bread did the customer ask for in {0}?", OneColumn4Answers, "WHITE", "MULTIGRAIN", "GLUTEN FREE", "WHOLE WHEAT")]
    Bread,
    
    [SouvenirQuestion("Which of these was not asked for in {0}?", OneColumn4Answers, "TUNA", "CHICKEN", "TURKEY", "HAM", "PASTRAMI", "MYSTERY MEAT", "AMERICAN", "MOZZARELLA", "PROVOLONE", "SWISS", "CHEDDAR", "TOAST THE BREAD", "OLIVES", "LETTUCE", "PICKLES", "ONIONS", "TOMATOES", "JALAPENOS", "KETCHUP", "MAYONNAISE", "RANCH", "SALT", "PEPPER", "VINEGAR")]
    Items
}

public partial class SouvenirModule
{
    [SouvenirHandler("subway", "Subway", typeof(SSubway), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessSubway(ModuleData module)
    {
        var comp = GetComponent(module, "subwayScript");

        var ingredients = GetStaticField<string[][]>(comp.GetType(), "ingredients")
            .Get(v => v.Length != 5 ? "expected length 5" : null)
            // Replace newlines with space
            .Select(v => v.Select(w => w.Replace("\n", " ")).ToArray())
            .ToArray();

        var allBreads = ingredients[0];
        var allMeats = ingredients[1];
        var allCheeses = ingredients[2];
        var allVegetables = ingredients[3];
        var allCondiments = ingredients[4];

        yield return WaitForSolve;

        if (GetField<bool>(comp, "pizzaTime").Get())
            yield return legitimatelyNoQuestion(module, "The customer asked for pizza.");

        if (GetField<bool>(comp, "asMuch").Get())
            yield return legitimatelyNoQuestion(module, "You got fired.");

        var order = GetField<List<int>[]>(comp, "order").Get(v => v.Length != 5 ? "expected length 5" : v.Any(lst => lst == null) ? "a list within ‘order’ was null" : v[0].Count == 0 ? "expected an item in ‘order[0]’" : null);
        var orderedBreadIndex = order[0][0];
        var orderedMeatIndexes = order[1].ToList(); // Take a copy because we may be modifying it
        var orderedCheeseIndexes = order[2];
        var orderedVegetablesIndexes = order[3];
        var orderedCondimentsIndexes = order[4].ToList(); // Take a copy because we may be modifying it

        // If asked for tuna, remove mayo from condiment indices and add tuna to meat indices
        if (GetField<bool>(comp, "replaceTuna").Get())
        {
            orderedMeatIndexes.Add(0);
            orderedCondimentsIndexes.Remove(1);
        }

        var requestedItems = orderedMeatIndexes.Select(i => allMeats[i])
            .Concat(orderedCheeseIndexes.Select(i => allCheeses[i]))
            .Concat(orderedVegetablesIndexes.Select(i => allVegetables[i]))
            .Concat(orderedCondimentsIndexes.Select(i => allCondiments[i])).ToArray();

        addQuestions(module,
            makeQuestion(Question.SubwayBread, module, correctAnswers: new[] { allBreads[orderedBreadIndex] }),
            makeQuestion(Question.SubwayItems, module, correctAnswers: allMeats.Concat(allCheeses).Concat(allVegetables).Concat(allCondiments).Except(requestedItems).ToArray()));
    }
}