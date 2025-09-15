using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRedHerring
{
    [SouvenirQuestion("What was the first color flashed by {0}?", TwoColumns4Answers, "Green", "Blue", "Purple", "Orange")]
    FirstFlash
}

public partial class SouvenirModule
{
    [SouvenirHandler("RedHerring", "Red Herring", typeof(SRedHerring), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessRedHerring(ModuleData module)
    {
        var comp = GetComponent(module, "RedHerring");
        yield return WaitForSolve;

        string[] colorNames = { "Green", "Blue", "Purple", "Orange" };
        var firstColor = GetArrayField<int>(comp, "colorIndices").Get(expectedLength: 4).First();
        yield return question(SRedHerring.FirstFlash).Answers(colorNames[firstColor]);
    }
}