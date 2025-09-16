using System;
using UnityEngine;

namespace Souvenir;

public class Discriminator(Enum discriminator, string id, object value, string[] args = null, Sprite questionSprite = null, object[] avoidAnswers = null)
{
    public Enum EnumValue { get; } = discriminator;
    public string Id { get; } = id;
    public object Value { get; } = value;
    public string[] Arguments { get; } = args;
    public Sprite QuestionSprite { get; } = questionSprite;
    public object[] AvoidAnswers { get; } = avoidAnswers;

    public int Priority { get; set; }
    public Func<Enum, int> PriorityFromQuestion { get; set; }
    public Func<Enum, string[]> ArgumentsFromQuestion { get; set; }

    public override string ToString() => $"Discriminator {EnumValue}/{Id}={Value.Stringify()}, {(Arguments ?? ArgumentsFromQuestion?.Invoke(null)).Stringify()}{(QuestionSprite == null ? "" : $", question sprite={QuestionSprite}")}, avoid answers: {AvoidAnswers.Stringify()}{(Priority != 0 ? $" (P{Priority})" : "")}{(PriorityFromQuestion != null ? $" (PQ)" : "")}";
}
