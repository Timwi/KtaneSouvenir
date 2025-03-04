using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using Souvenir.Reflection;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessCactisConundrum(ModuleData module)
    {
        int[] colors = new int[3];
        var comp = GetComponent(module, "conundramScript");
        var fldStage = GetIntField(comp, "Stage");
        var fldColor = GetIntField(comp, "ColLedState");

        while (!module.IsSolved)
        {
            var stage = fldStage.Get(min: 0, max: 3);
            if (stage == 3) yield break;
            colors[stage] = fldColor.Get(min: 2, max: 5);
            yield return null;
        }

        addQuestions(module, colors.Select((c, i) =>
            makeQuestion(Question.CactisConundrumColor, module,
                formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { Question.CactisConundrumColor.GetAnswers()[c - 2] })));
    }

    private IEnumerator<YieldInstruction> ProcessCaesarCycle(ModuleData module)
    {
        return processSpeakingEvilCycle1(module, "CaesarCycleScript", Question.CaesarCycleWord);
    }

    private IEnumerator<YieldInstruction> ProcessCaesarPsycho(ModuleData module)
    {
        var comp = GetComponent(module, "CaesarPsychoScript");
        var stage = GetIntField(comp, "stage");
        var cols = GetArrayField<Color>(comp, "cols").Get();
        var dletters = GetArrayField<TextMesh>(comp, "dletters", isPublic: true);

        string[] texts = new string[2];
        int c = -1;
        var colorNames = new string[] { "white", "red", "magenta", "yellow", "green", "cyan", "violet" };
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);
        while (stage.Get() == 0)
        {
            texts[0] = dletters.Get().Take(5).Select(i => i.text).JoinString();
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log($"<Souvenir #{_moduleId}> CPSY " + texts[0]);
        var tmc = new Color(0, 0, 0);
        while (stage.Get() == 1)
        {
            texts[1] = dletters.Get().Take(5).Select(i => i.text).JoinString();
            var dColor = dletters.Get()[0].color;
            var r = dColor.r;
            r = (int) (r * 10) / 10f;
            tmc = new Color(r, dColor.g, dColor.b);
            c = Array.IndexOf(cols, tmc);
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log($"<Souvenir #{_moduleId}> CPSY " + texts[1]);
        Debug.Log($"<Souvenir #{_moduleId}> CPSY COLORS: " + cols.JoinString());
        Debug.Log($"<Souvenir #{_moduleId}> CPSY TMC: " + tmc);
        Debug.Log($"<Souvenir #{_moduleId}> CPSY " + c);

        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (int st = 0; st < 2; st++)
            qs.Add(makeQuestion(Question.CaesarPsychoScreenTexts, module, formatArgs: new[] { Ordinal(st + 1) }, correctAnswers: new[] { texts[st] }));
        qs.Add(makeQuestion(Question.CaesarPsychoScreenColor, module, correctAnswers: new[] { colorNames[c] }, preferredWrongAnswers: colorNames));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessCalendar(ModuleData module)
    {
        var comp = GetComponent(module, "calendar");
        var fldLightsOn = GetField<bool>(comp, "_lightsOn");

        while (!fldLightsOn.Get())
            yield return new WaitForSeconds(.1f);

        var colorblindText = GetField<TextMesh>(comp, "colorblindText", isPublic: true).Get(v => v.text == null ? "text is null" : null);

        yield return WaitForSolve;

        addQuestion(module, Question.CalendarLedColor, correctAnswers: new[] { colorblindText.text });
    }

    private IEnumerator<YieldInstruction> ProcessCARPS(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "carpsScript");
        var grid = GetArrayField<int[,]>(comp, "grid").Get(expectedLength: 3)[0];
        if ((grid.GetLength(0), grid.GetLength(1)) is not (8, 6))
            throw new AbandonModuleException($"Expected 8×6 array, got {grid.GetLength(0)}×{grid.GetLength(1)}");

        var niceGrid = Enumerable.Range(0, 8).SelectMany(y => Enumerable.Range(0, 6).Select(x => grid[y, x])).ToArray();
        if (niceGrid.Any(v => v is < 0 or > 3))
            throw new AbandonModuleException($"Expected all values in range 0–3. Got: {niceGrid.JoinString(" ")}");

        var colors = new[] { "Black", "Red", "Green", "Blue" };
        addQuestions(module, niceGrid.Select((c, i) => makeQuestion(Question.CARPSCell, module, questionSprite: Sprites.GenerateGridSprite(6, 8, i), correctAnswers: new[] { colors[c] })));
    }

    private IEnumerator<YieldInstruction> ProcessCartinese(ModuleData module)
    {
        var comp = GetComponent(module, "cartinese");
        yield return WaitForSolve;

        var buttonColors = GetArrayField<int>(comp, "buttonColors").Get(expectedLength: 4);
        var buttonLyrics = GetArrayField<string>(comp, "buttonLyrics").Get(expectedLength: 4);

        var buttonNames = new[] { "up", "right", "down", "left" };

        addQuestions(module,
            Enumerable.Range(0, 4).Select(btn => makeQuestion(Question.CartineseButtonColors, module, formatArgs: new[] { buttonNames[btn] }, correctAnswers: new[] { Question.CartineseButtonColors.GetAnswers()[buttonColors[btn]] }))
            .Concat(Enumerable.Range(0, 4).Select(btn => makeQuestion(Question.CartineseLyrics, module, formatArgs: new[] { buttonNames[btn] }, correctAnswers: new[] { buttonLyrics[btn] }))));
    }

    private IEnumerator<YieldInstruction> ProcessCatchphrase(ModuleData module)
    {
        var comp = GetComponent(module, "catchphraseScript");
        yield return WaitForSolve;

        var panelColors = GetListField<string>(comp, "selectedColours").Get(expectedLength: 4);
        var panelNames = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };

        panelColors = panelColors.Select(x => char.ToUpperInvariant(x[0]) + x.Substring(1)).ToList();

        addQuestions(module,
            Enumerable.Range(0, 4).Select(panel => makeQuestion(Question.CatchphraseColour, module, formatArgs: new[] { panelNames[panel] }, correctAnswers: new[] { panelColors[panel] })));
    }

    private IEnumerator<YieldInstruction> ProcessChallengeAndContact(ModuleData module)
    {
        var comp = GetComponent(module, "moduleScript");
        var fldAnswers = GetArrayField<string>(comp, "answers");
        var fldFirstSet = GetArrayField<string>(comp, "possibleFirstAnswers");
        var fldSecondSet = GetArrayField<string>(comp, "possibleSecondAnswers");
        var fldThirdSet = GetArrayField<string>(comp, "possibleFinalAnswers");

        yield return WaitForSolve;

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
            makeQuestion(Question.ChallengeAndContactAnswers, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { answers[0] }, preferredWrongAnswers: allAnswers.Where(x => x[0] == answers[0][0]).ToArray()),
            makeQuestion(Question.ChallengeAndContactAnswers, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { answers[1] }, preferredWrongAnswers: allAnswers.Where(x => x[0] == answers[1][0]).ToArray()),
            makeQuestion(Question.ChallengeAndContactAnswers, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { answers[2] }, preferredWrongAnswers: allAnswers.Where(x => x[0] == answers[2][0]).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessCharacterCodes(ModuleData module)
    {
        var comp = GetComponent(module, "CharacterCodes");
        yield return WaitForSolve;

        var code = GetArrayField<string>(comp, "chosenLetters").Get();
        var allChars = GetStaticField<Dictionary<ushort, string>>(comp.GetType(), "characterList").Get().Values.ToArray();
        addQuestions(module, code.Select((c, i) => makeQuestion(Question.CharacterCodesCharacter, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { c }, preferredWrongAnswers: allChars)));
    }

    private IEnumerator<YieldInstruction> ProcessCharacterShift(ModuleData module)
    {
        var comp = GetComponent(module, "characterShift");

        yield return WaitForSolve;

        var leftAnswer = GetField<TextMesh>(comp, "letterText", isPublic: true).Get().text;
        var rightAnswer = GetField<TextMesh>(comp, "numberText", isPublic: true).Get().text;
        var letters = GetArrayField<string>(comp, "letters").Get(expectedLength: 5).Except(new[] { leftAnswer, "*" }).ToArray();
        var digits = GetArrayField<string>(comp, "numbers").Get(expectedLength: 5).Except(new[] { rightAnswer, "*" }).ToArray();

        addQuestions(module,
            makeQuestion(Question.CharacterShiftLetters, module, correctAnswers: letters),
            makeQuestion(Question.CharacterShiftDigits, module, correctAnswers: digits));
    }

    private IEnumerator<YieldInstruction> ProcessCharacterSlots(ModuleData module)
    {
        var comp = GetComponent(module, "CharacterSlotsScript");
        yield return WaitForSolve;

        var characters = GetField<Array>(comp, "slotStates").Get(arr => arr.Rank != 2 || arr.GetLength(0) != 3 || arr.GetLength(1) != 3 ? "expected size 3×3 array" : null);

        var fldName = GetField<Enum>(characters.GetValue(0, 0), "characterName");
        var stageNumber = GetField<int>(comp, "stageNumber").Get();
        var qs = new List<QandA>();

        for (int row = 0; row < stageNumber; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                string name = fldName.GetFrom(characters.GetValue(row, col), ch => !CharacterSlotsSprites.Any(s => s.name.Replace(" ", "") == ch.ToString()) ? "unexpected character name" : null).ToString();
                qs.Add(makeQuestion(
                    question: Question.CharacterSlotsDisplayedCharacters,
                    data: module,
                    formatArgs: new[] { Ordinal(col + 1), Ordinal(row + 1) },
                    correctAnswers: new[] { CharacterSlotsSprites.First(sprite => sprite.name.Replace(" ", "") == name) },
                    preferredWrongAnswers: CharacterSlotsSprites));
            }
        }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessCheapCheckout(ModuleData module)
    {
        var comp = GetComponent(module, "CheapCheckoutModule");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var paids = new List<decimal> { GetField<decimal>(comp, "Display").Get() };
        var paid = GetField<decimal>(comp, "Paid").Get();
        if (paid != paids[0])
            paids.Add(paid);

        yield return WaitForSolve;

        addQuestions(module, paids.Select((p, i) => makeQuestion(Question.CheapCheckoutPaid, module,
            formatArgs: new[] { paids.Count == 1 ? "the paid amount" : i == 0 ? "the first paid amount" : "the second paid amount" },
            correctAnswers: new[] { "$" + p.ToString("N2") })));
    }

    private IEnumerator<YieldInstruction> ProcessCheepCheckout(ModuleData module)
    {
        var comp = GetComponent(module, "cheepCheckoutScript");
        var fldUnicorn = GetField<bool>(comp, "unicorn");
        yield return WaitForSolve;

        if (fldUnicorn.Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Cheep Checkout because the unicorn happened.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        var shuffledList = GetField<List<int>>(comp, "numberList", isPublic: false).Get();
        var birdsPresent = shuffledList.Take(5).Where(ix => ix < 26).Select(ix => Question.CheepCheckoutBirds.GetAnswers()[ix]).ToArray();

        addQuestions(module,
           makeQuestion(Question.CheepCheckoutBirds, module, formatArgs: new[] { "was" }, correctAnswers: birdsPresent),
           makeQuestion(Question.CheepCheckoutBirds, module, formatArgs: new[] { "was not" }, correctAnswers: Question.CheepCheckoutBirds.GetAnswers().Except(birdsPresent).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessChess(ModuleData module)
    {
        var comp = GetComponent(module, "ChessBehaviour");
        var fldIndexSelected = GetArrayField<int>(comp, "indexSelected"); // this contains both the coordinates and the solution

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var indexSelected = fldIndexSelected.Get(expectedLength: 7, validator: b => b / 10 < 0 || b / 10 >= 6 || b % 10 < 0 || b % 10 >= 6 ? "unexpected value" : null);

        yield return WaitForSolve;

        addQuestions(module, Enumerable.Range(0, 6).Select(i => makeQuestion(Question.ChessCoordinate, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { "" + ((char) (indexSelected[i] / 10 + 'a')) + (indexSelected[i] % 10 + 1) })));
    }

    private IEnumerator<YieldInstruction> ProcessChineseCounting(ModuleData module)
    {
        var comp = GetComponent(module, "chineseCounting");
        yield return WaitForSolve;

        var ledIndices = GetArrayField<int>(comp, "ledIndices").Get(expectedLength: 2, validator: ix => ix < 0 || ix > 3 ? "expected range 0–3" : null);
        var ledColors = new[] { "White", "Red", "Green", "Orange" };

        addQuestions(module,
          makeQuestion(Question.ChineseCountingLED, module, formatArgs: new[] { "left" }, correctAnswers: new[] { ledColors[ledIndices[0]] }),
          makeQuestion(Question.ChineseCountingLED, module, formatArgs: new[] { "right" }, correctAnswers: new[] { ledColors[ledIndices[1]] }));
    }

    private IEnumerator<YieldInstruction> ProcessChineseRemainderTheorem(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "ChineseRemainderTheoremScript");
        var moduli = GetListField<int>(comp, "_moduli").Get(minLength: 4, maxLength: 8, validator: v => v is < 2 or > 51 ? "Out of range 2–51" : null);
        var remainders = GetListField<int>(comp, "_remainder").Get(expectedLength: moduli.Count, validator: v => v is < 0 or > 50 ? "Out of range 0–50" : null);
        if (moduli.Select((m, i) => remainders[i] >= m).Any(x => x))
            throw new AbandonModuleException($"A remainder was bigger than its corresponding modulus: {moduli.Select((m, i) => $"N % {m} = {remainders[i]}").JoinString("; ")}");

        var right = moduli
            .Select((m, i) => $"N % {m} = {remainders[i]}")
            .ToArray();

        var wrong = Enumerable
            .Range(0, 10)
            .Select(_ => UnityEngine.Random.Range(2, 51))
            .Select(m => $"N % {m} = {UnityEngine.Random.Range(0, m)}");

        addQuestion(module, Question.ChineseRemainderTheoremEquations,
            correctAnswers: right, allAnswers: right.Concat(wrong).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessChordQualities(ModuleData module)
    {
        var comp = GetComponent(module, "ChordQualities");

        var givenChord = GetField<object>(comp, "givenChord").Get();
        var lights = GetField<Array>(comp, "lights", isPublic: true).Get(v => v.Length != 12 ? "expected length 12" : null);
        var mthSetOutputLight = GetMethod<object>(lights.GetValue(0), "setOutputLight", numParameters: 1, isPublic: true);
        var mthTurnInputLightOff = GetMethod<object>(lights.GetValue(0), "turnInputLightOff", numParameters: 0, isPublic: true);

        yield return WaitForSolve;

        for (int lightIx = 0; lightIx < lights.Length; lightIx++)
        {
            mthSetOutputLight.InvokeOn(lights.GetValue(lightIx), false);
            mthTurnInputLightOff.InvokeOn(lights.GetValue(lightIx));
        }

        var noteNames = GetField<Array>(givenChord, "notes").Get(v => v.Length != 4 ? "expected length 4" : null).Cast<object>().Select(note => note.ToString().Replace("sharp", "♯")).ToArray();
        addQuestions(module, makeQuestion(Question.ChordQualitiesNotes, module, correctAnswers: noteNames));
    }

    private IEnumerator<YieldInstruction> ProcessClockCounter(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "ClockCounter");
        var ans = GetArrayField<int>(comp, "answer").Get(10, false, false, i => i is > 26 or < 1 ? "Out of range 1-26" : null);
        addQuestion(module, Question.ClockCounterArrows, correctAnswers: ans.Select(i => ClockCounterSprites[i - 1]).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessCode(ModuleData module)
    {
        var comp = GetComponent(module, "TheCodeModule");
        var fldCode = GetIntField(comp, "moduleNumber");
        var fldResetBtn = GetField<KMSelectable>(comp, "ButtonR", isPublic: true);
        var fldSubmitBtn = GetField<KMSelectable>(comp, "ButtonS", isPublic: true);

        var code = fldCode.Get(min: 999, max: 9999);

        yield return WaitForSolve;

        // Block the submit/reset buttons
        fldResetBtn.Get().OnInteract = delegate { return false; };
        fldSubmitBtn.Get().OnInteract = delegate { return false; };

        addQuestion(module, Question.CodeDisplayNumber, correctAnswers: new[] { code.ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessCodenames(ModuleData module)
    {
        var comp = GetComponent(module, "codenames");
        yield return WaitForSolve;

        var words = GetArrayField<string>(comp, "grid").Get(expectedLength: 25);
        var solution = GetArrayField<bool>(comp, "solution").Get(expectedLength: 25);
        var solutionWords = words.Where((w, i) => solution[i]).ToArray();
        addQuestion(module, Question.CodenamesAnswers, correctAnswers: solutionWords, preferredWrongAnswers: words.Where(x => !solutionWords.Contains(x)).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessCoffeeBeans(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "coffeeBeansScript");
        var moves = GetListField<int>(comp, "moves").Get(minLength: 3, maxLength: 5, validator: v => v is < 0 or > 2 ? "Out of range [0, 2]" : null);
        var names = Question.CoffeeBeansMovements.GetAnswers();

        addQuestions(module, moves.Select((m, i) =>
            makeQuestion(Question.CoffeeBeansMovements, module,
                correctAnswers: new[] { names[m] },
                formatArgs: new[] { Ordinal(i + 1) })));
    }

    private IEnumerator<YieldInstruction> ProcessCoffeebucks(ModuleData module)
    {
        var comp = GetComponent(module, "coffeebucksScript");

        yield return WaitForSolve;

        var coffees = GetArrayField<string>(comp, "coffeeOptions", isPublic: true).Get();
        var currCoffee = GetIntField(comp, "startCoffee").Get(min: 0, max: coffees.Length - 1);

        for (int i = 0; i < coffees.Length; i++)
            coffees[i] = coffees[i].Replace("\n", " ");

        addQuestion(module, Question.CoffeebucksCoffee, correctAnswers: new[] { coffees[currCoffee] }, preferredWrongAnswers: coffees);
    }

    private IEnumerator<YieldInstruction> ProcessCoinage(ModuleData module)
    {
        var comp = GetComponent(module, "CoinageScript");
        yield return WaitForSolve;

        addQuestion(module, Question.CoinageFlip,
            correctAnswers: new[] { GetField<string>(comp, "souvenirCoin").Get() },
            preferredWrongAnswers: Enumerable.Range(0, 64).Select(i => "abcdefgh"[i % 8].ToString() + "87654321"[i / 8]).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessColorAddition(ModuleData module)
    {
        var script = GetComponent(module, "ColorAddition");
        var numbersField = GetArrayField<string>(script, "numbers");
        yield return WaitForSolve;

        var numbersObtained = numbersField.Get(expectedLength: 3);
        var channelRefs = new[] { "red", "green", "blue" };
        addQuestions(module, channelRefs.Select((chn, idx) => makeQuestion(Question.ColorAdditionNumbers, module, formatArgs: new[] { chn }, correctAnswers: new[] { numbersObtained[idx] })));
    }

    private IEnumerator<YieldInstruction> ProcessColorBraille(ModuleData module)
    {
        var comp = GetComponent(module, "ColorBrailleModule");
        yield return WaitForSolve;

        var colorIxs = GetArrayField<int>(comp, "_colorIxs").Get(expectedLength: 5 * 6);
        var colorNames = Question.ColorBrailleColor.GetAnswers();

        addQuestions(module, Enumerable.Range(0, 5 * 6).Select(ix =>
            makeQuestion(Question.ColorBrailleColor, module,
                questionSprite: Sprites.GenerateCirclesSprite(5 * 2, 3, 1 << ix, 20, 5, outline: true, vertical: true),
                correctAnswers: new[] { colorNames[colorIxs[ix]] })));
    }

    private IEnumerator<YieldInstruction> ProcessColorDecoding(ModuleData module)
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
                Debug.Log($"<Souvenir #{_moduleId}> Abandoning Color Decoding because: {amex.Message}");
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

        for (int ix = 0; ix < inputButtons.Length; ix++)
            inputButtons[ix].OnInteract = origInteract[ix];

        if (isAbandoned)
            throw new AbandonModuleException("See error logged earlier.");

        if (Enumerable.Range(0, 3).Any(k => !patterns.ContainsKey(k) || !colors.ContainsKey(k)))
            throw new AbandonModuleException($"I have a discontinuous set of stages: {patterns.Keys.JoinString(", ")}/{colors.Keys.JoinString(", ")}.");

        addQuestions(module, Enumerable.Range(0, 3).SelectMany(stage => Ut.NewArray(
             colors[stage].Length <= 2 ? makeQuestion(Question.ColorDecodingIndicatorColors, module, formatArgs: new[] { "appeared", Ordinal(stage + 1) }, correctAnswers: colors[stage]) : null,
             colors[stage].Length >= 3 ? makeQuestion(Question.ColorDecodingIndicatorColors, module, formatArgs: new[] { "did not appear", Ordinal(stage + 1) }, correctAnswers: colorNameMapping.Values.Except(colors[stage]).ToArray()) : null,
             makeQuestion(Question.ColorDecodingIndicatorPattern, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { patterns[stage] }))));
    }

    private IEnumerator<YieldInstruction> ProcessColoredKeys(ModuleData module)
    {
        var comp = GetComponent(module, "ColoredKeysScript");

        yield return WaitForSolve;

        var colors = GetArrayField<string>(comp, "loggingWords", isPublic: true).Get();
        var letters = GetArrayField<string>(comp, "letters", isPublic: true).Get();
        var displayWord = GetIntField(comp, "displayIndex").Get(0, colors.Length - 1);
        var displayColor = GetIntField(comp, "displayColIndex").Get(0, colors.Length - 1);
        var matsNames = GetArrayField<Material>(comp, "buttonmats", isPublic: true).Get().Select(x => x.name).ToArray();

        var btnLetter = Enumerable.Range(1, 4).Select(i => GetIntField(comp, $"b{i}LetIndex").Get(0, letters.Length - 1)).ToArray();
        var btnColor = Enumerable.Range(1, 4).Select(i => GetIntField(comp, $"b{i}ColIndex").Get(0, matsNames.Length - 1)).ToArray();

        addQuestions(module,
            makeQuestion(Question.ColoredKeysDisplayWord, module, correctAnswers: new[] { colors[displayWord] }, preferredWrongAnswers: colors),
            makeQuestion(Question.ColoredKeysDisplayWordColor, module, correctAnswers: new[] { colors[displayColor] }, preferredWrongAnswers: colors),
            makeQuestion(Question.ColoredKeysKeyLetter, module, formatArgs: new[] { "top-left" }, correctAnswers: new[] { letters[btnLetter[0]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyLetter, module, formatArgs: new[] { "top-right" }, correctAnswers: new[] { letters[btnLetter[1]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyLetter, module, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { letters[btnLetter[2]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyLetter, module, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { letters[btnLetter[3]] }, preferredWrongAnswers: letters),
            makeQuestion(Question.ColoredKeysKeyColor, module, formatArgs: new[] { "top-left" }, correctAnswers: new[] { matsNames[btnColor[0]] }, preferredWrongAnswers: matsNames),
            makeQuestion(Question.ColoredKeysKeyColor, module, formatArgs: new[] { "top-right" }, correctAnswers: new[] { matsNames[btnColor[1]] }, preferredWrongAnswers: matsNames),
            makeQuestion(Question.ColoredKeysKeyColor, module, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { matsNames[btnColor[2]] }, preferredWrongAnswers: matsNames),
            makeQuestion(Question.ColoredKeysKeyColor, module, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { matsNames[btnColor[3]] }, preferredWrongAnswers: matsNames));
    }

    private IEnumerator<YieldInstruction> ProcessColoredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "ColoredSquaresModule");
        yield return WaitForSolve;
        addQuestion(module, Question.ColoredSquaresFirstGroup, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor").Get().ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessColoredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "ColoredSwitchesModule");
        var fldSwitches = GetIntField(comp, "_switchState");
        var fldSolution = GetIntField(comp, "_solutionState");

        var initial = fldSwitches.Get(0, (1 << 5) - 1);

        while (fldSolution.Get() == -1)
            yield return null;  // not waiting for .1 seconds this time to make absolutely sure we catch it before the player toggles another switch

        var afterReveal = fldSwitches.Get(0, (1 << 5) - 1);

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.ColoredSwitchesInitialPosition, module, correctAnswers: new[] { Enumerable.Range(0, 5).Select(b => (initial & (1 << b)) != 0 ? "Q" : "R").Reverse().JoinString() }),
            makeQuestion(Question.ColoredSwitchesWhenLEDsCameOn, module, correctAnswers: new[] { Enumerable.Range(0, 5).Select(b => (afterReveal & (1 << b)) != 0 ? "Q" : "R").Reverse().JoinString() }));
    }

    private IEnumerator<YieldInstruction> ProcessColorMorse(ModuleData module)
    {
        var comp = GetComponent(module, "ColorMorseModule");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        yield return WaitForSolve;

        var numbers = GetArrayField<int>(comp, "Numbers").Get(expectedLength: 3);
        var colorNames = GetArrayField<string>(comp, "ColorNames", isPublic: true).Get();
        var colors = GetArrayField<int>(comp, "Colors").Get(expectedLength: 3, validator: c => c < 0 || c >= colorNames.Length ? "out of range" : null);

        var flashedColorNames = colors.Select(c => colorNames[c].Substring(0, 1) + colorNames[c].Substring(1).ToLowerInvariant()).ToArray();
        var flashedCharacters = numbers.Select(num => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(num, 1)).ToArray();

        addQuestions(module, Enumerable.Range(0, 3).SelectMany(ix => Ut.NewArray(
             makeQuestion(Question.ColorMorseColor, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { flashedColorNames[ix] }, preferredWrongAnswers: flashedColorNames),
             makeQuestion(Question.ColorMorseCharacter, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { flashedCharacters[ix] }, preferredWrongAnswers: flashedCharacters))));
    }

    private IEnumerator<YieldInstruction> ProcessColorOneTwo(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "colorOneTwoScript");
        var left = GetIntField(comp, "leftLEDColor").Get(0, 3);
        var right = GetIntField(comp, "rightLEDColor").Get(0, 3);
        var colors = new[] { "Red", "Blue", "Green", "Yellow" };
        addQuestions(module,
            makeQuestion(Question.ColorOneTwoColor, module, formatArgs: new[] { "left" }, correctAnswers: new[] { colors[left] }),
            makeQuestion(Question.ColorOneTwoColor, module, formatArgs: new[] { "right" }, correctAnswers: new[] { colors[right] }));
    }

    private IEnumerator<YieldInstruction> ProcessColorsMaximization(ModuleData module)
    {
        var comp = GetComponent(module, "ColorsMaximizationModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Colors Maximization because the module was force-solved.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        var colorNameDic = GetStaticField<Dictionary<Color, string>>(comp.GetType(), "colorNames", true).Get();
        var colorNames = colorNameDic.Values.ToArray();
        var allColors = GetStaticField<Color[]>(comp.GetType(), "allColors").Get();

        var questions = new List<QandA>();
        foreach (var color in allColors)
            questions.Add(makeQuestion(Question.ColorsMaximizationColorCount, module,
                formatArgs: new[] { colorNameDic[color] },
                correctAnswers: new[] { GetField<Dictionary<Color, int>>(comp, "countOfColor").Get()[color].ToString() }));

        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessColouredCubes(ModuleData module)
    {
        var comp = GetComponent(module, "ColouredCubesModule");

        var screenText = GetField<string>(GetField<object>(comp, "Screen", isPublic: true).Get(), "_defaultText");

        var cubes = GetField<Array>(comp, "Cubes", isPublic: true).Get(arr => arr.Length != 9 ? "expected length 9" : null);
        var fldCubePosition = GetIntField(cubes.GetValue(0), "_position");
        var fldCubeColour = GetField<string>(cubes.GetValue(0), "_colourName");
        var allCubeColours = GetField<Dictionary<string, string>>(cubes.GetValue(0), "TernaryColourValuesToName").Get().Values.ToArray();

        var stageLights = GetField<Array>(comp, "StageLights", isPublic: true).Get(arr => arr.Length != 3 ? "expected length 3" : null);
        var fldStageLightNumber = GetIntField(stageLights.GetValue(0), "_stageNumber");
        var fldStageLightColour = GetField<string>(stageLights.GetValue(0), "_colourName");
        var allStageLightColours = GetStaticField<Dictionary<string, string>>(stageLights.GetValue(0).GetType(), "BinaryColourValuesToName").Get().Values.ToArray();

        var cubeColours = new string[3, 9];
        var stageLightColours = new string[2, 3];

        for (int nextStage = 1; nextStage <= 3; nextStage++)
        {
            while (screenText.Get(nullAllowed: true) != $"Stage {nextStage}")
                yield return null; // Do not wait 0.1 seconds to make sure we get the correct colours.
            foreach (var cube in cubes)
            {
                int position = fldCubePosition.GetFrom(cube, min: 0, max: 8);
                cubeColours[nextStage - 1, position] = fldCubeColour.GetFrom(cube, col => !allCubeColours.Contains(col) ? $"invalid cube colour ‘{col}’" : null);
            }
            if (nextStage != 3)
            {
                foreach (var light in stageLights)
                {
                    int number = 3 - fldStageLightNumber.GetFrom(light, min: 1, max: 3);
                    stageLightColours[nextStage - 1, number] = fldStageLightColour.GetFrom(light, col => !allStageLightColours.Contains(col) ? $"invalid stage light colour ‘{col}’" : null);
                }
            }
        }

        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (int stage = 0; stage < 3; stage++)
        {
            for (int ix = 0; ix < 9; ix++)
                qs.Add(makeQuestion(Question.ColouredCubesColours, module, Sprites.GenerateGridSprite(3, 3, ix), formatArgs: new[] { "cube", Ordinal(stage + 1) }, correctAnswers: new[] { cubeColours[stage, ix] }, preferredWrongAnswers: allCubeColours));
            if (stage < 2)
                for (int ix = 0; ix < 3; ix++)
                    qs.Add(makeQuestion(Question.ColouredCubesColours, module, Sprites.GenerateGridSprite(1, 3, ix), formatArgs: new[] { "stage light", Ordinal(stage + 1) }, correctAnswers: new[] { stageLightColours[stage, ix] }, preferredWrongAnswers: allStageLightColours));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessColouredCylinder(ModuleData module)
    {
        yield return WaitForSolve;
        var comp = GetComponent(module, "colouredCylinder");
        // The module can theoretically generate an arbitrarily large sequence of colours
        var sequence = GetListField<int>(comp, "colourIndexes").Get(minLength: 6, validator: v => v is < 0 or > 6 ? "Out of range [0, 6]" : null);
        addQuestions(module, sequence.Select((c, i) =>
            makeQuestion(Question.ColouredCylinderColours, module,
                correctAnswers: new[] { Question.ColouredCylinderColours.GetAnswers()[c] },
                formatArgs: new[] { Ordinal(i + 1) })));
    }

    private IEnumerator<YieldInstruction> ProcessColourFlash(ModuleData module)
    {
        var comp = GetComponent(module, "ColourFlashModule");

        yield return WaitForSolve;

        var fldColorSequence = GetArrayField<object>(comp, "_colourSequence").Get(ar => ar.Length != 8 ? "expected length 8" : null);
        var colorValue = GetField<object>(fldColorSequence.GetValue(7), "ColourValue", isPublic: true).Get();

        addQuestion(module, Question.ColourFlashLastColor, correctAnswers: new[] { colorValue.ToString() });
    }

    private List<int[]> _concentrationStages = new();
    private IEnumerator<YieldInstruction> ProcessConcentration(ModuleData module)
    {
        var comp = GetComponent(module, "ConcentrationModule");
        const string moduleId = "ConcentrationModule";

        yield return null; // The module waits one frame after Start(), so we wait two (Souvenir already waits one frame on its own)

        var stage = GetArrayField<int>(comp, "_initialOrder").Get(expectedLength: 15, validator: i => i is < 0 or > 14 ? "Out of range 0-14" : null);
        if (stage.Distinct().Count() != 15)
            throw new AbandonModuleException($"Unexpected duplicate numbers. {stage.JoinString(" ")}");
        _concentrationStages.Add(stage);

        var swapCount = GetIntField(comp, "_lastStage").Get(0, 106) - 1;

        var swappedPositions = GetArrayField<(int one, int two)>(comp, "_swaps")
            .Get(expectedLength: Math.Max(swapCount, 0), validator: t => t.one is < 0 or > 14 ? "First out of range 0-14" : t.two <= t.one || t.two > 14 ? "Second out of range (first+1)-14" : null)
            .SelectMany(t => new[] { t.one, t.two })
            .Distinct()
            .ToList();

        yield return null; // Wait for other instances of this handler to find the first stage.

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);

        if (swapCount < 1)
        {
            legitimatelyNoQuestion(module, "No question for Concentration because no swaps occurred.");
            yield break;
        }

        if (_moduleCounts[moduleId] == 1)
            addQuestions(module, swappedPositions.Select(ix => makeQuestion(Question.ConcentrationStartingDigit, moduleId, 1, questionSprite: Sprites.GenerateGridSprite(3, 5, ix), correctAnswers: new[] { (stage[ix] + 1).ToString() })));
        else
        {
            var validUnique = Enumerable
                .Range(0, 14)
                .Where(ix => _concentrationStages.Count(s => s[ix] == stage[ix]) == 1)
                .ToArray();

            if (validUnique.Length is 0)
            {
                int id = GetIntField(comp, "_moduleId").Get(min: 0);
                legitimatelyNoQuestion(module, $"No question for Concentration because no position was unique for this one (#{id}).");
                yield break;
            }

            if (validUnique.Length == 1 && swappedPositions.Contains(validUnique[0]))
                swappedPositions.Remove(validUnique[0]); // swappedPositions cannot have a single element

            List<QandA> qs = new();
            foreach (var ix in swappedPositions)
            {
                var unique = validUnique.Except(new[] { ix }).PickRandom();
                var moduleName = string.Format(translateString(Question.ForgetMeNotDisplayedDigits, "the Concentration which began with {1} in the {0} position (in reading order)"), Ordinal(unique + 1), stage[unique] + 1);
                qs.Add(makeQuestion(Question.ConcentrationStartingDigit, moduleId, 1, questionSprite: Sprites.GenerateGridSprite(3, 5, ix), correctAnswers: new[] { (stage[ix] + 1).ToString() }, formattedModuleName: moduleName));
            }

            addQuestions(module, qs);
        }
    }

    private IEnumerator<YieldInstruction> ProcessConditionalButtons(ModuleData module)
    {
        var comp = GetComponent(module, "conditionalButtons");
        // Get the colors of the buttons when first starting the module
        var buttonColors = new List<string>();
        foreach (var button in GetListField<KMSelectable>(comp, "Buttons", isPublic: true).Get(expectedLength: 6))
        {
            var buttonColor = button.GetComponent<MeshRenderer>().material.name;
            buttonColors.Add(buttonColor.Remove(buttonColor.IndexOf(" (Instance)")));
        }
        yield return WaitForSolve;
        addQuestions(module, buttonColors.Select((color, ix) => makeQuestion(Question.ConditionalButtonsColors, module, questionSprite: Sprites.GenerateGridSprite(new Coord(3, 2, ix)), correctAnswers: new[] { color })));
    }

    private IEnumerator<YieldInstruction> ProcessConnectedMonitors(ModuleData module)
    {
        var comp = GetComponent(module, "ConnectedMonitorsScript");

        yield return null; // Wait for monitors to become available
        var monitors = GetField<IList>(comp, "_monitors").Get(l => l.Count != 15 ? $"Bad monitor list length {l}" : null);
        var displayProp = GetProperty<int>(monitors[0], "DisplayValue", isPublic: true);
        // Grab screen values before they change from the blue-green rule
        var displays = monitors.Cast<object>().Select(m => displayProp.GetFrom(m, v => v is > 99 or < 0 ? $"Bad monitor value {v}" : null)).ToArray();

        yield return WaitForSolve;

        var indsProp = GetProperty<IList>(monitors[0], "Indicators", isPublic: true);
        PropertyInfo<object> colorProp = null;
        IEnumerable<QandA> processMonitor(object mon, int ix)
        {
            yield return makeQuestion(Question.ConnectedMonitorsNumber, module, questionSprite: ConnectedMonitorsSprites[ix],
                    correctAnswers: new[] { displays[ix].ToString() });
            var inds = indsProp.GetFrom(mon, validator: v => v.Count is > 3 or < 0 ? $"Bad indicator count {v.Count} (Monitor {ix})" : null);
            foreach (var q in inds.Cast<object>().Select((ind, indIx) =>
                    makeQuestion(inds.Count == 1 ? Question.ConnectedMonitorsSingleIndicator : Question.ConnectedMonitorsOrdinalIndicator, module, questionSprite: ConnectedMonitorsSprites[ix],
                    correctAnswers: new[] { (colorProp ??= GetProperty<object>(ind, "Color", isPublic: true)).GetFrom(ind, v => (int) v is < 0 or > 5 ? $"Bad indicator color {v} (Monitor {ix}) (Indicator {indIx})" : null).ToString() },
                    formatArgs: new[] { Ordinal(indIx + 1) })))
                yield return q;
        }
        addQuestions(module, monitors.Cast<object>().SelectMany(processMonitor));
    }

    private IEnumerator<YieldInstruction> ProcessConnectionCheck(ModuleData module)
    {
        var comp = GetComponent(module, "GraphModule");

        yield return WaitForSolve;

        float[] valid = new float[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        var queries = GetArrayField<Vector2>(comp, "Queries")
            .Get(expectedLength: 4, validator: v =>
            !valid.Contains(v.x) ? $"x out of bounds (got: {v.x})" :
            !valid.Contains(v.y) ? $"y out of bounds (got: {v.y})" :
            v.y <= v.x ? $"y less than or equal to x (got: {v.x} {v.y})" : null);

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

    private IEnumerator<YieldInstruction> ProcessCoordinates(ModuleData module)
    {
        var comp = GetComponent(module, "CoordinatesModule");
        var fldFirstSubmitted = GetField<int?>(comp, "_firstCorrectSubmitted");

        while (fldFirstSubmitted.Get(nullAllowed: true) == null)
            yield return new WaitForSeconds(.1f);

        var fldClues = GetField<IList>(comp, "_clues");
        var clues = fldClues.Get();
        var index = fldFirstSubmitted.Get(v => v < 0 || v >= clues.Count ? $"out of range; clues.Count={clues.Count}" : null).Value;
        var clue = clues[index];
        var fldClueText = GetField<string>(clue, "Text");
        var fldClueSystem = GetField<int?>(clue, "System");
        var clueText = fldClueText.Get();

        yield return WaitForSolve;

        // The size clue is the only one where fldClueSystem is null
        var sizeClue = clues.Cast<object>().Where(szCl => fldClueSystem.GetFrom(szCl, nullAllowed: true) == null).FirstOrDefault();
        addQuestions(module,
            makeQuestion(Question.CoordinatesFirstSolution, module, correctAnswers: new[] { clueText.Replace("\n", " ") }, preferredWrongAnswers: clues.Cast<object>().Select(c => fldClueText.GetFrom(c).Replace("\n", " ")).Where(t => t != null).ToArray()),
            sizeClue == null ? null : makeQuestion(Question.CoordinatesSize, module, correctAnswers: new[] { fldClueText.GetFrom(sizeClue) }));
    }

    private IEnumerator<YieldInstruction> ProcessCoralCipher(ModuleData module)
    {
        return processColoredCiphers(module, "coralCipher", Question.CoralCipherScreen);
    }

    private IEnumerator<YieldInstruction> ProcessCorners(ModuleData module)
    {
        var comp = GetComponent(module, "CornersModule");
        yield return WaitForSolve;

        var colorNames = new[] { "red", "green", "blue", "yellow" };
        var cornerNames = new[] { "top-left", "top-right", "bottom-right", "bottom-left" };

        var clampColors = GetArrayField<int>(comp, "_clampColors").Get(expectedLength: 4, validator: v => v < 0 || v >= colorNames.Length ? $"expected 0–{colorNames.Length - 1}" : null);
        var qs = new List<QandA>();
        qs.AddRange(cornerNames.Select((corner, cIx) => makeQuestion(Question.CornersColors, module, formatArgs: new[] { corner }, correctAnswers: new[] { colorNames[clampColors[cIx]] })));
        qs.AddRange(colorNames.Select((col, colIx) => makeQuestion(Question.CornersColorCount, module, formatArgs: new[] { col }, correctAnswers: new[] { clampColors.Count(cc => cc == colIx).ToString() })));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessCornflowerCipher(ModuleData module)
    {
        return processColoredCiphers(module, "cornflowerCipher", Question.CornflowerCipherScreen);
    }

    private IEnumerator<YieldInstruction> ProcessCosmic(ModuleData module)
    {
        var comp = GetComponent(module, "CosmicModule");
        var answer = GetField<TextMesh>(comp, "DisplayText", isPublic: true).Get().text;

        yield return WaitForSolve;

        addQuestion(module, Question.CosmicNumber, correctAnswers: new[] { answer });
    }

    private IEnumerator<YieldInstruction> ProcessCrazyHamburger(ModuleData module)
    {
        var comp = GetComponent(module, "CrazyHamburgerScript");
        var fldIngredients = GetField<string>(comp, "Ingredients");

        yield return WaitForSolve;

        var dic = new Dictionary<char, string>()
        {
            ['B'] = "Bread",
            ['C'] = "Cheese",
            ['G'] = "Grass",
            ['H'] = "Meat",
            ['O'] = "Oil",
            ['R'] = "Peppers"
        };

        var ingredients = fldIngredients.Get(v => v.Any(ch => !dic.ContainsKey(ch)) ? $"expected only characters {dic.Keys.JoinString()}" : null);

        addQuestions(module, ingredients.Select((ing, i) =>
            makeQuestion(
                question: Question.CrazyHamburgerIngredient,
                data: module,
                formatArgs: new string[] { Ordinal(i + 1) },
                correctAnswers: new[] { dic[ing] })));
    }

    private IEnumerator<YieldInstruction> ProcessCrazyMaze(ModuleData module)
    {
        var comp = GetComponent(module, "CrazyMazeScript");
        var fldStart = GetIntField(comp, "_startingCell");
        var fldGoal = GetIntField(comp, "_goalCell");
        var fldCellLetters = GetArrayField<string>(comp, "_cellLetters");

        yield return WaitForSolve;

        var cellLetters = fldCellLetters.Get(expectedLength: 26 * 26);
        var start = cellLetters[fldStart.Get(min: 0, max: 26 * 26 - 1)];
        var goal = cellLetters[fldGoal.Get(min: 0, max: 26 * 26 - 1)];
        addQuestions(module,
            makeQuestion(Question.CrazyMazeStartOrGoal, module, formatArgs: new[] { "starting" }, correctAnswers: new[] { start }, preferredWrongAnswers: new[] { goal }),
            makeQuestion(Question.CrazyMazeStartOrGoal, module, formatArgs: new[] { "goal" }, correctAnswers: new[] { goal }, preferredWrongAnswers: new[] { start }));
    }

    private IEnumerator<YieldInstruction> ProcessCreamCipher(ModuleData module)
    {
        return processColoredCiphers(module, "creamCipher", Question.CreamCipherScreen);
    }

    private IEnumerator<YieldInstruction> ProcessCreation(ModuleData module)
    {
        var comp = GetComponent(module, "CreationModule");
        var fldDay = GetIntField(comp, "Day");
        var fldWeather = GetField<string>(comp, "Weather");

        var weatherNames = Question.CreationWeather.GetAnswers();

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var currentDay = fldDay.Get(min: 1, max: 1);
        var currentWeather = fldWeather.Get(cw => !weatherNames.Contains(cw) ? "unknown weather" : null);
        var allWeather = new List<string>();
        while (true)
        {
            while (fldDay.Get() == currentDay && module.Unsolved && currentWeather == fldWeather.Get())
                yield return new WaitForSeconds(0.1f);

            if (module.IsSolved)
                break;

            if (fldDay.Get() <= currentDay)
                allWeather.Clear();
            else
                allWeather.Add(currentWeather);

            currentDay = fldDay.Get(min: 1, max: 6);
            currentWeather = fldWeather.Get(cw => !weatherNames.Contains(cw) ? "unknown weather" : null);
        }

        addQuestions(module, allWeather.Select((t, i) => makeQuestion(Question.CreationWeather, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { t })));
    }

    private IEnumerator<YieldInstruction> ProcessCrimsonCipher(ModuleData module)
    {
        return processColoredCiphers(module, "crimsonCipher", Question.CrimsonCipherScreen);
    }

    private IEnumerator<YieldInstruction> ProcessCritters(ModuleData module)
    {
        var comp = GetComponent(module, "CrittersScript");
        var fldColorIx = GetIntField(comp, "_randomiser");

        yield return WaitForSolve;

        var colorNames = new[] { "Yellow", "Pink", "Blue" };
        var colorIx = fldColorIx.Get(min: 0, max: 2);

        addQuestions(module, makeQuestion(Question.CrittersColor, module, correctAnswers: new[] { colorNames[colorIx] }));
    }

    private IEnumerator<YieldInstruction> ProcessCruelBinary(ModuleData module)
    {
        var comp = GetComponent(module, "CruelBinary");

        yield return WaitForSolve;

        var wordList = GetArrayField<string>(comp, "_WordList", isPublic: true).Get();
        var displayedWord = GetField<string>(comp, "h", isPublic: true).Get();
        addQuestion(module, Question.CruelBinaryDisplayedWord, correctAnswers: new[] { displayedWord }, preferredWrongAnswers: wordList);
    }

    private IEnumerator<YieldInstruction> ProcessCruelKeypads(ModuleData module)
    {
        var comp = GetComponent(module, "CruelKeypadScript");

        yield return WaitForSolve;

        var firstTwoColors = GetField<Array>(comp, "StageColor").Get(arr => arr.Length != 2 ? "expected length 2" : null);
        var colors = new string[]
        {
            firstTwoColors.GetValue(0).ToString(),
            firstTwoColors.GetValue(1).ToString(),
            GetField<Enum>(comp, "stripColor").Get().ToString()
        };
        var fieldNames = new[] { "Stage1Symbols", "Stage2Symbols", "pickedSymbols" };
        // Unfortunately, these are stored as IList<char> types instead of just List<char>, so we can't use GetListField.
        string[][] displayedSymbolSets = fieldNames.Select(name => GetField<IList<char>>(comp, name).Get(list => list.Count != 4 ? "expected length 4" : null).Select(c => c.ToString()).ToArray()).ToArray();

        var qs = new List<QandA>();
        for (int stage = 0; stage < 3; stage++)
        {
            string stageNum = Ordinal(stage + 1);
            qs.Add(makeQuestion(Question.CruelKeypadsColors, module, formatArgs: new[] { stageNum }, correctAnswers: new[] { colors[stage] }));
            qs.Add(makeQuestion(Question.CruelKeypadsDisplayedSymbols, module, formatArgs: new[] { stageNum }, correctAnswers: displayedSymbolSets[stage]));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessCRule(ModuleData module)
    {
        var comp = GetComponent(module, "TheCRuleScript");

        var symbolTextMeshes = GetArrayField<TextMesh>(comp, "symbols", isPublic: true).Get(expectedLength: 26);
        var symbols = symbolTextMeshes.Select((tm, ix) => (symPair: tm.text, cell: ix)).Where(tup => !string.IsNullOrEmpty(tup.symPair)).ToArray();

        yield return WaitForSolve;

        // This contains the indexes of the pre-filled squares, but counting from 1
        var initOn = GetArrayField<int>(comp, "initOn").Get(expectedLength: 10);

        var cells = Enumerable.Range(0, 8).Select(x => (x: 4 * x, y: 0))
            .Concat(Enumerable.Range(0, 7).Select(x => (x: 4 * x + 2, y: 4)))
            .Concat(Enumerable.Range(0, 6).Select(x => (x: 4 * x + 4, y: 8)))
            .Concat(Enumerable.Range(0, 5).Select(x => (x: 4 * x + 6, y: 12)))
            .ToArray();
        var cellSprites = Enumerable.Range(0, 26).Select(cell => Sprites.GenerateGridSprite("cRule", 4 * 8 + 1, 4 * 4 + 1, cells, cell, $"cRule row {cells[cell].y / 4 + 1} cell {(cells[cell].x - cells[cell].y / 2) / 4 + 1}", 80)).ToArray();

        var qs = new List<QandA>();

        var displayedSymbols = symbols.Select(tup => tup.symPair).ToArray();
        var displayedCells = symbols.Select(tup => cellSprites[tup.cell]).ToArray();
        foreach (var (symPair, cell) in symbols)
        {
            // "Which symbol pair was here in {0}?"
            qs.Add(makeQuestion(Question.CRuleSymbolPair, module, questionSprite: cellSprites[cell],
                correctAnswers: new[] { symPair }, preferredWrongAnswers: displayedSymbols));

            // "Where was {1} in {0}?"
            qs.Add(makeQuestion(Question.CRuleSymbolPairCell, module, formatArgs: new[] { symPair },
                correctAnswers: new[] { cellSprites[cell] }, allAnswers: cellSprites, preferredWrongAnswers: displayedCells));
        }

        // "Which symbol pair was present on {0}?"
        qs.Add(makeQuestion(Question.CRuleSymbolPairPresent, module, correctAnswers: displayedSymbols));

        // "Which cell was pre-filled at the start of {0}?"
        qs.Add(makeQuestion(Question.CRulePrefilled, module, correctAnswers: initOn.Select(cellOffBy1 => cellSprites[cellOffBy1 - 1]).ToArray(), allAnswers: cellSprites));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessCrypticCycle(ModuleData module)
    {
        return processSpeakingEvilCycle2(module, "CrypticCycleScript", Question.CrypticCycleWord);
    }

    private IEnumerator<YieldInstruction> ProcessCrypticKeypad(ModuleData module)
    {
        var comp = GetComponent(module, "CrypticKeypadScript");
        yield return WaitForSolve;

        var letters = GetArrayField<string>(comp, "Letters2").Get();
        var rotations = GetArrayField<int>(comp, "Rotations").Get();

        var qs = new List<QandA>();
        var directions = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };
        var cardinalDirections = new[] { "North", "East", "South", "West" };
        for (int i = 0; i < 4; i++)
        {
            qs.Add(makeQuestion(Question.CrypticKeypadLabels, module, formatArgs: new[] { directions[i] }, correctAnswers: new[] { letters[i] }));
            qs.Add(makeQuestion(Question.CrypticKeypadRotations, module, formatArgs: new[] { directions[i] }, correctAnswers: new[] { cardinalDirections[rotations[i]] }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessCube(ModuleData module)
    {
        var comp = GetComponent(module, "theCubeScript");
        yield return WaitForSolve;

        var rotations = GetListField<int>(comp, "selectedRotations").Get(expectedLength: 6);
        var rotationNames = new[] { "rotate cw", "tip left", "tip backwards", "rotate ccw", "tip right", "tip forwards" };
        var allRotations = rotations.Select(r => rotationNames[r]).ToArray();

        addQuestions(module, rotations.Select((rot, ix) => makeQuestion(Question.CubeRotations, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { rotationNames[rot] }, preferredWrongAnswers: allRotations)));
    }

    private IEnumerator<YieldInstruction> ProcessCursedDoubleOh(ModuleData module)
    {
        var comp = GetComponent(module, "DoubleOhModule");

        yield return WaitForSolve;

        int firstNumber = GetField<List<int>>(comp, "visitedNumbers").Get().First();
        string firstDigit = (firstNumber / 10).ToString();
        addQuestion(module, Question.CursedDoubleOhInitialPosition, correctAnswers: new[] { firstDigit });
    }

    private IEnumerator<YieldInstruction> ProcessCustomerIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "CustomerIdentificationScript");

        yield return WaitForSolve;

        var seedPacketIdentifier = GetField<Sprite[]>(comp, "SeedPacketIdentifier", isPublic: true).Get();
        var unique = GetArrayField<int>(comp, "Unique").Get(expectedLength: 3);
        var answers = unique.Select(uq => seedPacketIdentifier[uq].name).ToArray();

        addQuestions(module, Enumerable.Range(0, 3).Select(i => makeQuestion(
            question: Question.CustomerIdentificationCustomer,
            data: module,
            formatArgs: new[] { Ordinal(i + 1) },
            correctAnswers: new[] { answers[i] })));
    }

    private IEnumerator<YieldInstruction> ProcessCyanButton(ModuleData module)
    {
        var comp = GetComponent(module, "CyanButtonScript");
        yield return WaitForSolve;
        var positions = GetArrayField<int>(comp, "_buttonPositions").Get(expectedLength: 6);

        addQuestions(module, Enumerable.Range(0, 6).Select(stage => makeQuestion(Question.CyanButtonPositions, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { Question.CyanButtonPositions.GetAttribute().AllAnswers[positions[stage]] })));
    }
}
