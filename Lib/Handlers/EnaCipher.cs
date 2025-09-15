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

        addQuestions(module, makeQuestion(Question.EnaCipherKeywordAnswer, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { keywords[0] }, preferredWrongAnswers: allWords[keywords[0].Length - 3].ToArray()),
            makeQuestion(Question.EnaCipherKeywordAnswer, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { keywords[1] }, preferredWrongAnswers: allWords[keywords[1].Length - 3].ToArray()),
            makeQuestion(Question.EnaCipherExtAnswer, module, correctAnswers: new[] { extNumbers }),
            makeQuestion(Question.EnaCipherEncryptedAnswer, module, correctAnswers: new[] { encryptedWord }));
    }
}