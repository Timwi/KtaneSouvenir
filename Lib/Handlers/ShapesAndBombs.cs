using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SShapesAndBombs
{
    [SouvenirQuestion("What was the initial letter in {0}?", ThreeColumns6Answers, "A", "B", "D", "E", "G", "I", "K", "L", "N", "O", "P", "S", "T", "X", "Y")]
    InitialLetter
}

public partial class SouvenirModule
{
    [SouvenirHandler("ShapesBombs", "Shapes And Bombs", typeof(SShapesAndBombs), "KingSlendy")]
    private IEnumerator<SouvenirInstruction> ProcessShapesAndBombs(ModuleData module)
    {
        var comp = GetComponent(module, "ShapesBombs");
        var initialLetter = GetIntField(comp, "selectLetter").Get(min: 0, max: 14);

        yield return WaitForSolve;

        var letterChars = new[] { "A", "B", "D", "E", "G", "I", "K", "L", "N", "O", "P", "S", "T", "X", "Y" };
        yield return question(SShapesAndBombs.InitialLetter).Answers(letterChars[initialLetter]);
    }
}