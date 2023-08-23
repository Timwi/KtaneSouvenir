using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.Scripts.Platform.Common;
using BombGame;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessSafetySquare(KMBombModule module)
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

        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SafetySquare);

        var qs = colors.Select((col, ix) => makeQuestion(Question.SafetySquareDigits, _SafetySquare, formatArgs: new[] { col }, correctAnswers: new[] { digits[ix] })).ToList();
        qs.Add(makeQuestion(Question.SafetySquareSpecialRule, _SafetySquare, correctAnswers: new[] { specialRules[symbol] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSamsung(KMBombModule module)
    {
        var comp = GetComponent(module, "theSamsung");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Samsung);

        var appPositions = GetListField<int>(comp, "positionNumbers").Get();
        var appNames = new[] { "Duolingo", "Google Maps", "Kindle", "Google Authenticator", "Photomath", "Spotify", "Google Arts & Culture", "Discord" };
        var qs = new List<QandA>();
        for (int i = 0; i < 8; i++)
            qs.Add(makeQuestion(Question.SamsungAppPositions, _Samsung, formatArgs: new[] { appNames[i] }, correctAnswers: new[] { GetAnswers(Question.SamsungAppPositions)[appPositions[i]] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessScavengerHunt(KMBombModule module)
    {
        var comp = GetComponent(module, "scavengerHunt");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
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

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ScavengerHunt);

        addQuestions(module,
            makeQuestion(Question.ScavengerHuntKeySquare, _ScavengerHunt, correctAnswers: new[] { new Coord(4, 4, keySquare) }),
            makeQuestion(Question.ScavengerHuntColoredTiles, _ScavengerHunt, formatArgs: new[] { "red" }, correctAnswers: redTiles.Select(c => new Coord(4, 4, c)).ToArray()),
            makeQuestion(Question.ScavengerHuntColoredTiles, _ScavengerHunt, formatArgs: new[] { "green" }, correctAnswers: greenTiles.Select(c => new Coord(4, 4, c)).ToArray()),
            makeQuestion(Question.ScavengerHuntColoredTiles, _ScavengerHunt, formatArgs: new[] { "blue" }, correctAnswers: blueTiles.Select(c => new Coord(4, 4, c)).ToArray()));
    }

    private IEnumerable<object> ProcessSchlagDenBomb(KMBombModule module)
    {
        var comp = GetComponent(module, "qSchlagDenBomb");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SchlagDenBomb);

        var contestantName = GetField<string>(comp, "contestantName").Get();
        var contestantScore = GetIntField(comp, "scoreC").Get(min: 0, max: 75);
        var bombScore = GetIntField(comp, "scoreB").Get(min: 0, max: 75);

        addQuestions(module,
            makeQuestion(Question.SchlagDenBombContestantName, _SchlagDenBomb, correctAnswers: new[] { contestantName }),
            makeQuestion(Question.SchlagDenBombContestantScore, _SchlagDenBomb, correctAnswers: new[] { contestantScore.ToString() }, preferredWrongAnswers:
               Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(0, 75).ToString()).Distinct().Take(6).ToArray()),
            makeQuestion(Question.SchlagDenBombBombScore, _SchlagDenBomb, correctAnswers: new[] { bombScore.ToString() }, preferredWrongAnswers:
               Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(0, 75).ToString()).Distinct().Take(6).ToArray()));
    }

    private IEnumerable<object> ProcessScramboozledEggain(KMBombModule module)
    {
        var comp = GetComponent(module, "ScramboozledEggainScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_ScramboozledEggain);

        var wordList = GetStaticField<string[]>(comp.GetType(), "_wordList").Get().Select(i => i.Substring(0, 1) + i.Substring(1).ToLowerInvariant()).ToArray();
        var selectedWords = GetArrayField<string>(comp, "_selectedWords").Get().Select(i => i.Substring(0, 1) + i.Substring(1).ToLowerInvariant()).ToArray();

        var qs = new List<QandA>();
        for (int i = 0; i < 4; i++)
            qs.Add(makeQuestion(Question.ScramboozledEggainWord, _ScramboozledEggain, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { selectedWords[i] }, preferredWrongAnswers: wordList));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessScrutinySquares(KMBombModule module)
    {
        var comp = GetComponent(module, "ScrutinySquaresScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ScrutinySquares);

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

    private IEnumerable<object> ProcessScripting(KMBombModule module)
    {
        var comp = GetComponent(module, "KritScript");

        var solved = false;
        module.OnPass += () => { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Scripting);

        var variableType = GetField<string>(comp, "VariableKindValue", isPublic: true).Get();
        addQuestion(module, Question.ScriptingVariableDataType, correctAnswers: new[] { variableType });
    }

    private IEnumerable<object> ProcessSeaShells(KMBombModule module)
    {
        var comp = GetComponent(module, "SeaShellsModule");
        var fldRow = GetIntField(comp, "row");
        var fldCol = GetIntField(comp, "col");
        var fldKeynum = GetIntField(comp, "keynum");
        var fldStage = GetIntField(comp, "stage");
        var fldSolved = GetField<bool>(comp, "isPassed");
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
                if (fldSolved.Get())
                    goto solved;
            }

            var stage = fldStage.Get(min: 0, max: 2);
            rows[stage] = fldRow.Get();
            cols[stage] = fldCol.Get();
            keynums[stage] = fldKeynum.Get();

            while (fldDisplay.Get().text != " ")
            {
                yield return new WaitForSeconds(.1f);
                if (fldSolved.Get())
                    goto solved;
            }
        }

        solved:
        _modulesSolved.IncSafe(_SeaShells);

        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.SeaShells1, _SeaShells, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { new[] { "she sells", "she shells", "sea shells", "sea sells" }[rows[i]] }));
            qs.Add(makeQuestion(Question.SeaShells2, _SeaShells, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { new[] { "sea shells", "she shells", "sea sells", "she sells" }[cols[i]] }));
            qs.Add(makeQuestion(Question.SeaShells3, _SeaShells, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { new[] { "sea shore", "she sore", "she sure", "seesaw" }[keynums[i]] }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSemamorse(KMBombModule module)
    {
        var comp = GetComponent(module, "semamorse");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Semamorse);

        var letters = GetArrayField<int[]>(comp, "displayedLetters").Get(expectedLength: 2, validator: arr => arr.Length != 5 ? "expected length 5" : arr.Any(v => v < 0 || v > 25) ? "expected range 0–25" : null);
        var relevantIx = Enumerable.Range(0, letters[0].Length).First(ix => letters[0][ix] != letters[1][ix]);
        var colorNames = new[] { "red", "green", "cyan", "indigo", "pink" };
        var colors = GetArrayField<int>(comp, "displayedColors").Get(expectedLength: 5, validator: c => c < 0 || c >= colorNames.Length ? $"expected range 0–{colorNames.Length - 1}" : null);
        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.SemamorseColor, _Semamorse, correctAnswers: new[] { colorNames[colors[relevantIx]] }));
        qs.Add(makeQuestion(Question.SemamorseLetters, _Semamorse, formatArgs: new[] { "semaphore" }, correctAnswers: new[] { ((char) ('A' + letters[0][relevantIx])).ToString() }));
        qs.Add(makeQuestion(Question.SemamorseLetters, _Semamorse, formatArgs: new[] { "Morse" }, correctAnswers: new[] { ((char) ('A' + letters[1][relevantIx])).ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSequencyclopedia(KMBombModule module)
    {
        var comp = GetComponent(module, "TheSequencyclopediaScript");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Sequencyclopedia);

        var maxSeqId = int.Parse(GetField<string>(comp, "Tridal").Get(str => str == "" ? "Tridal is empty, meaning module was unable to gather the amount of sequence" : null));
        var answer = GetField<string>(comp, "APass").Get();
        var wrongAnswers = new HashSet<string>();
        wrongAnswers.Add(answer);
        while (wrongAnswers.Count < 6)
            foreach (var ans in new AnswerGenerator.Integers(0, maxSeqId, "'A'000000").GetAnswers(this).Take(6 - wrongAnswers.Count))
                wrongAnswers.Add(ans);

        addQuestions(module, makeQuestion(Question.SequencyclopediaSequence, _Sequencyclopedia, correctAnswers: new[] { answer }, preferredWrongAnswers: wrongAnswers.ToArray()));
    }

    private IEnumerable<object> ProcessSetTheory(KMBombModule module)
    {
        var comp = GetComponent(module, "SetTheoryScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");
        var fldEquations = GetField<Array>(comp, "_equations");
        var mthGenerate = GetMethod<object>(comp, "GenerateEquationForStage", 1);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SetTheory);

        var equations = fldEquations.Get(v => v.Length != 4 ? "expected length 4" : null).Cast<object>().Select(eq => eq.ToString()).ToArray();
        addQuestions(module, Enumerable.Range(0, 4).Select(stage =>
        {
            var wrongAnswers = new HashSet<string> { equations[stage] };
            while (wrongAnswers.Count < 4)
                wrongAnswers.Add(mthGenerate.Invoke(stage).ToString());
            return makeQuestion(Question.SetTheoryEquations, _SetTheory, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { equations[stage] }, preferredWrongAnswers: wrongAnswers.ToArray());
        }));
    }

    private IEnumerable<object> ProcessShapesAndBombs(KMBombModule module)
    {
        var comp = GetComponent(module, "ShapesBombs");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var initialLetter = GetIntField(comp, "selectLetter").Get(min: 0, max: 14);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ShapesAndBombs);

        var letterChars = new[] { "A", "B", "D", "E", "G", "I", "K", "L", "N", "O", "P", "S", "T", "X", "Y" };
        addQuestion(module, Question.ShapesAndBombsInitialLetter, correctAnswers: new[] { letterChars[initialLetter] });
    }

    private IEnumerable<object> ProcessShapeShift(KMBombModule module)
    {
        var comp = GetComponent(module, "ShapeShiftModule");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ShapeShift);

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
            _legitimatelyNoQuestions.Add(module);
        }
        else
            addQuestion(module, Question.ShapeShiftInitialShape, correctAnswers: new[] { ((char) ('A' + stR + (4 * stL))).ToString() }, preferredWrongAnswers: answers.ToArray());
    }

    private IEnumerable<object> ProcessShiftedMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "shiftedMazeScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var expectedCBTexts = new[] { "W", "B", "Y", "M", "G" };
        var colorNames = new[] { "White", "Blue", "Yellow", "Magenta", "Green" };
        var cornerNames = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };

        var colorblindTexts = GetArrayField<TextMesh>(comp, "colorblindTexts", isPublic: true).Get(expectedLength: 4).Select(c => c.text).ToArray();
        var invalid = colorblindTexts.IndexOf(c => !expectedCBTexts.Contains(c));
        if (invalid != -1)
            throw new AbandonModuleException($"Found unexpected color text: “{colorblindTexts[invalid]}”.");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ShiftedMaze);

        addQuestions(module, Enumerable.Range(0, 4).Select(corner => makeQuestion(Question.ShiftedMazeColors, _ShiftedMaze,
            formatArgs: new[] { cornerNames[corner] }, correctAnswers: new[] { colorNames[Array.IndexOf(expectedCBTexts, colorblindTexts[corner])] })));
    }

    private IEnumerable<object> ProcessShiftingMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "ShiftingMazeScript");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");
        var seedTextMesh = GetField<TextMesh>(comp, "Seedling", isPublic: true).Get();
        var seed = seedTextMesh.text;

        var hadStrike = false;
        module.OnStrike += delegate { hadStrike = true; return false; };

        while (!fldSolved.Get())
        {
            if (hadStrike)
            {
                seed = seedTextMesh.text;
                hadStrike = false;
            }
            yield return null;
        }

        _modulesSolved.IncSafe(_ShiftingMaze);
        var seedSplit = Regex.Replace(seed, " ", "").Split(':');
        addQuestions(module, makeQuestion(Question.ShiftingMazeSeed, _ShiftingMaze, formatArgs: null, correctAnswers: new[] { seedSplit[1] }));
    }

    private IEnumerable<object> ProcessShogiIdentification(KMBombModule module)
    {
        var comp = GetComponent(module, "ShogiIdentificationScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ShogiIdentification);

        var fldPiece = GetField<object>(comp, "chosenPiece");
        var propName = GetProperty<string>(fldPiece.Get(), "name", isPublic: true);

        addQuestion(module, Question.ShogiIdentificationPiece, correctAnswers: new[] { propName.Get() });
    }

    private IEnumerable<object> ProcessSignLanguage(KMBombModule module)
    {
        var comp = GetComponent(module, "SignLanguageAlphabetScript");
        var fldSolved = GetField<bool>(comp, "IsSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SignLanguage);

        var entryObj = GetField<object>(comp, "entry").Get();
        var answer = GetField<string>(entryObj, "word").Get();

        addQuestion(module, Question.SignLanguageWord, correctAnswers: new[] { answer });
    }

    private IEnumerable<object> ProcessSillySlots(KMBombModule module)
    {
        var comp = GetComponent(module, "SillySlots");
        var fldSolved = GetField<bool>(comp, "solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_SillySlots);

        var prevSlots = GetField<IList>(comp, "mPreviousSlots").Get(lst => lst.Cast<object>().Any(obj => !(obj is Array ar) || ar.Length != 3) ? "expected arrays of length 3" : null);
        if (prevSlots.Count < 2)
        {
            // Legitimate: first stage was a keep already
            Debug.Log($"[Souvenir #{_moduleId}] No question for Silly Slots because there was only one stage.");
            _legitimatelyNoQuestions.Add(module);
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
                qs.Add(makeQuestion(Question.SillySlots, _SillySlots, formatArgs: new[] { ordinal(slot + 1), ordinal(stage + 1) }, correctAnswers: new[] { slotStrings[slot] }, preferredWrongAnswers: slotStrings));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSiloAuthorization(KMBombModule module)
    {
        var comp = GetComponent(module, "WarGamesModuleScript");
        var fldStage = GetField<object>(comp, "mStatus");
        while (fldStage.Get().ToString() != "Solved")
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_SiloAuthorization);
        var qs = new List<QandA>();

        var messageColor = GetField<object>(comp, "correctColor").Get();
        var colorNames = new[] { "Red-Alpha", "Yellow-Alpha", "Green-Alpha" };
        var correctColor = messageColor.ToString() == "Red" ? colorNames[0] : messageColor.ToString() == "Yellow" ? colorNames[1] : colorNames[2];
        qs.Add(makeQuestion(Question.SiloAuthorizationMessageType, _SiloAuthorization, correctAnswers: new[] { correctColor }, preferredWrongAnswers: colorNames));

        var outMessages = GetArrayField<string>(comp, "outMessages").Get();
        var messages = new[] { outMessages[0], outMessages[2] };
        for (int message = 0; message < 2; message++)
            qs.Add(makeQuestion(Question.SiloAuthorizationEncryptedMessage, _SiloAuthorization, formatArgs: new[] { ordinal(message + 1) }, correctAnswers: new[] { messages[message] }, preferredWrongAnswers: messages));

        qs.Add(makeQuestion(Question.SiloAuthorizationAuthCode, _SiloAuthorization, correctAnswers: new[] { GetField<int>(comp, "outAuthCode").Get().ToString("0000") }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonSaid(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSaidScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_SimonSaid);

        var btnColors = GetListField<int>(comp, "btnColors").Get();
        var colorNames = GetArrayField<string>(comp, "colorNames").Get();
        var correctPresses = GetListField<int>(comp, "correctBtnPresses").Get();

        addQuestions(module, correctPresses.Select((val, ix) => makeQuestion(Question.SimonSaidPresses, _SimonSaid, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colorNames[btnColors[val]] })));
    }

    private IEnumerable<object> ProcessSimonSamples(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSamples");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSamples);

        var calls = GetListField<string>(comp, "_calls").Get(expectedLength: 3);
        if (Enumerable.Range(1, 2).Any(i => calls[i].Length <= calls[i - 1].Length || !calls[i].StartsWith(calls[i - 1])))
            throw new AbandonModuleException($"_calls=[{calls.Select(c => $"“{c}”").JoinString(", ")}]; expected each element to start with the previous.");

        var formatArgs = new[] { "played in the first stage", "added in the second stage", "added in the third stage" };
        addQuestions(module, calls.Select((c, ix) => makeQuestion(Question.SimonSamplesSamples, _SimonSamples, formatArgs: new[] { formatArgs[ix] }, correctAnswers: new[] { (ix == 0 ? c : c.Substring(calls[ix - 1].Length)).Replace("0", "K").Replace("1", "S").Replace("2", "H").Replace("3", "O") })));
    }

    private IEnumerable<object> ProcessSimonSays(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSays);

        var colorNames = new[] { "red", "blue", "green", "yellow" };
        var sequence = GetArrayField<int>(comp, "currentSequence").Get(validator: arr => arr.Any(i => i < 0 || i >= colorNames.Length) ? "expected values 0–3" : null);
        addQuestions(module, Enumerable.Range(0, sequence.Length).Select(i =>
            makeQuestion(Question.SimonSaysFlash, _SimonSays, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colorNames[sequence[i]] })));
    }

    private IEnumerable<object> ProcessSimonScrambles(KMBombModule module)
    {
        var comp = GetComponent(module, "simonScramblesScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonScrambles);

        int[] sequence = GetArrayField<int>(comp, "sequence").Get(expectedLength: 10);
        string[] colors = GetArrayField<string>(comp, "colorNames").Get(expectedLength: 4);

        if (sequence[9] < 0 || sequence[9] >= colors.Length)
            throw new AbandonModuleException($"‘sequence[9]’ points to illegal color: {sequence[9]} (expected 0-3).");

        addQuestions(module, sequence.Select((val, ix) => makeQuestion(Question.SimonScramblesColors, _SimonScrambles, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colors[val] })));
    }

    private IEnumerable<object> ProcessSimonScreams(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonScreamsModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_SimonScreams);

        var seqs = GetArrayField<int[]>(comp, "_sequences").Get(expectedLength: 3);
        var stageIxs = GetArrayField<int>(comp, "_stageIxs").Get(expectedLength: 3);
        var rules = GetField<Array>(comp, "_rowCriteria").Get(ar => ar.Length != 6 ? "expected length 6" : null);
        var colorsRaw = GetField<Array>(comp, "_colors").Get(ar => ar.Length != 6 ? "expected length 6" : null);     // array of enum values
        var colors = colorsRaw.Cast<object>().Select(obj => obj.ToString()).ToArray();

        var qs = new List<QandA>();
        var lastSeq = seqs.Last();
        foreach (var i in stageIxs)     // Only ask about the flashing colors that were relevant in the big table
            qs.Add(makeQuestion(Question.SimonScreamsFlashing, _SimonScreams, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colors[lastSeq[i]] }));

        // First determine which rule applied in which stage
        var fldCheck = GetField<Func<int[], bool>>(rules.GetValue(0), "Check", isPublic: true);
        var fldRuleName = GetField<string>(rules.GetValue(0), "Name", isPublic: true);
        var stageRules = new int[seqs.Length];
        for (int i = 0; i < seqs.Length; i++)
        {
            stageRules[i] = rules.Cast<object>().IndexOf(rule => fldCheck.GetFrom(rule)(seqs[i]));
            if (stageRules[i] == -1)
                throw new AbandonModuleException($"Apparently none of the criteria applies to Stage {i + 1} ([{seqs[i].Select(ix => colors[ix]).JoinString(", ")}]).");
        }

        // Now set the questions
        // Skip the last rule because it’s the “otherwise” row
        for (int rule = 0; rule < rules.Length - 1; rule++)
        {
            var applicableStages = new List<string>();
            for (int stage = 0; stage < stageRules.Length; stage++)
                if (stageRules[stage] == rule)
                    applicableStages.Add(ordinal(stage + 1));
            if (applicableStages.Count > 0)
                qs.Add(makeQuestion(Question.SimonScreamsRule, _SimonScreams,
                    formatArgs: new[] { fldRuleName.GetFrom(rules.GetValue(rule)) },
                    correctAnswers: new[] { applicableStages.Count == stageRules.Length ? "all of them" : applicableStages.JoinString(", ", lastSeparator: " and ") }));
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonSelects(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSelectsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSelects);

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

        addQuestions(module, seqs.SelectMany((seq, stage) => seq.Select((col, ix) => makeQuestion(Question.SimonSelectsOrder, _SimonSelects,
            formatArgs: new[] { ordinal(ix + 1), ordinal(stage + 1) },
            correctAnswers: new[] { col }))));
    }

    private IEnumerable<object> ProcessSimonSends(KMBombModule module)
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

        // Simon Sends sets “_answerSoFar” to null when it’s done
        var fldAnswerSoFar = GetListField<int>(comp, "_answerSoFar");
        while (fldAnswerSoFar.Get(nullAllowed: true) != null)
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_SimonSends);
        addQuestions(module,
            makeQuestion(Question.SimonSendsReceivedLetters, _SimonSends, formatArgs: new[] { "red" }, correctAnswers: new[] { charR }, preferredWrongAnswers: new[] { charG, charB }),
            makeQuestion(Question.SimonSendsReceivedLetters, _SimonSends, formatArgs: new[] { "green" }, correctAnswers: new[] { charG }, preferredWrongAnswers: new[] { charR, charB }),
            makeQuestion(Question.SimonSendsReceivedLetters, _SimonSends, formatArgs: new[] { "blue" }, correctAnswers: new[] { charB }, preferredWrongAnswers: new[] { charR, charG }));
    }

    private IEnumerable<object> ProcessSimonShapes(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonShapesScript");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var fldAllFinalShapes = GetListField<List<int>>(comp, "_possibleFinalShapes");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonShapes);

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

    private IEnumerable<object> ProcessSimonShouts(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonShoutsModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonShouts);

        var diagramBPositions = GetArrayField<int>(comp, "_diagramBPositions").Get(expectedLength: 4, validator: b => b < 0 || b > 24 ? "expected range 0–24" : null);
        var diagramB = GetField<string>(comp, "_diagramB").Get(str => str.Length != 24 ? "expected length 24" : str.Any(ch => ch < 'A' || ch > 'Z') ? "expected letters A–Z" : null);

        var qs = new List<QandA>();
        var buttonNames = new[] { "top", "right", "bottom", "left" };
        for (int i = 0; i < 4; i++)
            qs.Add(makeQuestion(Question.SimonShoutsFlashingLetter, _SimonShouts, formatArgs: new[] { buttonNames[i] }, correctAnswers: new[] { diagramB[diagramBPositions[i]].ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonShrieks(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonShrieksModule");
        var fldStage = GetIntField(comp, "_stage");

        while (fldStage.Get() < 3)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonShrieks);

        var arrow = GetIntField(comp, "_arrow").Get(min: 0, max: 6);
        var flashingButtons = GetArrayField<int>(comp, "_flashingButtons").Get(expectedLength: 8, validator: b => b < 0 || b > 6 ? "expected range 0–6" : null);

        var qs = new List<QandA>();
        for (int i = 0; i < flashingButtons.Length; i++)
            qs.Add(makeQuestion(Question.SimonShrieksFlashingButton, _SimonShrieks, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { ((flashingButtons[i] + 7 - arrow) % 7).ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonSignals(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSignalsModule");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSignals);

        var numRotations = GetArrayField<int>(comp, "_numRotations").Get(expectedLength: 5);
        var colorsShapes = GetArrayField<int>(comp, "_colorsShapes").Get(expectedLength: 5);
        var qs = new List<QandA>();

        var colorNames = new[] { "red", "green", "blue", "gray" };

        for (var i = 0; i < 5; i++)
        {
            // If this arrow has a unique color, we can ask for its shape and its number of rotations
            if (colorsShapes.Count(cs => (cs >> 3) == (colorsShapes[i] >> 3)) == 1)
            {
                qs.Add(makeQuestion(Question.SimonSignalsColorToShape, _SimonSignals,
                    formatArgs: new[] { colorNames[colorsShapes[i] >> 3] }, correctAnswers: new[] { SimonSignalsSprites[colorsShapes[i] & 7] }));
                qs.Add(makeQuestion(Question.SimonSignalsColorToRotations, _SimonSignals,
                    formatArgs: new[] { colorNames[colorsShapes[i] >> 3] }, correctAnswers: new[] { numRotations[i].ToString() }));
            }

            // If this arrow has a unique shape, we can ask for its color and its number of rotations
            if (colorsShapes.Count(cs => (cs & 7) == (colorsShapes[i] & 7)) == 1)
            {
                qs.Add(makeQuestion(Question.SimonSignalsShapeToColor, _SimonSignals,
                    questionSprite: SimonSignalsSprites[colorsShapes[i] & 7], correctAnswers: new[] { colorNames[colorsShapes[i] >> 3] }));
                qs.Add(makeQuestion(Question.SimonSignalsShapeToRotations, _SimonSignals,
                    questionSprite: SimonSignalsSprites[colorsShapes[i] & 7], correctAnswers: new[] { numRotations[i].ToString() }));
            }

            // If this arrow has a unique number of rotations, we can ask for its color and shape
            if (numRotations.Count(nr => nr == numRotations[i]) == 1)
            {
                qs.Add(makeQuestion(Question.SimonSignalsRotationsToColor, _SimonSignals,
                    formatArgs: new[] { numRotations[i].ToString() }, correctAnswers: new[] { colorNames[colorsShapes[i] >> 3] }));
                qs.Add(makeQuestion(Question.SimonSignalsRotationsToShape, _SimonSignals,
                    formatArgs: new[] { numRotations[i].ToString() }, correctAnswers: new[] { SimonSignalsSprites[colorsShapes[i] & 7] }));
            }
        }

        if (qs.Count == 0)
            legitimatelyNoQuestion(module, "none of the arrows had a unique color, shape, or number of directions.");
        else
            addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonSimons(KMBombModule module)
    {
        var comp = GetComponent(module, "simonsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSimons);

        var flashes = new[] { "TR", "TY", "TG", "TB", "LR", "LY", "LG", "LB", "RR", "RY", "RG", "RB", "BR", "BY", "BG", "BB" };
        var buttonFlashes = GetArrayField<KMSelectable>(comp, "selButtons").Get(expectedLength: 5, validator: sel => !flashes.Contains(sel.name.ToUpperInvariant()) ? "invalid flash" : null);
        addQuestions(module, buttonFlashes.Select((btn, i) =>
            makeQuestion(Question.SimonSimonsFlashingColors, _SimonSimons, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { btn.name.ToUpperInvariant() })));
    }

    private IEnumerable<object> ProcessSimonSings(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSingsModule");
        var fldCurStage = GetIntField(comp, "_curStage");

        while (fldCurStage.Get() < 3)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSings);

        var noteNames = new[] { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };
        var flashingColorSequences = GetArrayField<int[]>(comp, "_flashingColors").Get(expectedLength: 3, validator: seq => seq.Any(col => col < 0 || col >= noteNames.Length) ? $"expected range 0–{noteNames.Length - 1}" : null);
        addQuestions(module, flashingColorSequences.SelectMany((seq, stage) => seq.Select((col, ix) => makeQuestion(Question.SimonSingsFlashing, _SimonSings, formatArgs: new[] { ordinal(ix + 1), ordinal(stage + 1) }, correctAnswers: new[] { noteNames[col] }))));
    }

    private IEnumerable<object> ProcessSimonSmothers(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSmothersScript");

        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSmothers);

        var flashes = GetField<IList>(comp, "flashes").Get();
        var qs = new List<QandA>();
        for (int pos = 0, length = flashes.Count; pos < length; pos++)
        {
            string position = ordinal(pos + 1);
            qs.Add(makeQuestion(Question.SimonSmothersColors, _SimonSmothers, formatArgs: new[] { position }, correctAnswers: new[] { GetField<Enum>(flashes[pos], "color", isPublic: true).Get().ToString() }));
            qs.Add(makeQuestion(Question.SimonSmothersDirections, _SimonSmothers, formatArgs: new[] { position }, correctAnswers: new[] { GetField<Enum>(flashes[pos], "direction", isPublic: true).Get().ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonSounds(KMBombModule module)
    {
        var comp = GetComponent(module, "simonSoundsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSounds);

        var colorNames = new[] { "red", "blue", "yellow", "green" };
        var flashed = GetArrayField<List<int>>(comp, "stage").Get(ar => ar == null ? "contains null" : ar.Any(list => list.Last() < 0 || list.Last() >= colorNames.Length) ? "expected last value in range 0–3" : null);

        var qs = new List<QandA>();
        for (var stage = 0; stage < flashed.Length; stage++)
            qs.Add(makeQuestion(Question.SimonSoundsFlashingColors, _SimonSounds, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { colorNames[flashed[stage].Last()] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonSpeaks(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSpeaksModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonSpeaks);

        var sequence = GetArrayField<int>(comp, "_sequence").Get(expectedLength: 5);
        var colors = GetArrayField<int>(comp, "_colors").Get(expectedLength: 9);
        var words = GetArrayField<int>(comp, "_words").Get(expectedLength: 9);
        var shapes = GetArrayField<int>(comp, "_shapes").Get(expectedLength: 9);
        var languages = GetArrayField<int>(comp, "_languages").Get(expectedLength: 9);
        var wordsTable = GetStaticField<string[][]>(comp.GetType(), "_wordsTable").Get(ar => ar.Length != 9 ? "expected length 9" : null);
        var positionNames = GetStaticField<string[]>(comp.GetType(), "_positionNames").Get(ar => ar.Length != 9 ? "expected length 9" : null);
        var languageNames = new[] { "English", "Danish", "Dutch", "Esperanto", "Finnish", "French", "German", "Hungarian", "Italian" };

        addQuestions(module,
            makeQuestion(Question.SimonSpeaksPositions, _SimonSpeaks, correctAnswers: new[] { positionNames[sequence[0]] }),
            makeQuestion(Question.SimonSpeaksShapes, _SimonSpeaks, correctAnswers: new[] { SimonSpeaksSprites[shapes[sequence[1]]] }, preferredWrongAnswers: SimonSpeaksSprites),
            makeQuestion(Question.SimonSpeaksLanguages, _SimonSpeaks, correctAnswers: new[] { languageNames[languages[sequence[2]]] }),
            makeQuestion(Question.SimonSpeaksWords, _SimonSpeaks, correctAnswers: new[] { wordsTable[words[sequence[3]]][languages[sequence[3]]] }),
            makeQuestion(Question.SimonSpeaksColors, _SimonSpeaks, correctAnswers: new[] { wordsTable[colors[sequence[4]]][0] }));
    }

    private IEnumerable<object> ProcessSimonsStar(KMBombModule module)
    {
        var comp = GetComponent(module, "simonsStarScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var validColors = new[] { "red", "yellow", "green", "blue", "purple" };
        var flashes = "first,second,third,fourth,fifth".Split(',').Select(n => GetField<string>(comp, n + "FlashColour", isPublic: true).Get(c => !validColors.Contains(c) ? "invalid color" : null)).ToArray();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonsStar);

        addQuestions(module, flashes.Select((f, ix) => makeQuestion(Question.SimonsStarColors, _SimonsStar, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { f })));
    }

    private IEnumerable<object> ProcessSimonStacks(KMBombModule module)
    {
        var comp = GetComponent(module, "simonstacksScript");

        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonStacks);

        var colors = GetListField<string>(comp, "Colors").Get(minLength: 3, maxLength: 5);
        addQuestions(module, colors.Select((c, ix) => makeQuestion(Question.SimonStacksColors, _SimonStacks, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { c })));
    }

    private IEnumerable<object> ProcessSimonStages(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonStagesHandler");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var indicatorList = GetMethod<List<string>>(comp, "grabIndicatorColorsAll", numParameters: 0, isPublic: true);
        var flashList = GetMethod<List<string>>(comp, "grabSequenceColorsOneStage", numParameters: 1, isPublic: true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonStages);

        var indicators = indicatorList.Invoke();
        var stage1Flash = flashList.Invoke(1);
        var stage2Flash = flashList.Invoke(2);
        var stage3Flash = flashList.Invoke(3);
        var stage4Flash = flashList.Invoke(4);
        var stage5Flash = flashList.Invoke(5);

        addQuestions(module, indicators.Select((ans, i) => makeQuestion(Question.SimonStagesIndicator, _SimonStages, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { ans }))
            .Concat(stage1Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, _SimonStages, formatArgs: new[] { ordinal(i + 1), "first" }, correctAnswers: new[] { ans })))
            .Concat(stage2Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, _SimonStages, formatArgs: new[] { ordinal(i + 1), "second" }, correctAnswers: new[] { ans })))
            .Concat(stage3Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, _SimonStages, formatArgs: new[] { ordinal(i + 1), "third" }, correctAnswers: new[] { ans })))
            .Concat(stage4Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, _SimonStages, formatArgs: new[] { ordinal(i + 1), "4th" }, correctAnswers: new[] { ans })))
            .Concat(stage5Flash.Select((ans, i) => makeQuestion(Question.SimonStagesFlashes, _SimonStages, formatArgs: new[] { ordinal(i + 1), "5th" }, correctAnswers: new[] { ans }))));
    }

    private IEnumerable<object> ProcessSimonStates(KMBombModule module)
    {
        var comp = GetComponent(module, "AdvancedSimon");
        var fldPuzzleDisplay = GetArrayField<bool[]>(comp, "PuzzleDisplay");
        var fldProgress = GetIntField(comp, "Progress");

        bool[][] puzzleDisplay;
        while ((puzzleDisplay = fldPuzzleDisplay.Get(nullAllowed: true)) == null)
            yield return new WaitForSeconds(.1f);

        if (puzzleDisplay.Length != 4 || puzzleDisplay.Any(arr => arr.Length != 4))
            throw new AbandonModuleException($"‘PuzzleDisplay’ has an unexpected length or value: [{puzzleDisplay.Select(arr => arr == null ? "null" : "[" + arr.JoinString(", ") + "]").JoinString("; ")}]");

        var colorNames = new[] { "Red", "Yellow", "Green", "Blue" };

        while (fldProgress.Get() < 4)
            yield return new WaitForSeconds(.1f);
        // Consistency check
        if (fldPuzzleDisplay.Get(nullAllowed: true) != null)
            throw new AbandonModuleException($"‘PuzzleDisplay’ was expected to be null when Progress reached 4, but wasn’t.");

        _modulesSolved.IncSafe(_SimonStates);

        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)     // Do not ask about fourth stage because it can sometimes be solved without waiting for the flashes
        {
            var c = puzzleDisplay[i].Count(b => b);
            if (c != 3)
                qs.Add(makeQuestion(Question.SimonStatesDisplay, _SimonStates,
                    formatArgs: new[] { "color(s) flashed", ordinal(i + 1) },
                    correctAnswers: new[] { c == 4 ? "all 4" : puzzleDisplay[i].Select((v, j) => v ? colorNames[j] : null).Where(x => x != null).JoinString(", ") }));
            if (c != 1)
                qs.Add(makeQuestion(Question.SimonStatesDisplay, _SimonStates,
                    formatArgs: new[] { "color(s) didn’t flash", ordinal(i + 1) },
                    correctAnswers: new[] { c == 4 ? "none" : puzzleDisplay[i].Select((v, j) => v ? null : colorNames[j]).Where(x => x != null).JoinString(", ") }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonStops(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonStops");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SimonStops);

        var colors = GetArrayField<string>(comp, "outputSequence").Get(expectedLength: 5);
        addQuestions(module, colors.Select((color, ix) =>
             makeQuestion(Question.SimonStopsColors, _SimonStops, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { color }, preferredWrongAnswers: colors)));
    }

    private IEnumerable<object> ProcessSimonStores(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonStoresScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_SimonStores);

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
            qs.Add(makeQuestion(Question.SimonStoresColors, _SimonStores,
                formatArgs: new[] { flashSequences[i].Length == 1 ? "flashed" : "was among the colors flashed", ordinal(i + 1) },
                correctAnswers: flashSequences[i].Select(ch => colorNames[ch]).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonSubdivides(KMBombModule module)
    {
        var comp = GetComponent(module, "SSubScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_SimonSubdivides);
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
                qs.Add(makeQuestion(Question.SimonSubdividesButton, _SimonSubdivides, questionSprite: Grid.GenerateGridSprite(new Coord(2, 2, dirs[a].x, dirs[a].y)), correctAnswers: new[] { colors[arrange[0, a]] }, questionSpriteRotation: 45f));
                for (int b = 0; b < 4; b++)
                    if (split[a + 1][b])
                        qs.Add(makeQuestion(Question.SimonSubdividesButton, _SimonSubdivides, questionSprite: Grid.GenerateGridSprite(new Coord(4, 4, dirs[a].x * 2 + dirs[b].x, dirs[a].y * 2 + dirs[b].y)), correctAnswers: new[] { colors[arrange[a + 1, b]] }, questionSpriteRotation: 45f));
            }
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimonSupports(KMBombModule module)
    {
        var comp = GetComponent(module, "SimonSupportsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_SimonSupports);

        var combo = GetField<bool[][]>(comp, "combo").Get();
        var traits = GetArrayField<int>(comp, "tra").Get(expectedLength: 8);
        var traitNames = new[] { "Boss", "Cruel", "Faulty", "Lookalike", "Puzzle", "Simon", "Time-Based", "Translated" };
        var chosenTopics = Enumerable.Range(0, 3).Select(x => traitNames[traits[x]]).ToArray();

        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.SimonSupportsTopics, _SimonSupports, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { chosenTopics[i] }, preferredWrongAnswers: chosenTopics));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSimultaneousSimons(KMBombModule module)
    {
        var comp = GetComponent(module, "SimultaneousSimons");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_SimultaneousSimons);

        var sequences = GetField<int[,]>(comp, "sequences").Get();
        var btnColors = GetStaticField<int[]>(comp.GetType(), "buttonColors").Get();
        var colorNames = new[] { "Blue", "Yellow", "Red", "Green" };

        var qs = new List<QandA>();
        for (int simon = 0; simon < 4; simon++)
            for (int flash = 0; flash < 4; flash++)
                qs.Add(makeQuestion(Question.SimultaneousSimonsFlash, _SimultaneousSimons,
                    formatArgs: new[] { ordinal(simon + 1), ordinal(flash + 1) },
                    correctAnswers: new[] { colorNames[btnColors[sequences[simon, flash]]] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSkewedSlots(KMBombModule module)
    {
        var comp = GetComponent(module, "SkewedModule");
        var fldNumbers = GetArrayField<int>(comp, "Numbers");
        var fldModuleActivated = GetField<bool>(comp, "moduleActivated");
        var fldSolved = GetField<bool>(comp, "solved");

        var originalNumbers = new List<string>();

        while (true)
        {
            // Skewed Slots sets moduleActivated to false while the slots are spinning.
            // If there was a correct answer, it will set solved to true, otherwise it will set moduleActivated to true.
            while (!fldModuleActivated.Get() && !fldSolved.Get())
                yield return new WaitForSeconds(.1f);

            if (fldSolved.Get())
                break;

            // Get the current original digits.
            originalNumbers.Add(fldNumbers.Get(expectedLength: 3, validator: n => n < 0 || n > 9 ? "expected range 0–9" : null).JoinString());

            // When the user presses anything, Skewed Slots sets moduleActivated to false while the slots are spinning.
            while (fldModuleActivated.Get())
                yield return new WaitForSeconds(.1f);
        }

        _modulesSolved.IncSafe(_SkewedSlots);
        addQuestion(module, Question.SkewedSlotsOriginalNumbers, correctAnswers: new[] { originalNumbers.Last() },
            preferredWrongAnswers: originalNumbers.Take(originalNumbers.Count - 1).ToArray());
    }

    private IEnumerable<object> ProcessSkyrim(KMBombModule module)
    {
        var comp = GetComponent(module, "skyrimScript");
        var fldSolved = GetField<bool>(comp, "solved");

        while (!fldSolved.Get())
            // Usually we’d wait 0.1 seconds at a time, but in this case we need to know immediately so that we can hook the buttons
            yield return null;
        _modulesSolved.IncSafe(_Skyrim);

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
            qs.Add(makeQuestion(questions[i], _Skyrim, correctAnswers: list.Except(new[] { correct }).Select(t => t.name.Replace("'", "’")).ToArray()));
        }
        var shoutNames = GetListField<string>(comp, "shoutNameOptions").Get(expectedLength: 3);
        qs.Add(makeQuestion(Question.SkyrimDragonShout, _Skyrim, correctAnswers: shoutNames.Except(new[] { GetField<string>(comp, "shoutName").Get() }).Select(n => n.Replace("'", "’")).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSlowMath(KMBombModule module)
    {
        var comp = GetComponent(module, "SlowMathScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SlowMath);

        var ogLetters = GetListField<string>(comp, "_chosenLetters").Get(minLength: 3, maxLength: 5);
        addQuestion(module, Question.SlowMathLastLetters, correctAnswers: new[] { ogLetters.Last() });
    }

    private IEnumerable<object> ProcessSmallCircle(KMBombModule module)
    {
        var comp = GetComponent(module, "smallCircle");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SmallCircle);

        var shift = GetField<int>(comp, "shift").Get();
        var tableColor = GetField<int>(comp, "tableColor").Get();
        var solution = GetArrayField<int>(comp, "solution").Get();
        var colorNames = GetStaticField<string[]>(comp.GetType(), "colorNames").Get().Select(x => x[0].ToString().ToUpperInvariant() + x.Substring(1)).ToArray();
        var qs = new List<QandA>
        {
            makeQuestion(Question.SmallCircleShift, _SmallCircle, correctAnswers: new[] { shift.ToString() }),
            makeQuestion(Question.SmallCircleWedge, _SmallCircle, correctAnswers: new[] { colorNames[tableColor] })
        };
        for (int i = 0; i < 3; i++)
            qs.Add(makeQuestion(Question.SmallCircleSolution, _SmallCircle, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colorNames[solution[i]] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSnooker(KMBombModule module)
    {
        var comp = GetComponent(module, "snookerScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldActiveReds = GetIntField(comp, "activeReds");
        var activeReds = 0;

        var getNewValue = true;
        module.OnStrike += delegate { getNewValue = true; return true; };

        while (!fldSolved.Get())
        {
            if (getNewValue)
            {
                activeReds = fldActiveReds.Get(min: 8, max: 10);
                getNewValue = false;
            }
            yield return null;
        }
        _modulesSolved.IncSafe(_Snooker);
        yield return new WaitForSeconds(.1f);

        addQuestion(module, Question.SnookerReds, correctAnswers: new[] { activeReds.ToString() });
    }

    private IEnumerable<object> ProcessSnowflakes(KMBombModule module)
    {
        var comp = GetComponent(module, "snowflakes");

        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var gameOnPassDelegate = module.OnPass;
        module.OnPass = () => { return false; };

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Snowflakes);
        yield return new WaitForSeconds(5); // Wait for the snowflakes to disappear
        gameOnPassDelegate();

        var displays = GetArrayField<TextMesh>(comp, "displays", isPublic: true).Get(expectedLength: 4);
        var directions = new[] { "top", "right", "bottom", "left" };
        addQuestions(module, directions.Select((dir, ix) => makeQuestion(Question.SnowflakesDisplayedSnowflakes, _Snowflakes, formatArgs: new[] { dir }, correctAnswers: new[] { displays[ix].text })));
    }

    private IEnumerable<object> ProcessSonicKnuckles(KMBombModule module)
    {
        var comp = GetComponent(module, "sonicKnucklesScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        var heroArr = GetArrayField<object>(comp, "heroes", isPublic: true).Get();
        var badniksArr = GetArrayField<object>(comp, "badniks", isPublic: true).Get();
        var monitorArr = GetArrayField<object>(comp, "monitors", isPublic: true).Get();

        var fldAttachedSound = GetField<AudioClip>(heroArr[0], "attachedSound", isPublic: true);
        var fldContainsIllegalSound = GetField<bool>(heroArr[0], "containsIllegalSound", isPublic: true);
        var fldLabel = GetField<string>(heroArr[0], "label", isPublic: true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SonicKnuckles);

        var hero = heroArr[GetIntField(comp, "heroIndex").Get(0, heroArr.Length - 1)];
        var badnik = badniksArr[GetIntField(comp, "badnikIndex").Get(0, badniksArr.Length - 1)];
        var monitor = monitorArr[GetIntField(comp, "monitorIndex").Get(0, monitorArr.Length - 1)];

        string capitalizeWords(string input) => Regex.Replace(input, @"\b[a-z]", m => m.Value.ToUpperInvariant());

        var badnikName = fldLabel.GetFrom(badnik, v => !SonicKnucklesBadniksSprites.Any(s => s.name == v) ? "not a recognized badnik name" : null);
        var monitorName = fldLabel.GetFrom(monitor, v => !SonicKnucklesMonitorsSprites.Any(s => s.name == v) ? "not a recognized monitor name" : null);
        var illegalSoundName =
            fldContainsIllegalSound.GetFrom(hero) ? capitalizeWords(fldAttachedSound.GetFrom(hero).name) :
            fldContainsIllegalSound.GetFrom(monitor) ? capitalizeWords(fldAttachedSound.GetFrom(monitor).name) :
            fldContainsIllegalSound.GetFrom(badnik) ? capitalizeWords(fldAttachedSound.GetFrom(badnik).name) :
            throw new AbandonModuleException("None of the three items (hero, monitor, badnik) contain the illegal sound.");

        addQuestions(module,
            makeQuestion(Question.SonicKnucklesSounds, _SonicKnuckles, correctAnswers: new[] { illegalSoundName }),
            makeQuestion(Question.SonicKnucklesBadnik, _SonicKnuckles, correctAnswers: new[] { SonicKnucklesBadniksSprites.First(sprite => sprite.name == badnikName) }, preferredWrongAnswers: SonicKnucklesBadniksSprites),
            makeQuestion(Question.SonicKnucklesMonitor, _SonicKnuckles, correctAnswers: new[] { SonicKnucklesMonitorsSprites.First(sprite => sprite.name == monitorName) }, preferredWrongAnswers: SonicKnucklesMonitorsSprites));
    }

    private IEnumerable<object> ProcessSonicTheHedgehog(KMBombModule module)
    {
        var comp = GetComponent(module, "sonicScript");
        var fldsButtonSounds = new[] { "boots", "invincible", "life", "rings" }.Select(name => GetField<string>(comp, name + "Press"));
        var fldsPics = Enumerable.Range(0, 3).Select(i => GetField<Texture>(comp, "pic" + (i + 1))).ToArray();
        var fldStage = GetIntField(comp, "stage");

        while (fldStage.Get() < 5)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SonicTheHedgehog);

        var soundNameMapping =
            @"boss=Boss Theme;breathe=Breathe;continueSFX=Continue;drown=Drown;emerald=Emerald;extraLife=Extra Life;finalZone=Final Zone;invincibleSFX=Invincibility;jump=Jump;lamppost=Lamppost;marbleZone=Marble Zone;bumper=Bumper;skid=Skid;spikes=Spikes;spin=Spin;spring=Spring"
                .Split(';').Select(str => str.Split('=')).ToDictionary(ar => ar[0], ar => ar[1]);
        var pictureNameMapping =
            @"annoyedSonic=Annoyed Sonic=2;ballhog=Ballhog=1;blueLamppost=Blue Lamppost=3;burrobot=Burrobot=1;buzzBomber=Buzz Bomber=1;crabMeat=Crab Meat=1;deadSonic=Dead Sonic=2;drownedSonic=Drowned Sonic=2;fallingSonic=Falling Sonic=2;motoBug=Moto Bug=1;redLamppost=Red Lamppost=3;redSpring=Red Spring=3;standingSonic=Standing Sonic=2;switch=Switch=3;yellowSpring=Yellow Spring=3"
                .Split(';').Select(str => str.Split('=')).ToDictionary(ar => ar[0], ar => new { Name = ar[1], Stage = int.Parse(ar[2]) - 1 });

        var pics = fldsPics.Select(f => f.Get(p => p.name == null || !pictureNameMapping.ContainsKey(p.name) ? "unknown pic" : null)).ToArray();
        var sounds = fldsButtonSounds.Select(f => f.Get(s => !soundNameMapping.ContainsKey(s) ? "unknown sound" : null)).ToArray();

        addQuestions(module,
            Enumerable.Range(0, 3).Select(i =>
                makeQuestion(
                    Question.SonicTheHedgehogPictures,
                    _SonicTheHedgehog,
                    formatArgs: new[] { ordinal(i + 1) },
                    correctAnswers: new[] { pictureNameMapping[pics[i].name].Name },
                    preferredWrongAnswers: pictureNameMapping.Values.Where(inf => inf.Stage == i).Select(inf => inf.Name).ToArray()))
            .Concat(new[] { "Running Boots", "Invincibility", "Extra Life", "Rings" }.Select((screenName, i) =>
                makeQuestion(
                    Question.SonicTheHedgehogSounds,
                    _SonicTheHedgehog,
                    formatArgs: new[] { screenName },
                    correctAnswers: new[] { soundNameMapping[sounds[i]] },
                    preferredWrongAnswers: sounds.Select(s => soundNameMapping[s]).ToArray()))));
    }

    private IEnumerable<object> ProcessSorting(KMBombModule module)
    {
        var comp = GetComponent(module, "Sorting");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Sorting);

        var lastSwap = GetField<byte>(comp, "swapButtons").Get();
        if (lastSwap % 10 == 0 || lastSwap % 10 > 5 || lastSwap / 10 == 0 || lastSwap / 10 > 5 || lastSwap / 10 == lastSwap % 10)
            throw new AbandonModuleException($"‘swap’ has unexpected value (expected two digit number, each with a unique digit from 1-5): {lastSwap}");

        addQuestions(module, makeQuestion(Question.SortingLastSwap, _Sorting, correctAnswers: new[] { lastSwap.ToString().Insert(1, " & ") }));
    }

    private IEnumerable<object> ProcessSouvenir(KMBombModule module)
    {
        var comp = module.GetComponent<SouvenirModule>();
        if (comp == this)
        {
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        if (!_moduleCounts.TryGetValue(_Souvenir, out var souvenirCount) || souvenirCount != 2)
        {
            if (souvenirCount > 2)
                Debug.Log($"[Souvenir #{_moduleId}] There are more than two Souvenir modules on this bomb. Not asking any questions about them.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        // Prefer names of supported modules on the bomb other than Souvenir.
        IEnumerable<string> modules = _supportedModuleNames.Except(new[] { "Souvenir" }).Select(m => m.Replace("'", "’"));
        if (_supportedModuleNames.Count < 5)
        {
            // If there are less than 4 eligible modules, fill the remaining spaces with random other modules.
            var allModules = _attributes.Where(x => x.Value != null).Select(x => x.Value.ModuleNameWithThe).Distinct().ToList();
            modules = modules.Concat(Enumerable.Range(0, 1000).Select(i => allModules[Rnd.Range(0, allModules.Count)]).Except(_supportedModuleNames).Take(5 - _supportedModuleNames.Count));
        }
        while (comp._currentQuestion == null)
            yield return new WaitForSeconds(0.1f);

        var firstQuestion = comp._currentQuestion;
        var firstModule = firstQuestion.ModuleNameWithThe;

        // Wait for the user to solve that question before asking about it
        while (comp._currentQuestion == firstQuestion)
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_Souvenir);
        addQuestion(module, Question.SouvenirFirstQuestion, correctAnswers: new[] { firstModule }, preferredWrongAnswers: modules.ToArray());
    }

    private IEnumerable<object> ProcessSpaceTraders(KMBombModule module)
    {
        var comp = GetComponent(module, "SpaceTradersModule");
        var fldSolved = GetProperty<bool>(comp, "solved", true);
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SpaceTraders);

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Space Traders because the module was force-solved.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }
        if (GetProperty<int>(comp, "maxPossibleTaxAmount", true).Get() < 4)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Space Traders because all paths from the solar system are too short.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        addQuestions(module, makeQuestion(Question.SpaceTradersMaxTax, _SpaceTraders, correctAnswers: new[] { GetProperty<int>(comp, "maxTax", true).Get().ToString() + " GCr" }));
    }

    private IEnumerable<object> ProcessSpellingBee(KMBombModule module)
    {
        var comp = GetComponent(module, "spellingBeeScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var wordList = GetField<List<string>>(comp, "wordList", isPublic: true).Get();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_SpellingBee);
        var focus = GetField<int>(comp, "chosenWord").Get();
        addQuestions(module, makeQuestion(Question.SpellingBeeWord, _SpellingBee, formatArgs: null, correctAnswers: new[] { wordList[focus] }, preferredWrongAnswers: wordList.ToArray()));
    }

    private IEnumerable<object> ProcessSphere(KMBombModule module)
    {
        var comp = GetComponent(module, "theSphereScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        string[] colorNames = GetArrayField<string>(comp, "colourNames", isPublic: true).Get();
        int[] colors = GetArrayField<int>(comp, "selectedColourIndices", isPublic: true).Get(expectedLength: 5, validator: c => c < 0 || c >= colorNames.Length ? $"expected range 0–{colorNames.Length - 1}" : null);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Sphere);
        addQuestions(module,
            makeQuestion(Question.SphereColors, _Sphere, formatArgs: new[] { "first" }, correctAnswers: new[] { colorNames[colors[0]] }),
            makeQuestion(Question.SphereColors, _Sphere, formatArgs: new[] { "second" }, correctAnswers: new[] { colorNames[colors[1]] }),
            makeQuestion(Question.SphereColors, _Sphere, formatArgs: new[] { "third" }, correctAnswers: new[] { colorNames[colors[2]] }),
            makeQuestion(Question.SphereColors, _Sphere, formatArgs: new[] { "fourth" }, correctAnswers: new[] { colorNames[colors[3]] }),
            makeQuestion(Question.SphereColors, _Sphere, formatArgs: new[] { "fifth" }, correctAnswers: new[] { colorNames[colors[4]] }));
    }

    private IEnumerable<object> ProcessSplittingTheLoot(KMBombModule module)
    {
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var comp = GetComponent(module, "SplittingTheLootScript");
        var bags = (IList) GetField<object>(comp, "bags").Get(lst => !(lst is IList list) ? "expected an IList" : list.Count != 7 ? "expected length 7" : null);
        var fldBagColor = GetField<object>(bags[0], "Color");
        var fldBagLabel = GetField<string>(bags[0], "Label");
        var bagColors = bags.Cast<object>().Select(obj => fldBagColor.GetFrom(obj)).ToArray();
        var bagLabels = bags.Cast<object>().Select(obj => fldBagLabel.GetFrom(obj)).ToArray();
        var paintedBag = bagColors.IndexOf(bc => bc.ToString() != "Normal");
        if (paintedBag == -1)
            throw new AbandonModuleException($"No colored bag was found: [{bagColors.JoinString(", ")}]");

        var fldSolved = GetField<bool>(comp, "isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SplittingTheLoot);

        addQuestion(module, Question.SplittingTheLootColoredBag, correctAnswers: new[] { bagLabels[paintedBag] }, preferredWrongAnswers: bagLabels);
    }

    private IEnumerable<object> ProcessSpongebobBirthdayIdentification(KMBombModule module)
    {
        var comp = GetComponent(module, "SpongebobBirthdayIdentificationScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldStage = GetIntField(comp, "stage");
        var fldAnswer = GetField<string>(comp, "answer");

        var answers = new List<string>();
        var currentStage = fldStage.Get();
        while (!fldSolved.Get())
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
        _modulesSolved.IncSafe(_SpongebobBirthdayIdentification);

        answers.Add(fldAnswer.Get());

        var allNames = GetField<Texture[]>(comp, "allImages", isPublic: true).Get().Select(x => x.name).ToArray();
        addQuestions(module, answers.Select((ans, ix) => makeQuestion(
            question: Question.SpongebobBirthdayIdentificationChildren,
            moduleKey: _SpongebobBirthdayIdentification,
            formatArgs: new[] { ordinal(ix + 1) },
            correctAnswers: new[] { ans },
            preferredWrongAnswers: allNames)));
    }

    private IEnumerable<object> ProcessStability(KMBombModule module)
    {
        var colorNames = new[] { "Red", "Yellow", "Blue" };

        var comp = GetComponent(module, "StabilityScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Stability);

        var qs = new List<QandA>();

        var litLedStates = GetArrayField<int>(comp, "ledStates").Get().Where(l => l != 5).ToArray();
        for (int i = 0; i < litLedStates.Length; i++)
            qs.Add(makeQuestion(Question.StabilityLedColors, _Stability, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colorNames[litLedStates[i]] }));

        if (litLedStates.Length > 3)
            qs.Add(makeQuestion(Question.StabilityIdNumber, _Stability, correctAnswers: new[] { GetField<string>(comp, "idNumber").Get() }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessStackedSequences(KMBombModule module)
    {
        var comp = GetComponent(module, "stackedSequencesScript");
        var fldSolved = GetField<bool>(comp, "solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_StackedSequences);

        var sequences = GetArrayField<List<int>>(comp, "answer").Get();

        addQuestion(module, Question.StackedSequences, correctAnswers: sequences.Select(x => x.Count.ToString()).ToArray());
    }

    private IEnumerable<object> ProcessStars(KMBombModule module)
    {
        var comp = GetComponent(module, "Stars2Script");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");
        var originalNumber = GetField<TextMesh>(comp, "Number", isPublic: true).Get().text;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Stars);

        addQuestion(module, Question.StarsCenter, correctAnswers: new[] { originalNumber });
    }

    private IEnumerable<object> ProcessStateOfAggregation(KMBombModule module)
    {
        var comp = GetComponent(module, "StateOfAggregation");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var element = GetField<TextMesh>(comp, "Element", isPublic: true).Get().text;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_StateOfAggregation);

        // Convert to proper case.
        addQuestions(module, makeQuestion(Question.StateOfAggregationElement, _StateOfAggregation, formatArgs: null, correctAnswers: new[] { element.Substring(0, 1).ToUpperInvariant() + element.Substring(1).ToLowerInvariant() }));
    }

    private IEnumerable<object> ProcessStellar(KMBombModule module)
    {
        var comp = GetComponent(module, "StellarScript");
        var propSolved = GetProperty<bool>(comp, "IsSolved", isPublic: true);

        while (!propSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Stellar);

        var lastPlayed = GetField<string>(comp, "lastPlayed").Get(validator: str => str.Length != 3 ? "expected length 3" : str.Any(ch => ch < 'a' || ch > 'z') ? "expected letters a–z" : null);
        var allLetters = lastPlayed.Select(c => c.ToString()).ToArray();
        addQuestions(module,
            makeQuestion(Question.StellarLetters, _Stellar, formatArgs: new[] { "Braille" }, correctAnswers: new[] { lastPlayed[0].ToString() }, preferredWrongAnswers: allLetters),
            makeQuestion(Question.StellarLetters, _Stellar, formatArgs: new[] { "tap code" }, correctAnswers: new[] { lastPlayed[1].ToString() }, preferredWrongAnswers: allLetters),
            makeQuestion(Question.StellarLetters, _Stellar, formatArgs: new[] { "Morse code" }, correctAnswers: new[] { lastPlayed[2].ToString() }, preferredWrongAnswers: allLetters));
    }

    private IEnumerable<object> ProcessStupidSlots(KMBombModule module)
    {
        var comp = GetComponent(module, "StupidSlotsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_StupidSlots);

        var values = GetArrayField<int>(comp, "allValues").Get(expectedLength: 6);
        var validPositions = Enumerable.Range(0, 6).Where(x => values[x] != 0);
        var posNames = new[] { "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right" };

        var qs = new List<QandA>();
        foreach (var pos in validPositions)
            qs.Add(makeQuestion(Question.StupidSlotsValues, _StupidSlots, formatArgs: new[] { posNames[pos] }, correctAnswers: new[] { values[pos].ToString() }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSubscribeToPewdiepie(KMBombModule module)
    {
        var comp = GetComponent(module, "subscribeToPewdiepieScript");
        var fldSolved = GetField<bool>(comp, "solved");

        var pewdiepieNumber = GetField<int>(comp, "startingPewdiepie").Get();
        var tSeriesNumber = GetField<int>(comp, "startingTSeries").Get();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SubscribeToPewdiepie);

        addQuestions(module,
           makeQuestion(Question.SubscribeToPewdiepieSubCount, _SubscribeToPewdiepie, formatArgs: new[] { "PewDiePie" }, correctAnswers: new[] { pewdiepieNumber.ToString() }),
           makeQuestion(Question.SubscribeToPewdiepieSubCount, _SubscribeToPewdiepie, formatArgs: new[] { "T-Series" }, correctAnswers: new[] { tSeriesNumber.ToString() }));
    }

    private IEnumerable<object> ProcessSugarSkulls(KMBombModule module)
    {
        var comp = GetComponent(module, "sugarSkulls");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SugarSkulls);

        var skulls = new List<string>();
        var textInfo = GetArrayField<TextMesh>(comp, "texts", isPublic: true).Get();
        for (var x = 0; x < textInfo.Length; x++)
            skulls.Add(textInfo[x].text);

        addQuestions(module,
           makeQuestion(Question.SugarSkullsSkull, _SugarSkulls, formatArgs: new[] { "top" }, correctAnswers: new[] { skulls[0] }),
           makeQuestion(Question.SugarSkullsSkull, _SugarSkulls, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { skulls[1] }),
           makeQuestion(Question.SugarSkullsSkull, _SugarSkulls, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { skulls[2] }),
           makeQuestion(Question.SugarSkullsAvailability, _SugarSkulls, formatArgs: new[] { "was" }, correctAnswers: skulls.ToArray()),
           makeQuestion(Question.SugarSkullsAvailability, _SugarSkulls, formatArgs: new[] { "was not" }, correctAnswers: GetAnswers(Question.SugarSkullsAvailability).Except(skulls).ToArray()));
    }

    private IEnumerable<object> ProcessSuperparsing(KMBombModule module)
    {
        var comp = GetComponent(module, "SuperparsingScript");
        bool solved = false;
        module.OnPass += delegate () { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_Superparsing);

        string disp = GetField<string>(comp, "displayedWord").Get(str => str.Length != 4 ? "expected length 4" : null);
        addQuestion(module, Question.SuperparsingDisplayed, correctAnswers: new[] { disp });
    }

    private IEnumerable<object> ProcessSwitch(KMBombModule module)
    {
        var comp = GetComponent(module, "Switch");
        var fldSolved = GetField<bool>(comp, "SOLVED");
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
            else if (!fldSolved.Get())
            {
                topColor2 = fldTopColor.Get();
                bottomColor2 = fldBottomColor.Get();
            }
            return ret;
        };

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Switch);

        if (topColor1 < 1 || topColor1 > 6 || bottomColor1 < 1 || bottomColor1 > 6 || topColor2 < 1 || topColor2 > 6 || bottomColor2 < 1 || bottomColor2 > 6)
            throw new AbandonModuleException($"topColor1/bottomColor1/topColor2/bottomColor2 have unexpected values: {topColor1}, {bottomColor1}, {topColor2}, {bottomColor2} (expected 1–6).");

        addQuestions(module,
            makeQuestion(Question.SwitchInitialColor, _Switch, formatArgs: new[] { "top", "first" }, correctAnswers: new[] { colorNames[topColor1 - 1] }),
            makeQuestion(Question.SwitchInitialColor, _Switch, formatArgs: new[] { "bottom", "first" }, correctAnswers: new[] { colorNames[bottomColor1 - 1] }),
            makeQuestion(Question.SwitchInitialColor, _Switch, formatArgs: new[] { "top", "second" }, correctAnswers: new[] { colorNames[topColor2 - 1] }),
            makeQuestion(Question.SwitchInitialColor, _Switch, formatArgs: new[] { "bottom", "second" }, correctAnswers: new[] { colorNames[bottomColor2 - 1] }));
    }

    private IEnumerable<object> ProcessSwitches(KMBombModule module)
    {
        var comp = GetComponent(module, "SwitchModule");
        var fldGoal = GetField<object>(comp, "_goalConfiguration");
        var mthCurConfig = GetMethod<object>(comp, "GetCurrentConfiguration", 0);
        var switches = GetArrayField<MonoBehaviour>(comp, "Switches", isPublic: true).Get(expectedLength: 5);

        // The special font Souvenir uses to display switch states uses Q for up and R for down
        var initialState = switches.Select(sw => sw.GetComponent<Animator>().GetBool("Up") ? "Q" : "R").JoinString();

        while (!fldGoal.Get().Equals(mthCurConfig.Invoke()))
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Switches);

        addQuestion(module, Question.SwitchesInitialPosition, correctAnswers: new[] { initialState });
    }

    private IEnumerable<object> ProcessSwitchingMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "SwitchingMazeScript");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");
        var seedTextMesh = GetField<TextMesh>(comp, "Seedling", isPublic: true).Get();
        var fldNumberBasis = GetField<int>(comp, "NumberBasis");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var seed = seedTextMesh.text;
        var numberBasis = fldNumberBasis.Get();

        var hadStrike = false;
        module.OnStrike += delegate { hadStrike = true; return false; };

        while (!fldSolved.Get())
        {
            if (hadStrike)
            {
                seed = seedTextMesh.text;
                numberBasis = fldNumberBasis.Get();
                hadStrike = false;
            }
            yield return null;
        }

        _modulesSolved.IncSafe(_SwitchingMaze);

        var seedSplit = Regex.Replace(seed, " ", "").Split(':');
        var colorsOfTheMaze = GetArrayField<string>(comp, "ColorsOfMaze").Get();

        addQuestions(module,
            makeQuestion(Question.SwitchingMazeSeed, _SwitchingMaze, formatArgs: null, correctAnswers: new[] { seedSplit[1] }),
            makeQuestion(Question.SwitchingMazeColor, _SwitchingMaze, formatArgs: null, correctAnswers: new[] { colorsOfTheMaze[numberBasis] }, preferredWrongAnswers: colorsOfTheMaze));
    }

    private IEnumerable<object> ProcessSymbolCycle(KMBombModule module)
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

        _modulesSolved.IncSafe(_SymbolCycle);
        addQuestions(module, new[] { "left", "right" }.Select((screen, ix) => makeQuestion(Question.SymbolCycleSymbolCounts, _SymbolCycle, formatArgs: new[] { screen }, correctAnswers: new[] { cycles[ix].Length.ToString() })));
    }

    private IEnumerable<object> ProcessSymbolicCoordinates(KMBombModule module)
    {
        var comp = GetComponent(module, "symbolicCoordinatesScript");
        var letter1 = GetField<string>(comp, "letter1").Get();
        var letter2 = GetField<string>(comp, "letter2").Get();
        var letter3 = GetField<string>(comp, "letter3").Get();

        var stageLetters = new[] { letter1.Split(' '), letter2.Split(' '), letter3.Split(' ') };

        if (stageLetters.Any(x => x.Length != 3) || stageLetters.SelectMany(x => x).Any(y => !"ACELP".Contains(y)))
            throw new AbandonModuleException($"One of the stages has fewer than 3 symbols or symbols are of unexpected value (expected symbols “ACELP”, got “{stageLetters.Select(x => $"“{x.JoinString()}”").JoinString(", ")}”).");

        var fldStage = GetIntField(comp, "stage");
        while (fldStage.Get() < 4)
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_SymbolicCoordinates);
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
            _SymbolicCoordinates,
            formatArgs: new[] { position[pos], ordinal(stage + 1) },
            correctAnswers: new[] { SymbolicCoordinatesSprites["ACELP".IndexOf(letter, StringComparison.Ordinal)] },
            preferredWrongAnswers: SymbolicCoordinatesSprites))));
    }

    private IEnumerable<object> ProcessSymbolicTasha(KMBombModule module)
    {
        var comp = GetComponent(module, "symbolicTasha");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_SymbolicTasha);

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
            qs.Add(makeQuestion(Question.SymbolicTashaFlashes, _SymbolicTasha, formatArgs: new[] { ordinal(pos + 1) },
                correctAnswers: new[] { positionNames[flashing[pos]], colorNames[buttonColors[flashing[pos]]] }));

        for (var btn = 0; btn < 4; btn++)
            if (presentSymbols[btn] < 0)
            {
                qs.Add(makeQuestion(Question.SymbolicTashaSymbols, _SymbolicTasha, formatArgs: new[] { positionNamesLc[btn] }, correctAnswers: new[] { SymbolicTashaSprites[-presentSymbols[btn] - 1] }, preferredWrongAnswers: SymbolicTashaSprites));
                qs.Add(makeQuestion(Question.SymbolicTashaSymbols, _SymbolicTasha, formatArgs: new[] { colorNamesLc[buttonColors[btn]] }, correctAnswers: new[] { SymbolicTashaSprites[-presentSymbols[btn] - 1] }, preferredWrongAnswers: SymbolicTashaSprites));
            }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSync_125_3(KMBombModule module)
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

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Sync_125_3);

        var qs = new List<QandA>();
        for (var stage = 0; stage < 3; stage++)
            qs.Add(makeQuestion(Question.Sync125_3Word, _Sync_125_3, screenText.font, screenText.GetComponent<MeshRenderer>().sharedMaterial.mainTexture, formatArgs: new[] { (stage + 1).ToString() }, correctAnswers: new[] { words[textIds[stage]] }, preferredWrongAnswers: words));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessSynonyms(KMBombModule module)
    {
        var comp = GetComponent(module, "Synonyms");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        var numberText = GetField<TextMesh>(comp, "NumberText", isPublic: true).Get();

        if (numberText.text == null || !int.TryParse(numberText.text, out var number) || number < 0 || number > 9)
            throw new AbandonModuleException($"The display text (“{numberText.text ?? "<null>"}”) is not an integer 0–9.");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Synonyms);

        numberText.gameObject.SetActive(false);
        GetField<TextMesh>(comp, "BadLabel", isPublic: true).Get().text = "INPUT";
        GetField<TextMesh>(comp, "GoodLabel", isPublic: true).Get().text = "ACCEPTED";

        addQuestion(module, Question.SynonymsNumber, correctAnswers: new[] { number.ToString() });
    }

    private IEnumerable<object> ProcessSysadmin(KMBombModule module)
    {
        var comp = GetComponent(module, "SysadminModule");
        var fldSolved = GetProperty<bool>(comp, "solved", true);
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Sysadmin);

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Alfa-Bravo because the module was force-solved.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var fixedErrorCodes = GetProperty<HashSet<string>>(comp, "fixedErrorCodes", true).Get();
        if (fixedErrorCodes.Count == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Sysadmin because there are no errors to ask about.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }
        var allErrorCodes = GetStaticProperty<HashSet<string>>(comp.GetType(), "allErrorCodes", true).Get();
        addQuestion(module, Question.SysadminFixedErrorCodes, correctAnswers: fixedErrorCodes.ToArray(), preferredWrongAnswers: allErrorCodes.ToArray());
    }
}
