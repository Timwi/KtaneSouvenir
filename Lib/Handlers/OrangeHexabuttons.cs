using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SOrangeHexabuttons
{
    // Although Vigenère is technically how the cipher name is spelled, the module does not include the diacritic.
    [Question("What was the cipher used in {0}?", TwoColumns4Answers, "Atbash", "Caesar", "Condi", "Monoalphabetic", "Porta", "Ragbaby", "Vigenere")]
    QCipher
}

public partial class SouvenirModule
{
    [Handler("orangeHexabuttons", "Orange Hexabuttons", typeof(SOrangeHexabuttons), "Infusion")]
    [ManualQuestion("What was the cipher used?")]
    private IEnumerator<SouvenirInstruction> ProcessOrangeHexabuttons(ModuleData module)
    {
        var comp = GetComponent(module, "orangeHexabuttons");
        var message = GetArrayField<string>(comp, "voiceMessage").Get();
        string cipher = message[0][0] + message[0].Substring(1).ToLowerInvariant();

        yield return WaitForSolve;

        yield return question(SOrangeHexabuttons.QCipher).Answers(cipher);
    }
}
