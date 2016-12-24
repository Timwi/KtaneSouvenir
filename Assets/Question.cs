using System;

namespace Souvenir
{
    enum Question
    {
        [SouvenirQuestion("What were the markings in {0}?", "3D Maze", 6, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
        _3DMazeMarkings,

        [SouvenirQuestion("What was the cardinal direction in {0}?", "3D Maze", 4, "North", "South", "West", "East")]
        _3DMazeBearing,

        [SouvenirQuestion("Which one of these letters was wrong when you got {1} strike in {0}?", "Adjacent Letters", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "a", "your first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdjacentLettersWrong,

        [SouvenirQuestion("What was your {1} before you took the potion in {0}?", "Adventure Game", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13",
            ExampleExtraFormatArguments = new[] { "strength", "intelligence", "dexterity" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGamePotion,

        [SouvenirQuestion("Which item was the {1} correct item you used in {0}?", "Adventure Game", 4, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGameCorrectItem,

        [SouvenirQuestion("Using which item gave you {1} strike in {0}?", "Adventure Game", 4, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
            ExampleExtraFormatArguments = new[] { "a", "your first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGameWrongItem,

        [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", "Bitmaps", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16",
            ExampleExtraFormatArguments = new[] { "white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 2)]
        Bitmaps,

        [SouvenirQuestion("What was the {1} coordinate in {0}?", "Chess", 6, "a1", "a2", "a3", "a4", "a5", "a6", "b1", "b2", "b3", "b4", "b5", "b6", "c1", "c2", "c3", "c4", "c5", "c6", "d1", "d2", "d3", "d4", "d5", "d6", "e1", "e2", "e3", "e4", "e5", "e6", "f1", "f2", "f3", "f4", "f5", "f6",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChessCoordinate,

        [SouvenirQuestion("What were the initial colors on {0} in reading order?", "Connection Check", 6, "RRRR", "RRRG", "RRGR", "RRGG", "RGRR", "RGRG", "RGGR", "RGGG", "GRRR", "GRRG", "GRGR", "GRGG", "GGRR", "GGRG", "GGGR", "GGGG")]
        ConnectionCheckInitial,

        [SouvenirQuestion("What were the colors on {0} when you got {1} strike?", "Connection Check", 6, "RRRR", "RRRG", "RRGR", "RRGG", "RGRR", "RGRG", "RGGR", "RGGG", "GRRR", "GRRG", "GRGR", "GRGG", "GGRR", "GGRG", "GGGR", "GGGG",
            ExampleExtraFormatArguments = new[] { "a", "your first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ConnectionCheckStrike,

        [SouvenirQuestion("What is the {1}-stage displayed number in {2}?", "Forget Me Not", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "first", "Forget Me Not", "second", "the Forget Me Not whose first-stage displayed number was 5" }, ExampleExtraFormatArgumentGroupSize = 2)]
        ForgetMeNot,

        [SouvenirQuestion("What was the color of the pawn in {0}?", "Hexamaze", 4, "Red", "Yellow", "Green", "Cyan", "Blue", "Pink")]
        HexamazePawnColor,

        [SouvenirQuestion("What was the correct code you entered in {0}?", "Listening", 4, null,
            ExampleAnswers = new[] { "&&&**", "&$#$&", "$#$*&", "#$$**", "$#$#*", "**$*#", "#$$&*", "##*$*", "$#*$&", "**#**", "#&&*#", "&#**&", "$&**#", "&#$$#", "$&&**", "#&$##", "&*$*$", "&$$&*", "#&&&&", "**$$$", "*&*&&", "*#&*&", "**###", "&&$&*", "&$**&", "#$#&$", "&#&&#", "$$*$*", "$&#$$", "&**$$", "$&&*&", "&$&##", "#&$*&", "$*$**", "*#$&&", "###&$", "*$$&$", "$*&##", "#&$&&", "$&$$*", "*$*$*" })]
        Listening,

        [SouvenirQuestion("Which creature was displayed {1}in {0}?", "Monsplode, Fight!", 4, "Caadarim", "Buhar", "Melbor", "Lanaluff", "Bob", "Mountoise", "Aluga", "Nibs", "Zapra", "Zenlad", "Vellarim", "Ukkens", "Lugirit", "Flaurim", "Myrchat", "Clondar", "Gloorim", "Docsplode", "Magmy", "Pouse", "Asteran", "Violan",
            ExampleExtraFormatArguments = new[] { "", "first ", "second ", "third " }, ExampleExtraFormatArgumentGroupSize = 1)]
        MonsplodeFightCreature,

        [SouvenirQuestion("Which one of these moves {1} selectable {2}in {0}?", "Monsplode, Fight!", 4, "Tic", "Tac", "Toe", "Hollow Gaze", "Splash", "Heavy Rain", "Fountain", "Candle", "Torchlight", "Flame Spear", "Tangle", "Grass Blade", "Ivy Spikes", "Spectre", "Boo", "Battery Power", "Zap", "Double Zap", "Shock", "High Voltage", "Dark Portal", "Last Word", "Void", "Boom", "Fiery Soul", "Stretch", "Shrink", "Appearify", "Sendify", "Freak Out", "Glyph", "Bug Spray", "Bedrock", "Earthquake", "Cave In", "Toxic Waste", "Venom Fang", "Countdown",
            ExampleExtraFormatArguments = new[] { "was", "", "was", "for the first creature ", "was", "for the second creature ", "was not", "", "was not", "for the first creature ", "was not", "for the second creature " }, ExampleExtraFormatArgumentGroupSize = 2)]
        MonsplodeFightMove,

        [SouvenirQuestion("What color was the torus in {0}?", "Mouse in the Maze", 4, "white", "green", "blue", "yellow")]
        MouseInTheMazeTorus,

        [SouvenirQuestion("Which color sphere was the goal in {0}?", "Mouse in the Maze", 4, "white", "green", "blue", "yellow")]
        MouseInTheMazeSphere,

        [SouvenirQuestion("What was the observer’s intial position in {0}?", "Orientation Cube", 4, "front", "left", "back", "right")]
        OrientationCubeInitialObserverPosition,

        [SouvenirQuestion("Which peg was {1} in the solution to {0}?", "Perspective Pegs", 4, "top", "top right", "bottom right", "bottom left", "top left",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PerspectivePegsSolution,

        [SouvenirQuestion("What was the {1} slot in the {2} stage in {0}?", "Silly Slots", 4, "red bomb", "red cherry", "red coin", "red grape", "green bomb", "green cherry", "green coin", "green grape", "blue bomb", "blue cherry", "blue coin", "blue grape",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third", "third", "first", "third", "second", "third", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SillySlots,

        [SouvenirQuestion("Which {1} in the {2} stage in {0}?", "Simon States", 4, "Red", "Yellow", "Green", "Blue", "Red, Yellow", "Red, Green", "Red, Blue", "Yellow, Green", "Yellow, Blue", "Green, Blue", "all 4", "none",
            ExampleExtraFormatArguments = new[] { "color(s) flashed", "first", "color(s) didn’t flash", "first", "color(s) flashed", "second", "color(s) didn’t flash", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStatesDisplay,

        [SouvenirQuestion("What were your button presses in {0}{1}?", "Bulb", 6, ExampleAnswers = new[] { "OOO", "OOI", "OIO", "OII", "IOO", "IOI", "IIO", "III", "IOOO", "IOOI", "IOIO", "IOII", "IIOO", "IIOI", "IIIO", "IIII" },
            ExampleExtraFormatArguments = new[] { "", ", including strikes" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        TheBulbButtonPresses,

        [SouvenirQuestion("What was the {1} query response from {0}?", "Two Bits", 6, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99",
            ExampleExtraFormatArguments = new[] { "first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TwoBitsResponse,
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
        public string[] ExampleAnswers { get; set; }

        public SouvenirQuestionAttribute(string questionText, string moduleName, int numAnswers, params string[] allAnswers)
        {
            QuestionText = questionText;
            ModuleName = moduleName;
            NumAnswers = numAnswers;
            AllAnswers = allAnswers == null || allAnswers.Length == 0 ? null : allAnswers;
        }
    }

    sealed class QandA
    {
        public string QuestionText { get; private set; }
        public string[] Answers { get; private set; }
        public int CorrectIndex { get; private set; }
        public int UnleashAt { get; private set; }
        public QandA(string question, string[] answers, int correct, int unleashAt)
        {
            QuestionText = question;
            Answers = answers;
            CorrectIndex = correct;
            UnleashAt = unleashAt;
        }
        public string DebugString { get { return string.Format("{0} (answers: {1}, correct: {2}, unleashAt: {3})", QuestionText, string.Join(", ", Answers), CorrectIndex, UnleashAt); } }
    }
}
