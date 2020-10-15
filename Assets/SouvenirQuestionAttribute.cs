using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Souvenir
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class SouvenirQuestionAttribute : Attribute
    {
        public string QuestionText { get; private set; }
        public string ModuleName { get; private set; }
        public string[] AllAnswers { get; private set; }
        public AnswerGeneratorAttribute AnswerGenerator { get; internal set; }

        public string[] ExampleExtraFormatArguments { get; set; }
        public int ExampleExtraFormatArgumentGroupSize { get; set; }
        public bool AddThe { get; set; }
        public string[] ExampleAnswers { get; set; }
        public AnswerType Type { get; set; }
        public AnswerLayout Layout { get; set; }
        public string SpriteField { get; set; }

        public string ModuleNameWithThe { get { return (AddThe ? "The " : "") + ModuleName; } }

        public int NumAnswers
        {
            get
            {
                switch (Layout)
                {
                    case AnswerLayout.TwoColumns4Answers: return 4;
                    case AnswerLayout.ThreeColumns6Answers: return 6;
                    case AnswerLayout.OneColumn4Answers: return 4;
                    case AnswerLayout.OneColumn2Answers: return 2;
                    default: throw new InvalidOperationException("Unexpected AnswerLayout value.");
                }
            }
        }

        public SouvenirQuestionAttribute(string questionText, string moduleName, AnswerLayout layout, params string[] allAnswers)
        {
            QuestionText = questionText;
            ModuleName = moduleName;
            Layout = layout;
            AllAnswers = allAnswers == null || allAnswers.Length == 0 ? null : allAnswers;
            Type = AnswerType.Default;
        }
    }
}
