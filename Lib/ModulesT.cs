using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessTapCode(ModuleData module)
    {
        var comp = GetComponent(module, "TapCodeScript");
        yield return WaitForSolve;

        var words = GetStaticField<string[]>(comp.GetType(), "_wordList").Get();
        var chosenWord = GetField<string>(comp, "_chosenWord").Get(str => !words.Contains(str) ? $"word is not in list: {words.JoinString(", ")}" : null);
        var w = words.Select(i => i.Substring(0, 1).ToUpperInvariant() + i.Substring(1).ToLowerInvariant()).ToArray();
        var cw = chosenWord.Substring(0, 1).ToUpperInvariant() + chosenWord.Substring(1).ToLowerInvariant();
        addQuestion(module, Question.TapCodeReceivedWord, correctAnswers: new[] { cw }, preferredWrongAnswers: w);
    }

    private IEnumerator<YieldInstruction> ProcessTashaSqueals(ModuleData module)
    {
        var comp = GetComponent(module, "tashaSquealsScript");

        var colors = GetStaticField<string[]>(comp.GetType(), "colorNames").Get(ar => ar.Length != 4 ? "expected length 4" : null).ToArray();
        var sequence = GetArrayField<int>(comp, "flashing").Get(expectedLength: 5, validator: val => val < 0 || val >= colors.Length ? $"expected range 0–{colors.Length - 1}" : null);

        for (int i = 0; i < colors.Length; i++)
            colors[i] = char.ToUpperInvariant(colors[i][0]) + colors[i].Substring(1);

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.TashaSquealsColors, module, formatArgs: new[] { "first" }, correctAnswers: new[] { colors[sequence[0]] }),
            makeQuestion(Question.TashaSquealsColors, module, formatArgs: new[] { "second" }, correctAnswers: new[] { colors[sequence[1]] }),
            makeQuestion(Question.TashaSquealsColors, module, formatArgs: new[] { "third" }, correctAnswers: new[] { colors[sequence[2]] }),
            makeQuestion(Question.TashaSquealsColors, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { colors[sequence[3]] }),
            makeQuestion(Question.TashaSquealsColors, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { colors[sequence[4]] }));
    }

    private IEnumerator<YieldInstruction> ProcessTasqueManaging(ModuleData module)
    {
        var comp = GetComponent(module, "tasqueManaging");
        yield return WaitForSolve;
        addQuestion(module, Question.TasqueManagingStartingPos,
            correctAnswers: new[] { TasqueManagingSprites[GetIntField(comp, "startingPosition").Get(min: 0, max: 15)] },
            preferredWrongAnswers: TasqueManagingSprites);
    }

    private IEnumerator<YieldInstruction> ProcessTeaSet(ModuleData module)
    {
        var comp = GetComponent(module, "TeaSetScript");

        yield return WaitForSolve;

        var displayedIngredients = GetListField<int>(comp, "Order").Get(expectedLength: 8);
        addQuestions(module, displayedIngredients.Select((ing, ix) => makeQuestion(Question.TeaSetDisplayedIngredients, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { TeaSetSprites[ing] }, preferredWrongAnswers: TeaSetSprites)));
    }

    private IEnumerator<YieldInstruction> ProcessTechnicalKeypad(ModuleData module)
    {
        var comp = GetComponent(module, "TechnicalKeypadModule");
        var digits = GetProperty<string>(GetField<object>(comp, "_keypadInfo").Get(), "Digits", isPublic: true).Get(seq => seq.Length != 12 ? "expected length 12" : null);

        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (int position = 0; position < 12; position++)
        {
            var tex = TechnicalKeypadQuestions[position];
            var tmp = new Texture2D(400, 320, TextureFormat.ARGB32, false);

            tmp.SetPixels(tex.GetPixels());
            tex = TechnicalKeypadQuestions.First(t => t.name.Equals("name"));
            tmp.SetPixels(40, 90, tex.width, tex.height, tex.GetPixels());

            var modCount = _moduleCounts.Get("TechnicalKeypad");
            if (modCount > 1)
            {
                var numText = module.SolveIndex.ToString();
                for (int digit = 0; digit < numText.Length; digit++)
                {
                    tex = DigitTextures[numText[digit] - '0'];
                    tmp.SetPixels(140 + 40 * digit, 90, tex.width, tex.height, tex.GetPixels());
                }
            }

            tmp.Apply(updateMipmaps: false, makeNoLongerReadable: true);
            _questionTexturesToDestroyLater.Add(tmp);
            tex = tmp;

            var questionSprite = Sprite.Create(tex, Rect.MinMaxRect(0, 0, 400, 320), new Vector2(.5f, .5f), 1280f, 1, SpriteMeshType.Tight);
            questionSprite.name = $"Technical-Keypad-{position}-{module.SolveIndex}";
            qs.Add(makeSpriteQuestion(questionSprite, Question.TechnicalKeypadDisplayedDigits, module, formatArgs: new[] { Ordinal(position + 1) }, correctAnswers: new[] { digits[position].ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessTenButtonColorCode(ModuleData module)
    {
        var comp = GetComponent(module, "scr_colorCode");
        var fldSolvedFirstStage = GetField<bool>(comp, "solvedFirstStage");
        var fldColors = GetArrayField<int>(comp, "prevColors");

        // Take a copy because the module modifies the same array in the second stage
        var firstStageColors = fldColors.Get(expectedLength: 10).ToArray();

        while (!fldSolvedFirstStage.Get())
            yield return new WaitForSeconds(.1f);

        var secondStageColors = fldColors.Get(expectedLength: 10);

        yield return WaitForSolve;

        var colorNames = new[] { "red", "green", "blue" };
        addQuestions(module, new[] { firstStageColors, secondStageColors }.SelectMany((colors, stage) => Enumerable.Range(0, 10)
            .Select(slot => makeQuestion(Question.TenButtonColorCodeInitialColors, module, formatArgs: new[] { Ordinal(slot + 1), Ordinal(stage + 1) }, correctAnswers: new[] { colorNames[colors[slot]] }))));
    }

    private IEnumerator<YieldInstruction> ProcessTenpins(ModuleData module)
    {
        var comp = GetComponent(module, "tenpins");
        yield return WaitForSolve;

        var splitNames = new[] { "Goal Posts", "Cincinnati", "Woolworth Store", "Lily", "3-7 Split", "Cocked Hat", "4-7-10 Split", "Big Four", "Greek Church", "Big Five", "Big Six", "HOW" };
        var splits = GetArrayField<int>(comp, "splits").Get(validator: ar => ar.Length != 3 ? "expected length 3" : ar.Any(v => v < 0 || v >= splitNames.Length) ? $"out of range for splitNames (0–{splitNames.Length - 1})" : null);
        var colorNames = new[] { "red", "green", "blue" };
        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.TenpinsSplits, module, formatArgs: new[] { colorNames[i] }, correctAnswers: new[] { splitNames[splits[i]] }, preferredWrongAnswers: splits.Select(x => splitNames[x]).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessTetriamonds(ModuleData module)
    {
        return processPolyiamonds(module, "tetriamondsScript", Question.TetriamondsPulsingColours, new[] { "orange", "lime", "jade", "azure", "violet", "rose", "grey" });
    }

    private IEnumerator<YieldInstruction> ProcessTextField(ModuleData module)
    {
        var comp = GetComponent(module, "TextField");

        var fldActivated = GetField<bool>(comp, "_lightson");
        while (!fldActivated.Get())
            yield return new WaitForSeconds(0.1f);

        var displayMeshes = GetArrayField<TextMesh>(comp, "ButtonLabels", true).Get(expectedLength: 12, validator: tm => tm.text == null ? "text is null" : null);
        var answer = displayMeshes.Select(x => x.text).FirstOrDefault(x => x != "✓" && x != "✗");
        var possibleAnswers = new[] { "A", "B", "C", "D", "E", "F" };

        if (!possibleAnswers.Contains(answer))
            throw new AbandonModuleException($"Answer ‘{answer ?? "<null>"}’ is not of expected value ({possibleAnswers.JoinString(", ")}).");

        yield return WaitForSolve;

        for (var i = 0; i < 12; i++)
            if (displayMeshes[i].text == answer)
                displayMeshes[i].text = "✓";

        addQuestion(module, Question.TextFieldDisplay, correctAnswers: new[] { answer });
    }

    private IEnumerator<YieldInstruction> ProcessThinkingWires(ModuleData module)
    {
        var comp = GetComponent(module, "thinkingWiresScript");
        yield return WaitForSolve;

        var validWires = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", "Black", "Any" };
        var firstCorrectWire = GetIntField(comp, "firstWireToCut").Get(min: 1, max: 7);
        var secondCorrectWire = GetField<string>(comp, "secondWireToCut").Get(str => !validWires.Contains(str) ? $"invalid color; expected: {validWires.JoinString(", ")}" : null);
        var displayNumber = GetField<string>(comp, "screenNumber").Get();

        // List of valid display numbers for validation. 69 happens in the case of "Any" while 11 is expected to be the longest.
        // Basic calculations by hand and algorithm seem to confirm this, but may want to recalculate to ensure it is right.
        if (!new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "69" }.Contains(displayNumber))
            throw new AbandonModuleException($"‘displayNumber’ has an unexpected value: {displayNumber}");

        addQuestions(module,
            makeQuestion(Question.ThinkingWiresFirstWire, module, formatArgs: null, correctAnswers: new[] { firstCorrectWire.ToString() }),
            makeQuestion(Question.ThinkingWiresSecondWire, module, formatArgs: null, correctAnswers: new[] { secondCorrectWire }),
            makeQuestion(Question.ThinkingWiresDisplayNumber, module, formatArgs: null, correctAnswers: new[] { displayNumber }));
    }

    private IEnumerator<YieldInstruction> ProcessThirdBase(ModuleData module)
    {
        var comp = GetComponent(module, "ThirdBaseModule");
        var fldStage = GetIntField(comp, "stage");
        var fldActivated = GetField<bool>(comp, "isActivated");
        var displayTextMesh = GetField<TextMesh>(comp, "Display", isPublic: true).Get();

        while (!fldActivated.Get())
            yield return new WaitForSeconds(0.1f);

        var displayWords = new string[2];

        for (var i = 0; i < 2; i++)
            while (fldStage.Get() == i)
            {
                while (!fldActivated.Get())
                    yield return new WaitForSeconds(0.1f);

                displayWords[i] = displayTextMesh.text;

                while (fldActivated.Get())
                    yield return new WaitForSeconds(0.1f);
            }

        yield return WaitForSolve;
        addQuestions(module, displayWords.Select((word, stage) => makeQuestion(Question.ThirdBaseDisplay, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { word })));
    }

    private IEnumerator<YieldInstruction> ProcessThirtyDollarModule(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "ThirtyDollarModule");
        var displayTD = GetField<IList>(comp, "displaySounds").Get(v => v.Count is not 5 || v.Cast<object>().Any(o => o is null) ? "Expected 5 played sounds" : null);
        var allTD = GetField<IList>(comp, "sounds").Get(v => v.Count is not 204 || v.Cast<object>().Any(o => o is null) ? "Expected 204 total sounds" : null);
        var fldSound = GetField<string>(displayTD[0], "sound");
        var foreignID = Question.ThirtyDollarModuleSounds.GetAttribute().ForeignAudioID;
        var display = displayTD.Cast<object>().Select(o => fldSound.GetFrom(o)).Select(s => Sounds.GetForeignClip(foreignID, s)).ToArray();
        var all = allTD.Cast<object>().Select(o => fldSound.GetFrom(o)).Select(s => Sounds.GetForeignClip(foreignID, s)).ToArray();

        var displays = GetArrayField<Renderer>(comp, "DisplayEmojis", true).Get(expectedLength: 5);
        var emojis = GetArrayField<Texture>(comp, "Emojis", true).Get(expectedLength: 204);
        IEnumerator hideBacksolve()
        {
            for(int i = 0; i < displays.Length; i++)
            {
                yield return new WaitForSeconds(0.1f);
                displays[i].material.mainTexture = emojis[i % 2 == 0 ? 5 : 36];
            }
        }
        StartCoroutine(hideBacksolve());

        addQuestion(module, Question.ThirtyDollarModuleSounds, correctAnswers: display, allAnswers: all);
    }

    private IEnumerator<YieldInstruction> ProcessTicTacToe(ModuleData module)
    {
        var comp = GetComponent(module, "TicTacToeModule");
        var fldIsInitialized = GetField<bool>(comp, "_isInitialized");

        while (!fldIsInitialized.Get())
            yield return new WaitForSeconds(.1f);

        var keypadButtons = GetArrayField<KMSelectable>(comp, "KeypadButtons", isPublic: true).Get(expectedLength: 9);
        var keypadPhysical = GetArrayField<KMSelectable>(comp, "_keypadButtonsPhysical").Get(expectedLength: 9);

        // Take a copy of the placedX array because it changes
        var placedX = GetArrayField<bool?>(comp, "_placedX").Get(expectedLength: 9, nullContentAllowed: true).ToArray();

        yield return WaitForSolve;

        var buttonNames = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        addQuestions(module, Enumerable.Range(0, 9).Select(ix => makeQuestion(Question.TicTacToeInitialState, module,
            formatArgs: new[] { buttonNames[Array.IndexOf(keypadPhysical, keypadButtons[ix])] },
            correctAnswers: new[] { placedX[ix] == null ? (ix + 1).ToString() : placedX[ix].Value ? "X" : "O" })));
    }

    private IEnumerator<YieldInstruction> ProcessTimeSignatures(ModuleData module)
    {
        var comp = GetComponent(module, "TimeSigModule");
        yield return WaitForSolve;

        var sequence = GetArrayField<string>(comp, "randomSequence")
            .Get(expectedLength: 5, validator: s => s.Length != 2 ? "Bad length" : !"123456789".Contains(s[0]) ? "Bad top digit" : !"1248".Contains(s[1]) ? "Bad bottom digit" : null);
        var answers = sequence.Select(s => $"{s[0]}/{s[1]}").ToArray();
        addQuestions(module, sequence.Select((s, i) => makeQuestion(Question.TimeSignaturesSignatures, module,
            formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { answers[i] }, preferredWrongAnswers: answers)));
    }

    private IEnumerator<YieldInstruction> ProcessTimezone(ModuleData module)
    {
        var comp = GetComponent(module, "TimezoneScript");
        var fldFromCity = GetField<string>(comp, "from");
        var fldToCity = GetField<string>(comp, "to");
        var textFromCity = GetField<TextMesh>(comp, "TextFromCity", isPublic: true).Get();
        var textToCity = GetField<TextMesh>(comp, "TextToCity", isPublic: true).Get();

        if (fldFromCity.Get() != textFromCity.text || fldToCity.Get() != textToCity.text)
            throw new AbandonModuleException($"The city names don’t match up: “{fldFromCity.Get()}” vs. “{textFromCity.text}” and “{fldToCity.Get()}” vs. “{textToCity.text}”.");

        yield return WaitForSolve;

        textFromCity.text = "WELL";
        textToCity.text = "DONE!";
        addQuestions(module,
            makeQuestion(Question.TimezoneCities, module, formatArgs: new[] { "departure" }, correctAnswers: new[] { fldFromCity.Get() }),
            makeQuestion(Question.TimezoneCities, module, formatArgs: new[] { "destination" }, correctAnswers: new[] { fldToCity.Get() }));
    }

    private IEnumerator<YieldInstruction> ProcessTipToe(ModuleData module)
    {
        var comp = GetComponent(module, "Main");
        yield return WaitForSolve;

        Array grid = GetField<Array>(comp, "Grid").Get();
        var rowNineSafeSquares = new List<string>();
        var rowTenSafeSquares = new List<string>();

        for (int col = 0; col < 10; col++)
        {
            if (!GetField<bool>(grid.GetValue(0, col), "Flicker").Get())
                rowTenSafeSquares.Add(((col + 1) % 10).ToString());
            if (!GetField<bool>(grid.GetValue(1, col), "Flicker").Get())
                rowNineSafeSquares.Add(((col + 1) % 10).ToString());
        }

        addQuestions(module,
            makeQuestion(Question.TipToeSafeSquares, module, formatArgs: new[] { "9" }, correctAnswers: rowNineSafeSquares.ToArray()),
            makeQuestion(Question.TipToeSafeSquares, module, formatArgs: new[] { "10" }, correctAnswers: rowTenSafeSquares.ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessTopsyTurvy(ModuleData module)
    {
        var comp = GetComponent(module, "topsyTurvy");
        yield return WaitForSolve;

        addQuestion(module, Question.TopsyTurvyWord, correctAnswers: new[] { Question.TopsyTurvyWord.GetAnswers()[GetField<int>(comp, "displayIndex").Get()] });
    }

    private IEnumerator<YieldInstruction> ProcessTouchTransmission(ModuleData module)
    {
        var comp = GetComponent(module, "TouchTransmissionScript");
        yield return WaitForSolve;

        var fldGenWord = GetField<string>(comp, "generatedWord");
        var fldOrder = GetField<object>(comp, "chosenOrder");

        addQuestions(module,
            makeQuestion(Question.TouchTransmissionWord, module, correctAnswers: new[] { fldGenWord.Get().ToLowerInvariant() }),
            makeQuestion(Question.TouchTransmissionOrder, module, correctAnswers: new[] { fldOrder.Get().ToString().Replace('_', ' ') }));
    }

    private IEnumerator<YieldInstruction> ProcessTrajectory(ModuleData module)
    {
        var comp = GetComponent(module, "TrajectoryModule");
        yield return WaitForSolve;

        object puzzle = GetField<object>(comp, "puzzle").Get();

        Array configs = GetField<Array>(puzzle, "configurations").Get(arr => arr.Length == 3 ? null : "expected length 3");
        string[] colorNames = { "red", "green", "blue" };
        string[][] functions = new string[3][] { new string[3], new string[3], new string[3] };
        for (int buttonIx = 0; buttonIx < 3; buttonIx++)
        {
            object config = configs.GetValue(buttonIx);
            int[] xMovements = GetArrayField<int>(config, "dxs").Get(expectedLength: 3);
            int[] yMovements = GetArrayField<int>(config, "dys").Get(expectedLength: 3);
            for (int componentIx = 0; componentIx < 3; componentIx++)
            {
                string function = colorNames[componentIx] + " ";
                int xmove = xMovements[componentIx];
                int ymove = yMovements[componentIx];
                if (xmove == 0 && ymove == 0)
                    function += "reverse";
                else if (xmove == 0 && ymove == +1)
                    function += "up";
                else if (xmove == 0 && ymove == -1)
                    function += "down";
                else if (xmove == -1 && ymove == 0)
                    function += "left";
                else if (xmove == +1 && ymove == 0)
                    function += "right";
                functions[buttonIx][componentIx] = function;
            }
        }
        addQuestions(module,
            makeQuestion(Question.TrajectoryButtonFunctions, module, formatArgs: new[] { "A" }, correctAnswers: functions[0], preferredWrongAnswers: functions.SelectMany(x => x).ToArray()),
            makeQuestion(Question.TrajectoryButtonFunctions, module, formatArgs: new[] { "B" }, correctAnswers: functions[1], preferredWrongAnswers: functions.SelectMany(x => x).ToArray()),
            makeQuestion(Question.TrajectoryButtonFunctions, module, formatArgs: new[] { "C" }, correctAnswers: functions[2], preferredWrongAnswers: functions.SelectMany(x => x).ToArray()));
    }
    private IEnumerator<YieldInstruction> ProcessTransmittedMorse(ModuleData module)
    {
        var comp = GetComponent(module, "TransmittedMorseScript");
        var fldMessage = GetField<string>(comp, "messagetrans");
        var fldStage = GetIntField(comp, "stage");

        string[] messages = new string[2];
        int stage = 0;

        while (module.Unsolved)
        {
            stage = fldStage.Get(min: 1, max: 2);
            messages[stage - 1] = fldMessage.Get();
            yield return new WaitForSeconds(.1f);
        }

        addQuestions(module, messages.Select((msg, index) => makeQuestion(Question.TransmittedMorseMessage, module,
            formatArgs: new[] { Ordinal(index + 1) },
            correctAnswers: new[] { msg },
            preferredWrongAnswers: messages)));
    }

    private IEnumerator<YieldInstruction> ProcessTriamonds(ModuleData module)
    {
        return processPolyiamonds(module, "triamondsScript", Question.TriamondsPulsingColours, new[] { "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white" });
    }

    private IEnumerator<YieldInstruction> ProcessTribalCouncil(ModuleData module)
    {
        var comp = GetComponent(module, "tribalCouncilScript");
        var texts = GetArrayField<TextMesh>(comp, "namesText", true).Get(expectedLength: 6);
        var chart = GetField<int[,]>(comp, "chart").Get(v => (v.GetLength(0), v.GetLength(1)) is not (10, 10) ? "Expected 10x10 array" : null);

        var ix = Bomb.GetSerialNumberLetters().First() - 'A' + 1;
        ix *= Bomb.GetOnIndicators().Count() is var lit and > 0 ? lit : 21;
        HashSet<(int, int)> invert = new() { (6, 1), (9, 1), (5, 4), (1, 5), (2, 5), (9, 5), (5, 9), (7, 9), (8, 9) };
        var ne = Array.IndexOf(Question.TribalCouncilClosestAlly.GetAnswers(), texts[1].text);
        var sw = Array.IndexOf(Question.TribalCouncilClosestAlly.GetAnswers(), texts[4].text);
        ix += chart[ne, sw] * (invert.Contains((ne, sw)) ? -1 : 1);
        ix = Mathf.Abs(ix);
        ix %= 20;
        ix %= 6;
        var answer = texts[ix].text;

        yield return WaitForSolve;

        addQuestion(module, Question.TribalCouncilClosestAlly, correctAnswers: new[] { answer });
    }

    private IEnumerator<YieldInstruction> ProcessTripleTerm(ModuleData module)
    {
        var comp = GetComponent(module, "TripleTermScript");
        yield return WaitForSolve;

        var wordList = GetArrayField<string>(comp, "wordList").Get().Select(i => i.Substring(0, 1).ToUpperInvariant() + i.Substring(1).ToLowerInvariant()).ToArray();
        var chosenWords = GetArrayField<int>(comp, "chosenWords").Get().Select(i => wordList[i]).ToArray();

        addQuestion(module, Question.TripleTermPasswords, correctAnswers: chosenWords, preferredWrongAnswers: wordList);
    }

    private IEnumerator<YieldInstruction> ProcessTurtleRobot(ModuleData module)
    {
        var comp = GetComponent(module, "TurtleRobot");
        var fldCursor = GetIntField(comp, "_cursor");
        var mthFormatCommand = GetMethod<string>(comp, "FormatCommand", 2);
        var commands = GetField<IList>(comp, "_commands").Get();
        var deleteButton = GetField<KMSelectable>(comp, "ButtonDelete", isPublic: true).Get();

        var codeLines = commands.Cast<object>().Select(cmd => mthFormatCommand.Invoke(cmd, false)).ToArray();
        var bugs = new List<string>();
        var bugsMarked = new HashSet<int>();

        var buttonHandler = deleteButton.OnInteract;
        deleteButton.OnInteract = delegate
        {
            var ret = buttonHandler();
            var cursor = fldCursor.Get();   // int field: avoid throwing exceptions inside of the button handler
            var command = mthFormatCommand.Invoke(commands[cursor], true);
            if (command.StartsWith("#") && bugsMarked.Add(cursor))
                bugs.Add(codeLines[cursor]);
            return ret;
        };

        yield return WaitForSolve;
        addQuestions(module, bugs.Take(2).Select((bug, ix) => makeQuestion(Question.TurtleRobotCodeLines, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { bug }, preferredWrongAnswers: codeLines)));
    }

    private IEnumerator<YieldInstruction> ProcessTwoBits(ModuleData module)
    {
        var comp = GetComponent(module, "TwoBitsModule");

        yield return WaitForSolve;

        var queryLookups = GetField<Dictionary<int, string>>(comp, "queryLookups").Get();
        var queryResponses = GetField<Dictionary<string, int>>(comp, "queryResponses").Get();

        var zerothNumCode = GetIntField(comp, "firstQueryCode").Get();
        var zerothLetterCode = queryLookups[zerothNumCode];
        var firstResponse = queryResponses[zerothLetterCode];
        var firstLookup = queryLookups[firstResponse];
        var secondResponse = queryResponses[firstLookup];
        var secondLookup = queryLookups[secondResponse];
        var thirdResponse = queryResponses[secondLookup];
        var preferredWrongAnswers = new[] { zerothNumCode.ToString("00"), firstResponse.ToString("00"), secondResponse.ToString("00"), thirdResponse.ToString("00") };

        addQuestions(module,
            makeQuestion(Question.TwoBitsResponse, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { firstResponse.ToString("00") }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.TwoBitsResponse, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { secondResponse.ToString("00") }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.TwoBitsResponse, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { thirdResponse.ToString("00") }, preferredWrongAnswers: preferredWrongAnswers));
    }
}
