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

        yield return question(SMSeq.Obtained, args: ["first"]).Answers(obtainedDigits[0].ToString());
        yield return question(SMSeq.Obtained, args: ["second"]).Answers(obtainedDigits[1].ToString());
        yield return question(SMSeq.Obtained, args: ["third"]).Answers(obtainedDigits[2].ToString());
        yield return question(SMSeq.Submitted).Answers(submittedNum.ToString());
    }
}