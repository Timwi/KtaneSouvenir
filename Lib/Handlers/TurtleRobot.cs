using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum STurtleRobot
{
    [SouvenirQuestion("What was the {1} line you commented out in {0}?", TwoColumns4Answers, ExampleAnswers = ["LT 90", "FD 1", "RT 180 2", "LT 90 2", "RT 180", "FD 6", "RT 90 2"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, Type = AnswerType.TurtleRobotFont)]
    CodeLines
}

public partial class SouvenirModule
{
    [SouvenirHandler("turtleRobot", "Turtle Robot", typeof(STurtleRobot), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessTurtleRobot(ModuleData module)
    {
        var comp = GetComponent(module, "TurtleRobot");
        var fldCursor = GetIntField(comp, "_cursor");
        var mthFormatCommand = GetMethod<string>(comp, "FormatCommand", 2);
        var commands = GetField<IList>(comp, "_commands").Get();
        var deleteButton = GetField<KMSelectable>(comp, "ButtonDelete", isPublic: true).Get();

        var codeLines = commands.Cast<object>().Select(cmd => mthFormatCommand.Invoke(cmd, false)).ToArray();
        var bugs = new List<string>();
        var bugsMarked = new HashSet<int>();

        var buttonHandler = deleteButton.OnInteract;
        deleteButton.OnInteract = delegate
        {
            var ret = buttonHandler();
            var cursor = fldCursor.Get();   // int field: avoid throwing exceptions inside of the button handler
            var command = mthFormatCommand.Invoke(commands[cursor], true);
            if (command.StartsWith("#") && bugsMarked.Add(cursor))
                bugs.Add(codeLines[cursor]);
            return ret;
        };

        yield return WaitForSolve;
        for (var ix = 0; ix < bugs.Count && ix < 2; ix++)
            yield return question(STurtleRobot.CodeLines, args: [Ordinal(ix + 1)]).Answers(bugs[ix], preferredWrong: codeLines);
    }
}
