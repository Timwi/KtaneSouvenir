using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SStackedSequences
{
    [SouvenirQuestion("Which of these is the length of a sequence in {0}?", TwoColumns4Answers, ExampleAnswers = ["3", "4", "5", "6"])]
    [AnswerGenerator.Integers(3, 9)]
    Question
}

public partial class SouvenirModule
{
    [SouvenirHandler("stackedSequences", "Stacked Sequences", typeof(SStackedSequences), "GhostSalt")]
    private IEnumerator<SouvenirInstruction> ProcessStackedSequences(ModuleData module)
    {
        var comp = GetComponent(module, "stackedSequencesScript");
        yield return WaitForSolve;

        var sequences = GetArrayField<List<int>>(comp, "answer").Get();

        addQuestion(module, Question.StackedSequences, correctAnswers: sequences.Select(x => x.Count.ToString()).ToArray());
    }
}