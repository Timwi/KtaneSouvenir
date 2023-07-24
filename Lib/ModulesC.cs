﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessCaesarCycle(KMBombModule module)
    {
        return processSpeakingEvilCycle1(module, "CaesarCycleScript", Question.CaesarCycleWord, _CaesarCycle);
    }

    private IEnumerable<object> ProcessCalendar(KMBombModule module)
    {
        var comp = GetComponent(module, "calendar");
        var fldLightsOn = GetField<bool>(comp, "_lightsOn");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");

        while (!fldLightsOn.Get())
            yield return new WaitForSeconds(.1f);

        var colorblindText = GetField<TextMesh>(comp, "colorblindText", isPublic: true).Get(v => v.text == null ? "text is null" : null);

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Calendar);

        addQuestion(module, Question.CalendarLedColor, correctAnswers: new[] { colorblindText.text });
    }

    private IEnumerable<object> ProcessCartinese(KMBombModule module)
    {
        var comp = GetComponent(module, "cartinese");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Cartinese);

        var buttonColors = GetArrayField<int>(comp, "buttonColors").Get(expectedLength: 4);
        var buttonLyrics = GetArrayField<string>(comp, "buttonLyrics").Get(expectedLength: 4);

        var buttonNames = new[] { "up", "right", "down", "left" };

        addQuestions(module,
            Enumerable.Range(0, 4).Select(btn => makeQuestion(Question.CartineseButtonColors, _Cartinese, formatArgs: new[] { buttonNames[btn] }, correctAnswers: new[] { GetAnswers(Question.CartineseButtonColors)[buttonColors[btn]] }))
            .Concat(Enumerable.Range(0, 4).Select(btn => makeQuestion(Question.CartineseLyrics, _Cartinese, formatArgs: new[] { buttonNames[btn] }, correctAnswers: new[] { buttonLyrics[btn] }))));
    }

    private IEnumerable<object> ProcessChallengeAndContact(KMBombModule module)
    {
        var comp = GetComponent(module, "moduleScript");
        var fldSolved = GetField<bool>(comp, "solved");
        var fldAnswers = GetArrayField<string>(comp, "answers");
        var fldFirstSet = GetArrayField<string>(comp, "possibleFirstAnswers");
        var fldSecondSet = GetArrayField<string>(comp, "possibleSecondAnswers");
        var fldThirdSet = GetArrayField<string>(comp, "possibleFinalAnswers");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ChallengeAndContact);

        string[] answers = fldAnswers.Get(expectedLength: 3);
        string[] firstSet = fldFirstSet.Get();
        string[] secondSet = fldSecondSet.Get();
        string[] thirdSet = fldThirdSet.Get();

        string[] allAnswers = new string[firstSet.Length + secondSet.Length + thirdSet.Length];
        firstSet.CopyTo(allAnswers, 0);
        secondSet.CopyTo(allAnswers, firstSet.Length);
        thirdSet.CopyTo(allAnswers, firstSet.Length + secondSet.Length);

        for (int i = 0; i < answers.Length; i++)
            answers[i] = char.ToUpperInvariant(answers[i][0]) + answers[i].Substring(1);
        for (int i = 0; i < allAnswers.Length; i++)
            allAnswers[i] = char.ToUpperInvariant(allAnswers[i][0]) + allAnswers[i].Substring(1);

        addQuestions(module,
            makeQuestion(Question.ChallengeAndContactAnswers, _ChallengeAndContact, formatArgs: new[] { "first" }, correctAnswers: new[] { answers[0] }, preferredWrongAnswers: allAnswers.Where(x => x[0] == answers[0][0]).ToArray()),
            makeQuestion(Question.ChallengeAndContactAnswers, _ChallengeAndContact, formatArgs: new[] { "second" }, correctAnswers: new[] { answers[1] }, preferredWrongAnswers: allAnswers.Where(x => x[0] == answers[1][0]).ToArray()),
            makeQuestion(Question.ChallengeAndContactAnswers, _ChallengeAndContact, formatArgs: new[] { "third" }, correctAnswers: new[] { answers[2] }, preferredWrongAnswers: allAnswers.Where(x => x[0] == answers[2][0]).ToArray()));
    }

    private IEnumerable<object> ProcessCharacterCodes(KMBombModule module)
    {
        var comp = GetComponent(module, "CharacterCodes");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_CharacterCodes);

        var code = GetArrayField<string>(comp, "chosenLetters").Get();
        var allChars = GetStaticField<Dictionary<ushort, string>>(comp.GetType(), "characterList").Get().Values.ToArray();
        addQuestions(module, code.Select((c, i) => makeQuestion(Question.CharacterCodesCharacter, _CharacterCodes, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { c }, preferredWrongAnswers: allChars)));
    }

    private IEnumerable<object> ProcessCheapCheckout(KMBombModule module)
    {
        var comp = GetComponent(module, "CheapCheckoutModule");
        var fldSolved = GetField<bool>(comp, "solved");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var paids = new List<decimal> { GetField<decimal>(comp, "Display").Get() };
        var paid = GetField<decimal>(comp, "Paid").Get();
        if (paid != paids[0])
            paids.Add(paid);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_CheapCheckout);

        addQuestions(module, paids.Select((p, i) => makeQuestion(Question.CheapCheckoutPaid, _CheapCheckout,
            formatArgs: new[] { paids.Count == 1 ? "" : ordinal(i + 1) + " " },
            correctAnswers: new[] { "$" + p.ToString("N2") })));
    }

    private IEnumerable<object> ProcessCheepCheckout(KMBombModule module)
    {
        var comp = GetComponent(module, "cheepCheckoutScript");
        var solved = false;
        module.OnPass += delegate { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_CheepCheckout);

        var shuffledList = GetField<List<int>>(comp, "numberList", isPublic: false).Get();
        var birdsPresent = shuffledList.Take(5).Where(ix => ix < 26).Select(ix => GetAnswers(Question.CheepCheckoutBirds)[ix]).ToArray();

        addQuestions(module,
           makeQuestion(Question.CheepCheckoutBirds, _CheepCheckout, formatArgs: new[] { "was" }, correctAnswers: birdsPresent),
           makeQuestion(Question.CheepCheckoutBirds, _CheepCheckout, formatArgs: new[] { "was not" }, correctAnswers: GetAnswers(Question.CheepCheckoutBirds).Except(birdsPresent).ToArray()));
    }

    private IEnumerable<object> ProcessChess(KMBombModule module)
    {
        var comp = GetComponent(module, "ChessBehaviour");
        var fldIndexSelected = GetArrayField<int>(comp, "indexSelected"); // this contains both the coordinates and the solution
        var fldIsSolved = GetField<bool>(comp, "isSolved", isPublic: true);

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var indexSelected = fldIndexSelected.Get(expectedLength: 7, validator: b => b / 10 < 0 || b / 10 >= 6 || b % 10 < 0 || b % 10 >= 6 ? "unexpected value" : null);

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Chess);

        addQuestions(module, Enumerable.Range(0, 6).Select(i => makeQuestion(Question.ChessCoordinate, _Chess, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { "" + ((char) (indexSelected[i] / 10 + 'a')) + (indexSelected[i] % 10 + 1) })));
    }

    private IEnumerable<object> ProcessChineseCounting(KMBombModule module)
    {
        var comp = GetComponent(module, "chineseCounting");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ChineseCounting);

        var index1 = GetIntField(comp, "ledIndex").Get(0, 3);
        var index2 = GetIntField(comp, "led2Index").Get(0, 3);
        var ledColors = new[] { "White", "Red", "Green", "Orange" };

        addQuestions(module,
          makeQuestion(Question.ChineseCountingLED, _ChineseCounting, formatArgs: new[] { "left" }, correctAnswers: new[] { ledColors[index1] }),
          makeQuestion(Question.ChineseCountingLED, _ChineseCounting, formatArgs: new[] { "right" }, correctAnswers: new[] { ledColors[index2] }));
    }

    private IEnumerable<object> ProcessChordQualities(KMBombModule module)
    {
        var comp = GetComponent(module, "ChordQualities");
        var fldIsSolved = GetField<bool>(comp, "isSolved", isPublic: true);

        var givenChord = GetField<object>(comp, "givenChord").Get();
        var quality = GetField<object>(givenChord, "quality").Get();
        var qualityName = GetField<string>(quality, "name").Get();
        var lights = GetField<Array>(comp, "lights", isPublic: true).Get(v => v.Length != 12 ? "expected length 12" : null);
        var mthSetOutputLight = GetMethod<object>(lights.GetValue(0), "setOutputLight", numParameters: 1, isPublic: true);
        var mthTurnInputLightOff = GetMethod<object>(lights.GetValue(0), "turnInputLightOff", numParameters: 0, isPublic: true);

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ChordQualities);

        for (int lightIx = 0; lightIx < lights.Length; lightIx++)
        {
            mthSetOutputLight.InvokeOn(lights.GetValue(lightIx), false);
            mthTurnInputLightOff.InvokeOn(lights.GetValue(lightIx));
        }

        var noteNames = GetField<Array>(givenChord, "notes").Get(v => v.Length != 4 ? "expected length 4" : null).Cast<object>().Select(note => note.ToString().Replace("sharp", "♯")).ToArray();
        addQuestions(module,
            makeQuestion(Question.ChordQualitiesNotes, _ChordQualities, correctAnswers: noteNames),
            makeQuestion(Question.ChordQualitiesQuality, _ChordQualities, correctAnswers: new[] { qualityName }));
    }

    private IEnumerable<object> ProcessCode(KMBombModule module)
    {
        var comp = GetComponent(module, "TheCodeModule");
        var fldCode = GetIntField(comp, "moduleNumber");
        var fldResetBtn = GetField<KMSelectable>(comp, "ButtonR", isPublic: true);
        var fldSubmitBtn = GetField<KMSelectable>(comp, "ButtonS", isPublic: true);

        var code = fldCode.Get(min: 999, max: 9999);

        // Hook into the module’s OnPass handler
        var isSolved = false;
        module.OnPass += delegate { isSolved = true; return false; };
        yield return new WaitUntil(() => isSolved);
        _modulesSolved.IncSafe(_Code);

        // Block the submit/reset buttons
        fldResetBtn.Get().OnInteract = delegate { return false; };
        fldSubmitBtn.Get().OnInteract = delegate { return false; };

        addQuestions(module, makeQuestion(Question.CodeDisplayNumber, _Code, correctAnswers: new[] { code.ToString() }));
    }

    private IEnumerable<object> ProcessCodenames(KMBombModule module)
    {
        var comp = GetComponent(module, "codenames");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Codenames);

        var words = GetArrayField<string>(comp, "grid").Get(expectedLength: 25);
        var solution = GetArrayField<bool>(comp, "solution").Get(expectedLength: 25);
        var solutionWords = words.Where((w, i) => solution[i]).ToArray();
        addQuestion(module, Question.CodenamesAnswers, correctAnswers: solutionWords, preferredWrongAnswers: words.Where(x => !solutionWords.Contains(x)).ToArray());
    }

    private IEnumerable<object> ProcessCoffeebucks(KMBombModule module)
    {
        var comp = GetComponent(module, "coffeebucksScript");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Coffeebucks);

        var coffees = GetArrayField<string>(comp, "coffeeOptions", isPublic: true).Get();
        var currCoffee = GetIntField(comp, "startCoffee").Get(min: 0, max: coffees.Length - 1);

        for (int i = 0; i < coffees.Length; i++)
            coffees[i] = coffees[i].Replace("\n", " ");

        addQuestion(module, Question.CoffeebucksCoffee, correctAnswers: new[] { coffees[currCoffee] }, preferredWrongAnswers: coffees);
    }

    private IEnumerable<object> ProcessCoinage(KMBombModule module)
    {
        var comp = GetComponent(module, "CoinageScript");
        var fldSolved = GetProperty<bool>(comp, "IsSolved", isPublic: true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Coinage);

        addQuestion(module, Question.CoinageFlip,
            correctAnswers: new[] { GetField<string>(comp, "souvenirCoin").Get() },
            preferredWrongAnswers: Enumerable.Range(0, 64).Select(i => "abcdefgh"[i % 8].ToString() + "87654321"[i / 8]).ToArray());
    }

    private IEnumerable<object> ProcessColorAddition(KMBombModule module)
    {
        var script = GetComponent(module, "ColorAddition");
        var modSolvedField = GetField<bool>(script, "moduleSolved");
        var numbersField = GetArrayField<string>(script, "numbers");
        while (!modSolvedField.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_ColorAddition);
        var numbersObtained = numbersField.Get(expectedLength: 3);
        var channelRefs = new[] { "red", "green", "blue" };
        addQuestions(module, channelRefs.Select((chn, idx) => makeQuestion(Question.ColorAdditionNumbers, _ColorAddition, formatArgs: new[] { chn }, correctAnswers: new[] { numbersObtained[idx] })));
    }

    private IEnumerable<object> ProcessColorBraille(KMBombModule module)
    {
        var comp = GetComponent(module, "ColorBrailleModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ColorBraille);

        var manglingNames = new Dictionary<string, string>
        {
            { "TopRowShiftedToTheRight", "Top row shifted to the right" },
            { "TopRowShiftedToTheLeft", "Top row shifted to the left" },
            { "MiddleRowShiftedToTheRight", "Middle row shifted to the right" },
            { "MiddleRowShiftedToTheLeft", "Middle row shifted to the left" },
            { "BottomRowShiftedToTheRight", "Bottom row shifted to the right" },
            { "BottomRowShiftedToTheLeft", "Bottom row shifted to the left" },
            { "EachLetterUpsideDown", "Each letter upside-down" },
            { "EachLetterHorizontallyFlipped", "Each letter horizontally flipped" },
            { "EachLetterVerticallyFlipped", "Each letter vertically flipped" },
            { "DotsAreInverted", "Dots are inverted" }
        };

        var allWordsType = comp.GetType().Assembly.GetType("ColorBraille.WordsData");
        if (allWordsType == null)
            throw new AbandonModuleException("I cannot find the ColorBraille.WordsData type.");
        var allWords = GetStaticField<Dictionary<string, int[]>>(allWordsType, "Words", isPublic: true).Get().Keys.ToArray();

        var words = GetArrayField<string>(comp, "_words").Get(expectedLength: 3);
        var mangling = GetField<object>(comp, "_mangling").Get(m => !manglingNames.ContainsKey(m.ToString()) ? "mangling is not in the dictionary" : null);
        addQuestions(module,
            makeQuestion(Question.ColorBrailleWords, _ColorBraille, formatArgs: new[] { "red" }, correctAnswers: new[] { words[0] }, preferredWrongAnswers: allWords),
            makeQuestion(Question.ColorBrailleWords, _ColorBraille, formatArgs: new[] { "green" }, correctAnswers: new[] { words[1] }, preferredWrongAnswers: allWords),
            makeQuestion(Question.ColorBrailleWords, _ColorBraille, formatArgs: new[] { "blue" }, correctAnswers: new[] { words[2] }, preferredWrongAnswers: allWords),
            makeQuestion(Question.ColorBrailleMangling, _ColorBraille, correctAnswers: new[] { manglingNames[mangling.ToString()] }));
    }

    private IEnumerable<object> ProcessColorDecoding(KMBombModule module)
    {
        var comp = GetComponent(module, "ColorDecoding");
        var fldInputButtons = GetArrayField<KMSelectable>(comp, "InputButtons", isPublic: true);
        var fldStageNum = GetIntField(comp, "stagenum");
        var fldIndicator = GetField<object>(comp, "indicator");
        var indicatorGrid = GetArrayField<GameObject>(comp, "IndicatorGrid", isPublic: true).Get();

        var patterns = new Dictionary<int, string>();
        var colors = new Dictionary<int, string[]>();
        var isSolved = false;
        var isAbandoned = false;

        var inputButtons = fldInputButtons.Get();
        var origInteract = inputButtons.Select(ib => ib.OnInteract).ToArray();
        object lastIndicator = null;

        var colorNameMapping = new Dictionary<string, string>
        {
            { "R", "Red" },
            { "G", "Green" },
            { "B", "Blue" },
            { "Y", "Yellow" },
            { "P", "Purple" }
        };

        var update = new Action(() =>
        {
            // We mustn’t throw an exception during the module’s button handler
            try
            {
                var ind = fldIndicator.Get();
                if (ReferenceEquals(ind, lastIndicator))
                    return;
                lastIndicator = ind;
                var indColors = GetField<IList>(ind, "indicator_colors").Get(
                    v => v.Count == 0 ? "no indicator colors" :
                    v.Cast<object>().Any(col => !colorNameMapping.ContainsKey(col.ToString())) ? "color is not in the color name mapping" : null);
                var stageNum = fldStageNum.Get();
                var patternName = GetField<object>(ind, "pattern").Get().ToString();
                patterns[stageNum] = patternName.Substring(0, 1) + patternName.Substring(1).ToLowerInvariant();
                colors[stageNum] = indColors.Cast<object>().Select(obj => colorNameMapping[obj.ToString()]).ToArray();
            }
            catch (AbandonModuleException amex)
            {
                Debug.LogFormat(@"<Souvenir #{0}> Abandoning Color Decoding because: {1}", _moduleId, amex.Message);
                isAbandoned = true;
            }
        });
        update();

        foreach (var i in Enumerable.Range(0, inputButtons.Length))    // Do not use ‘for’ loop as the loop variable is captured by a lambda
        {
            inputButtons[i].OnInteract = delegate
            {
                var ret = origInteract[i]();
                if (isSolved || isAbandoned)
                    return ret;

                if (fldStageNum.Get() >= 3)
                {
                    for (int j = 0; j < indicatorGrid.Length; j++)
                        indicatorGrid[j].GetComponent<MeshRenderer>().material.color = Color.black;
                    isSolved = true;
                }
                else
                    update();

                return ret;
            };
        }

        while (!isSolved && !isAbandoned)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ColorDecoding);

        for (int ix = 0; ix < inputButtons.Length; ix++)
            inputButtons[ix].OnInteract = origInteract[ix];

        if (isAbandoned)
            throw new AbandonModuleException("See error logged earlier.");

        if (Enumerable.Range(0, 3).Any(k => !patterns.ContainsKey(k) || !colors.ContainsKey(k)))
            throw new AbandonModuleException(@"I have a discontinuous set of stages: {0}/{1}.", patterns.Keys.JoinString(", "), colors.Keys.JoinString(", "));

        addQuestions(module, Enumerable.Range(0, 3).SelectMany(stage => Ut.NewArray(
             colors[stage].Length <= 2 ? makeQuestion(Question.ColorDecodingIndicatorColors, _ColorDecoding, formatArgs: new[] { "appeared", ordinal(stage + 1) }, correctAnswers: colors[stage]) : null,
             colors[stage].Length >= 3 ? makeQuestion(Question.ColorDecodingIndicatorColors, _ColorDecoding, formatArgs: new[] { "did not appear", ordinal(stage + 1) }, correctAnswers: colorNameMapping.Values.Except(colors[stage]).ToArray()) : null,
             makeQuestion(Question.ColorDecodingIndicatorPattern, _ColorDecoding, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { patterns[stage] }))));
    }

    private IEnumerable<object> ProcessColoredKeys(KMBombModule module)
    {
        var comp = GetComponent(module, "ColoredKeysScript");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ColoredKeys);

        var colors = GetArrayField<string>(comp, "loggingWords", isPublic: true).Get();
        var letters = GetArrayField<string>(comp, "letters", isPublic: true).Get();
        var displayWord = GetIntField(comp, "displayIndex").Get(0, colors.Length - 1);
        var displayColor = GetIntField(comp, "displayColIndex").Get(0, colors.Length - 1);
        var matsNames = GetArrayField<Material>(comp, "buttonmats", isPublic: true).Get().Select(x => x.name).ToArray();

        var btnLetter = Enumerable.Range(1, 4).Select(i => GetIntField(comp, string.Format("b{0}LetIndex", i)).Get(0, letters.Length - 1)).ToArray();
        var btnColor = Enumerable.Range(1, 4).Select(i => GetIntField(comp, string.Format("b{0}ColIndex", i)).Get(0, matsNames.Length - 1)).ToArray();

        addQuestions(module,
            makeQuestion(Question.ColoredKeysDisplayWord, _ColoredKeys, correctAnswers: new[] { colors[displayWord] }, preferredWrongAnswers: colors),
            makeQuestion(Question.ColoredKeysDisplayWordColor, _ColoredKeys, correctAnswers: new[] { colors[displayColor] }, preferredWrongAnswers: colors),
            makeQuestion(Question.ColoredKeysKeyLetter, _ColoredKeys, formatArgs: new[] { "top-left" }, correctAnswers: new[] { letters[btnLetter[0]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyLetter, _ColoredKeys, formatArgs: new[] { "top-right" }, correctAnswers: new[] { letters[btnLetter[1]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyLetter, _ColoredKeys, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { letters[btnLetter[2]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyLetter, _ColoredKeys, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { letters[btnLetter[3]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyColor, _ColoredKeys, formatArgs: new[] { "top-left" }, correctAnswers: new[] { matsNames[btnColor[0]] }, preferredWrongAnswers: matsNames),
            makeQuestion(Question.ColoredKeysKeyColor, _ColoredKeys, formatArgs: new[] { "top-right" }, correctAnswers: new[] { matsNames[btnColor[1]] }, preferredWrongAnswers: matsNames),
            makeQuestion(Question.ColoredKeysKeyColor, _ColoredKeys, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { matsNames[btnColor[2]] }, preferredWrongAnswers: matsNames),
            makeQuestion(Question.ColoredKeysKeyColor, _ColoredKeys, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { matsNames[btnColor[3]] }, preferredWrongAnswers: matsNames));
    }

    private IEnumerable<object> ProcessColoredSquares(KMBombModule module)
    {
        var comp = GetComponent(module, "ColoredSquaresModule");
        var fldExpectedPresses = GetField<object>(comp, "_expectedPresses");

        // Colored Squares sets _expectedPresses to null when it’s solved
        while (fldExpectedPresses.Get(nullAllowed: true) != null)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_ColoredSquares);
        addQuestion(module, Question.ColoredSquaresFirstGroup, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor").Get().ToString() });
    }

    private IEnumerable<object> ProcessColoredSwitches(KMBombModule module)
    {
        var comp = GetComponent(module, "ColoredSwitchesModule");
        var fldSwitches = GetIntField(comp, "_switchState");
        var fldSolution = GetIntField(comp, "_solutionState");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        var initial = fldSwitches.Get(0, (1 << 5) - 1);

        while (fldSolution.Get() == -1)
            yield return null;  // not waiting for .1 seconds this time to make absolutely sure we catch it before the player toggles another switch

        var afterReveal = fldSwitches.Get(0, (1 << 5) - 1);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ColoredSwitches);
        addQuestions(module,
            makeQuestion(Question.ColoredSwitchesInitialPosition, _ColoredSwitches, correctAnswers: new[] { Enumerable.Range(0, 5).Select(b => (initial & (1 << b)) != 0 ? "Q" : "R").Reverse().JoinString() }),
            makeQuestion(Question.ColoredSwitchesWhenLEDsCameOn, _ColoredSwitches, correctAnswers: new[] { Enumerable.Range(0, 5).Select(b => (afterReveal & (1 << b)) != 0 ? "Q" : "R").Reverse().JoinString() }));
    }

    private IEnumerable<object> ProcessColorMorse(KMBombModule module)
    {
        var comp = GetComponent(module, "ColorMorseModule");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        // Once Color Morse is activated, ‘flashingEnabled’ is set to true, and then it is only set to false when the module is solved.
        var fldFlashingEnabled = GetField<bool>(comp, "flashingEnabled");
        while (fldFlashingEnabled.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ColorMorse);

        var numbers = GetArrayField<int>(comp, "Numbers").Get(expectedLength: 3);
        var colorNames = GetArrayField<string>(comp, "ColorNames", isPublic: true).Get();
        var colors = GetArrayField<int>(comp, "Colors").Get(expectedLength: 3, validator: c => c < 0 || c >= colorNames.Length ? "out of range" : null);

        var flashedColorNames = colors.Select(c => colorNames[c].Substring(0, 1) + colorNames[c].Substring(1).ToLowerInvariant()).ToArray();
        var flashedCharacters = numbers.Select(num => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(num, 1)).ToArray();

        addQuestions(module, Enumerable.Range(0, 3).SelectMany(ix => Ut.NewArray(
             makeQuestion(Question.ColorMorseColor, _ColorMorse, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { flashedColorNames[ix] }, preferredWrongAnswers: flashedColorNames),
             makeQuestion(Question.ColorMorseCharacter, _ColorMorse, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { flashedCharacters[ix] }, preferredWrongAnswers: flashedCharacters))));
    }

    private IEnumerable<object> ProcessColorsMaximization(KMBombModule module)
    {
        var comp = GetComponent(module, "ColorsMaximizationModule");
        var fldSolved = GetField<bool>(comp, "solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ColorsMaximization);

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            Debug.LogFormat("[Souvenir #{0}] No question for Colors Maximization because the module was force-solved.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var submittedScore = GetProperty<int>(comp, "submittedScore", true).Get();
        var submittedColors = GetProperty<HashSet<Color>>(comp, "submittedColors", true).Get();
        var colorNameDic = GetStaticField<Dictionary<Color, string>>(comp.GetType(), "colorNames", true).Get();
        var colorNames = colorNameDic.Values.ToArray();
        var allColors = GetStaticField<Color[]>(comp.GetType(), "allColors").Get();

        var questions = new List<QandA>();
        questions.Add(makeQuestion(Question.ColorsMaximizationSubmittedScore, _ColorsMaximization, correctAnswers: new[] { submittedScore.ToString() }));
        questions.Add(makeQuestion(Question.ColorsMaximizationSubmittedColor, _ColorsMaximization, formatArgs: new[] { "was" }, correctAnswers: submittedColors.Select(c => colorNameDic[c]).ToArray(), preferredWrongAnswers: colorNames));
        questions.Add(makeQuestion(Question.ColorsMaximizationSubmittedColor, _ColorsMaximization, formatArgs: new[] { "was not" }, correctAnswers: allColors.Except(submittedColors).Select(c => colorNameDic[c]).ToArray(), preferredWrongAnswers: colorNames));

        foreach (var color in allColors)
            questions.Add(makeQuestion(Question.ColorsMaximizationColorCount, _ColorsMaximization,
                formatArgs: new[] { colorNameDic[color] },
                correctAnswers: new[] { GetField<Dictionary<Color, int>>(comp, "countOfColor").Get()[color].ToString() }));

        addQuestions(module, questions);
    }

    private IEnumerable<object> ProcessColourFlash(KMBombModule module)
    {
        var comp = GetComponent(module, "ColourFlashModule");

        var fldSolved = GetField<bool>(comp, "_solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ColourFlash);

        var fldColorSequence = GetArrayField<object>(comp, "_colourSequence").Get(ar => ar.Length != 8 ? "expected length 8" : null);
        var colorValue = GetField<object>(fldColorSequence.GetValue(7), "ColourValue", isPublic: true).Get();

        addQuestion(module, Question.ColourFlashLastColor, correctAnswers: new[] { colorValue.ToString() });
    }

    private IEnumerable<object> ProcessConnectionCheck(KMBombModule module)
    {
        var comp = GetComponent(module, "GraphModule");

        var fldSolved = GetField<bool>(comp, "_isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        float[] valid = new float[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        var queries = GetArrayField<Vector2>(comp, "Queries")
            .Get(expectedLength: 4, validator: v =>
            !valid.Contains(v.x) ? $"x out of bounds (got: {v.x})" :
            !valid.Contains(v.y) ? $"y out of bounds (got: {v.y})" :
            v.y <= v.x ? $"y less than or equal to x (got: {v.x} {v.y})" : null);

        _modulesSolved.IncSafe(_ConnectionCheck);
        addQuestion(module, Question.ConnectionCheckNumbers, correctAnswers: queries.SelectMany(v => new[] { $"{v.x} {v.y}", $"{v.y} {v.x}" }).ToArray());

        var L = GetArrayField<GameObject>(comp, "L", true).Get(expectedLength: 4);
        var R = GetArrayField<GameObject>(comp, "R", true).Get(expectedLength: 4);
        IEnumerator removeDisplays()
        {
            foreach (var num in Enumerable.Range(0, 4).SelectMany(i => new[] { L[i], R[i] }))
            {
                num.GetComponentInChildren<TextMesh>().text = "!";
                yield return new WaitForSeconds(.1f);
            }
        }
        StartCoroutine(removeDisplays());
    }

    private IEnumerable<object> ProcessCoordinates(KMBombModule module)
    {
        var comp = GetComponent(module, "CoordinatesModule");
        var fldFirstSubmitted = GetField<int?>(comp, "_firstCorrectSubmitted");

        while (fldFirstSubmitted.Get(nullAllowed: true) == null)
            yield return new WaitForSeconds(.1f);

        var fldClues = GetField<IList>(comp, "_clues");
        var clues = fldClues.Get();
        var index = fldFirstSubmitted.Get(v => v < 0 || v >= clues.Count ? string.Format("out of range; clues.Count={0}", clues.Count) : null).Value;
        var clue = clues[index];
        var fldClueText = GetField<string>(clue, "Text");
        var fldClueSystem = GetField<int?>(clue, "System");
        var clueText = fldClueText.Get();

        // The module sets ‘clues’ to null to indicate that it is solved.
        while (fldClues.Get(nullAllowed: true) != null)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Coordinates);

        // The size clue is the only one where fldClueSystem is null
        var sizeClue = clues.Cast<object>().Where(szCl => fldClueSystem.GetFrom(szCl, nullAllowed: true) == null).FirstOrDefault();
        addQuestions(module,
            makeQuestion(Question.CoordinatesFirstSolution, _Coordinates, correctAnswers: new[] { clueText.Replace("\n", " ") }, preferredWrongAnswers: clues.Cast<object>().Select(c => fldClueText.GetFrom(c).Replace("\n", " ")).Where(t => t != null).ToArray()),
            sizeClue == null ? null : makeQuestion(Question.CoordinatesSize, _Coordinates, correctAnswers: new[] { fldClueText.GetFrom(sizeClue) }));
    }

    private IEnumerable<object> ProcessCoralCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "coralCipher", Question.CoralCipherAnswer, _CoralCipher);
    }

    private IEnumerable<object> ProcessCorners(KMBombModule module)
    {
        var comp = GetComponent(module, "CornersModule");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Corners);

        var colorNames = new[] { "red", "green", "blue", "yellow" };
        var cornerNames = new[] { "top-left", "top-right", "bottom-right", "bottom-left" };

        var clampColors = GetArrayField<int>(comp, "_clampColors").Get(expectedLength: 4, validator: v => v < 0 || v >= colorNames.Length ? string.Format("expected 0–{0}", colorNames.Length - 1) : null);
        var qs = new List<QandA>();
        qs.AddRange(cornerNames.Select((corner, cIx) => makeQuestion(Question.CornersColors, _Corners, formatArgs: new[] { corner }, correctAnswers: new[] { colorNames[clampColors[cIx]] })));
        qs.AddRange(colorNames.Select((col, colIx) => makeQuestion(Question.CornersColorCount, _Corners, formatArgs: new[] { col }, correctAnswers: new[] { clampColors.Count(cc => cc == colIx).ToString() })));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessCornflowerCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "cornflowerCipher", Question.CornflowerCipherAnswer, _CornflowerCipher);
    }

    private IEnumerable<object> ProcessCosmic(KMBombModule module)
    {
        var comp = GetComponent(module, "CosmicModule");
        var fldSolved = GetField<bool>(comp, "isSolved");
        var answer = GetField<TextMesh>(comp, "DisplayText", isPublic: true).Get().text;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Cosmic);

        addQuestion(module, Question.CosmicNumber, correctAnswers: new[] { answer });
    }

    private IEnumerable<object> ProcessCrazyHamburger(KMBombModule module) {
        var comp = GetComponent(module, "CrazyHamburgerScript");
        var fldSolved = GetField<bool>(comp,"Solved");
        var fldIngredients = GetField<string>(comp,"Ingredients");

        while (!fldSolved.Get()) {
            yield return new WaitForSeconds(.1f);
        }

        var Ingredients = fldIngredients.Get();

        if (Ingredients.Length > 10) {
            Ingredients = Ingredients.Substring(0, 10);
        }

        Dictionary<char, string> TheDictionary = new Dictionary<char, string>(){ 
            { 'B',"Bread" },
            { 'C',"Cheese" },
            { 'G',"Grass" },
            { 'H',"Meat" },
            { 'O',"Oil" },
            { 'R',"Peppers" }
        };

        Dictionary<int, string> IntegerToIndex = new Dictionary<int, string>(){
            {1,"first"},
            {2,"second"},
            {3,"third"},
            {4,"fourth"},
            {5,"fifth"},
            {6,"sixth"},
            {7,"seventh"},
            {8,"eighth"},
            {9,"ninth"},
            {10,"tenth"},
        };

        var qs = new List<QandA>();

        Debug.LogFormat("<Souvenir> Ingredients are {0}",Ingredients);

        _modulesSolved.IncSafe(_CrazyHamburger);

        for (int i = 0; i < Ingredients.Length; i++) {
            Debug.LogFormat("<Souvenir> Doing the {0}", i+1);
            qs.Add(makeQuestion(Question.CrazyHamburgerIngredient,_CrazyHamburger,formatArgs: new string[] { IntegerToIndex[i+1] },correctAnswers: new[] { TheDictionary[Ingredients[i]] }));
        }

        addQuestions(module,qs);
    }

    private IEnumerable<object> ProcessCreamCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "creamCipher", Question.CreamCipherAnswer, _CreamCipher);
    }

    private IEnumerable<object> ProcessCreation(KMBombModule module)
    {
        var comp = GetComponent(module, "CreationModule");
        var fldSolved = GetField<bool>(comp, "Solved");
        var fldDay = GetIntField(comp, "Day");
        var fldWeather = GetField<string>(comp, "Weather");

        var weatherNames = GetAnswers(Question.CreationWeather);

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var currentDay = fldDay.Get(min: 1, max: 1);
        var currentWeather = fldWeather.Get(cw => !weatherNames.Contains(cw) ? "unknown weather" : null);
        var allWeather = new List<string>();
        while (true)
        {
            while (fldDay.Get() == currentDay && !fldSolved.Get() && currentWeather == fldWeather.Get())
                yield return new WaitForSeconds(0.1f);

            if (fldSolved.Get())
                break;

            if (fldDay.Get() <= currentDay)
                allWeather.Clear();
            else
                allWeather.Add(currentWeather);

            currentDay = fldDay.Get(min: 1, max: 6);
            currentWeather = fldWeather.Get(cw => !weatherNames.Contains(cw) ? "unknown weather" : null);
        }

        _modulesSolved.IncSafe(_Creation);
        addQuestions(module, allWeather.Select((t, i) => makeQuestion(Question.CreationWeather, _Creation, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { t })));
    }

    private IEnumerable<object> ProcessCrimsonCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "crimsonCipher", Question.CrimsonCipherAnswer, _CrimsonCipher);
    }

    private IEnumerable<object> ProcessCritters(KMBombModule module)
    {
        var comp = GetComponent(module, "CrittersScript");
        var fldSolved = GetField<bool>(comp, "_isModuleSolved");
        var fldColorIx = GetIntField(comp, "_randomiser");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Critters);

        var colorNames = new[] { "Yellow", "Pink", "Blue" };
        var colorIx = fldColorIx.Get(min: 0, max: 2);

        addQuestions(module, makeQuestion(Question.CrittersAlterationColor, _Critters, correctAnswers: new[] { colorNames[colorIx] }));
    }

    private IEnumerable<object> ProcessCruelBinary(KMBombModule module)
    {
        var comp = GetComponent(module, "CruelBinary");

        var fldSolved = GetField<bool>(comp, "solved", isPublic: true);
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_CruelBinary);

        var wordList = GetArrayField<string>(comp, "_WordList", isPublic: true).Get();
        var displayedWord = GetField<string>(comp, "h", isPublic: true).Get();
        addQuestion(module, Question.CruelBinaryDisplayedWord, correctAnswers: new[] { displayedWord }, preferredWrongAnswers: wordList);
    }

    private IEnumerable<object> ProcessCruelKeypads(KMBombModule module)
    {
        var comp = GetComponent(module, "CruelKeypadScript");

        var fldSolved = GetField<bool>(comp, "_isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_CruelKeypads);

        var firstTwoColors = GetField<Array>(comp, "StageColor").Get(arr => arr.Length != 2 ? "expected length 2" : null);
        var colors = new string[]
        {
            firstTwoColors.GetValue(0).ToString(),
            firstTwoColors.GetValue(1).ToString(),
            GetField<Enum>(comp, "stripColor").Get().ToString()
        };
        var fieldNames = new[] { "Stage1Symbols", "Stage2Symbols", "pickedSymbols" };
        // Unfortunately, these are stored as IList<char> types instead of just List<char>, so we can't used GetListField.
        string[][] displayedSymbolSets = fieldNames.Select(name => GetField<IList<char>>(comp, name).Get(list => list.Count != 4 ? "expected length 4" : null).Select(c => c.ToString()).ToArray()).ToArray();

        var qs = new List<QandA>();
        for (int stage = 0; stage < 3; stage++)
        {
            string stageNum = ordinal(stage + 1);
            qs.Add(makeQuestion(Question.CruelKeypadsColors, _CruelKeypads, formatArgs: new[] { stageNum }, correctAnswers: new[] { colors[stage] }));
            qs.Add(makeQuestion(Question.CruelKeypadsDisplayedSymbols, _CruelKeypads, formatArgs: new[] { stageNum }, correctAnswers: displayedSymbolSets[stage]));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessCrypticCycle(KMBombModule module)
    {
        return processSpeakingEvilCycle2(module, "CrypticCycleScript", Question.CrypticCycleWord, _CrypticCycle);
    }

    private IEnumerable<object> ProcessCrypticKeypad(KMBombModule module)
    {
        var comp = GetComponent(module, "CrypticKeypadScript");
        var fldSolved = GetField<bool>(comp, "Solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_CrypticKeypad);

        var letters = GetArrayField<string>(comp, "Letters2").Get();
        var rotations = GetArrayField<int>(comp, "Rotations").Get();

        var qs = new List<QandA>();
        var directions = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };
        var cardinalDirections = new[] { "North", "East", "South", "West" };
        for (int i = 0; i < 4; i++)
        {
            qs.Add(makeQuestion(Question.CrypticKeypadLabels, _CrypticKeypad, formatArgs: new[] { directions[i] }, correctAnswers: new[] { letters[i] }));
            qs.Add(makeQuestion(Question.CrypticKeypadRotations, _CrypticKeypad, formatArgs: new[] { directions[i] }, correctAnswers: new[] { cardinalDirections[rotations[i]] }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessCube(KMBombModule module)
    {
        var comp = GetComponent(module, "theCubeScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Cube);

        var rotations = GetListField<int>(comp, "selectedRotations").Get(expectedLength: 6);
        var rotationNames = new[] { "rotate cw", "tip left", "tip backwards", "rotate ccw", "tip right", "tip forwards" };
        var allRotations = rotations.Select(r => rotationNames[r]).ToArray();

        addQuestions(module, rotations.Select((rot, ix) => makeQuestion(Question.CubeRotations, _Cube, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { rotationNames[rot] }, preferredWrongAnswers: allRotations)));
    }

    private IEnumerable<object> ProcessCursedDoubleOh(KMBombModule module)
    {
        var comp = GetComponent(module, "DoubleOhModule");

        var fldSolved = GetField<bool>(comp, "_isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_CursedDoubleOh);

        int firstNumber = GetField<List<int>>(comp, "visitedNumbers").Get().First();
        string firstDigit = (firstNumber / 10).ToString();
        addQuestion(module, Question.CursedDoubleOhInitialPosition, correctAnswers: new[] { firstDigit });
    }

    private IEnumerable<object> ProcessCyanButton(KMBombModule module)
    {
        var comp = GetComponent(module, "CyanButtonScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_CyanButton);
        var positions = GetArrayField<int>(comp, "_buttonPositions").Get(expectedLength: 6);

        addQuestions(module, Enumerable.Range(0, 6).Select(stage => makeQuestion(Question.CyanButtonPositions, _CyanButton, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { _attributes[Question.CyanButtonPositions].AllAnswers[positions[stage]] })));
    }
}
