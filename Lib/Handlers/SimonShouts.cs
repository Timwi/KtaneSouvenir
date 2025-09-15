using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SSimonShouts
{
    [SouvenirQuestion("Which letter flashed on the {1} button in {0}?", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", TranslateArguments = [true], Arguments = ["top", "left", "right", "bottom"], ArgumentGroupSize = 1)]
    FlashingLetter
}

public partial class SouvenirModule
{
    [SouvenirHandler("SimonShoutsModule", "Simon Shouts", typeof(SSimonShouts), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSimonShouts(ModuleData module)
    {
        var comp = GetComponent(module, "SimonShoutsModule");
        yield return WaitForSolve;

        var diagramBPositions = GetArrayField<int>(comp, "_diagramBPositions").Get(expectedLength: 4, validator: b => b is < 0 or > 24 ? "expected range 0–24" : null);
        var diagramB = GetField<string>(comp, "_diagramB").Get(str => str.Length != 24 ? "expected length 24" : str.Any(ch => ch is < 'A' or > 'Z') ? "expected letters A–Z" : null);
        var buttonNames = new[] { "top", "right", "bottom", "left" };
        for (var i = 0; i < 4; i++)
            yield return question(SSimonShouts.FlashingLetter, args: [buttonNames[i]]).Answers(diagramB[diagramBPositions[i]].ToString());
    }
}