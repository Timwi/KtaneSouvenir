namespace Souvenir
{
    using UnityEngine;
    using static AnswerLayout;

    public enum Question
    {
        [SouvenirQuestion("What was the {1} word shown in {0}?", "1000 Words", ThreeColumns6Answers, null,
            ExampleAnswers = new[] { "Baken", "Ghost", "Tolts", "Oyers", "Sweel", "Rangy", "Noses", "Chapt", "Phuts", "Pingo", "Hylas", "Podia", "Vizor" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        _1000WordsWords,

        [SouvenirQuestion("What was the {1} displayed letter in {0}?", "100 Levels of Defusal", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        _100LevelsOfDefusalLetters,

        [SouvenirQuestion("What was {1} in {0}?", "1D Chess", ThreeColumns6Answers, "B a→c", "B b→d", "B c→a", "B c→e", "B d→b", "B d→f", "B e→c", "B e→g", "B f→d", "B f→h", "B g→e", "B g→i", "B h→f", "B i→g", "K a→b", "K b→a", "K b→c", "K c→b", "K c→d", "K d→c", "K d→e", "K e→d", "K e→f", "K f→e", "K f→g", "K g→f", "K g→h", "K h→g", "K h→i", "K i→h", "N a→c", "N b→d", "N c→a", "N c→e", "N d→b", "N d→f", "N e→c", "N e→g", "N f→d", "N f→h", "N g→e", "N g→i", "N h→f", "N i→g", "P a→b", "P a→c", "P b→a", "P b→c", "P b→d", "P c→a", "P c→b", "P c→d", "P c→e", "P d→b", "P d→c", "P d→e", "P d→f", "P e→c", "P e→d", "P e→f", "P e→g", "P f→d", "P f→e", "P f→g", "P f→h", "P g→e", "P g→f", "P g→h", "P g→i", "P h→f", "P h→g", "P i→g", "Q a→b", "Q a→c", "Q b→a", "Q b→c", "Q b→d", "Q c→a", "Q c→b", "Q c→d", "Q c→e", "Q d→b", "Q d→c", "Q d→e", "Q d→f", "Q e→c", "Q e→d", "Q e→f", "Q e→g", "Q f→d", "Q f→e", "Q f→g", "Q f→h", "Q g→e", "Q g→f", "Q g→h", "Q g→i", "Q h→f", "Q h→g", "Q i→g", "R a→b", "R b→a", "R b→c", "R c→b", "R c→d", "R d→c", "R d→e", "R e→d", "R e→f", "R f→e", "R f→g", "R g→f", "R g→h", "R h→g", "R h→i", "R i→h",
            ExampleExtraFormatArguments = new[] { "your first move", "Rustmate’s first move", "your second move", "Rustmate’s second move", "your third move", "Rustmate’s third move", "your fourth move", "Rustmate’s fourth move", "your fifth move", "Rustmate’s fifth move", "your sixth move", "Rustmate’s sixth move", "your seventh move", "Rustmate’s seventh move", "your eighth move", "Rustmate’s eighth move", }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        _1DChessMoves,

        [SouvenirQuestion("What were the markings in {0}?", "3D Maze", ThreeColumns6Answers, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
        _3DMazeMarkings,
        [SouvenirQuestion("What was the cardinal direction in {0}?", "3D Maze", TwoColumns4Answers, "North", "South", "West", "East", TranslateAnswers = true)]
        _3DMazeBearing,

        [SouvenirQuestion("What was the received word in {0}?", "3D Tap Code", ThreeColumns6Answers, null,
            ExampleAnswers = new[] { "Aback", "Backs", "Habit", "Oasis", "Unzip", "Vogue" })]
        _3DTapCodeWord,

        [SouvenirQuestion("What was the {1} goal node in {0}?", "3D Tunnels", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings("a-z.")]
        _3DTunnelsTargetNode,

        [SouvenirQuestion("What was the initial state of the LEDs in {0} (in reading order)?", "3 LEDs", TwoColumns4Answers, "off/off/off", "off/off/on", "off/on/off", "off/on/on", "on/off/off", "on/off/on", "on/on/off", "on/on/on")]
        _3LEDsInitialState,

        [SouvenirQuestion("What number was initially displayed in {0}?", "3N+1", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 100)]
        _3NPlus1,

        [SouvenirQuestion("What was the displayed number in {0}?", "64", ThreeColumns6Answers, Type = AnswerType.SixtyFourFont, ExampleAnswers = new[] { "A0A3", "bbda", "30", "h3X1", "ABCD", "1234" })]
        _64DisplayedNumber, // Use the font from the module because o and 0 are almost identical in the default font.

        [SouvenirQuestion("What was the {1} channel’s initial value in {0}?", "7", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(-9, 9)]
        _7InitialValues,
        [SouvenirQuestion("What LED color was shown in stage {1} of {0}?", "7", TwoColumns4Answers,
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3" }, ExampleExtraFormatArgumentGroupSize = 1, ExampleAnswers = new[] { "red", "blue", "green", "white" })]
        _7LedColors,

        [SouvenirQuestion("What was the number of ball {1} in {0}?", "9-Ball", ThreeColumns6Answers, ExampleAnswers = new[] { "2", "3", "4", "5", "6", "7" },
            ExampleExtraFormatArguments = new[] { "A", "B", "C", "D", "E", "F", "G" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(2, 8)]
        _9BallLetters,
        [SouvenirQuestion("What was the letter of ball {1} in {0}?", "9-Ball", ThreeColumns6Answers, ExampleAnswers = new[] { "A", "B", "C", "D", "E", "F" },
            ExampleExtraFormatArguments = new[] { "2", "3", "4", "5", "6", "7", "8" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-G")]
        _9BallNumbers,

        [SouvenirQuestion("What was the {1} character displayed on {0}?", "Abyss", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings(1, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")]
        AbyssSeed,

        [SouvenirQuestion("What was the background color on the {1} stage in {0}?", "Accumulation", ThreeColumns6Answers, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        AccumulationBackgroundColor,
        [SouvenirQuestion("What was the border color in {0}?", "Accumulation", ThreeColumns6Answers, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow", TranslateAnswers = true)]
        AccumulationBorderColor,

        [SouvenirQuestion("Which item was the {1} correct item you used in {0}?", "Adventure Game", TwoColumns4Answers, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGameCorrectItem,

        [SouvenirQuestion("What enemy were you fighting in {0}?", "Adventure Game", TwoColumns4Answers, "Dragon", "Demon", "Eagle", "Goblin", "Troll", "Wizard", "Golem", "Lizard")]
        AdventureGameEnemy,

        [SouvenirQuestion("What was the {1} in {0}?", "Affine Cycle", TwoColumns4Answers, "Advanced", "Addition", "Allocate", "Allotted", "Binaries", "Billions", "Bulkhead", "Bulwarks", "Ciphered", "Circuits", "Computer", "Compiler", "Decrypts", "Division", "Discover", "Discrete", "Encipher", "Entrance", "Equation", "Equalise", "Finished", "Findings", "Fortress", "Fortunes", "Gauntlet", "Gambling", "Gathered", "Gateways", "Hazarded", "Haziness", "Hunkered", "Hungrier", "Indicate", "Indigoes", "Illusion", "Illuding", "Jigsawed", "Jimmying", "Junction", "Juncture", "Kilowatt", "Kinetics", "Knockout", "Knowable", "Limiting", "Linearly", "Linkages", "Lingered", "Monogram", "Monotone", "Multiply", "Mulcting", "Nanogram", "Nanotube", "Numbered", "Numerate", "Octangle", "Octuples", "Observed", "Obstacle", "Progress", "Projects", "Position", "Positron", "Quadrant", "Quadrics", "Quickest", "Quitters", "Reversed", "Revolved", "Rotation", "Relative", "Starting", "Standard", "Stopping", "Stoccata", "Triggers", "Triangle", "Tomogram", "Tomorrow", "Underrun", "Underlie", "Ultimate", "Ultrahot", "Vicinity", "Viceless", "Voltages", "Voluming", "Wingding", "Winnable", "Whatever", "Whatsits", "Yellowed", "Yeasayer", "Yielders", "Yourself", "Zippered", "Zigzaggy", "Zugzwang", "Zymogene",
            ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        AffineCycleWord,

        [SouvenirQuestion("What was the initial letter in {0}?", "A Letter", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-Z")]
        ALetterInitialLetter,

        [SouvenirQuestion("Which letter was pressed in {0}?", "Alfa-Bravo", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        AlfaBravoPressedLetter,
        [SouvenirQuestion("Which letter was to the left of the pressed one in {0}?", "Alfa-Bravo", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        AlfaBravoLeftPressedLetter,
        [SouvenirQuestion("Which letter was to the right of the pressed one in {0}?", "Alfa-Bravo", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        AlfaBravoRightPressedLetter,
        [SouvenirQuestion("What was the last digit on the small display in {0}?", "Alfa-Bravo", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        AlfaBravoDigit,

        [SouvenirQuestion("What was the first equation in {0}?", "Algebra", TwoColumns4Answers, "a=3z", "a=5+y", "a=6-x", "a=7x", "a=8y", "a=9+z", "a=x/2", "a=x+1", "a=y/4", "a=y-2", "a=z/10", "a=z-7")]
        AlgebraEquation1,
        [SouvenirQuestion("What was the second equation in {0}?", "Algebra", TwoColumns4Answers, "b=(2x/10)-y", "b=(7x)y", "b=(x+y)-(z/2)", "b=(y/2)-z", "b=(zy)-(2x)", "b=(z-y)/2", "b=2(z+7)", "b=2z+7", "b=xy-(2+x)", "b=xyz")]
        AlgebraEquation2,

        [SouvenirQuestion("Which position was the {1} position in {0}?", "Algorithmia", ThreeColumns6Answers, null, Type = AnswerType.Grid, ExampleExtraFormatArguments = new[] { "starting", "goal" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Grid(4, 4)]
        AlgorithmiaPositions,
        [SouvenirQuestion("What was the color of the colored bulb in {0}?", "Algorithmia", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Yellow", "Magenta")]
        AlgorithmiaColor,
        [SouvenirQuestion("Which number was present in the seed in {0}?", "Algorithmia", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 99)]
        AlgorithmiaSeed,

        [SouvenirQuestion("What was the letter displayed in the {1} stage of {0}?", "Alphabetical Ruling", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings(1, 'A', 'Z')]
        AlphabeticalRulingLetter,
        [SouvenirQuestion("What was the number displayed in the {1} stage of {0}?", "Alphabetical Ruling", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 9)]
        AlphabeticalRulingNumber,

        [SouvenirQuestion("Which of these numbers was on one of the buttons in the {1} stage of {0}?", "Alphabet Numbers", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 32)]
        AlphabetNumbersDisplayedNumbers,

        [SouvenirQuestion("What was the {1} letter shown during the cycle in {0}?", "Alphabet Tiles", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        AlphabetTilesCycle,
        [SouvenirQuestion("What was the missing letter in {0}?", "Alphabet Tiles", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        AlphabetTilesMissingLetter,

        [SouvenirQuestion("What character was displayed on the {1} screen on the {2} in {0}?", "Alpha-Bits", ThreeColumns6Answers, TranslateFormatArgs = new[] { false, true },
            Type = AnswerType.DynamicFont, ExampleExtraFormatArguments = new[] { QandA.Ordinal, "left", QandA.Ordinal, "right" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Strings("0-9A-V")]
        AlphaBitsDisplayedCharacters,

        [SouvenirQuestion("What letter was shown by the raised buttons on the {1} stage on {0}?", "Ángel Hernández", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        AngelHernandezMainLetter,

        [SouvenirQuestion("What was the maximum weapon damage of the attack phase in {0}?", "Arena", ThreeColumns6Answers, AddThe = true)]
        [AnswerGenerator.Integers(1, 99)]
        ArenaDamage,
        [SouvenirQuestion("Which enemy was present in the defend phase of {0}?", "Arena", TwoColumns4Answers, "Bat", "Snake", "Spider", "Cobra", "Scorpion", "Mole", "Creeper", "Goblin", "Golem", "Robo-Mouse", "Skeleton", "Undead Guard", "The Reaper", "The Mole’s Dad", AddThe = true)]
        ArenaEnemies,
        [SouvenirQuestion("Which was a number present in the grab phase of {0}?", "Arena", ThreeColumns6Answers, AddThe = true)]
        [AnswerGenerator.Integers(10, 99)]
        ArenaNumbers,

        [SouvenirQuestion("What was the symbol on the submit button in {0}?", "Arithmelogic", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "ArithmelogicSprites")]
        ArithmelogicSubmit,
        [SouvenirQuestion("Which number was selectable, but not the solution, in the {1} screen on {0}?", "Arithmelogic", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "left", "middle", "right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(10, 40)]
        ArithmelogicNumbers,

        [SouvenirQuestion("What was the {1} character displayed on {0}?", "ASCII Maze", ThreeColumns6Answers, "NUL", "SOH", "STX", "ETX", "EOT", "ENQ", "ACK", "BEL", "BS", "HT", "LF", "VT", "FF", "CR", "SO", "SI", "DLE", "DC1", "DC2", "DC3", "DC4", "NAK", "SYN", "ETB", "CAN", "EM", "SUB", "ESC", "FS", "GS", "RS", "US", "(space)", "!", "\"", "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", "DEL", "Ç", "ü", "é", "â", "ä", "à", "å", "ç", "ê", "ë", "è", "ï", "î", "ì", "Ä", "Å", "É", "æ", "Æ", "ô", "ö", "ò", "û", "ù", "ÿ", "Ö", "Ü", "ø", "£", "Ø", "×", "ƒ", "á", "í", "ó", "ú", "ñ", "Ñ", "ª", "º", "¿", "®", "¬", "½", "¼", "¡", "«", "»", "░", "▒", "▓", "│", "┤", "Á", "Â", "À", "©", "╣", "║", "╗", "╝", "¢", "¥", "┐", "└", "┴", "┬", "├", "─", "┼", "ã", "Ã", "╚", "╔", "╩", "╦", "╠", "═", "╬", "¤", "ð", "Ð", "Ê", "Ë", "È", "ı", "Í", "Î", "Ï", "┘", "┌", "█", "▄", "¦", "Ì", "▀", "Ó", "ß", "Ô", "Ò", "õ", "Õ", "µ", "þ", "Þ", "Ú", "Û", "Ù", "ý", "Ý", "¯", "´", "­­\u2261", "±", "‗", "¾", "¶", "§", "÷", "¸", "°", "¨", "·", "¹", "³", "²", "■", "nbsp",
            Type = AnswerType.AsciiMazeFont, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ASCIIMazeCharacters,

        [SouvenirQuestion("Which of these was an index color in {0}?", "A Square", ThreeColumns6Answers, "Orange", "Pink", "Cyan", "Yellow", "Lavender", "Brown", "Tan", "Blue", "Jade", "Indigo", "White")]
        ASquareIndexColors,
        [SouvenirQuestion("Which color was submitted {1} in {0}?", "A Square", ThreeColumns6Answers, "Orange", "Pink", "Cyan", "Yellow", "Lavender", "Brown", "Tan", "Blue", "Jade", "Indigo", "White",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ASquareCorrectColors,

        [SouvenirQuestion("What was T in {0}?", "Azure Button", ThreeColumns6Answers, AddThe = true, Type = AnswerType.Sprites, SpriteField = "AzureButtonSprites")]
        AzureButtonT,
        [SouvenirQuestion("Which of these cards was shown in Stage 1, but not T, in {0}?", "Azure Button", ThreeColumns6Answers, AddThe = true, Type = AnswerType.Sprites, SpriteField = "AzureButtonSprites")]
        AzureButtonNotT,
        [SouvenirQuestion("What was M in {0}?", "Azure Button", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", AddThe = true)]
        AzureButtonM,
        [SouvenirQuestion("What was the {1} direction in the decoy arrow in {0}?", "Azure Button", TwoColumns4Answers, "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west",
            AddThe = true, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        AzureButtonDecoyArrowDirection,
        [SouvenirQuestion("What was the {1} direction in the {2} non-decoy arrow in {0}?", "Azure Button", TwoColumns4Answers, "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west",
            AddThe = true, ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        AzureButtonNonDecoyArrowDirection,

        [SouvenirQuestion("Which menu item was present in {0}?", "Bakery", OneColumn4Answers, null,
            ExampleAnswers = new[] { "Butter slab", "Sugar cookie", "Applie pie", "Tea biscuit", "Tuile", "Sprinkles Cookie" })]
        BakeryItems,

        [SouvenirQuestion("What color was the {1} correct button in {0}?", "Bamboozled Again", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainButtonColor,
        [SouvenirQuestion("What was the text on the {1} correct button in {0}?", "Bamboozled Again", TwoColumns4Answers, "THE LETTER", "ONE LETTER", "THE COLOUR", "ONE COLOUR", "THE PHRASE", "ONE PHRASE", "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "QUEBEC", "TANGO", "WHISKEY", "VICTOR", "YANKEE", "ECHO ECHO", "E THEN E", "ALPHA PAPA", "PAPA ALPHA", "PAPHA ALPA", "T GOLF", "TANGOLF", "WHISKEE", "WHISKY", "CHARLIE C", "C CHARLIE", "YANGO", "DELTA NEXT", "CUEBEQ", "MILO", "KI LO", "HI-LO", "VVICTOR", "VICTORR", "LIME BRAVO", "BLUE BRAVO", "G IN JADE", "G IN ROSE", "BLUE IN RED", "YES BUT NO", "COLOUR", "MESSAGE", "CIPHER", "BUTTON", "TWO BUTTONS", "SIX BUTTONS", "I GIVE UP", "ONE ELEVEN", "ONE ONE ONE", "THREE ONES", "WHAT?", "THIS?", "THAT?", "BLUE!", "ECHO!", "BLANK", "BLANK?!", "NOTHING", "YELLOW TEXT", "BLACK TEXT?", "QUOTE V", "END QUOTE", "\"QUOTE K\"", "IN RED", "ORANGE", "IN YELLOW", "LIME", "IN GREEN", "JADE", "IN CYAN", "AZURE", "IN BLUE", "VIOLET", "IN MAGENTA", "ROSE",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainButtonText,
        [SouvenirQuestion("What was the {1} decrypted text on the display in {0}?", "Bamboozled Again", TwoColumns4Answers, "THE LETTER", "ONE LETTER", "THE COLOUR", "ONE COLOUR", "THE PHRASE", "ONE PHRASE",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainDisplayTexts1,
        [SouvenirQuestion("What was the {1} decrypted text on the display in {0}?", "Bamboozled Again", TwoColumns4Answers, "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "QUEBEC", "TANGO", "WHISKEY", "VICTOR", "YANKEE", "ECHO ECHO", "E THEN E", "ALPHA PAPA", "PAPA ALPHA", "PAPHA ALPA", "T GOLF", "TANGOLF", "WHISKEE", "WHISKY", "CHARLIE C", "C CHARLIE", "YANGO", "DELTA NEXT", "CUEBEQ", "MILO", "KI LO", "HI-LO", "VVICTOR", "VICTORR", "LIME BRAVO", "BLUE BRAVO", "G IN JADE", "G IN ROSE", "BLUE IN RED", "YES BUT NO", "COLOUR", "MESSAGE", "CIPHER", "BUTTON", "TWO BUTTONS", "SIX BUTTONS", "I GIVE UP", "ONE ELEVEN", "ONE ONE ONE", "THREE ONES", "WHAT?", "THIS?", "THAT?", "BLUE!", "ECHO!", "BLANK", "BLANK?!", "NOTHING", "YELLOW TEXT", "BLACK TEXT?", "QUOTE V", "END QUOTE", "\"QUOTE K\"", "IN RED", "ORANGE", "IN YELLOW", "LIME", "IN GREEN", "JADE", "IN CYAN", "AZURE", "IN BLUE", "VIOLET", "IN MAGENTA", "ROSE",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainDisplayTexts2,
        [SouvenirQuestion("What color was the {1} text on the display in {0}?", "Bamboozled Again", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainDisplayColor,

        [SouvenirQuestion("What color was the button in the {1} stage of {0}?", "Bamboozling Button", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black", TranslateAnswers = true,
          ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozlingButtonColor,
        [SouvenirQuestion("What was the {2} label on the button in the {1} stage of {0}?", "Bamboozling Button", TwoColumns4Answers, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE", TranslateFormatArgs = new[] { false, true },
          ExampleExtraFormatArguments = new[] { QandA.Ordinal, "top", QandA.Ordinal, "bottom" }, ExampleExtraFormatArgumentGroupSize = 2)]
        BamboozlingButtonLabel,
        [SouvenirQuestion("What was the {2} display in the {1} stage of {0}?", "Bamboozling Button", TwoColumns4Answers, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE",
          ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        BamboozlingButtonDisplay,
        [SouvenirQuestion("What was the color of the {2} display in the {1} stage of {0}?", "Bamboozling Button", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", TranslateAnswers = true,
          ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        BamboozlingButtonDisplayColor,

        [SouvenirQuestion("What was the screen number in {0}?", "Barcode Cipher", OneColumn4Answers, "637842", "145612", "765465", "523987", "452387")]
        [AnswerGenerator.Integers(100000, 999999)]
        BarcodeCipherScreenNumber,
        [SouvenirQuestion("What was the edgework represented by the {1} barcode in {0}?", "Barcode Cipher", OneColumn4Answers, "SERIAL NUMBER", "BATTERIES", "BATTERY HOLDERS", "PORTS", "PORT PLATES", "LIT INDICATORS", "UNLIT INDICATORS", "INDICATORS",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BarcodeCipherBarcodeEdgework,
        [SouvenirQuestion("What was the answer for the {1} barcode in {0}?", "Barcode Cipher", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BarcodeCipherBarcodeAnswers,

        [SouvenirQuestion("Which ingredient was in the {1} position on {0}?", "Bartending", TwoColumns4Answers, "Adelhyde", "Flanergide", "Bronson Extract", "Karmotrine", "Powdered Delta",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BartendingIngredients,

        [SouvenirQuestion("What was this bean in {0}?", "Beans", OneColumn4Answers, "Wobbly Orange", "Wobbly Yellow", "Wobbly Green", "Not Wobbly Orange", "Not Wobbly Yellow", "Not Wobbly Green",
            UsesQuestionSprite = true, TranslateAnswers = true)]
        BeansColors,

        [SouvenirQuestion("What was sprout {1} in {0}?", "Bean Sprouts", TwoColumns4Answers,
            "Raw", "Cooked", "Burnt", "Fake",
            ExampleExtraFormatArgumentGroupSize = 1,
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" })]
        BeanSproutsColors,
        [SouvenirQuestion("What bean was on sprout {1} in {0}?", "Bean Sprouts", TwoColumns4Answers,
            "Left", "Right", "None", "Both",
            ExampleExtraFormatArgumentGroupSize = 1,
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" })]
        BeanSproutsBeans,

        [SouvenirQuestion("What was the bean in {0}?", "Big Bean", OneColumn4Answers, "Wobbly Orange", "Wobbly Yellow", "Wobbly Green", "Not Wobbly Orange", "Not Wobbly Yellow", "Not Wobbly Green", TranslateAnswers = true)]
        BigBeanColor,

        [SouvenirQuestion("What color was {1} in the solution to {0}?", "Big Circle", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BigCircleColors,

        [SouvenirQuestion("At which numeric value did you cut the correct wire in {0}?", "Binary LEDs", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 31)]
        BinaryLEDsValue,

        [SouvenirQuestion("What was the {1} initial number in {0}?", "Binary Shift", ThreeColumns6Answers, null, ExampleAnswers = new[] { "13", "14", "34", "46", "53", "64", "67", "77", "82", "96" },
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        BinaryShiftInitialNumber,
        [SouvenirQuestion("What number was selected at stage {1} in {0}?", "Binary Shift", ThreeColumns6Answers, null, ExampleAnswers = new[] { "bottom-left", "bottom-middle", "bottom-right", "center", "left-middle", "right-middle", "top-left", "top-middle", "top-right" },
            ExampleExtraFormatArguments = new[] { "0", "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BinaryShiftSelectedNumberPossition,
        [SouvenirQuestion("What number was not selected at stage {1} in {0}?", "Binary Shift", ThreeColumns6Answers, null, ExampleAnswers = new[] { "bottom-left", "bottom-middle", "bottom-right", "center", "left-middle", "right-middle", "top-left", "top-middle", "top-right" },
            ExampleExtraFormatArguments = new[] { "0", "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BinaryShiftNotSelectedNumberPossition,

        [SouvenirQuestion("What word was displayed in {0}?", "Binary", ThreeColumns6Answers, "ah", "at", "am", "as", "an", "be", "by", "go", "if", "in", "is", "it", "mu", "nu", "no", "nu", "of", "pi", "to", "up", "us", "we", "xi", "ace", "aim", "air", "bed", "bob", "but", "buy", "can", "cat", "chi", "cut", "day", "die", "dog", "dot", "eat", "eye", "for", "fly", "get", "gut", "had", "hat", "hot", "ice", "lie", "lit", "mad", "map", "may", "new", "not", "now", "one", "pay", "phi", "pie", "psi", "red", "rho", "sad", "say", "sea", "see", "set", "six", "sky", "tau", "the", "too", "two", "why", "win", "yes", "zoo", "alfa", "beta", "blue", "chat", "cyan", "demo", "door", "east", "easy", "each", "edit", "fail", "fall", "fire", "five", "four", "game", "golf", "grid", "hard", "hate", "help", "hold", "iota", "kilo", "lima", "lime", "list", "lock", "lost", "stop", "test", "time", "tree", "type", "west", "wire", "wood", "xray", "yell", "zero", "zeta", "zulu", "abort", "about", "alpha", "black", "bravo", "clock", "close", "could", "crash", "delta", "digit", "eight", "gamma", "glass", "green", "guess", "hotel", "india", "kappa", "later", "least", "lemon", "month", "morse", "north", "omega", "oscar", "panic", "press", "romeo", "seven", "sigma", "smash", "south", "tango", "timer", "voice", "while", "white", "world", "worry", "would", "binary", "defuse", "disarm", "expert", "finish", "forget", "lambda", "manual", "module", "number", "orange", "period", "purple", "quebec", "should", "sierra", "source", "strike", "submit", "twitch", "victor", "violet", "window", "yellow", "yankee", "charlie", "epsilon", "explode", "foxtrot", "juliett", "measure", "mission", "omicron", "subject", "uniform", "upsilon", "whiskey", "detonate", "notsolve", "november")]
        BinaryWord,

        [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", "Bitmaps", ThreeColumns6Answers, TranslateFormatArgs = new[] { true, true },
            ExampleExtraFormatArguments = new[] { "white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(0, 16)]
        Bitmaps,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Black Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        BlackCipherScreen,

        [SouvenirQuestion("What color was the {1} button in {0}?", "Blind Maze", TwoColumns4Answers, "Red", "Green", "Blue", "Gray", "Yellow", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "north", "east", "west", "south" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BlindMazeColors,
        [SouvenirQuestion("Which maze did you solve {0} on?", "Blind Maze", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        BlindMazeMaze,

        [SouvenirQuestion("How many times did the LED flash in {0}?", "Blinkstop", ThreeColumns6Answers, "30", "33", "37", "39", "42", "44", "47", "51", "55", "59")]
        BlinkstopNumberOfFlashes,
        [SouvenirQuestion("Which color did the LED flash the fewest times in {0}?", "Blinkstop", TwoColumns4Answers, "Purple", "Cyan", "Yellow", "Multicolor")]
        BlinkstopFewestFlashedColor,

        [SouvenirQuestion("What was the last letter pressed on {0}?", "Blockbusters", ThreeColumns6Answers)]
        [AnswerGenerator.Strings('A', 'Z')]
        BlockbustersLastLetter,

        [SouvenirQuestion("What were the letters on the screen in {0}?", "Blue Arrows", ThreeColumns6Answers, "CA", "C1", "CB", "C8", "CF", "C4", "CE", "C6", "3A", "31", "3B", "38", "3F", "34", "3E", "36", "GA", "G1", "GB", "G8", "GF", "G4", "GE", "G6", "7A", "71", "7B", "78", "7F", "74", "7E", "76", "DA", "D1", "DB", "D8", "DF", "D4", "DE", "D6", "5A", "51", "5B", "58", "5F", "54", "5E", "56", "HA", "H1", "HB", "H8", "HF", "H4", "HE", "H6", "2A", "21", "2B", "28", "2F", "24", "2E", "26")]
        BlueArrowsInitialLetters,

        [SouvenirQuestion("What was D in {0}?", "Blue Button", TwoColumns4Answers, AddThe = true)]
        [AnswerGenerator.Integers(1, 4)]
        BlueButtonD,
        [SouvenirQuestion("What was {1} in {0}?", "Blue Button", TwoColumns4Answers, AddThe = true,
            ExampleExtraFormatArguments = new[] { "E", "F", "G", "H" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 3)]
        BlueButtonEFGH,
        [SouvenirQuestion("What was M in {0}?", "Blue Button", ThreeColumns6Answers, AddThe = true)]
        [AnswerGenerator.Integers(1, 9)]
        BlueButtonM,
        [SouvenirQuestion("What was N in {0}?", "Blue Button", ThreeColumns6Answers, AddThe = true)]
        [AnswerGenerator.Integers(4, 9)]
        BlueButtonN,
        [SouvenirQuestion("What was P in {0}?", "Blue Button", ThreeColumns6Answers, "♠♥♣", "♠♣♥", "♥♠♣", "♥♣♠", "♣♠♥", "♣♥♠", AddThe = true)]
        BlueButtonP,
        [SouvenirQuestion("What was Q in {0}?", "Blue Button", ThreeColumns6Answers, "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow", AddThe = true)]
        BlueButtonQ,
        [SouvenirQuestion("What was X in {0}?", "Blue Button", TwoColumns4Answers, AddThe = true)]
        [AnswerGenerator.Integers(1, 5)]
        BlueButtonX,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Blue Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "ANCHOR", "ATTAIN", "DECIDE", "JAILOR", "LIGHTS", "OFFERS", "POETIC", "UNISON", "VECTOR", "VISION" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        BlueCipherScreen,

        [SouvenirQuestion("What was the {1} indicator label in {0}?", "Bob Barks", ThreeColumns6Answers, "BOB", "CAR", "CLR", "IND", "FRK", "FRQ", "MSA", "NSA", "SIG", "SND", "TRN", "BUB", "DOG", "ETC", "KEY", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "top left", "top right", "bottom left", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BobBarksIndicators,
        [SouvenirQuestion("Which button flashed {1} in sequence in {0}?", "Bob Barks", TwoColumns4Answers, "top left", "top right", "bottom left", "bottom right", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BobBarksPositions,

        [SouvenirQuestion("What letter was initially visible on {0}?", "Boggle", ThreeColumns6Answers, null, ExampleAnswers = new[] { "A", "E", "G", "M", "T", "W" })]
        BoggleLetters,

        [SouvenirQuestion("What was the license number in {0}?", "Bomb Diffusal", TwoColumns4Answers, ExampleAnswers = new[] { "A4BIK5", "HI391D", "ZX98O1", "12K9PL" })]
        BombDiffusalLicenseNumber,

        [SouvenirQuestion("Who said the {1} quote in {0}?", "Book of Mario", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "BookOfMarioSprites",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BookOfMarioPictures,
        [SouvenirQuestion("What did {1} say in the {2} stage of {0}?", "Book of Mario", OneColumn4Answers, ExampleAnswers = new[] { "Dark Koopatrol. These people just blow hard...", "I came, Mario! You finna", "Absolutely, I came! Got it!", "Well, I’m so desperate, so you better save me…" },
            ExampleExtraFormatArguments = new[] { "Goombell", QandA.Ordinal, "Prince Peach", QandA.Ordinal, "God Browser", QandA.Ordinal, "Mr.Krump", QandA.Ordinal, "Mario", QandA.Ordinal, "Flavio", QandA.Ordinal, "Quiz Thwomb", QandA.Ordinal, "Carbon", QandA.Ordinal, "Belda", QandA.Ordinal, "Make", QandA.Ordinal, "Yoshi Kid", QandA.Ordinal, "Bob", QandA.Ordinal, "Prosecutor Grubba", QandA.Ordinal },
            ExampleExtraFormatArgumentGroupSize = 2)]
        BookOfMarioQuotes,

        [SouvenirQuestion("Which operator did you submit in the {1} stage of {0}?", "Boolean Wires", TwoColumns4Answers, "OR", "XOR", "AND", "NAND", "NOR", ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BooleanWiresEnteredOperators,

        [SouvenirQuestion("What was rule {1} in {0}?", "Boomtar the Great", ThreeColumns6Answers,
            ExampleExtraFormatArgumentGroupSize = 1, ExampleExtraFormatArguments = new[] { "one", "two" })]
        [AnswerGenerator.Integers(1, 6)]
        BoomtarTheGreatRules,

        [SouvenirQuestion("Which {1} appeared on {0}?", "Boxing", TwoColumns4Answers, null, ExampleAnswers = new[] { "Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander" },
            ExampleExtraFormatArguments = new[] { "contestant’s first name", "contestant’s last name", "substitute’s first name", "substitute’s last name" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        BoxingNames,
        [SouvenirQuestion("What was the {1} of the contestant with strength rating {2} on {0}?", "Boxing", TwoColumns4Answers, null, ExampleAnswers = new[] { "Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander" }, TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "first name", "0", "first name", "1", "first name", "2", "last name", "0", "last name", "1", "last name", "2", "substitute’s first name", "0", "substitute’s first name", "1", "substitute’s first name", "2", "substitute’s last name", "0", "substitute’s last name", "1", "substitute’s last name", "2" }, ExampleExtraFormatArgumentGroupSize = 2)]
        BoxingContestantByStrength,
        [SouvenirQuestion("What was {1}’s strength rating on {0}?", "Boxing", ThreeColumns6Answers, "0", "1", "2", "3", "4",
            ExampleExtraFormatArguments = new[] { "Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BoxingStrengthByContestant,

        [SouvenirQuestion("What was the solution word in {0}?", "Braille", ThreeColumns6Answers, "acting", "dating", "heading", "meaning", "server", "aiming", "dealer", "hearing", "miners", "shaking", "artist", "eating", "heating", "nearer", "sought", "asking", "eighth", "higher", "parish", "staying", "bearing", "farmer", "insist", "parker", "strands", "beating", "farming", "lasted", "parking", "strings", "beings", "faster", "laying", "paying", "teaching", "binding", "father", "leader", "powers", "tended", "bought", "finding", "leading", "pushed", "tender", "boxing", "finest", "leaned", "pushing", "testing", "breach", "finish", "leaning", "rather", "throwing", "breast", "flying", "leaving", "reaching", "towers", "breath", "foster", "linking", "reader", "vested", "breathe", "fought", "listed", "reading", "warned", "bringing", "gaining", "listen", "resting", "warning", "brings", "gather", "living", "riding", "weaker", "carers", "gazing", "making", "rushed", "wealth", "carter", "gender", "marked", "rushing", "winner", "charter", "growing", "marking", "saying", "winning", "crying", "headed", "master", "served", "winter")]
        BrailleWord,

        [SouvenirQuestion("Which color appeared on the egg in {0}?", "Breakfast Egg", TwoColumns4Answers, "Crimson", "Orange", "Pink", "Beige", "Cyan", "Lime", "Petrol")]
        BreakfastEggColor,

        [SouvenirQuestion("What was the {1} correct button you pressed in {0}?", "Broken Buttons", ThreeColumns6Answers, "bomb", "blast", "boom", "burst", "wire", "button", "module", "light", "led", "switch", "RJ-45", "DVI-D", "RCA", "PS/2", "serial", "port", "row", "column", "one", "two", "three", "four", "five", "six", "seven", "eight", "size", "this", "that", "other", "submit", "abort", "drop", "thing", "blank", "broken", "too", "to", "yes", "see", "sea", "c", "wait", "word", "bob", "no", "not", "first", "hold", "late", "fail",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        BrokenButtons,

        [SouvenirQuestion("What was the displayed chord in {0}?", "Broken Guitar Chords", ThreeColumns6Answers, ExampleAnswers = new[] { "C", "Dm", "F#sus", "Gm7", "A9", "Eadd9" })]
        BrokenGuitarChordsDisplayedChord,
        [SouvenirQuestion("In which position, from left to right, was the broken string in {0}?", "Broken Guitar Chords", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 6)]
        BrokenGuitarChordsMutedString,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Brown Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "AROUND", "JUKING", "OCELOT", "PARDON", "SCHOOL", "SOCCER", "SPRING", "TIMING", "VALVES", "VORTEX" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        BrownCipherScreen,

        [SouvenirQuestion("What was the color of the middle contact point in {0}?", "Brush Strokes", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Cyan", "Sky", "Blue", "Purple", "Magenta", "Brown", "White", "Gray", "Black", "Pink", TranslateAnswers = true)]
        BrushStrokesMiddleColor,

        [SouvenirQuestion("What were the correct button presses in {0}?", "Bulb", ThreeColumns6Answers, "OOO", "OOI", "OIO", "OII", "IOO", "IOI", "IIO", "III", AddThe = true, Type = AnswerType.TicTacToeFont)]
        BulbButtonPresses,

        [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Burger Alarm", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        BurgerAlarmDigits,
        [SouvenirQuestion("What was the {1} order number in {0}?", "Burger Alarm", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 99, "00")]
        BurgerAlarmOrderNumbers,

        [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Burglar Alarm", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        BurglarAlarmDigits,

        [SouvenirQuestion("What color did the light glow in {0}?", "Button", TwoColumns4Answers, "red", "blue", "yellow", "white", AddThe = true, TranslateAnswers = true)]
        ButtonLightColor,

        [SouvenirQuestion("How many of the buttons in {0} were {1}?", "Button Sequence", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "blue", "yellow", "white" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 12)]
        ButtonSequencesColorOccurrences,

        [SouvenirQuestion("What was the {1} in {0}?", "Caesar Cycle", TwoColumns4Answers, "Advanced", "Addition", "Allocate", "Altering", "Binaries", "Billions", "Bulkhead", "Bulleted", "Ciphered", "Circuits", "Computer", "Continue", "Decrypts", "Division", "Discover", "Disposal", "Encipher", "Entrance", "Equation", "Equipped", "Finished", "Findings", "Fortress", "Forwards", "Gauntlet", "Gambling", "Gathered", "Glooming", "Hazarded", "Haziness", "Hunkered", "Huntsman", "Indicate", "Indigoes", "Illusion", "Illumine", "Jigsawed", "Jimmying", "Junction", "Judgment", "Kilowatt", "Kinetics", "Knockout", "Knuckled", "Limiting", "Linearly", "Linkages", "Labeling", "Monogram", "Monotone", "Multiply", "Mulligan", "Nanogram", "Nanotube", "Numbered", "Numerals", "Octangle", "Octuples", "Observed", "Obscured", "Progress", "Projects", "Position", "Positive", "Quadrant", "Quadrics", "Quickest", "Quintics", "Reversed", "Revolved", "Rotation", "Relation", "Starting", "Standard", "Stopping", "Stopword", "Triggers", "Triangle", "Toggling", "Together", "Underrun", "Underlie", "Ultimate", "Ultrared", "Vicinity", "Viceless", "Voltages", "Volatile", "Wingding", "Winnable", "Whatever", "Whatnots", "Yellowed", "Yeasayer", "Yielding", "Yourself", "Zippered", "Zigzaggy", "Zugzwang", "Zymogram",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        CaesarCycleWord,

        [SouvenirQuestion("What text was on the top display in the {1} stage of {0}?", "Caesar Psycho", ThreeColumns6Answers,
        ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("5*A-Z")]
        CaesarPsychoScreenTexts,
        [SouvenirQuestion("What color was the text on the top display in the second stage of {0}?", "Caesar Psycho", ThreeColumns6Answers, "white", "red", "magenta", "yellow", "green", "cyan", "violet")]
        CaesarPsychoScreenColor,

        [SouvenirQuestion("What was the LED color in {0}?", "Calendar", TwoColumns4Answers, "Green", "Yellow", "Red", "Blue", TranslateAnswers = true)]
        CalendarLedColor,

        [SouvenirQuestion("What color was the {1} button in {0}?", "Cartinese", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue",
            ExampleExtraFormatArguments = new[] { "up", "right", "down", "left" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateAnswers = true, TranslateFormatArgs = new[] { true })]
        CartineseButtonColors,
        [SouvenirQuestion("What lyric was played by the {1} button in {0}?", "Cartinese", TwoColumns4Answers, "Aingobodirou", "Dongifubounan", "Ayofumylu", "Dimycamilayw", "Dogosemiu", "Bitgosemiu", "Iwittyluyu", "Herolideca", "Anseweke", "Likwoveke", "Omeygah", "Dediamnatifney",
            ExampleExtraFormatArguments = new[] { "up", "right", "down", "left" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        CartineseLyrics,

        [SouvenirQuestion("What was the colour of the {1} panel in {0}?", "Catchphrase", ThreeColumns6Answers, "Red", "Green", "Blue", "Orange", "Purple", "Yellow",
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateAnswers = true, TranslateFormatArgs = new[] { true })]
        CatchphraseColour,

        [SouvenirQuestion("What was the {1} submitted answer in {0}?", "Challenge & Contact", TwoColumns4Answers, null, ExampleAnswers = new[] { "Accumulation", "Coffeebucks", "Perplexing", "Zoo", "Sunstone", "Bob" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChallengeAndContactAnswers,

        [SouvenirQuestion("What was the {1} character in {0}?", "Character Codes", ThreeColumns6Answers, ExampleAnswers = new[] { "♥", "♣", "•" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        CharacterCodesCharacter,

        [SouvenirQuestion("Which letter was present but not submitted on the left slider of {0}?", "Character Shift", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-Z")]
        CharacterShiftLetters,
        [SouvenirQuestion("Which digit was present but not submitted on the right slider of {0}?", "Character Shift", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("0-9")]
        CharacterShiftDigits,

        [SouvenirQuestion("Who was displayed in the {1} slot in the {2} stage of {0}?", "Character Slots", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "CharacterSlotsSprites",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        CharacterSlotsDisplayedCharacters,

        [SouvenirQuestion("What was {1} in {0}?", "Cheap Checkout", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "the paid amount", "the first paid amount", "the second paid amount" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(5, 50, "$0\".00\"")]
        CheapCheckoutPaid,

        [SouvenirQuestion("Which bird {1} present in {0}?", "Cheep Checkout", OneColumn4Answers, "Auklet", "Bluebird", "Chickadee", "Dove", "Egret", "Finch", "Godwit", "Hummingbird", "Ibis", "Jay", "Kinglet", "Loon", "Magpie", "Nuthatch", "Oriole", "Pipit", "Quail", "Raven", "Shrike", "Thrush", "Umbrellabird", "Vireo", "Warbler", "Xantus’s Hummingbird", "Yellowlegs", "Zigzag Heron",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        CheepCheckoutBirds,

        [SouvenirQuestion("What was the {1} coordinate in {0}?", "Chess", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("a-f", "1-6")]
        ChessCoordinate,

        [SouvenirQuestion("What color was the {1} LED in {0}?", "Chinese Counting", TwoColumns4Answers, "White", "Red", "Green", "Orange", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
          ExampleExtraFormatArguments = new[] { "left", "right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChineseCountingLED,

        [SouvenirQuestion("Which note was part of the given chord in {0}?", "Chord Qualities", ThreeColumns6Answers, "A", "A♯", "B", "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯")]
        ChordQualitiesNotes,

        [SouvenirQuestion("What was the given chord quality in {0}?", "Chord Qualities", ThreeColumns6Answers, "7", "-7", "Δ7", "-Δ7", "7♯9", "ø", "add9", "-add9", "7♯5", "Δ7♯5", "7sus", "-Δ7♯5")]
        ChordQualitiesQuality,

        [SouvenirQuestion("What was the displayed number in {0}?", "Code", ThreeColumns6Answers, null, AddThe = true)]
        [AnswerGenerator.Integers(999, 9999)]
        CodeDisplayNumber,

        [SouvenirQuestion("Which of these words was submitted in {0}?", "Codenames", TwoColumns4Answers, null, ExampleAnswers = new[] { "Hyperborean", "Weenus", "Melody", "King" })]
        CodenamesAnswers,

        [SouvenirQuestion("What was the last served coffee in {0}?", "Coffeebucks", OneColumn4Answers, "Twix Frappuccino", "The Blue Drink", "Matcha & Espresso Fusion", "Caramel Snickerdoodle Macchiato", "Liquid Cocaine", "S’mores Hot Chocolate", "The Pink Drink", "Grasshopper Frappuccino")]
        CoffeebucksCoffee,

        [SouvenirQuestion("Which coin was flipped in {0}?", "Coinage", ThreeColumns6Answers, ExampleAnswers = new[] { "e4", "h5", "d4", "h4", "c4", "h3", "c3", "g2", "f3", "h1", "f7" })]
        CoinageFlip,

        [SouvenirQuestion("What was {1}'s number in {0}?", "Color Addition", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Strings(3, "0123456789")]
        ColorAdditionNumbers,

        [SouvenirQuestion("What mangling was applied in {0}?", "Color Braille", OneColumn4Answers, "Top row shifted to the right", "Top row shifted to the left", "Middle row shifted to the right", "Middle row shifted to the left", "Bottom row shifted to the right", "Bottom row shifted to the left", "Each letter upside-down", "Each letter horizontally flipped", "Each letter vertically flipped", "Dots are inverted")]
        ColorBrailleMangling,
        [SouvenirQuestion("What was the {1} word in {0}?", "Color Braille", TwoColumns4Answers, ExampleAnswers = new[] { "advent", "barman", "carrying", "drowning", "holding", "landowner", "mandate", "narrowed", "remain", "shallow", "therefore", "western", "yield" }, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorBrailleWords,

        [SouvenirQuestion("What was the {1}-stage indicator pattern in {0}?", "Color Decoding", TwoColumns4Answers, "Checkered", "Horizontal", "Vertical", "Solid",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorDecodingIndicatorPattern,
        [SouvenirQuestion("Which color {1} in the {2}-stage indicator pattern in {0}?", "Color Decoding", TwoColumns4Answers, "Green", "Purple", "Red", "Blue", "Yellow", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "appeared", QandA.Ordinal, "did not appear", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        ColorDecodingIndicatorColors,

        [SouvenirQuestion("What was the displayed word in {0}?", "Colored Keys", ThreeColumns6Answers, null, ExampleAnswers = new[] { "blue", "white" })]
        ColoredKeysDisplayWord,
        [SouvenirQuestion("What was the displayed word’s color in {0}?", "Colored Keys", ThreeColumns6Answers, null, ExampleAnswers = new[] { "blue", "white" })]
        ColoredKeysDisplayWordColor,
        [SouvenirQuestion("What was the color of the {1} key in {0}?", "Colored Keys", ThreeColumns6Answers, null, ExampleAnswers = new[] { "blue", "white" },
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        ColoredKeysKeyColor,
        [SouvenirQuestion("What letter was on the {1} key in {0}?", "Colored Keys", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Strings('A', 'Z')]
        ColoredKeysKeyLetter,

        [SouvenirQuestion("What was the first color group in {0}?", "Colored Squares", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true)]
        ColoredSquaresFirstGroup,

        [SouvenirQuestion("What was the initial position of the switches in {0}?", "Colored Switches", ThreeColumns6Answers,
            Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings(5, 'Q', 'R')]
        ColoredSwitchesInitialPosition,
        [SouvenirQuestion("What was the position of the switches when the LEDs came on in {0}?", "Colored Switches", ThreeColumns6Answers,
            Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings(5, 'Q', 'R')]
        ColoredSwitchesWhenLEDsCameOn,

        [SouvenirQuestion("What was the color of the {1} LED in {0}?", "Color Morse", ThreeColumns6Answers, "Blue", "Green", "Orange", "Purple", "Red", "Yellow", "White", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorMorseColor,

        [SouvenirQuestion("What character was flashed by the {1} LED in {0}?", "Color Morse", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("0-9A-Z")]
        ColorMorseCharacter,

        [SouvenirQuestion("What was the submitted score in {0}?", "Colors Maximization", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(27, 65)]
        ColorsMaximizationSubmittedScore,
        [SouvenirQuestion("What color {1} submitted as part of the solution in {0}?", "Colors Maximization", TwoColumns4Answers, "Blue", "Green", "Magenta", "Red", "White", "Yellow", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        ColorsMaximizationSubmittedColor,
        [SouvenirQuestion("How many buttons were {1} in {0}?", "Colors Maximization", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Integers(0, 11)]
        ColorsMaximizationColorCount,

        [SouvenirQuestion("What was the colour of this {1} in the {2} stage of {0}?", "Coloured Cubes", ThreeColumns6Answers, ExampleAnswers = new[] { "Red", "Green", "Blue", "Yellow", "Cyan", "Magenta" },
            ExampleExtraFormatArguments = new[] { "cube", QandA.Ordinal, "stage light", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2, UsesQuestionSprite = true, TranslateAnswers = true, TranslateFormatArgs = new[] { true, false })]
        ColouredCubesColours,

        [SouvenirQuestion("What was the color of the last word in the sequence in {0}?", "Colour Flash", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Magenta", "White", TranslateAnswers = true)]
        ColourFlashLastColor,

        [SouvenirQuestion("What pair of numbers was present in {0}?", "Connection Check", ThreeColumns6Answers, null)]
        [AnswerGenerator.Strings("1-8", " ", "1-8")]
        ConnectionCheckNumbers,

        [SouvenirQuestion("What was the solution you selected first in {0}?", "Coordinates", OneColumn4Answers, null, ExampleAnswers = new[] { "[4,7]", "C4", "<0, 2>", "3, 1", "(6,2)", "B-1", "“1, 0”", "4/3", "[12]", "#23", "四十七" })]
        CoordinatesFirstSolution,
        [SouvenirQuestion("What was the grid size in {0}?", "Coordinates", OneColumn4Answers, "9", "15", "25", "21", "35", "49", "(9)", "(15)", "(21)", "(25)", "(35)", "(49)", "3 by 3", "4 by 3", "5 by 3", "6 by 3", "7 by 3", "3 by 4", "4 by 4", "5 by 4", "6 by 4", "7 by 4", "3 by 5", "4 by 5", "5 by 5", "6 by 5", "7 by 5", "3 by 6", "4 by 6", "5 by 6", "6 by 6", "7 by 6", "3 by 7", "4 by 7", "5 by 7", "6 by 7", "7 by 7", "9*3", "12*4", "15*5", "18*6", "21*7", "12*3", "16*4", "20*5", "24*6", "28*7", "15*3", "20*4", "25*5", "30*6", "35*7", "18*3", "24*4", "30*5", "36*6", "42*7", "21*3", "28*4", "35*5", "42*6", "49*7", "9 : 3", "12 : 3", "15 : 3", "18 : 3", "21 : 3", "12 : 4", "16 : 4", "20 : 4", "24 : 4", "28 : 4", "15 : 5", "20 : 5", "25 : 5", "30 : 5", "35 : 5", "18 : 6", "24 : 6", "30 : 6", "36 : 6", "42 : 6", "21 : 7", "28 : 7", "35 : 7", "42 : 7", "49 : 7", "3×3", "3×4", "3×5", "3×6", "3×7", "4×3", "4×4", "4×5", "4×6", "4×7", "5×3", "5×4", "5×5", "5×6", "5×7", "6×3", "6×4", "6×5", "6×6", "6×7", "7×3", "7×4", "7×5", "7×6", "7×7")]
        CoordinatesSize,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Coral Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        CoralCipherScreen,

        [SouvenirQuestion("What was the color of the {1} corner in {0}?", "Corners", TwoColumns4Answers, "red", "green", "blue", "yellow", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-right", "bottom-left" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CornersColors,
        [SouvenirQuestion("How many corners in {0} were {1}?", "Corners", ThreeColumns6Answers, "0", "1", "2", "3", "4", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue", "yellow" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CornersColorCount,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Cornflower Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        CornflowerCipherScreen,

        [SouvenirQuestion("What was the number initially shown in {0}?", "Cosmic", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9999)]
        CosmicNumber,

        [SouvenirQuestion("What was the {1} ingredient shown in {0}?", "Crazy Hamburger", ThreeColumns6Answers, "Bread", "Cheese", "Grass", "Meat", "Oil", "Peppers",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        CrazyHamburgerIngredient,

        [SouvenirQuestion("What was the {1} location in {0}?", "Crazy Maze", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "starting", "goal" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Strings("A-Z", "A-Z")]
        CrazyMazeStartOrGoal,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Cream Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        CreamCipherScreen,

        [SouvenirQuestion("What were the weather conditions on the {1} day in {0}?", "Creation", TwoColumns4Answers, "Clear", "Heat Wave", "Meteor Shower", "Rain", "Windy",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, TranslateAnswers = true)]
        CreationWeather,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Crimson Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        CrimsonCipherScreen,

        [SouvenirQuestion("What was the alteration color used in {0}?", "Critters", TwoColumns4Answers, "Yellow", "Pink", "Blue", "White", TranslateAnswers = true)]
        CrittersAlterationColor,

        [SouvenirQuestion("What was the displayed word in {0}?", "Cruel Binary", TwoColumns4Answers, ExampleAnswers = new[] { "LEAST", "YELLOW", "SIERRA", "WHITE" })]
        CruelBinaryDisplayedWord,

        [SouvenirQuestion("Which of these characters appeared in the {1} stage of {0}?", "Cruel Keypads", ThreeColumns6Answers, "ㄹ", "ㅁ", "ㅂ", "ㄱ", "ㄲ", "ㄷ", "ㅈ", "ㅉ", "ㅟ", "ㅋ", "ㅌ", "ㅍ", "ㅃ", "ㅅ", "ㅆ", "ㅇ", "ㅢ", "ㄴ", "ㄸ", ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CruelKeypadsDisplayedSymbols,
        [SouvenirQuestion("What was the color of the bar in the {1} stage of {0}?", "Cruel Keypads", ThreeColumns6Answers, "Red", "Blue", "Yellow", "Green", "Magenta", "White", ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CruelKeypadsColors,

        [SouvenirQuestion("Which cell was pre-filled at the start of {0}?", "cRule", TwoColumns4Answers, Type = AnswerType.Sprites, AddThe = true)]
        CRulePrefilled,
        [SouvenirQuestion("Which symbol pair was here in {0}?", "cRule", ThreeColumns6Answers, "♤♤", "♤♧", "♤♢", "♤♡", "♧♤", "♧♧", "♧♢", "♧♡", "♢♤", "♢♧", "♢♢", "♢♡", "♡♤", "♡♧", "♡♢", "♡♡", AddThe = true, UsesQuestionSprite = true)]
        CRuleSymbolPair,
        [SouvenirQuestion("Which symbol pair was present on {0}?", "cRule", ThreeColumns6Answers, "♤♤", "♤♧", "♤♢", "♤♡", "♧♤", "♧♧", "♧♢", "♧♡", "♢♤", "♢♧", "♢♢", "♢♡", "♡♤", "♡♧", "♡♢", "♡♡", AddThe = true)]
        CRuleSymbolPairPresent,
        [SouvenirQuestion("Where was {1} in {0}?", "cRule", ThreeColumns6Answers, Type = AnswerType.Sprites, AddThe = true,
            ExampleExtraFormatArguments = new[] { "♤♤", "♤♧", "♤♢", "♤♡", "♧♤", "♧♧", "♧♢", "♧♡", "♢♤", "♢♧", "♢♢", "♢♡", "♡♤", "♡♧", "♡♢", "♡♡" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CRuleSymbolPairCell,

        [SouvenirQuestion("What was the {1} in {0}?", "Cryptic Cycle", TwoColumns4Answers, "Advanced", "Addition", "Allocate", "Altering", "Binaries", "Billions", "Bulkhead", "Bulleted", "Ciphered", "Circuits", "Computer", "Continue", "Decrypts", "Division", "Discover", "Disposal", "Examined", "Examples", "Equation", "Equipped", "Finished", "Findings", "Fortress", "Forwards", "Gauntlet", "Gambling", "Gathered", "Glooming", "Hazarded", "Haziness", "Hunkered", "Huntsman", "Indicate", "Indigoes", "Illusion", "Illumine", "Jigsawed", "Jimmying", "Junction", "Judgment", "Kilowatt", "Kinetics", "Knockout", "Knuckled", "Limiting", "Linearly", "Linkages", "Labeling", "Monogram", "Monotone", "Multiply", "Mulligan", "Nanogram", "Nanotube", "Numbered", "Numerals", "Octangle", "Octuples", "Observed", "Obscured", "Progress", "Projects", "Position", "Positive", "Quadrant", "Quadplex", "Quickest", "Quintics", "Reversed", "Revolved", "Rotation", "Relation", "Starting", "Standard", "Stopping", "Stopword", "Triggers", "Triangle", "Toggling", "Together", "Underrun", "Underlie", "Ultimate", "Ultrared", "Vicinity", "Viceless", "Voltages", "Volatile", "Wingding", "Winnable", "Whatever", "Whatnots", "Yellowed", "Yeasayer", "Yielding", "Yourself", "Zippered", "Zigzaggy", "Zugzwang", "Zymogram",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        CrypticCycleWord,

        [SouvenirQuestion("What was the label of the {1} key in {0}?", "Cryptic Keypad", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Strings("A-Z")]
        CrypticKeypadLabels,
        [SouvenirQuestion("Which cardinal direction was the {1} key rotated to in {0}?", "Cryptic Keypad", TwoColumns4Answers, "North", "East", "South", "West", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        CrypticKeypadRotations,

        [SouvenirQuestion("What was the {1} cube rotation in {0}?", "Cube", TwoColumns4Answers, "rotate cw", "tip left", "tip backwards", "rotate ccw", "tip right", "tip forwards",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true, TranslateFormatArgs = new[] { true })]
        CubeRotations,

        [SouvenirQuestion("What was the first digit of the initially displayed number in {0}?", "Cursed Double-Oh", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        CursedDoubleOhInitialPosition,

        [SouvenirQuestion("Who was the {1} customer in {0}?", "Customer Identification", OneColumn4Answers, "Akari", "Alberto", "Allan", "Amy", "Austin", "Bertha", "Big Pauly", "Boomer", "Boopsy & Bill", "Brody", "Bruna Romano", "C.J. Friskins", "Cameo", "Captain Cori", "Carlo Romano", "Cecilia", "Cherissa", "Chester", "Chuck", "Clair", "Cletus", "Clover", "Connor", "Cooper", "Crystal", "Daniela", "Deano", "Didar", "Doan", "Drakson", "Duke Gotcha", "Edna", "Elle", "Ember", "Emmlette", "Evelyn", "Fernanda", "Foodini", "Franco", "Georgito", "Gino Romano", "Greg", "Gremmie", "Hacky Zak", "Hank", "Hope", "Hugo", "Iggy", "Indigo", "Ivy", "James", "Janana", "Johnny", "Jojo", "Joy", "Julep", "Kahuna", "Kaleb", "Kasey O", "Kayla", "Kenji", "Kenton", "Kingsley", "Koilee", "LePete", "Liezel", "Lisa", "Little Edoardo", "Maggie", "Mandi", "Marty", "Mary", "Matt", "Mayor Mallow", "Mesa", "Mindy", "Mitch", "Moe", "Mousse", "Mr. Bombolony", "Nevada", "Nick", "Ninjoy", "Nye", "Okalani", "Olga", "Olivia", "Pally", "Papa Louie", "Peggy", "Penny", "Perri", "Petrona", "Pinch Hitwell", "Professor Fitz", "Prudence", "Quinn", "Radlynn", "Rhonda", "Rico", "Ripley", "Rita", "Robby", "Rollie", "Roy", "Rudy", "Santa", "Sarge Fan", "Sasha", "Scarlett", "Scooter", "Shannon", "Sienna", "Simone", "Skip", "Skyler", "Sprinks The Clown", "Steven", "Sue", "Taylor", "The Dynamoe", "Timm", "Tohru", "Tony", "Trishna", "Utah", "Vicky", "Vincent", "Wally", "Wendy", "Whiff", "Whippa", "Willow", "Wylan B", "Xandra", "Xolo", "Yippy", "Yui", "Zoe",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        CustomerIdentificationCustomer,

        [SouvenirQuestion("Where was the button at the {1} stage in {0}?", "Cyan Button", TwoColumns4Answers, "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true, TranslateAnswers = true)]
        CyanButtonPositions,

        [SouvenirQuestion("Which region did you depart from in {0}?", "DACH Maze", OneColumn4Answers, "Burgenland, A", "Carinthia, A", "Lower Austria, A", "North Tyrol, A", "Upper Austria, A", "East Tyrol, A", "Salzburg, A", "Styria, A", "Vorarlberg, A", "Vienna, A", "Aargau, CH", "Appenzell Inner Rhodes, CH", "Appenzell Outer Rhodes, CH", "Basel Country, CH", "Bern, CH", "Basel City, CH", "Fribourg, CH", "Geneva, CH", "Glarus, CH", "Grisons, CH", "Jura, CH", "Luzern, CH", "Nidwalden, CH", "Neuchâtel, CH", "Obwalden, CH", "Schaffhausen, CH", "St. Gallen, CH", "Solothurn, CH", "Schwyz, CH", "Thurgau, CH", "Ticino, CH", "Uri, CH", "Vaud, CH", "Valais, CH", "Zug, CH", "Zürich, CH", "Brandenburg, D", "Berlin, D", "Baden-Württemberg, D", "Bavaria, D", "Bremen, D", "Hesse, D", "Hamburg, D", "Mecklenburg-Vorpommern, D", "Lower Saxony, D", "North Rhine-Westphalia, D", "Rhineland-Palatinate, D", "Schleswig-Holstein, D", "Saarland, D", "Saxony, D", "Saxony-Anhalt, D", "Thuringia, D", "Liechtenstein")]
        DACHMazeOrigin,

        [SouvenirQuestion("What was the shape generated in {0}?", "Deaf Alley", ThreeColumns6Answers, null, ExampleAnswers = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "d", "e", "f", "g", "h", "i", "j", "k", "m", "n", "p", "q", "r", "t", "u", "y", "1", "2", "3", "4", "6", "7", "8", "9", "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "[", "]", "{", "}", ":", ";", "“", "‘", "<", ",", ">", ".", "?", "/", "\\" })]
        DeafAlleyShape,

        [SouvenirQuestion("What deck did the first card of {0} belong to?", "Deck of Many Things", TwoColumns4Answers, "Standard", "Metropolitan", "Maritime", "Arctic", "Tropical", "Oasis", "Celestial", AddThe = true)]
        DeckOfManyThingsFirstCard,

        [SouvenirQuestion("What was the starting {1} defining color in {0}?", "Decolored Squares", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "column", "row" }, ExampleExtraFormatArgumentGroupSize = 1)]
        DecoloredSquaresStartingPos,

        [SouvenirQuestion("What was the {1} of the {2} goal in {0}?", "Decolour Flash", ThreeColumns6Answers, "Blue", "Green", "Red", "Magenta", "Yellow", "White", ExampleExtraFormatArguments = new[] { "colour", QandA.Ordinal, "word", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2, TranslateAnswers = true, TranslateFormatArgs = new[] { true, false })]
        DecolourFlashGoal,

        [SouvenirQuestion("What number was initially shown on display {1} in {0}?", "Denial Displays", ThreeColumns6Answers, null, ExampleAnswers = new[] { "1", "22", "333", "4", "55", "666", "7", "88", "999" },
            ExampleExtraFormatArguments = new[] { "A", "B", "C", "D", "E" }, ExampleExtraFormatArgumentGroupSize = 1)]
        DenialDisplaysDisplays,

        [SouvenirQuestion("What was the {1} egg’s {2} rotation in {0}?", "Devilish Eggs", TwoColumns4Answers, "W90CW", "W180CW", "W270CW", "W360CW", "W90CCW", "W180CCW", "W270CCW", "W360CCW", "T90CW", "T180CW", "T270CW", "T360CW", "T90CCW", "T180CCW", "T270CCW", "T360CCW", TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "top", QandA.Ordinal, "bottom", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        DevilishEggsRotations,
        [SouvenirQuestion("What was the {1} digit in the string of numbers on {0}?", "Devilish Eggs", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        DevilishEggsNumbers,
        [SouvenirQuestion("What was the {1} letter in the string of letters on {0}?", "Devilish Eggs", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        DevilishEggsLetters,

        [SouvenirQuestion("What was the number on the {1} button in {0}?", "Digisibility", ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 9)]
        DigisibilityDisplayedNumber,

        [SouvenirQuestion("What was the initial number in {0}?", "Digit String", TwoColumns4Answers)]
        [AnswerGenerator.Strings("1-9", "6*0-9", "1-9")]
        DigitStringInitialNumber,

        [SouvenirQuestion("Which of these was a visible character in {0}?", "Dimension Disruption", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-Z0-9")]
        DimensionDisruptionVisibleLetters,

        [SouvenirQuestion("How many times did you press the button in the {1} stage of {0}?", "Directional Button", TwoColumns4Answers, "1", "2", "3", "4",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        DirectionalButtonButtonCount,

        [SouvenirQuestion("What was {1}’s remembered position in {0}?", "Discolored Squares", ThreeColumns6Answers, null, Type = AnswerType.Grid, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "Blue", "Red", "Yellow", "Green", "Magenta" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Grid(4, 4)]
        DiscoloredSquaresRememberedPositions,

        [SouvenirQuestion("What were the correct button presses in {0}?", "Divisible Numbers", OneColumn4Answers, "Nay, Nay, Nay", "Nay, Nay, Yea", "Nay, Yea, Nay", "Nay, Yea, Yea", "Yea, Nay, Nay", "Yea, Nay, Yea", "Yea, Yea, Nay", "Yea, Yea, Yea")]
        DivisibleNumbersAnswers,
        [SouvenirQuestion("What was the {1} stage’s number in {0}?", "Divisible Numbers", ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9999)]
        DivisibleNumbersNumbers,

        [SouvenirQuestion("What was the starting position in {0}?", "Double Arrows", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(1, 81, "00")]
        DoubleArrowsStart,
        [SouvenirQuestion("Which {1} arrow moved {2} in the grid in {0}?", "Double Arrows", TwoColumns4Answers, "Up", "Right", "Left", "Down",
            ExampleExtraFormatArguments = new[] { "inner", "up", "outer", "up", "inner", "down", "outer", "down", "inner", "left", "outer", "left", "inner", "riight", "outer", "right" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, true })]
        DoubleArrowsArrow,
        [SouvenirQuestion("Which direction in the grid did the {1} arrow move in {0}?", "Double Arrows", TwoColumns4Answers, "Up", "Right", "Left", "Down",
            ExampleExtraFormatArguments = new[] { "inner up", "inner down", "inner left", "inner right", "outer up", "outer down", "outer left", "outer right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        DoubleArrowsMovement,

        [SouvenirQuestion("What was the screen color on the {1} stage of {0}?", "Double Color", ThreeColumns6Answers, "Green", "Blue", "Red", "Pink", "Yellow", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        DoubleColorColors,

        [SouvenirQuestion("What was the most recent digit on the {1} display in {0}?", "Double Digits", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "left", "right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        DoubleDigitsDisplays,

        [SouvenirQuestion("What was the starting key number in {0}?", "Double Expert", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(30, 69)]
        DoubleExpertStartingKeyNumber,
        [SouvenirQuestion("What was the word you submitted in {0}?", "Double Expert", ThreeColumns6Answers, ExampleAnswers = new[] { "Echo", "November", "Rodeo", "Words", "Victor", "Zulu" })]
        DoubleExpertSubmittedWord,

        [SouvenirQuestion("Which button was the submit button in {0}?", "Double-Oh", ThreeColumns6Answers, "↕", "⇕", "↔", "⇔", "◆")]
        DoubleOhSubmitButton,

        [SouvenirQuestion("Which of these symptoms was listed on {0}?", "Dr. Doctor", TwoColumns4Answers, "Bloating", "Chills", "Cold Hands", "Constipation", "Cough", "Diarrhea", "Disappearance of the Ears", "Dizziness", "Excessive Crying", "Fatigue", "Fever", "Foot swelling", "Gas", "Hallucination", "Headache", "Loss of Smell", "Muscle Cramp", "Nausea", "Numbness", "Shortness of Breath", "Sleepiness", "Thirstiness", "Throat irritation")]
        DrDoctorSymptoms,
        [SouvenirQuestion("Which of these diseases was listed on {0}, but not the one treated?", "Dr. Doctor", TwoColumns4Answers, "Alztimer’s", "Braintenance", "Color allergy", "Detonession", "Emojilepsy", "Foot and Morse", "Gout of Life", "HRV", "Indicitis", "Jaundry", "Keypad stones", "Legomania", "Microcontusion", "Narcolization", "OCd", "Piekinson’s", "Quackgrounds", "Royal Flu", "Seizure Siphor", "Tetrinus", "Urinary LEDs", "Verticode", "Widgeting", "XMAs", "Yes-no infection", "Zooties", "Chronic Talk", "Jukepox", "Neurolysis", "Perspective Loss", "Orientitis", "Huntington’s disease")]
        DrDoctorDiseases,

        [SouvenirQuestion("What was the decrypted word in {0}?", "Dreamcipher", OneColumn4Answers, null, ExampleAnswers = new[] { "asparagus", "demonstration", "fossilizing", "foursquare", "grinning", "jumpiness", "pasteboard", "prosecution", "sarcastic", "transition" })]
        DreamcipherWord,

        [SouvenirQuestion("How did you approach the duck in {0}?", "Duck", OneColumn4Answers, "dove at the duck", "walked to the duck", "ran to the duck", "snuck up on the duck", "swam to the duck", "flew to the duck", "approached the duck with caution", AddThe = true)]
        DuckApproach,
        [SouvenirQuestion("What was the color of the curtain in {0}?", "Duck", TwoColumns4Answers, "blue", "yellow", "green", "orange", "red", AddThe = true)]
        DuckCurtainColor,

        [SouvenirQuestion("Which player {1} present in {0}?", "Dumb Waiters", OneColumn4Answers, null, ExampleAnswers = new[] { "Arceus", "Danny7007", "EpicToast", "eXish", "Fang", "Makebao", "MCD573", "Mr. Peanut", "Mythers", "Xmaster" },
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        DumbWaitersPlayerAvailable,

        [SouvenirQuestion("What was the background number in {0}?", "Earthbound", ThreeColumns6Answers, "57", "97", "77", "43", "53", "32", "18", "88", "31", "16", "76", "20", "13", "89", "44", "35", "48", "86", "90", "67", "45", "26", "24", "87", "22", "84", "47", "93", "49", "33")]
        EarthboundBackground,
        [SouvenirQuestion("Which monster was displayed in {0}?", "Earthbound", ThreeColumns6Answers, Type = AnswerType.Sprites)]
        EarthboundMonster,

        [SouvenirQuestion("What word was asked to be spelled in {0}?", "eeB gnillepS", TwoColumns4Answers, null, ExampleAnswers = new[] { "odontalgia", "precocious", "privilege", "prospicience" })]
        eeBgnillepSWord,

        [SouvenirQuestion("What was the last digit on the small display in {0}?", "Eight", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 9)]
        EightLastSmallDisplayDigit,
        [SouvenirQuestion("What was the position of the last broken digit in {0}?", "Eight", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(1, 8)]
        EightLastBrokenDigitPosition,
        [SouvenirQuestion("What were the last resulting digits in {0}?", "Eight", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(50, 99)]
        EightLastResultingDigits,
        [SouvenirQuestion("What was the last displayed number in {0}?", "Eight", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(10, 99)]
        EightLastDisplayedNumber,

        [SouvenirQuestion("What was the {1} rune shown on {0}?", "Elder Futhark", TwoColumns4Answers, "Algiz", "Ansuz", "Berkana", "Dagaz", "Ehwaz", "Eihwaz", "Fehu", "Gebo", "Hagalaz", "Isa", "Jera", "Kenaz", "Laguz", "Mannaz", "Nauthiz", "Othila", "Perthro", "Raido", "Sowulo", "Teiwaz", "Thurisaz", "Uruz", "Wunjo",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ElderFutharkRunes,

        [SouvenirQuestion("What was the {1} keyword in {0}?", "ENA Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        EnaCipherKeywordAnswer,
        [SouvenirQuestion("What was the transposition key in {0}?", "ENA Cipher", TwoColumns4Answers, null)]
        [AnswerGenerator.Strings(6, "123456")]
        EnaCipherExtAnswer,
        [SouvenirQuestion("What was the encrypted word in {0}?", "ENA Cipher", TwoColumns4Answers, null)]
        [AnswerGenerator.Strings(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        EnaCipherEncryptedAnswer,

        [SouvenirQuestion("Which of these numbers appeared on a die in the {1} stage of {0}?", "Encrypted Dice", TwoColumns4Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 6)]
        EncryptedDice,

        [SouvenirQuestion("Which shape was the {1} operand in {0}?", "Encrypted Equations", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "EncryptedEquationsSprites",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        EncryptedEquationsShapes,

        [SouvenirQuestion("What method of encryption was used by {0}?", "Encrypted Hangman", OneColumn4Answers, "Caesar Cipher", "Atbash Cipher", "Rot-13 Cipher", "Affine Cipher", "Modern Cipher", "Vigenère Cipher", "Playfair Cipher")]
        EncryptedHangmanEncryptionMethod,
        [SouvenirQuestion("What module name was encrypted by {0}?", "Encrypted Hangman", OneColumn4Answers, ExampleAnswers = new[] { "Anagrams", "Word Scramble", "Two Bits", "Switches", "Lights Out", "Emoji Math", "Math", "Semaphore", "Piano Keys", "Colour Flash" })]
        EncryptedHangmanModule,

        [SouvenirQuestion("Which symbol on {0} was spinning {1}?", "Encrypted Maze", ThreeColumns6Answers, "f", "H", "$", "l", "B", "N", "g", "I", "%", "m", "C", "O", "h", "J", "&", "n", "D", "P", "i", "K", "'", "o", "E", "Q", "j", "L", "(", "p", "F", "R",
            Type = AnswerType.DynamicFont, ExampleExtraFormatArguments = new[] { "clockwise", "counter-clockwise" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        EncryptedMazeSymbols,

        [SouvenirQuestion("What was the {1} on {0}?", "Encrypted Morse", TwoColumns4Answers, null, ExampleAnswers = new[] { "Detonate", "Ready Now", "Please No", "Cheesecake" },
            ExampleExtraFormatArguments = new[] { "received call", "sent response" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        EncryptedMorseCallResponse,

        [SouvenirQuestion("What was the first encoding used in {0}?", "Encryption Bingo", OneColumn4Answers, ExampleAnswers = new[] { "Morse code", "Braille", "Semaphore", "Lombax" })]
        EncryptionBingoEncoding,

        [SouvenirQuestion("What was the {1} in {0}?", "Enigma Cycle", TwoColumns4Answers, ExampleAnswers = new[] { "ABNORMAL", "AUTHORED", "BACKDOOR", "BOULDERS", "CHANGING", "CUMBERED", "DEBUGGED", "DODGIEST", "EDITABLE", "EXCESSES", "FAIRYISM", "FRAGMENT", "GIBBERED", "GROANING", "HEADACHE", "HUDDLING", "ILLUSORY", "IRONICAL", "JOKINGLY", "JUDGMENT", "KEYNOTES", "KINDLING", "LIKENESS", "LOCKOUTS", "MOBILITY", "MUFFLING", "NEUTRALS", "NOTIONAL", "OFFTRACK", "ORDERING", "PHANTASM", "PROVOKED", "QUITTERS", "QUOTABLE", "RHETORIC", "ROULETTE", "SHUTDOWN", "SUBLIMES", "TARTNESS", "TYPHONIC", "UNPURGED", "UGLINESS", "VARIANCE", "VOLATILE", "WACKIEST", "WORKFLOW", "XENOLITH", "XANTHENE", "YABBERED", "YOURSELF", "ZAPPIEST", "ZILLIONS" },
            ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        EnigmaCycleWords,

        [SouvenirQuestion("What was the first number shown in {0}?", "Entry Number Four", TwoColumns4Answers, ExampleAnswers = new[] { "01234567", "42424242", "99999999", "66669420" })]
        [AnswerGenerator.Integers(10000000, 99999999, 1, "00000000")]
        EntryNumberFourNumber1,
        [SouvenirQuestion("What was the second number shown in {0}?", "Entry Number Four", TwoColumns4Answers, ExampleAnswers = new[] { "01234567", "42424242", "99999999", "66669420" })]
        [AnswerGenerator.Integers(0, 99999999, 1, "00000000")]
        EntryNumberFourNumber2,
        [SouvenirQuestion("What was the third number shown in {0}?", "Entry Number Four", TwoColumns4Answers, ExampleAnswers = new[] { "01234567", "42424242", "99999999", "66669420" })]
        [AnswerGenerator.Integers(0, 99999999, 1, "00000000")]
        EntryNumberFourNumber3,
        [SouvenirQuestion("What was the expected fourth entry in {0}?", "Entry Number Four", TwoColumns4Answers, ExampleAnswers = new[] { "01234567", "42424242", "99999999", "66669420" })]
        [AnswerGenerator.Integers(0, 99999999, 1, "00000000")]
        EntryNumberFourExpected,
        [SouvenirQuestion("What was the constant coefficient in {0}?", "Entry Number Four", TwoColumns4Answers, ExampleAnswers = new[] { "01234567", "42424242", "99999999", "66669420" })]
        [AnswerGenerator.Integers(10000000, 99999999, 1, "00000000")]
        EntryNumberFourCoeff,

        [SouvenirQuestion("What was the {1} number shown in {0}?", "Entry Number One", TwoColumns4Answers, ExampleAnswers = new[] { "01234567", "42424242", "99999999", "66669420" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 99999999, 1, "00000000")]
        EntryNumberOneNumbers,
        [SouvenirQuestion("What was the expected first entry in {0}?", "Entry Number One", TwoColumns4Answers, ExampleAnswers = new[] { "01234567", "42424242", "99999999", "66669420" })]
        [AnswerGenerator.Integers(10000000, 99999999, 1, "00000000")]
        EntryNumberOneExpected,
        [SouvenirQuestion("What was the constant coefficient in {0}?", "Entry Number One", TwoColumns4Answers, ExampleAnswers = new[] { "01234567", "42424242", "99999999", "66669420" })]
        [AnswerGenerator.Integers(10000000, 99999999, 1, "00000000")]
        EntryNumberOneCoeff,

        [SouvenirQuestion("What word was asked to be spelled in {0}?", "Épelle-moi Ça", TwoColumns4Answers, ExampleAnswers = new[] { "abasourdi", "aberrant", "abrasive", "acatalectique", "accueil", "acrobatie", "aligot", "amphigourique", "analgésiante", "antipasti" })]
        EpelleMoiCaWord,

        [SouvenirQuestion("What was the displayed symbol in {0}?", "Equations X", ThreeColumns6Answers, "H(T)", "P", "\u03C7", "\u03C9", "Z(T)", "\u03C4", "\u03BC", "\u03B1", "K")]
        EquationsXSymbols,

        [SouvenirQuestion("What was the beat for the {1} arrow from the bottom in {0}?", "Etterna", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 32)]
        EtternaNumber,

        [SouvenirQuestion("What was the starting target planet in {0}?", "Exoplanets", TwoColumns4Answers, "outer", "middle", "inner", "none", TranslateAnswers = true)]
        ExoplanetsStartingTargetPlanet,
        [SouvenirQuestion("What was the starting target digit in {0}?", "Exoplanets", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        ExoplanetsStartingTargetDigit,
        [SouvenirQuestion("What was the final target planet in {0}?", "Exoplanets", TwoColumns4Answers, "outer", "middle", "inner", "none", TranslateAnswers = true)]
        ExoplanetsTargetPlanet,
        [SouvenirQuestion("What was the final target digit in {0}?", "Exoplanets", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        ExoplanetsTargetDigit,

        [SouvenirQuestion("What was one of the prime numbers chosen in {0}?", "Factoring Maze", ThreeColumns6Answers, "2", "3", "5", "7", "11", "13", "17", "19", "23", "29")]
        FactoringMazeChosenPrimes,

        [SouvenirQuestion("What room did you start in in {0}?", "Factory Maze", OneColumn4Answers, "Bathroom", "Assembly Line", "Cafeteria", "Room A9", "Broom Closet", "Basement", "Copy Room", "Unnecessarily Long-Named Room", "Library", "Break Room", "Empty Room with Two Doors", "Arcade", "Classroom", "Module Testing Room", "Music Studio", "Computer Room", "Infirmary", "Bomb Room", "Space", "Storage Room", "Lounge", "Conference Room", "Kitchen", "Incinerator")]
        FactoryMazeStartRoom,

        [SouvenirQuestion("What was the last pair of letters in {0}?", "Fast Math", ThreeColumns6Answers, null, ExampleAnswers = new[] { "CT", "DK", "SA", "SG", "SX", "TX", "TZ", "XP", "XX", "ZB" })]
        FastMathLastLetters,

        [SouvenirQuestion("Which button referred to the {1} button in reading order in {0}?", "Faulty Buttons", ThreeColumns6Answers, Type = AnswerType.Grid, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Grid(4, 4)]
        FaultyButtonsReferredToThisButton,
        [SouvenirQuestion("Which button did the {1} button in reading order refer to in {0}?", "Faulty Buttons", ThreeColumns6Answers, Type = AnswerType.Grid, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Grid(4, 4)]
        FaultyButtonsThisButtonReferredTo,

        [SouvenirQuestion("What was the exit coordinate in {0}?", "Faulty RGB Maze", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-G", "1-7")]
        FaultyRGBMazeExit,
        [SouvenirQuestion("Where was the {1} key in {0}?", "Faulty RGB Maze", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-G", "1-7")]
        FaultyRGBMazeKeys,
        [SouvenirQuestion("Which maze number was the {1} maze in {0}?", "Faulty RGB Maze", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("0-9a-f")]
        FaultyRGBMazeNumber,

        [SouvenirQuestion("What was the day displayed in the {1} stage of {0}?", "Find The Date", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 31)]
        FindTheDateDay,
        [SouvenirQuestion("What was the month displayed in the {1} stage of {0}?", "Find The Date", TwoColumns4Answers, "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        FindTheDateMonth,
        [SouvenirQuestion("What was the year displayed in the {1} stage of {0}?", "Find The Date", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 2899, "000")]
        FindTheDateYear,

        [SouvenirQuestion("Which of these words was on the display in {0}?", "Five Letter Words", ThreeColumns6Answers, ExampleAnswers = new[] { "ABAFF", "MAYOR", "PANUS", "FRIZE", "NIRIS", "TEJON" })]
        FiveLetterWordsDisplayedWords,

        [SouvenirQuestion("What was the {1} digit on the {2} display of {0}?", "FizzBuzz", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { QandA.Ordinal, "top", QandA.Ordinal, "middle", QandA.Ordinal, "bottom" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { false, true })]
        [AnswerGenerator.Integers(0, 9)]
        FizzBuzzDisplayedNumbers,

        [SouvenirQuestion("What was the displayed number in {0}?", "Flags", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 7)]
        FlagsDisplayedNumber,
        [SouvenirQuestion("What was the main country flag in {0}?", "Flags", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "FlagsSprites")]
        FlagsMainCountry,
        [SouvenirQuestion("Which of these country flags was shown, but not the main country flag, in {0}?", "Flags", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "FlagsSprites")]
        FlagsCountries,

        [SouvenirQuestion("What number was displayed on {0}?", "Flashing Arrows", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 99)]
        FlashingArrowsDisplayedValue,
        [SouvenirQuestion("What color flashed {1} black on the relevant arrow in {0}?", "Flashing Arrows", ThreeColumns6Answers, ExampleAnswers = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White" },
            ExampleExtraFormatArguments = new[] { "before", "after" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        FlashingArrowsReferredArrow,

        [SouvenirQuestion("How many times did the {1} LED flash {2} on {0}?", "Flashing Lights", ThreeColumns6Answers, TranslateFormatArgs = new[] { true, true },
            ExampleExtraFormatArguments = new[] { "top", "cyan", "top", "green", "top", "red", "top", "purple", "top", "orange", "bottom", "cyan", "bottom", "green", "bottom", "red", "bottom", "purple", "bottom", "orange" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(0, 12)]
        FlashingLightsLEDFrequency,

        [SouvenirQuestion("Which module’s flavor text was shown in {0}?", "Flavor Text", OneColumn4Answers, ExampleAnswers = new[] { "Totally Accurate Minecraft Simulator", "Rock-Paper-Scissors-Lizard-Spock", "The Octadecayotton", "Power Button" })]
        FlavorTextModule,

        [SouvenirQuestion("Which module’s flavor text was shown in the {1} stage of {0}?", "Flavor Text", OneColumn4Answers, ExampleAnswers = new[] { "Totally Accurate Minecraft Simulator", "Rock-Paper-Scissors-Lizard-Spock", "The Octadecayotton", "Power Button" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        FlavorTextEXModule,

        [SouvenirQuestion("Which fly was present, but not in the solution in {0}?", "Flyswatting", ThreeColumns6Answers, null)]
        [AnswerGenerator.Strings('A', 'Z')]
        FlyswattingUnpressed,

        [SouvenirQuestion("What was the {1} flashing direction in {0}?", "Follow Me", TwoColumns4Answers, "Up", "Down", "Left", "Right", ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        FollowMeDisplayedPath,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Forest Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        ForestCipherScreen,

        [SouvenirQuestion("What colors were the cylinders during the {1} stage of {0}?", "Forget Any Color", OneColumn4Answers, null,
            ExampleAnswers = new[] { "Orange, Yellow, Green", "Yellow, Cyan, Purple", "Green, Purple, Orange", "Green, Blue, Purple" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetAnyColorCylinder,
        [SouvenirQuestion("Which figure was used during the {1} stage of {0}?", "Forget Any Color", ThreeColumns6Answers, "LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetAnyColorSequence,

        [SouvenirQuestion("What was the {1} displayed digit in the first stage of {0}?", "Forget Everything", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetEverythingStageOneDisplay,

        [SouvenirQuestion("What number was in the {1} position of the initial puzzle in {0}?", "Forget Me", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 8)]
        ForgetMeInitialState,

        [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", "Forget Me Not", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetMeNotDisplayedDigits,

        [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Forget Me Now", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetMeNowDisplayedDigits,

        [SouvenirQuestion("What was the {1} digit of the answer in {0}?", "Forget’s Ultimate Showdown", ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetsUltimateShowdownAnswer,
        [SouvenirQuestion("What was the {1} digit of the initial number in {0}?", "Forget’s Ultimate Showdown", ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetsUltimateShowdownInitial,
        [SouvenirQuestion("What was the {1} digit of the bottom number in {0}?", "Forget’s Ultimate Showdown", ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetsUltimateShowdownBottom,
        [SouvenirQuestion("What was the {1} method used in {0}?", "Forget’s Ultimate Showdown", OneColumn4Answers, "Forget Me Not", "Simon’s Stages", "Forget Me Later", "Forget Infinity", "A>N<D", "Forget Me Now", "Forget Everything", "Forget Us Not",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetsUltimateShowdownMethod,

        [SouvenirQuestion("What number was on the gear during stage {1} of {0}?", "Forget The Colors", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetTheColorsGearNumber,
        [SouvenirQuestion("What number was on the large display during stage {1} of {0}?", "Forget The Colors", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 990)]
        ForgetTheColorsLargeDisplay,
        [SouvenirQuestion("What was the last decimal in the sine number received during stage {1} of {0}?", "Forget The Colors", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetTheColorsSineNumber,
        [SouvenirQuestion("What color was the gear during stage {1} of {0}?", "Forget The Colors", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetTheColorsGearColor,
        [SouvenirQuestion("Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?", "Forget The Colors", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetTheColorsRuleColor,

        [SouvenirQuestion("What color was the LED in the {1} stage of {0}?", "Forget This", ThreeColumns6Answers, "Cyan", "Magenta", "Yellow", "Black", "White", "Green",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetThisColors,
        [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", "Forget This", ThreeColumns6Answers, Type = AnswerType.AsciiMazeFont, // Use this font to make 0 and O distinguishable from each other.
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("0-9A-Z")]
        ForgetThisDigits,

        [SouvenirQuestion("What was the player token in {0}?", "Free Parking", ThreeColumns6Answers, "Dog", "Wheelbarrow", "Cat", "Iron", "Top Hat", "Car", "Battleship")]
        FreeParkingToken,

        [SouvenirQuestion("What was the last digit of your first query’s result in {0}?", "Functions", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        FunctionsLastDigit,
        [SouvenirQuestion("What number was to the left of the displayed letter in {0}?", "Functions", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 999)]
        FunctionsLeftNumber,
        [SouvenirQuestion("What letter was displayed in {0}?", "Functions", ThreeColumns6Answers)]
        [AnswerGenerator.Strings('A', 'Z')]
        FunctionsLetter,
        [SouvenirQuestion("What number was to the right of the displayed letter in {0}?", "Functions", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 999)]
        FunctionsRightNumber,

        [SouvenirQuestion("What color flashed {1} in {0}?", "Fuse Box", TwoColumns4Answers, Type = AnswerType.Sprites, AddThe = true, IsEntireQuestionSprite = true, SpriteField = "FuseBoxColorSprites",
            ExampleExtraFormatArgumentGroupSize = 1, ExampleExtraFormatArguments = new[] { QandA.Ordinal })]
        FuseBoxFlashes,
        [SouvenirQuestion("What arrow was shown {1} in {0}?", "Fuse Box", TwoColumns4Answers, Type = AnswerType.Sprites, AddThe = true, IsEntireQuestionSprite = true, SpriteField = "FuseBoxArrowSprites",
            ExampleExtraFormatArgumentGroupSize = 1, ExampleExtraFormatArguments = new[] { QandA.Ordinal })]
        FuseBoxArrows,

        [SouvenirQuestion("What was your current weapon in {0}?", "Gadgetron Vendor", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "GadgetronVendorIconSprites")]
        GadgetronVendorCurrentWeapon,
        [SouvenirQuestion("What was the weapon up for sale in {0}?", "Gadgetron Vendor", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "GadgetronVendorWeaponSprites")]
        GadgetronVendorWeaponForSale,

        [SouvenirQuestion("Which of these was a color combination that occurred in {0}?", "Game of Life Cruel", TwoColumns4Answers, null,
            ExampleAnswers = new[] { "Red/Orange", "Orange/Yellow", "Yellow/Green", "Green/Blue" })]
        GameOfLifeCruelColors,

        [SouvenirQuestion("What were the numbers on {0}?", "Gamepad", ThreeColumns6Answers, null, AddThe = true)]
        [AnswerGenerator.Strings("2*0-9", ":", "2*0-9")]
        GamepadNumbers,

        [SouvenirQuestion("Which faction did {1} claim to be in {0}?", "Garnet Thief", TwoColumns4Answers, "Mafia", "Cartel", "Beggar", "Police", AddThe = true,
            ExampleExtraFormatArguments = new[] { "Jungmoon", "Yeonseung", "Jinho", "Dongmin", "Kyunghoon", "Kyungran", "Yoohyun", "Junseok", "Sangmin", "Yohwan", "Yoonsun", "Hyunmin", "Junghyun" }, ExampleExtraFormatArgumentGroupSize = 1)]
        GarnetThiefClaim,

        [SouvenirQuestion("What was the language sung in {0}?", "Girlfriend", TwoColumns4Answers, "English", "French", "German", "Italian", "Japanese", "Mandarin", "Portuguese", "Spanish")]
        GirlfriendLanguage,

        [SouvenirQuestion("What was the cycling bit sequence in {0}?", "Glitched Button", OneColumn4Answers, null, AddThe = true)]
        [AnswerGenerator.Strings(12, "01")]
        GlitchedButtonSequence,

        [SouvenirQuestion("What was the {1} coordinate on the display in {0}?", "Gray Button", ThreeColumns6Answers, AddThe = true,
            ExampleExtraFormatArguments = new[] { "horizontal", "vertical" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Integers(0, 9)]
        GrayButtonCoordinates,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Gray Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "ASSUME", "EMBRYO", "GAMBIT", "LAMENT", "LEARNT", "NEBULA", "NEEDED", "OBJECT", "PHOTON", "QUARRY" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        GrayCipherScreen,

        [SouvenirQuestion("What was the {1} color in {0}?", "Great Void", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "Cyan", "White", AddThe = true, TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        GreatVoidColor,
        [SouvenirQuestion("What was the {1} digit in {0}?", "Great Void", ThreeColumns6Answers, null, AddThe = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        GreatVoidDigit,

        [SouvenirQuestion("What was the last number on the display on {0}?", "Green Arrows", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 99, "00")]
        GreenArrowsLastScreen,

        [SouvenirQuestion("What was the word submitted in {0}?", "Green Button", ThreeColumns6Answers, null, AddThe = true, ExampleAnswers = new[] { "model", "vigor", "pedal", "relic", "lemon", "spoke", "brick", "berry", "equal", "loopy", "trash", "learn", "amuse", "valve", "bench", "igloo", "maybe", "fluid", "truck", "torch" })]
        GreenButtonWord,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Green Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "BARBER", "ELIXIR", "HARDLY", "JACKED", "LAMEST", "OCTAVE", "UMPIRE", "UNVEIL", "WAFFLE", "ZONING" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        GreenCipherScreen,

        [SouvenirQuestion("What was the starting location in {0}?", "Gridlock", ThreeColumns6Answers, Type = AnswerType.Grid)]
        [AnswerGenerator.Grid(4, 4)]
        GridLockStartingLocation,
        [SouvenirQuestion("What was the ending location in {0}?", "Gridlock", ThreeColumns6Answers, Type = AnswerType.Grid)]
        [AnswerGenerator.Grid(4, 4)]
        GridLockEndingLocation,
        [SouvenirQuestion("What was the starting color in {0}?", "Gridlock", TwoColumns4Answers, "Green", "Yellow", "Red", "Blue", TranslateAnswers = true)]
        GridLockStartingColor,

        [SouvenirQuestion("What was the first item shown in {0}?", "Grocery Store", TwoColumns4Answers, null, ExampleAnswers = new[] { "Cheese", "Coffee", "Flour", "Glass Cleaner", "Pepper", "Salt", "Soup", "Steak", "Toilet Paper", "Turkey" })]
        GroceryStoreFirstItem,

        [SouvenirQuestion("What was the gryphon’s name in {0}?", "Gryphons", ThreeColumns6Answers, "Gabe", "Gabriel", "Gad", "Gael", "Gage", "Gaia", "Galena", "Galina", "Gallo", "Gallagher", "Ganymede", "Ganzorig", "Garen", "Gareth", "Garland", "Garnett", "Garret", "Garrick", "Gary", "Gaspar", "Gaston", "Gauthier", "Gavin", "Gaz", "Geena", "Geff", "Geffrey", "Gela", "Geltrude", "Gene", "Geneva", "Genevieve", "Geno", "Gentius", "Geoff", "George", "Georgio", "Georgius", "Gerald", "Geraldo", "Gerda", "Gerel", "Gergana", "Gerhardt", "Gerhart", "Gerry", "Gertrude", "Gervais", "Gervaise", "Ghada", "Ghadir", "Ghassan", "Ghjulia", "Gia", "Giada", "Giampaolo", "Giampiero", "Giancarlo", "Giana", "Gianna", "Gideon", "Gidon", "Gilbert", "Gilberta", "Gino", "Giorgio", "Giovanni", "Giove", "Girish", "Girisha", "Gisela", "Giselle", "Gittel", "Gizella", "Gjorgji", "Gladys", "Glauco", "Glaukos", "Glen", "Glenn", "Godfrey", "Godfried", "Gojko", "Gol", "Golda", "Gona", "Gonzalo", "Gordie", "Gordy", "Goretti", "Gosia", "Gosse", "Gotzon", "Gotzone", "Gowri", "Gozzo", "Grace", "Gracia", "Griffith", "Gwynnyth")]
        GryphonsName,
        [SouvenirQuestion("What was the gryphon’s age in {0}?", "Gryphons", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(23, 34)]
        GryphonsAge,

        [SouvenirQuestion("Who was the person recalled in {0}?", "Guess Who?", ThreeColumns6Answers, null, ExampleAnswers = new[] { "Aaron", "Albin", "Andre" })]
        GuessWhoPerson,

        [SouvenirQuestion("What was the transmitted letter in {0}?", "h", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-Z")]
        HLetter,

        [SouvenirQuestion("What was the given number in {0}?", "Hereditary Base Notation", TwoColumns4Answers, null, ExampleAnswers = new[] { "12", "33", "46", "112", "356" })]
        HereditaryBaseNotationInitialNumber,

        [SouvenirQuestion("What label was printed on {0}?", "Hexabutton", ThreeColumns6Answers, "Jump", "Boom", "Claim", "Button", "Hold", "Blue", AddThe = true)]
        HexabuttonLabel,

        [SouvenirQuestion("What was the color of the pawn in {0}?", "Hexamaze", ThreeColumns6Answers, "Red", "Yellow", "Green", "Cyan", "Blue", "Pink", TranslateAnswers = true)]
        HexamazePawnColor,

        [SouvenirQuestion("What were the deciphered letters in {0}?", "hexOS", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("2* A-Z")]
        HexOSCipher,
        [SouvenirQuestion("What was the deciphered phrase in {0}?", "hexOS", ThreeColumns6Answers, ExampleAnswers = new[] { "a maze", "someda", "but i ", "they h", "shorn o", "more s", "if onl", "grew b" })]
        HexOSOctCipher,
        [SouvenirQuestion("What was the {1} 3-digit number cycled by the screen in {0}?", "hexOS", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 999, "000")]
        HexOSScreen,
        [SouvenirQuestion("What were the rhythm values in {0}?", "hexOS", ThreeColumns6Answers, ExampleAnswers = new[] { "0001", "0012", "0123", "1230", "2300", "3000" })]
        HexOSSum,

        [SouvenirQuestion("What was the color of the main LED in {0}?", "Hidden Colors", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Magenta", "White", TranslateAnswers = true)]
        HiddenColorsLED,

        [SouvenirQuestion("What was the position of the player in {0}?", "High Score", TwoColumns4Answers, "1st", "2nd", "3rd", "4th", "5th", AddThe = true)]
        HighScorePosition,
        [SouvenirQuestion("What was the score of the player in {0}?", "High Score", TwoColumns4Answers, AddThe = true)]
        [AnswerGenerator.Integers(1750, 999990, 10)]
        HighScoreScore,

        [SouvenirQuestion("What was the {1} in {0}?", "Hill Cycle", TwoColumns4Answers, "Adverted", "Advocate", "Allocate", "Altering", "Binormal", "Binomial", "Bulkhead", "Bulleted", "Ciphered", "Circuits", "Compiler", "Commando", "Decimate", "Deceived", "Discover", "Disposal", "Encipher", "Entrance", "Equators", "Equalise", "Finalise", "Finnicky", "Fortress", "Forwards", "Gauntlet", "Gambling", "Gatepost", "Gateways", "Hazarded", "Haziness", "Hungrier", "Huntress", "Incoming", "Indirect", "Illusion", "Illumine", "Jigsawed", "Jiggling", "Junction", "Junkyard", "Kilowatt", "Kilobyte", "Knockout", "Knocking", "Lingered", "Linearly", "Linkages", "Linkwork", "Monogram", "Monomial", "Multiply", "Multiton", "Nanogram", "Nanowatt", "Numerous", "Numerals", "Ordinals", "Ordering", "Observed", "Obscured", "Progress", "Projects", "Prophase", "Prophecy", "Quadrant", "Quadrics", "Quartile", "Quartics", "Reversed", "Revolved", "Rotators", "Relaying", "Stanzaic", "Standout", "Stopping", "Stopword", "Trigonal", "Trickier", "Toggling", "Together", "Underway", "Underlie", "Ultrahot", "Ultrared", "Vicinity", "Viceless", "Volition", "Volatile", "Whatness", "Whatsits", "Whatever", "Whatnots", "Yearlong", "Yeasayer", "Yokozuna", "Yourself", "Zippered", "Zygomata", "Zugzwang", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        HillCycleWord,

        [SouvenirQuestion("Which of these hinges was initially {1} {0}?", "Hinges", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "HingesSprites",
            ExampleExtraFormatArguments = new[] { "present on", "absent from" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        HingesInitialHinges,

        [SouvenirQuestion("Which House was {1} solved\u00a0for in {0}?", "Hogwarts", TwoColumns4Answers, "Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { "Binary Puzzle", "Zoni", "Rock-Paper- Scissors-L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HogwartsHouse,
        [SouvenirQuestion("Which module was solved\u00a0for {1} in {0}?", "Hogwarts", OneColumn4Answers, null, ExampleAnswers = new[] { "Binary Puzzle", "Zoni", "Rock-Paper-Scissors-L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards" },
            ExampleExtraFormatArguments = new[] { "Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        HogwartsModule,

        [SouvenirQuestion("What was the name of the {1} shadow shown in {0}?", "Hold Ups", OneColumn4Answers, "Mandrake", "Silky", "Koropokguru", "Nue", "Jack Frost", "Leanan Sidhe", "Hua Po", "Orthrus", "Lamia", "Bicorn", "Kelpie", "Apsaras", "Makami", "Nekomata", "Sandman", "Naga", "Agathion", "Berith", "Mokoi", "Inugami", "High Pixie", "Yaksini", "Anzu", "Take-Minakata", "Thoth", "Isis", "Incubis", "Onmoraki", "Koppa-Tengu", "Orobas", "Rakshasa", "Pixie", "Angel", "Jack O' Lantern", "Succubus", "Andras",
        ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        HoldUpsShadows,

        [SouvenirQuestion("In what position was the button pressed on the {1} stage of {0}?", "Horrible Memory", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 6)]
        HorribleMemoryPositions,
        [SouvenirQuestion("What was the label of the button pressed on the {1} stage of {0}?", "Horrible Memory", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 6)]
        HorribleMemoryLabels,
        [SouvenirQuestion("What color was the button pressed on the {1} stage of {0}?", "Horrible Memory", ThreeColumns6Answers, "blue", "green", "red", "orange", "purple", "pink", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        HorribleMemoryColors,

        [SouvenirQuestion("What was the {1} displayed phrase in {0}?", "Homophones", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1,
            ExampleAnswers = new[] { "i", "C", "L", "1", "sees", "leemer", "aye-aye", "One" })]
        HomophonesDisplayedPhrases,

        [SouvenirQuestion("Which was a descriptor shown in {1} in {0}?", "Human Resources", TwoColumns4Answers, "Intellectual", "Deviser", "Confidant", "Helper", "Auditor", "Innovator", "Defender", "Chameleon", "Director", "Designer", "Educator", "Advocate", "Manager", "Showman", "Contributor", "Entertainer",
            ExampleExtraFormatArguments = new[] { "red", "green" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        HumanResourcesDescriptors,
        [SouvenirQuestion("Who was {1} in {0}?", "Human Resources", ThreeColumns6Answers, "Rebecca", "Damian", "Jean", "Mike", "River", "Samuel", "Yoshi", "Caleb", "Ashley", "Tim", "Eliott", "Ursula", "Silas", "Noah", "Quinn", "Dylan",
            ExampleExtraFormatArguments = new[] { "fired", "hired" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        HumanResourcesHiredFired,

        [SouvenirQuestion("Which of the first three stages of {0} had the {1} symbol {2}?", "Hunting", TwoColumns4Answers, "none", "first", "second", "first two", "third", "first & third", "second & third", "all three", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "column", QandA.Ordinal, "row", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        HuntingColumnsRows,

        [SouvenirQuestion("What was the {1} rotation in {0}?", "Hypercube", ThreeColumns6Answers, "XY", "YX", "XZ", "ZX", "XW", "WX", "YZ", "ZY", "YW", "WY", "ZW", "WZ", AddThe = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        HypercubeRotations,

        [SouvenirQuestion("What was the {1} character of the hyperlink in {0}?", "Hyperlink", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "_", "-", AddThe = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        HyperlinkCharacters,
        [SouvenirQuestion("Which module was referenced on {0}?", "Hyperlink", OneColumn4Answers, "3D Maze", "Adjacent Letters", "Adventure Game", "Alphabet", "Anagrams", "Answering Questions", "Astrology", "Backgrounds", "Battleship", "Big Circle", "Bitmaps", "Blind Alley", "Blind Maze", "Braille", "Broken Buttons", "Burglar Alarm", "Button Sequence", "Caesar Cipher", "Capacitor Discharge", "Cheap Checkout", "Chess", "Chord Qualities", "Color Flash", "Colored Squares", "Colored Switches", "Combination Lock", "Complicated Buttons", "Complicated Wires", "Connection Check", "Cooking", "Coordinates", "Crazy Talk", "Creation", "Cryptography", "Double-Oh", "Emoji Math", "English Test", "European Travel", "Faulty Backgrounds", "Filibuster", "Follow The Leader", "Foreign Exchange Rates", "Forget Me Not", "Friendship", "Game Of Life Cruel", "Game Of Life Simple", "Hexamaze", "HTTP Response", "Hunting", "Ice Cream", "Keypad", "Knob", "Laundry", "Letter Keys", "Light Cycle", "Lights Out", "Listening", "Logic", "Math", "Maze", "Memory", "Microcontroller", "Module Against Humanity", "Monsplode Trading Cards", "Monsplode, Fight!", "Morse Code", "Morsematics", "Mortal Kombat", "Motion Sense", "Mouse In The Maze", "Murder", "Mystic Square", "Neutralization", "Number Pad", "Orientation Cube", "Password", "Perspective Pegs", "Piano Keys", "Plumbing", "Probing", "Resistors", "Rock-Paper-Scissors-Lizard-Spock", "Rotary Phone", "Round Keypad", "Safety Safe", "Sea Shells", "Semaphore", "Shape Shift", "Silly Slots", "Simon Says", "Simon Screams", "Simon States", "Skewed Slots", "Souvenir", "Square Button", "Switches", "Symbolic Coordinates", "Symbolic Password", "Tetris", "Text Field", "The Bulb", "The Button", "The Clock", "The Gamepad", "The iPhone", "The Moon", "The Stopwatch", "The Sun", "The Swan", "Third Base", "Tic-Tac-Toe", "Turn The Key", "Turn The Keys", "Two Bits", "Venting Gas", "Who’s On First", "Who’s That Monsplode", "Wire Placement", "Wire Sequence", "Wires", "Word Scramble", "Word Search", "Zoo", AddThe = true)]
        HyperlinkAnswer,

        [SouvenirQuestion("Which one of these flavours {1} to the {2} customer in {0}?", "Ice Cream", OneColumn4Answers, "Tutti Frutti", "Rocky Road", "Raspberry Ripple", "Double Chocolate", "Double Strawberry", "Cookies & Cream", "Neapolitan", "Mint Chocolate Chip", "The Classic", "Vanilla", TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "was on offer, but not sold,", QandA.Ordinal, "was not on offer", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        IceCreamFlavour,
        [SouvenirQuestion("Who was the {1} customer in {0}?", "Ice Cream", ThreeColumns6Answers, "Mike", "Tim", "Tom", "Dave", "Adam", "Cheryl", "Sean", "Ashley", "Jessica", "Taylor", "Simon", "Sally", "Jade", "Sam", "Gary", "Victor", "George", "Jacob", "Pat", "Bob",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        IceCreamCustomer,

        [SouvenirQuestion("What was the {1} shape used in {0}?", "Identification Crisis", TwoColumns4Answers, "Circle", "Square", "Diamond", "Heart", "Star", "Triangle", "Pentagon", "Hexagon",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        IdentificationCrisisShape,
        [SouvenirQuestion("What was the {1} identification module used in {0}?", "Identification Crisis", OneColumn4Answers, "Morse Identification", "Boozleglyph Identification", "Plant Identification", "Pickup Identification", "Emotiguy Identification", "Ars Goetia Identification", "Mii Identification", "Customer identification", "Spongebob Birthday Identification", "VTuber Identification",
           ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        IdentificationCrisisDataset,

        [SouvenirQuestion("Which hair color {1} listed in {0}?", "Identity Parade", TwoColumns4Answers, "Black", "Blonde", "Brown", "Grey", "Red", "White",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        IdentityParadeHairColors,
        [SouvenirQuestion("Which build {1} listed in {0}?", "Identity Parade", TwoColumns4Answers, "Fat", "Hunched", "Muscular", "Short", "Slim", "Tall",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        IdentityParadeBuilds,
        [SouvenirQuestion("Which attire {1} listed in {0}?", "Identity Parade", TwoColumns4Answers, "Blazer", "Hoodie", "Jumper", "Suit", "T-shirt", "Tank top",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        IdentityParadeAttires,

        [SouvenirQuestion("Which module was {0} pretending to be?", "Impostor", OneColumn4Answers, ExampleAnswers = new[] { "Ice Cream", "Microcontroller", "Sea Shells", "Combination Lock" }, AddThe = true)]
        ImpostorDisguise,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Indigo Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "BEAVER", "INDENT", "LONELY", "PILLAR", "REFUGE", "RIPPED", "STOLEN", "TUMBLE", "WHIMSY", "WYVERN" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        IndigoCipherScreen,

        [SouvenirQuestion("What was the selected word in {0}?", "Infinite Loop", TwoColumns4Answers, "anchor", "axions", "brutal", "bunker", "ceased", "cypher", "demote", "devoid", "ejects", "expend", "fixate", "fondly", "geyser", "guitar", "hexing", "hybrid", "incite", "inject", "jacked", "jigsaw", "kayaks", "komodo", "lazuli", "logjam", "maimed", "musket", "nebula", "nuking", "overdo", "oblong", "photon", "probed", "quartz", "quebec", "refute", "regime", "sierra", "swerve", "tenacy", "thymes", "ultima", "utopia", "valved", "viable", "wither", "wrench", "xenons", "xylose", "yanked", "yellow", "zigged", "zodiac")]
        InfiniteLoopSelectedWord,

        [SouvenirQuestion("Which ingredient was used in {0}?", "Ingredients", TwoColumns4Answers, "Veal", "Beef", "Quail", "FiletMignon", "Crab", "Scallop", "Lobster", "Sole", "Eel", "SeaBass", "Mussel", "Cod", "Pumpkin", "Zucchini", "Onion", "Tomato", "Eggplant", "Carrot", "Garlic", "Celery", "Morel", "Porcini", "Chanterelle", "Portobello", "BlackTruffle", "KingOysterMushroom", "BlackTrumpet", "MillerMushroom", "Cloves", "Rosemary", "Thyme", "BayLeaf", "Basil", "Dill", "Parsley", "Saffron", "Apricot", "Gooseberry", "Lemon", "Orange", "Raspberry", "Pear", "Blackberry", "Apple", "Cheese", "Chocolate", "Caviar", "Butter", "OliveOil", "Cornichon", "Rice", "Honey", "SourCherry", "Strawberry", "BloodOrange", "Banana", "Grapes", "Melon", "Watermelon")]
        IngredientsIngredients,
        [SouvenirQuestion("Which ingredient was listed but not used in {0}?", "Ingredients", TwoColumns4Answers, "Veal", "Beef", "Quail", "FiletMignon", "Crab", "Scallop", "Lobster", "Sole", "Eel", "SeaBass", "Mussel", "Cod", "Pumpkin", "Zucchini", "Onion", "Tomato", "Eggplant", "Carrot", "Garlic", "Celery", "Morel", "Porcini", "Chanterelle", "Portobello", "BlackTruffle", "KingOysterMushroom", "BlackTrumpet", "MillerMushroom", "Cloves", "Rosemary", "Thyme", "BayLeaf", "Basil", "Dill", "Parsley", "Saffron", "Apricot", "Gooseberry", "Lemon", "Orange", "Raspberry", "Pear", "Blackberry", "Apple", "Cheese", "Chocolate", "Caviar", "Butter", "OliveOil", "Cornichon", "Rice", "Honey", "SourCherry", "Strawberry", "BloodOrange", "Banana", "Grapes", "Melon", "Watermelon")]
        IngredientsNonIngredients,

        [SouvenirQuestion("What color was the LED in {0}?", "Inner Connections", ThreeColumns6Answers, "Black", "Blue", "Red", "White", "Yellow", "Green", TranslateAnswers = true)]
        InnerConnectionsLED,
        [SouvenirQuestion("What was the digit flashed in Morse in {0}?", "Inner Connections", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        InnerConnectionsMorse,

        [SouvenirQuestion("What was the symbol displayed in the {1} stage of {0}?", "Interpunct", ThreeColumns6Answers, "(", ",", ">", "/", "}", "]", "_", "-", "\"", "|", "»", ":", ".", "{", "<", "”", "«", "`", "[", "?", ")", "!", "\\", "'", ";",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        InterpunctDisplay,

        [SouvenirQuestion("What symbol was the correct answer in {0}?", "IPA", ThreeColumns6Answers, null,
            Type = AnswerType.DynamicFont, ExampleAnswers = new[] { "p", "b", "t", "d", "c", "ɟ", "k", "g", "q", "ɢ", "ʔ", "m", "n", "ɲ", "ŋ", "ʙ", "r", "ʀ", "ⱱ", "ɾ" })]
        IpaSymbol,

        [SouvenirQuestion("What was the {1} PIN digit in {0}?", "iPhone", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        [AnswerGenerator.Integers(0, 9)]
        iPhoneDigits,

        [SouvenirQuestion("Which symbol was on the first correctly pulled block in {0}?", "Jenga", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "JengaSprites")]
        JengaFirstBlock,

        [SouvenirQuestion("What number was wheel {1} in {0}?", "Jewel Vault", TwoColumns4Answers,
            ExampleExtraFormatArguments = new[] { "A", "B", "C", "D" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        [AnswerGenerator.Integers(1, 4)]
        JewelVaultWheels,

        [SouvenirQuestion("What was the {1} in {0}?", "Jumble Cycle", TwoColumns4Answers, "Adverted", "Advocate", "Allotype", "Allotted", "Binormal", "Binomial", "Bullhorn", "Bulwarks", "Connects", "Conquers", "Commando", "Compiler", "Deceived", "Decimate", "Dispatch", "Discrete", "Encrypts", "Encoding", "Equators", "Equalise", "Finalise", "Finnicky", "Formulae", "Fortunes", "Garrison", "Garnered", "Gatepost", "Gateways", "Hotlinks", "Hotheads", "Huntress", "Hundreds", "Incoming", "Indirect", "Illusory", "Illuding", "Journeys", "Jousting", "Junkyard", "Juncture", "Kilovolt", "Kilobyte", "Knocking", "Knowable", "Language", "Landmark", "Linkwork", "Lingered", "Monomial", "Monolith", "Multiton", "Mulcting", "Nanowatt", "Nanobots", "Numerous", "Numerate", "Ordering", "Ordinals", "Obstruct", "Obstacle", "Prophase", "Prophecy", "Postsync", "Positron", "Quartile", "Quartics", "Quirkish", "Quitters", "Reversed", "Revealed", "Relaying", "Relative", "Stanzaic", "Standout", "Stockade", "Stoccata", "Trigonal", "Trickier", "Tomogram", "Tomahawk", "Underway", "Undoings", "Ulterior", "Ultrahot", "Venomous", "Vendetta", "Volition", "Voluming", "Weakened", "Weaponed", "Whatness", "Whatsits", "Yearlong", "Yearning", "Yokozuna", "Yourself", "Zygomata", "Zygotene", "Zymology", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        JumbleCycleWord,

        [SouvenirQuestion("What was the color of this square in {0}?", "Juxtacolored Squares", ThreeColumns6Answers, "Red", "Blue", "Yellow", "Green", "Magenta", "Orange", "Cyan", "Purple", "Chestnut", "Brown", "Mauve", "Azure", "Jade", "Forest", "Gray", "Black", UsesQuestionSprite = true)]
        JuxtacoloredSquaresColorsByPosition,
        [SouvenirQuestion("Which square was {1} in {0}?", "Juxtacolored Squares", ThreeColumns6Answers, Type = AnswerType.Grid, ExampleExtraFormatArguments = new[] { "chestnut", "brown", "mauve", "azure", "jade", "forest", "gray", "black" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Grid(4, 4)]
        JuxtacoloredSquaresPositionsByColor,

        [SouvenirQuestion("What was the displayed word in the {1} stage of {0}?", "Kanji", TwoColumns4Answers, Type = AnswerType.JapaneseFont, ExampleAnswers = new[] { "ばくはつ", "でんき", "でんしゃ", "でんわ" }, ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        KanjiDisplayedWords,

        [SouvenirQuestion("What was a food item displayed in {0}?", "Kanye Encounter", TwoColumns4Answers,
            "Onion", "Corn", "big MIOLK", "Yam", "Corn Cube", "Egg", "Eggchips", "hamger", "Tyler the Creator", "Onionade", "Soup", "jeb", AddThe = true)]
        KanyeEncounterFoods,

        [SouvenirQuestion("Which number was displayed on the {1} button, but not part of the answer on {0}?", "Keypad Combinations", ThreeColumns6Answers,
           ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        KeypadCombinationWrongNumbers,

        [SouvenirQuestion("What was the position of the LED in {0}?", "Keypad Magnified", TwoColumns4Answers, "Top-left", "Top-right", "Bottom-left", "Bottom-right")]
        KeypadMagnifiedLED,

        [SouvenirQuestion("What were the first four letters on the display in {0}?", "Keywords", ThreeColumns6Answers, ExampleAnswers = new[] { "abvo", "pola", "drea", "buew", "utre", "oidy" })]
        KeywordsDisplayedKey,

        [SouvenirQuestion("Which way was the arrow pointing in {0}?", "Know Your Way", TwoColumns4Answers, "Up", "Down", "Left", "Right")]
        KnowYourWayArrow,
        [SouvenirQuestion("Which LED was green in {0}?", "Know Your Way", TwoColumns4Answers, "Top", "Bottom", "Right", "Left")]
        KnowYourWayLed,

        [SouvenirQuestion("Which square was {1} in {0}?", "Kudosudoku", ThreeColumns6Answers, null, Type = AnswerType.Grid, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "pre-filled", "not pre-filled" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Grid(4, 4)]
        KudosudokuPrefilled,

        [SouvenirQuestion("Where was one of the portals in layer {1} in {0}?", "Labyrinth", ThreeColumns6Answers, null, AddThe = true, Type = AnswerType.Grid, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "1 (Red)", "2 (Orange)", "3 (Yellow)", "4 (Green)", "5 (Blue)" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Grid(6, 7)]
        LabyrinthPortalLocations,
        [SouvenirQuestion("In which layer was this portal in {0}?", "Labyrinth", TwoColumns4Answers, "1 (Red)", "2 (Orange)", "3 (Yellow)", "4 (Green)", "5 (Blue)", AddThe = true,
            UsesQuestionSprite = true)]
        LabyrinthPortalStage,

        [SouvenirQuestion("Which light was on in {0}?", "Ladder Lottery", TwoColumns4Answers, "A", "B", "C", "D")]
        LadderLotteryLightOn,

        [SouvenirQuestion("Which color was present on the second ladder in {0}?", "Ladders", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Cyan", "Purple", "Gray", TranslateAnswers = true)]
        LaddersStage2Colors,
        [SouvenirQuestion("What color was missing on the third ladder in {0}?", "Ladders", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Cyan", "Purple", "Gray", TranslateAnswers = true)]
        LaddersStage3Missing,

        [SouvenirQuestion("Which of these squares was initially {1} in {0}?", "Langton’s Anteater", ThreeColumns6Answers, Type = AnswerType.Grid,
            ExampleExtraFormatArguments = new[] { "black", "white" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Grid(5, 5)]
        LangtonsAnteaterInitialState,

        [SouvenirQuestion("What was the number on the {1} hatch on {0}?", "Lasers", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Integers(1, 9)]
        LasersHatches,

        [SouvenirQuestion("What was the correct letter you pressed in the {1} stage of {0}?", "LED Encryption", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings('A', 'Z')]
        LEDEncryptionPressedLetters,

        [SouvenirQuestion("What color was {1} in {0}?", "LED Math", TwoColumns4Answers, "Red", "Blue", "Yellow", "Green", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { "LED A", "LED B", "the operator LED" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        LEDMathLights,

        [SouvenirQuestion("What was the initial color of the changed LED in {0}?", "LEDs", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Black", "White")]
        LEDsOriginalColor,

        [SouvenirQuestion("What were the dimensions of the {1} piece in {0}?", "LEGOs", ThreeColumns6Answers, "2×2", "3×1", "3×2", "4×1", "4×2", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue", "cyan", "magenta", "yellow" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LEGOsPieceDimensions,

        [SouvenirQuestion("What was the letter on the {1} display in {0}?", "Letter Math", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "left", "right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Strings("A-Z")]
        LetterMathDisplay,

        [SouvenirQuestion("What was the color of the {1} bulb in {0}?", "Light Bulbs", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Cyan", "Magenta", ExampleExtraFormatArguments = new[] { "left", "right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        LightBulbsColors,

        [SouvenirQuestion("What was the {1} function in {0}?", "Linq", ThreeColumns6Answers, "First", "Last", "Min", "Max", "Distinct", "Skip", "SkipLast", "Take", "TakeLast", "ElementAt", "Except", "Intersect", "Concat", "Append", "Prepend",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        LinqFunction,

        [SouvenirQuestion("Which year was displayed on {0}?", "Lion’s Share", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16")]
        LionsShareYear,
        [SouvenirQuestion("Which lion was present but removed in {0}?", "Lion’s Share", TwoColumns4Answers, null, ExampleAnswers = new[] { "Taka", "Mufasa", "Uru", "Ahadi", "Zama", "Mohatu", "Kion", "Kiara", "Kopa", "Kovu", "Vitani", "Nuka", "Mheetu", "Zira", "Nala", "Simba", "Sarabi", "Sarafina" })]
        LionsShareRemovedLions,

        [SouvenirQuestion("What was the correct code you entered in {0}?", "Listening", ThreeColumns6Answers, "&&&**", "&$#$&", "$#$*&", "#$$**", "$#$#*", "**$*#", "#$$&*", "##*$*", "$#*$&", "**#**", "#&&*#", "&#**&", "$&**#", "&#$$#", "$&&**", "#&$##", "&*$*$", "&$$&*", "#&&&&", "**$$$", "*&*&&", "*#&*&", "**###", "&&$&*", "&$**&", "#$#&$", "&#&&#", "$$*$*", "$&#$$", "&**$$", "$&&*&", "&$&##", "#&$*&", "$*$**", "*#$&&", "###&$", "*$$&$", "$*&##", "#&$&&", "$&$$*", "*$*$*")]
        ListeningCode,

        [SouvenirQuestion("What was the color of the {1} button in the {2} stage of {0}?", "Logical Buttons", TwoColumns4Answers, "Red", "Blue", "Green", "Yellow", "Purple", "White", "Orange", "Cyan", "Grey", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "top", QandA.Ordinal, "bottom-left", QandA.Ordinal, "bottom-right", QandA.Ordinal, }, ExampleExtraFormatArgumentGroupSize = 2)]
        LogicalButtonsColor,
        [SouvenirQuestion("What was the label on the {1} button in the {2} stage of {0}?", "Logical Buttons", TwoColumns4Answers, "Logic", "Color", "Label", "Button", "Wrong", "Boom", "No", "Wait", "Hmmm", TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "top", QandA.Ordinal, "bottom-left", QandA.Ordinal, "bottom-right", QandA.Ordinal, }, ExampleExtraFormatArgumentGroupSize = 2)]
        LogicalButtonsLabel,
        [SouvenirQuestion("What was the final operator in the {1} stage of {0}?", "Logical Buttons", ThreeColumns6Answers, "AND", "OR", "XOR", "NAND", "NOR", "XNOR",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        LogicalButtonsOperator,

        [SouvenirQuestion("What was {1} in {0}?", "Logic Gates", ThreeColumns6Answers, "AND", "OR", "XOR", "NAND", "NOR", "XNOR", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "gate A", "gate B", "gate C", "gate D", "gate E", "gate F", "gate G", "the duplicated gate" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LogicGatesGates,

        [SouvenirQuestion("What was the {1} letter on the button in {0}?", "Lombax Cubes", ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-Z")]
        LombaxCubesLetters,

        [SouvenirQuestion("Where did the {1} journey on {0} {2}?", "London Underground", OneColumn4Answers, null, AddThe = true, ExampleAnswers = new[] { "Great Portland Street", "High Street Kensington", "King's Cross St. Pancras", "Mornington Crescent", "Shepherd's Bush Market", "Tottenham Court Road", "Walthamstow Central", "White City/Wood Lane" }, TranslateFormatArgs = new[] { false, true },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, "depart from", QandA.Ordinal, "arrive to" }, ExampleExtraFormatArgumentGroupSize = 2)]
        LondonUndergroundStations,

        [SouvenirQuestion("What was the word on the top display on {0}?", "Long Words", ThreeColumns6Answers, null, ExampleAnswers = new[] { "ABOARD", "ABRUPT", "SAFEST", "LAMBDA", "NARROW", "ECHOES", "VALVES", "YONDER", "ZIGGED", "UNBIND" })]
        LongWordsWord,

        [SouvenirQuestion("What was on the display in the {1} stage of {0}?", "Mad Memory", ThreeColumns6Answers, "1", "2", "3", "4", "01", "02", "03", "04", "ONE", "TWO", "THREE", "FOUR", "WON", "TOO", "TREE", "FOR", ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        MadMemoryDisplays,

        [SouvenirQuestion("Which tile was part of the {1} matched pair in {0}?", "Mahjong", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "MahjongSprites",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        MahjongMatches,
        [SouvenirQuestion("Which tile was shown in the bottom-left of {0}?", "Mahjong", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "MahjongSprites")]
        MahjongCountingTile,

        [SouvenirQuestion("Who was a player, but not the Godfather, in {0}?", "Mafia", ThreeColumns6Answers, "Rob", "Tim", "Mary", "Briane", "Hunter", "Macy", "John", "Will", "Lacy", "Claire", "Kenny", "Rick", "Walter", "Bonnie", "Luke", "Bill", "Sarah", "Larry", "Kate", "Stacy", "Diane", "Mac", "Jim", "Clyde", "Tommy", "Lenny", "Molly", "Benny", "Phil", "Bob", "Gary", "Ted", "Kim", "Nate", "Cher", "Ron", "Thomas", "Sam", "Duke", "Jack", "Ed", "Ronny", "Terry", "Claira", "Nick", "Cob", "Ash", "Don", "Jerry", "Simon")]
        MafiaPlayers,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Magenta Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        MagentaCipherScreen,

        [SouvenirQuestion("What color was the text on the {1} button in {0}?", "M&Ms", ThreeColumns6Answers, "red", "green", "orange", "blue", "yellow", "brown", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        MandMsColors,
        [SouvenirQuestion("What was the text on the {1} button in {0}?", "M&Ms", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings(5, 'M', 'N')]
        MandMsLabels,

        [SouvenirQuestion("What color was the text on the {1} button in {0}?", "M&Ns", ThreeColumns6Answers, "red", "green", "orange", "blue", "yellow", "brown", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        MandNsColors,
        [SouvenirQuestion("What was the text on the correct button in {0}?", "M&Ns", ThreeColumns6Answers)]
        [AnswerGenerator.Strings(5, 'M', 'N')]
        MandNsLabel,

        [SouvenirQuestion("What bearing was signalled in {0}?", "Maritime Flags", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 359)]
        MaritimeFlagsBearing,
        [SouvenirQuestion("Which callsign was signalled in {0}?", "Maritime Flags", TwoColumns4Answers, "1stmate", "2ndmate", "3rdmate", "abandon", "admiral", "advance", "aground", "allides", "anchors", "athwart", "azimuth", "bailers", "ballast", "barrack", "beached", "beacons", "beamend", "beamsea", "bearing", "beating", "belayed", "bermuda", "bobstay", "boilers", "bollard", "bonnets", "boomkin", "bounder", "bowline", "brailed", "breadth", "bridges", "brigged", "bringto", "bulwark", "bumboat", "bumpkin", "burthen", "caboose", "capsize", "capstan", "captain", "caravel", "careens", "carrack", "carrier", "catboat", "cathead", "chained", "channel", "charley", "charter", "citadel", "cleared", "cleated", "clinker", "clipper", "coaming", "coasted", "consort", "convoys", "corinth", "cotchel", "counter", "cranzes", "crewing", "cringle", "crojack", "cruiser", "cutters", "dandies", "deadrun", "debunks", "derrick", "dipping", "disrate", "dogvane", "doldrum", "dolphin", "draught", "drifter", "drogues", "drydock", "dunnage", "dunsels", "earings", "echelon", "embayed", "ensigns", "escorts", "fairway", "falkusa", "fantail", "fardage", "fathoms", "fenders", "ferries", "fitting", "flanked", "flaring", "flattop", "flemish", "floated", "floored", "flotsam", "folding", "follows", "forcing", "forward", "foulies", "founder", "framing", "freight", "frigate", "funnels", "furling", "galleon", "galleys", "galliot", "gangway", "garbled", "general", "georges", "ghosted", "ginpole", "giveway", "gondola", "graving", "gripies", "grounds", "growler", "guineas", "gundeck", "gunport", "gunwale", "halyard", "hammock", "hampers", "hangars", "harbors", "harbour", "hauling", "hawsers", "heading", "headsea", "heaving", "herring", "hogging", "holiday", "huffler", "inboard", "inirons", "inshore", "instays", "inwater", "inwayof", "jackies", "jacktar", "jennies", "jetties", "jiggers", "joggles", "jollies", "juryrig", "keelson", "kellets", "kicking", "killick", "kitchen", "lanyard", "laydays", "lazaret", "leehelm", "leeside", "leeward", "liberty", "lighter", "lizards", "loading", "lockers", "lofting", "lolling", "lookout", "lubbers", "luffing", "luggers", "lugsail", "maewest", "manowar", "marconi", "mariner", "matelot", "mizzens", "mooring", "mousing", "narrows", "nippers", "officer", "offpier", "oilskin", "oldsalt", "onboard", "oreboat", "outhaul", "outward", "painter", "panting", "parcels", "parleys", "parrels", "passage", "pelagic", "pendant", "pennant", "pickets", "pinnace", "pintles", "pirates", "pivoted", "pursers", "pursued", "quarter", "quaying", "rabbets", "ratline", "reduced", "reefers", "repairs", "rigging", "ripraps", "rompers", "rowlock", "rudders", "ruffles", "rummage", "sagging", "sailors", "salties", "salvors", "sampans", "sampson", "sculled", "scupper", "scuttle", "seacock", "sealing", "seekers", "serving", "sextant", "shelter", "shipped", "shiprig", "sickbay", "skipper", "skysail", "slinged", "slipway", "snagged", "snotter", "spliced", "splices", "sponson", "sponsor", "springs", "squares", "stackie", "standon", "starter", "station", "steamer", "steered", "steeves", "steward", "stopper", "stovein", "stowage", "strikes", "sunfish", "swimmie", "systems", "tacking", "thwarts", "tinclad", "tompion", "tonnage", "topmast", "topsail", "torpedo", "tossers", "trading", "traffic", "tramper", "transom", "trawler", "trenail", "trennel", "trimmer", "trooper", "trunnel", "tugboat", "turntwo", "unships", "upbound", "vessels", "voicing", "voyager", "weather", "whalers", "wharves", "whelkie", "whistle", "winches", "windage", "working", "yardarm")]
        MaritimeFlagsCallsign,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Maroon Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        MaroonCipherScreen,

        [SouvenirQuestion("What was the answer in {0}?", "Mashematics", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 99)]
        MashematicsAnswer,
        [SouvenirQuestion("What was the {1} number in the equation on {0}?", "Mashematics", ThreeColumns6Answers, null, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 99)]
        MashematicsCalculation,

        [SouvenirQuestion("Which song was played in {0}?", "Master Tapes", OneColumn4Answers, "Redemption Song", "Do You Want To Know A Secret", "La Bamba", "Rock-A-Hula Baby", "Pickney Gal", "Dogs", "Young Americans", "Duvet", "Shadows Of Lost Days")]
        MasterTapesPlayedSong,

        [SouvenirQuestion("Which planet was present in the {1} stage of {0}?", "Match Refereeing", TwoColumns4Answers, Type = AnswerType.Sprites,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        MatchRefereeingPlanet,

        [SouvenirQuestion("What was the color of this tile before the shuffle on {0}?", "Math ’em", TwoColumns4Answers, "White", "Bronze", "Silver", "Gold", UsesQuestionSprite = true)]
        MathEmColor,
        [SouvenirQuestion("What was the design on this tile before the shuffle on {0}?", "Math ’em", ThreeColumns6Answers, null, UsesQuestionSprite = true, Type = AnswerType.Sprites, SpriteField = "MathEmSprites")]
        MathEmLabel,

        [SouvenirQuestion("Which word was part of the latest access code in {0}?", "Matrix", TwoColumns4Answers, "Twins", "Neo", "Seraph", "Cypher", "Persephone", "Tank", "Dozer", "Mouse", "Switch", "Architect", "Smith", "Merovingian", "Morpheus", "Niobe", "Bane", "Oracle", "Keymaker", "Link", "Trinity", "Apoc", AddThe = true)]
        MatrixAccessCode,
        [SouvenirQuestion("What was the glitched word in {0}?", "Matrix", TwoColumns4Answers, "headjack", "phone", "dystopia", "control", "paradise", "utopia", "version", "nebuchadnezzar", "zion", "fight", "utopia", "mind", "squiddy", "guns", "trace", "spoon", "machine", "red", "white", "paradise", "metacortex", "flint", "nova", "white", "rabbit", "follow", "matrix", "free", "neural", "mind", "fight", "free", "nova", "blue", "fields", "choice", "battery", "program", "flint", "headjack", "kungfu", "choi", "red", "blue", "pill", "jump", "program", "agent", "sentient", "squiddy", "dystopia", "rabbit", "jump", "code", "mirror", "cookie", "human", "pill", "follow", "version", "sentinel", "machine", "prison", "human", "fields", "battery", "code", "training", "guns", "hel", "elevator", "sentinel", "choi", "matrix", "nebuchadnezzar", "control", "metacortex", "sentient", "unplug", "hardwire", "trainman", "spoon", "cookie", "elevator", "hardwire", "choice", "trace", "mirror", "unplug", "interface", "prison", "kungfu", "interface", "neural", "trainman", "hel", "agent", "training", "zion", "phone", AddThe = true)]
        MatrixGlitchWord,

        [SouvenirQuestion("In which {1} was the starting position in {0}, counting from the {2}?", "Maze", ThreeColumns6Answers, TranslateFormatArgs = new[] { true, true },
            ExampleExtraFormatArguments = new[] { "column", "left", "row", "top" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(1, 6)]
        MazeStartingPosition,

        [SouvenirQuestion("What was the color of the starting face in {0}?", "Maze³", ThreeColumns6Answers, "Red", "Blue", "Yellow", "Green", "Magenta", "Orange", TranslateAnswers = true)]
        Maze3StartingFace,

        [SouvenirQuestion("What was the seed of the maze in {0}?", "Maze Identification", ThreeColumns6Answers, null, ExampleAnswers = new[] { "1234", "1111", "2222", "3333", "4444", "4321" })]
        [AnswerGenerator.Strings("4*1-4")]
        MazeIdentificationSeed,
        [SouvenirQuestion("What was the function of button {1} in {0}?", "Maze Identification", OneColumn4Answers, new[] { "Forwards", "Clockwise", "Backwards", "Counter-clockwise" }, ExampleAnswers = new[] { "forwards", "clockwise", "backwards", "counter-clockwise" },
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        MazeIdentificationNum,
        [SouvenirQuestion("Which button {1} in {0}?", "Maze Identification", TwoColumns4Answers, new[] { "1", "2", "3", "4" }, ExampleAnswers = new[] { "1", "2", "3", "4" },
            ExampleExtraFormatArguments = new[] { "moved you forwards", "turned you clockwise", "moved you backwards", "turned you counter-clockwise" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        MazeIdentificationFunc,

        [SouvenirQuestion("Which was the {1} value in {0}?", "Mazematics", ThreeColumns6Answers, null, ExampleAnswers = new[] { "30", "42", "51" },
            ExampleExtraFormatArguments = new[] { "initial", "goal" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        MazematicsValue,

        [SouvenirQuestion("What was the starting position on {0}?", "Maze Scrambler", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        MazeScramblerStart,
        [SouvenirQuestion("What was the goal on {0}?", "Maze Scrambler", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        MazeScramblerGoal,
        [SouvenirQuestion("Which of these positions was a maze marking on {0}?", "Maze Scrambler", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        MazeScramblerIndicators,

        [SouvenirQuestion("How many walls surrounded this cell in {0}?", "Mazeseeker", TwoColumns4Answers, "0", "1", "2", "3", UsesQuestionSprite = true)]
        MazeseekerCell,
        [SouvenirQuestion("Where was the start in {0}?", "Mazeseeker", ThreeColumns6Answers, null, Type = AnswerType.Grid)]
        [AnswerGenerator.Grid(6, 6)]
        MazeseekerStart,
        [SouvenirQuestion("Where was the goal in {0}?", "Mazeseeker", ThreeColumns6Answers, null, Type = AnswerType.Grid)]
        [AnswerGenerator.Grid(6, 6)]
        MazeseekerGoal,

        [SouvenirQuestion("Who was the master shown in {0}?", "Mega Man 2", TwoColumns4Answers, "Cold Man", "Magma Man", "Dust Man", "Sword Man", "Splash Woman", "Ice Man", "Quick Man", "Hard Man", "Pharaoh Man", "Charge Man", "Pirate Man", "Pump Man", "Galaxy Man", "Grenade Man", "Snake Man", "Burst Man", "Cut Man", "Air Man", "Magnet Man", "Toad Man", "Gyro Man", "Tomahawk Man", "Wood Man", "Strike Man", "Blade Man", "Aqua Man", "Shade Man", "Flash Man", "Flame Man", "Concrete Man", "Metal Man", "Needle Man", "Wave Man", "Knight Man", "Slash Man", "Shadow Man", "Sheep Man", "Ground Man", "Wind Man", "Fire Man", "Stone Man", "Tengu Man", "Bright Man", "Centaur Man", "Cloud Man", "Frost Man", "Dynamo Man", "Chill Man", "Turbo Man", "Napalm Man", "Jewel Man", "Drill Man", "Freeze Man", "Blizzard Man", "Gravity Man", "Junk Man", "Clown Man", "Hornet Man", "Skull Man", "Solar Man", "Commando Man", "Yamato Man", "Dive Man", "Search Man", "Gemini Man", "Bubble Man", "Guts Man", "Tornado Man", "Astro Man", "Plug Man", "Elec Man", "Crystal Man", "Nitro Man", "Burner Man", "Spark Man", "Spring Man", "Plant Man", "Star Man", "Ring Man", "Top Man", "Crash Man", "Bomb Man", "Heat Man", "Magic Man")]
        MegaMan2SelectedMaster,
        [SouvenirQuestion("Whose weapon was shown in {0}?", "Mega Man 2", TwoColumns4Answers, "Cold Man", "Magma Man", "Dust Man", "Sword Man", "Splash Woman", "Ice Man", "Quick Man", "Hard Man", "Pharaoh Man", "Charge Man", "Pirate Man", "Pump Man", "Galaxy Man", "Grenade Man", "Snake Man", "Burst Man", "Cut Man", "Air Man", "Magnet Man", "Toad Man", "Gyro Man", "Tomahawk Man", "Wood Man", "Strike Man", "Blade Man", "Aqua Man", "Shade Man", "Flash Man", "Flame Man", "Concrete Man", "Metal Man", "Needle Man", "Wave Man", "Knight Man", "Slash Man", "Shadow Man", "Sheep Man", "Ground Man", "Wind Man", "Fire Man", "Stone Man", "Tengu Man", "Bright Man", "Centaur Man", "Cloud Man", "Frost Man", "Dynamo Man", "Chill Man", "Turbo Man", "Napalm Man", "Jewel Man", "Drill Man", "Freeze Man", "Blizzard Man", "Gravity Man", "Junk Man", "Clown Man", "Hornet Man", "Skull Man", "Solar Man", "Commando Man", "Yamato Man", "Dive Man", "Search Man", "Gemini Man", "Bubble Man", "Guts Man", "Tornado Man", "Astro Man", "Plug Man", "Elec Man", "Crystal Man", "Nitro Man", "Burner Man", "Spark Man", "Spring Man", "Plant Man", "Star Man", "Ring Man", "Top Man", "Crash Man", "Bomb Man", "Heat Man", "Magic Man")]
        MegaMan2SelectedWeapon,

        [SouvenirQuestion("Which part was in slot #{1} at the start of {0}?", "Melody Sequencer", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 8)]
        MelodySequencerSlots,
        [SouvenirQuestion("Which slot contained part #{1} at the start of {0}?", "Melody Sequencer", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 8)]
        MelodySequencerParts,

        [SouvenirQuestion("What was the {1} correct symbol pressed in {0}?", "Memorable Buttons", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "J", "K", "L", "P", "Q",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.DynamicFont)]
        MemorableButtonsSymbols,

        [SouvenirQuestion("What was the displayed number in the {1} stage of {0}?", "Memory", TwoColumns4Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 4)]
        MemoryDisplay,
        [SouvenirQuestion("In what position was the button that you pressed in the {1} stage of {0}?", "Memory", TwoColumns4Answers, null, Type = AnswerType.Sprites,
            SpriteField = "MemorySprites", ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        MemoryPosition,
        [SouvenirQuestion("What was the label of the button that you pressed in the {1} stage of {0}?", "Memory", TwoColumns4Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 4)]
        MemoryLabel,

        [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", "Memory Wires", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 6)]
        MemoryWiresDisplayedDigits,
        [SouvenirQuestion("What was the colour of wire {1} in {0}?", "Memory Wires", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", "Black",
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4", "29", "30" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateAnswers = true)]
        MemoryWiresWireColours,

        [SouvenirQuestion("What was the extracted letter in {0}?", "Metamorse", ThreeColumns6Answers, null)]
        [AnswerGenerator.Strings("A-Z")]
        MetamorseExtractedLetter,

        [SouvenirQuestion("What was the final answer in {0}?", "Metapuzzle", TwoColumns4Answers, null,
            ExampleAnswers = new[] { "GIBBONS", "GIRAFFE", "MISUSED", "RUSHING", "DUSTMAN", "STATICS" })]
        MetapuzzleAnswer,

        [SouvenirQuestion("Which pin lit up {1} in {0}?", "Microcontroller", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 10)]
        MicrocontrollerPinOrder,

        [SouvenirQuestion("What was the color of the starting cell in {0}?", "Minesweeper", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", "black", TranslateAnswers = true)]
        MinesweeperStartingColor,

        [SouvenirQuestion("What was the second word written by the original ghost in {0}?", "Mirror", TwoColumns4Answers, null, ExampleAnswers = new[] { "ALPACA", "BUBBLE", "COWBOY", "DIESEL", "EULOGY", "FUSION", "GASKET", "HOODIE", "ICEBOX", "JOYPOP" })]
        MirrorWord,

        [SouvenirQuestion("Where was the SpongeBob Bar on {0}?", "Mister Softee", ThreeColumns6Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
        MisterSofteeSpongebobPosition,
        [SouvenirQuestion("Which treat was present on {0}?", "Mister Softee", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "MisterSofteeSprites")]
        MisterSofteeTreatsPresent,

        [SouvenirQuestion("What was the decrypted word of the {1} stage in {0}?", "Modern Cipher", ThreeColumns6Answers, "Absent", "Abstract", "Abysmal", "Accident", "Activate", "Adjacent", "Afraid", "Agenda", "Agony", "Alchemy", "Alcohol", "Alive", "Allergic", "Allergy", "Alpha", "Alphabet", "Already", "Amethyst", "Amnesty", "Amperage", "Ancient", "Animals", "Animate", "Anthrax", "Anxious", "Aquarium", "Aquarius", "Arcade", "Arrange", "Arrow", "Artefact", "Asterisk", "Atrophy", "Audio", "Author", "Avoid", "Awesome", "Balance", "Banana", "Bandit", "Bankrupt", "Basket", "Battle", "Bazaar", "Beard", "Beauty", "Beaver", "Becoming", "Beetle", "Beseech", "Between", "Bicycle", "Bigger", "Biggest", "Biology", "Birthday", "Bistro", "Bites", "Blight", "Blockade", "Blubber", "Bomb", "Bonobo", "Books", "Bottle", "Brazil", "Brief", "Broccoli", "Broken", "Brother", "Bubble", "Budget", "Bulkhead", "Bumper", "Bunny", "Button", "Bytes", "Cables", "Caliber", "Campaign", "Canada", "Canister", "Caption", "Caution", "Cavity", "Chalk", "Chamber", "Chamfer", "Champion", "Changes", "Chicken", "Children", "Chlorine", "Chord", "Chronic", "Church", "Cinnamon", "Civic", "Cleric", "Clock", "Cocoon", "Combat", "Combine", "Comedy", "Comics", "Comma", "Command", "Comment", "Compost", "Computer", "Condom", "Conflict", "Consider", "Contour", "Control", "Corrupt", "Costume", "Criminal", "Crunch", "Cryptic", "Cuboid", "Cypher", "Daddy", "Dancer", "Dancing", "Daughter", "Dead", "Decapod", "Decay", "Decoy", "Defeat", "Defuser", "Degree", "Delay", "Demigod", "Dentist", "Desert", "Design", "Desire", "Dessert", "Detail", "Develop", "Device", "Diamond", "Dictate", "Diffuse", "Dilemma", "Dingy", "Dinosaur", "Disease", "Disgust", "Document", "Doubled", "Doubt", "Downbeat", "Dragon", "Drawer", "Dream", "Drink", "Drunken", "Dungeon", "Dynasty", "Dyslexia", "Eclipse", "Eldritch", "Email", "Emulator", "Encrypt", "England", "Enlist", "Enough", "Ensure", "Equality", "Equation", "Eruption", "Eternity", "Euphoria", "Exact", "Exclaim", "Exhaust", "Expert", "Expertly", "Explain", "Explodes", "Fabric", "Factory", "Faded", "Faint", "Fair", "False", "Falter", "Famous", "Fantasy", "Farm", "Father", "Faucet", "Faulty", "Fearsome", "Feast", "February", "Feint", "Festival", "Fiction", "Fighter", "Figure", "Finish", "Fireman", "Firework", "First", "Fixture", "Flagrant", "Flagship", "Flamingo", "Flesh", "Flipper", "Fluorine", "Flush", "Foreign", "Forensic", "Fractal", "Fragrant", "France", "Frantic", "Freak", "Friction", "Friday", "Friendly", "Frighten", "Furor", "Fused", "Garage", "Genes", "Genetic", "Genius", "Gentle", "Glacier", "Glitch", "Goat", "Golden", "Granular", "Graphics", "Graphite", "Grateful", "Gridlock", "Ground", "Guitar", "Gumption", "Halogen", "Harmony", "Hawk", "Headache", "Heard", "Hedgehog", "Heinous", "Herd", "Heretic", "Hexagon", "Hiccup", "Highway", "Holiday", "Home", "Homesick", "Honest", "Horror", "Horse", "House", "Huge", "Humanity", "Hungry", "Hydrogen", "Hysteria", "Imagine", "Industry", "Infamous", "Inside", "Integral", "Interest", "Ironclad", "Issue", "Italic", "Italy", "Itch", "Jaundice", "Jeans", "Jeopardy", "Joyful", "Joystick", "Juice", "Juncture", "Jungle", "Junkyard", "Justice", "Keep", "Keyboard", "Kilobyte", "Kilogram", "Kingdom", "Kitchen", "Kitten", "Knife", "Krypton", "Ladylike", "Language", "Large", "Laughter", "Launch", "Leaders", "Learn", "Leave", "Leopard", "Level", "Liberal", "Liberty", "Lifeboat", "Ligament", "Light", "Liquid", "Listen", "Little", "Lobster", "Logical", "Love", "Lucky", "Lulled", "Lunatic", "Lurks", "Machine", "Madam", "Magnetic", "Manager", "Manual", "Marina", "Marine", "Martian", "Master", "Matrix", "Measure", "Meaty", "Meddle", "Medical", "Mental", "Menu", "Meow", "Merchant", "Message", "Messes", "Metal", "Method", "Mettle", "Militant", "Minim", "Minimum", "Miracle", "Mirror", "Misjudge", "Misplace", "Misses", "Mistake", "Mixture", "Mnemonic", "Mobile", "Modern", "Modest", "Module", "Moist", "Money", "Morning", "Most", "Mother", "Movies", "Multiple", "Munch", "Musical", "Mustache", "Mystery", "Mystic", "Mystique", "Mythic", "Narcotic", "Nasty", "Nature", "Navigate", "Network", "Neutral", "Nobelium", "Nobody", "Noise", "Notice", "Noun", "Nuclear", "Numeral", "Nutrient", "Nymph", "Obelisk", "Obstacle", "Obvious", "Octopus", "Offset", "Omega", "Opaque", "Opinion", "Orange", "Organic", "Ouch", "Outbreak", "Outdo", "Overcast", "Overlaps", "Package", "Padlock", "Pancake", "Panda", "Panic", "Paper", "Papers", "Parent", "Park", "Particle", "Passive", "Patented", "Pathetic", "Patient", "Peace", "Peasant", "Penalty", "Pencil", "Penguin", "Perfect", "Person", "Persuade", "Perusing", "Phone", "Physical", "Piano", "Picture", "Piglet", "Pilfer", "Pillage", "Pinch", "Pirate", "Pitcher", "Pizza", "Plane", "Planet", "Platonic", "Player", "Please", "Plucky", "Plunder", "Plurals", "Pocket", "Police", "Portrait", "Potato", "Potently", "Pounce", "Poverty", "Practice", "Predict", "Prefect", "Premium", "Present", "Prince", "Printer", "Prison", "Profit", "Promise", "Prophet", "Protein", "Province", "Psalm", "Psychic", "Puddle", "Punchbag", "Pungent", "Punish", "Purchase", "Quagmire", "Qualify", "Quantify", "Quantize", "Quarter", "Querying", "Queue", "Quiche", "Quick", "Rabbit", "Racoon", "Radar", "Radical", "Rainbow", "Random", "Rattle", "Ravenous", "Reason", "Rebuke", "Refine", "Regular", "Reindeer", "Request", "Resort", "Respect", "Retire", "Revolt", "Reward", "Rhapsody", "Rhenium", "Rhodium", "Rhomboid", "Rhyme", "Rhythm", "Ridicule", "Roadwork", "Roar", "Roast", "Room", "Rooster", "Roster", "Rotor", "Rotunda", "Royal", "Ruler", "Rural", "Sailor", "Sainted", "Sales", "Sally", "Satisfy", "Saunter", "Scale", "Scandal", "Schedule", "School", "Science", "Scratch", "Screen", "Sensible", "Separate", "Serious", "Several", "Shampoo", "Shares", "Shelter", "Shift", "Ship", "Shirt", "Shiver", "Shorten", "Showcase", "Shuffle", "Silent", "Similar", "Sister", "Sixth", "Sixty", "Skater", "Skyward", "Slander", "Slayer", "Sleek", "Slipper", "Smart", "Smeared", "Soccer", "Society", "Source", "Spain", "Spare", "Spark", "Spatula", "Speaker", "Special", "Spectate", "Spectrum", "Spicy", "Spinach", "Spiral", "Splendid", "Splinter", "Sprayed", "Spread", "Spring", "Squadron", "Squander", "Squash", "Squib", "Squid", "Squish", "Stake", "Stalking", "Steak", "Steam", "Sticker", "Stinky", "Stocking", "Stone", "Store", "Stormy", "Strange", "Strike", "Stutter", "Subway", "Suffer", "Supreme", "Surf", "Surplus", "Survey", "Switch", "Symbol", "System", "Systemic", "Table", "Tadpole", "Talking", "Tangle", "Tank", "Tapeworm", "Target", "Tarot", "Teach", "Teamwork", "Terminal", "Terminus", "Terror", "Testify", "Their", "There", "Thick", "Thief", "Think", "Throat", "Through", "Thunder", "Thyme", "Ticket", "Time", "Toaster", "Tomato", "Tone", "Torque", "Tortoise", "Touchy", "Toupe", "Tower", "Transfix", "Transit", "Trash", "Trauma", "Treason", "Treasure", "Trick", "Tripod", "Trouble", "Truck", "Trumpet", "Turtle", "Twinkle", "Ugly", "Ultra", "Umbrella", "Underway", "Unique", "Unknown", "Unsteady", "Untoward", "Unwashed", "Upgrade", "Urban", "Used", "Useless", "Utopia", "Vacuum", "Vampire", "Vanish", "Vanquish", "Various", "Vast", "Velocity", "Vendor", "Verb", "Verbatim", "Verdict", "Vexation", "Vicious", "Victim", "Victory", "Video", "View", "Viking", "Village", "Violent", "Violin", "Virulent", "Visceral", "Vision", "Volatile", "Voltage", "Vortex", "Vulgar", "Warden", "Warlock", "Warning", "Wealth", "Weapon", "Wedding", "Weight", "Whack", "Wharf", "What", "When", "Whisk", "Whistle", "Wicked", "Window", "Winter", "Witness", "Wizard", "Wrench", "Wretch", "Wrinkle", "Writer", "Xanthous", "Yacht", "Yarn", "Yawn", "Yeah", "Yearlong", "Yearn", "Yeoman", "Yodel", "Yoga", "Yonder", "Youngest", "Yourself", "Zealot", "Zebra", "Zenith", "Zither", "Zodiac", "Zombie",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ModernCipherWord,

        [SouvenirQuestion("Which module did the sound played by the {1} button belong to in {0}?", "Module Listening", OneColumn4Answers, ExampleAnswers = new[] { "Zoni", "Lucky Dice", "Qwirkle", "Battleship" }, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue", "yellow" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ModuleListeningSounds,

        [SouvenirQuestion("Which of the following was the starting icon for {0}?", "Module Maze", ThreeColumns6Answers, null, Type = AnswerType.Sprites)]
        ModuleMazeStartingIcon,

        [SouvenirQuestion("What was the {1} module shown in {0}?", "Module Movements", TwoColumns4Answers, "3D Tunnels", "Alchemy", "Braille", "Button Sequence", "Chord Qualities", "Crackbox", "Functions", "Hunting", "Kudosudoku", "Logic Gates", "Morse-A-Maze", "Pattern Cube", "Planets", "Quintuples", "Schlag den Bomb", "Shapes and Bombs", "Simon Samples", "Simon States", "Symbol Cycle", "Turtle Robot", "Wavetapping", "The Wire", "Yahtzee",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ModuleMovementsDisplay,

        [SouvenirQuestion("Which creature was displayed in {0}?", "Monsplode, Fight!", TwoColumns4Answers, "Caadarim", "Buhar", "Melbor", "Lanaluff", "Bob", "Mountoise", "Aluga", "Nibs", "Zapra", "Zenlad", "Vellarim", "Ukkens", "Lugirit", "Flaurim", "Myrchat", "Clondar", "Gloorim", "Docsplode", "Magmy", "Pouse", "Asteran", "Violan", "Percy", "Cutie Pie")]
        MonsplodeFightCreature,
        [SouvenirQuestion("Which one of these moves {1} selectable in {0}?", "Monsplode, Fight!", TwoColumns4Answers, "Tic", "Tac", "Toe", "Hollow Gaze", "Splash", "Heavy Rain", "Fountain", "Candle", "Torchlight", "Flame Spear", "Tangle", "Grass Blade", "Ivy Spikes", "Spectre", "Boo", "Battery Power", "Zap", "Double Zap", "Shock", "High Voltage", "Dark Portal", "Last Word", "Void", "Boom", "Fiery Soul", "Stretch", "Shrink", "Appearify", "Sendify", "Freak Out", "Glyph", "Bug Spray", "Bedrock", "Earthquake", "Cave In", "Toxic Waste", "Venom Fang", "Countdown", "Finale", "Sidestep",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        MonsplodeFightMove,

        [SouvenirQuestion("What was the {1} before the last action in {0}?", "Monsplode Trading Cards", TwoColumns4Answers, "Aluga", "Asteran", "Bob", "Buhar", "Caadarim", "Clondar", "Cutie Pie", "Docsplode", "Flaurim", "Gloorim", "Lanaluff", "Lugirit", "Magmy", "Melbor", "Mountoise", "Myrchat", "Nibs", "Percy", "Pouse", "Ukkens", "Vellarim", "Violan", "Zapra", "Zenlad", "Aluga, The Fighter", "Bob, The Ancestor", "Buhar, The Protector", "Melbor, The Web Bug",
            ExampleExtraFormatArguments = new[] { "first card in your hand", "second card in your hand", "third card in your hand", "card on offer" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        MonsplodeTradingCardsCards,
        [SouvenirQuestion("What was the print version of the {1} before the last action in {0}?", "Monsplode Trading Cards", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first card in your hand", "second card in your hand", "third card in your hand", "card on offer" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Strings("A-J", "1-9")]
        MonsplodeTradingCardsPrintVersions,

        [SouvenirQuestion("What was the {1} set in clockwise order in {0}?", "Moon", TwoColumns4Answers, "south", "south-west", "west", "north-west", "north", "north-east", "east", "south-east", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { "first initially lit", "second initially lit", "third initially lit", "fourth initially lit", "first initially unlit", "second initially unlit", "third initially unlit", "fourth initially unlit" },
            ExampleExtraFormatArgumentGroupSize = 1, AddThe = true, TranslateFormatArgs = new[] { true })]
        MoonLitUnlit,

        [SouvenirQuestion("What was the flashing word in {0}?", "More Code", TwoColumns4Answers, "Allocate", "Bulwarks", "Compiler", "Disposal", "Encipher", "Formulae", "Gauntlet", "Hunkered", "Illusory", "Jousting", "Kinetics", "Linkwork", "Monolith", "Nanobots", "Octangle", "Postsync", "Quartics", "Revolved", "Stanzaic", "Tomahawk", "Ultrahot", "Vendetta", "Wafflers", "Yokozuna", "Zugzwang", "Allotype", "Bulkhead", "Computer", "Dispatch", "Encrypts", "Fortunes", "Gateways", "Huntress", "Illusion", "Junction", "Kilobyte", "Linkages", "Monogram", "Nanogram", "Octuples", "Positron", "Quintics", "Revealed", "Stoccata", "Tomogram", "Ultrared", "Venomous", "Weakened", "Xenolith", "Yeasayer", "Zymogram",
            ExampleAnswers = new[] { "Allocate", "Bulwarks", "Compiler", "Disposal", "Encipher", "Formulae", "Gauntlet", "Hunkered", "Illusory", "Jousting", "Kinetics", "Linkwork" })]
        MoreCodeWord,

        [SouvenirQuestion("What was the starting location in {0}?", "Morse-A-Maze", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-F", "1-6")]
        MorseAMazeStartingCoordinate,
        [SouvenirQuestion("What was the ending location in {0}?", "Morse-A-Maze", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-F", "1-6")]
        MorseAMazeEndingCoordinate,
        [SouvenirQuestion("What was the word shown as Morse code in {0}?", "Morse-A-Maze", ThreeColumns6Answers, null,
            ExampleAnswers = new[] { "couch", "strobe", "smoke", "assay", "monkey", "glass", "starts", "strode", "office", "essays", "couple", "bosses" })]
        MorseAMazeMorseCodeWord,

        [SouvenirQuestion("What was the character flashed by the {1} button in {0}?", "Morse Buttons", ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-Z0-9")]
        MorseButtonsButtonLabel,
        [SouvenirQuestion("What was the color flashed by the {1} button in {0}?", "Morse Buttons", ThreeColumns6Answers, "red", "blue", "green", "yellow", "orange", "purple", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        MorseButtonsButtonColor,

        [SouvenirQuestion("What was the {1} received letter in {0}?", "Morsematics", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings('A', 'Z')]
        MorsematicsReceivedLetters,

        [SouvenirQuestion("What were the LEDs in the {1} row in {0} (1\u00a0=\u00a0on, 0\u00a0=\u00a0off)?", "Morse War", ThreeColumns6Answers, "1100", "1010", "1001", "0110", "0101", "0011", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "bottom", "middle", "top" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MorseWarLeds,
        [SouvenirQuestion("What code was transmitted in {0}?", "Morse War", ThreeColumns6Answers, "ABR", "RBS", "SVR", "ZUX", "ZAQ", "MOI", "OPA", "VZQ", "XRP", "OLL", "AIR", "RHG", "MJN", "VTT", "XZS", "SUN")]
        MorseWarCode,

        [SouvenirQuestion("What color was the torus in {0}?", "Mouse in the Maze", TwoColumns4Answers, "white", "green", "blue", "yellow", TranslateAnswers = true)]
        MouseInTheMazeTorus,
        [SouvenirQuestion("Which color sphere was the goal in {0}?", "Mouse in the Maze", TwoColumns4Answers, "white", "green", "blue", "yellow", TranslateAnswers = true)]
        MouseInTheMazeSphere,

        [SouvenirQuestion("What was the {1} obtained digit in {0}?", "M-Seq", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 9)]
        MSeqObtained,
        [SouvenirQuestion("What was the final number from the iteration process in {0}?", "M-Seq", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(25, 225)]
        MSeqSubmitted,

        [SouvenirQuestion("What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?", "Multicolored Switches", TwoColumns4Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white", TranslateAnswers = true, TranslateFormatArgs = new[] { false, true, true },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, "top", "lit", QandA.Ordinal, "bottom", "lit", QandA.Ordinal, "top", "unlit", QandA.Ordinal, "bottom", "unlit" }, ExampleExtraFormatArgumentGroupSize = 3)]
        MulticoloredSwitchesLedColor,

        [SouvenirQuestion("Where was the body found in {0}?", "Murder", TwoColumns4Answers, "Dining Room", "Study", "Kitchen", "Lounge", "Billiard Room", "Conservatory", "Ballroom", "Hall", "Library")]
        MurderBodyFound,
        [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", TwoColumns4Answers, "Miss Scarlett", "Professor Plum", "Mrs Peacock", "Reverend Green", "Colonel Mustard", "Mrs White",
            ExampleExtraFormatArguments = new[] { "a suspect, but not the murderer,", "not a suspect" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        MurderSuspect,
        [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", TwoColumns4Answers, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner",
            ExampleExtraFormatArguments = new[] { "a potential weapon, but not the murder weapon,", "not a potential weapon" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        MurderWeapon,

        [SouvenirQuestion("Which module was the first requested to be solved by {0}?", "Mystery Module", OneColumn4Answers, null, ExampleAnswers = new[] { "Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox", "Rock-Paper-Scissors-L.-Sp." })]
        MysteryModuleFirstKey,
        [SouvenirQuestion("Which module was hidden by {0}?", "Mystery Module", OneColumn4Answers, null, ExampleAnswers = new[] { "Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox" })]
        MysteryModuleHiddenModule,

        [SouvenirQuestion("Where was the skull in {0}?", "Mystic Square", TwoColumns4Answers, "top left", "top middle", "top right", "middle left", "center", "middle right", "bottom left", "bottom middle", "bottom right", TranslateAnswers = true)]
        MysticSquareSkull,

        [SouvenirQuestion("What was the label of the correct button in {0}?", "N&Ms", ThreeColumns6Answers)]
        [AnswerGenerator.Strings(5, 'M', 'N')]
        NandMsAnswer,

        [SouvenirQuestion("What was the {1} index in {0}?", "Name Codes", TwoColumns4Answers, "2", "3", "4", "5", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "left", "right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NameCodesIndices,

        [SouvenirQuestion("What was the color of the maze in {0}?", "Navigation Determination", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", TranslateAnswers = true)]
        NavigationDeterminationColor,
        [SouvenirQuestion("What was the label of the maze in {0}?", "Navigation Determination", TwoColumns4Answers, "A", "B", "C", "D")]
        NavigationDeterminationLabel,

        [SouvenirQuestion("What was the initial middle digit in {0}?", "Navinums", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 9)]
        NavinumsMiddleDigit,
        [SouvenirQuestion("What was the {1} directional button pressed in {0}?", "Navinums", TwoColumns4Answers, "up", "left", "right", "down", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NavinumsDirectionalButtons,

        [SouvenirQuestion("Which Greek letter appeared on {0} (case-sensitive)?", "Navy Button", ThreeColumns6Answers, "Α", "Β", "Γ", "Δ", "Ε", "Ζ", "Η", "Θ", "Ι", "Κ", "Λ", "Μ", "Ν", "Ξ", "Ο", "Π", "Ρ", "Σ", "Τ", "Υ", "Φ", "Χ", "Ψ", "Ω", "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω", AddThe = true)]
        NavyButtonGreekLetters,
        [SouvenirQuestion("What was the {1} of the given in {0} (0-indexed)?", "Navy Button", ThreeColumns6Answers, "0", "1", "2", "3", AddThe = true,
            ExampleExtraFormatArguments = new[] { "column", "row", "value" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        NavyButtonGiven,

        [SouvenirQuestion("What was the chapter number of the {1} page in {0}?", "Necronomicon", ThreeColumns6Answers, null, ExampleAnswers = new[] { "1", "24", "36" }, AddThe = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NecronomiconChapters,

        [SouvenirQuestion("In base 10, what was the value submitted in {0}?", "Negativity", ThreeColumns6Answers, ExampleAnswers = new[] { "0", "9990", "-9990", "-1234", "5678", "-90" })]
        NegativitySubmittedValue,
        [SouvenirQuestion("Excluding 0s, what was the submitted balanced ternary in {0}?", "Negativity", TwoColumns4Answers, ExampleAnswers = new[] { "+-", "-++", "++-+-", "++++-", "-----", "+-----++++" })]
        NegativitySubmittedTernary,

        [SouvenirQuestion("What was the acid’s color in {0}?", "Neutralization", TwoColumns4Answers, "Yellow", "Green", "Red", "Blue", TranslateAnswers = true)]
        NeutralizationColor,
        [SouvenirQuestion("What was the acid’s volume in {0}?", "Neutralization", TwoColumns4Answers, "5", "10", "15", "20")]
        NeutralizationVolume,

        [SouvenirQuestion("Which button flashed in the {1} stage in {0}?", "❖", TwoColumns4Answers, IsEntireQuestionSprite = true, Type = AnswerType.Sprites, SpriteField = "NonverbalSimonSprites", ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NonverbalSimonFlashes,

        [SouvenirQuestion("What was the position of the square you initially pressed in {0}?", "Not Colored Squares", ThreeColumns6Answers, Type = AnswerType.Grid)]
        [AnswerGenerator.Grid(4, 4)]
        NotColoredSquaresInitialPosition,

        [SouvenirQuestion("What was the encrypted word in {0}?", "Not Colored Switches", ThreeColumns6Answers, null, ExampleAnswers = new[] { "Adjust", "Anchor", "Bowtie", "Button", "Cipher", "Corner" })]
        NotColoredSwitchesWord,

        [SouvenirQuestion("What symbol flashed on the {1} button in {0}?", "Not Connection Check", ThreeColumns6Answers, "+", "-", ".", ":", "/", "_", "=", ",",
            ExampleExtraFormatArguments = new[] { "top left", "top right", "bottom left", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotConnectionCheckFlashes,
        [SouvenirQuestion("What was the value of the {1} button in {0}?", "Not Connection Check", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "top left", "top right", "bottom left", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotConnectionCheckValues,

        [SouvenirQuestion("Which coordinate was part of the square in {0}?", "Not Coordinates", OneColumn4Answers, null, ExampleAnswers = new[] { "[4,7]", "C4", "<0, 2>", "3, 1", "(6,2)", "B-1", "“1, 0”", "4/3", "[12]", "#23", "四十七" })]
        NotCoordinatesSquareCoords,

        [SouvenirQuestion("What color flashed {1} in the final sequence in {0}?", "Not Keypad", ThreeColumns6Answers, "red", "orange", "yellow", "green", "cyan", "blue", "purple", "magenta", "pink", "brown", "grey", "white", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotKeypadColor,
        [SouvenirQuestion("Which symbol was on the button that flashed {1} in the final sequence in {0}?", "Not Keypad", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteField = "KeypadSprites",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotKeypadSymbol,

        [SouvenirQuestion("What was the starting distance in {0}?", "Not Maze", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 9)]
        NotMazeStartingDistance,

        [SouvenirQuestion("What was the {1} correct word you submitted in {0}?", "Not Morse Code", ThreeColumns6Answers, ExampleAnswers = new[] { "shelf", "pounds", "sister", "beef", "yeast", "drive" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotMorseCodeWord,

        [SouvenirQuestion("What was the transmitted word on {0}?", "Not Morsematics", ThreeColumns6Answers, null, ExampleAnswers = new[] { "ABORT", "AFTER", "AGONY", "ALIGN", "AMONG", "AMBER", "ANGST", "AZURE", "BAKER", "BAYOU", "BEACH", "BLACK", "BOGUS", "BOXES", "BRASH", "BUDGE", "CABLE", "CAULK", "CHIEF", "CLOVE", "CODEX", "CRAZE", "CRISP", "CRUEL" })]
        NotMorsematicsWord,

        [SouvenirQuestion("What room was {1} in initially on {0}?", "Not Murder", TwoColumns4Answers, "Ballroom", "Billiard Room", "Conservatory", "Dining Room", "Hall", "Kitchen", "Library", "Lounge", "Study",
            ExampleExtraFormatArguments = new[] { "Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White", }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotMurderRoom,
        [SouvenirQuestion("What weapon did {1} possess initially on {0}?", "Not Murder", TwoColumns4Answers, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner",
            ExampleExtraFormatArguments = new[] { "Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White", }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotMurderWeapon,

        [SouvenirQuestion("Which of these numbers {1} at the {2} stage of {0}?", "Not Number Pad", TwoColumns4Answers, TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "flashed", QandA.Ordinal, "did not flash", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(0, 9)]
        NotNumberPadFlashes,

        [SouvenirQuestion("What was the position of the {1} flashing peg on {0}?", "Not Perspective Pegs", ThreeColumns6Answers, ExampleAnswers = new[] { "top", "top-right", "bottom-right", "bottom-left", "top-left" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotPerspectivePegsPosition,
        [SouvenirQuestion("From what perspective did the {1} peg flash on {0}?", "Not Perspective Pegs", ThreeColumns6Answers, ExampleAnswers = new[] { "top", "top-right", "bottom-right", "bottom-left", "top-left" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotPerspectivePegsPerspective,
        [SouvenirQuestion("What was the color of the {1} flashing peg on {0}?", "Not Perspective Pegs", ThreeColumns6Answers, ExampleAnswers = new[] { "blue", "green", "purple", "red", "yellow" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotPerspectivePegsColor,

        [SouvenirQuestion("What was the first displayed symbol on {0}?", "Not Piano Keys", TwoColumns4Answers, "b", "n", "#", "", Type = AnswerType.PianoKeysFont)]
        NotPianoKeysFirstSymbol, //There are only 4 possibilities for the first symbol.
        [SouvenirQuestion("What was the second displayed symbol on {0}?", "Not Piano Keys", ThreeColumns6Answers, "c", "C", "^", "v", ">", "", "%", "\"", "*", Type = AnswerType.PianoKeysFont)]
        NotPianoKeysSecondSymbol, //There are only 9 possibilities for the 2nd and 3rd symbols
        [SouvenirQuestion("What was the third displayed symbol on {0}?", "Not Piano Keys", ThreeColumns6Answers, "U", "T", "m", "w", "", "B", "x", "", "", Type = AnswerType.PianoKeysFont)]
        NotPianoKeysThirdSymbol,

        [SouvenirQuestion("Which maze was used in {0}?", "Not Simaze", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", TranslateAnswers = true)]
        NotSimazeMaze,
        [SouvenirQuestion("What was the starting position in {0}?", "Not Simaze", TwoColumns4Answers, "(red, red)", "(red, orange)", "(red, yellow)", "(red, green)", "(red, blue)", "(red, purple)", "(orange, red)", "(orange, orange)", "(orange, yellow)", "(orange, green)", "(orange, blue)", "(orange, purple)", "(yellow, red)", "(yellow, orange)", "(yellow, yellow)", "(yellow, green)", "(yellow, blue)", "(yellow, purple)", "(green, red)", "(green, orange)", "(green, yellow)", "(green, green)", "(green, blue)", "(green, purple)", "(blue, red)", "(blue, orange)", "(blue, yellow)", "(blue, green)", "(blue, blue)", "(blue, purple)", "(purple, red)", "(purple, orange)", "(purple, yellow)", "(purple, green)", "(purple, blue)", "(purple, purple)", TranslateAnswers = true)]
        NotSimazeStart,
        [SouvenirQuestion("What was the goal position in {0}?", "Not Simaze", TwoColumns4Answers, "(red, red)", "(red, orange)", "(red, yellow)", "(red, green)", "(red, blue)", "(red, purple)", "(orange, red)", "(orange, orange)", "(orange, yellow)", "(orange, green)", "(orange, blue)", "(orange, purple)", "(yellow, red)", "(yellow, orange)", "(yellow, yellow)", "(yellow, green)", "(yellow, blue)", "(yellow, purple)", "(green, red)", "(green, orange)", "(green, yellow)", "(green, green)", "(green, blue)", "(green, purple)", "(blue, red)", "(blue, orange)", "(blue, yellow)", "(blue, green)", "(blue, blue)", "(blue, purple)", "(purple, red)", "(purple, orange)", "(purple, yellow)", "(purple, green)", "(purple, blue)", "(purple, purple)", TranslateAnswers = true)]
        NotSimazeGoal,

        [SouvenirQuestion("Which letter was pressed in the first stage of {0}?", "Not Text Field", TwoColumns4Answers)]
        [AnswerGenerator.Strings('A', 'F')]
        NotTextFieldInitialPresses,
        [SouvenirQuestion("Which letter appeared 9 times at the start of {0}?", "Not Text Field", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F")]
        [AnswerGenerator.Strings('A', 'F')]
        NotTextFieldBackgroundLetter,

        [SouvenirQuestion("What word flashed on {0}?", "Not The Bulb", OneColumn4Answers, ExampleAnswers = new[] { "Amplitude", "Boulevard", "Chemistry", "Duplicate", "Eightfold", "Filaments", "Goldsmith", "Harlequin", "Injectors", "Juxtapose", "Kilohertz", "Labyrinth", "Moustache", "Neighbour", "Obscurity", "Penumbral", "Quicksand", "Rhapsodic", "Squawking", "Triglyphs", "Universal", "Vexations", "Whizbangs", "Xenoglyph", "Yardstick", "Zigamorph" })]
        NotTheBulbWord,
        [SouvenirQuestion("What color was the bulb on {0}?", "Not The Bulb", ThreeColumns6Answers, ExampleAnswers = new[] { "Red", "Green", "Blue", "Yellow", "Purple", "White" })]
        NotTheBulbColor,
        [SouvenirQuestion("What was the material of the screw cap on {0}?", "Not The Bulb", ThreeColumns6Answers, ExampleAnswers = new[] { "Copper", "Silver", "Gold", "Plastic", "Carbon Fibre", "Ceramic" })]
        NotTheBulbScrewCap,

        [SouvenirQuestion("What colors did the light glow in {0}?", "Not the Button", ThreeColumns6Answers, "white", "red", "yellow", "green", "blue", "white/red", "white/yellow", "white/green", "white/blue", "red/yellow", "red/green", "red/blue", "yellow/green", "yellow/blue", "green/blue", TranslateAnswers = true)]
        NotTheButtonLightColor,

        [SouvenirQuestion("What was the initial position in {0}?", "Not the Screw", ThreeColumns6Answers, null, Type = AnswerType.Grid)]
        [AnswerGenerator.Grid(6, 4)]
        NotTheScrewInitialPosition,

        [SouvenirQuestion("In which position was the button you pressed in the {1} stage on {0}?", "Not Who’s on First", TwoColumns4Answers, "top left", "top right", "middle left", "middle right", "bottom left", "bottom right", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstPressedPosition,
        [SouvenirQuestion("What was the label on the button you pressed in the {1} stage on {0}?", "Not Who’s on First", ThreeColumns6Answers, "BLANK", "DONE", "FIRST", "HOLD", "LEFT", "LIKE", "MIDDLE", "NEXT", "NO", "NOTHING", "OKAY", "PRESS", "READY", "RIGHT", "SURE", "U", "UH HUH", "UH UH", "UHHH", "UR", "WAIT", "WHAT", "WHAT?", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstPressedLabel,
        [SouvenirQuestion("In which position was the reference button in the {1} stage on {0}?", "Not Who’s on First", TwoColumns4Answers, "top left", "top right", "middle left", "middle right", "bottom left", "bottom right", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstReferencePosition,
        [SouvenirQuestion("What was the label on the reference button in the {1} stage on {0}?", "Not Who’s on First", ThreeColumns6Answers, "BLANK", "DONE", "FIRST", "HOLD", "LEFT", "LIKE", "MIDDLE", "NEXT", "NO", "NOTHING", "OKAY", "PRESS", "READY", "RIGHT", "SURE", "U", "UH HUH", "UH UH", "UHHH", "UR", "WAIT", "WHAT", "WHAT?", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstReferenceLabel,
        [SouvenirQuestion("What was the calculated number in the second stage on {0}?", "Not Who’s on First", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 60)]
        NotWhosOnFirstSum,

        [SouvenirQuestion("Which of these consonants was missing in {0}?", "Not Word Search", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z")]
        NotWordSearchMissing,
        [SouvenirQuestion("What was the first correctly pressed letter in {0}?", "Not Word Search", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z")]
        NotWordSearchFirstPress,

        [SouvenirQuestion("Which sector value {1} present on {0}?", "Not X01", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotX01SectorValues,

        [SouvenirQuestion("What table were we in in {0} (numbered 1–8 in reading order in the manual)?", "Not X-Ray", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8")]
        NotXRayTable,
        [SouvenirQuestion("What direction was button {1} in {0}?", "Not X-Ray", ThreeColumns6Answers, "Up", "Right", "Down", "Left", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotXRayDirections,
        [SouvenirQuestion("Which button went {1} in {0}?", "Not X-Ray", ThreeColumns6Answers, "1", "2", "3", "4", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "up", "right", "down", "left" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotXRayButtons,
        [SouvenirQuestion("What was the scanner color in {0}?", "Not X-Ray", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", TranslateAnswers = true)]
        NotXRayScannerColor,

        [SouvenirQuestion("Which number was correctly pressed on {0}?", "Numbered Buttons", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 100)]
        NumberedButtonsButtons,

        [SouvenirQuestion("What two-digit number was given in {0}?", "Numbers", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 99, "00")]
        NumbersTwoDigit,

        [SouvenirQuestion("What was the color of the number on {0}?", "Numpath", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", TranslateAnswers = true)]
        NumpathColor,
        [SouvenirQuestion("What was the number displayed on {0}?", "Numpath", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(1, 9)]
        NumpathDigit,

        [SouvenirQuestion("Which of these was a contestant on {0}?", "Object Shows", TwoColumns4Answers, ExampleAnswers = new[] { "Battleship", "Big Circle", "Jack O’ Lantern", "Lego", "Moon", "Radio", "Combination Lock", "Cookie Jar", "Fidget Spinner" })]
        ObjectShowsContestants,

        [SouvenirQuestion("What was the starting sphere in {0}?", "The Octadecayotton", OneColumn4Answers, ExampleAnswers = new[] { "--+", "-+-+-++-+", "-++-+--+-", "+++-+-++-", "--++-++-+-++" })]
        OctadecayottonSphere,
        [SouvenirQuestion("What was one of the subrotations in the {1} rotation in {0}?", "The Octadecayotton", OneColumn4Answers, ExampleAnswers = new[] { "-X", "+Y-Z", "+U+V+W", "-R+S+T-O", "+P-Q-X+Y-Z" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        OctadecayottonRotations,

        [SouvenirQuestion("What was the button you pressed in the {1} stage of {0}?", "Odd One Out", TwoColumns4Answers, "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        OddOneOutButton,

        [SouvenirQuestion("What was the {1} of the numbers shown in {0}?", "Old AI", TwoColumns4Answers, "1", "2", "3", "4", "5",
            ExampleExtraFormatArguments = new[] { "group", "sub-group" }, ExampleExtraFormatArgumentGroupSize = 1)]
        OldAIGroup,

        [SouvenirQuestion("What was the initial color of the status light in {0}?", "Old Fogey", ThreeColumns6Answers, "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", "White")]
        OldFogeyStartingColor,

        [SouvenirQuestion("Which Egyptian hieroglyph was in the {1} in {0}?", "Only Connect", TwoColumns4Answers, "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        OnlyConnectHieroglyphs,

        [SouvenirQuestion("What was the {1} arrow on the display of the {2} stage of {0}?", "Orange Arrows", TwoColumns4Answers, "Up", "Right", "Down", "Left", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrangeArrowsSequences,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Orange Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "FORMAL", "FREEZE", "GLANCE", "JACKED", "JAMMED", "JAMMER", "NECTAR", "NEEDED", "QUEENS", "UTOPIA" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        OrangeCipherScreen,

        [SouvenirQuestion("What color was the {2} key in the {1} stage of {0}?", "Ordered Keys", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrderedKeysColors,
        [SouvenirQuestion("What was the label on the {2} key in the {1} stage of {0}?", "Ordered Keys", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrderedKeysLabels,
        [SouvenirQuestion("What color was the label of the {2} key in the {1} stage of {0}?", "Ordered Keys", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrderedKeysLabelColors,

        [SouvenirQuestion("What was the order ID in the {1} order of {0}?", "Order Picking", ThreeColumns6Answers, null, ExampleAnswers = new[] { "3141", "7946", "6905", "6408", "5030", "2803", "6918", "6642", "4645", "4356", "2868", "1887" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1000, 9999)]
        OrderPickingOrder,
        [SouvenirQuestion("What was the product ID in the {1} order of {0}?", "Order Picking", ThreeColumns6Answers, null, ExampleAnswers = new[] { "3141", "7946", "6905", "6408", "5030", "2803", "6918", "6642", "4645", "4356", "2868", "1887" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1000, 9999)]
        OrderPickingProduct,
        [SouvenirQuestion("What was the pallet in the {1} order of {0}?", "Order Picking", ThreeColumns6Answers, "CHEP", "SIPPL", "SLPR", "EWHITE", "ECHEP", "ESIPPL", "ESLPR",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        OrderPickingPallet,

        [SouvenirQuestion("What was the observer’s initial position in {0}?", "Orientation Cube", TwoColumns4Answers, "front", "left", "back", "right", TranslateAnswers = true)]
        OrientationCubeInitialObserverPosition,

        [SouvenirQuestion("What was the observer’s initial position in {0}?", "Orientation Hypercube", TwoColumns4Answers, "front", "left", "back", "right")]
        OrientationHypercubeInitialObserverPosition,
        [SouvenirQuestion("What was the initial colour of the {1} face in {0}?", "Orientation Hypercube", ThreeColumns6Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white",
             ExampleExtraFormatArguments = new[] { "right", "left", "top", "bottom", "back", "front", "zag", "zig" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        OrientationHypercubeInitialFaceColour,

        [SouvenirQuestion("What was {1}’s {2} digit from the right in {0}?", "Palindromes", ThreeColumns6Answers, TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "X", QandA.Ordinal, "Y", QandA.Ordinal, "Z", QandA.Ordinal, "the screen", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(0, 9)]
        PalindromesNumbers,

        [SouvenirQuestion("What was shown on the display on {0}?", "Parity", ThreeColumns6Answers, null, ExampleAnswers = new[] { "A1", "B2", "C3", "D4", "E5", "F6" })]
        ParityDisplay,

        [SouvenirQuestion("What was the LED color in the {1} stage of {0}?", "Partial Derivatives", ThreeColumns6Answers, "blue", "green", "orange", "purple", "red", "yellow", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PartialDerivativesLedColors,
        [SouvenirQuestion("What was the {1} term in {0}?", "Partial Derivatives", TwoColumns4Answers,
            ExampleAnswers = new[] { "−5x⁴z³", "8x⁴z⁴", "4xy³z²", "−3x⁴z", "3x⁵y⁵z³" }, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PartialDerivativesTerms,

        [SouvenirQuestion("What was the passport expiration year of the {1} inspected passenger in {0}?", "Passport Control", ThreeColumns6Answers, ExampleAnswers = new[] { "1931", "1956", "1977", "1980", "1991", "2000", "2004", "2019", "2047" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PassportControlPassenger,

        [SouvenirQuestion("What was the starting value when you solved {0}?", "Password Destroyer", TwoColumns4Answers)]
        [AnswerGenerator.Integers(1000000, 9999999)]
        PasswordDestroyerStartingValue,
        [SouvenirQuestion("What was the increase factor when you solved {0}?", "Password Destroyer", TwoColumns4Answers)]
        [AnswerGenerator.Integers(-1000000, 1000000)]
        PasswordDestroyerIncreaseFactor,
        [SouvenirQuestion("What was the TFA₁ value when you solved {0}?", "Password Destroyer", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 999)]
        PasswordDestroyerTF1,
        [SouvenirQuestion("What was the TFA₂ value when you solved {0}?", "Password Destroyer", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        PasswordDestroyerTF2,
        [SouvenirQuestion("What was the 2FAST™ value when you solved {0}?", "Password Destroyer", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(100100, 999999)]
        PasswordDestroyerTwoFactorV2,
        [SouvenirQuestion("What was the percentage of solved modules used in the final calculation when you solved {0}?", "Password Destroyer", ThreeColumns6Answers, "1%", "2%", "3%", "4%", "5%", "6%", "7%", "8%", "9%", "10%", "11%", "12%", "13%", "14%", "15%", "16%", "17%", "18%", "19%", "20%", "21%", "22%", "23%", "24%", "25%", "26%", "27%", "28%", "29%", "30%", "31%", "32%", "33%", "34%", "35%", "36%", "37%", "38%", "39%", "40%", "41%", "42%", "43%", "44%", "45%", "46%", "47%", "48%", "49%", "50%", "51%", "52%", "53%", "54%", "55%", "56%", "57%", "58%", "59%", "60%", "61%", "62%", "63%", "64%", "65%", "66%", "67%", "68%", "69%", "70%", "71%", "72%", "73%", "74%", "75%", "76%", "77%", "78%", "79%", "80%", "81%", "82%", "83%", "84%", "85%", "86%", "87%", "88%", "89%", "90%", "91%", "92%", "93%", "94%", "95%", "96%", "97%", "98%", "99%")]
        PasswordDestroyerSolvePercentage,

        [SouvenirQuestion("Which symbol was highlighted in {0}?", "Pattern Cube", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "PatternCubeSprites")]
        PatternCubeHighlightedSymbol,

        [SouvenirQuestion("What word was on the display in the {1} stage of {0}?", "Periodic Words", OneColumn4Answers, ExampleAnswers = new[] { "ATTACKERS", "BUY", "SUPERPOSITION", "WHO" }, ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PeriodicWordsDisplayedWords,

        [SouvenirQuestion("What was the {1} color in the initial sequence in {0}?", "Perspective Pegs", ThreeColumns6Answers, "red", "yellow", "green", "blue", "purple", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PerspectivePegsColorSequence,

        [SouvenirQuestion("What was the offset in {0}?", "Phosphorescence", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 419)]
        PhosphorescenceOffset,
        [SouvenirQuestion("What was the {1} button press in {0}?", "Phosphorescence", ThreeColumns6Answers, new[] { "Azure", "Blue", "Crimson", "Diamond", "Emerald", "Fuchsia", "Green", "Ice", "Jade", "Kiwi", "Lime", "Magenta", "Navy", "Orange", "Purple", "Quartz", "Red", "Salmon", "Tan", "Ube", "Vibe", "White", "Xotic", "Yellow", "Zen" }, TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PhosphorescenceButtonPresses,

        [SouvenirQuestion("What was the code in {0}?", "Pictionary", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("0-579", "0-68", "0-7", "0-68")]
        PictionaryCode,

        [SouvenirQuestion("What was the {1} digit of the displayed number in {0}?", "Pie", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        PieDigits,

        [SouvenirQuestion("What number was not displayed in {0}?", "Pie Flash", TwoColumns4Answers, ExampleAnswers = new[] { "31415", "62643", "28410", "93105" })]
        PieFlashDigits,

        [SouvenirQuestion("What was the {1} in {0}?", "Pigpen Cycle", TwoColumns4Answers, "Advanced", "Addition", "Allotype", "Allotted", "Binaries", "Billions", "Bullhorn", "Bulwarks", "Ciphered", "Circuits", "Commando", "Compiler", "Decrypts", "Division", "Dispatch", "Discrete", "Encipher", "Entrance", "Equators", "Equalise", "Finished", "Findings", "Formulae", "Fortunes", "Gauntlet", "Gambling", "Gatepost", "Gateways", "Hazarded", "Haziness", "Huntress", "Hungrier", "Indicate", "Indigoes", "Illusory", "Illuding", "Jigsawed", "Jimmying", "Junkyard", "Juncture", "Kilowatt", "Kinetics", "Knocking", "Knowable", "Limiting", "Linearly", "Linkwork", "Lingered", "Monogram", "Monotone", "Multiton", "Mulcting", "Nanogram", "Nanotube", "Numerous", "Numerate", "Octangle", "Octuples", "Obstruct", "Obstacle", "Progress", "Projects", "Postsync", "Positron", "Quadrant", "Quadrics", "Quirkish", "Quitters", "Reversed", "Revolved", "Rotators", "Relative", "Starting", "Standard", "Stockade", "Stoccata", "Triggers", "Triangle", "Tomogram", "Tomahawk", "Underrun", "Underlie", "Ulterior", "Ultrahot", "Vicinity", "Viceless", "Volition", "Voluming", "Wingding", "Winnable", "Whatness", "Whatsits", "Yellowed", "Yeasayer", "Yokozuna", "Yourself", "Zippered", "Zigzaggy", "Zymology", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        PigpenCycleWord,

        [SouvenirQuestion("What was the {1} word in {0}?", "Pink Button", TwoColumns4Answers, "BLK", "RED", "GRN", "YLW", "BLU", "MGT", "CYN", "WHT",
            AddThe = true, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PinkButtonWords,
        [SouvenirQuestion("What was the {1} color in {0}?", "Pink Button", TwoColumns4Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white", TranslateAnswers = true,
            AddThe = true, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PinkButtonColors,

        [SouvenirQuestion("What was the keyword in {0}?", "Pixel Cipher", ThreeColumns6Answers, "HEART", "HAPPY", "HOUSE", "ARROW", "ARMOR", "ACORN", "CROSS", "CHORD", "CLOCK", "DONUT", "DELTA", "DUCKY", "EQUAL", "EMOJI", "EDGES", "LIBRA", "LUCKY", "LUNAR", "MEDAL", "MOVIE", "MUSIC", "PANDA", "PEARL", "PIANO", "PIXEL")]
        PixelCipherKeyword,

        [SouvenirQuestion("What was the first half of the first phrase in {0}?", "Placeholder Talk", TwoColumns4Answers, null, ExampleAnswers = new[] { "", "IS IN THE", "IS THE", "IS IN UH", "IS", "IS AT", "IS INN", "IS THE IN", "IN IS", "IS IN.", "IS IN", "THE", "FIRST-", "IN", "UH IS IN", "AT", "LAST-", "UH", "KEYBORD", "A" })]
        PlaceholderTalkFirstPhrase,
        [SouvenirQuestion("What was the last half of the first phrase in {0}?", "Placeholder Talk", TwoColumns4Answers, null, ExampleAnswers = new[] { "", "FIRST POS.", "SECOND POS.", "THIRD POS.", "FOURTH POS.", "FIFTH POS.", "MILLIONTH POS.", "BILLIONTH POS.", "LAST POS.", "AN ANSWER" })]
        PlaceholderTalkOrdinal,
        [SouvenirQuestion("What was the second phrase’s calculated value in {0}?", "Placeholder Talk", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 8)]
        PlaceholderTalkSecondPhrase,

        [SouvenirQuestion("What was the character listed on the information display in {0}?", "Placement Roulette", TwoColumns4Answers, "Baby Mario", "Baby Luigi", "Baby Peach", "Baby Daisy", "Toad", "Toadette", "Koopa Troopa", "Dry Bones", "Mario", "Luigi", "Peach", "Daisy", "Yoshi", "Birdo", "Diddy Kong", "Bowser Jr.", "Mii", "Wario", "Waluigi", "Donkey Kong", "Bowser", "King Boo", "Rosalina", "Funky Kong", "Dry Bowser")]
        PlacementRouletteChar,
        [SouvenirQuestion("What was the drift type listed on the information display in {0}?", "Placement Roulette", TwoColumns4Answers, "Manual", "Automatic", "Inside", "Outside", "Smart Steering", "Self Steering")]
        PlacementRouletteDrift,
        [SouvenirQuestion("What was the track listed on the information display in {0}?", "Placement Roulette", OneColumn4Answers, "Luigi Circuit", "Moo Moo Meadows", "Mushroom Gorge", "Toad's Factory", "Mario Circuit", "Coconut Mall", "DK Snowboard Cross", "Wario's Gold Mine", "Daisy Circuit", "Koopa Cape", "Maple Treeway", "Grumble Volcano", "Dry Dry Ruins", "Moonview Highway", "Bowser's Castle", "Rainbow Road", "GCN Peach Beach", "DS Yoshi Falls", "SNES Ghost Valley 2", "N64 Mario Raceway", "N64 Sherbet Land", "GBA Shy Guy Beach", "DS Delfino Square", "GCN Waluigi Stadium", "DS Desert Hills", "GBA Bowser Castle 3", "N64 DK's Jungle Parkway", "GCN Mario Circuit", "SNES Mario Circuit 3", "DS Peach Gardens", "GCN DK Mountain", "N64 Bowser's Castle")]
        PlacementRouletteTrack,
        [SouvenirQuestion("What was the track type of the track listed on the information display in {0}?", "Placement Roulette", ThreeColumns6Answers, "Nitro", "Retro", "Original", "Remake", "Nintendo", "Custom")]
        PlacementRouletteTrackType,
        [SouvenirQuestion("What was the vehicle listed on the information display in {0}?", "Placement Roulette", OneColumn4Answers, "Standard Kart S", "Baby Booster", "Concerto", "Cheep Charger", "Rally Romper", "Blue Falcon", "Standard Bike S", "Bullet Bike", "Nanobike", "Quacker", "Magikruiser", "Bubble Bike", "Standard Kart M", "Nostalgia 1", "Wild Wing", "Turbo Blooper", "Royal Racer", "B Dasher Mk. 2", "Standard Bike M", "Mach Bike", "Bon Bon", "Rapide", "Nitrocycle", "Dolphin Dasher", "Standard Kart L", "Offroader", "Flame Flyer", "Piranha Prowler", "Jetsetter", "Honeycoupe", "Standard Bike L", "Bowser Bike", "Wario Bike", "Twinkle Star", "Torpedo", "Phantom")]
        PlacementRouletteVehicle,
        [SouvenirQuestion("What was the vehicle type of the vehicle listed on the information display in {0}?", "Placement Roulette", OneColumn4Answers, "Lightweight", "Mediumweight", "Heavyweight", "Featherweight")]
        PlacementRouletteVehicleType,

        [SouvenirQuestion("What was the planet shown in {0}?", "Planets", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "PlanetsSprites")]
        PlanetsPlanet,
        [SouvenirQuestion("What was the color of the {1} strip (from the top) in {0}?", "Planets", ThreeColumns6Answers, "Aqua", "Blue", "Green", "Lime", "Orange", "Red", "Yellow", "White", "Off", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PlanetsStrips,

        [SouvenirQuestion("What was the {1} in {0}?", "Playfair Cycle", TwoColumns4Answers, "Advanced", "Advocate", "Allotype", "Allotted", "Binaries", "Binomial", "Bullhorn", "Bulwarks", "Circular", "Circuits", "Commando", "Compiler", "Decrypts", "Decimals", "Dispatch", "Discrete", "Encipher", "Encoding", "Equators", "Equalise", "Finished", "Finnicky", "Formulae", "Fortunes", "Gauntlet", "Gauchest", "Gatepost", "Gateways", "Hotlinks", "Hotheads", "Huntress", "Hungrier", "Indicate", "Indirect", "Illusory", "Illuding", "Jigsawed", "Jiggling", "Junkyard", "Juncture", "Kilowatt", "Kilobyte", "Knocking", "Knowable", "Limiting", "Limerick", "Linkwork", "Lingered", "Monogram", "Monolith", "Multiton", "Mulcting", "Nanogram", "Nanobots", "Numerous", "Numerate", "Octangle", "Octonary", "Obstruct", "Obstacle", "Progress", "Programs", "Postsync", "Positron", "Quotient", "Quotable", "Quirkish", "Quitters", "Reversed", "Revealed", "Rotators", "Relative", "Stanzaic", "Standard", "Stockade", "Stoccata", "Triggers", "Trickier", "Tomogram", "Tomahawk", "Underrun", "Undoings", "Ulterior", "Ultrahot", "Vicinity", "Vicenary", "Volition", "Voluming", "Wingding", "Wingspan", "Whatness", "Whatsits", "Yearlong", "Yeasayer", "Yokozuna", "Yourself", "Ziggurat", "Zigzaggy", "Zymology", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        PlayfairCycleWord,

        [SouvenirQuestion("What was the {1} correct answer you pressed in {0}?", "Poetry", TwoColumns4Answers, "clarity", "flow", "fatigue", "hollow", "energy", "sunshine", "ocean", "reflection", "identity", "black", "crowd", "heart", "weather", "words", "past", "solitary", "relax", "dance", "weightless", "morality", "gaze", "failure", "bunny", "lovely", "romance", "future", "focus", "search", "cookies", "compassion", "creation", "patience",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PoetryAnswers,

        [SouvenirQuestion("What was the starting position in {0}?", "Polyhedral Maze", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 61)]
        PolyhedralMazeStartPosition,

        [SouvenirQuestion("What was the number shown in {0}?", "Prime Encryption", ThreeColumns6Answers, ExampleAnswers = new[] { "1147", "1271", "1333", "1457", "1643", "1829" })]
        PrimeEncryptionDisplayedValue,

        [SouvenirQuestion("What was the missing frequency in the {1} wire in {0}?", "Probing", TwoColumns4Answers, "10Hz", "22Hz", "50Hz", "60Hz", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red-white", "yellow-black", "green", "gray", "yellow-red", "red-blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ProbingFrequencies,

        [SouvenirQuestion("What was the initial seed in {0}?", "Procedural Maze", TwoColumns4Answers)]
        [AnswerGenerator.Strings("6*0-1")]
        ProceduralMazeInitialSeed,

        [SouvenirQuestion("What was the displayed number in {0}?", "...?", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 99, "00")]
        PunctuationMarksDisplayedNumber,

        [SouvenirQuestion("What was the target word on {0}?", "Purple Arrows", ThreeColumns6Answers, null, ExampleAnswers = new[] { "Thesis", "Immune", "Agency", "Height", "Active", "Bother", "Viable" })]
        PurpleArrowsFinish,

        [SouvenirQuestion("What was the {1} number in the cyclic sequence on {0}?", "Purple Button", ThreeColumns6Answers, null, AddThe = true, ExampleAnswers = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PurpleButtonNumbers,

        [SouvenirQuestion("What was the {1} puzzle number in {0}?", "Puzzle Identification", ThreeColumns6Answers, null, ExampleAnswers = new[] { "001", "002", "003", "004", "005", "006" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 170, 1, "000")]
        PuzzleIdentificationNum,
        [SouvenirQuestion("What game was the {1} puzzle in {0} from?", "Puzzle Identification", OneColumn4Answers, null, ExampleAnswers = new[] { "Professor Layton and the Curious Village", "Professor Layton and Pandora's Box", "Professor Layton and the Lost Future", "Professor Layton and the Spectre's Call" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PuzzleIdentificationGame,
        [SouvenirQuestion("What was the {1} puzzle in {0}?", "Puzzle Identification", OneColumn4Answers, null, ExampleAnswers = new[] { "Where's the Village?", "Dr Schrader's Map", "A Party Crasher", "A Secret Message" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        PuzzleIdentificationName,

        [SouvenirQuestion("What was the {1} sequence’s answer in {0}?", "Quaver", OneColumn4Answers, ExampleAnswers = new[] { "4", "10", "87", "320", "3, 3, 2, 3", "87, 85, 82, 84" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        QuaverArrows,

        [SouvenirQuestion("Which of these symbols was part of the flashing sequence in {0}?", "Question Mark", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "QuestionMarkSprites")]
        QuestionMarkFlashedSymbols,

        [SouvenirQuestion("What was the {1} color in the primary sequence in {0}?", "Quick Arithmetic", ThreeColumns6Answers, "red", "blue", "green", "yellow", "white", "black", "orange", "pink", "purple", "cyan", "brown",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        QuickArithmeticColors,
        [SouvenirQuestion("What was the {1} digit in the {2} sequence in {0}?", "Quick Arithmetic", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, "primary", QandA.Ordinal, "secondary" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(0, 9)]
        QuickArithmeticPrimSecDigits,

        [SouvenirQuestion("What was the {1} digit in the {2} slot in {0}?", "Quintuples", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(0, 9)]
        QuintuplesNumbers,
        [SouvenirQuestion("What color was the {1} digit in the {2} slot in {0}?", "Quintuples", TwoColumns4Answers, "red", "blue", "orange", "green", "pink", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        QuintuplesColors,
        [SouvenirQuestion("How many numbers were {1} in {0}?", "Quintuples", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "blue", "orange", "green", "pink" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 25)]
        QuintuplesColorCounts,

        [SouvenirQuestion("What was the number initially on the display in {0}?", "Quiz Buzz", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(6, 74)]
        QuizBuzzStartingNumber,

        [SouvenirQuestion("What tile did you place {1} in {0}?", "Qwirkle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "QwirkleSprites",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        QwirkleTilesPlaced,

        [SouvenirQuestion("How many jewels were in the starting common pool in {0}?", "Raiding Temples", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 10)]
        RaidingTemplesStartingCommonPool,

        [SouvenirQuestion("What was the {1} car in {0}?", "Railway Cargo Loading", TwoColumns4Answers, Type = AnswerType.Sprites,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        RailwayCargoLoadingCars,
        [SouvenirQuestion("Which freight table rule {1} in {0}?", "Railway Cargo Loading", OneColumn4Answers, "Over 150 lumber/75 logs", "Over 100 sheet metal", "Over 250 crude oil", "Over 400 mail", "Over 30 livestock", "Over 600 milk/water/resin", "Over 100 liquid fuel", "Over 700 industrial gas", "Over 150 food", "Over 100 coal", "Over 500 loose bulk (excl. coal)", "Over 7 large objects", "Over 5 automobiles", "Over 700 industrial gas",
            ExampleExtraFormatArguments = new[] { "was met", "wasn’t met" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        RailwayCargoLoadingFreightTableRules,

        [SouvenirQuestion("What was the displayed number in {0}?", "Rainbow Arrows", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 99)]
        RainbowArrowsNumber,

        [SouvenirQuestion("What was the color of the {1} LED in {0}?", "Recolored Switches", TwoColumns4Answers, "red", "green", "blue", "cyan", "orange", "purple", "white", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        RecoloredSwitchesLedColors,

        [SouvenirQuestion("Which of these words appeared, but was not the password, in {0}?", "Recursive Password", ThreeColumns6Answers, ExampleAnswers = new[] { "Abyss", "Ingot", "Nonce", "Whelk", "Obeys", "Lobed" })]
        RecursivePasswordNonPasswordWords,
        [SouvenirQuestion("What was the password in {0}?", "Recursive Password", ThreeColumns6Answers, ExampleAnswers = new[] { "Abyss", "Ingot", "Nonce", "Whelk", "Obeys", "Lobed" })]
        RecursivePasswordPassword,

        [SouvenirQuestion("What was the starting number in {0}?", "Red Arrows", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        RedArrowsStartNumber,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Red Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "EATING", "GOBLET", "INCOME", "INSIDE", "MARKED", "POWDER", "STRING", "WIZARD", "WOBBLE", "YELLOW" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        RedCipherScreen,

        [SouvenirQuestion("What was the first color flashed by {0}?", "Red Herring", TwoColumns4Answers, "Green", "Blue", "Purple", "Orange")]
        RedHerringFirstFlash,

        [SouvenirQuestion("Which condition was the solving condition in {0}?", "Reformed Role Reversal", ThreeColumns6Answers, "second", "third", "4th", "5th", "6th", "7th", "8th", TranslateAnswers = true)]
        ReformedRoleReversalCondition,
        [SouvenirQuestion("What color was the {1} wire in {0}?", "Reformed Role Reversal", ThreeColumns6Answers, "Navy", "Lapis", "Blue", "Sky", "Teal", "Plum", "Violet", "Purple", "Magenta", "Lavender", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ReformedRoleReversalWire,

        [SouvenirQuestion("What was the displayed digit that corresponded to the solution phrase in {0}?", "Regular Crazy Talk", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        RegularCrazyTalkDigit,
        [SouvenirQuestion("What was the embellishment of the solution phrase in {0}?", "Regular Crazy Talk", OneColumn4Answers, "[PHRASE]", "It says: [PHRASE]", "Quote: [PHRASE] End quote", "“[PHRASE]”", "It says: “[PHRASE]”", "“It says: [PHRASE]”")]
        RegularCrazyTalkModifier,

        [SouvenirQuestion("Which one of these houses was on offer, but not chosen by Bob in {0}?", "Retirement", TwoColumns4Answers, null, ExampleAnswers = new[] { "Hotham Place", "Homestead", "Riverwell", "Lodge Park" })]
        RetirementHouses,

        [SouvenirQuestion("What was the {1} character in the {2} message of {0}?", "Reverse Morse", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Strings("A-Z0-9")]
        ReverseMorseCharacters,

        [SouvenirQuestion("What character was used in the {1} round of {0}?", "Reverse Polish Notation", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-G0-9")]
        ReversePolishNotationCharacter,

        [SouvenirQuestion("What was the exit coordinate in {0}?", "RGB Maze", ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-H", "1-8")]
        RGBMazeExit,
        [SouvenirQuestion("Where was the {1} key in {0}?", "RGB Maze", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-H", "1-8")]
        RGBMazeKeys,
        [SouvenirQuestion("Which maze number was the {1} maze in {0}?", "RGB Maze", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        RGBMazeNumber,

        [SouvenirQuestion("What was the color of the {1} LED in {0}?", "RGB Sequences", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Cyan", "Yellow", "White",
        ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        RGBSequencesDisplay,

        [SouvenirQuestion("What was the color in {0}?", "Rhythms", TwoColumns4Answers, "Blue", "Red", "Green", "Yellow", TranslateAnswers = true)]
        RhythmsColor,

        [SouvenirQuestion("Where was the empty cell in {0}?", "Robo-Scanner", ThreeColumns6Answers, "A1", "A2", "A3", "A4", "A5", "B1", "B2", "B3", "B4", "B5", "C1", "C2", "C4", "C5", "D1", "D2", "D3", "D4", "D5", "E1", "E2", "E3", "E4", "E5")]
        RoboScannerEmptyCell,

        [SouvenirQuestion("What was the name of the robot in the {1} position of {0}?", "Robot Programming", TwoColumns4Answers, "R.O.B", "HAL", "R2D2", "Fender",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        RobotProgrammingName,

        [SouvenirQuestion("What four-digit number was given in {0}?", "Roger", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 9999, "0000")]
        RogerSeed,

        [SouvenirQuestion("What was the number to the correct condition in {0}?", "Role Reversal", ThreeColumns6Answers, "2", "3", "4", "5", "6", "7", "8")]
        RoleReversalNumber,
        [SouvenirQuestion("How many {1} wires were there in {0}?", "Role Reversal", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7",
            ExampleExtraFormatArguments = new[] { "warm-colored", "cold-colored", "primary-colored", "secondary-colored" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        RoleReversalWires,

        [SouvenirQuestion("What was the rule number in {0}?", "Rule", ThreeColumns6Answers, AddThe = true)]
        [AnswerGenerator.Integers(0, 15)]
        RuleNumber,

        [SouvenirQuestion("What was the {1} coordinate of the {2} vertex in {0}?", "Rule of Three", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "X", "red", "Y", "yellow", "Z", "blue" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { false, true })]
        [AnswerGenerator.Integers(-13, 13)]
        RuleOfThreeCoordinates,
        [SouvenirQuestion("What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?", "Rule of Three", TwoColumns4Answers, "-", "0", "+", TranslateFormatArgs = new[] { true, false, false },
            ExampleExtraFormatArguments = new[] { "red", "X", QandA.Ordinal, "yellow", "Y", QandA.Ordinal, "blue", "Z", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 3)]
        RuleOfThreeCycles,

        [SouvenirQuestion("What was the digit displayed on the {1} diamond in {0}?", "Safety Square", TwoColumns4Answers, ExampleExtraFormatArguments = new[] { "red", "yellow", "blue" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Integers(0, 4)]
        SafetySquareDigits,
        [SouvenirQuestion("What was the special rule displayed on the white diamond in {0}?", "Safety Square", OneColumn4Answers, "No special rule", "Reacts with water", "Simple asphyxiant", "Oxidizer")]
        SafetySquareSpecialRule,

        [SouvenirQuestion("Where was {1} in {0}?", "Samsung", ThreeColumns6Answers, "TL", "TM", "TR", "ML", "MM", "MR", "BL", "BM", "BR", AddThe = true,
            ExampleExtraFormatArguments = new[] { "Duolingo", "Google Maps", "Kindle", "Google Authenticator", "Photomath", "Spotify", "Google Arts & Culture", "Discord" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SamsungAppPositions,

        [SouvenirQuestion("Which tile was correctly submitted in the first stage of {0}?", "Scavenger Hunt", ThreeColumns6Answers, Type = AnswerType.Grid)]
        [AnswerGenerator.Grid(4, 4)]
        ScavengerHuntKeySquare,
        [SouvenirQuestion("Which of these tiles was {1} in the first stage of {0}?", "Scavenger Hunt", ThreeColumns6Answers, Type = AnswerType.Grid, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Grid(4, 4)]
        ScavengerHuntColoredTiles,

        [SouvenirQuestion("What was the contestant’s name in {0}?", "Schlag den Bomb", TwoColumns4Answers, "Ron", "Don", "Julia", "Cory", "Greg", "Paula", "Val", "Lisa", "Ozy", "Ozzy", "Elsa", "Cori", "Harry", "Gale", "Daniel", "Albert", "Spike", "Tommy", "Greta", "Tina", "Rob", "Edgar", "Julia", "Peter", "Millie", "Isolde", "Eris")]
        SchlagDenBombContestantName,
        [SouvenirQuestion("What was the contestant’s score in {0}?", "Schlag den Bomb", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 75)]
        SchlagDenBombContestantScore,
        [SouvenirQuestion("What was the bomb’s score in {0}?", "Schlag den Bomb", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 75)]
        SchlagDenBombBombScore,

        [SouvenirQuestion("What was the {1} encrypted word in {0}?", "Scramboozled Eggain", ThreeColumns6Answers, null,
            ExampleAnswers = new[] { "Basted", "Boiled", "Boxing", "Carton", "Dumpty", "French" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ScramboozledEggainWord,

        [SouvenirQuestion("What was the submitted data type of the variable in {0}?", "Scripting", TwoColumns4Answers, "int", "bool", "float", "char")]
        ScriptingVariableDataType,

        [SouvenirQuestion("What was the modified property of the first display in {0}?", "Scrutiny Squares", OneColumn4Answers, "Word", "Color around word", "Color of background", "Color of word")]
        ScrutinySquaresFirstDifference,

        [SouvenirQuestion("What were the first and second words in the {1} phrase in {0}?", "Sea Shells", TwoColumns4Answers, "she sells", "she shells", "sea shells", "sea sells",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells1,
        [SouvenirQuestion("What were the third and fourth words in the {1} phrase in {0}?", "Sea Shells", TwoColumns4Answers, "sea shells", "she shells", "sea sells", "she sells",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells2,
        [SouvenirQuestion("What was the end of the {1} phrase in {0}?", "Sea Shells", TwoColumns4Answers, "sea shore", "she sore", "she sure", "seesaw",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells3,

        [SouvenirQuestion("What was the {1} letter involved in the starting value in {0}?", "Semamorse", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "Morse", "semaphore" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        SemamorseLetters,
        [SouvenirQuestion("What was the color of the display involved in the starting value in {0}?", "Semamorse", TwoColumns4Answers, "red", "green", "cyan", "indigo", "pink", TranslateAnswers = true)]
        SemamorseColor,

        [SouvenirQuestion("What sequence was used in {0}?", "Sequencyclopedia", TwoColumns4Answers, null, ExampleAnswers = new[] { "A000001", "A069420", "A111111" }, AddThe = true)]
        [AnswerGenerator.Integers(0, 1000000, "'A'000000")]
        SequencyclopediaSequence,

        [SouvenirQuestion("What equation was shown in the {1} stage of {0}?", "S.E.T. Theory", OneColumn4Answers, null,
            ExampleAnswers = new[] { "(A ∩ B)", "(A ∪ B)", "(!B ∆ !A)", "(B ∩ !A)", "(!(C − B) ∪ !A)", "((B ∩ A) − C)", "(!(B ∪ A) ∆ (C ∩ !B))", "((A − !C) ∩ !(B ∪ !C))" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SetTheoryEquations,

        [SouvenirQuestion("What was the initial letter in {0}?", "Shapes And Bombs", ThreeColumns6Answers, "A", "B", "D", "E", "G", "I", "K", "L", "N", "O", "P", "S", "T", "X", "Y")]
        ShapesAndBombsInitialLetter,

        [SouvenirQuestion("What was the initial shape in {0}?", "Shape Shift", TwoColumns4Answers, Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings('A', 'P')]
        ShapeShiftInitialShape,

        [SouvenirQuestion("What color was the {1} marker in {0}?", "Shifted Maze", TwoColumns4Answers, "White", "Blue", "Yellow", "Magenta", "Green",
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        ShiftedMazeColors,

        [SouvenirQuestion("What was the seed in {0}?", "Shifting Maze", TwoColumns4Answers, null)]
        [AnswerGenerator.Strings(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/")]
        ShiftingMazeSeed,

        [SouvenirQuestion("What was the displayed piece in {0}?", "Shogi Identification", TwoColumns4Answers,
            "Go-Between", "Pawn", "Side Mover", "Vertical Mover", "Bishop", "Rook", "Dragon Horse", "Dragon King", "Lance", "Reverse Chariot", "Blind Tiger", "Ferocious Leopard", "Copper General", "Silver General", "Gold General", "Drunk Elephant", "Kirin", "Phoenix", "Queen", "Flying Stag", "Flying Ox", "Free Boar", "Whale", "White Horse", "King", "Prince", "Horned Falcon", "Soaring Eagle", "Lion")]
        ShogiIdentificationPiece,

        [SouvenirQuestion("What was the {1} slot in the {2} stage in {0}?", "Silly Slots", TwoColumns4Answers, "red bomb", "red cherry", "red coin", "red grape", "green bomb", "green cherry", "green coin", "green grape", "blue bomb", "blue cherry", "blue coin", "blue grape",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        SillySlots,

        [SouvenirQuestion("What was the deciphered word in {0}?", "Sign Language", TwoColumns4Answers,
            "PHALANX", "DIGITAL", "ACHIRAL", "DEAFENS", "LISTENS", "EXPLAIN", "SPEAKER", "TURTLES", "QUOTING", "MISTAKE", "REALIZE", "HELPERS", "HEARING", "STROKES", "OVERJOY", "ROYALTY", "EARDRUM", "COCHLEA", "AUDIBLE", "KABOOMS", "REFUGEE", "SWINGER", "BALANCE", "LIQUIDS", "VOYAGED")]
        SignLanguageWord,

        [SouvenirQuestion("What was the message type in {0}?", "Silo Authorization", TwoColumns4Answers, "Red-Alpha", "Yellow-Alpha", "Green-Alpha")]
        SiloAuthorizationMessageType,
        [SouvenirQuestion("What was the {1} part of the encrypted message in {0}?", "Silo Authorization", ThreeColumns6Answers, ExampleAnswers = new[] { "A1B2", "BC84", "QW47", "B420", "AFS2", "FUN9" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings(4, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")]
        SiloAuthorizationEncryptedMessage,
        [SouvenirQuestion("What was the received authentication code in {0}?", "Silo Authorization", ThreeColumns6Answers, ExampleAnswers = new[] { "1234", "5678", "1357", "2468", "0001", "9999" })]
        [AnswerGenerator.Integers(0, 9999, "0000")]
        SiloAuthorizationAuthCode,

        [SouvenirQuestion("What color was pressed {1} in the final sequence of {0}?", "Simon Said", TwoColumns4Answers, "Red", "Green", "Blue", "Yellow", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSaidPresses,

        [SouvenirQuestion("What were the call samples {1} of {0}?", "Simon Samples", ThreeColumns6Answers, "KKSS", "KKSH", "KSSH", "KHSS", "KHSH", "KHSO", "KHOH", "KOSH", "KOSO", "SKSK", "SHHS", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "played in the first stage", "added in the second stage", "added in the third stage" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSamplesSamples,

        [SouvenirQuestion("What color flashed {1} in the final sequence in {0}?", "Simon Says", TwoColumns4Answers, "red", "yellow", "green", "blue", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSaysFlash,

        [SouvenirQuestion("What color flashed {1} in {0}?", "Simon Scrambles", TwoColumns4Answers, "Red", "Green", "Blue", "Yellow", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonScramblesColors,

        [SouvenirQuestion("Which color flashed {1} in the final sequence in {0}?", "Simon Screams", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonScreamsFlashing,

        [SouvenirQuestion("In which stage(s) of {0} was “{1}” the applicable rule?", "Simon Screams", TwoColumns4Answers, "first", "second", "third", "first and second", "first and third", "second and third", "all of them",
            TranslateAnswers = true, TranslateFormatArgs = new[] { true }, ExampleExtraFormatArguments = new[] {
                "three adjacent colors flashing in clockwise order",
                "a color flashing, then an adjacent color, then the first again",
                "at most one color flashing out of red, yellow, and blue",
                "two colors opposite each other that didn’t flash",
                "two (but not three) adjacent colors flashing in clockwise order"
            }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonScreamsRule,

        [SouvenirQuestion("Which color flashed {1} in the {2} stage of {0}?", "Simon Selects", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Magenta", "Cyan", TranslateAnswers = true,
             ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonSelectsOrder,

        [SouvenirQuestion("What was the {1} received letter in {0}?", "Simon Sends", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings('A', 'Z')]
        SimonSendsReceivedLetters,

        [SouvenirQuestion("What was the shape submitted at the end of {0}?", "Simon Shapes", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "SimonShapesSprites")]
        SimonShapesSubmittedShape,

        [SouvenirQuestion("What was the {1} flash in the final sequence in {0}?", "Simon Simons", ThreeColumns6Answers, "TR", "TY", "TG", "TB", "LR", "LY", "LG", "LB", "RR", "RY", "RG", "RB", "BR", "BY", "BG", "BB",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSimonsFlashingColors,

        [SouvenirQuestion("Which key’s color flashed {1} in the {2} stage of {0}?", "Simon Sings", ThreeColumns6Answers, "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonSingsFlashing,

        [SouvenirQuestion("Which letter flashed on the {1} button in {0}?", "Simon Shouts", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "top", "left", "right", "bottom" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonShoutsFlashingLetter,

        [SouvenirQuestion("How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?", "Simon Shrieks", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonShrieksFlashingButton,

        [SouvenirQuestion("What shape was the {1} arrow in {0}?", "Simon Signals", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = nameof(SouvenirModule.SimonSignalsSprites),
            ExampleExtraFormatArguments = new[] { "red", "green", "blue", "gray" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        SimonSignalsColorToShape,
        [SouvenirQuestion("How many directions did the {1} arrow in {0} have?", "Simon Signals", TwoColumns4Answers, "3", "4", "5", "6",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue", "gray" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        SimonSignalsColorToRotations,
        [SouvenirQuestion("What color was the arrow with this shape in {0}?", "Simon Signals", TwoColumns4Answers, "red", "green", "blue", "gray", UsesQuestionSprite = true)]
        SimonSignalsShapeToColor,
        [SouvenirQuestion("How many directions did the arrow with this shape have in {0}?", "Simon Signals", TwoColumns4Answers, "3", "4", "5", "6", UsesQuestionSprite = true)]
        SimonSignalsShapeToRotations,
        [SouvenirQuestion("What color was the arrow with {1} possible directions in {0}?", "Simon Signals", TwoColumns4Answers, "red", "green", "blue", "gray", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { "3", "4", "5", "6" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSignalsRotationsToColor,
        [SouvenirQuestion("What shape was the arrow with {1} possible directions in {0}?", "Simon Signals", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteField = nameof(SouvenirModule.SimonSignalsSprites),
            ExampleExtraFormatArguments = new[] { "3", "4", "5", "6" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSignalsRotationsToShape,

        [SouvenirQuestion("What was the color of the {1} flash in {0}?", "Simon Smothers", ThreeColumns6Answers, "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, TranslateAnswers = true)]
        SimonSmothersColors,
        [SouvenirQuestion("What was the direction of the {1} flash in {0}?", "Simon Smothers", TwoColumns4Answers, "Up", "Down", "Left", "Right", ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, TranslateAnswers = true)]
        SimonSmothersDirections,

        [SouvenirQuestion("Which sample button sounded {1} in the final sequence in {0}?", "Simon Sounds", TwoColumns4Answers, "red", "blue", "yellow", "green", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSoundsFlashingColors,

        [SouvenirQuestion("Which bubble flashed first in {0}?", "Simon Speaks", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        SimonSpeaksPositions,
        [SouvenirQuestion("Which bubble flashed second in {0}?", "Simon Speaks", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "SimonSpeaksSprites")]
        SimonSpeaksShapes,
        [SouvenirQuestion("Which language was the bubble that flashed third in {0} in?", "Simon Speaks", TwoColumns4Answers, "English", "Danish", "Dutch", "Esperanto", "Finnish", "French", "German", "Hungarian", "Italian")]
        SimonSpeaksLanguages,
        [SouvenirQuestion("Which word was in the bubble that flashed fourth in {0}?", "Simon Speaks", ThreeColumns6Answers, "black", "sort", "zwart", "nigra", "musta", "noir", "schwarz", "fekete", "nero", "blue", "blå", "blauw", "blua", "sininen", "bleu", "blau", "kék", "blu", "green", "grøn", "groen", "verda", "vihreä", "vert", "grün", "zöld", "verde", "cyan", "turkis", "turkoois", "turkisa", "turkoosi", "turquoise", "türkis", "türkiz", "turchese", "red", "rød", "rood", "ruĝa", "punainen", "rouge", "rot", "piros", "rosso", "purple", "lilla", "purper", "purpura", "purppura", "pourpre", "lila", "bíbor", "porpora", "yellow", "gul", "geel", "flava", "keltainen", "jaune", "gelb", "sárga", "giallo", "white", "hvid", "wit", "blanka", "valkoinen", "blanc", "weiß", "fehér", "bianco", "gray", "grå", "grijs", "griza", "harmaa", "gris", "grau", "szürke", "grigio")]
        SimonSpeaksWords,
        [SouvenirQuestion("What color was the bubble that flashed fifth in {0}?", "Simon Speaks", ThreeColumns6Answers, "black", "blue", "green", "cyan", "red", "purple", "yellow", "white", "gray", TranslateAnswers = true)]
        SimonSpeaksColors,

        [SouvenirQuestion("Which color flashed {1} in sequence in {0}?", "Simon’s Star", ThreeColumns6Answers, "red", "yellow", "green", "blue", "purple", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonsStarColors,

        [SouvenirQuestion("Which color flashed in the {1} stage of {0}?", "Simon Stacks", TwoColumns4Answers, "Red", "Green", "Blue", "Yellow", ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonStacksColors,

        [SouvenirQuestion("Which color flashed {1} in the {2} stage in {0}?", "Simon Stages", ThreeColumns6Answers, "red", "blue", "yellow", "orange", "magenta", "green", "pink", "lime", "cyan", "white", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStagesFlashes,
        [SouvenirQuestion("What color was the indicator in the {1} stage in {0}?", "Simon Stages", ThreeColumns6Answers, "red", "blue", "yellow", "orange", "magenta", "green", "pink", "lime", "cyan", "white", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonStagesIndicator,

        [SouvenirQuestion("Which {1} in the {2} stage in {0}?", "Simon States", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", "Red, Yellow", "Red, Green", "Red, Blue", "Yellow, Green", "Yellow, Blue", "Green, Blue", "all 4", "none", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "color(s) flashed", QandA.Ordinal, "color(s) didn’t flash", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStatesDisplay,

        [SouvenirQuestion("Which color flashed {1} in the output sequence in {0}?", "Simon Stops", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Violet", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonStopsColors,

        [SouvenirQuestion("Which color {1} {2} in the final sequence of {0}?", "Simon Stores", TwoColumns4Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "flashed", QandA.Ordinal, "was among the colors flashed", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStoresColors,

        [SouvenirQuestion("What color was the button at this position in {0}?", "Simon Subdivides", TwoColumns4Answers, new[] { "Blue", "Green", "Red", "Violet" },
            UsesQuestionSprite = true)]
        SimonSubdividesButton,

        [SouvenirQuestion("What was the {1} topic in {0}?", "Simon Supports", TwoColumns4Answers, "Boss", "Cruel", "Faulty", "Lookalike", "Puzzle", "Simon", "Time-Based", "Translated",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSupportsTopics,

        [SouvenirQuestion("What color flashed {1} on the {2} Simon in {0}?", "Simultaneous Simons", TwoColumns4Answers, "Blue", "Yellow", "Red", "Green",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimultaneousSimonsFlash,

        [SouvenirQuestion("What were the original numbers in {0}?", "Skewed Slots", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 999, "000")]
        SkewedSlotsOriginalNumbers,

        [SouvenirQuestion("Which race was selectable, but not the solution, in {0}?", "Skyrim", TwoColumns4Answers, "Nord", "Khajiit", "Breton", "Argonian", "Dunmer", "Altmer", "Redguard", "Orc", "Imperial")]
        SkyrimRace,
        [SouvenirQuestion("Which weapon was selectable, but not the solution, in {0}?", "Skyrim", TwoColumns4Answers, "Axe of Whiterun", "Dawnbreaker", "Windshear", "Blade of Woe", "Firiniel’s End", "Bow of Hunt", "Volendrung", "Chillrend", "Mace of Molag Bal")]
        SkyrimWeapon,
        [SouvenirQuestion("Which enemy was selectable, but not the solution, in {0}?", "Skyrim", TwoColumns4Answers, "Alduin", "Blood Dragon", "Cave Bear", "Dragon Priest", "Draugr", "Draugr Overlord", "Frost Troll", "Frostbite Spider", "Mudcrab")]
        SkyrimEnemy,
        [SouvenirQuestion("Which city was selectable, but not the solution, in {0}?", "Skyrim", TwoColumns4Answers, "Dawnstar", "Ivarstead", "Markarth", "Riverwood", "Rorikstead", "Solitude", "Whiterun", "Windhelm", "Winterhold")]
        SkyrimCity,
        [SouvenirQuestion("Which dragon shout was selectable, but not the solution, in {0}?", "Skyrim", TwoColumns4Answers, "Disarm", "Dismay", "Dragonrend", "Fire Breath", "Ice Form", "Kyne’s Peace", "Slow Time", "Unrelenting Force", "Whirlwind Sprint")]
        SkyrimDragonShout,

        [SouvenirQuestion("What was the last triplet of letters in {0}?", "Slow Math", ThreeColumns6Answers, null, ExampleAnswers = new[] { "ABC", "DEG", "KNP", "STX", "ZAB", "CDE", "GKN", "PST", "XZA", "BCD" })]
        [AnswerGenerator.Strings(3, "ABCDEGKNPSTXZ")]
        SlowMathLastLetters,

        [SouvenirQuestion("How much did the sequence shift by in {0}?", "Small Circle", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8")]
        SmallCircleShift,
        [SouvenirQuestion("Which wedge made the different noise in the beginning of {0}?", "Small Circle", TwoColumns4Answers, "red", "orange", "yellow", "green", "blue", "magenta", "white", "black")]
        SmallCircleWedge,
        [SouvenirQuestion("Which color was {1} in the solution to {0}?", "Small Circle", TwoColumns4Answers, "red", "orange", "yellow", "green", "blue", "magenta", "white", "black",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SmallCircleSolution,

        [SouvenirQuestion("How many red balls were there at the start of {0}?", "Snooker", TwoColumns4Answers, "8", "9", "10", "11")]
        SnookerReds,

        [SouvenirQuestion("Which snowflake was on the {1} button of {0}?", "Snowflakes", ThreeColumns6Answers, Type = AnswerType.SnowflakesFont, FontSize = 400, CharacterSize = 0.2f, ExampleExtraFormatArguments = new[] { "top", "right", "bottom", "left" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Strings("A-Za-z")]
        SnowflakesDisplayedSnowflakes,

        [SouvenirQuestion("Which sound was played but not featured in the chosen zone in {0}?", "Sonic & Knuckles", OneColumn4Answers, "Invincibility Theme", "Jump", "Lightning Shield", "Blue Sphere", "Boss Theme", "Flag Bump", "Not Enough Rings", "Special Stage", "Bumper", "Drown Warning", "Ring Cash-In", "Spin", "Badnik Kill", "Breathe", "Lamppost", "Spikes", "Antigrav Funnel", "Flying Battery", "Mushroom Bounce", "Teleport", "Alarm", "Flying Battery Zone Theme", "Bridge Moving Up", "Regular Shield")]
        SonicKnucklesSounds,
        [SouvenirQuestion("Which badnik was shown in {0}?", "Sonic & Knuckles", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteField = "SonicKnucklesBadniksSprites")]
        SonicKnucklesBadnik,
        [SouvenirQuestion("Which monitor was shown in {0}?", "Sonic & Knuckles", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteField = "SonicKnucklesMonitorsSprites")]
        SonicKnucklesMonitor,

        [SouvenirQuestion("What was the {1} picture on {0}?", "Sonic The Hedgehog", TwoColumns4Answers, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.Sprites, SpriteField = "SonicTheHedgehogSprites")]
        SonicTheHedgehogPictures,
        [SouvenirQuestion("Which sound was played by the {1} screen on {0}?", "Sonic The Hedgehog", TwoColumns4Answers, "Boss Theme", "Breathe", "Continue", "Drown", "Emerald", "Extra Life", "Final Zone", "Invincibility", "Jump", "Lamppost", "Marble Zone", "Bumper", "Skid", "Spikes", "Spin", "Spring",
            ExampleExtraFormatArguments = new[] { "Running Boots", "Invincibility", "Extra Life", "Rings" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SonicTheHedgehogSounds,

        [SouvenirQuestion("What positions were the last swap used to solve {0}?", "Sorting", ThreeColumns6Answers, "1 & 2", "1 & 3", "1 & 4", "1 & 5", "2 & 3", "2 & 4", "2 & 5", "3 & 4", "3 & 5", "4 & 5")]
        SortingLastSwap,

        [SouvenirQuestion("What was the first module asked about in the other Souvenir on this bomb?", "Souvenir", OneColumn4Answers, null,
            ExampleAnswers = new[] { "Probing", "Microcontroller", "Third Base", "Kudosudoku", "Quintuples", "3D Tunnels", "Uncolored Squares", "Pattern Cube", "Synonyms", "The Moon", "Human Resources", "Algebra" })]
        SouvenirFirstQuestion,

        [SouvenirQuestion("What was the maximum tax amount per vessel in {0}?", "Space Traders", ThreeColumns6Answers, "0 GCr", "1 GCr", "2 GCr", "3 GCr", "4 GCr", "5 GCr")]
        SpaceTradersMaxTax,

        [SouvenirQuestion("What was the {1} flashed color in {0}?", "Sphere", ThreeColumns6Answers, "red", "blue", "green", "orange", "pink", "purple", "grey", "white", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        SphereColors,

        [SouvenirQuestion("What word was asked to be spelled in {0}?", "Spelling Bee", TwoColumns4Answers, null, ExampleAnswers = new[] { "allocation", "auxiliary", "cloying", "connoisseur", "controversial", "deceit", "garrulous", "malachite", "perambulate", "sedge" })]
        SpellingBeeWord,

        [SouvenirQuestion("What bag was initially colored in {0}?", "Splitting The Loot", ThreeColumns6Answers, null, ExampleAnswers = new[] { "A5", "E6", "19", "82" })]
        SplittingTheLootColoredBag,

        [SouvenirQuestion("Who was the {1} child displayed in {0}?", "Spongebob Birthday Identification", OneColumn4Answers, "Abela", "Aiden", "Allen", "Amber", "Apollo Yuojan", "Ashley", "Bobby", "Brayden", "Brendon", "Brent", "Bryce", "Caoimhe", "Carl Pobie", "Carlos Paolo", "Carson", "Chester Paul", "Christopher", "Cristian James Glavez", "Cyan Miguel", "Danny", "Dave", "Davian", "Donn Jeff Velionix Fijo", "Drew Justin", "Ethan", "Fabio", "Frame Baby", "Gabriel Felix", "Grayson", "Hayden", "Jacob", "Jaden", "Jake", "James", "Jayden", "Jeremiah", "Jon JonJon Eric Cabebe Jr.", "Juan Carlos", "Julian", "Junely Delos Reyes Jr.", "Kate Venus Valadores", "Ken Ivan", "Kenny Lee", "King Monic", "Kurt", "Landon", "Logan", "Lukas", "Makenly", "Mason", "Max", "Melvern Ryann", "Michael", "Miguel", "Myles A. Williams", "Neftali Xyler S. Ilao", "Noah", "Patrick", "Raymond", "Rhojus", "Sam Daniel", "Seth Laurence", "Shik", "Simon", "Sony Boy", "Spanky", "Spencer", "Stacey", "Steve Jr.", "Xander Chio E. Ceniza",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        SpongebobBirthdayIdentificationChildren,

        [SouvenirQuestion("What was the color of the {1} lit LED in {0}?", "Stability", TwoColumns4Answers, "Red", "Yellow", "Blue",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        StabilityLedColors,
        [SouvenirQuestion("What was the identification number in {0}?", "Stability", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9999, "0000")]
        StabilityIdNumber,

        [SouvenirQuestion("Which of these is the length of a sequence in {0}?", "Stacked Sequences", TwoColumns4Answers, null, ExampleAnswers = new[] { "3", "4", "5", "6" })]
        [AnswerGenerator.Integers(3, 9)]
        StackedSequences,

        [SouvenirQuestion("What was the digit in the center of {0}?", "Stars", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        StarsCenter,

        [SouvenirQuestion("What was the element shown in {0}?", "State of Aggregation", ThreeColumns6Answers, "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Al", "Si", "P", "S", "Cl", "Ar", "K", "Ca", "Sc", "Ti", "V", "Cr", "Mn", "Fe", "Co", "Ni", "Cu", "Zn", "Ga", "Ge", "As", "Se", "Br", "Kr", "Rb", "Sr", "Y", "Zr", "Nb", "Mo", "Tc", "Ru", "Rh", "Pd", "Ag", "Cd", "In", "Sn", "Sb", "Te", "I", "Xe", "Cs", "Ba", "La", "Ce", "Pr", "Nd", "Pm", "Sm", "Eu", "Gd", "Tb", "Dy", "Ho", "Er", "Tm", "Yb", "Lu", "Hf", "Ta", "W", "Re", "Os", "Ir", "Pt", "Au", "Hg", "Tl", "Pb", "Bi", "Po", "At", "Rn", "Fr", "Ra", "Ac", "Th", "Pa", "U", "Np", "Pu", "Am", "Cm", "Bk", "Cf", "Es", "Fm", "Md", "No", "Lr", "Rf", "Db", "Sg", "Bh", "Hs", "Mt", "Ds", "Rg", "Cn", "Nh", "Fl", "Mc", "Lv", "Ts", "Og")]
        StateOfAggregationElement,

        [SouvenirQuestion("What was the {1} letter in {0}?", "Stellar", ThreeColumns6Answers, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
            ExampleExtraFormatArguments = new[] { "Morse code", "tap code", "Braille" }, ExampleExtraFormatArgumentGroupSize = 1)]
        StellarLetters,

        [SouvenirQuestion("What was the value of the {1} arrow in {0}?", "Stupid Slots", ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Integers(-30, 30)]
        StupidSlotsValues,

        [SouvenirQuestion("How many subscribers does {1} have in {0}?", "Subscribe to Pewdiepie", TwoColumns4Answers, null,
            ExampleExtraFormatArguments = new[] { "PewDiePie", "T-Series" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        [AnswerGenerator.Integers(10000000, 99999999)]
        SubscribeToPewdiepieSubCount,

        [SouvenirQuestion("What skull was shown on the {1} square in {0}?", "Sugar Skulls", ThreeColumns6Answers, "A", "C", "E", "G", "I", "K", "M", "O", "P", "R", "T", "V", "X", "Z", "b", "d", "f", "h", "j", "l", "n", "p", "r", "t", "v", "x", "z", TranslateFormatArgs = new[] { true },
            Type = AnswerType.SugarSkullsFont, FontSize = 432, CharacterSize = 1 / 6f, ExampleExtraFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SugarSkullsSkull,
        [SouvenirQuestion("Which skull {1} present in {0}?", "Sugar Skulls", ThreeColumns6Answers, "A", "C", "E", "G", "I", "K", "M", "O", "P", "R", "T", "V", "X", "Z", "b", "d", "f", "h", "j", "l", "n", "p", "r", "t", "v", "x", "z",
            Type = AnswerType.SugarSkullsFont, FontSize = 432, CharacterSize = 1 / 6f, ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        SugarSkullsAvailability,

        [SouvenirQuestion("What was the displayed word in {0}?", "Superparsing", ThreeColumns6Answers, "ROOT", "WARM", "THEM", "MAKE", "TIES", "LOTS", "RUGS", "SUCH", "STEM", "DIET", "RENT", "AQUA", "LOWS", "PATH", "HULL", "DRAW", "SLOT", "BRAD", "EGGS", "MEMO", "WIFE", "SHED", "FIRM", "SKIP", "DARK", "REAR", "EDGE", "LOCK", "BOWL", "DEAD", "ZONE", "GOOD", "FLAT", "GREW", "CRAP", "PUBS", "PEST", "ORAL", "FORD", "TRIM", "HIGH", "OKAY", "FLOW", "HITS", "JACK", "RUBY", "REEF", "TANK", "PUTS", "TRIP", "POST", "PAIN", "READ", "GEAR", "THEN", "FATE", "ARMY", "MINS", "DREW", "WIND", "FAME", "FUNK", "SOLD", "CREW", "WIFI", "CITE", "DAMN", "FARM", "NOSE", "HOOD", "CORN", "FAIR", "ADAM", "TIDE", "DISH", "GOAL", "DUMP", "GUAM", "GETS", "SOUP", "SYNC", "LAWN", "ROPE", "CLAN", "MESA", "BALL", "LORD", "EACH", "REEL", "TIER", "BYTE", "LIFT", "TUNE", "HEAR", "MALL", "HELL", "CITY", "MENS", "HOSE", "PIKE", "GOES", "PALM", "MILD", "JURY", "JUAN", "SUCK", "NAVY", "POOR", "LETS", "BOOK", "ACRE", "INFO", "MESS", "JUMP", "LAWS", "BEST", "BOFA", "PURE", "BUCK", "TAIL", "LEGS", "BIND", "HOST", "EYES", "MILF", "HANG", "VERY", "OVAL", "WELL", "LENS", "OOPS", "DIRT", "RAIN", "ONTO", "VICE", "HOME", "DUTY", "VOLT", "GAIN", "BEAR", "DAVE", "GEEK", "SINK", "BASS", "DESK", "POLE", "LACK", "NINE", "KNEW", "DUCK", "BLOW", "ZOOM", "SLOW", "SPOT", "MEGA", "RAID", "BEND", "DIVE", "MOMS", "VOID", "HOLE", "TIME", "ENDS", "THEY", "SNAP", "LOGS", "FROG", "CAST", "YEAH", "PAYS", "SITE", "EVIL", "SWAP", "OILS", "GENE", "BOAT", "FELT", "RUNS", "PAGE", "FOLD", "PAIR", "PINS", "WATT", "PETS", "RIDE", "IRON", "GRIP", "STUD", "HORN", "AIDS", "AIDS", "SOUL", "FLAG", "FREE", "WILL", "CLIP", "SPAN", "NAIL", "RARE", "NUTS", "JOKE", "BARS", "PADS", "MOON", "THAT", "THIS", "LOUD", "BAGS", "JAZZ", "WITH", "RATS", "FEAR", "WEAR", "DUAL", "MINE", "SEAT", "BIAS", "FOAM", "POEM", "CAPE", "BIDS", "ASKS", "TOLL", "BEAN", "WHOM", "SWIM", "BIRD", "DOGS", "RISE", "LADY", "MATH", "FANS", "CUPS", "ZERO", "NULL", "KNEE", "ROLL", "PREP", "FROM", "HAVE", "CENT", "POND", "LIPS", "WIRE", "MORE", "TAPE", "SPAM", "HIDE", "ACID", "BATH", "BARE", "HUNG", "RIPE", "FAIL", "KEPT", "JEEP", "PRAY", "WARS", "POPE", "RELY", "BETA", "DRAG", "FONT", "ARMS", "TIRE", "APPS", "TILE", "BEAT", "BOOB", "WAIT", "JUST", "KEYS", "EXIT", "LIST", "NAME", "ROSE", "WAVE", "FUCK", "MESH", "YEAR", "SOFT", "ACTS", "HOLY", "WALK", "FUEL", "ROWS", "OVER", "CATS", "DATE", "FIND", "KITS", "HARM", "THAN", "BOMB", "BACK", "ARCH", "UGLY", "COIN", "PICK", "FAKE", "SOIL", "CURE", "LACE", "INTO", "COLD", "MOST", "WORK", "SEES", "LAST", "FOLK", "TRIO", "CUBE", "DENY", "SHOT", "LUNG", "QUIT", "DATA", "USED", "NEXT", "EARS", "MISS", "FIST", "TASK", "DEAN", "GODS", "TALE", "MART", "WILD", "HEAT", "BOND", "TAXI", "MASS", "FOOT", "MERE", "POLL", "BULL", "BENT", "SNOW", "SAGE", "TRAY", "CAMP", "ONLY", "SLIM", "ISLE", "WHEN", "MATE", "DIED", "INCH", "CHEF", "LANE", "FILL", "NUKE", "BOOM", "CALM", "PEAK", "BUZZ", "GONE", "WHAT", "FORT", "EURO", "NEWS", "BELL", "SALT", "DICK", "FORK", "TIED", "HERE", "CAVE", "BIKE", "DIES", "DROP", "RICH", "WERE", "TRAP", "FOOL", "OVEN", "SOME", "LOSE", "SEEK", "LOAD", "DEER", "TAGS", "LIKE", "COCK", "JAIL", "GUYS", "BEEN", "PISS", "ONES", "GAVE", "ALSO", "TITS", "WOOL", "RANK", "HELP", "EYED", "SEEM", "TONS", "NOON", "COOK", "PLUG", "GRAB", "HIRE", "AGES", "PINK", "HAWK", "SHOW", "EVEN", "MIND", "LOST", "TOUR", "SIZE", "CODE", "MENU", "HELD", "TEND", "GULF", "ADDS", "PINE", "LIME", "AIMS", "PULL", "HOPE", "EASY", "DECK", "BLUE", "FINE", "MOLD", "AXIS", "HERB", "ALOT", "ROLE", "SOAP", "WISH", "IDLE", "LONG", "CAME", "TUBE", "STAR", "DRUG", "COPY", "YANG", "KING", "PICS", "MINT", "OPEN", "CASH", "REID", "BUSY", "NICK", "TONE", "PLOT", "HEEL", "FLEX", "MEAN", "HUNT", "TURN", "LINK", "SEAL", "MUCH", "SIGN", "SOON", "HAND", "TINY", "RICK", "COVE", "WING", "WASH", "FILE", "NECK", "PORN", "SEEN", "SPIN", "STOP", "PORT", "KEEP", "HOUR", "BUSH", "CLUB", "EASE", "RUSH", "TEXT", "CHAR", "DOLL", "RATE", "MALE", "ORGY", "ORGY", "ORGY", "ORGY", "FUND", "DOCK", "TABS", "MODS", "DUMB", "MODE", "DICE", "ROAD", "GIFT", "WEED", "TOOK", "SELL", "OWEN", "BITE", "CHIP", "DOOM", "WIKI", "TOOL", "IDEA", "BODY", "PERU", "EAST", "MYTH", "SONG", "FALL", "LATE", "HARD", "BARN", "FARE", "BUTT", "PUNK", "WANT", "PACE", "DUKE", "MOVE", "GUNS", "BELT", "BALD", "HUGE", "KIND", "HERO", "LOGO", "WAGE", "BOLT", "TAKE", "WENT", "DAWN", "FOUR", "CARL", "COAT", "SPAS", "HINT", "LEAD", "SHOP", "PORK", "NICE", "GRID", "BUYS", "BAND", "ECHO", "SENT", "STAY", "TECH", "GRAD", "LEAF", "PEER", "MEET", "OURS", "BOTH", "LEVY", "PAST", "UNIT", "FACT", "EDIT", "AUTO", "PLUS", "UPON", "FIVE", "BRAS", "FAST", "SONS", "GURU", "GOLD", "KISS", "PIPE", "RISK", "CUBA", "TOWN", "MEAT", "SILK", "BOLD", "ARTS", "BANK", "SCAN", "ZINC", "LIES", "IDOL", "FEEL", "STAN", "TIPS", "CUTE", "GAPS", "WISE", "WORD", "ODDS", "BILL", "WARD", "SEED", "LAID", "DOWN", "NEST", "CLAY", "WEAK", "TEAR", "EVER", "CARE", "TALK", "PALE", "MARK", "SOLE", "BULK", "ROCK", "ROOF", "AGED", "SAYS", "LAND", "NEON", "DONE", "GALE", "GAME", "KIDS", "NUDE", "OWNS", "CHAT", "FIRE", "CORK", "MILL", "CUTS", "PENS", "MARS", "SAME", "HEAD", "GATE", "ONCE", "MATS", "SLIP", "DEAF", "AWAY", "CELL", "SELF", "WORM", "LOSS", "HASH", "LAZY", "PILL", "CASE", "FRED", "WOOD", "BABY", "META", "NEAR", "BASE", "DOME", "COPE", "FACE", "FILM", "CARB", "PUMP", "TERM", "CAPS", "ANNE", "SONY", "WIDE", "TELL", "LOAN", "LABS", "KEEN", "CARS", "BANG", "ALEX", "RYAN", "PEAS", "GIRL", "ABLE", "GREY", "TOYS", "GOLF", "WOLF", "URLS", "BEDS", "SORT", "BONE", "BLAN", "MIME", "NONE", "MEAL", "MOSS", "HURT", "LAKE", "KICK", "FITS", "EXAM", "SURE", "HALF", "STEP", "BUGS", "BURN", "SUIT", "QUAD", "LAMP", "CAGE", "SHOE", "DASH", "BOYS", "OAKS", "KNIT", "REST", "FORM", "MAIL", "DEEP", "PLAN", "WAYS", "LION", "LOOP", "CROP", "SICK", "CAKE", "TOLD", "RICE", "TALL", "DIAL", "FOOD", "CULT", "RULE", "DEAR", "GORE", "VOTE", "SKIN", "CARD", "HATE", "LAMB", "FELL", "MUST", "WALL", "THOU", "TREE", "THUS", "RAGE", "BEER", "FLUX", "COAL", "REAL", "PING", "WINE", "SEAS", "ITEM", "YARD", "COST", "LUCK", "MASK", "POOL", "FULL", "SANS", "SURF", "YARN", "MILK", "HOOK", "LEAN", "LIFE", "ASIA", "KNOW", "POET", "USES", "WEST", "RING", "MARY", "PASS", "ROOM", "GARY", "FEES", "SAKE", "BOSS", "EPIC", "FOUL", "TEEN", "JOIN", "ARAB", "CORD", "ROME", "JADE", "VARY", "HATS", "DUDE", "NOTE", "LIVE", "JUNK", "DOOR", "WEEK", "SOLO", "FEET", "ALTO", "DOSE", "SEXY", "NORM", "PART", "LEFT", "BEEF", "TEAM", "DAYS", "LOOK", "BOOT", "SALE", "WARE", "PUSH", "COOL", "GANG", "JOBS", "MANY", "WRAP", "FEED", "MOOD", "BLOG", "QUIZ", "LESS", "GLAD", "SOFA", "NEED", "VAST", "SAFE", "JOHN", "WAKE", "WINS", "TWIN", "GOAT", "GLOW", "HALO", "GAYS", "FEAT", "DONT", "USER", "GRAY", "SAID", "SHIT", "SAIL", "TYPE", "TILL", "DRUM", "CALL", "REED", "JETS", "SING", "DUST", "YOGA", "VIDS", "SETS", "MICE", "GIVE", "TEMP", "SIDE", "BITS", "PARK", "SEND", "HOLD", "DEAL", "CORE", "MAIN", "DEBT", "MAPS", "HILL", "SHIP", "THIN", "CART", "DOES", "SAND", "DEMO", "NOVA", "DEPT", "RAYS", "HACK", "PAID", "COME", "RACK", "SEMI", "ANAL", "JULY", "SEAN", "MADE", "FISH", "SHUT", "CONF", "HAIR", "EARN", "LOVE", "TEST", "PLAY", "PACK", "BORN", "INTL", "LITE", "ACNE", "TOPS", "RACE", "LIBS", "HALL", "BEAM", "LINE", "CAFE", "WALT", "UNDO", "SAVE", "ANTI", "POSE", "TENT")]
        SuperparsingDisplayed,

        [SouvenirQuestion("What color was the {1} LED on the {2} flip of {0}?", "Switch", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "top", QandA.Ordinal, "bottom", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2, AddThe = true)]
        SwitchInitialColor,

        [SouvenirQuestion("What was the initial position of the switches in {0}?", "Switches", ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings(5, 'Q', 'R')]
        SwitchesInitialPosition,

        [SouvenirQuestion("What was the seed in {0}?", "Switching Maze", TwoColumns4Answers, null)]
        [AnswerGenerator.Strings(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/")]
        SwitchingMazeSeed,
        [SouvenirQuestion("What was the starting maze color in {0}?", "Switching Maze", ThreeColumns6Answers, "Blue", "Cyan", "Magenta", "Orange", "Red", "White", TranslateAnswers = true)]
        SwitchingMazeColor,

        [SouvenirQuestion("How many symbols were cycling on the {1} screen in {0}?", "Symbol Cycle", TwoColumns4Answers, "2", "3", "4", "5", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "left", "right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SymbolCycleSymbolCounts,

        [SouvenirQuestion("What was the {1} symbol in the {2} stage of {0}?", "Symbolic Coordinates", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "SymbolicCoordinatesSprites", TranslateFormatArgs = new[] { true, false },
            ExampleExtraFormatArguments = new[] { "left", QandA.Ordinal, "middle", QandA.Ordinal, "right", QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        SymbolicCoordinateSymbols,

        [SouvenirQuestion("Which button flashed {1} in the final sequence of {0}?", "Symbolic Tasha", ThreeColumns6Answers, "Top", "Right", "Bottom", "Left", "Pink", "Green", "Yellow", "Blue",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        SymbolicTashaFlashes,
        [SouvenirQuestion("Which symbol was on the {1} button in {0}?", "Symbolic Tasha", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "SymbolicTashaSprites",
            ExampleExtraFormatArguments = new[] { "top", "right", "bottom", "left", "blue", "green", "yellow", "pink" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        SymbolicTashaSymbols,

        [SouvenirQuestion("What was displayed on the screen in the {1} stage of {0}?", "SYNC-125 [3]", TwoColumns4Answers, null, Type = AnswerType.DynamicFont, ExampleAnswers = new[] { "İ'ms'", "ăĠ'n'", "kğ'i", "kĞ'p'", "ăut'", "ăġ'r", "ăġ'm", "ărs", "kğp'", "kğk" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        Sync125_3Word,

        [SouvenirQuestion("Which number was displayed on {0}?", "Synonyms", ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        SynonymsNumber,

        [SouvenirQuestion("What error code did you fix in {0}?", "Sysadmin", ThreeColumns6Answers, null, ExampleAnswers = new[] { "391M", "4HZZ", "56OW", "6RO0", "6WMJ", "8V94", "CYB6", "HR71", "PT68", "X8IZ" })]
        SysadminFixedErrorCodes,

        [SouvenirQuestion("What was the received word in {0}?", "Tap Code", TwoColumns4Answers, null, ExampleAnswers = new[] { "child", "style", "shake", "alive", "axion", "wreck", "cause", "pupil", "cheat", "watch" })]
        TapCodeReceivedWord,

        [SouvenirQuestion("What was the {1} flashed color in {0}?", "Tasha Squeals", TwoColumns4Answers, "Pink", "Green", "Yellow", "Blue", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        TashaSquealsColors,

        [SouvenirQuestion("Where was the starting position in {0}?", "Tasque Managing", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "TasqueManagingSprites")]
        TasqueManagingStartingPos,

        [SouvenirQuestion("Which ingredient was displayed {1}, from left to right, in {0}?", "Tea Set", ThreeColumns6Answers, AddThe = true, Type = AnswerType.Sprites, SpriteField = "TeaSetSprites", ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        TeaSetDisplayedIngredients,

        [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Technical Keypad", ThreeColumns6Answers, IsEntireQuestionSprite = true, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        TechnicalKeypadDisplayedDigits,

        [SouvenirQuestion("What was the initial color of the {1} button in the {2} stage of {0}?", "Ten-Button Color Code", TwoColumns4Answers, "red", "green", "blue", "yellow", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        TenButtonColorCodeInitialColors,

        [SouvenirQuestion("What was the {1} split in {0}?", "Tenpins", OneColumn4Answers, "Goal Posts", "Cincinnati", "Woolworth Store", "Lily", "3-7 Split", "Cocked Hat", "4-7-10 Split", "Big Four", "Greek Church", "Big Five", "Big Six", "HOW", TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TenpinsSplits,

        [SouvenirQuestion("What colour triangle pulsed {1} in {0}?", "Tetriamonds", ThreeColumns6Answers, "orange", "lime", "jade", "azure", "violet", "rose", "grey", ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        TetriamondsPulsingColours,

        [SouvenirQuestion("What was the displayed letter in {0}?", "Text Field", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F")]
        TextFieldDisplay,

        [SouvenirQuestion("What was the position from top to bottom of the first wire needing to be cut in {0}?", "Thinking Wires", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7")]
        ThinkingWiresFirstWire,
        [SouvenirQuestion("What color did the second valid wire to cut have to have in {0}?", "Thinking Wires", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", "Black", "Any", TranslateAnswers = true)]
        ThinkingWiresSecondWire,
        [SouvenirQuestion("What was the display number in {0}?", "Thinking Wires", ThreeColumns6Answers, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "69")]
        ThinkingWiresDisplayNumber,

        [SouvenirQuestion("What was the display word in the {1} stage on {0}?", "Third Base", ThreeColumns6Answers, "NHXS", "IH6X", "XI8Z", "I8O9", "XOHZ", "H68S", "8OXN", "Z8IX", "SXHN", "6NZH", "H6SI", "6O8I", "NXO8", "66I8", "S89H", "SNZX", "9NZS", "8I99", "ZHOX", "SI9X", "SZN6", "ZSN8", "HZN9", "X9HI", "IS9H", "XZNS", "X6IS", "8NSZ",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ThirdBaseDisplay,

        [SouvenirQuestion("What was on the {1} button at the start of {0}?", "Tic Tac Toe", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "O", "X", Type = AnswerType.TicTacToeFont,
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        TicTacToeInitialState,

        [SouvenirQuestion("What was the {1} city in {0}?", "Timezone", TwoColumns4Answers, "Alofi", "Papeete", "Unalaska", "Whitehorse", "Denver", "Managua", "Quito", "Manaus", "Buenos Aires", "Sao Paulo", "Praia", "Edinburgh", "Berlin", "Bujumbura", "Moscow", "Tbilisi", "Lahore", "Omsk", "Bangkok", "Beijing", "Tokyo", "Brisbane", "Sydney", "Tarawa",
            ExampleExtraFormatArguments = new[] { "departure", "destination" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        TimezoneCities,

        [SouvenirQuestion("Which of these squares was safe in row {1} in {0}?", "Tip Toe", ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { "9", "10" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        TipToeSafeSquares,

        [SouvenirQuestion("What was the word initially shown in {0}?", "Topsy Turvy", ThreeColumns6Answers, "Topsy", "Robot", "Cloud", "Round", "Quilt", "Found", "Plaid", "Curve", "Water", "Ovals", "Verse", "Sandy", "Frown", "Windy", "Curse", "Ghost")]
        TopsyTurvyWord,

        [SouvenirQuestion("What was the transmitted word in {0}?", "Touch Transmission", ThreeColumns6Answers, "that", "this", "not", "your", "all", "new", "was", "can", "has", "but", "one", "may", "what", "which", "their", "use", "any", "there", "see", "his", "here", "web", "get", "been", "were", "these", "its", "than", "find", "top", "had", "list", "just", "over", "year", "day", "into", "two", "used", "last", "most", "buy", "post", "add", "such", "best", "where", "info", "high", "very", "read", "sex", "need", "user", "set", "map", "know", "way", "part", "real", "must", "line", "did", "send", "using", "forum", "even", "being", "much", "link", "open", "south", "both", "power", "care", "down", "him", "without", "think", "big", "law", "shop", "old", "main", "man", "card", "job", "teen", "too", "join", "west", "team", "box", "gay", "start", "air", "yes", "hot", "cost", "march", "say", "going", "test", "cart", "staff", "things", "tax", "got", "let", "park", "act", "key", "few", "age", "hard", "pay", "four", "offer", "easy", "fax", "china", "yet", "areas", "sun", "enter", "share", "run", "net", "term", "put", "try", "god", "head", "least", "log", "cars", "fun", "arts", "lot", "ask", "beach", "past", "due", "ever", "ago", "cheap", "mark", "bad", "edit", "fast", "often", "though", "town", "step", "shows", "enough", "death", "brand", "oil", "bit", "near", "stuff", "doing", "stay", "mean", "force", "cash", "bay", "seen", "stop", "dog", "mind", "lost", "tour", "menu", "wish", "lower", "fine", "hour", "gas", "six", "bush", "sat", "zip", "bid", "kind", "sent", "shown", "lead", "went", "idea", "deal", "forms", "feed", "cut", "earth", "ship", "kit", "boy", "wine", "stars", "owner", "son", "bring", "grand", "van", "skin", "pop", "rest", "hit", "fish", "eye", "string", "youth", "fee", "rent", "dark", "aid", "host", "hands", "fat", "saw", "dead", "farm", "showing", "hear", "fan", "former", "cat", "die", "flow", "path", "pet", "guy", "cup", "army", "gear", "forest", "ending", "wind", "bob", "fit", "pain", "cum", "edge", "ice", "pink", "shot", "bus", "heat", "nor", "bug", "soft", "theme", "rich", "touch", "chain", "died", "reach", "lab", "snow", "owned", "chart", "gene", "ends", "cast", "soul", "ended", "dining", "mix", "fix", "ray", "bear", "gain", "dry", "blow", "shared", "cent", "forced", "zero", "bath", "sharing", "won", "wear", "mom", "rare", "bars", "seat", "aim", "rings", "tip", "mine", "whom", "math", "fly", "fear", "standing", "wars", "hey", "beat", "arms", "sky", "toy", "slow", "hip", "nine", "grow", "dot", "rain", "yeah", "cap", "peak", "raw", "sharp", "wet", "ram", "fox", "mesh", "dean", "pub", "hop", "mouth", "gun", "lens", "warm", "rear", "showed", "mens", "bowl", "kid", "pan", "dish", "eating", "vary", "arab", "bands", "push", "tower", "sum", "shower", "dear", "vat", "beer", "sir", "earn", "twin", "spy", "chip", "sit", "echo", "fig", "stands", "teach", "tab", "beds", "aged", "seed", "peer", "meat", "inner", "leg", "tiny", "gap", "rob", "mining", "jet", "mad", "shoe", "joy", "ran", "seal", "ill", "lay", "wings", "bet", "throw", "dad", "pat", "yard", "pour", "dust", "kings", "tie", "ward", "roof", "beast", "rush", "wins", "ghost", "toe", "shit", "ease", "arena", "lands", "armed", "pine", "tend", "candy", "finger", "tough", "lie", "chest", "weak", "leaf", "pad", "rod", "sad", "meal", "pot", "mars", "theft", "swing", "mint", "spin", "wash", "jam", "hero", "ion", "peru", "singer", "aging", "reed", "ban", "vast", "odd", "beam", "shut", "inform", "cry", "zoo", "arrow", "rough", "outer", "steam", "ace", "sue", "eggs", "mins", "stem", "opt", "rap", "charm", "soup", "cod", "singing", "gel", "doug", "mart", "coin", "harm", "deer", "pal", "oven", "cheat", "gym", "tan", "pie", "tied", "bingo", "cedar", "stud", "bend", "dam", "chad", "dying", "bench", "tub", "inns", "easter", "landing", "bean", "wheat", "bee", "loud", "bare", "pit", "ton", "lying", "handed", "sink", "pins", "handy", "rid", "rip", "lip", "sap", "forming", "eyed", "ought", "aye", "forty", "rows", "ears", "fist", "mere", "dig", "caring", "deny", "rim", "tier", "andrea", "pig", "lit", "duo", "fog", "fur", "rug", "ham", "sheer", "bind", "lows", "pest", "sofa", "tent", "dare", "wax", "nut", "lean", "bye", "strand", "dash", "lap", "steal", "ant", "gem", "heath", "yeast", "myth", "gig", "weed", "hint", "barn", "fare", "herb", "ate", "mud", "shark", "shine", "dip", "hash", "lined", "pens", "lid", "deaf", "keen", "peas", "owns", "hay", "zinc", "tear", "nest", "cop", "dim", "stan", "sip", "feat", "glow", "ware", "foul", "seas", "forge", "pod", "ours", "wit", "yarn", "mug", "marsh", "bent", "hat")]
        TouchTransmissionWord,
        [SouvenirQuestion("In what order was the Braille read in {0}?", "Touch Transmission", OneColumn4Answers, "Standard Braille Order", "Individual Reading Order", "Merged Reading Order", "Chinese Reading Order")]
        TouchTransmissionOrder,

        [SouvenirQuestion("Which function did the {1} button perform in {0}?", "Trajectory", TwoColumns4Answers,
            "red up", "red right", "red down", "red left", "red reverse", "green up", "green right", "green down", "green left", "green reverse", "blue up", "blue right", "blue down", "blue left", "blue reverse",
            ExampleExtraFormatArguments = new[] { "A", "B", "C" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TrajectoryButtonFunctions,

        [SouvenirQuestion("What was the {1} received message in {0}?", "Transmitted Morse", TwoColumns4Answers, "BOMBS", "SHORT", "UNDERSTOOD", "W1RES", "SOS", "MANUAL", "STRIKED", "WEREDEAD", "GOTASOUV", "EXPLOSION", "EXPERT", "RIP", "LISTEN", "DETONATE", "ROGER", "WELOSTBRO", "AMIDEAF", "KEYPAD", "DEFUSER", "NUCLEARWEAPONS", "KAPPA", "DELTA", "PI3", "SMOKE", "SENDHELP", "LOST", "SWAN", "NOMNOM", "BLUE", "BOOM", "CANCEL", "DEFUSED", "BROKEN", "MEMORY", "R6S8T", "TRANSMISSION", "UMWHAT", "GREEN", "EQUATIONSX", "RED", "ENERGY", "JESTER", "CONTACT", "LONG",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        TransmittedMorseMessage,

        [SouvenirQuestion("What colour triangle pulsed {1} in {0}?", "Triamonds", ThreeColumns6Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white", ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        TriamondsPulsingColours,

        [SouvenirQuestion("Which of these was one of the passwords in {0}?", "Triple Term", ThreeColumns6Answers, null, ExampleAnswers = new[] { "Three", "Every", "These", "Would", "Where", "First", "Still", "Plant", "Small", })]
        TripleTermPasswords,

        [SouvenirQuestion("What was the {1} line you commented out in {0}?", "Turtle Robot", TwoColumns4Answers, null, ExampleAnswers = new[] { "LT 90", "FD 1", "RT 180 2", "LT 90 2", "RT 180", "FD 6", "RT 90 2" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.TurtleRobotFont)]
        TurtleRobotCodeLines,

        [SouvenirQuestion("What was the {1} correct query response from {0}?", "Two Bits", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 99, "00")]
        TwoBitsResponse,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Ultimate Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "ACCESS", "EMPIRE", "EXPEND", "INDUCE", "LOCATE", "MELODY", "SPIRIT", "STOLEN", "VESSEL", "WIGGLE" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        UltimateCipherScreen,

        [SouvenirQuestion("What was the {1} in {0}?", "Ultimate Cycle", TwoColumns4Answers, "Advanced", "Adverted", "Advocate", "Addition", "Allocate", "Allotype", "Allotted", "Altering", "Binaries", "Binormal", "Binomial", "Billions", "Bulkhead", "Bullhorn", "Bulleted", "Bulwarks", "Ciphered", "Circuits", "Connects", "Conquers", "Commando", "Compiler", "Computer", "Continue", "Decrypts", "Deceived", "Decimate", "Division", "Discover", "Discrete", "Dispatch", "Disposal", "Encipher", "Encrypts", "Encoding", "Entrance", "Equalise", "Equators", "Equation", "Equipped", "Finalise", "Finished", "Findings", "Finnicky", "Formulae", "Fortunes", "Fortress", "Forwards", "Garrison", "Garnered", "Gatepost", "Gateways", "Gauntlet", "Gambling", "Gathered", "Glooming", "Hazarded", "Haziness", "Hotlinks", "Hotheads", "Hundreds", "Hunkered", "Huntsman", "Huntress", "Incoming", "Indicate", "Indirect", "Indigoes", "Illuding", "Illusion", "Illusory", "Illumine", "Jigsawed", "Jimmying", "Journeys", "Jousting", "Junction", "Juncture", "Junkyard", "Judgment", "Kilowatt", "Kilovolt", "Kilobyte", "Kinetics", "Knocking", "Knockout", "Knowable", "Knuckled", "Language", "Landmark", "Limiting", "Linearly", "Lingered", "Linkages", "Linkwork", "Labeling", "Monogram", "Monolith", "Monomial", "Monotone", "Multiton", "Multiply", "Mulcting", "Mulligan", "NANOBOTS", "Nanogram", "Nanowatt", "Nanotube", "Numbered", "Numerous", "Numerals", "Numerate", "Octangle", "Octuples", "Ordering", "Ordinals", "Observed", "Obscured", "Obstruct", "Obstacle", "Progress", "Projects", "Prophase", "Prophecy", "Postsync", "Possible", "Positron", "Positive", "Quadrant", "Quadrics", "Quartile", "Quartics", "Quickest", "Quirkish", "Quintics", "Quitters", "Reversed", "Revolved", "Revealed", "Rotation", "Rotators", "Relation", "Relative", "Relaying", "Starting", "Standard", "Standout", "Stanzaic", "Stoccata", "Stockade", "Stopping", "Stopword", "Trickier", "Trigonal", "Triggers", "Triangle", "Tomogram", "Tomahawk", "Toggling", "Together", "Underrun", "UnderwaY", "Underlie", "Undoings", "Ulterior", "Ultimate", "Ultrared", "Ultrahot", "Venomous", "Vendetta", "Vicinity", "Viceless", "Volition", "Voltages", "Volatile", "Voluming", "Weakened", "Weaponed", "Wingding", "Winnable", "Whatever", "Whatness", "Whatnots", "Whatsits", "Yellowed", "Yearlong", "Yearning", "Yeasayer", "Yielding", "Yielders", "Yokozuna", "Yourself", "Zippered", "Ziggurat", "Zigzaggy", "Zugzwang", "Zygomata", "Zygotene", "Zymology", "Zymogram",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        UltimateCycleWord,

        [SouvenirQuestion("What was the {1} rotation in {0}?", "Ultracube", ThreeColumns6Answers, "XY", "YX", "XZ", "ZX", "XW", "WX", "XV", "VX", "YZ", "ZY", "YW", "WY", "YV", "VY", "ZW", "WZ", "ZV", "VZ", "WV", "VW", AddThe = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        UltracubeRotations,

        [SouvenirQuestion("What was the {1} rotation in the {2} stage of {0}?", "UltraStores", ThreeColumns6Answers, null, ExampleAnswers = new[] { "UZ", "VU", "WV", "YU", "YW", "YX" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        UltraStoresSingleRotation,
        [SouvenirQuestion("What was the {1} rotation in the {2} stage of {0}?", "UltraStores", TwoColumns4Answers, null, ExampleAnswers = new[] { "(XU, VY, WZ)", "(XY, VZ, UW)", "(XZ, YV, WU)", "(YX, UZ, VW)" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 2)]
        UltraStoresMultiRotation,

        [SouvenirQuestion("What was the {1} color in reading order used in the first stage of {0}?", "Uncolored Squares", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        UncoloredSquaresFirstStage,

        [SouvenirQuestion("What was the initial state of the switches in {0}?", "Uncolored Switches", ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings(5, 'Q', 'R')]
        UncoloredSwitchesInitialState,
        [SouvenirQuestion("What color was the {1} LED in reading order in {0}?", "Uncolored Switches", TwoColumns4Answers, "red", "green", "blue", "turquoise", "orange", "purple", "white", "black", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        UncoloredSwitchesLedColors,

        [SouvenirQuestion("What was the {1} received instruction in {0}?", "Unfair Cipher", ThreeColumns6Answers, "PCR", "PCG", "PCB", "SUB", "MIT", "CHK", "PRN", "BOB", "REP", "EAT", "STR", "IKE",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        UnfairCipherInstructions,

        [SouvenirQuestion("What was the {1} decrypted instruction in {0}?", "Unfair’s Revenge", ThreeColumns6Answers, "PCR", "PCG", "PCB", "SCC", "SCM", "SCY", "SUB", "MIT", "CHK", "PRN", "BOB", "REP", "EAT", "STR", "IKE", "SIG", "PVP", "NXP", "PVS", "NXS", "OPP",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        UnfairsRevengeInstructions,

        [SouvenirQuestion("What was the {1} submitted code in {0}?", "Unicode", ThreeColumns6Answers, "00A7", "00B6", "0126", "04D4", "017F", "01F6", "01F7", "2042", "037C", "03C2", "040B", "20AA", "042E", "0460", "046C", "20B0", "222F", "222B", "2569", "04EC", "260A", "04A6", "2626", "FB21", "0428", "03A9", "0583", "2592", "254B", "2318", "2234", "2205", "2104", "04A8", "2605", "019B", "03EA", "062A", "067C", "063A", "06BA", "00FE", "0194", "0239",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        UnicodeSortedAnswer,

        [SouvenirQuestion("What was the initial card in {0}?", "UNO!", OneColumn4Answers, "Red 0", "Red 1", "Red 2", "Red 3", "Red 4", "Red 5", "Red 6", "Red 7", "Red 8", "Red 9", "Red +2", "Red Skip", "Red Reverse", "Green 0", "Green 1", "Green 2", "Green 3", "Green 4", "Green 5", "Green 6", "Green 7", "Green 8", "Green 9", "Green +2", "Green Skip", "Green Reverse", "Yellow 0", "Yellow 1", "Yellow 2", "Yellow 3", "Yellow 4", "Yellow 5", "Yellow 6", "Yellow 7", "Yellow 8", "Yellow 9", "Yellow +2", "Yellow Skip", "Yellow Reverse", "Blue 0", "Blue 1", "Blue 2", "Blue 3", "Blue 4", "Blue 5", "Blue 6", "Blue 7", "Blue 8", "Blue 9", "Blue +2", "Blue Skip", "Blue Reverse", "+4", "Wild")]
        UnoInitialCard,

        [SouvenirQuestion("What was the {1} submitted letter in {0}?", "Unown Cipher", ThreeColumns6Answers,
            Type = AnswerType.UnownFont, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings('A', 'Z')]
        UnownCipherAnswers,

        [SouvenirQuestion("Which state was displayed in {0}?", "USA Cycle", TwoColumns4Answers,
            Type = AnswerType.Sprites, SpriteField = "USACycleSprites")]
        USACycleDisplayed,

        [SouvenirQuestion("Which state did you depart from in {0}?", "USA Maze", TwoColumns4Answers, "Alaska", "Alabama", "Arkansas", "Arizona", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming")]
        USAMazeOrigin,

        [SouvenirQuestion("Which word {1} shown in {0}?", "V", OneColumn4Answers, null,
        ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true }, ExampleAnswers = new[] { "Vacant", "Valorous", "Volition", "Vermin", "Vanity", "Visage", "Voracious", "Veers", "Vengeance", "Violation", "Vigilant", "Veteran", "Vanguarding", "Villain" })]
        VWords,

        [SouvenirQuestion("What was the initial state of {0}?", "Valves", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteField = "ValvesSprites")]
        ValvesInitialState,

        [SouvenirQuestion("What was the initially pressed color on {0}?", "Varicolored Squares", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true)]
        VaricoloredSquaresInitialColor,

        [SouvenirQuestion("What was the word of the {1} goal in {0}?", "Varicolour Flash", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "White",
            TranslateAnswers = true, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        VaricolourFlashWords,
        [SouvenirQuestion("What was the color of the {1} goal in {0}?", "Varicolour Flash", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "White",
            TranslateAnswers = true, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        VaricolourFlashColors,

        [SouvenirQuestion("What was the word in {0}?", "Vcrcs", TwoColumns4Answers, "destiny", "control", "refresh", "grouped", "wedging", "summary", "kitchen", "teacher", "concern", "section", "similar", "western", "dropper", "checker", "xeroses", "sunrise", "abolish", "harvest", "protest", "shallow", "plotted", "deafens", "colored", "aroused", "unsling", "holiday", "dictate", "dribble", "retreat", "episode", "crashed", "crazily", "silvers", "usurped", "witcher", "jealous", "village", "wizards", "prosper", "recycle", "pounced", "nonfood", "imblaze", "dryable", "swiftly", "mention", "rubbish", "realize", "collect", "surgeon", "gearbox", "schnozz", "passion", "freshen", "society", "passive", "archive", "shelter", "harmful", "freedom", "papayas", "thwarts", "railway", "teapots", "ravines", "density", "provide", "diagram", "lighter", "general", "upriver", "editors", "mingled", "ransoms", "prairie", "balance", "applied", "history", "calorie", "realism", "liquids", "validly", "varying", "wickers", "isolate", "falsify", "painter", "mixture", "bedroom", "dilemma", "skylike", "ranging", "simplex", "gallied", "missile", "posture", "highway", "prevent", "bracket", "project")]
        VcrcsWord,

        [SouvenirQuestion("What was the color of the {1} vector in {0}?", "Vectors", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
        ExampleExtraFormatArguments = new[] { "first", "second", "third", "only" }, ExampleExtraFormatArgumentGroupSize = 1)]
        VectorsColors,

        [SouvenirQuestion("What was the {1} flagpole color on {0}?", "Vexillology", ThreeColumns6Answers, "Red", "Orange", "Green", "Yellow", "Blue", "Aqua", "White", "Black", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        VexillologyColors,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Violet Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "DISMAY", "FRIDGE", "GALLON", "JAMMER", "KIDNEY", "RITUAL", "TRIPOD", "VIKING", "YEANED", "ZIPPER" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        VioletCipherScreen,

        [SouvenirQuestion("What was the desired color in the {1} stage on {0}?", "Visual Impairment", TwoColumns4Answers, "Blue", "Green", "Red", "White", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        VisualImpairmentColors,

        [SouvenirQuestion("What was the displayed sign in {0}?", "Warning Signs", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "WarningSignsSprites")]
        WarningSignsDisplayedSign,

        [SouvenirQuestion("What was the location displayed in {0}?", "WASD", ThreeColumns6Answers, "Bank", "Grocery", "School", "Gym", "Home", "Mall", "Cafe", "Park", "Office")]
        WasdDisplayedLocation,

        [SouvenirQuestion("What was the color on the {1} stage in {0}?", "Wavetapping", TwoColumns4Answers, "Red", "Orange", "Orange-Yellow", "Chartreuse", "Lime", "Green", "Seafoam Green", "Cyan-Green", "Turquoise", "Dark Blue", "Indigo", "Purple", "Purple-Magenta", "Magenta", "Pink", "Gray", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        WavetappingColors,
        [SouvenirQuestion("What was the correct pattern on the {1} stage in {0}?", "Wavetapping", ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "WavetappingSprites",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        WavetappingPatterns,

        [SouvenirQuestion("Who did you eliminate in {0}?", "Weakest Link", OneColumn4Answers, null, AddThe = true, ExampleAnswers = new[] { "Annie", "Albert", "Josephine", "Frederick" })]
        WeakestLinkElimination,
        [SouvenirQuestion("Who made it to the Money Phase with you in {0}?", "Weakest Link", OneColumn4Answers, null, AddThe = true, ExampleAnswers = new[] { "Annie", "Albert", "Josephine", "Frederick" })]
        WeakestLinkMoneyPhaseName,
        [SouvenirQuestion("What ratio did {1} get in the Question Phase in {0}?", "Weakest Link", OneColumn4Answers, null, AddThe = true,
            ExampleExtraFormatArguments = new[] { "Annie", "Albert", "Josephine", "Frederick" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("0-5", "/", "56")]
        WeakestLinkRatio,
        [SouvenirQuestion("What was {1}’s skill in {0}?", "Weakest Link", OneColumn4Answers, "Geography", "Language", "Wildlife", "Biology", "Maths", "KTANE", "History", "Other", AddThe = true, ExampleAnswers = new[] { "KTANE", "Geography", "Language", "Wildlife" },
            ExampleExtraFormatArguments = new[] { "Annie", "Albert", "Josephine", "Frederick" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WeakestLinkSkill,

        [SouvenirQuestion("What was the display text in the {1} stage of {0}?", "What’s on Second", ThreeColumns6Answers, "got it", "says", "display", "leed", "their", "blank", "right", "reed", "hold", "they are", "louder", "lead", "repeat", "ready", "none", "led", "ur", "you’re", "no", "you", "nothing", "middle", "done", "empty", "your", "hold on", "like", "read", "wait", "left", "press", "what?", "uh uh", "they’re", "uhhh", "c", "error", "you are", "next", "yes", "u", "sure", "okay", "what", "cee", "first", "see", "uh huh", "there", "red",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        WhatsOnSecondDisplayText,
        [SouvenirQuestion("What was the display text color in the {1} stage of {0}?", "What’s on Second", ThreeColumns6Answers, "Blue", "Cyan", "Green", "Magenta", "Red", "Yellow", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        WhatsOnSecondDisplayColor,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "White Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "ATTEND", "BREATH", "CRUNCH", "EFFECT", "JAILED", "JUMPER", "PLASMA", "UPROAR", "VERTEX", "VIEWED" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        WhiteCipherScreen,

        [SouvenirQuestion("What was the display in the {1} stage on {0}?", "WhoOF", ThreeColumns6Answers, "FIRST", "OKAY", "C", "BLANK", "YOU", "READ", "YOUR", "UR", "YES", "LED", "THEIR", "RED", "HIRE", "THERE", "THEY", "THING", "CEE", "LEED", "NO", "HOLD", "PLAY", "LEAD", "HARE", "HERE", " ", "REED", "SAYS", "SEE",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        WhoOFDisplay,

        [SouvenirQuestion("What was the display in the {1} stage on {0}?", "Who’s on First", ThreeColumns6Answers, "", "BLANK", "C", "CEE", "DISPLAY", "FIRST", "HOLD ON", "LEAD", "LED", "LEED", "NO", "NOTHING", "OK", "OKAY", "READ", "RED", "REED", "SAY", "SAYS", "SEE", "THEIR", "THERE", "THEY ARE", "THEY’RE", "U", "UR", "YES", "YOU", "YOU ARE", "YOU’RE", "YOUR",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        WhosOnFirstDisplay,

        [SouvenirQuestion("What word was transmitted in the {1} stage on {0}?", "Who’s on Morse", ThreeColumns6Answers, "SHELL", "HALLS", "SLICK", "TRICK", "BOXES", "LEAKS", "STROBE", "BISTRO", "FLICK", "BOMBS", "BREAK", "BRICK", "STEAK", "STING", "VECTOR", "BEATS", "CURSE", "NICE", "VERB", "NEARLY", "CREEK", "TRIBE", "CYBER", "CINEMA", "KOALA", "WATER", "WHISK", "MATTER", "KEYS", "STUCK",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        WhosOnMorseTransmitDisplay,

        [SouvenirQuestion("What was the color of the {1} dial in {0}?", "Wire", ThreeColumns6Answers, "blue", "green", "grey", "orange", "purple", "red", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        WireDialColors,
        [SouvenirQuestion("What was the displayed number in {0}?", "Wire", ThreeColumns6Answers, AddThe = true)]
        [AnswerGenerator.Integers(0, 9)]
        WireDisplayedNumber,

        [SouvenirQuestion("What color was the {1} display from the left in {0}?", "Wire Ordering", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", "white", "black", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        WireOrderingDisplayColor,
        [SouvenirQuestion("What number was on the {1} display from the left in {0}?", "Wire Ordering", TwoColumns4Answers, "1", "2", "3", "4",
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        WireOrderingDisplayNumber,
        [SouvenirQuestion("What color was the {1} wire from the left in {0}?", "Wire Ordering", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", "white", "black", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        WireOrderingWireColor,

        [SouvenirQuestion("How many {1} wires were there in {0}?", "Wire Sequence", TwoColumns4Answers, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "blue", "black" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        WireSequenceColorCount,

        [SouvenirQuestion("Which of these was {1} on {0}?", "Wolf, Goat, and Cabbage", ThreeColumns6Answers, "Cat", "Wolf", "Rabbit", "Berry", "Fish", "Dog", "Duck", "Goat", "Fox", "Grass", "Rice", "Mouse", "Bear", "Cabbage", "Chicken", "Goose", "Corn", "Carrot", "Horse", "Earthworm", "Kiwi", "Seeds",
            ExampleExtraFormatArguments = new[] { "present", "not present" }, ExampleExtraFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
        WolfGoatAndCabbageAnimals,
        [SouvenirQuestion("What was the boat size in {0}?", "Wolf, Goat, and Cabbage", ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 9)]
        WolfGoatAndCabbageBoatSize,

        [SouvenirQuestion("What was the label shown in {0}?", "Working Title", OneColumn4Answers, "foo", "foobar", "quuz", "garply", "plugh", "wibble", "flob", "fuga", "toto", "tutu", "eggs", "alice", "lorem ipsum", "widget", "eek", "bat", "haystack", "blarg", "kalaa", "sub", "momo", "change this", "hi", "thing", "xyz", "bar", "qux", "corge", "waldo", "xyzzy", "wobble", "hoge", "hogera", "tata", "spam", "raboof", "bob", "do stuff", "bla", "moof", "shme", "beekeeper", "dothestuff", "mum", "temp", "var", "placeholder", "hello", "stuff", "text", "baz", "quux", "grault", "fred", "thud", "wubble", "piyo", "hogehoge", "titi", "ham", "fruit", "john doe", "data", "gadget", "gleep", "needle", "blah", "grault", "puppu", "test", "change", "null", "hey", "something", "abc")]
        WorkingTitleLabel,

        [SouvenirQuestion("What was the color of the {1} flash in {0}?", "Xenocryst", ThreeColumns6Answers, ExampleAnswers = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo" },
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        Xenocryst,

        [SouvenirQuestion("What was the {1} displayed letter (in reading order) in {0}?", "XmORse Code", ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-Z")]
        XmORseCodeDisplayedLetters,
        [SouvenirQuestion("What word did you decrypt in {0}?", "XmORse Code", ThreeColumns6Answers, "ADMIT", "AWARD", "BANJO", "BRAVO", "CHILL", "CYCLE", "DECOR", "DISCO", "EERIE", "ERUPT", "FEWER", "FUZZY", "GERMS", "GUSTO", "HAULT", "HEXED", "ICHOR", "INFER", "JEWEL", "KTANE", "LADLE", "LYRIC", "MANGO", "MUTED", "NERDS", "NIXIE", "OOZED", "OXIDE", "PARTY", "PURSE", "QUEST", "RETRO", "ROUGH", "SCOWL", "SIXTH", "THANK", "TWINE", "UNBOX", "USHER", "VIBES", "VOICE", "WHIZZ", "WRUNG", "XENON", "YOLKS", "ZILCH")]
        XmORseCodeWord,

        [SouvenirQuestion("What song was played on {0}?", "xobekuJ ehT", OneColumn4Answers, null, ExampleAnswers = new[] { "Gimme Gimme Gimme", "Take On Me", "Barbie Girl", "Do I Wanna Know" })]
        XobekuJehTSong,

        [SouvenirQuestion("What was the initial roll on {0}?", "Yahtzee", TwoColumns4Answers, "Yahtzee", "large straight", "small straight", "four of a kind", "full house", "three of a kind", "two pairs", "pair", TranslateAnswers = true)]
        YahtzeeInitialRoll,

        [SouvenirQuestion("What was the starting row letter in {0}?", "Yellow Arrows", ThreeColumns6Answers, null)]
        [AnswerGenerator.Strings('A', 'Z')]
        YellowArrowsStartingRow,

        [SouvenirQuestion("What was the {1} color in {0}?", "Yellow Button", TwoColumns4Answers, "Red", "Yellow", "Green", "Cyan", "Blue", "Magenta", AddThe = true,
            ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1, TranslateAnswers = true)]
        YellowButtonColors,

        [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Yellow Cipher", TwoColumns4Answers, null, ExampleAnswers = new[] { "ALTHOUGH", "BUSINESS", "CHILDREN", "DIRECTOR", "EXCHANGE", "FUNCTION", "GUIDANCE", "HOSPITAL", "INDUSTRY", "JUNCTION", "KEYBOARD", "LANGUAGE", "MATERIAL", "NUMEROUS", "OFFERING", "POSSIBLE", "QUESTION", "RESEARCH", "SOFTWARE", "TOGETHER", "ULTIMATE", "VALUABLE", "WIRELESS", "XENOLITH", "YOURSELF", "ZUCCHINI" },
            ExampleExtraFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleExtraFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
        YellowCipherScreen,

        [SouvenirQuestion("What color was the {1} star in {0}?", "Zero, Zero", TwoColumns4Answers, "black", "blue", "green", "cyan", "red", "magenta", "yellow", "white", TranslateAnswers = true,
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ZeroZeroStarColors,
        [SouvenirQuestion("How many points were on the {1} star in {0}?", "Zero, Zero", ThreeColumns6Answers, "2", "3", "4", "5", "6", "7", "8",
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ZeroZeroStarPoints,
        [SouvenirQuestion("Where was the {1} square in {0}?", "Zero, Zero", ThreeColumns6Answers, null, Type = AnswerType.Grid, TranslateFormatArgs = new[] { true },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Grid(6, 6)]
        ZeroZeroSquares,

        [SouvenirQuestion("What was the {1} word in {0}?", "Zoni", OneColumn4Answers, null, ExampleAnswers = new[] { "angel", "thing", "dance", "heavy", "quote", "radio" },
            Type = AnswerType.DynamicFont, ExampleExtraFormatArguments = new[] { QandA.Ordinal }, ExampleExtraFormatArgumentGroupSize = 1)]
        ZoniWords
    }
}
