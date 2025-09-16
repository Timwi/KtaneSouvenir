using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonStacks
{
    [SouvenirQuestion("Which color flashed in the {1} stage of {0}?", TwoColumns4Answers, "Red", "Green", "Blue", "Yellow", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonstacks", "Simon Stacks", typeof(SSimonStacks), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessSimonStacks(ModuleData module)
    {
        var comp = GetComponent(module, "simonstacksScript");

        yield return WaitForSolve;

        var colors = GetListField<string>(comp, "Colors").Get(minLength: 3, maxLength: 5);
        for (var ix = 0; ix < colors.Length; ix++)
            yield return question(SSimonStacks.Colors, args: [Ordinal(ix + 1)]).Answers(colors[ix]);
    }
}