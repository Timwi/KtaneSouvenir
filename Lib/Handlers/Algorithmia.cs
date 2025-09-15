using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SAlgorithmia
{
    [SouvenirQuestion("Which position was the {1} position in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, Arguments = ["starting", "goal"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Grid(4, 4)]
    Positions,

    [SouvenirQuestion("What was the color of the colored bulb in {0}?", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Yellow", "Magenta")]
    Color,

    [SouvenirQuestion("Which number was present in the seed in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99)]
    Seed
}

public partial class SouvenirModule
{
    [SouvenirHandler("algorithmia", "Algorithmia", typeof(SAlgorithmia), "tandyCake")]
    private IEnumerator<SouvenirInstruction> ProcessAlgorithmia(ModuleData module)
    {
        var comp = GetComponent(module, "AlgorithmiaScript");

        var startingPos = GetIntField(comp, "currentPos").Get(min: 0, max: 15);
        var goalPos = GetIntField(comp, "goalPos").Get(min: 0, max: 15);

        yield return WaitForSolve;

        var fldColor = GetField<object>(comp, "mazeAlg");
        var color = fldColor.Get(col => (int) col is < 0 or >= 6 ? "expected in range 0-5" : null).ToString();

        var fldSeed = GetField<object>(comp, "seed");
        var prpSeedValues = GetProperty<int[]>(fldSeed.Get(), "values", true);
        var seedVals = prpSeedValues.Get(arr =>
            arr.Length != 10 ? "expected length 10" :
            arr.Any(val => val is < 0 or > 99) ? "expected in range 0-99" :
            null);

        addQuestions(module,
            makeQuestion(Question.AlgorithmiaPositions, module, formatArgs: new[] { "starting" }, correctAnswers: new[] { new Coord(4, 4, startingPos) }, preferredWrongAnswers: new[] { new Coord(4, 4, goalPos) }),
            makeQuestion(Question.AlgorithmiaPositions, module, formatArgs: new[] { "goal" }, correctAnswers: new[] { new Coord(4, 4, goalPos) }, preferredWrongAnswers: new[] { new Coord(4, 4, startingPos) }),
            makeQuestion(Question.AlgorithmiaColor, module, correctAnswers: new[] { color }),
            makeQuestion(Question.AlgorithmiaSeed, module, correctAnswers: seedVals.Select(x => x.ToString()).ToArray()));
    }
}