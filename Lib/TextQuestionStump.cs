using System;
using UnityEngine;

namespace Souvenir;

public class TextQuestionStump(Enum enumValue, SouvenirModule souvenir, string[] args, Sprite questionSprite, float questionSpriteRotation) : QuestionStump(enumValue, souvenir)
{
    public string[] Args { get; } = args;
    public Sprite QuestionSprite { get; } = questionSprite;
    public float QuestionSpriteRotation { get; } = questionSpriteRotation;

    public override QuestionBase MakeQuestion(string moduleFormat, SouvenirModule souvenir)
    {
        var attr = EnumValue.GetQuestionAttribute();
        var allFormatArgs = new object[Args != null ? Args.Length + 1 : 1];
        allFormatArgs[0] = moduleFormat;

        if (Args != null)
            for (var i = 0; i < Args.Length; i++)
                allFormatArgs[i + 1] = attr.TranslateArgs is { } ta && ta[i] ? souvenir.TranslateFormatArg(EnumValue, Args[i]) : Args[i];

        return new TextQuestion(string.Format(souvenir.TranslateQuestion(EnumValue), allFormatArgs), attr.Layout, QuestionSprite, QuestionSpriteRotation);
    }
}
