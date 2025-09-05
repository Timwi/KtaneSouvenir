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
    public bool[] TranslateArguments { get; set; }

    public override string ToString() => $"Discriminator {EnumValue}/{Id}={Value.Stringify()}, {Arguments.Stringify()}{(QuestionSprite == null ? "" : ", uses question sprite")}, avoid answers: {AvoidAnswers.Stringify()}";
}
