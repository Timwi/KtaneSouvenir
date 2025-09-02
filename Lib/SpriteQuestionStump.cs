using System;
using UnityEngine;

namespace Souvenir;

public class SpriteQuestionStump(Enum question, SouvenirModule souvenir, Sprite sprite) : QuestionStump(question, souvenir)
{
    public Sprite Sprite { get; } = sprite;

    public override QuestionBase MakeQuestion(string moduleFormat, SouvenirModule souvenir) =>
        new SpriteQuestion(souvenir.TranslateQuestion(EnumValue), Sprite);
}
