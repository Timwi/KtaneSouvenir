using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCodenames
{
    [Question("Which of these words was submitted in {0}?", TwoColumns4Answers, ExampleAnswers = ["Hyperborean", "Weenus", "Melody", "King"])]
    Answers
}

public partial class SouvenirModule
{
    [Handler("codenames", "Codenames", typeof(SCodenames), "TasThiluna")]
    [ManualQuestion("Which words were submitted?")]
    private IEnumerator<SouvenirInstruction> ProcessCodenames(ModuleData module)
    {
        var comp = GetComponent(module, "codenames");
        yield return WaitForSolve;

        var words = GetArrayField<string>(comp, "grid").Get(expectedLength: 25);
        var solution = GetArrayField<bool>(comp, "solution").Get(expectedLength: 25);
        var solutionWords = words.Where((w, i) => solution[i]).ToArray();
        yield return question(SCodenames.Answers).Answers(solutionWords, preferredWrong: words.Where(x => !solutionWords.Contains(x)).ToArray());
    }
}