using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Souvenir;
using UnityEngine;
using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessFactoringMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "FactoringMazeScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_FactoringMaze);

        var chosenPrimes = GetArrayField<int>(comp, "chosenPrimes").Get(expectedLength: 4);
        addQuestion(module, Question.FactoringMazeChosenPrimes, correctAnswers: chosenPrimes.Select(i => i.ToString()).ToArray());
    }

    private IEnumerable<object> ProcessFactoryMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "FactoryMazeScript");
        var fldSolved = GetField<bool>(comp, "solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_FactoryMaze);

        var usedRooms = GetArrayField<string>(comp, "usedRooms").Get(expectedLength: 5).ToArray();
        var startRoom = GetIntField(comp, "startRoom").Get(0, usedRooms.Length - 1);

        for (int i = usedRooms.Length - 1; i >= 0; --i)
            usedRooms[i] = usedRooms[i].Replace('\n', ' ');

        addQuestion(module, Question.FactoryMazeStartRoom, correctAnswers: new[] { usedRooms[startRoom] }, preferredWrongAnswers: usedRooms);
    }

    private IEnumerable<object> ProcessFastMath(KMBombModule module)
    {
        var comp = GetComponent(module, "FastMathModule");
        var fldScreen = GetField<TextMesh>(comp, "Screen", isPublic: true);
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var usableLetters = GetField<string>(comp, "letters").Get();

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var wrongAnswers = new HashSet<string>();
        string letters = null;
        while (!fldSolved.Get())
        {
            var display = fldScreen.Get().text;
            if (display.Length != 3)
                throw new AbandonModuleException($"The screen contains something other than three characters: “{display}” ({display.Length} characters).");
            letters = display[0] + "" + display[2];
            wrongAnswers.Add(letters);
            yield return new WaitForSeconds(.1f);
        }
        if (letters == null)
            throw new AbandonModuleException("No letters were extracted before the module was solved.");

        _modulesSolved.IncSafe(_FastMath);

        while (wrongAnswers.Count < 6)
            foreach (var ans in new AnswerGenerator.Strings(2, usableLetters).GetAnswers(this).Take(6 - wrongAnswers.Count))
                wrongAnswers.Add(ans);

        addQuestion(module, Question.FastMathLastLetters, correctAnswers: new[] { letters }, preferredWrongAnswers: wrongAnswers.ToArray());
    }

    private IEnumerable<object> ProcessFaultyButtons(KMBombModule module)
    {
        var comp = GetComponent(module, "FaultyButtonsScript");

        var fldSolved = GetField<bool>(comp, "Solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_FaultyButtons);

        var referredButtons = GetField<int[]>(comp, "ReferredButtons").Get();
        var qs = new List<QandA>();
        for (int pos = 0; pos < 16; pos++)
        {
            var buttonRefersTo = new Coord(4, 4, referredButtons[pos]);
            var refersToButton = new Coord(4, 4, Array.IndexOf(referredButtons, pos));
            qs.Add(makeQuestion(Question.FaultyButtonsReferredToThisButton, _FaultyButtons, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { refersToButton }, preferredWrongAnswers: new[] { buttonRefersTo }));
            qs.Add(makeQuestion(Question.FaultyButtonsThisButtonReferredTo, _FaultyButtons, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { buttonRefersTo }, preferredWrongAnswers: new[] { refersToButton }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessFaultyRGBMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "FaultyRGBMazeScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_FaultyRGBMaze);

        var keyPos = GetArrayField<int[]>(comp, "keylocations").Get(expectedLength: 3, validator: key => key.Length != 2 ? "expected length 2" : key.Any(number => number < 0 || number > 6) ? "expected range 0–6" : null);
        var mazeNum = GetArrayField<int[]>(comp, "mazenumber").Get(expectedLength: 3, validator: maze => maze.Length != 2 ? "expected length 2" : maze[0] < 0 || maze[0] > 15 ? "expected range 0–15" : null);
        var exitPos = GetArrayField<int>(comp, "exitlocation").Get(expectedLength: 3);

        if (exitPos[1] < 0 || exitPos[1] > 6 || exitPos[2] < 0 || exitPos[2] > 6)
            throw new AbandonModuleException($"‘exitPos’ contains invalid coordinate: ({exitPos[2]},{exitPos[1]})");

        string[] colors = { "red", "green", "blue" };

        var qs = new List<QandA>();

        for (int index = 0; index < 3; index++)
        {
            qs.Add(makeQuestion(Question.FaultyRGBMazeKeys, _FaultyRGBMaze,
                formatArgs: new[] { colors[index] },
                correctAnswers: new[] { "ABCDEFG"[keyPos[index][1]] + (keyPos[index][0] + 1).ToString() }));
            qs.Add(makeQuestion(Question.FaultyRGBMazeNumber, _FaultyRGBMaze,
                formatArgs: new[] { colors[index] },
                correctAnswers: new[] { "0123456789abcdef"[mazeNum[index][0]].ToString() }));
        }

        qs.Add(makeQuestion(Question.FaultyRGBMazeExit, _FaultyRGBMaze,
            correctAnswers: new[] { "ABCDEFG"[exitPos[2]] + (exitPos[1] + 1).ToString() }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessFindTheDate(KMBombModule module)
    {
        var comp = GetComponent(module, "DateFinder");
        var fldStage = GetIntField(comp, "count");
        var fldDate = GetIntField(comp, "day");
        var fldMonth = GetField<string>(comp, "month");
        var fldYear = GetIntField(comp, "year");
        var fldCentury = GetIntField(comp, "century");

        var solved = false;
        module.OnPass += () => { solved = true; return false; };
        var dateArr = new int[3];
        var yearArr = new string[3];
        var monthArr = new string[3];
        var currentStage = -1;

        while (!solved)
        {
            var newStage = fldStage.Get();
            if (currentStage != newStage)
            {
                currentStage = newStage;
                dateArr[newStage] = fldDate.Get();
                monthArr[newStage] = fldMonth.Get();
                yearArr[newStage] = "" + fldCentury.Get() + fldYear.Get();
            }
            yield return null;
        }
        _modulesSolved.IncSafe(_FindTheDate);

        var qs = new List<QandA>();

        for (int i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.FindTheDateMonth, _FindTheDate,
            formatArgs: new[] { (i + 1).ToString() },
            correctAnswers: new[] { monthArr[i] }));

            qs.Add(makeQuestion(Question.FindTheDateDay, _FindTheDate,
            formatArgs: new[] { (i + 1).ToString() },
            correctAnswers: new[] { dateArr[i].ToString() }));

            qs.Add(makeQuestion(Question.FindTheDateYear, _FindTheDate,
            formatArgs: new[] { (i + 1).ToString() },
            correctAnswers: new[] { yearArr[i] }));
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessFiveLetterWords(KMBombModule module)
    {
        var comp = GetComponent(module, "FiveLetterWords");

        var fldSolved = GetField<bool>(comp, "ModuleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_FiveLetterWords);

        var wordList = JsonConvert.DeserializeObject<string[]>(GetField<TextAsset>(comp, "FiverData", isPublic: true).Get().text);
        var displayedWords = GetArrayField<string>(comp, "TheNames").Get(expectedLength: 3, validator: name => name.Length != 5 ? "expected length 5" : null);
        addQuestion(module, Question.FiveLetterWordsDisplayedWords, correctAnswers: displayedWords, preferredWrongAnswers: wordList);
    }

    private IEnumerable<object> ProcessFizzBuzz(KMBombModule module)
    {
        var comp = GetComponent(module, "FizzBuzzModule");
        var labels = GetArrayField<TextMesh>(comp, "Labels", isPublic: true).Get(expectedLength: 3);
        var solutions = GetArrayField<int[]>(comp, "Solutions").Get(expectedLength: 2, nullContentAllowed: true);

        while (solutions.GetValue(0) is null)
            yield return null; // Don't wait 0.1 seconds to make sure we get the labels before they are changed.

        var displayedDigits = labels.Select(t => t.text).ToArray();
        if (displayedDigits.Any(ds => ds.Length != 7 || ds.Any(d => !char.IsDigit(d))))
            throw new AbandonModuleException($"Unexpected display value: \"{displayedDigits.First(ds => ds.Length != 7 || ds.Any(d => !char.IsDigit(d)))}\".");

        var fldSolved = GetField<bool>(comp, "isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_FizzBuzz);

        var qs = new List<QandA>();
        var displays = new[] { "top", "middle", "bottom" };
        for (int pos = 0; pos < 3; pos++)
        {
            if (displayedDigits[pos] != labels[pos].text)
            {
                for (int digit = 0; digit < 6; digit++)
                    qs.Add(makeQuestion(Question.FizzBuzzDisplayedNumbers, _FizzBuzz, formatArgs: new[] { ordinal(digit + 1), displays[pos] }, correctAnswers: new[] { displayedDigits[pos][digit].ToString() }));
                if (!labels[pos].text.ToLower().Contains("buzz")) // Do not ask about the last digit if the answer was buzz because there are only two possible correct answers.
                    qs.Add(makeQuestion(Question.FizzBuzzDisplayedNumbers, _FizzBuzz, formatArgs: new[] { "7th", displays[pos] }, correctAnswers: new[] { displayedDigits[pos][6].ToString() }));
            }
        }
        if (qs.Count == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No questions for FizzBuzz because all of the numbers remained on the displays.");
            _legitimatelyNoQuestions.Add(module);
        }
        else
            addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessFlags(KMBombModule module)
    {
        var comp = GetComponent(module, "FlagsModule");
        var fldCanInteract = GetField<bool>(comp, "canInteract");
        var mainCountry = GetField<object>(comp, "mainCountry").Get();
        var countries = GetField<IList>(comp, "countries").Get(v => v.Count != 7 ? "expected length 7" : null);
        var number = GetIntField(comp, "number").Get(1, 7);

        var propCountryName = GetProperty<string>(mainCountry, "CountryName", isPublic: true);
        var mainCountryName = propCountryName.GetFrom(mainCountry);
        var mainCountrySprite = FlagsSprites.FirstOrDefault(spr => spr.name == mainCountryName) ?? throw new AbandonModuleException($"Country name “{mainCountryName}” (main country) has no corresponding sprite.");

        var otherCountrySprites = countries.Cast<object>()
            .Select(country => propCountryName.GetFrom(country))
            .Select((countryName, countryIx) => FlagsSprites.FirstOrDefault(spr => spr.name == countryName) ?? throw new AbandonModuleException($"Country name “{countryName}” (country #{countryIx}) has no corresponding sprite."))
            .ToArray();

        while (fldCanInteract.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Flags);

        addQuestions(module,
            // Displayed number
            makeQuestion(Question.FlagsDisplayedNumber, _Flags, correctAnswers: new[] { number.ToString() }),
            // Main country flag
            makeQuestion(Question.FlagsMainCountry, _Flags, correctAnswers: new[] { mainCountrySprite }, preferredWrongAnswers: otherCountrySprites),
            // Rest of the country flags
            makeQuestion(Question.FlagsCountries, _Flags, correctAnswers: otherCountrySprites, preferredWrongAnswers: FlagsSprites));
    }

    private IEnumerable<object> ProcessFlashingArrows(KMBombModule module)
    {
        var comp = GetComponent(module, "FlashingArrowsScript");

        var isSolved = false;
        module.OnPass += delegate { isSolved = true; return false; };
        while (!isSolved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_FlashingArrows);

        var colorReference = GetArrayField<string>(comp, "debugColors").Get(expectedLength: 7);
        var displayedValue = GetField<int>(comp, "displayNumber").Get(num => num < 0 || num >= 100 ? "Expected the displayed value to be within 0 and 99 inclusive." : null);
        var idxReferencedArrow = GetField<int>(comp, "idxReferencedArrow").Get(num => num < 0 || num >= 4 ? "Expected the value to be within 0 and 3 inclusive." : null);
        var idxFlashedArrows = GetArrayField<int[]>(comp, "idxColorFlashingArrows").Get(expectedLength: 4);
        var arrowSet = idxFlashedArrows[idxReferencedArrow];
        var idxBlack = Array.IndexOf(arrowSet, -1);
        var colorAfterBlack = arrowSet[(idxBlack + 1) % 3];
        var colorBeforeBlack = arrowSet[(idxBlack + 2) % 3];

        addQuestions(module,
            makeQuestion(Question.FlashingArrowsDisplayedValue, _FlashingArrows, correctAnswers: new[] { displayedValue.ToString() }),
            makeQuestion(Question.FlashingArrowsReferredArrow, _FlashingArrows, formatArgs: new[] { "before" }, correctAnswers: new[] { colorReference[colorBeforeBlack] }, preferredWrongAnswers: colorReference),
            makeQuestion(Question.FlashingArrowsReferredArrow, _FlashingArrows, formatArgs: new[] { "after" }, correctAnswers: new[] { colorReference[colorAfterBlack] }, preferredWrongAnswers: colorReference));
    }

    private IEnumerable<object> ProcessFlashingLights(KMBombModule module)
    {
        var comp = GetComponent(module, "doubleNegativesScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_FlashingLights);

        var topColors = GetListField<int>(comp, "selectedColours").Get(expectedLength: 12);
        var bottomColors = GetListField<int>(comp, "selectedColours2").Get(expectedLength: 12);
        var colorNames = new[] { "cyan", "green", "red", "purple", "orange" };
        var topTotals = Enumerable.Range(1, 5).Select(num => topColors.Count(x => x == num)).ToArray();
        var bottomTotals = Enumerable.Range(1, 5).Select(num => bottomColors.Count(x => x == num)).ToArray();

        var qs = new List<QandA>();
        for (int i = 0; i < 5; i++)
        {
            qs.Add(makeQuestion(Question.FlashingLightsLEDFrequency, _FlashingLights, formatArgs: new[] { "top", colorNames[i] }, correctAnswers: new[] { topTotals[i].ToString() }, preferredWrongAnswers: new[] { bottomTotals[i].ToString() }));
            qs.Add(makeQuestion(Question.FlashingLightsLEDFrequency, _FlashingLights, formatArgs: new[] { "bottom", colorNames[i] }, correctAnswers: new[] { bottomTotals[i].ToString() }, preferredWrongAnswers: new[] { topTotals[i].ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessFlyswatting(KMBombModule module)
    {
        var comp = GetComponent(module, "flyswattingScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        bool[] swatted = GetArrayField<int>(comp, "answers").Get(expectedLength: 5).Select(x => x == 1).ToArray();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_Flyswatting);

        string[] letters = GetArrayField<string>(comp, "chosens").Get(expectedLength: 5);
        string[] outsideLetters = letters.Where((_, pos) => !swatted[pos]).ToArray();
        if (outsideLetters.Length == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Flyswatting because every fly was part of the solution.");
            _legitimatelyNoQuestions.Add(module);
        }
        else
            addQuestion(module, Question.FlyswattingUnpressed, correctAnswers: outsideLetters);
    }

    private IEnumerable<object> ProcessFollowMe(KMBombModule module)
    {
        var comp = GetComponent(module, "FollowMe");

        var fldSolved = GetField<bool>(comp, "ModuleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_FollowMe);

        var directionWords = new Dictionary<string, string> { { "U", "Up" }, { "D", "Down" }, { "L", "Left" }, { "R", "Right" } };
        var path = GetListField<string>(comp, "Path").Get(minLength: 1, validator: x => !directionWords.ContainsKey(x) ? $"expected only {directionWords.Keys.JoinString(", ")}" : null);

        var qs = new List<QandA>();
        for (int pos = 0; pos < path.Count; pos++)
            qs.Add(makeQuestion(Question.FollowMeDisplayedPath, _FollowMe, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { directionWords[path[pos]] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessForestCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "forestCipher", Question.ForestCipherScreen, _ForestCipher);
    }

    private List<Array> _facCylinders = new List<Array>();
    private List<List<int>> _facFigures = new List<List<int>>();
    private IEnumerable<object> ProcessForgetAnyColor(KMBombModule module)
    {
        var comp = GetComponent(module, "FACScript");

        var init = GetField<object>(comp, "init").Get();
        var fldCurrentStage = GetIntField(init, "stage");
        var fldCylinders = GetField<Array>(init, "cylinders");
        var calculate = GetField<object>(init, "calculate").Get();
        var fldFigures = GetListField<int>(calculate, "figureSequences");

        var activated = false;
        module.OnActivate += () => activated = true;
        while (!activated)
            yield return null;
        yield return null; // Wait one extra frame to ensure that maxStage has been set.

        var maxStage = GetIntField(init, "maxStage").Get(min: 0);
        if (maxStage == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Any Color because there were no stages.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var myCylinders = fldCylinders.Get(v => v.Rank != 2 || v.GetLength(0) != maxStage + 1 || v.GetLength(1) != 3 ? $"expected a {maxStage + 1}×3 2D array" : null);
        var myFigures = fldFigures.Get();
        _facCylinders.Add(myCylinders);
        _facFigures.Add(myFigures);

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ForgetAnyColor);

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White" };
        var figureNames = new[] { "LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR" };

        string getCylinders(Array cylinders, int stage) => Enumerable.Range(0, 3).Select(ix => colorNames[(int) cylinders.GetValue(stage, ix)]).JoinString(", ");

        var randomStage = Rnd.Range(0, fldCurrentStage.Get(min: 0, max: maxStage));
        var formattedName = "Forget Any Color";
        if (_moduleCounts[_ForgetAnyColor] > 1)
        {
            for (int stage = 0; stage < maxStage; stage++)
            {
                if (stage == randomStage)
                    continue;
                var cylindersThisStage = getCylinders(myCylinders, stage);
                if (_facFigures.Count(f => f[stage] == myFigures[stage]) == 1)
                    formattedName = $"the Forget Any Color which used figure {figureNames[myFigures[stage]]} in the {ordinal(stage + 1)} stage";
                if (_facCylinders.Count(c => getCylinders(c, stage) == cylindersThisStage) == 1)
                    formattedName = $"the Forget Any Color whose cylinders in the {ordinal(stage + 1)} stage were {cylindersThisStage}";
                if (formattedName != "Forget Any Color")
                    break;
            }
            if (formattedName == "Forget Any Color")
            {
                Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Any Color because there were not enough stages where this one (#{GetIntField(init, "moduleId").Get()}) was unique.");
                _legitimatelyNoQuestions.Add(module);
                yield break;
            }
        }

        var correctCylinders = getCylinders(myCylinders, randomStage);
        var preferredCylinders = new HashSet<string> { correctCylinders };
        while (preferredCylinders.Count < 6)
            preferredCylinders.Add(Enumerable.Range(0, 3).Select(i => colorNames.PickRandom()).JoinString(", "));

        addQuestions(module,
            makeQuestion(Question.ForgetAnyColorCylinder, _ForgetAnyColor, formattedModuleName: formattedName, formatArgs: new[] { ordinal(randomStage + 1) },
                correctAnswers: new[] { correctCylinders }, preferredWrongAnswers: preferredCylinders.ToArray()),
            makeQuestion(Question.ForgetAnyColorSequence, _ForgetAnyColor, formattedModuleName: formattedName, formatArgs: new[] { ordinal(randomStage + 1) },
                correctAnswers: new[] { figureNames[myFigures[randomStage]] }));
    }

    private List<int[]> _feFirstDisplays = new List<int[]>();
    private IEnumerable<object> ProcessForgetEverything(KMBombModule module)
    {
        var comp = GetComponent(module, "EvilMemory");

        var activated = false;
        module.OnActivate += () => activated = true;
        while (!activated)
            yield return new WaitForSeconds(.1f);
        yield return null; // Wait one extra frame to ensure DialDisplay is set.

        var allDisplays = GetArrayField<int[]>(comp, "DialDisplay").Get(nullAllowed: true);
        if (allDisplays is null)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Everything because there were no stages.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }
        if (allDisplays.Length < 1)
            throw new AbandonModuleException("‘DialDisplay’ had length 0, when I expected length at least 1.");

        var myFirstDisplay = allDisplays.First();
        if (myFirstDisplay.Length != 10)
            throw new AbandonModuleException($"First element of ‘DialDisplay’ had length {myFirstDisplay.Length}, when I expected length 10.");
        _feFirstDisplays.Add(myFirstDisplay);

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ForgetEverything);

        var stageOrdering = GetArrayField<int>(comp, "StageOrdering").Get();
        var myIgnoredList = GetStaticField<string[]>(comp.GetType(), "ignoredModules", isPublic: true).Get();
        if (Array.IndexOf(stageOrdering, 0) + 1 > Bomb.GetSolvableModuleNames().Count(x => !myIgnoredList.Contains(x)))
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Everything because stage one was not displayed before non-ignored modules were solved.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        if (_feFirstDisplays.Count != _moduleCounts[_ForgetEverything])
            throw new AbandonModuleException($"The number of displays ({_feFirstDisplays.Count}) did not match the number of Forget Everything modules ({_moduleCounts[_ForgetEverything]}).");

        if (_moduleCounts[_ForgetEverything] == 1)
            addQuestions(module, myFirstDisplay.Select((digit, pos) => makeQuestion(Question.ForgetEverythingStageOneDisplay, _ForgetEverything, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { digit.ToString() })));
        else
        {
            var uniquePositions = Enumerable.Range(0, 10).Where(pos => _feFirstDisplays.Count(dis => dis[pos] == myFirstDisplay[pos]) == 1).Take(2).ToArray();
            if (!uniquePositions.Any())
            {
                Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Everything because this one (#{GetIntField(comp, "thisLoggingID", isPublic: true)}) had a non-unique first stage.");
                _legitimatelyNoQuestions.Add(module);
                yield break;
            }
            var qs = new List<QandA>();
            for (int pos = 0; pos < 10; pos++)
            {
                if (uniquePositions.Any(p => p != pos))
                {
                    var reference = uniquePositions.First(p => p != pos);
                    qs.Add(makeQuestion(Question.ForgetEverythingStageOneDisplay, _ForgetEverything,
                        formattedModuleName: $"the Forget Everything whose {ordinal(reference + 1)} displayed digit in this stage was {myFirstDisplay[reference]}", formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { myFirstDisplay[pos].ToString() }));
                }
            }
            addQuestions(module, qs);
        }
    }

    private IEnumerable<object> ProcessForgetMe(KMBombModule module)
    {
        var comp = GetComponent(module, "NotForgetMeNotScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ForgetMe);

        string[] positions = { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        int[] initState = GetArrayField<int>(comp, "givenPuzzle").Get(expectedLength: 9);
        addQuestions(module,
            Enumerable.Range(0, 9).Where(ix => initState[ix] != 0).Select(ix =>
            makeQuestion(Question.ForgetMeInitialState, _ForgetMe, formatArgs: new[] { positions[ix] }, correctAnswers: new[] { initState[ix].ToString() })));
    }

    private List<int[]> _forgetMeNotDisplays = new List<int[]>();
    private IEnumerable<object> ProcessForgetMeNot(KMBombModule module)
    {
        var comp = GetComponent(module, "AdvancedMemory");

        var fldDisplayedDigits = GetArrayField<int>(comp, "Display");
        var activated = false;
        module.OnActivate += () => { activated = true; };
        while (!activated)
            yield return new WaitForSeconds(.1f);
        yield return null; // Wait one frame to make sure the Display field has been set.

        var myDisplay = fldDisplayedDigits.Get(minLength: 0, validator: d => d < 0 || d > 9 ? "expected range 0-9" : null);
        if (_forgetMeNotDisplays.Any() && myDisplay.Length != _forgetMeNotDisplays[0].Length)
            throw new AbandonModuleException("The number of stages in each ‘Display’ is inconsistent.");
        _forgetMeNotDisplays.Add(myDisplay);

        if (myDisplay.Length == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Me Not because there were no stages.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ForgetMeNot);

        var myIgnoredList = GetStaticField<string[]>(comp.GetType(), "ignoredModules", isPublic: true).Get();
        var displayedStageCount = Bomb.GetSolvedModuleNames().Count(x => !myIgnoredList.Contains(x));

        if (_forgetMeNotDisplays.Count != _moduleCounts[_ForgetMeNot])
            throw new AbandonModuleException("The number of displays did not match the number of Forget Me Not modules.");

        if (_moduleCounts[_ForgetMeNot] == 1)
            addQuestions(module, myDisplay.Take(displayedStageCount).Select((digit, ix) => makeQuestion(Question.ForgetMeNotDisplayedDigits, _ForgetMeNot, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { digit.ToString() })));
        else
        {
            var uniqueStages = Enumerable.Range(1, displayedStageCount).Where(stage => _forgetMeNotDisplays.Count(display => display[stage - 1] == myDisplay[stage - 1]) == 1).Take(2).ToArray();
            if (uniqueStages.Length == 0 || displayedStageCount == 1)
            {
                var fmnId = GetIntField(comp, "thisLoggingID", isPublic: true).Get();
                Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Me Not because there are not enough stages at which this one (#{fmnId}) had a unique displayed number.");
                _legitimatelyNoQuestions.Add(module);
            }
            else
            {
                var qs = new List<QandA>();
                for (int stage = 0; stage < displayedStageCount; stage++)
                {
                    var uniqueStage = uniqueStages.FirstOrDefault(s => s != stage + 1);
                    if (uniqueStage != 0)
                    {
                        Debug.Log(uniqueStage);
                        qs.Add(makeQuestion(Question.ForgetMeNotDisplayedDigits, _ForgetMeNot, formattedModuleName: $"the Forget Me Not which displayed a {myDisplay[uniqueStage - 1]} in the {ordinal(uniqueStage)} stage", formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { myDisplay[stage].ToString() }));
                    }
                }
                addQuestions(module, qs);
            }
        }
    }

    private IEnumerable<object> ProcessForgetMeNow(KMBombModule module)
    {
        var comp = GetComponent(module, "ForgetMeNow");

        var fldStatus = GetIntField(comp, "moduleStatus");
        while (fldStatus.Get(min: -1, max: 3) != 3)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ForgetMeNow);

        var displayedDigits = GetArrayField<int>(comp, "displayDigits").Get(expectedLength: Bomb.GetSolvableModuleNames().Count, validator: d => d < 0 || d > 9 ? "expected range 0-9" : null);
        addQuestions(module, displayedDigits.Select((d, ix) => makeQuestion(Question.ForgetMeNowDisplayedDigits, _ForgetMeNow, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { d.ToString() })));
    }

    private IEnumerable<object> ProcessForgetsUltimateShowdown(KMBombModule module)
    {
        var comp = GetComponent(module, "ForgetsUltimateShowdownScript");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var methods = GetField<IList>(comp, "_usedMethods").Get();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_ForgetsUltimateShowdown);
        if (methods.Count != 4)
            throw new AbandonModuleException($"‘methods’ had an invalid length: {methods.Count}, expected 4");

        var answer = GetField<string>(comp, "_answer").Get();
        var initial = GetField<string>(comp, "_initialNumber").Get();
        var bottom = GetField<string>(comp, "_bottomNumber").Get();
        var methodNames = methods.Cast<object>().Select(x => GetProperty<string>(x, "Name", isPublic: true).Get()).ToList();

        var questions = new List<QandA>();
        for (int i = 0; i < 12; i++)
        {
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownAnswer, _ForgetsUltimateShowdown, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { answer[i].ToString() }));
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownBottom, _ForgetsUltimateShowdown, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { bottom[i].ToString() }));
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownInitial, _ForgetsUltimateShowdown, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { initial[i].ToString() }));
        }
        for (int i = 0; i < 4; i++)
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownMethod, _ForgetsUltimateShowdown, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { methodNames[i].Replace("'", "’") }));
        addQuestions(module, questions);
    }

    private List<List<byte>> _ftcGearNumbers = new List<List<byte>>();
    private List<List<short>> _ftcLargeDisplays = new List<List<short>>();
    private List<List<int>> _ftcSineNumbers = new List<List<int>>();
    private List<List<string>> _ftcGearColors = new List<List<string>>();
    private List<List<string>> _ftcRuleColors = new List<List<string>>();
    private IEnumerable<object> ProcessForgetTheColors(KMBombModule module)
    {
        var comp = GetComponent(module, "FTCScript");

        var myGearNumbers = GetListField<byte>(comp, "gear").Get();
        var myLargeDisplays = GetListField<short>(comp, "largeDisplay").Get();
        var mySineNumbers = GetListField<int>(comp, "sineNumber").Get();
        var myGearColors = GetListField<string>(comp, "gearColor").Get();
        var myRuleColors = GetListField<string>(comp, "ruleColor").Get();

        _ftcGearNumbers.Add(myGearNumbers);
        _ftcLargeDisplays.Add(myLargeDisplays);
        _ftcSineNumbers.Add(mySineNumbers);
        _ftcGearColors.Add(myGearColors);
        _ftcRuleColors.Add(myRuleColors);

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ForgetTheColors);

        var allLists = new IList[] { _ftcGearNumbers, _ftcLargeDisplays, _ftcSineNumbers, _ftcGearColors, _ftcRuleColors };
        if (allLists.Any(l => l.Count != _ftcGearColors.Count))
            throw new AbandonModuleException($"One or more of the lists of sets of information are not the same length as the others. AllGears: {_ftcGearNumbers.Count}, AllLargeDisplays: {_ftcLargeDisplays.Count}, AllSineNumbers: {_ftcSineNumbers.Count}, AllGearColors: {_ftcGearColors.Count}, AllRuleColors: {_ftcRuleColors.Count}");

        if (myGearColors.Count == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget The Colors because there were no stages.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        foreach (var list in allLists)
            for (int ix = 0; ix < list.Count - 1; ix++)
                if ((list[ix] as IList).Count != (list[ix + 1] as IList).Count)
                    throw new AbandonModuleException("One or more of the lists of sets of information have different lengths across modules.");

        if (!new[] { myLargeDisplays.Count, mySineNumbers.Count, myGearColors.Count, myRuleColors.Count }.All(x => x == myGearNumbers.Count))
            throw new AbandonModuleException($"One or more of the lists of information for this module are not the same length as the others. Gears: {myGearNumbers.Count}, LargeDisplays: {myLargeDisplays.Count}, SineNumbers: {mySineNumbers.Count}, GearColors: {myGearColors.Count}, RuleColors: {myRuleColors.Count}");

        var colors = GetAnswers(Question.ForgetTheColorsGearColor);
        for (int i = 0; i < myGearNumbers.Count; i++)
        {
            if (myGearNumbers[i] < 0 || myGearNumbers[i] > 9)
                throw new AbandonModuleException($"‘gear[{i}]’ had an unexpected value. (Expected 0-9): {myGearNumbers[i]}");
            if (myLargeDisplays[i] < 0 || myLargeDisplays[i] > 990)
                throw new AbandonModuleException($"‘largeDisplay[{i}]’ had an unexpected value. (Expected 0-990): {myLargeDisplays[i]}");
            if (mySineNumbers[i] < -99999 || mySineNumbers[i] > 99999)
                throw new AbandonModuleException($"‘sineNumber[{i}]’ had an unexpected value. (Expected (-99999)-99999): {mySineNumbers[i]}");
            if (!colors.Contains(myGearColors[i]))
                throw new AbandonModuleException($"‘gearColor[{i}]’ had an unexpected value. (Expected {colors.JoinString(", ")}): {mySineNumbers[i]}");
            if (!colors.Contains(myRuleColors[i]))
                throw new AbandonModuleException($"‘ruleColor[{i}]’ had an unexpected value. (Expected {colors.JoinString(", ")}): {myRuleColors[i]}");
        }

        var chosenStage = Rnd.Range(0, myGearNumbers.Count);
        var formattedName = "Forget The Colors";

        if (_moduleCounts[_ForgetTheColors] > 1)
        {
            for (int ix = 0; ix < myGearNumbers.Count; ix++)
            {
                if (ix == chosenStage)
                    continue;
                if (_ftcGearNumbers.Count(l => l[ix] == myGearNumbers[ix]) == 1)
                    formattedName = $"the Forget The Colors whose gear number was {myGearNumbers[ix]} in stage {ix}";
                if (_ftcLargeDisplays.Count(l => l[ix] == myLargeDisplays[ix]) == 1)
                    formattedName = $"the Forget The Colors which had {myLargeDisplays[ix]} on its large display in stage {ix}";
                if (_ftcSineNumbers.Count(l => l[ix] == mySineNumbers[ix]) == 1)
                    formattedName = $"the Forget The Colors whose received sine number in stage {ix} ended with a {Mathf.Abs(mySineNumbers[ix]) % 10}";
                if (_ftcGearColors.Count(l => l[ix] == myGearColors[ix]) == 1)
                    formattedName = $"the Forget The Colors whose gear color was {myGearColors[ix]} in stage {ix}";
                if (_ftcRuleColors.Count(l => l[ix] == myRuleColors[ix]) == 1)
                    formattedName = $"the Forget The Colors whose rule color was {myRuleColors[ix]} in stage {ix}";
                if (formattedName != "Forget The Colors")
                    break;
            }
            if (formattedName == "Forget The Colors")
            {
                Debug.Log($"[Souvenir #{_moduleId}] No question for Forget The Colors because there are not enough stages at which this one (#{GetIntField(comp, "_moduleId").Get()}) is unique.");
                _legitimatelyNoQuestions.Add(module);
                yield break;
            }
        }

        var stage = chosenStage.ToString();
        addQuestions(module,
            makeQuestion(Question.ForgetTheColorsGearNumber, _ForgetTheColors, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myGearNumbers[chosenStage].ToString() }),
            makeQuestion(Question.ForgetTheColorsLargeDisplay, _ForgetTheColors, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myLargeDisplays[chosenStage].ToString() }),
            makeQuestion(Question.ForgetTheColorsSineNumber, _ForgetTheColors, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { (Mathf.Abs(mySineNumbers[chosenStage]) % 10).ToString() }),
            makeQuestion(Question.ForgetTheColorsGearColor, _ForgetTheColors, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myGearColors[chosenStage].ToString() }),
            makeQuestion(Question.ForgetTheColorsRuleColor, _ForgetTheColors, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myRuleColors[chosenStage].ToString() }));
    }

    private List<List<int>> _ftColors = new List<List<int>>();
    private List<List<int>> _ftDigits = new List<List<int>>();
    private IEnumerable<object> ProcessForgetThis(KMBombModule module)
    {
        var comp = GetComponent(module, "ForgetThis");

        if (GetField<bool>(comp, "autoSolved").Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget This because it solved itself due to a lack of stages.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var myColors = GetListField<int>(comp, "stageColors").Get();
        var myDigits = GetListField<int>(comp, "stageNumbers").Get();

        if (myColors.Count != myDigits.Count)
            throw new AbandonModuleException($"The number of colors ({myColors.Count}) did not match the number of digits ({myDigits.Count})");
        if (_ftColors.Any() && _ftColors.Last().Count != myColors.Count)
            throw new AbandonModuleException("The number of colors was not consistent across all Forget This modules.");
        if (_ftDigits.Any() && _ftDigits.Last().Count != myDigits.Count)
            throw new AbandonModuleException("The number of digits was not consistent across all Forget This modules.");

        _ftColors.Add(myColors);
        _ftDigits.Add(myDigits);

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ForgetThis);

        var displayedStagesCount = GetIntField(comp, "curStageNum").Get(min: 0, max: myColors.Count);

        var allColors = new[] { "Cyan", "Magenta", "Yellow", "Black", "White", "Green" };
        var base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var chosenStage = Rnd.Range(0, displayedStagesCount);

        var formattedName = "Forget This";
        if (_moduleCounts[_ForgetThis] > 1)
        {
            for (int stage = 0; stage < displayedStagesCount; stage++)
            {
                if (stage == chosenStage)
                    continue;
                if (_ftColors.Count(c => c[stage] == myColors[stage]) == 1)
                    formattedName = $"the Forget This whose LED was {allColors[myColors[stage]]} in stage {stage + 1}";
                if (_ftDigits.Count(d => d[stage] == myDigits[stage]) == 1)
                    formattedName = $"the Forget This which displayed a {base36[myDigits[stage]]} in stage {stage + 1}";
                if (formattedName != "Forget This")
                    break;
            }
            if (formattedName == "Forget This")
            {
                Debug.Log($"[Souvenir #{_moduleId}] No question for Forget This because there were not enough stages in which this one (#{GetIntField(comp, "_moduleId").Get()}) was unique.");
                _legitimatelyNoQuestions.Add(module);
                yield break;
            }
        }
        addQuestions(module,
            makeQuestion(Question.ForgetThisColors, _ForgetThis, formattedModuleName: formattedName, formatArgs: new[] { ordinal(chosenStage + 1) }, correctAnswers: new[] { allColors[myColors[chosenStage]] }),
            makeQuestion(Question.ForgetThisDigits, _ForgetThis, formattedModuleName: formattedName, formatArgs: new[] { ordinal(chosenStage + 1) }, correctAnswers: new[] { base36[myDigits[chosenStage]].ToString() }));
    }

    private IEnumerable<object> ProcessFreeParking(KMBombModule module)
    {
        var comp = GetComponent(module, "FreeParkingScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        var tokens = GetArrayField<Material>(comp, "tokenOptions", isPublic: true).Get(expectedLength: 7);
        var selected = GetIntField(comp, "tokenIndex").Get(0, tokens.Length - 1);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_FreeParking);
        addQuestion(module, Question.FreeParkingToken, correctAnswers: new[] { tokens[selected].name });
    }

    private IEnumerable<object> ProcessFunctions(KMBombModule module)
    {
        var comp = GetComponent(module, "qFunctions");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Functions);

        var lastDigit = GetIntField(comp, "firstLastDigit").Get(-1, 9);
        if (lastDigit == -1)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No questions for Functions because it was solved with no queries! This isn’t a bug, just impressive (or cheating).");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var lNum = GetIntField(comp, "numberA").Get(1, 999);
        var rNum = GetIntField(comp, "numberB").Get(1, 999);
        var theLetter = GetField<string>(comp, "ruleLetter").Get(s => s.Length != 1 ? "expected length 1" : null);

        addQuestions(module,
            makeQuestion(Question.FunctionsLastDigit, _Functions, correctAnswers: new[] { lastDigit.ToString() }),
            makeQuestion(Question.FunctionsLeftNumber, _Functions, correctAnswers: new[] { lNum.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(1, 999).ToString()).Distinct().Take(6).ToArray()),
            makeQuestion(Question.FunctionsLetter, _Functions, correctAnswers: new[] { theLetter }),
            makeQuestion(Question.FunctionsRightNumber, _Functions, correctAnswers: new[] { rNum.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(1, 999).ToString()).Distinct().Take(6).ToArray()));
    }

    private IEnumerable<object> ProcessFuseBox(KMBombModule module)
    {
        var comp = GetComponent(module, "FuseBoxScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldAnimating = GetField<bool>(comp, "animating");
        var fldOpened = GetField<bool>(comp, "opened");
        var mthToggleDoor = GetMethod<IEnumerator>(comp, "ToggleDoor", 0);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        var children = comp.GetComponent<KMSelectable>().Children;
        if (children.Length == 0)
        {
            // Another Souvenir is already taking care of the coroutine
            while (fldAnimating.Get() || fldOpened.Get())
                yield return new WaitForSeconds(0.1f);
        }
        else
        {
            // Disable all the button handlers
            foreach (var button in children)
                button.OnInteract = () => false;

            // Set the children array to an empty array to signal to other Souvenirs on the same bomb that we’re already taking care of this
            comp.GetComponent<KMSelectable>().Children = new KMSelectable[0];

            while (fldAnimating.Get())
                yield return new WaitForSeconds(0.1f);
            if (fldOpened.Get())
                yield return ((MonoBehaviour) comp).StartCoroutine(mthToggleDoor.Invoke(new object[0]));
        }

        _modulesSolved.IncSafe(_FuseBox);

        var flashes = GetField<int[]>(comp, "lightColors")
            .Get(arr => arr.Length != 4 ? "Bad length" : arr.Any(i => i < 0 || i > 3) ? "Bad item" : null)
            .ToList();
        var qs = new List<QandA>(8);
        var arrows = GetField<int[]>(comp, "correctButtons")
            .Get(arr => arr.Length != 4 ? "Bad length" : arr.Any(i => i < 0 || i > 3) ? "Bad item" : null)
            .ToList();

        for (int ix = 0; ix < 4; ix++)
        {
            var tex = FuseBoxQuestions.First(t => t.name.Equals($"flash{ix + 1}"));
            var tex2 = FuseBoxQuestions.First(t => t.name.Equals($"arrow{ix + 1}"));

            if (_moduleCounts.Get(_FuseBox) > 1)
            {
                var num = _modulesSolved.Get(_FuseBox).ToString();
                var tmp = new Texture2D(400, 320, TextureFormat.ARGB32, false);
                var tmp2 = new Texture2D(400, 320, TextureFormat.ARGB32, false);
                tmp.SetPixels(tex.GetPixels());
                tmp2.SetPixels(tex2.GetPixels());

                tex = FuseBoxQuestions.First(t => t.name.Equals("name"));
                tmp.SetPixels(20, 120, tex.width, tex.height, tex.GetPixels());
                tmp2.SetPixels(32, 0, tex.width, tex.height, tex.GetPixels());
                for (var digit = 0; digit < num.Length; digit++)
                {
                    tex = DigitTextures[num[digit] - '0'];
                    tmp.SetPixels(20 + tex.width + 40 * digit, 120, tex.width, tex.height, tex.GetPixels());
                    tmp2.SetPixels(32 + tex.width + 40 * digit, 0, tex.width, tex.height, tex.GetPixels());
                }

                tmp.Apply(false, true);
                tmp2.Apply(false, true);
                _questionTexturesToDestroyLater.Add(tmp);
                _questionTexturesToDestroyLater.Add(tmp2);
                tex = tmp;
                tex2 = tmp2;
            }

            var q = Sprite.Create(tex, Rect.MinMaxRect(0f, 0f, 400f, 320f), new Vector2(.5f, .5f), 1280f, 1u, SpriteMeshType.Tight);
            var q2 = Sprite.Create(tex2, Rect.MinMaxRect(0f, 0f, 400f, 320f), new Vector2(.5f, .5f), 1280f, 1u, SpriteMeshType.Tight);
            q.name = $"FuseBox-Flash-{ix}-{_moduleCounts.Get(_FuseBox)}";
            q2.name = $"FuseBox-Arrow-{ix}-{_moduleCounts.Get(_FuseBox)}";
            qs.Add(makeSpriteQuestion(q, Question.FuseBoxFlashes, _FuseBox, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { FuseBoxColorSprites[flashes[ix]] }));
            qs.Add(makeSpriteQuestion(q2, Question.FuseBoxArrows, _FuseBox, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { FuseBoxArrowSprites[arrows[ix]] }));
        }

        addQuestions(module, qs);
    }
}