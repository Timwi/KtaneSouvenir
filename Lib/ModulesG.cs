using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessGadgetronVendor(ModuleData module)
    {
        var comp = GetComponent(module, "GadgetronVendorScript");

        yield return WaitForSolve;

        GetField<TextMesh>(comp, "currentAmmo", isPublic: true).Get().text = "";
        GetField<TextMesh>(comp, "maxAmmo", isPublic: true).Get().text = "";
        GetField<SpriteRenderer>(comp, "yourWeaponIcon", isPublic: true).Get().enabled = false;

        var currentWeaponIndex = GetIntField(comp, "yourWeaponIndex").Get(min: 0, max: GadgetronVendorIconSprites.Length);
        var saleWeaponIndex = GetIntField(comp, "saleWeaponIndex").Get(min: 0, max: GadgetronVendorWeaponSprites.Length);
        addQuestions(
            module,
            makeQuestion(Question.GadgetronVendorCurrentWeapon, module, correctAnswers: new[] { GadgetronVendorIconSprites[currentWeaponIndex] }, preferredWrongAnswers: GadgetronVendorIconSprites),
            makeQuestion(Question.GadgetronVendorWeaponForSale, module, correctAnswers: new[] { GadgetronVendorWeaponSprites[saleWeaponIndex] }, preferredWrongAnswers: GadgetronVendorWeaponSprites)
        );
    }

    private IEnumerator<YieldInstruction> ProcessGameOfLifeCruel(ModuleData module)
    {
        var comp = GetComponent(module, "GameOfLifeCruel");
        yield return WaitForSolve;

        var colors = new[] { "Black", "White", "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Brown" };
        var colors1 = GetArrayField<int>(comp, "BtnColor1init").Get();
        var colors2 = GetArrayField<int>(comp, "BtnColor2init").Get();

        var answersList = new List<string>();
        for (int i = 0; i < 9; i++)
            if (i != 1)
                for (int j = 0; j < 9; j++)
                    if (j != 1 && (i > 1 || j > 1))
                        answersList.Add(colors[i] == colors[j] ? $"Solid {colors[i]}" : $"{colors[i]}/{colors[j]}");
        var allAnswers = answersList.ToArray();
        var correctAnswers = Enumerable.Range(0, 48).Where(i => (colors1[i] > 1 || colors2[i] > 1) && colors1[i] != 1 && colors2[i] != 1)
            .SelectMany(i => colors1[i] == colors2[i] ? new[] { $"Solid {colors[colors1[i]]}" } : new[] { $"{colors[colors1[i]]}/{colors[colors2[i]]}", $"{colors[colors2[i]]}/{colors[colors1[i]]}" })
            .Distinct().ToArray();

        if (correctAnswers.Length == 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No questions for Game of Life Cruel because there were no colored squares.");
            _legitimatelyNoQuestions.Add(module.Module);
            yield break;
        }

        addQuestion(module, Question.GameOfLifeCruelColors, correctAnswers: correctAnswers, preferredWrongAnswers: allAnswers);
    }

    private IEnumerator<YieldInstruction> ProcessGamepad(ModuleData module)
    {
        var comp = GetComponent(module, "GamepadModule");
        yield return WaitForSolve;

        var x = GetIntField(comp, "x").Get(min: 1, max: 99);
        var y = GetIntField(comp, "y").Get(min: 1, max: 99);
        var display = GetField<GameObject>(comp, "Input", isPublic: true).Get().GetComponent<TextMesh>();
        var digits1 = GetField<GameObject>(comp, "Digits1", isPublic: true).Get().GetComponent<TextMesh>();
        var digits2 = GetField<GameObject>(comp, "Digits2", isPublic: true).Get().GetComponent<TextMesh>();

        if (display == null || digits1 == null || digits2 == null)
            throw new AbandonModuleException($"One of the three displays does not have a TextMesh ({(display == null ? "null" : "not null")}, {(digits1 == null ? "null" : "not null")}, {(digits2 == null ? "null" : "not null")}).");

        addQuestion(module, Question.GamepadNumbers, correctAnswers: new[] { $"{x:00}:{y:00}" },
            preferredWrongAnswers: Enumerable.Range(0, int.MaxValue).Select(i => $"{Rnd.Range(1, 99):00}:{Rnd.Range(1, 99):00}").Distinct().Take(6).ToArray());
        digits1.GetComponent<TextMesh>().text = "--";
        digits2.GetComponent<TextMesh>().text = "--";
    }

    private IEnumerator<YieldInstruction> ProcessGarfieldKart(ModuleData module)
    {
        var comp = GetComponent(module, "garfieldKartScript");
        yield return WaitForSolve;

        var allAnswers = GetListField<string>(comp, "trackNames", isPublic: true).Get(expectedLength: 16);
        var answerIndex = GetIntField(comp, "trackNum").Get(min: 0, max: allAnswers.Count - 1);
        var puzzlePiecesMeshRenders = GetArrayField<GameObject>(comp, "PuzzlePieces", isPublic: true).Get(expectedLength: 3).Select(obj => obj.GetComponent<MeshRenderer>());
        var materials = GetArrayField<Material>(comp, "PuzzleMats", isPublic: true).Get(expectedLength: 2);
        var puzzleCount = puzzlePiecesMeshRenders.Count(mr => mr.material.name.Substring(0, 6).Trim() == materials[1].name);

        // Change the puzzle pieces to orange
        foreach (var mr in puzzlePiecesMeshRenders)
            mr.material = materials[0];

        addQuestions(module,
            makeQuestion(Question.GarfieldKartTrack, module, correctAnswers: new[] { allAnswers[answerIndex] }, allAnswers: allAnswers.ToArray()),
            makeQuestion(Question.GarfieldKartPuzzleCount, module, correctAnswers: new[] { puzzleCount.ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessGarnetThief(ModuleData module)
    {
        var comp = GetComponent(module, "TheGarnetThiefScript");

        var qs = new List<QandA>();

        module.Module.OnPass += () =>
        {
            var contestants = GetField<Array>(comp, "contestants").Get(arr => arr.Length != 7 ? "expected length 7" : null) as object[];
            var fldLying = GetField<bool>(contestants[0], "lying", isPublic: true);
            var fldName = GetField<Enum>(contestants[0], "name", isPublic: true);
            var fldClaimedFaction = GetField<Enum>(contestants[0], "claimedFaction", isPublic: true);

            foreach (var cont in contestants)
                fldLying.SetTo(cont, true);

            GetArrayField<Color>(comp, "frameColors").Set(Enumerable.Repeat(Color.gray, 4).ToArray());
            GetArrayField<Sprite>(comp, "allFactionIcons", isPublic: true).Set(new Sprite[4]);

            qs.AddRange(Enumerable.Range(0, 7).Select(i => makeQuestion(
                question: Question.GarnetThiefClaim,
                data: module,
                formatArgs: new[] { fldName.GetFrom(contestants[i]).ToString() },
                correctAnswers: new[] { fldClaimedFaction.GetFrom(contestants[i]).ToString() })));
            return false;
        };

        yield return WaitForSolve;
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessGhostMovement(ModuleData module)
    {
        var comp = GetComponent(module, "ghostMovementScript");
        yield return WaitForSolve;

        var screens = GetArrayField<TextMesh>(comp, "Screens", isPublic: true).Get(expectedLength: 5);
        foreach (var s in screens)
            s.text = "";

        var validPositions = new[] { 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 57, 62, 68, 71, 77, 82, 85, 90, 96, 99, 105, 110, 113, 118, 124, 127, 133, 138, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 169, 174, 177, 186, 189, 194, 197, 202, 205, 214, 217, 222, 225, 226, 227, 228, 229, 230, 233, 234, 235, 236, 239, 240, 241, 242, 245, 246, 247, 248, 249, 250, 258, 264, 267, 273, 286, 292, 295, 301, 314, 317, 318, 319, 320, 321, 322, 323, 324, 325, 326, 329, 342, 345, 354, 357, 370, 373, 382, 385, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401, 410, 411, 412, 413, 414, 415, 416, 417, 418, 419, 426, 429, 438, 441, 454, 457, 466, 469, 482, 485, 486, 487, 488, 489, 490, 491, 492, 493, 494, 497, 510, 513, 522, 525, 538, 541, 550, 553, 561, 562, 563, 564, 565, 566, 567, 568, 569, 570, 571, 572, 575, 576, 577, 578, 579, 580, 581, 582, 583, 584, 585, 586, 589, 594, 600, 603, 609, 614, 617, 622, 628, 631, 637, 642, 645, 646, 647, 650, 651, 652, 653, 654, 655, 656, 657, 658, 659, 660, 661, 662, 663, 664, 665, 668, 669, 670, 675, 678, 681, 690, 693, 696, 703, 706, 709, 718, 721, 724, 729, 730, 731, 732, 733, 734, 737, 738, 739, 740, 743, 744, 745, 746, 749, 750, 751, 752, 753, 754, 757, 768, 771, 782, 785, 796, 799, 810, 813, 814, 815, 816, 817, 818, 819, 820, 821, 822, 823, 824, 825, 826, 827, 828, 829, 830, 831, 832, 833, 834, 835, 836, 837, 838 };
        var nameFunction = GetMethod<string>(comp, "LocationName", 1);
        var combos = new (string name, string id)[] { ("Pac-Man", "pacman"), ("Blinky", "blinky"), ("Inky", "inky"), ("Pinky", "pinky"), ("Clyde", "clyde") }
            .Select(c => (c.name, position: nameFunction.Invoke(GetField<int>(comp, $"{c.id}Pos").Get(v => !validPositions.Contains(v) ? "not a valid position" : null))))
            .ToArray();
        var shownPositions = combos.Select(c => c.position).ToArray();
        addQuestions(module, combos.Select(c => makeQuestion(Question.GhostMovementPosition, module, formatArgs: new[] { c.name }, correctAnswers: new[] { c.position }, preferredWrongAnswers: shownPositions)));
    }

    private IEnumerator<YieldInstruction> ProcessGirlfriend(ModuleData module)
    {
        var comp = GetComponent(module, "Girlfriend");
        yield return WaitForSolve;

        var languageArr = GetArrayField<string>(comp, "languages").Get();
        var answerIndex = GetIntField(comp, "answerIndex").Get(min: 0, max: languageArr.Length - 1);
        addQuestion(module, Question.GirlfriendLanguage, correctAnswers: new[] { languageArr[answerIndex] });
    }

    private IEnumerator<YieldInstruction> ProcessGlitchedButton(ModuleData module)
    {
        var comp = GetComponent(module, "GlitchedButtonScript");
        yield return WaitForSolve;

        var correctAnswer = GetField<string>(comp, "_cyclingBits").Get();
        var wrongAnswers = new List<string>();
        var gen = new AnswerGenerator.Strings(16, "01");
        foreach (var wrong in gen.GetAnswers(this))
        {
            if (Enumerable.Range(0, 16).Any(amount => wrong.Substring(amount) + wrong.Substring(0, amount) == correctAnswer))
                continue;
            wrongAnswers.Add(wrong);
            if (wrongAnswers.Count == 3)
                break;
        }

        addQuestion(module, Question.GlitchedButtonSequence, correctAnswers: Enumerable.Range(0, 12).Select(amount => correctAnswer.Substring(amount) + correctAnswer.Substring(0, amount)).ToArray(), preferredWrongAnswers: wrongAnswers.ToArray());
    }

    private IEnumerator<YieldInstruction> ProcessGoofysGame(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "GoofysGameScript");
        var nums = GetListField<int>(comp, "lightCodes").Get(expectedLength: 3, validator: v => v is < 0 or > 9 ? "Out of range [0, 9]" : null);
        var directions = new[] { "left", "center", "right" };

        addQuestions(module, nums.Select((n, i) =>
            makeQuestion(Question.GoofysGameNumber, module,
                correctAnswers: new[] { n.ToString() },
                formatArgs: new[] { directions[i] })));
    }

    private IEnumerator<YieldInstruction> ProcessGrandPiano(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "grandPianoScript");
        var sets = GetArrayField<int[]>(comp, "Duck").Get(expectedLength: 5, validator: v => v.Length is not 5 ? "Expected length 5" : v.Any(c => c is < -1 or > 87) ? "Expected range [-1, 87]" : null);

        var noteNames = new[] { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };
        string toNote(int note, bool flat = false)
        {
            note += 9;
            int octave = note / 12;
            var name = noteNames[note % 12];
            if (flat && name.Length is 2)
                name = noteNames[note % 12 + 1] + "♭";
            name += octave;
            return name;
        }

        addQuestions(module, sets.Take(4).Select((s, i) =>
            makeQuestion(Question.GrandPianoKey, module,
                correctAnswers: s.Select(n => toNote(n)).ToArray(),
                formatArgs: new[] { Ordinal(i + 1) }))
            .Concat(new[] { 
                makeQuestion(Question.GrandPianoFinalKey, module,
                correctAnswers: new[] { toNote(sets[4][4], true) })}));
    }

    private IEnumerator<YieldInstruction> ProcessGrayButton(ModuleData module)
    {
        var comp = GetComponent(module, "GrayButtonScript");

        var text = GetField<TextMesh>(comp, "ScreenText", isPublic: true).Get();
        var m = Regex.Match(text.text, @"^(\d), (\d)$");
        if (!m.Success)
            throw new AbandonModuleException($"Unexpected text on Gray Button display: {text.text}");

        yield return WaitForSolve;

        addQuestions(module,
            makeQuestion(Question.GrayButtonCoordinates, module, formatArgs: new[] { "horizontal" }, correctAnswers: new[] { m.Groups[1].Value }),
            makeQuestion(Question.GrayButtonCoordinates, module, formatArgs: new[] { "vertical" }, correctAnswers: new[] { m.Groups[2].Value }));
    }

    private IEnumerator<YieldInstruction> ProcessGrayCipher(ModuleData module)
    {
        return processColoredCiphers(module, "grayCipher", Question.GrayCipherScreen);
    }

    private IEnumerator<YieldInstruction> ProcessGreatVoid(ModuleData module)
    {
        var comp = GetComponent(module, "TheGreatVoid");
        var fldDigits = GetArrayField<int>(comp, "Displays");
        var fldColors = GetArrayField<int>(comp, "ColorNums");

        yield return WaitForSolve;

        var colorNames = new[] { "Red", "Green", "Blue", "Magenta", "Yellow", "Cyan", "White" };

        var questions = new List<QandA>();
        for (int i = 0; i < 6; i++)
        {
            questions.Add(makeQuestion(Question.GreatVoidDigit, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { fldDigits.Get()[i].ToString() }));
            questions.Add(makeQuestion(Question.GreatVoidColor, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: new[] { colorNames[fldColors.Get()[i]] }));
        }
        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessGreenArrows(ModuleData module)
    {
        var comp = GetComponent(module, "GreenArrowsScript");
        var fldNumDisplay = GetField<GameObject>(comp, "numDisplay", isPublic: true);
        var fldStreak = GetIntField(comp, "streak");
        var fldAnimating = GetField<bool>(comp, "isanimating");

        string numbers = null;
        bool activated = false;
        while (module.Unsolved)
        {
            int streak = fldStreak.Get();
            bool animating = fldAnimating.Get();
            if (streak == 6 && !animating && !activated)
            {
                var numDisplay = fldNumDisplay.Get();
                numbers = numDisplay.GetComponent<TextMesh>().text;
                if (numbers == null)
                    throw new AbandonModuleException("numDisplay TextMesh text was null.");
                activated = true;
            }
            if (streak == 0)
                activated = false;
            yield return new WaitForSeconds(.1f);
        }

        if (!int.TryParse(numbers, out var number))
            throw new AbandonModuleException($"The screen is not an integer: “{number}”.");
        if (number < 0 || number > 99)
            throw new AbandonModuleException($"The number on the screen is out of range: number = {number}, expected 0-99");

        addQuestion(module, Question.GreenArrowsLastScreen, correctAnswers: new[] { number.ToString("00") });
    }

    private IEnumerator<YieldInstruction> ProcessGreenButton(ModuleData module)
    {
        var comp = GetComponent(module, "GreenButtonScript");
        var words = GetStaticField<List<string>>(comp.GetType(), "_words").Get().Select(w => w[0] + w.Substring(1).ToLowerInvariant()).ToArray();

        yield return WaitForSolve;

        var displayedString = GetField<string>(comp, "_displayedString").Get(validator: str => str.Length != 7 ? "expected length 7" : null);
        var submission = GetArrayField<bool>(comp, "_submission").Get(expectedLength: 7);
        var submittedWord = Enumerable.Range(0, displayedString.Length).Select(ix => submission[ix] ? displayedString.Substring(ix, 1) : "").JoinString();
        addQuestion(module, Question.GreenButtonWord, correctAnswers: new[] { submittedWord[0] + submittedWord.Substring(1).ToLowerInvariant() }, preferredWrongAnswers: words);
    }

    private IEnumerator<YieldInstruction> ProcessGreenCipher(ModuleData module)
    {
        return processColoredCiphers(module, "greenCipher", Question.GreenCipherScreen);
    }

    private IEnumerator<YieldInstruction> ProcessGridLock(ModuleData module)
    {
        var comp = GetComponent(module, "GridlockModule");

        var colors = Question.GridLockStartingColor.GetAnswers();

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var solution = GetIntField(comp, "_solution").Get(min: 0, max: 15);
        var pages = GetArrayField<int[]>(comp, "_pages").Get(minLength: 5, maxLength: 10, validator: p => p.Length != 16 ? "expected length 16" : p.Any(q => q < 0 || (q & 15) > 12 || (q & (15 << 4)) > (4 << 4)) ? "unexpected value" : null);
        var start = pages[0].IndexOf(i => (i & 15) == 4);

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question.GridLockStartingLocation, module, correctAnswers: new[] { new Coord(4, 4, start) }),
            makeQuestion(Question.GridLockEndingLocation, module, correctAnswers: new[] { new Coord(4, 4, solution) }),
            makeQuestion(Question.GridLockStartingColor, module, correctAnswers: new[] { colors[(pages[0][start] >> 4) - 1] }));
    }

    private IEnumerator<YieldInstruction> ProcessGroceryStore(ModuleData module)
    {
        var comp = GetComponent(module, "GroceryStoreBehav");
        var display = GetField<TextMesh>(comp, "displayTxt", isPublic: true);
        var items = GetField<Dictionary<string, float>>(comp, "itemPrices").Get().Keys.ToArray();

        var finalAnswer = display.Get().text;

        module.Module.OnStrike += delegate { finalAnswer = display.Get().text; return false; };

        yield return WaitForSolve;
        addQuestion(module, Question.GroceryStoreFirstItem, correctAnswers: new[] { finalAnswer }, preferredWrongAnswers: items);
    }

    private IEnumerator<YieldInstruction> ProcessGryphons(ModuleData module)
    {
        var comp = GetComponent(module, "Gryphons");
        yield return WaitForSolve;

        var age = GetIntField(comp, "age").Get(23, 34);
        var name = GetField<string>(comp, "theirName").Get();

        addQuestions(module,
            makeQuestion(Question.GryphonsName, module, correctAnswers: new[] { name }),
            makeQuestion(Question.GryphonsAge, module, correctAnswers: new[] { age.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(23, 34).ToString()).Distinct().Take(6).ToArray()));
    }

    private IEnumerator<YieldInstruction> ProcessGuessWho(ModuleData module)
    {
        var comp = GetComponent(module, "GuessWhoScript");
        var bases = GetField<int[]>(comp, "Bases").Get();

        var colors = new string[] { "Red", "Orange", "Yellow", "Green", "Blue", "Violet", "Cyan", "Pink" };

        yield return WaitForSolve;
        var questions = new List<QandA>();
        for (int i = 0; i < colors.Length; i++)
            questions.Add(makeQuestion(Question.GuessWhoColors, module, formatArgs: new[] { colors[i] }, correctAnswers: new[] { bases[i] == 1 ? "Yes" : "No" }));
        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessGyromaze(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "GyromazeScript");

        var leds = GetArrayField<MeshRenderer>(comp, "leds", true).Get(expectedLength: 2);
        foreach (var l in leds)
            l.material.color = Color.black;

        var colorNames = new[] { "Red", "Blue", "Green", "Yellow" };
        var endPos = GetIntField(comp, "endPos").Get(0, 15);

        addQuestions(module,
            makeQuestion(Question.GyromazeLEDColor, module, correctAnswers: new[] { colorNames[endPos % 4] }, formatArgs: new[] { "top" }),
            makeQuestion(Question.GyromazeLEDColor, module, correctAnswers: new[] { colorNames[endPos / 4] }, formatArgs: new[] { "bottom" })
            );
    }
}
