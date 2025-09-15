using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSiloAuthorization
{
    [SouvenirQuestion("What was the message type in {0}?", TwoColumns4Answers, "Red-Alpha", "Yellow-Alpha", "Green-Alpha")]
    MessageType,

    [SouvenirQuestion("What was the {1} part of the encrypted message in {0}?", ThreeColumns6Answers, ExampleAnswers = ["A1B2", "BC84", "QW47", "B420", "AFS2", "FUN9"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(4, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")]
    EncryptedMessage,

    [SouvenirQuestion("What was the received authentication code in {0}?", ThreeColumns6Answers, ExampleAnswers = ["1234", "5678", "1357", "2468", "0001", "9999"])]
    [AnswerGenerator.Integers(0, 9999, "0000")]
    AuthCode
}

public partial class SouvenirModule
{
    [SouvenirHandler("siloAuthorization", "Silo Authorization", typeof(SSiloAuthorization), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSiloAuthorization(ModuleData module)
    {
        var comp = GetComponent(module, "WarGamesModuleScript");
        yield return WaitForSolve;

        var messageColor = GetField<object>(comp, "correctColor").Get();
        var colorNames = new[] { "Red-Alpha", "Yellow-Alpha", "Green-Alpha" };
        var correctColor = messageColor.ToString() == "Red" ? colorNames[0] : messageColor.ToString() == "Yellow" ? colorNames[1] : colorNames[2];
        yield return question(SSiloAuthorization.MessageType).Answers(correctColor, preferredWrong: colorNames);

        var outMessages = GetArrayField<string>(comp, "outMessages").Get();
        var messages = new[] { outMessages[0], outMessages[2] };
        for (var message = 0; message < 2; message++)
            yield return question(SSiloAuthorization.EncryptedMessage, args: [Ordinal(message + 1)]).Answers(messages[message], preferredWrong: messages);

        yield return question(SSiloAuthorization.AuthCode).Answers(GetField<int>(comp, "outAuthCode").Get().ToString("0000"));
    }
}