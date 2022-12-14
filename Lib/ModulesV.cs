using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessV(KMBombModule module)
    {
        var comp = GetComponent(module, "qkV");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_V);

        var allWords = GetArrayField<string>(comp, "allWords").Get();
        var currentWords = GetField<List<string>>(comp, "currentWords").Get();

        addQuestions(module,
           makeQuestion(Question.VWords, _V, formatArgs: new[] { "was" }, correctAnswers: currentWords.ToArray(), preferredWrongAnswers: allWords),
           makeQuestion(Question.VWords, _V, formatArgs: new[] { "was not" }, correctAnswers: allWords.Where(a => !currentWords.Contains(a)).ToArray(), preferredWrongAnswers: allWords));
    }

    private IEnumerable<object> ProcessVaricoloredSquares(KMBombModule module)
    {
        var comp = GetComponent(module, "VaricoloredSquaresModule");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_VaricoloredSquares);

        addQuestion(module, Question.VaricoloredSquaresInitialColor, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor").Get().ToString() });
    }

    private IEnumerable<object> ProcessVaricolourFlash(KMBombModule module)
    {
        var comp = GetComponent(module, "VCFScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldStage = GetIntField(comp, "stage");
        var fldGoal = GetArrayField<int>(comp, "sequence");

        var words = new int[4];
        var colors = new int[4];
        var names = new string[] { "Red", "Green", "Blue", "Magenta", "Yellow", "White" };
        while (!fldSolved.Get())
        {
            int s = fldStage.Get(min: 0, max: 5);
            if (s < 4)
            {
                var goal = fldGoal.Get(expectedLength: 5)[4];
                if (goal < 0 || goal >= 36)
                    throw new AbandonModuleException("‘sequence[4]’ has value {0} (expected 0–35)", goal);
                words[s] = goal / 6;
                colors[s] = goal % 6;
            }
            yield return new WaitForSeconds(0.1f);
        }

        _modulesSolved.IncSafe(_VaricolourFlash);

        var qs = new List<QandA>();
        qs.AddRange(words.Select((val, ix) => makeQuestion(Question.VaricolourFlashWords, _VaricolourFlash, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { names[val] })));
        qs.AddRange(colors.Select((val, ix) => makeQuestion(Question.VaricolourFlashColors, _VaricolourFlash, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { names[val] })));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessVcrcs(KMBombModule module)
    {
        var comp = GetComponent(module, "VcrcsScript");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");

        var wordTextMesh = GetField<TextMesh>(comp, "Words", isPublic: true).Get();
        var word = wordTextMesh.text;
        if (word == null)
            throw new AbandonModuleException("‘Words.text’ is null.");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Vcrcs);

        addQuestion(module, Question.VcrcsWord, correctAnswers: new[] { word });
    }

    private IEnumerable<object> ProcessVectors(KMBombModule module)
    {
        var comp = GetComponent(module, "VectorsScript");

        // After moduleSolved is set to true, the module still performs an animation before it actually marks as solved.
        // Therefore, we use OnPass to wait for it to be solved
        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Vectors);

        var colorsName = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple" };
        var vectorCount = GetIntField(comp, "vectorct").Get(min: 1, max: 3);
        var colors = GetArrayField<string>(comp, "colors").Get(expectedLength: 24, nullContentAllowed: true);
        var pickedVectors = GetArrayField<int>(comp, "vectorsPicked").Get(expectedLength: 3, validator: v => v < 0 || v >= colors.Length ? string.Format("expected range 0–{0}", colors.Length - 1) : null);
        var nullIx = pickedVectors.Take(vectorCount).IndexOf(ix => colors[ix] == null);
        if (nullIx != -1)
            throw new AbandonModuleException("‘colors[{0}]’ was null; ‘pickedVectors’ = [{1}]", pickedVectors[nullIx], pickedVectors.JoinString(", "));

        for (int i = 0; i < vectorCount; i++)
            if (!colorsName.Contains(colors[pickedVectors[i]]))
                throw new AbandonModuleException("‘colors[{1}]’ pointed to illegal color “{2}” (colors=[{3}], pickedVectors=[{4}], index {0}).",
                    i, pickedVectors[i], colors[pickedVectors[i]], colors.JoinString(", "), pickedVectors.JoinString(", "));

        var qs = new List<QandA>();
        for (int i = 0; i < vectorCount; i++)
            qs.Add(makeQuestion(Question.VectorsColors, _Vectors, formatArgs: new[] { vectorCount == 1 ? "only" : ordinal(i + 1) }, correctAnswers: new[] { colors[pickedVectors[i]] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessVexillology(KMBombModule module)
    {
        var comp = GetComponent(module, "vexillologyScript");
        var fldSolved = GetField<bool>(comp, "_issolved");

        string[] colors = GetArrayField<string>(comp, "coloursStrings").Get();
        int color1 = GetIntField(comp, "ActiveFlagTop1").Get(min: 0, max: colors.Length - 1);
        int color2 = GetIntField(comp, "ActiveFlagTop2").Get(min: 0, max: colors.Length - 1);
        int color3 = GetIntField(comp, "ActiveFlagTop3").Get(min: 0, max: colors.Length - 1);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Vexillology);

        addQuestions(module,
            makeQuestion(Question.VexillologyColors, _Vexillology, formatArgs: new[] { "first" }, correctAnswers: new[] { colors[color1] }, preferredWrongAnswers: new[] { colors[color2], colors[color3] }),
            makeQuestion(Question.VexillologyColors, _Vexillology, formatArgs: new[] { "second" }, correctAnswers: new[] { colors[color2] }, preferredWrongAnswers: new[] { colors[color1], colors[color3] }),
            makeQuestion(Question.VexillologyColors, _Vexillology, formatArgs: new[] { "third" }, correctAnswers: new[] { colors[color3] }, preferredWrongAnswers: new[] { colors[color2], colors[color1] }));
    }

    private IEnumerable<object> ProcessVioletCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "violetCipher", Question.VioletCipherAnswer, _VioletCipher);
    }

    private IEnumerable<object> ProcessVisualImpairment(KMBombModule module)
    {
        var comp = GetComponent(module, "VisualImpairment");
        var fldRoundsFinished = GetIntField(comp, "roundsFinished");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldColor = GetIntField(comp, "color");
        var fldPicture = GetArrayField<string>(comp, "picture");

        // Wait for the first picture to be assigned
        while (fldPicture.Get(nullAllowed: true) == null)
            yield return new WaitForSeconds(.1f);

        var stageCount = GetIntField(comp, "stageCount").Get(min: 2, max: 3);
        var colorsPerStage = new int[stageCount];
        var colorNames = new[] { "Blue", "Green", "Red", "White" };

        while (!fldSolved.Get())
        {
            var newStage = fldRoundsFinished.Get();
            if (newStage >= stageCount)
                break;

            var newColor = fldColor.Get(min: 0, max: 3);
            colorsPerStage[newStage] = newColor;
            yield return new WaitForSeconds(.1f);
        }
        _modulesSolved.IncSafe(_VisualImpairment);

        addQuestions(module, colorsPerStage.Select((col, ix) => makeQuestion(Question.VisualImpairmentColors, _VisualImpairment, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colorNames[col] })));
    }
}