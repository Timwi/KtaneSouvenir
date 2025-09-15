using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonsStar
{
    [SouvenirQuestion("Which color flashed {1} in {0}?", ThreeColumns6Answers, "red", "yellow", "green", "blue", "purple", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("simonsStar", "Simon’s Star", typeof(SSimonsStar), "TasThiluna")]
    private IEnumerator<SouvenirInstruction> ProcessSimonsStar(ModuleData module)
    {
        var comp = GetComponent(module, "simonsStarScript");
        var validColors = new[] { "red", "yellow", "green", "blue", "purple" };
        var flashes = "first,second,third,fourth,fifth".Split(',').Select(n => GetField<string>(comp, n + "FlashColour", isPublic: true).Get(c => !validColors.Contains(c) ? "invalid color" : null)).ToArray();

        yield return WaitForSolve;

        addQuestions(module, flashes.Select((f, ix) => makeQuestion(SSimonsStar.Colors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { f })));
    }
}