using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessSafetySquare(ModuleData module)
    {
        var comp = GetComponent(module, "SafetySquareScript");

        var specialRules = new Dictionary<string, string>
        {
            [" "] = "No special rule",
            ["W"] = "Reacts with water",
            ["SA"] = "Simple asphyxiant",
            ["OX"] = "Oxidizer"
        };
        var colors = new[] { "red", "yellow", "blue" };
        var digits = colors.Select(col => GetField<TextMesh>(comp, $"{col}Text", isPublic: true).Get(mesh => mesh.text.Length != 1 || !"01234".Contains(mesh.text) ? $"text value was \"{mesh.text}\", but expected a single digit from 0-4." : null).text).ToArray();
        var symbol = GetField<TextMesh>(comp, "whiteText", isPublic: true).Get(mesh => !specialRules.ContainsKey(mesh.text) ? $"text value was \"{mesh.text}\", but expected one of {specialRules.Keys.JoinString(", ", "\"", "\"", " or ")}" : null).text;

        yield return WaitForSolve;

        var qs = colors.Select((col, ix) => makeQuestion(Question.SafetySquareDigits, module, formatArgs: new[] { col }, correctAnswers: new[] { digits[ix] })).ToList();
        qs.Add(makeQuestion(Question.SafetySquareSpecialRule, module, correctAnswers: new[] { specialRules[symbol] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSamsung(ModuleData module)
    {
        var comp = GetComponent(module, "theSamsung");
        yield return WaitForSolve;

        var appPositions = GetListField<int>(comp, "positionNumbers").Get();
        var appNames = new[] { "Duolingo", "Google Maps", "Kindle", "Google Authenticator", "Photomath", "Spotify", "Google Arts & Culture", "Discord" };
        var qs = new List<QandA>();
        for (int i = 0; i < 8; i++)
            qs.Add(makeQuestion(Question.SamsungAppPositions, module, formatArgs: new[] { appNames[i] }, correctAnswers: new[] { Question.SamsungAppPositions.GetAnswers()[appPositions[i]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSaturn(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "SaturnScript");

        var hideButton = GetField<KMSelectable>(comp, "HideButton", true).Get();
        if (!TwitchPlaysActive && hideButton.OnInteract is not null)
            StartCoroutine(GetMethod<IEnumerator>(comp, "HidePlanet", 0).Invoke());
        hideButton.OnInteract = null;

        var index = GetIntField(comp, "EndIndex").Get(min: 0, max: 64 * 5);
        var outer = GetField<bool>(comp, "EndOuter").Get();

        addQuestion(module, Question.SaturnGoal, correctAnswers: new[] { $"{(outer ? 9 : 4) - (index / 64)} {index % 64}" });
    }

    private List<List<int>> _sbemailSongsDisplays = new();
    private IEnumerator<YieldInstruction> ProcessSbemailSongs(ModuleData module)
    {
        var comp = GetComponent(module, "_sbemailsongs");
        const string moduleId = "sbemailsongs";

        var fldDisplayedSongNumbers = GetListField<int>(comp, "stages", isPublic: true);
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);
        yield return null; // Wait one frame to make sure the Display field has been set.

        var myDisplay = fldDisplayedSongNumbers.Get(minLength: 0, validator: d => d < 1 || d > 209 ? "expected range 1-209" : null);
        if (_sbemailSongsDisplays.Any() && myDisplay.Count != _sbemailSongsDisplays[0].Count)
            throw new AbandonModuleException("The number of stages in each ‘Display’ is inconsistent.");
        _sbemailSongsDisplays.Add(myDisplay);

        var totalNonIgnoredSbemailSongs = GetIntField(comp, "totalNonIgnored").Get();

        if (myDisplay.Count == 0 || totalNonIgnoredSbemailSongs == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Sbemail Songs because there were no stages.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);
        module.SolveIndex = 1;

        var myIgnoredList = GetArrayField<string>(comp, "ignoredModules").Get();
        var displayedStageCount = Bomb.GetSolvedModuleNames().Count(x => !myIgnoredList.Contains(x));

        if (_sbemailSongsDisplays.Count != _moduleCounts[moduleId])
            throw new AbandonModuleException("The number of displays did not match the number of Sbemail Songs modules.");

        var transcriptionsAbridged = GetArrayField<string>(comp, "transcriptionsAbridged").Get(expectedLength: 209);

        if (_moduleCounts[moduleId] == 1)
            addQuestions(module, myDisplay.Take(displayedStageCount).Select((digit, ix) => makeQuestion(Question.SbemailSongsSongs, module, formatArgs: new[] { (ix + 1).ToString("X2") }, correctAnswers: new[] { transcriptionsAbridged[digit - 1] }, preferredWrongAnswers: transcriptionsAbridged)));
        else
        {
            var uniqueStages = Enumerable.Range(1, displayedStageCount).Where(stage => _sbemailSongsDisplays.Count(display => display[stage - 1] == myDisplay[stage - 1]) == 1).Take(2).ToArray();
            if (uniqueStages.Length == 0 || displayedStageCount == 1)
            {
                Debug.Log($"[Souvenir #{_moduleId}] No question for Sbemail Songs because there are not enough stages at which at least one of them had a unique displayed number.");
                _legitimatelyNoQuestions.Add(module.Module);
            }
            else
            {
                var qs = new List<QandA>();
                for (int stage = 0; stage < displayedStageCount; stage++)
                {
                    var uniqueStage = uniqueStages.FirstOrDefault(s => s != stage + 1);
                    if (uniqueStage != 0)
                        qs.Add(makeQuestion(Question.SbemailSongsSongs, moduleId, 0,
                            formattedModuleName: string.Format(translateString(Question.SbemailSongsSongs,
                                "the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)"),
                                transcriptionsAbridged[myDisplay[uniqueStage - 1] - 1],
                                uniqueStage.ToString("X2")), formatArgs: new[] { (stage + 1).ToString("X2") },
                            correctAnswers: new[] { transcriptionsAbridged[myDisplay[stage] - 1] },
                            preferredWrongAnswers: transcriptionsAbridged));
                }
                addQuestions(module, qs);
            }
        }
    }

    private IEnumerator<YieldInstruction> ProcessScavengerHunt(ModuleData module)
    {
        var comp = GetComponent(module, "scavengerHunt");
        var keySquare = GetIntField(comp, "keySquare").Get(min: 0, max: 15);

        // Coordinates of the color that the user needed
        var relTiles = GetArrayField<int>(comp, "relTiles").Get(expectedLength: 2, validator: v => v < 0 || v > 15 ? "expected range 0–15" : null);

        // Coordinates of the other colors
        var decoyTiles = GetArrayField<int>(comp, "decoyTiles").Get(expectedLength: 4, validator: v => v < 0 || v > 15 ? "expected range 0–15" : null);

        // Which color is the ‘relTiles’ color
        var colorIndex = GetIntField(comp, "colorIndex").Get(min: 0, max: 2);

        // 0 = red, 1 = green, 2 = blue
        var redTiles = colorIndex == 0 ? relTiles : decoyTiles.Take(2).ToArray();
        var greenTiles = colorIndex == 1 ? relTiles : colorIndex == 0 ? decoyTiles.Take(2).ToArray() : decoyTiles.Skip(2).ToArray();
        var blueTiles = colorIndex == 2 ? relTiles : decoyTiles.Skip(2).ToArray();

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.ScavengerHuntKeySquare, module, correctAnswers: new[] { new Coord(4, 4, keySquare) }),
            makeQuestion(Question.ScavengerHuntColoredTiles, module, formatArgs: new[] { "red" }, correctAnswers: redTiles.Select(c => new Coord(4, 4, c)).ToArray()),
            makeQuestion(Question.ScavengerHuntColoredTiles, module, formatArgs: new[] { "green" }, correctAnswers: greenTiles.Select(c => new Coord(4, 4, c)).ToArray()),
            makeQuestion(Question.ScavengerHuntColoredTiles, module, formatArgs: new[] { "blue" }, correctAnswers: blueTiles.Select(c => new Coord(4, 4, c)).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessSchlagDenBomb(ModuleData module)
    {
        var comp = GetComponent(module, "qSchlagDenBomb");
        yield return WaitForSolve;

        var contestantName = GetField<string>(comp, "contestantName").Get();
        var contestantScore = GetIntField(comp, "scoreC").Get(min: 0, max: 75);
        var bombScore = GetIntField(comp, "scoreB").Get(min: 0, max: 75);

        addQuestions(module,
            makeQuestion(Question.SchlagDenBombContestantName, module, correctAnswers: new[] { contestantName }),
            makeQuestion(Question.SchlagDenBombContestantScore, module, correctAnswers: new[] { contestantScore.ToString() }, preferredWrongAnswers:
               Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(0, 75).ToString()).Distinct().Take(6).ToArray()),
            makeQuestion(Question.SchlagDenBombBombScore, module, correctAnswers: new[] { bombScore.ToString() }, preferredWrongAnswers:
               Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(0, 75).ToString()).Distinct().Take(6).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessScramboozledEggain(ModuleData module)
    {
        var comp = GetComponent(module, "ScramboozledEggainScript");
        yield return WaitForSolve;

        var wordList = GetStaticField<string[]>(comp.GetType(), "_wordList").Get().Select(i => i.Substring(0, 1) + i.Substring(1).ToLowerInvariant()).ToArray();
        var selectedWords = GetArrayField<string>(comp, "_selectedWords").Get().Select(i => i.Substring(0, 1) + i.Substring(1).ToLowerInvariant()).ToArray();

        var qs = new List<QandA>();
        for (int i = 0; i < 4; i++)
            qs.Add(makeQuestion(Question.ScramboozledEggainWord, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { selectedWords[i] }, preferredWrongAnswers: wordList));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessScrutinySquares(ModuleData module)
    {
        var comp = GetComponent(module, "ScrutinySquaresScript");
        yield return WaitForSolve;

        var pathCells = GetField<IList>(comp, "pathCells").Get();
        var direction = GetField<Enum>(pathCells[0], "direction", isPublic: true).Get();
        var answer = direction.ToString() switch
        {
            "Up" => "Word",
            "Left" => "Color around word",
            "Right" => "Color of background",
            "Down" => "Color of word",
            _ => throw new AbandonModuleException($"Unexpected value of ‘direction’: {direction}")
        };
        addQuestion(module, Question.ScrutinySquaresFirstDifference, correctAnswers: new[] { answer });
    }

    private IEnumerator<YieldInstruction> ProcessScripting(ModuleData module)
    {
        var comp = GetComponent(module, "KritScript");

        yield return WaitForSolve;

        var variableType = GetField<string>(comp, "VariableKindValue", isPublic: true).Get();
        addQuestion(module, Question.ScriptingVariableDataType, correctAnswers: new[] { variableType });
    }

    private IEnumerator<YieldInstruction> ProcessSeaShells(ModuleData module)
    {
        var comp = GetComponent(module, "SeaShellsModule");
        var fldRow = GetIntField(comp, "row");
        var fldCol = GetIntField(comp, "col");
        var fldKeynum = GetIntField(comp, "keynum");
        var fldStage = GetIntField(comp, "stage");
        var fldDisplay = GetField<TextMesh>(comp, "Display", isPublic: true);

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var rows = new int[3];
        var cols = new int[3];
        var keynums = new int[3];
        while (true)
        {
            while (fldDisplay.Get().text == " ")
            {
                yield return new WaitForSeconds(.1f);
                if (module.IsSolved)
                    goto solved;
            }

            var stage = fldStage.Get(min: 0, max: 2);
            rows[stage] = fldRow.Get();
            cols[stage] = fldCol.Get();
            keynums[stage] = fldKeynum.Get();

            while (fldDisplay.Get().text != " ")
            {
                yield return new WaitForSeconds(.1f);
                if (module.IsSolved)
                    goto solved;
            }
        }

        solved:
        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.SeaShells1, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { new[] { "she sells", "she shells", "sea shells", "sea sells" }[rows[i]] }));
            qs.Add(makeQuestion(Question.SeaShells2, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { new[] { "sea shells", "she shells", "sea sells", "she sells" }[cols[i]] }));
            qs.Add(makeQuestion(Question.SeaShells3, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { new[] { "sea shore", "she sore", "she sure", "seesaw" }[keynums[i]] }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSemamorse(ModuleData module)
    {
        var comp = GetComponent(module, "semamorse");
        yield return WaitForSolve;

        var letters = GetArrayField<int[]>(comp, "displayedLetters").Get(expectedLength: 2, validator: arr => arr.Length != 5 ? "expected length 5" : arr.Any(v => v < 0 || v > 25) ? "expected range 0–25" : null);
        var relevantIx = Enumerable.Range(0, letters[0].Length).First(ix => letters[0][ix] != letters[1][ix]);
        var colorNames = new[] { "red", "green", "cyan", "indigo", "pink" };
        var colors = GetArrayField<int>(comp, "displayedColors").Get(expectedLength: 5, validator: c => c < 0 || c >= colorNames.Length ? $"expected range 0–{colorNames.Length - 1}" : null);
        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.SemamorseColor, module, correctAnswers: new[] { colorNames[colors[relevantIx]] }));
        qs.Add(makeQuestion(Question.SemamorseLetters, module, formatArgs: new[] { "semaphore" }, correctAnswers: new[] { ((char) ('A' + letters[0][relevantIx])).ToString() }));
        qs.Add(makeQuestion(Question.SemamorseLetters, module, formatArgs: new[] { "Morse" }, correctAnswers: new[] { ((char) ('A' + letters[1][relevantIx])).ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSequencyclopedia(ModuleData module)
    {
        var comp = GetComponent(module, "TheSequencyclopediaScript");
        yield return WaitForSolve;

        var maxSeqId = int.Parse(GetField<string>(comp, "Tridal").Get(str => str == "" ? "Tridal is empty, meaning module was unable to gather the amount of sequence" : null));
        var answer = GetField<string>(comp, "APass").Get();
        var wrongAnswers = new HashSet<string>();
        wrongAnswers.Add(answer);
        while (wrongAnswers.Count < 6)
            foreach (var ans in new AnswerGenerator.Integers(0, maxSeqId, "'A'000000").GetAnswers(this).Take(6 - wrongAnswers.Count))
                wrongAnswers.Add(ans);

        addQuestion(module, Question.SequencyclopediaSequence, correctAnswers: new[] { answer }, preferredWrongAnswers: wrongAnswers.ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessSetTheory(ModuleData module)
    {
        var comp = GetComponent(module, "SetTheoryScript");
        var fldEquations = GetField<Array>(comp, "_equations");
        var mthGenerate = GetMethod<object>(comp, "GenerateEquationForStage", 1);

        yield return WaitForSolve;

        var equations = fldEquations.Get(v => v.Length != 4 ? "expected length 4" : null).Cast<object>().Select(eq => eq.ToString()).ToArray();
        addQuestions(module, Enumerable.Range(0, 4).Select(stage =>
        {
            var wrongAnswers = new HashSet<string> { equations[stage] };
            while (wrongAnswers.Count < 4)
                wrongAnswers.Add(mthGenerate.Invoke(stage).ToString());
            return makeQuestion(Question.SetTheoryEquations, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { equations[stage] }, preferredWrongAnswers: wrongAnswers.ToArray());
        }));
    }

    private IEnumerator<YieldInstruction> ProcessShapesAndBombs(ModuleData module)
    {
        var comp = GetComponent(module, "ShapesBombs");
        var initialLetter = GetIntField(comp, "selectLetter").Get(min: 0, max: 14);

        yield return WaitForSolve;

        var letterChars = new[] { "A", "B", "D", "E", "G", "I", "K", "L", "N", "O", "P", "S", "T", "X", "Y" };
        addQuestion(module, Question.ShapesAndBombsInitialLetter, correctAnswers: new[] { letterChars[initialLetter] });
    }

    private IEnumerator<YieldInstruction> ProcessShapeShift(ModuleData module)
    {
        var comp = GetComponent(module, "ShapeShiftModule");
        yield return WaitForSolve;

        var stL = GetIntField(comp, "startL").Get();
        var stR = GetIntField(comp, "startR").Get();
        var solL = GetIntField(comp, "solutionL").Get();
        var solR = GetIntField(comp, "solutionR").Get();
        var answers = new HashSet<string>();
        for (int l = 0; l < 4; l++)
            if (stL != solL || l == stL)
                for (int r = 0; r < 4; r++)
                    if (stR != solR || r == stR)
                        answers.Add(((char) ('A' + r + (4 * l))).ToString());
        if (answers.Count < 4)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Shape Shift because the answer was the same as the initial state.");
            _legitimatelyNoQuestions.Add(module.Module);
        }
        else
            addQuestion(module, Question.ShapeShiftInitialShape, correctAnswers: new[] { ((char) ('A' + stR + (4 * stL))).ToString() }, preferredWrongAnswers: answers.ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessShiftedMaze(ModuleData module)
    {
        var comp = GetComponent(module, "shiftedMazeScript");
        var expectedCBTexts = new[] { "W", "B", "Y", "M", "G" };
        var colorNames = new[] { "White", "Blue", "Yellow", "Magenta", "Green" };
        var cornerNames = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };

        var colorblindTexts = GetArrayField<TextMesh>(comp, "colorblindTexts", isPublic: true).Get(expectedLength: 4).Select(c => c.text).ToArray();
        var invalid = colorblindTexts.IndexOf(c => !expectedCBTexts.Contains(c));
        if (invalid != -1)
            throw new AbandonModuleException($"Found unexpected color text: “{colorblindTexts[invalid]}”.");

        yield return WaitForSolve;

        addQuestions(module, Enumerable.Range(0, 4).Select(corner => makeQuestion(Question.ShiftedMazeColors, module,
            formatArgs: new[] { cornerNames[corner] }, correctAnswers: new[] { colorNames[Array.IndexOf(expectedCBTexts, colorblindTexts[corner])] })));
    }

    private IEnumerator<YieldInstruction> ProcessShiftingMaze(ModuleData module)
    {
        var comp = GetComponent(module, "ShiftingMazeScript");
        var seedTextMesh = GetField<TextMesh>(comp, "Seedling", isPublic: true).Get();
        var seed = seedTextMesh.text;

        var hadStrike = false;
        module.Module.OnStrike += delegate { hadStrike = true; return false; };

        while (module.Unsolved)
        {
            if (hadStrike)
            {
                seed = seedTextMesh.text;
                hadStrike = false;
            }
            yield return null;
        }

        var seedSplit = Regex.Replace(seed, " ", "").Split(':');
        addQuestion(module, Question.ShiftingMazeSeed, correctAnswers: new[] { seedSplit[1] });
    }

    private IEnumerator<YieldInstruction> ProcessShogiIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "ShogiIdentificationScript");
        yield return WaitForSolve;

        var fldPiece = GetField<object>(comp, "chosenPiece");
        var propName = GetProperty<string>(fldPiece.Get(), "name", isPublic: true);

        addQuestion(module, Question.ShogiIdentificationPiece, correctAnswers: new[] { propName.Get() });
    }

    private IEnumerator<YieldInstruction> ProcessSignLanguage(ModuleData module)
    {
        var comp = GetComponent(module, "SignLanguageAlphabetScript");
        yield return WaitForSolve;

        var entryObj = GetField<object>(comp, "entry").Get();
        var answer = GetField<string>(entryObj, "word").Get();

        addQuestion(module, Question.SignLanguageWord, correctAnswers: new[] { answer });
    }

    private IEnumerator<YieldInstruction> ProcessSillySlots(ModuleData module)
    {
        var comp = GetComponent(module, "SillySlots");
        yield return WaitForSolve;

        var prevSlots = GetField<IList>(comp, "mPreviousSlots").Get(lst => lst.Cast<object>().Any(obj => obj is not Array ar || ar.Length != 3) ? "expected arrays of length 3" : null);
        if (prevSlots.Count < 2)
        {
            // Legitimate: first stage was a keep already
            Debug.Log($"[Souvenir #{_moduleId}] No question for Silly Slots because there was only one stage.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        var testSlot = ((Array) prevSlots[0]).GetValue(0);
        var fldShape = GetField<object>(testSlot, "shape", isPublic: true);
        var fldColor = GetField<object>(testSlot, "color", isPublic: true);

        var qs = new List<QandA>();
        // Skip the last stage because if the last action was Keep, it is still visible on the module
        for (int stage = 0; stage < prevSlots.Count - 1; stage++)
        {
            var slotStrings = ((Array) prevSlots[stage]).Cast<object>().Select(obj => (fldColor.GetFrom(obj).ToString() + " " + fldShape.GetFrom(obj).ToString()).ToLowerInvariant()).ToArray();
            for (int slot = 0; slot < slotStrings.Length; slot++)
                qs.Add(makeQuestion(Question.SillySlots, module, formatArgs: new[] { Ordinal(slot + 1), Ordinal(stage + 1) }, correctAnswers: new[] { slotStrings[slot] }, preferredWrongAnswers: slotStrings));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSiloAuthorization(ModuleData module)
    {
        var comp = GetComponent(module, "WarGamesModuleScript");
        yield return WaitForSolve;
        var qs = new List<QandA>();

        var messageColor = GetField<object>(comp, "correctColor").Get();
        var colorNames = new[] { "Red-Alpha", "Yellow-Alpha", "Green-Alpha" };
        var correctColor = messageColor.ToString() == "Red" ? colorNames[0] : messageColor.ToString() == "Yellow" ? colorNames[1] : colorNames[2];
        qs.Add(makeQuestion(Question.SiloAuthorizationMessageType, module, correctAnswers: new[] { correctColor }, preferredWrongAnswers: colorNames));

        var outMessages = GetArrayField<string>(comp, "outMessages").Get();
        var messages = new[] { outMessages[0], outMessages[2] };
        for (int message = 0; message < 2; message++)
            qs.Add(makeQuestion(Question.SiloAuthorizationEncryptedMessage, module, formatArgs: new[] { Ordinal(message + 1) }, correctAnswers: new[] { messages[message] }, preferredWrongAnswers: messages));

        qs.Add(makeQuestion(Question.SiloAuthorizationAuthCode, module, correctAnswers: new[] { GetField<int>(comp, "outAuthCode").Get().ToString("0000") }));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonSaid(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSaidScript");
        yield return WaitForSolve;

        var btnColors = GetListField<int>(comp, "btnColors").Get();
        var colorNames = GetArrayField<string>(comp, "colorNames").Get();
        var correctPresses = GetListField<int>(comp, "correctBtnPresses").Get();

        addQuestions(module, correctPresses.Select((val, ix) => makeQuestion(Question.SimonSaidPresses, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { colorNames[btnColors[val]] })));
    }

    private IEnumerator<YieldInstruction> ProcessSimonSamples(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSamples");
        yield return WaitForSolve;

        var calls = GetListField<string>(comp, "_calls").Get(expectedLength: 3);
        if (Enumerable.Range(1, 2).Any(i => calls[i].Length <= calls[i - 1].Length || !calls[i].StartsWith(calls[i - 1])))
            throw new AbandonModuleException($"_calls=[{calls.Select(c => $"“{c}”").JoinString(", ")}]; expected each element to start with the previous.");
        var possibleCalls = "0012|0112|0212|0213|0011|0211|0312|0313|0011|1010|1221|0232".Split('|');
        var check = Enumerable.Range(0, 3)
            .Select(i => calls[i].Substring(4 * i))
            .Select((s, i) => !possibleCalls.Skip(4 * i).Take(4).Contains(s) ? $"Invalid call for stage {i + 1}: {s}" : null)
            .Where(s => s is not null)
            .Aggregate<string, string>(null, (a, b) => a is null ? b : a + "; " + b);
        if (check is not null)
            throw new AbandonModuleException(check);

        var formatArgs = new[] { "played in the first stage", "added in the second stage", "added in the third stage" };
        addQuestions(module, calls.Select((c, ix) =>
            makeQuestion(Question.SimonSamplesSamples, module, formatArgs: new[] { formatArgs[ix] },
            correctAnswers: new[] { SimonSamplesAudio[Array.IndexOf(possibleCalls, c.Substring(ix * 4))] }, preferredWrongAnswers: SimonSamplesAudio.Skip(4 * ix).Take(4).ToArray())));
    }

    private IEnumerator<YieldInstruction> ProcessSimonSays(ModuleData module)
    {
        var comp = GetComponent(module, "SimonComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        module.SolveIndex = _modulesSolved.IncSafe("Simon");

        var colorNames = new[] { "red", "blue", "green", "yellow" };
        var sequence = GetArrayField<int>(comp, "currentSequence").Get(validator: arr => arr.Any(i => i < 0 || i >= colorNames.Length) ? "expected values 0–3" : null);
        addQuestions(module, Enumerable.Range(0, sequence.Length).Select(i =>
            makeQuestion(Question.SimonSaysFlash, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { colorNames[sequence[i]] })));
    }

    private IEnumerator<YieldInstruction> ProcessSimonScrambles(ModuleData module)
    {
        var comp = GetComponent(module, "simonScramblesScript");
        yield return WaitForSolve;

        int[] sequence = GetArrayField<int>(comp, "sequence").Get(expectedLength: 10);
        string[] colors = GetArrayField<string>(comp, "colorNames").Get(expectedLength: 4);

        if (sequence[9] < 0 || sequence[9] >= colors.Length)
            throw new AbandonModuleException($"‘sequence[9]’ points to illegal color: {sequence[9]} (expected 0-3).");

        addQuestions(module, sequence.Select((val, ix) => makeQuestion(Question.SimonScramblesColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { colors[val] })));
    }

    private IEnumerator<YieldInstruction> ProcessSimonScreams(ModuleData module)
    {
        var comp = GetComponent(module, "SimonScreamsModule");
        yield return WaitForSolve;

        var seqs = GetArrayField<int[]>(comp, "_sequences").Get(expectedLength: 3);
        var stageIxs = GetArrayField<int>(comp, "_stageIxs").Get(expectedLength: 3);
        var rules = GetField<Array>(comp, "_rowCriteria").Get(ar => ar.Length != 6 ? "expected length 6" : null);
        var colorsRaw = GetField<Array>(comp, "_colors").Get(ar => ar.Length != 6 ? "expected length 6" : null);     // array of enum values
        var colors = colorsRaw.Cast<object>().Select(obj => obj.ToString()).ToArray();

        var qs = new List<QandA>();
        var lastSeq = seqs.Last();
        foreach (var i in stageIxs)     // Only ask about the flashing colors that were relevant in the big table
            qs.Add(makeQuestion(Question.SimonScreamsFlashing, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { colors[lastSeq[i]] }));

        // First determine which rule applied in which stage
        var fldCheck = GetField<Func<int[], bool>>(rules.GetValue(0), "Check", isPublic: true);
        var fldRuleName = GetField<string>(rules.GetValue(0), "Name", isPublic: true);
        var fldSouvenirCode = GetField<int>(rules.GetValue(0), "SouvenirCode", isPublic: true);
        var stageRules = new int[seqs.Length];
        for (int i = 0; i < seqs.Length; i++)
        {
            stageRules[i] = rules.Cast<object>().IndexOf(rule => fldCheck.GetFrom(rule)(seqs[i]));
            if (stageRules[i] == -1)
                throw new AbandonModuleException($"Apparently none of the criteria applies to Stage {i + 1} ([{seqs[i].Select(ix => colors[ix]).JoinString(", ")}]).");
        }

        // Now set the questions
        // Skip the last rule because it’s the “otherwise” row
        var colorNames = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple" };
        for (int rule = 0; rule < rules.Length - 1; rule++)
        {
            var applicableStages = new List<string>();
            for (int stage = 0; stage < stageRules.Length; stage++)
                if (stageRules[stage] == rule)
                    applicableStages.Add(Ordinal(stage + 1));
            if (applicableStages.Count > 0)
            {
                var code = fldSouvenirCode.GetFrom(rules.GetValue(rule));
                if (code == 0)
                    qs.Add(makeQuestion(Question.SimonScreamsRuleSimple, module,
                        formatArgs: new[] { fldRuleName.GetFrom(rules.GetValue(rule)) },
                        correctAnswers: new[] { applicableStages.Count == stageRules.Length ? "all of them" : applicableStages.JoinString(", ", lastSeparator: " and ") }));
                else
                {
                    var color1 = colorNames[code >> 7];
                    var color2 = colorNames[(code >> 4) & 7];
                    var color3 = colorNames[(code >> 1) & 7];
                    qs.Add(makeQuestion(Question.SimonScreamsRuleComplex, module,
                        formatArgs: new[] { (code & 1) != 0 ? "at least two colors" : "at most one color", color1, color2, color3 },
                        correctAnswers: new[] { applicableStages.Count == stageRules.Length ? "all of them" : applicableStages.JoinString(", ", lastSeparator: " and ") }));
                }
            }
        }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonSelects(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSelectsScript");
        yield return WaitForSolve;

        var order = Enumerable.Range(0, 3).Select(i => GetArrayField<int>(comp, $"stg{i + 1}order").Get(minLength: 3, maxLength: 5)).ToArray();
        var btnRenderers = GetArrayField<Renderer>(comp, "buttonrend", isPublic: true).Get(expectedLength: 8);

        // Sequences of colors that flashes in each stage
        var seqs = new string[3][];

        // Parsing the received string
        for (int stage = 0; stage < 3; stage++)
        {
            var parsedString = new string[order[stage].Length];
            for (int flash = 0; flash < order[stage].Length; flash++)
                parsedString[flash] = btnRenderers[order[stage][flash]].material.name.Replace(" (Instance)", "");
            seqs[stage] = parsedString;
        }

        // Used to validate colors
        string[] colorNames = { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Magenta", "Cyan" };

        if (seqs.Any(seq => seq.Any(color => !colorNames.Contains(color))))
            throw new AbandonModuleException($"‘colors’ contains an invalid color: [{seqs.Select(seq => seq.JoinString(", ")).JoinString("; ")}]");

        addQuestions(module, seqs.SelectMany((seq, stage) => seq.Select((col, ix) => makeQuestion(Question.SimonSelectsOrder, module,
            formatArgs: new[] { Ordinal(ix + 1), Ordinal(stage + 1) },
            correctAnswers: new[] { col }))));
    }

    private IEnumerator<YieldInstruction> ProcessSimonSends(ModuleData module)
    {
        string[] morse = { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };

        var comp = GetComponent(module, "SimonSendsModule");
        var morseR = GetField<string>(comp, "_morseR").Get();
        var morseG = GetField<string>(comp, "_morseG").Get();
        var morseB = GetField<string>(comp, "_morseB").Get();
        var charR = ((char) ('A' + Array.IndexOf(morse, morseR.Replace("###", "-").Replace("#", ".").Replace("_", "")))).ToString();
        var charG = ((char) ('A' + Array.IndexOf(morse, morseG.Replace("###", "-").Replace("#", ".").Replace("_", "")))).ToString();
        var charB = ((char) ('A' + Array.IndexOf(morse, morseB.Replace("###", "-").Replace("#", ".").Replace("_", "")))).ToString();

        if (charR == "@" || charG == "@" || charB == "@")
            throw new AbandonModuleException($"Could not decode Morse code: {morseR} / {morseG} / {morseB}");

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.SimonSendsReceivedLetters, module, formatArgs: new[] { "red" }, correctAnswers: new[] { charR }, preferredWrongAnswers: new[] { charG, charB }),
            makeQuestion(Question.SimonSendsReceivedLetters, module, formatArgs: new[] { "green" }, correctAnswers: new[] { charG }, preferredWrongAnswers: new[] { charR, charB }),
            makeQuestion(Question.SimonSendsReceivedLetters, module, formatArgs: new[] { "blue" }, correctAnswers: new[] { charB }, preferredWrongAnswers: new[] { charR, charG }));
    }

    private IEnumerator<YieldInstruction> ProcessSimonServes(ModuleData module)
    {
        // Constants
        var names = new[] { "Riley", "Brandon", "Gabriel", "Veronica", "Wendy", "Kayle" };
        var foodCourse = Ut.NewArray(
            new[] { "Cruelo Juice", "Defuse Juice", "Simon’s Special Mix", "Boom Lager Beer", "Forget Cocktail", "Wire Shake", "Deto Bull", "Tasha’s Drink" },
            new[] { "Caesar Salad", "Edgework Toast", "Ticking Timecakes", "Big Boom Tortellini", "Status Light Rolls", "Blast Shrimps", "Morse Soup", "Boolean Waffles" },
            new[] { "Forghetti Bombognese", "NATO Shrimps", "Wire Spaghetti", "Indicator Tar Tar", "Centurion Wings", "Colored Spare Ribs", "Omelette au Bombage", "Veggie Blast Plate" },
            new[] { "Strike Pie", "Blastwave Compote", "Not Ice Cream", "Defuse au Chocolat", "Solve Cake", "Baked Batterys", "Bamboozling Waffles", "Bomb Brûlée" });

        // Reflection
        var comp = GetComponent(module, "simonServesScript");
        var fldStage = GetIntField(comp, "stage");
        var fldFood = GetArrayField<int>(comp, "foods");
        var fldBlinkingOrder = GetArrayField<int>(comp, "blinkingOrder");

        // Variables
        var currentStage = fldStage.Get(min: -1, max: 5);
        var flashOrder = new string[][] { null, null, null, null };
        var foodDisplayed = new string[][] { null, null, null, null };

        while (module.Unsolved)
        {
            var stage = fldStage.Get(min: -1, max: 5);
            if (currentStage != stage)
            {
                currentStage = stage;
                if (stage >= 0 && stage < 4)
                {
                    var flashes = fldBlinkingOrder.Get(expectedLength: 7, validator: i => i < 0 || i > 6 ? "expected range 0–6" : null);
                    flashOrder[stage] = flashes.Take(6).Select(flash => names[flash]).ToArray();
                    var food = fldFood.Get(expectedLength: 6, validator: i => i < 0 || i > 7 ? "expected range 0–7" : null);
                    foodDisplayed[stage] = food.Select(i => foodCourse[stage][i]).ToArray();
                }
            }
            yield return null;  // no ‘WaitForSeconds’ here to make absolutely sure we don’t miss a stage
        }

        var qs = new List<QandA>();
        for (int stage = 0; stage < 4; stage++)
        {
            for (int flashIx = 0; flashIx < 6; flashIx++)
                qs.Add(makeQuestion(Question.SimonServesFlash, module, formatArgs: new[] { Ordinal(flashIx + 1), (stage + 1).ToString() }, correctAnswers: new[] { flashOrder[stage][flashIx] }));
            qs.Add(makeQuestion(Question.SimonServesFood, module, formatArgs: new[] { (stage + 1).ToString() }, allAnswers: foodCourse[stage], correctAnswers: foodCourse[stage].Except(foodDisplayed[stage]).ToArray()));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonShapes(ModuleData module)
    {
        var comp = GetComponent(module, "SimonShapesScript");
        var fldAllFinalShapes = GetListField<List<int>>(comp, "_possibleFinalShapes");

        yield return WaitForSolve;

        var solutionShape = fldAllFinalShapes.Get(minLength: 1).First();

        // Converts the shape (aligned with the top-left corner) into a binary number
        var binaryValue = 0;
        var hOffset = solutionShape.Min(x => x % 3);
        var vOffset = solutionShape.Min(x => x / 3);
        foreach (var pos in solutionShape)
            binaryValue |= 1 << (3 * (pos / 3 - vOffset) + (pos % 3 - hOffset));

        // Every shape (in reading order of Table B) converted into binary using the above method.
        var binaryIx = Array.IndexOf(new[] { 210, 201, 61, 147, 55, 244, 409, 15, 30, 403, 7, 90, 73, 214, 11, 39, 60, 27, 313, 51, 19, 57, 59, 203, 25, 307, 211, 218, 75, 3, 94, 47, 91, 26, 9, 153 }, binaryValue);
        if (binaryIx == -1)
            throw new AbandonModuleException($"Obtained binary value {binaryValue} does not match any entry corresponding to a shape.");

        addQuestion(module, Question.SimonShapesSubmittedShape,
            correctAnswers: new[] { SimonShapesSprites[binaryIx] },
            preferredWrongAnswers: SimonShapesSprites);
    }

    private IEnumerator<YieldInstruction> ProcessSimonShouts(ModuleData module)
    {
        var comp = GetComponent(module, "SimonShoutsModule");
        yield return WaitForSolve;

        var diagramBPositions = GetArrayField<int>(comp, "_diagramBPositions").Get(expectedLength: 4, validator: b => b < 0 || b > 24 ? "expected range 0–24" : null);
        var diagramB = GetField<string>(comp, "_diagramB").Get(str => str.Length != 24 ? "expected length 24" : str.Any(ch => ch < 'A' || ch > 'Z') ? "expected letters A–Z" : null);

        var qs = new List<QandA>();
        var buttonNames = new[] { "top", "right", "bottom", "left" };
        for (int i = 0; i < 4; i++)
            qs.Add(makeQuestion(Question.SimonShoutsFlashingLetter, module, formatArgs: new[] { buttonNames[i] }, correctAnswers: new[] { diagramB[diagramBPositions[i]].ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonShrieks(ModuleData module)
    {
        var comp = GetComponent(module, "SimonShrieksModule");
        yield return WaitForSolve;

        var arrow = GetIntField(comp, "_arrow").Get(min: 0, max: 6);
        var flashingButtons = GetArrayField<int>(comp, "_flashingButtons").Get(expectedLength: 8, validator: b => b < 0 || b > 6 ? "expected range 0–6" : null);

        var qs = new List<QandA>();
        for (int i = 0; i < flashingButtons.Length; i++)
            qs.Add(makeQuestion(Question.SimonShrieksFlashingButton, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { ((flashingButtons[i] + 7 - arrow) % 7).ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonSignals(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSignalsModule");
        yield return WaitForSolve;

        var numRotations = GetArrayField<int>(comp, "_numRotations").Get(expectedLength: 5);
        var colorsShapes = GetArrayField<int>(comp, "_colorsShapes").Get(expectedLength: 5);
        var qs = new List<QandA>();

        var colorNames = new[] { "red", "green", "blue", "gray" };

        for (var i = 0; i < 5; i++)
        {
            // If this arrow has a unique color, we can ask for its shape and its number of rotations
            if (colorsShapes.Count(cs => (cs >> 3) == (colorsShapes[i] >> 3)) == 1)
            {
                qs.Add(makeQuestion(Question.SimonSignalsColorToShape, module,
                    formatArgs: new[] { colorNames[colorsShapes[i] >> 3] }, correctAnswers: new[] { SimonSignalsSprites[colorsShapes[i] & 7] }));
                qs.Add(makeQuestion(Question.SimonSignalsColorToRotations, module,
                    formatArgs: new[] { colorNames[colorsShapes[i] >> 3] }, correctAnswers: new[] { numRotations[i].ToString() }));
            }

            // If this arrow has a unique shape, we can ask for its color and its number of rotations
            if (colorsShapes.Count(cs => (cs & 7) == (colorsShapes[i] & 7)) == 1)
            {
                qs.Add(makeQuestion(Question.SimonSignalsShapeToColor, module,
                    questionSprite: SimonSignalsSprites[colorsShapes[i] & 7], correctAnswers: new[] { colorNames[colorsShapes[i] >> 3] }));
                qs.Add(makeQuestion(Question.SimonSignalsShapeToRotations, module,
                    questionSprite: SimonSignalsSprites[colorsShapes[i] & 7], correctAnswers: new[] { numRotations[i].ToString() }));
            }

            // If this arrow has a unique number of rotations, we can ask for its color and shape
            if (numRotations.Count(nr => nr == numRotations[i]) == 1)
            {
                qs.Add(makeQuestion(Question.SimonSignalsRotationsToColor, module,
                    formatArgs: new[] { numRotations[i].ToString() }, correctAnswers: new[] { colorNames[colorsShapes[i] >> 3] }));
                qs.Add(makeQuestion(Question.SimonSignalsRotationsToShape, module,
                    formatArgs: new[] { numRotations[i].ToString() }, correctAnswers: new[] { SimonSignalsSprites[colorsShapes[i] & 7] }));
            }
        }

        if (qs.Count == 0)
            legitimatelyNoQuestion(module.Module, "none of the arrows had a unique color, shape, or number of directions.");
        else
            addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonSimons(ModuleData module)
    {
        var comp = GetComponent(module, "simonsScript");
        yield return WaitForSolve;

        var flashes = new[] { "TR", "TY", "TG", "TB", "LR", "LY", "LG", "LB", "RR", "RY", "RG", "RB", "BR", "BY", "BG", "BB" };
        var buttonFlashes = GetArrayField<KMSelectable>(comp, "selButtons").Get(expectedLength: 5, validator: sel => !flashes.Contains(sel.name.ToUpperInvariant()) ? "invalid flash" : null);
        addQuestions(module, buttonFlashes.Select((btn, i) =>
            makeQuestion(Question.SimonSimonsFlashingColors, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { btn.name.ToUpperInvariant() })));
    }

    private IEnumerator<YieldInstruction> ProcessSimonSings(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSingsModule");
        yield return WaitForSolve;

        var noteNames = new[] { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };
        var flashingColorSequences = GetArrayField<int[]>(comp, "_flashingColors").Get(expectedLength: 3, validator: seq => seq.Any(col => col < 0 || col >= noteNames.Length) ? $"expected range 0–{noteNames.Length - 1}" : null);
        addQuestions(module, flashingColorSequences.SelectMany((seq, stage) => seq.Select((col, ix) => makeQuestion(Question.SimonSingsFlashing, module, formatArgs: new[] { Ordinal(ix + 1), Ordinal(stage + 1) }, correctAnswers: new[] { noteNames[col] }))));
    }

    private IEnumerator<YieldInstruction> ProcessSimonSmiles(ModuleData module)
    {
        var comp = GetComponent(module, "ShitassSays");

        yield return WaitForSolve;

        var shitassMode = GetField<bool>(GetField<object>(comp, "Settings").Get(), "shitassMode", true).Get();
        var sounds = GetField<int[]>(comp, "Sounds")
            .Get(a => a.Select((b, i) => b < 0 ? $"Sounds[{i}] = {b} < 0" : b > 2 ? $"Sounds[{i}] = {b} > 2" : null).Aggregate((x, y) => x is null ? y : y is null ? x : x + ", " + y));
        var allAnswers = shitassMode ? SimonSmilesAudio.Skip(3).ToArray() : SimonSmilesAudio.Take(3).ToArray();
        addQuestions(module, Enumerable.Range(0, 9).Select(ix =>
            makeQuestion(Question.SimonSmilesSounds, module, formatArgs: new[] { Ordinal(ix + 1) },
                correctAnswers: new[] { allAnswers[sounds[ix]] }, allAnswers: allAnswers)));
    }

    private IEnumerator<YieldInstruction> ProcessSimonSmothers(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSmothersScript");

        yield return WaitForSolve;

        var flashes = GetField<IList>(comp, "flashes").Get();
        var qs = new List<QandA>();
        for (int pos = 0, length = flashes.Count; pos < length; pos++)
        {
            string position = Ordinal(pos + 1);
            qs.Add(makeQuestion(Question.SimonSmothersColors, module, formatArgs: new[] { position }, correctAnswers: new[] { GetField<Enum>(flashes[pos], "color", isPublic: true).Get().ToString() }));
            qs.Add(makeQuestion(Question.SimonSmothersDirections, module, formatArgs: new[] { position }, correctAnswers: new[] { GetField<Enum>(flashes[pos], "direction", isPublic: true).Get().ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonSounds(ModuleData module)
    {
        var comp = GetComponent(module, "simonSoundsScript");
        yield return WaitForSolve;

        var colorNames = new[] { "red", "blue", "yellow", "green" };
        var flashed = GetArrayField<List<int>>(comp, "stage").Get(ar => ar == null ? "contains null" : ar.Any(list => list.Last() < 0 || list.Last() >= colorNames.Length) ? "expected last value in range 0–3" : null);

        var qs = new List<QandA>();
        for (var stage = 0; stage < flashed.Length; stage++)
            qs.Add(makeQuestion(Question.SimonSoundsFlashingColors, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { colorNames[flashed[stage].Last()] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonSpeaks(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSpeaksModule");
        yield return WaitForSolve;

        var sequence = GetArrayField<int>(comp, "_sequence").Get(expectedLength: 5);
        var colors = GetArrayField<int>(comp, "_colors").Get(expectedLength: 9);
        var words = GetArrayField<int>(comp, "_words").Get(expectedLength: 9);
        var shapes = GetArrayField<int>(comp, "_shapes").Get(expectedLength: 9);
        var languages = GetArrayField<int>(comp, "_languages").Get(expectedLength: 9);
        var wordsTable = GetStaticField<string[][]>(comp.GetType(), "_wordsTable").Get(ar => ar.Length != 9 ? "expected length 9" : null);
        var positionNames = GetStaticField<string[]>(comp.GetType(), "_positionNames").Get(ar => ar.Length != 9 ? "expected length 9" : null);
        var languageNames = new[] { "English", "Danish", "Dutch", "Esperanto", "Finnish", "French", "German", "Hungarian", "Italian" };

        addQuestions(module,
            makeQuestion(Question.SimonSpeaksPositions, module, correctAnswers: new[] { positionNames[sequence[0]] }),
            makeQuestion(Question.SimonSpeaksShapes, module, correctAnswers: new[] { SimonSpeaksSprites[shapes[sequence[1]]] }, preferredWrongAnswers: SimonSpeaksSprites),
            makeQuestion(Question.SimonSpeaksLanguages, module, correctAnswers: new[] { languageNames[languages[sequence[2]]] }),
            makeQuestion(Question.SimonSpeaksWords, module, correctAnswers: new[] { wordsTable[words[sequence[3]]][languages[sequence[3]]] }),
            makeQuestion(Question.SimonSpeaksColors, module, correctAnswers: new[] { wordsTable[colors[sequence[4]]][0] }));
    }

    private IEnumerator<YieldInstruction> ProcessSimonsStar(ModuleData module)
    {
        var comp = GetComponent(module, "simonsStarScript");
        var validColors = new[] { "red", "yellow", "green", "blue", "purple" };
        var flashes = "first,second,third,fourth,fifth".Split(',').Select(n => GetField<string>(comp, n + "FlashColour", isPublic: true).Get(c => !validColors.Contains(c) ? "invalid color" : null)).ToArray();

        yield return WaitForSolve;

        addQuestions(module, flashes.Select((f, ix) => makeQuestion(Question.SimonsStarColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { f })));
    }

    private IEnumerator<YieldInstruction> ProcessSimonStacks(ModuleData module)
    {
        var comp = GetComponent(module, "simonstacksScript");

        yield return WaitForSolve;

        var colors = GetListField<string>(comp, "Colors").Get(minLength: 3, maxLength: 5);
        addQuestions(module, colors.Select((c, ix) => makeQuestion(Question.SimonStacksColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { c })));
    }

    private IEnumerator<YieldInstruction> ProcessSimonStages(ModuleData module)
    {
        var comp = GetComponent(module, "SimonStagesHandler");
        var indicatorList = GetMethod<List<string>>(comp, "grabIndicatorColorsAll", numParameters: 0, isPublic: true);
        var flashList = GetMethod<List<string>>(comp, "grabSequenceColorsOneStage", numParameters: 1, isPublic: true);

        yield return WaitForSolve;

        var indicators = indicatorList.Invoke();
        var stage1Flash = flashList.Invoke(1);
        var stage2Flash = flashList.Invoke(2);
        var stage3Flash = flashList.Invoke(3);
        var stage4Flash = flashList.Invoke(4);
        var stage5Flash = flashList.Invoke(5);

        addQuestions(module, indicators.Select((ans, i) => makeQuestion(Question.SimonStagesIndicator, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { ans }))
            .Concat(stage1Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, module, formatArgs: new[] { Ordinal(i + 1), "first" }, correctAnswers: new[] { ans })))
            .Concat(stage2Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, module, formatArgs: new[] { Ordinal(i + 1), "second" }, correctAnswers: new[] { ans })))
            .Concat(stage3Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, module, formatArgs: new[] { Ordinal(i + 1), "third" }, correctAnswers: new[] { ans })))
            .Concat(stage4Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, module, formatArgs: new[] { Ordinal(i + 1), "4th" }, correctAnswers: new[] { ans })))
            .Concat(stage5Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, module, formatArgs: new[] { Ordinal(i + 1), "5th" }, correctAnswers: new[] { ans }))));
    }

    private IEnumerator<YieldInstruction> ProcessSimonStates(ModuleData module)
    {
        var comp = GetComponent(module, "AdvancedSimon");
        var fldPuzzleDisplay = GetArrayField<bool[]>(comp, "PuzzleDisplay");

        bool[][] puzzleDisplay;
        while ((puzzleDisplay = fldPuzzleDisplay.Get(nullAllowed: true)) == null)
            yield return new WaitForSeconds(.1f);

        if (puzzleDisplay.Length != 4 || puzzleDisplay.Any(arr => arr.Length != 4))
            throw new AbandonModuleException($"‘PuzzleDisplay’ has an unexpected length or value: [{puzzleDisplay.Select(arr => arr == null ? "null" : "[" + arr.JoinString(", ") + "]").JoinString("; ")}]");

        var colorNames = new[] { "Red", "Yellow", "Green", "Blue" };

        yield return WaitForSolve;
        // Consistency check
        if (fldPuzzleDisplay.Get(nullAllowed: true) != null)
            throw new AbandonModuleException($"‘PuzzleDisplay’ was expected to be null when the module solved, but wasn’t.");

        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)     // Do not ask about fourth stage because it can sometimes be solved without waiting for the flashes
        {
            var c = puzzleDisplay[i].Count(b => b);
            if (c != 3)
                qs.Add(makeQuestion(Question.SimonStatesDisplay, module,
                    formatArgs: new[] { "color(s) flashed", Ordinal(i + 1) },
                    correctAnswers: new[] { c == 4 ? "all 4" : puzzleDisplay[i].Select((v, j) => v ? colorNames[j] : null).Where(x => x != null).JoinString(", ") }));
            if (c != 1)
                qs.Add(makeQuestion(Question.SimonStatesDisplay, module,
                    formatArgs: new[] { "color(s) didn’t flash", Ordinal(i + 1) },
                    correctAnswers: new[] { c == 4 ? "none" : puzzleDisplay[i].Select((v, j) => v ? null : colorNames[j]).Where(x => x != null).JoinString(", ") }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonStops(ModuleData module)
    {
        var comp = GetComponent(module, "SimonStops");
        yield return WaitForSolve;

        var colors = GetArrayField<string>(comp, "outputSequence").Get(expectedLength: 5);
        addQuestions(module, colors.Select((color, ix) =>
             makeQuestion(Question.SimonStopsColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { color }, preferredWrongAnswers: colors)));
    }

    private IEnumerator<YieldInstruction> ProcessSimonStores(ModuleData module)
    {
        var comp = GetComponent(module, "SimonStoresScript");
        yield return WaitForSolve;

        var flashSequences = GetListField<string>(comp, "flashingColours").Get();
        var colors = "RGBCMY";

        foreach (var flash in flashSequences)
        {
            var set = new HashSet<char>();
            if (flash.Length < 1 || flash.Length > 3 || flash.Any(color => !set.Add(color) || !colors.Contains(color)))
                throw new AbandonModuleException($"'flashingColours' contains value with duplicated colors, invalid color, or unexpected length (expected: 1-3): [flash: {flash}, length: {flash.Length}]");
        }

        var colorNames = new Dictionary<char, string> {
            { 'R', "Red" },
            { 'G', "Green" },
            { 'B', "Blue" },
            { 'C', "Cyan" },
            { 'M', "Magenta" },
            { 'Y', "Yellow" }
        };

        var qs = new List<QandA>();
        for (var i = 0; i < 5; i++)
            qs.Add(makeQuestion(Question.SimonStoresColors, module,
                formatArgs: new[] { flashSequences[i].Length == 1 ? "flashed" : "was among the colors flashed", Ordinal(i + 1) },
                correctAnswers: flashSequences[i].Select(ch => colorNames[ch]).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonSubdivides(ModuleData module)
    {
        var comp = GetComponent(module, "SSubScript");
        yield return WaitForSolve;
        //URDL
        //RBVG
        var split = GetArrayField<bool[]>(comp, "split").Get(arr => arr.Length != 5 ? "Wrong outer array size" : arr.All(a => a.Length == 4) ? null : "Wrong inner array size");
        var arrange = GetField<int[,]>(comp, "arrange").Get(arr => arr.Length == 84 ? null : "Bad arrange size");
        var qs = new List<QandA>(12);
        var dirs = new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, 1) };
        var colors = new[] { "Red", "Blue", "Violet", "Green" };
        for (int a = 0; a < 4; a++)
        {
            if (split[0][a])
            {
                qs.Add(makeQuestion(Question.SimonSubdividesButton, module, questionSprite: Sprites.GenerateGridSprite(new Coord(2, 2, dirs[a].x, dirs[a].y)), correctAnswers: new[] { colors[arrange[0, a]] }, questionSpriteRotation: 45f));
                for (int b = 0; b < 4; b++)
                    if (split[a + 1][b])
                        qs.Add(makeQuestion(Question.SimonSubdividesButton, module, questionSprite: Sprites.GenerateGridSprite(new Coord(4, 4, dirs[a].x * 2 + dirs[b].x, dirs[a].y * 2 + dirs[b].y)), correctAnswers: new[] { colors[arrange[a + 1, b]] }, questionSpriteRotation: 45f));
            }
        }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimonSupports(ModuleData module)
    {
        var comp = GetComponent(module, "SimonSupportsScript");
        yield return WaitForSolve;

        var combo = GetField<bool[][]>(comp, "combo").Get();
        var traits = GetArrayField<int>(comp, "tra").Get(expectedLength: 8);
        var traitNames = new[] { "Boss", "Cruel", "Faulty", "Lookalike", "Puzzle", "Simon", "Time-Based", "Translated" };
        var chosenTopics = Enumerable.Range(0, 3).Select(x => traitNames[traits[x]]).ToArray();

        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.SimonSupportsTopics, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { chosenTopics[i] }, preferredWrongAnswers: chosenTopics));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSimultaneousSimons(ModuleData module)
    {
        var comp = GetComponent(module, "SimultaneousSimons");
        yield return WaitForSolve;

        var sequences = GetField<int[,]>(comp, "sequences").Get();
        var btnColors = GetStaticField<int[]>(comp.GetType(), "buttonColors").Get();
        var colorNames = new[] { "Blue", "Yellow", "Red", "Green" };

        var qs = new List<QandA>();
        for (int simon = 0; simon < 4; simon++)
            for (int flash = 0; flash < 4; flash++)
                qs.Add(makeQuestion(Question.SimultaneousSimonsFlash, module,
                    formatArgs: new[] { Ordinal(simon + 1), Ordinal(flash + 1) },
                    correctAnswers: new[] { colorNames[btnColors[sequences[simon, flash]]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSkewedSlots(ModuleData module)
    {
        var comp = GetComponent(module, "SkewedModule");
        var fldNumbers = GetArrayField<int>(comp, "Numbers");
        var fldModuleActivated = GetField<bool>(comp, "moduleActivated");

        var originalNumbers = new List<string>();

        while (true)
        {
            // Skewed Slots sets moduleActivated to false while the slots are spinning.
            // If there was a correct answer, it will set solved to true, otherwise it will set moduleActivated to true.
            while (!fldModuleActivated.Get() && module.Unsolved)
                yield return new WaitForSeconds(.1f);

            if (module.IsSolved)
                break;

            // Get the current original digits.
            originalNumbers.Add(fldNumbers.Get(expectedLength: 3, validator: n => n < 0 || n > 9 ? "expected range 0–9" : null).JoinString());

            // When the user presses anything, Skewed Slots sets moduleActivated to false while the slots are spinning.
            while (fldModuleActivated.Get())
                yield return new WaitForSeconds(.1f);
        }

        addQuestion(module, Question.SkewedSlotsOriginalNumbers, correctAnswers: new[] { originalNumbers.Last() },
            preferredWrongAnswers: originalNumbers.Take(originalNumbers.Count - 1).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessSkewers(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "Skewers");

        var color = GetListField<int>(comp, "GemColors").Get(expectedLength: 16, validator: v => v is < 0 or > 7 ? "Out of range [0, 7]" : null);
        addQuestions(module, color.Select((c, i) =>
            makeQuestion(Question.SkewersColor, module,
                correctAnswers: new[] { Question.SkewersColor.GetAnswers()[c] },
                questionSprite: Sprites.GenerateGridSprite(4, 4, i))));
    }

    private IEnumerator<YieldInstruction> ProcessSkyrim(ModuleData module)
    {
        var comp = GetComponent(module, "skyrimScript");

        yield return WaitForSolve;

        foreach (var fieldName in new[] { "cycleUp", "cycleDown", "accept", "submit", "race", "weapon", "enemy", "city", "shout" })
        {
            var btn = GetField<KMSelectable>(comp, fieldName, isPublic: true).Get();
            btn.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
                btn.AddInteractionPunch(.5f);
                return false;
            };
        }

        var qs = new List<QandA>();
        var questions = new[] { Question.SkyrimRace, Question.SkyrimWeapon, Question.SkyrimEnemy, Question.SkyrimCity };
        var fieldNames = new[] { "race", "weapon", "enemy", "city" };
        var flds = fieldNames.Select(name => GetListField<Texture>(comp, name + "Images", isPublic: true)).ToArray();
        var fldsCorrect = new[] { "correctRace", "correctWeapon", "correctEnemy", "correctCity" }.Select(name => GetField<Texture>(comp, name)).ToArray();
        for (int i = 0; i < fieldNames.Length; i++)
        {
            var list = flds[i].Get(expectedLength: 3);
            var correct = fldsCorrect[i].Get();
            qs.Add(makeQuestion(questions[i], module, correctAnswers: list.Except(new[] { correct }).Select(t => t.name.Replace("'", "’")).ToArray()));
        }
        var shoutNames = GetListField<string>(comp, "shoutNameOptions").Get(expectedLength: 3);
        qs.Add(makeQuestion(Question.SkyrimDragonShout, module, correctAnswers: shoutNames.Except(new[] { GetField<string>(comp, "shoutName").Get() }).Select(n => n.Replace("'", "’")).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSlowMath(ModuleData module)
    {
        var comp = GetComponent(module, "SlowMathScript");
        yield return WaitForSolve;

        var ogLetters = GetListField<string>(comp, "_chosenLetters").Get(minLength: 3, maxLength: 5);
        addQuestion(module, Question.SlowMathLastLetters, correctAnswers: new[] { ogLetters.Last() });
    }

    private IEnumerator<YieldInstruction> ProcessSmallCircle(ModuleData module)
    {
        var comp = GetComponent(module, "smallCircle");
        yield return WaitForSolve;

        var shift = GetField<int>(comp, "shift").Get();
        var tableColor = GetField<int>(comp, "tableColor").Get();
        var solution = GetArrayField<int>(comp, "solution").Get();
        var colorNames = GetStaticField<string[]>(comp.GetType(), "colorNames").Get().Select(x => x[0].ToString().ToUpperInvariant() + x.Substring(1)).ToArray();
        var qs = new List<QandA>
        {
            makeQuestion(Question.SmallCircleShift, module, correctAnswers: new[] { shift.ToString() }),
            makeQuestion(Question.SmallCircleWedge, module, correctAnswers: new[] { colorNames[tableColor] })
        };
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.SmallCircleSolution, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { colorNames[solution[i]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSmashMarryKill(ModuleData module)
    {
        var comp = GetComponent(module, "SmashMarryKill");
        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);
        // All SMyK modules on a bomb share information,
        // so we don't need to keep track of solve order at all,
        // nor even disambiguate the modules.

        var assignments = GetStaticField<IDictionary>(comp.GetType(), "allModules").Get();
        if (assignments.Count == 0)
        {
            legitimatelyNoQuestion(module.Module, "No modules were categorized.");
            yield break;
        }

        var moduleName = translateModuleName(Question.SmashMarryKillCategory, "Smash, Marry, Kill");
        List<QandA> questions = new();
        var smash = new List<string>();
        var marry = new List<string>();
        var kill = new List<string>();
        foreach (DictionaryEntry de in assignments)
        {
            if (de.Value.ToString() == "SMASH")
                smash.Add((string) de.Key);
            if (de.Value.ToString() == "MARRY")
                marry.Add((string) de.Key);
            if (de.Value.ToString() == "KILL")
                kill.Add((string) de.Key);
            questions.Add(makeQuestion(Question.SmashMarryKillCategory, module, formattedModuleName: moduleName, formatArgs: new[] { (string) de.Key }, correctAnswers: new[] { de.Value.ToString() }));
        }
        var allMods = smash.Concat(marry).Concat(kill).ToArray();
        if (allMods.Length < 4)
            allMods = Bomb.GetSolvableModuleNames().Distinct().ToArray();
        if (smash.Count > 0)
            questions.Add(makeQuestion(Question.SmashMarryKillModule, module, formattedModuleName: moduleName, formatArgs: new[] { "SMASH" },
                correctAnswers: smash.ToArray(), allAnswers: allMods));
        if (marry.Count > 0)
            questions.Add(makeQuestion(Question.SmashMarryKillModule, module, formattedModuleName: moduleName, formatArgs: new[] { "MARRY" },
                correctAnswers: marry.ToArray(), allAnswers: allMods));
        if (kill.Count > 0)
            questions.Add(makeQuestion(Question.SmashMarryKillModule, module, formattedModuleName: moduleName, formatArgs: new[] { "KILL" },
                correctAnswers: kill.ToArray(), allAnswers: allMods));

        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessSnooker(ModuleData module)
    {
        var comp = GetComponent(module, "snookerScript");
        var fldActiveReds = GetIntField(comp, "activeReds");
        var activeReds = 0;

        var getNewValue = true;
        module.Module.OnStrike += delegate { getNewValue = true; return true; };

        while (module.Unsolved)
        {
            if (getNewValue)
            {
                activeReds = fldActiveReds.Get(min: 8, max: 10);
                getNewValue = false;
            }
            yield return null;
        }
        yield return new WaitForSeconds(.1f);

        addQuestion(module, Question.SnookerReds, correctAnswers: new[] { activeReds.ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessSnowflakes(ModuleData module)
    {
        var comp = GetComponent(module, "snowflakes");

        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var gameOnPassDelegate = module.Module.OnPass;
        module.Module.OnPass = () => { return false; };

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        yield return new WaitForSeconds(5); // Wait for the snowflakes to disappear
        gameOnPassDelegate();

        var displays = GetArrayField<TextMesh>(comp, "displays", isPublic: true).Get(expectedLength: 4);
        var directions = new[] { "top", "right", "bottom", "left" };
        addQuestions(module, directions.Select((dir, ix) => makeQuestion(Question.SnowflakesDisplayedSnowflakes, module, formatArgs: new[] { dir }, correctAnswers: new[] { displays[ix].text })));
    }

    private IEnumerator<YieldInstruction> ProcessSonicKnuckles(ModuleData module)
    {
        var comp = GetComponent(module, "sonicKnucklesScript");

        var heroArr = GetArrayField<object>(comp, "heroes", isPublic: true).Get();
        var badniksArr = GetArrayField<object>(comp, "badniks", isPublic: true).Get();
        var monitorArr = GetArrayField<object>(comp, "monitors", isPublic: true).Get();

        var fldAttachedSound = GetField<AudioClip>(heroArr[0], "attachedSound", isPublic: true);
        var fldContainsIllegalSound = GetField<bool>(heroArr[0], "containsIllegalSound", isPublic: true);
        var fldLabel = GetField<string>(heroArr[0], "label", isPublic: true);

        yield return WaitForSolve;

        var hero = heroArr[GetIntField(comp, "heroIndex").Get(0, heroArr.Length - 1)];
        var badnik = badniksArr[GetIntField(comp, "badnikIndex").Get(0, badniksArr.Length - 1)];
        var monitor = monitorArr[GetIntField(comp, "monitorIndex").Get(0, monitorArr.Length - 1)];

        var badnikName = fldLabel.GetFrom(badnik, v => !SonicKnucklesBadniksSprites.Any(s => s.name == v) ? "not a recognized badnik name" : null);
        var monitorName = fldLabel.GetFrom(monitor, v => !SonicKnucklesMonitorsSprites.Any(s => s.name == v) ? "not a recognized monitor name" : null);
        var illegalSound =
            fldContainsIllegalSound.GetFrom(hero) ? fldAttachedSound.GetFrom(hero) :
            fldContainsIllegalSound.GetFrom(monitor) ? fldAttachedSound.GetFrom(monitor) :
            fldContainsIllegalSound.GetFrom(badnik) ? fldAttachedSound.GetFrom(badnik) :
            throw new AbandonModuleException("None of the three items (hero, monitor, badnik) contain the illegal sound.");

        var usedSounds = new[] { fldAttachedSound.GetFrom(hero), fldAttachedSound.GetFrom(hero), fldAttachedSound.GetFrom(badnik) };
        var allSounds = GetArrayField<AudioClip>(comp, "mushroomSounds", true).Get(expectedLength: 4).Concat(
                GetArrayField<AudioClip>(comp, "noMushroomSounds", true).Get(expectedLength: 20)
            ).ToArray();

        addQuestions(module,
            makeQuestion(Question.SonicKnucklesSounds, module, correctAnswers: new[] { illegalSound }, preferredWrongAnswers: usedSounds, allAnswers: allSounds),
            makeQuestion(Question.SonicKnucklesBadnik, module, correctAnswers: new[] { SonicKnucklesBadniksSprites.First(sprite => sprite.name == badnikName) }, preferredWrongAnswers: SonicKnucklesBadniksSprites),
            makeQuestion(Question.SonicKnucklesMonitor, module, correctAnswers: new[] { SonicKnucklesMonitorsSprites.First(sprite => sprite.name == monitorName) }, preferredWrongAnswers: SonicKnucklesMonitorsSprites));
    }

    private IEnumerator<YieldInstruction> ProcessSonicTheHedgehog(ModuleData module)
    {
        var comp = GetComponent(module, "sonicScript");
        var fldsButtonSounds = new[] { "boots", "invincible", "life", "rings" }.Select(name => GetField<string>(comp, name + "Press"));
        var fldsPics = Enumerable.Range(0, 3).Select(i => GetField<Texture>(comp, "pic" + (i + 1))).ToArray();
        yield return WaitForSolve;

        if (SonicTheHedgehogSprites.Length != 15)
            throw new AbandonModuleException($@"Sonic the Hedgehog should have 15 sprites. Counted {SonicTheHedgehogSprites.Length}");

        var soundNameMapping = "boss|breathe|bumper|continueSFX|drown|emerald|extraLife|finalZone|invincibleSFX|jump|lamppost|marbleZone|skid|spikes|spin|spring"
            .Split('|').Select((s, i) => (s, i)).ToDictionary(t => t.s, t => t.i);

        var pictureNames = new string[] { "annoyedSonic", "ballhog", "blueLamppost", "burrobot", "buzzBomber", "crabMeat", "deadSonic", "drownedSonic", "fallingSonic", "motoBug", "redLamppost", "redSpring", "standingSonic", "switch", "yellowSpring" };
        var pics = fldsPics.Select(f => f.Get(p => p.name == null || !pictureNames.Contains(p.name) ? "unknown pic" : null)).ToArray();
        var sounds = fldsButtonSounds.Select(f => f.Get(s => !soundNameMapping.ContainsKey(s) ? "unknown sound" : null)).ToArray();

        var screenNames = new[] { "Running Boots", "Invincibility", "Extra Life", "Rings" };
        var spriteArr = new Sprite[][]
        {
            SonicTheHedgehogSprites.Where(sprite => new[] { "ballhog", "burrobot", "buzzBomber", "crabMeat", "motoBug" }.Contains(sprite.name)).ToArray(),
            SonicTheHedgehogSprites.Where(sprite => new[] { "annoyedSonic", "deadSonic", "drownedSonic", "fallingSonic", "standingSonic" }.Contains(sprite.name)).ToArray(),
            SonicTheHedgehogSprites.Where(sprite => new[] { "blueLamppost", "redLamppost", "redSpring", "switch", "yellowSpring" }.Contains(sprite.name)).ToArray()
        };

        var qs = new List<QandA>();

        for (var stage = 0; stage < 3; stage++)
            qs.Add(makeQuestion(
                question: Question.SonicTheHedgehogPictures,
                formatArgs: new[] { Ordinal(stage + 1) },
                data: module,
                allAnswers: spriteArr[stage],
                correctAnswers: new[] { spriteArr[stage].First(sprite => sprite.name == pics[stage].name) }));

        for (var screen = 0; screen < 4; screen++)
            qs.Add(makeQuestion(
                Question.SonicTheHedgehogSounds,
                data: module,
                formatArgs: new[] { screenNames[screen] },
                correctAnswers: new[] { SonicTheHedgehogAudio[soundNameMapping[sounds[screen]]] },
                preferredWrongAnswers: sounds.Select(s => SonicTheHedgehogAudio[soundNameMapping[s]]).ToArray()));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSorting(ModuleData module)
    {
        var comp = GetComponent(module, "Sorting");
        yield return WaitForSolve;

        var lastSwap = GetField<byte>(comp, "swapButtons").Get();
        if (lastSwap % 10 == 0 || lastSwap % 10 > 5 || lastSwap / 10 == 0 || lastSwap / 10 > 5 || lastSwap / 10 == lastSwap % 10)
            throw new AbandonModuleException($"‘swap’ has unexpected value (expected two digit number, each with a unique digit from 1-5): {lastSwap}");

        addQuestion(module, Question.SortingLastSwap, correctAnswers: new[] { lastSwap.ToString().Insert(1, " & ") });
    }

    private IEnumerator<YieldInstruction> ProcessSouvenir(ModuleData module)
    {
        var comp = module.Module.GetComponent<SouvenirModule>();
        const string moduleId = "SouvenirModule";
        if (comp == this)
        {
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        if (!_moduleCounts.TryGetValue(moduleId, out var souvenirCount) || souvenirCount != 2)
        {
            if (souvenirCount > 2)
                Debug.Log($"[Souvenir #{_moduleId}] There are more than two Souvenir modules on this bomb. Not asking any questions about them.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        // Prefer names of supported modules on the bomb other than Souvenir.
        var preferredWrongAnswers = new List<string>();
        var allAnswers = new List<string>();
        var modulesOnTheBomb = _supportedModuleNames.Where(s => s != "Souvenir").Select(m => m.Replace("'", "’"));
        foreach (var (name, trName) in Ut.Attributes.Select(a => (a.Value.ModuleNameWithThe, _translation?.Translate(a.Key)?.ModuleName ?? a.Value.ModuleNameWithThe)).Distinct())
        {
            allAnswers.Add(trName);
            if (modulesOnTheBomb.Contains(name.Replace("\u00a0", " ")))
                preferredWrongAnswers.Add(trName);
        }

        while (comp._currentQuestion == null)
            yield return new WaitForSeconds(0.1f);

        var firstQuestion = comp._currentQuestion;
        var firstModule = (
            _translation?.Translate(firstQuestion.Question)?.ModuleName ??
            firstQuestion.ModuleNameWithThe).Replace("\u00a0", " ");

        // Wait for the user to solve that question before asking about it
        while (comp._currentQuestion == firstQuestion)
            yield return new WaitForSeconds(0.1f);

        module.SolveIndex = 1; // The question does not use the formatted module name. However, since the module may not be solved at this point, we need to specify a solve index anyways.
        addQuestion(module, Question.SouvenirFirstQuestion, correctAnswers: new[] { firstModule },
            preferredWrongAnswers: preferredWrongAnswers.ToArray(), allAnswers: allAnswers.ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessSpaceTraders(ModuleData module)
    {
        var comp = GetComponent(module, "SpaceTradersModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Space Traders because the module was force-solved.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }
        if (GetProperty<int>(comp, "maxPossibleTaxAmount", true).Get() < 4)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Space Traders because all paths from the solar system are too short.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        addQuestion(module, Question.SpaceTradersMaxTax, correctAnswers: new[] { GetProperty<int>(comp, "maxTax", true).Get().ToString() + " GCr" });
    }

    private IEnumerator<YieldInstruction> ProcessSpellingBee(ModuleData module)
    {
        var comp = GetComponent(module, "spellingBeeScript");
        var wordList = GetField<List<string>>(comp, "wordList", isPublic: true).Get();

        yield return WaitForSolve;
        var focus = GetField<int>(comp, "chosenWord").Get();
        addQuestion(module, Question.SpellingBeeWord, correctAnswers: new[] { wordList[focus] }, preferredWrongAnswers: wordList.ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessSphere(ModuleData module)
    {
        var comp = GetComponent(module, "theSphereScript");

        string[] colorNames = GetArrayField<string>(comp, "colourNames", isPublic: true).Get();
        int[] colors = GetArrayField<int>(comp, "selectedColourIndices", isPublic: true).Get(expectedLength: 5, validator: c => c < 0 || c >= colorNames.Length ? $"expected range 0–{colorNames.Length - 1}" : null);

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.SphereColors, module, formatArgs: new[] { "first" }, correctAnswers: new[] { colorNames[colors[0]] }),
            makeQuestion(Question.SphereColors, module, formatArgs: new[] { "second" }, correctAnswers: new[] { colorNames[colors[1]] }),
            makeQuestion(Question.SphereColors, module, formatArgs: new[] { "third" }, correctAnswers: new[] { colorNames[colors[2]] }),
            makeQuestion(Question.SphereColors, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { colorNames[colors[3]] }),
            makeQuestion(Question.SphereColors, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { colorNames[colors[4]] }));
    }

    private IEnumerator<YieldInstruction> ProcessSplittingTheLoot(ModuleData module)
    {
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var comp = GetComponent(module, "SplittingTheLootScript");
        var bags = (IList) GetField<object>(comp, "bags").Get(lst => lst is not IList list ? "expected an IList" : list.Count != 7 ? "expected length 7" : null);
        var fldBagColor = GetField<object>(bags[0], "Color");
        var fldBagLabel = GetField<string>(bags[0], "Label");
        var bagColors = bags.Cast<object>().Select(obj => fldBagColor.GetFrom(obj)).ToArray();
        var bagLabels = bags.Cast<object>().Select(obj => fldBagLabel.GetFrom(obj)).ToArray();
        var paintedBag = bagColors.IndexOf(bc => bc.ToString() != "Normal");
        if (paintedBag == -1)
            throw new AbandonModuleException($"No colored bag was found: [{bagColors.JoinString(", ")}]");

        yield return WaitForSolve;

        addQuestion(module, Question.SplittingTheLootColoredBag, correctAnswers: new[] { bagLabels[paintedBag] }, preferredWrongAnswers: bagLabels);
    }

    private IEnumerator<YieldInstruction> ProcessSpongebobBirthdayIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "SpongebobBirthdayIdentificationScript");
        var fldStage = GetIntField(comp, "stage");
        var fldAnswer = GetField<string>(comp, "answer");

        var answers = new List<string>();
        var currentStage = fldStage.Get();
        while (module.Unsolved)
        {
            var newStage = fldStage.Get();
            if (currentStage != newStage)
            {
                answers.Add(fldAnswer.Get());
                currentStage = newStage;
            }
            yield return null;
        }
        yield return new WaitForSeconds(.1f);

        var allNames = GetField<Texture[]>(comp, "allImages", isPublic: true).Get().Select(x => x.name).ToArray();
        addQuestions(module, answers.Select((ans, ix) => makeQuestion(
            question: Question.SpongebobBirthdayIdentificationChildren,
            data: module,
            formatArgs: new[] { Ordinal(ix + 1) },
            correctAnswers: new[] { ans },
            preferredWrongAnswers: allNames)));
    }

    private IEnumerator<YieldInstruction> ProcessStability(ModuleData module)
    {
        var colorNames = new[] { "Red", "Yellow", "Blue" };

        var comp = GetComponent(module, "StabilityScript");
        yield return WaitForSolve;

        var qs = new List<QandA>();

        var litLedStates = GetArrayField<int>(comp, "ledStates").Get().Where(l => l != 5).ToArray();
        for (int i = 0; i < litLedStates.Length; i++)
            qs.Add(makeQuestion(Question.StabilityLedColors, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { colorNames[litLedStates[i]] }));

        if (litLedStates.Length > 3)
            qs.Add(makeQuestion(Question.StabilityIdNumber, module, correctAnswers: new[] { GetField<string>(comp, "idNumber").Get() }));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessStableTimeSignatures(ModuleData module)
    {
        var comp = GetComponent(module, "StableTimeSignatures");
        yield return WaitForSolve;

        var topSequence = GetListField<string>(comp, "randomSequenceTop", isPublic: true)
            .Get(validator: l => l.All(s => !"123456789".Contains(s)) ? "Bad digit" : null);
        var bottomSequence = GetListField<string>(comp, "randomSequenceBottom", isPublic: true)
            .Get(expectedLength: topSequence.Count, validator: s => !"1248".Contains(s) ? "Bad digit" : null);
        var answers = Enumerable.Range(0, topSequence.Count).Select(i => $"{topSequence[i]}/{bottomSequence[i]}").ToArray();
        addQuestions(module, answers.Select((s, i) => makeQuestion(Question.StableTimeSignaturesSignatures, module,
            formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { s }, preferredWrongAnswers: answers)));
    }

    private IEnumerator<YieldInstruction> ProcessStackedSequences(ModuleData module)
    {
        var comp = GetComponent(module, "stackedSequencesScript");
        yield return WaitForSolve;

        var sequences = GetArrayField<List<int>>(comp, "answer").Get();

        addQuestion(module, Question.StackedSequences, correctAnswers: sequences.Select(x => x.Count.ToString()).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessStars(ModuleData module)
    {
        var comp = GetComponent(module, "Stars2Script");
        var originalNumber = GetField<TextMesh>(comp, "Number", isPublic: true).Get().text;

        yield return WaitForSolve;

        addQuestion(module, Question.StarsCenter, correctAnswers: new[] { originalNumber });
    }

    private IEnumerator<YieldInstruction> ProcessStarstruck(ModuleData module)
    {
        // This handler *should* ask about the color of a given star, but currently I can't turn a font character into a sprite.
        yield return WaitForSolve;

        var comp = GetComponent(module, "starstruck");
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^^&*()=+_,./<>?;:[]\\{}|-";
        var stars = GetArrayField<char>(comp, "piecePositions").Get(expectedLength: 3, validator: v => !valid.Contains(v) ? $"Expected chars in \"{valid}\"" : null);
        var text = GetArrayField<TextMesh>(comp, "bigStars", true).Get(expectedLength: 3)[0];

        addQuestions(module, makeQuestion(Question.StarstruckStar, module, text.font, text.GetComponent<Renderer>().sharedMaterial.mainTexture, correctAnswers: stars.Select(c => c.ToString()).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessStateOfAggregation(ModuleData module)
    {
        var comp = GetComponent(module, "StateOfAggregation");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var element = GetField<TextMesh>(comp, "Element", isPublic: true).Get().text;


        // Convert to proper case.
        addQuestion(module, Question.StateOfAggregationElement, correctAnswers: new[] { element.Substring(0, 1).ToUpperInvariant() + element.Substring(1).ToLowerInvariant() });
    }

    private IEnumerator<YieldInstruction> ProcessStellar(ModuleData module)
    {
        var comp = GetComponent(module, "StellarScript");
        yield return WaitForSolve;

        var lastPlayed = GetField<string>(comp, "lastPlayed").Get(validator: str => str.Length != 3 ? "expected length 3" : str.Any(ch => ch < 'a' || ch > 'z') ? "expected letters a–z" : null);
        var allLetters = lastPlayed.Select(c => c.ToString()).ToArray();
        addQuestions(module,
            makeQuestion(Question.StellarLetters, module, formatArgs: new[] { "Braille" }, correctAnswers: new[] { lastPlayed[0].ToString() }, preferredWrongAnswers: allLetters),
            makeQuestion(Question.StellarLetters, module, formatArgs: new[] { "tap code" }, correctAnswers: new[] { lastPlayed[1].ToString() }, preferredWrongAnswers: allLetters),
            makeQuestion(Question.StellarLetters, module, formatArgs: new[] { "Morse code" }, correctAnswers: new[] { lastPlayed[2].ToString() }, preferredWrongAnswers: allLetters));
    }

    private IEnumerator<YieldInstruction> ProcessStupidSlots(ModuleData module)
    {
        var comp = GetComponent(module, "StupidSlotsScript");
        yield return WaitForSolve;

        var values = GetArrayField<int>(comp, "allValues").Get(expectedLength: 6);
        var validPositions = Enumerable.Range(0, 6).Where(x => values[x] != 0);
        var posNames = new[] { "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right" };

        var qs = new List<QandA>();
        foreach (var pos in validPositions)
            qs.Add(makeQuestion(Question.StupidSlotsValues, module, formatArgs: new[] { posNames[pos] }, correctAnswers: new[] { values[pos].ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSubblyJubbly(ModuleData module)
    {
        var comp = GetComponent(module, "JubblyScript");
        yield return WaitForSolve;

        // Phrases can be customized in mod settings
        var all = GetField<Dictionary<char, List<string>>>(comp, "subblies").Get(v => v.Count == 26 ? "Subblies dict too big" : null).Values.SelectMany(x => x).ToArray();

        var used = GetArrayField<string>(comp, "subselect").Get(expectedLength: 9, validator: v => all.Contains(v) ? null : $"Unknown word {v}");
        addQuestion(module, Question.SubblyJubblySubstitutions, allAnswers: all, correctAnswers: used);
    }

    private IEnumerator<YieldInstruction> ProcessSubscribeToPewdiepie(ModuleData module)
    {
        var comp = GetComponent(module, "subscribeToPewdiepieScript");

        var pewdiepieNumber = GetField<int>(comp, "startingPewdiepie").Get();
        var tSeriesNumber = GetField<int>(comp, "startingTSeries").Get();

        yield return WaitForSolve;

        addQuestions(module,
           makeQuestion(Question.SubscribeToPewdiepieSubCount, module, formatArgs: new[] { "PewDiePie" }, correctAnswers: new[] { pewdiepieNumber.ToString() }),
           makeQuestion(Question.SubscribeToPewdiepieSubCount, module, formatArgs: new[] { "T-Series" }, correctAnswers: new[] { tSeriesNumber.ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessSubway(ModuleData module)
    {
        var comp = GetComponent(module, "subwayScript");

        var ingredients = GetStaticField<string[][]>(comp.GetType(), "ingredients")
            .Get(v => v.Length != 5 ? "expected length 5" : null)
            // Replace newlines with space
            .Select(v => v.Select(w => w.Replace("\n", " ")).ToArray())
            .ToArray();

        var allBreads = ingredients[0];
        var allMeats = ingredients[1];
        var allCheeses = ingredients[2];
        var allVegetables = ingredients[3];
        var allCondiments = ingredients[4];

        yield return WaitForSolve;

        if (GetField<bool>(comp, "pizzaTime").Get())
        {
            legitimatelyNoQuestion(module.Module, "The customer asked for pizza.");
            yield break;
        }

        if (GetField<bool>(comp, "asMuch").Get())
        {
            legitimatelyNoQuestion(module.Module, "You got fired.");
            yield break;
        }

        var order = GetField<List<int>[]>(comp, "order").Get(v => v.Length != 5 ? "expected length 5" : v.Any(lst => lst == null) ? "a list within ‘order’ was null" : v[0].Count == 0 ? "expected an item in ‘order[0]’" : null);
        var orderedBreadIndex = order[0][0];
        var orderedMeatIndexes = order[1].ToList(); // Take a copy because we may be modifying it
        var orderedCheeseIndexes = order[2];
        var orderedVegetablesIndexes = order[3];
        var orderedCondimentsIndexes = order[4].ToList(); // Take a copy because we may be modifying it

        // If asked for tuna, remove mayo from condiment indices and add tuna to meat indices
        if (GetField<bool>(comp, "replaceTuna").Get())
        {
            orderedMeatIndexes.Add(0);
            orderedCondimentsIndexes.Remove(1);
        }

        var requestedItems = orderedMeatIndexes.Select(i => allMeats[i])
            .Concat(orderedCheeseIndexes.Select(i => allCheeses[i]))
            .Concat(orderedVegetablesIndexes.Select(i => allVegetables[i]))
            .Concat(orderedCondimentsIndexes.Select(i => allCondiments[i])).ToArray();

        addQuestions(module,
            makeQuestion(Question.SubwayBread, module, correctAnswers: new[] { allBreads[orderedBreadIndex] }),
            makeQuestion(Question.SubwayItems, module, correctAnswers: allMeats.Concat(allCheeses).Concat(allVegetables).Concat(allCondiments).Except(requestedItems).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessSugarSkulls(ModuleData module)
    {
        var comp = GetComponent(module, "sugarSkulls");
        yield return WaitForSolve;

        var skulls = new List<string>();
        var textInfo = GetArrayField<TextMesh>(comp, "texts", isPublic: true).Get();
        for (var x = 0; x < textInfo.Length; x++)
            skulls.Add(textInfo[x].text);

        addQuestions(module,
           makeQuestion(Question.SugarSkullsSkull, module, formatArgs: new[] { "top" }, correctAnswers: new[] { skulls[0] }),
           makeQuestion(Question.SugarSkullsSkull, module, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { skulls[1] }),
           makeQuestion(Question.SugarSkullsSkull, module, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { skulls[2] }),
           makeQuestion(Question.SugarSkullsAvailability, module, formatArgs: new[] { "was" }, correctAnswers: skulls.ToArray()),
           makeQuestion(Question.SugarSkullsAvailability, module, formatArgs: new[] { "was not" }, correctAnswers: Question.SugarSkullsAvailability.GetAnswers().Except(skulls).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessSuitsAndColours(ModuleData module)
    {
        var comp = GetComponent(module, "SuitsAndColoursScript");
        yield return WaitForSolve;

        var suits = new[] { "spades", "hearts", "clubs", "diamonds" };
        var colours = new[] { "red", "orange", "yellow", "green" };
        var correctSuitIndices = GetListField<int>(comp, "ChosenSuits").Get(li => li.Count != 9 ? "expected length 9" : null);
        var correctColourIndices = GetListField<int>(comp, "ChosenColours").Get(li => li.Count != 9 ? "expected length 9" : null);

        var questions = new List<QandA>();
        for (int i = 0; i < 9; i++)
        {
            var coordinate = new Coord(3, 3, i);
            questions.Add(makeQuestion(Question.SuitsAndColourColour, module, questionSprite: Sprites.GenerateGridSprite(coordinate), correctAnswers: new[] { colours[correctColourIndices[i]] }));
            questions.Add(makeQuestion(Question.SuitsAndColourSuit, module, questionSprite: Sprites.GenerateGridSprite(coordinate), correctAnswers: new[] { colours[correctSuitIndices[i]] }));
        }

        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessSuperparsing(ModuleData module)
    {
        var comp = GetComponent(module, "SuperparsingScript");
        yield return WaitForSolve;

        string disp = GetField<string>(comp, "displayedWord").Get(str => str.Length != 4 ? "expected length 4" : null);
        addQuestion(module, Question.SuperparsingDisplayed, correctAnswers: new[] { disp });
    }

    private IEnumerator<YieldInstruction> ProcessSUSadmin(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "SusadminModule");
        var protocols = GetListField<int>(comp, "securityProtocols").Get(expectedLength: 3, validator: v => v is < 0 or > 5 ? "Expected range [0, 5]" : null);
        addQuestion(module, Question.SUSadminSecurity, correctAnswers: protocols.Select(i => Question.SUSadminSecurity.GetAnswers()[i]).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessSwitch(ModuleData module)
    {
        var comp = GetComponent(module, "Switch");
        var fldBottomColor = GetIntField(comp, "BottomColor");
        var fldTopColor = GetIntField(comp, "TopColor");
        var fldFirstSuccess = GetField<bool>(comp, "FirstSuccess");

        var colorNames = new[] { "red", "orange", "yellow", "green", "blue", "purple" };

        var topColor1 = fldTopColor.Get();
        var bottomColor1 = fldBottomColor.Get();
        var topColor2 = -1;
        var bottomColor2 = -1;

        var switchSelectable = GetField<KMSelectable>(comp, "FlipperSelectable", isPublic: true).Get();

        var prevInteract = switchSelectable.OnInteract;
        switchSelectable.OnInteract = delegate
        {
            var ret = prevInteract();

            // Only access bool and int fields in this button handler, so no exceptions are thrown
            var firstSuccess = fldFirstSuccess.Get();
            if (!firstSuccess)  // This means the user got a strike. Need to retrieve the new colors
            {
                topColor1 = fldTopColor.Get();
                bottomColor1 = fldBottomColor.Get();
            }
            else if (module.Unsolved)
            {
                topColor2 = fldTopColor.Get();
                bottomColor2 = fldBottomColor.Get();
            }
            return ret;
        };

        yield return WaitForSolve;

        if (topColor1 < 1 || topColor1 > 6 || bottomColor1 < 1 || bottomColor1 > 6 || topColor2 < 1 || topColor2 > 6 || bottomColor2 < 1 || bottomColor2 > 6)
            throw new AbandonModuleException($"topColor1/bottomColor1/topColor2/bottomColor2 have unexpected values: {topColor1}, {bottomColor1}, {topColor2}, {bottomColor2} (expected 1–6).");

        addQuestions(module,
            makeQuestion(Question.SwitchInitialColor, module, formatArgs: new[] { "top", "first" }, correctAnswers: new[] { colorNames[topColor1 - 1] }),
            makeQuestion(Question.SwitchInitialColor, module, formatArgs: new[] { "bottom", "first" }, correctAnswers: new[] { colorNames[bottomColor1 - 1] }),
            makeQuestion(Question.SwitchInitialColor, module, formatArgs: new[] { "top", "second" }, correctAnswers: new[] { colorNames[topColor2 - 1] }),
            makeQuestion(Question.SwitchInitialColor, module, formatArgs: new[] { "bottom", "second" }, correctAnswers: new[] { colorNames[bottomColor2 - 1] }));
    }

    private IEnumerator<YieldInstruction> ProcessSwitches(ModuleData module)
    {
        var comp = GetComponent(module, "SwitchModule");
        var mthCurConfig = GetMethod<object>(comp, "GetCurrentConfiguration", 0);
        var switches = GetArrayField<MonoBehaviour>(comp, "Switches", isPublic: true).Get(expectedLength: 5);

        // The special font Souvenir uses to display switch states uses Q for up and R for down
        var initialState = switches.Select(sw => sw.GetComponent<Animator>().GetBool("Up") ? "Q" : "R").JoinString();

        yield return WaitForSolve;

        addQuestion(module, Question.SwitchesInitialPosition, correctAnswers: new[] { initialState });
    }

    private IEnumerator<YieldInstruction> ProcessSwitchingMaze(ModuleData module)
    {
        var comp = GetComponent(module, "SwitchingMazeScript");
        var seedTextMesh = GetField<TextMesh>(comp, "Seedling", isPublic: true).Get();
        var fldNumberBasis = GetField<int>(comp, "NumberBasis");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var seed = seedTextMesh.text;
        var numberBasis = fldNumberBasis.Get();

        var hadStrike = false;
        module.Module.OnStrike += delegate { hadStrike = true; return false; };

        while (module.Unsolved)
        {
            if (hadStrike)
            {
                seed = seedTextMesh.text;
                numberBasis = fldNumberBasis.Get();
                hadStrike = false;
            }
            yield return null;
        }

        var seedSplit = Regex.Replace(seed, " ", "").Split(':');
        var colorsOfTheMaze = GetArrayField<string>(comp, "ColorsOfMaze").Get();

        addQuestions(module,
            makeQuestion(Question.SwitchingMazeSeed, module, formatArgs: null, correctAnswers: new[] { seedSplit[1] }),
            makeQuestion(Question.SwitchingMazeColor, module, formatArgs: null, correctAnswers: new[] { colorsOfTheMaze[numberBasis] }, preferredWrongAnswers: colorsOfTheMaze));
    }

    private IEnumerator<YieldInstruction> ProcessSymbolCycle(ModuleData module)
    {
        var comp = GetComponent(module, "SymbolCycleModule");
        var fldCycles = GetArrayField<int[]>(comp, "_cycles");
        var fldState = GetField<object>(comp, "_state");

        int[][] cycles = null;
        while (fldState.Get().ToString() != "Solved")
        {
            cycles = fldCycles.Get(expectedLength: 2, validator: x => x.Length < 2 || x.Length > 5 ? "expected length 2–5" : null);

            while (fldState.Get().ToString() == "Cycling")
                yield return new WaitForSeconds(0.1f);

            while (fldState.Get().ToString() == "Retrotransphasic" || fldState.Get().ToString() == "Anterodiametric")
                yield return new WaitForSeconds(0.1f);
        }

        if (cycles == null)
            throw new AbandonModuleException("No cycles.");

        yield return WaitForSolve;
        addQuestions(module, new[] { "left", "right" }.Select((screen, ix) => makeQuestion(Question.SymbolCycleSymbolCounts, module, formatArgs: new[] { screen }, correctAnswers: new[] { cycles[ix].Length.ToString() })));
    }

    private IEnumerator<YieldInstruction> ProcessSymbolicCoordinates(ModuleData module)
    {
        var comp = GetComponent(module, "symbolicCoordinatesScript");
        var letter1 = GetField<string>(comp, "letter1").Get();
        var letter2 = GetField<string>(comp, "letter2").Get();
        var letter3 = GetField<string>(comp, "letter3").Get();

        var stageLetters = new[] { letter1.Split(' '), letter2.Split(' '), letter3.Split(' ') };

        if (stageLetters.Any(x => x.Length != 3) || stageLetters.SelectMany(x => x).Any(y => !"ACELP".Contains(y)))
            throw new AbandonModuleException($"One of the stages has fewer than 3 symbols or symbols are of unexpected value (expected symbols “ACELP”, got “{stageLetters.Select(x => $"“{x.JoinString()}”").JoinString(", ")}”).");

        yield return WaitForSolve;
        GetField<TextMesh>(comp, "lettersText", isPublic: true).Get().text = "";
        GetField<TextMesh>(comp, "digitsText", isPublic: true).Get().text = "";

        foreach (var btnFieldName in new[] { "lettersUp", "lettersDown", "digitsUp", "digitsDown" })
        {
            var btn = GetField<KMSelectable>(comp, btnFieldName, isPublic: true).Get();
            btn.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
                btn.AddInteractionPunch(0.5f);
                return false;
            };
        }

        var position = new[] { "left", "middle", "right" };
        addQuestions(module, stageLetters.SelectMany((letters, stage) => letters.Select((letter, pos) => makeQuestion(
            Question.SymbolicCoordinateSymbols,
            module,
            formatArgs: new[] { position[pos], Ordinal(stage + 1) },
            correctAnswers: new[] { SymbolicCoordinatesSprites["ACELP".IndexOf(letter, StringComparison.Ordinal)] },
            preferredWrongAnswers: SymbolicCoordinatesSprites))));
    }

    private IEnumerator<YieldInstruction> ProcessSymbolicTasha(ModuleData module)
    {
        var comp = GetComponent(module, "symbolicTasha");
        yield return WaitForSolve;

        var positionNames = new[] { "Top", "Right", "Bottom", "Left" };
        var colorNames = new[] { "Pink", "Green", "Yellow", "Blue" };

        var positionNamesLc = new[] { "top", "right", "bottom", "left" };
        var colorNamesLc = new[] { "pink", "green", "yellow", "blue" };

        var cracked = GetArrayField<bool>(comp, "cracked").Get();
        var flashing = GetArrayField<int>(comp, "flashing").Get();
        var presentSymbols = GetField<Array>(comp, "presentSymbols").Get(validator: arr => arr.Length != 4 ? "expected length 4" : null).Cast<object>().Select(obj => (int) obj).ToArray();
        var buttonColors = GetField<Array>(comp, "buttonColors").Get(validator: arr => arr.Length != 4 ? "expected length 4" : null).Cast<object>().Select(obj => (int) obj).ToArray();

        var qs = new List<QandA>();
        for (var pos = 0; pos < 5; pos++)
            qs.Add(makeQuestion(Question.SymbolicTashaFlashes, module, formatArgs: new[] { Ordinal(pos + 1) },
                correctAnswers: new[] { positionNames[flashing[pos]], colorNames[buttonColors[flashing[pos]]] }));

        for (var btn = 0; btn < 4; btn++)
            if (presentSymbols[btn] < 0)
            {
                qs.Add(makeQuestion(Question.SymbolicTashaSymbols, module, formatArgs: new[] { positionNamesLc[btn] }, correctAnswers: new[] { SymbolicTashaSprites[-presentSymbols[btn] - 1] }, preferredWrongAnswers: SymbolicTashaSprites));
                qs.Add(makeQuestion(Question.SymbolicTashaSymbols, module, formatArgs: new[] { colorNamesLc[buttonColors[btn]] }, correctAnswers: new[] { SymbolicTashaSprites[-presentSymbols[btn] - 1] }, preferredWrongAnswers: SymbolicTashaSprites));
            }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSync_125_3(ModuleData module)
    {
        var comp = GetComponent(module, "sync125_3");
        var fldTextId = GetIntField(comp, "textId");
        var fldStage = GetIntField(comp, "stage");
        var words = GetArrayField<string>(comp, "words").Get();
        var screenText = GetField<TextMesh>(comp, "screenText", isPublic: true).Get();
        var submitButton = GetField<KMSelectable>(comp, "submitButton", isPublic: true).Get();

        var textIds = new int[4];

        while (!_isActivated)
            yield return null;

        var oldInteract = submitButton.OnInteract;
        submitButton.OnInteract = () =>
        {
            textIds[fldStage.Get(0, 3)] = fldTextId.Get();
            return oldInteract();
        };

        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (var stage = 0; stage < 3; stage++)
            qs.Add(makeQuestion(Question.Sync125_3Word, module, screenText.font, screenText.GetComponent<MeshRenderer>().sharedMaterial.mainTexture, formatArgs: new[] { (stage + 1).ToString() }, correctAnswers: new[] { words[textIds[stage]] }, preferredWrongAnswers: words));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessSynonyms(ModuleData module)
    {
        var comp = GetComponent(module, "Synonyms");
        var numberText = GetField<TextMesh>(comp, "NumberText", isPublic: true).Get();

        if (numberText.text == null || !int.TryParse(numberText.text, out var number) || number < 0 || number > 9)
            throw new AbandonModuleException($"The display text (“{numberText.text ?? "<null>"}”) is not an integer 0–9.");

        yield return WaitForSolve;

        numberText.gameObject.SetActive(false);
        GetField<TextMesh>(comp, "BadLabel", isPublic: true).Get().text = "INPUT";
        GetField<TextMesh>(comp, "GoodLabel", isPublic: true).Get().text = "ACCEPTED";

        addQuestion(module, Question.SynonymsNumber, correctAnswers: new[] { number.ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessSysadmin(ModuleData module)
    {
        var comp = GetComponent(module, "SysadminModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Alfa-Bravo because the module was force-solved.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        var fixedErrorCodes = GetProperty<HashSet<string>>(comp, "fixedErrorCodes", true).Get();
        if (fixedErrorCodes.Count == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Sysadmin because there are no errors to ask about.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }
        var allErrorCodes = GetStaticProperty<HashSet<string>>(comp.GetType(), "allErrorCodes", true).Get();
        addQuestion(module, Question.SysadminFixedErrorCodes, correctAnswers: fixedErrorCodes.ToArray(), preferredWrongAnswers: allErrorCodes.ToArray());
    }
}
