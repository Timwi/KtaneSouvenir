using System;

namespace Souvenir;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class SouvenirDiscriminatorAttribute(string discriminatorText) : Attribute
{
    public string DiscriminatorText { get; private set; } = discriminatorText;

    public string[] Arguments { get; set; }
    public int ArgumentGroupSize { get; set; }
    public bool[] TranslateArgs { get; set; }
    public string[] TranslatableStrings { get; set; }
    public bool UsesQuestionSprite { get; set; }
}
