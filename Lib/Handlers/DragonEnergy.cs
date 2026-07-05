using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDragonEnergy
{
    [Question("What color was the indicator in {0}?", ThreeColumns3Answers, "Orange", "Cyan", "Purple", TranslateAnswers = true)]
    IndColor
}

public partial class SouvenirModule
{
    [Handler("dragonEnergy", "Dragon Energy", typeof(SDragonEnergy), "Espik")]
    [ManualQuestion("What was the indicator color?")]
    private IEnumerator<SouvenirInstruction> ProcessDragonEnergy(ModuleData module)
    {
        var comp = GetComponent(module, "dragonEnergy");
        var indColor = GetIntField(comp, "indicatorColor").Get();

        yield return WaitForSolve;
        yield return question(SDragonEnergy.IndColor).Answers(SDragonEnergy.IndColor.GetAnswers()[indColor]);
    }
}
