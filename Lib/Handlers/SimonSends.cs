using System;
using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SSimonSends
{
    [SouvenirQuestion("What was the {1} received letter in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    ReceivedLetters
}

public partial class SouvenirModule
{
    [SouvenirHandler("SimonSendsModule", "Simon Sends", typeof(SSimonSends), "EternityShack")]
    private IEnumerator<SouvenirInstruction> ProcessSimonSends(ModuleData module)
    {
        string[] morse = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };

        var comp = GetComponent(module, "SimonSendsModule");
        var morseR = GetField<string>(comp, "_morseR").Get();
        var morseG = GetField<string>(comp, "_morseG").Get();
        var morseB = GetField<string>(comp, "_morseB").Get();
        var charR = ((char) ('A' + Array.IndexOf(morse, morseR.Replace("###", "-").Replace("#", ".").Replace("_", "")))).ToString();
        var charG = ((char) ('A' + Array.IndexOf(morse, morseG.Replace("###", "-").Replace("#", ".").Replace("_", "")))).ToString();
        var charB = ((char) ('A' + Array.IndexOf(morse, morseB.Replace("###", "-").Replace("#", ".").Replace("_", "")))).ToString();

        if (charR == "@" || charG == "@" || charB == "@")
            throw new AbandonModuleException($"Could not decode Morse code: {morseR} / {morseG} / {morseB}");

        yield return WaitForSolve;
        yield return question(SSimonSends.ReceivedLetters, args: ["red"]).Answers(charR, preferredWrong: [charG, charB]);
        yield return question(SSimonSends.ReceivedLetters, args: ["green"]).Answers(charG, preferredWrong: [charR, charB]);
        yield return question(SSimonSends.ReceivedLetters, args: ["blue"]).Answers(charB, preferredWrong: [charR, charG]);
    }
}
