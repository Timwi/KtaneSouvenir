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
            qs.Add(makeQuestion(Question.QuaverArrows, _Quaver, new[] { ordinal(i + 1) }, correctAnswers: new[] { correctValues[i].JoinString(", ") }, preferredWrongAnswers: preferredWrongAnswers.ToArray()));
        }
        addQuestions(module, qs);
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
            numbers.Select((n, ix) => makeQuestion(Question.QuintuplesNumbers, _Quintuples, new[] { ordinal(ix % 5 + 1), ordinal(ix / 5 + 1) }, new[] { (n % 10).ToString() })).Concat(
            colors.Select((color, ix) => makeQuestion(Question.QuintuplesColors, _Quintuples, new[] { ordinal(ix % 5 + 1), ordinal(ix / 5 + 1) }, new[] { color }))).Concat(
            colorCounts.Select((cc, ix) => makeQuestion(Question.QuintuplesColorCounts, _Quintuples, new[] { colorNames[ix] }, new[] { cc.ToString() }))));
    }
}