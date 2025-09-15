using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SShapeShift
{
    [SouvenirQuestion("What was the initial shape in {0}?", TwoColumns4Answers, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings('A', 'P')]
    InitialShape
}

public partial class SouvenirModule
{
    [SouvenirHandler("shapeshift", "Shape Shift", typeof(SShapeShift), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessShapeShift(ModuleData module)
    {
        var comp = GetComponent(module, "ShapeShiftModule");
        yield return WaitForSolve;

        var stL = GetIntField(comp, "startL").Get();
        var stR = GetIntField(comp, "startR").Get();
        var solL = GetIntField(comp, "solutionL").Get();
        var solR = GetIntField(comp, "solutionR").Get();
        var answers = new HashSet<string>();
        for (var l = 0; l < 4; l++)
            if (stL != solL || l == stL)
                for (var r = 0; r < 4; r++)
                    if (stR != solR || r == stR)
                        answers.Add(((char) ('A' + r + (4 * l))).ToString());
        if (answers.Count < 4)
            legitimatelyNoQuestion(module, "The answer was the same as the initial state.");
        else
            addQuestion(module, Question.ShapeShiftInitialShape, correctAnswers: new[] { ((char) ('A' + stR + (4 * stL))).ToString() }, preferredWrongAnswers: answers.ToArray());
    }
}