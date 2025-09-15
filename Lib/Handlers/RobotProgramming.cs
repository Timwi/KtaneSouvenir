using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SRobotProgramming
{
    [SouvenirQuestion("What was the color of the {1} robot in {0}?", TwoColumns4Answers, "Blue", "Green", "Red", "Yellow", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Color,

    [SouvenirQuestion("What was the shape of the {1} robot in {0}?", TwoColumns4Answers, "Triangle", "Square", "Hexagon", "Circle", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Shape
}

public partial class SouvenirModule
{
    [SouvenirHandler("robotProgramming", "Robot Programming", typeof(SRobotProgramming), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessRobotProgramming(ModuleData module)
    {
        var comp = GetComponent(module, "robotProgramming");
        yield return WaitForSolve;

        var robotsArr = GetArrayField<object>(comp, "robots").Get(expectedLength: 4);
        var fldColor = GetField<Enum>(robotsArr[0], "Color", isPublic: true);
        var fldShape = GetField<Enum>(robotsArr[0], "Shape", isPublic: true);

        for (var i = 0; i < 4; i++)
        {
            var robot = robotsArr[i];
            var color = fldColor.GetFrom(robot).ToString();
            var shape = fldShape.GetFrom(robot).ToString();

            yield return question(SRobotProgramming.Color, args: [Ordinal(i + 1)]).Answers(color);
            yield return question(SRobotProgramming.Shape, args: [Ordinal(i + 1)]).Answers(shape);
        }
    }
}