using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SUnfairsCruelRevenge
{
    [Question("What was the {1} letter of the encrypted message in {0}?", ThreeColumns6Answers, AnswerType = InfoType.DynamicFont, CharacterSize = 0.4f, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    Message,

    [Question("What digit corresponded to the {1} cipher used to encrypt the message in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0-9A-F")]
    CipherDigits
}

public partial class SouvenirModule
{
    [Handler("unfairsRevengeCruel", "Unfair’s Cruel Revenge", typeof(SUnfairsCruelRevenge), "Espik")]
    [ManualQuestion("What was the encrypted message?")]
    [ManualQuestion("What digits corresponded to the ciphers used?")]
    private IEnumerator<SouvenirInstruction> ProcessUnfairsCruelRevenge(ModuleData module)
    {
        var comp = GetComponent(module, "UnfairsCruelRevengeHandler");

        yield return WaitForActivate;
        yield return null; // Wait an extra frame to make sure we got all the information

        var isLegacyUCR = GetField<bool>(comp, "legacyUCR").Get();
        var isCustomUCR = GetField<bool>(comp, "customCruelRevenge").Get();

        var instructions = isLegacyUCR ? 6 : GetIntField(comp, "instructionsToGenerate").Get(min: 2, max: 12);
        var letterCount = instructions * 3;
        var displayLetterCount = letterCount + (letterCount / 13);

        var encMessageText = GetField<TextMesh>(comp, "pigpenDisplay", isPublic: true).Get();
        var fontInfo = new TextAnswerInfo(encMessageText.font, encMessageText.GetComponent<MeshRenderer>().sharedMaterial.mainTexture);

        // Wait until the display has finished animating
        while (encMessageText.text.Length != displayLetterCount)
            yield return null;

        var message = encMessageText.text.Replace("\n", "");

        yield return WaitForSolve;

        for (var i = 0; i < letterCount; i++)
            yield return question(SUnfairsCruelRevenge.Message, args: [Ordinal(i + 1)]).Answers(message[i].ToString(), info: fontInfo);

        if (!isLegacyUCR && !isCustomUCR)
        {
            var cipherDigits = GetField<string>(comp, "encodingDisplay").Get();

            for (var i = 0; i < cipherDigits.Length; i++)
                yield return question(SUnfairsCruelRevenge.CipherDigits, args: [Ordinal(i + 1)]).Answers(cipherDigits[i].ToString());
        }
    }
}
