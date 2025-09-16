using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SEncryptedMaze
{
    [SouvenirQuestion("Which symbol on {0} was spinning {1}?", ThreeColumns6Answers, "f", "H", "$", "l", "B", "N", "g", "I", "%", "m", "C", "O", "h", "J", "&", "n", "D", "P", "i", "K", "'", "o", "E", "Q", "j", "L", "(", "p", "F", "R", Type = AnswerType.DynamicFont, Arguments = ["clockwise", "counter-clockwise"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Symbols
}

public partial class SouvenirModule
{
    [SouvenirHandler("encryptedMaze", "Encrypted Maze", typeof(SEncryptedMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessEncryptedMaze(ModuleData module)
    {
        var comp = GetComponent(module, "encryptedMazeScript");
        var shapeCw = GetIntField(comp, "shapeMarkerCw").Get(0, 4);
        var shapeCcw = GetIntField(comp, "shapeMarkerCcw").Get(0, 4);
        var markerCw = GetIntField(comp, "featureMarkerCw").Get(0, 5);
        var markerCcw = GetIntField(comp, "featureMarkerCcw").Get(0, 5);
        var markerCharacters = GetField<string[,]>(comp, "markerIndex").Get(validator: arr => arr.GetLength(0) != 5 ? "expected length 5 in dimension 0" : arr.GetLength(1) != 6 ? "expected length 6 in dimension 1" : null);

        yield return WaitForSolve;

        var textMesh = GetArrayField<TextMesh>(comp, "mazeIndex", isPublic: true).Get(expectedLength: 36)[0];
        var info = new TextAnswerInfo(font: textMesh.font, fontTexture: textMesh.GetComponent<MeshRenderer>().sharedMaterial.mainTexture);
        crashPlease_PUT_INFO_IN();
        yield return question(SEncryptedMaze.Symbols, args: ["clockwise"]).Answers(markerCharacters[shapeCw, markerCw], preferredWrong: [markerCharacters[shapeCcw, markerCcw]]);
        yield return question(SEncryptedMaze.Symbols, args: ["counter-clockwise"]).Answers(markerCharacters[shapeCcw, markerCcw], preferredWrong: [markerCharacters[shapeCw, markerCw]]);
    }
}
