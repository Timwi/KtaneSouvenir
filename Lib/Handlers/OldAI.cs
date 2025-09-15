using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SOldAI
{
    [SouvenirQuestion("What was the {1} of the numbers shown in {0}?", TwoColumns4Answers, "1", "2", "3", "4", "5", Arguments = ["group", "sub-group"], TranslateArguments = [true], ArgumentGroupSize = 1)]
    Group
}

public partial class SouvenirModule
{
    [SouvenirHandler("SCP079", "Old AI", typeof(SOldAI), "noting3548")]
    private IEnumerator<SouvenirInstruction> ProcessOldAI(ModuleData module)
    {
        var comp = GetComponent(module, "SCP079");
        var fldSeed = GetField<int>(comp, "Seed");

        yield return WaitForSolve;

        var seed = fldSeed.Get();
        addQuestions(module,
            makeQuestion(Question.OldAIGroup, module, formatArgs: new[] { "group" }, correctAnswers: new[] { ((seed - 1) / 5 + 1).ToString() }),
            makeQuestion(Question.OldAIGroup, module, formatArgs: new[] { "sub-group" }, correctAnswers: new[] { ((seed - 1) % 5 + 1).ToString() }));
    }
}