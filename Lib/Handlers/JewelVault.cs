using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SJewelVault
{
    [Question("Which wheel turned as a result of turning wheel {1} in {0}?", TwoColumns4Answers, "1", "2", "3", "4", "none", Arguments = ["1", "2", "3", "4"], ArgumentGroupSize = 1, TranslateAnswers = true)]
    WheelTurns
}

public partial class SouvenirModule
{
    [Handler("jewelVault", "Jewel Vault", typeof(SJewelVault), "Quinn Wuest", AddThe = true)]
    [ManualQuestion("Which wheel spun another wheel, and which one did it spin?")]
    private IEnumerator<SouvenirInstruction> ProcessJewelVault(ModuleData module)
    {
        var comp = GetComponent(module, "jewelWheelsScript");

        var wheels = GetArrayField<KMSelectable>(comp, "wheels", isPublic: true).Get(expectedLength: 4);
        var assignedWheels = GetListField<KMSelectable>(comp, "assignedWheels").Get(expectedLength: 4);

        yield return WaitForSolve;

        for (var ix = 0; ix < assignedWheels.Count; ix++)
        {
            var wheelIx = assignedWheels.IndexOf(wheels[ix]);
            string answerStr = wheelIx == 0 ? "none" : (Array.IndexOf(wheels, assignedWheels[wheelIx - 1]) + 1).ToString();
            yield return question(SJewelVault.WheelTurns, args: [(ix + 1).ToString()]).Answers(answerStr, all: SJewelVault.WheelTurns.GetAnswers().Where(a => a != (ix + 1).ToString()).ToArray());
        }
    }
}
