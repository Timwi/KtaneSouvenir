using System;
using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SZeroZero
{
    [SouvenirQuestion("Where was the {1} square in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, TranslateArguments = [true], Arguments = ["red", "green", "blue"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(6, 6)]
    Squares,

    [SouvenirQuestion("What color was the {1} star in {0}?", TwoColumns4Answers, "black", "blue", "green", "cyan", "red", "magenta", "yellow", "white", TranslateAnswers = true, Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    StarColors,

    [SouvenirQuestion("How many points were on the {1} star in {0}?", ThreeColumns6Answers, "2", "3", "4", "5", "6", "7", "8", Arguments = ["top-left", "top-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    StarPoints
}

public partial class SouvenirModule
{
    [SouvenirHandler("zeroZero", "Zero, Zero", typeof(SZeroZero), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessZeroZero(ModuleData module)
    {
        var comp = GetComponent(module, "ZeroZeroScript");
        yield return WaitForSolve;

        var redPos = GetField<int>(comp, "redPos").Get();
        var greenPos = GetField<int>(comp, "greenPos").Get();
        var bluePos = GetField<int>(comp, "bluePos").Get();

        var stars = GetField<Array>(comp, "stars").Get(validator: arr => arr.Length != 4 ? "expected length 4" : null);
        var fldChannels = GetArrayField<bool>(stars.GetValue(0), "channels");
        var fldPoints = GetIntField(stars.GetValue(0), "Points", isPublic: true);
        var gridSquares = GetArrayField<MeshRenderer>(comp, "gridColors", isPublic: true).Get(expectedLength: 49);

        Color? white = null;
        for (var i = 0; i < gridSquares.Length && white == null; i++)
            if (i != redPos && i != greenPos && i != bluePos)
                white = gridSquares[i].sharedMaterial.color;
        if (white == null)
            throw new AbandonModuleException("Could not find a square that is not red, green, or blue.");

        gridSquares[redPos].material.color = white.Value;
        gridSquares[greenPos].material.color = white.Value;
        gridSquares[bluePos].material.color = white.Value;

        yield return question(SZeroZero.Squares, args: ["red"]).Answers(new Coord(7, 7, redPos));
        yield return question(SZeroZero.Squares, args: ["green"]).Answers(new Coord(7, 7, greenPos));
        yield return question(SZeroZero.Squares, args: ["blue"]).Answers(new Coord(7, 7, bluePos));
        var positionNames = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };
        var colorNames = new[] { "black", "blue", "green", "cyan", "red", "magenta", "yellow", "white" };
        for (var starIx = 0; starIx < 4; starIx++)
        {
            var channels = fldChannels.GetFrom(stars.GetValue(starIx), expectedLength: 3);
            yield return question(SZeroZero.StarColors, args: [positionNames[starIx]]).Answers(colorNames[(channels[0] ? 4 : 0) + (channels[1] ? 2 : 0) + (channels[2] ? 1 : 0)]);
            var points = fldPoints.GetFrom(stars.GetValue(starIx), 2, 8);
            yield return question(SZeroZero.StarPoints, args: [positionNames[starIx]]).Answers(points.ToString());
        }
    }
}
