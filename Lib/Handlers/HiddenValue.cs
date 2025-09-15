using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHiddenValue
{
    [SouvenirQuestion("What was displayed on {0}?", TwoColumns4Answers, ExampleAnswers = ["Red 1", "Green 2", "White 3", "Yellow 4", "Magenta 5", "Cyan 6", "Purple 7"], TranslatableStrings = ["Red", "Green", "White", "Yellow", "Magenta", "Cyan", "Purple", "{0} {1}"])]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("theHiddenValue", "Hidden Value", typeof(SHiddenValue), "Anonymous", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessHiddenValue(ModuleData module)
    {
        var comp = GetComponent(module, "hiddenValue");
        var numbers = GetListField<int>(comp, "numbers")
            .Get(minLength: 4, maxLength: 6, validator: v => v is < 0 or > 9 ? "Out of range [0, 9]" : null)
            .ToArray(); // Make a copy so the module can't modify it
        var colors = GetListField<char>(comp, "numberColors")
            .Get(expectedLength: numbers.Length, validator: v => "RGWYMCP".Contains(v) ? null : "Not in “RGWYMCP”")
            .Select(c => "RGWYMCP".IndexOf(c))
            .ToArray();

        yield return WaitForSolve;

        var format = translateString(Question.HiddenValueDisplay, "{0} {1}");
        var colorNames = new[] { "Red", "Green", "White", "Yellow", "Magenta", "Cyan", "Purple" }
            .Select(s => translateString(Question.HiddenValueDisplay, s))
            .ToArray();
        var all = from i in Enumerable.Range(0, 10) from c in colorNames select string.Format(format, c, i);
        var correct = numbers.Select((n, i) => string.Format(format, colorNames[colors[i]], n)).ToArray();
        addQuestion(module, Question.HiddenValueDisplay, correctAnswers: correct, allAnswers: all.ToArray());
    }
}