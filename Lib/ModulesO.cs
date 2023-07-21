using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using Rnd = UnityEngine.Random;

public partial class SouvenirModule
{
    private IEnumerable<object> ProcessObjectShows(KMBombModule module)
    {
        var comp = GetComponent(module, "objectShows");
        var fldSolved = GetField<bool>(comp, "moduleSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_ObjectShows);

        var contestantsPresent = GetField<IList>(comp, "contestantsPresent").Get(lst => lst.Count != 6 ? "expected length 6" : null);
        var fldId = GetField<int>(contestantsPresent[0], "id", isPublic: true);
        var allContestantNames = GetStaticField<string[]>(comp.GetType(), "characterNames").Get(v => v.Length != 30 ? "expected length 30" : null);
        var contestantNames = Enumerable.Range(0, contestantsPresent.Count).Select(ix => allContestantNames[fldId.GetFrom(contestantsPresent[ix], v => v < 0 || v >= 30 ? "expected range 0–29" : null)]).ToArray();
        addQuestion(module, Question.ObjectShowsContestants, correctAnswers: contestantNames, preferredWrongAnswers: allContestantNames);
    }

    private IEnumerable<object> ProcessOctadecayotton(KMBombModule module)
    {
        var comp = GetComponent(module, "TheOctadecayottonScript");
        var fldSolved = GetProperty<bool>(comp, "IsSolved");

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_Octadecayotton);

        var interact = GetField<object>(comp, "Interact", isPublic: true).Get();
        var dimension = GetProperty<int>(interact, "Dimension").Get();
        var sphere = GetField<string>(comp, "souvenirSphere").Get().Where(c => c == '-' || c == '+').JoinString();
        var rotations = GetField<string>(comp, "souvenirRotations").Get().Split('&').ToArray();

        var qs = new List<QandA>();
        qs.Add(makeQuestion(Question.OctadecayottonSphere, _Octadecayotton, correctAnswers: new[] { sphere }, preferredWrongAnswers: Enumerable.Range(0, (int) Math.Pow(2, dimension)).Select(i => Convert.ToString(i, 2).Select(s => s == '0' ? '-' : '+').JoinString().PadLeft(dimension, '-')).ToArray()));
        for (int i = 0; i < rotations.Length; i++)
            qs.Add(makeQuestion(Question.OctadecayottonRotations, _Octadecayotton, formatArgs: new[] { ordinal(i + 1) }, correctAnswers: rotations[i].Split(',').Select(s => s.Trim()).ToArray(), preferredWrongAnswers: Enumerable.Range(1, 10).Select(n => "XYZWUVRSTOPQ".Substring(0, dimension).ToCharArray().Shuffle().Take(Rnd.Range(1, Math.Min(6, dimension + 1))).Select(c => (Rnd.Range(0, 1f) > 0.5 ? "+" : "-") + c).JoinString()).ToArray()));
        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessOddOneOut(KMBombModule module)
    {
        var comp = GetComponent(module, "OddOneOutModule");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);

        var stages = GetField<Array>(comp, "_stages").Get(ar => ar.Length != 6 ? "expected length 6" : ar.Cast<object>().Any(obj => obj == null) ? "contains null" : null);
        var btnNames = new[] { "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right" };
        var stageBtn = stages.Cast<object>().Select(x => GetIntField(x, "CorrectIndex", isPublic: true).Get(min: 0, max: btnNames.Length - 1)).ToArray();

        _modulesSolved.IncSafe(_OddOneOut);
        addQuestions(module,
            makeQuestion(Question.OddOneOutButton, _OddOneOut, formatArgs: new[] { "first" }, correctAnswers: new[] { btnNames[stageBtn[0]] }),
            makeQuestion(Question.OddOneOutButton, _OddOneOut, formatArgs: new[] { "second" }, correctAnswers: new[] { btnNames[stageBtn[1]] }),
            makeQuestion(Question.OddOneOutButton, _OddOneOut, formatArgs: new[] { "third" }, correctAnswers: new[] { btnNames[stageBtn[2]] }),
            makeQuestion(Question.OddOneOutButton, _OddOneOut, formatArgs: new[] { "fourth" }, correctAnswers: new[] { btnNames[stageBtn[3]] }),
            makeQuestion(Question.OddOneOutButton, _OddOneOut, formatArgs: new[] { "fifth" }, correctAnswers: new[] { btnNames[stageBtn[4]] }),
            makeQuestion(Question.OddOneOutButton, _OddOneOut, formatArgs: new[] { "sixth" }, correctAnswers: new[] { btnNames[stageBtn[5]] }));
    }

