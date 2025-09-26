using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum S7
{
    [SouvenirQuestion("What was the {1} channel’s initial value in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(-9, 9)]
    QInitialValues,

    [SouvenirQuestion("What LED color was shown in stage {1} of {0}?", TwoColumns4Answers, "red", "blue", "green", "white", Arguments = ["1", "2", "3"], ArgumentGroupSize = 1, TranslateAnswers = true)]
    QLedColors,

    [SouvenirDiscriminator("the 7 whose {0} channel’s initial value was {1}", Arguments = ["red", "-9", "green", "0", "blue", "9"], ArgumentGroupSize = 2, TranslateArguments = [true, false])]
    [AnswerGenerator.Integers(-9, 9)]
    DInitialValues,

    [SouvenirDiscriminator("the 7 whose stage {0} LED color was {1}", Arguments = ["1", "red", "2", "blue", "3", "green", "4", "white"], ArgumentGroupSize = 2, TranslateArguments = [false, true])]
    DLedColors,
}

public partial class SouvenirModule
{
    [SouvenirHandler("7", "7", typeof(S7), "Timwi")]
    private IEnumerator<SouvenirInstruction> Process7(ModuleData module)
    {
        var comp = GetComponent(module, "SevenHandler");
        yield return WaitForSolve;

        var allDisplayedValues = GetListField<int[]>(comp, "displayedValues")
            .Get(minLength: 0, validator: a => a.Length != 3 ? "expected length 3" : null);

        // Check if all of the stages have exactly 3 sets of values.
        var allIdxDisplayedOperators = GetListField<int>(comp, "idxOperations").Get(idx =>
            // Stage 0: initial values: expecting index -1
            idx[0] != -1 ? "Stage 0: expected -1" :
            // Stage 1 onwards: expecting 0–3
            !idx.Skip(1).All(a => a is >= 0 and <= 3) ? "After stage 0, expected range 0–3" :
            null);

        var colorNames = new[] { "red", "green", "blue", "white" };

        for (var channel = 0; channel < 3; channel++)
        {
            yield return new Discriminator(S7.DInitialValues, $"initial{channel}", allDisplayedValues[0][channel], args: [colorNames[channel], allDisplayedValues[0][channel].ToString()]);
            yield return question(S7.QInitialValues, args: [colorNames[channel]]).AvoidDiscriminators(S7.DInitialValues).Answers(allDisplayedValues[0][channel].ToString());
        }

        for (var stage = 1; stage < allDisplayedValues.Count; stage++)
        {
            yield return new Discriminator(S7.DLedColors, $"stage{stage}", allIdxDisplayedOperators[stage], args: [stage.ToString(), colorNames[allIdxDisplayedOperators[stage]]]);
            yield return question(S7.QLedColors, args: [stage.ToString()]).AvoidDiscriminators(S7.DLedColors).Answers(colorNames[allIdxDisplayedOperators[stage]]);
        }
    }
}
