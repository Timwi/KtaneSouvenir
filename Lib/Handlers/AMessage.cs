using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAMessage
{
    [SouvenirQuestion("What was the initial message in {0}?", TwoColumns4Answers, Type = AnswerType.AMessageFont, FontSize = 560, CharacterSize = 0.125f)]
    [AnswerGenerator.AMessage]
    AMessage
}

public partial class SouvenirModule
{
    [SouvenirHandler("AMessage", "A Message", typeof(SAMessage), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessAMessage(ModuleData module)
    {
        yield return WaitForSolve;
        var comp = GetComponent(module, "AMessageScriptRedone");
        var data = GetArrayField<int>(comp, "SequenceNumbers").Get(expectedLength: 5, validator: v => v is < 0 or > 31 ? "Out of range [0, 31]" : null);
        var sol = GetArrayField<int>(comp, "RealNumbers").Get(expectedLength: 5, validator: v => v is < 0 or > 31 ? "Out of range [0, 31]" : null);
        static string convert(int[] nums) => new(nums.Select(i => (char) ('\ue900' + i)).ToArray());
        yield return question(SAMessage.AMessage).Answers(convert(data), preferredWrong: [convert(sol)]);
    }
}