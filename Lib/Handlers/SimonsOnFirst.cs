using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SSimonsOnFirst
{
    [Question("Which colour flashed {1} in the final sequence in {0}?", TwoColumns4Answers, "Red", "Blue", "Light green", "Dark green", "Orange", "Pink", "Purple", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    FlashingColours
}

public partial class SouvenirModule
{
    [Handler("simonsOnFirst", "Simon’s On First", typeof(SSimonsOnFirst), "Timwi")]
    [ManualQuestion("Which colours flashed?")]
    private IEnumerator<SouvenirInstruction> ProcessSimonsOnFirst(ModuleData module)
    {
        var comp = GetComponent(module, "SimonsOnFirstScript");
        var buttonObjects = GetField<IList>(comp, "buttons", isPublic: true).Get(v => v.Count != 8 ? "expected length 8" : null);
        var fldColourValue = GetIntField(buttonObjects[0], "colourValue", isPublic: true);
        var buttonColors = Enumerable.Range(0, 8).Select(ix => fldColourValue.GetFrom(buttonObjects[ix], min: 0, max: 7)).ToArray();
        var flashingButtons = Enumerable.Range(1, 9).Select(i => GetIntField(comp, $"colour{i}").Get(min: 0, max: 7)).ToArray();
        var colorNames = new[] { "Red", "Blue", "Light green", "Dark green", "Orange", "Pink", "Purple", "Yellow" };
        var allFlashedColors = Enumerable.Range(0, 9).Select(ix => buttonColors[flashingButtons[ix]]).Distinct().Select(ix => colorNames[ix]).ToArray();
        yield return WaitForSolve;

        for (var ix = 0; ix < 9; ix++)
            yield return question(SSimonsOnFirst.FlashingColours, args: [Ordinal(ix + 1)]).Answers(colorNames[buttonColors[flashingButtons[ix]]], preferredWrong: allFlashedColors);
    }
}
