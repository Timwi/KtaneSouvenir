using System;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SEncryptedMorse
{
    [Question("What was the received key in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings("8*A-Z")]
    Key
}

public partial class SouvenirModule
{
    [Handler("EncryptedMorse", "Encrypted Morse", typeof(SEncryptedMorse), "Espik")]
    [ManualQuestion("What was the received key?")]
    [NoDiscriminator]   // We have to ask about the key in full since the module's answer can be deduced with only some of the characters
    private IEnumerator<SouvenirInstruction> ProcessEncryptedMorse(ModuleData module)
    {
        var comp = GetComponent(module, "EncryptedMorseModule");
        var key = GetField<string>(comp, "key").Get();

        yield return WaitForSolve;

        yield return question(SEncryptedMorse.Key).Answers(key);
    }
}
