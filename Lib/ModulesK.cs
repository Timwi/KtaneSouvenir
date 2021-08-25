using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessKudosudoku(KMBombModule module)
    {
        var comp = GetComponent(module, "KudosudokuModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var shown = GetArrayField<bool>(comp, "_shown").Get(expectedLength: 16).ToArray();  // Take a copy of the array because the module changes it

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Kudosudoku);

        addQuestions(module,
            makeQuestion(Question.KudosudokuPrefilled, _Kudosudoku, new[] { "pre-filled" },
                preferredWrongAnswers: Tiles4x4Sprites,
                correctAnswers: Enumerable.Range(0, 16).Where(ix => shown[ix]).Select(coord => Tiles4x4Sprites[coord]).ToArray()),
            makeQuestion(Question.KudosudokuPrefilled, _Kudosudoku, new[] { "not pre-filled" },
                preferredWrongAnswers: Tiles4x4Sprites,
                correctAnswers: Enumerable.Range(0, 16).Where(ix => !shown[ix]).Select(coord => Tiles4x4Sprites[coord]).ToArray()));
    }
}