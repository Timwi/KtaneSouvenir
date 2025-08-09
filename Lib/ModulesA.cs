using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessAbyss(ModuleData module)
    {
        var comp = GetComponent(module, "AbyssScript");
        yield return WaitForSolve;
        var seedAbyss = GetField<string>(comp, "SeedVar").Get();
        addQuestions(module, seedAbyss.Select((aChar, idx) => makeQuestion(Question.AbyssSeed, module, formatArgs: new[] { Ordinal(idx + 1) }, correctAnswers: new[] { aChar.ToString() })));
    }

    private IEnumerator<YieldInstruction> ProcessAccumulation(ModuleData module)
    {
        var comp = GetComponent(module, "accumulationScript");

        yield return WaitForSolve;

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
            makeQuestion(Question.AccumulationBorderColor, module, correctAnswers: new[] { colorNames[borderIx] }),
            makeQuestion(Question.AccumulationBackgroundColor, module, formatArgs: new[] { "first" }, correctAnswers: new[] { bgNames[0] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, module, formatArgs: new[] { "second" }, correctAnswers: new[] { bgNames[1] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, module, formatArgs: new[] { "third" }, correctAnswers: new[] { bgNames[2] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { bgNames[3] }, preferredWrongAnswers: bgNames),
            makeQuestion(Question.AccumulationBackgroundColor, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { bgNames[4] }, preferredWrongAnswers: bgNames));
    }

    private IEnumerator<YieldInstruction> ProcessAdventureGame(ModuleData module)
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
            for (var j = invWeaponCount; j < invValues.Count; j++)
                shouldUse &= !mthShouldUseItem.Invoke(j);

            var ret = prevInteract();

            if (invValues.Count != origInvValues.Count)
            {
                // If the length of the inventory has changed, the user used a correct non-weapon item.
                var itemIndex = ++correctItemsUsed;
                qs.Add(() => makeQuestion(Question.AdventureGameCorrectItem, module, formatArgs: new[] { Ordinal(itemIndex) }, correctAnswers: new[] { titleCase(mthItemName.Invoke(itemUsed)) }));
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
        var enemyName = enemy.ToString();
        enemyName = enemyName.Substring(0, 1).ToUpperInvariant() + enemyName.Substring(1).ToLowerInvariant();
        addQuestions(module, qs.Select(q => q()).Concat(new[] { makeQuestion(Question.AdventureGameEnemy, module, correctAnswers: new[] { enemyName }) }));
    }

    private IEnumerator<YieldInstruction> ProcessAffineCycle(ModuleData module) => processSpeakingEvilCycle(module, "AffineCycleScript", Question.AffineCycleDialDirections, Question.AffineCycleDialLabels,
            overrideAnswers: Enumerable.Range(0, 8).Except(new[] { 6 }).Select(x => CycleModuleEightSprites[x]).ToArray());

    private IEnumerator<YieldInstruction> ProcessAlcoholicRampage(ModuleData module)
    {
        var comp = GetComponent(module, "AlcoholicRampageScript");
        var fldStage = GetIntField(comp, "stage");
        var fldChosenMerc = GetIntField(comp, "chosenMerc");
        var mercs = new int[3];

        while (fldStage.Get() != 3)
        {
            mercs[fldStage.Get()] = fldChosenMerc.Get();
            yield return null;
        }

        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (var s = 0; s < 3; s++)
            qs.Add(makeQuestion(Question.AlcoholicRampageMercenaries, module, formatArgs: new[] { Ordinal(s + 1) }, correctAnswers: new[] { AlcoholicRampageSprites[mercs[s]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessALetter(ModuleData module)
    {
        var comp = GetComponent(module, "Letter");
        yield return WaitForSolve;
        var initialLetter = GetField<string>(comp, "LetterList").Get(x => x.Length != 26 ? "expected length 26" : null)[0];

        addQuestion(module, Question.ALetterInitialLetter, correctAnswers: new[] { initialLetter.ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessAlfaBravo(ModuleData module)
    {
        var comp = GetComponent(module, "AlfaBravoModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Alfa-Bravo because the module was force-solved.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        var questions = new List<QandA>();

        var pressedLetter = GetProperty<char>(comp, "souvenirPressedLetter", true).Get();
        if (pressedLetter != 0)
            questions.Add(makeQuestion(Question.AlfaBravoPressedLetter, module, correctAnswers: new[] { pressedLetter.ToString() }));

        var letterToTheLeftOfPressedOne = GetProperty<char>(comp, "souvenirLetterToTheLeftOfPressedOne", true).Get();
        if (letterToTheLeftOfPressedOne != 0)
            questions.Add(makeQuestion(Question.AlfaBravoLeftPressedLetter, module, correctAnswers: new[] { letterToTheLeftOfPressedOne.ToString() }));

        var letterToTheRightOfPressedOne = GetProperty<char>(comp, "souvenirLetterToTheRightOfPressedOne", true).Get();
        if (letterToTheRightOfPressedOne != 0)
            questions.Add(makeQuestion(Question.AlfaBravoRightPressedLetter, module, correctAnswers: new[] { letterToTheRightOfPressedOne.ToString() }));

        questions.Add(makeQuestion(Question.AlfaBravoDigit, module, correctAnswers: new[] { GetProperty<int>(comp, "souvenirDisplayedDigit", true).Get().ToString() }));

        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessAlgebra(ModuleData module)
    {
        var comp = GetComponent(module, "algebraScript");
        yield return WaitForSolve;

        addQuestions(module, Enumerable.Range(0, 2).Select(i => makeQuestion(
            question: i == 0 ? Question.AlgebraEquation1 : Question.AlgebraEquation2,
            data: module,
            correctAnswers: new[] { GetField<Texture>(comp, $"level{i + 1}Equation").Get().name.Replace(';', '/') })));
    }

    private IEnumerator<YieldInstruction> ProcessAlgorithmia(ModuleData module)
    {
        var comp = GetComponent(module, "AlgorithmiaScript");

        var startingPos = GetIntField(comp, "currentPos").Get(min: 0, max: 15);
        var goalPos = GetIntField(comp, "goalPos").Get(min: 0, max: 15);

        yield return WaitForSolve;

        var fldColor = GetField<object>(comp, "mazeAlg");
        var color = fldColor.Get(col => (int) col is < 0 or >= 6 ? "expected in range 0-5" : null).ToString();

        var fldSeed = GetField<object>(comp, "seed");
        var prpSeedValues = GetProperty<int[]>(fldSeed.Get(), "values", true);
        var seedVals = prpSeedValues.Get(arr =>
            arr.Length != 10 ? "expected length 10" :
            arr.Any(val => val is < 0 or > 99) ? "expected in range 0-99" :
            null);

        addQuestions(module,
            makeQuestion(Question.AlgorithmiaPositions, module, formatArgs: new[] { "starting" }, correctAnswers: new[] { new Coord(4, 4, startingPos) }, preferredWrongAnswers: new[] { new Coord(4, 4, goalPos) }),
            makeQuestion(Question.AlgorithmiaPositions, module, formatArgs: new[] { "goal" }, correctAnswers: new[] { new Coord(4, 4, goalPos) }, preferredWrongAnswers: new[] { new Coord(4, 4, startingPos) }),
            makeQuestion(Question.AlgorithmiaColor, module, correctAnswers: new[] { color }),
            makeQuestion(Question.AlgorithmiaSeed, module, correctAnswers: seedVals.Select(x => x.ToString()).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessAlphabeticalRuling(ModuleData module)
    {
        var comp = GetComponent(module, "AlphabeticalRuling");
        var fldStage = GetIntField(comp, "currentStage");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var letterDisplay = GetField<TextMesh>(comp, "LetterDisplay", isPublic: true).Get();
        var numberDisplays = GetArrayField<TextMesh>(comp, "NumberDisplays", isPublic: true).Get(expectedLength: 2);
        var curStage = 0;
        var letters = new char[3];
        var numbers = new int[3];
        while (module.Unsolved)
        {
            var newStage = fldStage.Get();
            if (newStage != curStage)
            {
                if (letterDisplay.text.Length != 1 || letterDisplay.text[0] < 'A' || letterDisplay.text[0] > 'Z')
                    throw new AbandonModuleException($"‘LetterDisplay’ shows {letterDisplay.text} (expected single letter A–Z).");
                letters[newStage - 1] = letterDisplay.text[0];
                if (!int.TryParse(numberDisplays[0].text, out var number) || number < 1 || number > 9)
                    throw new AbandonModuleException($"‘NumberDisplay[0]’ shows {numberDisplays[0].text} (expected integer 1–9).");
                numbers[newStage - 1] = number;
                curStage = newStage;
            }

            yield return null;
        }

        if (letters.Any(l => l is < 'A' or > 'Z') || numbers.Any(n => n is < 1 or > 9))
            throw new AbandonModuleException($"The captured letters/numbers are unexpected (letters: [{letters.JoinString(", ")}], numbers: [{numbers.JoinString(", ")}]).");

        var qs = new List<QandA>();
        for (var ix = 0; ix < letters.Length; ix++)
            qs.Add(makeQuestion(Question.AlphabeticalRulingLetter, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { letters[ix].ToString() }));
        for (var ix = 0; ix < numbers.Length; ix++)
            qs.Add(makeQuestion(Question.AlphabeticalRulingNumber, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { numbers[ix].ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessAlphabetNumbers(ModuleData module)
    {
        var comp = GetComponent(module, "alphabeticalOrderScript");

        var fldStageNumber = GetIntField(comp, "stage");
        var labels = GetArrayField<object>(comp, "buttons", isPublic: true).Get(expectedLength: 6).Select(b => GetField<TextMesh>(b, "text", isPublic: true).Get());
        var stageOptionCounts = new[] { 22, 28, 28, 32 };
        var allOptions = Enumerable.Range(1, 32).Select(pos => pos.ToString());
        var displayedNumberSets = new List<string[]>();

        var fldLevelOrdered = GetField<IList>(comp, "levelOrdered", isPublic: true);
        while (fldLevelOrdered.Get().Count == 0) // Make sure labels have been set for the first time.
            yield return null; // Don’t wait .1 seconds so that we are absolutely sure we get the right stage.

        var qs = new List<QandA>();
        do
        {
            while (fldStageNumber.Get(min: displayedNumberSets.Count - 1, max: displayedNumberSets.Count) < displayedNumberSets.Count)
                yield return null; // Don’t wait .1 seconds so that we are absolutely sure we get the right stage.
            displayedNumberSets.Add(labels.Select(l => l.text).ToArray());
        }
        while (displayedNumberSets.Count < 4);

        yield return WaitForSolve;

        addQuestions(module, displayedNumberSets.Select((numArr, stage) => makeQuestion(Question.AlphabetNumbersDisplayedNumbers, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: displayedNumberSets[stage], preferredWrongAnswers: allOptions.Take(stageOptionCounts[stage]).ToArray())));
    }

    private IEnumerator<YieldInstruction> ProcessAlphabetTiles(ModuleData module)
    {
        var comp = GetComponent(module, "AlphabetTilesScript");

        yield return WaitForSolve;

        var shuffled = GetArrayField<string>(comp, "ShuffledAlphabet").Get(expectedLength: 26);
        var lettersShown = GetArrayField<string>(comp, "LettersShown").Get(expectedLength: 6);

        addQuestions(module,
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "first" }, correctAnswers: new[] { lettersShown[0] }),
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "second" }, correctAnswers: new[] { lettersShown[1] }),
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "third" }, correctAnswers: new[] { lettersShown[2] }),
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { lettersShown[3] }),
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { lettersShown[4] }),
            makeQuestion(Question.AlphabetTilesCycle, module, formatArgs: new[] { "sixth" }, correctAnswers: new[] { lettersShown[5] }),
            makeQuestion(Question.AlphabetTilesMissingLetter, module, correctAnswers: new[] { shuffled[25] }));
    }

    private IEnumerator<YieldInstruction> ProcessAlphaBits(ModuleData module)
    {
        var comp = GetComponent(module, "AlphaBitsScript");

        var textMeshes = new[] { "displayTL", "displayML", "displayBL", "displayTR", "displayMR", "displayBR" }.Select(fieldName => GetField<TextMesh>(comp, fieldName, isPublic: true).Get()).ToArray();
        var font = textMeshes[0].font;
        var fontTexture = textMeshes[0].GetComponent<MeshRenderer>().sharedMaterial.mainTexture;
        var displayedCharacters = textMeshes.Select(textMesh => textMesh.text.Trim()).ToArray();

        yield return displayedCharacters.Any(ch => !(ch.Length == 1 && (ch[0] >= 'A' && ch[0] <= 'V' || ch[0] >= '0' && ch[0] <= '9')))
            ? throw new AbandonModuleException($"The displayed characters are {displayedCharacters.Select(str => $"“{str}”").JoinString(", ")} (expected six single-character strings 0–9/A–V each).")
            : (YieldInstruction) WaitForSolve;
        addQuestions(module, Enumerable.Range(0, 6).Select(displayIx => makeQuestion(
            Question.AlphaBitsDisplayedCharacters, module, font, fontTexture,
            formatArgs: new[] { new[] { "top", "middle", "bottom" }[displayIx % 3], new[] { "left", "right" }[displayIx / 3] },
            correctAnswers: new[] { displayedCharacters[displayIx] },
            preferredWrongAnswers: displayedCharacters)));
    }

    private IEnumerator<YieldInstruction> ProcessAMessage(ModuleData module)
    {
        yield return WaitForSolve;
        var comp = GetComponent(module, "AMessageScriptRedone");
        var data = GetArrayField<int>(comp, "SequenceNumbers").Get(expectedLength: 5, validator: v => v is < 0 or > 31 ? "Out of range [0, 31]" : null);
        var sol = GetArrayField<int>(comp, "RealNumbers").Get(expectedLength: 5, validator: v => v is < 0 or > 31 ? "Out of range [0, 31]" : null);
        static string convert(int[] nums) => new(nums.Select(i => (char) ('\ue900' + i)).ToArray());
        addQuestion(module, Question.AMessageAMessage, correctAnswers: new[] { convert(data) }, preferredWrongAnswers: new[] { convert(sol) });
    }

    private IEnumerator<YieldInstruction> ProcessAmusementParks(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "amusementParksScript");
        var avail = GetField<IEnumerable>(comp, "ridesAvailable").Get(v => v.Cast<object>().Count() != 3 ? "Expected length 3" : null).Cast<object>().ToArray();
        var correct = GetField<object>(comp, "correctInvestment").Get();

        var fldName = GetField<string>(avail[0], "name", true);
        var options = avail.Select(r => fldName.GetFrom(r, v => !Question.AmusementParksRides.GetAnswers().Contains(v) ? $"Unknown ride type {v}" : null));
        var correctName = fldName.GetFrom(correct, v => !Question.AmusementParksRides.GetAnswers().Contains(v) ? $"Unknown ride type {v}" : null);

        addQuestion(module, Question.AmusementParksRides,
            correctAnswers: options.Except(new[] { correctName }).ToArray(),
            allAnswers: Question.AmusementParksRides.GetAnswers().Except(new[] { correctName }).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessAngelHernandez(ModuleData module)
    {
        var comp = GetComponent(module, "AngelHernandezScript");
        var fldActivated = GetField<bool>(comp, "_canPress");
        var fldStage = GetIntField(comp, "_currentStage");
        var fldMainLetter = GetIntField(comp, "_mainLetter");

        while (!fldActivated.Get())
            yield return new WaitForSeconds(0.1f);

        var displayedLetters = new string[2];
        var alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Select(i => i.ToString()).ToArray();

        for (var i = 0; i < 2; i++)
        {
            while (fldStage.Get() == i)
            {
                while (!fldActivated.Get())
                    yield return null;

                displayedLetters[i] = alph[fldMainLetter.Get()];

                while (fldActivated.Get())
                    yield return null;
            }
        }

        yield return WaitForSolve;
        addQuestions(module, displayedLetters.Select((word, stage) => makeQuestion(Question.AngelHernandezMainLetter, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { word }, preferredWrongAnswers: alph)));
    }

    private IEnumerator<YieldInstruction> ProcessArena(ModuleData module)
    {
        var comp = GetComponent(module, "TheArena");
        yield return WaitForSolve;

        var grabNums = GetArrayField<TextMesh>(comp, "GrbNums", isPublic: true).Get(expectedLength: 9).Select(textMesh => textMesh.text).ToArray();
        var enemyNames = GetField<TextMesh>(comp, "DefEnemies", isPublic: true).Get().text.Split('\n').ToArray();
        var maxNum = GetField<TextMesh>(comp, "AtkNum", isPublic: true).Get().text.Replace("[", "").Replace("]", "");

        addQuestions(module,
            makeQuestion(Question.ArenaDamage, module, correctAnswers: new[] { maxNum }),
            makeQuestion(Question.ArenaEnemies, module, correctAnswers: enemyNames),
            makeQuestion(Question.ArenaNumbers, module, correctAnswers: grabNums));
    }

    private IEnumerator<YieldInstruction> ProcessArithmelogic(ModuleData module)
    {
        var comp = GetComponent(module, "Arithmelogic");
        var fldSymbolNum = GetIntField(comp, "submitSymbol");
        var fldSelectableValues = GetArrayField<int[]>(comp, "selectableValues");
        var fldCurrentDisplays = GetArrayField<int>(comp, "currentDisplays");
        yield return WaitForSolve;

        var symbolNum = fldSymbolNum.Get(min: 0, max: 21);
        var selVal = fldSelectableValues.Get(expectedLength: 3, validator: arr => arr.Length != 4 ? $"length {arr.Length}, expected 4" : null);
        var curDisp = fldCurrentDisplays.Get(expectedLength: 3, validator: val => val is < 0 or >= 4 ? $"expected 0–3" : null);

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.ArithmelogicSubmit, module, correctAnswers: new[] { ArithmelogicSprites[symbolNum] }, preferredWrongAnswers: ArithmelogicSprites));
        var screens = new[] { "left", "middle", "right" };
        for (var i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.ArithmelogicNumbers, module, formatArgs: new[] { screens[i] },
                correctAnswers: Enumerable.Range(0, 4).Where(ix => ix != curDisp[i]).Select(ix => selVal[i][ix].ToString()).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessASquare(ModuleData module)
    {
        var comp = GetComponent(module, "ASquareScript");
        yield return WaitForSolve;
        var qs = new List<QandA>();
        var colorNames = new[] { "Orange", "Pink", "Cyan", "Yellow", "Lavender", "Brown", "Tan", "Blue", "Jade", "Indigo", "White" };

        // Index colors
        var indexColors = GetListField<int>(comp, "_indexColors").Get();
        var indexColorNames = Enumerable.Range(0, indexColors.Count).Select(index => colorNames[indexColors[index]]).ToArray();

        qs.Add(makeQuestion(Question.ASquareIndexColors, module, correctAnswers: indexColorNames, preferredWrongAnswers: colorNames));

        // Correct colors
        var correctColors = GetArrayField<int>(comp, "_correctColors").Get(expectedLength: 3);
        var correctColorNames = Enumerable.Range(0, correctColors.Length).Select(correct => colorNames[correctColors[correct]]).ToArray();
        for (var correct = 0; correct < 3; correct++)
            qs.Add(makeQuestion(Question.ASquareCorrectColors, module, formatArgs: new[] { Ordinal(correct + 1) }, correctAnswers: new[] { correctColorNames[correct] }, preferredWrongAnswers: colorNames));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessASCIIMaze(ModuleData module)
    {
        var comp = GetComponent(module, "ASCIIMazeScript");
        var characters = GetArrayField<string>(comp, "detchars").Get(expectedLength: 12).Select(ch => ch == " " ? "(space)" : ch).ToArray();

        yield return WaitForSolve;

        addQuestions(module, Enumerable.Range(0, 12).Select(ix => makeQuestion(Question.ASCIIMazeCharacters, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { characters[ix] }, preferredWrongAnswers: characters)));
    }

    private static readonly Dictionary<string, AudioClip> _audioMorseAudio = new();
    private IEnumerator<YieldInstruction> ProcessAudioMorse(ModuleData module)
    {
        var morse = "   ;A.-;B-...;C-.-.;D-..;E.;F..-.;G--.;H....;I..;J.---;K-.-;L.-..;M--;N-.;O---;P.--.;Q--.-;R.-.;S...;T-;U..-;V...-;W.--;X-..-;Y-.--;Z--..;1.----;2..---;3...--;4....-;5.....;6-....;7--...;8---..;9----.;0-----"
            .Split(';').ToDictionary(s => s[0], s => s.Substring(1) + " ");

        var comp = GetComponent(module, "AudioMorseModuleScript");
        var words = GetArrayField<string>(comp, "words").Get(expectedLength: 87, validator: v => v.Any(c => c is < 'A' or > 'Z') ? "Expected only uppercase letters" : null);

        var word = words[GetIntField(comp, "wordIndex").Get(min: 0, max: words.Length)];
        var a = GetIntField(comp, "num1Index").Get(min: 0, max: 9);
        var b = GetIntField(comp, "num2Index").Get(min: 0, max: 9);
        var c = GetIntField(comp, "num3Index").Get(min: 0, max: 9);

        var key = $"{word} {a}{b}{c}";

        var all = new List<string>() { key };
        while (all.Count < 6)
        {
            var wrongWord = words.PickRandom();
            var num = UnityEngine.Random.Range(0, 1000).ToString("D3");
            var wrongKey = $"{wrongWord} {num}";
            if (!all.Contains(wrongKey))
                all.Add(wrongKey);
        }

        var clips = all.Select(k =>
        {
            if (!_audioMorseAudio.TryGetValue(k, out var clip))
            {
                List<Sounds.AudioPosition> clips = new();
                var head = 0f;
                var m = k.Select(c => morse[c]).JoinString();
                for (var i = 0; i < m.Length; i++)
                {
                    switch (m[i])
                    {
                        case '.':
                            clips.Add((AudioMorseAudio[0], head));
                            head += 0.125f;
                            break;
                        case '-':
                            clips.Add((AudioMorseAudio[1], head));
                            goto case ' ';
                        case ' ':
                            head += 0.25f;
                            break;
                    }
                }
                clip = _audioMorseAudio[k] = Sounds.Combine(k, clips.ToArray());
            }
            return clip;
        }).ToArray();

        yield return WaitForSolve;

        addQuestion(module, Question.AudioMorseSound, correctAnswers: new[] { clips[0] }, allAnswers: clips);
    }

    private IEnumerator<YieldInstruction> ProcessAzureButton(ModuleData module)
    {
        var comp = GetComponent(module, "AzureButtonScript");
        yield return WaitForSolve;

        var qs = new List<QandA>();

        var cards = GetListField<int>(comp, "_cards").Get(expectedLength: 7);
        var cardT = cards.Last();
        qs.Add(makeQuestion(Question.AzureButtonT, module, correctAnswers: new[] { AzureButtonSprites[cardT] }, preferredWrongAnswers: cards.Select(c => AzureButtonSprites[c]).ToArray()));
        qs.Add(makeQuestion(Question.AzureButtonNotT, module, correctAnswers: cards.Take(6).Select(c => AzureButtonSprites[c]).ToArray(), preferredWrongAnswers: cards.Select(c => AzureButtonSprites[c]).ToArray()));

        var m = Math.Abs(GetField<int>(comp, "_offset").Get(v => Math.Abs(v) is >= 1 and <= 9 ? null : "value out of range 1–9 (or -1–-9)"));
        qs.Add(makeQuestion(Question.AzureButtonM, module, correctAnswers: new[] { m.ToString() }));

        var puzzle = GetField<object>(comp, "_puzzle").Get();
        var arrows = GetField<Array>(puzzle, "Arrows", isPublic: true).Get(a => a.Length != 5 ? "expected length 5" : null);
        var fldArrowDirections = GetProperty<int[]>(arrows.GetValue(0), "Directions", isPublic: true);
        var dirNames = Question.AzureButtonDecoyArrowDirection.GetAnswers();

        for (var arrowIx = 0; arrowIx < 5; arrowIx++)
        {
            var dirs = fldArrowDirections.GetFrom(arrows.GetValue(arrowIx), validator: ar => ar.Length != 3 ? "expected length 3" : null);
            for (var dirIx = 0; dirIx < 3; dirIx++)
            {
                qs.Add(makeQuestion(
                    arrowIx == 0 ? Question.AzureButtonDecoyArrowDirection : Question.AzureButtonNonDecoyArrowDirection,
                    module,
                    formatArgs: arrowIx == 0 ? new[] { Ordinal(dirIx + 1) } : new[] { Ordinal(dirIx + 1), Ordinal(arrowIx) },
                    correctAnswers: new[] { dirNames[dirs[dirIx]] }));
            }
        }

        addQuestions(module, qs);
    }
}
