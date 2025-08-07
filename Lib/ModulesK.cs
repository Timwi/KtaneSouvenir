using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessKanji(ModuleData module)
    {
        var comp = GetComponent(module, "KanjiModule");

        var fldCalculating = GetField<bool>(comp, "Calculating");
        var fldStage = GetIntField(comp, "Stage");
        var screen = GetField<TextMesh>(comp, "ScreenText", isPublic: true).Get();
        var displayedWords = new string[3];
        var stage = 0;

        module.Module.OnStrike += () => { stage--; return false; }; // Grab the text on the screen again on strike.

        while (screen.text == "爆発")
            yield return null; // Don’t wait .1 seconds so that we are absolutely sure we get the right stage. (yes I stole this comment :D)
        displayedWords[0] = screen.text;

        for (stage = 1; stage <= 2 || module.Unsolved; stage++)
        {
            while (fldStage.Get(min: stage, max: stage + 1) == stage)
                yield return new WaitForSeconds(.1f); // Stage animation takes much longer than .1 seconds anyway.
            while (fldCalculating.Get())
                yield return null; // Don’t wait .1 seconds so that we are absolutely sure we get the right stage.
            if (stage < 3)
                displayedWords[stage] = screen.text;
            yield return new WaitForSeconds(.1f); // Keep looping until solve here so we can still grab the text in the event of a strike on the last stage.
        }

        var wordLists = new string[][]
        {
            GetArrayField<string>(comp, "Stage1Words").Get(arr => !arr.Contains(displayedWords[0]) ? $"expected array to contain \"{displayedWords[0]}\"" : null),
            GetArrayField<string>(comp, "Stage2Char").Get(arr => !arr.Contains(displayedWords[1]) ? $"expected array to contain \"{displayedWords[1]}\"" : null),
            GetArrayField<string>(comp, "Stage3Words").Get(arr => !arr.Contains(displayedWords[2]) ? $"expected array to contain \"{displayedWords[2]}\"" : null)
        };
        addQuestions(module, Enumerable.Range(0, 3).Select(stage => makeQuestion(Question.KanjiDisplayedWords, module, formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { displayedWords[stage] }, preferredWrongAnswers: wordLists[stage])));
    }

    private IEnumerator<YieldInstruction> ProcessKanyeEncounter(ModuleData module)
    {
        var comp = GetComponent(module, "TheKanyeEncounter");

        var fldFoodsAvailable = GetArrayField<int>(comp, "FooderPickerNumberSelector");
        var foodNames = GetField<string[]>(comp, "FoodsButCodeText").Get();
        for (int i = 0; i < foodNames.Length; i++)
            if (foodNames[i] == "Corn [inedible]")
                foodNames[i] = "Corn";

        yield return WaitForSolve;

        var selectedFoods = fldFoodsAvailable.Get(expectedLength: 3);
        var selectedFoodNames = selectedFoods.Select(x => foodNames[x]).ToArray();
        addQuestion(module, Question.KanyeEncounterFoods, correctAnswers: selectedFoodNames);
    }

    private IEnumerator<YieldInstruction> ProcessKayMazeyTalk(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "KMazeyTalk");
        var valid = GetListField<int>(comp, "WeedKhungus").Get(expectedLength: 84, validator: i => i is < 16 or > 358 ? "Out of range [16, 358]" : null);
        var endIx = GetIntField(comp, "Wavecheck").Get(i => valid.Contains(i) ? null : "Unexpected end index");
        var startIx = GetIntField(comp, "BigIfSad").Get(i => valid.Contains(i) ? null : "Unexpected start index");

        var endPhrase = Question.KayMazeyTalkPhrase.GetAnswers()[valid.IndexOf(endIx)];
        var startPhrase = Question.KayMazeyTalkPhrase.GetAnswers()[valid.IndexOf(startIx)];

        addQuestions(module,
            makeQuestion(Question.KayMazeyTalkPhrase, module, correctAnswers: new[] { endPhrase }, preferredWrongAnswers: new[] { startPhrase }, formatArgs: new[] { "ending" }),
            makeQuestion(Question.KayMazeyTalkPhrase, module, correctAnswers: new[] { startPhrase }, preferredWrongAnswers: new[] { endPhrase }, formatArgs: new[] { "starting" }));
    }

    private IEnumerator<YieldInstruction> ProcessKeypadCombination(ModuleData module)
    {
        var comp = GetComponent(module, "KeypadCombinations");
        yield return WaitForSolve;

        var buttonNums = GetField<int[,]>(comp, "buttonnum").Get(v => v.GetLength(0) != 4 || v.GetLength(1) != 3 ? "expected 4×3 array" : null);
        var moduleAnswer = GetField<string>(comp, "answer").Get();

        addQuestions(module, Enumerable.Range(0, 4).Select(i => makeQuestion(Question.KeypadCombinationWrongNumbers, module,
            formatArgs: new[] { Ordinal(i + 1) },
            correctAnswers: Enumerable.Range(0, 3).Select(buttonIndex => buttonNums[i, buttonIndex])
                .Where(num => num != moduleAnswer[i] - '0').Select(num => num.ToString()).ToArray())));
    }

    private IEnumerator<YieldInstruction> ProcessKeypadMagnified(ModuleData module)
    {
        var comp = GetComponent(module, "KeypadMagnifiedScript");

        var LEDPos = GetIntField(comp, "chosenPosition").Get(min: 0, max: 3);
        yield return WaitForSolve;

        var posNames = new[] { "Top-left", "Top-right", "Bottom-left", "Bottom-right" };
        addQuestion(module, Question.KeypadMagnifiedLED, correctAnswers: new[] { posNames[LEDPos] });
    }

    private IEnumerator<YieldInstruction> ProcessKeypadMaze(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "KeypadMaze");
        var yellow = GetArrayField<int>(comp, "yellow", true).Get(expectedLength: 5, validator: v => v is < 0 or > 35 ? "Expected range 0–35" : null);

        addQuestion(module, Question.KeypadMazeYellow, correctAnswers: yellow.Take(4).Select(i => new Coord(6, 6, i)).ToArray());
    }

    private static Sprite[] _keypadSequenceSprites;
    private IEnumerator<YieldInstruction> ProcessKeypadSequence(ModuleData module)
    {
        var comp = GetComponent(module, "KeypadSeqScript");
        _keypadSequenceSprites ??= GetArrayField<Material>(comp, "symbols", true)
            .Get(expectedLength: 36).Select(m => ((Texture2D) m.mainTexture).Recolor().ToSprite()).ToArray();

        yield return WaitForSolve;

        var symbols = GetArrayField<int>(comp, "symbselect").Get(expectedLength: 16, validator: v => v is < 0 or > 35 ? "Expected range 0–35" : null);

        addQuestions(module, Enumerable.Range(0, 4).SelectMany(p =>
            symbols.Skip(4 * p).Take(4).Select((s, i) =>
                makeQuestion(Question.KeypadSequenceLabels, module,
                    correctAnswers: new[] { _keypadSequenceSprites[s] },
                    formatArgs: new[] { Ordinal(p + 1) },
                    questionSprite: Sprites.GenerateGridSprite(2, 2, i),
                    allAnswers: _keypadSequenceSprites))));
    }

    private IEnumerator<YieldInstruction> ProcessKeywords(ModuleData module)
    {
        var comp = GetComponent(module, "keywordsScript");

        yield return WaitForSolve;

        var consonants = "bcdfghjklmnpqrstvwxyz".ToCharArray();
        var vowels = "aeiou".ToCharArray();
        var displayedKey = GetField<string>(comp, "displayKey").Get(v => v.Length < 4 ? "expected length at least 4" : null).Substring(0, 4);
        if (displayedKey.Count(x => consonants.Contains(x)) != 2 || displayedKey.Count(x => vowels.Contains(x)) != 2)
            throw new AbandonModuleException($"‘displayKey’ had an unexpected value of “{displayedKey}” when I expected a string starting with two consonants and two vowels in any order.");

        var possibleAnswers = new HashSet<string>() { displayedKey };
        while (possibleAnswers.Count < 6)
            possibleAnswers.Add(consonants.Shuffle().Take(2).Concat(vowels.Shuffle().Take(2)).ToArray().Shuffle().JoinString());

        addQuestion(module, Question.KeywordsDisplayedKey, correctAnswers: new[] { displayedKey }, preferredWrongAnswers: possibleAnswers.ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessKlaxon(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "KlaxonScript");
        var letters = GetArrayField<char>(comp, "CorrectLetters").Get(minLength: 1, validator: c => c is < 'A' or > 'Z' ? "Expected uppercase letters" : null);

        var all = Bomb.GetSolvedModuleNames();
        all.Remove("The Klaxon");

        addQuestions(module, all.Distinct().Select(n =>
            makeQuestion(Question.KlaxonKlaxon, module, formatArgs: new[] { n }, correctAnswers: new[] { n.ToUpperInvariant().Contains(letters.First()) ? "Yes" : "No" })));
    }

    private IEnumerator<YieldInstruction> ProcessKnowYourWay(ModuleData module)
    {
        var comp = GetComponent(module, "KnowYourWayScript");

        yield return WaitForSolve;

        var ledIndex = GetIntField(comp, "LEDLoc").Get(min: 0, max: 3);
        var arrowIndex = GetIntField(comp, "ArrowLoc").Get(min: 0, max: 3);
        GetArrayField<GameObject>(comp, "Bars", isPublic: true)
            .Get(expectedLength: 4)[ledIndex]
            .GetComponent<MeshRenderer>().material = GetArrayField<Material>(comp, "LEDs", isPublic: true).Get(expectedLength: 2)[0];

        addQuestions(
            module,
            makeQuestion(Question.KnowYourWayArrow, module, correctAnswers: new[] { new[] { "Up", "Left", "Down", "Right" }[arrowIndex] }),
            makeQuestion(Question.KnowYourWayLed, module, correctAnswers: new[] { new[] { "Top", "Left", "Bottom", "Right" }[ledIndex] })
        );
    }

    private IEnumerator<YieldInstruction> ProcessKookyKeypad(ModuleData module)
    {
        yield return WaitForSolve;

        var colorMapping = new Dictionary<string, string>()
        {
            ["crimson"] = "Crimson",
            ["red"] = "Red",
            ["coral"] = "Coral",
            ["orange"] = "Orange",
            ["lemonchiffon"] = "Lemon Chiffon",
            ["mediumspringgreen"] = "Medium Spring Green",
            ["deepseagreen"] = "Deep Sea Green",
            ["cadetblue"] = "Cadet Blue",
            ["slateblue"] = "Slate Blue",
            ["darkmagenta"] = "Dark Magenta",
            ["unlit"] = "Unlit"
        };

        var comp = GetComponent(module, "KookyKeypadScript");
        var combos = GetArrayField<string[]>(comp, "colorcombos").Get(expectedLength: 15, validator: v => v.Length is not 4 ? "Expected length 4" : v.Any(i => !colorMapping.ContainsKey(i)) ? "Unexpected color" : null);
        var index = GetIntField(comp, "correctindex").Get(min: 0, max: 14);

        var formats = new[] { "top-left", "top-right", "bottom-left", "bottom-right" };
        addQuestions(module, combos[index].Select((c, i) =>
            makeQuestion(Question.KookyKeypadColor, module,
                correctAnswers: new[] { colorMapping[c] },
                formatArgs: new[] { formats[i] })));
    }

    private IEnumerator<YieldInstruction> ProcessKudosudoku(ModuleData module)
    {
        var comp = GetComponent(module, "KudosudokuModule");
        var shown = GetArrayField<bool>(comp, "_shown").Get(expectedLength: 16).ToArray();  // Take a copy of the array because the module changes it

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.KudosudokuPrefilled, module, formatArgs: new[] { "pre-filled" },
                correctAnswers: Enumerable.Range(0, 16).Where(ix => shown[ix]).Select(coord => new Coord(4, 4, coord)).ToArray()),
            makeQuestion(Question.KudosudokuPrefilled, module, formatArgs: new[] { "not pre-filled" },
                correctAnswers: Enumerable.Range(0, 16).Where(ix => !shown[ix]).Select(coord => new Coord(4, 4, coord)).ToArray()));
    }

    private readonly Dictionary<object, HashSet<int>> _kugelblitzUsedQuirks = new();
    private readonly List<HashSet<int>> _kugelblitzQuirksGroupings = new();
    private IEnumerator<YieldInstruction> ProcessKugelblitz(ModuleData module)
    {
        while (!_isActivated)
            yield return null;

        yield return null;
        yield return null; // The module takes this long to subscribe to a lobby

        if (module.IsSolved)
        {
            legitimatelyNoQuestion(module, "The module had too few stages to generate and autosolved.");
            yield break;
        }

        while (!_noUnignoredModulesLeft)
            yield return new WaitForSeconds(.1f);

        var comp = GetComponent(module, "KugelblitzScript");
        var lobby = GetField<object>(comp, "_lobby").Get();
        var linkSize = GetField<IList>(lobby, "_members").Get().Count;
        var quirks = GetField<IList>(lobby, "_quirks").Get().Cast<object>().ToArray();

        var quirkTypes = new[] { "BaseStageManager", "OffsetStageManager", "InvertStageManager", "InsertStageManager", "LengthStageManager", "TurnStageManager", "FlipStageManager", "WrapStageManager" };
        var orderedQuirks = Enumerable.Range(0, 8).Select(i => quirks.FirstOrDefault(q => q.GetType().Name == quirkTypes[i])).ToArray();
        var usedQuirks = new HashSet<int>(Enumerable.Range(0, 8).Where(i => quirks.Any(q => q.GetType().Name == quirkTypes[i])));


        if (!_kugelblitzUsedQuirks.TryGetValue(lobby, out HashSet<int> askedQuirks))
        {
            askedQuirks = _kugelblitzUsedQuirks[lobby] = new();
            _kugelblitzQuirksGroupings.Add(usedQuirks);
        }

        var KOYIV = new List<byte>[5];
        var RGB = new List<byte[]>[3];
        var indices = new int?[8];

        if (!usedQuirks.Contains(0))
            throw new AbandonModuleException("There was no black Kugelblitz in the lobby.");

        var normalQuirks = new[] { 0, 2, 3, 6, 7 };
        for (int q = 0; q < normalQuirks.Length; q++)
        {
            if (usedQuirks.Contains(normalQuirks[q]))
            {
                var stageObjects = GetField<IList>(orderedQuirks[normalQuirks[q]], "_stages").Get().Cast<object>();
                var fldData = GetArrayField<bool>(stageObjects.First(), "_data");
                KOYIV[q] = stageObjects.Select(s => (byte) fldData.GetFrom(s, expectedLength: 7).Select((b, i) => b ? 1 << i : 0).Sum()).ToList();
                indices[normalQuirks[q]] = GetIntField(orderedQuirks[normalQuirks[q]], "_index").Get(min: 0);
            }
        }

        if (usedQuirks.Contains(1))
        {
            var stageObjects = GetField<IList>(orderedQuirks[1], "_stages").Get().Cast<object>();
            var fldData = GetArrayField<byte>(stageObjects.First(), "_data");
            RGB[0] = stageObjects.Select(s => fldData.GetFrom(s, expectedLength: 7, validator: b => b is > 6 ? "Expected red data to be [0, 6]" : null)).ToList();
            indices[1] = GetIntField(orderedQuirks[1], "_index").Get(min: 0);
        }
        if (usedQuirks.Contains(4))
        {
            var stageObjects = GetField<IList>(orderedQuirks[4], "_stages").Get().Cast<object>();
            var fldData = GetArrayField<byte>(stageObjects.First(), "_data");
            RGB[1] = stageObjects.Select(s => fldData.GetFrom(s, expectedLength: 7, validator: b => b is > 6 ? "Expected green data to be [0, 6]" : null)).ToList();
            indices[4] = GetIntField(orderedQuirks[4], "_index").Get(min: 0);
        }
        if (usedQuirks.Contains(5))
        {
            var stageObjects = GetField<IList>(orderedQuirks[5], "_stages").Get().Cast<object>();
            var fldData = GetArrayField<byte>(stageObjects.First(), "_data");
            RGB[2] = stageObjects.Select(s => fldData.GetFrom(s, expectedLength: 7, validator: b => b is > 2 ? "Expected blue data to be [0, 2]" : null)).ToList();
            indices[5] = GetIntField(orderedQuirks[5], "_index").Get(min: 0);
        }

        if (indices.Skip(1).Any(x => x is not null && x != indices[0]))
            throw new AbandonModuleException("Two quirks disagreed on how many stages were shown.");

        yield return null;

        string constructStandardAnswer(int b)
        {
            var format = translateString(Question.KugelblitzBlackOrangeYellowIndigoViolet, "{0}{1}{2}{3}{4}{5}{6}");
            return string.Format(format, Enumerable.Range(0, 7).Select(i => (b & (1 << i)) != 0 ? translateString(Question.KugelblitzBlackOrangeYellowIndigoViolet, "ROYGBIV"[i].ToString()) : "").ToArray());
        }

        string constructRGBAnswer(byte[] b)
        {
            var format = translateString(Question.KugelblitzRedGreenBlue, "R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}");
            return string.Format(format, b[0], b[1], b[2], b[3], b[4], b[5], b[6]);
        }
        IEnumerable<string> allRGBAnswers(byte max)
        {
            List<string> options = new();

            for (int c = 0; c < 6; c++)
            {
                string answer;
                do
                {
                    var choice = new byte[7];
                    for (int i = 0; i < 7; i++)
                        choice[i] = (byte) UnityEngine.Random.Range(0, max + 1);
                    answer = constructRGBAnswer(choice);
                } while (options.Contains(answer));
                options.Add(answer);
            }

            return options;
        }

        var allStandardAnswers = Enumerable.Range(0, 128).Select(x => constructStandardAnswer(x)).ToArray();
        allStandardAnswers[0] = translateString(Question.KugelblitzBlackOrangeYellowIndigoViolet, "None");

        var quirkNames = new[] { "black", "red", "orange", "yellow", "green", "blue", "indigo", "violet" };
        string formatName(int color) => string.Format(translateString(Question.KugelblitzBlackOrangeYellowIndigoViolet, "the {0} Kugelblitz"), translateString(Question.KugelblitzBlackOrangeYellowIndigoViolet, quirkNames[color]));
        string[] templates = new[] { "the Kugelblitz linked with no other Kugelblitzes", "the {0} Kugelblitz linked with one other Kugelblitz", "the {0} Kugelblitz linked with two other Kugelblitzes", "the {0} Kugelblitz linked with three other Kugelblitzes", "the {0} Kugelblitz linked with four other Kugelblitzes", "the {0} Kugelblitz linked with five other Kugelblitzes", "the {0} Kugelblitz linked with six other Kugelblitzes", "the {0} Kugelblitz linked with seven other Kugelblitzes" };
        string formatNameSized(int color, int size) => string.Format(translateString(Question.KugelblitzBlackOrangeYellowIndigoViolet, templates[size - 1]), translateString(Question.KugelblitzBlackOrangeYellowIndigoViolet, quirkNames[color]));

        bool myFormat(int color, out string format)
        {
            if (_kugelblitzQuirksGroupings.Count(g => g.Contains(color)) is 1)
                format = formatName(color);
            else if (_kugelblitzQuirksGroupings.Where(g => g.Contains(color)).Count(g => g.Count == linkSize) is 1)
                format = formatNameSized(color, linkSize);
            else
            {
                legitimatelyNoQuestion(module, $"There are multiple lobbies with {linkSize} kugelblitzes and a(n) {quirkNames[color]} one, so I can't ask about them.");
                format = null;
                return false;
            }
            return true;
        }

        QandA[] qs;
        string format;
        if (!askedQuirks.Contains(0))
        {
            askedQuirks.Add(0);
            if (_kugelblitzQuirksGroupings.Count is 1 && _kugelblitzQuirksGroupings[0].Count is 1)
            {
                module.SolveIndex = 1;
                format = null;
            }
            else if (!myFormat(0, out format))
                yield break;

            qs = KOYIV[0].Select(b => allStandardAnswers[b]).Select((a, i) => makeQuestion(Question.KugelblitzBlackOrangeYellowIndigoViolet, module, formattedModuleName: format, correctAnswers: new[] { a }, formatArgs: new[] { Ordinal(i + 1) }, allAnswers: allStandardAnswers)).ToArray();
        }
        else if (new[] { 2, 3, 6, 7 }.Any(x => usedQuirks.Contains(x) && !askedQuirks.Contains(x)))
        {
            var q = new[] { 2, 3, 6, 7 }.First(x => usedQuirks.Contains(x) && !askedQuirks.Contains(x));
            askedQuirks.Add(q);
            var i = Array.IndexOf(new[] { 2, 3, 6, 7 }, q);

            if (!myFormat(q, out format))
                yield break;

            qs = KOYIV[i + 1].Select(b => allStandardAnswers[b]).Select((a, i) => makeQuestion(Question.KugelblitzBlackOrangeYellowIndigoViolet, module, formattedModuleName: format, correctAnswers: new[] { a }, formatArgs: new[] { Ordinal(i + 1) }, allAnswers: allStandardAnswers)).ToArray();
        }
        else if (new[] { 1, 4 }.Any(x => usedQuirks.Contains(x) && !askedQuirks.Contains(x)))
        {
            var q = new[] { 1, 4 }.First(x => usedQuirks.Contains(x) && !askedQuirks.Contains(x));
            askedQuirks.Add(q);
            var i = Array.IndexOf(new[] { 1, 4 }, q);

            if (!myFormat(q, out format))
                yield break;

            qs = RGB[i].Select(constructRGBAnswer).Select((a, i) => makeQuestion(Question.KugelblitzRedGreenBlue, module, formattedModuleName: format, correctAnswers: new[] { a }, formatArgs: new[] { Ordinal(i + 1) }, preferredWrongAnswers: allRGBAnswers(6).ToArray())).ToArray();
        }
        else if (usedQuirks.Contains(5) && !askedQuirks.Contains(5))
        {
            usedQuirks.Add(5);

            if (!myFormat(5, out format))
                yield break;

            qs = RGB[2].Select(constructRGBAnswer).Select((a, i) => makeQuestion(Question.KugelblitzRedGreenBlue, module, formattedModuleName: format, correctAnswers: new[] { a }, formatArgs: new[] { Ordinal(i + 1) }, preferredWrongAnswers: allRGBAnswers(2).ToArray())).ToArray();
        }
        else
            throw new AbandonModuleException("I somehow ran out of quirks.");

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessKuro(ModuleData module)
    {
        var comp = GetComponent(module, "Kuro");
        yield return WaitForSolve;

        var desiredTask = GetField<Enum>(comp, "desiredTask").Get().ToString();
        var moods = GetArrayField<Texture2D>(comp, "kuroMoods", isPublic: true).Get(expectedLength: 5).Select(texture => texture.name);

        if (desiredTask != "Eat" && desiredTask != "PlayKTANE")
        {
            legitimatelyNoQuestion(module.Module, "Mood is not relevant to the answer");
            yield break;
        }

        var currentMood = GetField<Enum>(comp, "currentMood").Get().ToString();
        addQuestion(module, Question.KuroMood, correctAnswers: new[] { currentMood }, preferredWrongAnswers: moods.ToArray());
    }
}
