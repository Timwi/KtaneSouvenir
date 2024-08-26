using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessXenocryst(ModuleData module)
    {
        var comp = GetComponent(module, "XenocrystScript");
        yield return WaitForSolve;

        var flashes = GetArrayField<int>(comp, "Outputs").Get();

        var qs = new List<QandA>();

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet" };
        for (int i = 0; i < 10; i++)
            qs.Add(makeQuestion(Question.Xenocryst, module,
                formatArgs: new[] { ordinal(i + 1) },
                correctAnswers: new[] { colorNames[flashes[i]] },
                preferredWrongAnswers: colorNames));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessXmORseCode(ModuleData module)
    {
        var comp = GetComponent(module, "XmORseCode");

        var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        yield return WaitForSolve;

        var displayLetters = GetArrayField<int>(comp, "displayed").Get(expectedLength: 5, validator: number => number < 0 || number > 25 ? "expected range 0–25" : null);
        var words = GetAnswers(Question.XmORseCodeWord);
        var answerWord = words[GetIntField(comp, "answer").Get(validator: number => number < 0 || number > 45 ? "expected range 0–45" : null)];

        var qs = new List<QandA>();
        for (int i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.XmORseCodeDisplayedLetters, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { alphabet.Substring(displayLetters[i], 1) }, preferredWrongAnswers: displayLetters.Select(x => alphabet.Substring(x, 1)).ToArray()));
        qs.Add(makeQuestion(Question.XmORseCodeWord, module, correctAnswers: new[] { answerWord }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessXobekuJehT(ModuleData module)
    {
        var comp = GetComponent(module, "tpircSxobekuJ");
        yield return WaitForSolve;

        var songIx = GetIntField(comp, "songselect").Get();
        var songList = GetArrayField<string>(comp, "titles").Get();

        addQuestion(module, Question.XobekuJehTSong, correctAnswers: new[] { songList[songIx] }, preferredWrongAnswers: songList);
    }

    private IEnumerator<YieldInstruction> ProcessXRing(ModuleData module)
    {
        var comp = GetComponent(module, "XRingScript");
        yield return WaitForSolve;

        var used = GetArrayField<int>(comp, "symbselect").Get(expectedLength: 5, validator: v => v is < 0 or > 63 ? "expected symbol index 0-63" : null);
        addQuestion(module, Question.XRingSymbol, correctAnswers: used.Select(i => XRingSprites[i]).ToArray());
    }
}
