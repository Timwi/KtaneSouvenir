using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SBooleanWires
{
    [Question("Which letter was present in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    DisplayedLetters
}

public partial class SouvenirModule
{
    [Handler("booleanWires", "Boolean Wires", typeof(SBooleanWires), "Espik")]
    [ManualQuestion("Which letters were displayed in each stage?")]
    private IEnumerator<SouvenirInstruction> ProcessBooleanWires(ModuleData module)
    {
        var comp = GetComponent(module, "BooleanWiresScript");

        var stage = GetIntField(comp, "stageNumber", isPublic: true);
        var currentStage = -1; // Set this to an unused value so the while loop isn't immediately skipped

        var leftLetter = GetField<string>(comp, "firstLetter", isPublic: true);
        var rightLetter = GetField<string>(comp, "secondLetter", isPublic: true);

        var allLeftLetters = new string[5];
        var allRightLetters = new string[5];

        while (stage.Get() != currentStage)
        {
            if (module.IsSolved)
                break;

            currentStage = stage.Get();

            if (currentStage > 0) // The module uses 1-index stages, but still uses 0 during animations
            {
                yield return null; // Wait 0.1 seconds so we're sure we got the right stage
                allLeftLetters[currentStage - 1] = leftLetter.Get();
                allRightLetters[currentStage - 1] = rightLetter.Get();
            }

            while (stage.Get() == currentStage)
                yield return null;
        }

        yield return WaitForSolve;

        for (var pos = 0; pos < 5; pos++)
            yield return question(SBooleanWires.DisplayedLetters, args: [Ordinal(pos + 1)]).Answers([allLeftLetters[pos], allRightLetters[pos]]);
    }
}
