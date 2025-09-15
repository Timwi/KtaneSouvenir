using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SXRing
{
    [SouvenirQuestion("Which symbol was scanned in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "XRingSprites")]
    Symbol
}

public partial class SouvenirModule
{
    [SouvenirHandler("xring", "X-Ring", typeof(SXRing), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessXRing(ModuleData module)
    {
        var comp = GetComponent(module, "XRingScript");
        yield return WaitForSolve;

        var used = GetArrayField<int>(comp, "symbselect").Get(expectedLength: 5, validator: v => v is < 0 or > 63 ? "expected symbol index 0-63" : null);
        addQuestion(module, Question.XRingSymbol, correctAnswers: used.Select(i => XRingSprites[i]).ToArray());
    }
}