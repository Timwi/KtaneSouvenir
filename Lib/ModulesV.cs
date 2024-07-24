using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessV(KMBombModule module)
    {
        var comp = GetComponent(module, "qkV");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_V);

        var allWords = GetArrayField<string>(comp, "allWords").Get();
        var currentWords = GetField<List<string>>(comp, "currentWords").Get();

        addQuestions(module,
           makeQuestion(Question.VWords, _V, formatArgs: new[] { "was" }, correctAnswers: currentWords.ToArray(), preferredWrongAnswers: allWords),
           makeQuestion(Question.VWords, _V, formatArgs: new[] { "was not" }, correctAnswers: allWords.Where(a => !currentWords.Contains(a)).ToArray(), preferredWrongAnswers: allWords));
    }

    private IEnumerator<YieldInstruction> ProcessValves(KMBombModule module)
    {
        var comp = GetComponent(module, "Valves");
        var fldSolved = GetField<bool>(comp, "bombSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Valves);

        if (ValvesSprites.Length != 8)
            throw new AbandonModuleException($"Valves should have 8 sprites. Counted {ValvesSprites.Length}");

        var valvesColorNums = GetArrayField<int>(comp, "valvesColorNum").Get(expectedLength: 3, validator: val => val != 0 && val != 1 ? "expected 0 or 1" : null);
        var spriteIx = valvesColorNums.Aggregate(0, (p, n) => (p << 1) | (n ^ 1));
        addQuestion(module, Question.ValvesInitialState, correctAnswers: new[] { ValvesSprites[spriteIx] });
    }

    private IEnumerator<YieldInstruction> ProcessVaricoloredSquares(KMBombModule module)
    {
        var comp = GetComponent(module, "VaricoloredSquaresModule");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_VaricoloredSquares);

        addQuestion(module, Question.VaricoloredSquaresInitialColor, correctAnswers: new[] { GetField<object>(comp, "_firstStageColor").Get().ToString() });
    }

    private IEnumerator<YieldInstruction> ProcessVaricolourFlash(KMBombModule module)
    {
        var comp = GetComponent(module, "VCFScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldStage = GetIntField(comp, "stage");
        var fldGoal = GetArrayField<int>(comp, "sequence");

        var words = new int[4];
        var colors = new int[4];
        var names = new[] { "Red", "Green", "Blue", "Magenta", "Yellow", "White" };
        while (!fldSolved.Get())
        {
            int s = fldStage.Get(min: 0, max: 5);
            if (s < 4)
            {
                var goal = fldGoal.Get(expectedLength: 5)[4];
                if (goal < 0 || goal >= 36)
                    throw new AbandonModuleException($"‘sequence[4]’ has value {goal} (expected 0–35).");
                words[s] = goal / 6;
                colors[s] = goal % 6;
            }
            yield return new WaitForSeconds(0.1f);
        }

        _modulesSolved.IncSafe(_VaricolourFlash);

        var qs = new List<QandA>();
        qs.AddRange(words.Select((val, ix) => makeQuestion(Question.VaricolourFlashWords, _VaricolourFlash, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { names[val] })));
        qs.AddRange(colors.Select((val, ix) => makeQuestion(Question.VaricolourFlashColors, _VaricolourFlash, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { names[val] })));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessVariety(KMBombModule module)
    {
        var comp = GetComponent(module, "VarietyModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Variety);

        var items = GetField<IEnumerable>(comp, "_items").Get().Cast<object>().ToArray();
        if (items.Length != 10)
            throw new AbandonModuleException($"Expected 10 items, found {items.Length}.");
        var itemTypes = items.Select(i => i.GetType().Name).ToArray();

        List<QandA> questions = new();
        bool disableSelectables = false;

        if (Array.IndexOf(itemTypes, "Led") is var i and not -1)
        {
            var led = items[i];
            var c1 = GetProperty<object>(led, "Color1", isPublic: true).Get(null, v => (int) v is < 0 or > 4 ? $"Unknown LED color {v}" : null).ToString();
            var c2 = GetProperty<object>(led, "Color2", isPublic: true).Get(null, v => (int) v is < 0 or > 4 ? $"Unknown LED color {v}" : null).ToString();
            questions.Add(makeQuestion(Question.VarietyLED, _Variety, correctAnswers: new[] { c1, c2 }));
            disableSelectables = true;
        }

        if (Array.IndexOf(itemTypes, "DigitDisplay") is var j and not -1)
        {
            var display = items[j];
            var amount = GetField<int>(display, "_numStates").Get(v => v is < 1 or > 9 ? $"Bad number of digit display states {v}" : null);
            if (amount == 1)
                Debug.Log($"<Souvenir #{_moduleId}> Variety: Not asking about the digit display because there was only one valid digit.");
            else
            {
                var displays = GetArrayField<int>(display, "_displayedDigitPerState").Get(expectedLength: 9);
                var solution = GetProperty<int>(display, "State", isPublic: true).Get(v => v is < 0 || v >= amount ? $"Bad digit display solution state {v}" : null);
                List<string> ans = new();
                for (int ix = 0; ix < amount; ix++)
                    if (ix != solution)
                        ans.Add(displays[ix].ToString());
                questions.Add(makeQuestion(Question.VarietyDigitDisplay, _Variety, correctAnswers: ans.ToArray(), preferredWrongAnswers: new[] { displays[solution].ToString() }));
                disableSelectables = true;
            }
        }

        if (Array.IndexOf(itemTypes, "LetterDisplay") is var k and not -1)
        {
            var display = items[k];
            // Actually a property, but this method handles that
            var words = GetArrayField<string>(display, "FormableWords", isPublic: true).Get();
            if (words.Length == 1)
                Debug.Log($"<Souvenir #{_moduleId}> Variety: Not asking about the letter display because there was only one valid word.");
            else
            {
                var solution = GetProperty<int>(display, "State", isPublic: true).Get(v => v is < 0 || v >= words.Length ? $"Bad letter display solution state {v}" : null);
                questions.Add(makeQuestion(Question.VarietyLetterDisplay, _Variety, correctAnswers: words.Where((_, i) => i != solution).ToArray(), preferredWrongAnswers: new[] { words[solution] }));
                disableSelectables = true;
            }
        }

        if (Array.IndexOf(itemTypes, "Timer") is var l and not -1)
        {
            object[] timers;
            if (Array.IndexOf(itemTypes, "Timer", l + 1) is var x and not -1)
                timers = new[] { items[l], items[x] };
            else
                timers = new[] { items[l] };

            var data = timers.Select(timer =>
            {
                var a = GetField<int>(timer, "_a").Get(v => v is not 2 and not 3 and not 5 and not 7 ? $"Unknown timer A value {v}" : null);
                var b = GetField<int>(timer, "_b").Get(v => v is not 2 and not 3 and not 5 || a is 3 && v is 5 || a is 5 && v is > 2 || a is 7 && v is not 2 ? $"Unknown timer B value {v}" : null);
                var flavor = GetProperty<object>(timer, "FlavorType", isPublic: true).Get(null, validator: v => v is < 0 or > 1 ? $"Unknown timer flavor {v}" : null);
                return (A: a, B: b, Flavor: flavor);
            }).ToArray();
            if (data.Length == 1)
                questions.Add(makeQuestion(Question.VarietyTimer, _Variety, formatArgs: new[] { "" }, correctAnswers: new[] { $"{data[0].A - 1} {data[0].B - 1}" }));
            else
                questions.AddRange(new[] {
                    makeQuestion(Question.VarietyTimer, _Variety, formatArgs: new[] { data[0].Flavor.ToString().ToLowerInvariant() + " " }, correctAnswers: new[] { $"{data[0].A - 1} {data[0].B - 1}" }),
                    makeQuestion(Question.VarietyTimer, _Variety, formatArgs: new[] { data[1].Flavor.ToString().ToLowerInvariant() + " " }, correctAnswers: new[] { $"{data[1].A - 1} {data[1].B - 1}" })
                });
            disableSelectables = true;
        }

        var cknobs = itemTypes.Select((t, i) => (t, i)).Where(tup => tup.t == "ColoredKnob").Select(tup => tup.i).ToArray();
        if (cknobs.Length != 0)
        {
            string format;
            if (cknobs.Length == 1 && itemTypes.Contains("Knob"))
                format = "colored ";
            else if (cknobs.Length == 1)
                format = "";
            else
                format = null;

            foreach (var knob in cknobs.Select(i => items[i]))
            {
                var ans = GetProperty<int>(knob, "NumStates", isPublic: true).Get(null, v => v is < 3 or > 6 ? $"Bad colored knob state count {v}" : null);
                var flavor = GetProperty<object>(knob, "Color", isPublic: true).Get(null, v => (int) v is < 0 or > 3 ? $"Unknown knob color {v}" : null).ToString();
                questions.Add(makeQuestion(Question.VarietyColoredKnob, _Variety, formatArgs: new[] { format ?? flavor.ToLowerInvariant().Substring(0, flavor.Length - 4) + " " }, correctAnswers: new[] { ans.ToString() }));
            }
            disableSelectables = true;
        }

        var bulbs = itemTypes.Select((t, i) => (t, i)).Where(tup => tup.t == "Bulb").Select(tup => tup.i).ToArray();
        if (bulbs.Length != 0)
        {
            string format = bulbs.Length == 1 ? "" : null;

            foreach (var bulb in bulbs.Select(ix => items[ix]))
            {
                var ans = GetProperty<int>(bulb, "N", isPublic: true).Get(null, v => v is < 5 or > 13 ? $"Unknown bulb N {v}" : null);
                var flavor = GetProperty<object>(bulb, "Color", isPublic: true).Get(null, v => (int) v is < 0 or > 1 ? $"Unknown bulb color {v}" : null).ToString();
                questions.Add(makeQuestion(Question.VarietyBulb, _Variety, formatArgs: new[] { format ?? flavor.Substring(0, flavor.Length - 4).ToLowerInvariant() + " " }, correctAnswers: new[] { ans.ToString() }));
            }
            disableSelectables = true;
        }

        if (questions.Count == 0)
        {
            legitimatelyNoQuestion(module, "There were no relevant components (or they were not possible to ask about).");
            yield break;
        }

        if (disableSelectables)
        {
            var sel = GetField<KMSelectable>(comp, "ModuleSelectable", isPublic: true).Get();
            foreach (var c in sel.Children)
            {
                if (c is not null)
                {
                    c.OnInteract = () => false;
                    c.OnInteractEnded = () => { };
                }
            }
        }

        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessVcrcs(KMBombModule module)
    {
        var comp = GetComponent(module, "VcrcsScript");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");

        var wordTextMesh = GetField<TextMesh>(comp, "Words", isPublic: true).Get();
        var word = wordTextMesh.text;
        if (word == null)
            throw new AbandonModuleException("‘Words.text’ is null.");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Vcrcs);

        addQuestion(module, Question.VcrcsWord, correctAnswers: new[] { word });
    }

    private IEnumerator<YieldInstruction> ProcessVectors(KMBombModule module)
    {
        var comp = GetComponent(module, "VectorsScript");

        // After moduleSolved is set to true, the module still performs an animation before it actually marks as solved.
        // Therefore, we use OnPass to wait for it to be solved
        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Vectors);

        var colorsName = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple" };
        var vectorCount = GetIntField(comp, "vectorct").Get(min: 1, max: 3);
        var colors = GetArrayField<string>(comp, "colors").Get(expectedLength: 24, nullContentAllowed: true);
        var pickedVectors = GetArrayField<int>(comp, "vectorsPicked").Get(expectedLength: 3, validator: v => v < 0 || v >= colors.Length ? $"expected range 0–{colors.Length - 1}" : null);
        var nullIx = pickedVectors.Take(vectorCount).IndexOf(ix => colors[ix] == null);
        if (nullIx != -1)
            throw new AbandonModuleException($"‘colors[{pickedVectors[nullIx]}]’ was null; ‘pickedVectors’ = [{pickedVectors.JoinString(", ")}]");

        for (int i = 0; i < vectorCount; i++)
            if (!colorsName.Contains(colors[pickedVectors[i]]))
                throw new AbandonModuleException($"‘colors[{pickedVectors[i]}]’ pointed to illegal color “{colors[pickedVectors[i]]}” (colors=[{colors.JoinString(", ")}], pickedVectors=[{pickedVectors.JoinString(", ")}], index {i}).");

        var qs = new List<QandA>();
        for (int i = 0; i < vectorCount; i++)
            qs.Add(makeQuestion(Question.VectorsColors, _Vectors, formatArgs: new[] { vectorCount == 1 ? "only" : ordinal(i + 1) }, correctAnswers: new[] { colors[pickedVectors[i]] }));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessVexillology(KMBombModule module)
    {
        var comp = GetComponent(module, "vexillologyScript");
        var fldSolved = GetField<bool>(comp, "_issolved");

        string[] colors = GetArrayField<string>(comp, "coloursStrings").Get();
        int color1 = GetIntField(comp, "ActiveFlagTop1").Get(min: 0, max: colors.Length - 1);
        int color2 = GetIntField(comp, "ActiveFlagTop2").Get(min: 0, max: colors.Length - 1);
        int color3 = GetIntField(comp, "ActiveFlagTop3").Get(min: 0, max: colors.Length - 1);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Vexillology);

        addQuestions(module,
            makeQuestion(Question.VexillologyColors, _Vexillology, formatArgs: new[] { "first" }, correctAnswers: new[] { colors[color1] }, preferredWrongAnswers: new[] { colors[color2], colors[color3] }),
            makeQuestion(Question.VexillologyColors, _Vexillology, formatArgs: new[] { "second" }, correctAnswers: new[] { colors[color2] }, preferredWrongAnswers: new[] { colors[color1], colors[color3] }),
            makeQuestion(Question.VexillologyColors, _Vexillology, formatArgs: new[] { "third" }, correctAnswers: new[] { colors[color3] }, preferredWrongAnswers: new[] { colors[color2], colors[color1] }));
    }

    private IEnumerator<YieldInstruction> ProcessVioletCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "violetCipher", Question.VioletCipherScreen, _VioletCipher);
    }

    private IEnumerator<YieldInstruction> ProcessVisualImpairment(KMBombModule module)
    {
        var comp = GetComponent(module, "VisualImpairment");
        var fldRoundsFinished = GetIntField(comp, "roundsFinished");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldColor = GetIntField(comp, "color");
        var fldPicture = GetArrayField<string>(comp, "picture");

        // Wait for the first picture to be assigned
        while (fldPicture.Get(nullAllowed: true) == null)
            yield return new WaitForSeconds(.1f);

        var stageCount = GetIntField(comp, "stageCount").Get(min: 2, max: 3);
        var colorsPerStage = new int[stageCount];
        var colorNames = new[] { "Blue", "Green", "Red", "White" };

        while (!fldSolved.Get())
        {
            var newStage = fldRoundsFinished.Get();
            if (newStage >= stageCount)
                break;

            var newColor = fldColor.Get(min: 0, max: 3);
            colorsPerStage[newStage] = newColor;
            yield return new WaitForSeconds(.1f);
        }
        _modulesSolved.IncSafe(_VisualImpairment);

        addQuestions(module, colorsPerStage.Select((col, ix) => makeQuestion(Question.VisualImpairmentColors, _VisualImpairment, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colorNames[col] })));
    }
}
