using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLabyrinth
{
    [SouvenirQuestion("Where was one of the portals in layer {1} in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites, TranslateArguments = [true], Arguments = ["1 (Red)", "2 (Orange)", "3 (Yellow)", "4 (Green)", "5 (Blue)"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(6, 7)]
    PortalLocations,
    
    [SouvenirQuestion("In which layer was this portal in {0}?", TwoColumns4Answers, "1 (Red)", "2 (Orange)", "3 (Yellow)", "4 (Green)", "5 (Blue)", TranslateAnswers = true, UsesQuestionSprite = true)]
    PortalStage
}

public partial class SouvenirModule
{
    [SouvenirHandler("labyrinth", "Labyrinth", typeof(SLabyrinth), "Anonymous", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessLabyrinth(ModuleData module)
    {
        var comp = GetComponent(module, "labyrinthScript");
        yield return WaitForSolve;

        var l1 = new object[]
        {
            GetField<object>(comp, "level1Info", true).Get(),
            GetField<object>(comp, "level2Info", true).Get(),
            GetField<object>(comp, "level3Info", true).Get(),
            GetField<object>(comp, "level4Info", true).Get(),
            GetField<object>(comp, "level5Info", true).Get()
        };

        var t1 = GetField<int>(l1[0], "target1", isPublic: true);
        var t2 = GetField<int>(l1[0], "target2", isPublic: true);
        var portals = l1.Select(info =>
            new int[] { t1.GetFrom(info), t2.GetFrom(info) }
                .Select(t => t >= 5 ? t + 1 : t).ToArray()) // Top-right corner
            .ToArray();
        var flatPortals = portals.SelectMany(i => i).ToArray();
        var distinctPortals = flatPortals.Distinct().ToArray();

        var qs = new List<QandA>(15);

        var args = new[] { "1 (Red)", "2 (Orange)", "3 (Yellow)", "4 (Green)", "5 (Blue)" };
        for (var layer = 0; layer < 5; layer++)
            qs.Add(makeQuestion(Question.LabyrinthPortalLocations, module, formatArgs: new[] { args[layer] }, correctAnswers: new[] { new Coord(6, 7, portals[layer][0]), new Coord(6, 7, portals[layer][1]) }, preferredWrongAnswers: distinctPortals.Select(i => new Coord(6, 7, i)).ToArray()));

        foreach (var p in distinctPortals)
        {
            var correct = new List<string>();
            for (var i = 0; i < 10; i++)
                if (flatPortals[i] == p)
                    correct.Add(args[i / 2]); // Integer division gives layer #
            if (correct.Distinct().Count() > 2)
                continue; // Don't have a question with less than 4 answers
            qs.Add(makeQuestion(Question.LabyrinthPortalStage, module, questionSprite: Sprites.GenerateGridSprite(new Coord(6, 7, p)), correctAnswers: correct.Distinct().ToArray()));
        }

        addQuestions(module, qs);
    }
}