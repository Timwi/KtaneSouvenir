using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SSimonShapes
{
    [Question("What position was the {2} flash in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal, QandA.Ordinal], ArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(1, 6)]
    PositionFlash
}

public partial class SouvenirModule
{
    [Handler("SimonShapesModule", "Simon Shapes", typeof(SSimonShapes), "Espik")]
    [ManualQuestion("What were the positions of each flash in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessSimonShapes(ModuleData module)
    {
        var comp = GetComponent(module, "SimonShapesScript");
        yield return WaitForSolve;

        var allFlashes = GetListField<List<int>>(comp, "_flashes").Get();

        for (var stage = 0; stage < allFlashes.Count; stage++)
            for (var flash = 0; flash < allFlashes[stage].Count; flash++)
                yield return question(SSimonShapes.PositionFlash, args: [Ordinal(stage + 1), Ordinal(flash + 1)]).Answers(allFlashes[stage][flash].ToString());
    }
}
