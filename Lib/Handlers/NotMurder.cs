using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SNotMurder
{
    [SouvenirQuestion("What room was {1} in initially on {0}?", TwoColumns4Answers, "Ballroom", "Billiard Room", "Conservatory", "Dining Room", "Hall", "Kitchen", "Library", "Lounge", "Study", TranslateAnswers = true, Arguments = ["Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White", ], ArgumentGroupSize = 1, TranslateFormatArgs = [true], TranslatableStrings = ["the Not Murder where he initially held the {0}", "the Not Murder where she initially held the {0}", "the Not Murder where he started in the {0}", "the Not Murder where she started in the {0}", "the Not Murder where he was present", "the Not Murder where she was present", "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner", "Ballroom", "Billiard Room", "Conservatory", "Dining Room", "Hall", "Kitchen", "Library", "Lounge", "Study"])]
    Room,
    
    [SouvenirQuestion("What weapon did {1} possess initially on {0}?", TwoColumns4Answers, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner", TranslateAnswers = true, Arguments = ["Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White", ], ArgumentGroupSize = 1, TranslateFormatArgs = [true])]
    Weapon
}

public partial class SouvenirModule
{
    [SouvenirHandler("notMurder", "Not Murder", typeof(SNotMurder), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessNotMurder(ModuleData module)
    {
        while (!_isActivated)
            yield return null;

        var comp = GetComponent(module, "NMurScript");

        // whats displayed
        var dispinfo = GetArrayField<List<int>>(comp, "dispinfo").Get(expectedLength: 3).Select(i => i.ToArray()).ToArray();

        // turn number, then suspect, then room/weapon
        var turns = GetListField<List<int[]>>(comp, "turns").Get(expectedLength: 6);

        var data = Enumerable.Range(0, 5).Select(i => (suspect: dispinfo[0][i], room: turns[0][i][0], weapon: turns[0][i][1])).ToArray();
        _notMurderInfo.Add(data);

        yield return WaitForSolve;

        var suspectNames = new[] { "Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White" };
        var weaponNames = new[] { "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner" };
        var roomNames = new[] { "Ballroom", "Billiard Room", "Conservatory", "Dining Room", "Hall", "Kitchen", "Library", "Lounge", "Study" };
        var suspectIsFemale = new[] { true, false, false, true, false, true };

        var qs = new List<QandA>();

        for (var i = 0; i < 5; i++)
        {
            string dRoom = null, dWeapon = null;
            if (Rnd.Range(0, 3) != 0)
            {
                if (_notMurderInfo.Count(n => n.Any(t => t.suspect == data[i].suspect)) == 1)
                    dRoom = dWeapon = translateString(Question.NotMurderRoom, suspectIsFemale[data[i].suspect] ? "the Not Murder where she was present" : "the Not Murder where he was present");
                else
                {
                    if (_notMurderInfo.Count(n => n.Any(t => t.suspect == data[i].suspect && t.weapon == data[i].weapon)) == 1)
                        dRoom = string.Format(translateString(Question.NotMurderRoom, suspectIsFemale[data[i].suspect] ? "the Not Murder where she initially held the {0}" : "the Not Murder where he initially held the {0}"), translateString(Question.NotMurderRoom, weaponNames[data[i].weapon]));
                    if (_notMurderInfo.Count(n => n.Any(t => t.suspect == data[i].suspect && t.room == data[i].room)) == 1)
                        dWeapon = string.Format(translateString(Question.NotMurderRoom, suspectIsFemale[data[i].suspect] ? "the Not Murder where she started in the {0}" : "the Not Murder where he started in the {0}"), translateString(Question.NotMurderRoom, roomNames[data[i].room]));
                }
            }
            qs.Add(makeQuestion(Question.NotMurderRoom, module, formattedModuleName: dRoom, formatArgs: new[] { suspectNames[data[i].suspect] }, correctAnswers: new[] { roomNames[data[i].room] }));
            qs.Add(makeQuestion(Question.NotMurderWeapon, module, formattedModuleName: dWeapon, formatArgs: new[] { suspectNames[data[i].suspect] }, correctAnswers: new[] { weaponNames[data[i].weapon] }));
        }
        addQuestions(module, qs);
    }
}