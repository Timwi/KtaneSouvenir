using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSiloAuthorization
{
    [Question("What was the message type in {0}?", OneColumn3Answers, "Red-Alpha", "Yellow-Alpha", "Green-Alpha")]
    QMessageType,

    [Question("What was the {1} part of the encrypted message in {0}?", ThreeColumns6Answers, ExampleAnswers = ["A1B2", "BC84", "QW47", "B420", "AFS2", "FUN9"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(4, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")]
    QEncryptedMessage,

    [Question("What was the received authentication code in {0}?", ThreeColumns6Answers, ExampleAnswers = ["1234", "5678", "1357", "2468", "0001", "9999"])]
    [AnswerGenerator.Integers(0, 9999, "0000")]
    QAuthCode,

    [Discriminator("the Silo Authorization where the message type was {0}", Arguments = ["Red-Alpha", "Yellow-Alpha", "Green-Alpha"], ArgumentGroupSize = 1)]
    DMessageType,

    [Discriminator("the Silo Authorization where the {0} part of the encrypted message was {1}", Arguments = [QandA.Ordinal, "A1B2", QandA.Ordinal, "BC84", QandA.Ordinal, "QW47", QandA.Ordinal, "B420", QandA.Ordinal, "AFS2", QandA.Ordinal, "FUN9"], ArgumentGroupSize = 2)]
    DEncryptedMessage,

    [Discriminator("the Silo Authorization where the received authentication code was {0}", Arguments = ["1234", "5678", "1357", "2468", "0001", "9999"], ArgumentGroupSize = 1)]
    DAuthCode
}

public partial class SouvenirModule
{
    [Handler("siloAuthorization", "Silo Authorization", typeof(SSiloAuthorization), "Timwi")]
    [ManualQuestion("What were the message type, encrypted message, and received authentication code?")]
    private IEnumerator<SouvenirInstruction> ProcessSiloAuthorization(ModuleData module)
    {
        var comp = GetComponent(module, "WarGamesModuleScript");
        yield return WaitForSolve;

        var messageColor = GetField<object>(comp, "correctColor").Get();
        var colorNames = new[] { "Red-Alpha", "Yellow-Alpha", "Green-Alpha" };
        var correctColor = messageColor.ToString() == "Red" ? colorNames[0] : messageColor.ToString() == "Yellow" ? colorNames[1] : colorNames[2];
        yield return new Discriminator(SSiloAuthorization.DMessageType, "messagetype", correctColor, args: [correctColor]);
        yield return question(SSiloAuthorization.QMessageType).AvoidDiscriminators("messagetype").Answers(correctColor, preferredWrong: colorNames);

        var outMessages = GetArrayField<string>(comp, "outMessages").Get();
        var messages = new[] { outMessages[0], outMessages[2] };
        for (var message = 0; message < 2; message++)
        {
            yield return new Discriminator(SSiloAuthorization.DEncryptedMessage, $"message{message}", messages[message], args: [Ordinal(message + 1), messages[message]]);
            yield return question(SSiloAuthorization.QEncryptedMessage, args: [Ordinal(message + 1)]).AvoidDiscriminators($"message{message}").Answers(messages[message], preferredWrong: messages);
        }

        string authCode = GetField<int>(comp, "outAuthCode").Get().ToString("0000");

        yield return new Discriminator(SSiloAuthorization.DAuthCode, "code", authCode, args: [authCode]);
        yield return question(SSiloAuthorization.QAuthCode).AvoidDiscriminators("code").Answers(authCode);
    }
}
