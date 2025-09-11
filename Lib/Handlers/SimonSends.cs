using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSimonSends
{
    [SouvenirQuestion("What was the {1} received letter in {0}?", ThreeColumns6Answers, TranslateFormatArgs = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
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

        yield return charR == "@" || charG == "@" || charB == "@"
            ? throw new AbandonModuleException($"Could not decode Morse code: {morseR} / {morseG} / {morseB}")
            : (YieldInstruction) WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.SimonSendsReceivedLetters, module, formatArgs: new[] { "red" }, correctAnswers: new[] { charR }, preferredWrongAnswers: new[] { charG, charB }),
            makeQuestion(Question.SimonSendsReceivedLetters, module, formatArgs: new[] { "green" }, correctAnswers: new[] { charG }, preferredWrongAnswers: new[] { charR, charB }),
            makeQuestion(Question.SimonSendsReceivedLetters, module, formatArgs: new[] { "blue" }, correctAnswers: new[] { charB }, preferredWrongAnswers: new[] { charR, charG }));
    }
}