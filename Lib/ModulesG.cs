using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessGadgetronVendor(KMBombModule module)
    {
        var comp = GetComponent(module, "GadgetronVendorScript");

        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_GadgetronVendor);

        GetField<TextMesh>(comp, "currentAmmo", isPublic: true).Get().text = "";
        GetField<TextMesh>(comp, "maxAmmo", isPublic: true).Get().text = "";
        GetField<SpriteRenderer>(comp, "yourWeaponIcon", isPublic: true).Get().enabled = false;

        var currentWeaponIndex = GetIntField(comp, "yourWeaponIndex").Get(min: 0, max: GadgetronVendorIconSprites.Length);
        var saleWeaponIndex = GetIntField(comp, "saleWeaponIndex").Get(min: 0, max: GadgetronVendorWeaponSprites.Length);
        addQuestions(
            module,
            makeQuestion(Question.GadgetronVendorCurrentWeapon, _GadgetronVendor, correctAnswers: new[] { GadgetronVendorIconSprites[currentWeaponIndex] }, preferredWrongAnswers: GadgetronVendorIconSprites),
            makeQuestion(Question.GadgetronVendorWeaponForSale, _GadgetronVendor, correctAnswers: new[] { GadgetronVendorWeaponSprites[saleWeaponIndex] }, preferredWrongAnswers: GadgetronVendorWeaponSprites)
        );
    }

    private IEnumerable<object> ProcessGameOfLifeCruel(KMBombModule module)
    {
        var comp = GetComponent(module, "GameOfLifeCruel");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_GameOfLifeCruel);

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
            Debug.LogFormat("[Souvenir #{0}] No questions for Game of Life Cruel because there were no colored squares.", _moduleId);
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        addQuestion(module, Question.GameOfLifeCruelColors, correctAnswers: correctAnswers, preferredWrongAnswers: allAnswers);
    }

    private IEnumerable<object> ProcessGamepad(KMBombModule module)
    {
        var comp = GetComponent(module, "GamepadModule");
        var fldSolved = GetField<bool>(comp, "solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.05f);
        _modulesSolved.IncSafe(_Gamepad);

        var x = GetIntField(comp, "x").Get(min: 1, max: 99);
        var y = GetIntField(comp, "y").Get(min: 1, max: 99);
        var display = GetField<GameObject>(comp, "Input", isPublic: true).Get().GetComponent<TextMesh>();
        var digits1 = GetField<GameObject>(comp, "Digits1", isPublic: true).Get().GetComponent<TextMesh>();
        var digits2 = GetField<GameObject>(comp, "Digits2", isPublic: true).Get().GetComponent<TextMesh>();

        if (display == null || digits1 == null || digits2 == null)
            throw new AbandonModuleException("One of the three displays does not have a TextMesh ({0}, {1}, {2}).",
                display == null ? "null" : "not null", digits1 == null ? "null" : "not null", digits2 == null ? "null" : "not null");

        addQuestions(module, makeQuestion(Question.GamepadNumbers, _Gamepad, correctAnswers: new[] { string.Format("{0:00}:{1:00}", x, y) },
            preferredWrongAnswers: Enumerable.Range(0, int.MaxValue).Select(i => string.Format("{0:00}:{1:00}", Rnd.Range(1, 99), Rnd.Range(1, 99))).Distinct().Take(6).ToArray()));
        digits1.GetComponent<TextMesh>().text = "--";
        digits2.GetComponent<TextMesh>().text = "--";
    }

    private IEnumerable<object> ProcessGarnetThief(KMBombModule module)
    {
        var comp = GetComponent(module, "TheGarnetThiefScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_GarnetTheif);

        var contestant = GetArrayField<object>(comp, "contestants").Get();

        var qs = new List<QandA>();

        for (int i = 0; i < 7; i++)
        {
            qs.Add(makeQuestion(Question.GarnetThiefClaim, _GarnetTheif, formatArgs: new[] { GetField<Enum>(contestant[i], "name", isPublic: true).Get().ToString() }, correctAnswers: new[] { GetField<Enum>(contestant[i], "claimedFaction", isPublic: true).Get().ToString() }));
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessGirlfriend(KMBombModule module)
    {
        var comp = GetComponent(module, "Girlfriend");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Girlfriend);

        var languageArr = GetArrayField<string>(comp, "languages").Get();
        var answerIndex = GetIntField(comp, "answerIndex").Get(min: 0, max: languageArr.Length - 1);
        addQuestion(module, Question.GirlfriendLanguage, correctAnswers: new[] { languageArr[answerIndex] });
    }

    private IEnumerable<object> ProcessGlitchedButton(KMBombModule module)
    {
        var comp = GetComponent(module, "GlitchedButtonScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_GlitchedButton);

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

    private IEnumerable<object> ProcessGrayButton(KMBombModule module)
    {
        var comp = GetComponent(module, "GrayButtonScript");

        var fldSolved = GetField<bool>(comp, "_moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        var text = GetField<TextMesh>(comp, "GrayButtonText", isPublic: true).Get();
        var m = Regex.Match(text.text, @"^(\d), (\d)$");
        if (!m.Success)
            throw new AbandonModuleException("Unexpected text on Gray Button display: {0}", text.text);
        _modulesSolved.IncSafe(_GrayButton);

        addQuestions(module,
            makeQuestion(Question.GrayButtonCoordinates, _GrayButton, formatArgs: new[] { "horizontal" }, correctAnswers: new[] { m.Groups[1].Value }),
            makeQuestion(Question.GrayButtonCoordinates, _GrayButton, formatArgs: new[] { "vertical" }, correctAnswers: new[] { m.Groups[2].Value }));
    }

    private IEnumerable<object> ProcessGrayCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "grayCipher", Question.GrayCipherScreen, _GrayCipher);
    }

    private IEnumerable<object> ProcessGreatVoid(KMBombModule module)
    {
        var comp = GetComponent(module, "TheGreatVoid");
        var fldSolved = GetField<bool>(comp, "Solved");
        var fldDigits = GetArrayField<int>(comp, "Displays");
        var fldColors = GetArrayField<int>(comp, "ColorNums");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_GreatVoid);

        var colorNames = new[] { "Red", "Green", "Blue", "Magenta", "Yellow", "Cyan", "White" };

        var questions = new List<QandA>();
        for (int i = 0; i < 6; i++)
        {
            questions.Add(makeQuestion(Question.GreatVoidDigit, _GreatVoid, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { fldDigits.Get()[i].ToString() }));
            questions.Add(makeQuestion(Question.GreatVoidColor, _GreatVoid, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colorNames[fldColors.Get()[i]] }));
        }
        addQuestions(module, questions);
    }

    private IEnumerable<object> ProcessGreenArrows(KMBombModule module)
    {
        var comp = GetComponent(module, "GreenArrowsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldNumDisplay = GetField<GameObject>(comp, "numDisplay", isPublic: true);
        var fldStreak = GetIntField(comp, "streak");
        var fldAnimating = GetField<bool>(comp, "isanimating");

        string numbers = null;
        bool activated = false;
        while (!fldSolved.Get())
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

        _modulesSolved.IncSafe(_GreenArrows);

        if (!int.TryParse(numbers, out var number))
            throw new AbandonModuleException("The screen is not an integer: “{0}”.", number);
        if (number < 0 || number > 99)
            throw new AbandonModuleException("The number on the screen is out of range: number = {1}, expected 0-99", number);

        addQuestions(module, makeQuestion(Question.GreenArrowsLastScreen, _GreenArrows, correctAnswers: new[] { number.ToString("00") }));
    }

    private IEnumerable<object> ProcessGreenButton(KMBombModule module)
    {
        var comp = GetComponent(module, "GreenButtonScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");
        var words = GetStaticField<List<string>>(comp.GetType(), "_words").Get().Select(w => w[0] + w.Substring(1).ToLowerInvariant()).ToArray();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_GreenButton);

        var displayedString = GetField<string>(comp, "_displayedString").Get(validator: str => str.Length != 7 ? "expected length 7" : null);
        var submission = GetArrayField<bool>(comp, "_submission").Get(expectedLength: 7);
        var submittedWord = Enumerable.Range(0, displayedString.Length).Select(ix => submission[ix] ? displayedString.Substring(ix, 1) : "").JoinString();
        addQuestions(module, makeQuestion(Question.GreenButtonWord, _GreenButton, correctAnswers: new[] { submittedWord[0] + submittedWord.Substring(1).ToLowerInvariant() }, preferredWrongAnswers: words));
    }

    private IEnumerable<object> ProcessGreenCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "greenCipher", Question.GreenCipherScreen, _GreenCipher);
    }

    private IEnumerable<object> ProcessGridLock(KMBombModule module)
    {
        var comp = GetComponent(module, "GridlockModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        var colors = GetAnswers(Question.GridLockStartingColor);

        while (!_isActivated)
            yield return new WaitForSeconds(0.1f);

        var solution = GetIntField(comp, "_solution").Get(min: 0, max: 15);
        var pages = GetArrayField<int[]>(comp, "_pages").Get(minLength: 5, maxLength: 10, validator: p => p.Length != 16 ? "expected length 16" : p.Any(q => q < 0 || (q & 15) > 12 || (q & (15 << 4)) > (4 << 4)) ? "unexpected value" : null);
        var start = pages[0].IndexOf(i => (i & 15) == 4);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_GridLock);
        addQuestions(module,
            makeQuestion(Question.GridLockStartingLocation, _GridLock, correctAnswers: new[] { new Coord(4, 4, start) }),
            makeQuestion(Question.GridLockEndingLocation, _GridLock, correctAnswers: new[] { new Coord(4, 4, solution) }),
            makeQuestion(Question.GridLockStartingColor, _GridLock, correctAnswers: new[] { colors[(pages[0][start] >> 4) - 1] }));
    }

    private IEnumerable<object> ProcessGroceryStore(KMBombModule module)
    {
        var comp = GetComponent(module, "GroceryStoreBehav");
        var solved = false;
        var display = GetField<TextMesh>(comp, "displayTxt", isPublic: true);
        var items = GetField<Dictionary<string, float>>(comp, "itemPrices").Get().Keys.ToArray();

        var finalAnswer = display.Get().text;
        module.OnPass += delegate { solved = true; return false; };

        var hadStrike = false;
        module.OnStrike += delegate { hadStrike = true; return false; };

        while (!solved)
        {
            if (hadStrike)
            {
                finalAnswer = display.Get().text;
                hadStrike = false;
            }
            yield return null;
        }

        _modulesSolved.IncSafe(_GroceryStore);
        addQuestions(module, makeQuestion(Question.GroceryStoreFirstItem, _GroceryStore, formatArgs: null, correctAnswers: new[] { finalAnswer }, preferredWrongAnswers: items));
    }

    private IEnumerable<object> ProcessGryphons(KMBombModule module)
    {
        var comp = GetComponent(module, "Gryphons");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Gryphons);

        var age = GetIntField(comp, "age").Get(23, 34);
        var name = GetField<string>(comp, "theirName").Get();

        addQuestions(module,
            makeQuestion(Question.GryphonsName, _Gryphons, correctAnswers: new[] { name }),
            makeQuestion(Question.GryphonsAge, _Gryphons, correctAnswers: new[] { age.ToString() }, preferredWrongAnswers:
                Enumerable.Range(0, int.MaxValue).Select(i => Rnd.Range(23, 34).ToString()).Distinct().Take(6).ToArray()));
    }

    private IEnumerable<object> ProcessGuessWho(KMBombModule module)
    {
        var comp = GetComponent(module, "GuessWhoScript");
        var names = GetField<string[]>(comp, "Names", isPublic: true).Get();

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };

        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_GuessWho);

        var correctAnswer = names[GetField<int>(comp, "TheCombination").Get()];
        addQuestions(module, makeQuestion(Question.GuessWhoPerson, _GuessWho, formatArgs: null, correctAnswers: new[] { correctAnswer }, preferredWrongAnswers: names));
    }
}
