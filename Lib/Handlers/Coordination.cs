using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCoordination
{
    [SouvenirQuestion("What was the label of the starting coordinate in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-F", "1-6")]
    Label,
    
    [SouvenirQuestion("Where was the starting coordinate in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(6, 6)]
    Position
}

public partial class SouvenirModule
{
    [SouvenirHandler("Coordination", "Coordination", typeof(SCoordination), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessCoordination(ModuleData module)
    {
        var comp = GetComponent(module, "Coordination");
        yield return WaitForSolve;
        var startingCoordinate = GetIntField(comp, "StartingCoordinate").Get();
        var modCoordinates = GetArrayField<string>(comp, "ModuleCoordinates").Get();
        addQuestions(module,
            makeQuestion(Question.CoordinationLabel, module, correctAnswers: new[] { modCoordinates[startingCoordinate].ToString() }),
            makeQuestion(Question.CoordinationPosition, module, correctAnswers: new[] { new Coord(6, 6, startingCoordinate) }));
    }
}