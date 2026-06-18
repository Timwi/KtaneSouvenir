using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SUnfairsRevenge
{
    [Question("What was the {1} letter of the encrypted message in {0}?", ThreeColumns6Answers, Type = AnswerType.DynamicFont, CharacterSize = 0.4f, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letters
}

public partial class SouvenirModule
{
    [Handler("unfairsRevenge", "Unfair’s Revenge", typeof(SUnfairsRevenge), "Espik")]
    [ManualQuestion("What was the encrypted message?")]
    private IEnumerator<SouvenirInstruction> ProcessUnfairsRevenge(ModuleData module)
    {
        var comp = GetComponent(module, "UnfairsRevengeHandler");
        yield return WaitForSolve;

        var message = GetField<string>(comp, "fullyEncryptedString").Get();

        // Get font from module
        var textMesh = GetField<TextMesh>(comp, "pigpenDisplay", isPublic: true).Get();
        var info = new TextAnswerInfo(textMesh.font, textMesh.GetComponent<MeshRenderer>().sharedMaterial.mainTexture);

        for (var i = 0; i < 12; i++)
            yield return question(SUnfairsRevenge.Letters, args: [Ordinal(i + 1)]).Answers(message[i].ToString(), info: info);
    }
}
