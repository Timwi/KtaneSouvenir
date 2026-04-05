using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SOldAI
{
    [Question("What was the {1} of the numbers shown in {0}?", TwoColumns4Answers, "1", "2", "3", "4", "5", Arguments = ["group", "sub-group"], TranslateArguments = [true], ArgumentGroupSize = 1)]
    Group
}

public partial class SouvenirModule
{
    [Handler("SCP079", "Old AI", typeof(SOldAI), "noting3548")]
    [ManualQuestion("What was the group/sub-group of the displayed numbers?")]
    private IEnumerator<SouvenirInstruction> ProcessOldAI(ModuleData module)
    {
        var comp = GetComponent(module, "SCP079");
        var fldSeed = GetField<int>(comp, "Seed");

        yield return WaitForSolve;

        var seed = fldSeed.Get();
        yield return question(SOldAI.Group, args: ["group"]).Answers(((seed - 1) / 5 + 1).ToString());
        yield return question(SOldAI.Group, args: ["sub-group"]).Answers(((seed - 1) % 5 + 1).ToString());
    }
}
