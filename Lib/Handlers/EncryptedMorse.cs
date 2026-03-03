using System;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SEncryptedMorse
{
    [SouvenirQuestion("What was the received key in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings("8*A-Z")]
    Key
}

public partial class SouvenirModule
{
    [SouvenirHandler("EncryptedMorse", "Encrypted Morse", typeof(SEncryptedMorse), "Espik")]
    private IEnumerator<SouvenirInstruction> ProcessEncryptedMorse(ModuleData module)
    {
        var comp = GetComponent(module, "EncryptedMorseModule");
        var key = GetField<string>(comp, "key").Get();

        yield return WaitForSolve;

        yield return question(SEncryptedMorse.Key).Answers(key);
    }
}
