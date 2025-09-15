using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDimensionDisruption
{
    [SouvenirQuestion("Which of these was a visible character in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z0-9")]
    VisibleLetters
}

public partial class SouvenirModule
{
    [SouvenirHandler("dimensionDisruption", "Dimension Disruption", typeof(SDimensionDisruption), "Hawker")]
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
        addQuestion(module, Question.DimensionDisruptionVisibleLetters, correctAnswers: answers);
    }
}