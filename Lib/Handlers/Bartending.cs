using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBartending
{
    [SouvenirQuestion("Which ingredient was in the {1} position on {0}?", TwoColumns4Answers, "Adelhyde", "Flanergide", "Bronson Extract", "Karmotrine", "Powdered Delta", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Ingredients
}

public partial class SouvenirModule
{
    [SouvenirHandler("BartendingModule", "Bartending", typeof(SBartending), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessBartending(ModuleData module)
    {
        var comp = GetComponent(module, "Maker");
        var fldIngredientIxs = GetArrayField<int>(comp, "ingIndices");

        yield return WaitForSolve;

        var ingIxs = fldIngredientIxs.Get(expectedLength: 5, validator: ing => ing is < 0 or > 4 ? "expected 0–4" : null);
        var ingredientNames = new[] { "Powdered Delta", "Flanergide", "Adelhyde", "Bronson Extract", "Karmotrine" };
        addQuestions(module, ingIxs.Select((ingIx, pos) => makeQuestion(Question.BartendingIngredients, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { ingredientNames[ingIx] })));
    }
}