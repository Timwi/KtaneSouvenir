using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLimeArrows
{
    [SouvenirQuestion("What was the starting coordinate in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-I", "1-9")]
    Coordinates
}

public partial class SouvenirModule
{
    [SouvenirHandler("limeArrowsModule", "Lime Arrows", typeof(SLimeArrows), "thunder725")]
    private IEnumerator<SouvenirInstruction> ProcessLimeArrows(ModuleData module)
    {
        var struck = false;
        module.Module.OnStrike += () => struck = true;

        yield return WaitForSolve;

        if (struck)
            yield return legitimatelyNoQuestion(module, "You got a strike from Lime Arrows.");

        var comp = GetComponent(module, "LimeArrowsScript");
        var startingCoordinate = GetField<string>(comp, "startingCoordinate").Get();

        yield return question(SLimeArrows.Coordinates).Answers(startingCoordinate);
    }
}
