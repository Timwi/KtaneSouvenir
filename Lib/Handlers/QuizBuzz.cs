using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SQuizBuzz
{
    [SouvenirQuestion("What was the number initially on the display in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(6, 74)]
    StartingNumber
}

public partial class SouvenirModule
{
    [SouvenirHandler("quizBuzz", "Quiz Buzz", typeof(SQuizBuzz), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessQuizBuzz(ModuleData module)
    {
        var comp = GetComponent(module, "quizBuzz");

        yield return WaitForSolve;

        var startingNumber = GetIntField(comp, "startNumber").Get(min: 6, max: 74);
        yield return question(SQuizBuzz.StartingNumber).Answers(startingNumber.ToString());
    }
}