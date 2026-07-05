using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SFollowingOrders
{
    [Question("What was the {1} shout in the last sequence of shouts in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, AudioFieldName = "FollowingOrdersAudio", AnswerType = InfoType.Audio)]
    Shouts,

    [Question("What was the starting position in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites)]
    [AnswerGenerator.Grid(5, 5)]
    Start
}

public partial class SouvenirModule
{
    [Handler("FollowingOrders", "Following Orders", typeof(SFollowingOrders), "Espik")]
    [ManualQuestion("What was the last sequence of shouts?")]
    [ManualQuestion("What was the starting position?")]
    private IEnumerator<SouvenirInstruction> ProcessFollowingOrders(ModuleData module)
    {
        var comp = GetComponent(module, "FollowingOrders");

        var fldPosition = GetArrayField<int>(comp, "position");
        var fldCanMove = GetField<bool>(comp, "canMove");
        var switchObject = GetField<KMSelectable>(comp, "Switch", isPublic: true).Get();

        var foundStartPos = 0;
        var shoutsGenerated = false;

        IEnumerator GetStartPos()
        {
            yield return null;

            while (!fldCanMove.Get())
                yield return null;

            var foundPosition = fldPosition.Get(expectedLength: 2);
            foundStartPos = foundPosition[1] * 5 + foundPosition[0];
        }

        StartCoroutine(GetStartPos());

        module.Module.OnStrike += delegate
        {
            StartCoroutine(GetStartPos());
            return false;
        };

        switchObject.OnInteract += delegate ()
        {
            if (fldCanMove.Get() && !module.IsSolved)
                shoutsGenerated = true;

            return false;
        };

        yield return WaitForSolve;

        yield return question(SFollowingOrders.Start).Answers(new Coord(5, 5, foundStartPos));

        if (!GetField<bool>(comp, "isUnicorn").Get() && shoutsGenerated)
        {
            var shouts = GetField<IList>(comp, "shouts").Get(x => x.Count != 5 ? "expected length 5" : null);
            var fldVoice = GetField<string>(shouts[0], "voice");
            var fldDirection = GetField<string>(shouts[0], "direction");
            var fldPresent = GetField<bool>(shouts[0], "present");

            var voices = shouts.Cast<object>().Select(x => fldVoice.GetFrom(x)).ToArray();
            var directions = shouts.Cast<object>().Select(x => fldDirection.GetFrom(x)).ToArray();
            var shoutsPresent = shouts.Cast<object>().Select(x => fldPresent.GetFrom(x)).ToArray();

            for (var i = 0; i < 5; i++)
                if (shoutsPresent[i])
                {
                    var wholeShout = voices[i] + directions[i];
                    yield return question(SFollowingOrders.Shouts, args: [Ordinal(i + 1)]).Answers(FollowingOrdersAudio.First(x => x.name == wholeShout), all: FollowingOrdersAudio);
                }
        } 
    }
}
