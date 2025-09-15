using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SArithmelogic
{
    [SouvenirQuestion("What was the symbol on the submit button in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "ArithmelogicSprites")]
    Submit,

    [SouvenirQuestion("Which number was selectable, but not the solution, in the {1} screen on {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["left", "middle", "right"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(10, 40)]
    Numbers
}

public partial class SouvenirModule
{
    [SouvenirHandler("arithmelogic", "Arithmelogic", typeof(SArithmelogic), "JerryEris")]
    private IEnumerator<SouvenirInstruction> ProcessArithmelogic(ModuleData module)
    {
        var comp = GetComponent(module, "Arithmelogic");
        var fldSymbolNum = GetIntField(comp, "submitSymbol");
        var fldSelectableValues = GetArrayField<int[]>(comp, "selectableValues");
        var fldCurrentDisplays = GetArrayField<int>(comp, "currentDisplays");
        yield return WaitForSolve;

        var symbolNum = fldSymbolNum.Get(min: 0, max: 21);
        var selVal = fldSelectableValues.Get(expectedLength: 3, validator: arr => arr.Length != 4 ? $"length {arr.Length}, expected 4" : null);
        var curDisp = fldCurrentDisplays.Get(expectedLength: 3, validator: val => val is < 0 or >= 4 ? $"expected 0–3" : null);
        yield return question(SArithmelogic.Submit).Answers(ArithmelogicSprites[symbolNum], preferredWrong: ArithmelogicSprites);
        var screens = new[] { "left", "middle", "right" };
        for (var i = 0; i < 3; i++)
            yield return question(SArithmelogic.Numbers, args: [screens[i]]).Answers(Enumerable.Range(0, 4).Where(ix => ix != curDisp[i]).Select(ix => selVal[i][ix].ToString()).ToArray());
    }
}