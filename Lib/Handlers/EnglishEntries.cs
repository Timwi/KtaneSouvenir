using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEnglishEntries
{
    [Question("What was the displayed quote on {0}?", OneColumn4Answers, ExampleAnswers = ["Let’s go shopping!", "We wear our shoes in the house.", "(Kevin starts to clap)", "What is Namsu doing?"])]
    Display
}

public partial class SouvenirModule
{
    [Handler("EnglishEntries", "English Entries", typeof(SEnglishEntries), "Quinn Wuest")]
    [ManualQuestion("What was the displayed quote?")]
    private IEnumerator<SouvenirInstruction> ProcessEnglishEntries(ModuleData module)
    {
        var comp = GetComponent(module, "EnglishEntries");
        yield return WaitForSolve;

        var loudclapping = GetArrayField<string[]>(comp, "LoudClapping").Get().Select(i => i.Select(j => j.Replace('\n', ' ')).ToArray()).ToArray();
        var ann = GetIntField(comp, "Ann").Get();
        var kevin = GetIntField(comp, "Kevin").Get();
        var allAnswers = loudclapping.SelectMany(i => i).ToArray();

        yield return question(SEnglishEntries.Display).Answers(loudclapping[ann][kevin], preferredWrong: allAnswers);
    }
}