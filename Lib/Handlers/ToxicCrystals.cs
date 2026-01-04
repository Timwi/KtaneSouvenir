using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SToxicCrystals
{
    [SouvenirQuestion("What letter was written on the casing when solving {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z?")]
    Letter
}

public partial class SouvenirModule
{
    [SouvenirHandler("ToxicCrystals", "Toxic Crystals", typeof(SToxicCrystals), "thunder725")]
    private IEnumerator<SouvenirInstruction> ProcessToxicCrystals(ModuleData module)
    {
        var comp = GetComponent(module, "ToxicCrystals");

        yield return WaitForSolve;

        var letterAnswer = GetField<string>(comp, "currentCasingLetter").Get(v => v.Length != 1 ? "expected singular letter" : null);

        yield return question(SToxicCrystals.Letter).Answers(letterAnswer);
    }
}
