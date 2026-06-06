using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SEtterna
{
    [Question("What color was the {1} arrow from the bottom in {0}?", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Pink", "Orange", "Cyan", "Gray", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [Handler("etterna", "Etterna", typeof(SEtterna), "Espik")]
    [ManualQuestion("What color was each arrow?")]
    private IEnumerator<SouvenirInstruction> ProcessEtterna(ModuleData module)
    {
        var comp = GetComponent(module, "Etterna");
        yield return WaitForSolve;

        var arrowColors = GetArrayField<byte>(comp, "_color").Get(expectedLength: 4);
        var allColors = SEtterna.Color.GetAnswers();
        
        for (var ix = 0; ix < arrowColors.Length; ix++)
            yield return question(SEtterna.Color, args: [Ordinal(ix + 1)]).Answers(allColors[arrowColors[ix]]);
    }
}
