using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STripleTerm
{
    [SouvenirQuestion("Which of these was one of the passwords in {0}?", ThreeColumns6Answers, ExampleAnswers = ["Three", "Every", "These", "Would", "Where", "First", "Still", "Plant", "Small"])]
    Passwords
}

public partial class SouvenirModule
{
    [SouvenirHandler("tripleTermModule", "Triple Term", typeof(STripleTerm), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessTripleTerm(ModuleData module)
    {
        var comp = GetComponent(module, "TripleTermScript");
        yield return WaitForSolve;

        var wordList = GetArrayField<string>(comp, "wordList").Get().Select(i => i.Substring(0, 1).ToUpperInvariant() + i.Substring(1).ToLowerInvariant()).ToArray();
        var chosenWords = GetArrayField<int>(comp, "chosenWords").Get().Select(i => wordList[i]).ToArray();

        addQuestion(module, Question.TripleTermPasswords, correctAnswers: chosenWords, preferredWrongAnswers: wordList);
    }
}