using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNecronomicon
{
    [SouvenirQuestion("What was the chapter number of the {1} page in {0}?", ThreeColumns6Answers, ExampleAnswers = ["1", "24", "36"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Chapters
}

public partial class SouvenirModule
{
    [SouvenirHandler("necronomicon", "Necronomicon", typeof(SNecronomicon), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessNecronomicon(ModuleData module)
    {
        var comp = GetComponent(module, "necronomiconScript");

        yield return WaitForSolve;

        var chapters = GetArrayField<int>(comp, "selectedChapters").Get(expectedLength: 7);
        var chaptersString = chapters.Select(x => x.ToString()).ToArray();

        addQuestions(module,
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { chaptersString[0] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { chaptersString[1] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { chaptersString[2] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(4) }, correctAnswers: new[] { chaptersString[3] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(5) }, correctAnswers: new[] { chaptersString[4] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(6) }, correctAnswers: new[] { chaptersString[5] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(7) }, correctAnswers: new[] { chaptersString[6] }, preferredWrongAnswers: chaptersString));
    }
}