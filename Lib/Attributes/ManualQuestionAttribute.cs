using System;

namespace Souvenir;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
public sealed class ManualQuestionAttribute(string questionText) : Attribute
{
    public string QuestionText { get; } = questionText;
}
