using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCrazyHamburger
{
    [SouvenirQuestion("What was the {1} ingredient shown in {0}?", ThreeColumns6Answers, "Bread", "Cheese", "Grass", "Meat", "Oil", "Peppers", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Ingredient
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSCrazyHamburger", "Crazy Hamburger", typeof(SCrazyHamburger), "noting3548")]
    private IEnumerator<SouvenirInstruction> ProcessCrazyHamburger(ModuleData module)
    {
        var comp = GetComponent(module, "CrazyHamburgerScript");
        var fldIngredients = GetField<string>(comp, "Ingredients");

        yield return WaitForSolve;

        var dic = new Dictionary<char, string>()
        {
            ['B'] = "Bread",
            ['C'] = "Cheese",
            ['G'] = "Grass",
            ['H'] = "Meat",
            ['O'] = "Oil",
            ['R'] = "Peppers"
        };

        var ingredients = fldIngredients.Get(v => v.Any(ch => !dic.ContainsKey(ch)) ? $"expected only characters {dic.Keys.JoinString()}" : null);

        addQuestions(module, ingredients.Select((ing, i) =>
            makeQuestion(
                question: Question.CrazyHamburgerIngredient,
                data: module,
                formatArgs: new string[] { Ordinal(i + 1) },
                correctAnswers: new[] { dic[ing] })));
    }
}