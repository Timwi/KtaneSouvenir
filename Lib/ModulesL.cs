using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Souvenir;
using Souvenir.Reflection;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessLabyrinth(KMBombModule module)
    {
        var comp = GetComponent(module, "labyrinthScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_Labyrinth);

        var l1 = new object[]
        {
            GetField<object>(comp, "level1Info", true).Get(),
            GetField<object>(comp, "level2Info", true).Get(),
            GetField<object>(comp, "level3Info", true).Get(),
            GetField<object>(comp, "level4Info", true).Get(),
            GetField<object>(comp, "level5Info", true).Get()
        };

        var t1 = GetField<int>(l1[0], "target1", isPublic: true);
        var t2 = GetField<int>(l1[0], "target2", isPublic: true);
        var portals = l1.Select(info =>
            new int[] { t1.GetFrom(info), t2.GetFrom(info) }
                .Select(t => t >= 5 ? t + 1 : t).ToArray()) // Top-right corner
            .ToArray();
        var flatPortals = portals.SelectMany(i => i).ToArray();
        var distinctPortals = flatPortals.Distinct().ToArray();

        var qs = new List<QandA>(15);

        var args = new[] { "1 (Red)", "2 (Orange)", "3 (Yellow)", "4 (Green)", "5 (Blue)" };
        for (var layer = 0; layer < 5; layer++)
            qs.Add(makeQuestion(Question.LabyrinthPortalLocations, _Labyrinth, formatArgs: new[] { args[layer] }, correctAnswers: new[] { new Coord(6, 7, portals[layer][0]), new Coord(6, 7, portals[layer][1]) }, preferredWrongAnswers: distinctPortals.Select(i => new Coord(6, 7, i)).ToArray()));

        foreach (int p in distinctPortals)
        {
            var correct = new List<string>();
            for (var i = 0; i < 10; i++)
                if (flatPortals[i] == p)
                    correct.Add(args[i / 2]); // Integer division gives layer #
            if (correct.Distinct().Count() > 2)
                continue; // Don't have a question with less than 4 answers
            qs.Add(makeQuestion(Question.LabyrinthPortalStage, _Labyrinth, questionSprite: Grid.GenerateGridSprite(new Coord(6, 7, p)), correctAnswers: correct.Distinct().ToArray()));
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessLadderLottery(KMBombModule module)
    {
        var comp = GetComponent(module, "LadderLottery");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var fldPoint = GetField<Enum>(comp, "_startingPoint");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_LadderLottery);

        addQuestion(module, Question.LadderLotteryLightOn, correctAnswers: new[] { fldPoint.Get().ToString() });
    }

    private IEnumerable<object> ProcessLadders(KMBombModule module)
    {
        var comp = GetComponent(module, "LaddersScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        var fldLadderCols = GetArrayField<int[]>(comp, "ladderColors");
        var fldMissing = GetIntField(comp, "missingColor");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_Ladders);

        var secondLadder = fldLadderCols.Get(expectedLength: 3)[1];
        var missing = fldMissing.Get(min: 0, max: 7);
        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Cyan", "Purple", "Gray" };

        addQuestions(module,
            makeQuestion(Question.LaddersStage2Colors, _Ladders, correctAnswers: secondLadder.Distinct().Select(x => colorNames[x]).ToArray()),
            makeQuestion(Question.LaddersStage3Missing, _Ladders, correctAnswers: new[] { colorNames[missing] }));
    }

    private IEnumerable<object> ProcessLasers(KMBombModule module)
    {
        var comp = GetComponent(module, "LasersModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Lasers);

        var laserOrder = GetListField<int>(comp, "_laserOrder").Get(expectedLength: 9);
        var hatchesPressed = GetListField<int>(comp, "_hatchesAlreadyPressed").Get(expectedLength: 7);
        var hatchNames = new[] { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        addQuestions(module, hatchesPressed.Select((hatch, ix) => makeQuestion(Question.LasersHatches, _Lasers, formatArgs: new[] { hatchNames[hatch] }, correctAnswers: new[] { laserOrder[hatch].ToString() }, preferredWrongAnswers: hatchesPressed.Select(number => laserOrder[number].ToString()).ToArray())));
    }

    private IEnumerable<object> ProcessLEDEncryption(KMBombModule module)
    {
        var comp = GetComponent(module, "LEDEncryption");

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var buttons = GetArrayField<KMSelectable>(comp, "buttons", true).Get(expectedLength: 4);
        var buttonLabels = buttons.Select(btn => btn.GetComponentInChildren<TextMesh>()).ToArray();
        if (buttonLabels.Any(x => x == null))
            throw new AbandonModuleException("At least one of the buttons’ TextMesh is null.");

        var multipliers = GetArrayField<int>(comp, "layerMultipliers").Get(minLength: 2, maxLength: 5, validator: m => m < 2 || m > 7 ? "expected range 2–7" : null);
        var numStages = multipliers.Length;
        var pressedLetters = new string[numStages];
        var wrongLetters = new HashSet<string>();
        var fldStage = GetIntField(comp, "layer");

        var reassignButton = Ut.Lambda((KMSelectable btn, TextMesh lbl) =>
        {
            var prevInteract = btn.OnInteract;
            var stage = fldStage.Get();
            btn.OnInteract = delegate
            {
                var label = lbl.text;
                var ret = prevInteract();
                if (fldStage.Get() > stage)
                    pressedLetters[stage] = label;
                return ret;
            };
        });

        while (fldStage.Get() < numStages)
        {
            foreach (var lbl in buttonLabels)
                wrongLetters.Add(lbl.text);

            // LED Encryption re-hooks the buttons at every press, so we have to re-hook it at each stage as well
            for (int i = 0; i < 4; i++)
                reassignButton(buttons[i], buttonLabels[i]);

            var stage = fldStage.Get();
            while (fldStage.Get() == stage)
                yield return null;
        }

        _modulesSolved.IncSafe(_LEDEncryption);
        addQuestions(module, Enumerable.Range(0, pressedLetters.Length - 1)
            .Where(i => pressedLetters[i] != null)
            .Select(stage => makeQuestion(Question.LEDEncryptionPressedLetters, _LEDEncryption, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { pressedLetters[stage] }, preferredWrongAnswers: wrongLetters.ToArray())));
    }

    private IEnumerable<object> ProcessLEDMath(KMBombModule module)
    {
        var comp = GetComponent(module, "LEDMathScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var ledA = GetIntField(comp, "ledAIndex").Get(min: 0, max: 3);
        var ledB = GetIntField(comp, "ledBIndex").Get(min: 0, max: 3);
        var ledOp = GetIntField(comp, "ledOpIndex").Get(min: 0, max: 3);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_LEDMath);

        var ledColors = new[] { "Red", "Blue", "Green", "Yellow" };

        addQuestions(module,
            makeQuestion(Question.LEDMathLights, _LEDMath, formatArgs: new[] { "LED A" }, correctAnswers: new[] { ledColors[ledA] }),
            makeQuestion(Question.LEDMathLights, _LEDMath, formatArgs: new[] { "LED B" }, correctAnswers: new[] { ledColors[ledB] }),
            makeQuestion(Question.LEDMathLights, _LEDMath, formatArgs: new[] { "the operator LED" }, correctAnswers: new[] { ledColors[ledOp] }));
    }

    private IEnumerable<object> ProcessLEDs(KMBombModule module)
    {
        var comp = GetComponent(module, "LEDsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_LEDs);

        var fldInitColor = GetField<object>(comp, "colorChangedTo");
        var fldActualColor = GetField<object>(comp, "currentDisplayOnChanged");

        string initStr = fldInitColor.Get(col => (int) col > 7 || (int) col < 0 ? "expected value 0-7" : null).ToString();
        string actualStr = fldActualColor.Get(col => (int) col > 7 || (int) col < 0 ? "expected value 0-7" : null).ToString();

        addQuestion(module, Question.LEDsOriginalColor, correctAnswers: new[] { initStr }, preferredWrongAnswers: new[] { actualStr });
    }

    private IEnumerable<object> ProcessLEGOs(KMBombModule module)
    {
        var comp = GetComponent(module, "LEGOModule");

        var solutionStruct = GetField<object>(comp, "SolutionStructure").Get();
        var pieces = GetField<IList>(solutionStruct, "Pieces", isPublic: true).Get(ar => ar.Count != 6 ? "expected length 6" : null);

        // Hook into the module’s OnPass handler
        var isSolved = false;
        module.OnPass += delegate { isSolved = true; return false; };
        yield return new WaitUntil(() => isSolved);

        // Block the left/right buttons so the player can’t see the instruction pages anymore
        var leftButton = GetField<KMSelectable>(comp, "LeftButton", isPublic: true).Get();
        var rightButton = GetField<KMSelectable>(comp, "RightButton", isPublic: true).Get();

        leftButton.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.transform);
            leftButton.AddInteractionPunch(0.5f);
            return false;
        };
        rightButton.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, module.transform);
            rightButton.AddInteractionPunch(0.5f);
            return false;
        };

        // Erase the solution so the player can’t see brick sizes on it either
        var submission = GetArrayField<int>(comp, "Submission").Get();
        for (int i = 0; i < submission.Length; i++)
            submission[i] = 0;
        GetMethod(comp, "UpdateDisplays", numParameters: 0).Invoke();

        // Obtain the brick sizes and colors
        var fldBrickColors = GetIntField(pieces[0], "BrickColor", isPublic: true);
        var fldBrickDimensions = GetArrayField<int>(pieces[0], "Dimensions", isPublic: true);
        var brickColors = Enumerable.Range(0, 6).Select(i => fldBrickColors.GetFrom(pieces[i])).ToList();
        var brickDimensions = Enumerable.Range(0, 6).Select(i => fldBrickDimensions.GetFrom(pieces[i])).ToList();

        _modulesSolved.IncSafe(_LEGOs);
        var colorNames = new[] { "red", "green", "blue", "cyan", "magenta", "yellow" };
        addQuestions(module, Enumerable.Range(0, 6).Select(i => makeQuestion(Question.LEGOsPieceDimensions, _LEGOs, formatArgs: new[] { colorNames[brickColors[i]] }, correctAnswers: new[] { brickDimensions[i][0] + "×" + brickDimensions[i][1] })));
    }

    private IEnumerable<object> ProcessLetterMath(KMBombModule module)
    {
        var comp = GetComponent(module, "LetterMathModule");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        var characters = GetArrayField<int>(comp, "characters").Get();
        var letters = Enumerable.Range(0, 2).ToArray().Select(i => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(characters[i], 1)).ToArray();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_LetterMath);

        var wrongLetters = Enumerable.Range(0, 26).ToArray().Select(i => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(i, 1)).ToArray();

        addQuestions(module,
            makeQuestion(Question.LetterMathDisplay, _LetterMath, formatArgs: new[] { "left" }, correctAnswers: new[] { letters[0] }, preferredWrongAnswers: wrongLetters),
            makeQuestion(Question.LetterMathDisplay, _LetterMath, formatArgs: new[] { "right" }, correctAnswers: new[] { letters[1] }, preferredWrongAnswers: wrongLetters));
    }

    private IEnumerable<object> ProcessLightBulbs(KMBombModule module)
    {
        var comp = GetComponent(module, "LightBulbsScript");

        var fldSolved = GetField<bool>(comp, "isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_LightBulbs);

        var bulbs = GetField<IList>(comp, "Bulbs").Get(lst => lst.Count != 3 ? "expected length 3" : null);

        addQuestions(
            module,
            makeQuestion(Question.LightBulbsColors, _LightBulbs, formatArgs: new[] { "left" }, correctAnswers: new[] { GetField<Enum>(bulbs[0], "Color", isPublic: true).Get().ToString() }),
            makeQuestion(Question.LightBulbsColors, _LightBulbs, formatArgs: new[] { "right" }, correctAnswers: new[] { GetField<Enum>(bulbs[2], "Color", isPublic: true).Get().ToString() })
        );
    }

    private IEnumerable<object> ProcessLinq(KMBombModule module)
    {
        var comp = GetComponent(module, "LinqScript");
        var fldSolved = GetProperty<bool>(comp, "IsSolved", isPublic: true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Linq);

        var select = GetField<object>(comp, "select").Get();
        var functions = GetField<Array>(select, "functions").Get(ar =>
            ar.Length != 3 ? "expected length 3" :
            ar.OfType<object>().Any(v => !GetAnswers(Question.LinqFunction).Contains(v.ToString())) ? "contains unknown function" : null);

        var qs = new List<QandA>();
        for (int i = 0; i < functions.GetLength(0); i++)
            qs.Add(makeQuestion(Question.LinqFunction, _Linq, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { functions.GetValue(i).ToString() }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessLionsShare(KMBombModule module)
    {
        var comp = GetComponent(module, "LionsShareModule");
        var yearText = GetField<TextMesh>(comp, "Year", isPublic: true).Get().text;
        if (!int.TryParse(yearText, out var year) || year < 1 || year > 16)
            throw new AbandonModuleException($"Expected year number between 1 and 16; got: {yearText}");

        var fldSolved = GetField<bool>(comp, "_isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_LionsShare);

        var lionNames = GetArrayField<string>(comp, "_lionNames").Get(minLength: 2);
        var correctPortions = GetArrayField<int>(comp, "_correctPortions").Get(expectedLength: lionNames.Length);
        var removedLions = Enumerable.Range(0, lionNames.Length).Where(ix => correctPortions[ix] == 0).Select(ix => lionNames[ix]).ToArray();
        var allLionNames = GetListField<string>(comp, "_allLionNames").Get(expectedLength: 35);

        addQuestions(module,
            makeQuestion(Question.LionsShareYear, _LionsShare, correctAnswers: new[] { yearText }),
            makeQuestion(Question.LionsShareRemovedLions, _LionsShare, correctAnswers: removedLions, preferredWrongAnswers: allLionNames.ToArray()));
    }

    private IEnumerable<object> ProcessListening(KMBombModule module)
    {
        var comp = GetComponent(module, "Listening");
        var fldCode = GetArrayField<char>(comp, "codeInput");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Listening);
        var correctCode = fldCode.Get(expectedLength: 5).JoinString();
        addQuestion(module, Question.ListeningCode, correctAnswers: new[] { correctCode });
    }

    private IEnumerable<object> ProcessLogicalButtons(KMBombModule module)
    {
        var comp = GetComponent(module, "LogicalButtonsScript");
        var fldSolved = GetField<bool>(comp, "isSolved");
        var fldStage = GetIntField(comp, "stage");
        var fldButtons = GetField<Array>(comp, "buttons");
        var fldGateOperator = GetField<object>(comp, "gateOperator");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var curStage = 0;
        var colors = new string[3][];
        var labels = new string[3][];
        var initialOperators = new string[3];

        FieldInfo<string> fldLabel = null;
        FieldInfo<object> fldColor = null;
        FieldInfo<int> fldIndex = null;
        MethodInfo<string> mthGetName = null;

        while (!fldSolved.Get())
        {
            var buttons = fldButtons.Get(ar => ar.Length != 3 ? "expected length 3" : null);
            var infs = buttons.Cast<object>().Select(obj =>
            {
                fldLabel ??= GetField<string>(obj, "<Label>k__BackingField");
                fldColor ??= GetField<object>(obj, "<Color>k__BackingField");
                fldIndex ??= GetIntField(obj, "<Index>k__BackingField");
                return fldLabel == null || fldColor == null || fldIndex == null
                    ? null
                    : new { Label = fldLabel.GetFrom(obj), Color = fldColor.GetFrom(obj), Index = fldIndex.GetFrom(obj) };
            }).ToArray();
            if (infs.Length != 3 || infs.Any(inf => inf == null || inf.Label == null || inf.Color == null) || infs[0].Index != 0 || infs[1].Index != 1 || infs[2].Index != 2)
                throw new AbandonModuleException($"I got an unexpected value ([{infs.Select(inf => inf == null ? "<null>" : inf.ToString()).JoinString(", ")}]).");

            var gateOperator = fldGateOperator.Get();
            if (mthGetName == null)
            {
                var interfaceType = gateOperator.GetType().Assembly.GetType("ILogicalGateOperator") ?? throw new AbandonModuleException("Interface type ILogicalGateOperator not found.");
                var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
                var mths = interfaceType.GetMethods(bindingFlags).Where(m => m.Name == "get_Name" && m.GetParameters().Length == 0 && typeof(string).IsAssignableFrom(m.ReturnType)).Take(2).ToArray();
                if (mths.Length == 0)
                    throw new AbandonModuleException($"Type {interfaceType} does not contain public method {name} with return type string and 0 parameters.");
                if (mths.Length > 1)
                    throw new AbandonModuleException($"Type {interfaceType} contains multiple public methods {name} with return type string and 0 parameters.");
                mthGetName = new MethodInfo<string>(null, mths[0]);
            }

            var clrs = infs.Select(inf => inf.Color.ToString()).ToArray();
            var lbls = infs.Select(inf => inf.Label).ToArray();
            var iOp = mthGetName.InvokeOn(gateOperator);

            var stage = fldStage.Get();
            if (stage != curStage || !clrs.SequenceEqual(colors[stage - 1]) || !lbls.SequenceEqual(labels[stage - 1]) || iOp != initialOperators[stage - 1])
            {
                if (stage != curStage && stage != curStage + 1)
                    throw new AbandonModuleException($"I must have missed a stage (it went from {curStage} to {stage}).");
                if (stage < 1 || stage > 3)
                    throw new AbandonModuleException($"‘stage’ has unexpected value {stage} (expected 1–3).");

                colors[stage - 1] = clrs;
                labels[stage - 1] = lbls;
                initialOperators[stage - 1] = iOp;
                curStage = stage;
            }

            yield return new WaitForSeconds(.1f);
        }

        _modulesSolved.IncSafe(_LogicalButtons);
        if (initialOperators.Any(io => io == null))
            throw new AbandonModuleException($"There is a null initial operator ([{initialOperators.Select(io => io == null ? "<null>" : $"“{io}”").JoinString(", ")}]).");

        var _logicalButtonsButtonNames = new[] { "top", "bottom-left", "bottom-right" };
        addQuestions(module,
            colors.SelectMany((clrs, stage) => clrs.Select((clr, btnIx) => makeQuestion(Question.LogicalButtonsColor, _LogicalButtons, formatArgs: new[] { _logicalButtonsButtonNames[btnIx], ordinal(stage + 1) }, correctAnswers: new[] { clr })))
                .Concat(labels.SelectMany((lbls, stage) => lbls.Select((lbl, btnIx) => makeQuestion(Question.LogicalButtonsLabel, _LogicalButtons, formatArgs: new[] { _logicalButtonsButtonNames[btnIx], ordinal(stage + 1) }, correctAnswers: new[] { lbl }))))
                .Concat(initialOperators.Select((op, stage) => makeQuestion(Question.LogicalButtonsOperator, _LogicalButtons, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { op }))));
    }

    private IEnumerable<object> ProcessLogicGates(KMBombModule module)
    {
        var comp = GetComponent(module, "LogicGates");
        var gates = GetField<IList>(comp, "_gates").Get(lst => lst.Count == 0 ? "empty" : null);
        var btnNext = GetField<KMSelectable>(comp, "ButtonNext", isPublic: true).Get();
        var btnPrevious = GetField<KMSelectable>(comp, "ButtonPrevious", isPublic: true).Get();
        var tmpGateType = GetField<object>(gates[0], "GateType", isPublic: true).Get();
        var fldGateTypeName = GetField<string>(tmpGateType, "Name", isPublic: true);

        var gateTypeNames = gates.Cast<object>().Select(obj => fldGateTypeName.GetFrom(GetField<object>(gates[0], "GateType", isPublic: true).GetFrom(obj)).ToString()).ToArray();
        string duplicate = null;
        bool isDuplicateInvalid = false;
        for (int i = 0; i < gateTypeNames.Length; i++)
            for (int j = i + 1; j < gateTypeNames.Length; j++)
                if (gateTypeNames[i] == gateTypeNames[j])
                {
                    if (duplicate != null)
                        isDuplicateInvalid = true;
                    else
                        duplicate = gateTypeNames[i];
                }

        // Unfortunately Logic Gates has no “isSolved” field, so we need to hook into the module
        var solved = false;
        module.OnPass += delegate { solved = true; return true; };

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_LogicGates);

        btnNext.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btnNext.transform);
            btnNext.AddInteractionPunch(0.2f);
            return false;
        };
        btnPrevious.OnInteract = delegate
        {
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btnPrevious.transform);
            btnPrevious.AddInteractionPunch(0.2f);
            return false;
        };

        var qs = new List<QandA>();
        for (int i = 0; i < gateTypeNames.Length; i++)
            qs.Add(makeQuestion(Question.LogicGatesGates, _LogicGates, formatArgs: new[] { "gate " + (char) ('A' + i) }, correctAnswers: new[] { gateTypeNames[i] }));
        if (!isDuplicateInvalid)
            qs.Add(makeQuestion(Question.LogicGatesGates, _LogicGates, formatArgs: new[] { "the duplicated gate" }, correctAnswers: new[] { duplicate }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessLombaxCubes(KMBombModule module)
    {
        var comp = GetComponent(module, "LombaxCubesScript");
        var fldLetter1 = GetField<TextMesh>(comp, "buttonLetter1", isPublic: true);
        var fldLetter2 = GetField<TextMesh>(comp, "buttonLetter2", isPublic: true);

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_LombaxCubes);

        addQuestions(module,
            makeQuestion(Question.LombaxCubesLetters, _LombaxCubes, formatArgs: new[] { "first" },
                correctAnswers: new[] { fldLetter1.Get().text }),
            makeQuestion(Question.LombaxCubesLetters, _LombaxCubes, formatArgs: new[] { "second" },
                correctAnswers: new[] { fldLetter2.Get().text }));
    }

    private IEnumerable<object> ProcessLondonUnderground(KMBombModule module)
    {
        var comp = GetComponent(module, "londonUndergroundScript");
        var fldStage = GetIntField(comp, "levelsPassed");
        var fldDepartureStation = GetField<string>(comp, "departureStation");
        var fldDestinationStation = GetField<string>(comp, "destinationStation");
        var fldDepartureOptions = GetArrayField<string>(comp, "departureOptions");
        var fldDestinationOptions = GetArrayField<string>(comp, "destinationOptions");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        var mustReevaluate = false;
        module.OnStrike += delegate { mustReevaluate = true; return false; };

        var departures = new List<string>();
        var destinations = new List<string>();
        var extraOptions = new HashSet<string>();
        var lastStage = -1;
        while (!fldSolved.Get())
        {
            var stage = fldStage.Get();
            if (stage != lastStage || mustReevaluate)
            {
                if (mustReevaluate)
                {
                    // The player got a strike and the module reset
                    departures.Clear();
                    destinations.Clear();
                    extraOptions.Clear();
                    mustReevaluate = false;
                }
                departures.Add(fldDepartureStation.Get());
                destinations.Add(fldDestinationStation.Get());

                foreach (var option in fldDepartureOptions.Get())
                    extraOptions.Add(option);
                foreach (var option in fldDestinationOptions.Get())
                    extraOptions.Add(option);
                lastStage = stage;
            }
            yield return null;
        }
        _modulesSolved.IncSafe(_LondonUnderground);
        var primary = departures.Union(destinations).ToArray();
        if (primary.Length < 4)
            primary = primary.Union(extraOptions).ToArray();

        addQuestions(module,
            departures.Select((dep, ix) => makeQuestion(Question.LondonUndergroundStations, _LondonUnderground, formatArgs: new[] { ordinal(ix + 1), "depart from" }, correctAnswers: new[] { dep }, preferredWrongAnswers: primary)).Concat(
            destinations.Select((dest, ix) => makeQuestion(Question.LondonUndergroundStations, _LondonUnderground, formatArgs: new[] { ordinal(ix + 1), "arrive to" }, correctAnswers: new[] { dest }, preferredWrongAnswers: primary))));
    }

    private IEnumerable<object> ProcessLongWords(KMBombModule module)
    {
        var comp = GetComponent(module, "LongWords");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");
        var fldPossibleWords = GetField<List<string>>(comp, "SixLetterWords");
        var word = GetField<string>(comp, "chosenSixLetterWord").Get(str => str.Length != 6 ? $"length is {str.Length} instead of 6" : null);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_LongWords);

        addQuestion(module, Question.LongWordsWord, correctAnswers: new[] { word }, preferredWrongAnswers: fldPossibleWords.Get().ToArray());
    }
}