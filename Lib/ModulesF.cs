using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Souvenir;
using UnityEngine;
using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessFactoringMaze(ModuleData module)
    {
        var comp = GetComponent(module, "FactoringMazeScript");
        yield return WaitForSolve;

        var chosenPrimes = GetArrayField<int>(comp, "chosenPrimes").Get(expectedLength: 4);
        addQuestion(module, Question.FactoringMazeChosenPrimes, correctAnswers: chosenPrimes.Select(i => i.ToString()).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessFactoryMaze(ModuleData module)
    {
        var comp = GetComponent(module, "FactoryMazeScript");
        yield return WaitForSolve;

        var usedRooms = GetArrayField<string>(comp, "usedRooms").Get(expectedLength: 5).ToArray();
        var startRoom = GetIntField(comp, "startRoom").Get(0, usedRooms.Length - 1);

        for (int i = usedRooms.Length - 1; i >= 0; --i)
            usedRooms[i] = usedRooms[i].Replace('\n', ' ');

        addQuestion(module, Question.FactoryMazeStartRoom, correctAnswers: new[] { usedRooms[startRoom] }, preferredWrongAnswers: usedRooms);
    }

    private IEnumerator<YieldInstruction> ProcessFastMath(ModuleData module)
    {
        var comp = GetComponent(module, "FastMathModule");
        var fldScreen = GetField<TextMesh>(comp, "Screen", isPublic: true);
        var usableLetters = GetField<string>(comp, "letters").Get();

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var wrongAnswers = new HashSet<string>();
        string letters = null;
        while (module.Unsolved)
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

        while (wrongAnswers.Count < 6)
            foreach (var ans in new AnswerGenerator.Strings(2, usableLetters).GetAnswers(this).Take(6 - wrongAnswers.Count))
                wrongAnswers.Add(ans);

        addQuestion(module, Question.FastMathLastLetters, correctAnswers: new[] { letters }, preferredWrongAnswers: wrongAnswers.ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessFaultyButtons(ModuleData module)
    {
        var comp = GetComponent(module, "FaultyButtonsScript");

        yield return WaitForSolve;

        var referredButtons = GetField<int[]>(comp, "ReferredButtons").Get();
        var qs = new List<QandA>();
        for (int pos = 0; pos < 16; pos++)
        {
            var buttonRefersTo = new Coord(4, 4, referredButtons[pos]);
            var refersToButton = new Coord(4, 4, Array.IndexOf(referredButtons, pos));
            qs.Add(makeQuestion(Question.FaultyButtonsReferredToThisButton, module, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { refersToButton }, preferredWrongAnswers: new[] { buttonRefersTo }));
            qs.Add(makeQuestion(Question.FaultyButtonsThisButtonReferredTo, module, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { buttonRefersTo }, preferredWrongAnswers: new[] { refersToButton }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessFaultyRGBMaze(ModuleData module)
    {
        var comp = GetComponent(module, "FaultyRGBMazeScript");
        yield return WaitForSolve;

        var keyPos = GetArrayField<int[]>(comp, "keylocations").Get(expectedLength: 3, validator: key => key.Length != 2 ? "expected length 2" : key.Any(number => number < 0 || number > 6) ? "expected range 0–6" : null);
        var mazeNum = GetArrayField<int[]>(comp, "mazenumber").Get(expectedLength: 3, validator: maze => maze.Length != 2 ? "expected length 2" : maze[0] < 0 || maze[0] > 15 ? "expected range 0–15" : null);
        var exitPos = GetArrayField<int>(comp, "exitlocation").Get(expectedLength: 3);

        if (exitPos[1] < 0 || exitPos[1] > 6 || exitPos[2] < 0 || exitPos[2] > 6)
            throw new AbandonModuleException($"‘exitPos’ contains invalid coordinate: ({exitPos[2]},{exitPos[1]})");

        string[] colors = { "red", "green", "blue" };

        var qs = new List<QandA>();

        for (int index = 0; index < 3; index++)
        {
            qs.Add(makeQuestion(Question.FaultyRGBMazeKeys, module,
                formatArgs: new[] { colors[index] },
                correctAnswers: new[] { "ABCDEFG"[keyPos[index][1]] + (keyPos[index][0] + 1).ToString() }));
            qs.Add(makeQuestion(Question.FaultyRGBMazeNumber, module,
                formatArgs: new[] { colors[index] },
                correctAnswers: new[] { "0123456789abcdef"[mazeNum[index][0]].ToString() }));
        }

        qs.Add(makeQuestion(Question.FaultyRGBMazeExit, module,
            correctAnswers: new[] { "ABCDEFG"[exitPos[2]] + (exitPos[1] + 1).ToString() }));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessFindTheDate(ModuleData module)
    {
        var comp = GetComponent(module, "DateFinder");
        var fldStage = GetIntField(comp, "count");
        var fldDate = GetIntField(comp, "day");
        var fldMonth = GetField<string>(comp, "month");
        var fldYear = GetIntField(comp, "year");
        var fldCentury = GetIntField(comp, "century");

        var dateArr = new int[3];
        var yearArr = new string[3];
        var monthArr = new string[3];
        var currentStage = -1;

        while (module.Unsolved)
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

        var qs = new List<QandA>();

        for (int i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.FindTheDateMonth, module,
            formatArgs: new[] { ordinal(i + 1) },
            correctAnswers: new[] { monthArr[i] }));

            qs.Add(makeQuestion(Question.FindTheDateDay, module,
            formatArgs: new[] { ordinal(i + 1) },
            correctAnswers: new[] { dateArr[i].ToString() }));

            qs.Add(makeQuestion(Question.FindTheDateYear, module,
            formatArgs: new[] { ordinal(i + 1) },
            correctAnswers: new[] { yearArr[i] }));
        }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessFiveLetterWords(ModuleData module)
    {
        var comp = GetComponent(module, "FiveLetterWords");

        yield return WaitForSolve;

        var wordList = JsonConvert.DeserializeObject<string[]>(GetField<TextAsset>(comp, "FiverData", isPublic: true).Get().text);
        var displayedWords = GetArrayField<string>(comp, "TheNames").Get(expectedLength: 3, validator: name => name.Length != 5 ? "expected length 5" : null);
        addQuestion(module, Question.FiveLetterWordsDisplayedWords, correctAnswers: displayedWords, preferredWrongAnswers: wordList);
    }

    private IEnumerator<YieldInstruction> ProcessFizzBuzz(ModuleData module)
    {
        var comp = GetComponent(module, "FizzBuzzModule");
        var labels = GetArrayField<TextMesh>(comp, "Labels", isPublic: true).Get(expectedLength: 3);
        var solutions = GetArrayField<int[]>(comp, "Solutions").Get(expectedLength: 2, nullContentAllowed: true);

        while (solutions.GetValue(0) is null)
            yield return null; // Don't wait 0.1 seconds to make sure we get the labels before they are changed.

        var labelTexts = labels.Select(t => t.text).ToArray();
        var displayedDigits = new List<string>();
        foreach (var text in labelTexts)
        {
            var match = Regex.Match(text, @"^(?:[RGBYW]\s)?(\d{7})$");
            if (!match.Success)
                throw new AbandonModuleException($"Unexpected display value: “{text}”.");
            displayedDigits.Add(match.Groups[1].Value);
        }

        yield return WaitForSolve;

        var qs = new List<QandA>();
        var displays = new[] { "top", "middle", "bottom" };
        for (int pos = 0; pos < 3; pos++)
        {
            if (labelTexts[pos] != labels[pos].text)
            {
                for (int digit = 0; digit < 6; digit++)
                    qs.Add(makeQuestion(Question.FizzBuzzDisplayedNumbers, module, formatArgs: new[] { ordinal(digit + 1), displays[pos] }, correctAnswers: new[] { displayedDigits[pos][digit].ToString() }));
                if (!labels[pos].text.ToLowerInvariant().Contains("buzz")) // Do not ask about the last digit if the answer was buzz because there are only two possible correct answers.
                    qs.Add(makeQuestion(Question.FizzBuzzDisplayedNumbers, module, formatArgs: new[] { "7th", displays[pos] }, correctAnswers: new[] { displayedDigits[pos][6].ToString() }));
            }
        }
        if (qs.Count == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No questions for FizzBuzz because all of the numbers remained on the displays.");
            _legitimatelyNoQuestions.Add(module.Module);
        }
        else
            addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessFlags(ModuleData module)
    {
        var comp = GetComponent(module, "FlagsModule");
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

        yield return WaitForSolve;

        addQuestions(module,
            // Displayed number
            makeQuestion(Question.FlagsDisplayedNumber, module, correctAnswers: new[] { number.ToString() }),
            // Main country flag
            makeQuestion(Question.FlagsMainCountry, module, correctAnswers: new[] { mainCountrySprite }, preferredWrongAnswers: otherCountrySprites),
            // Rest of the country flags
            makeQuestion(Question.FlagsCountries, module, correctAnswers: otherCountrySprites, preferredWrongAnswers: FlagsSprites));
    }

    private IEnumerator<YieldInstruction> ProcessFlashingArrows(ModuleData module)
    {
        var comp = GetComponent(module, "FlashingArrowsScript");

        yield return WaitForSolve;

        var colorReference = GetArrayField<string>(comp, "debugColors").Get(expectedLength: 7);
        var displayedValue = GetField<int>(comp, "displayNumber").Get(num => num < 0 || num >= 100 ? "Expected the displayed value to be within 0 and 99 inclusive." : null);
        var idxReferencedArrow = GetField<int>(comp, "idxReferencedArrow").Get(num => num < 0 || num >= 4 ? "Expected the value to be within 0 and 3 inclusive." : null);
        var idxFlashedArrows = GetArrayField<int[]>(comp, "idxColorFlashingArrows").Get(expectedLength: 4);
        var arrowSet = idxFlashedArrows[idxReferencedArrow];
        var idxBlack = Array.IndexOf(arrowSet, -1);
        var colorAfterBlack = arrowSet[(idxBlack + 1) % 3];
        var colorBeforeBlack = arrowSet[(idxBlack + 2) % 3];

        addQuestions(module,
            makeQuestion(Question.FlashingArrowsDisplayedValue, module, correctAnswers: new[] { displayedValue.ToString() }),
            makeQuestion(Question.FlashingArrowsReferredArrow, module, formatArgs: new[] { "before" }, correctAnswers: new[] { colorReference[colorBeforeBlack] }, preferredWrongAnswers: colorReference),
            makeQuestion(Question.FlashingArrowsReferredArrow, module, formatArgs: new[] { "after" }, correctAnswers: new[] { colorReference[colorAfterBlack] }, preferredWrongAnswers: colorReference));
    }

    private IEnumerator<YieldInstruction> ProcessFlashingLights(ModuleData module)
    {
        var comp = GetComponent(module, "doubleNegativesScript");
        yield return WaitForSolve;

        var topColors = GetListField<int>(comp, "selectedColours").Get(expectedLength: 12);
        var bottomColors = GetListField<int>(comp, "selectedColours2").Get(expectedLength: 12);
        var colorNames = new[] { "cyan", "green", "red", "purple", "orange" };
        var topTotals = Enumerable.Range(1, 5).Select(num => topColors.Count(x => x == num)).ToArray();
        var bottomTotals = Enumerable.Range(1, 5).Select(num => bottomColors.Count(x => x == num)).ToArray();

        var qs = new List<QandA>();
        for (int i = 0; i < 5; i++)
        {
            qs.Add(makeQuestion(Question.FlashingLightsLEDFrequency, module, formatArgs: new[] { "top", colorNames[i] }, correctAnswers: new[] { topTotals[i].ToString() }, preferredWrongAnswers: new[] { bottomTotals[i].ToString() }));
            qs.Add(makeQuestion(Question.FlashingLightsLEDFrequency, module, formatArgs: new[] { "bottom", colorNames[i] }, correctAnswers: new[] { bottomTotals[i].ToString() }, preferredWrongAnswers: new[] { topTotals[i].ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessFlavorText(ModuleData module)
    {
        var comp = GetComponent(module, "FlavorText");
        yield return WaitForSolve;

        var textOptionList = GetField<IList>(comp, "textOptions").Get();
        var textOption = GetField<object>(comp, "textOption").Get();
        var fldName = GetField<string>(textOption, "name", isPublic: true);
        var moduleNames = Enumerable.Range(0, textOptionList.Count).Select(index => fldName.GetFrom(textOptionList[index])).ToArray();

        addQuestion(module, Question.FlavorTextModule,
            correctAnswers: new[] { fldName.GetFrom(textOption) },
            preferredWrongAnswers: moduleNames);
    }

    private IEnumerator<YieldInstruction> ProcessFlavorTextEX(ModuleData module)
    {
        var comp = GetComponent(module, "FlavorTextCruel");
        var fldStage = GetIntField(comp, "stage");
        var fldTextOption = GetField<object>(comp, "textOption");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var maxStageAmount = GetIntField(comp, "maxStageAmount").Get();
        var answers = new string[maxStageAmount];
        var fldName = GetField<string>(fldTextOption.Get(), "name", isPublic: true);

        while (fldStage.Get() < maxStageAmount)
        {
            answers[fldStage.Get()] = fldName.GetFrom(fldTextOption.Get());
            yield return null;
        }

        if (answers.Any(a => a == null))
            throw new AbandonModuleException($"Abandoning Flavor Text EX because Stage {Array.IndexOf(answers, null)} has a null entry.");

        var textOptionList = GetField<IList>(comp, "textOptions").Get();
        var moduleNames = Enumerable.Range(0, textOptionList.Count).Select(index => fldName.GetFrom(textOptionList[index])).ToArray();

        var qs = new List<QandA>();

        for (var i = 0; i < maxStageAmount; i++)
            qs.Add(makeQuestion(
                    question: Question.FlavorTextEXModule,
                    data: module,
                    formatArgs: new[] { ordinal(i + 1) },
                    correctAnswers: new[] { answers[i] },
                    preferredWrongAnswers: answers,
                    allAnswers: moduleNames));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessFlyswatting(ModuleData module)
    {
        var comp = GetComponent(module, "flyswattingScript");

        bool[] swatted = GetArrayField<int>(comp, "answers").Get(expectedLength: 5).Select(x => x == 1).ToArray();

        yield return WaitForSolve;

        string[] letters = GetArrayField<string>(comp, "chosens").Get(expectedLength: 5);
        string[] outsideLetters = letters.Where((_, pos) => !swatted[pos]).ToArray();
        if (outsideLetters.Length == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Flyswatting because every fly was part of the solution.");
            _legitimatelyNoQuestions.Add(module.Module);
        }
        else
            addQuestion(module, Question.FlyswattingUnpressed, correctAnswers: outsideLetters);
    }

    private IEnumerator<YieldInstruction> ProcessFollowMe(ModuleData module)
    {
        var comp = GetComponent(module, "FollowMe");

        yield return WaitForSolve;

        var directionWords = new Dictionary<string, string> { { "U", "Up" }, { "D", "Down" }, { "L", "Left" }, { "R", "Right" } };
        var path = GetListField<string>(comp, "Path").Get(minLength: 1, validator: x => !directionWords.ContainsKey(x) ? $"expected only {directionWords.Keys.JoinString(", ")}" : null);

        var qs = new List<QandA>();
        for (int pos = 0; pos < path.Count; pos++)
            qs.Add(makeQuestion(Question.FollowMeDisplayedPath, module, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { directionWords[path[pos]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessForestCipher(ModuleData module)
    {
        return processColoredCiphers(module, "forestCipher", Question.ForestCipherScreen);
    }

    private List<Array> _facCylinders = new();
    private List<List<int>> _facFigures = new();
    private IEnumerator<YieldInstruction> ProcessForgetAnyColor(ModuleData module)
    {
        var comp = GetComponent(module, "FACScript");
        const string moduleId = "ForgetAnyColor";

        var init = GetField<object>(comp, "init").Get();
        var fldCurrentStage = GetIntField(init, "stage");
        var fldCylinders = GetField<Array>(init, "cylinders");
        var calculate = GetField<object>(init, "calculate").Get();
        var fldFigures = GetListField<int>(calculate, "figureSequences");

        var activated = false;
        module.Module.OnActivate += () => activated = true;
        while (!activated)
            yield return null;
        yield return null; // Wait one extra frame to ensure that maxStage has been set.

        var maxStage = GetIntField(init, "maxStage").Get(min: 0);
        if (maxStage == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Any Color because there were no stages.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        var myCylinders = fldCylinders.Get(v => v.Rank != 2 || v.GetLength(0) != maxStage + 1 || v.GetLength(1) != 3 ? $"expected a {maxStage + 1}×3 2D array" : null);
        var myFigures = fldFigures.Get();
        _facCylinders.Add(myCylinders);
        _facFigures.Add(myFigures);

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(moduleId + "❖presolve");

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White" }
            .Select(str => translateString(Question.ForgetAnyColorCylinder, str)).ToArray();
        var figureNames = new[] { "LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR" }
            .Select(str => str.Select(ch => translateString(Question.ForgetAnyColorCylinder, ch.ToString())).JoinString()).ToArray();

        string getCylinders(Array cylinders, int stage) => string.Format(translateString(Question.ForgetAnyColorCylinder, "{0}, {1}, {2}"),
            Enumerable.Range(0, 3).Select(ix => colorNames[(int) cylinders.GetValue(stage, ix)]).ToArray());

        var randomStage = Rnd.Range(0, fldCurrentStage.Get(min: 0, max: maxStage));
        string formattedName = null;
        if (_moduleCounts[moduleId] > 1)
        {
            for (int stage = 0; stage < maxStage; stage++)
            {
                if (stage == randomStage)
                    continue;
                var cylindersThisStage = getCylinders(myCylinders, stage);
                var formatCandidates = new List<string>();
                if (_facFigures.Count(f => f[stage] == myFigures[stage]) == 1)
                    formatCandidates.Add(string.Format(
                        translateString(Question.ForgetAnyColorCylinder, "the Forget Any Color which used figure {0} in the {1} stage"),
                        figureNames[myFigures[stage]],
                        ordinal(stage + 1)));
                if (_facCylinders.Count(c => getCylinders(c, stage) == cylindersThisStage) == 1)
                    formatCandidates.Add(string.Format(
                        translateString(Question.ForgetAnyColorCylinder, "the Forget Any Color whose cylinders in the {0} stage were {1}"),
                        ordinal(stage + 1),
                        cylindersThisStage));
                if (formatCandidates.Count > 0)
                {
                    formattedName = formatCandidates.PickRandom();
                    break;
                }
            }
            if (formattedName == null)
            {
                Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Any Color because there were not enough stages where this one (#{GetIntField(init, "moduleId").Get()}) was unique.");
                _legitimatelyNoQuestions.Add(module.Module);
                yield break;
            }
        }

        formattedName ??= _translation?.Translations[Question.ForgetAnyColorCylinder].ModuleName ?? "Forget Any Color";
        var correctCylinders = getCylinders(myCylinders, randomStage);
        var preferredCylinders = new HashSet<string> { correctCylinders };
        while (preferredCylinders.Count < 6)
            preferredCylinders.Add(string.Format(translateString(Question.ForgetAnyColorCylinder, "{0}, {1}, {2}"),
                Enumerable.Range(0, 3).Select(i => colorNames.PickRandom()).ToArray()));

        addQuestions(module,
            makeQuestion(Question.ForgetAnyColorCylinder, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { ordinal(randomStage + 1) },
                correctAnswers: new[] { correctCylinders }, preferredWrongAnswers: preferredCylinders.ToArray()),
            makeQuestion(Question.ForgetAnyColorSequence, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { ordinal(randomStage + 1) },
                correctAnswers: new[] { figureNames[myFigures[randomStage]] }, allAnswers: figureNames));
    }

    private List<int[]> _feFirstDisplays = new();
    private IEnumerator<YieldInstruction> ProcessForgetEverything(ModuleData module)
    {
        var comp = GetComponent(module, "EvilMemory");
        const string moduleId = "HexiEvilFMN";

        var activated = false;
        module.Module.OnActivate += () => activated = true;
        while (!activated)
            yield return new WaitForSeconds(.1f);
        yield return null; // Wait one extra frame to ensure DialDisplay is set.

        var allDisplays = GetArrayField<int[]>(comp, "DialDisplay").Get(nullAllowed: true);
        if (allDisplays is null)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Everything because there were no stages.");
            _legitimatelyNoQuestions.Add(module.Module);
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
        _modulesSolved.IncSafe(moduleId + "❖presolve");

        var stageOrdering = GetArrayField<int>(comp, "StageOrdering").Get();
        var myIgnoredList = GetStaticField<string[]>(comp.GetType(), "ignoredModules", isPublic: true).Get();
        if (Array.IndexOf(stageOrdering, 0) + 1 > Bomb.GetSolvableModuleNames().Count(x => !myIgnoredList.Contains(x)))
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Everything because stage one was not displayed before non-ignored modules were solved.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        if (_feFirstDisplays.Count != _moduleCounts[moduleId])
            throw new AbandonModuleException($"The number of displays ({_feFirstDisplays.Count}) did not match the number of Forget Everything modules ({_moduleCounts[moduleId]}).");

        if (_moduleCounts[moduleId] == 1)
        {
            module.SolveIndex = 1;
            addQuestions(module, myFirstDisplay.Select((digit, pos) => makeQuestion(Question.ForgetEverythingStageOneDisplay, module, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { digit.ToString() })));
        }
        else
        {
            var uniquePositions = Enumerable.Range(0, 10).Where(pos => _feFirstDisplays.Count(dis => dis[pos] == myFirstDisplay[pos]) == 1).Take(2).ToArray();
            if (!uniquePositions.Any())
            {
                Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Everything because this one (#{GetIntField(comp, "thisLoggingID", isPublic: true)}) had a non-unique first stage.");
                _legitimatelyNoQuestions.Add(module.Module);
                yield break;
            }
            var qs = new List<QandA>();
            for (int pos = 0; pos < 10; pos++)
            {
                if (uniquePositions.Any(p => p != pos))
                {
                    var reference = uniquePositions.First(p => p != pos);
                    qs.Add(makeQuestion(Question.ForgetEverythingStageOneDisplay, moduleId, 0,
                        formattedModuleName: string.Format(translateString(Question.ForgetEverythingStageOneDisplay, "the Forget Everything whose {0} displayed digit in that stage was {1}"), ordinal(reference + 1), myFirstDisplay[reference]),
                        formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { myFirstDisplay[pos].ToString() }));
                }
            }
            addQuestions(module, qs);
        }
    }

    private IEnumerator<YieldInstruction> ProcessForgetMe(ModuleData module)
    {
        var comp = GetComponent(module, "NotForgetMeNotScript");
        yield return WaitForSolve;

        string[] positions = { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        int[] initState = GetArrayField<int>(comp, "givenPuzzle").Get(expectedLength: 9);
        addQuestions(module,
            Enumerable.Range(0, 9).Where(ix => initState[ix] != 0).Select(ix =>
            makeQuestion(Question.ForgetMeInitialState, module, formatArgs: new[] { positions[ix] }, correctAnswers: new[] { initState[ix].ToString() })));
    }

    private List<int[]> _forgetMeNotDisplays = new();
    private IEnumerator<YieldInstruction> ProcessForgetMeNot(ModuleData module)
    {
        var comp = GetComponent(module, "AdvancedMemory");
        const string moduleId = "MemoryV2";

        var fldDisplayedDigits = GetArrayField<int>(comp, "Display");
        var activated = false;
        module.Module.OnActivate += () => { activated = true; };
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
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(moduleId + "❖presolve");

        var myIgnoredList = GetStaticField<string[]>(comp.GetType(), "ignoredModules", isPublic: true).Get();
        var displayedStageCount = Bomb.GetSolvedModuleNames().Count(x => !myIgnoredList.Contains(x));

        if (_forgetMeNotDisplays.Count != _moduleCounts[moduleId])
            throw new AbandonModuleException("The number of displays did not match the number of Forget Me Not modules.");

        if (_moduleCounts[moduleId] == 1)
            addQuestions(module, myDisplay.Take(displayedStageCount).Select((digit, ix) => makeQuestion(Question.ForgetMeNotDisplayedDigits, moduleId, 1, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { digit.ToString() })));
        else
        {
            var uniqueStages = Enumerable.Range(1, displayedStageCount).Where(stage => _forgetMeNotDisplays.Count(display => display[stage - 1] == myDisplay[stage - 1]) == 1).Take(2).ToArray();
            if (uniqueStages.Length == 0 || displayedStageCount == 1)
            {
                var fmnId = GetIntField(comp, "thisLoggingID", isPublic: true).Get();
                Debug.Log($"[Souvenir #{_moduleId}] No question for Forget Me Not because there are not enough stages at which this one (#{fmnId}) had a unique displayed number.");
                _legitimatelyNoQuestions.Add(module.Module);
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
                        qs.Add(makeQuestion(Question.ForgetMeNotDisplayedDigits, moduleId, 0,
                            formattedModuleName: string.Format(translateString(Question.ForgetMeNotDisplayedDigits, "the Forget Me Not which displayed a {0} in the {1} stage"), myDisplay[uniqueStage - 1], ordinal(uniqueStage)),
                            formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { myDisplay[stage].ToString() }));
                    }
                }
                addQuestions(module, qs);
            }
        }
    }

    private IEnumerator<YieldInstruction> ProcessForgetMeNow(ModuleData module)
    {
        var comp = GetComponent(module, "ForgetMeNow");

        yield return WaitForSolve;

        var displayedDigits = GetArrayField<int>(comp, "displayDigits").Get(expectedLength: Bomb.GetSolvableModuleNames().Count, validator: d => d < 0 || d > 9 ? "expected range 0-9" : null);
        addQuestions(module, displayedDigits.Select((d, ix) => makeQuestion(Question.ForgetMeNowDisplayedDigits, module, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { d.ToString() })));
    }

    private IEnumerator<YieldInstruction> ProcessForgetsUltimateShowdown(ModuleData module)
    {
        var comp = GetComponent(module, "ForgetsUltimateShowdownScript");
        var methods = GetField<IList>(comp, "_usedMethods").Get();

        yield return WaitForSolve;

        if (methods.Count != 4)
            throw new AbandonModuleException($"‘methods’ had an invalid length: {methods.Count}, expected 4");

        var answer = GetField<string>(comp, "_answer").Get();
        var initial = GetField<string>(comp, "_initialNumber").Get();
        var bottom = GetField<string>(comp, "_bottomNumber").Get();
        var methodNames = methods.Cast<object>().Select(x => GetProperty<string>(x, "Name", isPublic: true).Get()).ToList();

        var questions = new List<QandA>();
        for (int i = 0; i < 12; i++)
        {
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownAnswer, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { answer[i].ToString() }));
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownBottom, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { bottom[i].ToString() }));
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownInitial, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { initial[i].ToString() }));
        }
        for (int i = 0; i < 4; i++)
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownMethod, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { methodNames[i].Replace("'", "’") }));
        addQuestions(module, questions);
    }

    private readonly List<List<byte>> _ftcGearNumbers = new();
    private readonly List<List<short>> _ftcLargeDisplays = new();
    private readonly List<List<int>> _ftcSineNumbers = new();
    private readonly List<List<string>> _ftcGearColors = new();
    private readonly List<List<string>> _ftcRuleColors = new();
    private IEnumerator<YieldInstruction> ProcessForgetTheColors(ModuleData module)
    {
        var comp = GetComponent(module, "FTCScript");
        const string moduleId = "ForgetTheColors";

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
        _modulesSolved.IncSafe(moduleId + "❖presolve");

        var allLists = new IList[] { _ftcGearNumbers, _ftcLargeDisplays, _ftcSineNumbers, _ftcGearColors, _ftcRuleColors };
        if (allLists.Any(l => l.Count != _ftcGearColors.Count))
            throw new AbandonModuleException($"One or more of the lists of sets of information are not the same length as the others. AllGears: {_ftcGearNumbers.Count}, AllLargeDisplays: {_ftcLargeDisplays.Count}, AllSineNumbers: {_ftcSineNumbers.Count}, AllGearColors: {_ftcGearColors.Count}, AllRuleColors: {_ftcRuleColors.Count}");

        if (myGearColors.Count == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget The Colors because there were no stages.");
            _legitimatelyNoQuestions.Add(module.Module);
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
        string formattedName = null;

        if (_moduleCounts[moduleId] > 1)
        {
            for (int ix = 0; ix < myGearNumbers.Count; ix++)
            {
                if (ix == chosenStage)
                    continue;
                var formatCandidates = new List<string>();
                if (_ftcGearNumbers.Count(l => l[ix] == myGearNumbers[ix]) == 1)
                    formatCandidates.Add(string.Format(translateString(Question.ForgetTheColorsGearNumber, "the Forget The Colors whose gear number was {0} in stage {1}"), myGearNumbers[ix], ix));
                if (_ftcLargeDisplays.Count(l => l[ix] == myLargeDisplays[ix]) == 1)
                    formatCandidates.Add(string.Format(translateString(Question.ForgetTheColorsGearNumber, "the Forget The Colors which had {0} on its large display in stage {1}"), myLargeDisplays[ix], ix));
                if (_ftcSineNumbers.Count(l => l[ix] == mySineNumbers[ix]) == 1)
                    formatCandidates.Add(string.Format(translateString(Question.ForgetTheColorsGearNumber, "the Forget The Colors whose received sine number in stage {1} ended with a {0}"), Mathf.Abs(mySineNumbers[ix]) % 10, ix));
                if (_ftcGearColors.Count(l => l[ix] == myGearColors[ix]) == 1)
                    formatCandidates.Add(string.Format(translateString(Question.ForgetTheColorsGearNumber, "the Forget The Colors whose gear color was {0} in stage {1}"), translateAnswer(Question.ForgetTheColorsGearColor, myGearColors[ix]), ix));
                if (_ftcRuleColors.Count(l => l[ix] == myRuleColors[ix]) == 1)
                    formatCandidates.Add(string.Format(translateString(Question.ForgetTheColorsGearNumber, "the Forget The Colors whose rule color was {0} in stage {1}"), translateAnswer(Question.ForgetTheColorsRuleColor, myRuleColors[ix]), ix));
                if (formatCandidates.Count > 0)
                {
                    formattedName = formatCandidates.PickRandom();
                    break;
                }
            }
            if (formattedName == null)
            {
                Debug.Log($"[Souvenir #{_moduleId}] No question for Forget The Colors because there are not enough stages at which this one (#{GetIntField(comp, "_moduleId").Get()}) is unique.");
                _legitimatelyNoQuestions.Add(module.Module);
                yield break;
            }
        }
        formattedName ??= _translation?.Translations[Question.ForgetTheColorsGearNumber].ModuleName ?? "Forget The Colors";

        var stage = chosenStage.ToString();
        addQuestions(module,
            makeQuestion(Question.ForgetTheColorsGearNumber, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myGearNumbers[chosenStage].ToString() }),
            makeQuestion(Question.ForgetTheColorsLargeDisplay, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myLargeDisplays[chosenStage].ToString() }),
            makeQuestion(Question.ForgetTheColorsSineNumber, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { (Mathf.Abs(mySineNumbers[chosenStage]) % 10).ToString() }),
            makeQuestion(Question.ForgetTheColorsGearColor, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myGearColors[chosenStage].ToString() }),
            makeQuestion(Question.ForgetTheColorsRuleColor, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myRuleColors[chosenStage].ToString() }));
    }

    private List<List<int>> _ftColors = new();
    private List<List<int>> _ftDigits = new();
    private IEnumerator<YieldInstruction> ProcessForgetThis(ModuleData module)
    {
        var comp = GetComponent(module, "ForgetThis");
        const string moduleId = "forgetThis";

        if (GetField<bool>(comp, "autoSolved").Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Forget This because it solved itself due to a lack of stages.");
            _legitimatelyNoQuestions.Add(module.Module);
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
        _modulesSolved.IncSafe(moduleId + "❖presolve");

        var displayedStagesCount = GetIntField(comp, "curStageNum").Get(min: 0, max: myColors.Count);

        var allColors = new[] { "Cyan", "Magenta", "Yellow", "Black", "White", "Green" };
        var base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var chosenStage = Rnd.Range(0, displayedStagesCount);

        string formattedName = null;
        if (_moduleCounts[moduleId] > 1)
        {
            for (var stage = 0; stage < displayedStagesCount; stage++)
            {
                if (stage == chosenStage)
                    continue;
                var formatCandidates = new List<string>();
                if (_ftColors.Count(c => c[stage] == myColors[stage]) == 1)
                    formatCandidates.Add(string.Format(translateString(Question.ForgetThisColors, "the Forget This whose LED was {0} in the {1} stage"), translateAnswer(Question.ForgetThisColors, allColors[myColors[stage]]), ordinal(stage + 1)));
                if (_ftDigits.Count(d => d[stage] == myDigits[stage]) == 1)
                    formatCandidates.Add(string.Format(translateString(Question.ForgetThisColors, "the Forget This which displayed {0} in the {1} stage"), base36[myDigits[stage]], ordinal(stage + 1)));
                if (formatCandidates.Count > 0)
                {
                    formattedName = formatCandidates.PickRandom();
                    break;
                }
            }
            if (formattedName == null)
            {
                Debug.Log($"[Souvenir #{_moduleId}] No question for Forget This because there were not enough stages in which this one (#{GetIntField(comp, "_moduleId").Get()}) was unique.");
                _legitimatelyNoQuestions.Add(module.Module);
                yield break;
            }
        }
        formattedName ??= _translation?.Translations[Question.ForgetThisColors].ModuleName ?? "Forget This";
        addQuestions(module,
            makeQuestion(Question.ForgetThisColors, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { ordinal(chosenStage + 1) }, correctAnswers: new[] { allColors[myColors[chosenStage]] }),
            makeQuestion(Question.ForgetThisDigits, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { ordinal(chosenStage + 1) }, correctAnswers: new[] { base36[myDigits[chosenStage]].ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessFreeParking(ModuleData module)
    {
        var comp = GetComponent(module, "FreeParkingScript");

        var tokens = GetArrayField<Material>(comp, "tokenOptions", isPublic: true).Get(expectedLength: 7);
        var selected = GetIntField(comp, "tokenIndex").Get(0, tokens.Length - 1);

        yield return WaitForSolve;

        addQuestion(module, Question.FreeParkingToken, correctAnswers: new[] { tokens[selected].name });
    }

    private IEnumerator<YieldInstruction> ProcessFunctions(ModuleData module)
    {
        var comp = GetComponent(module, "qFunctions");
        yield return WaitForSolve;

        var lastDigit = GetIntField(comp, "firstLastDigit").Get(-1, 9);
        if (lastDigit == -1)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No questions for Functions because it was solved with no queries! This isn’t a bug, just impressive (or cheating).");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        var lNum = GetIntField(comp, "numberA").Get(1, 999);
        var rNum = GetIntField(comp, "numberB").Get(1, 999);
        var theLetter = GetField<string>(comp, "ruleLetter").Get(s => s.Length != 1 ? "expected length 1" : null);

        addQuestions(module,
            makeQuestion(Question.FunctionsLastDigit, module, correctAnswers: new[] { lastDigit.ToString() }),
            makeQuestion(Question.FunctionsLeftNumber, module, correctAnswers: new[] { lNum.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(1, 999).ToString()).Distinct().Take(6).ToArray()),
            makeQuestion(Question.FunctionsLetter, module, correctAnswers: new[] { theLetter }),
            makeQuestion(Question.FunctionsRightNumber, module, correctAnswers: new[] { rNum.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(1, 999).ToString()).Distinct().Take(6).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessFuseBox(ModuleData module)
    {
        var comp = GetComponent(module, "FuseBoxScript");
        var fldAnimating = GetField<bool>(comp, "animating");
        var fldOpened = GetField<bool>(comp, "opened");
        var mthToggleDoor = GetMethod<IEnumerator>(comp, "ToggleDoor", 0);

        yield return WaitForSolve;

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

        var flashes = GetField<int[]>(comp, "lightColors")
            .Get(arr => arr.Length != 4 ? "Bad length" : arr.Any(i => i < 0 || i > 3) ? "Bad item" : null)
            .ToList();
        var qs = new List<QandA>(8);
        var arrows = GetField<int[]>(comp, "correctButtons")
            .Get(arr => arr.Length != 4 ? "Bad length" : arr.Any(i => i < 0 || i > 3) ? "Bad item" : null)
            .ToList();

        var moduleCount = _moduleCounts.Get("FuseBox");
        for (int ix = 0; ix < 4; ix++)
        {
            var tex = FuseBoxQuestions.First(t => t.name.Equals($"flash{ix + 1}"));
            var tex2 = FuseBoxQuestions.First(t => t.name.Equals($"arrow{ix + 1}"));

            if (moduleCount > 1)
            {
                var num = module.SolveIndex.ToString();
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
            q.name = $"FuseBox-Flash-{ix}-{module.SolveIndex}";
            q2.name = $"FuseBox-Arrow-{ix}-{module.SolveIndex}";
            qs.Add(makeSpriteQuestion(q, Question.FuseBoxFlashes, module, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { FuseBoxColorSprites[flashes[ix]] }));
            qs.Add(makeSpriteQuestion(q2, Question.FuseBoxArrows, module, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { FuseBoxArrowSprites[arrows[ix]] }));
        }

        addQuestions(module, qs);
    }
}
