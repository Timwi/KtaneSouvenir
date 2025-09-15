using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEnaCipher
{
    [SouvenirQuestion("What was the {1} keyword in {0}?", TwoColumns4Answers, ExampleAnswers = ["AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    KeywordAnswer,

    [SouvenirQuestion("What was the transposition key in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(6, "123456")]
    ExtAnswer,

    [SouvenirQuestion("What was the encrypted word in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    EncryptedAnswer
}

public partial class SouvenirModule
{
    [SouvenirHandler("enaCipher", "ƎNA Cipher", typeof(SEnaCipher), "KiloBites")]
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

        yield return question(SEnaCipher.KeywordAnswer, args: [Ordinal(1)]).Answers(keywords[0], preferredWrong: allWords[keywords[0].Length - 3].ToArray());
        yield return question(SEnaCipher.KeywordAnswer, args: [Ordinal(2)]).Answers(keywords[1], preferredWrong: allWords[keywords[1].Length - 3].ToArray());
        yield return question(SEnaCipher.ExtAnswer).Answers(extNumbers);
        yield return question(SEnaCipher.EncryptedAnswer).Answers(encryptedWord);
    }
}