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
    private IEnumerator<YieldInstruction> ProcessPalindromes(ModuleData module)
    {
        var comp = GetComponent(module, "Palindromes");
        var fldX = GetField<string>(comp, "x");
        var fldY = GetField<string>(comp, "y");
        var fldZ = GetField<string>(comp, "z");
        var fldN = GetField<string>(comp, "n");

        yield return WaitForSolve;

        var vars = new[] { fldN, fldX, fldY, fldZ };
        var qs = new List<QandA>();
        for (var varIx = 0; varIx < vars.Length; varIx++)
            for (var digitIx = 0; digitIx < (varIx < 2 ? 5 : 4); digitIx++)       // 5 if x or n, else 4
            {
                var numString = vars[varIx].Get();
                var digit = numString[numString.Length - 1 - digitIx];
                if (digit is < '0' or > '9')
                    throw new AbandonModuleException($"The chosen character ('{digit}') was unexpected (expected a digit 0–9).");

                var labels = new[] { "the screen", "X", "Y", "Z" };
                qs.Add(makeQuestion(Question.PalindromesNumbers, module, formatArgs: new[] { labels[varIx], Ordinal(digitIx + 1) }, correctAnswers: new[] { digit.ToString() }));
            }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessPapasPizzeria(ModuleData module)
    {
        var comp = GetComponent(module, "papasPizzeriaScript");
        yield return WaitForSolve;
        var request = GetField<string>(comp, "request").Get(x => Regex.IsMatch(x, @"^[0-7]{3}[ACQBJMSD]$") ? null : "Unexpected order number.");
        addQuestions(module, Enumerable.Range(0, 4).Select(i =>
            makeQuestion(i == 3 ? Question.PapasPizzeriaLetter : Question.PapasPizzeriaDigit, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { request[i].ToString() })));
    }

    private IEnumerator<YieldInstruction> ProcessParity(ModuleData module)
    {
        var comp = GetComponent(module, "ParityScript");
        yield return WaitForSolve;

        var text = GetField<string>(comp, "_displayedText").Get();
        var pairs = new List<string>();
        for (var i = 0; i < 26; i++)
            for (var j = 0; j < 10; j++)
                pairs.Add("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[i].ToString() + j.ToString());
        addQuestion(module, Question.ParityDisplay, correctAnswers: new[] { text }, preferredWrongAnswers: pairs.ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessPartialDerivatives(ModuleData module)
    {
        var comp = GetComponent(module, "PartialDerivativesScript");
        var fldLeds = GetArrayField<int>(comp, "ledIndex");

        var display = GetField<TextMesh>(comp, "display", isPublic: true).Get();
        var terms = display.text.Split('\n').Select(term => Regex.Replace(Regex.Replace(term.Trim(), @"^(f.*?=|\+) ", ""), @"^- ", "−")).ToArray();
        if (terms.Length != 3)
            throw new AbandonModuleException($"The display does not appear to contain three terms: “{_moduleId}”");

        static string writeTerm(int coeff, bool negative, int[] exps)
        {
            if (coeff == 0)
                return "0";

            var term = negative ? "−" : "";
            if (coeff > 1)
                term += coeff;
            for (var j = 0; j < 3; j++)
            {
                if (exps[j] != 0)
                {
                    term += "xyz"[j];
                    if (exps[j] > 1)
                        term += "²³⁴⁵"[exps[j] - 2];
                }
            }
            return term;
        }

        var wrongAnswers = new HashSet<string>();
        while (wrongAnswers.Count < 3)
        {
            var exps = new int[3];
            for (var j = 0; j < 3; j++)
                exps[j] = Rnd.Range(0, 6);
            if (exps.All(e => e == 0))
                exps[Rnd.Range(0, 3)] = Rnd.Range(1, 6);
            var wrongTerm = writeTerm(Rnd.Range(1, 10), Rnd.Range(0, 2) != 0, exps);
            if (!terms.Contains(wrongTerm))
                wrongAnswers.Add(wrongTerm);
        }

        yield return WaitForSolve;

        var leds = fldLeds.Get(expectedLength: 3, validator: l => l is < 0 or > 5 ? "expected range 0–5" : null);
        var colorNames = new[] { "blue", "green", "orange", "purple", "red", "yellow" };
        var qs = new List<QandA>();
        for (var stage = 0; stage < 3; stage++)
            qs.Add(makeQuestion(Question.PartialDerivativesLedColors, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { colorNames[leds[stage]] }));
        for (var term = 0; term < 3; term++)
            qs.Add(makeQuestion(Question.PartialDerivativesTerms, module, formatArgs: new[] { Ordinal(term + 1) }, correctAnswers: new[] { terms[term] }, preferredWrongAnswers: wrongAnswers.ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessPassportControl(ModuleData module)
    {
        var comp = GetComponent(module, "passportControlScript");
        var fldPassages = GetIntField(comp, "passages");
        var fldExpiration = GetArrayField<int>(comp, "expiration");
        var stamps = GetArrayField<KMSelectable>(comp, "stamps", isPublic: true).Get();
        var textToHide1 = GetArrayField<GameObject>(comp, "passport", isPublic: true).Get(validator: objs => objs.Any(go => go.GetComponent<TextMesh>() == null) ? "doesn’t have a TextMesh component" : null);
        var textToHide2 = GetField<GameObject>(comp, "ticket", isPublic: true).Get(go => go.GetComponent<TextMesh>() == null ? "doesn’t have a TextMesh component" : null);

        var expirationDates = new List<int>();

        for (var i = 0; i < stamps.Length; i++)
        {
            var oldHandler = stamps[i].OnInteract;
            stamps[i].OnInteract = delegate
            {
                // Only add the expiration date if there is no error. The error is caught later when the length of ‘expirationDates’ is checked.
                // Avoid throwing exceptions inside of the button handler
                var date = fldExpiration.Get(nullAllowed: true);
                if (date == null || date.Length != 3)
                    return oldHandler();

                var year = date[2];
                var passages = fldPassages.Get();
                var ret = oldHandler();
                if (fldPassages.Get() == passages) // player got strike, ignoring retrieved info
                    return ret;

                expirationDates.Add(year);
                return ret;
            };
        }

        yield return WaitForSolve;

        if (expirationDates.Count != 3)
            throw new AbandonModuleException($"The number of retrieved sets of information was {expirationDates.Count} (expected 3).");

        for (var i = 0; i < textToHide1.Length; i++)
            textToHide1[i].GetComponent<TextMesh>().text = "";
        textToHide2.GetComponent<TextMesh>().text = "";

        var altDates = new List<string[]>();

        for (var i = 0; i < expirationDates.Count; i++)
        {
            altDates.Add(new string[6]);
            var startVal = expirationDates[i] - Rnd.Range(0, 6);
            for (var j = 0; j < altDates[i].Length; j++)
                altDates[i][j] = (startVal + j).ToString();
        }

        addQuestions(module,
            makeQuestion(Question.PassportControlPassenger, module, formatArgs: new[] { "first" }, correctAnswers: new[] { expirationDates[0].ToString() }, preferredWrongAnswers: altDates[0]),
            makeQuestion(Question.PassportControlPassenger, module, formatArgs: new[] { "second" }, correctAnswers: new[] { expirationDates[1].ToString() }, preferredWrongAnswers: altDates[1]),
            makeQuestion(Question.PassportControlPassenger, module, formatArgs: new[] { "third" }, correctAnswers: new[] { expirationDates[2].ToString() }, preferredWrongAnswers: altDates[2]));
    }

    private IEnumerator<YieldInstruction> ProcessPasswordDestroyer(ModuleData module)
    {
        var comp = GetComponent(module, "passwordDestroyer");

        var fldTwoFactorV2 = GetIntField(comp, "identityDigit");        // 2FAST value

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.PasswordDestroyerTwoFactorV2, module, correctAnswers: new[] { fldTwoFactorV2.Get(100100, 999999).ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessPatternCube(ModuleData module)
    {
        var comp = GetComponent(module, "PatternCubeModule");
        var selectableSymbolObjects = GetArrayField<MeshRenderer>(comp, "_selectableSymbolObjs").Get(expectedLength: 5);
        var placeableSymbolObjects = GetArrayField<MeshRenderer>(comp, "_placeableSymbolObjs").Get(expectedLength: 6);
        var highlightPos = GetIntField(comp, "_highlightedPosition").Get(min: 0, max: 4);

        yield return WaitForSolve;

        var symbols = selectableSymbolObjects.Concat(placeableSymbolObjects.Where(r => r.gameObject.activeSelf))
            .Select(r => PatternCubeSprites[int.Parse(r.sharedMaterial.mainTexture.name.Substring(6))]).ToArray();
        addQuestion(module, Question.PatternCubeHighlightedSymbol, correctAnswers: new[] { symbols[highlightPos] }, preferredWrongAnswers: symbols);
    }

    private readonly List<string> _pentabuttonLabels = new();
    private IEnumerator<YieldInstruction> ProcessPentabutton(ModuleData module)
    {
        var comp = GetComponent(module, "PentabuttonScript");

        var label = GetField<TextMesh>(comp, "Label", isPublic: true).Get().text;
        _pentabuttonLabels.Add(label);

        yield return WaitForSolve;

        string format = null;
        if (_pentabuttonLabels.Count(x => x == label) == 1)
            format = string.Format(translateString(Question.PentabuttonBaseColor, "the Pentabutton labelled “{0}”"), label.ToUpperInvariant());

        var colors = new string[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White" };
        var ans = GetField<int>(comp, "RndColour").Get(i => i is < 0 or > 6 ? $"Unknown color index {i}" : null);
        addQuestion(module, Question.PentabuttonBaseColor, formattedModuleName: format, correctAnswers: new[] { colors[ans] });
    }

    private IEnumerator<YieldInstruction> ProcessPeriodicWords(ModuleData module)
    {
        var comp = GetComponent(module, "PeriodicWordsScript");

        yield return WaitForSolve;

        var words = GetArrayField<string>(comp, "Words").Get().Take(4).ToArray();
        addQuestions(module, Enumerable.Range(0, 3).Select(stage => makeQuestion(Question.PeriodicWordsDisplayedWords, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { words[stage] }, preferredWrongAnswers: words)));
    }

    private IEnumerator<YieldInstruction> ProcessPerspectivePegs(ModuleData module)
    {
        var comp = GetComponent(module, "PerspectivePegsModule");
        yield return WaitForSolve;

        var serialNumber = Bomb.GetSerialNumber();

        var keyNumber = 0;
        var prevChar = '\0';
        foreach (var letter in serialNumber)
        {
            if (!char.IsLetter(letter))
                continue;
            if (prevChar == 0)
                prevChar = letter;
            else
            {
                keyNumber += Math.Abs(letter - prevChar);
                prevChar = '\0';
            }
        }
        var colorNames = new[] { "red", "yellow", "green", "blue", "purple" };
        var keyColour = (keyNumber % 10) switch
        {
            0 or 3 => "ColourRed",
            4 or 9 => "ColourYellow",
            1 or 7 => "ColourGreen",
            5 or 8 => "ColourBlue",
            2 or 6 => "ColourPurple",
            _ => throw new AbandonModuleException("Invalid keyNumber % 10."),
        };
        var colourMeshes = GetField<MeshRenderer[,]>(comp, "ColourMeshes").Get();
        var pegIndex = Enumerable.Range(0, 5).IndexOf(px => Enumerable.Range(0, 5).Count(i => colourMeshes[px, i].sharedMaterial.name.StartsWith(keyColour)) >= 3);
        if (pegIndex == -1)
            throw new AbandonModuleException($"The key peg couldn't be found (the key colour was {keyColour}).");

        addQuestions(module, Enumerable.Range(0, 5)
            .Select(i => (pegIndex + i) % 5)
            .Select(n => colorNames.First(cn => colourMeshes[n, n].sharedMaterial.name.Substring(6).StartsWith(cn, StringComparison.InvariantCultureIgnoreCase)))
            .Select((col, ix) => makeQuestion(Question.PerspectivePegsColorSequence, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { col })));
    }

    private IEnumerator<YieldInstruction> ProcessPhosphorescence(ModuleData module)
    {
        var comp = GetComponent(module, "PhosphorescenceScript");
        var init = GetField<object>(comp, "init").Get();
        yield return WaitForSolve;

        var index = GetIntField(init, "index").Get(min: 0, max: 419);
        var buttonPresses = GetField<Array>(init, "buttonPresses").Get(ar =>
            ar.Length is < 3 or > 6 ? "expected length 3–6" :
            ar.OfType<object>().Any(v => !Question.PhosphorescenceButtonPresses.GetAnswers().Contains(v.ToString())) ? "contains unknown color" : null);

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.PhosphorescenceOffset, module, correctAnswers: new[] { index.ToString() }));

        for (var i = 0; i < buttonPresses.GetLength(0); i++)
            qs.Add(makeQuestion(Question.PhosphorescenceButtonPresses, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { buttonPresses.GetValue(i).ToString() }));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessPickupIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "PickupIdentificationScript");

        yield return WaitForSolve;

        var allSprites = GetArrayField<Sprite>(comp, "SeedPacketIdentifier", isPublic: true).Get(expectedLength: 719).TranslateSprites(166).ToArray();
        var chosen = GetArrayField<int>(comp, "Unique").Get(expectedLength: 3, validator: v => v is < 0 or >= 719 ? "Expected pickup number 0–718" : null);

        addQuestions(module, chosen.Select((sprite, stage) => makeQuestion(Question.PickupIdentificationItem, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { allSprites[sprite] }, allAnswers: allSprites)));
    }

    private IEnumerator<YieldInstruction> ProcessPictionary(ModuleData module)
    {
        var comp = GetComponent(module, "pictionaryModuleScript");

        yield return WaitForSolve;

        var code = GetField<string>(comp, "code").Get(c => c.Length != 4 || c.Any(ch => !char.IsDigit(ch)) ? "expected a sequence of four digits" : null);
        addQuestion(module, Question.PictionaryCode, correctAnswers: new[] { code });
    }

    private IEnumerator<YieldInstruction> ProcessPie(ModuleData module)
    {
        var comp = GetComponent(module, "PieScript");
        var digits = GetArrayField<string>(comp, "codes").Get(expectedLength: 5);

        yield return WaitForSolve;

        addQuestions(module, digits.Select((digit, ix) => makeQuestion(Question.PieDigits, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { digit }, preferredWrongAnswers: digits)));
    }

    private IEnumerator<YieldInstruction> ProcessPieFlash(ModuleData module)
    {
        var comp = GetComponent(module, "pieFlashScript");
        var digits = GetArrayField<string>(comp, "codes").Get(expectedLength: 3);

        yield return WaitForSolve;

        // Find valid answers within pi that do not overlap with any of the other strings
        var piString = GetField<string>(comp, "pi").Get();
        var validAnswers = Enumerable.Range(0, piString.Length - 5).Select(ix => piString.Substring(ix, 5)).Where(sps => !digits.Contains(sps)).ToArray();
        addQuestion(module, Question.PieFlashDigits, correctAnswers: validAnswers, preferredWrongAnswers: digits);
    }

    private IEnumerator<YieldInstruction> ProcessPigpenCycle(ModuleData module) => processSpeakingEvilCycle(module, "PigpenCycleScript", Question.PigpenCycleDialDirections, Question.PigpenCycleDialLabels);

    private IEnumerator<YieldInstruction> ProcessPlaceholderTalk(ModuleData module)
    {
        var comp = GetComponent(module, "placeholderTalk");
        yield return WaitForSolve;

        var answer = GetField<byte>(comp, "answerId").Get(b => b is < 0 or > 16 ? "expected range 0–16" : null) + 1;
        var firstPhrase = GetArrayField<string>(comp, "firstPhrase").Get();
        var firstString = GetField<string>(comp, "firstString").Get(str => !firstPhrase.Contains(str) ? $"expected string to be contained in “{firstPhrase}” (‘firstPhrase’)" : null);
        var ordinals = GetArrayField<string>(comp, "ordinals").Get();
        var currentOrdinal = GetField<string>(comp, "currentOrdinal").Get(str => !ordinals.Contains(str) ? $"expected string to be contained in “{ordinals}” (‘ordinals’)" : null);

        var qs = new List<QandA>();

        qs.Add(makeQuestion(Question.PlaceholderTalkFirstPhrase, module, correctAnswers: new[] { firstString }, preferredWrongAnswers: firstPhrase));
        qs.Add(makeQuestion(Question.PlaceholderTalkOrdinal, module, correctAnswers: new[] { currentOrdinal }, preferredWrongAnswers: ordinals));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessPinkButton(ModuleData module)
    {
        var comp = GetComponent(module, "PinkButtonScript");
        var words = GetArrayField<int>(comp, "_words").Get(expectedLength: 4, validator: v => v is < 0 or > 7 ? "expected range 0–7" : null);
        var colors = GetArrayField<int>(comp, "_colors").Get(expectedLength: 4, validator: v => v is < 0 or > 7 ? "expected range 0–7" : null);

        var abbreviatedColorNames = GetStaticField<string[]>(comp.GetType(), "_abbreviatedColorNames").Get(v => v.Length != 8 ? "expected length 8" : null);
        var colorNames = GetStaticField<string[]>(comp.GetType(), "_colorNames").Get(v => v.Length != 8 ? "expected length 8" : null);

        yield return WaitForSolve;

        addQuestions(module,
            Enumerable.Range(0, 4).SelectMany(ix => Ut.NewArray(
                 makeQuestion(Question.PinkButtonWords, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { abbreviatedColorNames[words[ix]] }),
                 makeQuestion(Question.PinkButtonColors, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { colorNames[colors[ix]] }))));
    }

    private IEnumerator<YieldInstruction> ProcessPinpoint(ModuleData module)
    {
        var comp = GetComponent(module, "pinpointScript");
        yield return WaitForSolve;

        var dists = GetArrayField<float>(comp, "dists").Get(expectedLength: 3);
        var points = GetArrayField<int>(comp, "points").Get(expectedLength: 4); // includes target point, which we ignore
        addQuestions(module,
            makeQuestion(Question.PinpointPoints, module, correctAnswers: points.Take(3).Select(i => $"{(char) ('A' + i % 10)}{i / 10 + 1}").ToArray()),
            makeQuestion(Question.PinpointDistances, module, correctAnswers: dists.Select(dist => dist.ToString("0.000")).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessPixelCipher(ModuleData module)
    {
        var comp = GetComponent(module, "pixelcipherScript");
        yield return WaitForSolve;

        var keywords = GetArrayField<string>(comp, "pixelKeyword").Get();
        var pickedKeyword = GetIntField(comp, "pickedKeyword").Get(0, keywords.Length - 1);

        addQuestion(module, Question.PixelCipherKeyword, correctAnswers: new[] { keywords[pickedKeyword] });
    }

    private IEnumerator<YieldInstruction> ProcessPlacementRoulette(ModuleData module)
    {
        var comp = GetComponent(module, "PlacementRouletteModule");
        yield return WaitForSolve;

        var character = GetField<string>(comp, "Character").Get();
        var vehicle = GetField<string>(comp, "Vehicle").Get();
        var track = GetField<string>(comp, "Track").Get();

        addQuestions(module,
            makeQuestion(Question.PlacementRouletteChar, module, correctAnswers: new[] { character }),
            makeQuestion(Question.PlacementRouletteTrack, module, correctAnswers: new[] { track }),
            makeQuestion(Question.PlacementRouletteVehicle, module, correctAnswers: new[] { vehicle })
        );
    }

    private IEnumerator<YieldInstruction> ProcessPlanets(ModuleData module)
    {
        var comp = GetComponent(module, "planetsModScript");
        var planetShown = GetIntField(comp, "planetShown").Get(0, 9);
        var stripColors = GetArrayField<int>(comp, "stripColours").Get(expectedLength: 5, validator: x => x is < 0 or > 8 ? "expected range 0–8" : null);

        yield return WaitForSolve;

        var stripNames = new[] { "Aqua", "Blue", "Green", "Lime", "Orange", "Red", "Yellow", "White", "Off" };
        addQuestions(module,
            stripColors.Select((strip, count) => makeQuestion(Question.PlanetsStrips, module, formatArgs: new[] { Ordinal(count + 1) }, correctAnswers: new[] { stripNames[strip] }))
                .Concat(new[] { makeQuestion(Question.PlanetsPlanet, module, correctAnswers: new[] { PlanetsSprites[planetShown] }, preferredWrongAnswers: (DateTime.Now.Month == 4 && DateTime.Now.Day == 1) ? PlanetsSprites : PlanetsSprites.Take(PlanetsSprites.Length - 2).ToArray()) }));
    }

    private IEnumerator<YieldInstruction> ProcessPlayfairCycle(ModuleData module) => processSpeakingEvilCycle(module, "PlayfairCycleScript", Question.PlayfairCycleDialDirections, Question.PlayfairCycleDialLabels);

    private IEnumerator<YieldInstruction> ProcessPoetry(ModuleData module)
    {
        var comp = GetComponent(module, "PoetryModule");
        var fldStage = GetIntField(comp, "currentStage");
        var fldStageCount = GetIntField(comp, "stageCount", isPublic: true);

        var answers = new List<string>();
        var selectables = GetArrayField<KMSelectable>(comp, "wordSelectables", isPublic: true).Get(expectedLength: 6);
        var wordTextMeshes = GetArrayField<TextMesh>(comp, "words", isPublic: true).Get(expectedLength: 6);

        for (var i = 0; i < 6; i++)
        {
            var j = i;
            var oldHandler = selectables[i].OnInteract;
            selectables[i].OnInteract = delegate
            {
                var prevStage = fldStage.Get();
                var word = wordTextMeshes[j].text;
                var ret = oldHandler();

                if (fldStage.Get() > prevStage)
                    answers.Add(word);

                return ret;
            };
        }

        yield return WaitForSolve;

        if (answers.Count != fldStageCount.Get())
            throw new AbandonModuleException($"The number of answers captured is not equal to the number of stages played ({fldStageCount.Get()}). Answers were: [{answers.JoinString(", ")}]");

        addQuestions(module, answers.Select((ans, st) => makeQuestion(Question.PoetryAnswers, module, formatArgs: new[] { Ordinal(st + 1) }, correctAnswers: new[] { ans }, preferredWrongAnswers: answers.ToArray())));
    }

    private IEnumerator<YieldInstruction> ProcessPointlessMachines(ModuleData module)
    {
        var comp = GetComponent(module, "PointlessMachinesScript");
        yield return WaitForSolve;

        var flashes = GetField<Array>(comp, "_souvenirFlashes")
            .Get(v =>
                v.Length != 6 ? "Expected array length 6" :
                v.Cast<int>().Any(i => i is < 0 or >= 5) ? "Expected color 0–4" : null)
            .Cast<object>()
            .Select(v => v.ToString())
            .ToArray();

        // All 5 colors always appear (with one duplicate), so no need to add preferredWrongAnswers
        addQuestions(module, flashes.Select((f, i) =>
            makeQuestion(Question.PointlessMachinesFlashes, module, formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { f })));
    }

    private IEnumerator<YieldInstruction> ProcessPolygons(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "polygons");

        var incompatibleGroups = Ut.NewArray(
            new[] { 26, 30 }, // short trapezoid, tall trapezoid
            new[] { 16, 29 }, // long rectangle, tall rectangle
            new[] { 3, 12, 21, 31 }, // arrows (dlru)
            new[] { 4, 13, 22, 32 }, // arrowheads (dlru)
            new[] { 5, 14, 23, 33 }, // crosses (dlru)
            new[] { 6, 15, 24, 34 } // kites (dlru)
        );

        // [0, 34] except incompatible
        var compatibleShapes = new[] { 0, 1, 2, 7, 8, 9, 10, 11, 17, 18, 19, 20, 25, 27, 28 };

        var correctShape = GetIntField(comp, "trueshape").Get(0, 34);
        var incorrectShapes = GetArrayField<int>(comp, "falseshapes").Get(3, 3, validator: v => v is < 0 or > 34 ? "Out of range 0-34" : null);
        var allShapes = new[] { correctShape, incorrectShapes[0], incorrectShapes[1], incorrectShapes[2] };

        // We remove questionably ambiguous answers, but add one back chosen randomly from each group.
        // If a group has at least one correct answer, don't add another, but multiple correct answers can't show up anyways.
        var use = incompatibleGroups
            .Where(g => !g.Any(v => allShapes.Contains(v)))
            .Select(a => a.PickRandom())
            .ToArray();

        var valid = compatibleShapes
            .Concat(use)
            .Concat(allShapes)
            .Select(v => PolygonsSprites[v])
            .ToArray();
        var correct = allShapes.Select(v => PolygonsSprites[v]).ToArray();

        addQuestion(module, Question.PolygonsPolygon, correctAnswers: correct, allAnswers: valid);
    }

    private readonly List<string> _polyhedralMazeTypes = new();
    private IEnumerator<YieldInstruction> ProcessPolyhedralMaze(ModuleData module)
    {
        Dictionary<string, string> nameMapping = new()
        {
            ["4TruncatedDeltoidalIcositetrahedron2"] = "the 4-truncated deltoidal icositetrahedral Polyhedral Maze",
            ["ChamferedDodecahedron1"] = "the chamfered dodecahedral Polyhedral Maze",
            ["ChamferedIcosahedron2"] = "the chamfered icosahedral Polyhedral Maze",
            ["DeltoidalHexecontahedron"] = "the deltoidal hexecontahedral Polyhedral Maze",
            ["DisdyakisDodecahedron"] = "the disdyakis dodecahedral Polyhedral Maze",
            ["JoinedLsnubCube"] = "the joined snub cubic Polyhedral Maze",
            ["JoinedRhombicuboctahedron"] = "the joined rhombicuboctahedral Polyhedral Maze",
            ["LpentagonalHexecontahedron"] = "the pentagonal hexecontahedral Polyhedral Maze",
            ["OrthokisPropelloCube"] = "the orthokis propello cubic Polyhedral Maze",
            ["PentakisDodecahedron"] = "the pentakis dodecahedral Polyhedral Maze",
            ["RectifiedRhombicuboctahedron"] = "the rectified rhombicuboctahedral Polyhedral Maze",
            ["TriakisIcosahedron"] = "the triakis icosahedral Polyhedral Maze",
            ["Rhombicosidodecahedron"] = "the rhombicosidodecahedral Polyhedral Maze",
            ["CanonicalRectifiedLsnubCube"] = "the canonical rectified snub cubic Polyhedral Maze",
        };

        var comp = GetComponent(module, "PolyhedralMazeModule");
        var polyhedron = GetField<object>(comp, "_polyhedron").Get();
        var internalName = GetField<string>(polyhedron, "Name", isPublic: true).Get(s => !nameMapping.ContainsKey(s) ? "Unexpected polyhedron name" : null);
        var souvenirName = nameMapping[internalName];
        _polyhedralMazeTypes.Add(souvenirName);

        yield return WaitForSolve;

        string format = null;
        if (_polyhedralMazeTypes.Count(n => n == souvenirName) == 1 && Rnd.Range(0, 2) != 0)
            format = translateString(Question.PolyhedralMazeStartPosition, souvenirName);

        addQuestion(module, Question.PolyhedralMazeStartPosition, formattedModuleName: format, correctAnswers: new[] { GetIntField(comp, "_startFace").Get().ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessPrimeEncryption(ModuleData module)
    {
        var comp = GetComponent(module, "PrimeEncryptionScript");
        yield return WaitForSolve;

        var displayedValue = GetField<int>(comp, "encryption").Get();
        var allPrimeNumbersUsed = GetArrayField<int>(comp, "primeNumbers").Get();

        // Generate wrong answers based on a combination of prime numbers determined from the module.
        var incorrectValues = new List<int>();
        while (incorrectValues.Count < 5)
        {
            var onePrime = allPrimeNumbersUsed.PickRandom();
            var anotherPrime = allPrimeNumbersUsed.PickRandom();
            while (anotherPrime == onePrime)
                anotherPrime = allPrimeNumbersUsed.PickRandom();

            var productPrimes = onePrime * anotherPrime;
            if (productPrimes != displayedValue && !incorrectValues.Contains(productPrimes))
                incorrectValues.Add(productPrimes);
        }

        addQuestion(module, Question.PrimeEncryptionDisplayedValue,
            correctAnswers: new[] { displayedValue.ToString() },
            preferredWrongAnswers: incorrectValues.Select(val => val.ToString()).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessPrisonBreak(ModuleData module)
    {
        var comp = GetComponent(module, "prisonBreakScript");
        var startPos = GetIntField(comp, "currentPos").Get(v => v is < 0 or > 598 ? "Out of range [0, 598]" : (v % 25) % 2 != 1 ? "Invalid X position" : (v / 25) % 2 != 1 ? "Invalid Y position" : null);
        string error = null;

        IEnumerator waitForReset()
        {
            if (error is not null) yield break;
            var timer = GetField<TextMesh>(comp, "timer", true).Get();
            yield return new WaitUntil(() => timer.text == "-:--");
            startPos = GetIntField(comp, "currentPos").Get();
            error = startPos is < 0 or > 598 ? "Out of range [0, 598]"
                    : (startPos % 25) % 2 != 1 ? "Invalid X position"
                    : (startPos / 25) % 2 != 1 ? "Invalid Y position"
                    : null;
        }
        module.Module.OnStrike += () =>
        {
            StartCoroutine(waitForReset());
            return false;
        };

        yield return WaitForSolve;

        if (error != null)
            throw new AbandonModuleException($"currentPos ({startPos}) invalid: {error}");

        var cell = GetIntField(comp, "cell").Get(min: 0, max: 14);

        addQuestions(module,
            makeQuestion(Question.PrisonBreakPrisoner, module, correctAnswers: new[] { (cell + 1).ToString() }),
            makeQuestion(Question.PrisonBreakDefuser, module, correctAnswers: new[] { $"{(char) ('A' + (startPos % 25 - 1) / 2)}{(startPos / 25 + 1) / 2}" }));
    }

    private IEnumerator<YieldInstruction> ProcessProbing(ModuleData module)
    {
        var comp = GetComponent(module, "ProbingModule");
        yield return WaitForSolve;

        var display = GetField<TextMesh>(comp, "display", isPublic: true).Get();

        // Blank out the display so that the user cannot see the readout for the solution wires
        display.text = "";

        // Prevent the user from interacting with the wires after solving
        foreach (var selectable in GetArrayField<KMSelectable>(comp, "selectables", isPublic: true).Get(expectedLength: 6))
            selectable.OnInteract = delegate { return false; };

        var wireNames = new[] { "red-white", "yellow-black", "green", "gray", "yellow-red", "red-blue" };
        var frequencyDic = new Dictionary<int, string> { { 7, "60Hz" }, { 11, "50Hz" }, { 13, "22Hz" }, { 14, "10Hz" } };
        var wireFrequenciesRaw = GetField<Array>(comp, "mWires").Get(ar => ar.Length != 6 ? "expected length 6" : ar.Cast<int>().Any(v => !frequencyDic.ContainsKey(v)) ? "contains unknown frequency value" : null);
        var wireFrequencies = wireFrequenciesRaw.Cast<int>().Select(val => frequencyDic[val]).ToArray();

        addQuestions(module, wireFrequencies.Select((wf, ix) => makeQuestion(Question.ProbingFrequencies, module, formatArgs: new[] { wireNames[ix] }, correctAnswers: new[] { wf })));
    }

    private IEnumerator<YieldInstruction> ProcessProceduralMaze(ModuleData module)
    {
        var comp = GetComponent(module, "ProceduralMazeModule");
        yield return WaitForSolve;

        var initialSeed = GetField<string>(comp, "_initialSeed").Get();

        StartCoroutine(GetMethod<IEnumerator>(GetField<object>(comp, "_mazeRenderer").Get(), "HideRings", 0, true).Invoke());
        addQuestion(module, Question.ProceduralMazeInitialSeed, correctAnswers: new[] { initialSeed });
    }

    private IEnumerator<YieldInstruction> ProcessPunctuationMarks(ModuleData module)
    {
        var comp = GetComponent(module, "script");

        yield return WaitForSolve;

        var number = GetIntField(comp, "memoryBankNumber").Get(min: 0, max: 99).ToString("00");
        addQuestion(module, Question.PunctuationMarksDisplayedNumber, correctAnswers: new[] { number });
    }

    private IEnumerator<YieldInstruction> ProcessPurpleArrows(ModuleData module)
    {
        var comp = GetComponent(module, "PurpleArrowsScript");

        yield return WaitForSolve;

        var finishWord = GetField<string>(comp, "finish").Get(str => str.Length != 6 ? "expected length 6" : null);
        var wordList = GetArrayField<string>(comp, "words").Get(v => v.Length == 0 ? "wordlist is empty" : null);

        if (!wordList.Contains(finishWord))
            throw new AbandonModuleException($"‘words’ does not contain ‘finish’: [Length: {wordList.Length}, finishWord: {finishWord}].");

        var wordScreen = GetField<GameObject>(comp, "wordDisplay", isPublic: true).Get();
        var wordScreenTextMesh = wordScreen.GetComponent<TextMesh>() ?? throw new AbandonModuleException("‘wordDisplay’ does not have a TextMesh component.");
        wordScreenTextMesh.text = "SOLVED";

        addQuestion(module, Question.PurpleArrowsFinish, correctAnswers: new[] { Regex.Replace(finishWord, @"(?<!^).", m => m.Value.ToLowerInvariant()) }, preferredWrongAnswers: wordList.Select(w => w[0] + w.Substring(1).ToLowerInvariant()).ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessPurpleButton(ModuleData module)
    {
        var comp = GetComponent(module, "PurpleButtonScript");
        var cyclingNumbers = GetArrayField<int>(comp, "_cyclingNumbers").Get(expectedLength: 6);
        yield return WaitForSolve;

        var preferredWrongNumbers = Enumerable.Range(0, cyclingNumbers.Max() + 1).ToList();
        while (preferredWrongNumbers.Count < 6)
            preferredWrongNumbers.Add(preferredWrongNumbers.Max() + 1);
        var preferredWrongAnswers = preferredWrongNumbers.Select(n => n.ToString()).ToArray();

        addQuestions(module, Enumerable.Range(0, 6).Select(ix =>
            makeQuestion(Question.PurpleButtonNumbers, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { cyclingNumbers[ix].ToString() }, preferredWrongAnswers: preferredWrongAnswers)));
    }

    private IEnumerator<YieldInstruction> ProcessPuzzleIdentification(ModuleData module)
    {
        var comp = GetComponent(module, "PuzzleIdentificationScript");
        yield return WaitForSolve;

        var namesType = comp.GetType().Assembly.GetType("PuzzleIdentification.Data") ?? throw new AbandonModuleException("I cannot find the PuzzleIdentification.Data type.");
        var names = GetStaticField<string[][]>(namesType, "PuzzleNames", isPublic: true).Get();

        // Grabs the first two puzzle numbers and their games of origin
        var puzzlesOneAndTwo = GetArrayField<int>(comp, "ChosenPuzzles").Get();
        // Grabs the third puzzle number
        var puzzleThree = GetField<int>(comp, "ChosenPuzzle").Get();
        // Grabs the third game of origin
        var gameThree = GetField<int>(comp, "ChosenGame").Get();
        var gameNames = GetField<string[]>(comp, "GameNames").Get();

        addQuestions(module,
            makeQuestion(Question.PuzzleIdentificationNum, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { (puzzlesOneAndTwo[0] + 1).ToString("000") }),
            makeQuestion(Question.PuzzleIdentificationNum, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { (puzzlesOneAndTwo[1] + 1).ToString("000") }),
            makeQuestion(Question.PuzzleIdentificationNum, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { (puzzleThree + 1).ToString("000") }),
            makeQuestion(Question.PuzzleIdentificationGame, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { gameNames[puzzlesOneAndTwo[2]] }, preferredWrongAnswers: gameNames),
            makeQuestion(Question.PuzzleIdentificationGame, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { gameNames[puzzlesOneAndTwo[3]] }, preferredWrongAnswers: gameNames),
            makeQuestion(Question.PuzzleIdentificationGame, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { gameNames[gameThree] }, preferredWrongAnswers: gameNames),
            makeQuestion(Question.PuzzleIdentificationName, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { names[puzzlesOneAndTwo[2]][puzzlesOneAndTwo[0]] }, preferredWrongAnswers: names[puzzlesOneAndTwo[2]]),
            makeQuestion(Question.PuzzleIdentificationName, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { names[puzzlesOneAndTwo[3]][puzzlesOneAndTwo[1]] }, preferredWrongAnswers: names[puzzlesOneAndTwo[3]]),
            makeQuestion(Question.PuzzleIdentificationName, module, formatArgs: new[] { Ordinal(3) }, correctAnswers: new[] { names[gameThree][puzzleThree] }, preferredWrongAnswers: names[gameThree]));
    }

    private IEnumerator<YieldInstruction> ProcessPuzzlingHexabuttons(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "puzzlingHexabuttons");
        var texts = GetArrayField<TextMesh>(comp, "buttonText", true).Get(expectedLength: 7);
        var center = texts[6].text[0];
        if (center is < 'A' or > 'F')
            throw new AbandonModuleException($"Center button label ({center}) was not in \"ABCDEF\"");
        var outer = GetArrayField<char>(comp, "solution").Get(expectedLength: 6, validator: v => v is < 'A' or > 'F' ? "Expected character in “ABCDEF”" : null);

        var formats = new[] { "top-left", "top-right", "middle-left", "middle-right", "bottom-left", "bottom-right", "center" };
        addQuestions(module, outer.Concat(new[] { center }).Select((c, i) =>
            makeQuestion(Question.PuzzlingHexabuttonsLetter, module,
                correctAnswers: new[] { c.ToString() },
                formatArgs: new[] { formats[i] })));

        yield return null; // Allow other Souvenirs to grab the text

        foreach (var text in texts)
            text.text = "";
    }
}
