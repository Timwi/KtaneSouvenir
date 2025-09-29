using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SXRotor
{
    [SouvenirQuestion("Which symbol was scanned in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "XRotorSprites")]
    Symbol
}

public partial class SouvenirModule
{
    [SouvenirHandler("xrotor", "X-Rotor", typeof(SXRotor), "KiloBites")]
    private IEnumerator<SouvenirInstruction> ProcessXRotor(ModuleData module)
    {
        var comp = GetComponent(module, "XRotorScript");

        yield return WaitForSolve;

        var chosenSymbols = GetArrayField<int[]>(comp, "choosesymb").Get(expectedLength: 2, validator: arr => arr.Length != 5 ? "expected length 5" : null)[1];

        yield return question(SXRotor.Symbol).Answers(chosenSymbols.Select(x => XRotorSprites[x]).ToArray());
    }
}
