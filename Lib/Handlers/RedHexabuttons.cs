using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SRedHexabuttons
{
    [Question("What was the letter heard when pressing the center button in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    QLetter,

    [Question("What was the number heard when pressing the center button in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 6)]
    QNumber,
    
    [Discriminator("the Red Hexabuttons where the letter {0} was heard", Arguments = ["A", "B", "C", "D", "E", "F"], ArgumentGroupSize = 1)]
    DLetter,

    [Discriminator("the Red Hexabuttons where the number {0} was heard", Arguments = ["1", "2", "3", "4", "5", "6"], ArgumentGroupSize = 1)]
    DNumber
}

public partial class SouvenirModule
{
    [Handler("redHexabuttons", "Red Hexabuttons", typeof(SRedHexabuttons), "Infusion")]
    [ManualQuestion("What was the letter/number heard when pressing the center button?")]
    private IEnumerator<SouvenirInstruction> ProcessRedHexabuttons(ModuleData module)
    {
        var comp = GetComponent(module, "redHexabuttons");
        var fldMessage = GetArrayField<string>(comp, "voiceMessage").Get(expectedLength: 2);

        yield return WaitForSolve;

        yield return new Discriminator(SRedHexabuttons.DLetter, "letter", fldMessage[0], args: [fldMessage[0]]);
        yield return new Discriminator(SRedHexabuttons.DNumber, "number", fldMessage[1], args: [fldMessage[1]]);
        yield return question(SRedHexabuttons.QLetter).AvoidDiscriminators("letter").Answers(fldMessage[0]);
        yield return question(SRedHexabuttons.QNumber).AvoidDiscriminators("number").Answers(fldMessage[1]);
    }
}
