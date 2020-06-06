using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Souvenir
{
    enum Question
    {
        [SouvenirQuestion("What were the markings in {0}?", "3D Maze", 6, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
        _3DMazeMarkings,

        [SouvenirQuestion("What was the cardinal direction in {0}?", "3D Maze", 4, "North", "South", "West", "East")]
        _3DMazeBearing,

        [SouvenirQuestion("What was the {1} goal node in {0}?", "3D Tunnels", 6, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", ".",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.SymbolsFont)]
        _3DTunnelsTargetNode,

        [SouvenirQuestion("What was the background color on the {1} stage in {0}?", "Accumulation", 6, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AccumulationBackgroundColor,
        [SouvenirQuestion("What was the border color in {0}?", "Accumulation", 6, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow")]
        AccumulationBorderColor,

        [SouvenirQuestion("Which item was the {1} correct item you used in {0}?", "Adventure Game", 4, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGameCorrectItem,

        [SouvenirQuestion("What enemy were you fighting in {0}?", "Adventure Game", 4, "Dragon", "Demon", "Eagle", "Goblin", "Troll", "Wizard", "Golem", "Lizard")]
        AdventureGameEnemy,

        [SouvenirQuestion("What was the {1} in the last successful round of {0}?", "Affine Cycle", 4, "Advanced", "Addition", "Allocate", "Allotted", "Binaries", "Billions", "Bulkhead", "Bulwarks", "Ciphered", "Circuits", "Computer", "Compiler", "Decrypts", "Division", "Discover", "Discrete", "Encipher", "Entrance", "Equation", "Equalise", "Finished", "Findings", "Fortress", "Fortunes", "Gauntlet", "Gambling", "Gathered", "Gateways", "Hazarded", "Haziness", "Hunkered", "Hungrier", "Indicate", "Indigoes", "Illusion", "Illuding", "Jigsawed", "Jimmying", "Junction", "Juncture", "Kilowatt", "Kinetics", "Knockout", "Knowable", "Limiting", "Linearly", "Linkages", "Lingered", "Monogram", "Monotone", "Multiply", "Mulcting", "Nanogram", "Nanotube", "Numbered", "Numerate", "Octangle", "Octuples", "Observed", "Obstacle", "Progress", "Projects", "Position", "Positron", "Quadrant", "Quadrics", "Quickest", "Quitters", "Reversed", "Revolved", "Rotation", "Relative", "Starting", "Standard", "Stopping", "Stoccata", "Triggers", "Triangle", "Tomogram", "Tomorrow", "Underrun", "Underlie", "Ultimate", "Ultrahot", "Vicinity", "Viceless", "Voltages", "Voluming", "Wingding", "Winnable", "Whatever", "Whatsits", "Yellowed", "Yeasayer", "Yielders", "Yourself", "Zippered", "Zigzaggy", "Zugzwang", "Zymogene",
            ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AffineCycleWord,

        [SouvenirQuestion("What was the first equation in {0}?", "Algebra", 4, "a=3z", "a=5+y", "a=6-x", "a=7x", "a=8y", "a=9+z", "a=x/2", "a=x+1", "a=y/4", "a=y-2", "a=z/10", "a=z-7")]
        AlgebraEquation1,
        [SouvenirQuestion("What was the second equation in {0}?", "Algebra", 4, "b=(2x/10)-y", "b=(7x)y", "b=(x+y)-(z/2)", "b=(y/2)-z", "b=(zy)-(2x)", "b=(z-y)/2", "b=2(z+7)", "b=2z+7", "b=xy-(2+x)", "b=xyz")]
        AlgebraEquation2,

        [SouvenirQuestion("What was the symbol on the submit button in {0}?", "Arithmelogic", 6, null, Type = AnswerType.Sprites)]
        ArithmelogicSubmit,
        [SouvenirQuestion("Which number was selectable, but not the solution, in the {1} screen on {0}?", "Arithmelogic", 6, null, ExampleAnswers = new[] { "12", "33", "47", "82", "99" },
            ExampleExtraFormatArguments = new[] { "left", "middle", "right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ArithmelogicNumbers,

        [SouvenirQuestion("What color was the {1} correct button when you solved {0}?", "Bamboozled Again", 4, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainButtonColor,
        [SouvenirQuestion("What was the text on the {1} correct button when you solved {0}?", "Bamboozled Again", 4, "THE LETTER", "ONE LETTER", "THE COLOUR", "ONE COLOUR", "THE PHRASE", "ONE PHRASE", "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "QUEBEC", "TANGO", "WHISKEY", "VICTOR", "YANKEE", "ECHO ECHO", "E THEN E", "ALPHA PAPA", "PAPA ALPHA", "PAPHA ALPA", "T GOLF", "TANGOLF", "WHISKEE", "WHISKY", "CHARLIE C", "C CHARLIE", "YANGO", "DELTA NEXT", "CUEBEQ", "MILO", "KI LO", "HI-LO", "VVICTOR", "VICTORR", "LIME BRAVO", "BLUE BRAVO", "G IN JADE", "G IN ROSE", "BLUE IN RED", "YES BUT NO", "COLOUR", "MESSAGE", "CIPHER", "BUTTON", "TWO BUTTONS", "SIX BUTTONS", "I GIVE UP", "ONE ELEVEN", "ONE ONE ONE", "THREE ONES", "WHAT?", "THIS?", "THAT?", "BLUE!", "ECHO!", "BLANK", "BLANK?!", "NOTHING", "YELLOW TEXT", "BLACK TEXT?", "QUOTE V", "END QUOTE", "\"QUOTE K\"", "IN RED", "ORANGE", "IN YELLOW", "LIME", "IN GREEN", "JADE", "IN CYAN", "AZURE", "IN BLUE", "VIOLET", "IN MAGENTA", "ROSE",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainButtonText,
        [SouvenirQuestion("What was the {1} decrypted text on the display when you solved {0}?", "Bamboozled Again", 4, "THE LETTER", "ONE LETTER", "THE COLOUR", "ONE COLOUR", "THE PHRASE", "ONE PHRASE",
            ExampleExtraFormatArguments = new[] { "first", "third", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainDisplayTexts1,
        [SouvenirQuestion("What was the {1} decrypted text on the display when you solved {0}?", "Bamboozled Again", 4, "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "QUEBEC", "TANGO", "WHISKEY", "VICTOR", "YANKEE", "ECHO ECHO", "E THEN E", "ALPHA PAPA", "PAPA ALPHA", "PAPHA ALPA", "T GOLF", "TANGOLF", "WHISKEE", "WHISKY", "CHARLIE C", "C CHARLIE", "YANGO", "DELTA NEXT", "CUEBEQ", "MILO", "KI LO", "HI-LO", "VVICTOR", "VICTORR", "LIME BRAVO", "BLUE BRAVO", "G IN JADE", "G IN ROSE", "BLUE IN RED", "YES BUT NO", "COLOUR", "MESSAGE", "CIPHER", "BUTTON", "TWO BUTTONS", "SIX BUTTONS", "I GIVE UP", "ONE ELEVEN", "ONE ONE ONE", "THREE ONES", "WHAT?", "THIS?", "THAT?", "BLUE!", "ECHO!", "BLANK", "BLANK?!", "NOTHING", "YELLOW TEXT", "BLACK TEXT?", "QUOTE V", "END QUOTE", "\"QUOTE K\"", "IN RED", "ORANGE", "IN YELLOW", "LIME", "IN GREEN", "JADE", "IN CYAN", "AZURE", "IN BLUE", "VIOLET", "IN MAGENTA", "ROSE",
            ExampleExtraFormatArguments = new[] { "6th", "7th", "8th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainDisplayTexts2,
        [SouvenirQuestion("What color was the {1} text on the display when you solved {0}?", "Bamboozled Again", 4, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozledAgainDisplayColor,

        [SouvenirQuestion("What color was the button in stage {1} of {0}?", "Bamboozling Button", 4, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black",
          ExampleExtraFormatArguments = new[] { "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BamboozlingButtonColor,
        [SouvenirQuestion("What was the {2} label on the button in stage {1} of {0}?", "Bamboozling Button", 4, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE",
          ExampleExtraFormatArguments = new[] { "1", "top", "1", "bottom", "2", "top", "2", "bottom" }, ExampleExtraFormatArgumentGroupSize = 2)]
        BamboozlingButtonLabel,
        [SouvenirQuestion("What was the {2} display in stage {1} of {0}?", "Bamboozling Button", 4, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE",
          ExampleExtraFormatArguments = new[] { "1", "first", "1", "third", "1", "fourth", "1", "fifth" }, ExampleExtraFormatArgumentGroupSize = 2)]
        BamboozlingButtonDisplay,
        [SouvenirQuestion("What was the color of the {2} display in stage {1} of {0}?", "Bamboozling Button", 4, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey",
          ExampleExtraFormatArguments = new[] { "1", "fourth", "1", "fifth", "2", "fourth", "2", "fifth" }, ExampleExtraFormatArgumentGroupSize = 2)]
        BamboozlingButtonDisplayColor,

        [SouvenirQuestion("What color was {1} in the solution to {0}?", "Big Circle", 6, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BigCircleColors,

        [SouvenirQuestion("At which numeric value did you cut the correct wire in {0}?", "Binary LEDs", 6, null, ExampleAnswers = new[] { "1", "5", "12", "19", "25", "31" })]
        BinaryLEDsValue,

        [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", "Bitmaps", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16",
            ExampleExtraFormatArguments = new[] { "white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 2)]
        Bitmaps,

        [SouvenirQuestion("What color was the {1} button in {0}?", "Blind Maze", 4, "red", "blue", "yellow", "green", "gray",
            ExampleExtraFormatArguments = new[] { "north", "east", "west", "south" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BlindMazeColors,
        [SouvenirQuestion("Which maze did you solve {0} on?", "Blind Maze", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        BlindMazeMaze,

        [SouvenirQuestion("What was the last letter pressed on {0}?", "Blockbusters", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        BlockbustersLastLetter,

        [SouvenirQuestion("What were the letters on the screen in the last successful round of {0}?", "Blue Arrows", 6, "CA", "C1", "CB", "C8", "CF", "C4", "CE", "C6", "3A", "31", "3B", "38", "3F", "34", "3E", "36", "GA", "G1", "GB", "G8", "GF", "G4", "GE", "G6", "7A", "71", "7B", "78", "7F", "74", "7E", "76", "DA", "D1", "DB", "D8", "DF", "D4", "DE", "D6", "5A", "51", "5B", "58", "5F", "54", "5E", "56", "HA", "H1", "HB", "H8", "HF", "H4", "HE", "H6", "2A", "21", "2B", "28", "2F", "24", "2E", "26")]
        BlueArrowsInitialLetters,

        [SouvenirQuestion("What was the {1} indicator label in {0}?", "Bob Barks", 6, "BOB", "CAR", "CLR", "IND", "FRK", "FRQ", "MSA", "NSA", "SIG", "SND", "TRN", "BUB", "DOG", "ETC", "KEY",
            ExampleExtraFormatArguments = new[] { "top left", "top right", "bottom left", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BobBarksIndicators,
        [SouvenirQuestion("Which button flashed {1} in sequence in {0}?", "Bob Barks", 4, "top left", "top right", "bottom left", "bottom right",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BobBarksPositions,

        [SouvenirQuestion("What letter was initially visible on {0}?", "Boggle", 6, null, ExampleAnswers = new[] { "A", "E", "G", "M", "T", "W" })]
        BoggleLetters,

        [SouvenirQuestion("What was the final solution word in {0}?", "Braille", 6, "acting", "dating", "heading", "meaning", "server", "aiming", "dealer", "hearing", "miners", "shaking", "artist", "eating", "heating", "nearer", "sought", "asking", "eighth", "higher", "parish", "staying", "bearing", "farmer", "insist", "parker", "strands", "beating", "farming", "lasted", "parking", "strings", "beings", "faster", "laying", "paying", "teaching", "binding", "father", "leader", "powers", "tended", "bought", "finding", "leading", "pushed", "tender", "boxing", "finest", "leaned", "pushing", "testing", "breach", "finish", "leaning", "rather", "throwing", "breast", "flying", "leaving", "reaching", "towers", "breath", "foster", "linking", "reader", "vested", "breathe", "fought", "listed", "reading", "warned", "bringing", "gaining", "listen", "resting", "warning", "brings", "gather", "living", "riding", "weaker", "carers", "gazing", "making", "rushed", "wealth", "carter", "gender", "marked", "rushing", "winner", "charter", "growing", "marking", "saying", "winning", "crying", "headed", "master", "served", "winter")]
        BrailleWord,

        [SouvenirQuestion("What was the {1} correct button you pressed in {0}?", "Broken Buttons", 6, "bomb", "blast", "boom", "burst", "wire", "button", "module", "light", "led", "switch", "RJ-45", "DVI-D", "RCA", "PS/2", "serial", "port", "row", "column", "one", "two", "three", "four", "five", "six", "seven", "eight", "size", "this", "that", "other", "submit", "abort", "drop", "thing", "blank", "broken", "too", "to", "yes", "see", "sea", "c", "wait", "word", "bob", "no", "not", "first", "hold", "late", "fail",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BrokenButtons,

        [SouvenirQuestion("What was the color of the middle contact point in {0}?", "Brush Strokes", 6, "Red", "Orange", "Yellow", "Lime", "Green", "Cyan", "Sky", "Blue", "Purple", "Magenta", "Brown", "White", "Gray", "Black", "Pink")]
        BrushStrokesMiddleColor,

        [SouvenirQuestion("What were the correct button presses in {0}?", "Bulb", 6, "OOO", "OOI", "OIO", "OII", "IOO", "IOI", "IIO", "III", AddThe = true, Type = AnswerType.TicTacToeFont)]
        BulbButtonPresses,

        [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Burglar Alarm", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BurglarAlarmDigits,

        [SouvenirQuestion("What color did the light glow when you solved {0}?", "Button", 4, "red", "blue", "yellow", "white", AddThe = true)]
        ButtonLightColor,

        [SouvenirQuestion("How many of the buttons in {0} were {1}?", "Button Sequence", 6, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12",
            ExampleExtraFormatArguments = new[] { "red", "blue", "yellow", "white" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ButtonSequencesColorOccurrences,

        [SouvenirQuestion("What was the {1} in the last successful round of {0}?", "Caesar Cycle", 4, "Advanced", "Addition", "Allocate", "Altering", "Binaries", "Billions", "Bulkhead", "Bulleted", "Ciphered", "Circuits", "Computer", "Continue", "Decrypts", "Division", "Discover", "Disposal", "Encipher", "Entrance", "Equation", "Equipped", "Finished", "Findings", "Fortress", "Forwards", "Gauntlet", "Gambling", "Gathered", "Glooming", "Hazarded", "Haziness", "Hunkered", "Huntsman", "Indicate", "Indigoes", "Illusion", "Illumine", "Jigsawed", "Jimmying", "Junction", "Judgment", "Kilowatt", "Kinetics", "Knockout", "Knuckled", "Limiting", "Linearly", "Linkages", "Labeling", "Monogram", "Monotone", "Multiply", "Mulligan", "Nanogram", "Nanotube", "Numbered", "Numerals", "Octangle", "Octuples", "Observed", "Obscured", "Progress", "Projects", "Position", "Positive", "Quadrant", "Quadrics", "Quickest", "Quintics", "Reversed", "Revolved", "Rotation", "Relation", "Starting", "Standard", "Stopping", "Stopword", "Triggers", "Triangle", "Toggling", "Together", "Underrun", "Underlie", "Ultimate", "Ultrared", "Vicinity", "Viceless", "Voltages", "Volatile", "Wingding", "Winnable", "Whatever", "Whatnots", "Yellowed", "Yeasayer", "Yielding", "Yourself", "Zippered", "Zigzaggy", "Zugzwang", "Zymogram",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CaesarCycleWord,

        [SouvenirQuestion("What was the LED color in {0}?", "Calendar", 4, "Green", "Yellow", "Red", "Blue")]
        CalendarLedColor,

        [SouvenirQuestion("What was the {1} submitted answer in {0}?", "Challenge & Contact", 4, null, ExampleAnswers = new[] { "Accumulation", "Coffeebucks", "Perplexing", "Zoo", "Sunstone", "Bob" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChallengeAndContactAnswers,

        [SouvenirQuestion("What was the {1}paid amount in {0}?", "Cheap Checkout", 6, null, ExampleAnswers = new[] { "$11.00", "$12.00", "$14.00", "$17.00", "$24.00", "$25.00" },
            ExampleExtraFormatArguments = new[] { "", "first ", "second " }, ExampleExtraFormatArgumentGroupSize = 1)]
        CheapCheckoutPaid,

        [SouvenirQuestion("What was the {1} coordinate in {0}?", "Chess", 6, "a1", "a2", "a3", "a4", "a5", "a6", "b1", "b2", "b3", "b4", "b5", "b6", "c1", "c2", "c3", "c4", "c5", "c6", "d1", "d2", "d3", "d4", "d5", "d6", "e1", "e2", "e3", "e4", "e5", "e6", "f1", "f2", "f3", "f4", "f5", "f6",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChessCoordinate,

        [SouvenirQuestion("What color was the {1} LED in the last successful round of {0}?", "Chinese Counting", 4, "White", "Red", "Green", "Orange",
          ExampleExtraFormatArguments = new[] { "left", "right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChineseCountingLED,

        [SouvenirQuestion("Which note was part of the given chord in {0}?", "Chord Qualities", 6, "A", "A♯", "B", "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯")]
        ChordQualitiesNotes,

        [SouvenirQuestion("What was the given chord quality in {0}?", "Chord Qualities", 6, "7", "-7", "Δ7", "-Δ7", "7♯9", "ø", "add9", "-add9", "7♯5", "Δ7♯5", "7sus", "-Δ7♯5")]
        ChordQualitiesQuality,

        [SouvenirQuestion("What was the displayed number in {0}?", "Code", 6, null, AddThe = true, ExampleAnswers = new[] { "1234", "9876", "2345", "4567" })]
        CodeDisplayNumber,

        [SouvenirQuestion("What was the last written client name in {0}?", "Coffeebucks", 6, null, ExampleAnswers = new[] { "Gen", "Den" })]
        CoffeebucksClient,
        [SouvenirQuestion("What was the last served coffee in {0}?", "Coffeebucks", 4, null, ExampleAnswers = new[] { "The Blue Drink", "Twix Frappuccino" })]
        CoffeebucksCoffee,

        [SouvenirQuestion("What was the {1}-stage indicator pattern in {0}?", "Color Decoding", 4, "Checkered", "Horizontal", "Vertical", "Solid",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorDecodingIndicatorPattern,

        [SouvenirQuestion("Which color {1} in the {2}-stage indicator pattern in {0}?", "Color Decoding", 4, "Black", "Green", "Purple", "Red", "Blue", "Yellow",
            ExampleExtraFormatArguments = new[] { "appeared", "first", "appeared", "second", "appeared", "third", "did not appear", "first", "did not appear", "second", "did not appear", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        ColorDecodingIndicatorColors,

        [SouvenirQuestion("What was the displayed word in {0}?", "Colored Keys", 6, null, ExampleAnswers = new[] { "blue", "white" })]
        ColoredKeysDisplayWord,
        [SouvenirQuestion("What was the displayed word’s color in {0}?", "Colored Keys", 6, null, ExampleAnswers = new[] { "blue", "white" })]
        ColoredKeysDisplayWordColor,
        [SouvenirQuestion("What was the color of the {1} key in {0}?", "Colored Keys", 6, null, ExampleAnswers = new[] { "blue", "white" },
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColoredKeysKeyColor,
        [SouvenirQuestion("What letter was on the {1} key in {0}?", "Colored Keys", 6, null, ExampleAnswers = new[] { "A", "Z" },
            ExampleExtraFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColoredKeysKeyLetter,

        [SouvenirQuestion("What was the first color group in {0}?", "Colored Squares", 6, "White", "Red", "Blue", "Green", "Yellow", "Magenta")]
        ColoredSquaresFirstGroup,

        [SouvenirQuestion("What was the initial position of the switches in {0}?", "Colored Switches", 6, "RRRRR", "QRRRR", "RQRRR", "QQRRR", "RRQRR", "QRQRR", "RQQRR", "QQQRR", "RRRQR", "QRRQR", "RQRQR", "QQRQR", "RRQQR", "QRQQR", "RQQQR", "QQQQR", "RRRRQ", "QRRRQ", "RQRRQ", "QQRRQ", "RRQRQ", "QRQRQ", "RQQRQ", "QQQRQ", "RRRQQ", "QRRQQ", "RQRQQ", "QQRQQ", "RRQQQ", "QRQQQ", "RQQQQ", "QQQQQ",
            Type = AnswerType.SymbolsFont)]
        ColoredSwitchesInitialPosition,
        [SouvenirQuestion("What was the position of the switches when the LEDs came on in {0}?", "Colored Switches", 6, "RRRRR", "QRRRR", "RQRRR", "QQRRR", "RRQRR", "QRQRR", "RQQRR", "QQQRR", "RRRQR", "QRRQR", "RQRQR", "QQRQR", "RRQQR", "QRQQR", "RQQQR", "QQQQR", "RRRRQ", "QRRRQ", "RQRRQ", "QQRRQ", "RRQRQ", "QRQRQ", "RQQRQ", "QQQRQ", "RRRQQ", "QRRQQ", "RQRQQ", "QQRQQ", "RRQQQ", "QRQQQ", "RQQQQ", "QQQQQ",
            Type = AnswerType.SymbolsFont)]
        ColoredSwitchesWhenLEDsCameOn,

        [SouvenirQuestion("What was the color of the {1} LED in {0}?", "Color Morse", 6, "Blue", "Green", "Orange", "Purple", "Red", "Yellow", "White",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorMorseColor,

        [SouvenirQuestion("What character was flashed by the {1} LED in {0}?", "Color Morse", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorMorseCharacter,

        [SouvenirQuestion("What was the solution you selected first in {0}?", "Coordinates", 6, null, ExampleAnswers = new[] { "[4,7]", "C4", "<0, 2>", "3, 1", "(6,2)", "B-1", "“1, 0”", "4/3", "[12]", "#23", "四十七" })]
        CoordinatesFirstSolution,

        [SouvenirQuestion("What was the grid size in {0}?", "Coordinates", 6, "9", "15", "25", "21", "35", "49", "(9)", "(15)", "(21)", "(25)", "(35)", "(49)", "3 by 3", "4 by 3", "5 by 3", "6 by 3", "7 by 3", "3 by 4", "4 by 4", "5 by 4", "6 by 4", "7 by 4", "3 by 5", "4 by 5", "5 by 5", "6 by 5", "7 by 5", "3 by 6", "4 by 6", "5 by 6", "6 by 6", "7 by 6", "3 by 7", "4 by 7", "5 by 7", "6 by 7", "7 by 7", "9*3", "12*4", "15*5", "18*6", "21*7", "12*3", "16*4", "20*5", "24*6", "28*7", "15*3", "20*4", "25*5", "30*6", "35*7", "18*3", "24*4", "30*5", "36*6", "42*7", "21*3", "28*4", "35*5", "42*6", "49*7", "9 : 3", "12 : 3", "15 : 3", "18 : 3", "21 : 3", "12 : 4", "16 : 4", "20 : 4", "24 : 4", "28 : 4", "15 : 5", "20 : 5", "25 : 5", "30 : 5", "35 : 5", "18 : 6", "24 : 6", "30 : 6", "36 : 6", "42 : 6", "21 : 7", "28 : 7", "35 : 7", "42 : 7", "49 : 7", "3×3", "3×4", "3×5", "3×6", "3×7", "4×3", "4×4", "4×5", "4×6", "4×7", "5×3", "5×4", "5×5", "5×6", "5×7", "6×3", "6×4", "6×5", "6×6", "6×7", "7×3", "7×4", "7×5", "7×6", "7×7")]
        CoordinatesSize,

        [SouvenirQuestion("What was in square {1}{2} in the last round of {0}?", "Crackbox", 4, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "white", "black",
            ExampleExtraFormatArguments = new[] { "A", "1", "A", "2", "B", "1", "B", "2" }, ExampleExtraFormatArgumentGroupSize = 2)]
        CrackboxInitialState,

        [SouvenirQuestion("What were the weather conditions on the {1} day in {0}?", "Creation", 4, "Clear", "Heat Wave", "Meteor Shower", "Rain", "Windy",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CreationWeather,

        [SouvenirQuestion("What was the {1} in the last successful round of {0}?", "Cryptic Cycle", 4, "Advanced", "Addition", "Allocate", "Altering", "Binaries", "Billions", "Bulkhead", "Bulleted", "Ciphered", "Circuits", "Computer", "Continue", "Decrypts", "Division", "Discover", "Disposal", "Examined", "Examples", "Equation", "Equipped", "Finished", "Findings", "Fortress", "Forwards", "Gauntlet", "Gambling", "Gathered", "Glooming", "Hazarded", "Haziness", "Hunkered", "Huntsman", "Indicate", "Indigoes", "Illusion", "Illumine", "Jigsawed", "Jimmying", "Junction", "Judgment", "Kilowatt", "Kinetics", "Knockout", "Knuckled", "Limiting", "Linearly", "Linkages", "Labeling", "Monogram", "Monotone", "Multiply", "Mulligan", "Nanogram", "Nanotube", "Numbered", "Numerals", "Octangle", "Octuples", "Observed", "Obscured", "Progress", "Projects", "Position", "Positive", "Quadrant", "Quadplex", "Quickest", "Quintics", "Reversed", "Revolved", "Rotation", "Relation", "Starting", "Standard", "Stopping", "Stopword", "Triggers", "Triangle", "Toggling", "Together", "Underrun", "Underlie", "Ultimate", "Ultrared", "Vicinity", "Viceless", "Voltages", "Volatile", "Wingding", "Winnable", "Whatever", "Whatnots", "Yellowed", "Yeasayer", "Yielding", "Yourself", "Zippered", "Zigzaggy", "Zugzwang", "Zymogram",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        CrypticCycleWord,

        [SouvenirQuestion("What was the {1} cube rotation in {0}?", "Cube", 4, "rotate cw", "tip left", "tip backwards", "rotate ccw", "tip right", "tip forwards",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        CubeRotations,

        [SouvenirQuestion("What region did you depart from in {0}?", "DACH Maze", 4, "Burgenland, A", "Carinthia, A", "Lower Austria, A", "North Tyrol, A", "Upper Austria, A", "East Tyrol, A", "Salzburg, A", "Styria, A", "Vorarlberg, A", "Vienna, A", "Aargau, CH", "Appenzell Inner Rhodes, CH", "Appenzell Outer Rhodes, CH", "Basel Country, CH", "Bern, CH", "Basel City, CH", "Fribourg, CH", "Geneva, CH", "Glarus, CH", "Grisons, CH", "Jura, CH", "Luzern, CH", "Nidwalden, CH", "Neuchâtel, CH", "Obwalden, CH", "Schaffhausen, CH", "St. Gallen, CH", "Solothurn, CH", "Schwyz, CH", "Thurgau, CH", "Ticino, CH", "Uri, CH", "Vaud, CH", "Valais, CH", "Zug, CH", "Zürich, CH", "Brandenburg, D", "Berlin, D", "Baden-Württemberg, D", "Bavaria, D", "Bremen, D", "Hesse, D", "Hamburg, D", "Mecklenburg-Vorpommern, D", "Lower Saxony, D", "North Rhine-Westphalia, D", "Rhineland-Palatinate, D", "Schleswig-Holstein, D", "Saarland, D", "Saxony, D", "Saxony-Anhalt, D", "Thuringia, D", "Liechtenstein")]
        DACHMazeOrigin,

        [SouvenirQuestion("What deck did the first card of {0} belong to?", "Deck of Many Things", 4, "Standard", "Metropolitan", "Maritime", "Arctic", "Tropical", "Oasis", "Celestial", AddThe = true)]
        DeckOfManyThingsFirstCard,

        [SouvenirQuestion("What was the starting {1} defining color in {0}?", "Decolored Squares", 6, "White", "Red", "Blue", "Green", "Yellow", "Magenta",
            ExampleExtraFormatArguments = new[] { "column", "row" }, ExampleExtraFormatArgumentGroupSize = 1)]
        DecoloredSquaresStartingPos,

        [SouvenirQuestion("What was {1}’s remembered position in {0}?", "Discolored Squares", 6, null, Type = AnswerType.Sprites,
            ExampleExtraFormatArguments = new[] { "Blue", "Red", "Yellow", "Green", "Magenta" }, ExampleExtraFormatArgumentGroupSize = 1)]
        DiscoloredSquaresRememberedPositions,

        [SouvenirQuestion("What was the screen color on the {1} stage of {0}?", "Double Color", 5, "Green", "Blue", "Red", "Pink", "Yellow",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        DoubleColorColors,

        [SouvenirQuestion("Which button was the submit button in {0}?", "Double-Oh", 6, "↕", "⇕", "↔", "⇔", "◆")]
        DoubleOhSubmitButton,

        [SouvenirQuestion("Which of these symptoms was listed on {0}?", "Dr. Doctor", 4, "Bloating", "Chills", "Cold Hands", "Constipation", "Cough", "Diarrhea", "Disappearance of the Ears", "Dizziness", "Excessive Crying", "Fatigue", "Fever", "Foot swelling", "Gas", "Hallucination", "Headache", "Loss of Smell", "Muscle Cramp", "Nausea", "Numbness", "Shortness of Breath", "Sleepiness", "Thirstiness", "Throat irritation")]
        DrDoctorSymptoms,
        [SouvenirQuestion("Which of these diseases was listed on {0}, but not the one treated?", "Dr. Doctor", 4, "Alztimer’s", "Braintenance", "Color allergy", "Detonession", "Emojilepsy", "Foot and Morse", "Gout of Life", "HRV", "Indicitis", "Jaundry", "Keypad stones", "Legomania", "Microcontusion", "Narcolization", "OCd", "Piekinson’s", "Quackgrounds", "Royal Flu", "Seizure Siphor", "Tetrinus", "Urinary LEDs", "Verticode", "Widgeting", "XMAs", "Yes-no infection", "Zooties", "Chronic Talk", "Jukepox", "Neurolysis", "Perspective Loss", "Orientitis", "Huntington’s disease")]
        DrDoctorDiseases,

        [SouvenirQuestion("What was the {1} rune shown on {0}?", "Elder Futhark", 4, "Algiz", "Ansuz", "Berkana", "Dagaz", "Ehwaz", "Eihwaz", "Fehu", "Gebo", "Hagalaz", "Isa", "Jera", "Kenaz", "Laguz", "Mannaz", "Nauthiz", "Othila", "Perthro", "Raido", "Sowulo", "Teiwaz", "Thurisaz", "Uruz", "Wunjo",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ElderFutharkRunes,

        [SouvenirQuestion("What was the {1} on {0}?", "Encrypted Morse", 4, null, ExampleAnswers = new[] { "Detonate", "Ready Now", "Please No", "Cheesecake" },
            ExampleExtraFormatArguments = new[] { "received call", "sent response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        EncryptedMorseCallResponse,

        [SouvenirQuestion("What was the displayed symbol in {0}?", "Equations X", 6, "H(T)", "P", "\u03C7", "\u03C9", "Z(T)", "\u03C4", "\u03BC", "\u03B1", "K")]
        EquationsXSymbols,

        [SouvenirQuestion("What was the beat for the {1} arrow from the bottom in {0}?", "Etterna", 6, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        EtternaNumber,

        [SouvenirQuestion("What room did you start in in the last generated maze of {0}?", "Factory Maze", 4, "Bathroom", "Assembly Line", "Cafeteria", "Room A9", "Broom Closet", "Basement", "Copy Room", "Unnecessarily...", "Library", "Break Room", "Empty Room...", "Arcade", "Classroom", "Module Testing...", "Music Studio", "Computer Room", "Infirmary", "Bomb Room", "Space", "Storage Room", "Lounge", "Conference Room", "Kitchen", "Incinerator")]
        FactoryMazeStartRoom,

        [SouvenirQuestion("What was the last pair of letters in {0}?", "Fast Math", 6, "AA", "AB", "AC", "AD", "AE", "AG", "AK", "AN", "AP", "AS", "AT", "AX", "AZ", "BA", "BB", "BC", "BD", "BE", "BG", "BK", "BN", "BP", "BS", "BT", "BX", "BZ", "CA", "CB", "CC", "CD", "CE", "CG", "CK", "CN", "CP", "CS", "CT", "CX", "CZ", "DA", "DB", "DC", "DD", "DE", "DG", "DK", "DN", "DP", "DS", "DT", "DX", "DZ", "EA", "EB", "EC", "ED", "EE", "EG", "EK", "EN", "EP", "ES", "ET", "EX", "EZ", "GA", "GB", "GC", "GD", "GE", "GG", "GK", "GN", "GP", "GS", "GT", "GX", "GZ", "KA", "KB", "KC", "KD", "KE", "KG", "KK", "KN", "KP", "KS", "KT", "KX", "KZ", "NA", "NB", "NC", "ND", "NE", "NG", "NK", "NN", "NP", "NS", "NT", "NX", "NZ", "PA", "PB", "PC", "PD", "PE", "PG", "PK", "PN", "PP", "PS", "PT", "PX", "PZ", "SA", "SB", "SC", "SD", "SE", "SG", "SK", "SN", "SP", "SS", "ST", "SX", "SZ", "TA", "TB", "TC", "TD", "TE", "TG", "TK", "TN", "TP", "TS", "TT", "TX", "TZ", "XA", "XB", "XC", "XD", "XE", "XG", "XK", "XN", "XP", "XS", "XT", "XX", "XZ", "ZA", "ZB", "ZC", "ZD", "ZE", "ZG", "ZK", "ZN", "ZP", "ZS", "ZT", "ZX", "ZZ")]
        FastMathLastLetters,

        [SouvenirQuestion("What was the exit coordinate in {0}?", "Faulty RGB Maze", 6, "A1", "A2", "A3", "A4", "A5", "A6", "A7", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "G1", "G2", "G3", "G4", "G5", "G6", "G7")]
        FaultyRGBMazeExit,
        [SouvenirQuestion("Where was the {1} key in {0}?", "Faulty RGB Maze", 6, "A1", "A2", "A3", "A4", "A5", "A6", "A7", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "G1", "G2", "G3", "G4", "G5", "G6", "G7",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        FaultyRGBMazeKeys,
        [SouvenirQuestion("Which maze number was the {1} maze in {0}?", "Faulty RGB Maze", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        FaultyRGBMazeNumber,

        [SouvenirQuestion("What was the displayed number in {0}?", "Flags", 6, "1", "2", "3", "4", "5", "6", "7")]
        FlagsDisplayedNumber,
        [SouvenirQuestion("What was the main country flag in {0}?", "Flags", 6, null, Type = AnswerType.Sprites)]
        FlagsMainCountry,
        [SouvenirQuestion("Which of these country flags was shown, but not the main country flag, in {0}?", "Flags", 6, null, Type = AnswerType.Sprites)]
        FlagsCountries,

        [SouvenirQuestion("How many times did the {1} LED flash {2} on {0}?", "Flashing Lights", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12",
            ExampleExtraFormatArguments = new[] { "top", "blue", "top", "green", "top", "red", "top", "purple", "top", "orange", "bottom", "blue", "bottom", "green", "bottom", "red", "bottom", "purple", "bottom", "orange" }, ExampleExtraFormatArgumentGroupSize = 2)]
        FlashingLightsLEDFrequency,

        [SouvenirQuestion("What number was on the gear during stage {1} in {0}?", "Forget the Colors", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetTheColorsGearNumber,
        [SouvenirQuestion("What number was on the large display during stage {1} in {0}?", "Forget the Colors", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", "111", "112", "113", "114", "115", "116", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127", "128", "129", "130", "131", "132", "133", "134", "135", "136", "137", "138", "139", "140", "141", "142", "143", "144", "145", "146", "147", "148", "149", "150", "151", "152", "153", "154", "155", "156", "157", "158", "159", "160", "161", "162", "163", "164", "165", "166", "167", "168", "169", "170", "171", "172", "173", "174", "175", "176", "177", "178", "179", "180", "181", "182", "183", "184", "185", "186", "187", "188", "189", "190", "191", "192", "193", "194", "195", "196", "197", "198", "199", "200", "201", "202", "203", "204", "205", "206", "207", "208", "209", "210", "211", "212", "213", "214", "215", "216", "217", "218", "219", "220", "221", "222", "223", "224", "225", "226", "227", "228", "229", "230", "231", "232", "233", "234", "235", "236", "237", "238", "239", "240", "241", "242", "243", "244", "245", "246", "247", "248", "249", "250", "251", "252", "253", "254", "255", "256", "257", "258", "259", "260", "261", "262", "263", "264", "265", "266", "267", "268", "269", "270", "271", "272", "273", "274", "275", "276", "277", "278", "279", "280", "281", "282", "283", "284", "285", "286", "287", "288", "289", "290", "291", "292", "293", "294", "295", "296", "297", "298", "299", "300", "301", "302", "303", "304", "305", "306", "307", "308", "309", "310", "311", "312", "313", "314", "315", "316", "317", "318", "319", "320", "321", "322", "323", "324", "325", "326", "327", "328", "329", "330", "331", "332", "333", "334", "335", "336", "337", "338", "339", "340", "341", "342", "343", "344", "345", "346", "347", "348", "349", "350", "351", "352", "353", "354", "355", "356", "357", "358", "359", "360", "361", "362", "363", "364", "365", "366", "367", "368", "369", "370", "371", "372", "373", "374", "375", "376", "377", "378", "379", "380", "381", "382", "383", "384", "385", "386", "387", "388", "389", "390", "391", "392", "393", "394", "395", "396", "397", "398", "399", "400", "401", "402", "403", "404", "405", "406", "407", "408", "409", "410", "411", "412", "413", "414", "415", "416", "417", "418", "419", "420", "421", "422", "423", "424", "425", "426", "427", "428", "429", "430", "431", "432", "433", "434", "435", "436", "437", "438", "439", "440", "441", "442", "443", "444", "445", "446", "447", "448", "449", "450", "451", "452", "453", "454", "455", "456", "457", "458", "459", "460", "461", "462", "463", "464", "465", "466", "467", "468", "469", "470", "471", "472", "473", "474", "475", "476", "477", "478", "479", "480", "481", "482", "483", "484", "485", "486", "487", "488", "489", "490", "491", "492", "493", "494", "495", "496", "497", "498", "499", "500", "501", "502", "503", "504", "505", "506", "507", "508", "509", "510", "511", "512", "513", "514", "515", "516", "517", "518", "519", "520", "521", "522", "523", "524", "525", "526", "527", "528", "529", "530", "531", "532", "533", "534", "535", "536", "537", "538", "539", "540", "541", "542", "543", "544", "545", "546", "547", "548", "549", "550", "551", "552", "553", "554", "555", "556", "557", "558", "559", "560", "561", "562", "563", "564", "565", "566", "567", "568", "569", "570", "571", "572", "573", "574", "575", "576", "577", "578", "579", "580", "581", "582", "583", "584", "585", "586", "587", "588", "589", "590", "591", "592", "593", "594", "595", "596", "597", "598", "599", "600", "601", "602", "603", "604", "605", "606", "607", "608", "609", "610", "611", "612", "613", "614", "615", "616", "617", "618", "619", "620", "621", "622", "623", "624", "625", "626", "627", "628", "629", "630", "631", "632", "633", "634", "635", "636", "637", "638", "639", "640", "641", "642", "643", "644", "645", "646", "647", "648", "649", "650", "651", "652", "653", "654", "655", "656", "657", "658", "659", "660", "661", "662", "663", "664", "665", "666", "667", "668", "669", "670", "671", "672", "673", "674", "675", "676", "677", "678", "679", "680", "681", "682", "683", "684", "685", "686", "687", "688", "689", "690", "691", "692", "693", "694", "695", "696", "697", "698", "699", "700", "701", "702", "703", "704", "705", "706", "707", "708", "709", "710", "711", "712", "713", "714", "715", "716", "717", "718", "719", "720", "721", "722", "723", "724", "725", "726", "727", "728", "729", "730", "731", "732", "733", "734", "735", "736", "737", "738", "739", "740", "741", "742", "743", "744", "745", "746", "747", "748", "749", "750", "751", "752", "753", "754", "755", "756", "757", "758", "759", "760", "761", "762", "763", "764", "765", "766", "767", "768", "769", "770", "771", "772", "773", "774", "775", "776", "777", "778", "779", "780", "781", "782", "783", "784", "785", "786", "787", "788", "789", "790", "791", "792", "793", "794", "795", "796", "797", "798", "799", "800", "801", "802", "803", "804", "805", "806", "807", "808", "809", "810", "811", "812", "813", "814", "815", "816", "817", "818", "819", "820", "821", "822", "823", "824", "825", "826", "827", "828", "829", "830", "831", "832", "833", "834", "835", "836", "837", "838", "839", "840", "841", "842", "843", "844", "845", "846", "847", "848", "849", "850", "851", "852", "853", "854", "855", "856", "857", "858", "859", "860", "861", "862", "863", "864", "865", "866", "867", "868", "869", "870", "871", "872", "873", "874", "875", "876", "877", "878", "879", "880", "881", "882", "883", "884", "885", "886", "887", "888", "889", "890", "891", "892", "893", "894", "895", "896", "897", "898", "899", "900", "901", "902", "903", "904", "905", "906", "907", "908", "909", "910", "911", "912", "913", "914", "915", "916", "917", "918", "919", "920", "921", "922", "923", "924", "925", "926", "927", "928", "929", "930", "931", "932", "933", "934", "935", "936", "937", "938", "939", "940", "941", "942", "943", "944", "945", "946", "947", "948", "949", "950", "951", "952", "953", "954", "955", "956", "957", "958", "959", "960", "961", "962", "963", "964", "965", "966", "967", "968", "969", "970", "971", "972", "973", "974", "975", "976", "977", "978", "979", "980", "981", "982", "983", "984", "985", "986", "987", "988", "989", "990",
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetTheColorsLargeDisplay,
        [SouvenirQuestion("What was the last decimal in the sine number recieved during stage {1} in {0}?", "Forget the Colors", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetTheColorsSineNumber,
        [SouvenirQuestion("What color was the gear during stage {1} in {0}?", "Forget the Colors", 6, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray",
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetTheColorsGearColor,
        [SouvenirQuestion("Which edgework-based rule was applied to the total number during stage {1} in {0}?", "Forget the Colors", 6, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray",
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ForgetTheColorsRuleColor,

        [SouvenirQuestion("What was the player token in {0}?", "Free Parking", 6, "Dog", "Wheelbarrow", "Cat", "Iron", "Top Hat", "Car", "Battleship")]
        FreeParkingToken,

        [SouvenirQuestion("What was the last digit of your first query’s result in {0}?", "Functions", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        FunctionsLastDigit,
        [SouvenirQuestion("What number was to the left of the displayed letter in {0}?", "Functions", 6, null, ExampleAnswers = new[] { "123", "847", "14", "73", "961", "9", "858" })]
        FunctionsLeftNumber,
        [SouvenirQuestion("What letter was displayed in {0}?", "Functions", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        FunctionsLetter,
        [SouvenirQuestion("What number was to the right of the displayed letter in {0}?", "Functions", 6, null, ExampleAnswers = new[] { "321", "747", "47", "31", "169", "8", "858" })]
        FunctionsRightNumber,

        [SouvenirQuestion("What were the numbers on {0}?", "Gamepad", 6, null, AddThe = true, ExampleAnswers = new[] { "01:03", "12:92", "84:72", "24:56" })]
        GamepadNumbers,

        [SouvenirQuestion("What was the color of the liquid you drank in {0}?", "Giant’s Drink", 6, "Red", "Blue", "Green", "Orange", "Purple", "Cyan", AddThe = true)]
        GiantsDrinkLiquid,

        [SouvenirQuestion("What was the last number on the display on {0}?", "Green Arrows", 6, "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99")]
        GreenArrowsLastScreen,

        [SouvenirQuestion("What was the starting location in {0}?", "Gridlock", 6, "A1", "B1", "C1", "D1", "A2", "B2", "C2", "D2", "A3", "B3", "C3", "D3", "A4", "B4", "C4", "D4")]
        GridLockStartingLocation,

        [SouvenirQuestion("What was the ending location in {0}?", "Gridlock", 6, "A1", "B1", "C1", "D1", "A2", "B2", "C2", "D2", "A3", "B3", "C3", "D3", "A4", "B4", "C4", "D4")]
        GridLockEndingLocation,

        [SouvenirQuestion("What was the starting color in {0}?", "Gridlock", 4, "Green", "Yellow", "Red", "Blue")]
        GridLockStartingColor,

        [SouvenirQuestion("What was the gryphon’s name in {0}?", "Gryphons", 6, "Gabe", "Gabriel", "Gad", "Gael", "Gage", "Gaia", "Galena", "Galina", "Gallo", "Gallagher", "Ganymede", "Ganzorig", "Garen", "Gareth", "Garland", "Garnett", "Garret", "Garrick", "Gary", "Gaspar", "Gaston", "Gauthier", "Gavin", "Gaz", "Geena", "Geff", "Geffrey", "Gela", "Geltrude", "Gene", "Geneva", "Genevieve", "Geno", "Gentius", "Geoff", "George", "Georgio", "Georgius", "Gerald", "Geraldo", "Gerda", "Gerel", "Gergana", "Gerhardt", "Gerhart", "Gerry", "Gertrude", "Gervais", "Gervaise", "Ghada", "Ghadir", "Ghassan", "Ghjulia", "Gia", "Giada", "Giampaolo", "Giampiero", "Giancarlo", "Giana", "Gianna", "Gideon", "Gidon", "Gilbert", "Gilberta", "Gino", "Giorgio", "Giovanni", "Giove", "Girish", "Girisha", "Gisela", "Giselle", "Gittel", "Gizella", "Gjorgji", "Gladys", "Glauco", "Glaukos", "Glen", "Glenn", "Godfrey", "Godfried", "Gojko", "Gol", "Golda", "Gona", "Gonzalo", "Gordie", "Gordy", "Goretti", "Gosia", "Gosse", "Gotzon", "Gotzone", "Gowri", "Gozzo", "Grace", "Gracia", "Griffith", "Gwynnyth")]
        GryphonsName,
        [SouvenirQuestion("What was the gryphon’s age in {0}?", "Gryphons", 6, "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34")]
        GryphonsAge,

        [SouvenirQuestion("What was the given number in {0}?", "Hereditary Base Notation", 4, null, ExampleAnswers = new[] { "12", "33", "46", "112", "356" })]
        HereditaryBaseNotationInitialNumber,

        [SouvenirQuestion("What label was printed on {0}?", "Hexabutton", 6, "Jump", "Boom", "Claim", "Button", "Hold", "Blue", AddThe = true)]
        HexabuttonLabel,

        [SouvenirQuestion("What was the color of the pawn in {0}?", "Hexamaze", 6, "Red", "Yellow", "Green", "Cyan", "Blue", "Pink")]
        HexamazePawnColor,

        [SouvenirQuestion("What was the color of the main LED in {0}?", "Hidden Colors", 6, "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Magenta", "White")]
        HiddenColorsLED,

        [SouvenirQuestion("What was the {1} in the last successful round of {0}?", "Hill Cycle", 4, "Adverted", "Advocate", "Allocate", "Altering", "Binormal", "Binomial", "Bulkhead", "Bulleted", "Ciphered", "Circuits", "Compiler", "Commando", "Decimate", "Deceived", "Discover", "Disposal", "Encipher", "Entrance", "Equators", "Equalise", "Finalise", "Finnicky", "Fortress", "Forwards", "Gauntlet", "Gambling", "Gatepost", "Gateways", "Hazarded", "Haziness", "Hungrier", "Huntress", "Incoming", "Indirect", "Illusion", "Illumine", "Jigsawed", "Jiggling", "Junction", "Junkyard", "Kilowatt", "Kilobyte", "Knockout", "Knocking", "Lingered", "Linearly", "Linkages", "Linkwork", "Monogram", "Monomial", "Multiply", "Multiton", "Nanogram", "Nanowatt", "Numerous", "Numerals", "Ordinals", "Ordering", "Observed", "Obscured", "Progress", "Projects", "Prophase", "Prophecy", "Quadrant", "Quadrics", "Quartile", "Quartics", "Reversed", "Revolved", "Rotators", "Relaying", "Stanzaic", "Standout", "Stopping", "Stopword", "Trigonal", "Trickier", "Toggling", "Together", "Underway", "Underlie", "Ultrahot", "Ultrared", "Vicinity", "Viceless", "Volition", "Volatile", "Whatness", "Whatsits", "Whatever", "Whatnots", "Yearlong", "Yeasayer", "Yokozuna", "Yourself", "Zippered", "Zygomata", "Zugzwang", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HillCycleWord,

        [SouvenirQuestion("Which House was {1} solved\u00a0for in {0}?", "Hogwarts", 4, "Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw",
            ExampleExtraFormatArguments = new[] { "Binary Puzzle", "Zoni", "R.-P.-S.- L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HogwartsHouse,
        [SouvenirQuestion("Which module was solved\u00a0for {1} in {0}?", "Hogwarts", 4, null, ExampleAnswers = new[] { "Binary Puzzle", "Zoni", "R.-P.-S.-L.-Sp.", "M.A. Humanity", "Monsplode Tr.C." },
            ExampleExtraFormatArguments = new[] { "Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HogwartsModule,

        [SouvenirQuestion("In what position was the button pressed on the {1} stage of {0}?", "Horrible Memory", 6, "1", "2", "3", "4", "5", "6",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HorribleMemoryPositions,
        [SouvenirQuestion("What was the label of the button pressed on the {1} stage of {0}?", "Horrible Memory", 6, "1", "2", "3", "4", "5", "6",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HorribleMemoryLabels,
        [SouvenirQuestion("What color was the button pressed on the {1} stage of {0}?", "Horrible Memory", 6, "blue", "green", "red", "orange", "purple", "pink",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HorribleMemoryColors,

        [SouvenirQuestion("Who was {1} in {0}?", "Human Resources", 6, "Rebecca", "Damian", "Jean", "Mike", "River", "Samuel", "Yoshi", "Caleb", "Ashley", "Tim", "Eliott", "Ursula", "Silas", "Noah", "Quinn", "Dylan",
            ExampleExtraFormatArguments = new[] { "an employee that was not fired", "an applicant that was not hired" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HumanResourcesEmployees,

        [SouvenirQuestion("Which was a descriptor shown in {1} in {0}?", "Human Resources", 4, "Intellectual", "Deviser", "Confidant", "Helper", "Auditor", "Innovator", "Defender", "Chameleon", "Director", "Designer", "Educator", "Advocate", "Manager", "Showman", "Contributor", "Entertainer",
            ExampleExtraFormatArguments = new[] { "red", "green" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HumanResourcesDescriptors,

        [SouvenirQuestion("Which of the first three stages of {0} had the {1} symbol {2}?", "Hunting", 4, "none", "first", "second", "first two", "third", "first & third", "second & third", "all three",
            ExampleExtraFormatArguments = new[] { "column", "first", "row", "first", "column", "second", "row", "second", "column", "third", "row", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        HuntingColumnsRows,

        [SouvenirQuestion("What was the {1} rotation in {0}?", "Hypercube", 6, "XY", "YX", "XZ", "ZX", "XW", "WX", "YZ", "ZY", "YW", "WY", "ZW", "WZ", AddThe = true,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HypercubeRotations,

        [SouvenirQuestion("Which one of these flavours {1} to the {2} customer in {0}?", "Ice Cream", 4, "Tutti Frutti", "Rocky Road", "Raspb. Ripple", "Double Choc.", "Double Str.", "Cookies & Cr.", "Neapolitan", "Mint Ch. Chip", "The Classic", "Vanilla",
            ExampleExtraFormatArguments = new[] { "was on offer, but not sold,", "first", "was not on offer", "first", "was on offer, but not sold,", "second", "was not on offer", "second", "was on offer, but not sold,", "third", "was not on offer", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        IceCreamFlavour,
        [SouvenirQuestion("Who was the {1} customer in {0}?", "Ice Cream", 6, "Mike", "Tim", "Tom", "Dave", "Adam", "Cheryl", "Sean", "Ashley", "Jessica", "Taylor", "Simon", "Sally", "Jade", "Sam", "Gary", "Victor", "George", "Jacob", "Pat", "Bob",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        IceCreamCustomer,

        [SouvenirQuestion("Which hair color {1} listed in {0}?", "Identity Parade", 4, "Black", "Blonde", "Brown", "Grey", "Red", "White",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        IdentityParadeHairColors,
        [SouvenirQuestion("Which build {1} listed in {0}?", "Identity Parade", 4, "Fat", "Hunched", "Muscular", "Short", "Slim", "Tall",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        IdentityParadeBuilds,
        [SouvenirQuestion("Which attire {1} listed in {0}?", "Identity Parade", 4, "Blazer", "Hoodie", "Jumper", "Suit", "T-shirt", "Tank top",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        IdentityParadeAttires,

        [SouvenirQuestion("What was the {1} PIN digit in {0}?", "iPhone", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        iPhoneDigits,

        [SouvenirQuestion("What number was wheel {1} in {0}?", "Jewel Vault", 4, "1", "2", "3", "4",
            ExampleExtraFormatArguments = new[] { "A", "B", "C", "D" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        JewelVaultWheels,

        [SouvenirQuestion("What was the {1} in the last successful round of {0}?", "Jumble Cycle", 4, "Adverted", "Advocate", "Allotype", "Allotted", "Binormal", "Binomial", "Bullhorn", "Bulwarks", "Connects", "Conquers", "Commando", "Compiler", "Deceived", "Decimate", "Dispatch", "Discrete", "Encrypts", "Encoding", "Equators", "Equalise", "Finalise", "Finnicky", "Formulae", "Fortunes", "Garrison", "Garnered", "Gatepost", "Gateways", "Hotlinks", "Hotheads", "Huntress", "Hundreds", "Incoming", "Indirect", "Illusory", "Illuding", "Journeys", "Jousting", "Junkyard", "Juncture", "Kilovolt", "Kilobyte", "Knocking", "Knowable", "Language", "Landmark", "Linkwork", "Lingered", "Monomial", "Monolith", "Multiton", "Mulcting", "Nanowatt", "Nanobots", "Numerous", "Numerate", "Ordering", "Ordinals", "Obstruct", "Obstacle", "Prophase", "PROPHECY", "Postsync", "Positron", "Quartile", "Quartics", "Quirkish", "Quitters", "Reversed", "Revealed", "Relaying", "Relative", "Stanzaic", "Standout", "Stockade", "Stoccata", "Trigonal", "Trickier", "Tomogram", "Tomahawk", "Underway", "Undoings", "Ulterior", "Ultrahot", "Venomous", "Vendetta", "Volition", "Voluming", "Weakened", "Weaponed", "Whatness", "Whatsits", "Yearlong", "Yearning", "Yokozuna", "Yourself", "Zygomata", "Zygotene", "Zymology", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        JumbleCycleWord,

        [SouvenirQuestion("Which square was {1} in {0}?", "Kudosudoku", 6, null, Type = AnswerType.Sprites,
            ExampleExtraFormatArguments = new[] { "pre-filled", "not pre-filled" }, ExampleExtraFormatArgumentGroupSize = 1)]
        KudosudokuPrefilled,

        [SouvenirQuestion("What was the number on the {1} hatch on {0}?", "Lasers", 6, "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LasersHatches,

        [SouvenirQuestion("What was the correct letter you pressed in the {1} stage of {0}?", "LED Encryption", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LEDEncryptionPressedLetters,

        [SouvenirQuestion("What color was {1} in {0}?", "LED Math", 4, "Red", "Blue", "Yellow", "Green",
            ExampleExtraFormatArguments = new[] { "LED A", "LED B", "the operator LED" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LEDMathLights,

        [SouvenirQuestion("What were the dimensions of the {1} piece in {0}?", "LEGOs", 5, "2×2", "3×1", "3×2", "4×1", "4×2",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue", "cyan", "magenta", "yellow" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LEGOsPieceDimensions,

        [SouvenirQuestion("What was the correct code you entered in {0}?", "Listening", 6, null,
            ExampleAnswers = new[] { "&&&**", "&$#$&", "$#$*&", "#$$**", "$#$#*", "**$*#", "#$$&*", "##*$*", "$#*$&", "**#**", "#&&*#", "&#**&", "$&**#", "&#$$#", "$&&**", "#&$##", "&*$*$", "&$$&*", "#&&&&", "**$$$", "*&*&&", "*#&*&", "**###", "&&$&*", "&$**&", "#$#&$", "&#&&#", "$$*$*", "$&#$$", "&**$$", "$&&*&", "&$&##", "#&$*&", "$*$**", "*#$&&", "###&$", "*$$&$", "$*&##", "#&$&&", "$&$$*", "*$*$*" })]
        Listening,

        [SouvenirQuestion("What was the initial color of the {1} button in the {2} successful stage of {0}?", "Logical Buttons", 4, "Red", "Blue", "Green", "Yellow", "Purple", "White", "Orange", "Cyan", "Grey",
            ExampleExtraFormatArguments = new[] { "top", "first", "bottom-left", "first", "bottom-right", "first", "top", "second", "bottom-left", "second", "bottom-right", "second", "top", "third", "bottom-left", "third", "bottom-right", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        LogicalButtonsColor,
        [SouvenirQuestion("What was the initial label on the {1} button in the {2} successful stage of {0}?", "Logical Buttons", 4, "Logic", "Color", "Label", "Button", "Wrong", "Boom", "No", "Wait", "Hmmm",
            ExampleExtraFormatArguments = new[] { "top", "first", "bottom-left", "first", "bottom-right", "first", "top", "second", "bottom-left", "second", "bottom-right", "second", "top", "third", "bottom-left", "third", "bottom-right", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        LogicalButtonsLabel,
        [SouvenirQuestion("What was the final operator in the {1} successful stage of {0}?", "Logical Buttons", 6, "AND", "OR", "XOR", "NAND", "NOR", "XNOR",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LogicalButtonsOperator,

        [SouvenirQuestion("What was {1} in {0}?", "Logic Gates", 6, "AND", "OR", "XOR", "NAND", "NOR", "XNOR",
            ExampleExtraFormatArguments = new[] { "gate A", "gate B", "gate C", "gate D", "gate E", "gate F", "gate G", "the duplicated gate" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LogicGatesGates,

        [SouvenirQuestion("What was the first word in the name of the {1} {2} station successfully travelled {3} on {0}?", "London Underground", 4, null, AddThe = true, ExampleAnswers = new[] { "Waterloo", "Neasden", "Euston", "Victoria" },
            ExampleExtraFormatArguments = new[] { "first", "departure", "from", "second", "departure", "from", "first", "destination", "to", "second", "destination", "to" }, ExampleExtraFormatArgumentGroupSize = 3)]
        LondonUndergroundStations,

        [SouvenirQuestion("Which tile was part of the {1} matched pair in {0}?", "Mahjong", 6, null, ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.Sprites)]
        MahjongMatches,
        [SouvenirQuestion("Which tile was shown in the bottom-left of {0}?", "Mahjong", 6, null, Type = AnswerType.Sprites)]
        MahjongCountingTile,

        [SouvenirQuestion("Who was a player, but not the Godfather, in the last stage of {0}?", "Mafia", 6, "Rob", "Tim", "Mary", "Briane", "Hunter", "Macy", "John", "Will", "Lacy", "Claire", "Kenny", "Rick", "Walter", "Bonnie", "Luke", "Bill", "Sarah", "Larry", "Kate", "Stacy", "Diane", "Mac", "Jim", "Clyde", "Tommy", "Lenny", "Molly", "Benny", "Phil", "Bob", "Gary", "Ted", "Kim", "Nate", "Cher", "Ron", "Thomas", "Sam", "Duke", "Jack", "Ed", "Ronny", "Terry", "Claira", "Nick", "Cob", "Ash", "Don", "Jerry", "Simon")]
        MafiaPlayers,

        [SouvenirQuestion("What bearing was signalled in {0}?", "Maritime Flags", 6, "355", "109", "250", "308", "260", "356", "236", "28", "346", "78", "265", "357", "129", "23", "170", "121", "259", "316", "12", "105", "297", "107", "76", "190", "258", "332", "29", "99", "73", "165", "293", "43", "191", "279", "202", "193", "119", "294", "10", "194", "141", "1", "295", "217", "241", "94", "219", "177", "162", "214", "123", "228", "246", "306", "290", "163", "130", "198", "138", "324", "301", "225", "140", "20", "229", "27", "42", "13", "333", "147", "309", "327", "331", "289", "134", "158", "231", "238", "139", "351", "201", "143", "4", "137", "287", "60", "350", "62", "98", "11", "86", "243", "178", "133", "49", "337", "358", "89", "68", "172", "195", "323", "187", "262", "67", "15", "148", "118", "239", "145", "330", "156", "174", "227", "230", "204", "300", "292", "9", "210", "237", "354", "100", "30", "233", "74", "85", "253", "335", "153", "281", "164", "82", "221", "36", "325", "261", "161", "343", "182", "154", "65", "51", "205", "52", "298", "44", "189", "87", "282", "53", "111", "91", "285", "103", "69", "126", "110", "61", "71", "353", "215", "188", "166", "66", "41", "92", "277", "235", "155", "122", "283", "197", "116", "95", "81", "302", "276", "169", "180", "186", "34", "171", "340", "2", "106", "284", "5", "348", "244", "209", "157", "47", "310", "6", "97", "179", "252", "266", "31", "347", "249", "271", "77", "125", "75", "254", "113", "38", "334", "311", "159", "274", "135", "339", "149", "317", "37", "70", "108", "90", "183", "257", "315", "269", "329", "146", "35", "14", "167", "17", "26", "286", "39", "115", "33", "207", "93", "345", "79", "319", "291", "206", "58", "117", "22", "245", "220", "275", "25", "299", "247", "142", "45", "84", "196", "83", "50", "199", "268", "181", "19", "234", "273", "263", "132", "226", "59", "359", "57", "213", "314", "3", "242", "185", "203", "124", "342", "307", "255", "18", "54", "63", "270", "175", "305", "338", "267", "114", "251", "102", "150", "313", "127", "21", "173", "322", "46", "349", "131", "303", "318", "326", "55", "321", "212", "278", "7", "341", "223", "151", "211", "222", "218", "101")]
        MaritimeFlagsBearing,

        [SouvenirQuestion("Which callsign was signalled in {0}?", "Maritime Flags", 4, "1stmate", "2ndmate", "3rdmate", "abandon", "admiral", "advance", "aground", "allides", "anchors", "athwart", "azimuth", "bailers", "ballast", "barrack", "beached", "beacons", "beamend", "beamsea", "bearing", "beating", "belayed", "bermuda", "bobstay", "boilers", "bollard", "bonnets", "boomkin", "bounder", "bowline", "brailed", "breadth", "bridges", "brigged", "bringto", "bulwark", "bumboat", "bumpkin", "burthen", "caboose", "capsize", "capstan", "captain", "caravel", "careens", "carrack", "carrier", "catboat", "cathead", "chained", "channel", "charley", "charter", "citadel", "cleared", "cleated", "clinker", "clipper", "coaming", "coasted", "consort", "convoys", "corinth", "cotchel", "counter", "cranzes", "crewing", "cringle", "crojack", "cruiser", "cutters", "dandies", "deadrun", "debunks", "derrick", "dipping", "disrate", "dogvane", "doldrum", "dolphin", "draught", "drifter", "drogues", "drydock", "dunnage", "dunsels", "earings", "echelon", "embayed", "ensigns", "escorts", "fairway", "falkusa", "fantail", "fardage", "fathoms", "fenders", "ferries", "fitting", "flanked", "flaring", "flattop", "flemish", "floated", "floored", "flotsam", "folding", "follows", "forcing", "forward", "foulies", "founder", "framing", "freight", "frigate", "funnels", "furling", "galleon", "galleys", "galliot", "gangway", "garbled", "general", "georges", "ghosted", "ginpole", "giveway", "gondola", "graving", "gripies", "grounds", "growler", "guineas", "gundeck", "gunport", "gunwale", "halyard", "hammock", "hampers", "hangars", "harbors", "harbour", "hauling", "hawsers", "heading", "headsea", "heaving", "herring", "hogging", "holiday", "huffler", "inboard", "inirons", "inshore", "instays", "inwater", "inwayof", "jackies", "jacktar", "jennies", "jetties", "jiggers", "joggles", "jollies", "juryrig", "keelson", "kellets", "kicking", "killick", "kitchen", "lanyard", "laydays", "lazaret", "leehelm", "leeside", "leeward", "liberty", "lighter", "lizards", "loading", "lockers", "lofting", "lolling", "lookout", "lubbers", "luffing", "luggers", "lugsail", "maewest", "manowar", "marconi", "mariner", "matelot", "mizzens", "mooring", "mousing", "narrows", "nippers", "officer", "offpier", "oilskin", "oldsalt", "onboard", "oreboat", "outhaul", "outward", "painter", "panting", "parcels", "parleys", "parrels", "passage", "pelagic", "pendant", "pennant", "pickets", "pinnace", "pintles", "pirates", "pivoted", "pursers", "pursued", "quarter", "quaying", "rabbets", "ratline", "reduced", "reefers", "repairs", "rigging", "ripraps", "rompers", "rowlock", "rudders", "ruffles", "rummage", "sagging", "sailors", "salties", "salvors", "sampans", "sampson", "sculled", "scupper", "scuttle", "seacock", "sealing", "seekers", "serving", "sextant", "shelter", "shipped", "shiprig", "sickbay", "skipper", "skysail", "slinged", "slipway", "snagged", "snotter", "spliced", "splices", "sponson", "sponsor", "springs", "squares", "stackie", "standon", "starter", "station", "steamer", "steered", "steeves", "steward", "stopper", "stovein", "stowage", "strikes", "sunfish", "swimmie", "systems", "tacking", "thwarts", "tinclad", "tompion", "tonnage", "topmast", "topsail", "torpedo", "tossers", "trading", "traffic", "tramper", "transom", "trawler", "trenail", "trennel", "trimmer", "trooper", "trunnel", "tugboat", "turntwo", "unships", "upbound", "vessels", "voicing", "voyager", "weather", "whalers", "wharves", "whelkie", "whistle", "winches", "windage", "working", "yardarm")]
        MaritimeFlagsCallsign,

        [SouvenirQuestion("In which {1} was the starting position in {0}, counting from the {2}?", "Maze", 6, "1", "2", "3", "4", "5", "6",
            ExampleExtraFormatArguments = new[] { "column", "left", "row", "top" }, ExampleExtraFormatArgumentGroupSize = 2)]
        MazeStartingPosition,

        [SouvenirQuestion("What was the color of the starting face in {0}?", "Maze³", 6, "Red", "Blue", "Yellow", "Green", "Magenta", "Orange")]
        Maze3StartingFace,

        [SouvenirQuestion("Which was the {1} value in {0}?", "Mazematics", 6, null, ExampleAnswers = new[] { "30", "42", "51" },
            ExampleExtraFormatArguments = new[] { "initial", "goal" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MazematicsValue,

        [SouvenirQuestion("What was the starting position on {0}?", "Maze Scrambler", 4, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        MazeScramblerStart,
        [SouvenirQuestion("What was the goal on {0}?", "Maze Scrambler", 4, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        MazeScramblerGoal,
        [SouvenirQuestion("Which of these positions was a maze marking on {0}?", "Maze Scrambler", 4, "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right")]
        MazeScramblerIndicators,

        [SouvenirQuestion("Who was the master shown in {0}?", "Mega Man 2", 4, "Cold Man", "Magma Man", "Dust Man", "Sword Man", "Splash Woman", "Ice Man", "Quick Man", "Hard Man", "Pharaoh Man", "Charge Man", "Pirate Man", "Pump Man", "Galaxy Man", "Grenade Man", "Snake Man", "Burst Man", "Cut Man", "Air Man", "Magnet Man", "Toad Man", "Gyro Man", "Tomahawk Man", "Wood Man", "Strike Man", "Blade Man", "Aqua Man", "Shade Man", "Flash Man", "Flame Man", "Concrete Man", "Metal Man", "Needle Man", "Wave Man", "Knight Man", "Slash Man", "Shadow Man", "Sheep Man", "Ground Man", "Wind Man", "Fire Man", "Stone Man", "Tengu Man", "Bright Man", "Centaur Man", "Cloud Man", "Frost Man", "Dynamo Man", "Chill Man", "Turbo Man", "Napalm Man", "Jewel Man", "Drill Man", "Freeze Man", "Blizzard Man", "Gravity Man", "Junk Man", "Clown Man", "Hornet Man", "Skull Man", "Solar Man", "Commando Man", "Yamato Man", "Dive Man", "Search Man", "Gemini Man", "Bubble Man", "Guts Man", "Tornado Man", "Astro Man", "Plug Man", "Elec Man", "Crystal Man", "Nitro Man", "Burner Man", "Spark Man", "Spring Man", "Plant Man", "Star Man", "Ring Man", "Top Man", "Crash Man", "Bomb Man", "Heat Man", "Magic Man")]
        MegaMan2SelectedMaster,
        [SouvenirQuestion("Whose weapon was shown in {0}?", "Mega Man 2", 4, "Cold Man", "Magma Man", "Dust Man", "Sword Man", "Splash Woman", "Ice Man", "Quick Man", "Hard Man", "Pharaoh Man", "Charge Man", "Pirate Man", "Pump Man", "Galaxy Man", "Grenade Man", "Snake Man", "Burst Man", "Cut Man", "Air Man", "Magnet Man", "Toad Man", "Gyro Man", "Tomahawk Man", "Wood Man", "Strike Man", "Blade Man", "Aqua Man", "Shade Man", "Flash Man", "Flame Man", "Concrete Man", "Metal Man", "Needle Man", "Wave Man", "Knight Man", "Slash Man", "Shadow Man", "Sheep Man", "Ground Man", "Wind Man", "Fire Man", "Stone Man", "Tengu Man", "Bright Man", "Centaur Man", "Cloud Man", "Frost Man", "Dynamo Man", "Chill Man", "Turbo Man", "Napalm Man", "Jewel Man", "Drill Man", "Freeze Man", "Blizzard Man", "Gravity Man", "Junk Man", "Clown Man", "Hornet Man", "Skull Man", "Solar Man", "Commando Man", "Yamato Man", "Dive Man", "Search Man", "Gemini Man", "Bubble Man", "Guts Man", "Tornado Man", "Astro Man", "Plug Man", "Elec Man", "Crystal Man", "Nitro Man", "Burner Man", "Spark Man", "Spring Man", "Plant Man", "Star Man", "Ring Man", "Top Man", "Crash Man", "Bomb Man", "Heat Man", "Magic Man")]
        MegaMan2SelectedWeapon,

        [SouvenirQuestion("Which part was in slot #{1} at the start of {0}?", "Melody Sequencer", 6, "1", "2", "3", "4", "5", "6", "7", "8",
            ExampleExtraFormatArguments = new[] { "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MelodySequencerSlots,
        [SouvenirQuestion("Which slot contained part #{1} at the start of {0}?", "Melody Sequencer", 6, "1", "2", "3", "4", "5", "6", "7", "8",
            ExampleExtraFormatArguments = new[] { "1", "2" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MelodySequencerParts,

        [SouvenirQuestion("What was the displayed number in stage {1} when you solved {0}?", "Memory", 4, "1", "2", "3", "4",
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4", "5" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MemoryDisplay,
        [SouvenirQuestion("In what position was the button that you pressed in stage {1} when you solved {0}?", "Memory", 4, null, Type = AnswerType.Sprites,
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MemoryPosition,
        [SouvenirQuestion("What was the label of the button that you pressed in stage {1} when you solved {0}?", "Memory", 4, "1", "2", "3", "4",
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MemoryLabel,

        [SouvenirQuestion("Which pin lit up {1} in {0}?", "Microcontroller", 6, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MicrocontrollerPinOrder,

        [SouvenirQuestion("What was the color of the starting cell in {0}?", "Minesweeper", 6, "red", "orange", "yellow", "green", "blue", "purple", "black")]
        MinesweeperStartingColor,

        [SouvenirQuestion("What was the decrypted word of the {1} stage in {0}?", "Modern Cipher", 6, "Absent", "Abstract", "Abysmal", "Accident", "Activate", "Adjacent", "Afraid", "Agenda", "Agony", "Alchemy", "Alcohol", "Alive", "Allergic", "Allergy", "Alpha", "Alphabet", "Already", "Amethyst", "Amnesty", "Amperage", "Ancient", "Animals", "Animate", "Anthrax", "Anxious", "Aquarium", "Aquarius", "Arcade", "Arrange", "Arrow", "Artefact", "Asterisk", "Atrophy", "Audio", "Author", "Avoid", "Awesome", "Balance", "Banana", "Bandit", "Bankrupt", "Basket", "Battle", "Bazaar", "Beard", "Beauty", "Beaver", "Becoming", "Beetle", "Beseech", "Between", "Bicycle", "Bigger", "Biggest", "Biology", "Birthday", "Bistro", "Bites", "Blight", "Blockade", "Blubber", "Bomb", "Bonobo", "Books", "Bottle", "Brazil", "Brief", "Broccoli", "Broken", "Brother", "Bubble", "Budget", "Bulkhead", "Bumper", "Bunny", "Button", "Bytes", "Cables", "Caliber", "Campaign", "Canada", "Canister", "Caption", "Caution", "Cavity", "Chalk", "Chamber", "Chamfer", "Champion", "Changes", "Chicken", "Children", "Chlorine", "Chord", "Chronic", "Church", "Cinnamon", "Civic", "Cleric", "Clock", "Cocoon", "Combat", "Combine", "Comedy", "Comics", "Comma", "Command", "Comment", "Compost", "Computer", "Condom", "Conflict", "Consider", "Contour", "Control", "Corrupt", "Costume", "Criminal", "Crunch", "Cryptic", "Cuboid", "Cypher", "Daddy", "Dancer", "Dancing", "Daughter", "Dead", "Decapod", "Decay", "Decoy", "Defeat", "Defuser", "Degree", "Delay", "Demigod", "Dentist", "Desert", "Design", "Desire", "Dessert", "Detail", "Develop", "Device", "Diamond", "Dictate", "Diffuse", "Dilemma", "Dingy", "Dinosaur", "Disease", "Disgust", "Document", "Doubled", "Doubt", "Downbeat", "Dragon", "Drawer", "Dream", "Drink", "Drunken", "Dungeon", "Dynasty", "Dyslexia", "Eclipse", "Eldritch", "Email", "Emulator", "Encrypt", "England", "Enlist", "Enough", "Ensure", "Equality", "Equation", "Eruption", "Eternity", "Euphoria", "Exact", "Exclaim", "Exhaust", "Expert", "Expertly", "Explain", "Explodes", "Fabric", "Factory", "Faded", "Faint", "Fair", "False", "Falter", "Famous", "Fantasy", "Farm", "Father", "Faucet", "Faulty", "Fearsome", "Feast", "February", "Feint", "Festival", "Fiction", "Fighter", "Figure", "Finish", "Fireman", "Firework", "First", "Fixture", "Flagrant", "Flagship", "Flamingo", "Flesh", "Flipper", "Fluorine", "Flush", "Foreign", "Forensic", "Fractal", "Fragrant", "France", "Frantic", "Freak", "Friction", "Friday", "Friendly", "Frighten", "Furor", "Fused", "Garage", "Genes", "Genetic", "Genius", "Gentle", "Glacier", "Glitch", "Goat", "Golden", "Granular", "Graphics", "Graphite", "Grateful", "Gridlock", "Ground", "Guitar", "Gumption", "Halogen", "Harmony", "Hawk", "Headache", "Heard", "Hedgehog", "Heinous", "Herd", "Heretic", "Hexagon", "Hiccup", "Highway", "Holiday", "Home", "Homesick", "Honest", "Horror", "Horse", "House", "Huge", "Humanity", "Hungry", "Hydrogen", "Hysteria", "Imagine", "Industry", "Infamous", "Inside", "Integral", "Interest", "Ironclad", "Issue", "Italic", "Italy", "Itch", "Jaundice", "Jeans", "Jeopardy", "Joyful", "Joystick", "Juice", "Juncture", "Jungle", "Junkyard", "Justice", "Keep", "Keyboard", "Kilobyte", "Kilogram", "Kingdom", "Kitchen", "Kitten", "Knife", "Krypton", "Ladylike", "Language", "Large", "Laughter", "Launch", "Leaders", "Learn", "Leave", "Leopard", "Level", "Liberal", "Liberty", "Lifeboat", "Ligament", "Light", "Liquid", "Listen", "Little", "Lobster", "Logical", "Love", "Lucky", "Lulled", "Lunatic", "Lurks", "Machine", "Madam", "Magnetic", "Manager", "Manual", "Marina", "Marine", "Martian", "Master", "Matrix", "Measure", "Meaty", "Meddle", "Medical", "Mental", "Menu", "Meow", "Merchant", "Message", "Messes", "Metal", "Method", "Mettle", "Militant", "Minim", "Minimum", "Miracle", "Mirror", "Misjudge", "Misplace", "Misses", "Mistake", "Mixture", "Mnemonic", "Mobile", "Modern", "Modest", "Module", "Moist", "Money", "Morning", "Most", "Mother", "Movies", "Multiple", "Munch", "Musical", "Mustache", "Mystery", "Mystic", "Mystique", "Mythic", "Narcotic", "Nasty", "Nature", "Navigate", "Network", "Neutral", "Nobelium", "Nobody", "Noise", "Notice", "Noun", "Nuclear", "Numeral", "Nutrient", "Nymph", "Obelisk", "Obstacle", "Obvious", "Octopus", "Offset", "Omega", "Opaque", "Opinion", "Orange", "Organic", "Ouch", "Outbreak", "Outdo", "Overcast", "Overlaps", "Package", "Padlock", "Pancake", "Panda", "Panic", "Paper", "Papers", "Parent", "Park", "Particle", "Passive", "Patented", "Pathetic", "Patient", "Peace", "Peasant", "Penalty", "Pencil", "Penguin", "Perfect", "Person", "Persuade", "Perusing", "Phone", "Physical", "Piano", "Picture", "Piglet", "Pilfer", "Pillage", "Pinch", "Pirate", "Pitcher", "Pizza", "Plane", "Planet", "Platonic", "Player", "Please", "Plucky", "Plunder", "Plurals", "Pocket", "Police", "Portrait", "Potato", "Potently", "Pounce", "Poverty", "Practice", "Predict", "Prefect", "Premium", "Present", "Prince", "Printer", "Prison", "Profit", "Promise", "Prophet", "Protein", "Province", "Psalm", "Psychic", "Puddle", "Punchbag", "Pungent", "Punish", "Purchase", "Quagmire", "Qualify", "Quantify", "Quantize", "Quarter", "Querying", "Queue", "Quiche", "Quick", "Rabbit", "Racoon", "Radar", "Radical", "Rainbow", "Random", "Rattle", "Ravenous", "Reason", "Rebuke", "Refine", "Regular", "Reindeer", "Request", "Resort", "Respect", "Retire", "Revolt", "Reward", "Rhapsody", "Rhenium", "Rhodium", "Rhomboid", "Rhyme", "Rhythm", "Ridicule", "Roadwork", "Roar", "Roast", "Room", "Rooster", "Roster", "Rotor", "Rotunda", "Royal", "Ruler", "Rural", "Sailor", "Sainted", "Sales", "Sally", "Satisfy", "Saunter", "Scale", "Scandal", "Schedule", "School", "Science", "Scratch", "Screen", "Sensible", "Separate", "Serious", "Several", "Shampoo", "Shares", "Shelter", "Shift", "Ship", "Shirt", "Shiver", "Shorten", "Showcase", "Shuffle", "Silent", "Similar", "Sister", "Sixth", "Sixty", "Skater", "Skyward", "Slander", "Slayer", "Sleek", "Slipper", "Smart", "Smeared", "Soccer", "Society", "Source", "Spain", "Spare", "Spark", "Spatula", "Speaker", "Special", "Spectate", "Spectrum", "Spicy", "Spinach", "Spiral", "Splendid", "Splinter", "Sprayed", "Spread", "Spring", "Squadron", "Squander", "Squash", "Squib", "Squid", "Squish", "Stake", "Stalking", "Steak", "Steam", "Sticker", "Stinky", "Stocking", "Stone", "Store", "Stormy", "Strange", "Strike", "Stutter", "Subway", "Suffer", "Supreme", "Surf", "Surplus", "Survey", "Switch", "Symbol", "System", "Systemic", "Table", "Tadpole", "Talking", "Tangle", "Tank", "Tapeworm", "Target", "Tarot", "Teach", "Teamwork", "Terminal", "Terminus", "Terror", "Testify", "Their", "There", "Thick", "Thief", "Think", "Throat", "Through", "Thunder", "Thyme", "Ticket", "Time", "Toaster", "Tomato", "Tone", "Torque", "Tortoise", "Touchy", "Toupe", "Tower", "Transfix", "Transit", "Trash", "Trauma", "Treason", "Treasure", "Trick", "Tripod", "Trouble", "Truck", "Trumpet", "Turtle", "Twinkle", "Ugly", "Ultra", "Umbrella", "Underway", "Unique", "Unknown", "Unsteady", "Untoward", "Unwashed", "Upgrade", "Urban", "Used", "Useless", "Utopia", "Vacuum", "Vampire", "Vanish", "Vanquish", "Various", "Vast", "Velocity", "Vendor", "Verb", "Verbatim", "Verdict", "Vexation", "Vicious", "Victim", "Victory", "Video", "View", "Viking", "Village", "Violent", "Violin", "Virulent", "Visceral", "Vision", "Volatile", "Voltage", "Vortex", "Vulgar", "Warden", "Warlock", "Warning", "Wealth", "Weapon", "Wedding", "Weight", "Whack", "Wharf", "What", "When", "Whisk", "Whistle", "Wicked", "Window", "Winter", "Witness", "Wizard", "Wrench", "Wretch", "Wrinkle", "Writer", "Xanthous", "Yacht", "Yarn", "Yawn", "Yeah", "Yearlong", "Yearn", "Yeoman", "Yodel", "Yoga", "Yonder", "Youngest", "Yourself", "Zealot", "Zebra", "Zenith", "Zither", "Zodiac", "Zombie",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ModernCipherWord,

        [SouvenirQuestion("Which of the following was the starting icon for {0}?", "Module Maze", 6, null, Type = AnswerType.Sprites)]
        ModuleMazeStartingIcon,

        [SouvenirQuestion("Which creature was displayed in the successful round of {0}?", "Monsplode, Fight!", 4, "Caadarim", "Buhar", "Melbor", "Lanaluff", "Bob", "Mountoise", "Aluga", "Nibs", "Zapra", "Zenlad", "Vellarim", "Ukkens", "Lugirit", "Flaurim", "Myrchat", "Clondar", "Gloorim", "Docsplode", "Magmy", "Pouse", "Asteran", "Violan", "Percy", "Cutie Pie")]
        MonsplodeFightCreature,
        [SouvenirQuestion("Which one of these moves {1} selectable in the successful round of {0}?", "Monsplode, Fight!", 4, "Tic", "Tac", "Toe", "Hollow Gaze", "Splash", "Heavy Rain", "Fountain", "Candle", "Torchlight", "Flame Spear", "Tangle", "Grass Blade", "Ivy Spikes", "Spectre", "Boo", "Battery Power", "Zap", "Double Zap", "Shock", "High Voltage", "Dark Portal", "Last Word", "Void", "Boom", "Fiery Soul", "Stretch", "Shrink", "Appearify", "Sendify", "Freak Out", "Glyph", "Bug Spray", "Bedrock", "Earthquake", "Cave In", "Toxic Waste", "Venom Fang", "Countdown", "Finale", "Sidestep",
            ExampleExtraFormatArguments = new[] { "was", "was not" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MonsplodeFightMove,

        [SouvenirQuestion("What was the {1} before the last action in {0}?", "Monsplode Trading Cards", 4, "Aluga", "Asteran", "Bob", "Buhar", "Caadarim", "Clondar", "Cutie Pie", "Docsplode", "Flaurim", "Gloorim", "Lanaluff", "Lugirit", "Magmy", "Melbor", "Mountoise", "Myrchat", "Nibs", "Percy", "Pouse", "Ukkens", "Vellarim", "Violan", "Zapra", "Zenlad", "Aluga, The Fighter", "Bob, The Ancestor", "Buhar, The Protector", "Melbor, The Web Bug",
            ExampleExtraFormatArguments = new[] { "first card in your hand", "second card in your hand", "third card in your hand", "card on offer" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MonsplodeTradingCardsCards,
        [SouvenirQuestion("What was the rarity of the {1} before the last action in {0}?", "Monsplode Trading Cards", 4, "common", "uncommon", "rare", "ultra rare",
            ExampleExtraFormatArguments = new[] { "first card in your hand", "second card in your hand", "third card in your hand", "card on offer" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MonsplodeTradingCardsRarities,
        [SouvenirQuestion("What was the print version of the {1} before the last action in {0}?", "Monsplode Trading Cards", 6, "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "B10", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "E10", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "G1", "G2", "G3", "G4", "G5", "G6", "G7", "G8", "G9", "G10", "H1", "H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "H10", "I1", "I2", "I3", "I4", "I5", "I6", "I7", "I8", "I9", "I10", "J1", "J2", "J3", "J4", "J5", "J6", "J7", "J8", "J9", "J10",
            ExampleExtraFormatArguments = new[] { "first card in your hand", "second card in your hand", "third card in your hand", "card on offer" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MonsplodeTradingCardsPrintVersions,

        [SouvenirQuestion("What was the {1} set in clockwise order in {0}?", "Moon", 4, "south", "south-west", "west", "north-west", "north", "north-east", "east", "south-east",
            ExampleExtraFormatArguments = new[] { "first initially lit", "second initially lit", "third initially lit", "fourth initially lit", "first initially unlit", "second initially unlit", "third initially unlit", "fourth initially unlit" },
            ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        MoonLitUnlit,

        [SouvenirQuestion("What was the starting location in {0}?", "Morse-A-Maze", 6, "A1", "A2", "A3", "A4", "A5", "A6", "B1", "B2", "B3", "B4", "B5", "B6", "C1", "C2", "C3", "C4", "C5", "C6", "D1", "D2", "D3", "D4", "D5", "D6", "E1", "E2", "E3", "E4", "E5", "E6", "F1", "F2", "F3", "F4", "F5", "F6")]
        MorseAMazeStartingCoordinate,

        [SouvenirQuestion("What was the ending location in {0}?", "Morse-A-Maze", 6, "A1", "A2", "A3", "A4", "A5", "A6", "B1", "B2", "B3", "B4", "B5", "B6", "C1", "C2", "C3", "C4", "C5", "C6", "D1", "D2", "D3", "D4", "D5", "D6", "E1", "E2", "E3", "E4", "E5", "E6", "F1", "F2", "F3", "F4", "F5", "F6")]
        MorseAMazeEndingCoordinate,

        [SouvenirQuestion("What was the word shown as Morse code in {0}?", "Morse-A-Maze", 6, null,
            ExampleAnswers = new[] { "couch", "strobe", "smoke", "assay", "monkey", "glass", "starts", "strode", "office", "essays", "couple", "bosses" })]
        MorseAMazeMorseCodeWord,

        [SouvenirQuestion("What was the {1} flashed by the {2} button in {0}?", "Morse Buttons", 6, null, ExampleAnswers = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" },
            ExampleExtraFormatArguments = new[] { "character", "first", "character", "second", "character", "third", "character", "fourth", "character", "fifth", "character", "sixth", "color", "first", "color", "second", "color", "third", "color", "fourth", "color", "fifth", "color", "sixth" }, ExampleExtraFormatArgumentGroupSize = 2)]
        MorseButtonsButton,

        [SouvenirQuestion("What was the {1} received letter in {0}?", "Morsematics", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MorsematicsReceivedLetters,

        [SouvenirQuestion("What were the LEDs in the {1} row in {0} (1 = on, 0 = off)?", "Morse War", 6, "1100", "1010", "1001", "0110", "0101", "0011",
            ExampleExtraFormatArguments = new[] { "bottom", "middle", "top" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MorseWarLeds,
        [SouvenirQuestion("What code was transmitted in {0}?", "Morse War", 6, "ABR", "RBS", "SVR", "ZUX", "ZAQ", "MOI", "OPA", "VZQ", "XRP", "OLL", "AIR", "RHG", "MJN", "VTT", "XZS", "SUN")]
        MorseWarCode,

        [SouvenirQuestion("What color was the torus in {0}?", "Mouse in the Maze", 4, "white", "green", "blue", "yellow")]
        MouseInTheMazeTorus,

        [SouvenirQuestion("Which color sphere was the goal in {0}?", "Mouse in the Maze", 4, "white", "green", "blue", "yellow")]
        MouseInTheMazeSphere,

        [SouvenirQuestion("Where was the body found in {0}?", "Murder", 4, "Dining Room", "Study", "Kitchen", "Lounge", "Billiard Room", "Conservatory", "Ballroom", "Hall", "Library")]
        MurderBodyFound,

        [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", 4, "Miss Scarlett", "Professor Plum", "Mrs Peacock", "Reverend Green", "Colonel Mustard", "Mrs White",
            ExampleExtraFormatArguments = new[] { "a suspect, but not the murderer,", "not a suspect" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MurderSuspect,

        [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", 4, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner",
            ExampleExtraFormatArguments = new[] { "a potential weapon, but not the murder weapon,", "not a potential weapon" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MurderWeapon,

        [SouvenirQuestion("Which module was the first requested to be solved by {0}?", "Mystery Module", 4, null, ExampleAnswers = new[] { "Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox", "Rock-Paper-Scissors-Lizard-Spock" })]
        MysteryModuleFirstKey,
        [SouvenirQuestion("Which module was hidden by {0}?", "Mystery Module", 4, null, ExampleAnswers = new[] { "Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox" })]
        MysteryModuleHiddenModule,

        [SouvenirQuestion("Where was the {1} in {0}?", "Mystic Square", 4, "top left", "top middle", "top right", "middle left", "center", "middle right", "bottom left", "bottom middle", "bottom right",
            ExampleExtraFormatArguments = new[] { "knight", "skull" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MysticSquareKnightSkull,

        [SouvenirQuestion("What was the chapter number of the {1} page in {0}?", "Necronomicon", 6, null, ExampleAnswers = new[] { "1", "24", "36" }, AddThe = true,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth", "seventh" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NecronomiconChapters,

        [SouvenirQuestion("What was the acid’s color in {0}?", "Neutralization", 4, "Yellow", "Green", "Red", "Blue")]
        NeutralizationColor,
        [SouvenirQuestion("What was the acid’s volume in {0}?", "Neutralization", 6, "0", "5", "10", "15", "20", "25")]
        NeutralizationVolume,

        [SouvenirQuestion("What was the label of the correct button in the last successful round of {0}?", "N&Ms", 6, "NNNNN", "MNNNN", "NMNNN", "MMNNN", "NNMNN", "MNMNN", "NMMNN", "MMMNN", "NNNMN", "MNNMN", "NMNMN", "MMNMN", "NNMMN", "MNMMN", "NMMMN", "MMMMN", "NNNNM", "MNNNM", "NMNNM", "MMNNM", "NNMNM", "MNMNM", "NMMNM", "MMMNM", "NNNMM", "MNNMM", "NMNMM", "MMNMM", "NNMMM", "MNMMM", "NMMMM", "MMMMM")]
        NandMsAnswer,

        [SouvenirQuestion("What colors did the light glow when you solved {0}?", "Not the Button", 6, "white", "red", "yellow", "green", "blue", "white/red", "white/yellow", "white/green", "white/blue", "red/yellow", "red/green", "red/blue", "yellow/green", "yellow/blue", "green/blue")]
        NotButtonLightColor,
        [SouvenirQuestion("How many times did you press the button when you solved {0}?", "Not the Button", 6, ExampleAnswers = new[] { "10", "20", "30", "40", "50", "60" })]
        NotButtonMashCount,

        [SouvenirQuestion("What color flashed {1} in the final sequence in {0}?", "Not Keypad", 6, "red", "orange", "yellow", "green", "cyan", "blue", "purple", "magenta", "pink", "brown", "grey", "white",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotKeypadColor,
        [SouvenirQuestion("Which symbol was on the button that flashed {1} in the final sequence in {0}?", "Not Keypad", 4, Type = AnswerType.Sprites,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotKeypadSymbol,

        [SouvenirQuestion("What was the starting distance in {0}?", "Not Maze", 6, "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        NotMazeStartingDistance,

        [SouvenirQuestion("What was the {1} correct word you submitted in {0}?", "Not Morse Code", 6, ExampleAnswers = new[] { "shelf", "pounds", "sister", "beef", "yeast", "drive" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotMorseCodeWord,

        [SouvenirQuestion("Which maze was used in {0}?", "Not Simaze", 6, "red", "orange", "yellow", "green", "blue", "purple")]
        NotSimazeMaze,
        [SouvenirQuestion("What was the starting position in {0}?", "Not Simaze", 4, "(red, red)", "(red, orange)", "(red, yellow)", "(red, green)", "(red, blue)", "(red, purple)", "(orange, red)", "(orange, orange)", "(orange, yellow)", "(orange, green)", "(orange, blue)", "(orange, purple)", "(yellow, red)", "(yellow, orange)", "(yellow, yellow)", "(yellow, green)", "(yellow, blue)", "(yellow, purple)", "(green, red)", "(green, orange)", "(green, yellow)", "(green, green)", "(green, blue)", "(green, purple)", "(blue, red)", "(blue, orange)", "(blue, yellow)", "(blue, green)", "(blue, blue)", "(blue, purple)", "(purple, red)", "(purple, orange)", "(purple, yellow)", "(purple, green)", "(purple, blue)", "(purple, purple)")]
        NotSimazeStart,
        [SouvenirQuestion("What was the goal position in {0}?", "Not Simaze", 4, "(red, red)", "(red, orange)", "(red, yellow)", "(red, green)", "(red, blue)", "(red, purple)", "(orange, red)", "(orange, orange)", "(orange, yellow)", "(orange, green)", "(orange, blue)", "(orange, purple)", "(yellow, red)", "(yellow, orange)", "(yellow, yellow)", "(yellow, green)", "(yellow, blue)", "(yellow, purple)", "(green, red)", "(green, orange)", "(green, yellow)", "(green, green)", "(green, blue)", "(green, purple)", "(blue, red)", "(blue, orange)", "(blue, yellow)", "(blue, green)", "(blue, blue)", "(blue, purple)", "(purple, red)", "(purple, orange)", "(purple, yellow)", "(purple, green)", "(purple, blue)", "(purple, purple)")]
        NotSimazeGoal,

        [SouvenirQuestion("In which position was the button you pressed in stage {1} when you solved {0}?", "Not Who's on First", 6, "top left", "top right", "middle left", "middle right", "bottom left", "bottom right",
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstPressedPosition,
        [SouvenirQuestion("What was the label on the button you pressed in stage {1} when you solved {0}?", "Not Who's on First", 6, "BLANK", "DONE", "FIRST", "HOLD", "LEFT", "LIKE", "MIDDLE", "NEXT", "NO", "NOTHING", "OKAY", "PRESS", "READY", "RIGHT", "SURE", "U", "UH HUH", "UH UH", "UHHH", "UR", "WAIT", "WHAT", "WHAT?", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR",
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstPressedLabel,
        [SouvenirQuestion("In which position was the reference button in stage {1} when you solved {0}?", "Not Who's on First", 6, "top left", "top right", "middle left", "middle right", "bottom left", "bottom right",
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstReferencePosition,
        [SouvenirQuestion("What was the label on the reference button in stage {1} when you solved {0}?", "Not Who's on First", 6, "BLANK", "DONE", "FIRST", "HOLD", "LEFT", "LIKE", "MIDDLE", "NEXT", "NO", "NOTHING", "OKAY", "PRESS", "READY", "RIGHT", "SURE", "U", "UH HUH", "UH UH", "UHHH", "UR", "WAIT", "WHAT", "WHAT?", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR",
            ExampleExtraFormatArguments = new[] { "1", "2", "3", "4" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NotWhosOnFirstReferenceLabel,
        [SouvenirQuestion("What was the calculated number in stage 2 when you solved {0}?", "Not Who's on First", 6)]
        NotWhosOnFirstSum,

        [SouvenirQuestion("What number was on button {1} in reading order in {0}?", "Numbered Buttons", 6, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100",
            ExampleExtraFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" }, ExampleExtraFormatArgumentGroupSize = 1)]
        NumberedButtonsLabels,

        [SouvenirQuestion("What was the button you pressed in the {1} stage of {0}?", "Odd One Out", 6, "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        OddOneOutButton,

        [SouvenirQuestion("Which Egyptian hieroglyph was in the {1} in {0}?", "Only Connect", 4, "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus",
            ExampleExtraFormatArguments = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        OnlyConnectHieroglyphs,

        [SouvenirQuestion("What was the {1} arrow on the display of the {2} successful stage on {0}?", "Orange Arrows", 4, "Up", "Right", "Down", "Left",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrangeArrowsSequences,

        [SouvenirQuestion("What color was the {2} key in stage {1} of {0}?", "Ordered Keys", 6, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta",
            ExampleExtraFormatArguments = new[] { "first", "1", "first", "2" }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrderedKeysColors,
        [SouvenirQuestion("What was the label on the {2} key in stage {1} of {0}?", "Ordered Keys", 6, "1", "2", "3", "4", "5", "6",
            ExampleExtraFormatArguments = new[] { "first", "1", "first", "2" }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrderedKeysLabels,
        [SouvenirQuestion("What color was the label of the {2} key in stage {1} of {0}?", "Ordered Keys", 6, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta",
            ExampleExtraFormatArguments = new[] { "first", "1", "first", "2" }, ExampleExtraFormatArgumentGroupSize = 2)]
        OrderedKeysLabelColors,

        [SouvenirQuestion("What was the observer’s intial position in {0}?", "Orientation Cube", 4, "front", "left", "back", "right")]
        OrientationCubeInitialObserverPosition,

        [SouvenirQuestion("What was {1}'s {2} digit from the right in {0}?", "Palindromes", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "X", "first", "X", "second", "X", "third", "X", "4th", "X", "5th", "X", "first", "Y", "second", "Y", "third", "Y", "4th", "Y", "first", "Z", "second", "Z", "third", "Z", "4th", "Z", "5th", "the screen", "first", "the screen", "second", "the screen", "third", "the screen", "4th", "the screen", "5th" }, ExampleExtraFormatArgumentGroupSize = 2)]
        PalindromesNumbers,

        [SouvenirQuestion("What was the passport expiration year of the {1} correctly inspected passenger in {0}?", "Passport Control", 6, ExampleAnswers = new[] { "1931", "1956", "1977", "1980", "1991", "2000", "2004", "2019", "2047" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PassportControlPassenger,

        [SouvenirQuestion("Which symbol was highlighted in {0}?", "Pattern Cube", 6, Type = AnswerType.Sprites)]
        PatternCubeHighlightedSymbol,

        [SouvenirQuestion("Which peg had three sides of the key color in {0}?", "Perspective Pegs", 6, null, Type = AnswerType.Sprites)]
        PerspectivePegsSolution,

        [SouvenirQuestion("What was the {1} digit of the displayed number in {0}?", "Pie", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PieDigits,

        [SouvenirQuestion("What was the {1} in the last successful round of {0}?", "Pigpen Cycle", 4, "Advanced", "Addition", "Allotype", "Allotted", "Binaries", "Billions", "Bullhorn", "Bulwarks", "Ciphered", "Circuits", "Commando", "Compiler", "Decrypts", "Division", "Dispatch", "Discrete", "Encipher", "Entrance", "Equators", "Equalise", "Finished", "Findings", "Formulae", "Fortunes", "Gauntlet", "Gambling", "Gatepost", "Gateways", "Hazarded", "Haziness", "Huntress", "Hungrier", "Indicate", "Indigoes", "Illusory", "Illuding", "Jigsawed", "Jimmying", "Junkyard", "Juncture", "Kilowatt", "Kinetics", "Knocking", "Knowable", "Limiting", "Linearly", "Linkwork", "Lingered", "Monogram", "Monotone", "Multiton", "Mulcting", "Nanogram", "Nanotube", "Numerous", "Numerate", "Octangle", "Octuples", "Obstruct", "Obstacle", "Progress", "Projects", "Postsync", "Positron", "Quadrant", "Quadrics", "Quirkish", "Quitters", "Reversed", "Revolved", "Rotators", "Relative", "Starting", "Standard", "Stockade", "Stoccata", "Triggers", "Triangle", "Tomogram", "Tomahawk", "Underrun", "Underlie", "Ulterior", "Ultrahot", "Vicinity", "Viceless", "Volition", "Voluming", "Wingding", "Winnable", "Whatness", "Whatsits", "Yellowed", "Yeasayer", "Yokozuna", "Yourself", "Zippered", "Zigzaggy", "Zymology", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PigpenCycleWord,

        [SouvenirQuestion("What was the first half of first phrase in {0}?", "Placeholder Talk", 4, null, ExampleAnswers = new[] { "", "IS IN THE", "IS THE", "IS IN UH", "IS", "IS AT", "IS INN", "IS THE IN", "IN IS", "IS IN.", "IS IN", "THE", "FIRST-", "IN", "UH IS IN", "AT", "LAST-", "UH", "KEYBORD", "A" })]
        PlaceholderTalkFirstPhrase,
        [SouvenirQuestion("What was the last half of first phrase in {0}?", "Placeholder Talk", 4, null, ExampleAnswers = new[] { "", "FIRST POS.", "SECOND POS.", "THIRD POS.", "FOURTH POS.", "FIFTH POS.", "MILLIONTH POS.", "BILLIONTH POS.", "LAST POS.", "AN ANSWER" })]
        PlaceholderTalkOrdinal,
        [SouvenirQuestion("What was the second phrase's calculated value in {0}?", "Placeholder Talk", 6, null, ExampleAnswers = new[] { "1", "2", "3", "4", "5", "6", "7", "8", })]
        PlaceholderTalkSecondPhrase,

        [SouvenirQuestion("What was the planet shown in {0}?", "Planets", 6, null, Type = AnswerType.Sprites)]
        PlanetsPlanet,
        [SouvenirQuestion("What was the color of the {1} strip (from the top) in {0}?", "Planets", 6, "Aqua", "Blue", "Green", "Lime", "Orange", "Red", "Yellow", "White", "Off",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PlanetsStrips,

        [SouvenirQuestion("What was the {1} in the last successful round of {0}?", "Playfair Cycle", 4, "Advanced", "Advocate", "Allotype", "Allotted", "Binaries", "Binomial", "Bullhorn", "Bulwarks", "Circular", "Circuits", "Commando", "Compiler", "Decrypts", "Decimals", "Dispatch", "Discrete", "Encipher", "Encoding", "Equators", "Equalise", "Finished", "Finnicky", "Formulae", "Fortunes", "Gauntlet", "Gauchest", "Gatepost", "Gateways", "Hotlinks", "Hotheads", "Huntress", "Hungrier", "Indicate", "Indirect", "Illusory", "Illuding", "Jigsawed", "Jiggling", "Junkyard", "Juncture", "Kilowatt", "Kilobyte", "Knocking", "Knowable", "Limiting", "Limerick", "Linkwork", "Lingered", "Monogram", "Monolith", "Multiton", "Mulcting", "Nanogram", "Nanobots", "Numerous", "Numerate", "Octangle", "Octonary", "Obstruct", "Obstacle", "Progress", "Programs", "Postsync", "Positron", "Quotient", "Quotable", "Quirkish", "Quitters", "Reversed", "Revealed", "Rotators", "Relative", "Stanzaic", "Standard", "Stockade", "Stoccata", "Triggers", "Trickier", "Tomogram", "Tomahawk", "Underrun", "Undoings", "Ulterior", "Ultrahot", "Vicinity", "Vicenary", "Volition", "Voluming", "Wingding", "Wingspan", "Whatness", "Whatsits", "Yearlong", "Yeasayer", "Yokozuna", "Yourself", "Ziggurat", "Zigzaggy", "Zymology", "Zymogene",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PlayfairCycleWord,

        [SouvenirQuestion("What was the {1} correct answer you pressed in {0}?", "Poetry", 4, "clarity", "flow", "fatigue", "hollow", "energy", "sunshine", "ocean", "reflection", "identity", "black", "crowd", "heart", "weather", "words", "past", "solitary", "relax", "dance", "weightless", "morality", "gaze", "failure", "bunny", "lovely", "romance", "future", "focus", "search", "cookies", "compassion", "creation", "patience",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PoetryAnswers,

        [SouvenirQuestion("What was the starting position in {0}?", "Polyhedral Maze", 6, null, ExampleAnswers = new[] { "1", "16", "24", "47", "89" })]
        PolyhedralMazeStartPosition,

        [SouvenirQuestion("What was the missing frequency in the {1} wire in {0}?", "Probing", 4, "10Hz", "22Hz", "50Hz", "60Hz",
            ExampleExtraFormatArguments = new[] { "red-white", "yellow-black", "green", "gray", "yellow-red", "red-blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ProbingFrequencies,

        [SouvenirQuestion("What was the target word on {0}?", "Purple Arrows", 6, null, ExampleAnswers = new[] { "Thesis", "Immune", "Agency", "Height", "Active", "Bother", "Viable" })]
        PurpleArrowsFinish,

        [SouvenirQuestion("What was the {1} digit in the {2} slot in {0}?", "Quintuples", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        QuintuplesNumbers,
        [SouvenirQuestion("What color was the {1} digit in the {2} slot in {0}?", "Quintuples", 4, "red", "blue", "orange", "green", "pink",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        QuintuplesColors,
        [SouvenirQuestion("How many numbers were {1} in {0}?", "Quintuples", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25",
            ExampleExtraFormatArguments = new[] { "red", "blue", "orange", "green", "pink" }, ExampleExtraFormatArgumentGroupSize = 1)]
        QuintuplesColorCounts,

        [SouvenirQuestion("What was the starting number in {0}?", "Red Arrows", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        RedArrowsStartNumber,

        [SouvenirQuestion("Which one of these houses was on offer, but not chosen by Bob in {0}?", "Retirement", 4, null, ExampleAnswers = new[] { "Hotham Place", "Homestead", "Riverwell", "Lodge Park" })]
        RetirementHouses,

        [SouvenirQuestion("What was the {1} character in the {2} message of {0}?", "Reverse Morse", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second", "third", "first", "third", "second", "fourth", "first", "fourth", "second", "fifth", "first", "fifth", "second", "sixth", "first", "sixth", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        ReverseMorseCharacters,

        [SouvenirQuestion("What was the exit coordinate in {0}?", "RGB Maze", 6, "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "G1", "G2", "G3", "G4", "G5", "G6", "G7", "G8", "H1", "H2", "H3", "H4", "H5", "H6", "H7", "H8")]
        RGBMazeExit,
        [SouvenirQuestion("Where was the {1} key in {0}?", "RGB Maze", 6, "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "G1", "G2", "G3", "G4", "G5", "G6", "G7", "G8", "H1", "H2", "H3", "H4", "H5", "H6", "H7", "H8",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        RGBMazeKeys,
        [SouvenirQuestion("Which maze number was the {1} maze in {0}?", "RGB Maze", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        RGBMazeNumber,

        [SouvenirQuestion("What was the last color in {0}?", "Rhythms", 4, "Blue", "Red", "Green", "Yellow")]
        RhythmsColor,

        [SouvenirQuestion("What was the rule number when you solved {0}?", "Rule", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", AddThe = true)]
        RuleNumber,

        [SouvenirQuestion("What was the number to the correct condition in {0}?", "Role Reversal", 6, "2", "3", "4", "5", "6", "7", "8")]
        RoleReversalNumber,
        [SouvenirQuestion("How many {1} wires were there in {0}?", "Role Reversal", 6, "0", "1", "2", "3", "4", "5", "6", "7",
            ExampleExtraFormatArguments = new[] { "warm-colored", "cold-colored", "primary-colored", "secondary-colored" }, ExampleExtraFormatArgumentGroupSize = 1)]
        RoleReversalWires,

        [SouvenirQuestion("What was the contestant’s name in {0}?", "Schlag den Bomb", 4, "Ron", "Don", "Julia", "Cory", "Greg", "Paula", "Val", "Lisa", "Ozy", "Ozzy", "Elsa", "Cori", "Harry", "Gale", "Daniel", "Albert", "Spike", "Tommy", "Greta", "Tina", "Rob", "Edgar", "Julia", "Peter", "Millie", "Isolde", "Eris")]
        SchlagDenBombContestantName,
        [SouvenirQuestion("What was the contestant’s score in {0}?", "Schlag den Bomb", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75")]
        SchlagDenBombContestantScore,
        [SouvenirQuestion("What was the bomb’s score in {0}?", "Schlag den Bomb", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75")]
        SchlagDenBombBombScore,

        [SouvenirQuestion("What were the first and second words in the {1} phrase you answered correctly in {0}?", "Sea Shells", 4, "she sells", "she shells", "sea shells", "sea sells",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells1,
        [SouvenirQuestion("What were the third and fourth words in the {1} phrase you answered correctly in {0}?", "Sea Shells", 4, "sea shells", "she shells", "sea sells", "she sells",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells2,
        [SouvenirQuestion("What was the end of the {1} phrase you answered correctly in {0}?", "Sea Shells", 4, "sea shore", "she sore", "she sure", "seesaw",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells3,

        [SouvenirQuestion("What was the initial letter in {0}?", "Shapes And Bombs", 6, "A", "B", "D", "E", "G", "I", "K", "L", "N", "O", "P", "S", "T", "X", "Y")]
        ShapesAndBombsInitialLetter,

        [SouvenirQuestion("What was the initial shape in {0}?", "Shape Shift", 4, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", Type = AnswerType.SymbolsFont)]
        ShapeShiftInitialShape,

        [SouvenirQuestion("What was the initial position of the pearl in {0}?", "Shell Game", 4, "Left", "Middle", "Right")]
        ShellGameStartingCup,

        [SouvenirQuestion("What was the {1} slot in the {2} stage in {0}?", "Silly Slots", 4, "red bomb", "red cherry", "red coin", "red grape", "green bomb", "green cherry", "green coin", "green grape", "blue bomb", "blue cherry", "blue coin", "blue grape",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third", "third", "first", "third", "second", "third", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SillySlots,

        [SouvenirQuestion("What were the call samples {1} of {0}?", "Simon Samples", 6, "KKSS", "KKSH", "KSSH", "KHSS", "KHSH", "KHSO", "KHOH", "KOSH", "KOSO", "SKSK", "SHHS",
            ExampleExtraFormatArguments = new[] { "played in the first stage", "added in the second stage", "added in the third stage" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSamplesSamples,

        [SouvenirQuestion("What color flashed {1} in the final sequence in {0}?", "Simon Says", 4, "red", "yellow", "green", "blue",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSaysFlash,

        [SouvenirQuestion("What was the last flashed color in {0}?", "Simon Scrambles", 4, "Red", "Green", "Blue", "Yellow")]
        SimonScramblesLastColor,

        [SouvenirQuestion("Which color flashed {1} in the final sequence in {0}?", "Simon Screams", 6, "Red", "Orange", "Yellow", "Green", "Blue", "Purple",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonScreamsFlashing,

        [SouvenirQuestion("In which stage(s) of {0} was “{1}” the applicable rule?", "Simon Screams", 4, null, ExampleAnswers = new[] { "first", "second", "third", "first and second", "first and third", "second and third", "all of them" },
            ExampleExtraFormatArguments = new[] {
                "three adjacent colors flashing in clockwise order",
                "a color flashing, then an adjacent color, then the first again",
                "at most one color flashing out of red, yellow, and blue",
                "two colors opposite each other that didn’t flash",
                "two (but not three) adjacent colors flashing in clockwise order"
            }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonScreamsRule,

        [SouvenirQuestion("Which color flashed {1} in the {2} stage of {0}?", "Simon Selects", 6, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Magenta", "Cyan",
             ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonSelectsOrder,

        [SouvenirQuestion("What was the {1} received letter in {0}?", "Simon Sends", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSendsReceivedLetters,

        [SouvenirQuestion("What was the {1} flash in the final sequence in {0}?", "Simon Simons", 6, "TR", "TY", "TG", "TB", "LR", "LY", "LG", "LB", "RR", "RY", "RG", "RB", "BR", "BY", "BG", "BB",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSimonsFlashingColors,

        [SouvenirQuestion("Which key’s color flashed {1} in the {2} stage of {0}?", "Simon Sings", 6, "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third", "third", "first", "third", "second", "third", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonSingsFlashing,

        [SouvenirQuestion("How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?", "Simon Shrieks", 6, "0", "1", "2", "3", "4", "5", "6",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonShrieksFlashingButton,

        [SouvenirQuestion("Which color was the bubble that flashed {1} in {0}?", "Simon Speaks", 6, "black", "blue", "green", "cyan", "red", "purple", "yellow", "white", "gray",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSpeaksColors,
        [SouvenirQuestion("Which word was in the bubble that flashed {1} in {0}?", "Simon Speaks", 6, "black", "sort", "zwart", "nigra", "musta", "noir", "schwarz", "fekete", "nero", "blue", "blå", "blauw", "blua", "sininen", "bleu", "blau", "kék", "blu", "green", "grøn", "groen", "verda", "vihreä", "vert", "grün", "zöld", "verde", "cyan", "turkis", "turkoois", "turkisa", "turkoosi", "turquoise", "türkis", "türkiz", "turchese", "red", "rød", "rood", "ruĝa", "punainen", "rouge", "rot", "piros", "rosso", "purple", "lilla", "purper", "purpura", "purppura", "pourpre", "lila", "bíbor", "porpora", "yellow", "gul", "geel", "flava", "keltainen", "jaune", "gelb", "sárga", "giallo", "white", "hvid", "wit", "blanka", "valkoinen", "blanc", "weiß", "fehér", "bianco", "gray", "grå", "grijs", "griza", "harmaa", "gris", "grau", "szürke", "grigio",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSpeaksWords,
        [SouvenirQuestion("Which bubble flashed {1} in {0}?", "Simon Speaks", 6, "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSpeaksPositions,

        [SouvenirQuestion("Which color flashed {1} in sequence in {0}?", "Simon’s Star", 6, "red", "yellow", "green", "blue", "purple",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonsStarColors,

        [SouvenirQuestion("Which {1} in the {2} stage in {0}?", "Simon States", 4, "Red", "Yellow", "Green", "Blue", "Red, Yellow", "Red, Green", "Red, Blue", "Yellow, Green", "Yellow, Blue", "Green, Blue", "all 4", "none",
            ExampleExtraFormatArguments = new[] { "color(s) flashed", "first", "color(s) didn’t flash", "first", "color(s) flashed", "second", "color(s) didn’t flash", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStatesDisplay,

        [SouvenirQuestion("Which color flashed {1} in the output sequence in {0}?", "Simon Stops", 6, "Red", "Orange", "Yellow", "Green", "Blue", "Violet",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonStopsColors,

        [SouvenirQuestion("Which color {1} {2} in the final sequence of {0}?", "Simon Stores", 4, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
            ExampleExtraFormatArguments = new[] { "flashed", "first", "flashed", "second", "was one of the colors flashed", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStoresColors,
        [SouvenirQuestion("What was the correct answer in stage {1} of {0}?", "Simon Stores", 6, null, ExampleAnswers = new[] { "1", "-58", "12", "25", "364", "-244" },
            ExampleExtraFormatArguments = new[] { "1", "2", "3" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonStoresAnswers,

        [SouvenirQuestion("What were the original numbers when you solved {0}?", "Skewed Slots", 6, null, ExampleAnswers = new[] { "123", "847", "000", "245", "961", "253", "858" })]
        SkewedSlotsOriginalNumbers,

        [SouvenirQuestion("Which race was selectable, but not the solution, in {0}?", "Skyrim", 4, "Nord", "Khajiit", "Breton", "Argonian", "Dunmer", "Altmer", "Redguard", "Orc", "Imperial")]
        SkyrimRace,
        [SouvenirQuestion("Which weapon was selectable, but not the solution, in {0}?", "Skyrim", 4, "Axe of Whiterun", "Dawnbreaker", "Windshear", "Blade of Woe", "Firiniel’s End", "Bow of Hunt", "Volendrung", "Chillrend", "Mace of Molag Bal")]
        SkyrimWeapon,
        [SouvenirQuestion("Which enemy was selectable, but not the solution, in {0}?", "Skyrim", 4, "Alduin", "Blood Dragon", "Cave Bear", "Dragon Priest", "Draugr", "Draugr Overlord", "Frost Troll", "Frostbite Spider", "Mudcrab")]
        SkyrimEnemy,
        [SouvenirQuestion("Which city was selectable, but not the solution, in {0}?", "Skyrim", 4, "Dawnstar", "Ivarstead", "Markarth", "Riverwood", "Rorikstead", "Solitude", "Whiterun", "Windhelm", "Winterhold")]
        SkyrimCity,
        [SouvenirQuestion("Which dragon shout was selectable, but not the solution, in {0}?", "Skyrim", 4, "Disarm", "Dismay", "Dragonrend", "Fire Breath", "Ice Form", "Kyne’s Peace", "Slow Time", "Unrelenting Force", "Whirlwind Sprint")]
        SkyrimDragonShout,

        [SouvenirQuestion("How many red balls were there at the start of {0}?", "Snooker", 4, "8", "9", "10")]
        SnookerReds,

        [SouvenirQuestion("What positions were the last swap used to solve {0}?", "Sorting", 6, "1 & 2", "1 & 3", "1 & 4", "1 & 5", "2 & 3", "2 & 4", "2 & 5", "3 & 4", "3 & 5", "4 & 5")]
        SortingLastSwap,

        [SouvenirQuestion("What was the first module asked about in the other Souvenir on this bomb?", "Souvenir", 4, null,
            ExampleAnswers = new[] { "Probing", "Microcontroller", "Third Base", "Kudosudoku", "Quintuples", "3D Tunnels", "Uncolored Squares", "Pattern Cube", "Synonyms", "The Moon", "Human Resources", "Algebra" })]
        SouvenirFirstQuestion,

        [SouvenirQuestion("What was the {1} picture on {0}?", "Sonic The Hedgehog", 4, "Annoyed Sonic", "Ballhog", "Blue Lamppost", "Burrobot", "Buzz Bomber", "Crab Meat", "Dead Sonic", "Drowned Sonic", "Falling Sonic", "Moto Bug", "Red Lamppost", "Red Spring", "Standing Sonic", "Switch", "Yellow Spring",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SonicTheHedgehogPictures,

        [SouvenirQuestion("Which sound was played by the {1} screen on {0}?", "Sonic The Hedgehog", 4, "Boss Theme", "Breathe", "Continue", "Drown", "Emerald", "Extra Life", "Final Zone", "Invincibility", "Jump", "Lamppost", "Marble Zone", "Bumper", "Skid", "Spikes", "Spin", "Spring",
            ExampleExtraFormatArguments = new[] { "Running Boots", "Invincibility", "Extra Life", "Rings" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SonicTheHedgehogSounds,

        [SouvenirQuestion("What was the {1} flashed color in {0}?", "Sphere", 6, "red", "blue", "green", "orange", "pink", "purple", "grey", "white",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        SphereColors,

        [SouvenirQuestion("What bag was initially colored in {0}?", "Splitting The Loot", 6, null, ExampleAnswers = new[] { "A5", "E6", "19", "82" })]
        SplittingTheLootColoredBag,

        [SouvenirQuestion("What color was the {1} LED on the {2} flip when you solved {0}?", "Switch", 6, "red", "orange", "yellow", "green", "blue", "purple",
            ExampleExtraFormatArguments = new[] { "top", "first", "bottom", "first", "top", "second", "bottom", "second" }, ExampleExtraFormatArgumentGroupSize = 2, AddThe = true)]
        SwitchInitialColor,

        [SouvenirQuestion("What was the initial position of the switches in {0}?", "Switches", 6, "RRRRR", "QRRRR", "RQRRR", "QQRRR", "RRQRR", "QRQRR", "RQQRR", "QQQRR", "RRRQR", "QRRQR", "RQRQR", "QQRQR", "RRQQR", "QRQQR", "RQQQR", "QQQQR", "RRRRQ", "QRRRQ", "RQRRQ", "QQRRQ", "RRQRQ", "QRQRQ", "RQQRQ", "QQQRQ", "RRRQQ", "QRRQQ", "RQRQQ", "QQRQQ", "RRQQQ", "QRQQQ", "RQQQQ", "QQQQQ",
            Type = AnswerType.SymbolsFont)]
        SwitchesInitialPosition,

        [SouvenirQuestion("How many symbols were cycling on the {1} screen in the last successful round on {0}?", "Symbol Cycle", 4, "2", "3", "4", "5",
            ExampleExtraFormatArguments = new[] { "left", "right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SymbolCycleSymbolCounts,

        [SouvenirQuestion("What was the {1} symbol in the {2} stage of {0}?", "Symbolic Coordinates", 6, null,
            ExampleExtraFormatArguments = new[] { "left", "first", "left", "second", "left", "third", "middle", "first", "middle", "second", "middle", "third", "right", "first", "right", "second", "right", "third" }, ExampleExtraFormatArgumentGroupSize = 2, Type = AnswerType.Sprites)]
        SymbolicCoordinateSymbols,

        [SouvenirQuestion("Which number was displayed on {0}?", "Synonyms", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        SynonymsNumber,

        [SouvenirQuestion("What was the received word in {0}?", "Tap Code", 4, null, ExampleAnswers = new[] { "child", "style", "shake", "alive", "axion", "wreck", "cause", "pupil", "cheat", "watch" })]
        TapCodeReceivedWord,

        [SouvenirQuestion("What was the {1} flashed color in {0}?", "Tasha Squeals", 4, "Pink", "Green", "Yellow", "Blue",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TashaSquealsColors,

        [SouvenirQuestion("What was the initial color of the {1} button in the {2} stage of {0}?", "Ten-Button Color Code", 4, "red", "green", "blue",
            ExampleExtraFormatArguments = new[] { "first", "first", "second", "first", "first", "second", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        TenButtonColorCodeInitialColors,

        [SouvenirQuestion("What was the displayed letter in {0}?", "Text Field", 6, "A", "B", "C", "D", "E", "F")]
        TextFieldDisplay,

        [SouvenirQuestion("What was the position from top to bottom of the first wire needing to be cut in the last successful round of {0}?", "Thinking Wires", 6, "1", "2", "3", "4", "5", "6", "7")]
        ThinkingWiresFirstWire,
        [SouvenirQuestion("What color did the second valid wire to cut have to have during the last successful round of {0}?", "Thinking Wires", 6, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", "Black", "Any")]
        ThinkingWiresSecondWire,
        [SouvenirQuestion("What was the display number in the last successful round of {0}?", "Thinking Wires", 6, "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "69")]
        ThinkingWiresDisplayNumber,

        [SouvenirQuestion("What was the display word in the {1} successful stage on {0}?", "Third Base", 6, "NHXS", "IH6X", "XI8Z", "I8O9", "XOHZ", "H68S", "8OXN", "Z8IX", "SXHN", "6NZH", "H6SI", "6O8I", "NXO8", "66I8", "S89H", "SNZX", "9NZS", "8I99", "ZHOX", "SI9X", "SZN6", "ZSN8", "HZN9", "X9HI", "IS9H", "XZNS", "X6IS", "8NSZ",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ThirdBaseDisplay,

        [SouvenirQuestion("What was on the {1} button at the start of {0}?", "Tic Tac Toe", 6, "1", "2", "3", "4", "5", "6", "7", "8", "9", "O", "X", Type = AnswerType.TicTacToeFont,
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TicTacToeInitialState,

        [SouvenirQuestion("What was the {1} city in {0}?", "Timezone", 4, "Alofi", "Papeete", "Unalaska", "Whitehorse", "Denver", "Managua", "Quito", "Manaus", "Buenos Aires", "Sao Paulo", "Praia", "Edinburgh", "Berlin", "Bujumbura", "Moscow", "Tbilisi", "Lahore", "Omsk", "Bangkok", "Beijing", "Tokyo", "Brisbane", "Sydney", "Tarawa",
            ExampleExtraFormatArguments = new[] { "departure", "destination" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TimezoneCities,

        [SouvenirQuestion("What was the {1} received message in {0}?", "Transmitted Morse", 4, "BOMBS", "SHORT", "UNDERSTOOD", "W1RES", "SOS", "MANUAL", "STRIKED", "WEREDEAD", "GOTASOUV", "EXPLOSION", "EXPERT", "RIP", "LISTEN", "DETONATE", "ROGER", "WELOSTBRO", "AMIDEAF", "KEYPAD", "DEFUSER", "NUCLEARWEAPONS", "KAPPA", "DELTA", "PI3", "SMOKE", "SENDHELP", "LOST", "SWAN", "NOMNOM", "BLUE", "BOOM", "CANCEL", "DEFUSED", "BROKEN", "MEMORY", "R6S8T", "TRANSMISSION", "UMWHAT", "GREEN", "EQUATIONSX", "RED", "ENERGY", "JESTER", "CONTACT", "LONG",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TransmittedMorseMessage,

        [SouvenirQuestion("What was the {1} line you commented out in {0}?", "Turtle Robot", 4, null, ExampleAnswers = new[] { "LT 90", "FD 1", "RT 180 2", "LT 90 2", "RT 180", "FD 6", "RT 90 2" },
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.TurtleRobotFont)]
        TurtleRobotCodeLines,

        [SouvenirQuestion("What was the {1} correct query response from {0}?", "Two Bits", 6, "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99",
            ExampleExtraFormatArguments = new[] { "first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TwoBitsResponse,

        [SouvenirQuestion("What was the {1} in the last successful round of {0}?", "Ultimate Cycle", 4, "Advanced", "Adverted", "Advocate", "Addition", "Allocate", "Allotype", "Allotted", "Altering", "Binaries", "Binormal", "Binomial", "Billions", "Bulkhead", "Bullhorn", "Bulleted", "Bulwarks", "Ciphered", "Circuits", "Connects", "Conquers", "Commando", "Compiler", "Computer", "Continue", "Decrypts", "Deceived", "Decimate", "Division", "Discover", "Discrete", "Dispatch", "Disposal", "Encipher", "Encrypts", "Encoding", "Entrance", "Equalise", "Equators", "Equation", "Equipped", "Finalise", "Finished", "Findings", "Finnicky", "Formulae", "Fortunes", "Fortress", "Forwards", "Garrison", "Garnered", "Gatepost", "Gateways", "Gauntlet", "Gambling", "Gathered", "Glooming", "Hazarded", "Haziness", "Hotlinks", "Hotheads", "Hundreds", "Hunkered", "Huntsman", "Huntress", "Incoming", "Indicate", "Indirect", "Indigoes", "Illuding", "Illusion", "Illusory", "Illumine", "Jigsawed", "Jimmying", "Journeys", "Jousting", "Junction", "Juncture", "Junkyard", "Judgment", "Kilowatt", "Kilovolt", "Kilobyte", "Kinetics", "Knocking", "Knockout", "Knowable", "Knuckled", "Language", "Landmark", "Limiting", "Linearly", "Lingered", "Linkages", "Linkwork", "Labeling", "Monogram", "Monolith", "Monomial", "Monotone", "Multiton", "Multiply", "Mulcting", "Mulligan", "NANOBOTS", "Nanogram", "Nanowatt", "Nanotube", "Numbered", "Numerous", "Numerals", "Numerate", "Octangle", "Octuples", "Ordering", "Ordinals", "Observed", "Obscured", "Obstruct", "Obstacle", "Progress", "Projects", "Prophase", "Prophecy", "Postsync", "Possible", "Positron", "Positive", "Quadrant", "Quadrics", "Quartile", "Quartics", "Quickest", "Quirkish", "Quintics", "Quitters", "Reversed", "Revolved", "Revealed", "Rotation", "Rotators", "Relation", "Relative", "Relaying", "Starting", "Standard", "Standout", "Stanzaic", "Stoccata", "Stockade", "Stopping", "Stopword", "Trickier", "Trigonal", "Triggers", "Triangle", "Tomogram", "Tomahawk", "Toggling", "Together", "Underrun", "UnderwaY", "Underlie", "Undoings", "Ulterior", "Ultimate", "Ultrared", "Ultrahot", "Venomous", "Vendetta", "Vicinity", "Viceless", "Volition", "Voltages", "Volatile", "Voluming", "Weakened", "Weaponed", "Wingding", "Winnable", "Whatever", "Whatness", "Whatnots", "Whatsits", "Yellowed", "Yearlong", "Yearning", "Yeasayer", "Yielding", "Yielders", "Yokozuna", "Yourself", "Zippered", "Ziggurat", "Zigzaggy", "Zugzwang", "Zygomata", "Zygotene", "Zymology", "Zymogram",
          ExampleExtraFormatArguments = new[] { "message", "response" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UltimateCycleWord,

        [SouvenirQuestion("What was the {1} rotation in {0}?", "Ultracube", 6, "XY", "YX", "XZ", "ZX", "XW", "WX", "XV", "VX", "YZ", "ZY", "YW", "WY", "YV", "VY", "ZW", "WZ", "ZV", "VZ", "WV", "VW", AddThe = true,
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UltracubeRotations,

        [SouvenirQuestion("What was the {1} color in reading order used in the first stage of {0}?", "Uncolored Squares", 6, "White", "Red", "Blue", "Green", "Yellow", "Magenta",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UncoloredSquaresFirstStage,

        [SouvenirQuestion("What was the {1} received instruction in {0}?", "Unfair Cipher", 6, "PCR", "PCG", "PCB", "SUB", "MIT", "CHK", "PRN", "BOB", "REP", "EAT", "STR", "IKE",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UnfairCipherInstructions,

        [SouvenirQuestion("What was the {1} submitted letter in {0}?", "Unown Cipher", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            Type = AnswerType.UnownFont, ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UnownCipherAnswers,

        [SouvenirQuestion("What state did you depart from in {0}?", "USA Maze", 4, "Alaska", "Alabama", "Arkansas", "Arizona", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming")]
        USAMazeOrigin,

        [SouvenirQuestion("What was the initially pressed color on {0}?", "Varicolored Squares", 6, "White", "Red", "Blue", "Green", "Yellow", "Magenta")]
        VaricoloredSquaresInitialColor,

        [SouvenirQuestion("What was the color of the {1} vector in the last successful round on {0}?", "Vectors", 6, "Red", "Orange", "Yellow", "Green", "Blue", "Purple",
        ExampleExtraFormatArguments = new[] { "first", "second", "third", "only" }, ExampleExtraFormatArgumentGroupSize = 1)]
        VectorsColors,

        [SouvenirQuestion("What was the {1} flagpole color on {0}?", "Vexillology", 6, "Red", "Orange", "Green", "Yellow", "Blue", "Aqua", "White", "Black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        VexillologyColors,

        [SouvenirQuestion("What was the desired color in the {1} successful round on {0}?", "Visual Impairment", 4, "Blue", "Green", "Red", "White",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        VisualImpairmentColors,

        [SouvenirQuestion("What was the color on the {1} stage in {0}?", "Wavetapping", 4, "Red", "Orange", "Orange-Yellow", "Chartreuse", "Lime", "Green", "Seafoam Green", "Cyan-Green", "Turquoise", "Dark Blue", "Indigo", "Purple", "Purple-Magenta", "Magenta", "Pink", "Gray",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WavetappingColors,
        [SouvenirQuestion("What was the correct pattern on the {1} stage in {0}?", "Wavetapping", 6, null,
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1, Type = AnswerType.Sprites)]
        WavetappingPatterns,

        [SouvenirQuestion("What was the display in the {1} successful stage on {0}?", "Who's on First", 6, "", "BLANK", "C", "CEE", "DISPLAY", "FIRST", "HOLD ON", "LEAD", "LED", "LEED", "NO", "NOTHING", "OK", "OKAY", "READ", "RED", "REED", "SAY", "SAYS", "SEE", "THEIR", "THERE", "THEY ARE", "THEY'RE", "U", "UR", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WhosOnFirstDisplay,

        [SouvenirQuestion("What was the color of the {1} dial when you solved {0}?", "Wire", 6, "blue", "green", "grey", "orange", "purple", "red",
            ExampleExtraFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        WireDialColors,

        [SouvenirQuestion("What was the displayed number when you solved {0}?", "Wire", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        WireDisplayedNumber,

        [SouvenirQuestion("How many {1} wires were there in {0}?", "Wire Sequence", 4, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "red", "blue", "black" }, ExampleExtraFormatArgumentGroupSize = 1)]
        WireSequenceColorCount,

        [SouvenirQuestion("What was the initial roll on {0}?", "Yahtzee", 4, "Yahtzee", "large straight", "small straight", "four of a kind", "full house", "three of a kind", "two pairs", "pair")]
        YahtzeeInitialRoll,

        [SouvenirQuestion("What was the starting row letter in the last successful round of {0}?", "Yellow Arrows", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
        YellowArrowsStartingRow,

        [SouvenirQuestion("What was the {1} correctly decrypted word in {0}?", "Zoni", 6, null, ExampleAnswers = new[] { "angel", "thing", "dance", "heavy", "quote" },
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ZoniWords
    }

    enum AnswerType
    {
        // These values must match the indexes in SouvenirModule.Fonts/SouvenirModule.FontTextures
        Default = 0,
        SymbolsFont = 1,    // used by 3D Tunnels, Shape Shift, Switches and Colored Switches
        TurtleRobotFont = 2,
        TicTacToeFont = 3,  // also used by The Bulb
        UnownFont = 4,

        // Special value that doesn’t translate to a font
        Sprites = -1
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
        public AnswerType Type { get; set; }

        public string ModuleNameWithThe { get { return (AddThe ? "The " : "") + ModuleName; } }

        public SouvenirQuestionAttribute(string questionText, string moduleName, int numAnswers, params string[] allAnswers)
        {
            QuestionText = questionText;
            ModuleName = moduleName;
            NumAnswers = numAnswers;
            AllAnswers = allAnswers == null || allAnswers.Length == 0 ? null : allAnswers;
            Type = AnswerType.Default;
        }
    }

    abstract class QandA
    {
        public string ModuleNameWithThe { get; private set; }
        public string QuestionText { get; private set; }
        public int CorrectIndex { get; private set; }
        public int NumAnswers { get; private set; } // must be 4 or 6
        public QandA(string module, string question, int correct, int numAnswers)
        {
            ModuleNameWithThe = module;
            QuestionText = question;
            CorrectIndex = correct;
            NumAnswers = numAnswers;
        }
        public abstract void SetAnswers(SouvenirModule souvenir);
        public string DebugString { get { return string.Format("{0} — {1}", QuestionText, DebugAnswers.Select((a, ix) => string.Format(ix == CorrectIndex ? "[_{0}_]" : "{0}", a)).JoinString(" | ")); } }
        public abstract IEnumerable<string> DebugAnswers { get; }
        public abstract double DesiredHeightFactor { get; }
        public abstract void BlinkCorrectAnswer(bool on, SouvenirModule souvenir);

        protected void SetupAnswers(SouvenirModule souvenir, float xSpacing, float ySpacing)
        {
            var highlightMesh = NumAnswers > 4 ? souvenir.HighlightShort : souvenir.HighlightLong;
            for (int i = 0; i < souvenir.Answers.Length; i++)
            {
                souvenir.Answers[i].gameObject.SetActive(Application.isEditor || i < NumAnswers);
                souvenir.Answers[i].transform.Find("SpriteHolder").gameObject.SetActive(false);
                souvenir.Answers[i].transform.Find("AnswerText").gameObject.SetActive(false);
                var h1 = souvenir.Answers[i].transform.Find("Highlight");
                h1.GetComponent<MeshFilter>().sharedMesh = highlightMesh;
                var h2 = h1.Find("Highlight(Clone)");
                if (h2 != null)
                    h2.GetComponent<MeshFilter>().sharedMesh = highlightMesh;
                souvenir.Answers[i].transform.localPosition = new Vector3(-18.125f + xSpacing * (i / 2), 2.525f, -16.25f + ySpacing * (1 - i % 2));
                souvenir.Answers[i].GetComponent<BoxCollider>().center = new Vector3(NumAnswers > 4 ? 5 : 8, 0, 0);
                souvenir.Answers[i].GetComponent<BoxCollider>().size = new Vector3(NumAnswers > 4 ? 13 : 19, 6, 3);
            }
        }
    }

    sealed class QandAText : QandA
    {
        private readonly string[] _answers;
        private readonly Font _font;
        private readonly Texture _fontTexture;
        private readonly Material _fontMaterial;
        public QandAText(string module, string question, int correct, string[] answers, Font font, Texture fontTexture, Material fontMaterial) : base(module, question, correct, answers.Length)
        {
            _answers = answers;
            _font = font;
            _fontTexture = fontTexture;
            _fontMaterial = fontMaterial;
        }
        public override IEnumerable<string> DebugAnswers { get { return _answers; } }
        public override double DesiredHeightFactor { get { return 1.1; } }

        public override void SetAnswers(SouvenirModule souvenir)
        {
            SetupAnswers(souvenir, _answers.Length > 4 ? 13.125f : 19.375f, 6.25f);
            for (int i = 0; i < souvenir.Answers.Length; i++)
            {
                var mesh = souvenir.Answers[i].transform.Find("AnswerText").GetComponent<TextMesh>();
                mesh.gameObject.SetActive(true);

                mesh.text = i < _answers.Length ? _answers[i] : "•";
                mesh.font = _font;
                mesh.GetComponent<MeshRenderer>().material = _fontMaterial;
                mesh.GetComponent<MeshRenderer>().material.mainTexture = _fontTexture;

                // Determine size of the answer and if it’s too long, shrink it horizontally to make it fit
                var origRotation = mesh.transform.localRotation;
                mesh.transform.eulerAngles = new Vector3(90, 0, 0);
                mesh.transform.localScale = new Vector3(1, 1, 1);
                var bounds = mesh.GetComponent<Renderer>().bounds.size;
                var fac = (_answers.Length > 4 ? .45 : .7) * souvenir.SurfaceSizeFactor;
                if (bounds.x > fac)
                    mesh.transform.localScale = new Vector3((float) (fac / bounds.x), 1, 1);
                mesh.transform.localRotation = origRotation;
            }
        }

        public override void BlinkCorrectAnswer(bool on, SouvenirModule souvenir)
        {
            souvenir.Answers[CorrectIndex].transform.Find("AnswerText").gameObject.SetActive(on);
        }
    }

    sealed class QandASprite : QandA
    {
        private readonly Sprite[] _answers;
        public QandASprite(string module, string question, int correct, Sprite[] answers) : base(module, question, correct, answers.Length) { _answers = answers; }
        public override IEnumerable<string> DebugAnswers { get { return _answers.Select(s => s.name); } }
        public override double DesiredHeightFactor { get { return 1; } }

        public override void SetAnswers(SouvenirModule souvenir)
        {
            SetupAnswers(souvenir, _answers.Length > 4 ? 13.125f : 19.375f, 7.75f);
            for (int i = 0; i < souvenir.Answers.Length; i++)
            {
                var spriteRenderer = souvenir.Answers[i].transform.Find("SpriteHolder").GetComponent<SpriteRenderer>();
                spriteRenderer.gameObject.SetActive(i < _answers.Length);
                spriteRenderer.sprite = i < _answers.Length ? _answers[i] : null;
            }
        }

        public override void BlinkCorrectAnswer(bool on, SouvenirModule souvenir)
        {
            souvenir.Answers[CorrectIndex].transform.Find("SpriteHolder").gameObject.SetActive(on);
        }
    }
}
