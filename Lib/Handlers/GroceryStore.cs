using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGroceryStore
{
    [Question("What was the first item shown in {0}?", TwoColumns4Answers, ExampleAnswers = ["Cheese", "Coffee", "Flour", "Glass Cleaner", "Pepper", "Salt", "Soup", "Steak", "Toilet Paper", "Turkey"])]
    FirstItem
}

public partial class SouvenirModule
{
    [Handler("groceryStore", "Grocery Store", typeof(SGroceryStore), "BigCrunch22")]
    [ManualQuestion("What was the first item shown?")]
    private IEnumerator<SouvenirInstruction> ProcessGroceryStore(ModuleData module)
    {
        var comp = GetComponent(module, "GroceryStoreBehav");
        var display = GetField<TextMesh>(comp, "displayTxt", isPublic: true);
        var items = GetField<Dictionary<string, float>>(comp, "itemPrices").Get().Keys.ToArray();

        var finalAnswer = display.Get().text;

        module.Module.OnStrike += delegate { finalAnswer = display.Get().text; return false; };

        yield return WaitForSolve;
        yield return question(SGroceryStore.FirstItem).Answers(finalAnswer, preferredWrong: items);
    }
}