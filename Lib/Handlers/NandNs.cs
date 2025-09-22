using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNandNs
{
    [SouvenirQuestion("Which label was present in the {1} stage of {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(5, 'M', 'N')]
    Label,

    [SouvenirQuestion("Which color was missing in the third stage of {0}?", ThreeColumns6Answers, "Red", "Green", "Orange", "Blue", "Yellow", "Brown", TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("NandNs", "N&Ns", typeof(SNandNs), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessNandNs(ModuleData module)
    {
        // No chance of missing the second stage, since the animation takes some time
        yield return WaitForActivate;

        var comp = GetComponent(module, "NandNs");
        var labels = new string[2][];
        var colors = new int[5];

        var fldStage = GetIntField(comp, "stage");
        var fldLabels = GetArrayField<string>(comp, "labels");
        var fldColors = GetArrayField<int>(comp, "buttonColors");
        int stage;

        static string validate(string v) => v.Any(static c => c is not 'N' and not 'M') ? "String must be only N’s and M’s" : null;

        do
        {
            yield return null;
            stage = fldStage.Get(min: 0, max: 5);
            if (stage == 1)
            {
                labels[0] = fldLabels.Get(expectedLength: 5, validator: validate).ToArray();
            }
            else if (stage == 2)
            {
                labels[1] = fldLabels.Get(expectedLength: 5, validator: validate).ToArray();
                colors = fldColors.Get(expectedLength: 5, validator: static v => v is < 0 or > 5 ? "Expected range 0–5" : null).ToArray();
            }
        }
        while (stage < 5);
        yield return WaitForSolve;

        yield return question(SNandNs.Label, args: [Ordinal(2)]).Answers(labels[0]);
        yield return question(SNandNs.Label, args: [Ordinal(3)]).Answers(labels[1]);
        yield return question(SNandNs.Color).Answers(SNandNs.Color.GetAnswers()[Enumerable.Range(0, 6).Except(colors).Single()]);
    }
}
