using System;
using UnityEngine;

namespace Souvenir;

public class SpriteQuestionStump(Enum question, SouvenirModule souvenir, string[] args, Sprite sprite) : QuestionStump(question, souvenir, args)
{
    public Sprite Sprite { get; } = sprite;

    public override QuestionBase MakeQuestion(string moduleFormat, Sprite questionSpriteFromDiscriminator, float questionSpriteRotationFromDiscriminator) =>
        new SpriteQuestion(FormattedQuestion(moduleFormat), Sprite);
}
