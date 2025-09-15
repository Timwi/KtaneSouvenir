using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SNotCoordinates
{
    [SouvenirQuestion("Which coordinate was part of the square in {0}?", OneColumn4Answers, ExampleAnswers = ["[4,7]", "C4", "<0, 2>", "3, 1", "(6,2)", "B-1", "“1, 0”", "4/3", "[12]", "#23", "四十七"])]
    SquareCoords
}

public partial class SouvenirModule
{
    [SouvenirHandler("notCoordinates", "Not Coordinates", typeof(SNotCoordinates), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNotCoordinates(ModuleData module)
    {
        var comp = GetComponent(module, "NCooScript");
        yield return WaitForSolve;

        // Step 1: Finding square
        var disp = GetListField<string>(comp, "disp").Get(minLength: 3);
        var seq = GetArrayField<List<int>>(comp, "seq").Get(expectedLength: 2, validator: i => i.Count < 3 ? "expected length at least 3" : null);
        var answers = seq[0].Take(3).Select(coord => disp[seq[1].IndexOf(coord)]).ToArray();

        addQuestion(module, Question.NotCoordinatesSquareCoords, correctAnswers: answers, preferredWrongAnswers: disp.ToArray());
    }
}