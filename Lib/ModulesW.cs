using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Souvenir;
using UnityEngine;
using UnityEngine.Events;

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

    private IEnumerable<object> ProcessWeakestLink(KMBombModule module)
    {
        var comp = GetComponent(module, "WeakestLink");
        var fldSolved = GetField<bool>(comp, "ModuleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_WeakestLink);

        var contestantArr = GetArrayField<object>(comp, "contestants").Get(expectedLength: 3);

        var ratioArr = new string[3];
        var names = new string[3];
        var skill = new Enum[3];

        for (int i = 0; i < 3; i++)
        {
            var person = contestantArr[i];
            var ratio = GetIntField(person, "CorrectAnswer", isPublic: true).Get() + "/" + GetIntField(person, "QuestionsAsked", isPublic: true).Get();
            skill[i] = GetField<Enum>(person, "Category", isPublic: true).Get();

            ratioArr[i] = ratio;
            names[i] = GetField<string>(person, "Name", isPublic: true).Get();

        }
        
        var eliminatedPerson = GetField<object>(comp, "personToEliminate").Get();
        var eliminationPersonName = GetField<string>(eliminatedPerson, "Name").Get();

        var moneyPhaseName = eliminationPersonName == names[1] ? names[2] : names[1];

        var jsonReader = GetStaticField<object>(comp.GetType(), "jsonData").Get();

        var randomNames = GetStaticProperty<List<string>>(jsonReader.GetType(), "ContestantNames", isPublic: true).Get();

        addQuestions(module,
            makeQuestion(Question.WeakestLinkElimination, _WeakestLink, correctAnswers: new[] { eliminationPersonName }, preferredWrongAnswers: randomNames.ToArray()),
            makeQuestion(Question.WeakestLinkMoneyPhaseName, _WeakestLink, correctAnswers: new[] { moneyPhaseName }, preferredWrongAnswers: randomNames.ToArray()),
            makeQuestion(Question.WeakestLinkSkill, _WeakestLink, formatArgs: new[] { names[1] }, correctAnswers: new[] { skill[1].ToString() }),
            makeQuestion(Question.WeakestLinkSkill, _WeakestLink, formatArgs: new[] { names[2] }, correctAnswers: new[] { skill[2].ToString() }),
            makeQuestion(Question.WeakestLinkRatio, _WeakestLink, formatArgs: new[] { "you" }, correctAnswers: new[] { ratioArr[0] }),
            makeQuestion(Question.WeakestLinkRatio, _WeakestLink, formatArgs: new[] { names[1] }, correctAnswers: new[] { ratioArr[1] }),
            makeQuestion(Question.WeakestLinkRatio, _WeakestLink, formatArgs: new[] { names[2] }, correctAnswers: new[] { ratioArr[2] }));
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
           makeQuestion(Question.WhatsOnSecondDisplayText, _WhatsOnSecond, formatArgs: new[] { "first" }, correctAnswers: new[] { labels[0].ToLowerInvariant() }),
           makeQuestion(Question.WhatsOnSecondDisplayText, _WhatsOnSecond, formatArgs: new[] { "second" }, correctAnswers: new[] { labels[1].ToLowerInvariant() }),
           makeQuestion(Question.WhatsOnSecondDisplayColor, _WhatsOnSecond, formatArgs: new[] { "first" }, correctAnswers: new[] { labelColors[0] }),
           makeQuestion(Question.WhatsOnSecondDisplayColor, _WhatsOnSecond, formatArgs: new[] { "second" }, correctAnswers: new[] { labelColors[1] }));
    }

    private IEnumerable<object> ProcessWhiteCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "whiteCipher", Question.WhiteCipherScreen, _WhiteCipher);
    }

    private IEnumerable<object> ProcessWhoOF(KMBombModule module)
    {
        var comp = GetComponent(module, "whoOFScript");
        var fldSolved = GetField<bool>(comp, "mod_Done");
        var displayTextMesh = GetField<TextMesh>(comp, "Disp_Text", isPublic: true);
        var curStageField = GetField<int>(comp, "stage");
        var storedDisplays = new string[2];
        for (var x = 0; x < 2; x++)
            while (curStageField.Get() == x + 1)
            {
                storedDisplays[x] = displayTextMesh.Get().text;
                yield return new WaitForSeconds(0.1f);
            }
        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_WhoOF);
        addQuestions(module, storedDisplays.Select((disp, stage) => makeQuestion(Question.WhoOFDisplay, _WhoOF, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { disp }, preferredWrongAnswers: storedDisplays)));
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
        addQuestions(module, displayWords.Select((word, stage) => makeQuestion(Question.WhosOnFirstDisplay, _WhosOnFirst, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { word }, preferredWrongAnswers: displayWords)));
    }

    private IEnumerable<object> ProcessWhosOnMorse(KMBombModule module)
    {
        var comp = GetComponent(module, "WhosOnMorseScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var curStageField = GetField<int>(comp, "stage");
        var wordBank = GetField<string[]>(comp, "aWords").Get(); // The entire word bank from Who's On Morse. 
        var idxMorseWord = GetField<int>(comp, "lightMorsePos");
        var storedIdxDisplays = new int?[3];

        for (var x = 0; x < 3 && !fldSolved.Get(); x++)
            while (curStageField.Get() == x && !fldSolved.Get())
            {
                storedIdxDisplays[x] = idxMorseWord.Get();
                yield return new WaitForSeconds(0.1f);
            }
        while (!fldSolved.Get())
            yield return new WaitForSeconds(0.1f);
        _modulesSolved.IncSafe(_WhosOnMorse);

        var qs = new List<QandA>();
        for (var stage = 0; stage < storedIdxDisplays.Length; stage++)
            if (storedIdxDisplays[stage] != null)
                qs.Add(makeQuestion(Question.WhosOnMorseTransmitDisplay, _WhosOnMorse, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { wordBank[storedIdxDisplays[stage].Value] }, preferredWrongAnswers: storedIdxDisplays.Select(a => a == null ? null : wordBank[a.Value]).Where(s => s != null).ToArray()));
        addQuestions(module, qs);
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
            makeQuestion(Question.WireDialColors, _Wire, formatArgs: new[] { "top" }, correctAnswers: new[] { dials[0].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDialColors, _Wire, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { dials[1].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDialColors, _Wire, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { dials[2].material.mainTexture.name.Replace("Mat", "") }),
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
            qs.Add(makeQuestion(Question.WireOrderingDisplayColor, _WireOrdering, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colors[chosenColorsDisplay[ix]] }));
            qs.Add(makeQuestion(Question.WireOrderingDisplayNumber, _WireOrdering, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { chosenDisplayNumbers[ix].ToString() }));
            qs.Add(makeQuestion(Question.WireOrderingWireColor, _WireOrdering, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colors[chosenColorsWire[ix]] }));
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
            qs.Add(makeQuestion(Question.WireSequenceColorCount, _WireSequence, formatArgs: new[] { new[] { "black", "blue", "red" }[color] }, correctAnswers: new[] { counts[color].ToString() }, preferredWrongAnswers: preferredWrongAnswers));
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
        questions.Add(makeQuestion(Question.WolfGoatAndCabbageBoatSize, _WolfGoatAndCabbage, formatArgs: null, correctAnswers: new[] { boatSize.ToString() }));
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

        addQuestions(module, makeQuestion(Question.WorkingTitleLabel, _WorkingTitle, formatArgs: null, correctAnswers: new[] { correctAnswer }));
    }
}