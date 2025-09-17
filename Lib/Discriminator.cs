using System;
using UnityEngine;

namespace Souvenir;

public class Discriminator(Enum discriminator, string id, object value = null, string[] args = null, Sprite questionSprite = null, float questionSpriteRotation = 0f, object[] avoidAnswers = null)
{
    public Enum EnumValue { get; } = discriminator;
    public string Id { get; } = id;
    public object Value { get; } = value;
    public string[] Arguments { get; } = args;
    public Sprite QuestionSprite { get; } = questionSprite;
    public float QuestionSpriteRotation { get; } = questionSpriteRotation;
    public object[] AvoidAnswers { get; } = avoidAnswers;

    public int Priority { get; set; }
    public Func<Enum, int> PriorityFromQuestion { get; set; }
    public Func<Enum, string[]> ArgumentsFromQuestion { get; set; }

    public override string ToString() => $"Discriminator {EnumValue}/{Id}={Value.Stringify()}, {(Arguments ?? ArgumentsFromQuestion?.Invoke(null)).Stringify()}{(QuestionSprite == null ? "" : $", question sprite={QuestionSprite}{(QuestionSpriteRotation != 0 ? $" (rot {QuestionSpriteRotation})" : "")}")}, avoid answers: {AvoidAnswers.Stringify()}{(Priority != 0 ? $" (P{Priority})" : "")}{(PriorityFromQuestion != null ? $" (PQ)" : "")}";
}
