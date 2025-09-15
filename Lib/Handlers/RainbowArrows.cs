using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRainbowArrows
{
    [SouvenirQuestion("What was the displayed number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99, "00")]
    Number
}

public partial class SouvenirModule
{
    [SouvenirHandler("ksmRainbowArrows", "Rainbow Arrows", typeof(SRainbowArrows), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessRainbowArrows(ModuleData module)
    {
        var comp = GetComponent(module, "RainbowArrows");
        yield return WaitForSolve;

        addQuestion(module, Question.RainbowArrowsNumber, correctAnswers: new[] { GetIntField(comp, "displayedDigits").Get(min: 0, max: 99).ToString("00") });
    }
}