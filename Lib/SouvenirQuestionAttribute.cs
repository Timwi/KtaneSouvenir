﻿using System;

namespace Souvenir
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class SouvenirQuestionAttribute : Attribute
    {
        public string QuestionText { get; private set; }
        public string ModuleName { get; private set; }
        public string[] AllAnswers { get; private set; }
        public AnswerGeneratorAttribute AnswerGenerator { get; internal set; }

        public string[] ExampleFormatArguments { get; set; }
        public int ExampleFormatArgumentGroupSize { get; set; }
        public bool AddThe { get; set; }
        public bool TranslateAnswers { get; set; }
        public bool[] TranslateFormatArgs { get; set; }
        public string[] TranslatableStrings { get; set; }
        public bool UsesQuestionSprite { get; set; }
        public string[] ExampleAnswers { get; set; }
        public AnswerType Type { get; set; }
        public AnswerLayout Layout { get; set; }
        public string SpriteField { get; set; }
        public string AudioField { get; set; }
        public string ForeignAudioID { get; set; }
        public float AudioSizeMultiplier { get; set; } = 2f;
        public int FontSize { get; set; }
        public float CharacterSize { get; set; } = 1;
        public bool IsEntireQuestionSprite { get; set; }

        public string ModuleNameWithThe => (AddThe ? "The\u00a0" : "") + ModuleName;

        public int NumAnswers => Layout switch
        {
            AnswerLayout.TwoColumns4Answers => 4,
            AnswerLayout.ThreeColumns6Answers => 6,
            AnswerLayout.OneColumn4Answers => 4,
            _ => throw new InvalidOperationException("Unexpected AnswerLayout value."),
        };

        public SouvenirQuestionAttribute(string questionText, string moduleName, AnswerLayout layout, params string[] allAnswers)
        {
            QuestionText = questionText;
            ModuleName = moduleName;
            Layout = layout;
            AllAnswers = allAnswers == null || allAnswers.Length == 0 ? null : allAnswers;
            Type = AnswerType.Default;
            FontSize = layout == AnswerLayout.OneColumn4Answers ? 40 : 48;
        }
    }
}
