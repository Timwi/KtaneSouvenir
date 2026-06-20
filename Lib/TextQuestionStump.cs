using System;

namespace Souvenir;

public class TextQuestionStump : QuestionStump
{
    public QuestionExtra QuestionExtra { get; }

    public TextQuestionStump(Enum enumValue, SouvenirModule souvenir, string[] args, QuestionExtra questionExtra) : base(enumValue, souvenir, args)
    {
        var type = Ut.GetQuestionAttribute(enumValue).QuestionExtraType;
        if (!(type switch
        {
            InfoType.None => questionExtra == null,
            InfoType.Audio => false,
            InfoType.Sprites => questionExtra is QuestionExtraSprite,
            InfoType.DynamicFont => questionExtra is QuestionExtraText { Text: not null, Font: not null, FontTexture: not null },
            _ => questionExtra is QuestionExtraText { Text: not null, Font: null, FontTexture: null }
        }))
            throw new InvalidOperationException($"A question ({enumValue.GetType().Name}.{enumValue}) was constructed where the question extra ({(questionExtra == null ? "null" : questionExtra.GetType())}) does not match the QuestionExtraType on the attribute ({type}).");

        QuestionExtra = questionExtra;
    }

    public override QuestionBase MakeQuestion(string moduleFormat, QuestionExtra questionExtraFromDiscriminator) => new TextQuestion(
        question: FormattedQuestion(moduleFormat),
        layout: QuestionAttribute.Layout,
        questionExtra: questionExtraFromDiscriminator ?? QuestionExtra?.Uplift(Souvenir, QuestionAttribute.QuestionExtraType));

    public override string ToString() => $"{base.ToString()}{(QuestionExtra == null ? "" : $", {QuestionExtra}")}";
}
