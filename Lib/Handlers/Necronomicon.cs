using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNecronomicon
{
    [Question("Which chapter number was present in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 40)]
    Chapters
}

public partial class SouvenirModule
{
    [Handler("necronomicon", "Necronomicon", typeof(SNecronomicon), "luisdiogo98", AddThe = true)]
    [ManualQuestion("What were the chapter numbers?")]
    private IEnumerator<SouvenirInstruction> ProcessNecronomicon(ModuleData module)
    {
        var comp = GetComponent(module, "necronomiconScript");

        yield return WaitForSolve;

        var chapters = GetArrayField<int>(comp, "selectedChapters").Get(expectedLength: 7);
        var chaptersString = chapters.Select(x => x.ToString()).ToArray();

        yield return question(SNecronomicon.Chapters).Answers(chaptersString);
    }
}
