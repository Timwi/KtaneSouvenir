using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessQuaver(KMBombModule module)
    {
        var comp = GetComponent(module, "QuaverScript");
        var init = GetField<object>(comp, "init").Get();
        var fldSolved = GetField<bool>(init, "solved");
        var fldCorrectValues = GetListField<int[]>(init, "correctValues");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Quaver);

        var correctValues = fldCorrectValues.Get(minLength: 1, maxLength: 20, validator: arr => arr.Length != 1 && arr.Length != 4 ? "expected array of length 1 or 4" : null);
        var qs = new List<QandA>();

        for (var i = 0; i < correctValues.Count; i++)
        {
            var preferredWrongAnswers = new HashSet<string>();
            while (preferredWrongAnswers.Count < 6)
                preferredWrongAnswers.Add(correctValues[i].Select(x => Math.Max(x + Rnd.Range(-4, 5), 1)).JoinString(", "));
            qs.Add(makeQuestion(Question.QuaverArrows, _Quaver, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { correctValues[i].JoinString(", ") }, preferredWrongAnswers: preferredWrongAnswers.ToArray()));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessQuestionMark(KMBombModule module)
    {
        var comp = GetComponent(module, "Questionmark");

        var fldSolved = GetField<bool>(comp, "isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_QuestionMark);

        var flashedSpritePool = GetArrayField<int>(comp, "spritePool").Get(expectedLength: 4);
        addQuestion(module, Question.QuestionMarkFlashedSymbols, correctAnswers: flashedSpritePool.Select(ix => QuestionMarkSprites[ix]).ToArray(), preferredWrongAnswers: QuestionMarkSprites);
    }

    private IEnumerable<object> ProcessQuickArithmetic(KMBombModule module)
    {
        var comp = GetComponent(module, "QuickArithmetic");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_QuickArithmetic);

        var seqColors = GetArrayField<int>(comp, "ColorSequence").Get(expectedLength: 8);
        var primSeqDigits = GetArrayField<int>(comp, "LeftSequenceN").Get(expectedLength: 8);
        var secSeqDigits = GetArrayField<int>(comp, "RightSequence").Get(expectedLength: 8);
        var colorRef = new[] { "red", "blue", "green", "yellow", "white", "black", "orange", "pink", "purple", "cyan", "brown" };
        var allQuestions = new List<QandA>();
        for (var x = 0; x < 8; x++)
        {
            allQuestions.Add(makeQuestion(Question.QuickArithmeticColors, _QuickArithmetic, formatArgs: new[] { ordinal(x + 1) }, correctAnswers: new[] { colorRef[seqColors[x]] }, preferredWrongAnswers: colorRef));
            allQuestions.Add(makeQuestion(Question.QuickArithmeticPrimSecDigits, _QuickArithmetic, formatArgs: new[] { ordinal(x + 1), "primary" }, correctAnswers: new[] { primSeqDigits[x].ToString() }));
            allQuestions.Add(makeQuestion(Question.QuickArithmeticPrimSecDigits, _QuickArithmetic, formatArgs: new[] { ordinal(x + 1), "secondary" }, correctAnswers: new[] { secSeqDigits[x].ToString() }));
        }

        addQuestions(module, allQuestions);
    }

    private IEnumerable<object> ProcessQuintuples(KMBombModule module)
    {
        var comp = GetComponent(module, "quintuplesScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Quintuples);

        var numbers = GetArrayField<int>(comp, "cyclingNumbers", isPublic: true).Get(expectedLength: 25, validator: n => n < 1 || n > 10 ? "expected range 1–10" : null);
        var colors = GetArrayField<string>(comp, "chosenColorsName", isPublic: true).Get(expectedLength: 25);
        var colorCounts = GetArrayField<int>(comp, "numberOfEachColour", isPublic: true).Get(expectedLength: 5, validator: cc => cc < 0 || cc > 25 ? "expected range 0–25" : null);
        var colorNames = GetArrayField<string>(comp, "potentialColorsName", isPublic: true).Get(expectedLength: 5);

        addQuestions(module,
            numbers.Select((n, ix) => makeQuestion(Question.QuintuplesNumbers, _Quintuples, formatArgs: new[] { ordinal(ix % 5 + 1), ordinal(ix / 5 + 1) }, correctAnswers: new[] { (n % 10).ToString() })).Concat(
            colors.Select((color, ix) => makeQuestion(Question.QuintuplesColors, _Quintuples, formatArgs: new[] { ordinal(ix % 5 + 1), ordinal(ix / 5 + 1) }, correctAnswers: new[] { color }))).Concat(
            colorCounts.Select((cc, ix) => makeQuestion(Question.QuintuplesColorCounts, _Quintuples, formatArgs: new[] { colorNames[ix] }, correctAnswers: new[] { cc.ToString() }))));
    }
}