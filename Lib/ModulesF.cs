using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                throw new AbandonModuleException("The screen contains something other than three characters: “{0}” ({1} characters).", display, display.Length);
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
            throw new AbandonModuleException("‘exitPos’ contains invalid coordinate: ({0},{1})", exitPos[2], exitPos[1]);

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

    private IEnumerable<object> ProcessFlags(KMBombModule module)
    {
        var comp = GetComponent(module, "FlagsModule");
        var fldCanInteract = GetField<bool>(comp, "canInteract");
        var mainCountry = GetField<object>(comp, "mainCountry").Get();
        var countries = GetField<IList>(comp, "countries").Get();
        var number = GetIntField(comp, "number").Get(1, 7);

        if (countries.Count != 7)
            throw new AbandonModuleException("‘countries’ has length {0} (expected 7).", countries.Count);

        var propCountryName = GetProperty<string>(mainCountry, "CountryName", isPublic: true);
        var mainCountrySprite = FlagsSprites.FirstOrDefault(spr => spr.name == propCountryName.GetFrom(mainCountry));
        var otherCountrySprites = countries.Cast<object>().Select(country => FlagsSprites.FirstOrDefault(spr => spr.name == propCountryName.GetFrom(country))).ToArray();

        if (mainCountrySprite == null || otherCountrySprites.Any(spr => spr == null))
            throw new AbandonModuleException("Abandoning Flags because one of the countries has a name with no corresponding sprite: main country = {0}, other countries = [{1}].", propCountryName.GetFrom(mainCountry), countries.Cast<object>().Select(country => propCountryName.GetFrom(country)).JoinString(", "));

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

    private IEnumerable<object> ProcessForestCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "forestCipher", Question.ForestCipherAnswer, _ForestCipher);
    }

    private IEnumerable<object> ProcessForgetAnyColor(KMBombModule module)
    {
        var comp = GetComponent(module, "FACScript");

        var init = GetField<object>(comp, "init").Get();
        var fldStage = GetIntField(init, "stage");
        var fldCylinders = GetField<Array>(init, "cylinders");
        var calculate = GetField<object>(init, "calculate").Get();
        var fldSequences = GetListField<bool?>(calculate, "sequences");
        var fldFigures = GetListField<int>(calculate, "figureSequences");

        if (_moduleCounts.TryGetValue(_ForgetAnyColor, out var facCount) && facCount > 1)
        {
            Debug.LogFormat("[Souvenir #{0}] No question for Forget Any Color because there is more than one of them.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        while (fldSequences.Get(minLength: 0, maxLength: int.MaxValue, nullContentAllowed: true).Count == 0)
            yield return null;

        var maxStage = GetIntField(init, "maxStage").Get() + 1;
        var randomStage = Rnd.Range(0, Math.Min(5, maxStage - 1));

        Debug.LogFormat("<Souvenir #{0}> Forget Any Color: Waiting for stage {1}.", _moduleId, randomStage + 1);
        while (fldFigures.Get().Count < randomStage + 1)
            yield return null;  // Don’t wait .1 seconds so that we are absolutely sure we get the right stage
        _modulesSolved.IncSafe(_ForgetAnyColor);

        if (maxStage < fldStage.Get())
            throw new AbandonModuleException("‘stage’ had an unexpected value: expected 0-{0}, was {1}.", maxStage, fldStage.Get());

        var cylinders = fldCylinders.Get(v => v.Rank != 2 || v.GetLength(0) != maxStage || v.GetLength(1) != 3 ? string.Format("expected a {0}×3 2D array", maxStage) : null);
        var figures = fldFigures.Get(v => v.Count < randomStage + 1 ? string.Format("expected at least {0} entries", randomStage + 1) : null);

        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White" };
        var figureNames = new[] { "LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR" };
        var correctCylinder = Enumerable.Range(0, 3).Select(ix => colorNames[(int) cylinders.GetValue(randomStage, ix)]).JoinString(", ");
        var preferredCylinders = new HashSet<string> { correctCylinder };
        while (preferredCylinders.Count < 6)
            preferredCylinders.Add(Enumerable.Range(0, 3).Select(i => colorNames.PickRandom()).JoinString(", "));

        addQuestions(module,
            makeQuestion(Question.ForgetAnyColorCylinder, _ForgetAnyColor, formatArgs: new[] { (randomStage + 1).ToString() },
                correctAnswers: new[] { correctCylinder }, preferredWrongAnswers: preferredCylinders.ToArray()),
            makeQuestion(Question.ForgetAnyColorSequence, _ForgetAnyColor, formatArgs: new[] { (randomStage + 1).ToString() },
                correctAnswers: new[] { figureNames[figures[randomStage]] }, preferredWrongAnswers: figureNames));
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

    private IEnumerable<object> ProcessForgetsUltimateShowdown(KMBombModule module)
    {
        var comp = GetComponent(module, "ForgetsUltimateShowdownScript");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var methods = GetField<IList>(comp, "_usedMethods").Get();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_ForgetsUltimateShowdown);
        if (methods.Count != 4)
            throw new AbandonModuleException("‘methods’ had an invalid length: {0}, expected 4", methods.Count);

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

    private IEnumerable<object> ProcessForgetTheColors(KMBombModule module)
    {
        var comp = GetComponent(module, "FTCScript");
        var fldStage = GetIntField(comp, "stage");

        if (_moduleCounts.TryGetValue(_ForgetTheColors, out var ftcCount) && ftcCount > 1)
        {
            Debug.LogFormat("[Souvenir #{0}] No question for Forget The Colors because there is more than one of them.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var maxStage = GetIntField(comp, "maxStage").Get();
        var stage = fldStage.Get();
        var gear = GetListField<byte>(comp, "gear").Get();
        var largeDisplay = GetListField<short>(comp, "largeDisplay").Get();
        var sineNumber = GetListField<int>(comp, "sineNumber").Get();
        var gearColor = GetListField<string>(comp, "gearColor").Get();
        var ruleColor = GetListField<string>(comp, "ruleColor").Get();

        if (maxStage < stage)
            throw new AbandonModuleException("‘stage’ had an unexpected value: expected 0-{0}, was {1}.", maxStage, stage);

        string[] colors = { "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray" };

        var chosenStage = 0;
        Debug.LogFormat("<Souvenir #{0}> Forget The Colors: Waiting for stage {1}.", _moduleId, chosenStage);
        while (fldStage.Get() <= chosenStage)
            yield return null;  // Don’t wait .1 seconds so that we are absolutely sure we get the right stage
        _modulesSolved.IncSafe(_ForgetTheColors);

        if (gear.Count <= chosenStage || largeDisplay.Count <= chosenStage || sineNumber.Count <= chosenStage || gearColor.Count <= chosenStage || ruleColor.Count <= chosenStage)
            throw new AbandonModuleException("One or more of the lists have an unexpected level of entries. (Expected less than or equal {1}): Gear: {2}, LargeDisplay: {3}, SineNumber: {4}, GearColor: {5}, RuleColor: {6}", _moduleId, chosenStage, gear.Count, largeDisplay.Count, sineNumber.Count, gearColor.Count, ruleColor.Count);

        if (!new[] { gear.Count, largeDisplay.Count, sineNumber.Count, gearColor.Count, ruleColor.Count }.All(x => x == gear.Count))
            throw new AbandonModuleException("One or more of the lists aren't all the same length. (Expected {1}): Gear: {1}, LargeDisplay: {2}, SineNumber: {3}, GearColor: {4}, RuleColor: {5}", _moduleId, gear.Count, largeDisplay.Count, sineNumber.Count, gearColor.Count, ruleColor.Count);

        for (int i = 0; i < gear.Count; i++)
        {
            if (gear[i] < 0 || gear[i] > 9)
                throw new AbandonModuleException("‘gear[{0}]’ had an unexpected value. (Expected 0-9): {1}", i, gear[i]);
            if (largeDisplay[i] < 0 || largeDisplay[i] > 990)
                throw new AbandonModuleException("‘largeDisplay[{0}]’ had an unexpected value. (Expected 0-990): {1}", i, largeDisplay[i]);
            if (sineNumber[i] < -99999 || sineNumber[i] > 99999)
                throw new AbandonModuleException("‘sineNumber[{0}]’ had an unexpected value. (Expected (-99999)-99999): {1}", i, sineNumber[i]);
            if (!colors.Contains(gearColor[i]))
                throw new AbandonModuleException("‘gearColor[{0}]’ had an unexpected value. (Expected {1}): {2}", i, colors.JoinString(", "), sineNumber[i]);
            if (!colors.Contains(ruleColor[i]))
                throw new AbandonModuleException("‘ruleColor[{0}]’ had an unexpected value. (Expected {1}): {2}", i, colors.JoinString(", "), ruleColor[i]);
        }

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.ForgetTheColorsGearNumber, _ForgetTheColors, formatArgs: new[] { chosenStage.ToString() }, correctAnswers: new[] { gear[chosenStage].ToString() }, preferredWrongAnswers: new[] { Rnd.Range(0, 10).ToString() }));
        qs.Add(makeQuestion(Question.ForgetTheColorsLargeDisplay, _ForgetTheColors, formatArgs: new[] { chosenStage.ToString() }, correctAnswers: new[] { largeDisplay[chosenStage].ToString() }, preferredWrongAnswers: new[] { Rnd.Range(0, 991).ToString() }));
        qs.Add(makeQuestion(Question.ForgetTheColorsSineNumber, _ForgetTheColors, formatArgs: new[] { chosenStage.ToString() }, correctAnswers: new[] { (Mathf.Abs(sineNumber[chosenStage]) % 10).ToString() }, preferredWrongAnswers: new[] { Rnd.Range(0, 10).ToString() }));
        qs.Add(makeQuestion(Question.ForgetTheColorsGearColor, _ForgetTheColors, formatArgs: new[] { chosenStage.ToString() }, correctAnswers: new[] { gearColor[chosenStage].ToString() }, preferredWrongAnswers: new[] { colors[Rnd.Range(0, colors.Length)] }));
        qs.Add(makeQuestion(Question.ForgetTheColorsRuleColor, _ForgetTheColors, formatArgs: new[] { chosenStage.ToString() }, correctAnswers: new[] { ruleColor[chosenStage].ToString() }, preferredWrongAnswers: new[] { colors[Rnd.Range(0, colors.Length)] }));
        addQuestions(module, qs);
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
            Debug.LogFormat("[Souvenir #{0}] No questions for Functions because it was solved with no queries! This isn’t a bug, just impressive (or cheating).", _moduleId);
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
}