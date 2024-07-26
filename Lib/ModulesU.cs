using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Souvenir;
using Souvenir.Reflection;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessUltimateCipher(ModuleData module)
    {
        return processColoredCiphers(module, "ultimateCipher", Question.UltimateCipherScreen, _UltimateCipher);
    }

    private IEnumerator<YieldInstruction> ProcessUltimateCycle(ModuleData module)
    {
        return processSpeakingEvilCycle2(module, "UltimateCycleScript", Question.UltimateCycleWord, _UltimateCycle);
    }

    private IEnumerator<YieldInstruction> ProcessUltracube(ModuleData module)
    {
        return processHypercubeUltracube(module, "TheUltracubeModule", Question.UltracubeRotations, _Ultracube);
    }

    private IEnumerator<YieldInstruction> ProcessUltraStores(ModuleData module)
    {
        var comp = GetComponent(module, "UltraStoresScript");
        var fldStage = GetIntField(comp, "stage");
        var fldRotations = GetListField<string>(comp, "rotlist");
        var possibleRotations = GetArrayField<List<string>[]>(comp, "rotations").Get();
        var prevStage = 0;

        var rotations = new List<List<string>> { fldRotations.Get().ToList() };

        while (module.Unsolved)
        {
            var nextStage = fldStage.Get();
            if (nextStage != prevStage)
            {
                rotations.Add(fldRotations.Get().ToList());
                prevStage = nextStage;
            }
            yield return new WaitForSeconds(.1f);
        }

        var questions = new List<QandA>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < i + 3; j++)
            {
                var possibleWrong = possibleRotations[rotations[i][j].Count(ch => ch == ',')].SelectMany(x => x).ToArray();
                questions.Add(makeQuestion(rotations[i][j].Split(',').Length - 1 == 0 ? Question.UltraStoresSingleRotation : Question.UltraStoresMultiRotation, module, formatArgs: new[] { ordinal(j + 1), ordinal(i + 1) }, correctAnswers: new[] { rotations[i][j] }, preferredWrongAnswers: possibleWrong));
            }
        }
        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessUncoloredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "UncoloredSquaresModule");
        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.UncoloredSquaresFirstStage, module, formatArgs: new[] { ordinal(1) }, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor1").Get().ToString() }),
            makeQuestion(Question.UncoloredSquaresFirstStage, module, formatArgs: new[] { ordinal(2) }, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor2").Get().ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessUncoloredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "UncoloredSwitches");
        var fldLedColors = GetField<StringBuilder>(comp, "LEDsColorsString");
        var switchState = GetField<StringBuilder>(comp, "Switches_Current_State").Get(str => str.Length != 5 ? "expected length 5" : null);
        var switchStates = Enumerable.Range(0, 5).Select(swIx => switchState[swIx] == '1').ToArray();

        yield return WaitForSolve;

        var colorNames = new[] { "red", "green", "blue", "turquoise", "orange", "purple", "white", "black" };
        var curLedColors = fldLedColors.Get(str => str.Length != 10 ? "expected length 10" : null);
        var ledColors = Enumerable.Range(0, 10).Select(ledIx => "RGBTOPWK".IndexOf(curLedColors[ledIx])).ToArray();

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.UncoloredSwitchesInitialState, module, correctAnswers: new[] { switchStates.Select(b => b ? 'Q' : 'R').JoinString() }));
        for (var ledIx = 0; ledIx < 10; ledIx++)
            qs.Add(makeQuestion(Question.UncoloredSwitchesLedColors, module, formatArgs: new[] { ordinal(ledIx + 1) }, correctAnswers: new[] { colorNames[ledColors[ledIx]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessUnfairCipher(ModuleData module)
    {
        var comp = GetComponent(module, "unfairCipherScript");
        yield return WaitForSolve;

        var instructions = GetArrayField<string>(comp, "Message").Get(expectedLength: 4);
        addQuestions(module,
            makeQuestion(Question.UnfairCipherInstructions, module, formatArgs: new[] { "first" }, correctAnswers: new[] { instructions[0] }),
            makeQuestion(Question.UnfairCipherInstructions, module, formatArgs: new[] { "second" }, correctAnswers: new[] { instructions[1] }),
            makeQuestion(Question.UnfairCipherInstructions, module, formatArgs: new[] { "third" }, correctAnswers: new[] { instructions[2] }),
            makeQuestion(Question.UnfairCipherInstructions, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { instructions[3] }));
    }

    private IEnumerator<YieldInstruction> ProcessUnfairsRevenge(ModuleData module)
    {
        var comp = GetComponent(module, "UnfairsRevengeHandler");
        yield return WaitForSolve;

        var instructions = GetListField<string>(comp, "splittedInstructions").Get(expectedLength: 4);
        addQuestions(module,
            makeQuestion(Question.UnfairsRevengeInstructions, module, formatArgs: new[] { "first" }, correctAnswers: new[] { instructions[0] }),
            makeQuestion(Question.UnfairsRevengeInstructions, module, formatArgs: new[] { "second" }, correctAnswers: new[] { instructions[1] }),
            makeQuestion(Question.UnfairsRevengeInstructions, module, formatArgs: new[] { "third" }, correctAnswers: new[] { instructions[2] }),
            makeQuestion(Question.UnfairsRevengeInstructions, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { instructions[3] }));
    }

    private IEnumerator<YieldInstruction> ProcessUnicode(ModuleData module)
    {
        var comp = GetComponent(module, "UnicodeScript");
        yield return WaitForSolve;

        PropertyInfo<string> propCode = null;
        var symbols = GetField<IEnumerable>(comp, "SelectedSymbols").Get().Cast<object>().Select(x => (propCode ??= GetProperty<string>(x, "Code", isPublic: true)).GetFrom(x)).ToList();

        if (symbols.Count != 4)
            throw new AbandonModuleException($"‘SelectedSymbols’ has unexpected length {symbols.Count} (expected 4).");

        addQuestions(module,
            makeQuestion(Question.UnicodeSortedAnswer, module, formatArgs: new[] { "first" }, correctAnswers: new[] { symbols[0] }),
            makeQuestion(Question.UnicodeSortedAnswer, module, formatArgs: new[] { "second" }, correctAnswers: new[] { symbols[1] }),
            makeQuestion(Question.UnicodeSortedAnswer, module, formatArgs: new[] { "third" }, correctAnswers: new[] { symbols[2] }),
            makeQuestion(Question.UnicodeSortedAnswer, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { symbols[3] }));
    }

    private IEnumerator<YieldInstruction> ProcessUNO(ModuleData module)
    {
        var comp = GetComponent(module, "UNO");
        var fldFirstInDeck = GetField<string>(comp, "firstInDeck");
        var mthGetUnoName = GetMethod<string>(comp, "better", 1);

        yield return WaitForSolve;

        addQuestion(module, Question.UnoInitialCard, correctAnswers: new string[] { titleCase(mthGetUnoName.Invoke(fldFirstInDeck.Get())) });
    }

    private IEnumerator<YieldInstruction> ProcessUnownCipher(ModuleData module)
    {
        var comp = GetComponent(module, "UnownCipher");
        yield return WaitForSolve;

        var unownAnswer = GetArrayField<int>(comp, "letterIndexes").Get(expectedLength: 5, validator: v => v < 0 || v > 25 ? "expected 0–25" : null);
        addQuestions(module, unownAnswer.Select((ans, i) => makeQuestion(Question.UnownCipherAnswers, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { ((char) ('A' + ans)).ToString() })));
    }

    private IEnumerator<YieldInstruction> ProcessUSACycle(ModuleData module)
    {
        var comp = GetComponent(module, "USACycle");
        var fldStateIndices = GetListField<int>(comp, "StateIndexes");

        yield return WaitForSolve;

        int[] stateIndices = fldStateIndices.Get(minLength: 4).Where(ix => ix != 5 && ix != 49).ToArray();

        //Colorado and Wyoming are practically indistinguishable
        addQuestion(module, Question.USACycleDisplayed,
            correctAnswers: stateIndices.Select(ix => USACycleSprites[ix]).ToArray(),
            preferredWrongAnswers: USACycleSprites.Where((_, pos) => pos != 5 && pos != 49).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessUSAMaze(ModuleData module)
    {
        return processWorldMaze(module, "USAMaze", _USAMaze, Question.USAMazeOrigin);
    }
}
