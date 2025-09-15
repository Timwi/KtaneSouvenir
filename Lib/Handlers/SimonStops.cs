using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonStops
{
    [SouvenirQuestion("Which color flashed {1} in the output sequence in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Violet", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonStops", "Simon Stops", typeof(SSimonStops), "JerryEris")]
    private IEnumerator<SouvenirInstruction> ProcessSimonStops(ModuleData module)
    {
        var comp = GetComponent(module, "SimonStops");
        yield return WaitForSolve;

        var colors = GetArrayField<string>(comp, "outputSequence").Get(expectedLength: 5);
        addQuestions(module, colors.Select((color, ix) =>
             makeQuestion(Question.SimonStopsColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { color }, preferredWrongAnswers: colors)));
    }
}