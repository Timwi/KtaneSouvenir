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

        for (var i = usedRooms.Length - 1; i >= 0; --i)
            usedRooms[i] = usedRooms[i].Replace('\n', ' ');

        addQuestion(module, Question.FactoryMazeStartRoom, correctAnswers: new[] { usedRooms[startRoom] }, preferredWrongAnswers: usedRooms);
    }

    private IEnumerator<YieldInstruction> ProcessFaerieFires(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "FaerieFiresScript");
        var fires = GetField<IList>(comp, "FaerieFires").Get(v => v.Count is not 6 ? "Expected 6 fires" : v.Cast<object>().Any(o => o is null) ? "Unexpected null fire" : null);
        var fldOrder = GetIntField(fires[0], "Order", true);
        var fldSound = GetField<string>(fires[0], "Sound", true);
        var fldName = GetField<string>(fires[0], "Name", true);

        var faeries = fires.Cast<object>().Select(f => (
            Order: fldOrder.GetFrom(f, 0, 5),
            Sound: fldSound.GetFrom(f, v => !Regex.IsMatch(v, "^FaerieGlitter[1-6]$") ? "Expected sound to match \"^FaerieGlitter[1-6]$\"" : null),
            Name: fldName.GetFrom(f, v => !Question.FaerieFiresColor.GetAnswers().Contains(v) ? "Unexpected color name" : null)));

        addQuestions(module, faeries.SelectMany(f => new[] {
            makeQuestion(Question.FaerieFiresPitchOrdinal, module,
                formatArgs: new[] { Ordinal(f.Order + 1) },
                correctAnswers: new[] { FaerieFiresAudio[f.Sound.Last() - '1'] }),
            makeQuestion(Question.FaerieFiresPitchColor, module,
                formatArgs: new[] { f.Name },
                correctAnswers: new[] { FaerieFiresAudio[f.Sound.Last() - '1'] }),
            makeQuestion(Question.FaerieFiresColor, module,
                formatArgs: new[] { Ordinal(f.Order + 1) },
                correctAnswers: new[] { f.Name })}));
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
    private IEnumerator<YieldInstruction> ProcessFastPlayfairCipher(ModuleData module)
    {
        var comp = GetComponent(module, "FastPlayfairCipher");
        var fldScreen = GetField<TextMesh>(comp, "DisplayedMessage", isPublic: true);
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var wrongAnswers = new HashSet<string>();
        string letters = null;
        while (module.Unsolved)
        {
            letters = fldScreen.Get().text;
            if (letters.Length is not 1 and not 2)
                throw new AbandonModuleException($"The screen contains something other than one or two characters: “{letters}” ({letters.Length} characters).");
            wrongAnswers.Add(letters);
            yield return new WaitForSeconds(.1f);
        }
        if (letters == null)
            throw new AbandonModuleException("No letters were extracted before the module was solved.");
        addQuestion(module, Question.FastPlayfairCipherLastMessage, correctAnswers: new[] { letters }, preferredWrongAnswers: wrongAnswers.ToArray());
    }
    private IEnumerator<YieldInstruction> ProcessFaultyButtons(ModuleData module)
    {
        var comp = GetComponent(module, "FaultyButtonsScript");

        yield return WaitForSolve;

        var referredButtons = GetField<int[]>(comp, "ReferredButtons").Get();
        var qs = new List<QandA>();
        for (var pos = 0; pos < 16; pos++)
        {
            var buttonRefersTo = new Coord(4, 4, referredButtons[pos]);
            var refersToButton = new Coord(4, 4, Array.IndexOf(referredButtons, pos));
            qs.Add(makeQuestion(Question.FaultyButtonsReferredToThisButton, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { refersToButton }, preferredWrongAnswers: new[] { buttonRefersTo }));
            qs.Add(makeQuestion(Question.FaultyButtonsThisButtonReferredTo, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { buttonRefersTo }, preferredWrongAnswers: new[] { refersToButton }));
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

        for (var index = 0; index < 3; index++)
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

        for (var i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.FindTheDateMonth, module,
            formatArgs: new[] { Ordinal(i + 1) },
            correctAnswers: new[] { monthArr[i] }));

            qs.Add(makeQuestion(Question.FindTheDateDay, module,
            formatArgs: new[] { Ordinal(i + 1) },
            correctAnswers: new[] { dateArr[i].ToString() }));

            qs.Add(makeQuestion(Question.FindTheDateYear, module,
            formatArgs: new[] { Ordinal(i + 1) },
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
        for (var pos = 0; pos < 3; pos++)
        {
            if (labelTexts[pos] != labels[pos].text)
            {
                for (var digit = 0; digit < 6; digit++)
                    qs.Add(makeQuestion(Question.FizzBuzzDisplayedNumbers, module, formatArgs: new[] { Ordinal(digit + 1), displays[pos] }, correctAnswers: new[] { displayedDigits[pos][digit].ToString() }));
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
        for (var i = 0; i < 5; i++)
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
                    formatArgs: new[] { Ordinal(i + 1) },
                    correctAnswers: new[] { answers[i] },
                    preferredWrongAnswers: answers,
                    allAnswers: moduleNames));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessFlyswatting(ModuleData module)
    {
        var comp = GetComponent(module, "flyswattingScript");

        var swatted = GetArrayField<int>(comp, "answers").Get(expectedLength: 5).Select(x => x == 1).ToArray();

        yield return WaitForSolve;

        var letters = GetArrayField<string>(comp, "chosens").Get(expectedLength: 5);
        var outsideLetters = letters.Where((_, pos) => !swatted[pos]).ToArray();
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
        for (var pos = 0; pos < path.Count; pos++)
            qs.Add(makeQuestion(Question.FollowMeDisplayedPath, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { directionWords[path[pos]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessForestCipher(ModuleData module) => processColoredCiphers(module, "forestCipher", Question.ForestCipherScreen);

    private readonly List<Array> _facCylinders = new();
    private readonly List<List<int>> _facFigures = new();
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

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White" }
            .Select(str => translateString(Question.ForgetAnyColorCylinder, str)).ToArray();
        var figureNames = new[] { "LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR" }
            .Select(str => str.Select(ch => translateString(Question.ForgetAnyColorCylinder, ch.ToString())).JoinString()).ToArray();

        string getCylinders(Array cylinders, int stage)
        {
            return string.Format(translateString(Question.ForgetAnyColorCylinder, "{0}, {1}, {2}"),
            Enumerable.Range(0, 3).Select(ix => colorNames[(int) cylinders.GetValue(stage, ix)]).ToArray());
        }

        var randomStage = Rnd.Range(0, fldCurrentStage.Get(min: 0, max: maxStage));
        string formattedName = null;
        if (_moduleCounts[moduleId] > 1)
        {
            for (var stage = 0; stage < maxStage; stage++)
            {
                if (stage == randomStage)
                    continue;
                var cylindersThisStage = getCylinders(myCylinders, stage);
                var formatCandidates = new List<string>();
                if (_facFigures.Count(f => f[stage] == myFigures[stage]) == 1)
                    formatCandidates.Add(string.Format(
                        translateString(Question.ForgetAnyColorCylinder, "the Forget Any Color which used figure {0} in the {1} stage"),
                        figureNames[myFigures[stage]],
                        Ordinal(stage + 1)));
                if (_facCylinders.Count(c => getCylinders(c, stage) == cylindersThisStage) == 1)
                    formatCandidates.Add(string.Format(
                        translateString(Question.ForgetAnyColorCylinder, "the Forget Any Color whose cylinders in the {1} stage were {0}"),
                        cylindersThisStage,
                        Ordinal(stage + 1)));
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

        formattedName ??= _translation?.Translate(Question.ForgetAnyColorCylinder).ModuleName ?? "Forget Any Color";
        var correctCylinders = getCylinders(myCylinders, randomStage);
        var preferredCylinders = new HashSet<string> { correctCylinders };
        while (preferredCylinders.Count < 6)
            preferredCylinders.Add(string.Format(translateString(Question.ForgetAnyColorCylinder, "{0}, {1}, {2}"),
                Enumerable.Range(0, 3).Select(i => colorNames.PickRandom()).ToArray()));

        addQuestions(module,
            makeQuestion(Question.ForgetAnyColorCylinder, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { Ordinal(randomStage + 1) },
                correctAnswers: new[] { correctCylinders }, preferredWrongAnswers: preferredCylinders.ToArray()),
            makeQuestion(Question.ForgetAnyColorSequence, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { Ordinal(randomStage + 1) },
                correctAnswers: new[] { figureNames[myFigures[randomStage]] }, allAnswers: figureNames));
    }

    private readonly List<int[]> _feFirstDisplays = new();
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
            addQuestions(module, myFirstDisplay.Select((digit, pos) => makeQuestion(Question.ForgetEverythingStageOneDisplay, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { digit.ToString() })));
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
            for (var pos = 0; pos < 10; pos++)
            {
                if (uniquePositions.Any(p => p != pos))
                {
                    var reference = uniquePositions.Where(p => p != pos).PickRandom();
                    qs.Add(makeQuestion(Question.ForgetEverythingStageOneDisplay, moduleId, 0,
                        formattedModuleName: string.Format(translateString(Question.ForgetEverythingStageOneDisplay, "the Forget Everything whose {0} displayed digit in that stage was {1}"), Ordinal(reference + 1), myFirstDisplay[reference]),
                        formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { myFirstDisplay[pos].ToString() }));
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
        var initState = GetArrayField<int>(comp, "givenPuzzle").Get(expectedLength: 9);
        addQuestions(module,
            Enumerable.Range(0, 9).Where(ix => initState[ix] != 0).Select(ix =>
            makeQuestion(Question.ForgetMeInitialState, module, formatArgs: new[] { positions[ix] }, correctAnswers: new[] { initState[ix].ToString() })));
    }

    private readonly List<int[]> _forgetMeNotDisplays = new();
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

        var myIgnoredList = GetStaticField<string[]>(comp.GetType(), "ignoredModules", isPublic: true).Get();
        var displayedStageCount = Bomb.GetSolvedModuleNames().Count(x => !myIgnoredList.Contains(x));

        if (_forgetMeNotDisplays.Count != _moduleCounts[moduleId])
            throw new AbandonModuleException("The number of displays did not match the number of Forget Me Not modules.");

        if (_moduleCounts[moduleId] == 1)
            addQuestions(module, myDisplay.Take(displayedStageCount).Select((digit, ix) => makeQuestion(Question.ForgetMeNotDisplayedDigits, moduleId, 1, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { digit.ToString() })));
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
                for (var stage = 0; stage < displayedStageCount; stage++)
                {
                    var uniqueStage = uniqueStages.FirstOrDefault(s => s != stage + 1);
                    if (uniqueStage != 0)
                    {
                        qs.Add(makeQuestion(Question.ForgetMeNotDisplayedDigits, moduleId, 0,
                            formattedModuleName: string.Format(translateString(Question.ForgetMeNotDisplayedDigits, "the Forget Me Not which displayed a {0} in the {1} stage"), myDisplay[uniqueStage - 1], Ordinal(uniqueStage)),
                            formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { myDisplay[stage].ToString() }));
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
        addQuestions(module, displayedDigits.Select((d, ix) => makeQuestion(Question.ForgetMeNowDisplayedDigits, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { d.ToString() })));
    }

    private readonly List<List<int>> _forgetOurVoicesStages = new();
    private IEnumerator<YieldInstruction> ProcessForgetOurVoices(ModuleData module)
    {
        while (!_noUnignoredModulesLeft)
            yield return null;

        var comp = GetComponent(module, "FOVscript");
        const string moduleId = "forgetOurVoices";

        var numStages = GetField<int>(comp, "currentStage").Get();
        var soundIxs = GetArrayField<int>(comp, "initialString").Get(minLength: numStages, nullArrayAllowed: true, validator: x => x is < 0 or >= 250 ? "Out of range [0, 249]" : null);

        if (soundIxs is null)
        {
            legitimatelyNoQuestion(module, "There were no stages.");
            yield break;
        }

        var usedSoundIxs = soundIxs.Take(numStages).ToList();
        var allAudio = GetArrayField<AudioClip>(comp, "speakers", isPublic: true).Get(expectedLength: 250);

        if (_moduleCounts[moduleId] == 1)
        {
            addQuestions(module, usedSoundIxs.Select((v, i) => makeQuestion(Question.ForgetOurVoicesVoice, moduleId, 1, allAnswers: allAudio, correctAnswers: new[] { allAudio[v] }, formatArgs: new[] { Ordinal(i + 1) })));
            yield break;
        }

        _forgetOurVoicesStages.Add(usedSoundIxs);

        yield return null;

        if (_forgetOurVoicesStages.Any(s => s.Count != numStages))
            throw new AbandonModuleException("Stage counts were not consistent across modules.");

        var names = Ut.NewArray(
            "Umbra Moruka", "Dicey", "MásQuéÉlite", "Obvious", "1254",
            "Dbros1000", "Bomberjack", "Danielstigman", "Depresso", "ktane1",
            "OEGamer", "jTIS", "Krispy", "Grunkle Squeaky", "Arceus",
            "ScopingLandscape", "Emik", "GhostSalt", "Short_c1rcuit", "Eltrick",
            "Axodeau", "Asew", "Cooldoom", "Piissii", "CrazyCaleb");

        var qs = new List<QandA>();
        for (var stage = 0; stage < usedSoundIxs.Count; stage++)
        {
            var soundIx = usedSoundIxs[stage];
            var unique = Enumerable.Range(0, usedSoundIxs.Count).Where(s => s != stage && _forgetOurVoicesStages.Count(l => l[s] == usedSoundIxs[s]) == 1).ToArray();

            if (unique.Length > 0)
            {
                var u = unique.PickRandom();
                var format = string.Format(
                    translateString(Question.ForgetOurVoicesVoice, "the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage"),
                    usedSoundIxs[u] % 10, translateString(Question.ForgetOurVoicesVoice, names[usedSoundIxs[u] / 10]), Ordinal(u + 1));
                qs.Add(makeQuestion(Question.ForgetOurVoicesVoice, module, formattedModuleName: format, allAnswers: allAudio, correctAnswers: new[] { allAudio[soundIx] }, formatArgs: new[] { Ordinal(stage + 1) }));
            }
        }

        if (qs.Count == 0)
        {
            legitimatelyNoQuestion(module, $"There were not enough stages where this one (#{GetField<int>(comp, "moduleId").Get()}) was unique.");
            yield break;
        }

        addQuestions(module, qs);
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
        for (var i = 0; i < 12; i++)
        {
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownAnswer, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { answer[i].ToString() }));
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownBottom, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { bottom[i].ToString() }));
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownInitial, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { initial[i].ToString() }));
        }
        for (var i = 0; i < 4; i++)
            questions.Add(makeQuestion(Question.ForgetsUltimateShowdownMethod, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { methodNames[i].Replace("'", "’") }));
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
            for (var ix = 0; ix < list.Count - 1; ix++)
                if ((list[ix] as IList).Count != (list[ix + 1] as IList).Count)
                    throw new AbandonModuleException("One or more of the lists of sets of information have different lengths across modules.");

        if (!new[] { myLargeDisplays.Count, mySineNumbers.Count, myGearColors.Count, myRuleColors.Count }.All(x => x == myGearNumbers.Count))
            throw new AbandonModuleException($"One or more of the lists of information for this module are not the same length as the others. Gears: {myGearNumbers.Count}, LargeDisplays: {myLargeDisplays.Count}, SineNumbers: {mySineNumbers.Count}, GearColors: {myGearColors.Count}, RuleColors: {myRuleColors.Count}");

        var colors = Question.ForgetTheColorsGearColor.GetAnswers();
        for (var i = 0; i < myGearNumbers.Count; i++)
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
            for (var ix = 0; ix < myGearNumbers.Count; ix++)
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
        formattedName ??= _translation?.Translate(Question.ForgetTheColorsGearNumber).ModuleName ?? "Forget The Colors";

        var stage = chosenStage.ToString();
        addQuestions(module,
            makeQuestion(Question.ForgetTheColorsGearNumber, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myGearNumbers[chosenStage].ToString() }),
            makeQuestion(Question.ForgetTheColorsLargeDisplay, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myLargeDisplays[chosenStage].ToString() }),
            makeQuestion(Question.ForgetTheColorsSineNumber, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { (Mathf.Abs(mySineNumbers[chosenStage]) % 10).ToString() }),
            makeQuestion(Question.ForgetTheColorsGearColor, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myGearColors[chosenStage].ToString() }),
            makeQuestion(Question.ForgetTheColorsRuleColor, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { stage }, correctAnswers: new[] { myRuleColors[chosenStage].ToString() }));
    }

    private readonly List<List<int>> _ftColors = new();
    private readonly List<List<int>> _ftDigits = new();
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
                    formatCandidates.Add(string.Format(translateString(Question.ForgetThisColors, "the Forget This whose LED was {0} in the {1} stage"), translateAnswer(Question.ForgetThisColors, allColors[myColors[stage]]), Ordinal(stage + 1)));
                if (_ftDigits.Count(d => d[stage] == myDigits[stage]) == 1)
                    formatCandidates.Add(string.Format(translateString(Question.ForgetThisColors, "the Forget This which displayed {0} in the {1} stage"), base36[myDigits[stage]], Ordinal(stage + 1)));
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
        formattedName ??= _translation?.Translate(Question.ForgetThisColors).ModuleName ?? "Forget This";
        addQuestions(module,
            makeQuestion(Question.ForgetThisColors, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { Ordinal(chosenStage + 1) }, correctAnswers: new[] { allColors[myColors[chosenStage]] }),
            makeQuestion(Question.ForgetThisDigits, moduleId, 0, formattedModuleName: formattedName, formatArgs: new[] { Ordinal(chosenStage + 1) }, correctAnswers: new[] { base36[myDigits[chosenStage]].ToString() }));
    }

    private readonly List<List<string>> _forgetUsNotStages = new();
    private IEnumerator<YieldInstruction> ProcessForgetUsNot(ModuleData module)
    {
        // The format args for this question aren't ordinals because that would be ambiguous (i.e. does "first stage" refer to the stage displayed first or input first?)

        const string moduleId = "forgetUsNot";
        var comp = GetComponent(module, "AdvancedMemory");
        var foreignIgnored = new HashSet<string>(GetStaticField<string[]>(comp.GetType(), "ignoredModules", true).Get());
        var wrongAnswers = Bomb.GetSolvableModuleNames().Where(x => !foreignIgnored.Contains(x)).ToArray();
        if (wrongAnswers.Length == 0)
        {
            legitimatelyNoQuestion(module, "There were no stages.");
            yield break;
        }

        var solved = new List<string>();
        var order = new List<string>();

        while (!_noUnignoredModulesLeft)
        {
            var newCount = Bomb.GetSolvedModuleNames().Where(x => !foreignIgnored.Contains(x)).Count();
            if (newCount > solved.Count)
            {
                var nowSolved = Bomb.GetSolvedModuleNames().Where(x => !foreignIgnored.Contains(x)).ToList();
                var delta = nowSolved.ToList();
                foreach (var s in solved)
                    delta.Remove(s);
                solved = nowSolved;
                string name = null; // Replicating FUN's own logic with this. This is off if multiple modules solve within a few frames of one another, but it should be good enough
                foreach (var m in delta)
                    name = m;
                order.Add(name);
            }
            yield return null;
        }

        var stageOrder = GetArrayField<int>(comp, "Display").Get();
        order = order.Select((s, i) => (s, i: stageOrder[i])).OrderBy(t => t.i).Select(t => t.s).ToList();

        var allAnswers = Bomb.GetSolvableModuleNames().ToArray();

        if (_moduleCounts[moduleId] == 1)
        {
            addQuestions(module, order.Select((n, i) => makeQuestion(Question.ForgetUsNotStage, moduleId, 1, formatArgs: new[] { (i + 1).ToString() }, correctAnswers: new[] { n }, allAnswers: allAnswers, preferredWrongAnswers: wrongAnswers)));
            yield break;
        }

        _forgetUsNotStages.Add(order);
        yield return null;

        if (_forgetUsNotStages.Any(s => s.Count != order.Count))
            throw new AbandonModuleException("Stage counts were not consistent among modules.");

        var qs = new List<QandA>();

        for (var i = 0; i < order.Count; i++)
        {
            var n = order[i];
            var disambiguators = Enumerable.Range(0, order.Count).Where(x => x != i && _forgetUsNotStages.Count(s => s[x] == n) is 1).ToArray();
            if (disambiguators.Length == 0)
                continue;
            var d = disambiguators.PickRandom();
            var format = string.Format(translateString(Question.ForgetUsNotStage, "the Forget Us Not in which {0} was used for stage {1}"), order[d], d + 1);
            qs.Add(makeQuestion(Question.ForgetUsNotStage, module, formattedModuleName: format, formatArgs: new[] { (i + 1).ToString() }, correctAnswers: new[] { n }, allAnswers: allAnswers, preferredWrongAnswers: wrongAnswers));
        }

        if (qs.Count == 0)
        {
            legitimatelyNoQuestion(module, $"There were not enough stages in which this one(#{GetIntField(comp, "thisLoggingID", true).Get()}) was unique.");
            yield break;
        }

        addQuestions(module, qs);
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
        for (var ix = 0; ix < 4; ix++)
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
            qs.Add(makeSpriteQuestion(q, Question.FuseBoxFlashes, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { FuseBoxColorSprites[flashes[ix]] }));
            qs.Add(makeSpriteQuestion(q2, Question.FuseBoxArrows, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { FuseBoxArrowSprites[arrows[ix]] }));
        }

        addQuestions(module, qs);
    }
}
