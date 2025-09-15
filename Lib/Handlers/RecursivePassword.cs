using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRecursivePassword
{
    [SouvenirQuestion("Which of these words appeared, but was not the password, in {0}?", ThreeColumns6Answers, ExampleAnswers = ["Abyss", "Ingot", "Nonce", "Whelk", "Obeys", "Lobed"])]
    NonPasswordWords,

    [SouvenirQuestion("What was the password in {0}?", ThreeColumns6Answers, ExampleAnswers = ["Abyss", "Ingot", "Nonce", "Whelk", "Obeys", "Lobed"])]
    Password
}

public partial class SouvenirModule
{
    [SouvenirHandler("RecursivePassword", "Recursive Password", typeof(SRecursivePassword), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessRecursivePassword(ModuleData module)
    {
        var comp = GetComponent(module, "RecursivePassword");

        yield return WaitForSolve;

        var wordList = GetArrayField<string>(comp, "WordList").Get(expectedLength: 52);
        var selectedWords = GetArrayField<int>(comp, "SelectedWords").Get(expectedLength: 5, validator: ix => ix < 0 || ix >= wordList.Length ? $"expected range 0-{wordList.Length - 1}" : null).Select(ix => wordList[ix]).ToArray();
        var password = wordList[GetIntField(comp, "Password").Get(min: 0, max: wordList.Length - 1)];

        addQuestions(
            module,
            makeQuestion(Question.RecursivePasswordNonPasswordWords, module, correctAnswers: selectedWords, preferredWrongAnswers: wordList),
            makeQuestion(Question.RecursivePasswordPassword, module, correctAnswers: new[] { password }, preferredWrongAnswers: selectedWords)
        );
    }
}