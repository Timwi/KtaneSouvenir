using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SIngredients
{
    [SouvenirQuestion("Which ingredient was used in {0}?", TwoColumns4Answers, "Veal", "Beef", "Quail", "FiletMignon", "Crab", "Scallop", "Lobster", "Sole", "Eel", "SeaBass", "Mussel", "Cod", "Pumpkin", "Zucchini", "Onion", "Tomato", "Eggplant", "Carrot", "Garlic", "Celery", "Morel", "Porcini", "Chanterelle", "Portobello", "BlackTruffle", "KingOysterMushroom", "BlackTrumpet", "MillerMushroom", "Cloves", "Rosemary", "Thyme", "BayLeaf", "Basil", "Dill", "Parsley", "Saffron", "Apricot", "Gooseberry", "Lemon", "Orange", "Raspberry", "Pear", "Blackberry", "Apple", "Cheese", "Chocolate", "Caviar", "Butter", "OliveOil", "Cornichon", "Rice", "Honey", "SourCherry", "Strawberry", "BloodOrange", "Banana", "Grapes", "Melon", "Watermelon")]
    Ingredients,

    [SouvenirQuestion("Which ingredient was listed but not used in {0}?", TwoColumns4Answers, "Veal", "Beef", "Quail", "FiletMignon", "Crab", "Scallop", "Lobster", "Sole", "Eel", "SeaBass", "Mussel", "Cod", "Pumpkin", "Zucchini", "Onion", "Tomato", "Eggplant", "Carrot", "Garlic", "Celery", "Morel", "Porcini", "Chanterelle", "Portobello", "BlackTruffle", "KingOysterMushroom", "BlackTrumpet", "MillerMushroom", "Cloves", "Rosemary", "Thyme", "BayLeaf", "Basil", "Dill", "Parsley", "Saffron", "Apricot", "Gooseberry", "Lemon", "Orange", "Raspberry", "Pear", "Blackberry", "Apple", "Cheese", "Chocolate", "Caviar", "Butter", "OliveOil", "Cornichon", "Rice", "Honey", "SourCherry", "Strawberry", "BloodOrange", "Banana", "Grapes", "Melon", "Watermelon")]
    NonIngredients
}

public partial class SouvenirModule
{
    [SouvenirHandler("ingredients", "Ingredients", typeof(SIngredients), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessIngredients(ModuleData module)
    {
        var comp = GetComponent(module, "IngredientsScript");
        var initialIngredients = GetField<Array>(comp, "InitialIngredientsList").Get().Cast<object>().Select(ev => ev.ToString()).ToArray();
        yield return WaitForSolve;

        var unusedIngredients = GetField<IList>(comp, "CurrentIngredientsList").Get().Cast<object>().Select(ev => ev.ToString()).ToArray();
        var usedIngredients = initialIngredients.Except(unusedIngredients).ToArray();

        addQuestions(module,
            makeQuestion(Question.IngredientsIngredients, module, correctAnswers: usedIngredients, preferredWrongAnswers: unusedIngredients),
            makeQuestion(Question.IngredientsNonIngredients, module, correctAnswers: unusedIngredients, preferredWrongAnswers: usedIngredients));
    }
}