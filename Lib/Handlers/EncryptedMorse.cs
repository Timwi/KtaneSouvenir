using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SEncryptedMorse
{
    [SouvenirQuestion("What was the {1} on {0}?", TwoColumns4Answers, ExampleAnswers = ["Detonate", "Ready Now", "Please No", "Cheesecake"], Arguments = ["received call", "sent response"], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    CallResponse
}

public partial class SouvenirModule
{
    [SouvenirHandler("EncryptedMorse", "Encrypted Morse", typeof(SEncryptedMorse), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessEncryptedMorse(ModuleData module)
    {
        var comp = GetComponent(module, "EncryptedMorseModule");

        string[] formatCalls = { "Detonate", "Ready Now", "We're Dead", "She Sells", "Remember", "Great Job", "Solo This", "Keep Talk" };
        string[] formatResponses = { "Please No", "Cheesecake", "Sadface", "Sea Shells", "Souvenir", "Thank You", "I Dare You", "No Explode" };
        var index = GetIntField(comp, "callResponseIndex").Get(0, Math.Min(formatCalls.Length - 1, formatResponses.Length - 1));

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.EncryptedMorseCallResponse, module, formatArgs: new[] { "received call" }, correctAnswers: new[] { formatCalls[index] }, preferredWrongAnswers: formatCalls),
            makeQuestion(Question.EncryptedMorseCallResponse, module, formatArgs: new[] { "sent response" }, correctAnswers: new[] { formatResponses[index] }, preferredWrongAnswers: formatResponses));
    }
}