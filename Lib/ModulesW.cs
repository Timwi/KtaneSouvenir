using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessWarningSigns(ModuleData module)
    {
        var comp = GetComponent(module, "warningSignSrc");

        yield return WaitForSolve;

        var displayedSign = GetIntField(comp, "chosenSign").Get(min: 0, max: 19);
        addQuestion(module, Question.WarningSignsDisplayedSign, correctAnswers: new[] { WarningSignsSprites[displayedSign] }, preferredWrongAnswers: WarningSignsSprites);
    }

    private IEnumerator<YieldInstruction> ProcessWasd(ModuleData module)
    {
        var comp = GetComponent(module, "WasdModule");
        var display = GetArrayField<TextMesh>(comp, "DisplayTexts", isPublic: true).Get(minLength: 1).First();
        var displayedLocation = display.text;

        yield return WaitForSolve;
        display.text = "";

        addQuestion(module, Question.WasdDisplayedLocation, correctAnswers: new[] { displayedLocation });
    }

    private IEnumerator<YieldInstruction> ProcessWavetapping(ModuleData module)
    {
        var comp = GetComponent(module, "scr_wavetapping");
        var stageColors = GetArrayField<int>(comp, "stageColors").Get(expectedLength: 3);
        var intPatterns = GetArrayField<int>(comp, "intPatterns").Get(expectedLength: 3);

        yield return WaitForSolve;

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
            qs.Add(makeQuestion(Question.WavetappingPatterns, module,
                formatArgs: new[] { ordinal(stage + 1) },
                correctAnswers: new[] { patternSprites[stageColors[stage]][intPatterns[stage]] },
                preferredWrongAnswers: stageColors.SelectMany(stages => patternSprites[stages]).ToArray()));
        for (int stage = 0; stage < 2; stage++)
            qs.Add(makeQuestion(Question.WavetappingColors, module,
                formatArgs: new[] { ordinal(stage + 1) },
                correctAnswers: new[] { colorNames[stageColors[stage]] }));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessWeakestLink(ModuleData module)
    {
        var comp = GetComponent(module, "WeakestLink");
        yield return WaitForSolve;

        var contestantArr = GetArrayField<object>(comp, "contestants").Get(expectedLength: 3).Skip(1).ToArray();
        var fldCorrectAnswer = GetIntField(contestantArr[0], "CorrectAnswer", isPublic: true);
        var fldQuestionsAsked = GetIntField(contestantArr[0], "QuestionsAsked", isPublic: true);
        var fldCategory = GetField<Enum>(contestantArr[0], "Category", isPublic: true);
        var fldName = GetField<string>(contestantArr[0], "Name", isPublic: true);

        var ratioArr = new string[2];
        var names = new string[2];
        var skill = new Enum[2];

        for (int i = 0; i < 2; i++)
        {
            var person = contestantArr[i];
            skill[i] = fldCategory.GetFrom(person);
            ratioArr[i] = fldCorrectAnswer.GetFrom(person) + "/" + fldQuestionsAsked.GetFrom(person);
            names[i] = fldName.GetFrom(person);
        }

        var eliminatedPerson = GetField<object>(comp, "personToEliminate").Get();
        var eliminationPersonName = GetField<string>(eliminatedPerson, "Name").Get();
        var moneyPhaseName = eliminationPersonName == names[1] ? names[2] : names[1];
        var jsonReader = GetStaticField<object>(comp.GetType(), "jsonData").Get();
        var allNames = GetStaticProperty<List<string>>(jsonReader.GetType(), "ContestantNames", isPublic: true).Get().ToArray();

        addQuestions(module,
            makeQuestion(Question.WeakestLinkElimination, module, correctAnswers: new[] { eliminationPersonName }, preferredWrongAnswers: allNames),
            makeQuestion(Question.WeakestLinkMoneyPhaseName, module, correctAnswers: new[] { moneyPhaseName }, preferredWrongAnswers: allNames),
            makeQuestion(Question.WeakestLinkSkill, module, formatArgs: new[] { names[1] }, correctAnswers: new[] { skill[1].ToString() }),
            makeQuestion(Question.WeakestLinkSkill, module, formatArgs: new[] { names[2] }, correctAnswers: new[] { skill[2].ToString() }),
            makeQuestion(Question.WeakestLinkRatio, module, formatArgs: new[] { names[1] }, correctAnswers: new[] { ratioArr[1] }),
            makeQuestion(Question.WeakestLinkRatio, module, formatArgs: new[] { names[2] }, correctAnswers: new[] { ratioArr[2] })
        );
    }

    private IEnumerator<YieldInstruction> ProcessWhatsOnSecond(ModuleData module)
    {
        var comp = GetComponent(module, "WhatsonSecondScript");
        yield return WaitForSolve;

        var labels = GetArrayField<string>(comp, "Answers").Get(expectedLength: 2);
        var labelColors = GetArrayField<string>(comp, "AnswerColors").Get(expectedLength: 2);

        addQuestions(module,
           makeQuestion(Question.WhatsOnSecondDisplayText, module, formatArgs: new[] { "first" }, correctAnswers: new[] { labels[0].ToLowerInvariant() }),
           makeQuestion(Question.WhatsOnSecondDisplayText, module, formatArgs: new[] { "second" }, correctAnswers: new[] { labels[1].ToLowerInvariant() }),
           makeQuestion(Question.WhatsOnSecondDisplayColor, module, formatArgs: new[] { "first" }, correctAnswers: new[] { labelColors[0] }),
           makeQuestion(Question.WhatsOnSecondDisplayColor, module, formatArgs: new[] { "second" }, correctAnswers: new[] { labelColors[1] }));
    }

    private IEnumerator<YieldInstruction> ProcessWhiteCipher(ModuleData module)
    {
        return processColoredCiphers(module, "whiteCipher", Question.WhiteCipherScreen, _WhiteCipher);
    }

    private IEnumerator<YieldInstruction> ProcessWhoOF(ModuleData module)
    {
        var comp = GetComponent(module, "whoOFScript");
        var displayTextMesh = GetField<TextMesh>(comp, "Disp_Text", isPublic: true);
        var curStageField = GetField<int>(comp, "stage");
        var storedDisplays = new string[2];
        for (var x = 0; x < 2; x++)
            while (curStageField.Get() == x + 1)
            {
                storedDisplays[x] = displayTextMesh.Get().text;
                yield return new WaitForSeconds(0.1f);
            }
        yield return WaitForSolve;
        addQuestions(module, storedDisplays.Select((disp, stage) => makeQuestion(Question.WhoOFDisplay, module, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { disp }, preferredWrongAnswers: storedDisplays)));
    }

    private IEnumerator<YieldInstruction> ProcessWhosOnFirst(ModuleData module)
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
        addQuestions(module, displayWords.Select((word, stage) => makeQuestion(Question.WhosOnFirstDisplay, module, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { word }, preferredWrongAnswers: displayWords)));
    }

    private IEnumerator<YieldInstruction> ProcessWhosOnMorse(ModuleData module)
    {
        var comp = GetComponent(module, "WhosOnMorseScript");
        var curStageField = GetField<int>(comp, "stage");
        var wordBank = GetField<string[]>(comp, "aWords").Get(); // The entire word bank from Who's On Morse. 
        var idxMorseWord = GetField<int>(comp, "lightMorsePos");
        var storedIdxDisplays = new int?[3];

        for (var x = 0; x < 3 && module.Unsolved; x++)
            while (curStageField.Get() == x && module.Unsolved)
            {
                storedIdxDisplays[x] = idxMorseWord.Get();
                yield return new WaitForSeconds(0.1f);
            }
        yield return WaitForSolve;

        var qs = new List<QandA>();
        for (var stage = 0; stage < storedIdxDisplays.Length; stage++)
            if (storedIdxDisplays[stage] != null)
                qs.Add(makeQuestion(Question.WhosOnMorseTransmitDisplay, module, formatArgs: new[] { ordinal(stage + 1) }, correctAnswers: new[] { wordBank[storedIdxDisplays[stage].Value] }, preferredWrongAnswers: storedIdxDisplays.Select(a => a == null ? null : wordBank[a.Value]).Where(s => s != null).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessWire(ModuleData module)
    {
        var comp = GetComponent(module, "wireScript");
        yield return WaitForSolve;

        var dials = GetArrayField<Renderer>(comp, "renderers", isPublic: true).Get(expectedLength: 3);
        addQuestions(module,
            makeQuestion(Question.WireDialColors, module, formatArgs: new[] { "top" }, correctAnswers: new[] { dials[0].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDialColors, module, formatArgs: new[] { "bottom-left" }, correctAnswers: new[] { dials[1].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDialColors, module, formatArgs: new[] { "bottom-right" }, correctAnswers: new[] { dials[2].material.mainTexture.name.Replace("Mat", "") }),
            makeQuestion(Question.WireDisplayedNumber, module, correctAnswers: new[] { GetIntField(comp, "displayedNumber").Get().ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessWireOrdering(ModuleData module)
    {
        var comp = GetComponent(module, "WireOrderingScript");
        var fldChosenColorsDisplay = GetArrayField<int>(comp, "_chosenColorsDis");
        var fldChosenColorsWire = GetArrayField<int>(comp, "_chosenColorsWire");
        var fldChosenDisplayNumbers = GetArrayField<int>(comp, "_chosenDisNum");

        yield return WaitForSolve;

        var colors = GetAnswers(Question.WireOrderingDisplayColor);
        var chosenColorsDisplay = fldChosenColorsDisplay.Get(expectedLength: 4);
        var chosenDisplayNumbers = fldChosenDisplayNumbers.Get(expectedLength: 4);
        var chosenColorsWire = fldChosenColorsWire.Get(expectedLength: 4);

        var qs = new List<QandA>();
        for (var ix = 0; ix < 4; ix++)
        {
            qs.Add(makeQuestion(Question.WireOrderingDisplayColor, module, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colors[chosenColorsDisplay[ix]] }));
            qs.Add(makeQuestion(Question.WireOrderingDisplayNumber, module, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { chosenDisplayNumbers[ix].ToString() }));
            qs.Add(makeQuestion(Question.WireOrderingWireColor, module, formatArgs: new[] { ordinal(ix + 1) }, correctAnswers: new[] { colors[chosenColorsWire[ix]] }));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessWireSequence(ModuleData module)
    {
        var comp = GetComponent(module, "WireSequenceComponent");
        var fldSolved = GetField<bool>(comp, "IsSolved", true);

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_WireSequence);

        var wireSequence = GetField<IEnumerable>(comp, "wireSequence").Get();

        var counts = new int[3];
        var typeWireConfiguration = wireSequence.GetType().GetGenericArguments()[0];
        var fldNoWire = GetFieldFromType<bool>(typeWireConfiguration, "NoWire", isPublic: true);
        var fldColor = GetFieldFromType<object>(typeWireConfiguration, "Color", isPublic: true);

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
            qs.Add(makeQuestion(Question.WireSequenceColorCount, module, formatArgs: new[] { new[] { "black", "blue", "red" }[color] }, correctAnswers: new[] { counts[color].ToString() }, preferredWrongAnswers: preferredWrongAnswers));
        }
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessWolfGoatAndCabbage(ModuleData module)
    {
        var comp = GetComponent(module, "WolfGoatCabbageScript");

        yield return null;

        var animalsPresent = GetListField<string>(comp, "_startShore").Get().ToArray();

        yield return WaitForSolve;

        var boatSize = GetIntField(comp, "_boatSize").Get();
        var allAnimals = GetAnswers(Question.WolfGoatAndCabbageAnimals);

        var questions = new List<QandA>();
        foreach (var present in new[] { false, true })
        {
            questions.Add(makeQuestion(Question.WolfGoatAndCabbageAnimals, module,
                formatArgs: new[] { present ? "present" : "not present" },
                correctAnswers: present ? animalsPresent : allAnimals.Except(animalsPresent).ToArray(),
                preferredWrongAnswers: present ? allAnimals : animalsPresent));
        }
        questions.Add(makeQuestion(Question.WolfGoatAndCabbageBoatSize, module, formatArgs: null, correctAnswers: new[] { boatSize.ToString() }));
        addQuestions(module, questions);
    }

    private IEnumerator<YieldInstruction> ProcessWorkingTitle(ModuleData module)
    {
        var comp = GetComponent(module, "workingTitleCode");

        var correctAnswer = GetField<TextMesh>(comp, "screenText", isPublic: true).Get().text;

        yield return WaitForSolve;

        addQuestion(module, Question.WorkingTitleLabel, correctAnswers: new[] { correctAnswer });
    }
}
