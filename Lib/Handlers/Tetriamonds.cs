using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum STetriamonds
{
    [SouvenirQuestion("What colour triangle pulsed {1} in {0}?", ThreeColumns6Answers, "orange", "lime", "jade", "azure", "violet", "rose", "grey", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    PulsingColours
}

public partial class SouvenirModule
{
    [SouvenirHandler("tetriamonds", "Tetriamonds", typeof(STetriamonds), "Kuro")]
    private IEnumerator<SouvenirInstruction> ProcessTetriamonds(ModuleData module) => processPolyiamonds(module, "tetriamondsScript", Question.TetriamondsPulsingColours, new[] { "orange", "lime", "jade", "azure", "violet", "rose", "grey" });
}