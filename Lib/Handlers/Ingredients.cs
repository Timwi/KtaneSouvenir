using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SIngredients
{
    [Question("Which ingredient was listed in {0}?", TwoColumns4Answers, "Veal", "Beef", "Quail", "Filet Mignon", "Crab", "Scallop", "Lobster", "Sole", "Eel", "Sea Bass", "Mussel", "Cod", "Pumpkin", "Zucchini", "Onion", "Tomato", "Eggplant", "Carrot", "Garlic", "Celery", "Morel", "Porcini", "Chanterelle", "Portobello", "Black Truffle", "King Oyster Mushroom", "Black Trumpet", "Miller Mushroom", "Cloves", "Rosemary", "Thyme", "Bay Leaf", "Basil", "Dill", "Parsley", "Saffron", "Apricot", "Gooseberry", "Lemon", "Orange", "Raspberry", "Pear", "Blackberry", "Apple", "Cheese", "Chocolate", "Caviar", "Butter", "Olive Oil", "Cornichon", "Rice", "Honey", "Sour Cherry", "Strawberry", "Blood Orange", "Banana", "Grapes", "Melon", "Watermelon")]
    ListedIngredients,
}

public partial class SouvenirModule
{
    [Handler("ingredients", "Ingredients", typeof(SIngredients), "Quinn Wuest")]
    [ManualQuestion("Which ingredients were listed?")]
    private IEnumerator<SouvenirInstruction> ProcessIngredients(ModuleData module)
    {
        var comp = GetComponent(module, "IngredientsScript");
        var initialIngredients = GetField<Array>(comp, "InitialIngredientsList").Get().Cast<object>().Select(ToFriendlyString).ToArray();
        yield return WaitForSolve;

        yield return question(SIngredients.ListedIngredients).Answers(initialIngredients);
    }

    private string ToFriendlyString(object ingr)
    {
        return Regex.Replace(ingr.ToString(), "(.)([A-Z])", "$1 $2");
    }
}
