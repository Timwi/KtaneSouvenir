using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SKookyKeypad
{
    [SouvenirQuestion("What color was the {1} button’s LED in {0}?", OneColumn4Answers, "Crimson", "Red", "Coral", "Orange", "Lemon Chiffon", "Medium Spring Green", "Deep Sea Green", "Cadet Blue", "Slate Blue", "Dark Magenta", "Unlit", Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true], TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("kookyKeypadModule", "Kooky Keypad", typeof(SKookyKeypad), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessKookyKeypad(ModuleData module)
    {
        yield return WaitForSolve;

        var colorMapping = new Dictionary<string, string>()
        {
            ["crimson"] = "Crimson",
            ["red"] = "Red",
            ["coral"] = "Coral",
            ["orange"] = "Orange",
            ["lemonchiffon"] = "Lemon Chiffon",
            ["mediumspringgreen"] = "Medium Spring Green",
            ["deepseagreen"] = "Deep Sea Green",
            ["cadetblue"] = "Cadet Blue",
            ["slateblue"] = "Slate Blue",
            ["darkmagenta"] = "Dark Magenta",
            ["unlit"] = "Unlit"
        };

        var comp = GetComponent(module, "KookyKeypadScript");
        var combos = GetArrayField<string[]>(comp, "colorcombos").Get(expectedLength: 15, validator: v => v.Length is not 4 ? "Expected length 4" : v.Any(i => !colorMapping.ContainsKey(i)) ? "Unexpected color" : null);
        var index = GetIntField(comp, "correctindex").Get(min: 0, max: 14);

        var formats = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };
        addQuestions(module, combos[index].Select((c, i) =>
            makeQuestion(SKookyKeypad.Color, module,
                correctAnswers: new[] { colorMapping[c] },
                formatArgs: new[] { formats[i] })));
    }
}