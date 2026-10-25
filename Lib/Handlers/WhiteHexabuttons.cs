using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWhiteHexabuttons
{
    [Question("What was the color of the center button in {0}?", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple")]
    QCenter
}

public partial class SouvenirModule
{
    [Handler("whiteHexabuttons", "White Hexabuttons", typeof(SWhiteHexabuttons), "Infusion")]
    [ManualQuestion("What was the color of the center button?")]
    private IEnumerator<SouvenirInstruction> ProcessWhiteHexabuttons(ModuleData module)
    {
        var comp = GetComponent(module, "whiteHexabuttons");
        var colors = GetField<string>(comp, "whiteBHC").Get();

        string centerColor = "";

        switch (colors[6])
        {
            case 'R': centerColor = "Red"; break;
            case 'O': centerColor = "Orange"; break;
            case 'Y': centerColor = "Yellow"; break;
            case 'G': centerColor = "Green"; break;
            case 'B': centerColor = "Blue"; break;
            case 'P': centerColor = "Purple"; break;
        }

        yield return WaitForSolve;

        yield return question(SWhiteHexabuttons.QCenter).Answers(centerColor);
    }
}
