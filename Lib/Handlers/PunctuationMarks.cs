using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SPunctuationMarks
{
    [Question("What was the displayed number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99, "00")]
    DisplayedNumber
}

public partial class SouvenirModule
{
    [Handler("punctuationMarks", "...?", typeof(SPunctuationMarks), "Kuro")]
    [ManualQuestion("What was the displayed number?")]
    private IEnumerator<SouvenirInstruction> ProcessPunctuationMarks(ModuleData module)
    {
        var comp = GetComponent(module, "script");

        yield return WaitForSolve;

        var number = GetIntField(comp, "memoryBankNumber").Get(min: 0, max: 99).ToString("00");
        yield return question(SPunctuationMarks.DisplayedNumber).Answers(number);
    }
}