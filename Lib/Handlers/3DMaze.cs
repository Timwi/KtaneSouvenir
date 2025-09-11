using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum S3DMaze
{
    [SouvenirQuestion("What were the markings in {0}?", ThreeColumns6Answers, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
    Markings,
    
    [SouvenirQuestion("What was the cardinal direction in {0}?", TwoColumns4Answers, "North", "South", "West", "East", TranslateAnswers = true)]
    Bearing
}

public partial class SouvenirModule
{
    [SouvenirHandler("spwiz3DMaze", "3D Maze", typeof(S3DMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> Process3DMaze(ModuleData module)
    {
        var comp = GetComponent(module, "ThreeDMazeModule");

        while (!_isActivated)
            yield return new WaitForSeconds(.1f);

        var map = GetField<object>(comp, "map").Get();
        var mapData = GetField<Array>(map, "mapData").Get(arr => arr.GetLength(0) != 8 || arr.GetLength(1) != 8 ? $"size {arr.GetLength(0)},{arr.GetLength(1)}, expected 8,8" : null);

        var bearing = GetIntField(map, "end_dir").Get(min: 0, max: 3);
        var fldLabel = GetField<char>(mapData.GetValue(0, 0), "label", isPublic: true);
        var chars = new HashSet<char>();
        for (var i = 0; i < 8; i++)
            for (var j = 0; j < 8; j++)
            {
                var ch = fldLabel.GetFrom(mapData.GetValue(i, j));
                if ("ABCDH".Contains(ch))
                    chars.Add(ch);
            }
        var correctMarkings = chars.OrderBy(c => c).JoinString();

        yield return WaitForSolve;
        addQuestions(module,
            makeQuestion(Question._3DMazeMarkings, module, correctAnswers: new[] { correctMarkings }),
            makeQuestion(Question._3DMazeBearing, module, correctAnswers: new[] { new[] { "North", "East", "South", "West" }[bearing] }));
    }
}