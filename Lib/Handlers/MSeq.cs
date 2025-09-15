using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMSeq
{
    [SouvenirQuestion("What was the {1} obtained digit in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 9)]
    Obtained,

    [SouvenirQuestion("What was the final number from the iteration process in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(25, 225)]
    Submitted
}

public partial class SouvenirModule
{
    [SouvenirHandler("mSeq", "M-Seq", typeof(SMSeq), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessMSeq(ModuleData module)
    {
        var comp = GetComponent(module, "MSeqScript");
        yield return WaitForSolve;

        var obtainedDigits = GetArrayField<int>(comp, "obtainedDigits").Get(expectedLength: 3);
        var submittedNum = GetIntField(comp, "finalNumber").Get(min: 25, max: 225);

        addQuestions(module,
            makeQuestion(Question.MSeqObtained, module, correctAnswers: new[] { obtainedDigits[0].ToString() }, formatArgs: new[] { "first" }),
            makeQuestion(Question.MSeqObtained, module, correctAnswers: new[] { obtainedDigits[1].ToString() }, formatArgs: new[] { "second" }),
            makeQuestion(Question.MSeqObtained, module, correctAnswers: new[] { obtainedDigits[2].ToString() }, formatArgs: new[] { "third" }),
            makeQuestion(Question.MSeqSubmitted, module, correctAnswers: new[] { submittedNum.ToString() }));
    }
}