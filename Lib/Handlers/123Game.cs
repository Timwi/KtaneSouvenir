using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum S123Game
{
    [SouvenirQuestion("Who was the opponent in {0}?", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    Profile,

    [SouvenirQuestion("Who was the opponent in {0}?", ThreeColumns6Answers, "Changyeop", "Eunji", "Gura", "Jinho", "Jungmoon", "Junseok", "Kyungran", "Minseo", "Minsoo", "Poong", "Sangmin", "Sunggyu", "Yuram")]
    Name
}

public partial class SouvenirModule
{
    [SouvenirHandler("TheOneTwoThreeGame", "1, 2, 3 Game", typeof(S123Game), "Anonymous", AddThe = true)]
    private IEnumerator<SouvenirInstruction> Process123Game(ModuleData module)
    {
        var comp = GetComponent(module, "TheOneTwoThreeGame");
        yield return WaitForSolve;

        GetField<SpriteRenderer>(comp, "Players", isPublic: true).Get().gameObject.SetActive(false);

        var sprites = GetArrayField<Sprite>(comp, "PlayersSprites", isPublic: true).Get(expectedLength: 12).TranslateSprites(1666).ToArray();
        var names = GetArrayField<string>(comp, "Names").Get(expectedLength: 13);

        var sprite = GetField<int>(comp, "ProfileSelector").Get(v => v is < 0 or >= 12 ? "expected sprite index 0–11" : null);
        var name = GetField<int>(comp, "NameSelector").Get(v => v is < 0 or >= 13 ? "expected name index 0–12" : null);

        yield return question(S123Game.Profile).Answers(sprites[sprite], all: sprites);
        yield return question(S123Game.Name).Answers(names[name]);
    }
}
