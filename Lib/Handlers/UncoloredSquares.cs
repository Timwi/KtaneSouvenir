using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SUncoloredSquares
{
    [SouvenirQuestion("What was the {1} color in reading order used in the first stage of {0}?", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    FirstStage
}

public partial class SouvenirModule
{
    [SouvenirHandler("UncoloredSquaresModule", "Uncolored Squares", typeof(SUncoloredSquares), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessUncoloredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "UncoloredSquaresModule");
        yield return WaitForSolve;
        yield return question(SUncoloredSquares.FirstStage, args: [Ordinal(1)]).Answers(GetField<object>(comp, "_firstStageColor1").Get().ToString());
        yield return question(SUncoloredSquares.FirstStage, args: [Ordinal(2)]).Answers(GetField<object>(comp, "_firstStageColor2").Get().ToString());
    }
}