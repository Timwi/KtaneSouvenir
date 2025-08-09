using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Souvenir;
using Souvenir.Reflection;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessUltimateCipher(ModuleData module) => processColoredCiphers(module, "ultimateCipher", Question.UltimateCipherScreen);

    private IEnumerator<YieldInstruction> ProcessUltimateCycle(ModuleData module) => processSpeakingEvilCycle(module, "UltimateCycleScript", Question.UltimateCycleDialDirections, Question.UltimateCycleDialLabels);

    private IEnumerator<YieldInstruction> ProcessUltracube(ModuleData module) => processHypercubeUltracube(module, "TheUltracubeModule", Question.UltracubeRotations);

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

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < i + 3; j++)
            {
                var possibleWrong = possibleRotations[rotations[i][j].Count(ch => ch == ',')].SelectMany(x => x).ToArray();
                questions.Add(makeQuestion(rotations[i][j].Split(',').Length - 1 == 0 ? Question.UltraStoresSingleRotation : Question.UltraStoresMultiRotation, module, formatArgs: new[] { Ordinal(j + 1), Ordinal(i + 1) }, correctAnswers: new[] { rotations[i][j] }, preferredWrongAnswers: possibleWrong));
            }
        }
        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessUncoloredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "UncoloredSquaresModule");
        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.UncoloredSquaresFirstStage, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor1").Get().ToString() }),
            makeQuestion(Question.UncoloredSquaresFirstStage, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor2").Get().ToString() }));
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
            qs.Add(makeQuestion(Question.UncoloredSwitchesLedColors, module, formatArgs: new[] { Ordinal(ledIx + 1) }, correctAnswers: new[] { colorNames[ledColors[ledIx]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessUncolourFlash(ModuleData module)
    {
        var comp = GetComponent(module, "UCFScript");
        yield return WaitForSolve;

        var fldInitseq = GetArrayField<int[][]>(comp, "initseq").Get();
        var colornames = new[] { "Red", "Green", "Blue", "Yellow", "Magenta", "White" };

        var qs = new List<QandA>();
        for (var displayIx = 0; displayIx < 12; displayIx++)
            for (var yesno = 0; yesno < 2; yesno++)
                if (yesno != 0 || displayIx < 6)
                    for (var wordcolor = 0; wordcolor < 2; wordcolor++)
                        qs.Add(makeQuestion(Question.UncolourFlashDisplays, module, formatArgs: new[] { wordcolor == 0 ? "word" : "colour of the word", Ordinal(displayIx + 1), yesno == 0 ? "“YES”" : "“NO”" }, correctAnswers: new[] { colornames[fldInitseq[yesno][wordcolor][displayIx]] }));
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

    private IEnumerator<YieldInstruction> ProcessUnorderedKeys(ModuleData module)
    {
        var comp = GetComponent(module, "UnorderedKeysScript");
        var stages = new List<int[][]>(2);
        var fldResetCount = GetField<int>(comp, "resetCount");
        var fldInfo = GetArrayField<int[]>(comp, "info");
        var fldPressed = GetArrayField<bool>(comp, "alreadypressed");
        int[][] getInfo()
        {
            var info = fldInfo.Get(expectedLength: 6, validator: a => a.Length != 3 ? "expected inner array length of 3" : null).Select(ar => ar.ToArray()).ToArray();
            var pressed = fldPressed.Get(expectedLength: 7).ToArray();
            return info.Select((a, i) => pressed[i] ? null : a).ToArray();
        }
        stages.Add(getInfo());
        var resets = 0;

        while (module.Unsolved)
        {
            var newReset = fldResetCount.Get();
            if (newReset != resets)
            {
                if (newReset != resets + 1)
                    throw new AbandonModuleException($"I missed a stage (I noticed at stage {newReset})");
                resets = newReset;
                stages.Add(getInfo());
            }
            yield return null;
        }

        var colors = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };
        addQuestions(module, stages.Take(stages.Count - 1).SelectMany((stage, stageIx) => stage.SelectMany((key, keyIx) => key is null ? Enumerable.Empty<QandA>() : Ut.NewArray(
                makeQuestion(Question.UnorderedKeysKeyColor, module, OrderedKeysSprites[keyIx], formatArgs: new[] { Ordinal(stageIx + 1) }, correctAnswers: new[] { colors[key[0]] }),
                makeQuestion(Question.UnorderedKeysLabelColor, module, OrderedKeysSprites[keyIx], formatArgs: new[] { Ordinal(stageIx + 1) }, correctAnswers: new[] { colors[key[1]] }),
                makeQuestion(Question.UnorderedKeysLabel, module, OrderedKeysSprites[keyIx], formatArgs: new[] { Ordinal(stageIx + 1) }, correctAnswers: new[] { (key[2] + 1).ToString() })))));
    }

    private IEnumerator<YieldInstruction> ProcessUnownCipher(ModuleData module)
    {
        var comp = GetComponent(module, "UnownCipher");
        yield return WaitForSolve;

        var unownAnswer = GetArrayField<int>(comp, "letterIndexes").Get(expectedLength: 5, validator: v => v is < 0 or > 25 ? "expected 0–25" : null);
        addQuestions(module, unownAnswer.Select((ans, i) => makeQuestion(Question.UnownCipherAnswers, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { ((char) ('A' + ans)).ToString() })));
    }

    private IEnumerator<YieldInstruction> ProcessUpdog(ModuleData module)
    {
        var comp = GetComponent(module, "UpdogScript");
        yield return WaitForSolve;
        var word = GetField<string>(comp, "_souvenirWord").Get(v => Ut.Attributes[Question.UpdogWord].AllAnswers.Contains(v) ? null : $"Bad word {v}");
        var colors = GetArrayField<Color>(comp, "_souvenirColors").Get(expectedLength: 10);

        static string colorName(Color c) => (c.r, c.g, c.b) switch
        {
            (0.4f, 0.05f, 0.05f) => "Red",
            (0.4f, 0.3f, 0.05f) => "Orange",
            (0.5f, 0.5f, 0.05f) => "Yellow",
            (0.05f, 0.05f, 0.5f) => "Blue",
            (0.05f, 0.5f, 0.05f) => "Green",
            (0.5f, 0.05f, 0.5f) => "Purple",
            _ => throw new AbandonModuleException($"Unexpected color: {c.r}, {c.g}, {c.b}"),
        };

        var firstCol = colorName(colors[0]);
        var lastCol = colorName(colors[6]);

        addQuestions(module,
            makeQuestion(Question.UpdogWord, module, correctAnswers: new[] { word }),
            makeQuestion(Question.UpdogColor, module, correctAnswers: new[] { firstCol }, formatArgs: new[] { "first" }),
            makeQuestion(Question.UpdogColor, module, correctAnswers: new[] { lastCol }, formatArgs: new[] { "last" }));
    }

    private IEnumerator<YieldInstruction> ProcessUnpleasantSquares(ModuleData module)
    {
        var comp = GetComponent(module, "UnSqScript");
        yield return WaitForSolve;
        var subGrid = GetField<int[,]>(comp, "subgrid").Get();
        var colorNames = new string[] { "Red", "Yellow", "Jade", "Azure", "Violet", };

        var qs = new List<QandA>();
        for (var x = 0; x < 5; x++)
            for (var y = 0; y < 5; y++)
            {
                var p = x * 5 + y;
                if (p == 12)
                    continue;
                var coord = new Coord(5, 5, p);
                qs.Add(makeQuestion(Question.UnpleasantSquaresColor, module, questionSprite: Sprites.GenerateGridSprite(coord), correctAnswers: new[] { colorNames[subGrid[x, y]] }));
            }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessUSACycle(ModuleData module)
    {
        var comp = GetComponent(module, "USACycle");
        var fldStateIndices = GetListField<int>(comp, "StateIndexes");

        yield return WaitForSolve;

        var stateIndices = fldStateIndices.Get(minLength: 4).Where(ix => ix is not 5 and not 49).ToArray();

        //Colorado and Wyoming are practically indistinguishable
        addQuestion(module, Question.USACycleDisplayed,
            correctAnswers: stateIndices.Select(ix => USACycleSprites[ix]).ToArray(),
            preferredWrongAnswers: USACycleSprites.Where((_, pos) => pos is not 5 and not 49).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessUSAMaze(ModuleData module) => processWorldMaze(module, "USAMaze", Question.USAMazeOrigin);
}
