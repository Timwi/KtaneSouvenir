using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessMadMemory(ModuleData module)
    {
        var comp = GetComponent(module, "MadMemory");

        yield return WaitForSolve;

        var possibleTexts = GetArrayField<string>(comp, "screenTexts", true).Get(expectedLength: 16);
        var displayedLabels = GetArrayField<int>(comp, "screenLabels", true).Get(expectedLength: 4);

        var qs = new List<QandA>();
        for (int stageNum = 0; stageNum < 4; stageNum++)
            qs.Add(makeQuestion(Question.MadMemoryDisplays, module, formatArgs: new[] { ordinal(stageNum + 1) }, correctAnswers: new[] { possibleTexts[displayedLabels[stageNum]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessMafia(ModuleData module)
    {
        var comp = GetComponent(module, "MafiaModule");
        yield return WaitForSolve;

        var godfather = GetField<object>(comp, "_godfather").Get();
        var suspects = GetField<Array>(comp, "_suspects").Get(ar => ar.Length != 8 ? "expected length 8" : null);
        addQuestion(module, Question.MafiaPlayers, correctAnswers: suspects.Cast<object>().Select(obj => obj.ToString()).Except(new[] { godfather.ToString() }).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessMagentaCipher(ModuleData module)
    {
        return processColoredCiphers(module, "magentaCipher", Question.MagentaCipherScreen);
    }

    private IEnumerator<YieldInstruction> ProcessMahjong(ModuleData module)
    {
        var comp = GetComponent(module, "MahjongModule");

        // Capture the player’s matching pairs until the module is solved
        var taken = GetArrayField<bool>(comp, "_taken").Get();
        var currentTaken = taken.ToArray();
        var matchedTiles = new List<int>();

        while (true)
        {
            yield return null;
            if (!currentTaken.SequenceEqual(taken))
            {
                matchedTiles.AddRange(Enumerable.Range(0, taken.Length).Where(ix => currentTaken[ix] != taken[ix]));
                if (taken.All(x => x))
                    break;
                currentTaken = taken.ToArray();
            }
        }
        yield return WaitForSolve;

        // Remove the counting tile, complete with smoke animation
        var countingTile = GetField<MeshRenderer>(comp, "CountingTile", true).Get();
        if (countingTile.gameObject.activeSelf)     // Do it only if another Souvenir module on the same bomb hasn’t already done it
        {
            var smoke = GetField<ParticleSystem>(comp, "Smoke1", true).Get();
            GetField<KMAudio>(comp, "Audio", true).Get().PlaySoundAtTransform("Elimination", countingTile.transform);
            smoke.transform.localPosition = countingTile.transform.localPosition;
            smoke.Play();
            countingTile.gameObject.SetActive(false);
        }

        // Stuff for the “counting tile” question (bottom-left of the module)
        var countingTileName = countingTile.material.mainTexture.name.Replace(" normal", "");
        var countingTileSprite = MahjongSprites.FirstOrDefault(x => x.name == countingTileName) ?? throw new AbandonModuleException($"The sprite for the counting tile ({countingTileName}) doesn’t exist.");

        // Stuff for the “matching tiles” question
        var matchRow1 = GetArrayField<int>(comp, "_matchRow1").Get();
        var matchRow2 = GetArrayField<int>(comp, "_matchRow2").Get();
        var tileSelectables = GetArrayField<KMSelectable>(comp, "Tiles", true).Get();

        var tileSprites = matchRow1.Concat(matchRow2).Select(ix => MahjongSprites[ix]).ToArray();
        var matchedTileSpriteNames = matchedTiles.Select(ix => tileSelectables[ix].GetComponent<MeshRenderer>().material.mainTexture.name.Replace(" normal", "").Replace(" highlighted", "")).ToArray();
        var matchedTileSprites = matchedTileSpriteNames.Select(name => tileSprites.FirstOrDefault(spr => spr.name == name)).ToArray();

        var invalidIx = matchedTileSprites.IndexOf(spr => spr == null);
        if (invalidIx != -1)
            throw new AbandonModuleException($"The sprite for one of the matched tiles ({matchedTileSpriteNames[invalidIx]}) doesn’t exist. matchedTileSpriteNames=[{matchedTileSpriteNames.JoinString(", ")}], matchedTileSprites=[{matchedTileSprites.Select(spr => spr == null ? "<null>" : spr.name).JoinString(", ")}], countingRow=[{GetArrayField<int>(comp, "_countingRow").Get().JoinString(", ")}], matchRow1=[{matchRow1.JoinString(", ")}], matchRow2=[{matchRow2.JoinString(", ")}], tileSprites=[{tileSprites.Select(spr => spr.name).JoinString(", ")}]");

        addQuestions(module,
            makeQuestion(Question.MahjongCountingTile, module, correctAnswers: new[] { countingTileSprite }, preferredWrongAnswers: GetArrayField<int>(comp, "_countingRow").Get().Select(ix => MahjongSprites[ix]).ToArray()),
            makeQuestion(Question.MahjongMatches, module, formatArgs: new[] { "first" }, correctAnswers: matchedTileSprites.Take(2).ToArray(), preferredWrongAnswers: tileSprites),
            makeQuestion(Question.MahjongMatches, module, formatArgs: new[] { "second" }, correctAnswers: matchedTileSprites.Skip(2).Take(2).ToArray(), preferredWrongAnswers: tileSprites));
    }

    private IEnumerator<YieldInstruction> ProcessMainPage(ModuleData module)
    {
        var comp = GetComponent(module, "_mainpagescript");

        // Homestar and the background
        var homestarBackground = GetField<object>(comp, "HSBG", isPublic: true).Get();
        var homestarNum = GetIntField(homestarBackground, "HSnumber").Get(min: 0, max: 26) + 1;
        var backgroundNum = GetIntField(homestarBackground, "BGnumber").Get(min: 0, max: 26) + 1;

        yield return WaitForSolve;

        // The buttons' effects
        var anims = GetArrayField<KMSelectable>(comp, "menuButtons", isPublic: true).Get(expectedLength: 6);
        var animComps = anims.Select(b => b.GetComponent("_mpAnims")).ToArray();
        var animNums = Enumerable.Range(0, 6).Select(ix => GetIntField(animComps[ix], "animNum").Get(min: 0, max: 25) + 1).ToArray();

        // The color that the bubble did not show
        var absentBubbleColor = titleCase(GetField<string>(comp, "colorNotPresent").Get(col => !new[] { "blue", "green", "red", "yellow" }.Contains(col) ? $"unexpected color '{col}'" : null));

        // The bubble's messages
        var bubbleFirstMessage = GetArrayField<string>(comp, "chosenFirstMessages").Get(expectedLength: 5);
        var bubbleMessages = GetField<string[,]>(comp, "messages").Get(arr => arr.GetLength(0) != 5 || arr.GetLength(1) != 3 ? $"expected 5x3 array. Array was {arr.GetLength(0)}x{arr.GetLength(1)}" : null);
        var bubbleIndices = Enumerable.Range(1, 3).Select(ix => GetIntField(comp, $"message{ix}").Get(min: 0, max: ix == 1 ? 4 : 14)).ToArray();
        var bubbleMessageAnswers = new[] { bubbleFirstMessage[bubbleIndices[0]], bubbleMessages[bubbleIndices[1] % 5, bubbleIndices[1] / 5], bubbleMessages[bubbleIndices[2] % 5, bubbleIndices[2] / 5] };

        addQuestions(module,
            new[] { "toons", "games", "characters", "downloads", "store", "email" }.Select((text, ix) => makeQuestion(Question.MainPageButtonEffectOrigin, module, formatArgs: new[] { text }, correctAnswers: new[] { animNums[ix].ToString() })).Concat(
            new[] { makeQuestion(Question.MainPageHomestarBackgroundOrigin, module, formatArgs: new[] { "Homestar" }, correctAnswers: new[] { homestarNum.ToString() }),
            makeQuestion(Question.MainPageHomestarBackgroundOrigin, module, formatArgs: new[] { "the background" }, correctAnswers: new[] { backgroundNum.ToString() }),
            makeQuestion(Question.MainPageBubbleColors, module, correctAnswers: new[] { absentBubbleColor }),
            makeQuestion(Question.MainPageBubbleMessages, module, formatArgs: new[] { "display" }, correctAnswers: bubbleMessageAnswers),
            makeQuestion(Question.MainPageBubbleMessages, module, formatArgs: new[] { "not display" }, correctAnswers: GetAnswers(Question.MainPageBubbleMessages).Except(bubbleMessageAnswers).ToArray())}));
    }

    private IEnumerator<YieldInstruction> ProcessMandMs(ModuleData module)
    {
        var comp = GetComponent(module, "MandMs");
        yield return WaitForSolve;

        var colorNames = new[] { "red", "green", "orange", "blue", "yellow", "brown" };
        var colors = GetArrayField<int>(comp, "buttonColors").Get();
        var labels = GetArrayField<string>(comp, "labels").Get();
        var qs = new List<QandA>();
        for (int i = 0; i < 5; i++)
        {
            qs.Add(makeQuestion(Question.MandMsColors, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colorNames[colors[i]] }));
            qs.Add(makeQuestion(Question.MandMsLabels, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { labels[i] }, preferredWrongAnswers: labels));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessMandNs(ModuleData module)
    {
        var comp = GetComponent(module, "MandNs");
        yield return WaitForSolve;

        var colorNames = new[] { "red", "green", "orange", "blue", "yellow", "brown" };
        var colors = GetArrayField<int>(comp, "buttonColors").Get();
        var labels = GetArrayField<string>(comp, "convertedValues").Get();
        var solution = GetIntField(comp, "solution").Get();
        var qs = new List<QandA>();
        for (int i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.MandNsColors, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colorNames[colors[i]] }));
        qs.Add(makeQuestion(Question.MandNsLabel, module, correctAnswers: new[] { labels[solution] }, preferredWrongAnswers: labels));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessMaritimeFlags(ModuleData module)
    {
        var comp = GetComponent(module, "MaritimeFlagsModule");
        yield return WaitForSolve;

        var bearing = GetIntField(comp, "_bearingOnModule").Get(min: 0, max: 359);
        var callsignObj = GetField<object>(comp, "_callsign").Get();
        var callsign = GetField<string>(callsignObj, "Name", isPublic: true).Get(str => str.Length != 7 ? "expected length 7" : null);

        addQuestions(module,
            makeQuestion(Question.MaritimeFlagsBearing, module, correctAnswers: new[] { bearing.ToString() }),
            makeQuestion(Question.MaritimeFlagsCallsign, module, correctAnswers: new[] { callsign.ToLowerInvariant() }));
    }

    private IEnumerator<YieldInstruction> ProcessMaroonButton(ModuleData module)
    {
        var comp = GetComponent(module, "MaroonButtonScript");
        yield return WaitForSolve;

        var ans = GetField<int>(comp, "solveFlag").Get(v => v is < 0 or > 19 ? $"Bad flag index {v}" : null);
        var solveParent = GetField<Transform>(comp, "SolveParent", isPublic: true).Get();
        for (var i = 0; i < solveParent.childCount; i++)
            solveParent.GetChild(i).gameObject.SetActive(false);
        addQuestion(module, Question.MaroonButtonA, correctAnswers: new[] { MaroonButtonSprites[ans] });
    }

    private IEnumerator<YieldInstruction> ProcessMaroonCipher(ModuleData module)
    {
        return processColoredCiphers(module, "maroonCipher", Question.MaroonCipherScreen);
    }

    private IEnumerator<YieldInstruction> ProcessMashematics(ModuleData module)
    {
        var comp = GetComponent(module, "mashematicsScript");
        yield return WaitForSolve;

        var numberClass = GetField<object>(comp, "number").Get();
        var answer = GetMethod<int>(numberClass, "GetNumberOfRequiredPush", numParameters: 0, isPublic: true).Invoke();
        var number1 = GetField<int>(numberClass, "Number1", isPublic: true).Get();
        var number2 = GetField<int>(numberClass, "Number2", isPublic: true).Get();
        var number3 = GetField<int>(numberClass, "Number3", isPublic: true).Get();

        var questions = new List<QandA> { makeQuestion(Question.MashematicsAnswer, module, correctAnswers: new[] { answer.ToString() }) };
        for (int i = 0; i < 3; i++)
        {
            var number = i == 0 ? number1 : (i == 1 ? number2 : number3);
            questions.Add(makeQuestion(Question.MashematicsCalculation, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { number.ToString() }));
        }
        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessMasterTapes(ModuleData module)
    {
        var comp = GetComponent(module, "MasterTape");

        yield return WaitForSolve;

        var songIndex = GetIntField(comp, "currentSong").Get(min: 1, max: 9) - 1;
        addQuestion(module, Question.MasterTapesPlayedSong, correctAnswers: new[] { GetAnswers(Question.MasterTapesPlayedSong)[songIndex] });
    }

    private IEnumerator<YieldInstruction> ProcessMatchRefereeing(ModuleData module)
    {
        var comp = GetComponent(module, "MeteoRefereeingScript");
        yield return WaitForSolve;

        var planetsArr = GetField<Array>(comp, "planetsUsed").Get(x => x.Length != 3 ? "expected length 3" : null);
        var planetsUsed = new List<Sprite[]>();
        var planetImages = GetArrayField<Sprite>(comp, "planetImages", isPublic: true).Get().Select(sprite => sprite.TranslateSprite(280)).ToArray();

        for (int stage = 0; stage < 3; stage++)
        {
            if (planetsArr.GetValue(stage) is not Array innerArr)
                throw new AbandonModuleException($"Abandoning Match Refereeing because planetsUsed[{stage}] is not an array.");
            else if (innerArr.Length != 2 + stage)
                throw new AbandonModuleException($"Abandoning Match Refereeing because planetsUsed[{stage}] has unexpected length {innerArr.Length}. Expected {2 + stage}.");
            planetsUsed.Add(innerArr.Cast<object>().Select(pl =>
            {
                var intValue = (int) pl;
                if (intValue < 0 || intValue >= planetImages.Length)
                    throw new AbandonModuleException($"Abandoning Match Refereeing because planetsUsed[{stage}] contains value {pl} with integer value {intValue}, which is outside the range for planetImages (0–{planetImages.Length - 1}).");
                return planetImages[intValue];
            }).ToArray());
        }

        var allPlanetsUsed = planetsUsed.SelectMany(x => x).ToArray();

        var qs = new List<QandA>();
        for (int stage = 0; stage < 2; stage++)
            qs.Add(makeQuestion(Question.MatchRefereeingPlanet, module,
                formatArgs: new[] { ordinal(stage + 1) },
                correctAnswers: planetsUsed[stage],
                preferredWrongAnswers: allPlanetsUsed,
                allAnswers: planetImages));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessMathEm(ModuleData module)
    {
        var comp = GetComponent(module, "MathemScript");

        var fldArrangements = GetField<int[][]>(comp, "tarrange");
        var fldProps = GetField<int[,]>(comp, "tprops");
        var fldMats = GetArrayField<Material>(comp, "tpatterns", isPublic: true);

        yield return WaitForSolve;

        int[] initialArrangement = fldArrangements.Get().First();
        int[,] props = fldProps.Get();

        var qs = new List<QandA>();
        string[] colorNames = { "White", "Bronze", "Silver", "Gold" };
        Sprite[] displayedMarkings = Enumerable.Range(0, 16).Select(ix => MathEmSprites[(props[initialArrangement[ix], 0] * 10) + props[initialArrangement[ix], 2]]).ToArray();

        for (int tileIx = 0; tileIx < 16; tileIx++)
        {
            qs.Add(makeQuestion(Question.MathEmColor, module,
                questionSprite: Sprites.GenerateGridSprite(new Coord(4, 4, tileIx)),
                correctAnswers: new[] { colorNames[props[initialArrangement[tileIx], 1]] }));
            qs.Add(makeQuestion(Question.MathEmLabel, module,
                questionSprite: Sprites.GenerateGridSprite(new Coord(4, 4, tileIx)),
                correctAnswers: new[] { displayedMarkings[tileIx] },
                preferredWrongAnswers: displayedMarkings));
        }
        addQuestions(module, qs);

    }

    private IEnumerator<YieldInstruction> ProcessMatrix(ModuleData module)
    {
        var comp = GetComponent(module, "MatrixScript");
        yield return WaitForSolve;

        // “selectedNames” contains the scrambled versions of the names. Find the unscrambled name.
        var unscrambledNames = GetArrayField<string>(comp, "selectedNames").Get()
            .Select(n => GetAnswers(Question.MatrixAccessCode).FirstOrDefault(ac => n.ToLowerInvariant().OrderBy(ch => ch).JoinString() == ac.ToLowerInvariant().OrderBy(ch => ch).JoinString()))
            .ToArray();

        addQuestions(module,
            makeQuestion(Question.MatrixAccessCode, module, correctAnswers: unscrambledNames),
            makeQuestion(Question.MatrixGlitchWord, module, correctAnswers: new[] { GetField<string>(comp, "illegalWordText").Get().ToLowerInvariant() }));
    }

    private IEnumerator<YieldInstruction> ProcessMaze(ModuleData module)
    {
        var comp = GetComponent(module, "InvisibleWallsComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        var currentCell = GetProperty<object>(comp, "CurrentCell", isPublic: true).Get();  // Need to get the current cell at the start.
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        module.SolveIndex = _modulesSolved.IncSafe("Maze");

        addQuestions(module,
            makeQuestion(Question.MazeStartingPosition, module, formatArgs: new[] { "column", "left" }, correctAnswers: new[] { (GetIntField(currentCell, "X", true).Get() + 1).ToString() }),
            makeQuestion(Question.MazeStartingPosition, module, formatArgs: new[] { "row", "top" }, correctAnswers: new[] { (GetIntField(currentCell, "Y", true).Get() + 1).ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessMaze3(ModuleData module)
    {
        var comp = GetComponent(module, "maze3Script");
        var node = GetIntField(comp, "node").Get(min: 0, max: 53);

        yield return WaitForSolve;

        var colors = new[] { "Red", "Blue", "Yellow", "Green", "Magenta", "Orange" };
        addQuestion(module, Question.Maze3StartingFace, correctAnswers: new[] { colors[node / 9] });
    }

    private IEnumerator<YieldInstruction> ProcessMazeIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "MazeIdentificationScript");
        yield return WaitForSolve;

        var seed = GetArrayField<int>(comp, "Quadrants").Get(validator: x => x.Any(y => y >= 4 || y < 0) ? "quadrants out of range" : null);
        var buttonFuncs = GetArrayField<int>(comp, "ButtonFunctions").Get(validator: x => x.Any(y => y >= 4 || y < 0) ? "functions out of range" : null);
        var directions = new[] { "Forwards", "Clockwise", "Backwards", "Counter-clockwise" };
        addQuestions(module,
            makeQuestion(Question.MazeIdentificationSeed, module, correctAnswers: new[] { seed.Select(x => x + 1).JoinString() }),
            makeQuestion(Question.MazeIdentificationNum, module, formatArgs: new[] { "1" }, correctAnswers: new[] { directions[buttonFuncs[0]] }),
            makeQuestion(Question.MazeIdentificationNum, module, formatArgs: new[] { "2" }, correctAnswers: new[] { directions[buttonFuncs[1]] }),
            makeQuestion(Question.MazeIdentificationNum, module, formatArgs: new[] { "3" }, correctAnswers: new[] { directions[buttonFuncs[2]] }),
            makeQuestion(Question.MazeIdentificationNum, module, formatArgs: new[] { "4" }, correctAnswers: new[] { directions[buttonFuncs[3]] }),
            makeQuestion(Question.MazeIdentificationFunc, module, formatArgs: new[] { "moved you forwards" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 0) + 1).ToString() }),
            makeQuestion(Question.MazeIdentificationFunc, module, formatArgs: new[] { "turned you clockwise" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 1) + 1).ToString() }),
            makeQuestion(Question.MazeIdentificationFunc, module, formatArgs: new[] { "moved you backwards" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 2) + 1).ToString() }),
            makeQuestion(Question.MazeIdentificationFunc, module, formatArgs: new[] { "turned you counter-clockwise" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 3) + 1).ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessMazematics(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "Mazematics");
        var startVal = GetIntField(comp, "startValue").Get(17, 49).ToString();
        var goalVal = GetIntField(comp, "goalValue").Get(0, 49).ToString();

        string[] possibleStartVals = Enumerable.Range(17, 33).Select(x => x.ToString()).ToArray();
        string[] possibleGoalVals = Enumerable.Range(0, 50).Select(x => x.ToString()).ToArray();

        addQuestions(module,
            makeQuestion(Question.MazematicsValue, module, formatArgs: new[] { "initial" }, correctAnswers: new[] { startVal }, preferredWrongAnswers: possibleStartVals),
            makeQuestion(Question.MazematicsValue, module, formatArgs: new[] { "goal" }, correctAnswers: new[] { goalVal }, preferredWrongAnswers: possibleGoalVals));
    }

    private IEnumerator<YieldInstruction> ProcessMazeScrambler(ModuleData module)
    {
        var comp = GetComponent(module, "MazeScrambler");

        var ind1X = GetIntField(comp, "IDX1").Get(min: 0, max: 2);
        var ind1Y = GetIntField(comp, "IDY1").Get(min: 0, max: 2);
        var ind2X = GetIntField(comp, "IDX2").Get(min: 0, max: 2);
        var ind2Y = GetIntField(comp, "IDY2").Get(min: 0, max: 2);
        var startX = GetIntField(comp, "StartX").Get(min: 0, max: 2);
        var startY = GetIntField(comp, "StartY").Get(min: 0, max: 2);
        var goalX = GetIntField(comp, "GoalX").Get(min: 0, max: 2);
        var goalY = GetIntField(comp, "GoalY").Get(min: 0, max: 2);

        yield return WaitForSolve;

        var positionNames = new[] { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };

        addQuestions(module,
            makeQuestion(Question.MazeScramblerStart, module, correctAnswers: new[] { positionNames[startY * 3 + startX] }, preferredWrongAnswers: new[] { positionNames[goalY * 3 + goalX] }),
            makeQuestion(Question.MazeScramblerGoal, module, correctAnswers: new[] { positionNames[goalY * 3 + goalX] }, preferredWrongAnswers: new[] { positionNames[startY * 3 + startX] }),
            makeQuestion(Question.MazeScramblerIndicators, module, correctAnswers: new[] { positionNames[ind1Y * 3 + ind1X], positionNames[ind2Y * 3 + ind2X] }, preferredWrongAnswers: positionNames));
    }

    private IEnumerator<YieldInstruction> ProcessMazeseeker(ModuleData module)
    {
        var comp = GetComponent(module, "MazeseekerScript");
        yield return WaitForSolve;

        var nums = GetField<int[,]>(comp, "Grid").Get();
        var startRow = GetField<int>(comp, "StartingRow").Get();
        var startColumn = GetField<int>(comp, "StartingColumn").Get();
        var goalRow = GetField<int>(comp, "GoalRow").Get();
        var goalColumn = GetField<int>(comp, "GoalColumn").Get();

        var qs = new List<QandA>();
        for (int i = 0; i < 36; i++)
            qs.Add(makeQuestion(Question.MazeseekerCell, module, questionSprite: Sprites.GenerateGridSprite(new Coord(6, 6, i)), correctAnswers: new[] { nums[i / 6, i % 6].ToString() }));
        qs.Add(makeQuestion(Question.MazeseekerStart, module, correctAnswers: new[] { new Coord(6, 6, startColumn, startRow) }));
        qs.Add(makeQuestion(Question.MazeseekerGoal, module, correctAnswers: new[] { new Coord(6, 6, goalColumn, goalRow) }));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessMegaMan2(ModuleData module)
    {
        var comp = GetComponent(module, "Megaman2");
        var robotMasters = GetArrayField<string>(comp, "robotMasters").Get();
        var selectedMaster = GetIntField(comp, "selectedMaster").Get(min: 0, max: robotMasters.Length - 1);
        var selectedWeapon = GetIntField(comp, "selectedWeapon").Get(min: 0, max: robotMasters.Length - 1);

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.MegaMan2SelectedMaster, module, correctAnswers: new[] { robotMasters[selectedMaster] }, preferredWrongAnswers: robotMasters),
            makeQuestion(Question.MegaMan2SelectedWeapon, module, correctAnswers: new[] { robotMasters[selectedWeapon] }, preferredWrongAnswers: robotMasters));
    }

    private IEnumerator<YieldInstruction> ProcessMelodySequencer(ModuleData module)
    {
        var comp = GetComponent(module, "MelodySequencerScript");

        var parts = GetArrayField<int[]>(comp, "parts").Get(expectedLength: 8);  // the 8 parts in their “correct” order
        var moduleParts = GetArrayField<int[]>(comp, "moduleParts").Get(expectedLength: 8, nullContentAllowed: true);      // the parts as assigned to the slots
        var partsPerSlot = Enumerable.Range(0, 8).Select(slot => parts.IndexOf(p => ReferenceEquals(p, moduleParts[slot]))).ToArray();
        Debug.Log($"<Souvenir #{_moduleId}> Melody Sequencer: parts are: [{partsPerSlot.JoinString(", ")}].");

        yield return WaitForSolve;

        var qs = new List<QandA>();
        var givenSlots = Enumerable.Range(0, partsPerSlot.Length).Where(slot => partsPerSlot[slot] != -1).Select(slot => (slot + 1).ToString()).ToArray();
        var givenParts = partsPerSlot.Where(part => part != -1).Select(part => (part + 1).ToString()).ToArray();
        for (int i = 0; i < partsPerSlot.Length; i++)
        {
            if (partsPerSlot[i] != -1)
            {
                qs.Add(makeQuestion(Question.MelodySequencerParts, module, formatArgs: new[] { (partsPerSlot[i] + 1).ToString() }, correctAnswers: new[] { (i + 1).ToString() }, preferredWrongAnswers: givenSlots));
                qs.Add(makeQuestion(Question.MelodySequencerSlots, module, formatArgs: new[] { (i + 1).ToString() }, correctAnswers: new[] { (partsPerSlot[i] + 1).ToString() }, preferredWrongAnswers: givenParts));
            }
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessMemorableButtons(ModuleData module)
    {
        var comp = GetComponent(module, "MemorableButtons");
        var buttonLabels = GetArrayField<TextMesh>(comp, "buttonLabels", isPublic: true).Get(ar => ar.Length == 0 ? "empty" : null);

        yield return WaitForSolve;

        var combinedCode = GetField<string>(comp, "combinedCode", isPublic: true).Get(str => str.Length < 10 || str.Length > 15 ? "expected length 10–15" : null);
        addQuestions(module, combinedCode.Select((ch, ix) => makeQuestion(Question.MemorableButtonsSymbols, module, buttonLabels[0].font, buttonLabels[0].GetComponent<MeshRenderer>().sharedMaterial.mainTexture, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { ch.ToString() })));
    }

    private IEnumerator<YieldInstruction> ProcessMemory(ModuleData module)
    {
        var comp = GetComponent(module, "MemoryComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        module.SolveIndex = _modulesSolved.IncSafe("Memory");

        var displaySequence = GetProperty<string>(comp, "DisplaySequence", true).Get();
        var indices = GetListField<int>(comp, "buttonIndicesPressed", false).Get();
        var labels = GetListField<string>(comp, "buttonLabelsPressed", false).Get();
        var qs = new List<QandA>();
        for (var stage = 0; stage < 4; stage++)
        {
            qs.Add(makeQuestion(Question.MemoryDisplay, module, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { displaySequence[stage].ToString() }));
            qs.Add(makeQuestion(Question.MemoryPosition, module, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { MemorySprites[indices[stage]] }, preferredWrongAnswers: MemorySprites));
            qs.Add(makeQuestion(Question.MemoryLabel, module, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { labels[stage][labels[stage].Length - 1].ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessMemoryWires(ModuleData module)
    {
        var comp = GetComponent(module, "MemoryWiresScript");

        var fldStageNumber = GetIntField(comp, "stage");
        var fldDisplayNumber = GetIntField(comp, "dispnum");
        var displayedDigits = new int[5];
        var currentStage = 0;

        module.Module.OnStrike += () => { currentStage = -1; return false; };

        while (!_isActivated)
            yield return null; // Do not wait 0.1 seconds to make sure we get the right number.
        displayedDigits[0] = fldDisplayNumber.Get(min: 1, max: 6);

        while (module.Unsolved)
        {
            var stage = fldStageNumber.Get(min: 0, max: 4);
            if (currentStage != stage)
            {
                displayedDigits[stage] = fldDisplayNumber.Get(min: 1, max: 6);
                currentStage = stage;
            }
            yield return null; // Do not wait 0.1 seconds to make sure we get the right number each time.
        }

        var allColours = GetArrayField<string>(comp, "logcol").Get(expectedLength: 5);
        var wireColours = GetArrayField<int[]>(comp, "colset").Get(expectedLength: 5,
            validator: innerArr => innerArr.Length != 6 ? "expected length 6" : innerArr.Any(i => i < 0 || i > 4) ? "inner array contained value outside expected range 0-4" : null);

        var qs = new List<QandA>();
        for (int pos = 0; pos < 30; pos++)
            qs.Add(makeQuestion(Question.MemoryWiresWireColours, module, formatArgs: new[] { (pos + 1).ToString() }, correctAnswers: new[] { allColours[wireColours[pos / 6][pos % 6]] }));
        for (int stage = 0; stage < 5; stage++)
            qs.Add(makeQuestion(Question.MemoryWiresDisplayedDigits, module, formatArgs: new[] { (stage + 1).ToString() }, correctAnswers: new[] { displayedDigits[stage].ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessMetamorse(ModuleData module)
    {
        var comp = GetComponent(module, "MetamorseScript");
        var fldBigChar = GetField<char>(comp, "greaterLetter");

        yield return WaitForSolve;
        addQuestion(module, Question.MetamorseExtractedLetter, correctAnswers: new[] { fldBigChar.Get().ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessMetapuzzle(ModuleData module)
    {
        var comp = GetComponent(module, "metapuzzleScript");

        var wordsType = comp.GetType().Assembly.GetType("SevenLetterWords") ?? throw new AbandonModuleException("I cannot find the SevenLetterWords type.");
        var words = GetStaticField<string[]>(wordsType, "List", isPublic: true).Get();

        yield return WaitForSolve;

        var answer = GetField<string>(comp, "metaAnswer").Get(x => x.Length != 7 ? "expected length 7" : null);

        addQuestion(module, Question.MetapuzzleAnswer, correctAnswers: new[] { answer }, preferredWrongAnswers: words);
    }

    private IEnumerator<YieldInstruction> ProcessMicrocontroller(ModuleData module)
    {
        var comp = GetComponent(module, "Micro");
        yield return WaitForSolve;

        var ledsOrder = GetListField<int>(comp, "LEDorder").Get(lst => lst.Count != 6 && lst.Count != 8 && lst.Count != 10 ? "unexpected length (expected 6, 8 or 10)" : null);
        var positionTranslate = GetArrayField<int>(comp, "positionTranslate").Get(expectedLength: ledsOrder.Count);

        addQuestions(module, ledsOrder.Select((led, ix) => makeQuestion(Question.MicrocontrollerPinOrder, module,
            formatArgs: new[] { ordinal(ix + 1) },
            correctAnswers: new[] { (positionTranslate[led] + 1).ToString() },
            preferredWrongAnswers: Enumerable.Range(1, ledsOrder.Count).Select(i => i.ToString()).ToArray())));
    }

    private IEnumerator<YieldInstruction> ProcessMinesweeper(ModuleData module)
    {
        var comp = GetComponent(module, "MinesweeperModule");

        // Wait for activation as the above fields aren’t fully initialized until then
        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var color = GetField<string>(GetField<object>(comp, "StartingCell").Get(), "Color", isPublic: true).Get();

        yield return WaitForSolve;
        addQuestion(module, Question.MinesweeperStartingColor, correctAnswers: new[] { color });
    }

    private IEnumerator<YieldInstruction> ProcessMirror(ModuleData module)
    {
        var comp = GetComponent(module, "mirror");
        var fldModuleReady = GetField<bool>(comp, "moduleReady");
        var candidateWords =
            GetStaticField<string[]>(comp.GetType(), "table1").Get().Concat(
            GetStaticField<string[]>(comp.GetType(), "table2").Get().Concat(
            GetStaticField<string[]>(comp.GetType(), "table3").Get())).ToArray();

        while (!fldModuleReady.Get())
            yield return null;

        var position = GetIntField(comp, "fontPosition").Get(min: 0, max: 2);
        var texts = GetArrayField<TextMesh>(comp, "mirrorTexts", isPublic: true).Get(expectedLength: 3);

        yield return WaitForSolve;

        addQuestion(module, Question.MirrorWord, correctAnswers: new[] { texts[position].text }, preferredWrongAnswers: candidateWords);
    }

    private IEnumerator<YieldInstruction> ProcessMisterSoftee(ModuleData module)
    {
        var comp = GetComponent(module, "misterSoftee");
        yield return WaitForSolve;

        var iceCreams = GetArrayField<int>(comp, "iceCreamsPresent").Get();
        var iceCreamNames = GetStaticField<string[]>(comp.GetType(), "iceCreamNames").Get();
        var ix = Array.IndexOf(iceCreams, 14);
        var directions = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };
        var displayedIceCreamSprites = iceCreams.Where(x => x != 14).Select(index => MisterSofteeSprites.First(sprite => sprite.name == iceCreamNames[index])).ToArray();
        addQuestions(module,
            makeQuestion(Question.MisterSofteeSpongebobPosition, module, correctAnswers: new[] { directions[ix] }),
            makeQuestion(Question.MisterSofteeTreatsPresent, module, correctAnswers: displayedIceCreamSprites));
    }

    private IEnumerator<YieldInstruction> ProcessModernCipher(ModuleData module)
    {
        var comp = GetComponent(module, "modernCipher");
        yield return WaitForSolve;

        var dictionary = GetField<Dictionary<string, string>>(comp, "chosenWords").Get();

        if (!dictionary.TryGetValue("Stage1", out var stage1word) || stage1word == null || !dictionary.TryGetValue("Stage2", out var stage2word) || stage2word == null)
            throw new AbandonModuleException($"There is no word for {(stage1word == null ? "stage 1" : "stage 2")}.");

        stage1word = stage1word.Substring(0, 1).ToUpperInvariant() + stage1word.Substring(1).ToLowerInvariant();
        stage2word = stage2word.Substring(0, 1).ToUpperInvariant() + stage2word.Substring(1).ToLowerInvariant();

        addQuestions(module,
            makeQuestion(Question.ModernCipherWord, module, formatArgs: new[] { ordinal(1) }, correctAnswers: new[] { stage1word }, preferredWrongAnswers: new[] { stage2word }),
            makeQuestion(Question.ModernCipherWord, module, formatArgs: new[] { ordinal(2) }, correctAnswers: new[] { stage2word }, preferredWrongAnswers: new[] { stage1word }));
    }

    private IEnumerator<YieldInstruction> ProcessModuleListening(ModuleData module)
    {
        var comp = GetComponent(module, "ModuleListening");
        yield return WaitForSolve;

        var clipsPerModule = GetArrayField<AudioClip[]>(comp, "audioLibrary").Get(expectedLength: 100);
        var soundIndex = GetArrayField<int>(comp, "soundIndex").Get(expectedLength: 4);

        var moduleNames = GetArrayField<string>(comp, "moduleNames").Get();
        var indices = GetArrayField<int>(comp, "moduleIndex").Get(validator: ar => ar.Length != 4 ? "expected length 4" : ar.Any(v => v < 0 || v >= moduleNames.Length) ? $"out of range for moduleNames (0–{moduleNames.Length - 1})" : null);
        var colorNames = GetArrayField<string>(comp, "buttonColors").Get(expectedLength: 4);
        var colorOrder = GetArrayField<int>(comp, "btnColors").Get(validator: ar => ar.Length != 4 ? "expected length 4" : ar.Any(v => v < 0 || v >= colorNames.Length) ? $"out of range for colorNames (0–{colorNames.Length - 1})" : null);
        var allUsed = Enumerable.Range(0, 4).Select(i => clipsPerModule[indices[i]][soundIndex[i]]).ToArray();

        // Pick a single sound from each module to avoid (a) too many Boot To Big sounds, because there are a lot; (b) nearly-identical sounds (like Colored Squares)
        var allAnswers = clipsPerModule.Select((clips, ix) => Array.IndexOf(indices, ix) is int p && p >= 0 ? clips[soundIndex[p]] : clips.PickRandom()).ToArray();

        addQuestions(module,
            Enumerable.Range(0, 4).Select(btn =>
                makeQuestion(Question.ModuleListeningButtonAudio, module, formatArgs: new[] { colorNames[colorOrder[btn]] },
                correctAnswers: new[] { clipsPerModule[indices[btn]][soundIndex[btn]] },
                preferredWrongAnswers: allUsed,
                allAnswers: allAnswers))
            .Concat(new[] { makeQuestion(Question.ModuleListeningAnyAudio, module, correctAnswers: allUsed, allAnswers: allAnswers) }));
    }

    private IEnumerator<YieldInstruction> ProcessModuleMaze(ModuleData module)
    {
        var comp = GetComponent(module, "ModuleMazeModule");
        var fldSprites = GetArrayField<Sprite>(comp, "sprites", true);
        yield return WaitForSolve;

        var sprites = fldSprites.Get(expectedLength: 400);
        var translatedSprites = sprites.Select(spr => spr.TranslateSprite()).ToArray();

        addQuestions(module,
            makeQuestion(Question.ModuleMazeStartingIcon, module,
                correctAnswers: new[] { translatedSprites[Array.IndexOf(sprites, GetField<Sprite>(comp, "souvenirStart").Get())] }, allAnswers: translatedSprites));
    }

    private IEnumerator<YieldInstruction> ProcessModuleMovements(ModuleData module)
    {
        var comp = GetComponent(module, "moduleMovements");
        var fldStageNum = GetIntField(comp, "stageNumber");
        var fldDisplay = GetField<SpriteRenderer>(comp, "display", isPublic: true);
        var currentStage = -1;
        var answers = new string[3];
        var moduleNames = GetArrayField<string>(comp, "modules", true).Get();

        while (module.Unsolved)
        {
            var nextStage = fldStageNum.Get();
            if (currentStage != nextStage)
            {
                currentStage = nextStage;
                answers[currentStage] = fldDisplay.Get().sprite.name;
            }
            yield return null;
        }

        addQuestions(module, answers.Select((ans, i) =>
            makeQuestion(Question.ModuleMovementsDisplay, module, formatArgs: new[] { ordinal(i + 1) },
                correctAnswers: new[] { ans }, preferredWrongAnswers: answers)));
    }

    private IEnumerator<YieldInstruction> ProcessMonsplodeFight(ModuleData module)
    {
        var comp = GetComponent(module, "MonsplodeFightModule");
        var fldCreatureID = GetIntField(comp, "crID");
        var fldMoveIDs = GetArrayField<int>(comp, "moveIDs");
        var fldRevive = GetField<bool>(comp, "revive");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var creatureData = GetField<object>(comp, "CD", isPublic: true).Get();
        var movesData = GetField<object>(comp, "MD", isPublic: true).Get();
        var buttons = GetArrayField<KMSelectable>(comp, "buttons", isPublic: true).Get(expectedLength: 4);
        var creatureNames = GetArrayField<string>(creatureData, "names", isPublic: true).Get();
        var moveNames = GetArrayField<string>(movesData, "names", isPublic: true).Get();

        string displayedCreature = null;
        string[] displayedMoves = null;
        var finished = false;

        var origInteracts = buttons.Select(btn => btn.OnInteract).ToArray();
        foreach (var i in Enumerable.Range(0, buttons.Length))    // Do not use ‘for’ loop as the loop variable is captured by a lambda
        {
            buttons[i].OnInteract = delegate
            {
                // Before processing the button push, get the creature and moves
                string curCreatureName = null;
                string[] curMoveNames = null;

                var creatureID = fldCreatureID.Get();
                if (creatureID == -1)
                {
                    // Missingno: do nothing
                }
                else if (creatureID < 0 || creatureID >= creatureNames.Length || string.IsNullOrEmpty(creatureNames[creatureID]))
                    Debug.Log($"<Souvenir #{_moduleId}> Monsplode, Fight!: Unexpected creature ID: {creatureID}; creature names are: [{creatureNames.Select(cn => cn == null ? "null" : '"' + cn + '"').JoinString(", ")}]");
                else
                {
                    // Make sure not to throw exceptions inside of the module’s button handler!
                    var moveIDs = fldMoveIDs.Get(nullAllowed: true);
                    if (moveIDs == null || moveIDs.Length != 4 || moveIDs.Any(mid => mid >= moveNames.Length || string.IsNullOrEmpty(moveNames[mid])))
                        Debug.Log($"<Souvenir #{_moduleId}> Monsplode, Fight!: Unexpected move IDs: {(moveIDs == null ? null : "[" + moveIDs.JoinString(", ") + "]")}; moves names are: [{moveNames.Select(mn => mn == null ? "null" : '"' + mn + '"').JoinString(", ")}]");
                    else
                    {
                        curCreatureName = creatureNames[creatureID];
                        curMoveNames = moveIDs.Select(mid => moveNames[mid].Replace("\r", "").Replace("\n", " ")).ToArray();
                    }
                }

                var ret = origInteracts[i]();

                if (creatureID == -1)
                {
                    Debug.Log($"[Souvenir #{_moduleId}] No question on Monsplode, Fight! because the creature displayed was Missingno.");
                    _legitimatelyNoQuestions.Add(module.Module);
                    displayedCreature = null;
                    displayedMoves = null;
                    finished = true;
                }
                else if (curCreatureName == null || curMoveNames == null)
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Monsplode, Fight!: Abandoning due to error above.");
                    // Set these to null to signal that something went wrong and we need to abort
                    displayedCreature = null;
                    displayedMoves = null;
                    finished = true;
                }
                else
                {
                    // If ‘revive’ is ‘false’, there is not going to be another stage.
                    if (!fldRevive.Get())
                        finished = true;

                    if (curCreatureName != null && curMoveNames != null)
                    {
                        displayedCreature = curCreatureName;
                        displayedMoves = curMoveNames;
                    }
                }
                return ret;
            };
        }

        while (!finished)
            yield return new WaitForSeconds(.1f);
        yield return WaitForSolve;

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].OnInteract = origInteracts[i];

        // If either of these is the case, an error message will already have been output above (within the button handler)
        if (displayedCreature == null || displayedMoves == null)
            yield break;

        addQuestions(module,
            makeQuestion(Question.MonsplodeFightCreature, module, correctAnswers: new[] { displayedCreature }),
            makeQuestion(Question.MonsplodeFightMove, module, formatArgs: new[] { "was" }, correctAnswers: displayedMoves),
            makeQuestion(Question.MonsplodeFightMove, module, formatArgs: new[] { "was not" }, correctAnswers: GetAnswers(Question.MonsplodeFightMove).Except(displayedMoves).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessMonsplodeTradingCards(ModuleData module)
    {
        var comp = GetComponent(module, "MonsplodeCardModule");
        var fldStage = GetIntField(comp, "correctOffer", isPublic: true);

        var stageCount = GetIntField(comp, "offerCount", isPublic: true).Get(min: 3, max: 3);
        var data = GetField<object>(comp, "CD", isPublic: true).Get();
        var monsplodeNames = GetArrayField<string>(data, "names", isPublic: true).Get().Select(s => s.Replace("\r", "").Replace("\n", " ")).ToArray();

        yield return WaitForSolve;

        if (fldStage.Get() != stageCount)
            throw new AbandonModuleException($"Abandoning Monsplode Trading Cards because ‘correctOffer’ has unexpected value {fldStage.Get()} instead of {stageCount}.");

        var deck = GetField<Array>(comp, "deck", isPublic: true).Get(ar => ar.Length != 3 ? "expected length 3" : null).Cast<object>().ToArray();
        var offer = GetField<object>(comp, "offer", isPublic: true).Get();
        var fldMonsplode = GetIntField(offer, "monsplode", isPublic: true);
        var fldPrintDigit = GetIntField(offer, "printDigit", isPublic: true);
        var fldPrintChar = GetField<char>(offer, "printChar", isPublic: true);

        var monsplodeIds = new[] { fldMonsplode.Get(0, monsplodeNames.Length - 1) }.Concat(deck.Select(card => fldMonsplode.GetFrom(card, 0, monsplodeNames.Length - 1))).ToArray();
        var monsplodes = monsplodeIds.Select(mn => monsplodeNames[mn]).ToArray();
        var printVersions = new[] { fldPrintChar.Get() + "" + fldPrintDigit.Get() }.Concat(deck.Select(card => fldPrintChar.GetFrom(card) + "" + fldPrintDigit.GetFrom(card))).ToArray();
        addQuestions(module,
            makeQuestion(Question.MonsplodeTradingCardsCards, module, formatArgs: new[] { "card on offer" }, correctAnswers: new[] { monsplodes[0] }, preferredWrongAnswers: monsplodeNames),
            makeQuestion(Question.MonsplodeTradingCardsCards, module, formatArgs: new[] { "first card in your hand" }, correctAnswers: new[] { monsplodes[1] }, preferredWrongAnswers: monsplodeNames),
            makeQuestion(Question.MonsplodeTradingCardsCards, module, formatArgs: new[] { "second card in your hand" }, correctAnswers: new[] { monsplodes[2] }, preferredWrongAnswers: monsplodeNames),
            makeQuestion(Question.MonsplodeTradingCardsCards, module, formatArgs: new[] { "third card in your hand" }, correctAnswers: new[] { monsplodes[3] }, preferredWrongAnswers: monsplodeNames),
            makeQuestion(Question.MonsplodeTradingCardsPrintVersions, module, formatArgs: new[] { "card on offer" }, correctAnswers: new[] { printVersions[0] }, preferredWrongAnswers: printVersions),
            makeQuestion(Question.MonsplodeTradingCardsPrintVersions, module, formatArgs: new[] { "first card in your hand" }, correctAnswers: new[] { printVersions[1] }, preferredWrongAnswers: printVersions),
            makeQuestion(Question.MonsplodeTradingCardsPrintVersions, module, formatArgs: new[] { "second card in your hand" }, correctAnswers: new[] { printVersions[2] }, preferredWrongAnswers: printVersions),
            makeQuestion(Question.MonsplodeTradingCardsPrintVersions, module, formatArgs: new[] { "third card in your hand" }, correctAnswers: new[] { printVersions[3] }, preferredWrongAnswers: printVersions));
    }

    private IEnumerator<YieldInstruction> ProcessMoon(ModuleData module)
    {
        var comp = GetComponent(module, "theMoonScript");
        yield return WaitForSolve;

        var lightIndex = GetIntField(comp, "lightIndex").Get(min: 0, max: 7);
        var qNames = new[] { "first initially lit", "second initially lit", "third initially lit", "fourth initially lit", "first initially unlit", "second initially unlit", "third initially unlit", "fourth initially unlit" };
        var aNames = new[] { "south", "south-west", "west", "north-west", "north", "north-east", "east", "south-east" };
        addQuestions(module, Enumerable.Range(0, 8).Select(i => makeQuestion(Question.MoonLitUnlit, module, formatArgs: new[] { qNames[i] }, correctAnswers: new[] { aNames[(i + lightIndex) % 8] })));
    }

    private IEnumerator<YieldInstruction> ProcessMoreCode(ModuleData module)
    {
        var comp = GetComponent(module, "MoreCode");
        yield return WaitForSolve;

        var word = GetField<string>(comp, "SolutionWord").Get();
        word = word.Substring(0, 1) + word.Substring(1).ToLowerInvariant();
        addQuestion(module, Question.MoreCodeWord, correctAnswers: new[] { word });
    }

    private IEnumerator<YieldInstruction> ProcessMorseAMaze(ModuleData module)
    {
        var comp = GetComponent(module, "MorseAMaze");

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var start = GetField<string>(comp, "_souvenirQuestionStartingLocation").Get(str => str.Length != 2 ? "expected length 2" : null);
        var end = GetField<string>(comp, "_souvenirQuestionEndingLocation").Get(str => str.Length != 2 ? "expected length 2" : null);
        var word = GetField<string>(comp, "_souvenirQuestionWordPlaying").Get(str => str.Length < 4 ? "expected length ≥ 4" : null);
        var words = GetArrayField<string>(comp, "_souvenirQuestionWordList").Get(expectedLength: 36);

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.MorseAMazeStartingCoordinate, module, correctAnswers: new[] { start }),
            makeQuestion(Question.MorseAMazeEndingCoordinate, module, correctAnswers: new[] { end }),
            makeQuestion(Question.MorseAMazeMorseCodeWord, module, correctAnswers: new[] { word }, preferredWrongAnswers: words));
    }

    private IEnumerator<YieldInstruction> ProcessMorseButtons(ModuleData module)
    {
        var comp = GetComponent(module, "morseButtonsScript");
        yield return WaitForSolve;

        string alphabet = GetField<string>(comp, "alphabet").Get();
        var colorNames = new[] { "red", "blue", "green", "yellow", "orange", "purple" };
        int[] letters = GetArrayField<int>(comp, "letters").Get(expectedLength: 6, validator: x => x < 0 || x >= alphabet.Length ? "out of range" : null);
        int[] colors = GetArrayField<int>(comp, "colors").Get(expectedLength: 6, validator: x => x < 0 || x >= colorNames.Length ? "out of range" : null);

        addQuestions(module,
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "first" }, correctAnswers: new[] { alphabet[letters[0]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "second" }, correctAnswers: new[] { alphabet[letters[1]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "third" }, correctAnswers: new[] { alphabet[letters[2]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { alphabet[letters[3]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { alphabet[letters[4]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, module, formatArgs: new[] { "sixth" }, correctAnswers: new[] { alphabet[letters[5]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "first" }, correctAnswers: new[] { colorNames[colors[0]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "second" }, correctAnswers: new[] { colorNames[colors[1]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "third" }, correctAnswers: new[] { colorNames[colors[2]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { colorNames[colors[3]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { colorNames[colors[4]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, module, formatArgs: new[] { "sixth" }, correctAnswers: new[] { colorNames[colors[5]].ToString() }, preferredWrongAnswers: colorNames));
    }

    private IEnumerator<YieldInstruction> ProcessMorsematics(ModuleData module)
    {
        var comp = GetComponent(module, "AdvancedMorse");
        var chars = GetArrayField<string>(comp, "DisplayCharsRaw").Get(expectedLength: 3);

        yield return WaitForSolve;

        addQuestions(module, Enumerable.Range(0, 3).Select(i => makeQuestion(Question.MorsematicsReceivedLetters, module, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { chars[i] }, preferredWrongAnswers: chars)));
    }

    private IEnumerator<YieldInstruction> ProcessMorseWar(ModuleData module)
    {
        var comp = GetComponent(module, "MorseWar");
        yield return WaitForSolve;

        var wordTable = GetStaticField<string[]>(comp.GetType(), "WordTable").Get();
        var rowTable = GetStaticField<string[]>(comp.GetType(), "RowTable").Get(ar => ar.Length != 6 ? "expected length 6" : null);
        var wordNum = GetIntField(comp, "wordNum").Get(min: 0, max: wordTable.Length - 1);
        var lights = GetField<string>(comp, "lights").Get(str => str.Length != 3 ? "expected length 3" : str.Any(ch => ch < '1' || ch > '6') ? "expected characters 1–6" : null);

        var qs = new List<QandA>() { makeQuestion(Question.MorseWarCode, module, correctAnswers: new[] { wordTable[wordNum].ToUpperInvariant() }) };
        var rowNames = new[] { "bottom", "middle", "top" };
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.MorseWarLeds, module, formatArgs: new[] { rowNames[i] }, correctAnswers: new[] { rowTable[lights[i] - '1'] }));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessMouseInTheMaze(ModuleData module)
    {
        var comp = GetComponent(module, "MouseInTheMazeModule");
        var sphereColors = GetArrayField<int>(comp, "_sphereColors").Get(expectedLength: 4);

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var goalPos = GetIntField(comp, "_goalPosition").Get(min: 0, max: 3);
        var torusColor = GetIntField(comp, "_torusColor").Get(min: 0, max: 3);
        var goalColor = sphereColors[goalPos];
        if (goalColor < 0 || goalColor > 3)
            throw new AbandonModuleException($"Unexpected color (torus={torusColor}; goal={goalColor})");

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.MouseInTheMazeSphere, module, correctAnswers: new[] { new[] { "white", "green", "blue", "yellow" }[goalColor] }),
            makeQuestion(Question.MouseInTheMazeTorus, module, correctAnswers: new[] { new[] { "white", "green", "blue", "yellow" }[torusColor] }));
    }

    private IEnumerator<YieldInstruction> ProcessMSeq(ModuleData module)
    {
        var comp = GetComponent(module, "MSeqScript");
        yield return WaitForSolve;

        int[] obtainedDigits = GetArrayField<int>(comp, "obtainedDigits").Get(expectedLength: 3);
        int submittedNum = GetIntField(comp, "finalNumber").Get(min: 25, max: 225);

        addQuestions(module,
            makeQuestion(Question.MSeqObtained, module, correctAnswers: new[] { obtainedDigits[0].ToString() }, formatArgs: new[] { "first" }),
            makeQuestion(Question.MSeqObtained, module, correctAnswers: new[] { obtainedDigits[1].ToString() }, formatArgs: new[] { "second" }),
            makeQuestion(Question.MSeqObtained, module, correctAnswers: new[] { obtainedDigits[2].ToString() }, formatArgs: new[] { "third" }),
            makeQuestion(Question.MSeqSubmitted, module, correctAnswers: new[] { submittedNum.ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessMulticoloredSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "MultiColoredSwitches");
        var fldLedsUp = GetField<Array>(comp, "LEDsUp");
        var fldLedsDown = GetField<Array>(comp, "LEDsDown");

        yield return WaitForSolve;

        var ledsUp = fldLedsUp.Get(validator: arr => arr.Length != 5 ? "expected length 5" : null);
        var ledsDown = fldLedsDown.Get(validator: arr => arr.Length != 5 ? "expected length 5" : null);

        var fldCharColor1 = GetField<char>(ledsUp.GetValue(0), "CharColor1", isPublic: true);
        var fldCharColor2 = GetField<char>(ledsUp.GetValue(0), "CharColor2", isPublic: true);

        var upColors = Enumerable.Range(0, 5).Select(i => new[] { fldCharColor1.GetFrom(ledsUp.GetValue(i)), fldCharColor2.GetFrom(ledsUp.GetValue(i)) }).ToArray();
        var downColors = Enumerable.Range(0, 5).Select(i => new[] { fldCharColor1.GetFrom(ledsDown.GetValue(i)), fldCharColor2.GetFrom(ledsDown.GetValue(i)) }).ToArray();

        var colorNames = new[] { "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white" };
        var colorChars = "KRGYBMCW";

        var qs = new List<QandA>();
        for (var upDown = 0; upDown < 2; upDown++)
            for (var cycle = 0; cycle < 2; cycle++)
                for (var led = 0; led < 5; led++)
                    qs.Add(makeQuestion(Question.MulticoloredSwitchesLedColor, module,
                        formatArgs: new[] { ordinal(led + 1), upDown == 0 ? "top" : "bottom", cycle == 0 ? "lit" : "unlit" },
                        correctAnswers: new[] { colorNames[colorChars.IndexOf((upDown == 0 ? upColors : downColors)[led][cycle])] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessMurder(ModuleData module)
    {
        var comp = GetComponent(module, "MurderModule");

        // Just a consistency check
        GetIntField(comp, "suspects").Get(min: 4, max: 4);
        GetIntField(comp, "weapons").Get(min: 4, max: 4);

        yield return WaitForSolve;

        var solution = GetArrayField<int>(comp, "solution").Get(expectedLength: 3);
        var skipDisplay = GetField<int[,]>(comp, "skipDisplay").Get(ar => ar.GetLength(0) != 2 || ar.GetLength(1) != 6 ? $"dimensions are {ar.GetLength(0)},{ar.GetLength(1)}; expected 2,6" : null);
        var names = GetField<string[,]>(comp, "names").Get(ar => ar.GetLength(0) != 3 || ar.GetLength(1) != 9 ? $"dimensions are {ar.GetLength(0)},{ar.GetLength(1)}; expected 3,9" : null);
        var actualSuspect = solution[0];
        var actualWeapon = solution[1];
        var actualRoom = solution[2];
        var bodyFound = GetIntField(comp, "bodyFound").Get();
        if (actualSuspect < 0 || actualSuspect >= 6 || actualWeapon < 0 || actualWeapon >= 6 || actualRoom < 0 || actualRoom >= 9 || bodyFound < 0 || bodyFound >= 9)
            throw new AbandonModuleException($"Unexpected suspect, weapon, room or bodyFound (expected 0–5/0–5/0–8/0–8, got {actualSuspect}/{actualWeapon}/{actualRoom}/{bodyFound}).");

        addQuestions(module,
            makeQuestion(Question.MurderSuspect, module,
                formatArgs: new[] { "a suspect but not the murderer" },
                correctAnswers: Enumerable.Range(0, 6).Where(suspectIx => skipDisplay[0, suspectIx] == 0 && suspectIx != actualSuspect).Select(suspectIx => names[0, suspectIx]).ToArray()),
            makeQuestion(Question.MurderSuspect, module,
                formatArgs: new[] { "not a suspect" },
                correctAnswers: Enumerable.Range(0, 6).Where(suspectIx => skipDisplay[0, suspectIx] == 1).Select(suspectIx => names[0, suspectIx]).ToArray()),

            makeQuestion(Question.MurderWeapon, module,
                formatArgs: new[] { "a potential weapon but not the murder weapon" },
                correctAnswers: Enumerable.Range(0, 6).Where(weaponIx => skipDisplay[1, weaponIx] == 0 && weaponIx != actualWeapon).Select(weaponIx => names[1, weaponIx]).ToArray()),
            makeQuestion(Question.MurderWeapon, module,
                formatArgs: new[] { "not a potential weapon" },
                correctAnswers: Enumerable.Range(0, 6).Where(weaponIx => skipDisplay[1, weaponIx] == 1).Select(weaponIx => names[1, weaponIx]).ToArray()),

            bodyFound == actualRoom ? null : makeQuestion(Question.MurderBodyFound, module, correctAnswers: new[] { names[2, bodyFound] }));
    }

    private IEnumerator<YieldInstruction> ProcessMysticSquare(ModuleData module)
    {
        var comp = GetComponent(module, "MysticSquareModule");
        var skull = GetField<Transform>(comp, "Skull", true).Get();

        while (!skull.gameObject.activeSelf)
            yield return null;

        yield return WaitForSolve;

        var skullpos = GetIntField(comp, "_skullPos").Get(min: 0, max: 8);
        var spacepos = Array.IndexOf(GetArrayField<int>(comp, "_field").Get(), 0);

        // If the skull is in the empty space, shrink and then disappear it.
        if (skullpos == spacepos)
        {
            // Make sure that the last sliding animation finishes
            yield return new WaitForSeconds(0.5f);

            const float duration = 1.5f;
            var elapsed = 0f;
            while (elapsed < duration)
            {
                skull.localScale = Vector3.Lerp(new Vector3(0.004f, 0.004f, 0.004f), Vector3.zero, elapsed / duration);
                yield return null;
                elapsed += Time.deltaTime;
            }
        }

        skull.gameObject.SetActive(false);
        var answers = new[] { "top left", "top middle", "top right", "middle left", "center", "middle right", "bottom left", "bottom middle", "bottom right" };
        addQuestion(module, Question.MysticSquareSkull, correctAnswers: new[] { answers[skullpos] });
    }

    private IEnumerator<YieldInstruction> ProcessMysteryModule(ModuleData module)
    {
        var comp = GetComponent(module, "MysteryModuleScript");
        var fldKeyModules = GetListField<KMBombModule>(comp, "keyModules");
        var fldMystifiedModule = GetField<KMBombModule>(comp, "mystifiedModule");
        var fldAnimating = GetField<bool>(comp, "animating");
        var fldFailsolve = GetField<bool>(comp, "failsolve");

        while (fldKeyModules.Get(nullAllowed: true) == null && !fldFailsolve.Get())
            yield return null;
        while (fldMystifiedModule.Get(nullAllowed: true) == null && !fldFailsolve.Get())
            yield return null;

        if (fldFailsolve.Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Mystery Module because no module was hidden.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        var keyModule = fldKeyModules.Get(ar => ar.Count == 0 ? "empty" : null)[0];
        var mystifiedModule = fldMystifiedModule.Get();

        // Do not ask questions while Souvenir is hidden by Mystery Module.
        if (mystifiedModule == Module)
            _avoidQuestions++;

        yield return WaitForSolve;

        // Do not ask questions during the solve animation, since Mystery Module modifies the scaling of this module.
        while (fldAnimating.Get())
            yield return new WaitForSeconds(.1f);

        addQuestions(module,
            makeQuestion(Question.MysteryModuleFirstKey, module, correctAnswers: new[] { keyModule.ModuleDisplayName }, preferredWrongAnswers: Bomb.GetSolvableModuleNames().ToArray()),
            makeQuestion(Question.MysteryModuleHiddenModule, module, correctAnswers: new[] { mystifiedModule.ModuleDisplayName }, preferredWrongAnswers: Bomb.GetSolvableModuleNames().ToArray()));

        if (mystifiedModule == Module)
            _avoidQuestions--;
    }
}
