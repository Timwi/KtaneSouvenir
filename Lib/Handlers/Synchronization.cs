using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSynchronization
{
    [Question("Which position initially had the fastest light in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites)]
    [AnswerGenerator.Grid(3, 3)]
    FastestLight,

    [Question("What was the initial speed of the middle light in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 5)]
    MiddleSpeed
}

public partial class SouvenirModule
{
    [Handler("SynchronizationModule", "Synchronization", typeof(SSynchronization), "Espik")]
    [ManualQuestion("Where was the fastest light?")]
    [ManualQuestion("What was the speed of the middle light?")]
    private IEnumerator<SouvenirInstruction> ProcessSynchronization(ModuleData module)
    {
        var comp = GetComponent(module, "SynchronizationModule");
        yield return WaitForSolve;

        var initialSpeeds = GetArrayField<int>(comp, "InitialSpeeds").Get(expectedLength: 9);

        var fastestLight = 0;
        for (var i = 0; i < initialSpeeds.Length; i++)
            if (initialSpeeds[i] == 5)
            {
                fastestLight = i;
                break;
            }

        yield return question(SSynchronization.FastestLight).Answers(new Coord(3, 3, fastestLight));
        yield return question(SSynchronization.MiddleSpeed).Answers(initialSpeeds[4].ToString());
    }
}
