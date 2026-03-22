using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SArithmelogic
{
    [SouvenirQuestion("What was the symbol on the submit button in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "ArithmelogicSprites")]
    Submit
}

public partial class SouvenirModule
{
    [SouvenirHandler("arithmelogic", "Arithmelogic", typeof(SArithmelogic), "JerryEris")]
    [SouvenirManualQuestion("What was the symbol on the submit button?")]
    [SouvenirManualQuestion("What were the numbers selectable on the screens?")]
    private IEnumerator<SouvenirInstruction> ProcessArithmelogic(ModuleData module)
    {
        var comp = GetComponent(module, "Arithmelogic");
        var fldSymbolNum = GetIntField(comp, "submitSymbol");
        yield return WaitForSolve;

        var symbolNum = fldSymbolNum.Get(min: 0, max: 21);
        yield return question(SArithmelogic.Submit).Answers(ArithmelogicSprites[symbolNum], preferredWrong: ArithmelogicSprites);
    }
}
