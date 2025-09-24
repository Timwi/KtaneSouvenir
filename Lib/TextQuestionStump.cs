using System;
using UnityEngine;

namespace Souvenir;

public class TextQuestionStump(Enum enumValue, SouvenirModule souvenir, string[] args, Sprite questionSprite, float questionSpriteRotation) : QuestionStump(enumValue, souvenir, args)
{
    public Sprite QuestionSprite { get; } = questionSprite;
    public float QuestionSpriteRotation { get; } = questionSpriteRotation;

    public override QuestionBase MakeQuestion(string moduleFormat, Sprite questionSpriteFromDiscriminator, float questionSpriteRotationFromDiscriminator) =>
        new TextQuestion(FormattedQuestion(moduleFormat), EnumValue.GetQuestionAttribute().Layout,
            questionSpriteFromDiscriminator ?? QuestionSprite,
            questionSpriteFromDiscriminator == null ? QuestionSpriteRotation : questionSpriteRotationFromDiscriminator);

    public override string ToString() => $"{base.ToString()}{(QuestionSprite == null ? "" : $" question sprite={QuestionSprite.name}{(QuestionSpriteRotation != 0 ? $" (rot {QuestionSpriteRotation})" : "")}")}";
}
