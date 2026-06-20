using System;
using UnityEngine;

namespace Souvenir;

public class SpriteQuestionStump(Enum question, SouvenirModule souvenir, string[] args, Sprite sprite) : QuestionStump(question, souvenir, args)
{
    public Sprite Sprite { get; } = sprite;

    public override QuestionBase MakeQuestion(string moduleFormat, QuestionExtra questionExtraFromDiscriminator)
    {
        if (questionExtraFromDiscriminator != null)
            throw new InvalidOperationException($"A full-sprite question (for {EnumValue.GetType().Name}.{EnumValue}) was created with a question extra ({questionExtraFromDiscriminator}), which is not allowed.");
        return new SpriteQuestion(FormattedQuestion(moduleFormat), Sprite);
    }

    public override string ToString() => $"{base.ToString()} full-question sprite: {Sprite.name}";
}
