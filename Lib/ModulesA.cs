using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessAccumulation(KMBombModule module)
    {
        var comp = GetComponent(module, "accumulationScript");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Accumulation);

        var colorNames = new Dictionary<int, string> {
            { 9, "Blue" },
            { 23, "Brown" },
            { 4, "Green" },
            { 15, "Grey" },
            { 26, "Lime" },
            { 2, "Orange" },
            { 8, "Pink" },
            { 17, "Red" },
            { 11, "White" },
            { 10, "Yellow" }
        };

        var borderIx = GetIntField(comp, "borderValue").Get(v => !colorNames.ContainsKey(v) ? "value is not in the dictionary" : null);
        var bgNames = GetArrayField<Material>(comp, "chosenBackgroundColours", isPublic: true)
            .Get(expectedLength: 5)
            .Select(x => char.ToUpperInvariant(x.name[0]) + x.name.Substring(1))
            .ToArray();

        addQuestions(module,
            makeQuestion(Question.AccumulationBorderColor, _Accumulation, correctAnswers: new[] { colorNames[borderIx] }),
            makeQuestion(Question.AccumulationBackgroundColor, _Accumulation, formatArgs: new[] { "first" }, correctAnswers: new[] { bgNames[0] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, _Accumulation, formatArgs: new[] { "second" }, correctAnswers: new[] { bgNames[1] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, _Accumulation, formatArgs: new[] { "third" }, correctAnswers: new[] { bgNames[2] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, _Accumulation, formatArgs: new[] { "fourth" }, correctAnswers: new[] { bgNames[3] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, _Accumulation, formatArgs: new[] { "fifth" }, correctAnswers: new[] { bgNames[4] }, preferredWrongAnswers: bgNames));
    }

    private IEnumerable<object> ProcessAdventureGame(KMBombModule module)
    {
        var comp = GetComponent(module, "AdventureGameModule");
        var fldInvWeaponCount = GetIntField(comp, "InvWeaponCount");
        var fldSelectedItem = GetIntField(comp, "SelectedItem");
        var mthItemName = GetMethod<string>(comp, "ItemName", 1);
        var mthShouldUseItem = GetMethod<bool>(comp, "ShouldUseItem", 1);

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var invValues = GetField<IList>(comp, "InvValues").Get();   // actually List<AdventureGameModule.ITEM>
        var buttonUse = GetField<KMSelectable>(comp, "ButtonUse", isPublic: true).Get(b => b.OnInteract == null ? "ButtonUse.OnInteract is null" : null);
        var enemy = GetField<object>(comp, "SelectedEnemy").Get();
        var textEnemy = GetField<TextMesh>(comp, "TextEnemy", isPublic: true).Get();
        var invWeaponCount = fldInvWeaponCount.Get(v => v == 0 ? "zero" : null);

        var prevInteract = buttonUse.OnInteract;
        var origInvValues = new List<int>(invValues.Cast<int>());
        var correctItemsUsed = 0;
        var qs = new List<Func<QandA>>();
        var solved = false;

        buttonUse.OnInteract = delegate
        {
            var selectedItem = fldSelectedItem.Get();
            var itemUsed = origInvValues[selectedItem];
            var shouldUse = mthShouldUseItem.Invoke(selectedItem);
            for (int j = invWeaponCount; j < invValues.Count; j++)
                shouldUse &= !mthShouldUseItem.Invoke(j);

            var ret = prevInteract();

            if (invValues.Count != origInvValues.Count)
            {
                // If the length of the inventory has changed, the user used a correct non-weapon item.
                var itemIndex = ++correctItemsUsed;
                qs.Add(() => makeQuestion(Question.AdventureGameCorrectItem, _AdventureGame, formatArgs: new[] { ordinal(itemIndex) }, correctAnswers: new[] { titleCase(mthItemName.Invoke(itemUsed)) }));
                origInvValues.Clear();
                origInvValues.AddRange(invValues.Cast<int>());
            }
            else if (shouldUse)
            {
                // The user solved the module.
                solved = true;
                textEnemy.text = "Victory!";
            }

            return ret;
        };

        while (!solved)
            yield return new WaitForSeconds(.1f);

        buttonUse.OnInteract = prevInteract;
        _modulesSolved.IncSafe(_AdventureGame);
        var enemyName = enemy.ToString();
        enemyName = enemyName.Substring(0, 1).ToUpperInvariant() + enemyName.Substring(1).ToLowerInvariant();
        addQuestions(module, qs.Select(q => q()).Concat(new[] { makeQuestion(Question.AdventureGameEnemy, _AdventureGame, correctAnswers: new[] { enemyName }) }));
    }

    private IEnumerable<object> ProcessAffineCycle(KMBombModule module)
    {
        return processSpeakingEvilCycle1(module, "AffineCycleScript", Question.AffineCycleWord, _AffineCycle);
    }

    private IEnumerable<object> ProcessAlfaBravo(KMBombModule module)
    {
        var comp = GetComponent(module, "AlfaBravoModule");
        var fldSolved = GetProperty<bool>(comp, "solved", true);
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_AlfaBravo);

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            Debug.LogFormat("[Souvenir #{0}] No question for Alfa-Bravo because the module was force-solved.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var questions = new List<QandA>();

        var pressedLetter = GetProperty<char>(comp, "souvenirPressedLetter", true).Get();
        if (pressedLetter != 0)
            questions.Add(makeQuestion(Question.AlfaBravoPressedLetter, _AlfaBravo, correctAnswers: new[] { pressedLetter.ToString() }));

        var letterToTheLeftOfPressedOne = GetProperty<char>(comp, "souvenirLetterToTheLeftOfPressedOne", true).Get();
        if (letterToTheLeftOfPressedOne != 0)
            questions.Add(makeQuestion(Question.AlfaBravoLeftPressedLetter, _AlfaBravo, correctAnswers: new[] { letterToTheLeftOfPressedOne.ToString() }));

        var letterToTheRightOfPressedOne = GetProperty<char>(comp, "souvenirLetterToTheRightOfPressedOne", true).Get();
        if (letterToTheRightOfPressedOne != 0)
            questions.Add(makeQuestion(Question.AlfaBravoRightPressedLetter, _AlfaBravo, correctAnswers: new[] { letterToTheRightOfPressedOne.ToString() }));

        questions.Add(makeQuestion(Question.AlfaBravoDigit, _AlfaBravo, correctAnswers: new[] { GetProperty<int>(comp, "souvenirDisplayedDigit", true).Get().ToString() }));

        addQuestions(module, questions);
    }

    private IEnumerable<object> ProcessAlgebra(KMBombModule module)
    {
        var comp = GetComponent(module, "algebraScript");
        var fldStage = GetIntField(comp, "stage");

        while (fldStage.Get() <= 3)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Algebra);

        addQuestions(module, Enumerable.Range(0, 2).Select(i => makeQuestion(
            question: i == 0 ? Question.AlgebraEquation1 : Question.AlgebraEquation2,
            moduleKey: _Algebra,
            correctAnswers: new[] { GetField<Texture>(comp, string.Format("level{0}Equation", i + 1)).Get().name.Replace(';', '/') })));
    }

    private IEnumerable<object> ProcessAlgorithmia(KMBombModule module)
    {
        var comp = GetComponent(module, "AlgorithmiaScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        int startingPos = GetIntField(comp, "currentPos").Get(min: 0, max: 15);
        int goalPos = GetIntField(comp, "goalPos").Get(min: 0, max: 15);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Algorithmia);

        var fldColor = GetField<object>(comp, "mazeAlg");
        string color = fldColor.Get(col => (int) col < 0 || (int) col >= 6 ? "expected in range 0-5" : null).ToString();

        var fldSeed = GetField<object>(comp, "seed");
        var prpSeedValues = GetProperty<int[]>(fldSeed.Get(), "values", true);
        int[] seedVals = prpSeedValues.Get(arr =>
            arr.Length != 10 ? "expected length 10" :
            arr.Any(val => val < 0 || val > 99) ? "expected in range 0-99" :
            null);

        addQuestions(module,
            makeQuestion(Question.AlgorithmiaPositions, _Algorithmia, formatArgs: new[] { "starting" }, correctAnswers: new[] { new Coord(4, 4, startingPos) }, preferredWrongAnswers: new[] { new Coord(4, 4, goalPos) }),
            makeQuestion(Question.AlgorithmiaPositions, _Algorithmia, formatArgs: new[] { "goal" }, correctAnswers: new[] { new Coord(4, 4, goalPos) }, preferredWrongAnswers: new[] { new Coord(4, 4, startingPos) }),
            makeQuestion(Question.AlgorithmiaColor, _Algorithmia, correctAnswers: new[] { color }),
            makeQuestion(Question.AlgorithmiaSeed, _Algorithmia, correctAnswers: seedVals.Select(x => x.ToString()).ToArray()));
    }

    private IEnumerable<object> ProcessAlphabeticalRuling(KMBombModule module)
    {
        var comp = GetComponent(module, "AlphabeticalRuling");
        var fldSolved = GetField<bool>(comp, "solved");
        var fldStage = GetIntField(comp, "currentStage");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var letterDisplay = GetField<TextMesh>(comp, "LetterDisplay", isPublic: true).Get();
        var numberDisplays = GetArrayField<TextMesh>(comp, "NumberDisplays", isPublic: true).Get(expectedLength: 2);
        var curStage = 0;
        var letters = new char[3];
        var numbers = new int[3];
        while (!fldSolved.Get())
        {
            var newStage = fldStage.Get();
            if (newStage != curStage)
            {
                if (letterDisplay.text.Length != 1 || letterDisplay.text[0] < 'A' || letterDisplay.text[0] > 'Z')
                    throw new AbandonModuleException("‘LetterDisplay’ shows {0} (expected single letter A–Z).", letterDisplay.text);
                letters[newStage - 1] = letterDisplay.text[0];
                int number;
                if (!int.TryParse(numberDisplays[0].text, out number) || number < 1 || number > 9)
                    throw new AbandonModuleException("‘NumberDisplay[0]’ shows {0} (expected integer 1–9).", numberDisplays[0].text);
                numbers[newStage - 1] = number;
                curStage = newStage;
            }

            yield return null;
        }
        _modulesSolved.IncSafe(_AlphabeticalRuling);

        if (letters.Any(l => l < 'A' || l > 'Z') || numbers.Any(n => n < 1 || n > 9))
            throw new AbandonModuleException("The captured letters/numbers are unexpected (letters: [{0}], numbers: [{1}]).", letters.JoinString(", "), numbers.JoinString(", "));

        var qs = new List<QandA>();
        for (var ix = 0; ix < letters.Length; ix++)
            qs.Add(makeQuestion(Question.AlphabeticalRulingLetter, _AlphabeticalRuling, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { letters[ix].ToString() }));
        for (var ix = 0; ix < numbers.Length; ix++)
            qs.Add(makeQuestion(Question.AlphabeticalRulingNumber, _AlphabeticalRuling, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { numbers[ix].ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessAlphabetTiles(KMBombModule module)
    {
        var comp = GetComponent(module, "AlphabetTilesScript");

        var isSolved = false;
        module.OnPass += delegate { isSolved = true; return false; };

        while (!isSolved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_AlphabetTiles);

        var shuffled = GetArrayField<string>(comp, "ShuffledAlphabet").Get(expectedLength: 26);
        var lettersShown = GetArrayField<string>(comp, "LettersShown").Get(expectedLength: 6);

        addQuestions(module,
            makeQuestion(Question.AlphabetTilesCycle, _AlphabetTiles, formatArgs: new[] { "first" }, correctAnswers: new[] { lettersShown[0] }),
            makeQuestion(Question.AlphabetTilesCycle, _AlphabetTiles, formatArgs: new[] { "second" }, correctAnswers: new[] { lettersShown[1] }),
            makeQuestion(Question.AlphabetTilesCycle, _AlphabetTiles, formatArgs: new[] { "third" }, correctAnswers: new[] { lettersShown[2] }),
            makeQuestion(Question.AlphabetTilesCycle, _AlphabetTiles, formatArgs: new[] { "fourth" }, correctAnswers: new[] { lettersShown[3] }),
            makeQuestion(Question.AlphabetTilesCycle, _AlphabetTiles, formatArgs: new[] { "fifth" }, correctAnswers: new[] { lettersShown[4] }),
            makeQuestion(Question.AlphabetTilesCycle, _AlphabetTiles, formatArgs: new[] { "sixth" }, correctAnswers: new[] { lettersShown[5] }),
            makeQuestion(Question.AlphabetTilesMissingLetter, _AlphabetTiles, correctAnswers: new[] { shuffled[25] }));
    }

    private IEnumerable<object> ProcessAlphaBits(KMBombModule module)
    {
        var comp = GetComponent(module, "AlphaBitsScript");

        var isSolved = false;
        module.OnPass += delegate { isSolved = true; return false; };

        var displayedCharacters = new[] { "displayTL", "displayML", "displayBL", "displayTR", "displayMR", "displayBR" }.Select(fieldName => GetField<TextMesh>(comp, fieldName, isPublic: true).Get().text.Trim()).ToArray();
        if (displayedCharacters.Any(ch => ch.Length != 1 || ((ch[0] < 'A' || ch[0] > 'V') && (ch[0] < '0' || ch[0] > '9'))))
            throw new AbandonModuleException("The displayed characters are {0} (expected six single-character strings 0–9/A–V each).", displayedCharacters.Select(str => string.Format(@"""{0}""", str)).JoinString(", "));

        while (!isSolved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_AlphaBits);

        // If the correct answer is '0' or 'O', don't include these as wrong answers.
        addQuestions(module, Enumerable.Range(0, 6).Select(displayIx => makeQuestion(
            Question.AlphaBitsDisplayedCharacters,
            _AlphaBits,
            formatArgs: new[] { new[] { "top", "middle", "bottom" }[displayIx % 3], new[] { "left", "right" }[displayIx / 3] },
            correctAnswers: new[] { displayedCharacters[displayIx] },
            preferredWrongAnswers: new AnswerGenerator.Strings(displayedCharacters[displayIx] == "0" || displayedCharacters[displayIx] == "O" ? "1-9A-NP-V" : "0-9A-V")
                .GetAnswers(this).Distinct().Take(6).ToArray())));
    }

    private IEnumerable<object> ProcessArithmelogic(KMBombModule module)
    {
        var comp = GetComponent(module, "Arithmelogic");
        var fldSymbolNum = GetIntField(comp, "submitSymbol");
        var fldSelectableValues = GetArrayField<int[]>(comp, "selectableValues");
        var fldCurrentDisplays = GetArrayField<int>(comp, "currentDisplays");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Arithmelogic);

        var symbolNum = fldSymbolNum.Get(min: 0, max: 21);
        var selVal = fldSelectableValues.Get(expectedLength: 3, validator: arr => arr.Length != 4 ? string.Format("length {0}, expected 4", arr.Length) : null);
        var curDisp = fldCurrentDisplays.Get(expectedLength: 3, validator: val => val < 0 || val >= 4 ? string.Format("expected 0–3") : null);

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.ArithmelogicSubmit, _Arithmelogic, correctAnswers: new[] { ArithmelogicSprites[symbolNum] }, preferredWrongAnswers: ArithmelogicSprites));
        var screens = new[] { "left", "middle", "right" };
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.ArithmelogicNumbers, _Arithmelogic, formatArgs: new[] { screens[i] },
                correctAnswers: Enumerable.Range(0, 4).Where(ix => ix != curDisp[i]).Select(ix => selVal[i][ix].ToString()).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessASquare(KMBombModule module)
    {
        var comp = GetComponent(module, "ASquareScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ASquare);
        var qs = new List<QandA>();
        var colorNames = new[] { "Orange", "Pink", "Cyan", "Yellow", "Lavender", "Brown", "Tan", "Blue", "Jade", "Indigo", "White" };

        // Index colors
        var indexColors = GetListField<int>(comp, "_indexColors").Get();
        var indexColorNames = Enumerable.Range(0, indexColors.Count).Select(index => colorNames[indexColors[index]]).ToArray();

        qs.Add(makeQuestion(Question.ASquareIndexColors, _ASquare, correctAnswers: indexColorNames, preferredWrongAnswers: colorNames));

        // Correct colors
        var correctColors = GetArrayField<int>(comp, "_correctColors").Get(expectedLength: 3);
        var correctColorNames = Enumerable.Range(0, correctColors.Length).Select(correct => colorNames[correctColors[correct]]).ToArray();
        Debug.LogFormat("<> {0} {1} {2}", correctColorNames[0], correctColorNames[1], correctColorNames[2]);
        for (int correct = 0; correct < 3; correct++)
            qs.Add(makeQuestion(Question.ASquareCorrectColors, _ASquare, formatArgs: new[] { ordinal(correct + 1) }, correctAnswers: new[] { correctColorNames[correct] }, preferredWrongAnswers: colorNames));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessASCIIMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "ASCIIMazeScript");
        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        var characters = GetArrayField<string>(comp, "detchars").Get(expectedLength: 12).Select(ch => ch == " " ? "(space)" : ch).ToArray();

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ASCIIMaze);

        addQuestions(module, Enumerable.Range(0, 12).Select(ix => makeQuestion(Question.ASCIIMazeCharacters, _ASCIIMaze, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { characters[ix] }, preferredWrongAnswers: characters)));
    }
}