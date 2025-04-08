using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerator<YieldInstruction> ProcessObjectShows(ModuleData module)
    {
        var comp = GetComponent(module, "objectShows");
        yield return WaitForSolve;

        var contestantsPresent = GetField<IList>(comp, "contestantsPresent").Get(lst => lst.Count != 6 ? "expected length 6" : null);
        var fldId = GetField<int>(contestantsPresent[0], "id", isPublic: true);
        var allContestantNames = GetStaticField<string[]>(comp.GetType(), "characterNames").Get(v => v.Length != 30 ? "expected length 30" : null);
        var contestantNames = Enumerable.Range(0, contestantsPresent.Count).Select(ix => allContestantNames[fldId.GetFrom(contestantsPresent[ix], v => v < 0 || v >= 30 ? "expected range 0–29" : null)]).ToArray();
        addQuestion(module, Question.ObjectShowsContestants, correctAnswers: contestantNames, preferredWrongAnswers: allContestantNames);
    }

    private IEnumerator<YieldInstruction> ProcessOctadecayotton(ModuleData module)
    {
        var comp = GetComponent(module, "TheOctadecayottonScript");
        yield return WaitForSolve;

        var interact = GetField<object>(comp, "Interact", isPublic: true).Get();
        var dimension = GetProperty<int>(interact, "Dimension").Get();
        var sphere = GetField<string>(comp, "souvenirSphere").Get().Where(c => c == '-' || c == '+').JoinString();
        var rotations = GetField<string>(comp, "souvenirRotations").Get().Split('&').ToArray();

        string[] wrongPositions;
        string toPosition(int i) => Convert.ToString(i, 2).Select(s => s == '0' ? '-' : '+').JoinString().PadLeft(dimension, '-');
        if (dimension <= 9)
            wrongPositions = Enumerable.Range(0, (int) Math.Pow(2, dimension)).Select(toPosition).ToArray();
        else
        {
            wrongPositions = new string[10];
            for (int i = 0; i < wrongPositions.Length; i++)
                do { wrongPositions[i] = toPosition(Rnd.Range(0, (int) Math.Pow(2, dimension))); }
                while (wrongPositions.Take(i - 1).Contains(wrongPositions[i]));
        }
        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.OctadecayottonSphere, module, correctAnswers: new[] { sphere }, preferredWrongAnswers: wrongPositions));
        for (int i = 0; i < rotations.Length; i++)
            qs.Add(makeQuestion(Question.OctadecayottonRotations, module, formatArgs: new[] { Ordinal(i + 1) }, correctAnswers: rotations[i].Split(',').Select(s => s.Trim()).ToArray(), preferredWrongAnswers: Enumerable.Range(1, 10).Select(n => new[] { "X", "Y", "Z", "W", "U", "V", "R", "S", "T", "O", "P", "Q", "L", "M", "M", "I", "J", "K", "F", "G", "H", "C", "D", "E", "A", "B", "XX" }.Take(dimension).ToArray().Shuffle().Take(Rnd.Range(1, Math.Min(6, dimension + 1))).Select(c => (Rnd.Range(0, 1f) > 0.5 ? "+" : "-") + c).JoinString()).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessOddOneOut(ModuleData module)
    {
        var comp = GetComponent(module, "OddOneOutModule");

        yield return WaitForSolve;

        var stages = GetField<Array>(comp, "_stages").Get(ar => ar.Length != 6 ? "expected length 6" : ar.Cast<object>().Any(obj => obj == null) ? "contains null" : null);
        var btnNames = new[] { "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right" };
        var stageBtn = stages.Cast<object>().Select(x => GetIntField(x, "CorrectIndex", isPublic: true).Get(min: 0, max: btnNames.Length - 1)).ToArray();

        addQuestions(module,
            makeQuestion(Question.OddOneOutButton, module, formatArgs: new[] { "first" }, correctAnswers: new[] { btnNames[stageBtn[0]] }),
            makeQuestion(Question.OddOneOutButton, module, formatArgs: new[] { "second" }, correctAnswers: new[] { btnNames[stageBtn[1]] }),
            makeQuestion(Question.OddOneOutButton, module, formatArgs: new[] { "third" }, correctAnswers: new[] { btnNames[stageBtn[2]] }),
            makeQuestion(Question.OddOneOutButton, module, formatArgs: new[] { "fourth" }, correctAnswers: new[] { btnNames[stageBtn[3]] }),
            makeQuestion(Question.OddOneOutButton, module, formatArgs: new[] { "fifth" }, correctAnswers: new[] { btnNames[stageBtn[4]] }),
            makeQuestion(Question.OddOneOutButton, module, formatArgs: new[] { "sixth" }, correctAnswers: new[] { btnNames[stageBtn[5]] }));
    }

    private IEnumerator<YieldInstruction> ProcessOffKeys(ModuleData module)
    {
        var comp = GetComponent(module, "OffKeysScript");
        yield return WaitForSolve;

        var notes = new string[] { "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B" };
        var qs = new List<QandA>();

        var faultyKeys = GetListField<int>(comp, "FaultyKeys").Get();
        qs.Add(makeQuestion(Question.OffKeysIncorrectPitch, module, correctAnswers: faultyKeys.Select(i => notes[i]).ToArray()));

        var pickedSymbols = GetArrayField<int>(comp, "PickedSymbols").Get();
        Debug.Log("<><>" + OffKeysSprites.Length);
        var correctSymbols = pickedSymbols.Select(i => OffKeysSprites[i]).ToArray();

        qs.Add(makeQuestion(Question.OffKeysRunes, module, correctAnswers: correctSymbols));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessOldAI(ModuleData module)
    {
        var comp = GetComponent(module, "SCP079");
        var fldSeed = GetField<int>(comp, "Seed");

        yield return WaitForSolve;

        var seed = fldSeed.Get();
        addQuestions(module,
            makeQuestion(Question.OldAIGroup, module, formatArgs: new[] { "group" }, correctAnswers: new[] { ((seed - 1) / 5 + 1).ToString() }),
            makeQuestion(Question.OldAIGroup, module, formatArgs: new[] { "sub-group" }, correctAnswers: new[] { ((seed - 1) % 5 + 1).ToString() }));
    }

    private IEnumerator<YieldInstruction> ProcessOldFogey(ModuleData module)
    {
        var comp = GetComponent(module, "OldFogey");

        yield return WaitForSolve;

        var startingColor = GetMethod<string>(comp, "GetStartingColor", 0).Invoke();
        addQuestion(module, Question.OldFogeyStartingColor, correctAnswers: new[] { startingColor });
    }

    private IEnumerator<YieldInstruction> ProcessOneLinksToAll(ModuleData module)
    {
        var comp = GetComponent(module, "OneLinksToAllScript");

        yield return WaitForSolve;

        var start = GetField<string>(comp, "title1").Get();
        var end = GetField<string>(comp, "title2").Get();
        var path = GetListField<string>(comp, "exampleSolution").Get().ToArray();

        addQuestions(module,
            makeQuestion(Question.OneLinksToAllStart, module, correctAnswers: new[] { start }, allAnswers: path, preferredWrongAnswers: new[] { end }),
            makeQuestion(Question.OneLinksToAllEnd, module, correctAnswers: new[] { end }, allAnswers: path, preferredWrongAnswers: new[] { start }));
    }

    private IEnumerator<YieldInstruction> ProcessOnlyConnect(ModuleData module)
    {
        var comp = GetComponent(module, "OnlyConnectModule");
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var hieroglyphsDisplayed = GetArrayField<int>(comp, "_hieroglyphsDisplayed").Get(expectedLength: 6, validator: v => v < 0 || v > 5 ? "expected range 0–5" : null);

        yield return WaitForSolve;

        var hieroglyphs = new[] { "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus" };
        var positions = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" };
        addQuestions(module, positions.Select((p, i) => makeQuestion(Question.OnlyConnectHieroglyphs, module, formatArgs: new[] { p }, correctAnswers: new[] { hieroglyphs[hieroglyphsDisplayed[i]] })));
    }

    private IEnumerator<YieldInstruction> ProcessOrangeArrows(ModuleData module)
    {
        var comp = GetComponent(module, "OrangeArrowsScript");
        var fldMoves = GetArrayField<string>(comp, "moves");
        var fldStage = GetIntField(comp, "stage");

        // The module does not modify the arrays; it instantiates a new one for each stage.
        var correctMoves = new string[3][];

        var buttons = GetArrayField<KMSelectable>(comp, "buttons", isPublic: true).Get();
        var prevButtonInteracts = buttons.Select(b => b.OnInteract).ToArray();
        for (int i = 0; i < buttons.Length; i++)
        {
            var prevInteract = prevButtonInteracts[i];
            buttons[i].OnInteract = delegate
            {
                var ret = prevInteract();
                var st = fldStage.Get();
                if (st < 1 || st > 3)
                {
                    Debug.Log($"<Souvenir #{_moduleId}> Abandoning Orange Arrows because ‘stage’ was out of range: {st}.");
                    correctMoves = null;
                    for (int j = 0; j < buttons.Length; j++)
                        buttons[j].OnInteract = prevButtonInteracts[j];
                }
                else
                {
                    // We need to capture the array at each button press because the user might get a strike and the array might change.
                    // Avoid throwing an exception within the button handler
                    correctMoves[st - 1] = fldMoves.Get(nullAllowed: true);
                }
                return ret;
            };
        }

        yield return WaitForSolve;

        if (correctMoves == null)   // an error message has already been output
            yield break;

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].OnInteract = prevButtonInteracts[i];

        var directions = new[] { "UP", "RIGHT", "DOWN", "LEFT" };
        if (correctMoves.Any(arr => arr == null || arr.Any(dir => !directions.Contains(dir))))
            throw new AbandonModuleException($"One of the move arrays has an unexpected value: [{correctMoves.Select(arr => arr == null ? "null" : $"[{arr.JoinString(", ")}]").JoinString(", ")}].");

        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                qs.Add(makeQuestion(Question.OrangeArrowsSequences, module, formatArgs: new[] { Ordinal(j + 1), Ordinal(i + 1) }, correctAnswers: new[] { correctMoves[i][j].Substring(0, 1) + correctMoves[i][j].Substring(1).ToLowerInvariant() }));

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessOrangeCipher(ModuleData module)
    {
        return processColoredCiphers(module, "orangeCipher", Question.OrangeCipherScreen);
    }

    private IEnumerator<YieldInstruction> ProcessOrderedKeys(ModuleData module)
    {
        var comp = GetComponent(module, "OrderedKeysScript");
        var fldInfo = GetArrayField<int[]>(comp, "info");
        var fldStage = GetIntField(comp, "stage");

        var curStage = 0;
        var moduleData = new int[3][][];

        while (module.Unsolved)
        {
            var info = fldInfo.Get(expectedLength: 6, validator: arr => arr == null ? "null" : arr.Length != 4 ? "expected length 4" : null);
            var newStage = fldStage.Get(min: 1, max: 3);
            if (curStage != newStage || moduleData[newStage - 1] == null || !Enumerable.Range(0, 6).All(ix => info[ix].SequenceEqual(moduleData[newStage - 1][ix])))
            {
                curStage = newStage;
                moduleData[curStage - 1] = info.Select(arr => arr.ToArray()).ToArray(); // Take a copy of the array.
            }
            yield return new WaitForSeconds(.1f);
        }

        var colors = new[] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };

        var qs = new List<QandA>();
        for (var stage = 0; stage < 3; stage++)
        {
            for (var key = 0; key < 6; key++)
            {
                qs.Add(makeQuestion(Question.OrderedKeysColors, module, OrderedKeysSprites[key], formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { colors[moduleData[stage][key][0]] }));
                qs.Add(makeQuestion(Question.OrderedKeysLabels, module, OrderedKeysSprites[key], formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { (moduleData[stage][key][3] + 1).ToString() }));
                qs.Add(makeQuestion(Question.OrderedKeysLabelColors, module, OrderedKeysSprites[key], formatArgs: new[] { Ordinal(stage + 1) }, correctAnswers: new[] { colors[moduleData[stage][key][1]] }));
            }
        }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessOrderPicking(ModuleData module)
    {
        var comp = GetComponent(module, "OrderPickingScript");

        var fldProductId = GetField<int>(comp, "productId");
        var fldOrderId = GetField<int>(comp, "orderNumber");
        var fldPallet = GetField<string>(comp, "pallet");

        var orderCount = GetField<int>(comp, "orderCount").Get();
        var orderList = new int[orderCount];
        var productList = new int[orderCount];
        var palletList = new string[orderCount];

        var fldNewOrder = GetField<int>(comp, "currentOrder");
        var curOrder = 0;

        while (fldNewOrder.Get() <= orderCount)
        {
            var newOrder = fldNewOrder.Get();
            if (curOrder != newOrder)
            {
                curOrder = newOrder;
                orderList[curOrder - 1] = fldOrderId.Get();
                productList[curOrder - 1] = fldProductId.Get();
                palletList[curOrder - 1] = fldPallet.Get();
            }
            yield return new WaitForSeconds(.1f);
        }
        yield return WaitForSolve;

        var qs = new List<QandA>();

        for (int order = 0; order < orderCount; order++)
        {
            qs.Add(makeQuestion(Question.OrderPickingOrder, module, formatArgs: new[] { Ordinal(order + 1) }, correctAnswers: new[] { orderList[order].ToString() }));
            qs.Add(makeQuestion(Question.OrderPickingProduct, module, formatArgs: new[] { Ordinal(order + 1) }, correctAnswers: new[] { productList[order].ToString() }));
            qs.Add(makeQuestion(Question.OrderPickingPallet, module, formatArgs: new[] { Ordinal(order + 1) }, correctAnswers: new[] { palletList[order] }));
        }

        addQuestions(module, qs);
    }

    private IEnumerator<YieldInstruction> ProcessOrientationCube(ModuleData module)
    {
        var comp = GetComponent(module, "OrientationModule");

        yield return WaitForSolve;

        var initialVirtualViewAngle = GetField<float>(comp, "initialVirtualViewAngle").Get();
        var initialAnglePos = Array.IndexOf(new[] { 0f, 90f, 180f, 270f }, initialVirtualViewAngle);
        if (initialAnglePos == -1)
            throw new AbandonModuleException($"‘initialVirtualViewAngle’ has unexpected value: {initialVirtualViewAngle}");

        addQuestion(module, Question.OrientationCubeInitialObserverPosition, correctAnswers: new[] { new[] { "front", "left", "back", "right" }[initialAnglePos] });
    }

    private IEnumerator<YieldInstruction> ProcessOrientationHypercube(ModuleData module)
    {
        var comp = GetComponent(module, "OrientationHypercubeModule");
        yield return WaitForSolve;

        var initialObserverPosition = GetField<string>(comp, "_initialEyePosition").Get();
        var colourTexts = GetField<Dictionary<string, string>>(GetField<object>(comp, "_readGenerator").Get(), "_cbTexts").Get();
        var faceNames = new Dictionary<string, string>
        {
            ["+X"] = "right",
            ["-X"] = "left",
            ["+Y"] = "top",
            ["-Y"] = "bottom",
            ["+Z"] = "back",
            ["-Z"] = "front",
            ["+W"] = "zag",
            ["-W"] = "zig"
        };
        var qs = new List<QandA>();

        foreach (string key in faceNames.Keys)
            qs.Add(makeQuestion(Question.OrientationHypercubeInitialFaceColour, module, formatArgs: new[] { faceNames[key] }, correctAnswers: new[] { colourTexts[key] }));
        qs.Add(makeQuestion(Question.OrientationHypercubeInitialObserverPosition, module, correctAnswers: new[] { initialObserverPosition }));

        addQuestions(module, qs);
    }
}
