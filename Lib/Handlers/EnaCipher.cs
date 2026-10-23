using System;
using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SEnaCipher
{
    [Question("What was the {1} keyword in {0}?", TwoColumns4Answers, ExampleAnswers = ["AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    KeywordAnswer,

    [Question("What was the transposition key in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(6, "123456")]
    ExtAnswer,

    [Question("What was the encrypted word in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    EncryptedAnswer,

    [Discriminator("the ƎNA Cipher that had {0} as the {1} keyword", Arguments = ["AMBUSH", QandA.Ordinal, "BANZAI", QandA.Ordinal, "BIGGER", QandA.Ordinal, "GAMBLE", QandA.Ordinal, "KETOSE", QandA.Ordinal, "OCULUS", QandA.Ordinal, "SCRAMS", QandA.Ordinal, "SENSOR", QandA.Ordinal, "YEANED", QandA.Ordinal, "YOUTHS", QandA.Ordinal], ArgumentGroupSize = 2)]
    DKeywordAnswer,

    [Discriminator("the ƎNA Cipher that had {0} as the transposition key", Arguments = ["123456", "234561", "345612", "456123", "561234", "432165", "321654", "216543", "165432", "654321"], ArgumentGroupSize = 1)]
    DExtAnswer,

    [Discriminator("the ƎNA Cipher that had {0} as the encrypted word", Arguments = ["AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS"], ArgumentGroupSize = 1)]
    DEncryptedAnswer
}

public partial class SouvenirModule
{
    [Handler("enaCipher", "ƎNA Cipher", typeof(SEnaCipher), "KiloBites")]
    [ManualQuestion("What were the keyword, the transposition key and the encrypted word?")]
    private IEnumerator<SouvenirInstruction> ProcessEnaCipher(ModuleData module)
    {
        var comp = GetComponent(module, "enaCipherScript");

        var encryptedWord = GetField<string>(comp, "encrypted").Get();
        var keywords = GetField<string[]>(comp, "keywords").Get();
        var extNumbers = GetField<int[]>(comp, "reversed").Get().JoinString();

        yield return WaitForSolve;

        var allWordsType = comp.GetType().Assembly.GetType("Words.Data") ?? throw new AbandonModuleException("I cannot find the Words.Data type.");
        var allWordsObj = Activator.CreateInstance(allWordsType);
        var allWords = GetArrayField<List<string>>(allWordsObj, "_allWords").Get(expectedLength: 6);

        yield return new Discriminator(SEnaCipher.DKeywordAnswer, $"keyword{0}", keywords[0], args: [keywords[0], Ordinal(1)]);
        yield return new Discriminator(SEnaCipher.DKeywordAnswer, $"keyword{1}", keywords[1], args: [keywords[1], Ordinal(2)]);
        yield return new Discriminator(SEnaCipher.DExtAnswer,$"ext", extNumbers, args: [extNumbers]);
        yield return new Discriminator(SEnaCipher.DEncryptedAnswer,$"encrypted", encryptedWord, args: [encryptedWord]);

        yield return question(SEnaCipher.KeywordAnswer, args: [Ordinal(1)]).AvoidDiscriminators($"keyword{0}").Answers(keywords[0], preferredWrong: allWords[keywords[0].Length - 3].ToArray());
        yield return question(SEnaCipher.KeywordAnswer, args: [Ordinal(2)]).AvoidDiscriminators($"keyword{1}").Answers(keywords[1], preferredWrong: allWords[keywords[1].Length - 3].ToArray());
        yield return question(SEnaCipher.ExtAnswer).AvoidDiscriminators(SEnaCipher.DExtAnswer).Answers(extNumbers);
        yield return question(SEnaCipher.EncryptedAnswer).AvoidDiscriminators(SEnaCipher.DEncryptedAnswer).Answers(encryptedWord);
    }
}
