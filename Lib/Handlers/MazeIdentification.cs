using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SMazeIdentification
{
    [SouvenirQuestion("What was the seed of the maze in {0}?", ThreeColumns6Answers, ExampleAnswers = ["1234", "1111", "2222", "3333", "4444", "4321"])]
    [AnswerGenerator.Strings("4*1-4")]
    Seed,

    [SouvenirQuestion("What was the function of button {1} in {0}?", OneColumn4Answers, ["Forwards", "Clockwise", "Backwards", "Counter-clockwise"], ExampleAnswers = ["forwards", "clockwise", "backwards", "counter-clockwise"], TranslateAnswers = true, Arguments = ["1", "2", "3", "4"], ArgumentGroupSize = 1)]
    Num,

    [SouvenirQuestion("Which button {1} in {0}?", TwoColumns4Answers, ["1", "2", "3", "4"], ExampleAnswers = ["1", "2", "3", "4"], Arguments = ["moved you forwards", "turned you clockwise", "moved you backwards", "turned you counter-clockwise"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Func
}

public partial class SouvenirModule
{
    [SouvenirHandler("GSMazeIdentification", "Maze Identification", typeof(SMazeIdentification), "GhostSalt")]
    private IEnumerator<SouvenirInstruction> ProcessMazeIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "MazeIdentificationScript");
        yield return WaitForSolve;

        var seed = GetArrayField<int>(comp, "Quadrants").Get(validator: x => x.Any(y => y is >= 4 or < 0) ? "quadrants out of range" : null);
        var buttonFuncs = GetArrayField<int>(comp, "ButtonFunctions").Get(validator: x => x.Any(y => y is >= 4 or < 0) ? "functions out of range" : null);
        var directions = new[] { "Forwards", "Clockwise", "Backwards", "Counter-clockwise" };
        addQuestions(module,
            makeQuestion(Question.MazeIdentificationSeed, module, correctAnswers: new[] { seed.Select(x => x + 1).JoinString() }),
            makeQuestion(Question.MazeIdentificationNum, module, formatArgs: new[] { "1" }, correctAnswers: new[] { directions[buttonFuncs[0]] }),
            makeQuestion(Question.MazeIdentificationNum, module, formatArgs: new[] { "2" }, correctAnswers: new[] { directions[buttonFuncs[1]] }),
            makeQuestion(Question.MazeIdentificationNum, module, formatArgs: new[] { "3" }, correctAnswers: new[] { directions[buttonFuncs[2]] }),
            makeQuestion(Question.MazeIdentificationNum, module, formatArgs: new[] { "4" }, correctAnswers: new[] { directions[buttonFuncs[3]] }),
            makeQuestion(Question.MazeIdentificationFunc, module, formatArgs: new[] { "moved you forwards" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 0) + 1).ToString() }),
            makeQuestion(Question.MazeIdentificationFunc, module, formatArgs: new[] { "turned you clockwise" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 1) + 1).ToString() }),
            makeQuestion(Question.MazeIdentificationFunc, module, formatArgs: new[] { "moved you backwards" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 2) + 1).ToString() }),
            makeQuestion(Question.MazeIdentificationFunc, module, formatArgs: new[] { "turned you counter-clockwise" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 3) + 1).ToString() }));
    }
}