using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum STriamonds
{
    [SouvenirQuestion("What colour triangle pulsed {1} in {0}?", ThreeColumns6Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    PulsingColours
}

public partial class SouvenirModule
{
    [SouvenirHandler("triamonds", "Triamonds", typeof(STriamonds), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessTriamonds(ModuleData module) => processPolyiamonds(module, "triamondsScript", Question.TriamondsPulsingColours, new[] { "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white" });
}