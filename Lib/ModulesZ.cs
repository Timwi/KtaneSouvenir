using System;
using System.Collections.Generic;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessZeroZero(KMBombModule module)
    {
        var comp = GetComponent(module, "ZeroZeroScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ZeroZero);

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

        var qs = new List<QandA>
        {
            makeQuestion(Question.ZeroZeroSquares, _ZeroZero, formatArgs: new[] { "red" }, correctAnswers: new[] { new Coord(7, 7, redPos) }),
            makeQuestion(Question.ZeroZeroSquares, _ZeroZero, formatArgs: new[] { "green" }, correctAnswers: new[] { new Coord(7, 7, greenPos) }),
            makeQuestion(Question.ZeroZeroSquares, _ZeroZero, formatArgs: new[] { "blue" }, correctAnswers: new[] { new Coord(7, 7, bluePos) })
        };
        var positionNames = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };
        var colorNames = new[] { "black", "blue", "green", "cyan", "red", "magenta", "yellow", "white" };
        for (var starIx = 0; starIx < 4; starIx++)
        {
            var channels = fldChannels.GetFrom(stars.GetValue(starIx), expectedLength: 3);
            qs.Add(makeQuestion(Question.ZeroZeroStarColors, _ZeroZero, formatArgs: new[] { positionNames[starIx] }, correctAnswers: new[] { colorNames[(channels[0] ? 4 : 0) + (channels[1] ? 2 : 0) + (channels[2] ? 1 : 0)] }));
            var points = fldPoints.GetFrom(stars.GetValue(starIx), 2, 8);
            qs.Add(makeQuestion(Question.ZeroZeroStarPoints, _ZeroZero, formatArgs: new[] { positionNames[starIx] }, correctAnswers: new[] { points.ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessZoni(KMBombModule module)
    {
        var comp = GetComponent(module, "ZoniModuleScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldIndex = GetIntField(comp, "wordIndex");
        var fldStage = GetIntField(comp, "solvedStages");

        var buttons = GetArrayField<KMSelectable>(comp, "buttons", isPublic: true).Get();
        var words = GetArrayField<string>(comp, "wordlist", isPublic: true).Get();
        var index = fldIndex.Get(0, words.Length - 1);
        var stage = fldStage.Get(v => v != 0 ? "‘solvedStages’ did not start at 0" : null);

        var wordsAnswered = new List<int>();
        for (var i = 0; i < buttons.Length; i++)
        {
            var prevInteract = buttons[i].OnInteract;
            buttons[i].OnInteract = delegate
            {
                var ret = prevInteract();
                var st = fldStage.Get();
                if (stage != st)  // If they are equal, the user got a strike
                {
                    wordsAnswered.Add(index);
                    stage = st;
                }
                index = fldIndex.Get();
                return ret;
            };
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Zoni);

        if (wordsAnswered.Count != 3)
            throw new AbandonModuleException($"The received number of valid words was not 3: was {wordsAnswered.Count}.");

        var textbox = GetField<TextMesh>(comp, "textBox", isPublic: true).Get();
        var font = textbox.font;
        var fontTexture = textbox.GetComponent<MeshRenderer>().sharedMaterial.mainTexture;

        addQuestions(module,
            makeQuestion(Question.ZoniWords, _Zoni, formatArgs: new[] { "first" }, font: font, fontTexture: fontTexture, correctAnswers: new[] { words[wordsAnswered[0]] }, preferredWrongAnswers: words),
            makeQuestion(Question.ZoniWords, _Zoni, formatArgs: new[] { "second" }, font: font, fontTexture: fontTexture, correctAnswers: new[] { words[wordsAnswered[1]] }, preferredWrongAnswers: words),
            makeQuestion(Question.ZoniWords, _Zoni, formatArgs: new[] { "third" }, font: font, fontTexture: fontTexture, correctAnswers: new[] { words[wordsAnswered[2]] }, preferredWrongAnswers: words));
    }
}
