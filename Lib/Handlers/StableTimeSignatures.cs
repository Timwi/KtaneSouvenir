using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SStableTimeSignatures
{
    [SouvenirQuestion("What was the {1} time signature in {0}?", ThreeColumns6Answers, "1/1", "2/1", "3/1", "4/1", "5/1", "6/1", "7/1", "8/1", "9/1", "1/2", "2/2", "3/2", "4/2", "5/2", "6/2", "7/2", "8/2", "9/2", "1/4", "2/4", "3/4", "4/4", "5/4", "6/4", "7/4", "8/4", "9/4", "1/8", "2/8", "3/8", "4/8", "5/8", "6/8", "7/8", "8/8", "9/8", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Signatures
}

public partial class SouvenirModule
{
    [SouvenirHandler("StableTimeSignatures", "Stable Time Signatures", typeof(SStableTimeSignatures), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessStableTimeSignatures(ModuleData module)
    {
        var comp = GetComponent(module, "StableTimeSignatures");
        yield return WaitForSolve;

        var topSequence = GetListField<string>(comp, "randomSequenceTop", isPublic: true)
            .Get(validator: l => l.All(s => !"123456789".Contains(s)) ? "Bad digit" : null);
        var bottomSequence = GetListField<string>(comp, "randomSequenceBottom", isPublic: true)
            .Get(expectedLength: topSequence.Count, validator: s => !"1248".Contains(s) ? "Bad digit" : null);
        var answers = Enumerable.Range(0, topSequence.Count).Select(i => $"{topSequence[i]}/{bottomSequence[i]}").ToArray();
        for (var i = 0; i < answers.Length; i++)
            yield return question(SStableTimeSignatures.Signatures, args: [Ordinal(i + 1)]).Answers(answers[i], preferredWrong: answers);
    }
}