using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessMadMemory(KMBombModule module)
    {
        var comp = GetComponent(module, "MadMemory");

        bool isSolved = false;
        module.OnPass += () => { isSolved = true; return false; };
        while (!isSolved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MadMemory);

        var possibleTexts = GetArrayField<string>(comp, "screenTexts", true).Get(expectedLength: 16);
        var displayedLabels = GetArrayField<int>(comp, "screenLabels", true).Get(expectedLength: 4);

        var qs = new List<QandA>();
        for (int stageNum = 0; stageNum < 4; stageNum++)
            qs.Add(makeQuestion(Question.MadMemoryDisplays, _MadMemory, formatArgs: new[] { ordinal(stageNum + 1) }, correctAnswers: new[] { possibleTexts[displayedLabels[stageNum]] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMafia(KMBombModule module)
    {
        var comp = GetComponent(module, "MafiaModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Mafia);

        var godfather = GetField<object>(comp, "_godfather").Get();
        var suspects = GetField<Array>(comp, "_suspects").Get(ar => ar.Length != 8 ? "expected length 8" : null);
        addQuestion(module, Question.MafiaPlayers, correctAnswers: suspects.Cast<object>().Select(obj => obj.ToString()).Except(new[] { godfather.ToString() }).ToArray());
    }

    private IEnumerable<object> ProcessMagentaCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "magentaCipher", Question.MagentaCipherScreen, _MagentaCipher);
    }

    private IEnumerable<object> ProcessMahjong(KMBombModule module)
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
        _modulesSolved.IncSafe(_Mahjong);

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
            makeQuestion(Question.MahjongCountingTile, _Mahjong, correctAnswers: new[] { countingTileSprite }, preferredWrongAnswers: GetArrayField<int>(comp, "_countingRow").Get().Select(ix => MahjongSprites[ix]).ToArray()),
            makeQuestion(Question.MahjongMatches, _Mahjong, formatArgs: new[] { "first" }, correctAnswers: matchedTileSprites.Take(2).ToArray(), preferredWrongAnswers: tileSprites),
            makeQuestion(Question.MahjongMatches, _Mahjong, formatArgs: new[] { "second" }, correctAnswers: matchedTileSprites.Skip(2).Take(2).ToArray(), preferredWrongAnswers: tileSprites));
    }

    private IEnumerable<object> ProcessMandMs(KMBombModule module)
    {
        var comp = GetComponent(module, "MandMs");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MandMs);

        var colorNames = new[] { "red", "green", "orange", "blue", "yellow", "brown" };
        var colors = GetArrayField<int>(comp, "buttonColors").Get();
        var labels = GetArrayField<string>(comp, "labels").Get();
        var qs = new List<QandA>();
        for (int i = 0; i < 5; i++)
        {
            qs.Add(makeQuestion(Question.MandMsColors, _MandMs, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colorNames[colors[i]] }));
            qs.Add(makeQuestion(Question.MandMsLabels, _MandMs, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { labels[i] }, preferredWrongAnswers: labels));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMandNs(KMBombModule module)
    {
        var comp = GetComponent(module, "MandNs");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MandNs);

        var colorNames = new[] { "red", "green", "orange", "blue", "yellow", "brown" };
        var colors = GetArrayField<int>(comp, "buttonColors").Get();
        var labels = GetArrayField<string>(comp, "convertedValues").Get();
        var solution = GetIntField(comp, "solution").Get();
        var qs = new List<QandA>();
        for (int i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.MandNsColors, _MandNs, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colorNames[colors[i]] }));
        qs.Add(makeQuestion(Question.MandNsLabel, _MandNs, correctAnswers: new[] { labels[solution] }, preferredWrongAnswers: labels));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMaritimeFlags(KMBombModule module)
    {
        var comp = GetComponent(module, "MaritimeFlagsModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MaritimeFlags);

        var bearing = GetIntField(comp, "_bearingOnModule").Get(min: 0, max: 359);
        var callsignObj = GetField<object>(comp, "_callsign").Get();
        var callsign = GetField<string>(callsignObj, "Name", isPublic: true).Get(str => str.Length != 7 ? "expected length 7" : null);

        addQuestions(module,
            makeQuestion(Question.MaritimeFlagsBearing, _MaritimeFlags, correctAnswers: new[] { bearing.ToString() }),
            makeQuestion(Question.MaritimeFlagsCallsign, _MaritimeFlags, correctAnswers: new[] { callsign.ToLowerInvariant() }));
    }

    private IEnumerable<object> ProcessMaroonCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "maroonCipher", Question.MaroonCipherScreen, _MaroonCipher);
    }

    private IEnumerable<object> ProcessMashematics(KMBombModule module)
    {
        var comp = GetComponent(module, "mashematicsScript");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Mashematics);
        var numberClass = GetField<object>(comp, "number").Get();
        var answer = GetMethod<int>(numberClass, "GetNumberOfRequiredPush", numParameters: 0, isPublic: true).Invoke();
        var number1 = GetField<int>(numberClass, "Number1", isPublic: true).Get();
        var number2 = GetField<int>(numberClass, "Number2", isPublic: true).Get();
        var number3 = GetField<int>(numberClass, "Number3", isPublic: true).Get();

        var questions = new List<QandA> { makeQuestion(Question.MashematicsAnswer, _Mashematics, correctAnswers: new[] { answer.ToString() }) };
        for (int i = 0; i < 3; i++)
        {
            var number = i == 0 ? number1 : (i == 1 ? number2 : number3);
            questions.Add(makeQuestion(Question.MashematicsCalculation, _Mashematics, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { number.ToString() }));
        }
        addQuestions(module, questions);
    }

    private IEnumerable<object> ProcessMathEm(KMBombModule module)
    {
        var comp = GetComponent(module, "MathemScript");

        bool solved = false;
        module.OnPass += delegate () { solved = true; return false; };

        var fldArrangements = GetField<int[][]>(comp, "tarrange");
        var fldProps = GetField<int[,]>(comp, "tprops");
        var fldMats = GetArrayField<Material>(comp, "tpatterns", isPublic: true);

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MathEm);

        int[] initialArrangement = fldArrangements.Get().First();
        int[,] props = fldProps.Get();

        List<QandA> qs = new List<QandA>();
        string[] colorNames = { "White", "Bronze", "Silver", "Gold" };
        Sprite[] displayedMarkings = Enumerable.Range(0, 16).Select(ix => MathEmSprites[(props[initialArrangement[ix], 0] * 10) + props[initialArrangement[ix], 2]]).ToArray();

        for (int tileIx = 0; tileIx < 16; tileIx++)
        {
            qs.Add(makeQuestion(Question.MathEmColor, _MathEm,
                questionSprite: Grid.GenerateGridSprite(new Coord(4, 4, tileIx)),
                correctAnswers: new[] { colorNames[props[initialArrangement[tileIx], 1]] }));
            qs.Add(makeQuestion(Question.MathEmLabel, _MathEm,
                questionSprite: Grid.GenerateGridSprite(new Coord(4, 4, tileIx)),
                correctAnswers: new[] { displayedMarkings[tileIx] },
                preferredWrongAnswers: displayedMarkings));
        }
        addQuestions(module, qs);

    }

    private IEnumerable<object> ProcessMatrix(KMBombModule module)
    {
        var comp = GetComponent(module, "MatrixScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Matrix);

        // “selectedNames” contains the scrambled versions of the names. Find the unscrambled name.
        var unscrambledNames = GetArrayField<string>(comp, "selectedNames").Get()
            .Select(n => GetAnswers(Question.MatrixAccessCode).FirstOrDefault(ac => n.ToLowerInvariant().OrderBy(ch => ch).JoinString() == ac.ToLowerInvariant().OrderBy(ch => ch).JoinString()))
            .ToArray();

        addQuestions(module,
            makeQuestion(Question.MatrixAccessCode, _Matrix, correctAnswers: unscrambledNames),
            makeQuestion(Question.MatrixGlitchWord, _Matrix, correctAnswers: new[] { GetField<string>(comp, "illegalWordText").Get().ToLowerInvariant() }));
    }

    private IEnumerable<object> ProcessMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "InvisibleWallsComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        var currentCell = GetProperty<object>(comp, "CurrentCell", isPublic: true).Get();  // Need to get the current cell at the start.
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Maze);

        addQuestions(module,
            makeQuestion(Question.MazeStartingPosition, _Maze, formatArgs: new[] { "column", "left" }, correctAnswers: new[] { (GetIntField(currentCell, "X", true).Get() + 1).ToString() }),
            makeQuestion(Question.MazeStartingPosition, _Maze, formatArgs: new[] { "row", "top" }, correctAnswers: new[] { (GetIntField(currentCell, "Y", true).Get() + 1).ToString() }));
    }

    private IEnumerable<object> ProcessMaze3(KMBombModule module)
    {
        var comp = GetComponent(module, "maze3Script");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var node = GetIntField(comp, "node").Get(min: 0, max: 53);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Maze3);

        var colors = new[] { "Red", "Blue", "Yellow", "Green", "Magenta", "Orange" };
        addQuestion(module, Question.Maze3StartingFace, correctAnswers: new[] { colors[node / 9] });
    }

    private IEnumerable<object> ProcessMazeIdentification(KMBombModule module)
    {
        var comp = GetComponent(module, "MazeIdentificationScript");
        var fldSolved = GetField<bool>(comp, "Solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MazeIdentification);

        var seed = GetArrayField<int>(comp, "Quadrants").Get(validator: x => x.Any(y => y >= 4 || y < 0) ? "quadrants out of range" : null);
        var buttonFuncs = GetArrayField<int>(comp, "ButtonFunctions").Get(validator: x => x.Any(y => y >= 4 || y < 0) ? "functions out of range" : null);
        var directions = new[] { "Forwards", "Clockwise", "Backwards", "Counter-clockwise" };
        addQuestions(module,
            makeQuestion(Question.MazeIdentificationSeed, _MazeIdentification, correctAnswers: new[] { seed.Select(x => x + 1).JoinString() }),
            makeQuestion(Question.MazeIdentificationNum, _MazeIdentification, formatArgs: new[] { "1" }, correctAnswers: new[] { directions[buttonFuncs[0]] }),
            makeQuestion(Question.MazeIdentificationNum, _MazeIdentification, formatArgs: new[] { "2" }, correctAnswers: new[] { directions[buttonFuncs[1]] }),
            makeQuestion(Question.MazeIdentificationNum, _MazeIdentification, formatArgs: new[] { "3" }, correctAnswers: new[] { directions[buttonFuncs[2]] }),
            makeQuestion(Question.MazeIdentificationNum, _MazeIdentification, formatArgs: new[] { "4" }, correctAnswers: new[] { directions[buttonFuncs[3]] }),
            makeQuestion(Question.MazeIdentificationFunc, _MazeIdentification, formatArgs: new[] { "moved you forwards" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 0) + 1).ToString() }),
            makeQuestion(Question.MazeIdentificationFunc, _MazeIdentification, formatArgs: new[] { "turned you clockwise" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 1) + 1).ToString() }),
            makeQuestion(Question.MazeIdentificationFunc, _MazeIdentification, formatArgs: new[] { "moved you backwards" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 2) + 1).ToString() }),
            makeQuestion(Question.MazeIdentificationFunc, _MazeIdentification, formatArgs: new[] { "turned you counter-clockwise" }, correctAnswers: new[] { (Array.IndexOf(buttonFuncs, 3) + 1).ToString() }));
    }

    private IEnumerable<object> ProcessMazematics(KMBombModule module)
    {
        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Mazematics);

        var comp = GetComponent(module, "Mazematics");
        var startVal = GetIntField(comp, "startValue").Get(17, 49).ToString();
        var goalVal = GetIntField(comp, "goalValue").Get(0, 49).ToString();

        string[] possibleStartVals = Enumerable.Range(17, 33).Select(x => x.ToString()).ToArray();
        string[] possibleGoalVals = Enumerable.Range(0, 50).Select(x => x.ToString()).ToArray();

        addQuestions(module,
            makeQuestion(Question.MazematicsValue, _Mazematics, formatArgs: new[] { "initial" }, correctAnswers: new[] { startVal }, preferredWrongAnswers: possibleStartVals),
            makeQuestion(Question.MazematicsValue, _Mazematics, formatArgs: new[] { "goal" }, correctAnswers: new[] { goalVal }, preferredWrongAnswers: possibleGoalVals));
    }

    private IEnumerable<object> ProcessMazeScrambler(KMBombModule module)
    {
        var comp = GetComponent(module, "MazeScrambler");
        var fldSolved = GetField<bool>(comp, "SOLVED");

        var ind1X = GetIntField(comp, "IDX1").Get(min: 0, max: 2);
        var ind1Y = GetIntField(comp, "IDY1").Get(min: 0, max: 2);
        var ind2X = GetIntField(comp, "IDX2").Get(min: 0, max: 2);
        var ind2Y = GetIntField(comp, "IDY2").Get(min: 0, max: 2);
        var startX = GetIntField(comp, "StartX").Get(min: 0, max: 2);
        var startY = GetIntField(comp, "StartY").Get(min: 0, max: 2);
        var goalX = GetIntField(comp, "GoalX").Get(min: 0, max: 2);
        var goalY = GetIntField(comp, "GoalY").Get(min: 0, max: 2);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        var positionNames = new[] { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" };

        _modulesSolved.IncSafe(_MazeScrambler);
        addQuestions(module,
            makeQuestion(Question.MazeScramblerStart, _MazeScrambler, correctAnswers: new[] { positionNames[startY * 3 + startX] }, preferredWrongAnswers: new[] { positionNames[goalY * 3 + goalX] }),
            makeQuestion(Question.MazeScramblerGoal, _MazeScrambler, correctAnswers: new[] { positionNames[goalY * 3 + goalX] }, preferredWrongAnswers: new[] { positionNames[startY * 3 + startX] }),
            makeQuestion(Question.MazeScramblerIndicators, _MazeScrambler, correctAnswers: new[] { positionNames[ind1Y * 3 + ind1X], positionNames[ind2Y * 3 + ind2X] }, preferredWrongAnswers: positionNames));
    }

    private IEnumerable<object> ProcessMazeseeker(KMBombModule module)
    {
        var comp = GetComponent(module, "MazeseekerScript");
        var fldSolved = GetField<bool>(comp, "Solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Mazeseeker);

        var nums = GetField<int[,]>(comp, "Grid").Get();
        var startRow = GetField<int>(comp, "StartingRow").Get();
        var startColumn = GetField<int>(comp, "StartingColumn").Get();
        var goalRow = GetField<int>(comp, "GoalRow").Get();
        var goalColumn = GetField<int>(comp, "GoalColumn").Get();

        var qs = new List<QandA>();
        for (int i = 0; i < 36; i++)
            qs.Add(makeQuestion(Question.MazeseekerCell, _Mazeseeker, questionSprite: Grid.GenerateGridSprite(new Coord(6, 6, i)), correctAnswers: new[] { nums[i / 6, i % 6].ToString() }));
        for (int i = 0; i < 36; i++)
            qs.Add(makeQuestion(Question.MazeseekerStart, _Mazeseeker, correctAnswers: new[] { new Coord(6, 6, startColumn, startRow) }));
        for (int i = 0; i < 36; i++)
            qs.Add(makeQuestion(Question.MazeseekerGoal, _Mazeseeker, correctAnswers: new[] { new Coord(6, 6, goalColumn, goalRow) }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMegaMan2(KMBombModule module)
    {
        var comp = GetComponent(module, "Megaman2");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var robotMasters = GetArrayField<string>(comp, "robotMasters").Get();
        var selectedMaster = GetIntField(comp, "selectedMaster").Get(min: 0, max: robotMasters.Length - 1);
        var selectedWeapon = GetIntField(comp, "selectedWeapon").Get(min: 0, max: robotMasters.Length - 1);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MegaMan2);

        addQuestions(module,
            makeQuestion(Question.MegaMan2SelectedMaster, _MegaMan2, correctAnswers: new[] { robotMasters[selectedMaster] }, preferredWrongAnswers: robotMasters),
            makeQuestion(Question.MegaMan2SelectedWeapon, _MegaMan2, correctAnswers: new[] { robotMasters[selectedWeapon] }, preferredWrongAnswers: robotMasters));
    }

    private IEnumerable<object> ProcessMelodySequencer(KMBombModule module)
    {
        var comp = GetComponent(module, "MelodySequencerScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        var parts = GetArrayField<int[]>(comp, "parts").Get(expectedLength: 8);  // the 8 parts in their “correct” order
        var moduleParts = GetArrayField<int[]>(comp, "moduleParts").Get(expectedLength: 8, nullContentAllowed: true);      // the parts as assigned to the slots
        var partsPerSlot = Enumerable.Range(0, 8).Select(slot => parts.IndexOf(p => ReferenceEquals(p, moduleParts[slot]))).ToArray();
        Debug.Log($"<Souvenir #{_moduleId}> Melody Sequencer: parts are: [{partsPerSlot.JoinString(", ")}].");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MelodySequencer);

        var qs = new List<QandA>();
        var givenSlots = Enumerable.Range(0, partsPerSlot.Length).Where(slot => partsPerSlot[slot] != -1).Select(slot => (slot + 1).ToString()).ToArray();
        var givenParts = partsPerSlot.Where(part => part != -1).Select(part => (part + 1).ToString()).ToArray();
        for (int i = 0; i < partsPerSlot.Length; i++)
        {
            if (partsPerSlot[i] != -1)
            {
                qs.Add(makeQuestion(Question.MelodySequencerParts, _MelodySequencer, formatArgs: new[] { (partsPerSlot[i] + 1).ToString() }, correctAnswers: new[] { (i + 1).ToString() }, preferredWrongAnswers: givenSlots));
                qs.Add(makeQuestion(Question.MelodySequencerSlots, _MelodySequencer, formatArgs: new[] { (i + 1).ToString() }, correctAnswers: new[] { (partsPerSlot[i] + 1).ToString() }, preferredWrongAnswers: givenParts));
            }
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMemorableButtons(KMBombModule module)
    {
        var comp = GetComponent(module, "MemorableButtons");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var buttonLabels = GetArrayField<TextMesh>(comp, "buttonLabels", isPublic: true).Get(ar => ar.Length == 0 ? "empty" : null);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MemorableButtons);

        var combinedCode = GetField<string>(comp, "combinedCode", isPublic: true).Get(str => str.Length < 10 || str.Length > 15 ? "expected length 10–15" : null);
        addQuestions(module, combinedCode.Select((ch, ix) => makeQuestion(Question.MemorableButtonsSymbols, _MemorableButtons, buttonLabels[0].font, buttonLabels[0].GetComponent<MeshRenderer>().sharedMaterial.mainTexture, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { ch.ToString() })));
    }

    private IEnumerable<object> ProcessMemory(KMBombModule module)
    {
        var comp = GetComponent(module, "MemoryComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Memory);

        var displaySequence = GetProperty<string>(comp, "DisplaySequence", true).Get();
        var indices = GetListField<int>(comp, "buttonIndicesPressed", false).Get();
        var labels = GetListField<string>(comp, "buttonLabelsPressed", false).Get();
        var qs = new List<QandA>();
        for (var stage = 0; stage < 4; stage++)
        {
            qs.Add(makeQuestion(Question.MemoryDisplay, "Memory", formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { displaySequence[stage].ToString() }));
            qs.Add(makeQuestion(Question.MemoryPosition, "Memory", formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { MemorySprites[indices[stage]] }, preferredWrongAnswers: MemorySprites));
            qs.Add(makeQuestion(Question.MemoryLabel, "Memory", formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { labels[stage][labels[stage].Length - 1].ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMetamorse(KMBombModule module)
    {
        var comp = GetComponent(module, "MetamorseScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldBigChar = GetField<char>(comp, "greaterLetter");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_Metamorse);
        addQuestion(module, Question.MetamorseExtractedLetter, correctAnswers: new[] { fldBigChar.Get().ToString() });
    }

    private IEnumerable<object> ProcessMetapuzzle(KMBombModule module)
    {
        var comp = GetComponent(module, "metapuzzleScript");
        var fldSolved = GetField<bool>(comp, "solved");

        var wordsType = comp.GetType().Assembly.GetType("SevenLetterWords") ?? throw new AbandonModuleException("I cannot find the SevenLetterWords type.");
        var words = GetStaticField<string[]>(wordsType, "List", isPublic: true).Get();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_Metapuzzle);

        var answer = GetField<string>(comp, "metaAnswer").Get(x => x.Length != 7 ? "expected length 7" : null);

        addQuestion(module, Question.MetapuzzleAnswer, correctAnswers: new[] { answer }, preferredWrongAnswers: words);
    }

    private IEnumerable<object> ProcessMicrocontroller(KMBombModule module)
    {
        var comp = GetComponent(module, "Micro");
        var fldSolved = GetIntField(comp, "solved");

        while (fldSolved.Get() == 0)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Microcontroller);

        var ledsOrder = GetListField<int>(comp, "LEDorder").Get(lst => lst.Count != 6 && lst.Count != 8 && lst.Count != 10 ? "unexpected length (expected 6, 8 or 10)" : null);
        var positionTranslate = GetArrayField<int>(comp, "positionTranslate").Get(expectedLength: ledsOrder.Count);

        addQuestions(module, ledsOrder.Select((led, ix) => makeQuestion(Question.MicrocontrollerPinOrder, _Microcontroller,
            formatArgs: new[] { ordinal(ix + 1) },
            correctAnswers: new[] { (positionTranslate[led] + 1).ToString() },
            preferredWrongAnswers: Enumerable.Range(1, ledsOrder.Count).Select(i => i.ToString()).ToArray())));
    }

    private IEnumerable<object> ProcessMinesweeper(KMBombModule module)
    {
        var comp = GetComponent(module, "MinesweeperModule");

        // Wait for activation as the above fields aren’t fully initialized until then
        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var propSolved = GetProperty<bool>(GetField<object>(comp, "Game").Get(), "Solved", isPublic: true);
        var color = GetField<string>(GetField<object>(comp, "StartingCell").Get(), "Color", isPublic: true).Get();

        while (!propSolved.Get())
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_Minesweeper);
        addQuestion(module, Question.MinesweeperStartingColor, correctAnswers: new[] { color });
    }

    private IEnumerable<object> ProcessMirror(KMBombModule module)
    {
        var comp = GetComponent(module, "mirror");
        var fldModuleReady = GetField<bool>(comp, "moduleReady");
        var candidateWords =
            GetStaticField<string[]>(comp.GetType(), "table1").Get().Concat(
            GetStaticField<string[]>(comp.GetType(), "table2").Get().Concat(
            GetStaticField<string[]>(comp.GetType(), "table3").Get())).ToArray();
        var fldModuleSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldModuleReady.Get())
            yield return null;

        var position = GetIntField(comp, "fontPosition").Get(min: 0, max: 2);
        var texts = GetArrayField<TextMesh>(comp, "mirrorTexts", isPublic: true).Get(expectedLength: 3);

        while (!fldModuleSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Mirror);

        addQuestion(module, Question.MirrorWord, correctAnswers: new[] { texts[position].text }, preferredWrongAnswers: candidateWords);
    }

    private IEnumerable<object> ProcessMisterSoftee(KMBombModule module)
    {
        var comp = GetComponent(module, "misterSoftee");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MisterSoftee);

        var iceCreams = GetArrayField<int>(comp, "iceCreamsPresent").Get();
        var iceCreamNames = GetStaticField<string[]>(comp.GetType(), "iceCreamNames").Get();
        var ix = Array.IndexOf(iceCreams, 14);
        var directions = new[] { "TL", "TM", "TR", "ML", "MM", "MR", "BL", "BM", "BR" };
        addQuestions(module,
            makeQuestion(Question.MisterSofteeSpongebobPosition, _MisterSoftee, correctAnswers: new[] { directions[ix] }),
            makeQuestion(Question.MisterSofteeTreatsPresent, _MisterSoftee, correctAnswers: iceCreams.Where(x => x != 14).Select(x => iceCreamNames[x]).ToArray()));
    }

    private IEnumerable<object> ProcessModernCipher(KMBombModule module)
    {
        var comp = GetComponent(module, "modernCipher");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        var dictionary = GetField<Dictionary<string, string>>(comp, "chosenWords").Get();

        if (!dictionary.TryGetValue("Stage1", out var stage1word) || stage1word == null || !dictionary.TryGetValue("Stage2", out var stage2word) || stage2word == null)
            throw new AbandonModuleException($"There is no word for {(stage1word == null ? "stage 1" : "stage 2")}.");

        stage1word = stage1word.Substring(0, 1).ToUpperInvariant() + stage1word.Substring(1).ToLowerInvariant();
        stage2word = stage2word.Substring(0, 1).ToUpperInvariant() + stage2word.Substring(1).ToLowerInvariant();

        _modulesSolved.IncSafe(_ModernCipher);
        addQuestions(module,
            makeQuestion(Question.ModernCipherWord, _ModernCipher, formatArgs: new[] { "first" }, correctAnswers: new[] { stage1word }, preferredWrongAnswers: new[] { stage2word }),
            makeQuestion(Question.ModernCipherWord, _ModernCipher, formatArgs: new[] { "second" }, correctAnswers: new[] { stage2word }, preferredWrongAnswers: new[] { stage1word }));
    }

    private IEnumerable<object> ProcessModuleListening(KMBombModule module)
    {
        var comp = GetComponent(module, "ModuleListening");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ModuleListening);

        var moduleNames = GetArrayField<string>(comp, "moduleNames").Get();
        var indices = GetArrayField<int>(comp, "moduleIndex").Get(validator: ar => ar.Length != 4 ? "expected length 4" : ar.Any(v => v < 0 || v >= moduleNames.Length) ? $"out of range for moduleNames (0–{moduleNames.Length - 1})" : null);
        var colorNames = GetArrayField<string>(comp, "buttonColors").Get(expectedLength: 4);
        var colorOrder = GetArrayField<int>(comp, "btnColors").Get(validator: ar => ar.Length != 4 ? "expected length 4" : ar.Any(v => v < 0 || v >= colorNames.Length) ? $"out of range for colorNames (0–{colorNames.Length - 1})" : null);
        var qs = new List<QandA>();
        for (int i = 0; i < 4; i++)
            qs.Add(makeQuestion(Question.ModuleListeningSounds, _ModuleListening, formatArgs: new[] { colorNames[colorOrder[i]] }, correctAnswers: new[] { moduleNames[indices[i]] }, preferredWrongAnswers: moduleNames));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessModuleMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "ModuleMazeModule");
        var fldSprites = GetArrayField<Sprite>(comp, "sprites", true);
        var fldSolved = GetField<bool>(comp, "solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ModuleMaze);

        addQuestions(module,
            makeQuestion(Question.ModuleMazeStartingIcon, _ModuleMaze,
                correctAnswers: new[] { GetField<Sprite>(comp, "souvenirStart").Get() }, preferredWrongAnswers: fldSprites.Get()));
    }

    private IEnumerable<object> ProcessMonsplodeFight(KMBombModule module)
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
                    _legitimatelyNoQuestions.Add(module);
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
        _modulesSolved.IncSafe(_MonsplodeFight);

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].OnInteract = origInteracts[i];

        // If either of these is the case, an error message will already have been output above (within the button handler)
        if (displayedCreature == null || displayedMoves == null)
            yield break;

        addQuestions(module,
            makeQuestion(Question.MonsplodeFightCreature, _MonsplodeFight, correctAnswers: new[] { displayedCreature }),
            makeQuestion(Question.MonsplodeFightMove, _MonsplodeFight, formatArgs: new[] { "was" }, correctAnswers: displayedMoves),
            makeQuestion(Question.MonsplodeFightMove, _MonsplodeFight, formatArgs: new[] { "was not" }, correctAnswers: GetAnswers(Question.MonsplodeFightMove).Except(displayedMoves).ToArray()));
    }

    private IEnumerable<object> ProcessMonsplodeTradingCards(KMBombModule module)
    {
        var comp = GetComponent(module, "MonsplodeCardModule");
        var fldStage = GetIntField(comp, "correctOffer", isPublic: true);

        var stageCount = GetIntField(comp, "offerCount", isPublic: true).Get(min: 3, max: 3);
        var data = GetField<object>(comp, "CD", isPublic: true).Get();
        var monsplodeNames = GetArrayField<string>(data, "names", isPublic: true).Get().Select(s => s.Replace("\r", "").Replace("\n", " ")).ToArray();

        while (fldStage.Get() < stageCount)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MonsplodeTradingCards);

        if (fldStage.Get() != stageCount)
            throw new AbandonModuleException($"Abandoning Monsplode Trading Cards because ‘correctOffer’ has unexpected value {fldStage.Get()} instead of {stageCount}.");

        var deck = GetField<Array>(comp, "deck", isPublic: true).Get(ar => ar.Length != 3 ? "expected length 3" : null).Cast<object>().ToArray();
        var offer = GetField<object>(comp, "offer", isPublic: true).Get();
        var fldMonsplode = GetIntField(offer, "monsplode", isPublic: true);
        var fldPrintDigit = GetIntField(offer, "printDigit", isPublic: true);
        var fldPrintChar = GetField<char>(offer, "printChar", isPublic: true);

        var monsplodeIds = new[] { fldMonsplode.Get(0, monsplodeNames.Length - 1) }.Concat(deck.Select(card => fldMonsplode.GetFrom(card, 0, monsplodeNames.Length - 1))).ToArray();
        var monsplodes = monsplodeIds.Select(mn => monsplodeNames[mn]).ToArray();
        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, formatArgs: new[] { "card on offer" }, correctAnswers: new[] { monsplodes[0] }, preferredWrongAnswers: monsplodeNames));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, formatArgs: new[] { "first card in your hand" }, correctAnswers: new[] { monsplodes[1] }, preferredWrongAnswers: monsplodeNames));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, formatArgs: new[] { "second card in your hand" }, correctAnswers: new[] { monsplodes[2] }, preferredWrongAnswers: monsplodeNames));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsCards, _MonsplodeTradingCards, formatArgs: new[] { "third card in your hand" }, correctAnswers: new[] { monsplodes[3] }, preferredWrongAnswers: monsplodeNames));

        var printVersions = new[] { fldPrintChar.Get() + "" + fldPrintDigit.Get() }.Concat(deck.Select(card => fldPrintChar.GetFrom(card) + "" + fldPrintDigit.GetFrom(card))).ToArray();
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, formatArgs: new[] { "card on offer" }, correctAnswers: new[] { printVersions[0] }, preferredWrongAnswers: printVersions));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, formatArgs: new[] { "first card in your hand" }, correctAnswers: new[] { printVersions[1] }, preferredWrongAnswers: printVersions));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, formatArgs: new[] { "second card in your hand" }, correctAnswers: new[] { printVersions[2] }, preferredWrongAnswers: printVersions));
        qs.Add(makeQuestion(Question.MonsplodeTradingCardsPrintVersions, _MonsplodeTradingCards, formatArgs: new[] { "third card in your hand" }, correctAnswers: new[] { printVersions[3] }, preferredWrongAnswers: printVersions));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMoon(KMBombModule module)
    {
        var comp = GetComponent(module, "theMoonScript");
        var fldStage = GetIntField(comp, "stage");

        // The Moon sets ‘stage’ to 9 when the module is solved.
        while (fldStage.Get() != 9)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Moon);

        var lightIndex = GetIntField(comp, "lightIndex").Get(min: 0, max: 7);
        var qNames = new[] { "first initially lit", "second initially lit", "third initially lit", "fourth initially lit", "first initially unlit", "second initially unlit", "third initially unlit", "fourth initially unlit" };
        var aNames = new[] { "south", "south-west", "west", "north-west", "north", "north-east", "east", "south-east" };
        addQuestions(module, Enumerable.Range(0, 8).Select(i => makeQuestion(Question.MoonLitUnlit, _Moon, formatArgs: new[] { qNames[i] }, correctAnswers: new[] { aNames[(i + lightIndex) % 8] })));
    }

    private IEnumerable<object> ProcessMoreCode(KMBombModule module)
    {
        var comp = GetComponent(module, "MoreCode");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MoreCode);

        var word = GetField<string>(comp, "SolutionWord").Get();
        word = word.Substring(0, 1) + word.Substring(1).ToLowerInvariant();
        addQuestion(module, Question.MoreCodeWord, correctAnswers: new[] { word });
    }

    private IEnumerable<object> ProcessMorseAMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "MorseAMaze");

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var start = GetField<string>(comp, "_souvenirQuestionStartingLocation").Get(str => str.Length != 2 ? "expected length 2" : null);
        var end = GetField<string>(comp, "_souvenirQuestionEndingLocation").Get(str => str.Length != 2 ? "expected length 2" : null);
        var word = GetField<string>(comp, "_souvenirQuestionWordPlaying").Get(str => str.Length < 4 ? "expected length ≥ 4" : null);
        var words = GetArrayField<string>(comp, "_souvenirQuestionWordList").Get(expectedLength: 36);

        var fldSolved = GetField<bool>(comp, "_solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_MorseAMaze);
        addQuestions(module,
            makeQuestion(Question.MorseAMazeStartingCoordinate, _MorseAMaze, correctAnswers: new[] { start }),
            makeQuestion(Question.MorseAMazeEndingCoordinate, _MorseAMaze, correctAnswers: new[] { end }),
            makeQuestion(Question.MorseAMazeMorseCodeWord, _MorseAMaze, correctAnswers: new[] { word }, preferredWrongAnswers: words));
    }

    private IEnumerable<object> ProcessMorseButtons(KMBombModule module)
    {
        var comp = GetComponent(module, "morseButtonsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        string alphabet = GetField<string>(comp, "alphabet").Get();
        var colorNames = new[] { "red", "blue", "green", "yellow", "orange", "purple" };
        int[] letters = GetArrayField<int>(comp, "letters").Get(expectedLength: 6, validator: x => x < 0 || x >= alphabet.Length ? "out of range" : null);
        int[] colors = GetArrayField<int>(comp, "colors").Get(expectedLength: 6, validator: x => x < 0 || x >= colorNames.Length ? "out of range" : null);

        _modulesSolved.IncSafe(_MorseButtons);
        addQuestions(module,
            makeQuestion(Question.MorseButtonsButtonLabel, _MorseButtons, formatArgs: new[] { "first" }, correctAnswers: new[] { alphabet[letters[0]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, _MorseButtons, formatArgs: new[] { "second" }, correctAnswers: new[] { alphabet[letters[1]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, _MorseButtons, formatArgs: new[] { "third" }, correctAnswers: new[] { alphabet[letters[2]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, _MorseButtons, formatArgs: new[] { "fourth" }, correctAnswers: new[] { alphabet[letters[3]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, _MorseButtons, formatArgs: new[] { "fifth" }, correctAnswers: new[] { alphabet[letters[4]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonLabel, _MorseButtons, formatArgs: new[] { "sixth" }, correctAnswers: new[] { alphabet[letters[5]].ToString() }, preferredWrongAnswers: alphabet.Select(x => x.ToString()).ToArray()),
            makeQuestion(Question.MorseButtonsButtonColor, _MorseButtons, formatArgs: new[] { "first" }, correctAnswers: new[] { colorNames[colors[0]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, _MorseButtons, formatArgs: new[] { "second" }, correctAnswers: new[] { colorNames[colors[1]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, _MorseButtons, formatArgs: new[] { "third" }, correctAnswers: new[] { colorNames[colors[2]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, _MorseButtons, formatArgs: new[] { "fourth" }, correctAnswers: new[] { colorNames[colors[3]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, _MorseButtons, formatArgs: new[] { "fifth" }, correctAnswers: new[] { colorNames[colors[4]].ToString() }, preferredWrongAnswers: colorNames),
            makeQuestion(Question.MorseButtonsButtonColor, _MorseButtons, formatArgs: new[] { "sixth" }, correctAnswers: new[] { colorNames[colors[5]].ToString() }, preferredWrongAnswers: colorNames));
    }

    private IEnumerable<object> ProcessMorsematics(KMBombModule module)
    {
        var comp = GetComponent(module, "AdvancedMorse");
        var fldSolved = GetField<bool>(comp, "solved");
        var chars = GetArrayField<string>(comp, "DisplayCharsRaw").Get(expectedLength: 3);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Morsematics);
        addQuestions(module, Enumerable.Range(0, 3).Select(i => makeQuestion(Question.MorsematicsReceivedLetters, _Morsematics, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { chars[i] }, preferredWrongAnswers: chars)));
    }

    private IEnumerable<object> ProcessMorseWar(KMBombModule module)
    {
        var comp = GetComponent(module, "MorseWar");
        var fldIsSolved = GetField<bool>(comp, "isSolved");

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MorseWar);

        var wordTable = GetStaticField<string[]>(comp.GetType(), "WordTable").Get();
        var rowTable = GetStaticField<string[]>(comp.GetType(), "RowTable").Get(ar => ar.Length != 6 ? "expected length 6" : null);
        var wordNum = GetIntField(comp, "wordNum").Get(min: 0, max: wordTable.Length - 1);
        var lights = GetField<string>(comp, "lights").Get(str => str.Length != 3 ? "expected length 3" : str.Any(ch => ch < '1' || ch > '6') ? "expected characters 1–6" : null);

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.MorseWarCode, _MorseWar, correctAnswers: new[] { wordTable[wordNum].ToUpperInvariant() }));
        var rowNames = new[] { "bottom", "middle", "top" };
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.MorseWarLeds, _MorseWar, formatArgs: new[] { rowNames[i] }, correctAnswers: new[] { rowTable[lights[i] - '1'] }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMouseInTheMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "MouseInTheMazeModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var sphereColors = GetArrayField<int>(comp, "_sphereColors").Get(expectedLength: 4);

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var goalPos = GetIntField(comp, "_goalPosition").Get(min: 0, max: 3);
        var torusColor = GetIntField(comp, "_torusColor").Get(min: 0, max: 3);
        var goalColor = sphereColors[goalPos];
        if (goalColor < 0 || goalColor > 3)
            throw new AbandonModuleException($"Unexpected color (torus={torusColor}; goal={goalColor})");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_MouseInTheMaze);
        addQuestions(module,
            makeQuestion(Question.MouseInTheMazeSphere, _MouseInTheMaze, correctAnswers: new[] { new[] { "white", "green", "blue", "yellow" }[goalColor] }),
            makeQuestion(Question.MouseInTheMazeTorus, _MouseInTheMaze, correctAnswers: new[] { new[] { "white", "green", "blue", "yellow" }[torusColor] }));
    }

    private IEnumerable<object> ProcessMSeq(KMBombModule module)
    {
        var comp = GetComponent(module, "MSeqScript");
        var fldState = GetField<object>(comp, "state"); //Uses an enum value.
        while (fldState.Get().ToString() != "Solved")
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MSeq);

        int[] obtainedDigits = GetArrayField<int>(comp, "obtainedDigits").Get(expectedLength: 3);
        int submittedNum = GetIntField(comp, "finalNumber").Get(min: 25, max: 225);

        addQuestions(module,
            makeQuestion(Question.MSeqObtained, _MSeq, correctAnswers: new[] { obtainedDigits[0].ToString() }, formatArgs: new[] { "first" }),
            makeQuestion(Question.MSeqObtained, _MSeq, correctAnswers: new[] { obtainedDigits[1].ToString() }, formatArgs: new[] { "second" }),
            makeQuestion(Question.MSeqObtained, _MSeq, correctAnswers: new[] { obtainedDigits[2].ToString() }, formatArgs: new[] { "third" }),
            makeQuestion(Question.MSeqSubmitted, _MSeq, correctAnswers: new[] { submittedNum.ToString() }));
    }

    private IEnumerable<object> ProcessMulticoloredSwitches(KMBombModule module)
    {
        var comp = GetComponent(module, "MultiColoredSwitches");
        var fldSolved = GetField<bool>(comp, "Solved");
        var fldLedsUp = GetField<Array>(comp, "LEDsUp");
        var fldLedsDown = GetField<Array>(comp, "LEDsDown");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MulticoloredSwitches);

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
                    qs.Add(makeQuestion(Question.MulticoloredSwitchesLedColor, _MulticoloredSwitches,
                        formatArgs: new[] { ordinal(led + 1), upDown == 0 ? "top" : "bottom", cycle == 0 ? "lit" : "unlit" },
                        correctAnswers: new[] { colorNames[colorChars.IndexOf((upDown == 0 ? upColors : downColors)[led][cycle])] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessMurder(KMBombModule module)
    {
        var comp = GetComponent(module, "MurderModule");
        var fldSolved = GetField<bool>(comp, "isSolved");

        // Just a consistency check
        GetIntField(comp, "suspects").Get(min: 4, max: 4);
        GetIntField(comp, "weapons").Get(min: 4, max: 4);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Murder);

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
            makeQuestion(Question.MurderSuspect, _Murder,
                formatArgs: new[] { "a suspect but not the murderer" },
                correctAnswers: Enumerable.Range(0, 6).Where(suspectIx => skipDisplay[0, suspectIx] == 0 && suspectIx != actualSuspect).Select(suspectIx => names[0, suspectIx]).ToArray()),
            makeQuestion(Question.MurderSuspect, _Murder,
                formatArgs: new[] { "not a suspect" },
                correctAnswers: Enumerable.Range(0, 6).Where(suspectIx => skipDisplay[0, suspectIx] == 1).Select(suspectIx => names[0, suspectIx]).ToArray()),

            makeQuestion(Question.MurderWeapon, _Murder,
                formatArgs: new[] { "a potential weapon but not the murder weapon" },
                correctAnswers: Enumerable.Range(0, 6).Where(weaponIx => skipDisplay[1, weaponIx] == 0 && weaponIx != actualWeapon).Select(weaponIx => names[1, weaponIx]).ToArray()),
            makeQuestion(Question.MurderWeapon, _Murder,
                formatArgs: new[] { "not a potential weapon" },
                correctAnswers: Enumerable.Range(0, 6).Where(weaponIx => skipDisplay[1, weaponIx] == 1).Select(weaponIx => names[1, weaponIx]).ToArray()),

            bodyFound == actualRoom ? null : makeQuestion(Question.MurderBodyFound, _Murder, correctAnswers: new[] { names[2, bodyFound] }));
    }

    private IEnumerable<object> ProcessMysticSquare(KMBombModule module)
    {
        var comp = GetComponent(module, "MysticSquareModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var skull = GetField<Transform>(comp, "Skull", true).Get();

        while (!skull.gameObject.activeSelf)
            yield return null;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_MysticSquare);

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

    private IEnumerable<object> ProcessMysteryModule(KMBombModule module)
    {
        var comp = GetComponent(module, "MysteryModuleScript");
        var fldKeyModules = GetListField<KMBombModule>(comp, "keyModules");
        var fldMystifiedModule = GetField<KMBombModule>(comp, "mystifiedModule");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldAnimating = GetField<bool>(comp, "animating");
        var fldFailsolve = GetField<bool>(comp, "failsolve");

        while (fldKeyModules.Get(nullAllowed: true) == null && !fldFailsolve.Get())
            yield return null;
        while (fldMystifiedModule.Get(nullAllowed: true) == null && !fldFailsolve.Get())
            yield return null;

        if (fldFailsolve.Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Mystery Module because no module was hidden.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var keyModule = fldKeyModules.Get(ar => ar.Count == 0 ? "empty" : null)[0];
        var mystifiedModule = fldMystifiedModule.Get();

        // Do not ask questions while Souvenir is hidden by Mystery Module.
        if (mystifiedModule == Module)
            _avoidQuestions++;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_MysteryModule);

        // Do not ask questions during the solve animation, since Mystery Module modifies the scaling of this module.
        while (fldAnimating.Get())
            yield return new WaitForSeconds(.1f);

        addQuestions(module,
            makeQuestion(Question.MysteryModuleFirstKey, _MysteryModule, correctAnswers: new[] { keyModule.ModuleDisplayName }, preferredWrongAnswers: Bomb.GetSolvableModuleNames().ToArray()),
            makeQuestion(Question.MysteryModuleHiddenModule, _MysteryModule, correctAnswers: new[] { mystifiedModule.ModuleDisplayName }, preferredWrongAnswers: Bomb.GetSolvableModuleNames().ToArray()));

        if (mystifiedModule == Module)
            _avoidQuestions--;
    }
}