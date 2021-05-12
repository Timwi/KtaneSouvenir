namespace Souvenir
{
    public enum Question
    {
        [SouvenirQuestion("What was the {1} word shown in {0}?", "1000 Words", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "Baken", "Ghost", "Tolts", "Oyers", "Sweel", "Rangy", "Noses", "Chapt", "Phuts", "Pingo", "Hylas", "Podia", "Vizor" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        _1000WordsWords,

        [SouvenirQuestion("What was the {1} displayed letter in {0}?", "100 Levels of Defusal", AnswerLayout.ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        _100LevelsOfDefusalLetters,

        [SouvenirQuestion("What was {1} in {0}?", "1D Chess", AnswerLayout.ThreeColumns6Answers, "B a→c", "B b→d", "B c→a", "B c→e", "B d→b", "B d→f", "B e→c", "B e→g", "B f→d", "B f→h", "B g→e", "B g→i", "B h→f", "B i→g", "K a→b", "K b→a", "K b→c", "K c→b", "K c→d", "K d→c", "K d→e", "K e→d", "K e→f", "K f→e", "K f→g", "K g→f", "K g→h", "K h→g", "K h→i", "K i→h", "N a→c", "N b→d", "N c→a", "N c→e", "N d→b", "N d→f", "N e→c", "N e→g", "N f→d", "N f→h", "N g→e", "N g→i", "N h→f", "N i→g", "P a→b", "P a→c", "P b→a", "P b→c", "P b→d", "P c→a", "P c→b", "P c→d", "P c→e", "P d→b", "P d→c", "P d→e", "P d→f", "P e→c", "P e→d", "P e→f", "P e→g", "P f→d", "P f→e", "P f→g", "P f→h", "P g→e", "P g→f", "P g→h", "P g→i", "P h→f", "P h→g", "P i→g", "Q a→b", "Q a→c", "Q b→a", "Q b→c", "Q b→d", "Q c→a", "Q c→b", "Q c→d", "Q c→e", "Q d→b", "Q d→c", "Q d→e", "Q d→f", "Q e→c", "Q e→d", "Q e→f", "Q e→g", "Q f→d", "Q f→e", "Q f→g", "Q f→h", "Q g→e", "Q g→f", "Q g→h", "Q g→i", "Q h→f", "Q h→g", "Q i→g", "R a→b", "R b→a", "R b→c", "R c→b", "R c→d", "R d→c", "R d→e", "R e→d", "R e→f", "R f→e", "R f→g", "R g→f", "R g→h", "R h→g", "R h→i", "R i→h",
            ExampleExtraFormatArguments = new[] { "your first move", "Rustmate’s first move", "your second move", "Rustmate’s second move", "your third move", "Rustmate’s third move", "your fourth move", "Rustmate’s fourth move", "your fifth move", "Rustmate’s fifth move", "your sixth move", "Rustmate’s sixth move", "your seventh move", "Rustmate’s seventh move", "your eighth move", "Rustmate’s eighth move", }, ExampleExtraFormatArgumentGroupSize = 1)]
        _1DChessMoves,

        [SouvenirQuestion("What were the markings in {0}?", "3D Maze", AnswerLayout.ThreeColumns6Answers, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
        _3DMazeMarkings,

        [SouvenirQuestion("What was the cardinal direction in {0}?", "3D Maze", AnswerLayout.TwoColumns4Answers, "North", "South", "West", "East")]
        _3DMazeBearing,

        [SouvenirQuestion("What was the {1} goal node in {0}?", "3D Tunnels", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings("a-z.")]
        _3DTunnelsTargetNode,

        [SouvenirQuestion("What was the {1} channel’s initial value in {0}?", "7", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(-9, 9)]
        _7InitialValues,
        [SouvenirQuestion("What LED color was shown in stage {1} of {0}?", "7", AnswerLayout.TwoColumns4Answers,
            ExampleExtraFormatArguments = new[] { "1", "2", "3" }, ExampleExtraFormatArgumentGroupSize = 1, ExampleAnswers = new[] { "red", "blue", "green", "white" })]
        _7LedColors,

        [SouvenirQuestion("What was the background color on the {1} stage in {0}?", "Accumulation", AnswerLayout.ThreeColumns6Answers, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AccumulationBackgroundColor,
        [SouvenirQuestion("What was the border color in {0}?", "Accumulation", AnswerLayout.ThreeColumns6Answers, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow")]
        AccumulationBorderColor,

        [SouvenirQuestion("Which item was the {1} correct item you used in {0}?", "Adventure Game", AnswerLayout.TwoColumns4Answers, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGameCorrectItem,

        [SouvenirQuestion("What enemy were you fighting in {0}?", "Adventure Game", AnswerLayout.TwoColumns4Answers, "Dragon", "Demon", "Eagle", "Goblin", "Troll", "Wizard", "Golem", "Lizard")]
        AdventureGameEnemy,

        [SouvenirQuestion("What was the {1} in {0}?", "Affine Cycle", AnswerLayout.TwoColumns4Answers, "Advanced", "Addition", "Allocate", "Allotted", "Binaries", "Billions", "Bulkhead", "Bulwarks", "Ciphered", "Circuits", "Computer", "Compiler", "Decrypts", "Division", "Discover", "Discrete", "Encipher", "Entrance", "Equation", "Equalise", "Finished", "Findings", "Fortress", "Fortunes", "Gauntlet", "Gambling", "Gathered", "Gateways", "Hazarded", "Haziness", "Hunkered", "Hungrier", "Indicate", "Indigoes", "Illusion", "Illuding", "Jigsawed", "Jimmying", "Junction", "Juncture", "Kilowatt", "Kinetics", "Knockout", "Knowable", "Limiting", "Linearly", "Linkages", "Lingered", "Monogram", "Monotone", "Multiply", "Mulcting", "Nanogram", "Nanotube", "Numbered", "Numerate", "Octangle", "Octuples", "Observed", "Obstacle", "Progress", "Projects", "Position", "Positron", "Quadrant", "Quadrics", "Quickest", "Quitters", "Reversed", "Revolved", "Rotation", "Relative", "Starting", "Standard", "Stopping", "Stoccata", "Triggers", "Triangle", "Tomogram", "Tomorrow", "Underrun", "Underlie", "Ultimate", "Ultrahot", "Vicinity", "Viceless", "Voltages", "Voluming", "Wingding", "Winnable", "Whatever", "Whatsits", "Yellowed", "Yeasayer", "Yielders", "Yourself", "Zippered", "Zigzaggy", "Zugzwang", "Zymogene",
            ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AffineCycleWord,

        [SouvenirQuestion("Which letter was pressed in {0}?", "Alfa-Bravo", AnswerLayout.ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        AlfaBravoPressedLetter,
        [SouvenirQuestion("Which letter was to the left of the pressed one in {0}?", "Alfa-Bravo", AnswerLayout.ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        AlfaBravoLeftPressedLetter,
        [SouvenirQuestion("Which letter was to the right of the pressed one in {0}?", "Alfa-Bravo", AnswerLayout.ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        AlfaBravoRightPressedLetter,
        [SouvenirQuestion("What was the last digit on the small display in {0}?", "Alfa-Bravo", AnswerLayout.ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        AlfaBravoDigit,

        [SouvenirQuestion("What was the first equation in {0}?", "Algebra", AnswerLayout.TwoColumns4Answers, "a=3z", "a=5+y", "a=6-x", "a=7x", "a=8y", "a=9+z", "a=x/2", "a=x+1", "a=y/4", "a=y-2", "a=z/10", "a=z-7")]
        AlgebraEquation1,
        [SouvenirQuestion("What was the second equation in {0}?", "Algebra", AnswerLayout.TwoColumns4Answers, "b=(2x/10)-y", "b=(7x)y", "b=(x+y)-(z/2)", "b=(y/2)-z", "b=(zy)-(2x)", "b=(z-y)/2", "b=2(z+7)", "b=2z+7", "b=xy-(2+x)", "b=xyz")]
        AlgebraEquation2,

        [SouvenirQuestion("What was the letter displayed in the {1} stage of {0}?", "Alphabetical Ruling", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings(1, 'A', 'Z')]
        AlphabeticalRulingLetter,
        [SouvenirQuestion("What was the number displayed in the {1} stage of {0}?", "Alphabetical Ruling", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 9)]
        AlphabeticalRulingNumber,

        [SouvenirQuestion("What was the {1} letter shown during the cycle in {0}?", "Alphabet Tiles", AnswerLayout.ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AlphabetTilesCycle,
        [SouvenirQuestion("What was the missing letter in {0}?", "Alphabet Tiles", AnswerLayout.ThreeColumns6Answers, null)]
        AlphabetTilesMissingLetter,

        [SouvenirQuestion("What character was displayed on the {1} screen on the {2} in {0}?", "Alpha-Bits", AnswerLayout.ThreeColumns6Answers,
            ExampleAnswers = new[] { "0", "5", "A", "E", "G", "V" },
            ExampleExtraFormatArguments = new[] { "first", "left", "second", "left", "third", "left", "first", "right", "second", "right", "third", "right" }, ExampleExtraFormatArgumentGroupSize = 2)]
        AlphaBitsDisplayedCharacters,

        [SouvenirQuestion("What was the symbol on the submit button in {0}?", "Arithmelogic", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "ArithmelogicSprites")]
        ArithmelogicSubmit,
        [SouvenirQuestion("Which number was selectable, but not the solution, in the {1} screen on {0}?", "Arithmelogic", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "left", "middle", "right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(10, 40)]
        ArithmelogicNumbers,

        [SouvenirQuestion("What color was the {1} correct button in {0}?", "Bamboozled Again", AnswerLayout.TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainButtonColor,
        [SouvenirQuestion("What was the text on the {1} correct button in {0}?", "Bamboozled Again", AnswerLayout.TwoColumns4Answers, "THE LETTER", "ONE LETTER", "THE COLOUR", "ONE COLOUR", "THE PHRASE", "ONE PHRASE", "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "QUEBEC", "TANGO", "WHISKEY", "VICTOR", "YANKEE", "ECHO ECHO", "E THEN E", "ALPHA PAPA", "PAPA ALPHA", "PAPHA ALPA", "T GOLF", "TANGOLF", "WHISKEE", "WHISKY", "CHARLIE C", "C CHARLIE", "YANGO", "DELTA NEXT", "CUEBEQ", "MILO", "KI LO", "HI-LO", "VVICTOR", "VICTORR", "LIME BRAVO", "BLUE BRAVO", "G IN JADE", "G IN ROSE", "BLUE IN RED", "YES BUT NO", "COLOUR", "MESSAGE", "CIPHER", "BUTTON", "TWO BUTTONS", "SIX BUTTONS", "I GIVE UP", "ONE ELEVEN", "ONE ONE ONE", "THREE ONES", "WHAT?", "THIS?", "THAT?", "BLUE!", "ECHO!", "BLANK", "BLANK?!", "NOTHING", "YELLOW TEXT", "BLACK TEXT?", "QUOTE V", "END QUOTE", "\"QUOTE K\"", "IN RED", "ORANGE", "IN YELLOW", "LIME", "IN GREEN", "JADE", "IN CYAN", "AZURE", "IN BLUE", "VIOLET", "IN MAGENTA", "ROSE",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainButtonText,
        [SouvenirQuestion("What was the {1} decrypted text on the display in {0}?", "Bamboozled Again", AnswerLayout.TwoColumns4Answers, "THE LETTER", "ONE LETTER", "THE COLOUR", "ONE COLOUR", "THE PHRASE", "ONE PHRASE",
            ExampleExtraFormatArguments = new[] { "first", "third", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainDisplayTexts1,
        [SouvenirQuestion("What was the {1} decrypted text on the display in {0}?", "Bamboozled Again", AnswerLayout.TwoColumns4Answers, "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "QUEBEC", "TANGO", "WHISKEY", "VICTOR", "YANKEE", "ECHO ECHO", "E THEN E", "ALPHA PAPA", "PAPA ALPHA", "PAPHA ALPA", "T GOLF", "TANGOLF", "WHISKEE", "WHISKY", "CHARLIE C", "C CHARLIE", "YANGO", "DELTA NEXT", "CUEBEQ", "MILO", "KI LO", "HI-LO", "VVICTOR", "VICTORR", "LIME BRAVO", "BLUE BRAVO", "G IN JADE", "G IN ROSE", "BLUE IN RED", "YES BUT NO", "COLOUR", "MESSAGE", "CIPHER", "BUTTON", "TWO BUTTONS", "SIX BUTTONS", "I GIVE UP", "ONE ELEVEN", "ONE ONE ONE", "THREE ONES", "WHAT?", "THIS?", "THAT?", "BLUE!", "ECHO!", "BLANK", "BLANK?!", "NOTHING", "YELLOW TEXT", "BLACK TEXT?", "QUOTE V", "END QUOTE", "\"QUOTE K\"", "IN RED", "ORANGE", "IN YELLOW", "LIME", "IN GREEN", "JADE", "IN CYAN", "AZURE", "IN BLUE", "VIOLET", "IN MAGENTA", "ROSE",
            ExampleExtraFormatArguments = new[] { "6th", "7th", "8th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainDisplayTexts2,
        [SouvenirQuestion("What color was the {1} text on the display in {0}?", "Bamboozled Again", AnswerLayout.TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainDisplayColor,

        [SouvenirQuestion("What color was the button in the {1} stage of {0}?", "Bamboozling Button", AnswerLayout.TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black",
          ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozlingButtonColor,
        [SouvenirQuestion("What was the {2} label on the button in the {1} stage of {0}?", "Bamboozling Button", AnswerLayout.TwoColumns4Answers, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE",
          ExampleExtraFormatArguments = new[] { "first", "top", "first", "bottom", "second", "top", "second", "bottom" }, ExampleExtraFormatArgumentGroupSize = 2)]
        BamboozlingButtonLabel,
        [SouvenirQuestion("What was the {2} display in the {1} stage of {0}?", "Bamboozling Button", AnswerLayout.TwoColumns4Answers, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE",
          ExampleExtraFormatArguments = new[] { "first", "first", "first", "third", "first", "fourth", "first", "fifth" }, ExampleExtraFormatArgumentGroupSize = 2)]
        BamboozlingButtonDisplay,
        [SouvenirQuestion("What was the color of the {2} display in the {1} stage of {0}?", "Bamboozling Button", AnswerLayout.TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey",
          ExampleExtraFormatArguments = new[] { "first", "fourth", "first", "fifth", "second", "fourth", "second", "fifth" }, ExampleExtraFormatArgumentGroupSize = 2)]
        BamboozlingButtonDisplayColor,

        [SouvenirQuestion("Which ingredient was in the {1} position on {0}?", "Bartending", AnswerLayout.TwoColumns4Answers, "Adelhyde", "Flanergide", "Bronson Extract", "Karmotrine", "Powdered Delta",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BartendingIngredients,

        [SouvenirQuestion("What color was {1} in the solution to {0}?", "Big Circle", AnswerLayout.ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BigCircleColors,

        [SouvenirQuestion("At which numeric value did you cut the correct wire in {0}?", "Binary LEDs", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 31)]
        BinaryLEDsValue,

        [SouvenirQuestion("What was the {1} initial number in {0}?", "Binary Shift", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BinaryShiftInitialNumber,
        [SouvenirQuestion("What number was selected at stage {1} in {0}?", "Binary Shift", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "0", "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BinaryShiftSelectedNumberPossition,
        [SouvenirQuestion("What number was not selected at stage {1} in {0}?", "Binary Shift", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "0", "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BinaryShiftNotSelectedNumberPossition,

        [SouvenirQuestion("What word was displayed in {0}?", "Binary", AnswerLayout.ThreeColumns6Answers, null)]
        BinaryWord,

        [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", "Bitmaps", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(0, 16)]
        Bitmaps,

        [SouvenirQuestion("What was the answer in {0}?", "Black Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        BlackCipherAnswer,

        [SouvenirQuestion("What color was the {1} button in {0}?", "Blind Maze", AnswerLayout.TwoColumns4Answers, "Red", "Green", "Blue", "Gray", "Yellow",
            ExampleExtraFormatArguments = new[] { "north", "east", "west", "south" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BlindMazeColors,
        [SouvenirQuestion("Which maze did you solve {0} on?", "Blind Maze", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        BlindMazeMaze,

        [SouvenirQuestion("What was the last letter pressed on {0}?", "Blockbusters", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Strings('A', 'Z')]
        BlockbustersLastLetter,

        [SouvenirQuestion("What were the letters on the screen in {0}?", "Blue Arrows", AnswerLayout.ThreeColumns6Answers, "CA", "C1", "CB", "C8", "CF", "C4", "CE", "C6", "3A", "31", "3B", "38", "3F", "34", "3E", "36", "GA", "G1", "GB", "G8", "GF", "G4", "GE", "G6", "7A", "71", "7B", "78", "7F", "74", "7E", "76", "DA", "D1", "DB", "D8", "DF", "D4", "DE", "D6", "5A", "51", "5B", "58", "5F", "54", "5E", "56", "HA", "H1", "HB", "H8", "HF", "H4", "HE", "H6", "2A", "21", "2B", "28", "2F", "24", "2E", "26")]
        BlueArrowsInitialLetters,

        [SouvenirQuestion("What was the answer in {0}?", "Blue Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        BlueCipherAnswer,

        [SouvenirQuestion("What was the {1} indicator label in {0}?", "Bob Barks", AnswerLayout.ThreeColumns6Answers, "BOB", "CAR", "CLR", "IND", "FRK", "FRQ", "MSA", "NSA", "SIG", "SND", "TRN", "BUB", "DOG", "ETC", "KEY",
            ExampleExtraFormatArguments = new[] { "top left", "top right", "bottom left", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BobBarksIndicators,
        [SouvenirQuestion("Which button flashed {1} in sequence in {0}?", "Bob Barks", AnswerLayout.TwoColumns4Answers, "top left", "top right", "bottom left", "bottom right",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BobBarksPositions,

        [SouvenirQuestion("What letter was initially visible on {0}?", "Boggle", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "A", "E", "G", "M", "T", "W" })]
        BoggleLetters,

        [SouvenirQuestion("Which {1} appeared on {0}?", "Boxing", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander" },
            ExampleExtraFormatArguments = new[] { "contestant’s first name", "contestant’s last name", "substitute’s first name", "substitute’s last name" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BoxingNames,
        [SouvenirQuestion("What was the {1} of the contestant with strength rating {2} on {0}?", "Boxing", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander" },
            ExampleExtraFormatArguments = new[] { "first name", "0", "first name", "1", "first name", "2", "last name", "0", "last name", "1", "last name", "2", "substitute’s first name", "0", "substitute’s first name", "1", "substitute’s first name", "2", "substitute’s last name", "0", "substitute’s last name", "1", "substitute’s last name", "2" }, ExampleExtraFormatArgumentGroupSize = 2)]
        BoxingContestantByStrength,
        [SouvenirQuestion("What was {1}’s strength rating on {0}?", "Boxing", AnswerLayout.ThreeColumns6Answers, "0", "1", "2", "3", "4",
            ExampleExtraFormatArguments = new[] { "Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BoxingStrengthByContestant,

        [SouvenirQuestion("What was the solution word in {0}?", "Braille", AnswerLayout.ThreeColumns6Answers, "acting", "dating", "heading", "meaning", "server", "aiming", "dealer", "hearing", "miners", "shaking", "artist", "eating", "heating", "nearer", "sought", "asking", "eighth", "higher", "parish", "staying", "bearing", "farmer", "insist", "parker", "strands", "beating", "farming", "lasted", "parking", "strings", "beings", "faster", "laying", "paying", "teaching", "binding", "father", "leader", "powers", "tended", "bought", "finding", "leading", "pushed", "tender", "boxing", "finest", "leaned", "pushing", "testing", "breach", "finish", "leaning", "rather", "throwing", "breast", "flying", "leaving", "reaching", "towers", "breath", "foster", "linking", "reader", "vested", "breathe", "fought", "listed", "reading", "warned", "bringing", "gaining", "listen", "resting", "warning", "brings", "gather", "living", "riding", "weaker", "carers", "gazing", "making", "rushed", "wealth", "carter", "gender", "marked", "rushing", "winner", "charter", "growing", "marking", "saying", "winning", "crying", "headed", "master", "served", "winter")]
        BrailleWord,

        [SouvenirQuestion("What was the {1} correct button you pressed in {0}?", "Broken Buttons", AnswerLayout.ThreeColumns6Answers, "bomb", "blast", "boom", "burst", "wire", "button", "module", "light", "led", "switch", "RJ-45", "DVI-D", "RCA", "PS/2", "serial", "port", "row", "column", "one", "two", "three", "four", "five", "six", "seven", "eight", "size", "this", "that", "other", "submit", "abort", "drop", "thing", "blank", "broken", "too", "to", "yes", "see", "sea", "c", "wait", "word", "bob", "no", "not", "first", "hold", "late", "fail",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BrokenButtons,

        [SouvenirQuestion("What was the answer in {0}?", "Brown Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        BrownCipherAnswer,

        [SouvenirQuestion("What was the color of the middle contact point in {0}?", "Brush Strokes", AnswerLayout.ThreeColumns6Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Cyan", "Sky", "Blue", "Purple", "Magenta", "Brown", "White", "Gray", "Black", "Pink")]
        BrushStrokesMiddleColor,

        [SouvenirQuestion("What were the correct button presses in {0}?", "Bulb", AnswerLayout.ThreeColumns6Answers, "OOO", "OOI", "OIO", "OII", "IOO", "IOI", "IIO", "III", AddThe = true, Type = AnswerType.TicTacToeFont)]
        BulbButtonPresses,

        [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Burglar Alarm", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        BurglarAlarmDigits,

        [SouvenirQuestion("What color did the light glow in {0}?", "Button", AnswerLayout.TwoColumns4Answers, "red", "blue", "yellow", "white", AddThe = true)]
        ButtonLightColor,

        [SouvenirQuestion("How many of the buttons in {0} were {1}?", "Button Sequence", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "red", "blue", "yellow", "white" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 12)]
        ButtonSequencesColorOccurrences,

        [SouvenirQuestion("What was the {1} in {0}?", "Caesar Cycle", AnswerLayout.TwoColumns4Answers, "Advanced", "Addition", "Allocate", "Altering", "Binaries", "Billions", "Bulkhead", "Bulleted", "Ciphered", "Circuits", "Computer", "Continue", "Decrypts", "Division", "Discover", "Disposal", "Encipher", "Entrance", "Equation", "Equipped", "Finished", "Findings", "Fortress", "Forwards", "Gauntlet", "Gambling", "Gathered", "Glooming", "Hazarded", "Haziness", "Hunkered", "Huntsman", "Indicate", "Indigoes", "Illusion", "Illumine", "Jigsawed", "Jimmying", "Junction", "Judgment", "Kilowatt", "Kinetics", "Knockout", "Knuckled", "Limiting", "Linearly", "Linkages", "Labeling", "Monogram", "Monotone", "Multiply", "Mulligan", "Nanogram", "Nanotube", "Numbered", "Numerals", "Octangle", "Octuples", "Observed", "Obscured", "Progress", "Projects", "Position", "Positive", "Quadrant", "Quadrics", "Quickest", "Quintics", "Reversed", "Revolved", "Rotation", "Relation", "Starting", "Standard", "Stopping", "Stopword", "Triggers", "Triangle", "Toggling", "Together", "Underrun", "Underlie", "Ultimate", "Ultrared", "Vicinity", "Viceless", "Voltages", "Volatile", "Wingding", "Winnable", "Whatever", "Whatnots", "Yellowed", "Yeasayer", "Yielding", "Yourself", "Zippered", "Zigzaggy", "Zugzwang", "Zymogram",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CaesarCycleWord,

        [SouvenirQuestion("What was the LED color in {0}?", "Calendar", AnswerLayout.TwoColumns4Answers, "Green", "Yellow", "Red", "Blue")]
        CalendarLedColor,

        [SouvenirQuestion("What was the {1} submitted answer in {0}?", "Challenge & Contact", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "Accumulation", "Coffeebucks", "Perplexing", "Zoo", "Sunstone", "Bob" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChallengeAndContactAnswers,

        [SouvenirQuestion("What was the {1}paid amount in {0}?", "Cheap Checkout", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "", "first ", "second " }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(5, 50, "$0\".00\"")]
        CheapCheckoutPaid,

        [SouvenirQuestion("Which bird {1} present in {0}?", "Cheep Checkout", AnswerLayout.OneColumn4Answers, null,
        ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CheepCheckoutBirds,

        [SouvenirQuestion("What was the {1} coordinate in {0}?", "Chess", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("a-f", "1-6")]
        ChessCoordinate,

        [SouvenirQuestion("What color was the {1} LED in {0}?", "Chinese Counting", AnswerLayout.TwoColumns4Answers, "White", "Red", "Green", "Orange",
          ExampleExtraFormatArguments = new[] { "left", "right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChineseCountingLED,

        [SouvenirQuestion("Which note was part of the given chord in {0}?", "Chord Qualities", AnswerLayout.ThreeColumns6Answers, "A", "A♯", "B", "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯")]
        ChordQualitiesNotes,

        [SouvenirQuestion("What was the given chord quality in {0}?", "Chord Qualities", AnswerLayout.ThreeColumns6Answers, "7", "-7", "Δ7", "-Δ7", "7♯9", "ø", "add9", "-add9", "7♯5", "Δ7♯5", "7sus", "-Δ7♯5")]
        ChordQualitiesQuality,

        [SouvenirQuestion("What was the displayed number in {0}?", "Code", AnswerLayout.ThreeColumns6Answers, null, AddThe = true)]
        [AnswerGenerator.Integers(999, 9999)]
        CodeDisplayNumber,

        [SouvenirQuestion("Which of these words was submitted in {0}?", "Codenames", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "Hyperborean", "Weenus", "Melody", "King" })]
        CodenamesAnswers,

        [SouvenirQuestion("What was the last served coffee in {0}?", "Coffeebucks", AnswerLayout.OneColumn4Answers, "Twix Frappuccino", "The Blue Drink", "Matcha & Espresso Fusion", "Caramel Snickerdoodle Macchiato", "Liquid Cocaine", "S’mores Hot Chocolate", "The Pink Drink", "Grasshopper Frappuccino")]
        CoffeebucksCoffee,

        [SouvenirQuestion("Which coin was flipped in {0}?", "Coinage", AnswerLayout.ThreeColumns6Answers, ExampleAnswers = new[] { "e4", "h5", "d4", "h4", "c4", "h3", "c3", "g2", "f3", "h1", "f7" })]
        CoinageFlip,

        [SouvenirQuestion("What mangling was applied in {0}?", "Color Braille", AnswerLayout.OneColumn4Answers, "Top row shifted to the right", "Top row shifted to the left", "Middle row shifted to the right", "Middle row shifted to the left", "Bottom row shifted to the right", "Bottom row shifted to the left", "Each letter upside-down", "Each letter horizontally flipped", "Each letter vertically flipped", "Dots are inverted")]
        ColorBrailleMangling,
        [SouvenirQuestion("What was the {1} word in {0}?", "Color Braille", AnswerLayout.TwoColumns4Answers, ExampleAnswers = new[] { "advent", "barman", "carrying", "drowning", "holding", "landowner", "mandate", "narrowed", "remain", "shallow", "therefore", "western", "yield" },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorBrailleWords,

        [SouvenirQuestion("What was the {1}-stage indicator pattern in {0}?", "Color Decoding", AnswerLayout.TwoColumns4Answers, "Checkered", "Horizontal", "Vertical", "Solid",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorDecodingIndicatorPattern,
        [SouvenirQuestion("Which color {1} in the {2}-stage indicator pattern in {0}?", "Color Decoding", AnswerLayout.TwoColumns4Answers, "Green", "Purple", "Red", "Blue", "Yellow",
            ExampleExtraFormatArguments = new[] { "appeared", "first", "appeared", "second", "appeared", "third", "did not appear", "first", "did not appear", "second", "did not appear", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        ColorDecodingIndicatorColors,

        [SouvenirQuestion("What was the displayed word in {0}?", "Colored Keys", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "blue", "white" })]
        ColoredKeysDisplayWord,
        [SouvenirQuestion("What was the displayed word’s color in {0}?", "Colored Keys", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "blue", "white" })]
        ColoredKeysDisplayWordColor,
        [SouvenirQuestion("What was the color of the {1} key in {0}?", "Colored Keys", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "blue", "white" },
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColoredKeysKeyColor,
        [SouvenirQuestion("What letter was on the {1} key in {0}?", "Colored Keys", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings('A', 'Z')]
        ColoredKeysKeyLetter,

        [SouvenirQuestion("What was the first color group in {0}?", "Colored Squares", AnswerLayout.ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta")]
        ColoredSquaresFirstGroup,

        [SouvenirQuestion("What was the initial position of the switches in {0}?", "Colored Switches", AnswerLayout.ThreeColumns6Answers,
            Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings(5, 'Q', 'R')]
        ColoredSwitchesInitialPosition,
        [SouvenirQuestion("What was the position of the switches when the LEDs came on in {0}?", "Colored Switches", AnswerLayout.ThreeColumns6Answers,
            Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings(5, 'Q', 'R')]
        ColoredSwitchesWhenLEDsCameOn,

        [SouvenirQuestion("What was the color of the {1} LED in {0}?", "Color Morse", AnswerLayout.ThreeColumns6Answers, "Blue", "Green", "Orange", "Purple", "Red", "Yellow", "White",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorMorseColor,

        [SouvenirQuestion("What character was flashed by the {1} LED in {0}?", "Color Morse", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("0-9A-Z")]
        ColorMorseCharacter,

        [SouvenirQuestion("What was the submitted score in {0}?", "Colors Maximization", AnswerLayout.ThreeColumns6Answers)]
        ColorsMaximizationSubmittedScore,
        [SouvenirQuestion("What color was submitted as part of the solution in {0}?", "Colors Maximization", AnswerLayout.TwoColumns4Answers)]
        ColorsMaximizationSubmittedColor,
        [SouvenirQuestion("What color was not submitted as part of the solution in {0}?", "Colors Maximization", AnswerLayout.TwoColumns4Answers)]
        ColorsMaximizationNotSubmittedColor,
        [SouvenirQuestion("How many buttons were {1} in {0}?", "Colors Maximization", AnswerLayout.ThreeColumns6Answers, ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorsMaximizationColorCount,

        [SouvenirQuestion("What was the color of the last word in the sequence in {0}?", "Colour Flash", AnswerLayout.ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Magenta", "White")]
        ColourFlashLastColor,

        [SouvenirQuestion("What was the solution you selected first in {0}?", "Coordinates", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "[4,7]", "C4", "<0, 2>", "3, 1", "(6,2)", "B-1", "“1, 0”", "4/3", "[12]", "#23", "四十七" })]
        CoordinatesFirstSolution,

        [SouvenirQuestion("What was the grid size in {0}?", "Coordinates", AnswerLayout.ThreeColumns6Answers, "9", "15", "25", "21", "35", "49", "(9)", "(15)", "(21)", "(25)", "(35)", "(49)", "3 by 3", "4 by 3", "5 by 3", "6 by 3", "7 by 3", "3 by 4", "4 by 4", "5 by 4", "6 by 4", "7 by 4", "3 by 5", "4 by 5", "5 by 5", "6 by 5", "7 by 5", "3 by 6", "4 by 6", "5 by 6", "6 by 6", "7 by 6", "3 by 7", "4 by 7", "5 by 7", "6 by 7", "7 by 7", "9*3", "12*4", "15*5", "18*6", "21*7", "12*3", "16*4", "20*5", "24*6", "28*7", "15*3", "20*4", "25*5", "30*6", "35*7", "18*3", "24*4", "30*5", "36*6", "42*7", "21*3", "28*4", "35*5", "42*6", "49*7", "9 : 3", "12 : 3", "15 : 3", "18 : 3", "21 : 3", "12 : 4", "16 : 4", "20 : 4", "24 : 4", "28 : 4", "15 : 5", "20 : 5", "25 : 5", "30 : 5", "35 : 5", "18 : 6", "24 : 6", "30 : 6", "36 : 6", "42 : 6", "21 : 7", "28 : 7", "35 : 7", "42 : 7", "49 : 7", "3×3", "3×4", "3×5", "3×6", "3×7", "4×3", "4×4", "4×5", "4×6", "4×7", "5×3", "5×4", "5×5", "5×6", "5×7", "6×3", "6×4", "6×5", "6×6", "6×7", "7×3", "7×4", "7×5", "7×6", "7×7")]
        CoordinatesSize,

        [SouvenirQuestion("What was the color of the {1} corner in {0}?", "Corners", AnswerLayout.TwoColumns4Answers, "red", "green", "blue", "yellow",
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-right", "bottom-left" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CornersColors,
        [SouvenirQuestion("How many corners in {0} were {1}?", "Corners", AnswerLayout.ThreeColumns6Answers, "0", "1", "2", "3", "4",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue", "yellow" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CornersColorCount,

        [SouvenirQuestion("What was the number initially shown in {0}?", "Cosmic", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9999)]
        CosmicNumber,

        [SouvenirQuestion("What were the weather conditions on the {1} day in {0}?", "Creation", AnswerLayout.TwoColumns4Answers, "Clear", "Heat Wave", "Meteor Shower", "Rain", "Windy",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CreationWeather,

        [SouvenirQuestion("What was the {1} in {0}?", "Cryptic Cycle", AnswerLayout.TwoColumns4Answers, "Advanced", "Addition", "Allocate", "Altering", "Binaries", "Billions", "Bulkhead", "Bulleted", "Ciphered", "Circuits", "Computer", "Continue", "Decrypts", "Division", "Discover", "Disposal", "Examined", "Examples", "Equation", "Equipped", "Finished", "Findings", "Fortress", "Forwards", "Gauntlet", "Gambling", "Gathered", "Glooming", "Hazarded", "Haziness", "Hunkered", "Huntsman", "Indicate", "Indigoes", "Illusion", "Illumine", "Jigsawed", "Jimmying", "Junction", "Judgment", "Kilowatt", "Kinetics", "Knockout", "Knuckled", "Limiting", "Linearly", "Linkages", "Labeling", "Monogram", "Monotone", "Multiply", "Mulligan", "Nanogram", "Nanotube", "Numbered", "Numerals", "Octangle", "Octuples", "Observed", "Obscured", "Progress", "Projects", "Position", "Positive", "Quadrant", "Quadplex", "Quickest", "Quintics", "Reversed", "Revolved", "Rotation", "Relation", "Starting", "Standard", "Stopping", "Stopword", "Triggers", "Triangle", "Toggling", "Together", "Underrun", "Underlie", "Ultimate", "Ultrared", "Vicinity", "Viceless", "Voltages", "Volatile", "Wingding", "Winnable", "Whatever", "Whatnots", "Yellowed", "Yeasayer", "Yielding", "Yourself", "Zippered", "Zigzaggy", "Zugzwang", "Zymogram",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CrypticCycleWord,

        [SouvenirQuestion("What was the {1} cube rotation in {0}?", "Cube", AnswerLayout.TwoColumns4Answers, "rotate cw", "tip left", "tip backwards", "rotate ccw", "tip right", "tip forwards",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        CubeRotations,

        [SouvenirQuestion("Which region did you depart from in {0}?", "DACH Maze", AnswerLayout.OneColumn4Answers, "Burgenland, A", "Carinthia, A", "Lower Austria, A", "North Tyrol, A", "Upper Austria, A", "East Tyrol, A", "Salzburg, A", "Styria, A", "Vorarlberg, A", "Vienna, A", "Aargau, CH", "Appenzell Inner Rhodes, CH", "Appenzell Outer Rhodes, CH", "Basel Country, CH", "Bern, CH", "Basel City, CH", "Fribourg, CH", "Geneva, CH", "Glarus, CH", "Grisons, CH", "Jura, CH", "Luzern, CH", "Nidwalden, CH", "Neuchâtel, CH", "Obwalden, CH", "Schaffhausen, CH", "St. Gallen, CH", "Solothurn, CH", "Schwyz, CH", "Thurgau, CH", "Ticino, CH", "Uri, CH", "Vaud, CH", "Valais, CH", "Zug, CH", "Zürich, CH", "Brandenburg, D", "Berlin, D", "Baden-Württemberg, D", "Bavaria, D", "Bremen, D", "Hesse, D", "Hamburg, D", "Mecklenburg-Vorpommern, D", "Lower Saxony, D", "North Rhine-Westphalia, D", "Rhineland-Palatinate, D", "Schleswig-Holstein, D", "Saarland, D", "Saxony, D", "Saxony-Anhalt, D", "Thuringia, D", "Liechtenstein")]
        DACHMazeOrigin,

        [SouvenirQuestion("What was the shape generated in {0}?", "Deaf Alley", AnswerLayout.ThreeColumns6Answers, null)]
        DeafAlleyShape,

        [SouvenirQuestion("What deck did the first card of {0} belong to?", "Deck of Many Things", AnswerLayout.TwoColumns4Answers, "Standard", "Metropolitan", "Maritime", "Arctic", "Tropical", "Oasis", "Celestial", AddThe = true)]
        DeckOfManyThingsFirstCard,

        [SouvenirQuestion("What was the starting {1} defining color in {0}?", "Decolored Squares", AnswerLayout.ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta",
            ExampleExtraFormatArguments = new[] { "column", "row" }, ExampleExtraFormatArgumentGroupSize = 1)]
        DecoloredSquaresStartingPos,

        [SouvenirQuestion("What was {1}’s remembered position in {0}?", "Discolored Squares", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "Tiles4x4Sprites",
            ExampleExtraFormatArguments = new[] { "Blue", "Red", "Yellow", "Green", "Magenta" }, ExampleExtraFormatArgumentGroupSize = 1)]
        DiscoloredSquaresRememberedPositions,

        [SouvenirQuestion("What were the correct button presses in {0}?", "Divisible Numbers", AnswerLayout.OneColumn4Answers, "Nay, Nay, Nay", "Nay, Nay, Yea", "Nay, Yea, Nay", "Nay, Yea, Yea", "Yea, Nay, Nay", "Yea, Nay, Yea", "Yea, Yea, Nay", "Yea, Yea, Yea")]
        DivisibleNumbersAnswers,
        [SouvenirQuestion("What was the {1} stage’s number in {0}?", "Divisible Numbers", AnswerLayout.ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9999)]
        DivisibleNumbersNumbers,

        [SouvenirQuestion("What was the screen color on the {1} stage of {0}?", "Double Color", AnswerLayout.ThreeColumns6Answers, "Green", "Blue", "Red", "Pink", "Yellow",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        DoubleColorColors,

        [SouvenirQuestion("Which button was the submit button in {0}?", "Double-Oh", AnswerLayout.ThreeColumns6Answers, "↕", "⇕", "↔", "⇔", "◆")]
        DoubleOhSubmitButton,

        [SouvenirQuestion("Which of these symptoms was listed on {0}?", "Dr. Doctor", AnswerLayout.TwoColumns4Answers, "Bloating", "Chills", "Cold Hands", "Constipation", "Cough", "Diarrhea", "Disappearance of the Ears", "Dizziness", "Excessive Crying", "Fatigue", "Fever", "Foot swelling", "Gas", "Hallucination", "Headache", "Loss of Smell", "Muscle Cramp", "Nausea", "Numbness", "Shortness of Breath", "Sleepiness", "Thirstiness", "Throat irritation")]
        DrDoctorSymptoms,
        [SouvenirQuestion("Which of these diseases was listed on {0}, but not the one treated?", "Dr. Doctor", AnswerLayout.TwoColumns4Answers, "Alztimer’s", "Braintenance", "Color allergy", "Detonession", "Emojilepsy", "Foot and Morse", "Gout of Life", "HRV", "Indicitis", "Jaundry", "Keypad stones", "Legomania", "Microcontusion", "Narcolization", "OCd", "Piekinson’s", "Quackgrounds", "Royal Flu", "Seizure Siphor", "Tetrinus", "Urinary LEDs", "Verticode", "Widgeting", "XMAs", "Yes-no infection", "Zooties", "Chronic Talk", "Jukepox", "Neurolysis", "Perspective Loss", "Orientitis", "Huntington’s disease")]
        DrDoctorDiseases,

        [SouvenirQuestion("What was the decrypted word in {0}?", "Dreamcipher", AnswerLayout.OneColumn4Answers, null)]
        DreamcipherWord,

        [SouvenirQuestion("Which player {1} present in {0}?", "Dumb Waiters", AnswerLayout.OneColumn4Answers, null,
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        DumbWaitersPlayerAvailable,

        [SouvenirQuestion("What word was asked to be spelled in {0}?", "eeB gnillepS", AnswerLayout.TwoColumns4Answers, null)]
        eeBgnillepSWord,

        [SouvenirQuestion("What was the last digit on the small display in {0}?", "Eight", AnswerLayout.ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        EightLastSmallDisplayDigit,
        [SouvenirQuestion("What was the position of the last broken digit in {0}?", "Eight", AnswerLayout.ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8")]
        EightLastBrokenDigitPosition,
        [SouvenirQuestion("What were the last resulting digits in {0}?", "Eight", AnswerLayout.ThreeColumns6Answers)]
        EightLastResultingDigits,
        [SouvenirQuestion("What was the last displayed number in {0}?", "Eight", AnswerLayout.ThreeColumns6Answers)]
        EightLastDisplayedNumber,

        [SouvenirQuestion("What was the {1} rune shown on {0}?", "Elder Futhark", AnswerLayout.TwoColumns4Answers, "Algiz", "Ansuz", "Berkana", "Dagaz", "Ehwaz", "Eihwaz", "Fehu", "Gebo", "Hagalaz", "Isa", "Jera", "Kenaz", "Laguz", "Mannaz", "Nauthiz", "Othila", "Perthro", "Raido", "Sowulo", "Teiwaz", "Thurisaz", "Uruz", "Wunjo",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ElderFutharkRunes,

        [SouvenirQuestion("Which shape was the {1} operand in {0}?", "Encrypted Equations", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "EncryptedEquationsSprites",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        EncryptedEquationsShapes,

        [SouvenirQuestion("What method of encryption was used by {0}?", "Encrypted Hangman", AnswerLayout.OneColumn4Answers, "Caesar Cipher", "Atbash Cipher", "Rot-13 Cipher", "Affine Cipher", "Modern Cipher", "Vigenère Cipher", "Playfair Cipher")]
        EncryptedHangmanEncryptionMethod,
        [SouvenirQuestion("What module name was encrypted by {0}?", "Encrypted Hangman", AnswerLayout.OneColumn4Answers, ExampleAnswers = new[] { "Anagrams", "Word Scramble", "Two Bits", "Switches", "Lights Out", "Emoji Math", "Math", "Semaphore", "Piano Keys", "Colour Flash" })]
        EncryptedHangmanModule,

        [SouvenirQuestion("What was the {1} on {0}?", "Encrypted Morse", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "Detonate", "Ready Now", "Please No", "Cheesecake" },
            ExampleExtraFormatArguments = new[] { "received call", "sent response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        EncryptedMorseCallResponse,

        [SouvenirQuestion("What was the first encoding used in {0}?", "Encryption Bingo", AnswerLayout.OneColumn4Answers, ExampleAnswers = new[] { "Morse code", "Braille", "Semaphore", "Lombax" })]
        EncryptionBingoEncoding,

        [SouvenirQuestion("What was the displayed symbol in {0}?", "Equations X", AnswerLayout.ThreeColumns6Answers, "H(T)", "P", "\u03C7", "\u03C9", "Z(T)", "\u03C4", "\u03BC", "\u03B1", "K")]
        EquationsXSymbols,

        [SouvenirQuestion("What was the beat for the {1} arrow from the bottom in {0}?", "Etterna", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 32)]
        EtternaNumber,

        [SouvenirQuestion("What room did you start in in {0}?", "Factory Maze", AnswerLayout.OneColumn4Answers, "Bathroom", "Assembly Line", "Cafeteria", "Room A9", "Broom Closet", "Basement", "Copy Room", "Unnecessarily Long-Named Room", "Library", "Break Room", "Empty Room with Two Doors", "Arcade", "Classroom", "Module Testing Room", "Music Studio", "Computer Room", "Infirmary", "Bomb Room", "Space", "Storage Room", "Lounge", "Conference Room", "Kitchen", "Incinerator")]
        FactoryMazeStartRoom,

        [SouvenirQuestion("What was the last pair of letters in {0}?", "Fast Math", AnswerLayout.ThreeColumns6Answers, null)]
        FastMathLastLetters,

        [SouvenirQuestion("What was the exit coordinate in {0}?", "Faulty RGB Maze", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-G", "1-7")]
        FaultyRGBMazeExit,
        [SouvenirQuestion("Where was the {1} key in {0}?", "Faulty RGB Maze", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-G", "1-7")]
        FaultyRGBMazeKeys,
        [SouvenirQuestion("Which maze number was the {1} maze in {0}?", "Faulty RGB Maze", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("0-9a-f")]
        FaultyRGBMazeNumber,

        [SouvenirQuestion("What was the displayed number in {0}?", "Flags", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 7)]
        FlagsDisplayedNumber,
        [SouvenirQuestion("What was the main country flag in {0}?", "Flags", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "FlagsSprites")]
        FlagsMainCountry,
        [SouvenirQuestion("Which of these country flags was shown, but not the main country flag, in {0}?", "Flags", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "FlagsSprites")]
        FlagsCountries,

        [SouvenirQuestion("What number was displayed on {0}?", "Flashing Arrows", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 99)]
        FlashingArrowsDisplayedValue,
        [SouvenirQuestion("What color flashed {1} black on the relevant arrow in {0}?", "Flashing Arrows", AnswerLayout.ThreeColumns6Answers, ExampleAnswers = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White" },
            ExampleExtraFormatArguments = new[] { "before", "after" }, ExampleExtraFormatArgumentGroupSize = 1)]
        FlashingArrowsReferredArrow,

        [SouvenirQuestion("How many times did the {1} LED flash {2} on {0}?", "Flashing Lights", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "top", "blue", "top", "green", "top", "red", "top", "purple", "top", "orange", "bottom", "blue", "bottom", "green", "bottom", "red", "bottom", "purple", "bottom", "orange" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(0, 12)]
        FlashingLightsLEDFrequency,

        [SouvenirQuestion("What were the cylinders during stage {1} in {0}?", "Forget Any Color", AnswerLayout.OneColumn4Answers, null,
            ExampleAnswers = new[] { "Orange, Yellow, Green", "Yellow, Cyan, Purple", "Green, Purple, Orange" },
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetAnyColorCylinder,
        [SouvenirQuestion("What figure was used during stage {1} in {0}?", "Forget Any Color", AnswerLayout.ThreeColumns6Answers, "LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR",
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetAnyColorSequence,

        [SouvenirQuestion("What was the {1} digit of the answer in {0}?", "Forget’s Ultimate Showdown", AnswerLayout.ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetsUltimateShowdownAnswer,
        [SouvenirQuestion("What was the {1} digit of the initial number in {0}?", "Forget’s Ultimate Showdown", AnswerLayout.ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetsUltimateShowdownInitial,
        [SouvenirQuestion("What was the {1} digit of the bottom number in {0}?", "Forget’s Ultimate Showdown", AnswerLayout.ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetsUltimateShowdownBottom,
        [SouvenirQuestion("What was the {1} method used in {0}?", "Forget’s Ultimate Showdown", AnswerLayout.OneColumn4Answers, "Forget Me Not", "Simon’s Stages", "Forget Me Later", "Forget Infinity", "A>N<D", "Forget Me Now", "Forget Everything", "Forget Us Not",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetsUltimateShowdownMethod,

        [SouvenirQuestion("What number was on the gear during stage {1} in {0}?", "Forget the Colors", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetTheColorsGearNumber,
        [SouvenirQuestion("What number was on the large display during stage {1} in {0}?", "Forget the Colors", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 990)]
        ForgetTheColorsLargeDisplay,
        [SouvenirQuestion("What was the last decimal in the sine number received during stage {1} in {0}?", "Forget the Colors", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        ForgetTheColorsSineNumber,
        [SouvenirQuestion("What color was the gear during stage {1} in {0}?", "Forget the Colors", AnswerLayout.ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray",
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetTheColorsGearColor,
        [SouvenirQuestion("Which edgework-based rule was applied to the sum of nixies and gear during stage {1} in {0}?", "Forget the Colors", AnswerLayout.ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray",
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetTheColorsRuleColor,

        [SouvenirQuestion("What was the player token in {0}?", "Free Parking", AnswerLayout.ThreeColumns6Answers, "Dog", "Wheelbarrow", "Cat", "Iron", "Top Hat", "Car", "Battleship")]
        FreeParkingToken,

        [SouvenirQuestion("What was the last digit of your first query’s result in {0}?", "Functions", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        FunctionsLastDigit,
        [SouvenirQuestion("What number was to the left of the displayed letter in {0}?", "Functions", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 999)]
        FunctionsLeftNumber,
        [SouvenirQuestion("What letter was displayed in {0}?", "Functions", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Strings('A', 'Z')]
        FunctionsLetter,
        [SouvenirQuestion("What number was to the right of the displayed letter in {0}?", "Functions", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 999)]
        FunctionsRightNumber,

        [SouvenirQuestion("What were the numbers on {0}?", "Gamepad", AnswerLayout.ThreeColumns6Answers, null, AddThe = true)]
        [AnswerGenerator.Strings("2*0-9", ":", "2*0-9")]
        GamepadNumbers,

        [SouvenirQuestion("What was the answer in {0}?", "Gray Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        GrayCipherAnswer,

        [SouvenirQuestion("What was the {1} color in {0}?", "Great Void", AnswerLayout.ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "Cyan", "White", AddThe = true,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th", "6th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        GreatVoidColor,
        [SouvenirQuestion("What was the {1} digit in {0}?", "Great Void", AnswerLayout.ThreeColumns6Answers, null, AddThe = true,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th", "6th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        GreatVoidDigit,

        [SouvenirQuestion("What was the last number on the display on {0}?", "Green Arrows", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 99, "00")]
        GreenArrowsLastScreen,

        [SouvenirQuestion("What was the answer in {0}?", "Green Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        GreenCipherAnswer,

        [SouvenirQuestion("What was the starting location in {0}?", "Gridlock", AnswerLayout.ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "Tiles4x4Sprites")]
        GridLockStartingLocation,
        [SouvenirQuestion("What was the ending location in {0}?", "Gridlock", AnswerLayout.ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "Tiles4x4Sprites")]
        GridLockEndingLocation,
        [SouvenirQuestion("What was the starting color in {0}?", "Gridlock", AnswerLayout.TwoColumns4Answers, "Green", "Yellow", "Red", "Blue")]
        GridLockStartingColor,

        [SouvenirQuestion("What was the first item shown in {0}?", "Grocery Store", AnswerLayout.TwoColumns4Answers, null)]
        GroceryStoreFirstItem,

        [SouvenirQuestion("What was the gryphon’s name in {0}?", "Gryphons", AnswerLayout.ThreeColumns6Answers, "Gabe", "Gabriel", "Gad", "Gael", "Gage", "Gaia", "Galena", "Galina", "Gallo", "Gallagher", "Ganymede", "Ganzorig", "Garen", "Gareth", "Garland", "Garnett", "Garret", "Garrick", "Gary", "Gaspar", "Gaston", "Gauthier", "Gavin", "Gaz", "Geena", "Geff", "Geffrey", "Gela", "Geltrude", "Gene", "Geneva", "Genevieve", "Geno", "Gentius", "Geoff", "George", "Georgio", "Georgius", "Gerald", "Geraldo", "Gerda", "Gerel", "Gergana", "Gerhardt", "Gerhart", "Gerry", "Gertrude", "Gervais", "Gervaise", "Ghada", "Ghadir", "Ghassan", "Ghjulia", "Gia", "Giada", "Giampaolo", "Giampiero", "Giancarlo", "Giana", "Gianna", "Gideon", "Gidon", "Gilbert", "Gilberta", "Gino", "Giorgio", "Giovanni", "Giove", "Girish", "Girisha", "Gisela", "Giselle", "Gittel", "Gizella", "Gjorgji", "Gladys", "Glauco", "Glaukos", "Glen", "Glenn", "Godfrey", "Godfried", "Gojko", "Gol", "Golda", "Gona", "Gonzalo", "Gordie", "Gordy", "Goretti", "Gosia", "Gosse", "Gotzon", "Gotzone", "Gowri", "Gozzo", "Grace", "Gracia", "Griffith", "Gwynnyth")]
        GryphonsName,
        [SouvenirQuestion("What was the gryphon’s age in {0}?", "Gryphons", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(23, 34)]
        GryphonsAge,

        [SouvenirQuestion("Who was the person recalled in {0}?", "Guess Who?", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "Aaron", "Albin", "Andre" })]
        GuessWhoPerson,

        [SouvenirQuestion("What was the given number in {0}?", "Hereditary Base Notation", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "12", "33", "46", "112", "356" })]
        HereditaryBaseNotationInitialNumber,

        [SouvenirQuestion("What label was printed on {0}?", "Hexabutton", AnswerLayout.ThreeColumns6Answers, "Jump", "Boom", "Claim", "Button", "Hold", "Blue", AddThe = true)]
        HexabuttonLabel,

        [SouvenirQuestion("What was the color of the pawn in {0}?", "Hexamaze", AnswerLayout.ThreeColumns6Answers, "Red", "Yellow", "Green", "Cyan", "Blue", "Pink")]
        HexamazePawnColor,

        [SouvenirQuestion("What were the deciphered letters in {0}?", "hexOS", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Strings("2* A-Z")]
        HexOSCipher,
        [SouvenirQuestion("What was the deciphered phrase in {0}?", "hexOS", AnswerLayout.ThreeColumns6Answers, ExampleAnswers = new[] { "a maze", "someda", "but i ", "they h", "shorn o", "more s", "if onl", "grew b" })]
        HexOSOctCipher,
        [SouvenirQuestion("What was the {1} 3-digit number cycled by the screen in {0}?", "hexOS", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th", "6th", "7th", "8th", "9th", "10th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 999, "000")]
        HexOSScreen,
        [SouvenirQuestion("What were the rhythm values in {0}?", "hexOS", AnswerLayout.ThreeColumns6Answers, ExampleAnswers = new[] { "0001", "0012", "0123", "1230", "2300", "3000" })]
        HexOSSum,

        [SouvenirQuestion("What was the color of the main LED in {0}?", "Hidden Colors", AnswerLayout.ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Magenta", "White")]
        HiddenColorsLED,

        [SouvenirQuestion("What was the {1} in {0}?", "Hill Cycle", AnswerLayout.TwoColumns4Answers, "Adverted", "Advocate", "Allocate", "Altering", "Binormal", "Binomial", "Bulkhead", "Bulleted", "Ciphered", "Circuits", "Compiler", "Commando", "Decimate", "Deceived", "Discover", "Disposal", "Encipher", "Entrance", "Equators", "Equalise", "Finalise", "Finnicky", "Fortress", "Forwards", "Gauntlet", "Gambling", "Gatepost", "Gateways", "Hazarded", "Haziness", "Hungrier", "Huntress", "Incoming", "Indirect", "Illusion", "Illumine", "Jigsawed", "Jiggling", "Junction", "Junkyard", "Kilowatt", "Kilobyte", "Knockout", "Knocking", "Lingered", "Linearly", "Linkages", "Linkwork", "Monogram", "Monomial", "Multiply", "Multiton", "Nanogram", "Nanowatt", "Numerous", "Numerals", "Ordinals", "Ordering", "Observed", "Obscured", "Progress", "Projects", "Prophase", "Prophecy", "Quadrant", "Quadrics", "Quartile", "Quartics", "Reversed", "Revolved", "Rotators", "Relaying", "Stanzaic", "Standout", "Stopping", "Stopword", "Trigonal", "Trickier", "Toggling", "Together", "Underway", "Underlie", "Ultrahot", "Ultrared", "Vicinity", "Viceless", "Volition", "Volatile", "Whatness", "Whatsits", "Whatever", "Whatnots", "Yearlong", "Yeasayer", "Yokozuna", "Yourself", "Zippered", "Zygomata", "Zugzwang", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HillCycleWord,

        [SouvenirQuestion("Which House was {1} solved\u00a0for in {0}?", "Hogwarts", AnswerLayout.TwoColumns4Answers, "Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw",
            ExampleExtraFormatArguments = new[] { "Binary Puzzle", "Zoni", "Rock-Paper- Scissors-L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HogwartsHouse,
        [SouvenirQuestion("Which module was solved\u00a0for {1} in {0}?", "Hogwarts", AnswerLayout.OneColumn4Answers, null, ExampleAnswers = new[] { "Binary Puzzle", "Zoni", "Rock-Paper-Scissors-L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards" },
            ExampleExtraFormatArguments = new[] { "Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HogwartsModule,

        [SouvenirQuestion("What was the name of the {1} shadow shown in {0}?", "Hold Ups", AnswerLayout.OneColumn4Answers, "Mandrake", "Silky", "Koropokguru", "Nue", "Jack Frost", "Leanan Sidhe", "Hua Po", "Orthrus", "Lamia", "Bicorn", "Kelpie", "Apsaras", "Makami", "Nekomata", "Sandman", "Naga", "Agathion", "Berith", "Mokoi", "Inugami", "High Pixie", "Yaksini", "Anzu", "Take-Minakata", "Thoth", "Isis", "Incubis", "Onmoraki", "Koppa-Tengu", "Orobas", "Rakshasa", "Pixie", "Angel", "Jack O' Lantern", "Succubus", "Andras",
        ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HoldUpsShadows,

        [SouvenirQuestion("In what position was the button pressed on the {1} stage of {0}?", "Horrible Memory", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 6)]
        HorribleMemoryPositions,
        [SouvenirQuestion("What was the label of the button pressed on the {1} stage of {0}?", "Horrible Memory", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 6)]
        HorribleMemoryLabels,
        [SouvenirQuestion("What color was the button pressed on the {1} stage of {0}?", "Horrible Memory", AnswerLayout.ThreeColumns6Answers, "blue", "green", "red", "orange", "purple", "pink",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HorribleMemoryColors,

        [SouvenirQuestion("What was the {1} displayed phrase in {0}?", "Homophones", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth" }, ExampleExtraFormatArgumentGroupSize = 1,
            ExampleAnswers = new[] { "i", "C", "L", "1", "sees", "leemer", "aye-aye", "One" })]
        HomophonesDisplayedPhrases,

        [SouvenirQuestion("Which was a descriptor shown in {1} in {0}?", "Human Resources", AnswerLayout.TwoColumns4Answers, "Intellectual", "Deviser", "Confidant", "Helper", "Auditor", "Innovator", "Defender", "Chameleon", "Director", "Designer", "Educator", "Advocate", "Manager", "Showman", "Contributor", "Entertainer",
            ExampleExtraFormatArguments = new[] { "red", "green" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HumanResourcesDescriptors,
        [SouvenirQuestion("Who was {1} in {0}?", "Human Resources", AnswerLayout.ThreeColumns6Answers, "Rebecca", "Damian", "Jean", "Mike", "River", "Samuel", "Yoshi", "Caleb", "Ashley", "Tim", "Eliott", "Ursula", "Silas", "Noah", "Quinn", "Dylan",
            ExampleExtraFormatArguments = new[] { "fired", "hired" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HumanResourcesHiredFired,

        [SouvenirQuestion("Which of the first three stages of {0} had the {1} symbol {2}?", "Hunting", AnswerLayout.TwoColumns4Answers, "none", "first", "second", "first two", "third", "first & third", "second & third", "all three",
            ExampleExtraFormatArguments = new[] { "column", "first", "row", "first", "column", "second", "row", "second", "column", "third", "row", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        HuntingColumnsRows,

        [SouvenirQuestion("What was the {1} rotation in {0}?", "Hypercube", AnswerLayout.ThreeColumns6Answers, "XY", "YX", "XZ", "ZX", "XW", "WX", "YZ", "ZY", "YW", "WY", "ZW", "WZ", AddThe = true,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HypercubeRotations,

        [SouvenirQuestion("What was the {1} character of the hyperlink in {0}?", "Hyperlink", AnswerLayout.ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "_", "-", AddThe = true,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HyperlinkCharacters,
        [SouvenirQuestion("Which module was referenced on {0}?", "Hyperlink", AnswerLayout.OneColumn4Answers, "3D Maze", "Adjacent Letters", "Adventure Game", "Alphabet", "Anagrams", "Answering Questions", "Astrology", "Backgrounds", "Battleship", "Big Circle", "Bitmaps", "Blind Alley", "Blind Maze", "Braille", "Broken Buttons", "Burglar Alarm", "Button Sequence", "Caesar Cipher", "Capacitor Discharge", "Cheap Checkout", "Chess", "Chord Qualities", "Color Flash", "Colored Squares", "Colored Switches", "Combination Lock", "Complicated Buttons", "Complicated Wires", "Connection Check", "Cooking", "Coordinates", "Crazy Talk", "Creation", "Cryptography", "Double-Oh", "Emoji Math", "English Test", "European Travel", "Faulty Backgrounds", "Filibuster", "Follow The Leader", "Foreign Exchange Rates", "Forget Me Not", "Friendship", "Game Of Life Cruel", "Game Of Life Simple", "Hexamaze", "HTTP Response", "Hunting", "Ice Cream", "Keypad", "Knob", "Laundry", "Letter Keys", "Light Cycle", "Lights Out", "Listening", "Logic", "Math", "Maze", "Memory", "Microcontroller", "Module Against Humanity", "Monsplode Trading Cards", "Monsplode, Fight!", "Morse Code", "Morsematics", "Mortal Kombat", "Motion Sense", "Mouse In The Maze", "Murder", "Mystic Square", "Neutralization", "Number Pad", "Orientation Cube", "Password", "Perspective Pegs", "Piano Keys", "Plumbing", "Probing", "Resistors", "Rock-Paper-Scissors-Lizard-Spock", "Rotary Phone", "Round Keypad", "Safety Safe", "Sea Shells", "Semaphore", "Shape Shift", "Silly Slots", "Simon Says", "Simon Screams", "Simon States", "Skewed Slots", "Souvenir", "Square Button", "Switches", "Symbolic Coordinates", "Symbolic Password", "Tetris", "Text Field", "The Bulb", "The Button", "The Clock", "The Gamepad", "The iPhone", "The Moon", "The Stopwatch", "The Sun", "The Swan", "Third Base", "Tic-Tac-Toe", "Turn The Key", "Turn The Keys", "Two Bits", "Venting Gas", "Who's On First", "Who's That Monsplode", "Wire Placement", "Wire Sequence", "Wires", "Word Scramble", "Word Search", "Zoo", AddThe = true)]
        HyperlinkAnswer,

        [SouvenirQuestion("Which one of these flavours {1} to the {2} customer in {0}?", "Ice Cream", AnswerLayout.OneColumn4Answers, "Tutti Frutti", "Rocky Road", "Raspberry Ripple", "Double Chocolate", "Double Strawberry", "Cookies & Cream", "Neapolitan", "Mint Chocolate Chip", "The Classic", "Vanilla",
            ExampleExtraFormatArguments = new[] { "was on offer, but not sold,", "first", "was not on offer", "first", "was on offer, but not sold,", "second", "was not on offer", "second", "was on offer, but not sold,", "third", "was not on offer", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        IceCreamFlavour,
        [SouvenirQuestion("Who was the {1} customer in {0}?", "Ice Cream", AnswerLayout.ThreeColumns6Answers, "Mike", "Tim", "Tom", "Dave", "Adam", "Cheryl", "Sean", "Ashley", "Jessica", "Taylor", "Simon", "Sally", "Jade", "Sam", "Gary", "Victor", "George", "Jacob", "Pat", "Bob",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        IceCreamCustomer,

        [SouvenirQuestion("Which hair color {1} listed in {0}?", "Identity Parade", AnswerLayout.TwoColumns4Answers, "Black", "Blonde", "Brown", "Grey", "Red", "White",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        IdentityParadeHairColors,
        [SouvenirQuestion("Which build {1} listed in {0}?", "Identity Parade", AnswerLayout.TwoColumns4Answers, "Fat", "Hunched", "Muscular", "Short", "Slim", "Tall",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        IdentityParadeBuilds,
        [SouvenirQuestion("Which attire {1} listed in {0}?", "Identity Parade", AnswerLayout.TwoColumns4Answers, "Blazer", "Hoodie", "Jumper", "Suit", "T-shirt", "Tank top",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        IdentityParadeAttires,

        [SouvenirQuestion("What was the answer in {0}?", "Indigo Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        IndigoCipherAnswer,

        [SouvenirQuestion("What was the {1} PIN digit in {0}?", "iPhone", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        [AnswerGenerator.Integers(0, 9)]
        iPhoneDigits,

        [SouvenirQuestion("What number was wheel {1} in {0}?", "Jewel Vault", AnswerLayout.TwoColumns4Answers,
            ExampleExtraFormatArguments = new[] { "A", "B", "C", "D" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        [AnswerGenerator.Integers(1, 4)]
        JewelVaultWheels,

        [SouvenirQuestion("What was the {1} in {0}?", "Jumble Cycle", AnswerLayout.TwoColumns4Answers, "Adverted", "Advocate", "Allotype", "Allotted", "Binormal", "Binomial", "Bullhorn", "Bulwarks", "Connects", "Conquers", "Commando", "Compiler", "Deceived", "Decimate", "Dispatch", "Discrete", "Encrypts", "Encoding", "Equators", "Equalise", "Finalise", "Finnicky", "Formulae", "Fortunes", "Garrison", "Garnered", "Gatepost", "Gateways", "Hotlinks", "Hotheads", "Huntress", "Hundreds", "Incoming", "Indirect", "Illusory", "Illuding", "Journeys", "Jousting", "Junkyard", "Juncture", "Kilovolt", "Kilobyte", "Knocking", "Knowable", "Language", "Landmark", "Linkwork", "Lingered", "Monomial", "Monolith", "Multiton", "Mulcting", "Nanowatt", "Nanobots", "Numerous", "Numerate", "Ordering", "Ordinals", "Obstruct", "Obstacle", "Prophase", "Prophecy", "Postsync", "Positron", "Quartile", "Quartics", "Quirkish", "Quitters", "Reversed", "Revealed", "Relaying", "Relative", "Stanzaic", "Standout", "Stockade", "Stoccata", "Trigonal", "Trickier", "Tomogram", "Tomahawk", "Underway", "Undoings", "Ulterior", "Ultrahot", "Venomous", "Vendetta", "Volition", "Voluming", "Weakened", "Weaponed", "Whatness", "Whatsits", "Yearlong", "Yearning", "Yokozuna", "Yourself", "Zygomata", "Zygotene", "Zymology", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        JumbleCycleWord,

        [SouvenirQuestion("Which square was {1} in {0}?", "Kudosudoku", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "Tiles4x4Sprites",
            ExampleExtraFormatArguments = new[] { "pre-filled", "not pre-filled" }, ExampleExtraFormatArgumentGroupSize = 1)]
        KudosudokuPrefilled,

        [SouvenirQuestion("What was the number on the {1} hatch on {0}?", "Lasers", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 9)]
        LasersHatches,

        [SouvenirQuestion("What was the correct letter you pressed in the {1} stage of {0}?", "LED Encryption", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings('A', 'Z')]
        LEDEncryptionPressedLetters,

        [SouvenirQuestion("What color was {1} in {0}?", "LED Math", AnswerLayout.TwoColumns4Answers, "Red", "Blue", "Yellow", "Green",
            ExampleExtraFormatArguments = new[] { "LED A", "LED B", "the operator LED" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LEDMathLights,

        [SouvenirQuestion("What were the dimensions of the {1} piece in {0}?", "LEGOs", AnswerLayout.ThreeColumns6Answers, "2×2", "3×1", "3×2", "4×1", "4×2",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue", "cyan", "magenta", "yellow" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LEGOsPieceDimensions,

        [SouvenirQuestion("What was the {1} function in {0}?", "Linq", AnswerLayout.ThreeColumns6Answers, new[] { "First", "Last", "Min", "Max", "Distinct", "Skip", "SkipLast", "Take", "TakeLast", "ElementAt", "Except", "Intersect", "Concat", "Append", "Prepend" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th", "6th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LinqFunction,

        [SouvenirQuestion("What was the correct code you entered in {0}?", "Listening", AnswerLayout.ThreeColumns6Answers, "&&&**", "&$#$&", "$#$*&", "#$$**", "$#$#*", "**$*#", "#$$&*", "##*$*", "$#*$&", "**#**", "#&&*#", "&#**&", "$&**#", "&#$$#", "$&&**", "#&$##", "&*$*$", "&$$&*", "#&&&&", "**$$$", "*&*&&", "*#&*&", "**###", "&&$&*", "&$**&", "#$#&$", "&#&&#", "$$*$*", "$&#$$", "&**$$", "$&&*&", "&$&##", "#&$*&", "$*$**", "*#$&&", "###&$", "*$$&$", "$*&##", "#&$&&", "$&$$*", "*$*$*")]
        ListeningCode,

        [SouvenirQuestion("What was the color of the {1} button in the {2} stage of {0}?", "Logical Buttons", AnswerLayout.TwoColumns4Answers, "Red", "Blue", "Green", "Yellow", "Purple", "White", "Orange", "Cyan", "Grey",
            ExampleExtraFormatArguments = new[] { "top", "first", "bottom-left", "first", "bottom-right", "first", "top", "second", "bottom-left", "second", "bottom-right", "second", "top", "third", "bottom-left", "third", "bottom-right", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        LogicalButtonsColor,
        [SouvenirQuestion("What was the label on the {1} button in the {2} stage of {0}?", "Logical Buttons", AnswerLayout.TwoColumns4Answers, "Logic", "Color", "Label", "Button", "Wrong", "Boom", "No", "Wait", "Hmmm",
            ExampleExtraFormatArguments = new[] { "top", "first", "bottom-left", "first", "bottom-right", "first", "top", "second", "bottom-left", "second", "bottom-right", "second", "top", "third", "bottom-left", "third", "bottom-right", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        LogicalButtonsLabel,
        [SouvenirQuestion("What was the final operator in the {1} stage of {0}?", "Logical Buttons", AnswerLayout.ThreeColumns6Answers, "AND", "OR", "XOR", "NAND", "NOR", "XNOR",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LogicalButtonsOperator,

        [SouvenirQuestion("What was {1} in {0}?", "Logic Gates", AnswerLayout.ThreeColumns6Answers, "AND", "OR", "XOR", "NAND", "NOR", "XNOR",
            ExampleExtraFormatArguments = new[] { "gate A", "gate B", "gate C", "gate D", "gate E", "gate F", "gate G", "the duplicated gate" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LogicGatesGates,

        [SouvenirQuestion("What was the {1} letter on the button in {0}?", "Lombax Cubes", AnswerLayout.ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-Z")]
        LombaxCubesLetters,

        [SouvenirQuestion("Where did the {1} journey on {0} {2}?", "London Underground", AnswerLayout.OneColumn4Answers, null, AddThe = true, ExampleAnswers = new[] { "Great Portland Street", "High Street Kensington", "King's Cross St. Pancras", "Mornington Crescent", "Shepherd's Bush Market", "Tottenham Court Road", "Walthamstow Central", "White City/Wood Lane" },
            ExampleExtraFormatArguments = new[] { "first", "depart from", "second", "depart from", "first", "arrive to", "second", "arrive to" }, ExampleExtraFormatArgumentGroupSize = 2)]
        LondonUndergroundStations,

        [SouvenirQuestion("Which tile was part of the {1} matched pair in {0}?", "Mahjong", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "MahjongSprites",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MahjongMatches,
        [SouvenirQuestion("Which tile was shown in the bottom-left of {0}?", "Mahjong", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "MahjongSprites")]
        MahjongCountingTile,

        [SouvenirQuestion("Who was a player, but not the Godfather, in {0}?", "Mafia", AnswerLayout.ThreeColumns6Answers, "Rob", "Tim", "Mary", "Briane", "Hunter", "Macy", "John", "Will", "Lacy", "Claire", "Kenny", "Rick", "Walter", "Bonnie", "Luke", "Bill", "Sarah", "Larry", "Kate", "Stacy", "Diane", "Mac", "Jim", "Clyde", "Tommy", "Lenny", "Molly", "Benny", "Phil", "Bob", "Gary", "Ted", "Kim", "Nate", "Cher", "Ron", "Thomas", "Sam", "Duke", "Jack", "Ed", "Ronny", "Terry", "Claira", "Nick", "Cob", "Ash", "Don", "Jerry", "Simon")]
        MafiaPlayers,

        [SouvenirQuestion("What color was the text on the {1} button in {0}?", "M&Ms", AnswerLayout.ThreeColumns6Answers, "red", "green", "orange", "blue", "yellow", "brown",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MandMsColors,
        [SouvenirQuestion("What was the text on the {1} button in {0}?", "M&Ms", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings(5, 'M', 'N')]
        MandMsLabels,

        [SouvenirQuestion("What color was the text on the {1} button in {0}?", "M&Ns", AnswerLayout.ThreeColumns6Answers, "red", "green", "orange", "blue", "yellow", "brown",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MandNsColors,
        [SouvenirQuestion("What was the text on the correct button in {0}?", "M&Ns", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Strings(5, 'M', 'N')]
        MandNsLabel,

        [SouvenirQuestion("What bearing was signalled in {0}?", "Maritime Flags", AnswerLayout.ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 359)]
        MaritimeFlagsBearing,

        [SouvenirQuestion("Which callsign was signalled in {0}?", "Maritime Flags", AnswerLayout.TwoColumns4Answers, "1stmate", "2ndmate", "3rdmate", "abandon", "admiral", "advance", "aground", "allides", "anchors", "athwart", "azimuth", "bailers", "ballast", "barrack", "beached", "beacons", "beamend", "beamsea", "bearing", "beating", "belayed", "bermuda", "bobstay", "boilers", "bollard", "bonnets", "boomkin", "bounder", "bowline", "brailed", "breadth", "bridges", "brigged", "bringto", "bulwark", "bumboat", "bumpkin", "burthen", "caboose", "capsize", "capstan", "captain", "caravel", "careens", "carrack", "carrier", "catboat", "cathead", "chained", "channel", "charley", "charter", "citadel", "cleared", "cleated", "clinker", "clipper", "coaming", "coasted", "consort", "convoys", "corinth", "cotchel", "counter", "cranzes", "crewing", "cringle", "crojack", "cruiser", "cutters", "dandies", "deadrun", "debunks", "derrick", "dipping", "disrate", "dogvane", "doldrum", "dolphin", "draught", "drifter", "drogues", "drydock", "dunnage", "dunsels", "earings", "echelon", "embayed", "ensigns", "escorts", "fairway", "falkusa", "fantail", "fardage", "fathoms", "fenders", "ferries", "fitting", "flanked", "flaring", "flattop", "flemish", "floated", "floored", "flotsam", "folding", "follows", "forcing", "forward", "foulies", "founder", "framing", "freight", "frigate", "funnels", "furling", "galleon", "galleys", "galliot", "gangway", "garbled", "general", "georges", "ghosted", "ginpole", "giveway", "gondola", "graving", "gripies", "grounds", "growler", "guineas", "gundeck", "gunport", "gunwale", "halyard", "hammock", "hampers", "hangars", "harbors", "harbour", "hauling", "hawsers", "heading", "headsea", "heaving", "herring", "hogging", "holiday", "huffler", "inboard", "inirons", "inshore", "instays", "inwater", "inwayof", "jackies", "jacktar", "jennies", "jetties", "jiggers", "joggles", "jollies", "juryrig", "keelson", "kellets", "kicking", "killick", "kitchen", "lanyard", "laydays", "lazaret", "leehelm", "leeside", "leeward", "liberty", "lighter", "lizards", "loading", "lockers", "lofting", "lolling", "lookout", "lubbers", "luffing", "luggers", "lugsail", "maewest", "manowar", "marconi", "mariner", "matelot", "mizzens", "mooring", "mousing", "narrows", "nippers", "officer", "offpier", "oilskin", "oldsalt", "onboard", "oreboat", "outhaul", "outward", "painter", "panting", "parcels", "parleys", "parrels", "passage", "pelagic", "pendant", "pennant", "pickets", "pinnace", "pintles", "pirates", "pivoted", "pursers", "pursued", "quarter", "quaying", "rabbets", "ratline", "reduced", "reefers", "repairs", "rigging", "ripraps", "rompers", "rowlock", "rudders", "ruffles", "rummage", "sagging", "sailors", "salties", "salvors", "sampans", "sampson", "sculled", "scupper", "scuttle", "seacock", "sealing", "seekers", "serving", "sextant", "shelter", "shipped", "shiprig", "sickbay", "skipper", "skysail", "slinged", "slipway", "snagged", "snotter", "spliced", "splices", "sponson", "sponsor", "springs", "squares", "stackie", "standon", "starter", "station", "steamer", "steered", "steeves", "steward", "stopper", "stovein", "stowage", "strikes", "sunfish", "swimmie", "systems", "tacking", "thwarts", "tinclad", "tompion", "tonnage", "topmast", "topsail", "torpedo", "tossers", "trading", "traffic", "tramper", "transom", "trawler", "trenail", "trennel", "trimmer", "trooper", "trunnel", "tugboat", "turntwo", "unships", "upbound", "vessels", "voicing", "voyager", "weather", "whalers", "wharves", "whelkie", "whistle", "winches", "windage", "working", "yardarm")]
        MaritimeFlagsCallsign,

        [SouvenirQuestion("What was the answer in {0}?", "Mashematics", AnswerLayout.ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 99)]
        MashematicsAnswer,
        [SouvenirQuestion("What was the {1} number in the calculation in {0}?", "Mashematics", AnswerLayout.ThreeColumns6Answers, null, ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 99)]
        MashematicsCalculation,

        [SouvenirQuestion("Which word was part of the latest access code in {0}?", "Matrix", AnswerLayout.TwoColumns4Answers, null, AddThe = true)]
        MatrixAccessCode,
        [SouvenirQuestion("What was the glitched word in {0}?", "Matrix", AnswerLayout.TwoColumns4Answers, null, AddThe = true)]
        MatrixGlitchWord,

        [SouvenirQuestion("In which {1} was the starting position in {0}, counting from the {2}?", "Maze", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "column", "left", "row", "top" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(1, 6)]
        MazeStartingPosition,

        [SouvenirQuestion("What was the color of the starting face in {0}?", "Maze³", AnswerLayout.ThreeColumns6Answers, "Red", "Blue", "Yellow", "Green", "Magenta", "Orange")]
        Maze3StartingFace,

        [SouvenirQuestion("Which was the {1} value in {0}?", "Mazematics", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "30", "42", "51" },
            ExampleExtraFormatArguments = new[] { "initial", "goal" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MazematicsValue,

        [SouvenirQuestion("What was the starting position on {0}?", "Maze Scrambler", AnswerLayout.TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        MazeScramblerStart,
        [SouvenirQuestion("What was the goal on {0}?", "Maze Scrambler", AnswerLayout.TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        MazeScramblerGoal,
        [SouvenirQuestion("Which of these positions was a maze marking on {0}?", "Maze Scrambler", AnswerLayout.TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        MazeScramblerIndicators,

        [SouvenirQuestion("Who was the master shown in {0}?", "Mega Man 2", AnswerLayout.TwoColumns4Answers, "Cold Man", "Magma Man", "Dust Man", "Sword Man", "Splash Woman", "Ice Man", "Quick Man", "Hard Man", "Pharaoh Man", "Charge Man", "Pirate Man", "Pump Man", "Galaxy Man", "Grenade Man", "Snake Man", "Burst Man", "Cut Man", "Air Man", "Magnet Man", "Toad Man", "Gyro Man", "Tomahawk Man", "Wood Man", "Strike Man", "Blade Man", "Aqua Man", "Shade Man", "Flash Man", "Flame Man", "Concrete Man", "Metal Man", "Needle Man", "Wave Man", "Knight Man", "Slash Man", "Shadow Man", "Sheep Man", "Ground Man", "Wind Man", "Fire Man", "Stone Man", "Tengu Man", "Bright Man", "Centaur Man", "Cloud Man", "Frost Man", "Dynamo Man", "Chill Man", "Turbo Man", "Napalm Man", "Jewel Man", "Drill Man", "Freeze Man", "Blizzard Man", "Gravity Man", "Junk Man", "Clown Man", "Hornet Man", "Skull Man", "Solar Man", "Commando Man", "Yamato Man", "Dive Man", "Search Man", "Gemini Man", "Bubble Man", "Guts Man", "Tornado Man", "Astro Man", "Plug Man", "Elec Man", "Crystal Man", "Nitro Man", "Burner Man", "Spark Man", "Spring Man", "Plant Man", "Star Man", "Ring Man", "Top Man", "Crash Man", "Bomb Man", "Heat Man", "Magic Man")]
        MegaMan2SelectedMaster,
        [SouvenirQuestion("Whose weapon was shown in {0}?", "Mega Man 2", AnswerLayout.TwoColumns4Answers, "Cold Man", "Magma Man", "Dust Man", "Sword Man", "Splash Woman", "Ice Man", "Quick Man", "Hard Man", "Pharaoh Man", "Charge Man", "Pirate Man", "Pump Man", "Galaxy Man", "Grenade Man", "Snake Man", "Burst Man", "Cut Man", "Air Man", "Magnet Man", "Toad Man", "Gyro Man", "Tomahawk Man", "Wood Man", "Strike Man", "Blade Man", "Aqua Man", "Shade Man", "Flash Man", "Flame Man", "Concrete Man", "Metal Man", "Needle Man", "Wave Man", "Knight Man", "Slash Man", "Shadow Man", "Sheep Man", "Ground Man", "Wind Man", "Fire Man", "Stone Man", "Tengu Man", "Bright Man", "Centaur Man", "Cloud Man", "Frost Man", "Dynamo Man", "Chill Man", "Turbo Man", "Napalm Man", "Jewel Man", "Drill Man", "Freeze Man", "Blizzard Man", "Gravity Man", "Junk Man", "Clown Man", "Hornet Man", "Skull Man", "Solar Man", "Commando Man", "Yamato Man", "Dive Man", "Search Man", "Gemini Man", "Bubble Man", "Guts Man", "Tornado Man", "Astro Man", "Plug Man", "Elec Man", "Crystal Man", "Nitro Man", "Burner Man", "Spark Man", "Spring Man", "Plant Man", "Star Man", "Ring Man", "Top Man", "Crash Man", "Bomb Man", "Heat Man", "Magic Man")]
        MegaMan2SelectedWeapon,

        [SouvenirQuestion("Which part was in slot #{1} at the start of {0}?", "Melody Sequencer", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 8)]
        MelodySequencerSlots,
        [SouvenirQuestion("Which slot contained part #{1} at the start of {0}?", "Melody Sequencer", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 8)]
        MelodySequencerParts,

        [SouvenirQuestion("What was the {1} correct symbol pressed in {0}?", "Memorable Buttons", AnswerLayout.ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "J", "K", "L", "P", "Q",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.DynamicFont)]
        MemorableButtonsSymbols,

        [SouvenirQuestion("What was the displayed number in the {1} stage of {0}?", "Memory", AnswerLayout.TwoColumns4Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 4)]
        MemoryDisplay,
        [SouvenirQuestion("In what position was the button that you pressed in the {1} stage of {0}?", "Memory", AnswerLayout.TwoColumns4Answers, null, Type = AnswerType.Sprites, SpriteField = "MemorySprites",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MemoryPosition,
        [SouvenirQuestion("What was the label of the button that you pressed in the {1} stage of {0}?", "Memory", AnswerLayout.TwoColumns4Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 4)]
        MemoryLabel,

        [SouvenirQuestion("Which pin lit up {1} in {0}?", "Microcontroller", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(1, 10)]
        MicrocontrollerPinOrder,

        [SouvenirQuestion("What was the color of the starting cell in {0}?", "Minesweeper", AnswerLayout.ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", "black")]
        MinesweeperStartingColor,

        [SouvenirQuestion("What was the decrypted word of the {1} stage in {0}?", "Modern Cipher", AnswerLayout.ThreeColumns6Answers, "Absent", "Abstract", "Abysmal", "Accident", "Activate", "Adjacent", "Afraid", "Agenda", "Agony", "Alchemy", "Alcohol", "Alive", "Allergic", "Allergy", "Alpha", "Alphabet", "Already", "Amethyst", "Amnesty", "Amperage", "Ancient", "Animals", "Animate", "Anthrax", "Anxious", "Aquarium", "Aquarius", "Arcade", "Arrange", "Arrow", "Artefact", "Asterisk", "Atrophy", "Audio", "Author", "Avoid", "Awesome", "Balance", "Banana", "Bandit", "Bankrupt", "Basket", "Battle", "Bazaar", "Beard", "Beauty", "Beaver", "Becoming", "Beetle", "Beseech", "Between", "Bicycle", "Bigger", "Biggest", "Biology", "Birthday", "Bistro", "Bites", "Blight", "Blockade", "Blubber", "Bomb", "Bonobo", "Books", "Bottle", "Brazil", "Brief", "Broccoli", "Broken", "Brother", "Bubble", "Budget", "Bulkhead", "Bumper", "Bunny", "Button", "Bytes", "Cables", "Caliber", "Campaign", "Canada", "Canister", "Caption", "Caution", "Cavity", "Chalk", "Chamber", "Chamfer", "Champion", "Changes", "Chicken", "Children", "Chlorine", "Chord", "Chronic", "Church", "Cinnamon", "Civic", "Cleric", "Clock", "Cocoon", "Combat", "Combine", "Comedy", "Comics", "Comma", "Command", "Comment", "Compost", "Computer", "Condom", "Conflict", "Consider", "Contour", "Control", "Corrupt", "Costume", "Criminal", "Crunch", "Cryptic", "Cuboid", "Cypher", "Daddy", "Dancer", "Dancing", "Daughter", "Dead", "Decapod", "Decay", "Decoy", "Defeat", "Defuser", "Degree", "Delay", "Demigod", "Dentist", "Desert", "Design", "Desire", "Dessert", "Detail", "Develop", "Device", "Diamond", "Dictate", "Diffuse", "Dilemma", "Dingy", "Dinosaur", "Disease", "Disgust", "Document", "Doubled", "Doubt", "Downbeat", "Dragon", "Drawer", "Dream", "Drink", "Drunken", "Dungeon", "Dynasty", "Dyslexia", "Eclipse", "Eldritch", "Email", "Emulator", "Encrypt", "England", "Enlist", "Enough", "Ensure", "Equality", "Equation", "Eruption", "Eternity", "Euphoria", "Exact", "Exclaim", "Exhaust", "Expert", "Expertly", "Explain", "Explodes", "Fabric", "Factory", "Faded", "Faint", "Fair", "False", "Falter", "Famous", "Fantasy", "Farm", "Father", "Faucet", "Faulty", "Fearsome", "Feast", "February", "Feint", "Festival", "Fiction", "Fighter", "Figure", "Finish", "Fireman", "Firework", "First", "Fixture", "Flagrant", "Flagship", "Flamingo", "Flesh", "Flipper", "Fluorine", "Flush", "Foreign", "Forensic", "Fractal", "Fragrant", "France", "Frantic", "Freak", "Friction", "Friday", "Friendly", "Frighten", "Furor", "Fused", "Garage", "Genes", "Genetic", "Genius", "Gentle", "Glacier", "Glitch", "Goat", "Golden", "Granular", "Graphics", "Graphite", "Grateful", "Gridlock", "Ground", "Guitar", "Gumption", "Halogen", "Harmony", "Hawk", "Headache", "Heard", "Hedgehog", "Heinous", "Herd", "Heretic", "Hexagon", "Hiccup", "Highway", "Holiday", "Home", "Homesick", "Honest", "Horror", "Horse", "House", "Huge", "Humanity", "Hungry", "Hydrogen", "Hysteria", "Imagine", "Industry", "Infamous", "Inside", "Integral", "Interest", "Ironclad", "Issue", "Italic", "Italy", "Itch", "Jaundice", "Jeans", "Jeopardy", "Joyful", "Joystick", "Juice", "Juncture", "Jungle", "Junkyard", "Justice", "Keep", "Keyboard", "Kilobyte", "Kilogram", "Kingdom", "Kitchen", "Kitten", "Knife", "Krypton", "Ladylike", "Language", "Large", "Laughter", "Launch", "Leaders", "Learn", "Leave", "Leopard", "Level", "Liberal", "Liberty", "Lifeboat", "Ligament", "Light", "Liquid", "Listen", "Little", "Lobster", "Logical", "Love", "Lucky", "Lulled", "Lunatic", "Lurks", "Machine", "Madam", "Magnetic", "Manager", "Manual", "Marina", "Marine", "Martian", "Master", "Matrix", "Measure", "Meaty", "Meddle", "Medical", "Mental", "Menu", "Meow", "Merchant", "Message", "Messes", "Metal", "Method", "Mettle", "Militant", "Minim", "Minimum", "Miracle", "Mirror", "Misjudge", "Misplace", "Misses", "Mistake", "Mixture", "Mnemonic", "Mobile", "Modern", "Modest", "Module", "Moist", "Money", "Morning", "Most", "Mother", "Movies", "Multiple", "Munch", "Musical", "Mustache", "Mystery", "Mystic", "Mystique", "Mythic", "Narcotic", "Nasty", "Nature", "Navigate", "Network", "Neutral", "Nobelium", "Nobody", "Noise", "Notice", "Noun", "Nuclear", "Numeral", "Nutrient", "Nymph", "Obelisk", "Obstacle", "Obvious", "Octopus", "Offset", "Omega", "Opaque", "Opinion", "Orange", "Organic", "Ouch", "Outbreak", "Outdo", "Overcast", "Overlaps", "Package", "Padlock", "Pancake", "Panda", "Panic", "Paper", "Papers", "Parent", "Park", "Particle", "Passive", "Patented", "Pathetic", "Patient", "Peace", "Peasant", "Penalty", "Pencil", "Penguin", "Perfect", "Person", "Persuade", "Perusing", "Phone", "Physical", "Piano", "Picture", "Piglet", "Pilfer", "Pillage", "Pinch", "Pirate", "Pitcher", "Pizza", "Plane", "Planet", "Platonic", "Player", "Please", "Plucky", "Plunder", "Plurals", "Pocket", "Police", "Portrait", "Potato", "Potently", "Pounce", "Poverty", "Practice", "Predict", "Prefect", "Premium", "Present", "Prince", "Printer", "Prison", "Profit", "Promise", "Prophet", "Protein", "Province", "Psalm", "Psychic", "Puddle", "Punchbag", "Pungent", "Punish", "Purchase", "Quagmire", "Qualify", "Quantify", "Quantize", "Quarter", "Querying", "Queue", "Quiche", "Quick", "Rabbit", "Racoon", "Radar", "Radical", "Rainbow", "Random", "Rattle", "Ravenous", "Reason", "Rebuke", "Refine", "Regular", "Reindeer", "Request", "Resort", "Respect", "Retire", "Revolt", "Reward", "Rhapsody", "Rhenium", "Rhodium", "Rhomboid", "Rhyme", "Rhythm", "Ridicule", "Roadwork", "Roar", "Roast", "Room", "Rooster", "Roster", "Rotor", "Rotunda", "Royal", "Ruler", "Rural", "Sailor", "Sainted", "Sales", "Sally", "Satisfy", "Saunter", "Scale", "Scandal", "Schedule", "School", "Science", "Scratch", "Screen", "Sensible", "Separate", "Serious", "Several", "Shampoo", "Shares", "Shelter", "Shift", "Ship", "Shirt", "Shiver", "Shorten", "Showcase", "Shuffle", "Silent", "Similar", "Sister", "Sixth", "Sixty", "Skater", "Skyward", "Slander", "Slayer", "Sleek", "Slipper", "Smart", "Smeared", "Soccer", "Society", "Source", "Spain", "Spare", "Spark", "Spatula", "Speaker", "Special", "Spectate", "Spectrum", "Spicy", "Spinach", "Spiral", "Splendid", "Splinter", "Sprayed", "Spread", "Spring", "Squadron", "Squander", "Squash", "Squib", "Squid", "Squish", "Stake", "Stalking", "Steak", "Steam", "Sticker", "Stinky", "Stocking", "Stone", "Store", "Stormy", "Strange", "Strike", "Stutter", "Subway", "Suffer", "Supreme", "Surf", "Surplus", "Survey", "Switch", "Symbol", "System", "Systemic", "Table", "Tadpole", "Talking", "Tangle", "Tank", "Tapeworm", "Target", "Tarot", "Teach", "Teamwork", "Terminal", "Terminus", "Terror", "Testify", "Their", "There", "Thick", "Thief", "Think", "Throat", "Through", "Thunder", "Thyme", "Ticket", "Time", "Toaster", "Tomato", "Tone", "Torque", "Tortoise", "Touchy", "Toupe", "Tower", "Transfix", "Transit", "Trash", "Trauma", "Treason", "Treasure", "Trick", "Tripod", "Trouble", "Truck", "Trumpet", "Turtle", "Twinkle", "Ugly", "Ultra", "Umbrella", "Underway", "Unique", "Unknown", "Unsteady", "Untoward", "Unwashed", "Upgrade", "Urban", "Used", "Useless", "Utopia", "Vacuum", "Vampire", "Vanish", "Vanquish", "Various", "Vast", "Velocity", "Vendor", "Verb", "Verbatim", "Verdict", "Vexation", "Vicious", "Victim", "Victory", "Video", "View", "Viking", "Village", "Violent", "Violin", "Virulent", "Visceral", "Vision", "Volatile", "Voltage", "Vortex", "Vulgar", "Warden", "Warlock", "Warning", "Wealth", "Weapon", "Wedding", "Weight", "Whack", "Wharf", "What", "When", "Whisk", "Whistle", "Wicked", "Window", "Winter", "Witness", "Wizard", "Wrench", "Wretch", "Wrinkle", "Writer", "Xanthous", "Yacht", "Yarn", "Yawn", "Yeah", "Yearlong", "Yearn", "Yeoman", "Yodel", "Yoga", "Yonder", "Youngest", "Yourself", "Zealot", "Zebra", "Zenith", "Zither", "Zodiac", "Zombie",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ModernCipherWord,

        [SouvenirQuestion("Which module did the sound played by the {1} button belong to in {0}?", "Module Listening", AnswerLayout.OneColumn4Answers, ExampleAnswers = new[] { "Zoni", "Lucky Dice", "Qwirkle", "Battleship" },
            ExampleExtraFormatArguments = new[] { "red", "green", "blue", "yellow" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ModuleListeningSounds,

        [SouvenirQuestion("Which of the following was the starting icon for {0}?", "Module Maze", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites)]
        ModuleMazeStartingIcon,

        [SouvenirQuestion("Which creature was displayed in {0}?", "Monsplode, Fight!", AnswerLayout.TwoColumns4Answers, "Caadarim", "Buhar", "Melbor", "Lanaluff", "Bob", "Mountoise", "Aluga", "Nibs", "Zapra", "Zenlad", "Vellarim", "Ukkens", "Lugirit", "Flaurim", "Myrchat", "Clondar", "Gloorim", "Docsplode", "Magmy", "Pouse", "Asteran", "Violan", "Percy", "Cutie Pie")]
        MonsplodeFightCreature,
        [SouvenirQuestion("Which one of these moves {1} selectable in {0}?", "Monsplode, Fight!", AnswerLayout.TwoColumns4Answers, "Tic", "Tac", "Toe", "Hollow Gaze", "Splash", "Heavy Rain", "Fountain", "Candle", "Torchlight", "Flame Spear", "Tangle", "Grass Blade", "Ivy Spikes", "Spectre", "Boo", "Battery Power", "Zap", "Double Zap", "Shock", "High Voltage", "Dark Portal", "Last Word", "Void", "Boom", "Fiery Soul", "Stretch", "Shrink", "Appearify", "Sendify", "Freak Out", "Glyph", "Bug Spray", "Bedrock", "Earthquake", "Cave In", "Toxic Waste", "Venom Fang", "Countdown", "Finale", "Sidestep",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MonsplodeFightMove,

        [SouvenirQuestion("What was the {1} before the last action in {0}?", "Monsplode Trading Cards", AnswerLayout.TwoColumns4Answers, "Aluga", "Asteran", "Bob", "Buhar", "Caadarim", "Clondar", "Cutie Pie", "Docsplode", "Flaurim", "Gloorim", "Lanaluff", "Lugirit", "Magmy", "Melbor", "Mountoise", "Myrchat", "Nibs", "Percy", "Pouse", "Ukkens", "Vellarim", "Violan", "Zapra", "Zenlad", "Aluga, The Fighter", "Bob, The Ancestor", "Buhar, The Protector", "Melbor, The Web Bug",
            ExampleExtraFormatArguments = new[] { "first card in your hand", "second card in your hand", "third card in your hand", "card on offer" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MonsplodeTradingCardsCards,
        [SouvenirQuestion("What was the print version of the {1} before the last action in {0}?", "Monsplode Trading Cards", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first card in your hand", "second card in your hand", "third card in your hand", "card on offer" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-J", "1-9")]
        MonsplodeTradingCardsPrintVersions,

        [SouvenirQuestion("What was the {1} set in clockwise order in {0}?", "Moon", AnswerLayout.TwoColumns4Answers, "south", "south-west", "west", "north-west", "north", "north-east", "east", "south-east",
            ExampleExtraFormatArguments = new[] { "first initially lit", "second initially lit", "third initially lit", "fourth initially lit", "first initially unlit", "second initially unlit", "third initially unlit", "fourth initially unlit" },
            ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        MoonLitUnlit,

        [SouvenirQuestion("What was the flashing word in {0}?", "More Code", AnswerLayout.TwoColumns4Answers, "Allocate", "Bulwarks", "Compiler", "Disposal", "Encipher", "Formulae", "Gauntlet", "Hunkered", "Illusory", "Jousting", "Kinetics", "Linkwork", "Monolith", "Nanobots", "Octangle", "Postsync", "Quartics", "Revolved", "Stanzaic", "Tomahawk", "Ultrahot", "Vendetta", "Wafflers", "Yokozuna", "Zugzwang", "Allotype", "Bulkhead", "Computer", "Dispatch", "Encrypts", "Fortunes", "Gateways", "Huntress", "Illusion", "Junction", "Kilobyte", "Linkages", "Monogram", "Nanogram", "Octuples", "Positron", "Quintics", "Revealed", "Stoccata", "Tomogram", "Ultrared", "Venomous", "Weakened", "Xenolith", "Yeasayer", "Zymogram",
            ExampleAnswers = new[] { "Allocate", "Bulwarks", "Compiler", "Disposal", "Encipher", "Formulae", "Gauntlet", "Hunkered", "Illusory", "Jousting", "Kinetics", "Linkwork" })]
        MoreCodeWord,

        [SouvenirQuestion("What was the starting location in {0}?", "Morse-A-Maze", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-F", "1-6")]
        MorseAMazeStartingCoordinate,

        [SouvenirQuestion("What was the ending location in {0}?", "Morse-A-Maze", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-F", "1-6")]
        MorseAMazeEndingCoordinate,

        [SouvenirQuestion("What was the word shown as Morse code in {0}?", "Morse-A-Maze", AnswerLayout.ThreeColumns6Answers, null,
            ExampleAnswers = new[] { "couch", "strobe", "smoke", "assay", "monkey", "glass", "starts", "strode", "office", "essays", "couple", "bosses" })]
        MorseAMazeMorseCodeWord,

        [SouvenirQuestion("What was the character flashed by the {1} button in {0}?", "Morse Buttons", AnswerLayout.ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-Z0-9")]
        MorseButtonsButtonLabel,
        [SouvenirQuestion("What was the color flashed by the {1} button in {0}?", "Morse Buttons", AnswerLayout.ThreeColumns6Answers, "red", "blue", "green", "yellow", "orange", "purple",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MorseButtonsButtonColor,

        [SouvenirQuestion("What was the {1} received letter in {0}?", "Morsematics", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings('A', 'Z')]
        MorsematicsReceivedLetters,

        [SouvenirQuestion("What were the LEDs in the {1} row in {0} (1\u00a0=\u00a0on, 0\u00a0=\u00a0off)?", "Morse War", AnswerLayout.ThreeColumns6Answers, "1100", "1010", "1001", "0110", "0101", "0011",
            ExampleExtraFormatArguments = new[] { "bottom", "middle", "top" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MorseWarLeds,
        [SouvenirQuestion("What code was transmitted in {0}?", "Morse War", AnswerLayout.ThreeColumns6Answers, "ABR", "RBS", "SVR", "ZUX", "ZAQ", "MOI", "OPA", "VZQ", "XRP", "OLL", "AIR", "RHG", "MJN", "VTT", "XZS", "SUN")]
        MorseWarCode,

        [SouvenirQuestion("What color was the torus in {0}?", "Mouse in the Maze", AnswerLayout.TwoColumns4Answers, "white", "green", "blue", "yellow")]
        MouseInTheMazeTorus,

        [SouvenirQuestion("Which color sphere was the goal in {0}?", "Mouse in the Maze", AnswerLayout.TwoColumns4Answers, "white", "green", "blue", "yellow")]
        MouseInTheMazeSphere,

        [SouvenirQuestion("Where was the body found in {0}?", "Murder", AnswerLayout.TwoColumns4Answers, "Dining Room", "Study", "Kitchen", "Lounge", "Billiard Room", "Conservatory", "Ballroom", "Hall", "Library")]
        MurderBodyFound,

        [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", AnswerLayout.TwoColumns4Answers, "Miss Scarlett", "Professor Plum", "Mrs Peacock", "Reverend Green", "Colonel Mustard", "Mrs White",
            ExampleExtraFormatArguments = new[] { "a suspect, but not the murderer,", "not a suspect" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MurderSuspect,

        [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", AnswerLayout.TwoColumns4Answers, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner",
            ExampleExtraFormatArguments = new[] { "a potential weapon, but not the murder weapon,", "not a potential weapon" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MurderWeapon,

        [SouvenirQuestion("Which module was the first requested to be solved by {0}?", "Mystery Module", AnswerLayout.OneColumn4Answers, null, ExampleAnswers = new[] { "Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox", "Rock-Paper-Scissors-L.-Sp." })]
        MysteryModuleFirstKey,
        [SouvenirQuestion("Which module was hidden by {0}?", "Mystery Module", AnswerLayout.OneColumn4Answers, null, ExampleAnswers = new[] { "Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox" })]
        MysteryModuleHiddenModule,

        [SouvenirQuestion("Where was the skull in {0}?", "Mystic Square", AnswerLayout.TwoColumns4Answers, "top left", "top middle", "top right", "middle left", "center", "middle right", "bottom left", "bottom middle", "bottom right")]
        MysticSquareSkull,

        [SouvenirQuestion("What was the chapter number of the {1} page in {0}?", "Necronomicon", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "1", "24", "36" }, AddThe = true,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth", "seventh" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NecronomiconChapters,

        [SouvenirQuestion("In base 10, what was the value submitted in {0}?", "Negativity", AnswerLayout.ThreeColumns6Answers, ExampleAnswers = new[] { "0", "9990", "-9990", "-1234", "5678", "-90" })]
        NegativitySubmittedValue,
        [SouvenirQuestion("Excluding 0s, what was the submitted balanced ternary in {0}?", "Negativity", AnswerLayout.TwoColumns4Answers, ExampleAnswers = new[] { "+-", "-++", "++-+-", "++++-", "-----", "+-----++++" })]
        NegativitySubmittedTernary,

        [SouvenirQuestion("What was the acid’s color in {0}?", "Neutralization", AnswerLayout.TwoColumns4Answers, "Yellow", "Green", "Red", "Blue")]
        NeutralizationColor,
        [SouvenirQuestion("What was the acid’s volume in {0}?", "Neutralization", AnswerLayout.TwoColumns4Answers, "5", "10", "15", "20")]
        NeutralizationVolume,

        [SouvenirQuestion("What was the label of the correct button in {0}?", "N&Ms", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Strings(5, 'M', 'N')]
        NandMsAnswer,

        [SouvenirQuestion("What was the initial middle digit in {0}?", "Navinums", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 9)]
        NavinumsMiddleDigit,
        [SouvenirQuestion("What was the {1} directional button pressed in {0}?", "Navinums", AnswerLayout.TwoColumns4Answers, "up", "left", "right", "down",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NavinumsDirectionalButtons,

        [SouvenirQuestion("What color flashed {1} in the final sequence in {0}?", "Not Keypad", AnswerLayout.ThreeColumns6Answers, "red", "orange", "yellow", "green", "cyan", "blue", "purple", "magenta", "pink", "brown", "grey", "white",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotKeypadColor,
        [SouvenirQuestion("Which symbol was on the button that flashed {1} in the final sequence in {0}?", "Not Keypad", AnswerLayout.TwoColumns4Answers, Type = AnswerType.Sprites, SpriteField = "KeypadSprites",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotKeypadSymbol,

        [SouvenirQuestion("What was the starting distance in {0}?", "Not Maze", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 9)]
        NotMazeStartingDistance,

        [SouvenirQuestion("What was the {1} correct word you submitted in {0}?", "Not Morse Code", AnswerLayout.ThreeColumns6Answers, ExampleAnswers = new[] { "shelf", "pounds", "sister", "beef", "yeast", "drive" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotMorseCodeWord,

        [SouvenirQuestion("Which maze was used in {0}?", "Not Simaze", AnswerLayout.ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple")]
        NotSimazeMaze,
        [SouvenirQuestion("What was the starting position in {0}?", "Not Simaze", AnswerLayout.TwoColumns4Answers, "(red, red)", "(red, orange)", "(red, yellow)", "(red, green)", "(red, blue)", "(red, purple)", "(orange, red)", "(orange, orange)", "(orange, yellow)", "(orange, green)", "(orange, blue)", "(orange, purple)", "(yellow, red)", "(yellow, orange)", "(yellow, yellow)", "(yellow, green)", "(yellow, blue)", "(yellow, purple)", "(green, red)", "(green, orange)", "(green, yellow)", "(green, green)", "(green, blue)", "(green, purple)", "(blue, red)", "(blue, orange)", "(blue, yellow)", "(blue, green)", "(blue, blue)", "(blue, purple)", "(purple, red)", "(purple, orange)", "(purple, yellow)", "(purple, green)", "(purple, blue)", "(purple, purple)")]
        NotSimazeStart,
        [SouvenirQuestion("What was the goal position in {0}?", "Not Simaze", AnswerLayout.TwoColumns4Answers)]
        [AnswerGenerator.Inherit(NotSimazeStart)]
        NotSimazeGoal,

        [SouvenirQuestion("What colors did the light glow in {0}?", "Not the Button", AnswerLayout.ThreeColumns6Answers, "white", "red", "yellow", "green", "blue", "white/red", "white/yellow", "white/green", "white/blue", "red/yellow", "red/green", "red/blue", "yellow/green", "yellow/blue", "green/blue")]
        NotTheButtonLightColor,

        [SouvenirQuestion("In which position was the button you pressed in the {1} stage on {0}?", "Not Who's on First", AnswerLayout.TwoColumns4Answers, "top left", "top right", "middle left", "middle right", "bottom left", "bottom right",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstPressedPosition,
        [SouvenirQuestion("What was the label on the button you pressed in the {1} stage on {0}?", "Not Who's on First", AnswerLayout.ThreeColumns6Answers, "BLANK", "DONE", "FIRST", "HOLD", "LEFT", "LIKE", "MIDDLE", "NEXT", "NO", "NOTHING", "OKAY", "PRESS", "READY", "RIGHT", "SURE", "U", "UH HUH", "UH UH", "UHHH", "UR", "WAIT", "WHAT", "WHAT?", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstPressedLabel,
        [SouvenirQuestion("In which position was the reference button in the {1} stage on {0}?", "Not Who's on First", AnswerLayout.TwoColumns4Answers, "top left", "top right", "middle left", "middle right", "bottom left", "bottom right",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstReferencePosition,
        [SouvenirQuestion("What was the label on the reference button in the {1} stage on {0}?", "Not Who's on First", AnswerLayout.ThreeColumns6Answers, "BLANK", "DONE", "FIRST", "HOLD", "LEFT", "LIKE", "MIDDLE", "NEXT", "NO", "NOTHING", "OKAY", "PRESS", "READY", "RIGHT", "SURE", "U", "UH HUH", "UH UH", "UHHH", "UR", "WAIT", "WHAT", "WHAT?", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstReferenceLabel,
        [SouvenirQuestion("What was the calculated number in the second stage on {0}?", "Not Who's on First", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 60)]
        NotWhosOnFirstSum,

        [SouvenirQuestion("Which number was correctly pressed on {0}?", "Numbered Buttons", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 100)]
        NumberedButtonsButtons,

        [SouvenirQuestion("What two-digit number was given in {0}?", "Numbers", AnswerLayout.ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 99, "00")]
        NumbersTwoDigit,

        [SouvenirQuestion("Which of these was a contestant on {0} but not the final winner?", "Object Shows", AnswerLayout.TwoColumns4Answers, ExampleAnswers = new[] { "Battleship", "Big Circle", "Jack O’ Lantern", "Lego", "Moon", "Radio", "Combination Lock", "Cookie Jar", "Fidget Spinner" })]
        ObjectShowsContestants,

        [SouvenirQuestion("What was the starting sphere in {0}?", "The Octadecayotton", AnswerLayout.OneColumn4Answers, ExampleAnswers = new[] { "--+", "-+-+-++-+", "-++-+--+-", "+++-+-++-", "--++-++-+-++" })]
        OctadecayottonSphere,
        [SouvenirQuestion("What was one of the subrotations in the {1} rotation in {0}?", "The Octadecayotton", AnswerLayout.OneColumn4Answers, ExampleAnswers = new[] { "-X", "+Y-Z", "+U+V+W", "-R+S+T-O", "+P-Q-X+Y-Z" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        OctadecayottonRotations,

        [SouvenirQuestion("What was the button you pressed in the {1} stage of {0}?", "Odd One Out", AnswerLayout.TwoColumns4Answers, "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        OddOneOutButton,

        [SouvenirQuestion("Which Egyptian hieroglyph was in the {1} in {0}?", "Only Connect", AnswerLayout.TwoColumns4Answers, "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus",
            ExampleExtraFormatArguments = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        OnlyConnectHieroglyphs,

        [SouvenirQuestion("What was the {1} arrow on the display of the {2} stage of {0}?", "Orange Arrows", AnswerLayout.TwoColumns4Answers, "Up", "Right", "Down", "Left",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrangeArrowsSequences,

        [SouvenirQuestion("What was the answer in {0}?", "Orange Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        OrangeCipherAnswer,

        [SouvenirQuestion("What color was the {2} key in the {1} stage of {0}?", "Ordered Keys", AnswerLayout.ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrderedKeysColors,
        [SouvenirQuestion("What was the label on the {2} key in the {1} stage of {0}?", "Ordered Keys", AnswerLayout.ThreeColumns6Answers, "1", "2", "3", "4", "5", "6",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrderedKeysLabels,
        [SouvenirQuestion("What color was the label of the {2} key in the {1} stage of {0}?", "Ordered Keys", AnswerLayout.ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrderedKeysLabelColors,

        [SouvenirQuestion("What was the observer’s intial position in {0}?", "Orientation Cube", AnswerLayout.TwoColumns4Answers, "front", "left", "back", "right")]
        OrientationCubeInitialObserverPosition,

        [SouvenirQuestion("What was {1}'s {2} digit from the right in {0}?", "Palindromes", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "X", "first", "X", "second", "X", "third", "X", "4th", "X", "5th", "X", "first", "Y", "second", "Y", "third", "Y", "4th", "Y", "first", "Z", "second", "Z", "third", "Z", "4th", "Z", "5th", "the screen", "first", "the screen", "second", "the screen", "third", "the screen", "4th", "the screen", "5th" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(0, 9)]
        PalindromesNumbers,

        [SouvenirQuestion("What was the LED color in the {1} stage of {0}?", "Partial Derivatives", AnswerLayout.ThreeColumns6Answers, "blue", "green", "orange", "purple", "red", "yellow",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PartialDerivativesLedColors,
        [SouvenirQuestion("What was the {1} term in {0}?", "Partial Derivatives", AnswerLayout.TwoColumns4Answers,
            ExampleAnswers = new[] { "−5x⁴z³", "8x⁴z⁴", "4xy³z²", "−3x⁴z", "3x⁵y⁵z³" }, ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PartialDerivativesTerms,

        [SouvenirQuestion("What was the passport expiration year of the {1} inspected passenger in {0}?", "Passport Control", AnswerLayout.ThreeColumns6Answers, ExampleAnswers = new[] { "1931", "1956", "1977", "1980", "1991", "2000", "2004", "2019", "2047" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PassportControlPassenger,

        [SouvenirQuestion("Which symbol was highlighted in {0}?", "Pattern Cube", AnswerLayout.ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "PatternCubeSprites")]
        PatternCubeHighlightedSymbol,

        [SouvenirQuestion("What was the {1} color in the initial sequence in {0}?", "Perspective Pegs", AnswerLayout.ThreeColumns6Answers, "red", "yellow", "green", "blue", "purple",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PerspectivePegsColorSequence,

        [SouvenirQuestion("What was the offset in {0}?", "Phosphorescence", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 419)]
        PhosphorescenceOffset,
        [SouvenirQuestion("What was the {1} button press in {0}?", "Phosphorescence", AnswerLayout.ThreeColumns6Answers, new[] { "Azure", "Blue", "Crimson", "Diamond", "Emerald", "Fuchsia", "Green", "Ice", "Jade", "Kiwi", "Lime", "Magenta", "Navy", "Orange", "Purple", "Quartz", "Red", "Salmon", "Tan", "Ube", "Vibe", "White", "Xotic", "Yellow", "Zen" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th", "6th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PhosphorescenceButtonPresses,

        [SouvenirQuestion("What was the {1} digit of the displayed number in {0}?", "Pie", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        PieDigits,

        [SouvenirQuestion("What was the {1} in {0}?", "Pigpen Cycle", AnswerLayout.TwoColumns4Answers, "Advanced", "Addition", "Allotype", "Allotted", "Binaries", "Billions", "Bullhorn", "Bulwarks", "Ciphered", "Circuits", "Commando", "Compiler", "Decrypts", "Division", "Dispatch", "Discrete", "Encipher", "Entrance", "Equators", "Equalise", "Finished", "Findings", "Formulae", "Fortunes", "Gauntlet", "Gambling", "Gatepost", "Gateways", "Hazarded", "Haziness", "Huntress", "Hungrier", "Indicate", "Indigoes", "Illusory", "Illuding", "Jigsawed", "Jimmying", "Junkyard", "Juncture", "Kilowatt", "Kinetics", "Knocking", "Knowable", "Limiting", "Linearly", "Linkwork", "Lingered", "Monogram", "Monotone", "Multiton", "Mulcting", "Nanogram", "Nanotube", "Numerous", "Numerate", "Octangle", "Octuples", "Obstruct", "Obstacle", "Progress", "Projects", "Postsync", "Positron", "Quadrant", "Quadrics", "Quirkish", "Quitters", "Reversed", "Revolved", "Rotators", "Relative", "Starting", "Standard", "Stockade", "Stoccata", "Triggers", "Triangle", "Tomogram", "Tomahawk", "Underrun", "Underlie", "Ulterior", "Ultrahot", "Vicinity", "Viceless", "Volition", "Voluming", "Wingding", "Winnable", "Whatness", "Whatsits", "Yellowed", "Yeasayer", "Yokozuna", "Yourself", "Zippered", "Zigzaggy", "Zymology", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PigpenCycleWord,

        [SouvenirQuestion("What was the first half of the first phrase in {0}?", "Placeholder Talk", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "", "IS IN THE", "IS THE", "IS IN UH", "IS", "IS AT", "IS INN", "IS THE IN", "IN IS", "IS IN.", "IS IN", "THE", "FIRST-", "IN", "UH IS IN", "AT", "LAST-", "UH", "KEYBORD", "A" })]
        PlaceholderTalkFirstPhrase,
        [SouvenirQuestion("What was the last half of the first phrase in {0}?", "Placeholder Talk", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "", "FIRST POS.", "SECOND POS.", "THIRD POS.", "FOURTH POS.", "FIFTH POS.", "MILLIONTH POS.", "BILLIONTH POS.", "LAST POS.", "AN ANSWER" })]
        PlaceholderTalkOrdinal,
        [SouvenirQuestion("What was the second phrase’s calculated value in {0}?", "Placeholder Talk", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(1, 8)]
        PlaceholderTalkSecondPhrase,

        [SouvenirQuestion("What was the planet shown in {0}?", "Planets", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "PlanetsSprites")]
        PlanetsPlanet,
        [SouvenirQuestion("What was the color of the {1} strip (from the top) in {0}?", "Planets", AnswerLayout.ThreeColumns6Answers, "Aqua", "Blue", "Green", "Lime", "Orange", "Red", "Yellow", "White", "Off",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PlanetsStrips,

        [SouvenirQuestion("What was the {1} in {0}?", "Playfair Cycle", AnswerLayout.TwoColumns4Answers, "Advanced", "Advocate", "Allotype", "Allotted", "Binaries", "Binomial", "Bullhorn", "Bulwarks", "Circular", "Circuits", "Commando", "Compiler", "Decrypts", "Decimals", "Dispatch", "Discrete", "Encipher", "Encoding", "Equators", "Equalise", "Finished", "Finnicky", "Formulae", "Fortunes", "Gauntlet", "Gauchest", "Gatepost", "Gateways", "Hotlinks", "Hotheads", "Huntress", "Hungrier", "Indicate", "Indirect", "Illusory", "Illuding", "Jigsawed", "Jiggling", "Junkyard", "Juncture", "Kilowatt", "Kilobyte", "Knocking", "Knowable", "Limiting", "Limerick", "Linkwork", "Lingered", "Monogram", "Monolith", "Multiton", "Mulcting", "Nanogram", "Nanobots", "Numerous", "Numerate", "Octangle", "Octonary", "Obstruct", "Obstacle", "Progress", "Programs", "Postsync", "Positron", "Quotient", "Quotable", "Quirkish", "Quitters", "Reversed", "Revealed", "Rotators", "Relative", "Stanzaic", "Standard", "Stockade", "Stoccata", "Triggers", "Trickier", "Tomogram", "Tomahawk", "Underrun", "Undoings", "Ulterior", "Ultrahot", "Vicinity", "Vicenary", "Volition", "Voluming", "Wingding", "Wingspan", "Whatness", "Whatsits", "Yearlong", "Yeasayer", "Yokozuna", "Yourself", "Ziggurat", "Zigzaggy", "Zymology", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PlayfairCycleWord,

        [SouvenirQuestion("What was the {1} correct answer you pressed in {0}?", "Poetry", AnswerLayout.TwoColumns4Answers, "clarity", "flow", "fatigue", "hollow", "energy", "sunshine", "ocean", "reflection", "identity", "black", "crowd", "heart", "weather", "words", "past", "solitary", "relax", "dance", "weightless", "morality", "gaze", "failure", "bunny", "lovely", "romance", "future", "focus", "search", "cookies", "compassion", "creation", "patience",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PoetryAnswers,

        [SouvenirQuestion("What was the starting position in {0}?", "Polyhedral Maze", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 61)]
        PolyhedralMazeStartPosition,

        [SouvenirQuestion("What was the number shown in {0}?", "Prime Encryption", AnswerLayout.ThreeColumns6Answers, ExampleAnswers = new[] { "1147", "1271", "1333", "1457", "1643", "1829" })]
        PrimeEncryptionDisplayedValue,

        [SouvenirQuestion("What was the missing frequency in the {1} wire in {0}?", "Probing", AnswerLayout.TwoColumns4Answers, "10Hz", "22Hz", "50Hz", "60Hz",
            ExampleExtraFormatArguments = new[] { "red-white", "yellow-black", "green", "gray", "yellow-red", "red-blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ProbingFrequencies,

        [SouvenirQuestion("What was the target word on {0}?", "Purple Arrows", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "Thesis", "Immune", "Agency", "Height", "Active", "Bother", "Viable" })]
        PurpleArrowsFinish,

        [SouvenirQuestion("What was the {1} sequence’s answer in {0}?", "Quaver", AnswerLayout.OneColumn4Answers, ExampleAnswers = new[] { "4", "10", "87", "320", "3, 3, 2, 3", "87, 85, 82, 84" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "11th", "12th", "13th", "14th", "15th", "16th", "17th", "18th", "19th", "20th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        QuaverArrows,

        [SouvenirQuestion("What was the {1} digit in the {2} slot in {0}?", "Quintuples", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Integers(0, 9)]
        QuintuplesNumbers,
        [SouvenirQuestion("What color was the {1} digit in the {2} slot in {0}?", "Quintuples", AnswerLayout.TwoColumns4Answers, "red", "blue", "orange", "green", "pink",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        QuintuplesColors,
        [SouvenirQuestion("How many numbers were {1} in {0}?", "Quintuples", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "red", "blue", "orange", "green", "pink" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 25)]
        QuintuplesColorCounts,

        [SouvenirQuestion("What was the {1} coupled car in {0}?", "Railway Cargo Loading", AnswerLayout.OneColumn4Answers, "Baggage Car", "Closed Coach", "Dining Car", "Dome Car", "Double-decker", "Open Coach", "Sleeper Car", "Auto Rack", "Box Car", "Coil Car", "Flat Car", "Hopper Car", "Refrigerated Wagon", "Schnabel Car", "Stock Car", "Tank Car", "Crew Car", "Traveling Post Office",
            ExampleExtraFormatArguments = new[] { "second", "third", "4th", "5th", "6th", "7th", "8th", "9th", "10th", "11th", "12th", "13th", "14th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        RailwayCargoLoadingCars,
        [SouvenirQuestion("Which freight table rule {1} in {0}?", "Railway Cargo Loading", AnswerLayout.OneColumn4Answers, "Over 150 lumber/75 logs", "Over 100 sheet metal", "Over 250 crude oil", "Over 400 mail", "Over 30 livestock", "Over 600 milk/water/resin", "Over 100 liquid fuel", "Over 700 industrial gas", "Over 150 food", "Over 100 coal", "Over 500 loose bulk (excl. coal)", "Over 7 large objects", "Over 5 automobiles", "Over 700 industrial gas",
            ExampleExtraFormatArguments = new[] { "was met", "wasn’t met" }, ExampleExtraFormatArgumentGroupSize = 1)]
        RailwayCargoLoadingFreightTableRules,

        [SouvenirQuestion("What was the displayed number in {0}?", "Rainbow Arrows", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 99)]
        RainbowArrowsNumber,

        [SouvenirQuestion("What was the color of the {1} LED in {0}?", "Recolored Switches", AnswerLayout.TwoColumns4Answers, "red", "green", "blue", "cyan", "orange", "purple", "white",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        RecoloredSwitchesLedColors,

        [SouvenirQuestion("What was the starting number in {0}?", "Red Arrows", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        RedArrowsStartNumber,

        [SouvenirQuestion("What was the answer in {0}?", "Red Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        RedCipherAnswer,

        [SouvenirQuestion("Which condition was the solving condition in {0}?", "Reformed Role Reversal", AnswerLayout.ThreeColumns6Answers, "second", "third", "4th", "5th", "6th", "7th", "8th")]
        ReformedRoleReversalCondition,
        [SouvenirQuestion("What color was the {1} wire in {0}?", "Reformed Role Reversal", AnswerLayout.ThreeColumns6Answers, "Navy", "Lapis", "Blue", "Sky", "Teal", "Plum", "Violet", "Purple", "Magenta", "Lavender",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th", "6th", "7th", "8th", "9th", "10th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ReformedRoleReversalWire,

        [SouvenirQuestion("What was the displayed digit that corresponded to the solution phrase in {0}?", "Regular Crazy Talk", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        RegularCrazyTalkDigit,
        [SouvenirQuestion("What was the embellishment of the solution phrase in {0}?", "Regular Crazy Talk", AnswerLayout.OneColumn4Answers, "[PHRASE]", "It says: [PHRASE]", "Quote: [PHRASE] End quote", "“[PHRASE]”", "It says: “[PHRASE]”", "“It says: [PHRASE]”")]
        RegularCrazyTalkModifier,

        [SouvenirQuestion("Which one of these houses was on offer, but not chosen by Bob in {0}?", "Retirement", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "Hotham Place", "Homestead", "Riverwell", "Lodge Park" })]
        RetirementHouses,

        [SouvenirQuestion("What was the {1} character in the {2} message of {0}?", "Reverse Morse", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second", "third", "first", "third", "second", "fourth", "first", "fourth", "second", "fifth", "first", "fifth", "second", "sixth", "first", "sixth", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        [AnswerGenerator.Strings("A-Z0-9")]
        ReverseMorseCharacters,

        [SouvenirQuestion("What character was used in the {1} round of {0}?", "Reverse Polish Notation", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-G0-9")]
        ReversePolishNotationCharacter,

        [SouvenirQuestion("What was the exit coordinate in {0}?", "RGB Maze", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Strings("A-H", "1-8")]
        RGBMazeExit,
        [SouvenirQuestion("Where was the {1} key in {0}?", "RGB Maze", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-H", "1-8")]
        RGBMazeKeys,
        [SouvenirQuestion("Which maze number was the {1} maze in {0}?", "RGB Maze", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        RGBMazeNumber,

        [SouvenirQuestion("What was the color in {0}?", "Rhythms", AnswerLayout.TwoColumns4Answers, "Blue", "Red", "Green", "Yellow")]
        RhythmsColor,

        [SouvenirQuestion("What was the rule number in {0}?", "Rule", AnswerLayout.ThreeColumns6Answers, AddThe = true)]
        [AnswerGenerator.Integers(0, 15)]
        RuleNumber,

        [SouvenirQuestion("What four-digit number was given in {0}?", "Roger", AnswerLayout.ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 9999, "0000")]
        RogerSeed,

        [SouvenirQuestion("What was the number to the correct condition in {0}?", "Role Reversal", AnswerLayout.ThreeColumns6Answers, "2", "3", "4", "5", "6", "7", "8")]
        RoleReversalNumber,
        [SouvenirQuestion("How many {1} wires were there in {0}?", "Role Reversal", AnswerLayout.ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7",
            ExampleExtraFormatArguments = new[] { "warm-colored", "cold-colored", "primary-colored", "secondary-colored" }, ExampleExtraFormatArgumentGroupSize = 1)]
        RoleReversalWires,

        [SouvenirQuestion("Which tile was correctly submitted in the first stage of {0}?", "Scavenger Hunt", AnswerLayout.ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "Tiles4x4Sprites")]
        ScavengerHuntKeySquare,
        [SouvenirQuestion("Which of these tiles was {1} in the first stage of {0}?", "Scavenger Hunt", AnswerLayout.ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "Tiles4x4Sprites",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ScavengerHuntColoredTiles,

        [SouvenirQuestion("What was the contestant’s name in {0}?", "Schlag den Bomb", AnswerLayout.TwoColumns4Answers, "Ron", "Don", "Julia", "Cory", "Greg", "Paula", "Val", "Lisa", "Ozy", "Ozzy", "Elsa", "Cori", "Harry", "Gale", "Daniel", "Albert", "Spike", "Tommy", "Greta", "Tina", "Rob", "Edgar", "Julia", "Peter", "Millie", "Isolde", "Eris")]
        SchlagDenBombContestantName,
        [SouvenirQuestion("What was the contestant’s score in {0}?", "Schlag den Bomb", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 75)]
        SchlagDenBombContestantScore,
        [SouvenirQuestion("What was the bomb’s score in {0}?", "Schlag den Bomb", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 75)]
        SchlagDenBombBombScore,

        [SouvenirQuestion("What were the first and second words in the {1} phrase in {0}?", "Sea Shells", AnswerLayout.TwoColumns4Answers, "she sells", "she shells", "sea shells", "sea sells",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells1,
        [SouvenirQuestion("What were the third and fourth words in the {1} phrase in {0}?", "Sea Shells", AnswerLayout.TwoColumns4Answers, "sea shells", "she shells", "sea sells", "she sells",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells2,
        [SouvenirQuestion("What was the end of the {1} phrase in {0}?", "Sea Shells", AnswerLayout.TwoColumns4Answers, "sea shore", "she sore", "she sure", "seesaw",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells3,

        [SouvenirQuestion("What was the {1} letter involved in the starting value in {0}?", "Semamorse", AnswerLayout.ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "Morse", "semaphore" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SemamorseLetters,
        [SouvenirQuestion("What was the color of the display involved in the starting value in {0}?", "Semamorse", AnswerLayout.TwoColumns4Answers, "red", "green", "cyan", "indigo", "pink")]
        SemamorseColor,

        [SouvenirQuestion("What sequence was used in {0}?", "Sequencyclopedia", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "A000001", "A069420", "A111111" }, AddThe = true)]
        [AnswerGenerator.Integers(0, 1000000, "'A'000000")]
        SequencyclopediaSequence,

        [SouvenirQuestion("What was the initial letter in {0}?", "Shapes And Bombs", AnswerLayout.ThreeColumns6Answers, "A", "B", "D", "E", "G", "I", "K", "L", "N", "O", "P", "S", "T", "X", "Y")]
        ShapesAndBombsInitialLetter,

        [SouvenirQuestion("What was the initial shape in {0}?", "Shape Shift", AnswerLayout.TwoColumns4Answers, Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings('A', 'P')]
        ShapeShiftInitialShape,

        [SouvenirQuestion("What was the final position of the initial cup in {0}?", "Shell Game", AnswerLayout.TwoColumns4Answers, "Left", "Middle", "Right")]
        ShellGameInitialCupFinalPosition,

        [SouvenirQuestion("What was the seed in {0}?", "Shifting Maze", AnswerLayout.TwoColumns4Answers, null)]
        [AnswerGenerator.Strings(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/")]
        ShiftingMazeSeed,

        [SouvenirQuestion("What was the {1} slot in the {2} stage in {0}?", "Silly Slots", AnswerLayout.TwoColumns4Answers, "red bomb", "red cherry", "red coin", "red grape", "green bomb", "green cherry", "green coin", "green grape", "blue bomb", "blue cherry", "blue coin", "blue grape",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third", "third", "first", "third", "second", "third", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SillySlots,

        [SouvenirQuestion("What were the call samples {1} of {0}?", "Simon Samples", AnswerLayout.ThreeColumns6Answers, "KKSS", "KKSH", "KSSH", "KHSS", "KHSH", "KHSO", "KHOH", "KOSH", "KOSO", "SKSK", "SHHS",
            ExampleExtraFormatArguments = new[] { "played in the first stage", "added in the second stage", "added in the third stage" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSamplesSamples,

        [SouvenirQuestion("What color flashed {1} in the final sequence in {0}?", "Simon Says", AnswerLayout.TwoColumns4Answers, "red", "yellow", "green", "blue",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSaysFlash,

        [SouvenirQuestion("What color flashed {1} in {0}?", "Simon Scrambles", AnswerLayout.TwoColumns4Answers, "Red", "Green", "Blue", "Yellow",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonScramblesColors,

        [SouvenirQuestion("Which color flashed {1} in the final sequence in {0}?", "Simon Screams", AnswerLayout.ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonScreamsFlashing,

        [SouvenirQuestion("In which stage(s) of {0} was “{1}” the applicable rule?", "Simon Screams", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "first", "second", "third", "first and second", "first and third", "second and third", "all of them" },
            ExampleExtraFormatArguments = new[] {
                "three adjacent colors flashing in clockwise order",
                "a color flashing, then an adjacent color, then the first again",
                "at most one color flashing out of red, yellow, and blue",
                "two colors opposite each other that didn’t flash",
                "two (but not three) adjacent colors flashing in clockwise order"
            }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonScreamsRule,

        [SouvenirQuestion("Which color flashed {1} in the {2} stage of {0}?", "Simon Selects", AnswerLayout.ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Magenta", "Cyan",
             ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonSelectsOrder,

        [SouvenirQuestion("What was the {1} received letter in {0}?", "Simon Sends", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings('A', 'Z')]
        SimonSendsReceivedLetters,

        [SouvenirQuestion("What was the {1} flash in the final sequence in {0}?", "Simon Simons", AnswerLayout.ThreeColumns6Answers, "TR", "TY", "TG", "TB", "LR", "LY", "LG", "LB", "RR", "RY", "RG", "RB", "BR", "BY", "BG", "BB",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSimonsFlashingColors,

        [SouvenirQuestion("Which key’s color flashed {1} in the {2} stage of {0}?", "Simon Sings", AnswerLayout.ThreeColumns6Answers, "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third", "third", "first", "third", "second", "third", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonSingsFlashing,

        [SouvenirQuestion("How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?", "Simon Shrieks", AnswerLayout.ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonShrieksFlashingButton,

        [SouvenirQuestion("Which sample button sounded {1} in the final sequence in {0}?", "Simon Sounds", AnswerLayout.TwoColumns4Answers, "red", "blue", "yellow", "green",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSoundsFlashingColors,

        [SouvenirQuestion("Which bubble flashed first in {0}?", "Simon Speaks", AnswerLayout.TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        SimonSpeaksPositions,
        [SouvenirQuestion("Which bubble flashed second in {0}?", "Simon Speaks", AnswerLayout.ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteField = "SimonSpeaksSprites")]
        SimonSpeaksShapes,
        [SouvenirQuestion("Which language was the bubble that flashed third in {0} in?", "Simon Speaks", AnswerLayout.TwoColumns4Answers, "English", "Danish", "Dutch", "Esperanto", "Finnish", "French", "German", "Hungarian", "Italian")]
        SimonSpeaksLanguages,
        [SouvenirQuestion("Which word was in the bubble that flashed fourth in {0}?", "Simon Speaks", AnswerLayout.ThreeColumns6Answers, "black", "sort", "zwart", "nigra", "musta", "noir", "schwarz", "fekete", "nero", "blue", "blå", "blauw", "blua", "sininen", "bleu", "blau", "kék", "blu", "green", "grøn", "groen", "verda", "vihreä", "vert", "grün", "zöld", "verde", "cyan", "turkis", "turkoois", "turkisa", "turkoosi", "turquoise", "türkis", "türkiz", "turchese", "red", "rød", "rood", "ruĝa", "punainen", "rouge", "rot", "piros", "rosso", "purple", "lilla", "purper", "purpura", "purppura", "pourpre", "lila", "bíbor", "porpora", "yellow", "gul", "geel", "flava", "keltainen", "jaune", "gelb", "sárga", "giallo", "white", "hvid", "wit", "blanka", "valkoinen", "blanc", "weiß", "fehér", "bianco", "gray", "grå", "grijs", "griza", "harmaa", "gris", "grau", "szürke", "grigio")]
        SimonSpeaksWords,
        [SouvenirQuestion("What color was the bubble that flashed fifth in {0}?", "Simon Speaks", AnswerLayout.ThreeColumns6Answers, "black", "blue", "green", "cyan", "red", "purple", "yellow", "white", "gray")]
        SimonSpeaksColors,

        [SouvenirQuestion("Which color flashed {1} in sequence in {0}?", "Simon’s Star", AnswerLayout.ThreeColumns6Answers, "red", "yellow", "green", "blue", "purple",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonsStarColors,

        [SouvenirQuestion("Which color flashed {1} in the {2} stage in {0}?", "Simon Stages", AnswerLayout.ThreeColumns6Answers, "red", "blue", "yellow", "orange", "magenta", "green", "pink", "lime", "cyan", "white",
            ExampleExtraFormatArguments = new[] { "first", "first", "second", "first", "third", "first", "first", "second", "second", "second", "third", "second", "first", "third", "second", "third", "third", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStagesFlashes,
        [SouvenirQuestion("What color was the indicator in the {1} stage in {0}?", "Simon Stages", AnswerLayout.ThreeColumns6Answers, "red", "blue", "yellow", "orange", "magenta", "green", "pink", "lime", "cyan", "white",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonStagesIndicator,

        [SouvenirQuestion("Which {1} in the {2} stage in {0}?", "Simon States", AnswerLayout.TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", "Red, Yellow", "Red, Green", "Red, Blue", "Yellow, Green", "Yellow, Blue", "Green, Blue", "all 4", "none",
            ExampleExtraFormatArguments = new[] { "color(s) flashed", "first", "color(s) didn’t flash", "first", "color(s) flashed", "second", "color(s) didn’t flash", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStatesDisplay,

        [SouvenirQuestion("Which color flashed {1} in the output sequence in {0}?", "Simon Stops", AnswerLayout.ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Violet",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonStopsColors,

        [SouvenirQuestion("Which color {1} {2} in the final sequence of {0}?", "Simon Stores", AnswerLayout.TwoColumns4Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
            ExampleExtraFormatArguments = new[] { "flashed", "first", "flashed", "second", "was among the colors flashed", "first", "was among the colors flashed", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStoresColors,

        [SouvenirQuestion("What were the original numbers in {0}?", "Skewed Slots", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 999, "000")]
        SkewedSlotsOriginalNumbers,

        [SouvenirQuestion("Which race was selectable, but not the solution, in {0}?", "Skyrim", AnswerLayout.TwoColumns4Answers, "Nord", "Khajiit", "Breton", "Argonian", "Dunmer", "Altmer", "Redguard", "Orc", "Imperial")]
        SkyrimRace,
        [SouvenirQuestion("Which weapon was selectable, but not the solution, in {0}?", "Skyrim", AnswerLayout.TwoColumns4Answers, "Axe of Whiterun", "Dawnbreaker", "Windshear", "Blade of Woe", "Firiniel’s End", "Bow of Hunt", "Volendrung", "Chillrend", "Mace of Molag Bal")]
        SkyrimWeapon,
        [SouvenirQuestion("Which enemy was selectable, but not the solution, in {0}?", "Skyrim", AnswerLayout.TwoColumns4Answers, "Alduin", "Blood Dragon", "Cave Bear", "Dragon Priest", "Draugr", "Draugr Overlord", "Frost Troll", "Frostbite Spider", "Mudcrab")]
        SkyrimEnemy,
        [SouvenirQuestion("Which city was selectable, but not the solution, in {0}?", "Skyrim", AnswerLayout.TwoColumns4Answers, "Dawnstar", "Ivarstead", "Markarth", "Riverwood", "Rorikstead", "Solitude", "Whiterun", "Windhelm", "Winterhold")]
        SkyrimCity,
        [SouvenirQuestion("Which dragon shout was selectable, but not the solution, in {0}?", "Skyrim", AnswerLayout.TwoColumns4Answers, "Disarm", "Dismay", "Dragonrend", "Fire Breath", "Ice Form", "Kyne’s Peace", "Slow Time", "Unrelenting Force", "Whirlwind Sprint")]
        SkyrimDragonShout,

        [SouvenirQuestion("How many red balls were there at the start of {0}?", "Snooker", AnswerLayout.TwoColumns4Answers, "8", "9", "10", "11")]
        SnookerReds,

        [SouvenirQuestion("What positions were the last swap used to solve {0}?", "Sorting", AnswerLayout.ThreeColumns6Answers, "1 & 2", "1 & 3", "1 & 4", "1 & 5", "2 & 3", "2 & 4", "2 & 5", "3 & 4", "3 & 5", "4 & 5")]
        SortingLastSwap,

        [SouvenirQuestion("What was the first module asked about in the other Souvenir on this bomb?", "Souvenir", AnswerLayout.OneColumn4Answers, null,
            ExampleAnswers = new[] { "Probing", "Microcontroller", "Third Base", "Kudosudoku", "Quintuples", "3D Tunnels", "Uncolored Squares", "Pattern Cube", "Synonyms", "The Moon", "Human Resources", "Algebra" })]
        SouvenirFirstQuestion,

        [SouvenirQuestion("What was the maximum tax amount per vessel in {0}?", "Space Traders", AnswerLayout.ThreeColumns6Answers)]
        SpaceTradersMaxTax,

        [SouvenirQuestion("What was the {1} picture on {0}?", "Sonic The Hedgehog", AnswerLayout.TwoColumns4Answers, "Annoyed Sonic", "Ballhog", "Blue Lamppost", "Burrobot", "Buzz Bomber", "Crab Meat", "Dead Sonic", "Drowned Sonic", "Falling Sonic", "Moto Bug", "Red Lamppost", "Red Spring", "Standing Sonic", "Switch", "Yellow Spring",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SonicTheHedgehogPictures,
        [SouvenirQuestion("Which sound was played by the {1} screen on {0}?", "Sonic The Hedgehog", AnswerLayout.TwoColumns4Answers, "Boss Theme", "Breathe", "Continue", "Drown", "Emerald", "Extra Life", "Final Zone", "Invincibility", "Jump", "Lamppost", "Marble Zone", "Bumper", "Skid", "Spikes", "Spin", "Spring",
            ExampleExtraFormatArguments = new[] { "Running Boots", "Invincibility", "Extra Life", "Rings" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SonicTheHedgehogSounds,

        [SouvenirQuestion("What was the {1} flashed color in {0}?", "Sphere", AnswerLayout.ThreeColumns6Answers, "red", "blue", "green", "orange", "pink", "purple", "grey", "white",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        SphereColors,

        [SouvenirQuestion("What word was asked to be spelled in {0}?", "Spelling Bee", AnswerLayout.TwoColumns4Answers, null)]
        SpellingBeeWord,

        [SouvenirQuestion("What bag was initially colored in {0}?", "Splitting The Loot", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "A5", "E6", "19", "82" })]
        SplittingTheLootColoredBag,

        [SouvenirQuestion("What was the color of the faulty sphere in {0}?", "Spot the Difference", AnswerLayout.TwoColumns4Answers, null)]
        SpotTheDifferenceFaultyBall,

        [SouvenirQuestion("What was the digit in the center of {0}?", "Stars", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        StarsCenter,

        [SouvenirQuestion("What was the element shown in {0}?", "State of Aggregation", AnswerLayout.ThreeColumns6Answers, "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Al", "Si", "P", "S", "Cl", "Ar", "K", "Ca", "Sc", "Ti", "V", "Cr", "Mn", "Fe", "Co", "Ni", "Cu", "Zn", "Ga", "Ge", "As", "Se", "Br", "Kr", "Rb", "Sr", "Y", "Zr", "Nb", "Mo", "Tc", "Ru", "Rh", "Pd", "Ag", "Cd", "In", "Sn", "Sb", "Te", "I", "Xe", "Cs", "Ba", "La", "Ce", "Pr", "Nd", "Pm", "Sm", "Eu", "Gd", "Tb", "Dy", "Ho", "Er", "Tm", "Yb", "Lu", "Hf", "Ta", "W", "Re", "Os", "Ir", "Pt", "Au", "Hg", "Tl", "Pb", "Bi", "Po", "At", "Rn", "Fr", "Ra", "Ac", "Th", "Pa", "U", "Np", "Pu", "Am", "Cm", "Bk", "Cf", "Es", "Fm", "Md", "No", "Lr", "Rf", "Db", "Sg", "Bh", "Hs", "Mt", "Ds", "Rg", "Cn", "Nh", "Fl", "Mc", "Lv", "Ts", "Og")]
        StateOfAggregationElement,

        [SouvenirQuestion("How many subscribers does {1} have in {0}?", "Subscribe to Pewdiepie", AnswerLayout.TwoColumns4Answers, null,
            ExampleExtraFormatArguments = new[] { "PewDiePie", "T-Series" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(10000000, 99999999)]
        SubscribeToPewdiepieSubCount,

        [SouvenirQuestion("What skull was shown on the {1} square in {0}?", "Sugar Skulls", AnswerLayout.ThreeColumns6Answers, "A", "C", "E", "G", "I", "K", "M", "O", "P", "R", "T", "V", "X", "Z", "b", "d", "f", "h", "j", "l", "n", "p", "r", "t", "v", "x", "z",
            Type = AnswerType.SugarSkullsFont, FontSize = 72, ExampleExtraFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SugarSkullsSkull,
        [SouvenirQuestion("Which skull {1} present in {0}?", "Sugar Skulls", AnswerLayout.ThreeColumns6Answers, "A", "C", "E", "G", "I", "K", "M", "O", "P", "R", "T", "V", "X", "Z", "b", "d", "f", "h", "j", "l", "n", "p", "r", "t", "v", "x", "z",
            Type = AnswerType.SugarSkullsFont, FontSize = 72, ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SugarSkullsAvailability,

        [SouvenirQuestion("What color was the {1} LED on the {2} flip of {0}?", "Switch", AnswerLayout.ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple",
            ExampleExtraFormatArguments = new[] { "top", "first", "bottom", "first", "top", "second", "bottom", "second" }, ExampleExtraFormatArgumentGroupSize = 2, AddThe = true)]
        SwitchInitialColor,

        [SouvenirQuestion("What was the initial position of the switches in {0}?", "Switches", AnswerLayout.ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings(5, 'Q', 'R')]
        SwitchesInitialPosition,

        [SouvenirQuestion("What was the seed in {0}?", "Switching Maze", AnswerLayout.TwoColumns4Answers, null)]
        [AnswerGenerator.Strings(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/")]
        SwitchingMazeSeed,
        [SouvenirQuestion("What was the starting maze color in {0}?", "Switching Maze", AnswerLayout.ThreeColumns6Answers, null)]
        SwitchingMazeColor,

        [SouvenirQuestion("How many symbols were cycling on the {1} screen in {0}?", "Symbol Cycle", AnswerLayout.TwoColumns4Answers, "2", "3", "4", "5",
            ExampleExtraFormatArguments = new[] { "left", "right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SymbolCycleSymbolCounts,

        [SouvenirQuestion("What was the {1} symbol in the {2} stage of {0}?", "Symbolic Coordinates", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "SymbolicCoordinatesSprites",
            ExampleExtraFormatArguments = new[] { "left", "first", "left", "second", "left", "third", "middle", "first", "middle", "second", "middle", "third", "right", "first", "right", "second", "right", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SymbolicCoordinateSymbols,

        [SouvenirQuestion("Which number was displayed on {0}?", "Synonyms", AnswerLayout.ThreeColumns6Answers)]
        [AnswerGenerator.Integers(0, 9)]
        SynonymsNumber,

        [SouvenirQuestion("What error code did you fix in {0}?", "Sysadmin", AnswerLayout.ThreeColumns6Answers)]
        SysadminFixedErrorCodes,

        [SouvenirQuestion("What was the received word in {0}?", "Tap Code", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "child", "style", "shake", "alive", "axion", "wreck", "cause", "pupil", "cheat", "watch" })]
        TapCodeReceivedWord,

        [SouvenirQuestion("What was the {1} flashed color in {0}?", "Tasha Squeals", AnswerLayout.TwoColumns4Answers, "Pink", "Green", "Yellow", "Blue",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TashaSquealsColors,

        [SouvenirQuestion("What was the initial color of the {1} button in the {2} stage of {0}?", "Ten-Button Color Code", AnswerLayout.TwoColumns4Answers, "red", "green", "blue", "yellow",
            ExampleExtraFormatArguments = new[] { "first", "first", "second", "first", "first", "second", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        TenButtonColorCodeInitialColors,

        [SouvenirQuestion("What was the {1} split in {0}?", "Tenpins", AnswerLayout.OneColumn4Answers, "Goal Posts", "Cincinnati", "Woolworth Store", "Lily", "3-7 Split", "Cocked Hat", "4-7-10 Split", "Big Four", "Greek Church", "Big Five", "Big Six", "HOW",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TenpinsSplits,

        [SouvenirQuestion("What was the displayed letter in {0}?", "Text Field", AnswerLayout.ThreeColumns6Answers, "A", "B", "C", "D", "E", "F")]
        TextFieldDisplay,

        [SouvenirQuestion("What was the position from top to bottom of the first wire needing to be cut in {0}?", "Thinking Wires", AnswerLayout.ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7")]
        ThinkingWiresFirstWire,
        [SouvenirQuestion("What color did the second valid wire to cut have to have in {0}?", "Thinking Wires", AnswerLayout.ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", "Black", "Any")]
        ThinkingWiresSecondWire,
        [SouvenirQuestion("What was the display number in {0}?", "Thinking Wires", AnswerLayout.ThreeColumns6Answers, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "69")]
        ThinkingWiresDisplayNumber,

        [SouvenirQuestion("What was the display word in the {1} stage on {0}?", "Third Base", AnswerLayout.ThreeColumns6Answers, "NHXS", "IH6X", "XI8Z", "I8O9", "XOHZ", "H68S", "8OXN", "Z8IX", "SXHN", "6NZH", "H6SI", "6O8I", "NXO8", "66I8", "S89H", "SNZX", "9NZS", "8I99", "ZHOX", "SI9X", "SZN6", "ZSN8", "HZN9", "X9HI", "IS9H", "XZNS", "X6IS", "8NSZ",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ThirdBaseDisplay,

        [SouvenirQuestion("What was on the {1} button at the start of {0}?", "Tic Tac Toe", AnswerLayout.ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "O", "X", Type = AnswerType.TicTacToeFont,
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TicTacToeInitialState,

        [SouvenirQuestion("What was the {1} city in {0}?", "Timezone", AnswerLayout.TwoColumns4Answers, "Alofi", "Papeete", "Unalaska", "Whitehorse", "Denver", "Managua", "Quito", "Manaus", "Buenos Aires", "Sao Paulo", "Praia", "Edinburgh", "Berlin", "Bujumbura", "Moscow", "Tbilisi", "Lahore", "Omsk", "Bangkok", "Beijing", "Tokyo", "Brisbane", "Sydney", "Tarawa",
            ExampleExtraFormatArguments = new[] { "departure", "destination" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TimezoneCities,

        [SouvenirQuestion("What was the word initially shown in {0}?", "Topsy Turvy", AnswerLayout.ThreeColumns6Answers, null)]
        TopsyTurvyWord,

        [SouvenirQuestion("What was the {1} received message in {0}?", "Transmitted Morse", AnswerLayout.TwoColumns4Answers, "BOMBS", "SHORT", "UNDERSTOOD", "W1RES", "SOS", "MANUAL", "STRIKED", "WEREDEAD", "GOTASOUV", "EXPLOSION", "EXPERT", "RIP", "LISTEN", "DETONATE", "ROGER", "WELOSTBRO", "AMIDEAF", "KEYPAD", "DEFUSER", "NUCLEARWEAPONS", "KAPPA", "DELTA", "PI3", "SMOKE", "SENDHELP", "LOST", "SWAN", "NOMNOM", "BLUE", "BOOM", "CANCEL", "DEFUSED", "BROKEN", "MEMORY", "R6S8T", "TRANSMISSION", "UMWHAT", "GREEN", "EQUATIONSX", "RED", "ENERGY", "JESTER", "CONTACT", "LONG",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TransmittedMorseMessage,

        [SouvenirQuestion("What was the {1} line you commented out in {0}?", "Turtle Robot", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "LT 90", "FD 1", "RT 180 2", "LT 90 2", "RT 180", "FD 6", "RT 90 2" },
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.TurtleRobotFont)]
        TurtleRobotCodeLines,

        [SouvenirQuestion("What was the {1} correct query response from {0}?", "Two Bits", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 99, "00")]
        TwoBitsResponse,

        [SouvenirQuestion("What was the answer in {0}?", "Ultimate Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        UltimateCipherAnswer,

        [SouvenirQuestion("What was the {1} in {0}?", "Ultimate Cycle", AnswerLayout.TwoColumns4Answers, "Advanced", "Adverted", "Advocate", "Addition", "Allocate", "Allotype", "Allotted", "Altering", "Binaries", "Binormal", "Binomial", "Billions", "Bulkhead", "Bullhorn", "Bulleted", "Bulwarks", "Ciphered", "Circuits", "Connects", "Conquers", "Commando", "Compiler", "Computer", "Continue", "Decrypts", "Deceived", "Decimate", "Division", "Discover", "Discrete", "Dispatch", "Disposal", "Encipher", "Encrypts", "Encoding", "Entrance", "Equalise", "Equators", "Equation", "Equipped", "Finalise", "Finished", "Findings", "Finnicky", "Formulae", "Fortunes", "Fortress", "Forwards", "Garrison", "Garnered", "Gatepost", "Gateways", "Gauntlet", "Gambling", "Gathered", "Glooming", "Hazarded", "Haziness", "Hotlinks", "Hotheads", "Hundreds", "Hunkered", "Huntsman", "Huntress", "Incoming", "Indicate", "Indirect", "Indigoes", "Illuding", "Illusion", "Illusory", "Illumine", "Jigsawed", "Jimmying", "Journeys", "Jousting", "Junction", "Juncture", "Junkyard", "Judgment", "Kilowatt", "Kilovolt", "Kilobyte", "Kinetics", "Knocking", "Knockout", "Knowable", "Knuckled", "Language", "Landmark", "Limiting", "Linearly", "Lingered", "Linkages", "Linkwork", "Labeling", "Monogram", "Monolith", "Monomial", "Monotone", "Multiton", "Multiply", "Mulcting", "Mulligan", "NANOBOTS", "Nanogram", "Nanowatt", "Nanotube", "Numbered", "Numerous", "Numerals", "Numerate", "Octangle", "Octuples", "Ordering", "Ordinals", "Observed", "Obscured", "Obstruct", "Obstacle", "Progress", "Projects", "Prophase", "Prophecy", "Postsync", "Possible", "Positron", "Positive", "Quadrant", "Quadrics", "Quartile", "Quartics", "Quickest", "Quirkish", "Quintics", "Quitters", "Reversed", "Revolved", "Revealed", "Rotation", "Rotators", "Relation", "Relative", "Relaying", "Starting", "Standard", "Standout", "Stanzaic", "Stoccata", "Stockade", "Stopping", "Stopword", "Trickier", "Trigonal", "Triggers", "Triangle", "Tomogram", "Tomahawk", "Toggling", "Together", "Underrun", "UnderwaY", "Underlie", "Undoings", "Ulterior", "Ultimate", "Ultrared", "Ultrahot", "Venomous", "Vendetta", "Vicinity", "Viceless", "Volition", "Voltages", "Volatile", "Voluming", "Weakened", "Weaponed", "Wingding", "Winnable", "Whatever", "Whatness", "Whatnots", "Whatsits", "Yellowed", "Yearlong", "Yearning", "Yeasayer", "Yielding", "Yielders", "Yokozuna", "Yourself", "Zippered", "Ziggurat", "Zigzaggy", "Zugzwang", "Zygomata", "Zygotene", "Zymology", "Zymogram",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UltimateCycleWord,

        [SouvenirQuestion("What was the {1} rotation in {0}?", "Ultracube", AnswerLayout.ThreeColumns6Answers, "XY", "YX", "XZ", "ZX", "XW", "WX", "XV", "VX", "YZ", "ZY", "YW", "WY", "YV", "VY", "ZW", "WZ", "ZV", "VZ", "WV", "VW", AddThe = true,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UltracubeRotations,

        [SouvenirQuestion("What was the {1} rotation in the {2} stage of {0}?", "UltraStores", AnswerLayout.ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "first", "second", "first", "third", "first", "4th", "first", "5th", "first", "first", "second", "second", "second", "third", "second", "4th", "second", "5th", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        UltraStoresSingleRotation,
        [SouvenirQuestion("What was the {1} rotation in the {2} stage of {0}?", "UltraStores", AnswerLayout.TwoColumns4Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "first", "second", "first", "third", "first", "4th", "first", "5th", "first", "first", "second", "second", "second", "third", "second", "4th", "second", "5th", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        UltraStoresMultiRotation,

        [SouvenirQuestion("What was the {1} color in reading order used in the first stage of {0}?", "Uncolored Squares", AnswerLayout.ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UncoloredSquaresFirstStage,

        [SouvenirQuestion("What was the initial state of the switches in {0}?", "Uncolored Switches", AnswerLayout.ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
        [AnswerGenerator.Strings(5, 'Q', 'R')]
        UncoloredSwitchesInitialState,
        [SouvenirQuestion("What color was the {1} LED in reading order in {0}?", "Uncolored Switches", AnswerLayout.TwoColumns4Answers, "red", "green", "blue", "turquoise", "orange", "purple", "white", "black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UncoloredSwitchesLedColors,

        [SouvenirQuestion("What was the {1} received instruction in {0}?", "Unfair Cipher", AnswerLayout.ThreeColumns6Answers, "PCR", "PCG", "PCB", "SUB", "MIT", "CHK", "PRN", "BOB", "REP", "EAT", "STR", "IKE",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UnfairCipherInstructions,

        [SouvenirQuestion("What was the {1} decrypted instruction in {0}?", "Unfair’s Revenge", AnswerLayout.ThreeColumns6Answers, "PCR", "PCG", "PCB", "SCC", "SCM", "SCY", "SUB", "MIT", "CHK", "PRN", "BOB", "REP", "EAT", "STR", "IKE", "SIG", "PVP", "NXP", "PVS", "NXS", "OPP",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UnfairsRevengeInstructions,

        [SouvenirQuestion("What was the {1} submitted code in {0}?", "Unicode", AnswerLayout.ThreeColumns6Answers, "00A7", "00B6", "0126", "04D4", "017F", "01F6", "01F7", "2042", "037C", "03C2", "040B", "20AA", "042E", "0460", "046C", "20B0", "222F", "222B", "2569", "04EC", "260A", "04A6", "2626", "FB21", "0428", "03A9", "0583", "2592", "254B", "2318", "2234", "2205", "2104", "04A8", "2605", "019B", "03EA", "062A", "067C", "063A", "06BA", "00FE", "0194", "0239",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UnicodeSortedAnswer,

        [SouvenirQuestion("What was the {1} submitted letter in {0}?", "Unown Cipher", AnswerLayout.ThreeColumns6Answers,
            Type = AnswerType.UnownFont, ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings('A', 'Z')]
        UnownCipherAnswers,

        [SouvenirQuestion("Which state did you depart from in {0}?", "USA Maze", AnswerLayout.TwoColumns4Answers, "Alaska", "Alabama", "Arkansas", "Arizona", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming")]
        USAMazeOrigin,

        [SouvenirQuestion("Which word {1} shown in {0}?", "V", AnswerLayout.OneColumn4Answers, null,
        ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1, ExampleAnswers = new[] { "Vacant", "Valorous", "Volition", "Vermin", "Vanity", "Visage", "Voracious", "Veers", "Vengeance", "Violation", "Vigilant", "Veteran", "Vanguarding", "Villain" })]
        VWords,

        [SouvenirQuestion("What was the initially pressed color on {0}?", "Varicolored Squares", AnswerLayout.ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta")]
        VaricoloredSquaresInitialColor,

        [SouvenirQuestion("What was the word in {0}?", "Vcrcs", AnswerLayout.TwoColumns4Answers, "destiny", "control", "refresh", "grouped", "wedging", "summary", "kitchen", "teacher", "concern", "section", "similar", "western", "dropper", "checker", "xeroses", "sunrise", "abolish", "harvest", "protest", "shallow", "plotted", "deafens", "colored", "aroused", "unsling", "holiday", "dictate", "dribble", "retreat", "episode", "crashed", "crazily", "silvers", "usurped", "witcher", "jealous", "village", "wizards", "prosper", "recycle", "pounced", "nonfood", "imblaze", "dryable", "swiftly", "mention", "rubbish", "realize", "collect", "surgeon", "gearbox", "schnozz", "passion", "freshen", "society", "passive", "archive", "shelter", "harmful", "freedom", "papayas", "thwarts", "railway", "teapots", "ravines", "density", "provide", "diagram", "lighter", "general", "upriver", "editors", "mingled", "ransoms", "prairie", "balance", "applied", "history", "calorie", "realism", "liquids", "validly", "varying", "wickers", "isolate", "falsify", "painter", "mixture", "bedroom", "dilemma", "skylike", "ranging", "simplex", "gallied", "missile", "posture", "highway", "prevent", "bracket", "project")]
        VcrcsWord,

        [SouvenirQuestion("What was the color of the {1} vector in {0}?", "Vectors", AnswerLayout.ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple",
        ExampleExtraFormatArguments = new[] { "first", "second", "third", "only" }, ExampleExtraFormatArgumentGroupSize = 1)]
        VectorsColors,

        [SouvenirQuestion("What was the {1} flagpole color on {0}?", "Vexillology", AnswerLayout.ThreeColumns6Answers, "Red", "Orange", "Green", "Yellow", "Blue", "Aqua", "White", "Black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        VexillologyColors,

        [SouvenirQuestion("What was the answer in {0}?", "Violet Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        VioletCipherAnswer,

        [SouvenirQuestion("What was the desired color in the {1} stage on {0}?", "Visual Impairment", AnswerLayout.TwoColumns4Answers, "Blue", "Green", "Red", "White",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        VisualImpairmentColors,

        [SouvenirQuestion("What was the color on the {1} stage in {0}?", "Wavetapping", AnswerLayout.TwoColumns4Answers, "Red", "Orange", "Orange-Yellow", "Chartreuse", "Lime", "Green", "Seafoam Green", "Cyan-Green", "Turquoise", "Dark Blue", "Indigo", "Purple", "Purple-Magenta", "Magenta", "Pink", "Gray",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WavetappingColors,
        [SouvenirQuestion("What was the correct pattern on the {1} stage in {0}?", "Wavetapping", AnswerLayout.ThreeColumns6Answers, null, Type = AnswerType.Sprites, SpriteField = "WavetappingSprites",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WavetappingPatterns,

        [SouvenirQuestion("What was the display text in the {1} stage of {0}?", "What’s on Second", AnswerLayout.ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WhatsOnSecondDisplayText,
        [SouvenirQuestion("What was the display text color in the {1} stage of {0}?", "What’s on Second", AnswerLayout.ThreeColumns6Answers, null,
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WhatsOnSecondDisplayColor,

        [SouvenirQuestion("What was the answer in {0}?", "White Cipher", AnswerLayout.ThreeColumns6Answers, null)]
        WhiteCipherAnswer,

        [SouvenirQuestion("What was the display in the {1} stage on {0}?", "Who's on First", AnswerLayout.ThreeColumns6Answers, "", "BLANK", "C", "CEE", "DISPLAY", "FIRST", "HOLD ON", "LEAD", "LED", "LEED", "NO", "NOTHING", "OK", "OKAY", "READ", "RED", "REED", "SAY", "SAYS", "SEE", "THEIR", "THERE", "THEY ARE", "THEY'RE", "U", "UR", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WhosOnFirstDisplay,

        [SouvenirQuestion("What was the color of the {1} dial in {0}?", "Wire", AnswerLayout.ThreeColumns6Answers, "blue", "green", "grey", "orange", "purple", "red",
            ExampleExtraFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        WireDialColors,
        [SouvenirQuestion("What was the displayed number in {0}?", "Wire", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        [AnswerGenerator.Integers(0, 9)]
        WireDisplayedNumber,

        [SouvenirQuestion("What color was the {1} display from the left in {0}?", "Wire Ordering", AnswerLayout.ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", "white", "black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WireOrderingDisplayColor,
        [SouvenirQuestion("What number was on the {1} display from the left in {0}?", "Wire Ordering", AnswerLayout.TwoColumns4Answers, "1", "2", "3", "4",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WireOrderingDisplayNumber,
        [SouvenirQuestion("What color was the {1} wire from the left in {0}?", "Wire Ordering", AnswerLayout.ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", "white", "black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WireOrderingWireColor,

        [SouvenirQuestion("How many {1} wires were there in {0}?", "Wire Sequence", AnswerLayout.TwoColumns4Answers,
            ExampleExtraFormatArguments = new[] { "red", "blue", "black" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Integers(0, 9)]
        WireSequenceColorCount,

        [SouvenirQuestion("Which of these was {1} on {0}?", "Wolf, Goat, and Cabbage", AnswerLayout.ThreeColumns6Answers, "Cat", "Wolf", "Rabbit", "Berry", "Fish", "Dog", "Duck", "Goat", "Fox", "Grass", "Rice", "Mouse", "Bear", "Cabbage", "Chicken", "Goose", "Corn", "Carrot", "Horse", "Earthworm", "Kiwi", "Seeds",
            ExampleExtraFormatArguments = new[] { "present", "not present" }, ExampleExtraFormatArgumentGroupSize = 2)]
        WolfGoatAndCabbageAnimals,
        [SouvenirQuestion("What was the boat size in {0}?", "Wolf, Goat, and Cabbage", AnswerLayout.ThreeColumns6Answers, null)]
        [AnswerGenerator.Integers(0, 9)]
        WolfGoatAndCabbageBoatSize,

        [SouvenirQuestion("What was the label shown in {0}?", "Working Title", AnswerLayout.OneColumn4Answers, "foo", "foobar", "quuz", "garply", "plugh", "wibble", "flob", "fuga", "toto", "tutu", "eggs", "alice", "lorem ipsum", "widget", "eek", "bat", "haystack", "blarg", "kalaa", "sub", "momo", "change this", "hi", "thing", "xyz", "bar", "qux", "corge", "waldo", "xyzzy", "wobble", "hoge", "hogera", "tata", "spam", "raboof", "bob", "do stuff", "bla", "moof", "shme", "beekeeper", "dothestuff", "mum", "temp", "var", "placeholder", "hello", "stuff", "text", "baz", "quux", "grault", "fred", "thud", "wubble", "piyo", "hogehoge", "titi", "ham", "fruit", "john doe", "data", "gadget", "gleep", "needle", "blah", "grault", "puppu", "test", "change", "null", "hey", "something", "abc")]
        WorkingTitleLabel,

        [SouvenirQuestion("What was the {1} displayed letter (in reading order) in {0}?", "XmORse Code", AnswerLayout.ThreeColumns6Answers,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        [AnswerGenerator.Strings("A-Z")]
        XmORseCodeDisplayedLetters,
        [SouvenirQuestion("What word did you decrypt in {0}?", "XmORse Code", AnswerLayout.ThreeColumns6Answers, "ADMIT", "AWARD", "BANJO", "BRAVO", "CHILL", "CYCLE", "DECOR", "DISCO", "EERIE", "ERUPT", "FEWER", "FUZZY", "GERMS", "GUSTO", "HAULT", "HEXED", "ICHOR", "INFER", "JEWEL", "KTANE", "LADLE", "LYRIC", "MANGO", "MUTED", "NERDS", "NIXIE", "OOZED", "OXIDE", "PARTY", "PURSE", "QUEST", "RETRO", "ROUGH", "SCOWL", "SIXTH", "THANK", "TWINE", "UNBOX", "USHER", "VIBES", "VOICE", "WHIZZ", "WRUNG", "XENON", "YOLKS", "ZILCH")]
        XmORseCodeWord,

        [SouvenirQuestion("What was the initial roll on {0}?", "Yahtzee", AnswerLayout.TwoColumns4Answers, "Yahtzee", "large straight", "small straight", "four of a kind", "full house", "three of a kind", "two pairs", "pair")]
        YahtzeeInitialRoll,

        [SouvenirQuestion("What was the starting row letter in {0}?", "Yellow Arrows", AnswerLayout.ThreeColumns6Answers, null)]
        [AnswerGenerator.Strings('A', 'Z')]
        YellowArrowsStartingRow,

        [SouvenirQuestion("What was the answer in {0}?", "Yellow Cipher", AnswerLayout.TwoColumns4Answers, null, ExampleAnswers = new[] { "although", "business", "children", "director", "exchange", "function", "guidance", "hospital", "industry", "junction", "keyboard", "language", "material", "numerous", "offering", "possible", "question", "research", "software", "together", "ultimate", "valuable", "wireless", "xenolith", "yourself", "zucchini" })]
        YellowCipherAnswer,

        [SouvenirQuestion("What was the {1} decrypted word in {0}?", "Zoni", AnswerLayout.ThreeColumns6Answers, null, ExampleAnswers = new[] { "angel", "thing", "dance", "heavy", "quote" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ZoniWords
    }
}
