using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SForgetsUltimateShowdown
{
    [SouvenirQuestion("What was the {1} digit of the answer in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Answer,

    [SouvenirQuestion("What was the {1} digit of the bottom number in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Bottom,

    [SouvenirQuestion("What was the {1} digit of the initial number in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    Initial,

    [SouvenirQuestion("What was the {1} method used in {0}?", OneColumn4Answers, "Forget Me Not", "Simon’s Stages", "Forget Me Later", "Forget Infinity", "A>N<D", "Forget Me Now", "Forget Everything", "Forget Us Not", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Method
}

public partial class SouvenirModule
{
    [SouvenirHandler("ForgetsUltimateShowdownModule", "Forget’s Ultimate Showdown", typeof(SForgetsUltimateShowdown), "Marksam")]
    private IEnumerator<SouvenirInstruction> ProcessForgetsUltimateShowdown(ModuleData module)
    {
        var comp = GetComponent(module, "ForgetsUltimateShowdownScript");
        var methods = GetField<IList>(comp, "_usedMethods").Get();

        yield return WaitForSolve;

        if (methods.Count != 4)
            throw new AbandonModuleException($"‘methods’ had an invalid length: {methods.Count}, expected 4");

        var answer = GetField<string>(comp, "_answer").Get();
        var initial = GetField<string>(comp, "_initialNumber").Get();
        var bottom = GetField<string>(comp, "_bottomNumber").Get();
        var methodNames = methods.Cast<object>().Select(x => GetProperty<string>(x, "Name", isPublic: true).Get()).ToList();
        for (var i = 0; i < 12; i++)
        {
            yield return question(SForgetsUltimateShowdown.Answer, args: [Ordinal(i + 1)]).Answers(answer[i].ToString());
            yield return question(SForgetsUltimateShowdown.Bottom, args: [Ordinal(i + 1)]).Answers(bottom[i].ToString());
            yield return question(SForgetsUltimateShowdown.Initial, args: [Ordinal(i + 1)]).Answers(initial[i].ToString());
        }
        for (var i = 0; i < 4; i++)
            yield return question(SForgetsUltimateShowdown.Method, args: [Ordinal(i + 1)]).Answers(methodNames[i].Replace("'", "’"));
    }
}