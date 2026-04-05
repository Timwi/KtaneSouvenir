using System.Collections.Generic;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SPaintingCube
{
    [Question("What color was missing in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet", TranslateAnswers = true)]
    MissingColor
}

public partial class SouvenirModule
{
    [Handler("paintingCube", "Painting Cube", typeof(SPaintingCube), "KiloBites")]
    [ManualQuestion("What color was missing?")]
    private IEnumerator<SouvenirInstruction> ProcessPaintingCube(ModuleData module)
    {
        var comp = GetComponent(module, "PaintingCubeScript");
        yield return WaitForSolve;

        var missingColor = GetField<object>(comp, "missingColor").Get();

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };

        yield return question(SPaintingCube.MissingColor).Answers(colorNames[(int) missingColor], colorNames);
    }
}
