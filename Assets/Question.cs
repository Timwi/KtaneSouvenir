using System;

namespace Souvenir
{
    enum Question
    {
        [SouvenirQuestion("What were the markings in {0}?", "3D Maze", 6, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
        _3DMazeMarkings,
        [SouvenirQuestion("Which of these letters was wrong when you got {1} strike in {0}?", "Adjacent Letters", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        AdjacentLettersWrong,
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class SouvenirQuestionAttribute : Attribute
    {
        public string QuestionText { get; private set; }
        public string ModuleName { get; private set; }
        public int NumAnswers { get; private set; }
        public string[] AllAnswers { get; private set; }

        public SouvenirQuestionAttribute(string questionText, string moduleName, int numAnswers, params string[] allAnswers)
        {
            QuestionText = questionText;
            ModuleName = moduleName;
            NumAnswers = numAnswers;
            AllAnswers = allAnswers;
        }
    }

    abstract class QuestionBase
    {
        public string QuestionText { get; private set; }
        public int UnleashAt { get; private set; }
        protected QuestionBase(string question, int unleashAt) { QuestionText = question; UnleashAt = unleashAt; }
    }

    sealed class QuestionText : QuestionBase
    {
        public string[] Answers { get; private set; }
        public int CorrectIndex { get; private set; }
        public QuestionText(string question, string[] answers, int correct, int unleashAt) : base(question, unleashAt)
        {
            Answers = answers;
            CorrectIndex = correct;
        }
    }
}
