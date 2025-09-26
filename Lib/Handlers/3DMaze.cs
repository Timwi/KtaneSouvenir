using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum S3DMaze
{
    [SouvenirQuestion("What were the markings in {0}?", ThreeColumns6Answers, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
    QMarkings,

    [SouvenirQuestion("What was the cardinal direction in {0}?", TwoColumns4Answers, "North", "South", "West", "East", TranslateAnswers = true)]
    QBearing,

    [SouvenirDiscriminator("the 3D Maze whose markings were {0}", Arguments = ["ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH"], ArgumentGroupSize = 1)]
    DMarkings,

    [SouvenirDiscriminator("the 3D Maze whose cardinal direction was {0}", Arguments = ["North", "South", "West", "East"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    DBearing,
}

public partial class SouvenirModule
{
    [SouvenirHandler("spwiz3DMaze", "3D Maze", typeof(S3DMaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> Process3DMaze(ModuleData module)
    {
        var comp = GetComponent(module, "ThreeDMazeModule");

        yield return WaitForActivate;

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
        var cardinal = new[] { "North", "East", "South", "West" }[bearing];
        yield return new Discriminator(S3DMaze.DMarkings, "markings", correctMarkings, args: [correctMarkings]);
        yield return new Discriminator(S3DMaze.DBearing, "cardinal", cardinal, args: [cardinal]);
        yield return question(S3DMaze.QMarkings).AvoidDiscriminators(S3DMaze.DMarkings).Answers(correctMarkings);
        yield return question(S3DMaze.QBearing).AvoidDiscriminators(S3DMaze.DBearing).Answers(cardinal);
    }
}
