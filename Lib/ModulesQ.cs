using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessQnA(ModuleData module)
    {
        var comp = GetComponent(module, "QnA");
        var allQs = new[] { "WHAT", "WHEN", "WHERE", "WHO", "HOW", "WHY" };
        var qs = GetArrayField<string>(comp, "questions").Get(expectedLength: 6, validator: s => allQs.Contains(s) ? null : "Unknown question phrase");

        yield return WaitForSolve;

        addQuestions(module, qs.Select((q, i) => makeQuestion(Question.QnAQuestions, module, correctAnswers: new[] { q }, formatArgs: new[] { Ordinal(i + 1) })));
    }

    private IEnumerator<YieldInstruction> ProcessQuantumPasswords(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "QuantumPasswordsScript");
        var words = GetArrayField<string>(comp, "selectedWords").Get(expectedLength: 2, validator: v => v is { Length: 5 } ? null : "Expected word length 5");

        bool isCorrect(string word) => words.Any(w => word.ToUpperInvariant().OrderBy(c => c).SequenceEqual(w.OrderBy(c => c)));
        addQuestion(module, Question.QuantumPasswordsWord, correctAnswers: Question.QuantumPasswordsWord.GetAnswers().Where(isCorrect).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessQuaver(ModuleData module)
    {
        var comp = GetComponent(module, "QuaverScript");
        var init = GetField<object>(comp, "init").Get();
        var fldCorrectValues = GetListField<int[]>(init, "correctValues");

        yield return WaitForSolve;

        var correctValues = fldCorrectValues.Get(minLength: 1, maxLength: 20, validator: arr => arr.Length != 1 && arr.Length != 4 ? "expected array of length 1 or 4" : null);
        var qs = new List<QandA>();

        for (var i = 0; i < correctValues.Count; i++)
        {
            var preferredWrongAnswers = new HashSet<string>();
            while (preferredWrongAnswers.Count < 6)
                preferredWrongAnswers.Add(correctValues[i].Select(x => Math.Max(x + Rnd.Range(-4, 5), 1)).JoinString(", "));
            qs.Add(makeQuestion(Question.QuaverArrows, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { correctValues[i].JoinString(", ") }, preferredWrongAnswers: preferredWrongAnswers.ToArray()));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessQuestionMark(ModuleData module)
    {
        var comp = GetComponent(module, "Questionmark");

        yield return WaitForSolve;

        var flashedSpritePool = GetArrayField<int>(comp, "spritePool").Get(expectedLength: 4);
        addQuestion(module, Question.QuestionMarkFlashedSymbols, correctAnswers: flashedSpritePool.Select(ix => QuestionMarkSprites[ix]).ToArray(), preferredWrongAnswers: QuestionMarkSprites);
    }

    private IEnumerator<YieldInstruction> ProcessQuickArithmetic(ModuleData module)
    {
        var comp = GetComponent(module, "QuickArithmetic");
        yield return WaitForSolve;

        var seqColors = GetArrayField<int>(comp, "ColorSequence").Get(expectedLength: 8);
        var primSeqDigits = GetArrayField<int>(comp, "LeftSequenceN").Get(expectedLength: 8);
        var secSeqDigits = GetArrayField<int>(comp, "RightSequence").Get(expectedLength: 8);
        var colorRef = new[] { "red", "blue", "green", "yellow", "white", "black", "orange", "pink", "purple", "cyan", "brown" };
        var allQuestions = new List<QandA>();
        for (var x = 0; x < 8; x++)
        {
            allQuestions.Add(makeQuestion(Question.QuickArithmeticColors, module, formatArgs: new[] { Ordinal(x + 1) }, correctAnswers: new[] { colorRef[seqColors[x]] }, preferredWrongAnswers: colorRef));
            allQuestions.Add(makeQuestion(Question.QuickArithmeticPrimSecDigits, module, formatArgs: new[] { Ordinal(x + 1), "primary" }, correctAnswers: new[] { primSeqDigits[x].ToString() }));
            allQuestions.Add(makeQuestion(Question.QuickArithmeticPrimSecDigits, module, formatArgs: new[] { Ordinal(x + 1), "secondary" }, correctAnswers: new[] { secSeqDigits[x].ToString() }));
        }

        addQuestions(module, allQuestions);
    }

    private IEnumerator<YieldInstruction> ProcessQuintuples(ModuleData module)
    {
        var comp = GetComponent(module, "quintuplesScript");
        yield return WaitForSolve;

        var numbers = GetArrayField<int>(comp, "cyclingNumbers", isPublic: true).Get(expectedLength: 25, validator: n => n < 1 || n > 10 ? "expected range 1–10" : null);
        var colors = GetArrayField<string>(comp, "chosenColorsName", isPublic: true).Get(expectedLength: 25);
        var colorCounts = GetArrayField<int>(comp, "numberOfEachColour", isPublic: true).Get(expectedLength: 5, validator: cc => cc < 0 || cc > 25 ? "expected range 0–25" : null);
        var colorNames = GetArrayField<string>(comp, "potentialColorsName", isPublic: true).Get(expectedLength: 5);

        addQuestions(module,
            numbers.Select((n, ix) => makeQuestion(Question.QuintuplesNumbers, module, formatArgs: new[] { Ordinal(ix % 5 + 1), Ordinal(ix / 5 + 1) }, correctAnswers: new[] { (n % 10).ToString() })).Concat(
            colors.Select((color, ix) => makeQuestion(Question.QuintuplesColors, module, formatArgs: new[] { Ordinal(ix % 5 + 1), Ordinal(ix / 5 + 1) }, correctAnswers: new[] { color }))).Concat(
            colorCounts.Select((cc, ix) => makeQuestion(Question.QuintuplesColorCounts, module, formatArgs: new[] { colorNames[ix] }, correctAnswers: new[] { cc.ToString() }))));
    }

    private IEnumerator<YieldInstruction> ProcessQuiplash(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "QLModule");
        var config = GetField<object>(comp, "ChosenConfig").Get();
        var id = GetField<string>(config, "id", true).Get(v => !Question.QuiplashNumber.GetAnswers().Contains(v) ? "Unexpected number" : null);

        foreach (var t in GetArrayField<TextMesh>(comp, "PromptTexts", true).Get())
            t.text = "Quiplash!";

        addQuestion(module, Question.QuiplashNumber, correctAnswers: new[] { id });
    }

    private IEnumerator<YieldInstruction> ProcessQuizBuzz(ModuleData module)
    {
        var comp = GetComponent(module, "quizBuzz");

        yield return WaitForSolve;

        var startingNumber = GetIntField(comp, "startNumber").Get(min: 6, max: 74);
        addQuestion(module, Question.QuizBuzzStartingNumber, correctAnswers: new[] { startingNumber.ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessQwirkle(ModuleData module)
    {
        var comp = GetComponent(module, "qwirkleScript");
        yield return WaitForSolve;

        var tilesPlaces = GetField<IList>(comp, "placed").Get(l => l.Count != 4 ? "expected length 4" : null);
        var tilesIndex = new int[4];

        for (int i = 0; i < 4; i++)
        {
            var colourIndex = GetIntField(tilesPlaces[i], "color", isPublic: true).Get(min: 0, max: 5);
            var shapeIndex = GetIntField(tilesPlaces[i], "shape", isPublic: true).Get(min: 0, max: 5);
            tilesIndex[i] = shapeIndex * 6 + colourIndex;
        }

        addQuestions(module,
            Enumerable.Range(0, 4).Select(tile => makeQuestion(Question.QwirkleTilesPlaced, module,
            formatArgs: new[] { Ordinal(tile + 1) }, correctAnswers: new[] { QwirkleSprites[tilesIndex[tile]] })));
    }
}
