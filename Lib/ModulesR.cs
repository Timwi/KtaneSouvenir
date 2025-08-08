using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessRaidingTemples(ModuleData module)
    {
        var comp = GetComponent(module, "raidingTemplesScript");
        yield return WaitForSolve;

        var startingCommonPool = GetField<int>(comp, "startingCommonPool");
        var commonPoolText = GetField<TextMesh>(comp, "commonPoolText", isPublic: true).Get();

        commonPoolText.text = "";
        addQuestion(module, Question.RaidingTemplesStartingCommonPool, correctAnswers: new[] { startingCommonPool.Get().ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessRailwayCargoLoading(ModuleData module)
    {
        var comp = GetComponent(module, "TrainLoading");
        yield return WaitForSolve;

        // We need to take a copy of the sprites from the module in order to change the ‘pixelsPerUnit’ and ‘pivot’ properties.
        var allTrainCars = GetField<Array>(comp, "_trainCars").Get();
        var fldCarAppearance = GetField<Sprite>(allTrainCars.GetValue(0), "Appearance", isPublic: true);
        var fldCarFriendlyName = GetField<string>(allTrainCars.GetValue(0), "FriendlyName", isPublic: true);
        var carSpriteDic = allTrainCars.Cast<object>()
            .Select(car => (sprite: fldCarAppearance.GetFrom(car), name: fldCarFriendlyName.GetFrom(car)))
            .ToDictionary(tup => tup.sprite, tup => tup.sprite.TranslateSprite(420, tup.name));
        var allCarSprites = carSpriteDic.Values.ToArray();

        var trainCars = GetField<Array>(comp, "_train")
            .Get(ar => ar.Length != 15 ? "expected length 15" : null)
            .Cast<object>()
            .Select(car => carSpriteDic[fldCarAppearance.GetFrom(car)])
            .ToArray();

        var qs = new List<QandA>();

        // Ask about the correctly connected cars/locomotives
        for (int i = 0; i < 14; i++)    // skip 15 because it’s always the Caboose
            qs.Add(makeQuestion(Question.RailwayCargoLoadingCars, module, formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { trainCars[i] }, preferredWrongAnswers: allCarSprites));

        // Ask about the met or unmet freight table rules
        var freightTableRules = GetField<Array>(comp, "_freightTable").Get(ar => ar.Length != 14 ? "expected length 14" : null);
        var fldTableRuleMet = GetIntField(freightTableRules.GetValue(0), "_metAtStage", isPublic: false);
        var fldTableRuleResource = GetField<object>(freightTableRules.GetValue(0), "_resource", isPublic: false);
        var fldTableRuleResourceName = GetField<string>(fldTableRuleResource.Get(), "DisplayName", isPublic: true);

        var metRules = new List<string>();
        var unmetRules = new List<string>();

        for (int i = 0; i < 14; i++)
        {
            var ruleResource = fldTableRuleResource.GetFrom(freightTableRules.GetValue(i));
            var ruleName = fldTableRuleResourceName.GetFrom(ruleResource) switch
            {
                "Sulfuric Acid" or "Nitric Acid" => "Over 700 industrial gas",
                "Automobiles" => "Over 5 automobiles",
                "Farming Equipment" or "Military Hardware" or "Wings" => "Over 7 large objects",
                "Grain" or "Sand" or "Clay" or "Cement" or "Iron Ore" or "Gold Ore" => "Over 500 loose bulk (excl. coal)",
                "Coal" => "Over 100 coal",
                "Meat" or "Vegetables" or "Fruit" => "Over 150 food",
                "Helium" or "Argon" or "Nitrogen" or "Acetylene" => "Over 700 industrial gas",
                "Kerosene" or "Gasoline" or "Diesel" => "Over 100 liquid fuel",
                "Milk" or "Water" or "Resin" => "Over 600 milk/water/resin",
                "Livestock" => "Over 30 livestock",
                "Mail" => "Over 400 mail",
                "Crude Oil" => "Over 250 crude oil",
                "Sheet Metal" => "Over 100 sheet metal",
                "Lumber" or "Logs" => "Over 150 lumber/75 logs",
                var invalid => throw new AbandonModuleException($"There was an invalid resource found for one of the freight table rules: {invalid}"),
            };
            (fldTableRuleMet.GetFrom(freightTableRules.GetValue(i)) < 15 ? metRules : unmetRules).Add(ruleName);
        }

        if (metRules.Count + unmetRules.Count != 14)
            throw new AbandonModuleException($"The total amount of freight table rules is not 14. Met: {metRules.Count}, unmet: {unmetRules.Count}");

        if (metRules.Count >= 1 && unmetRules.Count >= 3)
            qs.Add(makeQuestion(Question.RailwayCargoLoadingFreightTableRules, module, formatArgs: new[] { "was met" }, correctAnswers: metRules.ToArray(), preferredWrongAnswers: unmetRules.ToArray()));
        if (unmetRules.Count >= 1 && metRules.Count >= 3)
            qs.Add(makeQuestion(Question.RailwayCargoLoadingFreightTableRules, module, formatArgs: new[] { "wasn’t met" }, correctAnswers: unmetRules.ToArray(), preferredWrongAnswers: metRules.ToArray()));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessRainbowArrows(ModuleData module)
    {
        var comp = GetComponent(module, "RainbowArrows");
        yield return WaitForSolve;

        addQuestion(module, Question.RainbowArrowsNumber, correctAnswers: new[] { GetIntField(comp, "displayedDigits").Get(min: 0, max: 99).ToString("00") });
    }

    private IEnumerator<YieldInstruction> ProcessRecoloredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "Recolored_Switches");

        yield return WaitForSolve;

        var colorNames = new Dictionary<char, string>
        {
            { 'R', "red" },
            { 'G', "green" },
            { 'B', "blue" },
            { 'C', "cyan" },
            { 'O', "orange" },
            { 'P', "purple" },
            { 'W', "white" }
        };
        var ledColors = GetField<StringBuilder>(comp, "LEDsColorsString").Get(sb => sb.Length != 10 ? "expected length 10" : Enumerable.Range(0, 10).Any(ix => !colorNames.ContainsKey(sb[ix])) ? $"expected {colorNames.Keys.JoinString()}" : null);
        addQuestions(module, Enumerable.Range(0, 10).Select(ix => makeQuestion(Question.RecoloredSwitchesLedColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { colorNames[ledColors[ix]] })));
    }

    private IEnumerator<YieldInstruction> ProcessRecursivePassword(ModuleData module)
    {
        var comp = GetComponent(module, "RecursivePassword");

        yield return WaitForSolve;

        var wordList = GetArrayField<string>(comp, "WordList").Get(expectedLength: 52);
        var selectedWords = GetArrayField<int>(comp, "SelectedWords").Get(expectedLength: 5, validator: ix => ix < 0 || ix >= wordList.Length ? $"expected range 0-{wordList.Length - 1}" : null).Select(ix => wordList[ix]).ToArray();
        var password = wordList[GetIntField(comp, "Password").Get(min: 0, max: wordList.Length - 1)];

        addQuestions(
            module,
            makeQuestion(Question.RecursivePasswordNonPasswordWords, module, correctAnswers: selectedWords, preferredWrongAnswers: wordList),
            makeQuestion(Question.RecursivePasswordPassword, module, correctAnswers: new[] { password }, preferredWrongAnswers: selectedWords)
        );
    }

    private IEnumerator<YieldInstruction> ProcessRedArrows(ModuleData module)
    {
        var comp = GetComponent(module, "RedArrowsScript");
        yield return WaitForSolve;

        addQuestion(module, Question.RedArrowsStartNumber, correctAnswers: new[] { GetIntField(comp, "start").Get(min: 0, max: 9).ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessRedButtont(ModuleData module)
    {
        var comp = GetComponent(module, "BaseButtonScript");
        yield return WaitForSolve;

        GetField<TextMesh>(comp, "DisplayText", isPublic: true).Get().gameObject.SetActive(false);
        var allWords = GetArrayField<string>(comp, "keyword").Get(expectedLength: 4027, validator: s => s.Length != 6 ? "expected word length 6" : null);
        var word = GetField<string>(comp, "selectkeyword", isPublic: true).Get(s => !allWords.Contains(s) ? "expected valid word" : null);
        addQuestion(module, Question.RedButtontWord, correctAnswers: new[] { word }, allAnswers: allWords);
    }

    private IEnumerator<YieldInstruction> ProcessRedCipher(ModuleData module)
    {
        return processColoredCiphers(module, "redCipher", Question.RedCipherScreen);
    }

    private IEnumerator<YieldInstruction> ProcessRedHerring(ModuleData module)
    {
        var comp = GetComponent(module, "RedHerring");
        yield return WaitForSolve;

        string[] colorNames = { "Green", "Blue", "Purple", "Orange" };
        int firstColor = GetArrayField<int>(comp, "colorIndices").Get(expectedLength: 4).First();
        addQuestion(module, Question.RedHerringFirstFlash, correctAnswers: new[] { colorNames[firstColor] });
    }

    private IEnumerator<YieldInstruction> ProcessReformedRoleReversal(ModuleData module)
    {
        var comp = GetComponent(module, "ReformedRoleReversal");
        var init = GetField<object>(comp, "Init").Get();
        var handleManual = GetField<object>(init, "Manual").Get();
        var fldIndex = GetArrayField<int>(handleManual, "SouvenirIndex");
        var fldWires = GetArrayField<int>(handleManual, "SouvenirWires");

        yield return WaitForSolve;

        var index = fldIndex.Get(expectedLength: 2);
        var wires = fldWires.Get(minLength: 3, maxLength: 9, validator: i => i < 0 || i > 9 ? "expected value 0–9" : null);

        var colors = new[] { "Navy", "Lapis", "Blue", "Sky", "Teal", "Plum", "Violet", "Purple", "Magenta", "Lavender" };
        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.ReformedRoleReversalCondition, module, correctAnswers: new[] { Ordinal(index[1] + 1) }));
        for (var ix = 0; ix < wires.Length; ix++)
            qs.Add(makeQuestion(Question.ReformedRoleReversalWire, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { colors[wires[ix]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessReGretBFiltering(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "regretScript");
        var operators = GetArrayField<int>(comp, "operators").Get(expectedLength: 3, validator: v => v is > 5 or < 0 ? "Out of range [0, 5]" : null);

        addQuestions(module, operators.Select((op, i) =>
            makeQuestion(Question.ReGretBFilteringOperator, module,
                correctAnswers: new[] { Question.ReGretBFilteringOperator.GetAnswers()[op] },
                formatArgs: new[] { Ordinal(i + 1) })));
    }

    private IEnumerator<YieldInstruction> ProcessRegularCrazyTalk(ModuleData module)
    {
        var comp = GetComponent(module, "RegularCrazyTalkModule");
        yield return WaitForSolve;

        var phrases = GetField<IList>(comp, "_phraseActions").Get();
        var selected = GetField<int>(comp, "_selectedPhraseIx").Get();

        var selectedPhrase = phrases[selected];
        var phraseText = GetField<string>(selectedPhrase, "Phrase", isPublic: true).Get(v => string.IsNullOrEmpty(v) ? "‘Phrase’ is empty" : null);
        var displayDigit = GetField<int>(selectedPhrase, "ExpectedDigit", isPublic: true).Get();

        string modifier = "[PHRASE]";

        if (phraseText.Length >= 10 && phraseText.Substring(0, 10) == "It says: “") modifier = "It says: “[PHRASE]”";
        else if (phraseText.Length >= 9 && phraseText.Substring(0, 9) == "“It says:") modifier = "“It says: [PHRASE]”";
        else if (phraseText.Length >= 8 && phraseText.Substring(0, 8) == "It says:") modifier = "It says: [PHRASE]";
        else if (phraseText.Length >= 6 && phraseText.Substring(0, 6) == "Quote:") modifier = "Quote: [PHRASE] End quote";
        else if (phraseText.Substring(0, 1) == "“") modifier = "“[PHRASE]”";

        addQuestions(module,
            makeQuestion(Question.RegularCrazyTalkDigit, module, correctAnswers: new[] { displayDigit.ToString() }),
            makeQuestion(Question.RegularCrazyTalkModifier, module, correctAnswers: new[] { modifier }));
    }

    private IEnumerator<YieldInstruction> ProcessReorderedKeys(ModuleData module)
    {
        var comp = GetComponent(module, "ReorderedKeysScript");
        var stages = new int[2][][];
        var pivots = new int[2] { -1, -1 };
        var fldStage = GetIntField(comp, "stage");
        var fldResets = GetIntField(comp, "resetCount");
        var info = GetArrayField<int[]>(comp, "info").Get(expectedLength: 6, validator: a => a.Length != 4 ? "expected inner array length of 4" : null);
        var fldPivot = GetIntField(comp, "pivot");
        var fldSolved = GetField<bool>(comp, "moduleSolved"); // The module also adds a reset when solving which must be ignored
        void getInfo()
        {
            var stage = fldStage.Get(min: 1, max: 2);
            stages[stage - 1] = info.Select(a => a.ToArray()).ToArray();
            pivots[stage - 1] = fldPivot.Get(min: 0, max: 5);
        }
        getInfo();
        var resets = fldResets.Get(min: 0);

        while (module.Unsolved)
        {
            var newReset = fldResets.Get(min: resets);
            if (newReset != resets && !fldSolved.Get())
            {
                if (newReset != resets + 1)
                    throw new AbandonModuleException($"I missed something (I noticed at reset {newReset})");
                resets = newReset;
                getInfo();
            }
            yield return null;
        }

        if (stages.Any(s => s is null) || pivots.Any(p => p is -1))
            throw new AbandonModuleException($"I missed a stage: ({stages.Stringify()}), ({pivots.Stringify()})");

        var colors = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };
        addQuestions(module, stages.SelectMany((stage, stageIx) => stage.SelectMany((key, keyIx) => Ut.NewArray(
                makeQuestion(Question.ReorderedKeysKeyColor, module, OrderedKeysSprites[keyIx], formatArgs: new[] { Ordinal(stageIx + 1) }, correctAnswers: new[] { colors[key[0]] }),
                makeQuestion(Question.ReorderedKeysLabelColor, module, OrderedKeysSprites[keyIx], formatArgs: new[] { Ordinal(stageIx + 1) }, correctAnswers: new[] { colors[key[2]] }),
                makeQuestion(Question.ReorderedKeysLabel, module, OrderedKeysSprites[keyIx], formatArgs: new[] { Ordinal(stageIx + 1) }, correctAnswers: new[] { (key[1] + 1).ToString() })))
            .Concat(new[] { makeQuestion(Question.ReorderedKeysPivot, module, formatArgs: new[] { Ordinal(stageIx + 1) }, correctAnswers: new[] { OrderedKeysSprites[pivots[stageIx]] }) })));
    }

    private IEnumerator<YieldInstruction> ProcessRetirement(ModuleData module)
    {
        var comp = GetComponent(module, "retirementScript");
        yield return WaitForSolve;

        string[] homes = GetArrayField<string>(comp, "retirementHomeOptions", isPublic: true).Get();
        string[] available = GetArrayField<string>(comp, "selectedHomes").Get();
        string correct = GetField<string>(comp, "correctHome").Get(str => str == "" ? "empty" : null);
        addQuestion(module, Question.RetirementHouses, correctAnswers: available.Where(x => x != correct).ToArray(), preferredWrongAnswers: homes);
    }

    private IEnumerator<YieldInstruction> ProcessReverseMorse(ModuleData module)
    {
        var comp = GetComponent(module, "reverseMorseScript");
        var message1 = GetListField<string>(comp, "selectedLetters1", isPublic: true).Get(expectedLength: 6);
        var message2 = GetListField<string>(comp, "selectedLetters2", isPublic: true).Get(expectedLength: 6);

        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (int i = 0; i < 6; i++)
        {
            qs.Add(makeQuestion(Question.ReverseMorseCharacters, module, formatArgs: new[] { Ordinal(i + 1), "first" }, correctAnswers: new[] { message1[i] }, preferredWrongAnswers: message1.ToArray()));
            qs.Add(makeQuestion(Question.ReverseMorseCharacters, module, formatArgs: new[] { Ordinal(i + 1), "second" }, correctAnswers: new[] { message2[i] }, preferredWrongAnswers: message2.ToArray()));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessReversePolishNotation(ModuleData module)
    {
        var comp = GetComponent(module, "ReversePolishNotation");
        yield return WaitForSolve;

        var usedChars = GetArrayField<string[]>(comp, "usedChars")
            .Get(expectedLength: 3, validator: x => x.Any(character => !Regex.IsMatch(character, @"^[0-9A-G]$")) ? "expected character to be in the range of 0-9 or A-G" : null);

        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
        {
            if (usedChars[i].Length != i + 3)
                throw new AbandonModuleException($"‘usedChars[{i}]’ is of an unexpected length (expected {i + 3}): [{string.Join(", ", usedChars[i])}]");
            qs.Add(makeQuestion(Question.ReversePolishNotationCharacter, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: usedChars[i]));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessRGBMaze(ModuleData module)
    {
        var comp = GetComponent(module, "RGBMazeScript");
        yield return WaitForSolve;

        var keyPos = GetArrayField<int[]>(comp, "keylocations").Get(expectedLength: 3, validator: key => key.Length != 2 ? "expected length 2" : key.Any(number => number < 0 || number > 7) ? "expected range 0–7" : null);
        var mazeNum = GetArrayField<int[]>(comp, "mazenumber").Get(expectedLength: 3, validator: maze => maze.Length != 2 ? "expected length 2" : maze[0] < 0 || maze[0] > 9 ? "expected maze[0] in range 0–9" : null);
        var exitPos = GetArrayField<int>(comp, "exitlocation").Get(expectedLength: 3);

        if (exitPos[1] < 0 || exitPos[1] > 7 || exitPos[2] < 0 || exitPos[2] > 7)
            throw new AbandonModuleException($"‘exitPos’ contains invalid coordinate: ({exitPos[2]},{exitPos[1]})");

        string[] colors = { "red", "green", "blue" };

        var qs = new List<QandA>();

        for (int index = 0; index < 3; index++)
        {
            qs.Add(makeQuestion(Question.RGBMazeKeys, module,
                formatArgs: new[] { colors[index] },
                correctAnswers: new[] { "ABCDEFGH"[keyPos[index][1]] + (keyPos[index][0] + 1).ToString() }));
            qs.Add(makeQuestion(Question.RGBMazeNumber, module,
                formatArgs: new[] { colors[index] },
                correctAnswers: new[] { mazeNum[index][0].ToString() }));
        }

        qs.Add(makeQuestion(Question.RGBMazeExit, module,
            correctAnswers: new[] { "ABCDEFGH"[exitPos[2]] + (exitPos[1] + 1).ToString() }));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessRGBSequences(ModuleData module)
    {
        var comp = GetComponent(module, "RGBSequences");
        yield return WaitForSolve;

        var colorDic = new Dictionary<char, string> { ['R'] = "Red", ['G'] = "Green", ['B'] = "Blue", ['C'] = "Cyan", ['M'] = "Magenta", ['Y'] = "Yellow", ['W'] = "White" };
        var displayStr = GetField<string>(comp, "StringFour").Get(val => val.Length != 10 ? "expected length of 10" : val.Any(ch => !colorDic.ContainsKey(ch)) ? $"expected characters {colorDic.Keys.JoinString()}" : null);

        addQuestions(module, Enumerable.Range(0, 10).Select(i =>
            makeQuestion(Question.RGBSequencesDisplay, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { colorDic[displayStr[i]] })));
    }

    private IEnumerator<YieldInstruction> ProcessRhythms(ModuleData module)
    {
        var comp = GetComponent(module, "Rhythms");
        yield return WaitForSolve;

        var color = GetIntField(comp, "lightColor").Get(min: 0, max: 3);
        addQuestion(module, Question.RhythmsColor, correctAnswers: new[] { new[] { "Blue", "Red", "Green", "Yellow" }[color] });
    }

    private IEnumerator<YieldInstruction> ProcessRNGCrystal(ModuleData module)
    {
        var comp = GetComponent(module, "RngCrystalScript");
        yield return WaitForSolve;

        var style = GetField<object>(comp, "_style").Get(v => (int) v is < 1 or > 3 ? $"Unexpected solve style {v}" : null);
        if ((int) style != 2)
        {
            legitimatelyNoQuestion(module.Module, $"The module was solved via luck or the autosolver. ({style})");
            yield break;
        }

        var taps = GetField<int>(comp, "_taps").Get();
        var degree = GetStaticMethod<int>(comp.GetType(), "LfsrPolynomialDegree", 1).Invoke(taps);
        if (degree is < 17 or > 23)
            throw new AbandonModuleException($"Bad register size {degree}");

        var allPossible = Enumerable.Range(0, degree).ToArray();
        var answers = allPossible.Where(i => ((1 << i) & taps) != 0).ToArray();

        addQuestion(module, Question.RNGCrystalTaps, allAnswers: allPossible.Select(i => i.ToString()).ToArray(), correctAnswers: answers.Select(i => i.ToString()).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessRoboScanner(ModuleData module)
    {
        var comp = GetComponent(module, "RoboScannerScript");
        yield return WaitForSolve;

        var emptyCell = GetIntField(comp, "emptyCell").Get(min: 0, max: 24);
        var sol = "ABCDE"[emptyCell % 5].ToString() + "12345"[emptyCell / 5].ToString();
        addQuestion(module, Question.RoboScannerEmptyCell, correctAnswers: new[] { sol });
    }

    private IEnumerator<YieldInstruction> ProcessRobotProgramming(ModuleData module)
    {
        var comp = GetComponent(module, "robotProgramming");
        yield return WaitForSolve;

        var robotsArr = GetArrayField<object>(comp, "robots").Get(expectedLength: 4);
        var fldColor = GetField<Enum>(robotsArr[0], "Color", isPublic: true);
        var fldShape = GetField<Enum>(robotsArr[0], "Shape", isPublic: true);

        var qs = new List<QandA>();

        for (int i = 0; i < 4; i++)
        {
            var robot = robotsArr[i];
            var color = fldColor.GetFrom(robot).ToString();
            var shape = fldShape.GetFrom(robot).ToString();

            qs.Add(makeQuestion(Question.RobotProgrammingColor, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { color }));
            qs.Add(makeQuestion(Question.RobotProgrammingShape, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { shape }));
        }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessRoger(ModuleData module)
    {
        var comp = GetComponent(module, "rogerScript");
        yield return WaitForSolve;

        var seededAnswer = GetField<int>(comp, "seed").Get().ToString().PadLeft(4, '0');
        addQuestion(module, Question.RogerSeed, correctAnswers: new[] { seededAnswer });
    }

    private IEnumerator<YieldInstruction> ProcessRoleReversal(ModuleData module)
    {
        var comp = GetComponent(module, "roleReversal");
        yield return WaitForSolve;

        var redWires = GetListField<byte>(comp, "redWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);
        var orangeWires = GetListField<byte>(comp, "orangeWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);
        var yellowWires = GetListField<byte>(comp, "yellowWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);
        var greenWires = GetListField<byte>(comp, "greenWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);
        var blueWires = GetListField<byte>(comp, "blueWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);
        var purpleWires = GetListField<byte>(comp, "purpleWires").Get(lst => lst.Count > 7 ? "expected 7 or fewer elements" : null);

        var totalWires = redWires.Count + orangeWires.Count + yellowWires.Count + greenWires.Count + blueWires.Count + purpleWires.Count;
        if (totalWires < 2 || totalWires > 7)
            throw new AbandonModuleException($"All wires combined has unexpected value (expected 2-7): {totalWires}");

        var answerIndex = GetField<byte>(comp, "souvenir").Get(b => b < 2 || b > 8 ? "expected range 2–8" : null);
        addQuestions(module,
            makeQuestion(Question.RoleReversalWires, module, formatArgs: new[] { "warm-colored" }, correctAnswers: new[] { (redWires.Count + orangeWires.Count + yellowWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalWires, module, formatArgs: new[] { "cold-colored" }, correctAnswers: new[] { (greenWires.Count + blueWires.Count + purpleWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalWires, module, formatArgs: new[] { "primary-colored" }, correctAnswers: new[] { (redWires.Count + yellowWires.Count + blueWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalWires, module, formatArgs: new[] { "secondary-colored" }, correctAnswers: new[] { (orangeWires.Count + greenWires.Count + purpleWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalNumber, module, correctAnswers: new[] { answerIndex.ToString() }, preferredWrongAnswers: new[] { "2", "3", "4", "5", "6", "7", "8" }));
    }

    private readonly List<(List<int> blue, List<int> red)> _RPSJudgingDisplays = new();
    private IEnumerator<YieldInstruction> ProcessRPSJudging(ModuleData module)
    {
        var comp = GetComponent(module, "RPSJudgingScript");
        const string moduleId = "RPSJudging";

        while (!_noUnignoredModulesLeft)
            yield return null;

        var leftDisplays = GetListField<int>(comp, "LeftDisplays").Get(minLength: 0, validator: v => v is < 0 or > 100 ? "Expected range [0, 101]" : null);
        var rightDisplays = GetListField<int>(comp, "RightDisplays").Get(expectedLength: leftDisplays.Count, validator: v => v is < 0 or > 100 ? "Expected range [0, 101]" : null);

        _RPSJudgingDisplays.Add((leftDisplays, rightDisplays));
        if (_RPSJudgingDisplays[0].blue.Count != leftDisplays.Count)
            throw new AbandonModuleException("There were inconsistent stage counts among modules.");
        if (leftDisplays.Count == 0)
        {
            legitimatelyNoQuestion(module, "There were no stages.");
            yield break;
        }

        yield return null;

        var myIgnoredList = GetField<string[]>(comp, "IgnoredModules").Get();
        var displayedStageCount = Bomb.GetSolvedModuleNames().Count(x => !myIgnoredList.Contains(x));

        if (_RPSJudgingDisplays.Count != _moduleCounts[moduleId])
            throw new AbandonModuleException("The number of displays did not match the number of RPS Judging modules.");

        var leftSprites = GetArrayField<Sprite>(comp, "SpriteLeft", true).Get(expectedLength: 101).TranslateSprites(500).ToArray();
        var rightSprites = GetArrayField<Sprite>(comp, "SpriteRight", true).Get(expectedLength: 101).TranslateSprites(500).ToArray();

        if (_moduleCounts[moduleId] == 1)
            addQuestions(module,
                leftDisplays.Select((digit, ix) => makeQuestion(Question.RPSJudgingThrow, moduleId, 1, formatArgs: new[] { "blue", Ordinal(ix + 1) }, correctAnswers: new[] { leftSprites[digit] }, allAnswers: leftSprites))
                .Concat(rightDisplays.Select((digit, ix) => makeQuestion(Question.RPSJudgingThrow, moduleId, 1, formatArgs: new[] { "red", Ordinal(ix + 1) }, correctAnswers: new[] { rightSprites[digit] }, allAnswers: rightSprites))));
        else
        {
            var leftUniqueStages = Enumerable.Range(1, displayedStageCount).Where(stage => _RPSJudgingDisplays.Count(display => display.blue[stage - 1] == leftDisplays[stage - 1]) == 1).Take(2).ToArray();
            var rightUniqueStages = Enumerable.Range(1, displayedStageCount).Where(stage => _RPSJudgingDisplays.Count(display => display.red[stage - 1] == rightDisplays[stage - 1]) == 1).Take(2).ToArray();

            if ((leftUniqueStages.Length == 0 && rightUniqueStages.Length == 0) || displayedStageCount == 1)
                legitimatelyNoQuestion(module, $"There are not enough stages at which this one (#{GetIntField(comp, "moduleId").Get()}) had a unique display.");
            else
            {
                // This could be replaced with a question sprite, but I feel like that'd be too cramped
                var throws = new[] { "dynamite", "tornado", "quicksand", "pit", "chain", "gun", "law", "whip", "sword", "rock", "death", "wall", "sun", "camera", "fire", "chainsaw", "school", "scissors", "poison", "cage", "axe", "peace", "computer", "castle", "snake", "blood", "porcupine", "vulture", "monkey", "king", "queen", "prince", "princess", "police", "woman", "baby", "man", "home", "train", "car", "noise", "bicycle", "tree", "turnip", "duck", "wolf", "cat", "bird", "fish", "spider", "cockroach", "brain", "community", "cross", "money", "vampire", "sponge", "church", "butter", "book", "paper", "cloud", "airplane", "moon", "grass", "film", "toilet", "air", "planet", "guitar", "bowl", "cup", "beer", "rain", "water", "tv", "rainbow", "ufo", "alien", "prayer", "mountain", "satan", "dragon", "diamond", "platinum", "gold", "devil", "fence", "video game", "math", "robot", "heart", "electricity", "lightning", "medusa", "power", "laser", "nuke", "sky", "tank", "helicopter" };

                var qs = new List<QandA>();
                for (int stage = 0; stage < displayedStageCount; stage++)
                {
                    var uniqueStage = leftUniqueStages.Concat(rightUniqueStages).FirstOrDefault(s => s != stage + 1);
                    if (uniqueStage != 0)
                    {
                        bool isLeft = leftUniqueStages.Contains(uniqueStage);
                        bool isRight = leftUniqueStages.Contains(uniqueStage);
                        if (isLeft && isRight)
                            isLeft = UnityEngine.Random.Range(0, 2) == 0;

                        var formattedName = string.Format(translateString(Question.RPSJudgingThrow, "the RPS Judging where the {0} team threw {1} in the {2} round"), isLeft ? "blue" : "red", translateString(Question.RPSJudgingThrow, throws[(isLeft ? leftDisplays : rightDisplays)[uniqueStage - 1]]), Ordinal(uniqueStage));
                        qs.Add(makeQuestion(Question.RPSJudgingThrow, moduleId, 0, formattedModuleName: formattedName,
                            formatArgs: new[] { "blue", Ordinal(stage + 1) }, correctAnswers: new[] { leftSprites[leftDisplays[stage]] }, allAnswers: leftSprites));
                        qs.Add(makeQuestion(Question.RPSJudgingThrow, moduleId, 0, formattedModuleName: formattedName,
                            formatArgs: new[] { "red", Ordinal(stage + 1) }, correctAnswers: new[] { rightSprites[rightDisplays[stage]] }, allAnswers: rightSprites));
                    }
                }
                addQuestions(module, qs);
            }
        }
    }

    private IEnumerator<YieldInstruction> ProcessRule(ModuleData module)
    {
        var comp = GetComponent(module, "TheRuleScript");

        yield return WaitForSolve;

        addQuestion(module, Question.RuleNumber, correctAnswers: new[] { GetIntField(comp, "ruleNumber").Get().ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessRuleOfThree(ModuleData module)
    {
        var comp = GetComponent(module, "RuleOfThreeScript");
        yield return WaitForSolve;

        var colorNames = new[] { "red", "yellow", "blue" };
        var qs = new List<QandA>();

        // Coordinates
        var redValues = GetArrayField<int>(comp, "_redValues").Get(expectedLength: 3);
        var yellowValues = GetArrayField<int>(comp, "_yellowValues").Get(expectedLength: 3);
        var blueValues = GetArrayField<int>(comp, "_blueValues").Get(expectedLength: 3);
        var values = new[] { redValues, yellowValues, blueValues };
        for (int color = 0; color < 3; color++)
            for (int coord = 0; coord < 3; coord++)
                qs.Add(makeQuestion(Question.RuleOfThreeCoordinates, module, formatArgs: new[] { "XYZ"[coord].ToString(), colorNames[color] }, correctAnswers: new[] { values[color][coord].ToString() }));

        // Cycles
        var redCoords = GetArrayField<int[]>(comp, "_redCoords").Get(expectedLength: 3, validator: arr => arr.Length != 3 ? "expected length 3" : null);
        var yellowCoords = GetArrayField<int[]>(comp, "_yellowCoords").Get(expectedLength: 3, validator: arr => arr.Length != 3 ? "expected length 3" : null);
        var blueCoords = GetArrayField<int[]>(comp, "_blueCoords").Get(expectedLength: 3, validator: arr => arr.Length != 3 ? "expected length 3" : null);
        var coords = new[] { redCoords, yellowCoords, blueCoords };
        for (int color = 0; color < 3; color++)
            for (int axis = 0; axis < 3; axis++)
                for (int cycle = 0; cycle < 3; cycle++)
                    qs.Add(makeQuestion(Question.RuleOfThreeCycles, module, formatArgs: new[] { colorNames[color], "XYZ"[axis].ToString(), Ordinal(cycle + 1) }, correctAnswers: new[] { coords[color][cycle][axis].ToString() }));

        addQuestions(module, qs);
    }
}
