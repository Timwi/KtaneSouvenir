using System;
using UnityEngine;

namespace Souvenir;

public class TextQuestionStump(Enum enumValue, SouvenirModule souvenir, string[] args, Sprite questionSprite, float questionSpriteRotation) : QuestionStump(enumValue, souvenir)
{
    public Sprite QuestionSprite { get; } = questionSprite;
    public float QuestionSpriteRotation { get; } = questionSpriteRotation;

    public override QuestionBase MakeQuestion(string moduleFormat, SouvenirModule souvenir) =>
        new TextQuestion(FormattedQuestion(moduleFormat), EnumValue.GetQuestionAttribute().Layout, QuestionSprite, QuestionSpriteRotation);
}
