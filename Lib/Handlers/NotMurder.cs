using System.Collections.Generic;
using System.Linq;
using Souvenir;
using static Souvenir.AnswerLayout;

public enum SNotMurder
{
    [SouvenirQuestion("What room was {1} in initially on {0}?", TwoColumns4Answers, "Ballroom", "Billiard Room", "Conservatory", "Dining Room", "Hall", "Kitchen", "Library", "Lounge", "Study", TranslateAnswers = true, Arguments = ["Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Room,

    [SouvenirQuestion("What weapon did {1} possess initially on {0}?", TwoColumns4Answers, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner", TranslateAnswers = true, Arguments = ["Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Weapon,

    [SouvenirDiscriminator("the Not Murder where {0} was present", Arguments = ["he", "she"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    Present,

    [SouvenirDiscriminator("the Not Murder where {0} initially held the {1}", Arguments = ["he", "Candlestick", "he", "Dagger", "she", "Lead Pipe", "she", "Revolver"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    InitialWeapon,

    [SouvenirDiscriminator("the Not Murder where {0} started in the {1}", Arguments = ["he", "Ballroom", "he", "Billiard Room", "she", "Conservatory", "she", "Dining Room"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    InitialRoom
}

public partial class SouvenirModule
{
    [SouvenirHandler("notMurder", "Not Murder", typeof(SNotMurder), "Quinn Wuest")]
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

        var suspectNames = new[] { "Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White" };
        var weaponNames = new[] { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner" };
        var roomNames = new[] { "Ballroom", "Billiard Room", "Conservatory", "Dining Room", "Hall", "Kitchen", "Library", "Lounge", "Study" };
        var suspectIsFemale = new[] { true, false, false, true, false, true };

        for (var i = 0; i < 5; i++)
        {
            var suspect = dispinfo[0][i];
            var initialRoom = turns[0][i][0];
            var initialWeapon = turns[0][i][1];

            yield return new Discriminator(SNotMurder.Present, $"present{suspect}", true, [suspectIsFemale[suspect] ? "he" : "she"]);
            yield return new Discriminator(SNotMurder.InitialWeapon, $"weapon{suspect}", initialWeapon, [suspectIsFemale[suspect] ? "he" : "she", weaponNames[initialWeapon]]);
            yield return new Discriminator(SNotMurder.InitialRoom, $"room{suspect}", initialRoom, [suspectIsFemale[suspect] ? "he" : "she", roomNames[initialRoom]]);

            var avoidDiscriminators = Enumerable.Range(0, 5).Except([i]).SelectMany(i => new[] { $"present{i}", $"weapon{i}", $"room{i}" }).ToArray();

            yield return question(SNotMurder.Room, args: [suspectNames[suspect]]).AvoidDiscriminators(avoidDiscriminators.Concat([$"room{i}"])).Answers(roomNames[initialRoom]);
            yield return question(SNotMurder.Weapon, args: [suspectNames[suspect]]).AvoidDiscriminators(avoidDiscriminators.Concat([$"weapon{i}"])).Answers(weaponNames[initialWeapon]);
        }
    }
}
