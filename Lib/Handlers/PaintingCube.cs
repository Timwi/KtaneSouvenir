using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;


public enum SPaintingCube
{
    [SouvenirQuestion("What color was missing in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet", TranslateAnswers = true)]
    MissingColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("paintingCube", "Painting Cube", typeof(SPaintingCube), "KiloBites")]
    private IEnumerator<SouvenirInstruction> ProcessPaintingCube(ModuleData module)
    {
        var comp = GetComponent(module, "PaintingCubeScript");
        yield return WaitForSolve;

        var fldMissingColor = GetField<object>(comp, "missingColor").Get();

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };

        yield return question(SPaintingCube.MissingColor).Answers(colorNames[(int) fldMissingColor], colorNames);
    }
}
