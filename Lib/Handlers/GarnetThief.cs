using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SGarnetThief
{
    [SouvenirQuestion("Which faction did {1} claim to be in {0}?", TwoColumns4Answers, "Mafia", "Cartel", "Beggar", "Police", Arguments = ["Jungmoon", "Yeonseung", "Jinho", "Dongmin", "Kyunghoon", "Kyungran", "Yoohyun", "Junseok", "Sangmin", "Yohwan", "Yoonsun", "Hyunmin", "Junghyun"], ArgumentGroupSize = 1)]
    Claim
}

public partial class SouvenirModule
{
    [SouvenirHandler("theGarnetThief", "Garnet Thief", typeof(SGarnetThief), "Hawker", AddThe = true)]
    private IEnumerator<SouvenirInstruction> ProcessGarnetThief(ModuleData module)
    {
        var comp = GetComponent(module, "TheGarnetThiefScript");

        var qs = new List<QandA>();

        module.Module.OnPass += () =>
        {
            var contestants = GetField<Array>(comp, "contestants").Get(arr => arr.Length != 7 ? "expected length 7" : null) as object[];
            var fldLying = GetField<bool>(contestants[0], "lying", isPublic: true);
            var fldName = GetField<Enum>(contestants[0], "name", isPublic: true);
            var fldClaimedFaction = GetField<Enum>(contestants[0], "claimedFaction", isPublic: true);

            foreach (var cont in contestants)
                fldLying.SetTo(cont, true);

            GetArrayField<Color>(comp, "frameColors").Set(Enumerable.Repeat(Color.gray, 4).ToArray());
            GetArrayField<Sprite>(comp, "allFactionIcons", isPublic: true).Set(new Sprite[4]);

            qs.AddRange(Enumerable.Range(0, 7).Select(i => makeQuestion(
                question: Question.GarnetThiefClaim,
                data: module,
                formatArgs: new[] { fldName.GetFrom(contestants[i]).ToString() },
                correctAnswers: new[] { fldClaimedFaction.GetFrom(contestants[i]).ToString() })));
            return false;
        };

        yield return WaitForSolve;
        addQuestions(module, qs);
    }
}