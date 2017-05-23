using System;
using System.Linq;

namespace Souvenir
{
    enum Question
    {
        [SouvenirQuestion("What were the markings in {0}?", "3D Maze", 6, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
        _3DMazeMarkings,

        [SouvenirQuestion("What was the cardinal direction in {0}?", "3D Maze", 4, "North", "South", "West", "East")]
        _3DMazeBearing,

        [SouvenirQuestion("What was your {1} before you took the potion in {0}?", "Adventure Game", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13",
            ExampleExtraFormatArguments = new[] { "strength", "intelligence", "dexterity" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGamePotion,

        [SouvenirQuestion("Which item was the {1} correct item you used in {0}?", "Adventure Game", 4, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGameCorrectItem,

        [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", "Bitmaps", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16",
            ExampleExtraFormatArguments = new[] { "white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 2)]
        Bitmaps,

        [SouvenirQuestion("What was the {1} correct button you pressed in {0}?", "Broken Buttons", 6, "bomb", "blast", "boom", "burst", "wire", "button", "module", "light", "led", "switch", "RJ-45", "DVI-D", "RCA", "PS/2", "serial", "port", "row", "column", "one", "two", "three", "four", "five", "six", "seven", "eight", "size", "this", "that", "other", "submit", "abort", "drop", "thing", "blank", "broken", "too", "to", "yes", "see", "sea", "c", "wait", "word", "bob", "no", "not", "first", "hold", "late", "fail",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BrokenButtons,

        [SouvenirQuestion("What was the {1}paid amount in {0}?", "Cheap Checkout", 6, ExampleAnswers = new[] { "$0.01", "$12.34", "$84.72", "$1.11", "$2.50", "$24.56" },
            ExampleExtraFormatArguments = new[] { "", "first ", "second " }, ExampleExtraFormatArgumentGroupSize = 1)]
        CheapCheckoutPaid,

        [SouvenirQuestion("What was the {1} coordinate in {0}?", "Chess", 6, "a1", "a2", "a3", "a4", "a5", "a6", "b1", "b2", "b3", "b4", "b5", "b6", "c1", "c2", "c3", "c4", "c5", "c6", "d1", "d2", "d3", "d4", "d5", "d6", "e1", "e2", "e3", "e4", "e5", "e6", "f1", "f2", "f3", "f4", "f5", "f6",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChessCoordinate,

        [SouvenirQuestion("What was the first color group in {0}?", "Colored Squares", 4, "White", "Red", "Blue", "Green", "Yellow", "Magenta")]
        ColoredSquares,

        [SouvenirQuestion("What were the initial colors on {0} in reading order?", "Connection Check", 6, "RRRR", "RRRG", "RRGR", "RRGG", "RGRR", "RGRG", "RGGR", "RGGG", "GRRR", "GRRG", "GRGR", "GRGG", "GGRR", "GGRG", "GGGR", "GGGG")]
        ConnectionCheckInitial,

        [SouvenirQuestion("What was the initial value displayed on {0}?", "Double-Oh", 6, "60", "15", "57", "36", "83", "48", "71", "24", "88", "46", "31", "70", "22", "64", "55", "13", "74", "27", "53", "41", "18", "86", "30", "62", "52", "10", "43", "85", "37", "61", "28", "76", "33", "65", "78", "21", "56", "12", "44", "87", "47", "81", "26", "68", "14", "72", "50", "35", "38", "42", "84", "63", "20", "75", "17", "51", "25", "73", "67", "16", "58", "34", "82", "40", "11", "54", "80", "32", "77", "45", "23", "66")]
        DoubleOh,

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

        [SouvenirQuestion("What was the {1} received letter in {0}?", "Morsematics", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MorsematicsReceivedLetters,

        [SouvenirQuestion("What color was the torus in {0}?", "Mouse in the Maze", 4, "white", "green", "blue", "yellow")]
        MouseInTheMazeTorus,

        [SouvenirQuestion("Which color sphere was the goal in {0}?", "Mouse in the Maze", 4, "white", "green", "blue", "yellow")]
        MouseInTheMazeSphere,

        [SouvenirQuestion("Where was the body found in {0}?", "Murder", 4, "Dining Room", "Study", "Kitchen", "Lounge", "Billiard Room", "Conservatory", "Ballroom", "Hall", "Library")]
        MurderBodyFound,

        [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", 4, "Miss Scarlett", "Professor Plum", "Mrs Peacock", "Reverend Green", "Colonel Mustard", "Mrs White",
            ExampleExtraFormatArguments = new[] { "a suspect but not the murderer", "not a suspect" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MurderSuspect,

        [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", 4, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner",
            ExampleExtraFormatArguments = new[] { "a potential weapon but not the murder weapon", "not a potential weapon" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MurderWeapon,

        [SouvenirQuestion("What was the acid’s color in {0}?", "Neutralization", 4, "Yellow", "Green", "Red", "Blue")]
        NeutralizationColor,

        [SouvenirQuestion("What was the acid’s volume in {0}?", "Neutralization", 6, "0", "5", "10", "15", "20", "25")]
        NeutralizationVolume,

        [SouvenirQuestion("Which Egyptian hieroglyph was in the {1} in {0}?", "Only Connect", 4, "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus",
            ExampleExtraFormatArguments = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        OnlyConnectHieroglyphs,

        [SouvenirQuestion("What was the observer’s intial position in {0}?", "Orientation Cube", 4, "front", "left", "back", "right")]
        OrientationCubeInitialObserverPosition,

        [SouvenirQuestion("Which peg was {1} in the solution to {0}?", "Perspective Pegs", 4, "top", "top right", "bottom right", "bottom left", "top left",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PerspectivePegsSolution,

        [SouvenirQuestion("What were the 1st and 2nd word in the {1} phrase in {0}?", "Sea Shells", 4, "she sells", "she shells", "sea shells", "sea sells")]
        SeaShells1,

        [SouvenirQuestion("What were the 3rd and 4th word in the {1} phrase in {0}?", "Sea Shells", 4, "sea shells", "she shells", "sea sells", "she sells")]
        SeaShells2,

        [SouvenirQuestion("What was the end of the {1} phrase in {0}?", "Sea Shells", 4, "sea shore", "she sore", "she sure", "seesaw")]
        SeaShells3,

        [SouvenirQuestion("What was the {1} slot in the {2} stage in {0}?", "Silly Slots", 4, "red bomb", "red cherry", "red coin", "red grape", "green bomb", "green cherry", "green coin", "green grape", "blue bomb", "blue cherry", "blue coin", "blue grape",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third", "third", "first", "third", "second", "third", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SillySlots,

        [SouvenirQuestion("Which {1} in the {2} stage in {0}?", "Simon States", 4, "Red", "Yellow", "Green", "Blue", "Red, Yellow", "Red, Green", "Red, Blue", "Yellow, Green", "Yellow, Blue", "Green, Blue", "all 4", "none",
            ExampleExtraFormatArguments = new[] { "color(s) flashed", "first", "color(s) didn’t flash", "first", "color(s) flashed", "second", "color(s) didn’t flash", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStatesDisplay,

        [SouvenirQuestion("What were the {1}original numbers in {0}?", "Skewed Slots", 6, ExampleAnswers = new[] { "123", "847", "000", "245", "961", "253", "858" },
            ExampleExtraFormatArguments = new[] { "", "first ", "second ", "third " }, ExampleExtraFormatArgumentGroupSize = 1)]
        SkewedSlotsOriginalNumbers,

        [SouvenirQuestion("What were the correct button presses in {0}?", "Bulb", 6, "OOO", "OOI", "OIO", "OII", "IOO", "IOI", "IIO", "III", AddThe = true)]
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
        public string DebugString { get { return string.Format("{0} — {1} — unleashAt={2}", QuestionText, Answers.Select((a, ix) => string.Format(ix == CorrectIndex ? "[_{0}_]" : "{0}", a)).JoinString(", "), UnleashAt); } }
    }
}
