using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDimensionDisruption
{
    [Question("Which of these was a visible character in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z0-9")]
    QVisibleLetters,

    [Discriminator("the Dimension Disruption where {0} was a visible character", Arguments = ["A"], ArgumentGroupSize = 1)]
    DVisibleLetters
}

public partial class SouvenirModule
{
    [Handler("dimensionDisruption", "Dimension Disruption", typeof(SDimensionDisruption), "Hawker")]
    [ManualQuestion("What were the visible characters?")]
    private IEnumerator<SouvenirInstruction> ProcessDimensionDisruption(ModuleData module)
    {
        var comp = GetComponent(module, "dimensionDisruptionScript");

        var letterIndex = new List<int>()
        {
            GetField<int>(comp, "letOne").Get(),
            GetField<int>(comp, "letTwo").Get(),
            GetField<int>(comp, "letThree").Get()
        };

        yield return WaitForSolve;

        var alphabet = GetField<string>(comp, "alphabet").Get();
        var answers = letterIndex.Select(li => alphabet[li].ToString()).ToArray();

        foreach (var ltr in answers)
            yield return new Discriminator(SDimensionDisruption.DVisibleLetters, $"letter-{ltr}", args: [ltr], avoidAnswers: [ltr]);

        yield return question(SDimensionDisruption.QVisibleLetters).Answers(answers);
    }
}