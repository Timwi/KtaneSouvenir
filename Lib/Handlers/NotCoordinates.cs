using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotCoordinates
{
    [Question("What was the initial shape in the second stage of {0}?", ThreeColumns6Answers, AnswerType = InfoType.SymbolsFont)]
    [AnswerGenerator.Strings("\u00a1-\u00a9", "\u00aa-\u00b2")]
    InitialShape
}

public partial class SouvenirModule
{
    [Handler("notCoordinates", "Not Coordinates", typeof(SNotCoordinates), "Espik")]
    [ManualQuestion("What was the initial shape in the second stage?")]
    private IEnumerator<SouvenirInstruction> ProcessNotCoordinates(ModuleData module)
    {
        var comp = GetComponent(module, "NCooScript");
        var fldCoordsSubmitted = GetArrayField<bool>(comp, "gud");

        // Waits for stage 2
        while (!fldCoordsSubmitted.Get(expectedLength: 3).All(x => x == true))
            yield return null;

        var positionArr = GetArrayField<int>(comp, "pos").Get(expectedLength: 4);
        var doubleOhGrid = GetField<int[,]>(comp, "dogrid").Get();

        var globalPosition = (((positionArr[0] * 3) + positionArr[2]) * 9) + (positionArr[1] * 3) + positionArr[3];
        var shape = doubleOhGrid[globalPosition / 9, globalPosition % 9];

        yield return WaitForSolve;

        yield return question(SNotCoordinates.InitialShape).Answers($"{(char) (0xa1 + (shape / 10))}{(char) (0xaa + (shape % 10))}");
    }
}
