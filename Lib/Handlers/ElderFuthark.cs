using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SElderFuthark
{
    [Question("What was the {1} rune shown on {0}?", ThreeColumns6Answers, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "y", "l", "m", "n", "o", "p", "z", "r", "s", "t", "u", "v", "x", Type = AnswerType.ElderRuneFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Runes
}

public partial class SouvenirModule
{
    [Handler("elderFuthark", "Elder Futhark", typeof(SElderFuthark), "Goofy")]
    [ManualQuestion("What were the runes shown?")]
    private IEnumerator<SouvenirInstruction> ProcessElderFuthark(ModuleData module)
    {
        var comp = GetComponent(module, "ElderFutharkScript");
        yield return WaitForSolve;

        var pickedRuneNames = GetArrayField<string>(comp, "pickedRuneNames").Get(expectedLength: 3);

        var runeCharacters = new Dictionary<string, string>
        {
            ["Ansuz"] = "a",
            ["Berkana"] = "b",
            ["Kenaz"] = "c",
            ["Dagaz"] = "d",
            ["Ehwaz"] = "e",
            ["Fehu"] = "f",
            ["Gebo"] = "g",
            ["Hagalaz"] = "h",
            ["Isa"] = "i",
            ["Jera"] = "j",
            ["Eihwaz"] = "y",
            ["Laguz"] = "l",
            ["Mannaz"] = "m",
            ["Nauthiz"] = "n",
            ["Othila"] = "o",
            ["Perthro"] = "p",
            ["Algiz"] = "z",
            ["Raido"] = "r",
            ["Sowulo"] = "s",
            ["Teiwaz"] = "t",
            ["Uruz"] = "u",
            ["Wunjo"] = "v",
            ["Thurisaz"] = "x",
        };

        var pickedRunes = pickedRuneNames.Select(x => runeCharacters[x]).ToArray();

        for (var i = 0; i < pickedRunes.Length; i++)
            yield return question(SElderFuthark.Runes, args: [Ordinal(i + 1)]).Answers(pickedRunes[i], preferredWrong: pickedRunes);
    }
}
