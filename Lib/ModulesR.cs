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
    private IEnumerable<object> ProcessRaidingTemples(KMBombModule module)
    {
        var comp = GetComponent(module, "raidingTemplesScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_RaidingTemples);

        var startingCommonPool = GetField<int>(comp, "startingCommonPool");
        var commonPoolText = GetField<TextMesh>(comp, "commonPoolText", isPublic: true).Get();

        commonPoolText.text = "";
        addQuestion(module, Question.RaidingTemplesStartingCommonPool, correctAnswers: new[] { startingCommonPool.Get().ToString() });
    }

    private IEnumerable<object> ProcessRailwayCargoLoading(KMBombModule module)
    {
        var comp = GetComponent(module, "TrainLoading");
        var fldCurrentStage = GetIntField(comp, "_currentStage");

        while (fldCurrentStage.Get() < 17)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_RailwayCargoLoading);

        // We need to take a copy of the sprites from the module in order to change the ‘pixelsPerUnit’ and ‘pivot’ properties.
        var allTrainCars = GetField<Array>(comp, "_trainCars").Get();
        var fldCarAppearance = GetField<Sprite>(allTrainCars.GetValue(0), "Appearance", isPublic: true);
        var fldCarFriendlyName = GetField<string>(allTrainCars.GetValue(0), "FriendlyName", isPublic: true);
        var carSpriteDic = allTrainCars.Cast<object>()
            .Select(car => (sprite: fldCarAppearance.GetFrom(car), name: fldCarFriendlyName.GetFrom(car)))
            .ToDictionary(tup => tup.sprite, tup =>
            {
                var newSprite = Sprite.Create(tup.sprite.texture, tup.sprite.rect, new Vector2(0, .5f), 420);
                newSprite.name = tup.name;
                return newSprite;
            });
        var allCarSprites = carSpriteDic.Values.ToArray();

        var trainCars = GetField<Array>(comp, "_train")
            .Get(ar => ar.Length != 15 ? "expected length 15" : null)
            .Cast<object>()
            .Select(car => carSpriteDic[fldCarAppearance.GetFrom(car)])
            .ToArray();

        var qs = new List<QandA>();

        // Ask about the correctly connected cars/locomotives
        for (int i = 0; i < 14; i++)    // skip 15 because it’s always the Caboose
            qs.Add(makeQuestion(Question.RailwayCargoLoadingCars, _RailwayCargoLoading, formatArgs: new[] { ordinal(i + 1) },
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
            qs.Add(makeQuestion(Question.RailwayCargoLoadingFreightTableRules, _RailwayCargoLoading, formatArgs: new[] { "was met" }, correctAnswers: metRules.ToArray(), preferredWrongAnswers: unmetRules.ToArray()));
        if (unmetRules.Count >= 1 && metRules.Count >= 3)
            qs.Add(makeQuestion(Question.RailwayCargoLoadingFreightTableRules, _RailwayCargoLoading, formatArgs: new[] { "wasn’t met" }, correctAnswers: unmetRules.ToArray(), preferredWrongAnswers: metRules.ToArray()));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessRainbowArrows(KMBombModule module)
    {
        var comp = GetComponent(module, "ksmRainbowArrows");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_RainbowArrows);

        addQuestion(module, Question.RainbowArrowsNumber, correctAnswers: new[] { GetIntField(comp, "displayedDigits").Get().ToString() });
    }

    private IEnumerable<object> ProcessRecoloredSwitches(KMBombModule module)
    {
        var comp = GetComponent(module, "Recolored_Switches");

        var isSolved = false;
        module.OnPass += delegate { isSolved = true; return false; };
        while (!isSolved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_RecoloredSwitches);

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
        addQuestions(module, Enumerable.Range(0, 10).Select(ix => makeQuestion(Question.RecoloredSwitchesLedColors, _RecoloredSwitches, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colorNames[ledColors[ix]] })));
    }

    private IEnumerable<object> ProcessRecursivePassword(KMBombModule module)
    {
        var comp = GetComponent(module, "RecursivePassword");

        var solved = false;
        module.OnPass += () => { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_RecursivePassword);

        var wordList = GetArrayField<string>(comp, "WordList").Get(expectedLength: 52);
        var selectedWords = GetArrayField<int>(comp, "SelectedWords").Get(expectedLength: 5, validator: ix => ix < 0 || ix >= wordList.Length ? $"expected range 0-{wordList.Length - 1}" : null).Select(ix => wordList[ix]).ToArray();
        var password = wordList[GetIntField(comp, "Password").Get(min: 0, max: wordList.Length - 1)];

        addQuestions(
            module,
            makeQuestion(Question.RecursivePasswordNonPasswordWords, _RecursivePassword, correctAnswers: selectedWords, preferredWrongAnswers: wordList),
            makeQuestion(Question.RecursivePasswordPassword, _RecursivePassword, correctAnswers: new[] { password }, preferredWrongAnswers: selectedWords)
        );
    }

    private IEnumerable<object> ProcessRedArrows(KMBombModule module)
    {
        var comp = GetComponent(module, "RedArrowsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_RedArrows);

        addQuestion(module, Question.RedArrowsStartNumber, correctAnswers: new[] { GetIntField(comp, "start").Get(min: 0, max: 9).ToString() });
    }

    private IEnumerable<object> ProcessRedCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "redCipher", Question.RedCipherScreen, _RedCipher);
    }

    private IEnumerable<object> ProcessRedHerring(KMBombModule module)
    {
        var comp = GetComponent(module, "RedHerring");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_RedHerring);

        string[] colorNames = { "Green", "Blue", "Purple", "Orange" };
        int firstColor = GetArrayField<int>(comp, "colorIndices").Get(expectedLength: 4).First();
        addQuestion(module, Question.RedHerringFirstFlash, correctAnswers: new[] { colorNames[firstColor] });
    }

    private IEnumerable<object> ProcessReformedRoleReversal(KMBombModule module)
    {
        var comp = GetComponent(module, "ReformedRoleReversal");
        var init = GetField<object>(comp, "Init").Get();
        var handleManual = GetField<object>(init, "Manual").Get();
        var fldSolved = GetField<bool>(init, "Solved");
        var fldIndex = GetArrayField<int>(handleManual, "SouvenirIndex");
        var fldWires = GetArrayField<int>(handleManual, "SouvenirWires");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ReformedRoleReversal);

        var index = fldIndex.Get(expectedLength: 2);
        var wires = fldWires.Get(minLength: 3, maxLength: 9, validator: i => i < 0 || i > 9 ? "expected value 0–9" : null);

        var colors = new[] { "Navy", "Lapis", "Blue", "Sky", "Teal", "Plum", "Violet", "Purple", "Magenta", "Lavender" };
        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.ReformedRoleReversalCondition, _ReformedRoleReversal, correctAnswers: new[] { ordinal(index[1] + 1) }));
        for (var ix = 0; ix < wires.Length; ix++)
            qs.Add(makeQuestion(Question.ReformedRoleReversalWire, _ReformedRoleReversal, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colors[wires[ix]] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessRegularCrazyTalk(KMBombModule module)
    {
        var comp = GetComponent(module, "RegularCrazyTalkModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_RegularCrazyTalk);

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
            makeQuestion(Question.RegularCrazyTalkDigit, _RegularCrazyTalk, correctAnswers: new[] { displayDigit.ToString() }),
            makeQuestion(Question.RegularCrazyTalkModifier, _RegularCrazyTalk, correctAnswers: new[] { modifier }));
    }

    private IEnumerable<object> ProcessRetirement(KMBombModule module)
    {
        var comp = GetComponent(module, "retirementScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Retirement);

        string[] homes = GetArrayField<string>(comp, "retirementHomeOptions", isPublic: true).Get();
        string[] available = GetArrayField<string>(comp, "selectedHomes").Get();
        string correct = GetField<string>(comp, "correctHome").Get(str => str == "" ? "empty" : null);
        addQuestion(module, Question.RetirementHouses, correctAnswers: available.Where(x => x != correct).ToArray(), preferredWrongAnswers: homes);
    }

    private IEnumerable<object> ProcessReverseMorse(KMBombModule module)
    {
        var comp = GetComponent(module, "reverseMorseScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var message1 = GetListField<string>(comp, "selectedLetters1", isPublic: true).Get(expectedLength: 6);
        var message2 = GetListField<string>(comp, "selectedLetters2", isPublic: true).Get(expectedLength: 6);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ReverseMorse);

        var qs = new List<QandA>();
        for (int i = 0; i < 6; i++)
        {
            qs.Add(makeQuestion(Question.ReverseMorseCharacters, _ReverseMorse, formatArgs: new[] { ordinal(i + 1), "first" }, correctAnswers: new[] { message1[i] }, preferredWrongAnswers: message1.ToArray()));
            qs.Add(makeQuestion(Question.ReverseMorseCharacters, _ReverseMorse, formatArgs: new[] { ordinal(i + 1), "second" }, correctAnswers: new[] { message2[i] }, preferredWrongAnswers: message2.ToArray()));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessReversePolishNotation(KMBombModule module)
    {
        var comp = GetComponent(module, "ReversePolishNotation");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ReversePolishNotation);

        var usedChars = GetArrayField<string[]>(comp, "usedChars")
            .Get(expectedLength: 3, validator: x => x.Any(character => !Regex.IsMatch(character, @"^[0-9A-G]$")) ? "expected character to be in the range of 0-9 or A-G" : null);

        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
        {
            if (usedChars[i].Length != i + 3)
                throw new AbandonModuleException($"‘usedChars[{i}]’ is of an unexpected length (expected {i + 3}): [{string.Join(", ", usedChars[i])}]");
            qs.Add(makeQuestion(Question.ReversePolishNotationCharacter, _ReversePolishNotation, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: usedChars[i]));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessRGBMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "RGBMazeScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_RGBMaze);

        var keyPos = GetArrayField<int[]>(comp, "keylocations").Get(expectedLength: 3, validator: key => key.Length != 2 ? "expected length 2" : key.Any(number => number < 0 || number > 7) ? "expected range 0–7" : null);
        var mazeNum = GetArrayField<int[]>(comp, "mazenumber").Get(expectedLength: 3, validator: maze => maze.Length != 2 ? "expected length 2" : maze[0] < 0 || maze[0] > 9 ? "expected maze[0] in range 0–9" : null);
        var exitPos = GetArrayField<int>(comp, "exitlocation").Get(expectedLength: 3);

        if (exitPos[1] < 0 || exitPos[1] > 7 || exitPos[2] < 0 || exitPos[2] > 7)
            throw new AbandonModuleException($"‘exitPos’ contains invalid coordinate: ({exitPos[2]},{exitPos[1]})");

        string[] colors = { "red", "green", "blue" };

        var qs = new List<QandA>();

        for (int index = 0; index < 3; index++)
        {
            qs.Add(makeQuestion(Question.RGBMazeKeys, _RGBMaze,
                formatArgs: new[] { colors[index] },
                correctAnswers: new[] { "ABCDEFGH"[keyPos[index][1]] + (keyPos[index][0] + 1).ToString() }));
            qs.Add(makeQuestion(Question.RGBMazeNumber, _RGBMaze,
                formatArgs: new[] { colors[index] },
                correctAnswers: new[] { mazeNum[index][0].ToString() }));
        }

        qs.Add(makeQuestion(Question.RGBMazeExit, _RGBMaze,
            correctAnswers: new[] { "ABCDEFGH"[exitPos[2]] + (exitPos[1] + 1).ToString() }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessRGBSequences(KMBombModule module)
    {
        var comp = GetComponent(module, "RGBSequences");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_RGBSequences);

        var displayStr = GetField<string>(comp, "StringFour").Get(val => val.Length != 10 ? "expected length of 10" : null);

        var colorDic = new Dictionary<char, string>() 
        { ['R'] = "Red", ['G'] = "Green", ['B'] = "Blue", ['C'] = "Cyan", ['M'] = "Magenta", ['Y'] = "Yellow", ['W'] = "White" };

        var qs = new List<QandA>();

        for (int i = 0; i < 10; i++)
        {
            qs.Add(makeQuestion(Question.RGBSequencesDisplay, _RGBSequences, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colorDic[displayStr[i]] }));
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessRhythms(KMBombModule module)
    {
        var comp = GetComponent(module, "Rhythms");
        var fldSolved = GetField<bool>(comp, "isSolved", isPublic: true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Rhythms);

        var color = GetIntField(comp, "lightColor").Get(min: 0, max: 3);
        addQuestion(module, Question.RhythmsColor, correctAnswers: new[] { new[] { "Blue", "Red", "Green", "Yellow" }[color] });
    }

    private IEnumerable<object> ProcessRoboScanner(KMBombModule module)
    {
        var comp = GetComponent(module, "RoboScannerScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_RoboScanner);

        var emptyCell = GetIntField(comp, "emptyCell").Get(min: 0, max: 24);
        var sol = "ABCDE"[emptyCell % 5].ToString() + "12345"[emptyCell / 5].ToString();
        addQuestion(module, Question.RoboScannerEmptyCell, correctAnswers: new[] { sol });
    }

    private IEnumerable<object> ProcessRoger(KMBombModule module)
    {
        var comp = GetComponent(module, "rogerScript");
        var solved = false;
        module.OnPass += delegate { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Roger);

        var seededAnswer = GetField<int>(comp, "seed").Get().ToString().PadLeft(4, '0');
        addQuestions(module, makeQuestion(Question.RogerSeed, _Roger, formatArgs: null, correctAnswers: new[] { seededAnswer }));
    }

    private IEnumerable<object> ProcessRoleReversal(KMBombModule module)
    {
        var comp = GetComponent(module, "roleReversal");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_RoleReversal);

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
            makeQuestion(Question.RoleReversalWires, _RoleReversal, formatArgs: new[] { "warm-colored" }, correctAnswers: new[] { (redWires.Count + orangeWires.Count + yellowWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalWires, _RoleReversal, formatArgs: new[] { "cold-colored" }, correctAnswers: new[] { (greenWires.Count + blueWires.Count + purpleWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalWires, _RoleReversal, formatArgs: new[] { "primary-colored" }, correctAnswers: new[] { (redWires.Count + yellowWires.Count + blueWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalWires, _RoleReversal, formatArgs: new[] { "secondary-colored" }, correctAnswers: new[] { (orangeWires.Count + greenWires.Count + purpleWires.Count).ToString() }),
            makeQuestion(Question.RoleReversalNumber, _RoleReversal, correctAnswers: new[] { answerIndex.ToString() }, preferredWrongAnswers: new[] { "2", "3", "4", "5", "6", "7", "8" }));
    }

    private IEnumerable<object> ProcessRule(KMBombModule module)
    {
        var comp = GetComponent(module, "TheRuleScript");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Rule);

        addQuestion(module, Question.RuleNumber, correctAnswers: new[] { GetIntField(comp, "ruleNumber").Get().ToString() });
    }

    private IEnumerable<object> ProcessRuleOfThree(KMBombModule module)
    {
        var comp = GetComponent(module, "RuleOfThreeScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_RuleOfThree);

        var colorNames = new[] { "red", "yellow", "blue" };
        var qs = new List<QandA>();

        // Coordinates
        var redValues = GetArrayField<int>(comp, "_redValues").Get(expectedLength: 3);
        var yellowValues = GetArrayField<int>(comp, "_yellowValues").Get(expectedLength: 3);
        var blueValues = GetArrayField<int>(comp, "_blueValues").Get(expectedLength: 3);
        var values = new[] { redValues, yellowValues, blueValues };
        for (int color = 0; color < 3; color++)
            for (int coord = 0; coord < 3; coord++)
                qs.Add(makeQuestion(Question.RuleOfThreeCoordinates, _RuleOfThree, formatArgs: new[] { "XYZ"[coord].ToString(), colorNames[color] }, correctAnswers: new[] { values[color][coord].ToString() }));

        // Cycles
        var redCoords = GetArrayField<int[]>(comp, "_redCoords").Get(expectedLength: 3, validator: arr => arr.Length != 3 ? "expected length 3" : null);
        var yellowCoords = GetArrayField<int[]>(comp, "_yellowCoords").Get(expectedLength: 3, validator: arr => arr.Length != 3 ? "expected length 3" : null);
        var blueCoords = GetArrayField<int[]>(comp, "_blueCoords").Get(expectedLength: 3, validator: arr => arr.Length != 3 ? "expected length 3" : null);
        var coords = new[] { redCoords, yellowCoords, blueCoords };
        for (int color = 0; color < 3; color++)
            for (int axis = 0; axis < 3; axis++)
                for (int cycle = 0; cycle < 3; cycle++)
                    qs.Add(makeQuestion(Question.RuleOfThreeCycles, _RuleOfThree, formatArgs: new[] { colorNames[color], "XYZ"[axis].ToString(), ordinal(cycle + 1) }, correctAnswers: new[] { coords[color][cycle][axis].ToString() }));

        addQuestions(module, qs);
    }
}