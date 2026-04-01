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
    /* Generalized handlers for modules that are extremely similar */

    // Used by Speakingevil’s Cycle modules
    private IEnumerator<SouvenirInstruction> processSpeakingEvilCycle(ModuleData module, string componentName,
        Enum rotQ, Enum labelQ, Enum labelDiscriminator, Func<Component, string> getDialLabels = null, Sprite[] answerSprites = null, Sprite[] all = null)
    {
        var comp = GetComponent(module, componentName);
        yield return WaitForSolve;

        var rotComp = GetArrayField<int[]>(comp, "rot").Get(expectedLength: 2, validator: arr => arr.Length != 8 ? "expected length 8" : null);
        var dialLabels = getDialLabels == null ? GetArrayField<string>(comp, "ciphertext").Get()[0] : getDialLabels(comp);

        for (var dial = 0; dial < 8; dial++)
        {
            yield return question(rotQ, args: [Ordinal(dial + 1)]).Answers((answerSprites ?? CycleModuleEightSprites)[rotComp[0][dial]], all: all ?? CycleModuleEightSprites);
            yield return question(labelQ, args: [Ordinal(dial + 1)]).AvoidDiscriminators(labelDiscriminator).Answers(dialLabels[dial].ToString());
        }
        if (labelDiscriminator != null)
            foreach (var ltr in dialLabels.Distinct())
                yield return new Discriminator(labelDiscriminator, $"ltr-{ltr}", args: [ltr.ToString()]);
    }

    // Used by the World Mazes modules (currently: USA Maze, DACH Maze)
    private IEnumerator<SouvenirInstruction> processWorldMaze(ModuleData module, string script, Enum q)
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

        yield return question(q).Answers(origin, preferredWrong: states);
    }

    // Used by Black, Blue, Brown, Coral, Cornflower, Cream, Crimson, Forest, Gray, Green, Indigo, Magenta, Maroon, Orange, Red, Violet, White, Yellow, and Ultimate Cipher
    private IEnumerator<SouvenirInstruction> processColoredCiphers(ModuleData module, string componentName, Enum q)
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

        var pagesList = new List<(int page, int screen, string text)>();
        for (var pageIx = 0; pageIx < pages.Count; pageIx++)
        {
            var screenObjs = fldScreens.GetFrom(pages[pageIx], v => v.Count == 0 ? "expected at least one screen per page" : null);
            for (var scrIx = 0; scrIx < screenObjs.Count; scrIx++)
                if (!fldAvoid.GetFrom(screenObjs[scrIx]) && fldText.GetFrom(screenObjs[scrIx], nullAllowed: true) is { } text && !string.IsNullOrEmpty(text))
                    pagesList.Add((pageIx, scrIx, text));
        }

        foreach (var obj in processColoredOrCompressionCiphers(q, pagesList, allWords))
            yield return obj;
    }

    // Used by Arithmetic Cipher, Blue Huffman Cipher, Lempel-Ziv Cipher, Pokémon Sprite Cipher and Yellow Huffman Cipher
    private IEnumerator<SouvenirInstruction> processCompressionCiphers(ModuleData module, string componentName, Enum q)
    {
        var comp = GetComponent(module, componentName);
        yield return WaitForSolve;

        var pages = GetArrayField<string[]>(comp, "_pages").Get(v => v.Length != 2 ? "expected exactly 2 pages" : null);
        var allWordsType = comp.GetType().Assembly.GetType("CompressionCiphersLib.Data") ?? throw new AbandonModuleException("I cannot find the CompressionCiphersLib.Data type.");
        var allWordsObj = Activator.CreateInstance(allWordsType);
        var allWords = GetField<List<string>[]>(allWordsObj, "allWords").Get();

        var pagesList = new List<(int page, int screen, string text)>();
        for (var pageIx = 0; pageIx < pages.Length; pageIx++)
            for (var scrIx = 0; scrIx < pages[pageIx].Length; scrIx++)
                if (!string.IsNullOrEmpty(pages[pageIx][scrIx]))
                    pagesList.Add((pageIx, scrIx, pages[pageIx][scrIx]));

        foreach (var obj in processColoredOrCompressionCiphers(q, pagesList, allWords))
            yield return obj;
    }

    // Used by all colored ciphers, Ultimate Cipher, and the compression ciphers
    private IEnumerable<SouvenirInstruction> processColoredOrCompressionCiphers(Enum q, IEnumerable<(int page, int screen, string text)> screenTexts, List<string>[] wordLists)
    {
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

        foreach (var (page, screen, text) in screenTexts)
        {
            // Black Cipher special case: A-VII-IV-V
            var rom = romanNumerals.JoinString("|");
            if (Regex.IsMatch(text, $@"^[ABC]-({rom})-({rom})-({rom})$"))
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswersFnc(text, () => $"{"ABC"[Rnd.Range(0, 3)]}-{romanNumerals.ToArray().Shuffle().Take(3).JoinString("-")}"));

            // Black Cipher special case: NJ-SG-CV
            else if (Regex.IsMatch(text, @"^[A-Z]{2}(-[A-Z]{2})+$"))
            {
                var n = (text.Length + 1) / 3;
                string gen()
                {
                    var shuffle = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray().Shuffle().Take(2 * n).JoinString();
                    for (var i = n - 1; i >= 1; i--)
                        shuffle = shuffle.Insert(2 * i, "-");
                    return shuffle;
                }
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()]).Answers(text, preferredWrong: generateWrongAnswersFnc(text, gen));
            }

            // Brown Cipher page 2 screen 3 will only have letters A to F
            else if (Regex.IsMatch(text, @"^[A-F]+$"))
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswers(text, new AnswerGenerator.Strings(text.Length, 'A', 'F')));

            // Cornflower Cipher special case: only letters A–P
            else if (Regex.IsMatch(text, @"^[A-P]{6}$"))
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswers(text, new AnswerGenerator.Strings(text.Length, 'A', 'P')));

            // Cornflower Cipher special case: three letters A–P and a digit
            else if (Regex.IsMatch(text, @"^[A-P]{3} \d$"))
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswersFnc(text, () => $"{"ABCDEFGHIJKLMNOP"[Rnd.Range(0, 16)]}{"ABCDEFGHIJKLMNOP"[Rnd.Range(0, 16)]}{"ABCDEFGHIJKLMNOP"[Rnd.Range(0, 16)]} {Rnd.Range(0, 10)}"));

            // Arithmetic Cipher special case: three letters and a 2- or 3-digit number
            else if (Regex.IsMatch(text, @"^[A-Z]{3} \d{2,3}$"))
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswersFnc(text, () => $"{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]}{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]}{"ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Rnd.Range(0, 26)]} {Rnd.Range(27, 26 * 27 + 1)}"));

            // Indigo Cipher special case: 24 ? 52 = 12
            else if (Regex.IsMatch(text, @"^\d+ \? \d+ = \d+$"))
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswersFnc(text, () => $"{Rnd.Range(0, 64)} ? {Rnd.Range(0, 64)} = {Rnd.Range(0, 64)}"));

            // Yellow Cipher special case: 8-5-7-20
            else if (Regex.IsMatch(text, @"^\d+-\d+-\d+-\d+$"))
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswersFnc(text, () => $"{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}-{Rnd.Range(0, 26)}"));

            // Screens that have a word on them: pick other words from the same wordlist as wrong answers
            else if (text.Length is >= 4 and <= 8 && wordLists.IndexOf(wl => wl.Contains(text)) is { } wlp and not -1)
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: wordLists[wlp].ToArray());

            // Screens that have only 0s and 1s on them
            else if (text.Length >= 3 && text.All(ch => ch is '0' or '1'))
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswers(text, new AnswerGenerator.Strings(text.Length, '0', '1')));

            // Screens that have only digits on them
            else if (text.All(ch => ch is >= '0' and <= '9'))
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswers(text, new AnswerGenerator.Strings(text.Length, '0', '9')));

            // Screens that have only capital letters on them
            else if (text.All(ch => ch is >= 'A' and <= 'Z'))
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswers(text, new AnswerGenerator.Strings(text.Length, 'A', 'Z')));

            // All other cases: jumble of letters and digits
            else
                yield return question(q, args: [screenNames[screen], (page + 1).ToString()])
                    .Answers(text, preferredWrong: generateWrongAnswers(text, new AnswerGenerator.Strings(text.Length, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")));
        }
    }

    // Used by The Hypercube and The Ultracube
    private IEnumerator<SouvenirInstruction> processHypercubeUltracube(ModuleData module, string componentName, Enum q)
    {
        var comp = GetComponent(module, componentName);
        var rotations = GetStaticField<string[]>(comp.GetType(), "_rotationNames").Get();
        var sequence = GetArrayField<int>(comp, "_rotations").Get(expectedLength: 5, validator: rot => rot < 0 || rot >= rotations.Length ? $"expected range 0–{rotations.Length - 1}" : null);

        yield return WaitForSolve;

        for (var i = 0; i < 5; i++)
            yield return question(q, args: [Ordinal(i + 1)]).Answers(rotations[sequence[i]]);
    }

    // Used by Triamonds and Tetriamonds
    private IEnumerator<SouvenirInstruction> processPolyiamonds(ModuleData module, string componentName, Enum q, string[] colourNames)
    {
        var comp = GetComponent(module, componentName);
        yield return WaitForSolve;

        var posColour = GetField<int[]>(comp, "poscolour").Get();
        var pulsing = GetField<int[]>(comp, "pulsing").Get();
        for (var pos = 0; pos < 3; pos++)
            yield return question(q, args: [Ordinal(pos + 1)]).Answers(colourNames[posColour[pulsing[pos]]]);
    }

    // Used by 64 & 21
    private IEnumerator<SouvenirInstruction> process6421(ModuleData module, string className, string fieldName, string alphabet, int radix, int min, int max, Enum q)
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
        yield return question(q).Answers(displayedNumber, preferredWrong: answers.ToArray());
    }
}
