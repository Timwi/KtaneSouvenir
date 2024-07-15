using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessPalindromes(KMBombModule module)
    {
        var comp = GetComponent(module, "Palindromes");
        var fldSolved = GetField<bool>(comp, "isSolved");
        var fldX = GetField<string>(comp, "x");
        var fldY = GetField<string>(comp, "y");
        var fldZ = GetField<string>(comp, "z");
        var fldN = GetField<string>(comp, "n");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_Palindromes);

        var vars = new[] { fldN, fldX, fldY, fldZ };
        var qs = new List<QandA>();
        for (var varIx = 0; varIx < vars.Length; varIx++)
            for (var digitIx = 0; digitIx < (varIx < 2 ? 5 : 4); digitIx++)       // 5 if x or n, else 4
            {
                var numString = vars[varIx].Get();
                var digit = numString[numString.Length - 1 - digitIx];
                if (digit < '0' || digit > '9')
                    throw new AbandonModuleException($"The chosen character ('{digit}') was unexpected (expected a digit 0–9).");

                var labels = new[] { "the screen", "X", "Y", "Z" };
                qs.Add(makeQuestion(Question.PalindromesNumbers, _Palindromes, formatArgs: new[] { labels[varIx], ordinal(digitIx + 1) }, correctAnswers: new[] { digit.ToString() }));
            }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessParity(KMBombModule module)
    {
        var comp = GetComponent(module, "ParityScript");
        var fldSolved = GetField<bool>(comp, "_solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_Parity);

        var text = GetField<string>(comp, "_displayedText").Get();
        var pairs = new List<string>();
        for (int i = 0; i < 26; i++)
            for (int j = 0; j < 10; j++)
                pairs.Add("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[i].ToString() + j.ToString());
        addQuestion(module, Question.ParityDisplay, correctAnswers: new[] { text }, preferredWrongAnswers: pairs.ToArray());
    }

    private IEnumerable<object> ProcessPartialDerivatives(KMBombModule module)
    {
        var comp = GetComponent(module, "PartialDerivativesScript");
        var fldLeds = GetArrayField<int>(comp, "ledIndex");

        var display = GetField<TextMesh>(comp, "display", isPublic: true).Get();
        var terms = display.text.Split('\n').Select(term => Regex.Replace(Regex.Replace(term.Trim(), @"^(f.*?=|\+) ", ""), @"^- ", "−")).ToArray();
        if (terms.Length != 3)
            throw new AbandonModuleException($"The display does not appear to contain three terms: “{_moduleId}”");

        var vars = new[] { "x", "y", "z" };
        var exponentStrs = new[] { "²", "³", "⁴", "⁵" };
        var writeTerm = new Func<int, bool, int[], string>((int coeff, bool negative, int[] exps) =>
        {
            if (coeff == 0)
                return "0";

            var function = negative ? "−" : "";
            if (coeff > 1)
                function += coeff.ToString();
            for (int j = 0; j < 3; j++)
            {
                if (exps[j] != 0)
                {
                    function += vars[j];
                    if (exps[j] > 1)
                        function += exponentStrs[exps[j] - 2];
                }
            }
            return function;
        });

        var wrongAnswers = new HashSet<string>();
        while (wrongAnswers.Count < 3)
        {
            var exps = new int[3];
            for (int j = 0; j < 3; j++)
                exps[j] = Rnd.Range(0, 6);
            if (exps.All(e => e == 0))
                exps[Rnd.Range(0, 3)] = Rnd.Range(1, 6);
            var wrongTerm = writeTerm(Rnd.Range(1, 10), Rnd.Range(0, 2) != 0, exps);
            if (!terms.Contains(wrongTerm))
                wrongAnswers.Add(wrongTerm);
        }

        var isSolved = false;
        module.OnPass += delegate { isSolved = true; return false; };
        while (!isSolved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PartialDerivatives);

        var leds = fldLeds.Get(expectedLength: 3, validator: l => l < 0 || l > 5 ? "expected range 0–5" : null);
        var colorNames = new[] { "blue", "green", "orange", "purple", "red", "yellow" };
        var qs = new List<QandA>();
        for (var stage = 0; stage < 3; stage++)
            qs.Add(makeQuestion(Question.PartialDerivativesLedColors, _PartialDerivatives, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { colorNames[leds[stage]] }));
        for (var term = 0; term < 3; term++)
            qs.Add(makeQuestion(Question.PartialDerivativesTerms, _PartialDerivatives, formatArgs: new[] { ordinal(term + 1) }, correctAnswers: new[] { terms[term] }, preferredWrongAnswers: wrongAnswers.ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessPassportControl(KMBombModule module)
    {
        var comp = GetComponent(module, "passportControlScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldPassages = GetIntField(comp, "passages");
        var fldExpiration = GetArrayField<int>(comp, "expiration");
        var stamps = GetArrayField<KMSelectable>(comp, "stamps", isPublic: true).Get();
        var textToHide1 = GetArrayField<GameObject>(comp, "passport", isPublic: true).Get(validator: objs => objs.Any(go => go.GetComponent<TextMesh>() == null) ? "doesn’t have a TextMesh component" : null);
        var textToHide2 = GetField<GameObject>(comp, "ticket", isPublic: true).Get(go => go.GetComponent<TextMesh>() == null ? "doesn’t have a TextMesh component" : null);

        var expirationDates = new List<int>();

        for (int i = 0; i < stamps.Length; i++)
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

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        if (expirationDates.Count != 3)
            throw new AbandonModuleException($"The number of retrieved sets of information was {expirationDates.Count} (expected 3).");

        for (int i = 0; i < textToHide1.Length; i++)
            textToHide1[i].GetComponent<TextMesh>().text = "";
        textToHide2.GetComponent<TextMesh>().text = "";

        var altDates = new List<string[]>();

        for (int i = 0; i < expirationDates.Count; i++)
        {
            altDates.Add(new string[6]);
            int startVal = expirationDates[i] - Rnd.Range(0, 6);
            for (int j = 0; j < altDates[i].Length; j++)
                altDates[i][j] = (startVal + j).ToString();
        }

        _modulesSolved.IncSafe(_PassportControl);
        addQuestions(module,
            makeQuestion(Question.PassportControlPassenger, _PassportControl, formatArgs: new[] { "first" }, correctAnswers: new[] { expirationDates[0].ToString() }, preferredWrongAnswers: altDates[0]),
            makeQuestion(Question.PassportControlPassenger, _PassportControl, formatArgs: new[] { "second" }, correctAnswers: new[] { expirationDates[1].ToString() }, preferredWrongAnswers: altDates[1]),
            makeQuestion(Question.PassportControlPassenger, _PassportControl, formatArgs: new[] { "third" }, correctAnswers: new[] { expirationDates[2].ToString() }, preferredWrongAnswers: altDates[2]));
    }

    private IEnumerable<object> ProcessPasswordDestroyer(KMBombModule module)
    {
        var comp = GetComponent(module, "passwordDestroyer");
        var fldSolved = GetField<bool>(comp, "solvedState");

        var fldStartingValue = GetIntField(comp, "CountUpBaseNumber");       // Rv value
        var fldIncreaseFactor = GetIntField(comp, "increaseFactor");    // If value
        var fldTwoFactorV2 = GetIntField(comp, "identityDigit");        // 2FAST value
        var fldTwoFactorAuth1 = GetIntField(comp, "identityDigit1");    // Left half
        var fldTwoFactorAuth2 = GetIntField(comp, "identityDigit2");    // Right half
        var fldSolvePercentage = GetIntField(comp, "solvePercentage");  // Solve Percentage

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PasswordDestroyer);

        addQuestions(module,
            makeQuestion(Question.PasswordDestroyerStartingValue, _PasswordDestroyer, correctAnswers: new[] { fldStartingValue.Get(1000000, 9999999).ToString() }),
            makeQuestion(Question.PasswordDestroyerIncreaseFactor, _PasswordDestroyer, correctAnswers: new[] { fldIncreaseFactor.Get(-1000000, 1000000).ToString() }),
            makeQuestion(Question.PasswordDestroyerTwoFactorV2, _PasswordDestroyer, correctAnswers: new[] { fldTwoFactorV2.Get(100100, 999999).ToString() }),
            makeQuestion(Question.PasswordDestroyerTF1, _PasswordDestroyer, correctAnswers: new[] { (fldTwoFactorAuth1.Get(100, 999) - 100).ToString() }),
            makeQuestion(Question.PasswordDestroyerTF2, _PasswordDestroyer, correctAnswers: new[] { (fldTwoFactorAuth2.Get(100, 999) % 9 == 0 ? 9 : fldTwoFactorAuth2.Get() % 9).ToString() }),
            makeQuestion(Question.PasswordDestroyerSolvePercentage, _PasswordDestroyer, correctAnswers: new[] { fldSolvePercentage.Get(1, 99).ToString() + "%" }));
    }

    private IEnumerable<object> ProcessPatternCube(KMBombModule module)
    {
        var comp = GetComponent(module, "PatternCubeModule");
        var selectableSymbols = GetField<Array>(comp, "_selectableSymbols").Get(ar => ar.Length != 5 ? "expected length 5" : null);
        var selectableSymbolObjects = GetArrayField<MeshRenderer>(comp, "_selectableSymbolObjs").Get(expectedLength: 5);
        var placeableSymbolObjects = GetArrayField<MeshRenderer>(comp, "_placeableSymbolObjs").Get(expectedLength: 6);
        var highlightPos = GetIntField(comp, "_highlightedPosition").Get(min: 0, max: 4);

        // Wait for it to be solved.
        while (selectableSymbols.Cast<object>().Any(obj => obj != null))
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PatternCube);

        var symbols = selectableSymbolObjects.Concat(placeableSymbolObjects.Where(r => r.gameObject.activeSelf))
            .Select(r => PatternCubeSprites[int.Parse(r.sharedMaterial.mainTexture.name.Substring(6))]).ToArray();
        addQuestion(module, Question.PatternCubeHighlightedSymbol, correctAnswers: new[] { symbols[highlightPos] }, preferredWrongAnswers: symbols);
    }

    private IEnumerable<object> ProcessPeriodicWords(KMBombModule module)
    {
        var comp = GetComponent(module, "PeriodicWordsScript");

        var fldSolved = GetField<bool>(comp, "Solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PeriodicWords);

        var words = GetArrayField<string>(comp, "Words").Get().Take(4).ToArray();
        addQuestions(module, Enumerable.Range(0, 3).Select(stage => makeQuestion(Question.PeriodicWordsDisplayedWords, _PeriodicWords, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { words[stage] }, preferredWrongAnswers: words)));
    }

    private IEnumerable<object> ProcessPerspectivePegs(KMBombModule module)
    {
        var comp = GetComponent(module, "PerspectivePegsModule");
        var fldIsComplete = GetField<bool>(comp, "isComplete");
        while (!fldIsComplete.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PerspectivePegs);

        var serialNumber = JObject.Parse(Bomb.QueryWidgets(KMBombInfo.QUERYKEY_GET_SERIAL_NUMBER, null).First())["serial"].ToString();

        int keyNumber = 0;
        string keyColour;
        char prevChar = '\0';
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
        switch (keyNumber % 10)
        {
            case 0: case 3: keyColour = "ColourRed"; break;
            case 4: case 9: keyColour = "ColourYellow"; break;
            case 1: case 7: keyColour = "ColourGreen"; break;
            case 5: case 8: keyColour = "ColourBlue"; break;
            case 2: case 6: keyColour = "ColourPurple"; break;
            default: throw new AbandonModuleException("Invalid keyNumber % 10.");
        }

        var colourMeshes = GetField<MeshRenderer[,]>(comp, "ColourMeshes").Get();
        var pegIndex = Enumerable.Range(0, 5).IndexOf(px => Enumerable.Range(0, 5).Count(i => colourMeshes[px, i].sharedMaterial.name.StartsWith(keyColour)) >= 3);
        if (pegIndex == -1)
            throw new AbandonModuleException($"The key peg couldn't be found (the key colour was {keyColour}).");

        addQuestions(module, Enumerable.Range(0, 5)
            .Select(i => (pegIndex + i) % 5)
            .Select(n => colorNames.First(cn => colourMeshes[n, n].sharedMaterial.name.Substring(6).StartsWith(cn, StringComparison.InvariantCultureIgnoreCase)))
            .Select((col, ix) => makeQuestion(Question.PerspectivePegsColorSequence, _PerspectivePegs, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { col })));
    }

    private IEnumerable<object> ProcessPhosphorescence(KMBombModule module)
    {
        var comp = GetComponent(module, "PhosphorescenceScript");
        var init = GetField<object>(comp, "init").Get();
        var fldSolved = GetField<bool>(init, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Phosphorescence);

        var index = GetIntField(init, "index").Get(min: 0, max: 419);
        var buttonPresses = GetField<Array>(init, "buttonPresses").Get(ar =>
            ar.Length < 3 || ar.Length > 6 ? "expected length 3–6" :
            ar.OfType<object>().Any(v => !GetAnswers(Question.PhosphorescenceButtonPresses).Contains(v.ToString())) ? "contains unknown color" : null);

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.PhosphorescenceOffset, _Phosphorescence, correctAnswers: new[] { index.ToString() }));

        for (int i = 0; i < buttonPresses.GetLength(0); i++)
            qs.Add(makeQuestion(Question.PhosphorescenceButtonPresses, _Phosphorescence, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { buttonPresses.GetValue(i).ToString() }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessPictionary(KMBombModule module)
    {
        var comp = GetComponent(module, "pictionaryModuleScript");

        var fldSolved = GetField<bool>(comp, "solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Pictionary);

        var code = GetField<string>(comp, "code").Get(c => c.Length != 4 || c.Any(ch => !char.IsDigit(ch)) ? "expected a sequence of four digits" : null);
        addQuestion(module, Question.PictionaryCode, correctAnswers: new[] { code });
    }

    private IEnumerable<object> ProcessPie(KMBombModule module)
    {
        var comp = GetComponent(module, "PieScript");
        var fldSolved = GetField<bool>(comp, "solveCoroutineStarted");
        var digits = GetArrayField<string>(comp, "codes").Get(expectedLength: 5);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Pie);

        addQuestions(module, digits.Select((digit, ix) => makeQuestion(Question.PieDigits, _Pie, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { digit }, preferredWrongAnswers: digits)));
    }

    private IEnumerable<object> ProcessPieFlash(KMBombModule module)
    {
        var comp = GetComponent(module, "pieFlashScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");
        var digits = GetArrayField<string>(comp, "codes").Get(expectedLength: 3);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PieFlash);

        // Find valid answers within pi that do not overlap with any of the other strings
        var piString = GetField<string>(comp, "pi").Get();
        var validAnswers = Enumerable.Range(0, piString.Length - 5).Select(ix => piString.Substring(ix, 5)).Where(sps => !digits.Contains(sps)).ToArray();
        addQuestions(module, makeQuestion(Question.PieFlashDigits, _PieFlash, correctAnswers: validAnswers, preferredWrongAnswers: digits));
    }

    private IEnumerable<object> ProcessPigpenCycle(KMBombModule module)
    {
        return processSpeakingEvilCycle1(module, "PigpenCycleScript", Question.PigpenCycleWord, _PigpenCycle);
    }

    private IEnumerable<object> ProcessPlaceholderTalk(KMBombModule module)
    {
        var comp = GetComponent(module, "placeholderTalk");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PlaceholderTalk);

        var answer = GetField<byte>(comp, "answerId").Get(b => b < 0 || b > 16 ? "expected range 0–16" : null) + 1;
        var firstPhrase = GetArrayField<string>(comp, "firstPhrase").Get();
        var firstString = GetField<string>(comp, "firstString").Get(str => !firstPhrase.Contains(str) ? $"expected string to be contained in “{firstPhrase}” (‘firstPhrase’)" : null);
        var ordinals = GetArrayField<string>(comp, "ordinals").Get();
        var currentOrdinal = GetField<string>(comp, "currentOrdinal").Get(str => !ordinals.Contains(str) ? $"expected string to be contained in “{ordinals}” (‘ordinals’)" : null);
        var previousModules = GetField<sbyte>(comp, "previousModules").Get();

        var qs = new List<QandA>();

        // Because the number of solved modules could be any number, the second phrase question should be deactivated if previousModule is either 1 or -1, meaning that they apply to the numbers
        if (previousModules == 0)
            qs.Add(makeQuestion(Question.PlaceholderTalkSecondPhrase, _PlaceholderTalk, correctAnswers: new[] { answer.ToString() }));

        qs.Add(makeQuestion(Question.PlaceholderTalkFirstPhrase, _PlaceholderTalk, correctAnswers: new[] { firstString }, preferredWrongAnswers: firstPhrase));
        qs.Add(makeQuestion(Question.PlaceholderTalkOrdinal, _PlaceholderTalk, correctAnswers: new[] { currentOrdinal }, preferredWrongAnswers: ordinals));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessPinkButton(KMBombModule module)
    {
        var comp = GetComponent(module, "PinkButtonScript");
        var words = GetArrayField<int>(comp, "_words").Get(expectedLength: 4, validator: v => v < 0 || v > 7 ? "expected range 0–7" : null);
        var colors = GetArrayField<int>(comp, "_colors").Get(expectedLength: 4, validator: v => v < 0 || v > 7 ? "expected range 0–7" : null);
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        var abbreviatedColorNames = GetStaticField<string[]>(comp.GetType(), "_abbreviatedColorNames").Get(v => v.Length != 8 ? "expected length 8" : null);
        var colorNames = GetStaticField<string[]>(comp.GetType(), "_colorNames").Get(v => v.Length != 8 ? "expected length 8" : null);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PinkButton);

        addQuestions(module,
            Enumerable.Range(0, 4).SelectMany(ix => Ut.NewArray(
                 makeQuestion(Question.PinkButtonWords, _PinkButton, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { abbreviatedColorNames[words[ix]] }),
                 makeQuestion(Question.PinkButtonColors, _PinkButton, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colorNames[colors[ix]] }))));
    }

    private IEnumerable<object> ProcessPixelCipher(KMBombModule module)
    {
        var comp = GetComponent(module, "pixelcipherScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PixelCipher);

        var keywords = GetArrayField<string>(comp, "pixelKeyword").Get();
        var pickedKeyword = GetIntField(comp, "pickedKeyword").Get(0, keywords.Length - 1);

        addQuestions(module,
            makeQuestion(Question.PixelCipherKeyword, _PixelCipher, correctAnswers: new[] { keywords[pickedKeyword] }));
    }

    private IEnumerable<object> ProcessPlacementRoulette(KMBombModule module)
    {
        var comp = GetComponent(module, "PlacementRouletteModule");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PlacementRoulette);

        var character = GetField<string>(comp, "Character").Get();
        var vehicleType = GetField<string>(comp, "VehicleType").Get();
        var drift = GetField<string>(comp, "Drift").Get();
        var trackType = GetField<string>(comp, "TrackType").Get();
        var vehicle = GetField<string>(comp, "Vehicle").Get();
        var track = GetField<string>(comp, "Track").Get();

        addQuestions(module,
            makeQuestion(Question.PlacementRouletteChar, _PlacementRoulette, correctAnswers: new[] { character }),
            makeQuestion(Question.PlacementRouletteVehicleType, _PlacementRoulette, correctAnswers: new[] { vehicleType }),
            makeQuestion(Question.PlacementRouletteDrift, _PlacementRoulette, correctAnswers: new[] { drift }),
            makeQuestion(Question.PlacementRouletteTrackType, _PlacementRoulette, correctAnswers: new[] { trackType }),
            makeQuestion(Question.PlacementRouletteTrack, _PlacementRoulette, correctAnswers: new[] { track }),
            makeQuestion(Question.PlacementRouletteVehicle, _PlacementRoulette, correctAnswers: new[] { vehicle })
        );
    }

    private IEnumerable<object> ProcessPlanets(KMBombModule module)
    {
        var comp = GetComponent(module, "planetsModScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var planetShown = GetIntField(comp, "planetShown").Get(0, 9);
        var stripColors = GetArrayField<int>(comp, "stripColours").Get(expectedLength: 5, validator: x => x < 0 || x > 8 ? "expected range 0–8" : null);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Planets);

        var stripNames = new[] { "Aqua", "Blue", "Green", "Lime", "Orange", "Red", "Yellow", "White", "Off" };
        addQuestions(module,
            stripColors.Select((strip, count) => makeQuestion(Question.PlanetsStrips, _Planets, formatArgs: new[] { ordinal(count + 1) }, correctAnswers: new[] { stripNames[strip] }))
                .Concat(new[] { makeQuestion(Question.PlanetsPlanet, _Planets, correctAnswers: new[] { PlanetsSprites[planetShown] }, preferredWrongAnswers: (DateTime.Now.Month == 4 && DateTime.Now.Day == 1) ? PlanetsSprites : PlanetsSprites.Take(PlanetsSprites.Length - 2).ToArray()) }));
    }

    private IEnumerable<object> ProcessPlayfairCycle(KMBombModule module)
    {
        return processSpeakingEvilCycle1(module, "PlayfairCycleScript", Question.PlayfairCycleWord, _PlayfairCycle);
    }

    private IEnumerable<object> ProcessPoetry(KMBombModule module)
    {
        var comp = GetComponent(module, "PoetryModule");
        var fldStage = GetIntField(comp, "currentStage");
        var fldStageCount = GetIntField(comp, "stageCount", isPublic: true);

        var answers = new List<string>();
        var selectables = GetArrayField<KMSelectable>(comp, "wordSelectables", isPublic: true).Get(expectedLength: 6);
        var wordTextMeshes = GetArrayField<TextMesh>(comp, "words", isPublic: true).Get(expectedLength: 6);

        for (int i = 0; i < 6; i++)
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

        while (fldStage.Get() < fldStageCount.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Poetry);

        if (answers.Count != fldStageCount.Get())
            throw new AbandonModuleException($"The number of answers captured is not equal to the number of stages played ({fldStageCount.Get()}). Answers were: [{answers.JoinString(", ")}]");

        addQuestions(module, answers.Select((ans, st) => makeQuestion(Question.PoetryAnswers, _Poetry, formatArgs: new[] { ordinal(st + 1) }, correctAnswers: new[] { ans }, preferredWrongAnswers: answers.ToArray())));
    }

    private IEnumerable<object> ProcessPolyhedralMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "PolyhedralMazeModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PolyhedralMaze);

        addQuestion(module, Question.PolyhedralMazeStartPosition, correctAnswers: new[] { GetIntField(comp, "_startFace").Get().ToString() });
    }

    private IEnumerable<object> ProcessPrimeEncryption(KMBombModule module)
    {
        var comp = GetComponent(module, "PrimeEncryptionScript");
        var isSolved = GetField<bool>(comp, "moduleSolved");
        while (!isSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PrimeEncryption);

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

    private IEnumerable<object> ProcessProbing(KMBombModule module)
    {
        var comp = GetComponent(module, "ProbingModule");
        var fldSolved = GetField<bool>(comp, "bSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Probing);

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

        addQuestions(module, wireFrequencies.Select((wf, ix) => makeQuestion(Question.ProbingFrequencies, _Probing, formatArgs: new[] { wireNames[ix] }, correctAnswers: new[] { wf })));
    }

    private IEnumerable<object> ProcessProceduralMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "ProceduralMazeModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ProceduralMaze);

        string initialSeed = GetField<string>(comp, "_initialSeed").Get();

        StartCoroutine(GetMethod<IEnumerator>(GetField<object>(comp, "_mazeRenderer").Get(), "HideRings", 0, true).Invoke());
        addQuestion(module, Question.ProceduralMazeInitialSeed, correctAnswers: new[] { initialSeed });
    }

    private IEnumerable<object> ProcessPunctuationMarks(KMBombModule module)
    {
        var comp = GetComponent(module, "script");

        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PunctuationMarks);

        var number = GetIntField(comp, "memoryBankNumber").Get(min: 0, max: 99).ToString("00");
        addQuestion(module, Question.PunctuationMarksDisplayedNumber, correctAnswers: new[] { number });
    }

    private IEnumerable<object> ProcessPurpleArrows(KMBombModule module)
    {
        var comp = GetComponent(module, "PurpleArrowsScript");

        // The module sets moduleSolved to true at the start of its solve animation but before it is actually marked as solved.
        // Therefore, we use OnPass to wait for the end of that animation and then set the text to “SOLVED” afterwards.
        var solved = false;
        module.OnPass += delegate { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PurpleArrows);

        string finishWord = GetField<string>(comp, "finish").Get(str => str.Length != 6 ? "expected length 6" : null);
        string[] wordList = GetArrayField<string>(comp, "words").Get(expectedLength: 9 * 13);

        if (!wordList.Contains(finishWord))
            throw new AbandonModuleException($"‘wordList’ does not contain ‘finishWord’: [Length: {wordList.Length}, finishWord: {finishWord}].");

        var wordScreen = GetField<GameObject>(comp, "wordDisplay", isPublic: true).Get();
        var wordScreenTextMesh = wordScreen.GetComponent<TextMesh>() ?? throw new AbandonModuleException("‘wordDisplay’ does not have a TextMesh component.");
        wordScreenTextMesh.text = "SOLVED";

        addQuestion(module, Question.PurpleArrowsFinish, correctAnswers: new[] { Regex.Replace(finishWord, @"(?<!^).", m => m.Value.ToLowerInvariant()) }, preferredWrongAnswers: wordList.Select(w => w[0] + w.Substring(1).ToLowerInvariant()).ToArray());
    }

    private IEnumerable<object> ProcessPurpleButton(KMBombModule module)
    {
        var comp = GetComponent(module, "PurpleButtonScript");
        var cyclingNumbers = GetArrayField<int>(comp, "_cyclingNumbers").Get(expectedLength: 6);
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PurpleButton);

        var preferredWrongNumbers = Enumerable.Range(0, cyclingNumbers.Max() + 1).ToList();
        while (preferredWrongNumbers.Count < 6)
            preferredWrongNumbers.Add(preferredWrongNumbers.Max() + 1);
        var preferredWrongAnswers = preferredWrongNumbers.Select(n => n.ToString()).ToArray();

        addQuestions(module, Enumerable.Range(0, 6).Select(ix =>
            makeQuestion(Question.PurpleButtonNumbers, _PurpleButton, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { cyclingNumbers[ix].ToString() }, preferredWrongAnswers: preferredWrongAnswers)));
    }

    private IEnumerable<object> ProcessPuzzleIdentification(KMBombModule module)
    {
        var comp = GetComponent(module, "PuzzleIdentificationScript");
        var fldSolved = GetField<bool>(comp, "Solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_PuzzleIdentification);

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
            makeQuestion(Question.PuzzleIdentificationNum, _PuzzleIdentification, formatArgs: new[] { "first" }, correctAnswers: new[] { (puzzlesOneAndTwo[0] + 1).ToString("000") }),
            makeQuestion(Question.PuzzleIdentificationNum, _PuzzleIdentification, formatArgs: new[] { "second" }, correctAnswers: new[] { (puzzlesOneAndTwo[1] + 1).ToString("000") }),
            makeQuestion(Question.PuzzleIdentificationNum, _PuzzleIdentification, formatArgs: new[] { "third" }, correctAnswers: new[] { (puzzleThree + 1).ToString("000") }),
            makeQuestion(Question.PuzzleIdentificationGame, _PuzzleIdentification, formatArgs: new[] { "first" }, correctAnswers: new[] { gameNames[puzzlesOneAndTwo[2]] }, preferredWrongAnswers: gameNames),
            makeQuestion(Question.PuzzleIdentificationGame, _PuzzleIdentification, formatArgs: new[] { "second" }, correctAnswers: new[] { gameNames[puzzlesOneAndTwo[3]] }, preferredWrongAnswers: gameNames),
            makeQuestion(Question.PuzzleIdentificationGame, _PuzzleIdentification, formatArgs: new[] { "third" }, correctAnswers: new[] { gameNames[gameThree] }, preferredWrongAnswers: gameNames),
            makeQuestion(Question.PuzzleIdentificationName, _PuzzleIdentification, formatArgs: new[] { "first" }, correctAnswers: new[] { names[puzzlesOneAndTwo[2]][puzzlesOneAndTwo[0]] }, preferredWrongAnswers: names[puzzlesOneAndTwo[2]]),
            makeQuestion(Question.PuzzleIdentificationName, _PuzzleIdentification, formatArgs: new[] { "second" }, correctAnswers: new[] { names[puzzlesOneAndTwo[3]][puzzlesOneAndTwo[1]] }, preferredWrongAnswers: names[puzzlesOneAndTwo[3]]),
            makeQuestion(Question.PuzzleIdentificationName, _PuzzleIdentification, formatArgs: new[] { "third" }, correctAnswers: new[] { names[gameThree][puzzleThree] }, preferredWrongAnswers: names[gameThree]));
    }
}
