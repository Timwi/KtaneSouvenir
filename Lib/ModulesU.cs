using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Souvenir;
using Souvenir.Reflection;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessUltimateCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "ultimateCipher", Question.UltimateCipherScreen, _UltimateCipher);
    }

    private IEnumerable<object> ProcessUltimateCycle(KMBombModule module)
    {
        return processSpeakingEvilCycle2(module, "UltimateCycleScript", Question.UltimateCycleWord, _UltimateCycle);
    }

    private IEnumerable<object> ProcessUltracube(KMBombModule module)
    {
        return processHypercubeUltracube(module, "TheUltracubeModule", Question.UltracubeRotations, _Ultracube);
    }

    private IEnumerable<object> ProcessUltraStores(KMBombModule module)
    {
        var comp = GetComponent(module, "UltraStoresScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldStage = GetIntField(comp, "stage");
        var fldRotations = GetListField<string>(comp, "rotlist");
        var possibleRotations = GetArrayField<List<string>[]>(comp, "rotations").Get();
        var prevStage = 0;

        var rotations = new List<List<string>> { fldRotations.Get().ToList() };

        while (!fldSolved.Get())
        {
            var nextStage = fldStage.Get();
            if (nextStage != prevStage)
            {
                rotations.Add(fldRotations.Get().ToList());
                prevStage = nextStage;
            }
            yield return new WaitForSeconds(.1f);
        }
        _modulesSolved.IncSafe(_UltraStores);

        var questions = new List<QandA>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < i + 3; j++)
            {
                var possibleWrong = possibleRotations[rotations[i][j].Count(ch => ch == ',')].SelectMany(x => x).ToArray();
                questions.Add(makeQuestion(rotations[i][j].Split(',').Length - 1 == 0 ? Question.UltraStoresSingleRotation : Question.UltraStoresMultiRotation, _UltraStores, formatArgs: new[] { ordinal(j + 1), ordinal(i + 1) }, correctAnswers: new[] { rotations[i][j] }, preferredWrongAnswers: possibleWrong));
            }
        }
        addQuestions(module, questions);
    }

    private IEnumerable<object> ProcessUncoloredSquares(KMBombModule module)
    {
        var comp = GetComponent(module, "UncoloredSquaresModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_UncoloredSquares);
        addQuestions(module,
            makeQuestion(Question.UncoloredSquaresFirstStage, _UncoloredSquares, formatArgs: new[] { "first" }, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor1").Get().ToString() }),
            makeQuestion(Question.UncoloredSquaresFirstStage, _UncoloredSquares, formatArgs: new[] { "second" }, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor2").Get().ToString() }));
    }

    private IEnumerable<object> ProcessUncoloredSwitches(KMBombModule module)
    {
        var comp = GetComponent(module, "UncoloredSwitches");
        var fldLedColors = GetField<StringBuilder>(comp, "LEDsColorsString");
        var switchState = GetField<StringBuilder>(comp, "Switches_Current_State").Get(str => str.Length != 5 ? "expected length 5" : null);
        var switchStates = Enumerable.Range(0, 5).Select(swIx => switchState[swIx] == '1').ToArray();

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_UncoloredSwitches);

        var colorNames = new[] { "red", "green", "blue", "turquoise", "orange", "purple", "white", "black" };
        var curLedColors = fldLedColors.Get(str => str.Length != 10 ? "expected length 10" : null);
        var ledColors = Enumerable.Range(0, 10).Select(ledIx => "RGBTOPWK".IndexOf(curLedColors[ledIx])).ToArray();

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.UncoloredSwitchesInitialState, _UncoloredSwitches, correctAnswers: new[] { switchStates.Select(b => b ? 'Q' : 'R').JoinString() }));
        for (var ledIx = 0; ledIx < 10; ledIx++)
            qs.Add(makeQuestion(Question.UncoloredSwitchesLedColors, _UncoloredSwitches, formatArgs: new[] { ordinal(ledIx + 1) }, correctAnswers: new[] { colorNames[ledColors[ledIx]] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessUnfairCipher(KMBombModule module)
    {
        var comp = GetComponent(module, "unfairCipherScript");
        var fldSolved = GetField<bool>(comp, "solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_UnfairCipher);

        var instructions = GetArrayField<string>(comp, "Message").Get(expectedLength: 4);
        addQuestions(module,
            makeQuestion(Question.UnfairCipherInstructions, _UnfairCipher, formatArgs: new[] { "first" }, correctAnswers: new[] { instructions[0] }),
            makeQuestion(Question.UnfairCipherInstructions, _UnfairCipher, formatArgs: new[] { "second" }, correctAnswers: new[] { instructions[1] }),
            makeQuestion(Question.UnfairCipherInstructions, _UnfairCipher, formatArgs: new[] { "third" }, correctAnswers: new[] { instructions[2] }),
            makeQuestion(Question.UnfairCipherInstructions, _UnfairCipher, formatArgs: new[] { "fourth" }, correctAnswers: new[] { instructions[3] }));
    }

    private IEnumerable<object> ProcessUnfairsRevenge(KMBombModule module)
    {
        var comp = GetComponent(module, "UnfairsRevengeHandler");
        var fldSolved = GetField<bool>(comp, "isFinished");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_UnfairsRevenge);

        var instructions = GetListField<string>(comp, "splittedInstructions").Get(expectedLength: 4);
        addQuestions(module,
            makeQuestion(Question.UnfairsRevengeInstructions, _UnfairsRevenge, formatArgs: new[] { "first" }, correctAnswers: new[] { instructions[0] }),
            makeQuestion(Question.UnfairsRevengeInstructions, _UnfairsRevenge, formatArgs: new[] { "second" }, correctAnswers: new[] { instructions[1] }),
            makeQuestion(Question.UnfairsRevengeInstructions, _UnfairsRevenge, formatArgs: new[] { "third" }, correctAnswers: new[] { instructions[2] }),
            makeQuestion(Question.UnfairsRevengeInstructions, _UnfairsRevenge, formatArgs: new[] { "fourth" }, correctAnswers: new[] { instructions[3] }));
    }

    private IEnumerable<object> ProcessUnicode(KMBombModule module)
    {
        var comp = GetComponent(module, "UnicodeScript");
        var solved = false;
        module.OnPass += delegate { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Unicode);

        PropertyInfo<string> propCode = null;
        var symbols = GetField<IEnumerable>(comp, "SelectedSymbols").Get().Cast<object>().Select(x => (propCode ??= GetProperty<string>(x, "Code", isPublic: true)).GetFrom(x)).ToList();

        if (symbols.Count != 4)
            throw new AbandonModuleException("‘SelectedSymbols’ has an unexpected length, length: {0} (expected 4).", symbols.Count);

        addQuestions(module,
            makeQuestion(Question.UnicodeSortedAnswer, _Unicode, formatArgs: new[] { "first" }, correctAnswers: new[] { symbols[0] }),
            makeQuestion(Question.UnicodeSortedAnswer, _Unicode, formatArgs: new[] { "second" }, correctAnswers: new[] { symbols[1] }),
            makeQuestion(Question.UnicodeSortedAnswer, _Unicode, formatArgs: new[] { "third" }, correctAnswers: new[] { symbols[2] }),
            makeQuestion(Question.UnicodeSortedAnswer, _Unicode, formatArgs: new[] { "fourth" }, correctAnswers: new[] { symbols[3] }));
    }

    private IEnumerable<object> ProcessUNO(KMBombModule module)
    {
        var comp = GetComponent(module, "UNO");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_UNO);

        var allAnswers = GetListField<string>(comp, "nicerNames").Get();
        var firstInDeck = GetField<string>(comp, "firstInDeck").Get();
        var GetUnoNameMethod = GetMethod<string>(comp, "better", 1);
        var answer = titleCase(GetUnoNameMethod.Invoke(firstInDeck));

        addQuestion(module, Question.UnoInitialCard, correctAnswers: new string[] { answer },preferredWrongAnswers: allAnswers.ToArray());
    }

    private IEnumerable<object> ProcessUnownCipher(KMBombModule module)
    {
        var comp = GetComponent(module, "UnownCipher");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_UnownCipher);
        
        var unownAnswer = GetArrayField<int>(comp, "letterIndexes").Get(expectedLength: 5, validator: v => v < 0 || v > 25 ? "expected 0–25" : null);
        addQuestions(module, unownAnswer.Select((ans, i) => makeQuestion(Question.UnownCipherAnswers, _UnownCipher, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { ((char) ('A' + ans)).ToString() })));
    }

    private IEnumerable<object> ProcessUSACycle(KMBombModule module)
    {
        var comp = GetComponent(module, "USACycle");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");
        var fldStateIndices = GetListField<int>(comp, "StateIndexes");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_USACycle);

        int[] stateIndices = fldStateIndices.Get(minLength: 4).Where(ix => ix != 5 && ix != 49).ToArray();

        //Colorado and Wyoming are practically indistinguishable
        addQuestion(module, Question.USACycleDisplayed,
            correctAnswers: stateIndices.Select(ix => USACycleSprites[ix]).ToArray(),
            preferredWrongAnswers: USACycleSprites.Where((_, pos) => pos != 5 && pos != 49).ToArray());
    }

    private IEnumerable<object> ProcessUSAMaze(KMBombModule module)
    {
        return processWorldMaze(module, "USAMaze", _USAMaze, Question.USAMazeOrigin);
    }
}