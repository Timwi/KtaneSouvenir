using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessWavetapping(KMBombModule module)
    {
        var comp = GetComponent(module, "scr_wavetapping");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var stageColors = GetArrayField<int>(comp, "stageColors").Get(expectedLength: 3);
        var intPatterns = GetArrayField<int>(comp, "intPatterns").Get(expectedLength: 3);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Wavetapping);

        var patternSprites = new Dictionary<int, Sprite[]>();
        var spriteTake = new[] { 4, 4, 3, 2, 2, 2, 2, 2, 9, 4, 40, 13, 4, 8, 21, 38 };
        var spriteSkip = 0;
        for (int i = 0; i < spriteTake.Length; i++)
        {
            patternSprites.Add(i, WavetappingSprites.Skip(spriteSkip).Take(spriteTake[i]).ToArray());
            spriteSkip += spriteTake[i];
        }

        var colorNames = new[] { "Red", "Orange", "Orange-Yellow", "Chartreuse", "Lime", "Green", "Seafoam Green", "Cyan-Green", "Turquoise", "Dark Blue", "Indigo", "Purple", "Purple-Magenta", "Magenta", "Pink", "Gray" };

        var qs = new List<QandA>();

        for (int stage = 0; stage < intPatterns.Length; stage++)
            qs.Add(makeQuestion(Question.WavetappingPatterns, _Wavetapping,
                formatArgs: new[] { ordinal(stage + 1) },
                correctAnswers: new[] { patternSprites[stageColors[stage]][intPatterns[stage]] },
                preferredWrongAnswers: stageColors.SelectMany(stages => patternSprites[stages]).ToArray()));
        for (int stage = 0; stage < 2; stage++)
            qs.Add(makeQuestion(Question.WavetappingColors, _Wavetapping,
                formatArgs: new[] { ordinal(stage + 1) },
                correctAnswers: new[] { colorNames[stageColors[stage]] }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessWhatsOnSecond(KMBombModule module)
    {
        var comp = GetComponent(module, "WhatsonSecondScript");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_WhatsOnSecond);

        var labels = GetArrayField<string>(comp, "Answers").Get(expectedLength: 2);
        var labelColors = GetArrayField<string>(comp, "AnswerColors").Get(expectedLength: 2);

        addQuestions(module,
           makeQuestion(Question.WhatsOnSecondDisplayText, _WhatsOnSecond, new[] { "first" }, new[] { labels[0].ToLowerInvariant() }),
           makeQuestion(Question.WhatsOnSecondDisplayText, _WhatsOnSecond, new[] { "second" }, new[] { labels[1].ToLowerInvariant() }),
           makeQuestion(Question.WhatsOnSecondDisplayColor, _WhatsOnSecond, new[] { "first" }, new[] { labelColors[0] }),
           makeQuestion(Question.WhatsOnSecondDisplayColor, _WhatsOnSecond, new[] { "second" }, new[] { labelColors[1] }));
    }

    private IEnumerable<object> ProcessWhiteCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "whiteCipher", Question.WhiteCipherAnswer, _WhiteCipher);
    }

    private IEnumerable<object> ProcessWhosOnFirst(KMBombModule module)
    {
        var comp = GetComponent(module, "WhosOnFirstComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);
        var propStage = GetProperty<int>(comp, "CurrentStage", true);
        var propButtonsEmerged = GetProperty<bool>(comp, "ButtonsEmerged", true);
        var displayTextMesh = GetField<MonoBehaviour>(comp, "DisplayText", true).Get(); // TextMeshPro
        var propText = GetProperty<string>(displayTextMesh, "text", true);

        while (!propButtonsEmerged.Get())
            yield return new WaitForSeconds(0.1f);

        var displayWords = new string[2];
        for (var i = 0; i < 2; i++)
            while (propStage.Get() == i)
            {
                while (!propButtonsEmerged.Get())
                    yield return new WaitForSeconds(0.1f);

                displayWords[i] = propText.Get().Replace("'", "’");

                while (propButtonsEmerged.Get())
                    yield return new WaitForSeconds(0.1f);
            }

        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);

        _modulesSolved.IncSafe(_WhosOnFirst);
        addQuestions(module, displayWords.Select((word, stage) => makeQuestion(Question.WhosOnFirstDisplay, _WhosOnFirst, new[] { ordinal(stage + 1) }, new[] { word }, displayWords)));
    }

    private IEnumerable<object> ProcessWire(KMBombModule module)
    {
        var comp = GetComponent(module, "wireScript");
        var fldSolved = GetField<bool>(comp, "moduleDone");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Wire);

        var dials = GetArrayField<Renderer>(comp, "renderers", isPublic: true).Get(expectedLength: 3);
        addQuestions(module,
            makeQuestion(Question.WireDialColors, _Wire, new[] { "top" }, new[] { dials[0].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDialColors, _Wire, new[] { "bottom-left" }, new[] { dials[1].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDialColors, _Wire, new[] { "bottom-right" }, new[] { dials[2].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDisplayedNumber, _Wire, correctAnswers: new[] { GetIntField(comp, "displayedNumber").Get().ToString() }));
    }

    private IEnumerable<object> ProcessWireOrdering(KMBombModule module)
    {
        var comp = GetComponent(module, "WireOrderingScript");
        var fldSolved = GetField<bool>(comp, "_modSolved");
        var fldChosenColorsDisplay = GetArrayField<int>(comp, "_chosenColorsDis");
        var fldChosenColorsWire = GetArrayField<int>(comp, "_chosenColorsWire");
        var fldChosenDisplayNumbers = GetArrayField<int>(comp, "_chosenDisNum");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_WireOrdering);

        var colors = GetAnswers(Question.WireOrderingDisplayColor);
        var chosenColorsDisplay = fldChosenColorsDisplay.Get(expectedLength: 4);
        var chosenDisplayNumbers = fldChosenDisplayNumbers.Get(expectedLength: 4);
        var chosenColorsWire = fldChosenColorsWire.Get(expectedLength: 4);

        var qs = new List<QandA>();
        for (var ix = 0; ix < 4; ix++)
        {
            qs.Add(makeQuestion(Question.WireOrderingDisplayColor, _WireOrdering, new[] { ordinal(ix + 1) }, new[] { colors[chosenColorsDisplay[ix]] }));
            qs.Add(makeQuestion(Question.WireOrderingDisplayNumber, _WireOrdering, new[] { ordinal(ix + 1) }, new[] { chosenDisplayNumbers[ix].ToString() }));
            qs.Add(makeQuestion(Question.WireOrderingWireColor, _WireOrdering, new[] { ordinal(ix + 1) }, new[] { colors[chosenColorsWire[ix]] }));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessWireSequence(KMBombModule module)
    {
        var comp = GetComponent(module, "WireSequenceComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_WireSequence);

        var wireSequence = GetField<IEnumerable>(comp, "wireSequence").Get();

        var counts = new int[3];
        var typeWireConfiguration = wireSequence.GetType().GetGenericArguments()[0];
        var fldNoWire = GetField<bool>(typeWireConfiguration, "NoWire", true);
        var fldColor = GetField<object>(typeWireConfiguration, "Color", true);

        foreach (var item in wireSequence.Cast<object>().Take(12))
            if (!fldNoWire.GetFrom(item))
                counts[(int) fldColor.GetFrom(item)]++;

        var qs = new List<QandA>();
        for (var color = 0; color < 3; color++)
        {
            var preferredWrongAnswers = new string[4];
            for (int i = 0; i < 3; i++)
                preferredWrongAnswers[i] = counts[i].ToString();
            preferredWrongAnswers[3] = (counts[color] == 0 ? 1 : counts[color] - 1).ToString();
            qs.Add(makeQuestion(Question.WireSequenceColorCount, _WireSequence, new[] { new[] { "black", "blue", "red" }[color] }, new[] { counts[color].ToString() }, preferredWrongAnswers));
        }
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessWolfGoatAndCabbage(KMBombModule module)
    {
        var comp = GetComponent(module, "WolfGoatCabbageScript");
        var fldSolved = GetField<bool>(comp, "_moduleSolved");

        yield return null;

        var animalsPresent = GetListField<string>(comp, "_startShore").Get().ToArray();

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_WolfGoatAndCabbage);

        var boatSize = GetIntField(comp, "_boatSize").Get();
        var allAnimals = GetAnswers(Question.WolfGoatAndCabbageAnimals);

        var questions = new List<QandA>();
        foreach (var present in new[] { false, true })
        {
            questions.Add(makeQuestion(Question.WolfGoatAndCabbageAnimals, _WolfGoatAndCabbage,
                formatArgs: new[] { present ? "present" : "not present" },
                correctAnswers: present ? animalsPresent : allAnimals.Except(animalsPresent).ToArray(),
                preferredWrongAnswers: present ? allAnimals : animalsPresent));
        }
        questions.Add(makeQuestion(Question.WolfGoatAndCabbageBoatSize, _WolfGoatAndCabbage, null, new[] { boatSize.ToString() }));
        addQuestions(module, questions);
    }

    private IEnumerable<object> ProcessWorkingTitle(KMBombModule module)
    {
        var comp = GetComponent(module, "workingTitleCode");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        var correctAnswer = GetField<TextMesh>(comp, "screenText", isPublic: true).Get().text;

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_WorkingTitle);

        addQuestions(module, makeQuestion(Question.WorkingTitleLabel, _WorkingTitle, null, new[] { correctAnswer }));
    }
}