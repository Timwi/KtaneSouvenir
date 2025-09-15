using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLightBulbs
{
    [SouvenirQuestion("What was the color of the {1} bulb in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Cyan", "Magenta", TranslateAnswers = true, Arguments = ["left", "right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("LightBulbs", "Light Bulbs", typeof(SLightBulbs), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessLightBulbs(ModuleData module)
    {
        var comp = GetComponent(module, "LightBulbsScript");

        yield return WaitForSolve;

        var bulbs = GetField<IList>(comp, "Bulbs").Get(lst => lst.Count != 3 ? "expected length 3" : null);

        addQuestions(
            module,
            makeQuestion(Question.LightBulbsColors, module, formatArgs: new[] { "left" }, correctAnswers: new[] { GetField<Enum>(bulbs[0], "Color", isPublic: true).Get().ToString() }),
            makeQuestion(Question.LightBulbsColors, module, formatArgs: new[] { "right" }, correctAnswers: new[] { GetField<Enum>(bulbs[2], "Color", isPublic: true).Get().ToString() })
        );
    }
}