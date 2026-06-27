using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SXmORseCode
{
    [Question("What number was transmitted by the {1} displayed letter in {0}?", TwoColumns4Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 4)]
    FlashedNumbers
}

public partial class SouvenirModule
{
    [Handler("xmorse", "XmORse Code", typeof(SXmORseCode), "Espik")]
    [ManualQuestion("What numbers were transmitted?")]
    private IEnumerator<SouvenirInstruction> ProcessXmORseCode(ModuleData module)
    {
        var comp = GetComponent(module, "XmORseCode");

        yield return WaitForSolve;

        var answerIndex = GetIntField(comp, "answer").Get();
        var wordList = GetArrayField<string>(comp, "words").Get(expectedLength: 46);
        var alphabet = GetField<string>(comp, "alphabet").Get();
        var morseTable = GetArrayField<string>(comp, "morseTable").Get(expectedLength: 36);

        var morseToNumber = new Dictionary<string, string>()
        {
            ["....-"] = "1",
            ["...--"] = "2",
            ["..---"] = "3",
            [".----"] = "4",
        };

        for (var i = 0; i < 5; i++)
        {
            var foundMorseNumber = morseTable[morseTable[alphabet.IndexOf(wordList[answerIndex][i])].Length + 26];
            yield return question(SXmORseCode.FlashedNumbers, args: [Ordinal(i + 1)]).Answers(morseToNumber[foundMorseNumber]);
        }
    }
}
