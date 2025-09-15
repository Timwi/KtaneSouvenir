using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STimeSignatures
{
    [SouvenirQuestion("What was the {1} time signature in {0}?", ThreeColumns6Answers, "1/1", "2/1", "3/1", "4/1", "5/1", "6/1", "7/1", "8/1", "9/1", "1/2", "2/2", "3/2", "4/2", "5/2", "6/2", "7/2", "8/2", "9/2", "1/4", "2/4", "3/4", "4/4", "5/4", "6/4", "7/4", "8/4", "9/4", "1/8", "2/8", "3/8", "4/8", "5/8", "6/8", "7/8", "8/8", "9/8", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Signatures
}

public partial class SouvenirModule
{
    [SouvenirHandler("timeSignatures", "Time Signatures", typeof(STimeSignatures), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessTimeSignatures(ModuleData module)
    {
        var comp = GetComponent(module, "TimeSigModule");
        yield return WaitForSolve;

        var sequence = GetArrayField<string>(comp, "randomSequence")
            .Get(expectedLength: 5, validator: s => s.Length != 2 ? "Bad length" : !"123456789".Contains(s[0]) ? "Bad top digit" : !"1248".Contains(s[1]) ? "Bad bottom digit" : null);
        var answers = sequence.Select(s => $"{s[0]}/{s[1]}").ToArray();
        addQuestions(module, sequence.Select((s, i) => makeQuestion(Question.TimeSignaturesSignatures, module,
            formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { answers[i] }, preferredWrongAnswers: answers)));
    }
}