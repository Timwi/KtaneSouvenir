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

        yield return question(SNecronomicon.Chapters, args: [Ordinal(1)]).Answers(chaptersString[0], preferredWrong: chaptersString);
        yield return question(SNecronomicon.Chapters, args: [Ordinal(2)]).Answers(chaptersString[1], preferredWrong: chaptersString);
        yield return question(SNecronomicon.Chapters, args: [Ordinal(3)]).Answers(chaptersString[2], preferredWrong: chaptersString);
        yield return question(SNecronomicon.Chapters, args: [Ordinal(4)]).Answers(chaptersString[3], preferredWrong: chaptersString);
        yield return question(SNecronomicon.Chapters, args: [Ordinal(5)]).Answers(chaptersString[4], preferredWrong: chaptersString);
        yield return question(SNecronomicon.Chapters, args: [Ordinal(6)]).Answers(chaptersString[5], preferredWrong: chaptersString);
        yield return question(SNecronomicon.Chapters, args: [Ordinal(7)]).Answers(chaptersString[6], preferredWrong: chaptersString);
    }
}