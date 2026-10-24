using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBrownHexabuttons
{
    [Question("What was the {1} letter heard in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("ALCHEMY")]
    QString,

    [Discriminator("the Brown Hexabuttons where the {0} letter heard was {1}", Arguments = [QandA.Ordinal, "A", QandA.Ordinal, "L", QandA.Ordinal, "C", QandA.Ordinal, "H", QandA.Ordinal, "E", QandA.Ordinal, "M", QandA.Ordinal, "Y"], ArgumentGroupSize = 2)]
    DString
}

public partial class SouvenirModule
{
    [Handler("brownHexabuttons", "Brown Hexabuttons", typeof(SBrownHexabuttons), "Infusion")]
    [ManualQuestion("What was the code heard?")]
    private IEnumerator<SouvenirInstruction> ProcessBrownHexabuttons(ModuleData module)
    {
        var comp = GetComponent(module, "brownHexabuttons");
        var fldMessage = GetArrayField<string>(comp, "voiceMessage").Get(expectedLength: 6);

        yield return WaitForSolve;

        for (var i = 0; i < 6; i++)
        {
            yield return new Discriminator(SBrownHexabuttons.DString, $"code{i}", fldMessage[i], args: [Ordinal(i + 1), fldMessage[i]]);
            yield return question(SBrownHexabuttons.QString, args: [Ordinal(i + 1)]).AvoidDiscriminators($"code{i}").Answers(fldMessage[i]);
        }
    }
}
