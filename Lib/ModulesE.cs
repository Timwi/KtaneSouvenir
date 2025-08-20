using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessEarthbound(ModuleData module)
    {
        var comp = GetComponent(module, "EarthboundScript");
        yield return WaitForSolve;

        var enemyIndex = GetIntField(comp, "enemyIndex").Get(val => val is < 0 or > 29 ? "expected range 0–29" : null);
        var enemySprites = GetArrayField<Sprite>(comp, "enemyOptions", isPublic: true).Get(expectedLength: 30).Select(sprite => sprite.TranslateSprite(sprite.name switch
        {
            "Absolutely Safe Capsule" => 350,
            "Mad Car" or "Mr Passion" => 200,
            _ => 250,
        })).ToArray();

        var backgroundMaterials = GetArrayField<Material>(comp, "backgroundOptions", isPublic: true).Get(expectedLength: 30);
        var backgroundIndex = GetIntField(comp, "usedBackgroundInt").Get(val => val is < 0 or > 29 ? "expected range 0–29" : null);

        // Get the smallest width and height to make all answers the same dimensions
        var width = backgroundMaterials.Min(m => m.mainTexture.width);
        var height = backgroundMaterials.Min(m => m.mainTexture.height);
        var backgroundSprites = backgroundMaterials.Select(material => Sprite.Create(material.mainTexture as Texture2D, new Rect(0, 0, width, height), new Vector2(0, .5f), 200f)).ToArray();

        addQuestions(module,
            makeQuestion(Question.EarthboundBackground, module, correctAnswers: new[] { backgroundSprites[backgroundIndex] }, allAnswers: backgroundSprites),
            makeQuestion(Question.EarthboundMonster, module, correctAnswers: new[] { enemySprites[enemyIndex] }, allAnswers: enemySprites));
    }

    private IEnumerator<YieldInstruction> ProcessEeBgnillepS(ModuleData module)
    {
        var comp = GetComponent(module, "tpircSeeBgnillepS");
        yield return WaitForSolve;

        var focus = GetField<string>(comp, "drowyek").Get().ToLowerInvariant();
        var spellTheWord = new[] { "accommodation", "acquiesce", "antediluvian", "appoggiatura", "autochthonous", "bouillabaisse", "bourgeoisie", "chauffeur", "chiaroscurist", "cholmondeley", "chrematistic", "chrysanthemum", "cnemidophorous", "conscientious", "courtoisie", "cymotrichous", "daquiri", "demitasse", "elucubrate", "embarrass", "eudaemonic", "euonym", "featherstonehaugh", "feuilleton", "fluorescent", "foudroyant", "gnocchi", "idiosyncracy", "irascible", "kierkagaardian", "laodicean", "liaison", "logorrhea", "mainwaring", "malfeasance", "manoeuvre", "memento", "milquetoast", "minuscule", "odontalgia", "onomatopoeia", "paraphernalia", "pharaoh", "playwright", "pococurante", "precocious", "privilege", "prospicience", "psittaceous", "psoriasis", "pterodactyl", "questionnaire", "rhythm", "sacreligious", "scherenschnitte", "sergeant", "smaragdine", "stromuhr", "succedaneum", "surveillance", "taaffeite", "unconscious", "ursprache", "vengeance", "vivisepulture", "wednesday", "withhold", "worcestershire", "xanthosis", "ytterbium" };

        addQuestion(module, Question.eeBgnillepSWord, correctAnswers: new[] { focus }, preferredWrongAnswers: spellTheWord);
    }

    private IEnumerator<YieldInstruction> ProcessEight(ModuleData module)
    {
        var comp = GetComponent(module, "EightModule");
        yield return WaitForSolve;

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            legitimatelyNoQuestion(module, "The module was force-solved.");
            yield break;
        }

        var questions = new List<QandA> {
            makeQuestion(Question.EightLastSmallDisplayDigit, module, correctAnswers: new[] { GetProperty<int>(comp, "souvenirLastStageDigit", true).Get().ToString() }),
            makeQuestion(Question.EightLastBrokenDigitPosition, module, correctAnswers: new[] { (GetProperty<int>(comp, "souvenirLastBrokenDigitPosition", true).Get() + 1).ToString() }),
            makeQuestion(Question.EightLastResultingDigits, module, correctAnswers: new[] { GetProperty<int>(comp, "souvenirLastResultingDigits", true).Get().ToString() }, preferredWrongAnswers: GetProperty<HashSet<int>>(comp, "souvenirPossibleLastResultingDigits", true).Get().Select(n => n.ToString().PadLeft(2, '0')).ToArray()),
        };
        var lastDisplayedNumber = GetProperty<int>(comp, "souvenirLastDisplayedNumber", true).Get();
        if (lastDisplayedNumber != -1)
            questions.Add(makeQuestion(Question.EightLastDisplayedNumber, module, correctAnswers: new[] { lastDisplayedNumber.ToString() }, preferredWrongAnswers: GetProperty<HashSet<int>>(comp, "souvenirPossibleLastNumbers", true).Get().Select(n => n.ToString().PadLeft(2, '0')).ToArray()));
        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessElderFuthark(ModuleData module)
    {
        var comp = GetComponent(module, "ElderFutharkScript");
        yield return WaitForSolve;

        var pickedRuneNames = GetArrayField<string>(comp, "pickedRuneNames").Get(expectedLength: 3);

        addQuestions(module,
            makeQuestion(Question.ElderFutharkRunes, module, correctAnswers: new[] { pickedRuneNames[0] }, formatArgs: new[] { "first" }, preferredWrongAnswers: pickedRuneNames),
            makeQuestion(Question.ElderFutharkRunes, module, correctAnswers: new[] { pickedRuneNames[1] }, formatArgs: new[] { "second" }, preferredWrongAnswers: pickedRuneNames),
            makeQuestion(Question.ElderFutharkRunes, module, correctAnswers: new[] { pickedRuneNames[2] }, formatArgs: new[] { "third" }, preferredWrongAnswers: pickedRuneNames));
    }

    private IEnumerator<YieldInstruction> ProcessEmoji(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "emojiScript");
        var spriteSlots = GetArrayField<SpriteRenderer>(comp, "SpriteSlots", true).Get(expectedLength: 4);
        var allSprites = GetArrayField<Sprite>(comp, "EmojiSprites", true).Get(expectedLength: 625);
        var usedSprites = new[] { Array.IndexOf(allSprites, spriteSlots[0].sprite), Array.IndexOf(allSprites, spriteSlots[1].sprite) };
        allSprites = allSprites.TranslateSprites(200).ToArray();

        addQuestions(module,
            makeQuestion(Question.EmojiEmoji, module, formatArgs: new[] { "left" }, correctAnswers: new[] { allSprites[usedSprites[0]] }, preferredWrongAnswers: new[] { allSprites[usedSprites[1]] }, allAnswers: allSprites),
            makeQuestion(Question.EmojiEmoji, module, formatArgs: new[] { "right" }, correctAnswers: new[] { allSprites[usedSprites[1]] }, preferredWrongAnswers: new[] { allSprites[usedSprites[0]] }, allAnswers: allSprites));
    }

    private IEnumerator<YieldInstruction> ProcessEnaCipher(ModuleData module)
    {
        var comp = GetComponent(module, "enaCipherScript");

        var encryptedWord = GetField<string>(comp, "encrypted").Get();
        var keywords = GetField<string[]>(comp, "keywords").Get();
        var extNumbers = GetField<int[]>(comp, "reversed").Get().JoinString();

        yield return WaitForSolve;

        var allWordsType = comp.GetType().Assembly.GetType("Words.Data") ?? throw new AbandonModuleException("I cannot find the Words.Data type.");
        var allWordsObj = Activator.CreateInstance(allWordsType);
        var allWords = GetArrayField<List<string>>(allWordsObj, "_allWords").Get(expectedLength: 6);

        addQuestions(module, makeQuestion(Question.EnaCipherKeywordAnswer, module, formatArgs: new[] { Ordinal(1) }, correctAnswers: new[] { keywords[0] }, preferredWrongAnswers: allWords[keywords[0].Length - 3].ToArray()),
            makeQuestion(Question.EnaCipherKeywordAnswer, module, formatArgs: new[] { Ordinal(2) }, correctAnswers: new[] { keywords[1] }, preferredWrongAnswers: allWords[keywords[1].Length - 3].ToArray()),
            makeQuestion(Question.EnaCipherExtAnswer, module, correctAnswers: new[] { extNumbers }),
            makeQuestion(Question.EnaCipherEncryptedAnswer, module, correctAnswers: new[] { encryptedWord }));
    }

    private IEnumerator<YieldInstruction> ProcessEncryptedDice(ModuleData module)
    {
        var comp = GetComponent(module, "EncrypedDice");

        var fldSolved = GetField<bool>(comp, "solved");
        var fldCanRoll = GetField<bool>(comp, "canRoll");
        var fldRolledValues = GetArrayField<int>(comp, "rolledValues");
        var fldStageNumber = GetIntField(comp, "stagesCompleted");

        var stage = 0;
        var rolledValues = new int[3][];

        module.Module.OnStrike += () => { stage--; return false; };
        module.Module.OnActivate += () => { stage--; };

        while (!fldSolved.Get())
        {
            if (fldStageNumber.Get(min: stage, max: stage + 1) > stage)
            {
                if (stage > 2)
                    throw new AbandonModuleException("Expected 3 stages but we have now exceeded this amount");
                while (!fldCanRoll.Get())
                    yield return null; // Do not wait .1 seconds so we are absolutely sure we get the right stage.
                stage++;
                rolledValues[stage] = fldRolledValues.Get(expectedLength: 3, validator: val => val is < 1 or > 6 ? "expected range 1-6" : null).ToArray();
            }
            yield return new WaitForSeconds(.1f); // Roll animation is much longer than .1 seconds anyway.
        }
        addQuestions(module, rolledValues.Select((vals, ix) => makeQuestion(Question.EncryptedDice, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: vals.Select(val => (val).ToString()).ToArray())));
    }

    private IEnumerator<YieldInstruction> ProcessEncryptedEquations(ModuleData module)
    {
        var comp = GetComponent(module, "EncryptedEquations");
        yield return WaitForSolve;

        var equation = GetField<object>(comp, "CurrentEquation").Get();

        addQuestions(module, new[] { "LeftOperand", "MiddleOperand", "RightOperand" }
            .Select(fldName => GetField<object>(equation, fldName, isPublic: true).Get())
            .Select(op => GetField<object>(op, "Shape", isPublic: true).Get())
            .Select(sh => GetIntField(sh, "TextureIndex", isPublic: true).Get(min: -1, max: EncryptedEquationsSprites.Length - 1))
            .Select((txIx, opIx) => txIx == -1 ? null : new { Shape = EncryptedEquationsSprites[txIx], Ordinal = Ordinal(opIx + 1) })
            .Where(inf => inf != null)
            .Select(inf => makeQuestion(Question.EncryptedEquationsShapes, module, formatArgs: new[] { inf.Ordinal }, correctAnswers: new[] { inf.Shape }, preferredWrongAnswers: EncryptedEquationsSprites)));
    }

    private IEnumerator<YieldInstruction> ProcessEncryptedHangman(ModuleData module)
    {
        var comp = GetComponent(module, "HangmanScript");
        var fldInAnimation = GetField<bool>(comp, "inAnimation", isPublic: true);
        var fldUncipheredAnswer = GetField<string>(comp, "uncipheredanswer", isPublic: true);
        var fldAnswerDisp = GetField<TextMesh>(comp, "AnswerDisp", isPublic: true);

        yield return WaitForSolve;

        // Dirty trick to circumvent the solve animation
        fldUncipheredAnswer.Set("");

        // Wait for the solve animation to finish (it still does one iteration despite the above trick)
        while (fldInAnimation.Get())
            yield return new WaitForSeconds(.1f);

        fldAnswerDisp.Get().text = "G O O D\nJ O B";

        var moduleName = GetField<string>(comp, "moduleName", isPublic: true).Get(v => v.Length == 0 ? "empty string" : null);
        var wrongModuleNames = Bomb.GetSolvableModuleNames().Distinct().ToList();
        // If there are less than 4 eligible modules, fill the remaining spaces with random other modules.
        if (wrongModuleNames.Count < 4)
            wrongModuleNames.AddRange(Ut.Attributes.Select(a => a.Value.ModuleNameWithThe).Distinct());

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.EncryptedHangmanModule, module, correctAnswers: new[] { moduleName }, preferredWrongAnswers: wrongModuleNames.ToArray()));

        var encryptionMethodNames = new[] { "Caesar Cipher", "Playfair Cipher", "Rot-13 Cipher", "Atbash Cipher", "Affine Cipher", "Modern Cipher", "Vigenère Cipher" };
        var encryptionMethod = GetIntField(comp, "encryptionMethod").Get(0, encryptionMethodNames.Length - 1);
        qs.Add(makeQuestion(Question.EncryptedHangmanEncryptionMethod, module, correctAnswers: new[] { encryptionMethodNames[encryptionMethod] }));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessEncryptedMaze(ModuleData module)
    {
        var comp = GetComponent(module, "encryptedMazeScript");
        var shapeCw = GetIntField(comp, "shapeMarkerCw").Get(0, 4);
        var shapeCcw = GetIntField(comp, "shapeMarkerCcw").Get(0, 4);
        var markerCw = GetIntField(comp, "featureMarkerCw").Get(0, 5);
        var markerCcw = GetIntField(comp, "featureMarkerCcw").Get(0, 5);
        var markerCharacters = GetField<string[,]>(comp, "markerIndex").Get(validator: arr => arr.GetLength(0) != 5 ? "expected length 5 in dimension 0" : arr.GetLength(1) != 6 ? "expected length 6 in dimension 1" : null);

        yield return WaitForSolve;

        var textMesh = GetArrayField<TextMesh>(comp, "mazeIndex", isPublic: true).Get(expectedLength: 36)[0];
        addQuestions(module,
            makeQuestion(Question.EncryptedMazeSymbols, module, textMesh.font, textMesh.GetComponent<MeshRenderer>().sharedMaterial.mainTexture, formatArgs: new[] { "clockwise" }, correctAnswers: new[] { markerCharacters[shapeCw, markerCw] }, preferredWrongAnswers: new[] { markerCharacters[shapeCcw, markerCcw] }),
            makeQuestion(Question.EncryptedMazeSymbols, module, textMesh.font, textMesh.GetComponent<MeshRenderer>().sharedMaterial.mainTexture, formatArgs: new[] { "counter-clockwise" }, correctAnswers: new[] { markerCharacters[shapeCcw, markerCcw] }, preferredWrongAnswers: new[] { markerCharacters[shapeCw, markerCw] }));
    }

    private IEnumerator<YieldInstruction> ProcessEncryptedMorse(ModuleData module)
    {
        var comp = GetComponent(module, "EncryptedMorseModule");

        string[] formatCalls = { "Detonate", "Ready Now", "We're Dead", "She Sells", "Remember", "Great Job", "Solo This", "Keep Talk" };
        string[] formatResponses = { "Please No", "Cheesecake", "Sadface", "Sea Shells", "Souvenir", "Thank You", "I Dare You", "No Explode" };
        var index = GetIntField(comp, "callResponseIndex").Get(0, Math.Min(formatCalls.Length - 1, formatResponses.Length - 1));

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.EncryptedMorseCallResponse, module, formatArgs: new[] { "received call" }, correctAnswers: new[] { formatCalls[index] }, preferredWrongAnswers: formatCalls),
            makeQuestion(Question.EncryptedMorseCallResponse, module, formatArgs: new[] { "sent response" }, correctAnswers: new[] { formatResponses[index] }, preferredWrongAnswers: formatResponses));
    }

    private IEnumerator<YieldInstruction> ProcessEncryptionBingo(ModuleData module)
    {
        var comp = GetComponent(module, "encryptionBingoScript");
        var fldBall = GetField<bool>(comp, "ballOut");
        var stampedSquares = GetField<List<int>>(comp, "stampedSquares").Get();
        var encodingNames = GetArrayField<string>(comp, "encryptions").Get();

        // When the first correct(!) square is pressed, Encryption Bingo adds an entry to stampedSquares but helpfully waits .25 sec before changing any variables, including encryptionIndex
        while (!fldBall.Get() || stampedSquares.Count == 0)
            yield return null;  // don’t wait .1 sec here because it’s important to not miss the moment
        var encoding = GetIntField(comp, "encryptionIndex").Get();

        yield return WaitForSolve;

        addQuestion(module, Question.EncryptionBingoEncoding, correctAnswers: new[] { encodingNames[encoding] }, preferredWrongAnswers: encodingNames);
    }

    private IEnumerator<YieldInstruction> ProcessEnigmaCycle(ModuleData module)
    {
        var comp = GetComponent(module, "EnigmaCycleScript");
        yield return WaitForSolve;

        var qs = new List<QandA>();
        var rotComp = GetArrayField<int>(comp, "assignedDialRotations").Get();
        var dialLabels = GetField<string>(comp, "encryptedDisplay").Get();

        for (var dial = 0; dial < 8; dial++)
        {
            switch (dial)
            {
                case 0:
                    qs.Add(makeQuestion(Question.EnigmaCycleDialDirectionsThree, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { CycleModuleThreeSprites[rotComp[dial]] }, preferredWrongAnswers: CycleModuleThreeSprites));
                    break;
                case 4:
                case 5:
                case 6:
                    qs.Add(makeQuestion(Question.EnigmaCycleDialDirectionsTwelve, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { CycleModuleTwelveSprites[rotComp[dial]] }, preferredWrongAnswers: CycleModuleTwelveSprites));
                    break;
                default:
                    qs.Add(makeQuestion(Question.EnigmaCycleDialDirectionsEight, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { CycleModuleEightSprites[rotComp[dial]] }, preferredWrongAnswers: CycleModuleEightSprites));
                    break;
            }
            qs.Add(makeQuestion(Question.EnigmaCycleDialLabels, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { dialLabels[dial].ToString() }));
        }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessEnglishEntries(ModuleData module)
    {
        var comp = GetComponent(module, "EnglishEntries");
        yield return WaitForSolve;

        var loudclapping = GetArrayField<string[]>(comp, "LoudClapping").Get().Select(i => i.Select(j => j.Replace('\n', ' ')).ToArray()).ToArray();
        var ann = GetIntField(comp, "Ann").Get();
        var kevin = GetIntField(comp, "Kevin").Get();
        var allAnswers = loudclapping.SelectMany(i => i).ToArray();

        addQuestion(module, Question.EnglishEntriesDisplay, correctAnswers: new[] { loudclapping[ann][kevin] }, preferredWrongAnswers: allAnswers);
    }

    private IEnumerator<YieldInstruction> ProcessEntryNumberFour(ModuleData module)
    {
        var comp = GetComponent(module, "EntryNumberFourScript");
        yield return WaitForSolve;

        var num1 = GetIntField(comp, "Num1").Get().ToString("00000000");
        var num2 = GetIntField(comp, "Num2").Get().ToString("00000000");
        var num3 = GetIntField(comp, "Num3").Get().ToString("00000000");

        addQuestions(module, new[] { num1, num2, num3 }.SelectMany((n, i) => Enumerable.Range(0, 8).Select(d =>
            makeQuestion(Question.EntryNumberFourDigits, module, formatArgs: new[] { Ordinal(d + 1), Ordinal(i + 1) }, correctAnswers: new[] { n[d].ToString() }))));
    }

    private IEnumerator<YieldInstruction> ProcessEntryNumberOne(ModuleData module)
    {
        var comp = GetComponent(module, "EntryNumberOneScript");
        yield return WaitForSolve;

        var num2 = GetIntField(comp, "Num2").Get().ToString("00000000");
        var num3 = GetIntField(comp, "Num3").Get().ToString("00000000");
        var num4 = GetIntField(comp, "Num4").Get().ToString("00000000");

        addQuestions(module, new[] { num2, num3, num4 }.SelectMany((n, i) => Enumerable.Range(0, 8).Select(d =>
            makeQuestion(Question.EntryNumberOneDigits, module, formatArgs: new[] { Ordinal(d + 1), Ordinal(i + 2) }, correctAnswers: new[] { n[d].ToString() }))));
    }

    private IEnumerator<YieldInstruction> ProcessEpelleMoiCa(ModuleData module)
    {
        var comp = GetComponent(module, "EpelleMoiCaScript");
        yield return WaitForSolve;

        var wordList = GetField<string[][]>(comp, "wordList").Get();
        var inputtedText = GetField<string>(comp, "inputtedText").Get();
        var index = -1;
        for (var i = 0; i < wordList.Length; i++)
            if (wordList[i].Contains(inputtedText))
                index = i;
        var words = Enumerable.Range(0, wordList.Length).Except(new[] { index }).Select(i => wordList[i][0]).ToArray();

        addQuestion(module, Question.ÉpelleMoiÇaWord, correctAnswers: new[] { inputtedText }, preferredWrongAnswers: words);
    }

    private IEnumerator<YieldInstruction> ProcessEquationsX(ModuleData module)
    {
        var comp = GetComponent(module, "EquationsScript");

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

        yield return WaitForSolve;

        addQuestion(module, Question.EquationsXSymbols, correctAnswers: new[] { symbol });
    }

    private IEnumerator<YieldInstruction> ProcessErrorCodes(ModuleData module)
    {
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var comp = GetComponent(module, "ErrorCodes");
        var errorTextDisplay = GetField<TextMesh>(comp, "errorText", isPublic: true).Get();
        var fixTextDisplay = GetField<TextMesh>(comp, "fixText", isPublic: true).Get();
        var errorPrefix = GetStaticField<string>(comp.GetType(), "ERROR_PREFIX").Get();
        var fixPrefix = GetStaticField<string>(comp.GetType(), "FIX_PREFIX").Get();
        var displayCodes = errorTextDisplay.text.Replace(errorPrefix + " ", "").Split(' ');
        var serialNumberHasVowel = Bomb.GetSerialNumber().Any(c => "AEIOU".Contains(c));
        var batteryEven = Bomb.GetBatteryCount() % 2 == 0;

        var conditionTable = new[] { serialNumberHasVowel && batteryEven,
                                        serialNumberHasVowel && !batteryEven,
                                        !serialNumberHasVowel && batteryEven,
                                        !serialNumberHasVowel && !batteryEven };
        var activeErrorIndex = Array.IndexOf(conditionTable, true);

        yield return WaitForSolve;

        addQuestion(module, Question.ErrorCodesActiveError,
            correctAnswers: new[] { displayCodes[activeErrorIndex] },
            preferredWrongAnswers: displayCodes);

        // Change the displays to blank
        errorTextDisplay.text = errorPrefix;
        fixTextDisplay.text = fixPrefix;
    }

    private IEnumerator<YieldInstruction> ProcessEtterna(ModuleData module)
    {
        var comp = GetComponent(module, "Etterna");
        yield return WaitForSolve;

        var correct = GetArrayField<byte>(comp, "correct").Get(expectedLength: 4, validator: b => b is > 32 or 0 ? "expected 1–32" : null);
        addQuestions(module, correct.Select((answer, ix) => makeQuestion(Question.EtternaNumber, module, formatArgs: new[] { Ordinal(ix + 1) }, correctAnswers: new[] { answer.ToString() })));
    }

    private IEnumerator<YieldInstruction> ProcessExoplanets(ModuleData module)
    {
        var comp = GetComponent(module, "exoplanets");
        yield return WaitForSolve;

        var targetPlanet = GetIntField(comp, "targetPlanet").Get(0, 2);
        var targetDigit = GetIntField(comp, "targetDigit").Get(0, 9);

        var startingTargetPlanet = GetIntField(comp, "startingTargetPlanet").Get(0, 2);
        var startingTargetDigit = GetIntField(comp, "startingTargetDigit").Get(0, 9);

        var positionNames = GetStaticField<string[]>(comp.GetType(), "positionNames").Get(validator: arr => arr.Length != 3 ? "expected length 3" : null);

        addQuestions(module,
            makeQuestion(Question.ExoplanetsStartingTargetPlanet, module, correctAnswers: new[] { positionNames[startingTargetPlanet] }),
            makeQuestion(Question.ExoplanetsStartingTargetDigit, module, correctAnswers: new[] { startingTargetDigit.ToString() }),
            makeQuestion(Question.ExoplanetsTargetPlanet, module, correctAnswers: new[] { positionNames[targetPlanet] }),
            makeQuestion(Question.ExoplanetsTargetDigit, module, correctAnswers: new[] { targetDigit.ToString() }));
    }
}
