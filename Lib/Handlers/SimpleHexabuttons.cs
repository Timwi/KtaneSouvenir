using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimpleHexabuttons
{
    [Question("What was the number heard when pressing the center button in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 6)]
    QNumber,

    [Question("What was the letter heard when pressing the center button in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'F')]
    QLetter,

    [Discriminator("the Simple Hexabuttons where the number {0} was heard", Arguments = ["1", "2", "3", "4", "5", "6"], ArgumentGroupSize = 1)]
    DNumber,
    
    [Discriminator("the Simple Hexabuttons where the letter {0} was heard", Arguments = ["A", "B", "C", "D", "E", "F"], ArgumentGroupSize = 1)]
    DLetter
}

public partial class SouvenirModule
{
    [Handler("simpleHexabuttons", "Simple Hexabuttons", typeof(SSimpleHexabuttons), "Infusion")]
    [ManualQuestion("What was the number/letter heard when pressing the center button?")]
    private IEnumerator<SouvenirInstruction> ProcessSimpleHexabuttons(ModuleData module)
    {
        var comp = GetComponent(module, "simpleHexabuttons");
        var fldMessage = GetArrayField<string>(comp, "voiceMessage").Get(expectedLength: 2);

        yield return WaitForSolve;

        yield return new Discriminator(SSimpleHexabuttons.DNumber, "number", fldMessage[1], args: [fldMessage[0]]);
        yield return new Discriminator(SSimpleHexabuttons.DLetter, "letter", fldMessage[0], args: [fldMessage[1]]);
        yield return question(SSimpleHexabuttons.QNumber).AvoidDiscriminators("number").Answers(fldMessage[0]);
        yield return question(SSimpleHexabuttons.QLetter).AvoidDiscriminators("letter").Answers(fldMessage[1]);
    }
}
