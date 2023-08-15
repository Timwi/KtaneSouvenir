using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessTapCode(KMBombModule module)
    {
        var comp = GetComponent(module, "TapCodeScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TapCode);

        var words = GetStaticField<string[]>(comp.GetType(), "_wordList").Get();
        var chosenWord = GetField<string>(comp, "_chosenWord").Get(str => !words.Contains(str) ? $"word is not in list: {words.JoinString(", ")}": null);
        var w = words.Select(i => i.Substring(0, 1).ToUpperInvariant() + i.Substring(1).ToLowerInvariant()).ToArray();
        var cw = chosenWord.Substring(0, 1).ToUpperInvariant() + chosenWord.Substring(1).ToLowerInvariant();
        addQuestion(module, Question.TapCodeReceivedWord, correctAnswers: new[] { cw }, preferredWrongAnswers: w);
    }

    private IEnumerable<object> ProcessTashaSqueals(KMBombModule module)
    {
        var comp = GetComponent(module, "tashaSquealsScript");
        var fldSolved = GetField<bool>(comp, "solved");

        var colors = GetStaticField<string[]>(comp.GetType(), "colorNames").Get(ar => ar.Length != 4 ? "expected length 4" : null).ToArray();
        var sequence = GetArrayField<int>(comp, "flashing").Get(expectedLength: 5, validator: val => val < 0 || val >= colors.Length ? $"expected range 0–{colors.Length - 1}": null);

        for (int i = 0; i < colors.Length; i++)
            colors[i] = char.ToUpperInvariant(colors[i][0]) + colors[i].Substring(1);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_TashaSqueals);
        addQuestions(module,
            makeQuestion(Question.TashaSquealsColors, _TashaSqueals, formatArgs: new[] { "first" }, correctAnswers: new[] { colors[sequence[0]] }),
            makeQuestion(Question.TashaSquealsColors, _TashaSqueals, formatArgs: new[] { "second" }, correctAnswers: new[] { colors[sequence[1]] }),
            makeQuestion(Question.TashaSquealsColors, _TashaSqueals, formatArgs: new[] { "third" }, correctAnswers: new[] { colors[sequence[2]] }),
            makeQuestion(Question.TashaSquealsColors, _TashaSqueals, formatArgs: new[] { "fourth" }, correctAnswers: new[] { colors[sequence[3]] }),
            makeQuestion(Question.TashaSquealsColors, _TashaSqueals, formatArgs: new[] { "fifth" }, correctAnswers: new[] { colors[sequence[4]] }));
    }

    private IEnumerable<object> ProcessTasqueManaging(KMBombModule module)
    {
        var comp = GetComponent(module, "tasqueManaging");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_TasqueManaging);
        addQuestion(module, Question.TasqueManagingStartingPos,
            correctAnswers: new[] { TasqueManagingSprites[GetIntField(comp, "startingPosition").Get(min: 0, max: 15)] },
            preferredWrongAnswers: TasqueManagingSprites);
    }

    private IEnumerable<object> ProcessTeaSet(KMBombModule module)
    {
        var comp = GetComponent(module, "TeaSetScript");

        var fldSolved = GetField<bool>(comp, "Solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TeaSet);

        var displayedIngredients = GetListField<int>(comp, "Order").Get(expectedLength: 8);
        addQuestions(module, displayedIngredients.Select((ing, ix) => makeQuestion(Question.TeaSetDisplayedIngredients, _TeaSet, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { TeaSetSprites[ing] }, preferredWrongAnswers: TeaSetSprites)));
    }

    private IEnumerable<object> ProcessTechnicalKeypad(KMBombModule module)
    {
        var comp = GetComponent(module, "TechnicalKeypadModule");
        var digits = GetProperty<string>(GetField<object>(comp, "_keypadInfo").Get(), "Digits", isPublic: true).Get(seq => seq.Length != 12 ? "expected length 12" : null);

        var fldSolved = GetField<bool>(comp, "_isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TechnicalKeypad);

        var qs = new List<QandA>();
        for (int position = 0; position < 12; position++)
        {
            var tex = TechnicalKeypadQuestions[position];
            var tmp = new Texture2D(400, 320, TextureFormat.ARGB32, false);

            tmp.SetPixels(tex.GetPixels());
            tex = TechnicalKeypadQuestions.First(t => t.name.Equals("name"));
            tmp.SetPixels(40, 90, tex.width, tex.height, tex.GetPixels());

            var modCount = _moduleCounts.Get(_TechnicalKeypad);
            if (modCount > 1)
            {
                var numText = _modulesSolved.Get(_TechnicalKeypad).ToString();
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
            questionSprite.name = $"Technical-Keypad-{position}-{_moduleCounts.Get(_TechnicalKeypad)}";
            qs.Add(makeSpriteQuestion(questionSprite, Question.TechnicalKeypadDisplayedDigits, _TechnicalKeypad, formatArgs: new[] { ordinal(position + 1) }, correctAnswers: new[] { digits[position].ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessTenButtonColorCode(KMBombModule module)
    {
        var comp = GetComponent(module, "scr_colorCode");
        var fldSolvedFirstStage = GetField<bool>(comp, "solvedFirstStage");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldColors = GetArrayField<int>(comp, "prevColors");

        // Take a copy because the module modifies the same array in the second stage
        var firstStageColors = fldColors.Get(expectedLength: 10).ToArray();

        while (!fldSolvedFirstStage.Get())
            yield return new WaitForSeconds(.1f);

        var secondStageColors = fldColors.Get(expectedLength: 10);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TenButtonColorCode);

        var colorNames = new[] { "red", "green", "blue" };
        addQuestions(module, new[] { firstStageColors, secondStageColors }.SelectMany((colors, stage) => Enumerable.Range(0, 10)
            .Select(slot => makeQuestion(Question.TenButtonColorCodeInitialColors, _TenButtonColorCode, formatArgs: new[] { ordinal(slot + 1), ordinal(stage + 1) }, correctAnswers: new[] { colorNames[colors[slot]] }))));
    }

    private IEnumerable<object> ProcessTenpins(KMBombModule module)
    {
        var comp = GetComponent(module, "tenpins");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Tenpins);

        var splitNames = new[] { "Goal Posts", "Cincinnati", "Woolworth Store", "Lily", "3-7 Split", "Cocked Hat", "4-7-10 Split", "Big Four", "Greek Church", "Big Five", "Big Six", "HOW" };
        var splits = GetArrayField<int>(comp, "splits").Get(validator: ar => ar.Length != 3 ? "expected length 3" : ar.Any(v => v < 0 || v >= splitNames.Length) ? $"out of range for splitNames (0–{splitNames.Length - 1})": null);
        var colorNames = new[] { "red", "green", "blue" };
        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.TenpinsSplits, _Tenpins, formatArgs: new[] { colorNames[i] }, correctAnswers: new[] { splitNames[splits[i]] }, preferredWrongAnswers: splits.Select(x => splitNames[x]).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessTetriamonds(KMBombModule module)
    {
        return processPolyiamonds(module, "tetriamondsScript", Question.TetriamondsPulsingColours, _Tetriamonds, new[] { "orange", "lime", "jade", "azure", "violet", "rose", "grey" });
    }

    private IEnumerable<object> ProcessTextField(KMBombModule module)
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

        var fldSolved = GetField<bool>(comp, "_isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        for (var i = 0; i < 12; i++)
            if (displayMeshes[i].text == answer)
                displayMeshes[i].text = "✓";

        _modulesSolved.IncSafe(_TextField);
        addQuestion(module, Question.TextFieldDisplay, correctAnswers: new[] { answer });
    }

    private IEnumerable<object> ProcessThinkingWires(KMBombModule module)
    {
        var comp = GetComponent(module, "thinkingWiresScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_ThinkingWires);

        var validWires = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", "Black", "Any" };
        var firstCorrectWire = GetIntField(comp, "firstWireToCut").Get(min: 1, max: 7);
        var secondCorrectWire = GetField<string>(comp, "secondWireToCut").Get(str => !validWires.Contains(str) ? $"invalid color; expected: {validWires.JoinString(", ")}": null);
        var displayNumber = GetField<string>(comp, "screenNumber").Get();

        // List of valid display numbers for validation. 69 happens in the case of "Any" while 11 is expected to be the longest.
        // Basic calculations by hand and algorithm seem to confirm this, but may want to recalculate to ensure it is right.
        if (!new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "69" }.Contains(displayNumber))
            throw new AbandonModuleException($"‘displayNumber’ has an unexpected value: {displayNumber}");

        addQuestions(module,
            makeQuestion(Question.ThinkingWiresFirstWire, _ThinkingWires, formatArgs: null, correctAnswers: new[] { firstCorrectWire.ToString() }),
            makeQuestion(Question.ThinkingWiresSecondWire, _ThinkingWires, formatArgs: null, correctAnswers: new[] { secondCorrectWire }),
            makeQuestion(Question.ThinkingWiresDisplayNumber, _ThinkingWires, formatArgs: null, correctAnswers: new[] { displayNumber }));
    }

    private IEnumerable<object> ProcessThirdBase(KMBombModule module)
    {
        var comp = GetComponent(module, "ThirdBaseModule");
        var fldStage = GetIntField(comp, "stage");
        var fldActivated = GetField<bool>(comp, "isActivated");
        var fldSolved = GetField<bool>(comp, "isPassed");
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

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_ThirdBase);
        addQuestions(module, displayWords.Select((word, stage) => makeQuestion(Question.ThirdBaseDisplay, _ThirdBase, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { word })));
    }

    private IEnumerable<object> ProcessTicTacToe(KMBombModule module)
    {
        var comp = GetComponent(module, "TicTacToeModule");
        var fldIsInitialized = GetField<bool>(comp, "_isInitialized");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");

        while (!fldIsInitialized.Get())
            yield return new WaitForSeconds(.1f);

        var keypadButtons = GetArrayField<KMSelectable>(comp, "KeypadButtons", isPublic: true).Get(expectedLength: 9);
        var keypadPhysical = GetArrayField<KMSelectable>(comp, "_keypadButtonsPhysical").Get(expectedLength: 9);

        // Take a copy of the placedX array because it changes
        var placedX = GetArrayField<bool?>(comp, "_placedX").Get(expectedLength: 9, nullContentAllowed: true).ToArray();

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TicTacToe);

        var buttonNames = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        addQuestions(module, Enumerable.Range(0, 9).Select(ix => makeQuestion(Question.TicTacToeInitialState, _TicTacToe,
            formatArgs: new[] { buttonNames[Array.IndexOf(keypadPhysical, keypadButtons[ix])] },
            correctAnswers: new[] { placedX[ix] == null ? (ix + 1).ToString() : placedX[ix].Value ? "X" : "O" })));
    }

    private IEnumerable<object> ProcessTimezone(KMBombModule module)
    {
        var comp = GetComponent(module, "TimezoneScript");
        var fldFromCity = GetField<string>(comp, "from");
        var fldToCity = GetField<string>(comp, "to");
        var textFromCity = GetField<TextMesh>(comp, "TextFromCity", isPublic: true).Get();
        var textToCity = GetField<TextMesh>(comp, "TextToCity", isPublic: true).Get();

        if (fldFromCity.Get() != textFromCity.text || fldToCity.Get() != textToCity.text)
            throw new AbandonModuleException($"The city names don’t match up: “{fldFromCity.Get()}” vs. “{textFromCity.text}” and “{fldToCity.Get()}” vs. “{textToCity.text}”.");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Timezone);

        textFromCity.text = "WELL";
        textToCity.text = "DONE!";
        addQuestions(module,
            makeQuestion(Question.TimezoneCities, _Timezone, formatArgs: new[] { "departure" }, correctAnswers: new[] { fldFromCity.Get() }),
            makeQuestion(Question.TimezoneCities, _Timezone, formatArgs: new[] { "destination" }, correctAnswers: new[] { fldToCity.Get() }));
    }

    private IEnumerable<object> ProcessTipToe(KMBombModule module)
    {
        var comp = GetComponent(module, "Main");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TipToe);

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
            makeQuestion(Question.TipToeSafeSquares, _TipToe, formatArgs: new[] { "9" }, correctAnswers: rowNineSafeSquares.ToArray()),
            makeQuestion(Question.TipToeSafeSquares, _TipToe, formatArgs: new[] { "10" }, correctAnswers: rowTenSafeSquares.ToArray()));
    }

    private IEnumerable<object> ProcessTopsyTurvy(KMBombModule module)
    {
        var comp = GetComponent(module, "topsyTurvy");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TopsyTurvy);

        addQuestions(module, makeQuestion(Question.TopsyTurvyWord, _TopsyTurvy, formatArgs: null, correctAnswers: new[] { GetAnswers(Question.TopsyTurvyWord)[GetField<int>(comp, "displayIndex").Get()] }));
    }

    private IEnumerable<object> ProcessTouchTransmission(KMBombModule module)
    {
        var comp = GetComponent(module, "TouchTransmissionScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TouchTransmission);

        var fldGenWord = GetField<string>(comp, "generatedWord");
        var fldOrder = GetField<object>(comp, "chosenOrder");

        addQuestions(module,
            makeQuestion(Question.TouchTransmissionWord, _TouchTransmission, correctAnswers: new[] { fldGenWord.Get().ToLowerInvariant() }),
            makeQuestion(Question.TouchTransmissionOrder, _TouchTransmission, correctAnswers: new[] { fldOrder.Get().ToString().Replace('_', ' ') }));
    }

    private IEnumerable<object> ProcessTrajectory(KMBombModule module)
    {
        var comp = GetComponent(module, "TrajectoryModule");
        var fldSolved = GetField<bool>(comp, "IsSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Trajectory);

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
            makeQuestion(Question.TrajectoryButtonFunctions, _Trajectory, formatArgs: new[] { "A" }, correctAnswers: functions[0], preferredWrongAnswers: functions.SelectMany(x => x).ToArray()),
            makeQuestion(Question.TrajectoryButtonFunctions, _Trajectory, formatArgs: new[] { "B" }, correctAnswers: functions[1], preferredWrongAnswers: functions.SelectMany(x => x).ToArray()),
            makeQuestion(Question.TrajectoryButtonFunctions, _Trajectory, formatArgs: new[] { "C" }, correctAnswers: functions[2], preferredWrongAnswers: functions.SelectMany(x => x).ToArray()));
    }
    private IEnumerable<object> ProcessTransmittedMorse(KMBombModule module)
    {
        var comp = GetComponent(module, "TransmittedMorseScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldMessage = GetField<string>(comp, "messagetrans");
        var fldStage = GetIntField(comp, "stage");

        string[] messages = new string[2];
        int stage = 0;

        while (!fldSolved.Get())
        {
            stage = fldStage.Get(min: 1, max: 2);
            messages[stage - 1] = fldMessage.Get();
            yield return new WaitForSeconds(.1f);
        }
        _modulesSolved.IncSafe(_TransmittedMorse);

        addQuestions(module, messages.Select((msg, index) => makeQuestion(Question.TransmittedMorseMessage, _TransmittedMorse,
            formatArgs: new[] { ordinal(index + 1) },
            correctAnswers: new[] { msg },
            preferredWrongAnswers: messages)));
    }

    private IEnumerable<object> ProcessTriamonds(KMBombModule module)
    {
        return processPolyiamonds(module, "triamondsScript", Question.TriamondsPulsingColours, _Tetriamonds, new[] { "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white" });
    }

    private IEnumerable<object> ProcessTripleTerm(KMBombModule module)
    {
        var comp = GetComponent(module, "TripleTermScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_TripleTerm);

        var wordList = GetArrayField<string>(comp, "wordList").Get().Select(i => i.Substring(0, 1).ToUpperInvariant() + i.Substring(1).ToLowerInvariant()).ToArray();
        var chosenWords = GetArrayField<int>(comp, "chosenWords").Get().Select(i => wordList[i]).ToArray();

        addQuestions(module, makeQuestion(Question.TripleTermPasswords, _TripleTerm, correctAnswers: chosenWords, preferredWrongAnswers: wordList));
    }

    private IEnumerable<object> ProcessTurtleRobot(KMBombModule module)
    {
        var comp = GetComponent(module, "TurtleRobot");
        var fldCursor = GetIntField(comp, "_cursor");
        var fldSolved = GetField<bool>(comp, "_isSolved");
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

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_TurtleRobot);
        addQuestions(module, bugs.Take(2).Select((bug, ix) => makeQuestion(Question.TurtleRobotCodeLines, _TurtleRobot, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { bug }, preferredWrongAnswers: codeLines)));
    }

    private IEnumerable<object> ProcessTwoBits(KMBombModule module)
    {
        var comp = GetComponent(module, "TwoBitsModule");

        var fldCurrentState = GetField<object>(comp, "currentState");
        while (fldCurrentState.Get().ToString() != "Complete")
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_TwoBits);

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
            makeQuestion(Question.TwoBitsResponse, _TwoBits, formatArgs: new[] { "first" }, correctAnswers: new[] { firstResponse.ToString("00") }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.TwoBitsResponse, _TwoBits, formatArgs: new[] { "second" }, correctAnswers: new[] { secondResponse.ToString("00") }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.TwoBitsResponse, _TwoBits, formatArgs: new[] { "third" }, correctAnswers: new[] { thirdResponse.ToString("00") }, preferredWrongAnswers: preferredWrongAnswers));
    }
}