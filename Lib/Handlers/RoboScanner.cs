using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SRoboScanner
{
    [SouvenirQuestion("Where was the empty cell in {0}?", ThreeColumns6Answers, "A1", "A2", "A3", "A4", "A5", "B1", "B2", "B3", "B4", "B5", "C1", "C2", "C4", "C5", "D1", "D2", "D3", "D4", "D5", "E1", "E2", "E3", "E4", "E5")]
    EmptyCell
}

public partial class SouvenirModule
{
    [SouvenirHandler("roboScannerModule", "Robo-Scanner", typeof(SRoboScanner), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessRoboScanner(ModuleData module)
    {
        var comp = GetComponent(module, "RoboScannerScript");
        yield return WaitForSolve;

        var emptyCell = GetIntField(comp, "emptyCell").Get(min: 0, max: 24);
        var sol = "ABCDE"[emptyCell % 5].ToString() + "12345"[emptyCell / 5].ToString();
        addQuestion(module, Question.RoboScannerEmptyCell, correctAnswers: new[] { sol });
    }
}