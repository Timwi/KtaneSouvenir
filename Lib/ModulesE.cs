using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessEeBgnillepS(KMBombModule module)
    {
        var comp = GetComponent(module, "tpircSeeBgnillepS");
        var fldSolved = GetField<bool>(comp, "devloSeludom");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_eeBgnillepS);
        var focus = GetField<string>(comp, "drowyek").Get().ToLowerInvariant();
        var spellTheWord = new[] { "accommodation", "acquiesce", "antediluvian", "appoggiatura", "autochthonous", "bouillabaisse", "bourgeoisie", "chauffeur", "chiaroscurist", "cholmondeley", "chrematistic", "chrysanthemum", "cnemidophorous", "conscientious", "courtoisie", "cymotrichous", "daquiri", "demitasse", "elucubrate", "embarrass", "eudaemonic", "euonym", "featherstonehaugh", "feuilleton", "fluorescent", "foudroyant", "gnocchi", "idiosyncracy", "irascible", "kierkagaardian", "laodicean", "liaison", "logorrhea", "mainwaring", "malfeasance", "manoeuvre", "memento", "milquetoast", "minuscule", "odontalgia", "onomatopoeia", "paraphernalia", "pharaoh", "playwright", "pococurante", "precocious", "privilege", "prospicience", "psittaceous", "psoriasis", "pterodactyl", "questionnaire", "rhythm", "sacreligious", "scherenschnitte", "sergeant", "smaragdine", "stromuhr", "succedaneum", "surveillance", "taaffeite", "unconscious", "ursprache", "vengeance", "vivisepulture", "wednesday", "withhold", "worcestershire", "xanthosis", "ytterbium" };

        addQuestions(module, makeQuestion(Question.eeBgnillepSWord, _eeBgnillepS, formatArgs: null, correctAnswers: new[] { focus }, preferredWrongAnswers: spellTheWord));
    }

    private IEnumerable<object> ProcessEight(KMBombModule module)
    {
        var comp = GetComponent(module, "EightModule");
        var fldSolved = GetField<bool>(comp, "solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Eight);

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Eight because the module was force-solved.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var questions = new List<QandA> {
            makeQuestion(Question.EightLastSmallDisplayDigit, _Eight, correctAnswers: new[] { GetProperty<int>(comp, "souvenirLastStageDigit", true).Get().ToString() }),
            makeQuestion(Question.EightLastBrokenDigitPosition, _Eight, correctAnswers: new[] { (GetProperty<int>(comp, "souvenirLastBrokenDigitPosition", true).Get() + 1).ToString() }),
            makeQuestion(Question.EightLastResultingDigits, _Eight, correctAnswers: new[] { GetProperty<int>(comp, "souvenirLastResultingDigits", true).Get().ToString() }, preferredWrongAnswers: GetProperty<HashSet<int>>(comp, "souvenirPossibleLastResultingDigits", true).Get().Select(n => n.ToString().PadLeft(2, '0')).ToArray()),
        };
        var lastDisplayedNumber = GetProperty<int>(comp, "souvenirLastDisplayedNumber", true).Get();
        if (lastDisplayedNumber != -1)
            questions.Add(makeQuestion(Question.EightLastDisplayedNumber, _Eight, correctAnswers: new[] { lastDisplayedNumber.ToString() }, preferredWrongAnswers: GetProperty<HashSet<int>>(comp, "souvenirPossibleLastNumbers", true).Get().Select(n => n.ToString().PadLeft(2, '0')).ToArray()));
        addQuestions(module, questions);
    }

    private IEnumerable<object> ProcessElderFuthark(KMBombModule module)
    {
        var comp = GetComponent(module, "ElderFutharkScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ElderFuthark);

        var pickedRuneNames = GetArrayField<string>(comp, "pickedRuneNames").Get(expectedLength: 3);

        addQuestions(module,
            makeQuestion(Question.ElderFutharkRunes, _ElderFuthark, correctAnswers: new[] { pickedRuneNames[0] }, formatArgs: new[] { "first" }, preferredWrongAnswers: pickedRuneNames),
            makeQuestion(Question.ElderFutharkRunes, _ElderFuthark, correctAnswers: new[] { pickedRuneNames[1] }, formatArgs: new[] { "second" }, preferredWrongAnswers: pickedRuneNames),
            makeQuestion(Question.ElderFutharkRunes, _ElderFuthark, correctAnswers: new[] { pickedRuneNames[2] }, formatArgs: new[] { "third" }, preferredWrongAnswers: pickedRuneNames));
    }

    private IEnumerable<object> ProcessEnaCipher(KMBombModule module)
    {
        var comp = GetComponent(module, "enaCipherScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        var encryptedWord = GetField<string>(comp, "encrypted").Get();
        var keywords = GetField<string[]>(comp, "keywords").Get();
        var extNumbers = GetField<int[]>(comp, "reversed").Get().JoinString();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_EnaCipher);

        var allWordsType = comp.GetType().Assembly.GetType("Words.Data");
        if (allWordsType == null)
            throw new AbandonModuleException("I cannot find the Words.Data type.");
        var allWordsObj = Activator.CreateInstance(allWordsType);
        var allWords = GetArrayField<List<string>>(allWordsObj, "_allWords").Get(expectedLength: 6);

        addQuestions(module, makeQuestion(Question.EnaCipherKeywordAnswer, _EnaCipher, formatArgs: new[] { ordinal(1) }, correctAnswers: new[] { keywords[0] }, preferredWrongAnswers: allWords[keywords[0].Length - 3].ToArray()),
            makeQuestion(Question.EnaCipherKeywordAnswer, _EnaCipher, formatArgs: new[] { ordinal(2) }, correctAnswers: new[] { keywords[1] }, preferredWrongAnswers: allWords[keywords[1].Length - 3].ToArray()),
            makeQuestion(Question.EnaCipherExtAnswer, _EnaCipher, correctAnswers: new[] { extNumbers }),
            makeQuestion(Question.EnaCipherEncryptedAnswer, _EnaCipher, correctAnswers: new[] { encryptedWord }));
    }

    private IEnumerable<object> ProcessEncryptedDice(KMBombModule module)
    {
        var comp = GetComponent(module, "EncrypedDice");

        var fldSolved = GetField<bool>(comp, "solved");
        var fldCanRoll = GetField<bool>(comp, "canRoll");
        var fldRolledValues = GetArrayField<int>(comp, "rolledValues");
        var fldStageNumber = GetIntField(comp, "stagesCompleted");

        var stage = 0;
        var rolledValues = new int[3][];

        module.OnStrike += () => { stage--; return false; };
        module.OnActivate += () => { stage--; };

        while (!fldSolved.Get())
        {
            if (fldStageNumber.Get(min: stage, max: stage + 1) > stage)
            {
                if (stage > 2)
                    throw new AbandonModuleException("Expected 3 stages but we have now exceeded this amount");
                while (!fldCanRoll.Get())
                    yield return null; // Do not wait .1 seconds so we are absolutely sure we get the right stage.
                stage++;
                rolledValues[stage] = fldRolledValues.Get(expectedLength: 3, validator: val => val < 1 || val > 6 ? "expected range 1-6" : null).ToArray();
            }
            yield return new WaitForSeconds(.1f); // Roll animation is much longer than .1 seconds anyway.
        }
        _modulesSolved.IncSafe(_EncryptedDice);
        addQuestions(module, rolledValues.Select((vals, ix) => makeQuestion(Question.EncryptedDice, _EncryptedDice, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: vals.Select(val => (val).ToString()).ToArray())));
    }

    private IEnumerable<object> ProcessEncryptedEquations(KMBombModule module)
    {
        var comp = GetComponent(module, "EncryptedEquations");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_EncryptedEquations);

        var equation = GetField<object>(comp, "CurrentEquation").Get();

        addQuestions(module, new[] { "LeftOperand", "MiddleOperand", "RightOperand" }
            .Select(fldName => GetField<object>(equation, fldName, isPublic: true).Get())
            .Select(op => GetField<object>(op, "Shape", isPublic: true).Get())
            .Select(sh => GetIntField(sh, "TextureIndex", isPublic: true).Get(min: -1, max: EncryptedEquationsSprites.Length - 1))
            .Select((txIx, opIx) => txIx == -1 ? null : new { Shape = EncryptedEquationsSprites[txIx], Ordinal = ordinal(opIx + 1) })
            .Where(inf => inf != null)
            .Select(inf => makeQuestion(Question.EncryptedEquationsShapes, _EncryptedEquations, formatArgs: new[] { inf.Ordinal }, correctAnswers: new[] { inf.Shape }, preferredWrongAnswers: EncryptedEquationsSprites)));
    }

    private IEnumerable<object> ProcessEncryptedHangman(KMBombModule module)
    {
        var comp = GetComponent(module, "HangmanScript");
        var fldSolved = GetField<bool>(comp, "isSolved", isPublic: true);
        var fldInAnimation = GetField<bool>(comp, "inAnimation", isPublic: true);
        var fldUncipheredAnswer = GetField<string>(comp, "uncipheredanswer", isPublic: true);
        var fldAnswerDisp = GetField<TextMesh>(comp, "AnswerDisp", isPublic: true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_EncryptedHangman);

        // Dirty trick to circumvent the solve animation
        fldUncipheredAnswer.Set("");

        // Wait for the solve animation to finish (it still does one iteration despite the above trick)
        while (fldInAnimation.Get())
            yield return new WaitForSeconds(.1f);

        fldAnswerDisp.Get().text = "G O O D\nJ O B";

        var moduleName = GetField<string>(comp, "moduleName", isPublic: true).Get(v => v.Length == 0 ? "empty string" : null);
        var wrongModuleNames = Bomb.GetSolvableModuleNames();
        // If there are less than 4 eligible modules, fill the remaining spaces with random other modules.
        if (wrongModuleNames.Count < 4)
            wrongModuleNames.AddRange(_attributes.Where(x => x.Value != null).Select(x => x.Value.ModuleNameWithThe).Distinct());

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.EncryptedHangmanModule, _EncryptedHangman, correctAnswers: new[] { moduleName }, preferredWrongAnswers: wrongModuleNames.ToArray()));

        var encryptionMethodNames = new[] { "Caesar Cipher", "Playfair Cipher", "Rot-13 Cipher", "Atbash Cipher", "Affine Cipher", "Modern Cipher", "Vigenère Cipher" };
        var encryptionMethod = GetIntField(comp, "encryptionMethod").Get(0, encryptionMethodNames.Length - 1);
        qs.Add(makeQuestion(Question.EncryptedHangmanEncryptionMethod, _EncryptedHangman, correctAnswers: new[] { encryptionMethodNames[encryptionMethod] }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessEncryptedMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "encryptedMazeScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var shapeCw = GetIntField(comp, "shapeMarkerCw").Get(0, 4);
        var shapeCcw = GetIntField(comp, "shapeMarkerCcw").Get(0, 4);
        var markerCw = GetIntField(comp, "featureMarkerCw").Get(0, 5);
        var markerCcw = GetIntField(comp, "featureMarkerCcw").Get(0, 5);
        var markerCharacters = GetField<string[,]>(comp, "markerIndex").Get(validator: arr => arr.GetLength(0) != 5 ? "expected length 5 in dimension 0" : arr.GetLength(1) != 6 ? "expected length 6 in dimension 1" : null);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_EncryptedMaze);

        var textMesh = GetArrayField<TextMesh>(comp, "mazeIndex", isPublic: true).Get(expectedLength: 36)[0];
        addQuestions(module,
            makeQuestion(Question.EncryptedMazeSymbols, _EncryptedMaze, textMesh.font, textMesh.GetComponent<MeshRenderer>().sharedMaterial.mainTexture, formatArgs: new[] { "clockwise" }, correctAnswers: new[] { markerCharacters[shapeCw, markerCw] }, preferredWrongAnswers: new[] { markerCharacters[shapeCcw, markerCcw] }),
            makeQuestion(Question.EncryptedMazeSymbols, _EncryptedMaze, textMesh.font, textMesh.GetComponent<MeshRenderer>().sharedMaterial.mainTexture, formatArgs: new[] { "counter-clockwise" }, correctAnswers: new[] { markerCharacters[shapeCcw, markerCcw] }, preferredWrongAnswers: new[] { markerCharacters[shapeCw, markerCw] }));
    }

    private IEnumerable<object> ProcessEncryptedMorse(KMBombModule module)
    {
        var comp = GetComponent(module, "EncryptedMorseModule");
        var fldSolved = GetField<bool>(comp, "solved");

        string[] formatCalls = { "Detonate", "Ready Now", "We're Dead", "She Sells", "Remember", "Great Job", "Solo This", "Keep Talk" };
        string[] formatResponses = { "Please No", "Cheesecake", "Sadface", "Sea Shells", "Souvenir", "Thank You", "I Dare You", "No Explode" };
        int index = GetIntField(comp, "callResponseIndex").Get(0, Math.Min(formatCalls.Length - 1, formatResponses.Length - 1));

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_EncryptedMorse);
        addQuestions(module,
            makeQuestion(Question.EncryptedMorseCallResponse, _EncryptedMorse, formatArgs: new[] { "received call" }, correctAnswers: new[] { formatCalls[index] }, preferredWrongAnswers: formatCalls),
            makeQuestion(Question.EncryptedMorseCallResponse, _EncryptedMorse, formatArgs: new[] { "sent response" }, correctAnswers: new[] { formatResponses[index] }, preferredWrongAnswers: formatResponses));
    }

    private IEnumerable<object> ProcessEncryptionBingo(KMBombModule module)
    {
        var comp = GetComponent(module, "encryptionBingoScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldBall = GetField<bool>(comp, "ballOut");
        var stampedSquares = GetField<List<int>>(comp, "stampedSquares").Get();
        var encodingNames = GetArrayField<string>(comp, "encryptions").Get();

        // When the first correct(!) square is pressed, Encryption Bingo adds an entry to stampedSquares but helpfully waits .25 sec before changing any variables, including encryptionIndex
        while (!fldBall.Get() || stampedSquares.Count == 0)
            yield return null;  // don’t wait .1 sec here because it’s important to not miss the moment
        var encoding = GetIntField(comp, "encryptionIndex").Get();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_EncryptionBingo);

        addQuestion(module, Question.EncryptionBingoEncoding, correctAnswers: new[] { encodingNames[encoding] }, preferredWrongAnswers: encodingNames);
    }

    private IEnumerable<object> ProcessEnigmaCycle(KMBombModule module)
    {
        var comp = GetComponent(module, "EnigmaCycleScript");
        var fldSolved = GetField<bool>(comp, "isSolving");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_EnigmaCycle);

        var kvp = GetField<KeyValuePair<string, string>>(comp, "selectedMessageResponsePair").Get();
        var message = kvp.Key.Substring(0, 1) + kvp.Key.Substring(1).ToLowerInvariant();
        var response = kvp.Value.Substring(0, 1) + kvp.Value.Substring(1).ToLowerInvariant();
        var wl = new[] { "ABNORMAL", "AUTHORED", "BACKDOOR", "BOULDERS", "CHANGING", "CUMBERED", "DEBUGGED", "DODGIEST", "EDITABLE", "EXCESSES", "FAIRYISM", "FRAGMENT", "GIBBERED", "GROANING", "HEADACHE", "HUDDLING", "ILLUSORY", "IRONICAL", "JOKINGLY", "JUDGMENT", "KEYNOTES", "KINDLING", "LIKENESS", "LOCKOUTS", "MOBILITY", "MUFFLING", "NEUTRALS", "NOTIONAL", "OFFTRACK", "ORDERING", "PHANTASM", "PROVOKED", "QUITTERS", "QUOTABLE", "RHETORIC", "ROULETTE", "SHUTDOWN", "SUBLIMES", "TARTNESS", "TYPHONIC", "UNPURGED", "UGLINESS", "VARIANCE", "VOLATILE", "WACKIEST", "WORKFLOW", "XENOLITH", "XANTHENE", "YABBERED", "YOURSELF", "ZAPPIEST", "ZILLIONS" };
        var wordList = Enumerable.Range(0, wl.Length).Select(word => wl[word].Substring(0, 1) + wl[word].Substring(1).ToLowerInvariant()).ToArray();

        addQuestions(module,
          makeQuestion(Question.EnigmaCycleWords, _EnigmaCycle, formatArgs: new[] { "message" }, correctAnswers: new[] { message }, preferredWrongAnswers: wordList),
          makeQuestion(Question.EnigmaCycleWords, _EnigmaCycle, formatArgs: new[] { "response" }, correctAnswers: new[] { response }, preferredWrongAnswers: wordList));
    }

    private IEnumerable<object> ProcessEntryNumberFour(KMBombModule module)
    {
        var comp = GetComponent(module, "EntryNumberFourScript");
        var fldSolved = GetField<bool>(comp, "Solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_EntryNumberFour);

        var num1 = GetIntField(comp, "Num1").Get().ToString("00000000");
        var num2 = GetIntField(comp, "Num2").Get().ToString("00000000");
        var num3 = GetIntField(comp, "Num3").Get().ToString("00000000");
        var expected = GetIntField(comp, "Expected").Get().ToString("00000000");
        var coeff = GetIntField(comp, "Add").Get().ToString("00000000");

        addQuestions(module,
            makeQuestion(Question.EntryNumberFourNumber1, _EntryNumberFour, correctAnswers: new[] { num1 }),
            makeQuestion(Question.EntryNumberFourNumber2, _EntryNumberFour, correctAnswers: new[] { num2 }),
            makeQuestion(Question.EntryNumberFourNumber3, _EntryNumberFour, correctAnswers: new[] { num3 }),
            makeQuestion(Question.EntryNumberFourExpected, _EntryNumberFour, correctAnswers: new[] { expected }),
            makeQuestion(Question.EntryNumberFourCoeff, _EntryNumberFour, correctAnswers: new[] { coeff }));
    }

    private IEnumerable<object> ProcessEntryNumberOne(KMBombModule module)
    {
        var comp = GetComponent(module, "EntryNumberOneScript");
        var fldSolved = GetField<bool>(comp, "Solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_EntryNumberOne);

        var num2 = GetIntField(comp, "Num2").Get().ToString("00000000");
        var num3 = GetIntField(comp, "Num3").Get().ToString("00000000");
        var num4 = GetIntField(comp, "Num4").Get().ToString("00000000");
        var expected = GetIntField(comp, "Expected").Get().ToString("00000000");
        var coeff = (100000000 - GetIntField(comp, "Subtract").Get()).ToString("00000000");

        addQuestions(module,
            makeQuestion(Question.EntryNumberOneNumbers, _EntryNumberOne, formatArgs: new[] { "second" }, correctAnswers: new[] { num2 }),
            makeQuestion(Question.EntryNumberOneNumbers, _EntryNumberOne, formatArgs: new[] { "third" }, correctAnswers: new[] { num3 }),
            makeQuestion(Question.EntryNumberOneNumbers, _EntryNumberOne, formatArgs: new[] { "fourth" }, correctAnswers: new[] { num4 }),
            makeQuestion(Question.EntryNumberOneExpected, _EntryNumberOne, correctAnswers: new[] { expected }),
            makeQuestion(Question.EntryNumberOneCoeff, _EntryNumberOne, correctAnswers: new[] { coeff }));
    }

    private IEnumerable<object> ProcessEpelleMoiCa(KMBombModule module)
    {
        var comp = GetComponent(module, "EpelleMoiCaScript");
        var fldSolved = GetField<bool>(comp, "IsSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_EpelleMoiCa);

        var wordList = GetField<string[][]>(comp, "wordList").Get();
        var inputtedText = GetField<string>(comp, "inputtedText").Get();
        int index = -1;
        for (int i = 0; i < wordList.Length; i++)
            if (wordList[i].Contains(inputtedText))
                index = i;
        var words = Enumerable.Range(0, wordList.Length).Except(new[] { index }).Select(i => wordList[i][0]).ToArray();

        addQuestions(module, makeQuestion(Question.EpelleMoiCaWord, _EpelleMoiCa, correctAnswers: new[] { inputtedText }, preferredWrongAnswers: words));
    }

    private IEnumerable<object> ProcessEquationsX(KMBombModule module)
    {
        var comp = GetComponent(module, "EquationsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var symbol = GetField<GameObject>(comp, "symboldisplay", isPublic: true).Get().GetComponentInChildren<TextMesh>().text;

        if (!new[] { "H(T)", "R", "c", "w", "Z(T)", "t", "m", "a", "K" }.Contains(symbol))
            throw new AbandonModuleException($"‘symbol’ has an unexpected character: {symbol}");

        // Equations X uses symbols that don’t translate well to Souvenir. This switch statement is used to correctly translate the answer.
        switch (symbol)
        {
            case "c":
                symbol = "χ";
                break;
            case "R":
                symbol = "P";
                break;
            case "w":
                symbol = "ω";
                break;
            case "t":
                symbol = "τ";
                break;
            case "m":
                symbol = "μ";
                break;
            case "a":
                symbol = "α";
                break;
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_EquationsX);

        addQuestion(module, Question.EquationsXSymbols, correctAnswers: new[] { symbol });
    }

    private IEnumerable<object> ProcessEtterna(KMBombModule module)
    {
        var comp = GetComponent(module, "Etterna");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Etterna);

        var correct = GetArrayField<byte>(comp, "correct").Get(expectedLength: 4, validator: b => b > 32 || b == 0 ? "expected 1–32" : null);
        addQuestions(module, correct.Select((answer, ix) => makeQuestion(Question.EtternaNumber, _Etterna, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { answer.ToString() })));
    }

    private IEnumerable<object> ProcessExoplanets(KMBombModule module)
    {
        var comp = GetComponent(module, "exoplanets");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Exoplanets);

        var targetPlanet = GetIntField(comp, "targetPlanet").Get(0, 2);
        var targetDigit = GetIntField(comp, "targetDigit").Get(0, 9);

        var startingTargetPlanet = GetIntField(comp, "startingTargetPlanet").Get(0, 2);
        var startingTargetDigit = GetIntField(comp, "startingTargetDigit").Get(0, 9);

        var positionNames = GetStaticField<string[]>(comp.GetType(), "positionNames").Get(validator: arr => arr.Length != 3 ? "expected length 3" : null);

        addQuestions(module,
            makeQuestion(Question.ExoplanetsStartingTargetPlanet, _Exoplanets, correctAnswers: new[] { positionNames[startingTargetPlanet] }),
            makeQuestion(Question.ExoplanetsStartingTargetDigit, _Exoplanets, correctAnswers: new[] { startingTargetDigit.ToString() }),
            makeQuestion(Question.ExoplanetsTargetPlanet, _Exoplanets, correctAnswers: new[] { positionNames[targetPlanet] }),
            makeQuestion(Question.ExoplanetsTargetDigit, _Exoplanets, correctAnswers: new[] { targetDigit.ToString() }));
    }
}