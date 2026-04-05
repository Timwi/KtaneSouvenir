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
    private IEnumerator<SouvenirInstruction> ProcessEncryptedMorse(ModuleData module)
    {
        var comp = GetComponent(module, "EncryptedMorseModule");
        var key = GetField<string>(comp, "key").Get();

        yield return WaitForSolve;

        yield return question(SEncryptedMorse.Key).Answers(key);
    }
}
