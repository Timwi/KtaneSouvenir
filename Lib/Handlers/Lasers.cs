using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLasers
{
    [SouvenirQuestion("What was the number on the {1} hatch on {0}?", ThreeColumns6Answers, Arguments = ["top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Integers(1, 9)]
    Hatches
}

public partial class SouvenirModule
{
    [SouvenirHandler("lasers", "Lasers", typeof(SLasers), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessLasers(ModuleData module)
    {
        var comp = GetComponent(module, "LasersModule");
        yield return WaitForSolve;

        var laserOrder = GetListField<int>(comp, "_laserOrder").Get(expectedLength: 9);
        var hatchesPressed = GetListField<int>(comp, "_hatchesAlreadyPressed").Get(expectedLength: 7);
        var hatchNames = new[] { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        addQuestions(module, hatchesPressed.Select((hatch, ix) => makeQuestion(Question.LasersHatches, module, formatArgs: new[] { hatchNames[hatch] }, correctAnswers: new[] { laserOrder[hatch].ToString() }, preferredWrongAnswers: hatchesPressed.Select(number => laserOrder[number].ToString()).ToArray())));
    }
}