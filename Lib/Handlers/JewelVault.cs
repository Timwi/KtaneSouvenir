using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SJewelVault
{
    [SouvenirQuestion("What number was wheel {1} in {0}?", TwoColumns4Answers, Arguments = ["A", "B", "C", "D"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 4)]
    Wheels
}

public partial class SouvenirModule
{
    [SouvenirHandler("jewelVault", "Jewel Vault", typeof(SJewelVault), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessJewelVault(ModuleData module)
    {
        var comp = GetComponent(module, "jewelWheelsScript");

        var wheels = GetArrayField<KMSelectable>(comp, "wheels", isPublic: true).Get(expectedLength: 4);
        var assignedWheels = GetListField<KMSelectable>(comp, "assignedWheels").Get(expectedLength: 4);

        yield return WaitForSolve;

        addQuestions(module, assignedWheels.Select((aw, ix) => makeQuestion(Question.JewelVaultWheels, module, formatArgs: new[] { "ABCD".Substring(ix, 1) }, correctAnswers: new[] { (Array.IndexOf(wheels, aw) + 1).ToString() })));
    }
}