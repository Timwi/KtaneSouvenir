using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWordCount
{
    [Question("What was the displayed number on {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 1000)]
    Number
}

public partial class SouvenirModule
{
    [Handler("wordCount", "Word Count", typeof(SWordCount), "Quinn Wuest")]
    [ManualQuestion("What was the displayed number?")]
    private IEnumerator<SouvenirInstruction> ProcessWordCount(ModuleData module)
    {
        var comp = GetComponent(module, "WordCountScript");
        yield return WaitForSolve;

        var displayNumber = GetIntField(comp, "DisplayNumber").Get();

        yield return question(SWordCount.Number).Answers(displayNumber.ToString());
    }
}