using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPurpleHexabuttons
{
    [Question("What was the {1} number heard when pressing the center hexabutton in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    QString,

    [Discriminator("the Purple Hexabuttons where the {0} number heard was {1}", Arguments = [QandA.Ordinal, "1", QandA.Ordinal, "2", QandA.Ordinal, "3", QandA.Ordinal, "4", QandA.Ordinal, "5", QandA.Ordinal, "6", QandA.Ordinal], ArgumentGroupSize = 2)]
    DString
}

public partial class SouvenirModule
{
    [Handler("purpleHexabuttons", "Purple Hexabuttons", typeof(SPurpleHexabuttons), "Infusion")]
    [ManualQuestion("What was the string of numbers heard when pressing the center hexabutton?")]
    private IEnumerator<SouvenirInstruction> ProcessPurpleHexabuttons(ModuleData module)
    {
        var comp = GetComponent(module, "purpleHexabuttons");
        var fldMessage = GetArrayField<string>(comp, "voiceMessage").Get(expectedLength: 6);

        yield return WaitForSolve;

        for (var i = 0; i < 6; i++)
        {
            yield return new Discriminator(SPurpleHexabuttons.DString, $"string{i}", fldMessage[i], args: [Ordinal(i + 1), fldMessage[i]]);
            yield return question(SPurpleHexabuttons.QString, args: [Ordinal(i + 1)]).AvoidDiscriminators($"string{i}").Answers(fldMessage[i]);
        }
    }
}
