using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private const string _version = "7.0";

    /* Generalized handlers for modules that are extremely similar */

    // Used by Speakingevil’s Cycle modules. question[0] is the dial rotations. question[1] is the dial labels.
    private IEnumerator<SouvenirInstruction> processSpeakingEvilCycle(ModuleData module, string componentName, TextQuestion rotQ, TextQuestion labelQ, Sprite[] overrideAnswers = null)
    {
        var comp = GetComponent(module, componentName);
        yield return WaitForSolve;

        var qs = new List<QandA>();

        var rotComp = GetArrayField<int[]>(comp, "rot").Get();
        var dialLabels = componentName switch
        {
            "UltimateCycleScript" => GetArrayField<string[]>(comp, "ciphertext").Get()[0][8],
            "JumbleCycleScript" => GetArrayField<string[]>(comp, "ciphertext").Get()[0][4],
            _ => GetArrayField<string>(comp, "ciphertext").Get()[0],
        };
        for (var dial = 0; dial < 8; dial++)
        {
            switch (componentName)
            {
                case "PlayfairCycleScript":
                case "HillCycleScript":
                    qs.Add(makeQuestion(rotQ, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { CycleModuleFiveSprites[rotComp[0][dial]] }, allAnswers: CycleModuleFiveSprites));
                    break;
                case "CrypticCycleScript":
                    qs.Add(makeQuestion(rotQ, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { CycleModuleCrypticSprites[rotComp[0][dial]] }, allAnswers: CycleModuleCrypticSprites));
                    break;
                default:
                    qs.Add(makeQuestion(rotQ, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { CycleModuleEightSprites[rotComp[0][dial]] }, allAnswers: overrideAnswers ?? CycleModuleEightSprites));
                    break;
            }
            qs.Add(makeQuestion(labelQ, module, formatArgs: new[] { Ordinal(dial + 1) }, correctAnswers: new[] { dialLabels[dial].ToString() }));
        }
        addQuestions(module, qs);
    }

    // Used by the World Mazes modules (currently: USA Maze, DACH Maze)
    private IEnumerator<SouvenirInstruction> processWorldMaze(ModuleData module, string script, TextQuestion question)
    {
        var comp = GetComponent(module, script);
        var fldOrigin = GetField<string>(comp, "_originState");
        var mthGetStates = GetMethod<List<string>>(comp, "GetAllStates", 0);
        var mthGetName = GetMethod<string>(comp, "GetStateFullName", 1);

        yield return WaitForSolve;

        var stateCodes = mthGetStates.Invoke() ?? throw new AbandonModuleException("GetAllStates() returned null.");
        if (stateCodes.Count == 0)
            throw new AbandonModuleException("GetAllStates() returned an empty list.");

        var states = stateCodes.Select(code => mthGetName.Invoke(code)).ToArray();
        var origin = mthGetName.Invoke(fldOrigin.Get());
        if (!states.Contains(origin))
            throw new AbandonModuleException($"‘_originState’ was not contained in the list of all states (“{origin}” not in: [{states.JoinString(", ")}]).");

        addQuestion(module, question, correctAnswers: new[] { origin }, preferredWrongAnswers: states);
    }

    // Used by Black, Blue, Brown, Coral, Cornflower, Cream, Crimson, Forest, Gray, Green, Indigo, Magenta, Maroon, Orange, Red, Violet, White, Yellow, and Ultimate Cipher
    private IEnumerator<SouvenirInstruction> processColoredCiphers(ModuleData module, string componentName, TextQuestion question)
    {
        var comp = GetComponent(module, componentName);
        yield return WaitForSolve;

        var pages = GetField<IList>(comp, "pages").Get(v => v.Count == 0 ? "expected at least one page" : null);
        var fldScreens = GetProperty<IList>(pages[0], "Screens", isPublic: true);
        var fldText = GetProperty<string>(fldScreens.Get(v => v.Count == 0 ? "expected at least one screen per page" : null)[0], "Text", isPublic: true);
        var fldAvoid = GetProperty<bool>(fldScreens.Get(v => v.Count == 0 ? "expected at least one screen per page" : null)[0], "SouvenirAvoid", isPublic: true);

        var allWordsType = comp.GetType().Assembly.GetType("Words.Data") ?? throw new AbandonModuleException("I cannot find the Words.Data type.");
        var allWordsObj = Activator.CreateInstance(allWordsType);
        var allWords = GetArrayField<List<string>>(allWordsObj, "_allWords").Get(expectedLength: 5);

        string[] generateWrongAnswers(string correctAnswer, AnswerGeneratorAttribute<string> gen)
        {
            var set = new HashSet<string> { correctAnswer };
            var iter = 0;
            while (set.Count < 6 && iter < 100)
            {
                foreach (var ans in gen.GetAnswers(this).Take(6 - set.Count))
                    set.Add(ans);
                iter++;
            }
            return set.ToArray();
        }

        string[] generateWrongAnswersFnc(string correctAnswer, Func<string> gen)
        {
            var set = new HashSet<string> { correctAnswer };
            var iter = 0;
            while (set.Count < 6 && iter < 100)
            {
                set.Add(gen());
                iter++;
            }
            return set.ToArray();
        }

        var screenNames = new[] { "top", "middle", "bottom" };
        var romanNumerals = new[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII" };
        addQuestions(module, Enumerable.Range(0, pages.Count).SelectMany(pageIx =>
        {
            var screenObjs = fldScreens.GetFrom(pages[pageIx], v => v.Count == 0 ? "expected at least one screen per page" : null);
            var screenTexts = Enumerable.Range(0, screenObjs.Count).Select(scrIx => (page: pageIx, screen: scrIx, text: fldText.GetFrom(screenObjs[scrIx], nullAllowed: true), avoid: fldAvoid.GetFrom(screenObjs[scrIx])));
            return screenTexts.Where(tup => !tup.avoid && !string.IsNullOrEmpty(tup.text)).Select(tup =>
            {
                // Black Cipher special case: A-VII-IV-V
                var rom = romanNumerals.JoinString("|");
                if (Regex.IsMatch(tup.text, $@"^[ABC]-({rom})-({rom})-({rom})$"))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{"ABC"[Rnd.Range(0, 3)]}-{romanNumerals.ToArray().Shuffle().Take(3).JoinString("-")}"));

                // Black Cipher special case: NJ-SG-CV
                if (Regex.IsMatch(tup.text, @"^[A-Z]{2}(-[A-Z]{2})+$"))
                {
                    var n = (tup.text.Length + 1) / 3;
                    string gen()
                    {
                        var shuffle = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().Shuffle().Take(2 * n).JoinString();
                        for (var i = n - 1; i >= 1; i--)
                            shuffle = shuffle.Insert(2 * i, "-");
                        return shuffle;
                    }
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, gen));
                }

                // Brown Cipher page 2 screen 3 will only have letters A to F
                if (Regex.IsMatch(tup.text, @"^[A-F]+$"))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, 'A', 'F')));

                // Cornflower Cipher special case: three letters and a digit
                if (Regex.IsMatch(tup.text, @"^[A-Z]{3} \d$"))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]}{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]}{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]} {Rnd.Range(0, 10)}"));

                // Indigo Cipher special case: 24 ? 52 = 12
                if (Regex.IsMatch(tup.text, @"^\d+ \? \d+ = \d+$"))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{Rnd.Range(0, 64)} ? {Rnd.Range(0, 64)} = {Rnd.Range(0, 64)}"));

                // Yellow Cipher special case: 8-5-7-20
                if (Regex.IsMatch(tup.text, @"^\d+-\d+-\d+-\d+$"))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswersFnc(tup.text, () => $"{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}"));

                // Screens that have a word on them: pick other words of the same length as wrong answers
                if (tup.text.Length >= 4 && tup.text.Length <= 8 && allWords[tup.text.Length - 4].Contains(tup.text))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: allWords[tup.text.Length - 4].ToArray());

                // Screens that have only 0s and 1s on them
                if (tup.text.Length >= 3 && tup.text.All(ch => ch is '0' or '1'))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, '0', '1')));

                // Screens that have only digits on them
                if (tup.text.All(ch => ch is >= '0' and <= '9'))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, '0', '9')));

                // Screens that have only capital letters on them
                if (tup.text.All(ch => ch is >= 'A' and <= 'Z'))
                    return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                        preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, 'A', 'Z')));

                // All other cases: jumble of letters and digits
                return makeQuestion(question, module, formatArgs: new[] { screenNames[tup.screen], (tup.page + 1).ToString() }, correctAnswers: new[] { tup.text },
                    preferredWrongAnswers: generateWrongAnswers(tup.text, new AnswerGenerator.Strings(tup.text.Length, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")));
            });
        }));
    }

    // Used by The Hypercube and The Ultracube
    private IEnumerator<SouvenirInstruction> processHypercubeUltracube(ModuleData module, string componentName, TextQuestion question)
    {
        var comp = GetComponent(module, componentName);
        var rotations = GetStaticField<string[]>(comp.GetType(), "_rotationNames").Get();
        var sequence = GetArrayField<int>(comp, "_rotations").Get(expectedLength: 5, validator: rot => rot < 0 || rot >= rotations.Length ? $"expected range 0–{rotations.Length - 1}" : null);

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(question, module, formatArgs: new[] { "first" }, correctAnswers: new[] { rotations[sequence[0]] }),
            makeQuestion(question, module, formatArgs: new[] { "second" }, correctAnswers: new[] { rotations[sequence[1]] }),
            makeQuestion(question, module, formatArgs: new[] { "third" }, correctAnswers: new[] { rotations[sequence[2]] }),
            makeQuestion(question, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { rotations[sequence[3]] }),
            makeQuestion(question, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { rotations[sequence[4]] }));
    }

    // Used by Triamonds and Tetriamonds
    private IEnumerator<SouvenirInstruction> processPolyiamonds(ModuleData module, string componentName, TextQuestion question, string[] colourNames)
    {
        var comp = GetComponent(module, componentName);
        yield return WaitForSolve;

        var posColour = GetField<int[]>(comp, "poscolour").Get();
        var pulsing = GetField<int[]>(comp, "pulsing").Get();

        var qs = new List<QandA>();
        for (var pos = 0; pos < 3; pos++)
            qs.Add(makeQuestion(question, module, formatArgs: new[] { Ordinal(pos + 1) }, correctAnswers: new[] { colourNames[posColour[pulsing[pos]]] }));
        addQuestions(module, qs);
    }

    // Used by 64 & 21
    private IEnumerator<SouvenirInstruction> process6421(ModuleData module, string className, string fieldName, string alphabet, int radix, int min, int max, TextQuestion question)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, className);
        var displayedNumber = GetField<string>(comp, fieldName).Get(num => num.Length == 0 || num.Length > 4 || num.Any(c => !alphabet.Contains(c)) ? $"expected 1-4 base-{radix} digits" : null);
        var mthConvertToBase = GetStaticMethod<string>(comp.GetType(), "DecimalToArbitrarySystem", 2, isPublic: true);
        var answers = new HashSet<string> { displayedNumber };

        foreach (var button in GetArrayField<KMSelectable>(comp, "buttons", true).Get(expectedLength: 2))
        {
            button.OnInteract = null;
            button.OnInteractEnded = null;
        }

        while (answers.Count < 6)
            answers.Add(mthConvertToBase.Invoke(Rnd.Range(min, max), radix));
        addQuestion(module, question, correctAnswers: new[] { displayedNumber }, preferredWrongAnswers: answers.ToArray());
    }
}
