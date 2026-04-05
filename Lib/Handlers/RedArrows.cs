using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRedArrows
{
    [Question("What was the starting number in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    StartNumber
}

public partial class SouvenirModule
{
    [Handler("redArrowsModule", "Red Arrows", typeof(SRedArrows), "kavinkul")]
    [ManualQuestion("What was the starting number?")]
    private IEnumerator<SouvenirInstruction> ProcessRedArrows(ModuleData module)
    {
        var comp = GetComponent(module, "RedArrowsScript");
        yield return WaitForSolve;

        yield return question(SRedArrows.StartNumber).Answers(GetIntField(comp, "start").Get(min: 0, max: 9).ToString());
    }
}