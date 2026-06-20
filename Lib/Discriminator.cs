using System;

namespace Souvenir;

public class Discriminator
{
    public Enum EnumValue { get; }
    public string Id { get; }
    public object Value { get; }
    public string[] Arguments { get; }
    public QuestionExtra QuestionExtra { get; }
    public object[] AvoidAnswers { get; }

    public bool AvoidEntirely { get; set; }
    public int Priority { get; set; }
    public Func<Enum, int> PriorityFromQuestion { get; set; }
    public Func<Enum, string[]> ArgumentsFromQuestion { get; set; }

    public Discriminator(Enum enumValue, string id, object value = null, string[] args = null, QuestionExtra questionExtra = null, object[] avoidAnswers = null)
    {
        var type = Ut.GetDiscriminatorAttribute(enumValue).QuestionExtraType;
        if (!(type switch
        {
            InfoType.None => questionExtra == null,
            InfoType.Audio => false,
            InfoType.Sprites => questionExtra is QuestionExtraSprite,
            InfoType.DynamicFont => questionExtra is QuestionExtraText { Text: not null, Font: not null, FontTexture: not null },
            _ => questionExtra is QuestionExtraText { Text: not null, Font: null, FontTexture: null }
        }))
            throw new InvalidOperationException($"A discriminator ({enumValue.GetType().Name}.{enumValue}) was constructed where the question extra ({(questionExtra == null ? "null" : questionExtra.GetType())}) does not match the QuestionExtraType on the attribute ({type}).");

        EnumValue = enumValue;
        Id = id;
        Value = value;
        Arguments = args;
        QuestionExtra = questionExtra;
        AvoidAnswers = avoidAnswers;
    }

    public override string ToString() => $"Discriminator {EnumValue}/{Id}={Value.Stringify()} {(ArgumentsFromQuestion != null ? "(dynamic)" : Arguments.Stringify())}{(QuestionExtra == null ? "" : $", {QuestionExtra}")}{(AvoidAnswers is { Length: > 0 } a ? $"; avoid answers: {a.Stringify()}" : "")}{(PriorityFromQuestion != null || Priority != 0 ? $" (P={(PriorityFromQuestion != null ? "dynamic" : Priority.Stringify())})" : "")}";
}
