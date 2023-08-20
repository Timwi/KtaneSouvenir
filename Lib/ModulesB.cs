using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessBamboozledAgain(KMBombModule module)
    {
        var comp = GetComponent(module, "BamboozledAgainScript");
        var fldDisplayTexts = GetArrayField<string[]>(comp, "message");
        var fldColorIndex = GetArrayField<int>(comp, "textRandomiser");
        var fldStage = GetIntField(comp, "pressCount");
        var fldCorrectButtons = GetArrayField<int[]>(comp, "answerKey");
        var fldButtonInfo = GetArrayField<string[]>(comp, "buttonRandomiser");
        var fldButtonTextMesh = GetArrayField<TextMesh>(comp, "buttonText", isPublic: true);

        //Beginning of correct button section.

        int stage = 0;

        string[] correctButtonTexts = new string[4];
        string[] correctButtonColors = new string[4];

        //The module cycle the stage count back to 0 regardless. So it gives no indications whether the module is solved or not on the fourth press.
        //Stores the first button in a separate variable. Then, restore it once the module is solved. Index 0 for text. Index 1 for color.

        string[] correctFirstStageButton = new string[2];

        bool dataAdded = false;

        //Not certain why, but the variable 'moduleSolved' in Bamboozled Again script becomes true at the start of the submit couroutine even though the answer may not be correct.
        //Hooking isSolved variable to mitigate the possible side effects.

        var isSolved = false;
        module.OnPass += delegate { isSolved = true; return false; };

        while (!isSolved)
        {
            var newStage = fldStage.Get(min: 0, max: 3);
            if (!dataAdded)
            {
                var buttonInfo = fldButtonInfo.Get(expectedLength: 2, validator: v => v.Length != 6 ? "expected length 6" : null);
                var correctButtons = fldCorrectButtons.Get(expectedLength: 2, validator: v => v.Length != 4 ? "expected length 4" : null);
                if (stage == 0)
                {
                    correctFirstStageButton[0] = correctButtonTexts[stage];
                    correctFirstStageButton[1] = correctButtonColors[stage];
                }
                correctButtonTexts[stage] = Regex.Replace(buttonInfo[1][correctButtons[0][stage]], "#", " ");
                correctButtonColors[stage] = buttonInfo[0][correctButtons[0][stage]][0] + buttonInfo[0][correctButtons[0][stage]].Substring(1).ToLowerInvariant();
                dataAdded = true;
            }
            if (stage != newStage)
            {
                stage = newStage;
                dataAdded = false;
            }
            var buttonTextMesh = fldButtonTextMesh.Get();

            if (buttonTextMesh == null)
                yield break;

            //Check if the module is resetting. There is no flag indicating the module is resetting, but each button will have exactly a string with length of 1 on it.
            if (buttonTextMesh.Any(strMesh => strMesh.text.Length == 1))
                dataAdded = false;

            yield return new WaitForSeconds(.1f);
        }
        _modulesSolved.IncSafe(_BamboozledAgain);

        //Restore the first button to the arrays.

        correctButtonTexts[0] = correctFirstStageButton[0];
        correctButtonColors[0] = correctFirstStageButton[1];

        //End of correct button section.

        //Beginning of the displayed texts section.

        var displayTexts = fldDisplayTexts.Get(expectedLength: 4, validator: v => v.Length != 8 ? "expected length 8" : null).ToArray();
        var colorIndex = fldColorIndex.Get(expectedLength: 8);

        if (displayTexts[0].Any(str => string.IsNullOrEmpty(str)))
            throw new AbandonModuleException($"'displayText[0]' contains null or an empty string: [{displayTexts[0].Select(str => str ?? "<null>").JoinString(", ")}]");

        displayTexts[0] = displayTexts[0].Select(str => Regex.Replace(str, "#", " ")).ToArray();

        string[] firstRowTexts = displayTexts[0].Where((item, index) => index == 0 || index == 2 || index == 4).ToArray();
        string[] lastThreeTexts = displayTexts[0].Where((item, index) => index > 4 && index < 8).ToArray();
        string[] color = new string[14] { "White", "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Grey", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose" };
        string[] displayColors = colorIndex.Select(index => color[index]).ToArray();

        //End of the displayed texts section.

        addQuestions(module,
            correctButtonTexts.Select((name, index) => makeQuestion(Question.BamboozledAgainButtonText, _BamboozledAgain,
                formatArgs: new[] { index == 3 ? "fourth" : ordinal(index + 1) },
                correctAnswers: new[] { name },
                preferredWrongAnswers: correctButtonTexts.Except(new[] { name }).ToArray())).Concat(
            correctButtonColors.Select((col, index) => makeQuestion(Question.BamboozledAgainButtonColor, _BamboozledAgain,
                formatArgs: new[] { index == 3 ? "fourth" : ordinal(index + 1) },
                correctAnswers: new[] { col },
                preferredWrongAnswers: correctButtonColors.Except(new[] { col }).ToArray()))).Concat(
            firstRowTexts.Select((text, index) => makeQuestion(Question.BamboozledAgainDisplayTexts1, _BamboozledAgain,
                formatArgs: new[] { ordinal(2 * index + 1) },
                correctAnswers: new[] { text },
                preferredWrongAnswers: firstRowTexts.Except(new[] { text }).ToArray()))).Concat(
            lastThreeTexts.Select((text, index) => makeQuestion(Question.BamboozledAgainDisplayTexts2, _BamboozledAgain,
                formatArgs: new[] { ordinal(index + 6) },
                correctAnswers: new[] { text },
                preferredWrongAnswers: lastThreeTexts.Except(new[] { text }).ToArray()))).Concat(
            displayColors.Select((col, index) => makeQuestion(Question.BamboozledAgainDisplayColor, _BamboozledAgain,
                formatArgs: new[] { ordinal(index + 1) },
                correctAnswers: new[] { col },
                preferredWrongAnswers: displayColors.Except(new[] { col }).ToArray()))));
    }

    private IEnumerable<object> ProcessBamboozlingButton(KMBombModule module)
    {
        var comp = GetComponent(module, "BamboozlingButtonScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldRandomiser = GetArrayField<int>(comp, "randomiser");
        var fldStage = GetIntField(comp, "stage");

        var moduleData = new int[2][];
        var stage = 0;

        while (!fldSolved.Get())
        {
            var randomiser = fldRandomiser.Get(expectedLength: 11);
            var newStage = fldStage.Get(min: 1, max: 2);
            if (stage != newStage || !randomiser.SequenceEqual(moduleData[newStage - 1]))
            {
                stage = newStage;
                moduleData[stage - 1] = randomiser.ToArray(); // Take a copy of the array.
            }
            yield return new WaitForSeconds(.1f);
        }

        _modulesSolved.IncSafe(_BamboozlingButton);

        var colors = new string[15] { "White", "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Grey", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "Black" };
        var texts = new string[55] { "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE" };
        var qs = new List<QandA>();
        for (var i = 0; i < 2; i++)
        {
            qs.Add(makeQuestion(Question.BamboozlingButtonColor, _BamboozlingButton, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { colors[moduleData[i][0]] }));
            qs.Add(makeQuestion(Question.BamboozlingButtonDisplayColor, _BamboozlingButton, formatArgs: new[] { ordinal(i + 1), "fourth" }, correctAnswers: new[] { colors[moduleData[i][1]] }));
            qs.Add(makeQuestion(Question.BamboozlingButtonDisplayColor, _BamboozlingButton, formatArgs: new[] { ordinal(i + 1), "fifth" }, correctAnswers: new[] { colors[moduleData[i][2]] }));
            qs.Add(makeQuestion(Question.BamboozlingButtonDisplay, _BamboozlingButton, formatArgs: new[] { ordinal(i + 1), "first" }, correctAnswers: new[] { texts[moduleData[i][3]] }));
            qs.Add(makeQuestion(Question.BamboozlingButtonDisplay, _BamboozlingButton, formatArgs: new[] { ordinal(i + 1), "third" }, correctAnswers: new[] { texts[moduleData[i][4]] }));
            qs.Add(makeQuestion(Question.BamboozlingButtonDisplay, _BamboozlingButton, formatArgs: new[] { ordinal(i + 1), "fourth" }, correctAnswers: new[] { texts[moduleData[i][5]] }));
            qs.Add(makeQuestion(Question.BamboozlingButtonDisplay, _BamboozlingButton, formatArgs: new[] { ordinal(i + 1), "fifth" }, correctAnswers: new[] { texts[moduleData[i][6]] }));
            qs.Add(makeQuestion(Question.BamboozlingButtonLabel, _BamboozlingButton, formatArgs: new[] { ordinal(i + 1), "top" }, correctAnswers: new[] { texts[moduleData[i][7]] }));
            qs.Add(makeQuestion(Question.BamboozlingButtonLabel, _BamboozlingButton, formatArgs: new[] { ordinal(i + 1), "bottom" }, correctAnswers: new[] { texts[moduleData[i][8]] }));
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessBakery(KMBombModule module)
    {
        var comp = GetComponent(module, "bakery");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Bakery);

        var cookieTypes = GetField<Array>(comp, "allCookieCategories").Get(validator: arr => arr.Length != 12 ? "expected length 12" : null).Cast<object>().Select(x => x.ToString()).ToArray();
        var cookieIndices = GetArrayField<int>(comp, "cookieIndices").Get(validator: arr => arr.Length != 12 ? "expected length 12" : null);
        var allNameArrays = new string[8][];
        var enumNames = new[] { "regular", "teaBiscuit", "chocolateButterBiscuit", "branded", "danishButter", "macaron", "notCookie", "seasonal" };
        var nameArrayNames = new[] { "regularCookieNames", "teaBiscuitNames", "chocolateButterBiscuitNames", "brandedNames", "danishButterCookieNames", "macaronNames", "notCookieNames", "seasonalCookieNames" };
        for (int i = 0; i < 8; i++)
            allNameArrays[i] = GetStaticField<string[]>(comp.GetType(), nameArrayNames[i]).Get();
        addQuestion(module, Question.BakeryItems,
            correctAnswers: Enumerable.Range(0, 12).Select(i => allNameArrays[Array.IndexOf(enumNames, cookieTypes[i])][cookieIndices[i]]).ToArray(),
            preferredWrongAnswers: allNameArrays.SelectMany(x => x).ToArray());
    }

    private IEnumerable<object> ProcessBarcodeCipher(KMBombModule module)
    {
        var comp = GetComponent(module, "BarcodeCipherScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        var edgeworkInfos = GetField<Array>(comp, "edgework").Get();
        var fldName = GetField<string>(edgeworkInfos.GetValue(0), "Name", isPublic: true);
        var barcodes = new[] { fldName.GetFrom(edgeworkInfos.GetValue(0)), fldName.GetFrom(edgeworkInfos.GetValue(1)), fldName.GetFrom(edgeworkInfos.GetValue(2)) };
        var fldScreenNumber = GetField<string>(comp, "screenNumber").Get(validator: str => str.Length != 6 ? "expected length 6" : str.Any(ch => ch < '0' || ch > '9') ? "expected digits 0–9" : null);
        var answers = GetArrayField<int>(comp, "answerNumbers").Get(validator: arr => arr.Length != 3 ? "expected length 3" : arr.Any(n => n < 0 || n > 8) ? "expected numbers 0–8" : null);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BarcodeCipher);

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.BarcodeCipherScreenNumber, _BarcodeCipher, correctAnswers: new[] { fldScreenNumber }));
        for (int i = 0; i < 3; i++)
        {
            qs.Add(makeQuestion(Question.BarcodeCipherBarcodeEdgework, _BarcodeCipher, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { barcodes[i] }));
            qs.Add(makeQuestion(Question.BarcodeCipherBarcodeAnswers, _BarcodeCipher, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { answers[i].ToString() }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessBartending(KMBombModule module)
    {
        var comp = GetComponent(module, "Maker");
        var fldSolved = GetField<bool>(comp, "_IsSolved");
        var fldIngredientIxs = GetArrayField<int>(comp, "ingIndices");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Bartending);

        var ingIxs = fldIngredientIxs.Get(expectedLength: 5, validator: ing => ing < 0 || ing > 4 ? "expected 0–4" : null);
        var ingredientNames = new[] { "Powdered Delta", "Flanergide", "Adelhyde", "Bronson Extract", "Karmotrine" };
        addQuestions(module, ingIxs.Select((ingIx, pos) => makeQuestion(Question.BartendingIngredients, _Bartending, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { ingredientNames[ingIx] })));
    }

    private IEnumerable<object> ProcessBeans(KMBombModule module)
    {
        var comp = GetComponent(module, "beansScript");
        var fldSolved = GetField<int>(comp, "eatenbeans");

        while (fldSolved.Get() < 3)
            yield return new WaitForSeconds(.1f);

        var bns = GetField<int[]>(comp, "beanArray").Get(a => a.Length != 9 ? "Bad length" : a.Any(i => i < 0 || i >= 6) ? "Bad bean value" : null);
        var eaten = GetField<KMSelectable[]>(comp, "Beans", true).Get(a => a.Length != 9 ? "Bad length" : null);

        _modulesSolved.IncSafe(_Beans);
        string[] flavors = new[] { "Not Wobbly Orange", "Not Wobbly Yellow", "Not Wobbly Green", "Wobbly Orange", "Wobbly Yellow", "Wobbly Green" };
        QandA beansQ(int i) => makeQuestion(Question.BeansColors, _Beans,
            questionSprite: Grid.GenerateGridSprite(3, 3, i),
            correctAnswers: new string[] { flavors[bns[i]] });
        addQuestions(module, Enumerable.Range(0, 9).Where(i => eaten[i].transform.localScale.magnitude <= Mathf.Epsilon).Select(beansQ));
    }

    private IEnumerable<object> ProcessBeanSprouts(KMBombModule module)
    {
        var comp = GetComponent(module, "beanSproutsScript");
        var fldSolved = GetField<int>(comp, "eatenbeans");

        while (fldSolved.Get() < 3)
            yield return new WaitForSeconds(.1f);

        var bns = GetField<int[]>(comp, "beanArray").Get(a => a.Length != 9 ? "Bad length" : a.Any(i => i < 0 || i >= 9) ? "Bad bean value" : null);
        var eaten = GetField<KMSelectable[]>(comp, "Beans", true).Get(a => a.Length != 9 ? "Bad length" : null);

        _modulesSolved.IncSafe(_BeanSprouts);
        string[] flavors = new[] { "Raw", "Cooked", "Burnt" };
        string[] flavors2 = new[] { "Left", "None", "Right" };
        IEnumerable<QandA> beansQ(int i)
        {
            yield return makeQuestion(Question.BeanSproutsColors, _BeanSprouts, formatArgs: new string[] { (i + 1).ToString() }, correctAnswers: new string[] { flavors[bns[i] % 3] });
            yield return makeQuestion(Question.BeanSproutsBeans, _BeanSprouts, formatArgs: new string[] { (i + 1).ToString() }, correctAnswers: new string[] { flavors2[bns[i] / 3] });
        };
        addQuestions(module, Enumerable.Range(0, 9).Where(i => eaten[i].transform.localScale.magnitude <= Mathf.Epsilon).SelectMany(beansQ));
    }

    private IEnumerable<object> ProcessBigBean(KMBombModule module)
    {
        var comp = GetComponent(module, "bigBeanScript");
        var fldSolved = GetField<int>(comp, "eatensteps");

        while (fldSolved.Get() < 3)
            yield return new WaitForSeconds(.1f);

        var bn = GetField<int>(comp, "bean").Get(i => i < 0 || i >= 6 ? "Bad bean value" : null);

        _modulesSolved.IncSafe(_BigBean);
        string[] flavors = new[] { "Not Wobbly Orange", "Not Wobbly Yellow", "Not Wobbly Green", "Wobbly Orange", "Wobbly Yellow", "Wobbly Green" };
        int? match = bn switch
        {
            0 => 5,
            5 => 0,
            2 => 3,
            3 => 2,
            _ => null
        };
        addQuestion(module, Question.BigBeanColor, correctAnswers: new[] { flavors[bn] }, preferredWrongAnswers: match is null ? null : new[] { flavors[match.Value] });
    }

    private IEnumerable<object> ProcessBigCircle(KMBombModule module)
    {
        var comp = GetComponent(module, "TheBigCircle");
        var fldSolved = GetField<bool>(comp, "_solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BigCircle);

        addQuestions(module, GetField<Array>(comp, "_currentSolution").Get(v => v.Length != 3 ? "expected length 3" : null).Cast<object>()
            .Select((color, ix) => makeQuestion(Question.BigCircleColors, _BigCircle, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { color.ToString() })));
    }

    private IEnumerable<object> ProcessBinary(KMBombModule module)
    {
        var comp = GetComponent(module, "Binary");
        var solved = false;

        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Binary);

        addQuestions(module, makeQuestion(Question.BinaryWord, _Binary, formatArgs: null, correctAnswers: new[] { GetAnswers(Question.BinaryWord)[GetField<int>(comp, "te").Get()] }));
    }

    private IEnumerable<object> ProcessBinaryLEDs(KMBombModule module)
    {
        var comp = GetComponent(module, "BinaryLeds");
        var fldSequences = GetField<int[,]>(comp, "sequences");
        var fldSequenceIndex = GetIntField(comp, "sequenceIndex");
        var fldColors = GetArrayField<int>(comp, "colorIndices");
        var fldSolutions = GetField<int[,]>(comp, "solutions");
        var fldSolved = GetField<bool>(comp, "solved");
        var fldBlinkDelay = GetField<float>(comp, "blinkDelay");
        var mthGetIndexFromTime = GetMethod<int>(comp, "GetIndexFromTime", 2);

        int answer = -1;
        var wires = GetArrayField<KMSelectable>(comp, "wires", isPublic: true).Get(expectedLength: 3);

        foreach (var i in Enumerable.Range(0, wires.Length))    // Do not use ‘for’ loop as the loop variable is captured by a lambda
        {
            var oldInteract = wires[i].OnInteract;
            wires[i].OnInteract = delegate
            {
                wires[i].OnInteract = oldInteract;  // Restore original interaction, so that this can only ever be called once per wire.
                var wasSolved = fldSolved.Get();    // Get this before calling oldInteract()
                var seqIx = fldSequenceIndex.Get();
                var numIx = mthGetIndexFromTime.Invoke(Time.time, fldBlinkDelay.Get());
                var colors = fldColors.Get(nullAllowed: true);  // We cannot risk throwing an exception during the module’s button handler
                var solutions = fldSolutions.Get(nullAllowed: true);
                var result = oldInteract();

                if (wasSolved)
                    return result;

                if (colors == null || colors.Length <= i)
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Abandoning Binary LEDs because ‘colors’ array has unexpected length ({(colors == null ? "null" : colors.Length.ToString())}).");
                    return result;
                }

                if (solutions == null || solutions.GetLength(0) <= seqIx || solutions.GetLength(1) <= colors[i])
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Abandoning Binary LEDs because ‘solutions’ array has unexpected lengths ({(solutions == null ? "null" : solutions.GetLength(0).ToString())}, {(solutions == null ? "null" : solutions.GetLength(1).ToString())}).");
                    return result;
                }

                // Ignore if this wasn’t a solve
                if (solutions[seqIx, colors[i]] != numIx)
                    return result;

                // Find out which value is displayed
                var sequences = fldSequences.Get(nullAllowed: true);

                if (sequences == null || sequences.GetLength(0) <= seqIx || sequences.GetLength(1) <= numIx)
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Abandoning Binary LEDs because ‘sequences’ array has unexpected lengths ({(sequences == null ? "null" : sequences.GetLength(0).ToString())}, {(sequences == null ? "null" : sequences.GetLength(1).ToString())}).");
                    return result;
                }

                answer = sequences[seqIx, numIx];
                return result;
            };
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BinaryLEDs);

        if (answer == -1)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Binary LEDs because the module auto-solved after all three wires were cut incorrectly.");
            _legitimatelyNoQuestions.Add(module);
        }
        else
            addQuestion(module, Question.BinaryLEDsValue, correctAnswers: new[] { answer.ToString() });
    }

    private IEnumerable<object> ProcessBinaryShift(KMBombModule module)
    {
        var comp = GetComponent(module, "BinaryShiftModule");
        var fldSolved = GetField<bool>(comp, "solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BinaryShift);

        if (GetProperty<bool>(comp, "forceSolved", true).Get())
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Binary Shift because the module was force-solved.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        var allPositions = new[] { "top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right" };
        var questions = new List<QandA>();
        for (var position = 0; position < 9; position++)
        {
            var initialNumber = GetMethod<int>(comp, "GetInitialNumber", 1, true).Invoke(position);
            var possibleInitialNumbers = GetProperty<HashSet<int>>(comp, "possibleInitialNumbers", true).Get().Select(n => n.ToString()).ToArray();
            questions.Add(makeQuestion(Question.BinaryShiftInitialNumber, _BinaryShift, formatArgs: new[] { allPositions[position] }, correctAnswers: new[] { initialNumber.ToString() }, preferredWrongAnswers: possibleInitialNumbers));
        }
        var stagesCount = GetProperty<int>(comp, "stagesCount", true).Get();
        for (var stage = 0; stage < stagesCount; stage++)
        {
            var selectedNumberPositions = GetMethod<HashSet<int>>(comp, "GetSelectedNumberPositions", 1, true).Invoke(stage);
            if (selectedNumberPositions.Count < 5)
                questions.Add(makeQuestion(Question.BinaryShiftSelectedNumberPossition, _BinaryShift, formatArgs: new[] { stage.ToString() }, correctAnswers: selectedNumberPositions.Select(p => allPositions[p]).ToArray(), preferredWrongAnswers: allPositions));
            else if (selectedNumberPositions.Count > 5)
                questions.Add(makeQuestion(Question.BinaryShiftNotSelectedNumberPossition, _BinaryShift, formatArgs: new[] { stage.ToString() }, correctAnswers: Enumerable.Range(0, 9).Except(selectedNumberPositions).Select(p => allPositions[p]).ToArray(), preferredWrongAnswers: allPositions));
        }
        addQuestions(module, questions);
    }

    private IEnumerable<object> ProcessBitmaps(KMBombModule module)
    {
        var comp = GetComponent(module, "BitmapsModule");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Bitmaps);

        var bitmap = GetArrayField<bool[]>(comp, "_bitmap").Get(expectedLength: 8, validator: arr => arr.Length != 8 ? "expected length 8" : null);
        var qCounts = new int[4];
        for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
                if (bitmap[x][y])
                    qCounts[(y / 4) * 2 + (x / 4)]++;

        var preferredWrongAnswers = qCounts.SelectMany(i => new[] { i, 16 - i }).Distinct().Select(i => i.ToString()).ToArray();

        addQuestions(module,
            makeQuestion(Question.Bitmaps, _Bitmaps, formatArgs: new[] { "white", "top left" }, correctAnswers: new[] { qCounts[0].ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, formatArgs: new[] { "white", "top right" }, correctAnswers: new[] { qCounts[1].ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, formatArgs: new[] { "white", "bottom left" }, correctAnswers: new[] { qCounts[2].ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, formatArgs: new[] { "white", "bottom right" }, correctAnswers: new[] { qCounts[3].ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, formatArgs: new[] { "black", "top left" }, correctAnswers: new[] { (16 - qCounts[0]).ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, formatArgs: new[] { "black", "top right" }, correctAnswers: new[] { (16 - qCounts[1]).ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, formatArgs: new[] { "black", "bottom left" }, correctAnswers: new[] { (16 - qCounts[2]).ToString() }, preferredWrongAnswers: preferredWrongAnswers),
            makeQuestion(Question.Bitmaps, _Bitmaps, formatArgs: new[] { "black", "bottom right" }, correctAnswers: new[] { (16 - qCounts[3]).ToString() }, preferredWrongAnswers: preferredWrongAnswers));
    }

    private IEnumerable<object> ProcessBlackCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "blackCipher", Question.BlackCipherScreen, _BlackCipher);
    }

    private IEnumerable<object> ProcessBlindMaze(KMBombModule module)
    {
        var comp = GetComponent(module, "BlindMaze");
        var fldSolved = GetField<bool>(comp, "Solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BlindMaze);

        // Despite the name “currentMaze”, this field actually contains the number of solved modules when Blind Maze was solved
        var numSolved = GetIntField(comp, "currentMaze").Get(v => v < 0 ? "negative" : null);
        var lastDigit = GetIntField(comp, "LastDigit").Get(min: 0, max: 9);
        var buttonColors = GetArrayField<int>(comp, "buttonColors").Get(expectedLength: 4, validator: bc => bc < 0 || bc > 4 ? "expected 0–4" : null);

        var colorNames = new[] { "Red", "Green", "Blue", "Gray", "Yellow" };
        var buttonNames = new[] { "north", "east", "south", "west" };

        addQuestions(module,
            buttonColors.Select((col, ix) => makeQuestion(Question.BlindMazeColors, _BlindMaze, formatArgs: new[] { buttonNames[ix] }, correctAnswers: new[] { colorNames[col] }))
                .Concat(new[] { makeQuestion(Question.BlindMazeMaze, _BlindMaze, correctAnswers: new[] { ((numSolved + lastDigit) % 10).ToString() }) }));
    }

    private IEnumerable<object> ProcessBlinkstop(KMBombModule module)
    {
        var comp = GetComponent(module, "BlinkstopScript");

        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Blinkstop);

        var flashes = GetArrayField<char>(comp, "prevledcols").Get(arr =>
            !GetAnswers(Question.BlinkstopNumberOfFlashes).Contains(arr.Length.ToString()) ? "unexpected flash count" :
            arr.Any(f => !"PMYC".Contains(f)) ? "expected only P, M, Y, or C flash values" : null);
        var leastFlashedColour = new[] { "Multicolor", "Purple", "Yellow", "Cyan" }.OrderBy(col => flashes.Count(f => f == col[0])).First();

        addQuestions(module,
            makeQuestion(Question.BlinkstopNumberOfFlashes, _Blinkstop, correctAnswers: new[] { flashes.Length.ToString() }),
            makeQuestion(Question.BlinkstopFewestFlashedColor, _Blinkstop, correctAnswers: new[] { leastFlashedColour }));
    }

    private IEnumerable<object> ProcessBlockbusters(KMBombModule module)
    {
        var comp = GetComponent(module, "blockbustersScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var legalLetters = GetListField<string>(comp, "legalLetters", isPublic: true).Get();
        var tiles = GetField<Array>(comp, "tiles", isPublic: true).Get(arr => arr.Cast<object>().Any(v => v == null) ? "contains null" : null);
        var selectables = new KMSelectable[tiles.Length];
        var prevInteracts = new KMSelectable.OnInteractHandler[tiles.Length];
        string lastPress = null;

        for (int i = 0; i < tiles.Length; i++)
        {
            var selectable = selectables[i] = GetField<KMSelectable>(tiles.GetValue(i), "selectable", isPublic: true).Get();
            var prevInteract = prevInteracts[i] = selectable.OnInteract;
            var letter = GetField<TextMesh>(tiles.GetValue(i), "containedLetter", isPublic: true).Get();
            selectable.OnInteract = delegate
            {
                lastPress = letter.text;
                return prevInteract();
            };
        }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Blockbusters);

        for (int i = 0; i < tiles.Length; i++)
            selectables[i].OnInteract = prevInteracts[i];

        if (lastPress == null)
            throw new AbandonModuleException("No pressed letter was retrieved.");

        addQuestion(module, Question.BlockbustersLastLetter, correctAnswers: new[] { lastPress }, preferredWrongAnswers: legalLetters.ToArray());
    }

    private IEnumerable<object> ProcessBlueArrows(KMBombModule module)
    {
        var comp = GetComponent(module, "BlueArrowsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldCoord = GetField<string>(comp, "coord");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BlueArrows);

        string[] letters = { "CA", "C1", "CB", "C8", "CF", "C4", "CE", "C6", "3A", "31", "3B", "38", "3F", "34", "3E", "36", "GA", "G1", "GB", "G8", "GF", "G4", "GE", "G6", "7A", "71", "7B", "78", "7F", "74", "7E", "76", "DA", "D1", "DB", "D8", "DF", "D4", "DE", "D6", "5A", "51", "5B", "58", "5F", "54", "5E", "56", "HA", "H1", "HB", "H8", "HF", "H4", "HE", "H6", "2A", "21", "2B", "28", "2F", "24", "2E", "26" };
        string coord = fldCoord.Get(v => !letters.Contains(v) ? $"expected one of: [{letters.JoinString(", ")}]" : null);
        addQuestion(module, Question.BlueArrowsInitialLetters, correctAnswers: new[] { coord });
    }

    private IEnumerable<object> ProcessBlueButton(KMBombModule module)
    {
        var comp = GetComponent(module, "BlueButtonScript");

        var fldStage = GetField<object>(comp, "_stage");
        while (fldStage.Get().ToString() != "Solved")
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BlueButton);

        var suitsGoal = GetArrayField<int>(comp, "_suitsGoal").Get(expectedLength: 4);
        var colorStageColors = GetArrayField<int>(comp, "_colorStageColors").Get();
        var jumps = GetArrayField<int>(comp, "_jumps").Get(expectedLength: 4, validator: v => v < 0 || v >= 4 ? "expected range 0–3" : null);
        var equationOffsets = GetArrayField<int>(comp, "_equationOffsets").Get(expectedLength: 4);

        var colorNames = new[] { "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow" };

        var valD = Array.IndexOf(suitsGoal, 3) + 1; // 1–4
        var valE = jumps[0];    // 0–3
        var valF = jumps[1];    // 0–3
        var valG = jumps[2];    // 0–3
        var valH = jumps[3];    // 0–3
        var valM = equationOffsets[3];  // 1–9
        var valN = colorStageColors.Length; // 4–9
        var valP = suitsGoal.Where(s => s != 3).Select(s => "♠♣♥"[s]).JoinString(); // permutation of ♠♣♥
        var valQ = colorNames[colorStageColors[3]]; // color
        var valX = equationOffsets[2];  // 1–5

        addQuestions(module,
            makeQuestion(Question.BlueButtonD, _BlueButton, correctAnswers: new[] { valD.ToString() }),
            makeQuestion(Question.BlueButtonEFGH, _BlueButton, formatArgs: new[] { "E" }, correctAnswers: new[] { valE.ToString() }),
            makeQuestion(Question.BlueButtonEFGH, _BlueButton, formatArgs: new[] { "F" }, correctAnswers: new[] { valF.ToString() }),
            makeQuestion(Question.BlueButtonEFGH, _BlueButton, formatArgs: new[] { "G" }, correctAnswers: new[] { valG.ToString() }),
            makeQuestion(Question.BlueButtonEFGH, _BlueButton, formatArgs: new[] { "H" }, correctAnswers: new[] { valH.ToString() }),
            makeQuestion(Question.BlueButtonM, _BlueButton, correctAnswers: new[] { valM.ToString() }),
            makeQuestion(Question.BlueButtonN, _BlueButton, correctAnswers: new[] { valN.ToString() }),
            makeQuestion(Question.BlueButtonP, _BlueButton, correctAnswers: new[] { valP }),
            makeQuestion(Question.BlueButtonQ, _BlueButton, correctAnswers: new[] { valQ }),
            makeQuestion(Question.BlueButtonX, _BlueButton, correctAnswers: new[] { valX.ToString() }));
    }

    private IEnumerable<object> ProcessBlueCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "blueCipher", Question.BlueCipherScreen, _BlueCipher);
    }

    private IEnumerable<object> ProcessBobBarks(KMBombModule module)
    {
        var comp = GetComponent(module, "BobBarks");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldIndicators = GetArrayField<int>(comp, "assigned");
        var fldFlashes = GetArrayField<int>(comp, "stages");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);

        _modulesSolved.IncSafe(_BobBarks);

        string[] validDirections = { "top left", "top right", "bottom left", "bottom right" };
        string[] validLabels = { "BOB", "CAR", "CLR", "IND", "FRK", "FRQ", "MSA", "NSA", "SIG", "SND", "TRN", "BUB", "DOG", "ETC", "KEY" };

        int[] indicators = fldIndicators.Get(expectedLength: 4, validator: idn => idn < 0 || idn >= validLabels.Length ? $"expected 0–{validLabels.Length - 1}" : null);
        int[] flashes = fldFlashes.Get(expectedLength: 5, validator: fn => fn < 0 || fn >= validDirections.Length ? $"expected 0–{validDirections.Length - 1}" : null);

        // To provide preferred wrong answers, mostly.
        string[] labelsOnModule = { validLabels[indicators[0]], validLabels[indicators[1]], validLabels[indicators[2]], validLabels[indicators[3]] };

        addQuestions(module,
            Enumerable.Range(0, 4).Select(ix => makeQuestion(Question.BobBarksIndicators, _BobBarks,
                correctAnswers: new[] { labelsOnModule[ix] },
                formatArgs: new[] { validDirections[ix] },
                preferredWrongAnswers: labelsOnModule.Except(new[] { labelsOnModule[ix] }).ToArray()
            )).Concat(
            Enumerable.Range(0, 5).Select(ix => makeQuestion(Question.BobBarksPositions, _BobBarks,
                correctAnswers: new[] { validDirections[flashes[ix]] },
                formatArgs: new[] { ordinal(ix + 1) }))
            ));
    }

    private IEnumerable<object> ProcessBoggle(KMBombModule module)
    {
        var comp = GetComponent(module, "boggle");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var map = GetField<char[,]>(comp, "letterMap").Get(m => m.GetLength(0) != 10 || m.GetLength(1) != 10 ? $"size was {m.GetLength(0)}×{m.GetLength(1)}, expected 10×10" : null);
        var visible = GetField<string>(comp, "visableLetters", isPublic: true).Get(v => v.Length != 4 ? "expected length 4" : null);
        var verOffset = GetIntField(comp, "verOffset").Get(min: 0, max: 6);
        var horOffset = GetIntField(comp, "horOffset").Get(min: 0, max: 6);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Boggle);

        var letters = new List<string>();
        for (int i = verOffset; i < verOffset + 4; i++)
            for (int j = horOffset; j < horOffset + 4; j++)
                letters.Add(map[i, j].ToString());

        addQuestion(module, Question.BoggleLetters, correctAnswers: visible.Select(v => v.ToString()).ToArray(), preferredWrongAnswers: letters.ToArray());
    }

    private IEnumerable<object> ProcessBombDiffusal(KMBombModule module)
    {
        var comp = GetComponent(module, "bombDiffusalScript");

        var solved = false;
        module.OnPass += () => { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BombDiffusal);

        var fldLicenseNumber = GetField<string>(comp, "licenseNo");
        var mthGenerateRandomLicenseNumber = GetMethod(comp, "GenerateLicenseNo", 0);
        var answers = new HashSet<string>();
        var correctAnswer = fldLicenseNumber.Get(x => x.Length != 6 || x.Any(c => !char.IsLetterOrDigit(c)) ? "expected 6 alphanumeric characters" : null);

        while (answers.Count < 4)
        {
            mthGenerateRandomLicenseNumber.Invoke();
            answers.Add(fldLicenseNumber.Get(x => x.Length != 6 || x.Any(c => !char.IsLetterOrDigit(c)) ? "expected 6 alphanumeric characters" : null));
        }
        fldLicenseNumber.Set(correctAnswer); // Set the license number back to what it was to allow other Souvenir modules to access it.
        addQuestion(module, Question.BombDiffusalLicenseNumber, correctAnswers: new[] { correctAnswer }, preferredWrongAnswers: answers.ToArray());
    }

    private IEnumerable<object> ProcessBooleanWires(KMBombModule module)
    {
        var comp = GetComponent(module, "BooleanWiresScript");

        var fldSolved = GetField<bool>(comp, "solved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BooleanWires);

        var operators = GetListField<string>(comp, "Entered", isPublic: true).Get(expectedLength: 10);
        var qs = new List<QandA>();
        for (int pos = 0; pos < 5; pos++)
            qs.Add(makeQuestion(Question.BooleanWiresEnteredOperators, _BooleanWires, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { operators[2 * pos] }));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessBoomtarTheGreat(KMBombModule module)
    {
        var comp = GetComponent(module, "boomtarTheGreat");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BoomtarTheGreat);

        int rule1 = GetField<int>(comp, "rule1").Get(i => i < 0 || i > 5 ? "Bad rule 1" : null);
        int rule2 = GetField<int>(comp, "rule2").Get(i => i < 0 || i > 5 ? "Bad rule 2" : null);

        addQuestions(module,
            makeQuestion(Question.BoomtarTheGreatRules, _BoomtarTheGreat, formatArgs: new string[] { "one" }, correctAnswers: new string[] { (rule1 + 1).ToString() }, preferredWrongAnswers: new string[] { (rule2 + 1).ToString() }),
            makeQuestion(Question.BoomtarTheGreatRules, _BoomtarTheGreat, formatArgs: new string[] { "two" }, correctAnswers: new string[] { (rule2 + 1).ToString() }, preferredWrongAnswers: new string[] { (rule1 + 1).ToString() }));
    }

    private IEnumerable<object> ProcessBoxing(KMBombModule module)
    {
        var comp = GetComponent(module, "boxing");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Boxing);

        var possibleNames = GetStaticField<string[]>(comp.GetType(), "possibleNames").Get();
        var possibleSubstituteNames = GetStaticField<string[]>(comp.GetType(), "possibleSubstituteNames").Get();
        var possibleLastNames = GetStaticField<string[]>(comp.GetType(), "possibleLastNames").Get();
        var contestantStrengths = GetArrayField<int>(comp, "contestantStrengths").Get(expectedLength: 5);
        var contestantIndices = GetArrayField<int>(comp, "contestantIndices").Get(expectedLength: 5, validator: v => v < 0 || v >= possibleNames.Length ? "out of range" : null);
        var lastNameIndices = GetArrayField<int>(comp, "lastNameIndices").Get(expectedLength: 5, validator: v => v < 0 || v >= possibleLastNames.Length ? "out of range" : null);
        var substituteIndices = GetArrayField<int>(comp, "substituteIndices").Get(expectedLength: 5, validator: v => v < 0 || v >= possibleSubstituteNames.Length ? "out of range" : null);
        var substituteLastNameIndices = GetArrayField<int>(comp, "substituteLastNameIndices").Get(expectedLength: 5, validator: v => v < 0 || v >= possibleLastNames.Length ? "out of range" : null);

        var qs = new List<QandA>();
        for (var ct = 0; ct < 5; ct++)
        {
            qs.Add(makeQuestion(Question.BoxingStrengthByContestant, _Boxing, formatArgs: new[] { possibleNames[contestantIndices[ct]] }, correctAnswers: new[] { contestantStrengths[ct].ToString() }));
            qs.Add(makeQuestion(Question.BoxingContestantByStrength, _Boxing, formatArgs: new[] { "first name", contestantStrengths[ct].ToString() }, correctAnswers: new[] { possibleNames[contestantIndices[ct]] }, preferredWrongAnswers: possibleNames));
            qs.Add(makeQuestion(Question.BoxingContestantByStrength, _Boxing, formatArgs: new[] { "last name", contestantStrengths[ct].ToString() }, correctAnswers: new[] { possibleLastNames[lastNameIndices[ct]] }, preferredWrongAnswers: possibleLastNames));
            qs.Add(makeQuestion(Question.BoxingContestantByStrength, _Boxing, formatArgs: new[] { "substitute’s first name", contestantStrengths[ct].ToString() }, correctAnswers: new[] { possibleSubstituteNames[substituteIndices[ct]] }, preferredWrongAnswers: possibleSubstituteNames));
            qs.Add(makeQuestion(Question.BoxingContestantByStrength, _Boxing, formatArgs: new[] { "substitute’s last name", contestantStrengths[ct].ToString() }, correctAnswers: new[] { possibleLastNames[substituteLastNameIndices[ct]] }, preferredWrongAnswers: possibleLastNames));
        }
        qs.Add(makeQuestion(Question.BoxingNames, _Boxing, formatArgs: new[] { "contestant’s first name", }, correctAnswers: contestantIndices.Select(ix => possibleNames[ix]).ToArray(), preferredWrongAnswers: possibleNames));
        qs.Add(makeQuestion(Question.BoxingNames, _Boxing, formatArgs: new[] { "contestant’s last name" }, correctAnswers: lastNameIndices.Select(ix => possibleLastNames[ix]).ToArray(), preferredWrongAnswers: possibleLastNames));
        qs.Add(makeQuestion(Question.BoxingNames, _Boxing, formatArgs: new[] { "substitute’s first name" }, correctAnswers: substituteIndices.Select(ix => possibleSubstituteNames[ix]).ToArray(), preferredWrongAnswers: possibleSubstituteNames));
        qs.Add(makeQuestion(Question.BoxingNames, _Boxing, formatArgs: new[] { "substitute’s last name" }, correctAnswers: substituteLastNameIndices.Select(ix => possibleLastNames[ix]).ToArray(), preferredWrongAnswers: possibleLastNames));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessBraille(KMBombModule module)
    {
        var comp = GetComponent(module, "BrailleModule");
        var fldSolved = GetField<bool>(comp, "_isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Braille);
        addQuestion(module, Question.BrailleWord, correctAnswers: new[] { GetField<string>(comp, "_word").Get() });
    }

    private IEnumerable<object> ProcessBreakfastEgg(KMBombModule module)
    {
        var comp = GetComponent(module, "breakfastEggScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BreakfastEgg);

        string[] colors = new[] { "Crimson", "Orange", "Pink", "Beige", "Cyan", "Lime", "Petrol" };
        int yolkA = GetIntField(comp, "yolkNumA").Get(min: 0, max: 7);
        int yolkB = GetIntField(comp, "yolkNumB").Get(min: 0, max: 7);
        addQuestion(module, Question.BreakfastEggColor, correctAnswers: new[] { colors[yolkA], colors[yolkB] });
    }

    private IEnumerable<object> ProcessBrokenButtons(KMBombModule module)
    {
        var comp = GetComponent(module, "BrokenButtonModule");
        var fldSolved = GetField<bool>(comp, "Solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BrokenButtons);

        var pressed = GetListField<string>(comp, "Pressed").Get();
        if (pressed.All(p => p.Length == 0))
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for Broken Buttons because the only buttons you pressed were literally blank.");
            _legitimatelyNoQuestions.Add(module);
            yield break;
        }

        // skip the literally blank buttons.
        addQuestions(module, pressed.Select((p, i) => p.Length == 0 ? null : makeQuestion(Question.BrokenButtons, _BrokenButtons, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: new[] { p }, preferredWrongAnswers: pressed.Except(new[] { "" }).ToArray())));
    }

    private IEnumerable<object> ProcessBrokenGuitarChords(KMBombModule module)
    {
        var comp = GetComponent(module, "BrokenGuitarChordsModule");
        var chordDisplay = GetField<TextMesh>(comp, "ChordDisplay", isPublic: true).Get();
        string displayedChord = chordDisplay.text;

        var fldSolved = GetField<bool>(comp, "_isSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BrokenGuitarChords);

        chordDisplay.text = "";

        foreach (var renderer in GetField<MeshRenderer[]>(comp, "FretRenderers", isPublic: true).Get())
            renderer.enabled = false;
        foreach (var renderer in GetField<MeshRenderer[]>(comp, "MuteRenderers", isPublic: true).Get())
            renderer.enabled = false;

        var rootNames = GetStaticField<string[][]>(comp.GetType(), "_noteNames").Get(arr => arr.Length != 12 ? "expected length 12" : null);
        var rootPositions = Enumerable.Range(0, 12).Select(i => i.ToString("00"));
        var qualities = new[] { "", "m", "6", "7", "9", "add9", "m6", "m7", "maj7", "dim", "dim7", "+", "sus" };

        var randomChords = new HashSet<string>();
        while (randomChords.Count < 8)
            randomChords.Add($"{rootPositions.PickRandom()}{qualities.PickRandom()}");
        var possibleAnswers = randomChords.Select(chord => $"{rootNames[int.Parse(chord.Substring(0, 2))].PickRandom()}{chord.Substring(2)}").ToList();
        if (displayedChord.Length > 1 && "#b".Contains(displayedChord[1]))
        {
            var letters = "ABCDEFG";
            var offset = displayedChord[1] == '#' ? 1 : 6;
            possibleAnswers.Remove($"{letters[(letters.IndexOf(displayedChord[0]) + offset) % 7]}b{(displayedChord.Length > 2 ? displayedChord.Substring(2) : "")}");
        }

        var brokenStringPosition = GetIntField(comp, "_brokenString").Get(min: 0, max: 5) + 1;

        addQuestions(
            module,
            makeQuestion(Question.BrokenGuitarChordsDisplayedChord, _BrokenGuitarChords, correctAnswers: new[] { displayedChord }, preferredWrongAnswers: possibleAnswers.ToArray()),
            makeQuestion(Question.BrokenGuitarChordsMutedString, _BrokenGuitarChords, correctAnswers: new[] { brokenStringPosition.ToString() })
        );
    }

    private IEnumerable<object> ProcessBrownCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "brownCipher", Question.BrownCipherScreen, _BrownCipher);
    }

    private IEnumerable<object> ProcessBrushStrokes(KMBombModule module)
    {
        var comp = GetComponent(module, "BrushStrokesScript");
        var fldSolved = GetField<bool>(comp, "solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BrushStrokes);

        string[] colorNames = GetStaticField<string[]>(comp.GetType(), "colorNames").Get();
        int[] colors = GetArrayField<int>(comp, "colors").Get(expectedLength: 9);

        if (colors[4] < 0 || colors[4] >= colorNames.Length)
            throw new AbandonModuleException($"‘colors[4]’ pointed to illegal color: {colors[4]}.");

        addQuestion(module, Question.BrushStrokesMiddleColor, correctAnswers: new[] { char.ToUpperInvariant(colorNames[colors[4]][0]) + colorNames[colors[4]].Substring(1) });
    }

    private IEnumerable<object> ProcessBurgerAlarm(KMBombModule module)
    {
        var comp = GetComponent(module, "burgerAlarmScript");
        var fldSolved = GetField<bool>(comp, "solved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BurgerAlarm);

        var displayedNumber = GetArrayField<int>(comp, "number").Get(expectedLength: 7);
        var orders = GetArrayField<string>(comp, "orderStrings").Get(expectedLength: 5);

        var qs = new List<QandA>();
        for (int pos = 0; pos < 7; pos++)
        {
            qs.Add(makeQuestion(Question.BurgerAlarmDigits, _BurgerAlarm, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { displayedNumber[pos].ToString() }));
            if (pos < 5)
                qs.Add(makeQuestion(Question.BurgerAlarmOrderNumbers, _BurgerAlarm, formatArgs: new[] { ordinal(pos + 1) }, correctAnswers: new[] { orders[pos].Replace("no.    ", "") }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessBulb(KMBombModule module)
    {
        var comp = GetComponent(module, "TheBulbModule");
        var fldStage = GetIntField(comp, "_stage");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        while (fldStage.Get() != 0)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Bulb);

        addQuestion(module, Question.BulbButtonPresses, correctAnswers: new[] { GetField<string>(comp, "_correctButtonPresses").Get(str => str.Length != 3 ? "expected length 3" : null) });
    }

    private IEnumerable<object> ProcessBurglarAlarm(KMBombModule module)
    {
        var comp = GetComponent(module, "BurglarAlarmScript");
        var fldSolved = GetField<bool>(comp, "isSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_BurglarAlarm);

        var displayText = GetField<TextMesh>(comp, "DisplayText", isPublic: true).Get();
        displayText.text = "";

        var moduleNumber = GetArrayField<int>(comp, "moduleNumber").Get(expectedLength: 8, validator: mn => mn < 0 || mn > 9 ? "expected 0–9" : null);
        addQuestions(module, moduleNumber.Select((mn, ix) => makeQuestion(Question.BurglarAlarmDigits, _BurglarAlarm, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { mn.ToString() }, preferredWrongAnswers: moduleNumber.Select(n => n.ToString()).ToArray())));
    }

    private IEnumerable<object> ProcessButton(KMBombModule module)
    {
        var comp = GetComponent(module, "ButtonComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);
        var propLightColor = GetProperty<object>(comp, "IndicatorColor", true);
        var ledOff = GetField<GameObject>(comp, "LED_Off", true).Get();

        var color = -1;
        while (!fldSolved.Get())
        {
            color = ledOff.activeSelf ? -1 : (int) propLightColor.Get();
            yield return new WaitForSeconds(.1f);
        }
        _modulesSolved.IncSafe(_Button);
        if (color < 0)
        {
            Debug.Log($"[Souvenir #{_moduleId}] No question for The Button because the button was tapped (or I missed the light color).");
            _legitimatelyNoQuestions.Add(module);
        }
        else
        {
            var answer = color switch
            {
                0 => "red",
                1 => "blue",
                2 => "yellow",
                3 => "white",
                _ => throw new AbandonModuleException($"IndicatorColor is out of range ({color})."),
            };
            addQuestion(module, Question.ButtonLightColor, correctAnswers: new[] { answer });
        }
    }

    private IEnumerable<object> ProcessButtonSequence(KMBombModule module)
    {
        var comp = GetComponent(module, "ButtonSequencesModule");
        var fldButtonsActive = GetField<bool>(comp, "buttonsActive");

        while (fldButtonsActive.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ButtonSequence);

        var panelInfo = GetField<Array>(comp, "PanelInfo").Get(arr =>
            arr.Rank != 2 ? $"has rank {arr.Rank}, expected 2" :
            arr.GetLength(1) != 3 ? $"GetLength(1) == {arr.GetLength(1)}, expected 3" :
            Enumerable.Range(0, arr.GetLength(0)).Any(x => Enumerable.Range(0, arr.GetLength(1)).Any(y => arr.GetValue(x, y) == null)) ? "contains null" : null);

        var obj = panelInfo.GetValue(0, 0);
        var fldColor = GetIntField(obj, "color", isPublic: true);
        var colorNames = GetStaticField<string[]>(comp.GetType(), "ColorNames").Get();
        var colorOccurrences = new Dictionary<int, int>();
        for (int i = panelInfo.GetLength(0) - 1; i >= 0; i--)
            for (int j = 0; j < 3; j++)
                colorOccurrences.IncSafe(fldColor.GetFrom(panelInfo.GetValue(i, j), v => v < 0 || v >= colorNames.Length ? $"out of range; colorNames.Length={colorNames.Length} ([{colorNames.JoinString(", ")}])" : null));

        addQuestions(module, colorOccurrences.Select(kvp =>
            makeQuestion(Question.ButtonSequencesColorOccurrences, _ButtonSequence,
                formatArgs: new[] { colorNames[kvp.Key].ToLowerInvariant() },
                correctAnswers: new[] { kvp.Value.ToString() },
                preferredWrongAnswers: colorOccurrences.Values.Select(v => v.ToString()).ToArray())));
    }
}