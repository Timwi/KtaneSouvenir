using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SMssngvWls
{
    [Question("Which vowel was missing in {0}?", TwoColumns4Answers, "A", "E", "I", "O", "U", Arguments = ["AEIOU"], ArgumentGroupSize = 1, TranslateArguments = [true], ReferenceDocumentation = true)]
    [MssNgvWlsGimmick]
    MssNgvwL
}

public partial class SouvenirModule
{
    [Handler("MssngvWls", "Mssngv Wls", typeof(SMssngvWls), "Anonymous")]
    [ManualQuestion("What was the missing vowel?")]
    private IEnumerator<SouvenirInstruction> ProcessMssngvWls(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "MssngvWls");
        var missingVowel = GetIntField(comp, "ForbiddenNumber").Get(0, 4);

        yield return question(SMssngvWls.MssNgvwL, args: ["AEIOU"]).Answers("AEIOU"[missingVowel].ToString());

        GetField<TextMesh>(comp, "Text", true).Get().text = "";
        GetField<KMSelectable>(comp, "CycleButton", true).Get().OnInteract = () => false;
    }
}
