using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDigisibility
{
    [SouvenirQuestion("What was the number on the {1} button in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 9)]
    DisplayedNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("digisibility", "Digisibility", typeof(SDigisibility), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessDigisibility(ModuleData module)
    {
        var comp = GetComponent(module, "digisibilityScript");
        yield return WaitForSolve;

        var displayedNums = GetField<int[][]>(comp, "Data").Get().First();

        var qs = new List<QandA>();
        for (var i = 0; i < 9; i++)
            qs.Add(makeQuestion(Question.DigisibilityDisplayedNumber, module,
                formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { displayedNums[i].ToString() },
                preferredWrongAnswers: displayedNums.Select(x => x.ToString()).ToArray()));
        addQuestions(module, qs);
    }
}