using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimultaneousSimons
{
    [SouvenirQuestion("What color flashed {1} on the {2} Simon in {0}?", TwoColumns4Answers, "Blue", "Yellow", "Red", "Green", TranslateAnswers = true, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    Flash
}

public partial class SouvenirModule
{
    [SouvenirHandler("simultaneousSimons", "Simultaneous Simons", typeof(SSimultaneousSimons), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessSimultaneousSimons(ModuleData module)
    {
        var comp = GetComponent(module, "SimultaneousSimons");
        yield return WaitForSolve;

        var sequences = GetField<int[,]>(comp, "sequences").Get();
        var btnColors = GetStaticField<int[]>(comp.GetType(), "buttonColors").Get();
        var colorNames = new[] { "Blue", "Yellow", "Red", "Green" };
        for (var simon = 0; simon < 4; simon++)
            for (var flash = 0; flash < 4; flash++)
                yield return question(SSimultaneousSimons.Flash, args: [Ordinal(flash + 1), Ordinal(simon + 1)]).Answers(colorNames[btnColors[sequences[simon, flash]]]);
    }
}