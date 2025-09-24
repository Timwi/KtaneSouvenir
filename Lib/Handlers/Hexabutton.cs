using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHexabutton
{
    [SouvenirQuestion("What label was printed on {0}?", ThreeColumns6Answers, "Jump", "Boom", "Claim", "Button", "Hold", "Blue")]
    Label
}

public partial class SouvenirModule
{
    [SouvenirHandler("hexabutton", "Hexabutton", typeof(SHexabutton), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessHexabutton(ModuleData module)
    {
        var comp = GetComponent(module, "hexabuttonScript");
        var labels = GetArrayField<string>(comp, "labels").Get();
        var index = GetIntField(comp, "labelNum").Get(0, labels.Length - 1);

        yield return WaitForSolve;
        yield return question(SHexabutton.Label).Answers(labels[index]);
    }
}