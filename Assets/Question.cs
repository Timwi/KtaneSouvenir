using System;

namespace Souvenir
{
    enum Question
    {
        [SouvenirQuestion("What were the markings in {0}?", "3D Maze", 6, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
        _3DMazeMarkings,

        [SouvenirQuestion("Which of these letters was wrong when you got {1} strike in {0}?", "Adjacent Letters", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "your first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdjacentLettersWrong,

        [SouvenirQuestion("What was your {1} before you took the potion in {0}?", "Adventure Game", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13",
            ExampleExtraFormatArguments = new[] { "strength", "intelligence", "dexterity" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGamePotion,

        [SouvenirQuestion("Which item was the {1} correct item you used in {0}?", "Adventure Game", 4, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
            ExampleExtraFormatArguments = new[] { "first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGameCorrectItem,

        [SouvenirQuestion("Using which item gave you {1} strike in {0}?", "Adventure Game", 4, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
            ExampleExtraFormatArguments = new[] { "your first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGameWrongItem,

        [SouvenirQuestion("What was the {1} query response from {0}?", "Two Bits", 6, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99",
            ExampleExtraFormatArguments = new[] { "first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TwoBitsResponse,

        [SouvenirQuestion("What were your button presses in {0}, including strikes?", "Bulb", 6, null, AddThe = true)]
        TheBulbButtonPresses,
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class SouvenirQuestionAttribute : Attribute
    {
        public string QuestionText { get; private set; }
        public string ModuleName { get; private set; }
        public int NumAnswers { get; private set; }
        public string[] AllAnswers { get; private set; }

        public string[] ExampleExtraFormatArguments { get; set; }
        public int ExampleExtraFormatArgumentGroupSize { get; set; }
        public bool AddThe { get; set; }

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
        public abstract string DebugString { get; }
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
        public override string DebugString { get { return string.Format("{0} (answers: {1}, correct: {2}, unleashAt: {3})", QuestionText, string.Join(", ", Answers), CorrectIndex, UnleashAt); } }
    }
}
