using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum S123Game
{
    [Question("What was the opponent avatar in {0}?", ThreeColumns6Answers, AnswerType = InfoType.Sprites)]
    QProfile,

    [Question("What was the opponent name in {0}?", TwoColumns4Answers, "Changyeop", "Eunji", "Gura", "Jinho", "Jungmoon", "Junseok", "Kyungran", "Minseo", "Minsoo", "Poong", "Sangmin", "Sunggyu", "Yuram")]
    QName,

    [Discriminator("the 1, 2, 3 Game with this opponent avatar", QuestionExtraType = InfoType.Sprites)]
    DProfile,

    [Discriminator("the 1, 2, 3 Game with the opponent name {0}", Arguments = ["Changyeop", "Eunji", "Gura", "Jinho", "Jungmoon", "Junseok", "Kyungran", "Minseo", "Minsoo", "Poong", "Sangmin", "Sunggyu"], ArgumentGroupSize = 1)]
    DName
}

public partial class SouvenirModule
{
    [Handler("TheOneTwoThreeGame", "1, 2, 3 Game", typeof(S123Game), "Anonymous", AddThe = true)]
    [ManualQuestion("What was the opponent’s avatar and name?")]
    private IEnumerator<SouvenirInstruction> Process123Game(ModuleData module)
    {
        var comp = GetComponent(module, "TheOneTwoThreeGame");
        yield return WaitForSolve;

        GetField<SpriteRenderer>(comp, "Players", isPublic: true).Get().gameObject.SetActive(false);
        GetArrayField<SpriteRenderer>(comp, "LastPlayed", isPublic: true).Get(expectedLength: 2)[0].gameObject.SetActive(false);

        var sprites = GetArrayField<Sprite>(comp, "PlayersSprites", isPublic: true).Get(expectedLength: 12).TranslateSprites(1666).ToArray();
        var names = GetArrayField<string>(comp, "Names").Get(expectedLength: 13);

        var sprite = GetField<int>(comp, "ProfileSelector").Get(v => v is < 0 or >= 12 ? "expected sprite index 0–11" : null);
        var name = GetField<int>(comp, "NameSelector").Get(v => v is < 0 or >= 13 ? "expected name index 0–12" : null);

        yield return question(S123Game.QProfile).AvoidDiscriminators(S123Game.DProfile).Answers(sprites[sprite], all: sprites);
        yield return question(S123Game.QName).AvoidDiscriminators(S123Game.DName).Answers(names[name]);
        yield return new Discriminator(S123Game.DProfile, "123profile", sprite, questionExtra: sprites[sprite]);
        yield return new Discriminator(S123Game.DName, "123name", sprite, args: [names[name]]);
    }
}
