using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSkyrim
{
    [SouvenirQuestion("Which race was selectable, but not the solution, in {0}?", TwoColumns4Answers, "Nord", "Khajiit", "Breton", "Argonian", "Dunmer", "Altmer", "Redguard", "Orc", "Imperial", TranslateAnswers = true)]
    Race,

    [SouvenirQuestion("Which weapon was selectable, but not the solution, in {0}?", TwoColumns4Answers, "Axe of Whiterun", "Dawnbreaker", "Windshear", "Blade of Woe", "Firiniel’s End", "Bow of Hunt", "Volendrung", "Chillrend", "Mace of Molag Bal", TranslateAnswers = true)]
    Weapon,

    [SouvenirQuestion("Which enemy was selectable, but not the solution, in {0}?", TwoColumns4Answers, "Alduin", "Blood Dragon", "Cave Bear", "Dragon Priest", "Draugr", "Draugr Overlord", "Frost Troll", "Frostbite Spider", "Mudcrab", TranslateAnswers = true)]
    Enemy,

    [SouvenirQuestion("Which city was selectable, but not the solution, in {0}?", TwoColumns4Answers, "Dawnstar", "Ivarstead", "Markarth", "Riverwood", "Rorikstead", "Solitude", "Whiterun", "Windhelm", "Winterhold", TranslateAnswers = true)]
    City,

    [SouvenirQuestion("Which dragon shout was selectable, but not the solution, in {0}?", TwoColumns4Answers, "Disarm", "Dismay", "Dragonrend", "Fire Breath", "Ice Form", "Kyne’s Peace", "Slow Time", "Unrelenting Force", "Whirlwind Sprint", TranslateAnswers = true)]
    DragonShout
}

public partial class SouvenirModule
{
    [SouvenirHandler("skyrim", "Skyrim", typeof(SSkyrim), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSkyrim(ModuleData module)
    {
        var comp = GetComponent(module, "skyrimScript");

        yield return WaitForSolve;

        foreach (var fieldName in new[] { "cycleUp", "cycleDown", "accept", "submit", "race", "weapon", "enemy", "city", "shout" })
        {
            var btn = GetField<KMSelectable>(comp, fieldName, isPublic: true).Get();
            btn.OnInteract = delegate
            {
                Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, btn.transform);
                btn.AddInteractionPunch(.5f);
                return false;
            };
        }
        var questions = new[] { SSkyrim.Race, SSkyrim.Weapon, SSkyrim.Enemy, SSkyrim.City };
        var fieldNames = new[] { "race", "weapon", "enemy", "city" };
        var flds = fieldNames.Select(name => GetListField<Texture>(comp, name + "Images", isPublic: true)).ToArray();
        var fldsCorrect = new[] { "correctRace", "correctWeapon", "correctEnemy", "correctCity" }.Select(name => GetField<Texture>(comp, name)).ToArray();
        for (var i = 0; i < fieldNames.Length; i++)
        {
            var list = flds[i].Get(expectedLength: 3);
            var correct = fldsCorrect[i].Get();
            qs.Add(makeQuestion(questions[i], module, correctAnswers: list.Except(new[] { correct }).Select(t => t.name.Replace("'", "’")).ToArray()));
        }
        var shoutNames = GetListField<string>(comp, "shoutNameOptions").Get(expectedLength: 3);
        yield return question(SSkyrim.DragonShout).Answers(shoutNames.Except(new[] { GetField<string>(comp, "shoutName").Get() }).Select(n => n.Replace("'", "’")).ToArray());
    }
}