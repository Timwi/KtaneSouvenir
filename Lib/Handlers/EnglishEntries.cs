using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEnglishEntries
{
    [SouvenirQuestion("What was the displayed quote on {0}?", OneColumn4Answers, ExampleAnswers = ["Let’s go shopping!", "We wear our shoes in the house.", "(Kevin starts to clap)", "What is Namsu doing?"])]
    Display
}

public partial class SouvenirModule
{
    [SouvenirHandler("EnglishEntries", "English Entries", typeof(SEnglishEntries), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessEnglishEntries(ModuleData module)
    {
        var comp = GetComponent(module, "EnglishEntries");
        yield return WaitForSolve;

        var loudclapping = GetArrayField<string[]>(comp, "LoudClapping").Get().Select(i => i.Select(j => j.Replace('\n', ' ')).ToArray()).ToArray();
        var ann = GetIntField(comp, "Ann").Get();
        var kevin = GetIntField(comp, "Kevin").Get();
        var allAnswers = loudclapping.SelectMany(i => i).ToArray();

        addQuestion(module, Question.EnglishEntriesDisplay, correctAnswers: new[] { loudclapping[ann][kevin] }, preferredWrongAnswers: allAnswers);
    }
}