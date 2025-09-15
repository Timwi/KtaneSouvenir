using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGridLock
{
    [SouvenirQuestion("What was the starting color in {0}?", TwoColumns4Answers, "Green", "Yellow", "Red", "Blue", TranslateAnswers = true)]
    StartingColor,

    [SouvenirQuestion("What was the starting location in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    StartingLocation,

    [SouvenirQuestion("What was the ending location in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    EndingLocation
}

public partial class SouvenirModule
{
    [SouvenirHandler("GridlockModule", "Gridlock", typeof(SGridLock), "CaitSith2")]
    private IEnumerator<SouvenirInstruction> ProcessGridLock(ModuleData module)
    {
        var comp = GetComponent(module, "GridlockModule");

        var colors = Question.GridLockStartingColor.GetAnswers();

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var solution = GetIntField(comp, "_solution").Get(min: 0, max: 15);
        var pages = GetArrayField<int[]>(comp, "_pages").Get(minLength: 5, maxLength: 10, validator: p => p.Length != 16 ? "expected length 16" : p.Any(q => q < 0 || (q & 15) > 12 || (q & (15 << 4)) > (4 << 4)) ? "unexpected value" : null);
        var start = pages[0].IndexOf(i => (i & 15) == 4);

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.GridLockStartingLocation, module, correctAnswers: new[] { new Coord(4, 4, start) }),
            makeQuestion(Question.GridLockEndingLocation, module, correctAnswers: new[] { new Coord(4, 4, solution) }),
            makeQuestion(Question.GridLockStartingColor, module, correctAnswers: new[] { colors[(pages[0][start] >> 4) - 1] }));
    }
}