using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SElderFuthark
{
    [Question("What was the {1} rune shown on {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, AnswerType = InfoType.Sprites, SpriteFieldName = "ElderFutharkSprites")]
    Runes,

    [Discriminator("the Elder Futhark that had this rune on it", QuestionExtraType = InfoType.Sprites)]
    Discriminator
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

        var runeCharacters = new string[] { "Ansuz", "Berkana", "Kenaz", "Dagaz", "Ehwaz", "Fehu", "Gebo", "Hagalaz", "Isa", "Jera", "Eihwaz", "Laguz", "Mannaz", "Nauthiz", "Othila", "Perthro", "Algiz", "Raido", "Sowulo", "Teiwaz", "Uruz", "Wunjo", "Thurisaz" };

        var pickedRunes = pickedRuneNames.Select(x => ElderFutharkSprites[Array.IndexOf(runeCharacters, x)]).ToArray();

        for (var i = 0; i < pickedRunes.Length; i++)
        {
            yield return question(SElderFuthark.Runes, args: [Ordinal(i + 1)]).Answers(pickedRunes[i]);
            yield return new Discriminator(SElderFuthark.Discriminator, $"futhark-{pickedRuneNames[i]}", questionExtra: pickedRunes[i]);
        }
    }
}
