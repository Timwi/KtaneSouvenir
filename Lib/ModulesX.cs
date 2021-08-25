using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessXmORseCode(KMBombModule module)
    {
        var comp = GetComponent(module, "XmORseCode");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_XmORseCode);

        var displayLetters = GetArrayField<int>(comp, "displayed").Get(expectedLength: 5, validator: number => number < 0 || number > 25 ? "expected range 0–25" : null);
        var words = GetAnswers(Question.XmORseCodeWord);
        var answerWord = words[GetIntField(comp, "answer").Get(validator: number => number < 0 || number > 45 ? "expected range 0–45" : null)];

        var qs = new List<QandA>();
        for (int i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.XmORseCodeDisplayedLetters, _XmORseCode, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { alphabet.Substring(displayLetters[i], 1) }, preferredWrongAnswers: displayLetters.Select(x => alphabet.Substring(x, 1)).ToArray()));
        qs.Add(makeQuestion(Question.XmORseCodeWord, _XmORseCode, correctAnswers: new[] { answerWord }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessXenocryst(KMBombModule module)
    {
        var comp = GetComponent(module, "XenocrystScript");
        var fldSolved = GetField<bool>(comp, "Solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Xenocryst);

        var flashes = GetArrayField<int>(comp, "Outputs").Get();

        var qs = new List<QandA>();

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };
        for (int i = 0; i < 10; i++)
            qs.Add(makeQuestion(Question.Xenocryst, _Xenocryst,
                formatArgs: new[] { ordinal(i + 1) },
                correctAnswers: new[] { colorNames[flashes[i]] },
                preferredWrongAnswers: colorNames));

        addQuestions(module, qs);
    }
}