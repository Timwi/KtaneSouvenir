using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLetteredHexabuttons
{
    [Question("What was the {1} digit heard in {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 5)]
    QNumber,

    [Discriminator("the Lettered Hexabuttons where the {0} digit heard was {1}", Arguments = [QandA.Ordinal, "1", QandA.Ordinal, "2", QandA.Ordinal, "3", QandA.Ordinal, "4", QandA.Ordinal, "5"], ArgumentGroupSize = 2)]
    DNumber
}

public partial class SouvenirModule
{
    [Handler("letteredHexabuttons", "Lettered Hexabuttons", typeof(SLetteredHexabuttons), "Infusion")]
    [ManualQuestion("What was the number heard?")]
    private IEnumerator<SouvenirInstruction> ProcessLetteredHexabuttons(ModuleData module)
    {
        var comp = GetComponent(module, "letteredHexabuttons");
        var message = GetArrayField<string>(comp, "voiceMessage").Get();

        yield return WaitForSolve;

        for (var i = 0; i < 5; i++)
        {
            yield return new Discriminator(SLetteredHexabuttons.DNumber, $"digit{i}", message[i], args: [Ordinal(i + 1), message[i]]);
            yield return question(SLetteredHexabuttons.QNumber, args: [Ordinal(i + 1)]).AvoidDiscriminators($"digit{i}").Answers(message[i]);
        }
    }
}
