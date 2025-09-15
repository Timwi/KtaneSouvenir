using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SDiscoloredSquares
{
    [SouvenirQuestion("What was {1}’s remembered position in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, TranslateArguments = [true], Arguments = ["Blue", "Red", "Yellow", "Green", "Magenta"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(4, 4)]
    RememberedPositions
}

public partial class SouvenirModule
{
    [SouvenirHandler("DiscoloredSquaresModule", "Discolored Squares", typeof(SDiscoloredSquares), "luisdiogo98")]
    private IEnumerator<SouvenirInstruction> ProcessDiscoloredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "DiscoloredSquaresModule");
        yield return WaitForSolve;

        var colorsRaw = GetField<Array>(comp, "_rememberedColors").Get(arr => arr.Length != 4 ? "expected length 4" : null);
        var positions = GetArrayField<int>(comp, "_rememberedPositions").Get(expectedLength: 4);
        var colors = colorsRaw.Cast<object>().Select(obj => obj.ToString()).ToArray();

        addQuestions(module, Enumerable.Range(0, 4).Select(color =>
            makeQuestion(Question.DiscoloredSquaresRememberedPositions, module, formatArgs: new[] { colors[color] }, correctAnswers: new[] { new Coord(4, 4, positions[color]) })));
    }
}