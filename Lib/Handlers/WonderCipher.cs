using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SWonderCipher
{
    [Question("What was the {1} character in the initial message in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0123456789BCDFGHJKLMNPQRSTVWXYZ?")]
    QInitialMessage,

    [Discriminator("the Wonder Cipher that had {0} as the {1} character", Arguments = ["0", QandA.Ordinal, "9", QandA.Ordinal, "B", QandA.Ordinal, "Z", QandA.Ordinal, "?", QandA.Ordinal], ArgumentGroupSize = 2)]
    DInitialMessage
}

public partial class SouvenirModule
{
    [Handler("WonderCipher", "Wonder Cipher", typeof(SWonderCipher), "Infusion")]
    [ManualQuestion("What was the initial message?")]
    private IEnumerator<SouvenirInstruction> ProcessWonderCipher(ModuleData module)
    {
        var comp = GetComponent(module, "WonderCipher");
        yield return WaitForSolve;

        var fldMessage = GetField<string>(comp, "givenText").Get();

        for (var i = 0; i < 20; i++)
        {
            yield return new Discriminator(SWonderCipher.DInitialMessage, $"initial{i}", fldMessage[i], args: [fldMessage[i].ToString(), Ordinal(i + 1)]);
            yield return question(SWonderCipher.QInitialMessage, args: [Ordinal(i + 1)]).AvoidDiscriminators($"initial{i}").Answers(fldMessage[i].ToString());
        }
    }
}
