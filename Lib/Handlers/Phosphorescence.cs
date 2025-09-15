using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPhosphorescence
{
    [SouvenirQuestion("What was the {1} button press in {0}?", ThreeColumns6Answers, ["Azure", "Blue", "Crimson", "Diamond", "Emerald", "Fuchsia", "Green", "Hazel", "Ice", "Jade", "Kiwi", "Lime", "Magenta", "Navy", "Orange", "Purple", "Quartz", "Red", "Salmon", "Tan", "Ube", "Vibe", "White", "Xotic", "Yellow", "Zen"], TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ButtonPresses,

    [SouvenirQuestion("What was the offset in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 419)]
    Offset
}

public partial class SouvenirModule
{
    [SouvenirHandler("Phosphorescence", "Phosphorescence", typeof(SPhosphorescence), "Emik")]
    private IEnumerator<SouvenirInstruction> ProcessPhosphorescence(ModuleData module)
    {
        var comp = GetComponent(module, "PhosphorescenceScript");
        var init = GetField<object>(comp, "init").Get();
        yield return WaitForSolve;

        var index = GetIntField(init, "index").Get(min: 0, max: 419);
        var buttonPresses = GetField<Array>(init, "buttonPresses").Get(ar =>
            ar.Length is < 3 or > 6 ? "expected length 3–6" :
            ar.OfType<object>().Any(v => !SPhosphorescence.ButtonPresses.GetAnswers().Contains(v.ToString())) ? "contains unknown color" : null);
        yield return question(SPhosphorescence.Offset).Answers(index.ToString());

        for (var i = 0; i < buttonPresses.GetLength(0); i++)
            yield return question(SPhosphorescence.ButtonPresses, args: [Ordinal(i + 1)]).Answers(buttonPresses.GetValue(i).ToString());
    }
}