    private IEnumerable<object> ProcessOldAI(KMBombModule module){
        var comp = GetComponent(module, "SCP079");
        var fldSolved = GetField<bool>(comp,"ModuleSolved");
        var fldSeed = GetField<int>(comp,"Seed");
        if(comp==null||fldSolved==null||fldSeed==null){
            yield break;
        }

        yield return null;



        while(!fldSolved.Get()){
            yield return new WaitForSeconds(.1f);
        }
       
        _modulesSolved.IncSafe(_OldAI);
        var Seed = fldSeed.Get();

        addQuestions(module,
            makeQuestion(Question.OldAIGroup,_OldAI,formatArgs: new[]{"Group"},correctAnswers: new[] {System.Convert.ToString((Seed-1)/5+1)} ),
            makeQuestion(Question.OldAIGroup,_OldAI,formatArgs: new[]{"Sub-Group"},correctAnswers: new[] {System.Convert.ToString((Seed-1)%5+1)})
            );

    }
    private IEnumerable<object> ProcessOldFogey(KMBombModule module)
    {
        var comp = GetComponent(module, "OldFogey");

        var fldSolved = GetField<bool>(comp, "moduleSolved");
        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_OldFogey);

        var startingColor = GetMethod<string>(comp, "GetStartingColor", 0).Invoke();
        addQuestion(module, Question.OldFogeyStartingColor, correctAnswers: new[] { startingColor });
    }

    private IEnumerable<object> ProcessOnlyConnect(KMBombModule module)
    {
        var comp = GetComponent(module, "OnlyConnectModule");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");
        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var hieroglyphsDisplayed = GetArrayField<int>(comp, "_hieroglyphsDisplayed").Get(expectedLength: 6, validator: v => v < 0 || v > 5 ? "expected range 0–5" : null);

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_OnlyConnect);

        var hieroglyphs = new[] { "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus" };
        var positions = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" };
        addQuestions(module, positions.Select((p, i) => makeQuestion(Question.OnlyConnectHieroglyphs, _OnlyConnect, formatArgs: new[] { p }, correctAnswers: new[] { hieroglyphs[hieroglyphsDisplayed[i]] })));
    }

    private IEnumerable<object> ProcessOrangeArrows(KMBombModule module)
    {
        var comp = GetComponent(module, "OrangeArrowsScript");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
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
                    Debug.LogFormat("<Souvenir #{0}> Abandoning Orange Arrows because ‘stage’ was out of range: {1}.", _moduleId, st);
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

        while (!fldSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_OrangeArrows);

        if (correctMoves == null)   // an error message has already been output
            yield break;

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].OnInteract = prevButtonInteracts[i];

        var directions = new[] { "UP", "RIGHT", "DOWN", "LEFT" };
        if (correctMoves.Any(arr => arr == null || arr.Any(dir => !directions.Contains(dir))))
            throw new AbandonModuleException("One of the move arrays has an unexpected value: [{0}].",
                correctMoves.Select(arr => arr == null ? "null" : string.Format("[{0}]", arr.JoinString(", "))).JoinString(", "));

        var qs = new List<QandA>();
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                qs.Add(makeQuestion(Question.OrangeArrowsSequences, _OrangeArrows, formatArgs: new[] { ordinal(j + 1), ordinal(i + 1) }, correctAnswers: new[] { correctMoves[i][j].Substring(0, 1) + correctMoves[i][j].Substring(1).ToLowerInvariant() }));

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessOrangeCipher(KMBombModule module)
    {
        return processColoredCiphers(module, "orangeCipher", Question.OrangeCipherAnswer, _OrangeCipher);
    }

    private IEnumerable<object> ProcessOrderedKeys(KMBombModule module)
    {
        var comp = GetComponent(module, "OrderedKeysScript");
        var fldInfo = GetArrayField<int[]>(comp, "info");
        var fldSolved = GetField<bool>(comp, "moduleSolved");
        var fldStage = GetIntField(comp, "stage");

        var curStage = 0;
        var moduleData = new int[3][][];

        while (!fldSolved.Get())
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

        _modulesSolved.IncSafe(_OrderedKeys);

        var colors = new string[6] { "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow" };

        var qs = new List<QandA>();
        for (var stage = 0; stage < 3; stage++)
        {
            for (var key = 0; key < 6; key++)
            {
                qs.Add(makeQuestion(Question.OrderedKeysColors, _OrderedKeys, formatArgs: new[] { ordinal(stage + 1), ordinal(key + 1) }, correctAnswers: new[] { colors[moduleData[stage][key][0]] }));
                qs.Add(makeQuestion(Question.OrderedKeysLabels, _OrderedKeys, formatArgs: new[] { ordinal(stage + 1), ordinal(key + 1) }, correctAnswers: new[] { (moduleData[stage][key][3] + 1).ToString() }));
                qs.Add(makeQuestion(Question.OrderedKeysLabelColors, _OrderedKeys, formatArgs: new[] { ordinal(stage + 1), ordinal(key + 1) }, correctAnswers: new[] { colors[moduleData[stage][key][1]] }));
            }
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessOrderPicking(KMBombModule module)
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
        _modulesSolved.IncSafe(_OrderPicking);

        var qs = new List<QandA>();

        for (int order = 0; order < orderCount; order++)
        {
            qs.Add(makeQuestion(Question.OrderPickingOrder, _OrderPicking, formatArgs: new[] { ordinal(order + 1) }, correctAnswers: new[] { orderList[order].ToString() }));
            qs.Add(makeQuestion(Question.OrderPickingProduct, _OrderPicking, formatArgs: new[] { ordinal(order + 1) }, correctAnswers: new[] { productList[order].ToString() }));
            qs.Add(makeQuestion(Question.OrderPickingPallet, _OrderPicking, formatArgs: new[] { ordinal(order + 1) }, correctAnswers: new[] { palletList[order] }));
        }

        addQuestions(module, qs);
    }

    private IEnumerable<object> ProcessOrientationCube(KMBombModule module)
    {
        var comp = GetComponent(module, "OrientationModule");

        var solved = false;
        module.OnPass += delegate { solved = true; return false; };
        while (!solved)
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_OrientationCube);

        var initialVirtualViewAngle = GetField<float>(comp, "initialVirtualViewAngle").Get();
        var initialAnglePos = Array.IndexOf(new[] { 0f, 90f, 180f, 270f }, initialVirtualViewAngle);
        if (initialAnglePos == -1)
            throw new AbandonModuleException("‘initialVirtualViewAngle’ has unexpected value: {0}", initialVirtualViewAngle);

        addQuestion(module, Question.OrientationCubeInitialObserverPosition, correctAnswers: new[] { new[] { "front", "left", "back", "right" }[initialAnglePos] });
    }

    private IEnumerable<object> ProcessOrientationHypercube(KMBombModule module)
    {
        var comp = GetComponent(module, "OrientationHypercubeModule");
        var fldIsSolved = GetField<bool>(comp, "_isSolved");

        while (!fldIsSolved.Get())
            yield return new WaitForSeconds(.1f);
        _modulesSolved.IncSafe(_OrientationHypercube);

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
            qs.Add(makeQuestion(Question.OrientationHypercubeInitialFaceColour, _OrientationHypercube, formatArgs: new[] { faceNames[key] }, correctAnswers: new[] { colourTexts[key] }));
        qs.Add(makeQuestion(Question.OrientationHypercubeInitialObserverPosition, _OrientationHypercube, correctAnswers: new[] { initialObserverPosition }));

        addQuestions(module, qs);
    }
}