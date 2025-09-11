using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCodenames
{
    [SouvenirQuestion("Which of these words was submitted in {0}?", TwoColumns4Answers, ExampleAnswers = ["Hyperborean", "Weenus", "Melody", "King"])]
    Answers
}

public partial class SouvenirModule
{
    [SouvenirHandler("codenames", "Codenames", typeof(SCodenames), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessCodenames(ModuleData module)
    {
        var comp = GetComponent(module, "codenames");
        yield return WaitForSolve;

        var words = GetArrayField<string>(comp, "grid").Get(expectedLength: 25);
        var solution = GetArrayField<bool>(comp, "solution").Get(expectedLength: 25);
        var solutionWords = words.Where((w, i) => solution[i]).ToArray();
        addQuestion(module, Question.CodenamesAnswers, correctAnswers: solutionWords, preferredWrongAnswers: words.Where(x => !solutionWords.Contains(x)).ToArray());
    }
}