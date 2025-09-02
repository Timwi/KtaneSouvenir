using System;
using System.Reflection;
using UnityEngine;

namespace Souvenir;

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class SouvenirQuestionAttribute(string questionText, AnswerLayout layout, params string[] allAnswers) : Attribute
{
    public string QuestionText { get; private set; } = questionText;
    public string[] AllAnswers { get; private set; } = allAnswers == null || allAnswers.Length == 0 ? null : allAnswers;
    public AnswerGeneratorAttribute[] AnswerGenerators { get; internal set; }

    public string[] Arguments { get; set; }
    public int ArgumentGroupSize { get; set; }
    public bool TranslateAnswers { get; set; }
    public bool[] TranslateArgs { get; set; }
    public string[] TranslatableStrings { get; set; }
    public bool UsesQuestionSprite { get; set; }
    public string[] ExampleAnswers { get; set; }
    public AnswerType Type { get; set; } = AnswerType.Default;
    public AnswerLayout Layout { get; set; } = layout;
    public string ForeignAudioID { get; set; }
    public float AudioSizeMultiplier { get; set; } = 2f;
    public int FontSize { get; set; } = layout == AnswerLayout.OneColumn4Answers ? 40 : 48;
    public float CharacterSize { get; set; } = 1;
    public bool IsEntireQuestionSprite { get; set; }

    private FieldInfo getField(string name, Type expectedFieldType)
    {
        var field = typeof(SouvenirModule).GetField(name, BindingFlags.Instance | BindingFlags.Public);
        return field?.FieldType == expectedFieldType ? field : throw new InvalidOperationException($"The field ‘{name}’ is not of expected type ‘{expectedFieldType.FullName}’.");
    }

    public string SpriteFieldName { get; set; }
    private FieldInfo _spriteFieldCache;
    public FieldInfo SpriteField => _spriteFieldCache ??= getField(SpriteFieldName, typeof(Sprite[]));

    public string AudioFieldName { get; set; }
    private FieldInfo _audioFieldCache;
    public FieldInfo AudioField => _audioFieldCache ??= getField(AudioFieldName, typeof(AudioClip[]));

    public int NumAnswers => Layout switch
    {
        AnswerLayout.OneColumn3Answers => 3,
        AnswerLayout.OneColumn4Answers => 4,
        AnswerLayout.TwoColumns2Answers => 2,
        AnswerLayout.TwoColumns4Answers => 4,
        AnswerLayout.ThreeColumns3Answers => 3,
        AnswerLayout.ThreeColumns6Answers => 6,
        _ => throw new InvalidOperationException("Unexpected AnswerLayout value."),
    };
}
