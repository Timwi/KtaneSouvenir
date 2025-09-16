using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SColouredCylinder
{
    [SouvenirQuestion("What was the {1} colour flashed on the cylinder in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "Magenta", "White", "Black", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colours
}

public partial class SouvenirModule
{
    [SouvenirHandler("colouredCylinder", "Coloured Cylinder", typeof(SColouredCylinder), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessColouredCylinder(ModuleData module)
    {
        yield return WaitForSolve;
        var comp = GetComponent(module, "colouredCylinder");
        // The module can theoretically generate an arbitrarily large sequence of colours
        var sequence = GetListField<int>(comp, "colourIndexes").Get(minLength: 6, validator: v => v is < 0 or > 6 ? "Out of range [0, 6]" : null);
        for (var i = 0; i < sequence.Length; i++)
            yield return question(SColouredCylinder.Colours, args: [Ordinal(i + 1)]).Answers(SColouredCylinder.Colours.GetAnswers()[sequence[i]]);
    }
}