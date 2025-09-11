using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SCube
{
    [SouvenirQuestion("What was the {1} cube rotation in {0}?", TwoColumns4Answers, "rotate cw", "tip left", "tip backwards", "rotate ccw", "tip right", "tip forwards", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Rotations
}

public partial class SouvenirModule
{
    [SouvenirHandler("cube", "Cube", typeof(SCube), "luisdiogo98", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessCube(ModuleData module)
    {
        var comp = GetComponent(module, "theCubeScript");
        yield return WaitForSolve;

        var rotations = GetListField<int>(comp, "selectedRotations").Get(expectedLength: 6);
        var rotationNames = new[] { "rotate cw", "tip left", "tip backwards", "rotate ccw", "tip right", "tip forwards" };
        var allRotations = rotations.Select(r => rotationNames[r]).ToArray();

        addQuestions(module, rotations.Select((rot, ix) => makeQuestion(Question.CubeRotations, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { rotationNames[rot] }, preferredWrongAnswers: allRotations)));
    }
}