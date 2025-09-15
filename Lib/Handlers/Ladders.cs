using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SLadders
{
    [SouvenirQuestion("Which color was present on the second ladder in {0}?", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Cyan", "Purple", "Gray", TranslateAnswers = true)]
    Stage2Colors,

    [SouvenirQuestion("What color was missing on the third ladder in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Cyan", "Purple", "Gray", TranslateAnswers = true)]
    Stage3Missing
}

public partial class SouvenirModule
{
    [SouvenirHandler("ladders", "Ladders", typeof(SLadders), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessLadders(ModuleData module)
    {
        var comp = GetComponent(module, "LaddersScript");

        var fldLadderCols = GetArrayField<int[]>(comp, "ladderColors");
        var fldMissing = GetIntField(comp, "missingColor");

        yield return WaitForSolve;

        var secondLadder = fldLadderCols.Get(expectedLength: 3)[1];
        var missing = fldMissing.Get(min: 0, max: 7);
        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Cyan", "Purple", "Gray" };

        yield return question(SLadders.Stage2Colors).Answers(secondLadder.Distinct().Select(x => colorNames[x]).ToArray());
        yield return question(SLadders.Stage3Missing).Answers(colorNames[missing]);
    }
}