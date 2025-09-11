using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SBakery
{
    [SouvenirQuestion("Which menu item was present in {0}?", OneColumn4Answers, ExampleAnswers = ["Butter slab", "Sugar cookie", "Applie pie", "Tea biscuit", "Tuile", "Sprinkles Cookie"])]
    Items
}

public partial class SouvenirModule
{
    [SouvenirHandler("bakery", "Bakery", typeof(SBakery), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessBakery(ModuleData module)
    {
        var comp = GetComponent(module, "bakery");
        yield return WaitForSolve;

        var cookieTypes = GetField<Array>(comp, "allCookieCategories").Get(validator: arr => arr.Length != 12 ? "expected length 12" : null).Cast<object>().Select(x => x.ToString()).ToArray();
        var cookieIndices = GetArrayField<int>(comp, "cookieIndices").Get(validator: arr => arr.Length != 12 ? "expected length 12" : null);
        var allNameArrays = new string[8][];
        var enumNames = new[] { "regular", "teaBiscuit", "chocolateButterBiscuit", "branded", "danishButter", "macaron", "notCookie", "seasonal" };
        var nameArrayNames = new[] { "regularCookieNames", "teaBiscuitNames", "chocolateButterBiscuitNames", "brandedNames", "danishButterCookieNames", "macaronNames", "notCookieNames", "seasonalCookieNames" };
        for (var i = 0; i < 8; i++)
            allNameArrays[i] = GetStaticField<string[]>(comp.GetType(), nameArrayNames[i]).Get();
        addQuestion(module, Question.BakeryItems,
            correctAnswers: Enumerable.Range(0, 12).Select(i => allNameArrays[Array.IndexOf(enumNames, cookieTypes[i])][cookieIndices[i]]).ToArray(),
            preferredWrongAnswers: allNameArrays.SelectMany(x => x).ToArray());
    }
}