﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;
using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessNameCodes(ModuleData module)
    {
        var comp = GetComponent(module, "NameCodesScript");
        yield return WaitForSolve;

        var leftIx = GetIntField(comp, "leftIndex").Get().ToString();
        var rightIx = GetIntField(comp, "rightIndex").Get().ToString();
        addQuestions(module, new[] {
            makeQuestion(Question.NameCodesIndices, module, formatArgs: new[] { "left" }, correctAnswers: new[] { leftIx }),
            makeQuestion(Question.NameCodesIndices, module, formatArgs: new[] { "right" }, correctAnswers: new[] { rightIx }),
        });
    }

    private IEnumerator<YieldInstruction> ProcessNamingConventions(ModuleData module)
    {
        var comp = GetComponent(module, "NamingConventionsScript");
        yield return WaitForSolve;

        // Set the relevant button to "naming"
        var texts = GetArrayField<int[]>(comp, "_textIndexes").Get(expectedLength: 7);
        texts[0] = new int[] { 11, 0, 10, 8, 11, 6, -1, -1, -1, -1 };

        var type = (int) GetProperty<object>(comp, "DataType").Get(v => (int) v is < 0 or > 9 ? "expected data type 0–9" : null);
        var ans = Ut.Attributes[Question.NamingConventionsObject].AllAnswers[type];
        addQuestion(module, Question.NamingConventionsObject, correctAnswers: new[] { ans });
    }

    private IEnumerator<YieldInstruction> ProcessNandMs(ModuleData module)
    {
        var comp = GetComponent(module, "NandMs");
        yield return WaitForSolve;

        var words = GetArrayField<string>(comp, "otherWords").Get();
        var index = GetIntField(comp, "otherwordindex").Get(min: 0, max: words.Length - 1);
        addQuestion(module, Question.NandMsAnswer, correctAnswers: new[] { words[index] });
    }

    private IEnumerator<YieldInstruction> ProcessNandNs(ModuleData module)
    {
        while (!_isActivated) // No chance of missing the second stage, since the animation takes some time
            yield return new WaitForSeconds(.1f);

        var comp = GetComponent(module, "NandNs");
        var labels = new string[2][];
        var colors = new int[5];

        var fldStage = GetIntField(comp, "stage");
        var fldLabels = GetArrayField<string>(comp, "labels");
        var fldColors = GetArrayField<int>(comp, "buttonColors");
        int stage;

        static string validate(string v) => v.Any(static c => c is not 'N' and not 'M') ? "String must be only N’s and M’s" : null;

        do
        {
            yield return null;
            stage = fldStage.Get(min: 0, max: 5);
            if (stage == 1)
            {
                labels[0] = fldLabels.Get(expectedLength: 5, validator: validate).ToArray();
            }
            else if (stage == 2)
            {
                labels[1] = fldLabels.Get(expectedLength: 5, validator: validate).ToArray();
                colors = fldColors.Get(expectedLength: 5, validator: static v => v is < 0 or > 5 ? "Expected range 0–5" : null).ToArray();
            }
        }
        while (stage < 5);
        yield return WaitForSolve;

        addQuestions(module,
                makeQuestion(Question.NandNsLabel, module, correctAnswers: labels[0], formatArgs: new[] { Ordinal(2) }),
                makeQuestion(Question.NandNsLabel, module, correctAnswers: labels[1], formatArgs: new[] { Ordinal(3) }),
                makeQuestion(Question.NandNsColor, module, correctAnswers: new[] { Question.NandNsColor.GetAnswers()[Enumerable.Range(0, 6).Except(colors).Single()] }));
    }

    private IEnumerator<YieldInstruction> ProcessNavigationDetermination(ModuleData module)
    {
        var comp = GetComponent(module, "NavigationDeterminationScript");
        yield return WaitForSolve;

        var chosenPath = GetField<object>(comp, "_chosenPath").Get();
        var mazeNum = GetIntField(chosenPath, "MazeNum").Get(min: 0, max: 15);

        var maze = GetArrayField<object>(comp, "_mazes").Get(expectedLength: 16).GetValue(mazeNum);
        var color = GetField<int>(maze, "Color").Get();
        var label = GetField<char>(maze, "Label").Get();

        var colors = new string[] { "Red", "Yellow", "Green", "Blue" };

        addQuestions(module,
            makeQuestion(Question.NavigationDeterminationColor, module, correctAnswers: new[] { colors[color] }),
            makeQuestion(Question.NavigationDeterminationLabel, module, correctAnswers: new[] { label.ToString() })
        );
    }

    private IEnumerator<YieldInstruction> ProcessNavinums(ModuleData module)
    {
        var comp = GetComponent(module, "navinumsScript");
        var fldStage = GetIntField(comp, "stage");
        var fldDirections = GetListField<int>(comp, "directions");
        var lookUp = GetArrayField<int[]>(comp, "lookUp").Get(expectedLength: 9, validator: ar => ar.Length != 8 ? "expected length 8" : null);
        var directionsSorted = GetListField<int>(comp, "directionsSorted").Get(expectedLength: 4);
        var centerDigit = GetIntField(comp, "center").Get(min: 1, max: 9);

        var curStage = -1;
        var answers = new int[8];
        while (true)
        {
            yield return null;
            var newStage = fldStage.Get();
            if (newStage != curStage)
            {
                if (newStage == 8)
                    break;
                var newDirections = fldDirections.Get();
                if (newDirections.Count != 4)
                    throw new AbandonModuleException($"‘directions’ has unexpected length {newDirections.Count} (expected 4).");

                answers[newStage] = newDirections.IndexOf(directionsSorted[lookUp[centerDigit - 1][newStage] - 1]);
                if (answers[newStage] == -1)
                    throw new AbandonModuleException($"‘directions’ ({newDirections.JoinString(", ")}) does not contain the value from ‘directionsSorted’ ({directionsSorted[lookUp[centerDigit - 1][newStage] - 1]}).");
                curStage = newStage;
            }
        }

        yield return WaitForSolve;

        var directionNames = new[] { "up", "left", "right", "down" };

        var qs = new List<QandA>();
        for (var stage = 0; stage < 8; stage++)
            qs.Add(makeQuestion(Question.NavinumsDirectionalButtons, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { directionNames[answers[stage]] }));
        qs.Add(makeQuestion(Question.NavinumsMiddleDigit, module, correctAnswers: new[] { centerDigit.ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessNavyButton(ModuleData module)
    {
        var comp = GetComponent(module, "NavyButtonScript");
        var puzzle = GetField<object>(comp, "_puzzle").Get();

        var greekLetters = GetProperty<int[]>(puzzle, "GreekLetterIxs", isPublic: true)
            .Get(validator: arr => arr.Any(v => v < 0 || v >= 48) ? "expected range 0–48" : null)
            .Select(ix => "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩαβγδεζηθικλμνξοπρστυφχψω"[ix].ToString())
            .ToArray();
        var givenIndex = GetProperty<int>(puzzle, "GivenIndex", isPublic: true).Get(validator: v => v < 0 || v >= 16 ? "expected range 0–16" : null);
        var givenValue = GetProperty<int>(puzzle, "GivenValue", isPublic: true).Get(validator: v => v < 0 || v >= 4 ? "expected range 0–4" : null);

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.NavyButtonGreekLetters, module, correctAnswers: greekLetters),
            makeQuestion(Question.NavyButtonGiven, module, formatArgs: new[] { "column" }, correctAnswers: new[] { (givenIndex % 4).ToString() }),
            makeQuestion(Question.NavyButtonGiven, module, formatArgs: new[] { "row" }, correctAnswers: new[] { (givenIndex / 4).ToString() }),
            makeQuestion(Question.NavyButtonGiven, module, formatArgs: new[] { "value" }, correctAnswers: new[] { givenValue.ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessNecronomicon(ModuleData module)
    {
        var comp = GetComponent(module, "necronomiconScript");

        yield return WaitForSolve;

        int[] chapters = GetArrayField<int>(comp, "selectedChapters").Get(expectedLength: 7);
        string[] chaptersString = chapters.Select(x => x.ToString()).ToArray();

        addQuestions(module,
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { chaptersString[0] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { chaptersString[1] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { chaptersString[2] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(4) }, correctAnswers: new[] { chaptersString[3] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(5) }, correctAnswers: new[] { chaptersString[4] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(6) }, correctAnswers: new[] { chaptersString[5] }, preferredWrongAnswers: chaptersString),
            makeQuestion(Question.NecronomiconChapters, module, formatArgs: new[] { Ordinal(7) }, correctAnswers: new[] { chaptersString[6] }, preferredWrongAnswers: chaptersString));
    }

    private IEnumerator<YieldInstruction> ProcessNegativity(ModuleData module)
    {
        var comp = GetComponent(module, "NegativityScript");
        yield return WaitForSolve;

        var convertedNums = GetArrayField<int>(comp, "NumberingConverted").Get();
        var expectedTotal = GetField<int>(comp, "Totale").Get();
        var submittedTernary = GetField<string>(comp, "Tables").Get(str => str.Any(ch => !"+-".Contains(ch)) ? "At least 1 character from the submitted ternary is not familar. (Accepted: '+','-')" : null);

        // Generate possible incorrect answers for this module
        var incorrectValues = new HashSet<int>();
        while (incorrectValues.Count < 5)
        {
            var sumPossible = 0;
            for (var i = 0; i < convertedNums.Length; i++)
            {
                var aValue = convertedNums[i];
                if (Rnd.Range(0, 2) != 0)
                    sumPossible += aValue;
                else
                    sumPossible -= aValue;
            }
            if (sumPossible != expectedTotal)
                incorrectValues.Add(sumPossible);
        }

        var incorrectSubmittedTernary = new HashSet<string>();
        while (incorrectSubmittedTernary.Count < 5)
        {
            var onePossible = "";
            var wantedLength = Rnd.Range(Mathf.Max(2, submittedTernary.Length - 1), Mathf.Min(11, Mathf.Max(submittedTernary.Length + 1, 5)));
            for (var x = 0; x < wantedLength; x++)
                onePossible += "+-".PickRandom();
            if (onePossible != submittedTernary)
                incorrectSubmittedTernary.Add(onePossible);
        }

        addQuestions(module,
            makeQuestion(Question.NegativitySubmittedValue, module, formatArgs: null, correctAnswers: new[] { expectedTotal.ToString() }, preferredWrongAnswers: incorrectValues.Select(a => a.ToString()).ToArray()),
            makeQuestion(Question.NegativitySubmittedTernary, module, formatArgs: null, correctAnswers: new[] { string.IsNullOrEmpty(submittedTernary) ? "(empty)" : submittedTernary }, preferredWrongAnswers: incorrectSubmittedTernary.ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessNeptune(ModuleData module)
    {
        var comp = GetComponent(module, "NeptuneScript");
        yield return WaitForSolve;

        var starNames = new string[] {
            "Bob-omb Battlefield #1", "Bob-omb Battlefield #2", "Bob-omb Battlefield #3", "Bob-omb Battlefield #4", "Bob-omb Battlefield #5", "Bob-omb Battlefield #6", "Bob-omb Battlefield 100 Coins", 
            "Whomp's Fortress #1", "Whomp's Fortress #2", "Whomp's Fortress #3", "Whomp's Fortress #4", "Whomp's Fortress #5", "Whomp's Fortress #6", "Whomp's Fortress 100 Coins", 
            "Jolly Roger Bay #1", "Jolly Roger Bay #2", "Jolly Roger Bay #3", "Jolly Roger Bay #4", "Jolly Roger Bay #5", "Jolly Roger Bay #6", "Jolly Roger Bay 100 Coins", 
            "Cool Cool Mountain #1", "Cool Cool Mountain #2", "Cool Cool Mountain #3", "Cool Cool Mountain #4", "Cool Cool Mountain #5", "Cool Cool Mountain #6", "Cool Cool Mountain 100 Coins", 
            "Big Boo's Haunt #1", "Big Boo's Haunt #2", "Big Boo's Haunt #3", "Big Boo's Haunt #4", "Big Boo's Haunt #5", "Big Boo's Haunt #6", "Big Boo's Haunt 100 Coins", 
            "Hazy Maze Cave #1", "Hazy Maze Cave #2", "Hazy Maze Cave #3", "Hazy Maze Cave #4", "Hazy Maze Cave #5", "Hazy Maze Cave #6", "Hazy Maze Cave 100 Coins", 
            "Lethal Lava Land #1", "Lethal Lava Land #2", "Lethal Lava Land #3", "Lethal Lava Land #4", "Lethal Lava Land #5", "Lethal Lava Land #6", "Lethal Lava Land 100 Coins", 
            "Shifting Sand Land #1", "Shifting Sand Land #2", "Shifting Sand Land #3", "Shifting Sand Land #4", "Shifting Sand Land #5", "Shifting Sand Land #6", "Shifting Sand Land 100 Coins", 
            "Dire Dire Docks #1", "Dire Dire Docks #2", "Dire Dire Docks #3", "Dire Dire Docks #4", "Dire Dire Docks #5", "Dire Dire Docks #6", "Dire Dire Docks 100 Coins", 
            "Snowman's Land #1", "Snowman's Land #2", "Snowman's Land #3", "Snowman's Land #4", "Snowman's Land #5", "Snowman's Land #6", "Snowman's Land 100 Coins", 
            "Wet Dry World #1", "Wet Dry World #2", "Wet Dry World #3", "Wet Dry World #4", "Wet Dry World #5", "Wet Dry World #6", "Wet Dry World 100 Coins", 
            "Tall Tall Mountain #1", "Tall Tall Mountain #2", "Tall Tall Mountain #3", "Tall Tall Mountain #4", "Tall Tall Mountain #5", "Tall Tall Mountain #6", "Tall Tall Mountain 100 Coins", 
            "Tiny Huge Island #1", "Tiny Huge Island #2", "Tiny Huge Island #3", "Tiny Huge Island #4", "Tiny Huge Island #5", "Tiny Huge Island #6", "Tiny Huge Island 100 Coins", 
            "Tick Tock Clock #1", "Tick Tock Clock #2", "Tick Tock Clock #3", "Tick Tock Clock #4", "Tick Tock Clock #5", "Tick Tock Clock #6", "Tick Tock Clock 100 Coins", 
            "Rainbow Ride #1", "Rainbow Ride #2", "Rainbow Ride #3", "Rainbow Ride #4", "Rainbow Ride #5", "Rainbow Ride #6", "Rainbow Ride 100 Coins", 
            "Princess' Secret Slide (Normal)", "Princess' Secret Slide (Fast)", "Secret Aquarium", "Tower of the Wing Cap", "Cavern of the Metal Cap", "Vanish Cap under the Moat", "Wing Mario over the Rainbow",
            "MIPS #1", "MIPS #2", "Toad #1", "Toad #2", "Toad #3", "Bowser in the Dark World", "Bowser in the Fire Sea", "Bowser in the Sky" };

        addQuestion(module, Question.NeptuneStar, correctAnswers: new[] { starNames[GetIntField(comp, "chosenStar").Get()] }, preferredWrongAnswers: starNames);
    }

    private IEnumerator<YieldInstruction> ProcessNeutralization(ModuleData module)
    {
        var comp = GetComponent(module, "neutralization");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var acidType = GetIntField(comp, "acidType").Get(min: 0, max: 3);
        var acidVol = GetIntField(comp, "acidVol").Get(av => av < 5 || av > 20 || av % 5 != 0 ? "unexpected acid volume" : null);

        yield return WaitForSolve;

        var colorText = GetField<GameObject>(comp, "colorText", isPublic: true).Get(nullAllowed: true);
        colorText?.SetActive(false);

        addQuestions(module,
            makeQuestion(Question.NeutralizationColor, module, correctAnswers: new[] { new[] { "Yellow", "Green", "Red", "Blue" }[acidType] }),
            makeQuestion(Question.NeutralizationVolume, module, correctAnswers: new[] { acidVol.ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessNextInLine(ModuleData module)
    {
        var comp = GetComponent(module, "NextInLine");
        var color = GetIntField(comp, "CurrentColor").Get(min: 0, max: 7);

        var hasStruck = false;
        module.Module.OnStrike += () => hasStruck = true;

        yield return WaitForSolve;

        if (hasStruck)
        {
            legitimatelyNoQuestion(module, "No question for Next In Line because the module struck, so the first wire color may be irretrievable.");
            yield break;
        }

        var colors = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Black", "White", "Gray" };
        addQuestion(module, Question.NextInLineFirstWire, correctAnswers: new[] { colors[color] });
    }

    private IEnumerator<YieldInstruction> ProcessNonverbalSimon(ModuleData module)
    {
        var comp = GetComponent(module, "NonverbalSimonHandler");
        yield return WaitForSolve;

        var flashes = GetMethod<List<string>>(comp, "GrabCombinedFlashes", 0, true).Invoke(new object[0]);
        var qs = new List<QandA>(flashes.Count);
        var names = new[] { "Red", "Orange", "Yellow", "Green" };

        for (int stage = 0; stage < flashes.Count; stage++)
        {
            var name = $"{flashes.Count}-{stage + 1}";
            var tex = NonverbalSimonQuestions.First(t => t.name.Equals(name));

            if (_moduleCounts.Get("nonverbalSimon") > 1)
            {
                var num = module.SolveIndex.ToString();
                var tmp = new Texture2D(400, 320, TextureFormat.ARGB32, false);
                tmp.SetPixels(tex.GetPixels());
                tex = NonverbalSimonQuestions.First(t => t.name.Equals("Name"));
                tmp.SetPixels(40, 90, tex.width, tex.height, tex.GetPixels());
                for (var digit = 0; digit < num.Length; digit++)
                {
                    tex = DigitTextures[num[digit] - '0'];
                    tmp.SetPixels(100 + 40 * digit, 90, tex.width, tex.height, tex.GetPixels());
                }

                tmp.Apply(false, true);
                _questionTexturesToDestroyLater.Add(tmp);
                tex = tmp;
            }

            var q = Sprite.Create(tex, Rect.MinMaxRect(0f, 0f, 400f, 320f), new Vector2(.5f, .5f), 1280f, 1u, SpriteMeshType.Tight);
            q.name = $"NVSQ{stage}-{module.SolveIndex}";
            qs.Add(makeSpriteQuestion(q, Question.NonverbalSimonFlashes, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { NonverbalSimonSprites[Array.IndexOf(names, flashes[stage])] }, preferredWrongAnswers: NonverbalSimonSprites));
        }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessNotColoredSquares(ModuleData module)
    {
        var comp = GetComponent(module, "NotColoredSquaresScript");

        yield return WaitForSolve;

        var firstPressedPosition = GetIntField(comp, "_stageOnePress").Get(min: 0, max: 15);
        addQuestion(module, Question.NotColoredSquaresInitialPosition, correctAnswers: new[] { new Coord(4, 4, firstPressedPosition) });
    }

    private IEnumerator<YieldInstruction> ProcessNotColoredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "NotColoredSwitchesScript");
        yield return WaitForSolve;
        var wordList = GetStaticField<string[]>(comp.GetType(), "_wordList").Get().Select(i => i.Substring(0, 1) + i.Substring(1).ToLowerInvariant()).ToArray();
        var solutionWordRaw = GetField<string>(comp, "_chosenWord").Get();
        var solutionWord = solutionWordRaw.Substring(0, 1) + solutionWordRaw.Substring(1).ToLowerInvariant();

        addQuestion(module, Question.NotColoredSwitchesWord, correctAnswers: new[] { solutionWord }, preferredWrongAnswers: wordList);
    }

    private IEnumerator<YieldInstruction> ProcessNotColourFlash(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "NCFScript");
        var seq = GetArrayField<int[]>(comp, "seq").Get(expectedLength: 4, validator: v => v is not { Length: 6 } ? "Expected length 6" : v.Any(i => i is < 0 or > 5) ? "Expected within range [0, 5]" : null);
        addQuestions(module,
            seq[0].Select((c, i) => makeQuestion(Question.NotColourFlashInitialWord, module, correctAnswers: new[] { Question.NotColourFlashInitialWord.GetAnswers()[c] }, formatArgs: new[] { Ordinal(i + 1) }))
            .Concat(seq[1].Select((c, i) => makeQuestion(Question.NotColourFlashInitialColour, module, correctAnswers: new[] { Question.NotColourFlashInitialColour.GetAnswers()[c] }, formatArgs: new[] { Ordinal(i + 1) }))));
    }

    private IEnumerator<YieldInstruction> ProcessNotConnectionCheck(ModuleData module)
    {
        var comp = GetComponent(module, "NCCScript");
        yield return WaitForSolve;
        var qs = new List<QandA>();
        var positions = new[] { "top left", "top right", "bottom left", "bottom right" };

        // Flashes
        var ops = GetArrayField<int>(comp, "ops").Get();
        var puncMarkNames = new[] { "+", "-", ".", ":", "/", "_", "=", "," };
        var puncMarks = Enumerable.Range(0, ops.Length).Select(i => puncMarkNames[ops[i]]).ToArray();
        for (int p = 0; p < 4; p++)
            qs.Add(makeQuestion(Question.NotConnectionCheckFlashes, module, formatArgs: new[] { positions[p] }, correctAnswers: new[] { puncMarks[p] }));

        // Values
        var outputs = GetArrayField<int>(comp, "outputs").Get();
        var vals = Enumerable.Range(0, outputs.Length).Select(i => outputs[i].ToString()).ToArray();
        for (int p = 0; p < 4; p++)
            qs.Add(makeQuestion(Question.NotConnectionCheckValues, module, formatArgs: new[] { positions[p] }, correctAnswers: new[] { vals[p] }, preferredWrongAnswers: Enumerable.Range(1, 9).Select(i => i.ToString()).ToArray()));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessNotCoordinates(ModuleData module)
    {
        var comp = GetComponent(module, "NCooScript");
        yield return WaitForSolve;

        // Step 1: Finding square
        var disp = GetListField<string>(comp, "disp").Get(minLength: 3);
        var seq = GetArrayField<List<int>>(comp, "seq").Get(expectedLength: 2, validator: i => i.Count < 3 ? "expected length at least 3" : null);
        var answers = seq[0].Take(3).Select(coord => disp[seq[1].IndexOf(coord)]).ToArray();

        addQuestion(module, Question.NotCoordinatesSquareCoords, correctAnswers: answers, preferredWrongAnswers: disp.ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessNotDoubleOh(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "NotDoubleOhScript");
        var positions = GetArrayField<int>(comp, "_goalPos").Get(expectedLength: 8, validator: v => v is < 0 or > 63 ? "Out of range 0–63" : null);
        var grid = GetStaticField<string[]>(comp.GetType(), "_grid").Get(arr => arr.Length != 64 ? "Expected 64 elements" : arr.Any(s => !Regex.IsMatch(s, "^[A-H]{2}$")) ? $"Expected all strings to match /^[A-H]{{2}}$/, got: {arr.JoinString(", ")}" : null);
        var displays = positions.Select(p => grid[p]).ToArray();

        var leftSegs = GetArrayField<GameObject>(comp, "LeftSegObjs", true).Get(expectedLength: 7);
        var rightSegs = GetArrayField<GameObject>(comp, "RightSegObjs", true).Get(expectedLength: 7);
        foreach (var seg in leftSegs.Concat(rightSegs))
            seg.SetActive(false);

        addQuestions(module, Enumerable.Range(0, 8).Select(i =>
            makeQuestion(Question.NotDoubleOhPosition, module, correctAnswers: new[] { displays[i] }, formatArgs: new[] { Ordinal(i + 1) })));
    }

    private IEnumerator<YieldInstruction> ProcessNotKeypad(ModuleData module)
    {
        var comp = GetComponent(module, "NotKeypad");
        var connectorComponent = GetComponent(module, "NotVanillaModulesLib.NotKeypadConnector");
        yield return WaitForSolve;

        var strings = Question.NotKeypadColor.GetAnswers();
        var colours = GetField<Array>(comp, "sequenceColours").Get(ar => ar.Cast<int>().Any(v => v <= 0 || v > strings.Length) ? "out of range" : null);
        var buttons = GetArrayField<int>(comp, "sequenceButtons").Get(expectedLength: colours.Length);
        var symbols = GetField<Array>(connectorComponent, "symbols").Get(ar => ar.Cast<int>().Any(v => v < 0 || v > KeypadSprites.Length) ? "out of range" : null);
        var sprites = symbols.Cast<int>().Select(i => KeypadSprites[i]).ToArray();

        var qs = new List<QandA>();
        for (var stage = 0; stage < colours.Length; stage++)
        {
            qs.Add(makeQuestion(Question.NotKeypadColor, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { strings[(int) colours.GetValue(stage) - 1] }));
            qs.Add(makeQuestion(Question.NotKeypadSymbol, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { KeypadSprites[(int) symbols.GetValue(buttons[stage])] }, preferredWrongAnswers: sprites));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessNotMaze(ModuleData module)
    {
        var component = GetComponent(module, "NotMaze");
        yield return WaitForSolve;

        addQuestion(module, Question.NotMazeStartingDistance, correctAnswers: new[] { GetIntField(component, "distance").Get().ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessNotMorseCode(ModuleData module)
    {
        var component = GetComponent(module, "NotMorseCode");
        yield return WaitForSolve;

        var words = GetArrayField<string>(component, "words").Get();
        var channels = GetArrayField<int>(component, "correctChannels").Get();
        var columns = GetStaticField<string[][]>(component.GetType(), "defaultColumns").Get();

        addQuestions(module, Enumerable.Range(0, 5).Select(stage => makeQuestion(
            question: Question.NotMorseCodeWord,
            data: module,
            formatArgs: new[] { Ordinal(stage + 1) },
            correctAnswers: new[] { words[channels[stage]] },
            preferredWrongAnswers: words.Concat(Enumerable.Range(0, 50).Select(_ => columns.PickRandom().PickRandom())).Except(new[] { words[channels[stage]] }).Distinct().Take(8).ToArray())));
    }

    private IEnumerator<YieldInstruction> ProcessNotMorsematics(ModuleData module)
    {
        var comp = GetComponent(module, "NMorScript");
        yield return WaitForSolve;

        var word = GetArrayField<string>(comp, "word").Get(expectedLength: 2);
        var wordList = GetArrayField<string>(comp, "keywords").Get();

        var wordLower = word[0].Substring(0, 1) + word[0].Substring(1).ToLowerInvariant();
        var wordListLower = Enumerable.Range(0, wordList.Length).Select(word => wordList[word].Substring(0, 1) + wordList[word].Substring(1).ToLowerInvariant()).ToArray();

        addQuestion(module, Question.NotMorsematicsWord, correctAnswers: new[] { wordLower }, preferredWrongAnswers: wordListLower);
    }

    private IEnumerator<YieldInstruction> ProcessNotMurder(ModuleData module)
    {
        var comp = GetComponent(module, "NMurScript");

        // whats displayed
        var dispinfo = GetArrayField<List<int>>(comp, "dispinfo").Get(expectedLength: 3).Select(i => i.ToArray()).ToArray();
        yield return WaitForSolve;
        var qs = new List<QandA>();

        // turn number, then suspect, then room/weapon
        var turns = GetListField<List<int[]>>(comp, "turns").Get(expectedLength: 6);
        var suspectNames = new[] { "Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White" };
        var weaponNames = new[] { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner" };
        var roomNames = new[] { "Ballroom", "Billiard Room", "Conservatory", "Dining Room", "Hall", "Kitchen", "Library", "Lounge", "Study" };

        for (int suspect = 0; suspect < 5; suspect++)
        {
            qs.Add(makeQuestion(Question.NotMurderRoom, module, formatArgs: new[] { suspectNames[dispinfo[0][suspect]] }, correctAnswers: new[] { roomNames[turns[0][suspect][0]] }));
            qs.Add(makeQuestion(Question.NotMurderWeapon, module, formatArgs: new[] { suspectNames[dispinfo[0][suspect]] }, correctAnswers: new[] { weaponNames[turns[0][suspect][1]] }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessNotNumberPad(ModuleData module)
    {
        var comp = GetComponent(module, "NotNumberPadScript");
        yield return WaitForSolve;

        var flashes = GetField<IList>(comp, "flashes").Get();
        var mthGetNumbers = GetMethod<int[]>(flashes[0], "GetNumbers", 0, isPublic: true);
        var numbers = Enumerable.Range(0, 3).Select(stage => mthGetNumbers.InvokeOn(flashes[stage]).Select(i => i.ToString()).ToArray()).ToArray();

        var qs = new List<QandA>();
        var numStrs = Enumerable.Range(0, 10).Select(i => i.ToString()).ToArray();
        for (int stage = 0; stage < 3; stage++)
        {
            if (numbers[stage].Length >= 3)
                qs.Add(makeQuestion(Question.NotNumberPadFlashes, module, formatArgs: new[] { "did not flash", Ordinal(stage + 1) }, correctAnswers: numStrs.Except(numbers[stage]).ToArray()));
            qs.Add(makeQuestion(Question.NotNumberPadFlashes, module, formatArgs: new[] { "flashed", Ordinal(stage + 1) }, correctAnswers: numbers[stage]));
        }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessNotPassword(ModuleData module)
    {
        var comp = GetComponent(module, "NotPassword");
        yield return WaitForSolve;

        var connector = GetProperty<object>(comp, "Connector").Get();
        var spinners = GetField<IEnumerable>(connector, "spinners").Get().Cast<object>().ToArray();
        var options = GetListField<char>(spinners[0], "Options", isPublic: true);
        foreach (var spinner in spinners)
            options.SetTo(spinner, null);

        var letter = GetField<char>(comp, "MissingLetter", isPublic: true).Get(c => c is < 'A' or > 'Z' ? $"Bad letter {c}" : null);
        addQuestion(module, Question.NotPasswordLetter, correctAnswers: new[] { letter.ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessNotPerspectivePegs(ModuleData module)
    {
        var comp = GetComponent(module, "NotPerspectivePegsScript");
        yield return WaitForSolve;
        var posNames = new[] { "top", "top-right", "bottom-right", "bottom-left", "top-left" };
        // Peg position
        var positions = GetArrayField<int>(comp, "_flashPegPosition").Get();
        var qs = new List<QandA>();
        for (int i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.NotPerspectivePegsPosition, module, formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { posNames[positions[i]] }));
        // Peg perspective
        var perspectives = GetArrayField<int>(comp, "_flashPegPerspective").Get();
        for (int i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.NotPerspectivePegsPerspective, module, formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { posNames[perspectives[i]] }));
        // Peg color
        var colors = GetArrayField<int>(comp, "_flashPegColor").Get();
        var colorNames = new[] { "blue", "green", "purple", "red", "yellow" };
        for (int i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.NotPerspectivePegsColor, module, formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { colorNames[colors[i]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessNotPianoKeys(ModuleData module)
    {
        var comp = GetComponent(module, "NotPianoKeysScript");
        yield return WaitForSolve;

        var symbols = GetField<Array>(comp, "displayedSymbols").Get(arr => arr.Length == 3 ? null : "expected length 3");
        var propName = GetProperty<char>(symbols.GetValue(0), "symbol", true);

        addQuestions(module,
            makeQuestion(Question.NotPianoKeysFirstSymbol, module, correctAnswers: new[] { propName.GetFrom(symbols.GetValue(0)).ToString() }),
            makeQuestion(Question.NotPianoKeysSecondSymbol, module, correctAnswers: new[] { propName.GetFrom(symbols.GetValue(1)).ToString() }),
            makeQuestion(Question.NotPianoKeysThirdSymbol, module, correctAnswers: new[] { propName.GetFrom(symbols.GetValue(2)).ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessNotRedArrows(ModuleData module)
    {
        var comp = GetComponent(module, "NotRedArrowsScript");
        int startNumber = -1;
        module.Module.OnActivate += () => startNumber = GetField<int>(comp, "currentNumber").Get(v => v is < 10 or > 99 ? "expected 10–99" : null);
        yield return WaitForSolve;

        if (startNumber == -1)
            throw new AbandonModuleException("Failed to capture the starting number.");

        addQuestion(module, Question.NotRedArrowsStart, correctAnswers: new[] { startNumber.ToString("00") });
    }

    private IEnumerator<YieldInstruction> ProcessNotSimaze(ModuleData module)
    {
        var comp = GetComponent(module, "NotSimaze");
        var fldMazeIndex = GetIntField(comp, "mazeIndex");

        var colours = Question.NotSimazeMaze.GetAnswers();
        var startPositionArray = new[] { $"({colours[GetIntField(comp, "x").Get()]}, {colours[GetIntField(comp, "y").Get()]})" };

        yield return WaitForSolve;

        var goalPositionArray = new[] { $"({colours[GetIntField(comp, "goalX").Get()]}, {colours[GetIntField(comp, "goalY").Get()]})" };

        addQuestions(module,
            makeQuestion(Question.NotSimazeMaze, module, correctAnswers: new[] { colours[fldMazeIndex.Get()] }),
            makeQuestion(Question.NotSimazeStart, module, correctAnswers: startPositionArray, preferredWrongAnswers: goalPositionArray),
            makeQuestion(Question.NotSimazeGoal, module, correctAnswers: goalPositionArray, preferredWrongAnswers: startPositionArray));
    }

    private IEnumerator<YieldInstruction> ProcessNotTextField(ModuleData module)
    {
        var comp = GetComponent(module, "NotTextFieldScript");
        bool hasStruck = false;
        module.Module.OnStrike += delegate () { hasStruck = true; return false; };

        var fldSolution = GetArrayField<char>(comp, "solution");
        string[] solution = fldSolution.Get(expectedLength: 3).Select(x => x.ToString()).ToArray();
        var fldBG = GetField<char>(comp, "bgChar");

        while (module.Unsolved)
        {
            if (hasStruck)
            {
                hasStruck = false;
                solution = fldSolution.Get(expectedLength: 3).Select(x => x.ToString()).ToArray();
            }
            yield return new WaitForSeconds(.1f);
        }

        char bgChar = fldBG.Get(ch => ch < 'A' || ch > 'F' ? "expected in range A-F" : null);

        addQuestions(module,
            makeQuestion(Question.NotTextFieldBackgroundLetter, module, correctAnswers: new[] { bgChar.ToString() }),
            makeQuestion(Question.NotTextFieldInitialPresses, module, correctAnswers: solution));
    }

    private IEnumerator<YieldInstruction> ProcessNotTheBulb(ModuleData module)
    {
        var comp = GetComponent(module, "NtBScript");
        yield return WaitForSolve;
        var qs = new List<QandA>();

        // Transmitted word
        var words = GetArrayField<string>(comp, "words").Get();
        var wordList = GetArrayField<string>(comp, "wordlist").Get();
        var targetWord = words[0].Substring(0, 1) + words[0].Substring(1).ToLowerInvariant();
        var wordListLower = Enumerable.Range(0, wordList.Length).Select(word => wordList[word].Substring(0, 1) + wordList[word].Substring(1).ToLowerInvariant()).ToArray();
        qs.Add(makeQuestion(Question.NotTheBulbWord, module, correctAnswers: new[] { targetWord }, preferredWrongAnswers: wordListLower));

        // Bulb color
        var properties = GetArrayField<int>(comp, "properties").Get();
        var colorNames = new[] { "Red", "Green", "Blue", "Yellow", "Purple", "White" };
        var bulbColor = colorNames[properties[0]];
        qs.Add(makeQuestion(Question.NotTheBulbColor, module, correctAnswers: new[] { bulbColor }, preferredWrongAnswers: colorNames));

        // Screw cap material
        var screwCapNames = new[] { "Copper", "Silver", "Gold", "Plastic", "Carbon Fibre", "Ceramic" };
        var screwCap = screwCapNames[properties[1]];
        qs.Add(makeQuestion(Question.NotTheBulbScrewCap, module, correctAnswers: new[] { screwCap }, preferredWrongAnswers: screwCapNames));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessNotTheButton(ModuleData module)
    {
        var comp = GetComponent(module, "NotButton");
        var propLightColour = GetProperty<object>(comp, "LightColour", isPublic: true); // actual type is an enum

        var lightColor = 0;
        while (module.Unsolved)
        {
            lightColor = (int) propLightColour.Get();   // casting boxed enum value to int
            yield return null;  // Don’t wait for .1 seconds so we don’t miss it
        }

        if (lightColor != 0)
        {
            var strings = Question.NotTheButtonLightColor.GetAnswers();
            if (lightColor <= 0 || lightColor > strings.Length)
                throw new AbandonModuleException($"‘LightColour’ is out of range ({lightColor}).");
            addQuestion(module, Question.NotTheButtonLightColor, correctAnswers: new[] { strings[lightColor - 1] });
        }
        else
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Not the Button because the strip didn’t light up (or I missed the light color).");
            _legitimatelyNoQuestions.Add(module.Module);
        }
    }

    private IEnumerator<YieldInstruction> ProcessNotThePlungerButton(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "NotThePlungerButtonScript");
        var colors = GetArrayField<int>(comp, "chosenColors").Get(expectedLength: 3, validator: v => v is < 0 or > 7 ? "Expected range [0, 7]" : null);
        addQuestion(module, Question.NotThePlungerButtonBackground, correctAnswers: colors.Select(i => Question.NotThePlungerButtonBackground.GetAnswers()[i]).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessNotTheScrew(ModuleData module)
    {
        var comp = GetComponent(module, "NotTheScrewModule");
        var position = GetField<int>(comp, "_curPos").Get();

        yield return WaitForSolve;

        addQuestion(module, Question.NotTheScrewInitialPosition, correctAnswers: new[] { new Coord(6, 4, position) });
    }

    private IEnumerator<YieldInstruction> ProcessNotWhosOnFirst(ModuleData module)
    {
        var comp = GetComponent(module, "NotWhosOnFirst");
        var fldPositions = GetArrayField<int>(comp, "rememberedPositions");
        var fldLabels = GetArrayField<string>(comp, "rememberedLabels");
        var fldSum = GetIntField(comp, "stage2Sum");

        yield return WaitForSolve;

        var positions = Question.NotWhosOnFirstPressedPosition.GetAnswers();
        var sumCorrectAnswers = new[] { fldSum.Get().ToString() };

        var qs = new List<QandA>();
        for (var i = 0; i < 4; i++)
        {
            qs.Add(makeQuestion(Question.NotWhosOnFirstPressedPosition, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { positions[fldPositions.Get()[i]] }));
            qs.Add(makeQuestion(Question.NotWhosOnFirstPressedLabel, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { fldLabels.Get()[i] }));
        }
        for (var i = 4; i < 6; i++)
        {
            qs.Add(makeQuestion(Question.NotWhosOnFirstReferencePosition, module, formatArgs: new[] { Ordinal(i - 1) }, correctAnswers: new[] { positions[fldPositions.Get()[i]] }));
            qs.Add(makeQuestion(Question.NotWhosOnFirstReferenceLabel, module, formatArgs: new[] { Ordinal(i - 1) }, correctAnswers: new[] { fldLabels.Get()[i] }));
        }
        qs.Add(makeQuestion(Question.NotWhosOnFirstSum, module, correctAnswers: sumCorrectAnswers));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessNotWordSearch(ModuleData module)
    {
        var comp = GetComponent(module, "NWSScript");
        yield return WaitForSolve;

        string[] missingConsonants = GetArrayField<string>(comp, "missing").Get(expectedLength: 3);
        string[] pressed = GetArrayField<string>(comp, "ans").Get(expectedLength: 12);

        addQuestions(module,
            makeQuestion(Question.NotWordSearchMissing, module, correctAnswers: missingConsonants),
            makeQuestion(Question.NotWordSearchFirstPress, module, correctAnswers: new[] { pressed[0] }));
    }

    private IEnumerator<YieldInstruction> ProcessNotX01(ModuleData module)
    {
        var comp = GetComponent(module, "NX01Script");
        yield return WaitForSolve;

        var nums = GetArrayField<int>(comp, "nums").Get();
        var numsStr = nums.Select(i => i.ToString()).ToArray();
        var numsNotPresent = Enumerable.Range(1, 20).Except(nums).Select(i => i.ToString()).ToArray();

        addQuestions(module,
            makeQuestion(Question.NotX01SectorValues, module, formatArgs: new[] { "was" }, correctAnswers: numsStr, preferredWrongAnswers: numsNotPresent),
            makeQuestion(Question.NotX01SectorValues, module, formatArgs: new[] { "was not" }, correctAnswers: numsNotPresent, preferredWrongAnswers: numsStr));
    }

    private IEnumerator<YieldInstruction> ProcessNotXRay(ModuleData module)
    {
        var comp = GetComponent(module, "NotXRayModule");
        yield return WaitForSolve;

        var table = GetIntField(comp, "_table").Get(0, 7);
        var directions = GetField<Array>(comp, "_directions").Get(validator: arr => arr.Length != 4 ? "expected length 4" : null);
        var allColors = Question.NotXRayScannerColor.GetAnswers();
        var scannerColor = GetField<object>(comp, "_scannerColor").Get(v => v == null ? "did not expected null" : !allColors.Contains(v.ToString()) ? "expected " + allColors.JoinString(", ") : null);

        var qs = new List<QandA>() {
            makeQuestion(Question.NotXRayTable, module, correctAnswers: new[] { (table + 1).ToString() }),
            makeQuestion(Question.NotXRayScannerColor, module, correctAnswers: new[] { scannerColor.ToString() })
        };
        for (var i = 0; i < 4; i++)
        {
            qs.Add(makeQuestion(Question.NotXRayDirections, module, formatArgs: new[] { (i + 1).ToString() }, correctAnswers: new[] { directions.GetValue(i).ToString() }));
            qs.Add(makeQuestion(Question.NotXRayButtons, module, formatArgs: new[] { directions.GetValue(i).ToString().ToLowerInvariant() }, correctAnswers: new[] { (i + 1).ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessNumberedButtons(ModuleData module)
    {
        var comp = GetComponent(module, "NumberedButtonsScript");
        var expectedButtons = GetListField<string>(comp, "ExpectedButtons").Get(list => list.Count == 0 ? "list is empty" : null).ToArray();

        var hadStrike = false;
        module.Module.OnStrike += delegate { hadStrike = true; return false; };

        while (module.Unsolved)
        {
            yield return null;
            if (hadStrike)
            {
                yield return null;
                expectedButtons = GetListField<string>(comp, "ExpectedButtons").Get(list => list.Count == 0 ? "list is empty" : null).ToArray();
            }
        }
        addQuestion(module, Question.NumberedButtonsButtons, correctAnswers: expectedButtons);
    }

    private IEnumerator<YieldInstruction> ProcessNumberGame(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "TheNumberGameScript");
        var num = GetIntField(comp, "RandomMaxNumber").Get(min: 10000000, max: 99999999);

        addQuestion(module, Question.NumberGameMaximum, correctAnswers: new[] { num.ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessNumbers(ModuleData module)
    {
        var comp = GetComponent(module, "WAnumbersScript");
        yield return WaitForSolve;

        var numberValue1 = GetField<int>(comp, "numberValue1").Get();
        var numberValue2 = GetField<int>(comp, "numberValue2").Get();
        var answer = numberValue1.ToString() + numberValue2.ToString();
        addQuestion(module, Question.NumbersTwoDigit, correctAnswers: new[] { answer });
    }

    private IEnumerator<YieldInstruction> ProcessNumpath(ModuleData module)
    {
        var comp = GetComponent(module, "NumpathScript");
        var disp = GetField<TextMesh>(comp, "screen", isPublic: true).Get().text;
        var color = GetIntField(comp, "colorIndex").Get();

        yield return WaitForSolve;

        var colorNames = new[] { "Red", "Green", "Blue", "Yellow", "Purple", "Orange" };
        addQuestions(module,
            makeQuestion(Question.NumpathColor, module, correctAnswers: new[] { colorNames[color] }),
            makeQuestion(Question.NumpathDigit, module, correctAnswers: new[] { disp }));
    }
}
