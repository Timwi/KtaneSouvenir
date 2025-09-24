using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SXYRay
{
    [SouvenirQuestion("Which shape was scanned by {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "XYRaySprites")]
    Shapes
}

public partial class SouvenirModule
{
    [SouvenirHandler("xyRay", "XY-Ray", typeof(SXYRay), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessXYRay(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "XYRayScript");
        var shapes = GetArrayField<int>(comp, "sindex").Get(expectedLength: 3, validator: v => v is < 0 or > 26 ? "Expected range [0, 26]" : null);

        yield return question(SXYRay.Shapes).Answers(shapes.Select(i => XYRaySprites[i]).ToArray());
    }
}