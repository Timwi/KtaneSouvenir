using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCoffeeBeans
{
    [SouvenirQuestion("What was the {1} movement in {0}?", TwoColumns4Answers, "Horizontal", "Vertical", "Diagonal", "Nothing", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Movements
}

public partial class SouvenirModule
{
    [SouvenirHandler("coffeeBeans", "Coffee Beans", typeof(SCoffeeBeans), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessCoffeeBeans(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "coffeeBeansScript");
        var moves = GetListField<int>(comp, "moves").Get(minLength: 3, maxLength: 5, validator: v => v is < 0 or > 2 ? "Out of range [0, 2]" : null);
        var names = Question.CoffeeBeansMovements.GetAnswers();

        addQuestions(module, moves.Select((m, i) =>
            makeQuestion(Question.CoffeeBeansMovements, module,
                correctAnswers: new[] { names[m] },
                formatArgs: new[] { Ordinal(i + 1) })));
    }
}