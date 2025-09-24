using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBlueArrows
{
    [SouvenirQuestion("What were the characters on the screen in {0}?", ThreeColumns6Answers, "CA", "C1", "CB", "C8", "CF", "C4", "CE", "C6", "3A", "31", "3B", "38", "3F", "34", "3E", "36", "GA", "G1", "GB", "G8", "GF", "G4", "GE", "G6", "7A", "71", "7B", "78", "7F", "74", "7E", "76", "DA", "D1", "DB", "D8", "DF", "D4", "DE", "D6", "5A", "51", "5B", "58", "5F", "54", "5E", "56", "HA", "H1", "HB", "H8", "HF", "H4", "HE", "H6", "2A", "21", "2B", "28", "2F", "24", "2E", "26")]
    InitialCharacters
}

public partial class SouvenirModule
{
    [SouvenirHandler("blueArrowsModule", "Blue Arrows", typeof(SBlueArrows), "kavinkul")]
    private IEnumerator<SouvenirInstruction> ProcessBlueArrows(ModuleData module)
    {
        var comp = GetComponent(module, "BlueArrowsScript");
        var fldCoord = GetField<string>(comp, "coord");

        yield return WaitForSolve;

        string[] characters = { "CA", "C1", "CB", "C8", "CF", "C4", "CE", "C6", "3A", "31", "3B", "38", "3F", "34", "3E", "36", "GA", "G1", "GB", "G8", "GF", "G4", "GE", "G6", "7A", "71", "7B", "78", "7F", "74", "7E", "76", "DA", "D1", "DB", "D8", "DF", "D4", "DE", "D6", "5A", "51", "5B", "58", "5F", "54", "5E", "56", "HA", "H1", "HB", "H8", "HF", "H4", "HE", "H6", "2A", "21", "2B", "28", "2F", "24", "2E", "26" };
        var coord = fldCoord.Get(v => !characters.Contains(v) ? $"expected one of: [{characters.JoinString(", ")}]" : null);
        yield return question(SBlueArrows.InitialCharacters).Answers(coord);
    }
}