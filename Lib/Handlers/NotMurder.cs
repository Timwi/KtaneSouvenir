using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SNotMurder
{
    [Question("Who was the first suspect in the sequence in {0}?", TwoColumns4Answers, "Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White")]
    FirstSuspect,

    [Question("What was the first weapon in the sequence in {0}?", TwoColumns4Answers, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner")]
    FirstWeapon,

    [Question("What was the first room in the sequence in {0}?", TwoColumns4Answers, "Ballroom", "Billiard Room", "Conservatory", "Dining Room", "Hall", "Kitchen", "Library", "Lounge", "Study")]
    FirstRoom
}

public partial class SouvenirModule
{
    [Handler("notMurder", "Not Murder", typeof(SNotMurder), "Espik")]
    [ManualQuestion("What were the first suspect, weapon, and room in the sequence?")]
    private IEnumerator<SouvenirInstruction> ProcessNotMurder(ModuleData module)
    {
        while (!_isActivated)
            yield return null;

        var comp = GetComponent(module, "NMurScript");

        // what’s displayed
        var dispinfo = GetArrayField<List<int>>(comp, "dispinfo").Get(expectedLength: 3).Select(i => i.ToArray()).ToArray();

        // turn number, then suspect, then room/weapon
        var turns = GetListField<List<int[]>>(comp, "turns").Get(expectedLength: 6);

        yield return WaitForSolve;

        yield return question(SNotMurder.FirstSuspect).Answers(SNotMurder.FirstSuspect.GetAnswers()[dispinfo[0][0]]);
        yield return question(SNotMurder.FirstWeapon).Answers(SNotMurder.FirstWeapon.GetAnswers()[turns[0][0][1]]);
        yield return question(SNotMurder.FirstRoom).Answers(SNotMurder.FirstRoom.GetAnswers()[turns[0][0][0]]);
    }
}
