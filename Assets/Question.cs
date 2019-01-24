using System;
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
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1, Font = AnswerFont.SymbolsFont)]
        _3DTunnelsTargetNode,

        [SouvenirQuestion("Which item was the {1} correct item you used in {0}?", "Adventure Game", 4, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGameCorrectItem,

        [SouvenirQuestion("What enemy were you fighting in {0}?", "Adventure Game", 4, "Dragon", "Demon", "Eagle", "Goblin", "Troll", "Wizard", "Golem", "Lizard")]
        AdventureGameEnemy,

        [SouvenirQuestion("Which symbol was {1} at the start of the last successful brew in {0}?", "Alchemy", 4, "Creation", "Fire", "Heva", "Meta", "Strucota", "Tera",
            ExampleExtraFormatArguments = new[] { "in the center", "at 1 o’clock", "at 3 o’clock", "at 5 o’clock", "at 7 o’clock", "at 9 o’clock", "at 11 o’clock" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AlchemySymbols,

        [SouvenirQuestion("What was the first equation in {0}?", "Algebra", 4, "a=3z", "a=5+y", "a=6-x", "a=7x", "a=8y", "a=9+z", "a=x/2", "a=x+1", "a=y/4", "a=y-2", "a=z/10", "a=z-7")]
        AlgebraEquation1,

        [SouvenirQuestion("What was the second equation in {0}?", "Algebra", 4, "b=(2x/10)-y", "b=(7x)y", "b=(x+y)-(z/2)", "b=(y/2)-z", "b=(zy)-(2x)", "b=(z-y)/2", "b=2(z+7)", "b=2z+7", "b=xy-(2+x)", "b=xyz")]
        AlgebraEquation2,

        [SouvenirQuestion("What color was {1} in the solution to {0}?", "Big Circle", 6, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BigCircleColors,

        [SouvenirQuestion("At which numeric value did you cut the correct wire in {0}?", "Binary LEDs", 6, null, ExampleAnswers = new[] { "1", "5", "12", "19", "25", "31" })]
        BinaryLEDsValue,

        [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", "Bitmaps", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16",
            ExampleExtraFormatArguments = new[] { "white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 2)]
        Bitmaps,

        [SouvenirQuestion("What was the final solution word in {0}?", "Braille", 6, "acting", "dating", "heading", "meaning", "server", "aiming", "dealer", "hearing", "miners", "shaking", "artist", "eating", "heating", "nearer", "sought", "asking", "eighth", "higher", "parish", "staying", "bearing", "farmer", "insist", "parker", "strands", "beating", "farming", "lasted", "parking", "strings", "beings", "faster", "laying", "paying", "teaching", "binding", "father", "leader", "powers", "tended", "bought", "finding", "leading", "pushed", "tender", "boxing", "finest", "leaned", "pushing", "testing", "breach", "finish", "leaning", "rather", "throwing", "breast", "flying", "leaving", "reaching", "towers", "breath", "foster", "linking", "reader", "vested", "breathe", "fought", "listed", "reading", "warned", "bringing", "gaining", "listen", "resting", "warning", "brings", "gather", "living", "riding", "weaker", "carers", "gazing", "making", "rushed", "wealth", "carter", "gender", "marked", "rushing", "winner", "charter", "growing", "marking", "saying", "winning", "crying", "headed", "master", "served", "winter")]
        BrailleWord,

        [SouvenirQuestion("What was the {1} correct button you pressed in {0}?", "Broken Buttons", 6, "bomb", "blast", "boom", "burst", "wire", "button", "module", "light", "led", "switch", "RJ-45", "DVI-D", "RCA", "PS/2", "serial", "port", "row", "column", "one", "two", "three", "four", "five", "six", "seven", "eight", "size", "this", "that", "other", "submit", "abort", "drop", "thing", "blank", "broken", "too", "to", "yes", "see", "sea", "c", "wait", "word", "bob", "no", "not", "first", "hold", "late", "fail",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BrokenButtons,

        [SouvenirQuestion("What were the correct button presses in {0}?", "Bulb", 6, "OOO", "OOI", "OIO", "OII", "IOO", "IOI", "IIO", "III", AddThe = true)]
        BulbButtonPresses,

        [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Burglar Alarm", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BurglarAlarmDigits,

        [SouvenirQuestion("How many of the buttons in {0} were {1}?", "Button Sequences", 6, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12",
            ExampleExtraFormatArguments = new[] { "red", "blue", "yellow", "white" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ButtonSequencesColorOccurrences,

        [SouvenirQuestion("What was the LED color in {0}?", "Calendar", 4, "Green", "Yellow", "Red", "Blue")]
        CalendarLedColor,

        [SouvenirQuestion("What was the {1}paid amount in {0}?", "Cheap Checkout", 6, null, ExampleAnswers = new[] { "$11.00", "$12.00", "$14.00", "$17.00", "$24.00", "$25.00" },
            ExampleExtraFormatArguments = new[] { "", "first ", "second " }, ExampleExtraFormatArgumentGroupSize = 1)]
        CheapCheckoutPaid,

        [SouvenirQuestion("What was the {1} coordinate in {0}?", "Chess", 6, "a1", "a2", "a3", "a4", "a5", "a6", "b1", "b2", "b3", "b4", "b5", "b6", "c1", "c2", "c3", "c4", "c5", "c6", "d1", "d2", "d3", "d4", "d5", "d6", "e1", "e2", "e3", "e4", "e5", "e6", "f1", "f2", "f3", "f4", "f5", "f6",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChessCoordinate,

        [SouvenirQuestion("Which note was part of the given chord in {0}?", "Chord Qualities", 6, "A", "A♯", "B", "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯")]
        ChordQualitiesNotes,

        [SouvenirQuestion("What was the given chord quality in {0}?", "Chord Qualities", 6, "7", "-7", "Δ7", "-Δ7", "7♯9", "ø", "add9", "-add9", "7♯5", "Δ7♯5", "7sus", "-Δ7♯5")]
        ChordQualitiesQuality,

        [SouvenirQuestion("What was the {1}-stage indicator pattern in {0}?", "Color Decoding", 4, "Checkered", "Horizontal", "Vertical", "Solid",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ColorDecodingIndicatorPattern,

        [SouvenirQuestion("Which color {1} in the {2}-stage indicator pattern in {0}?", "Color Decoding", 4, "Black", "Green", "Purple", "Red", "Blue", "Yellow",
            ExampleExtraFormatArguments = new[] { "appeared", "first", "appeared", "second", "appeared", "third", "did not appear", "first", "did not appear", "second", "did not appear", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        ColorDecodingIndicatorColors,

        [SouvenirQuestion("What was the first color group in {0}?", "Colored Squares", 6, "White", "Red", "Blue", "Green", "Yellow", "Magenta")]
        ColoredSquaresFirstGroup,

        [SouvenirQuestion("What was the initial position of the switches in {0}?", "Colored Switches", 6, "RRRRR", "QRRRR", "RQRRR", "QQRRR", "RRQRR", "QRQRR", "RQQRR", "QQQRR", "RRRQR", "QRRQR", "RQRQR", "QQRQR", "RRQQR", "QRQQR", "RQQQR", "QQQQR", "RRRRQ", "QRRRQ", "RQRRQ", "QQRRQ", "RRQRQ", "QRQRQ", "RQQRQ", "QQQRQ", "RRRQQ", "QRRQQ", "RQRQQ", "QQRQQ", "RRQQQ", "QRQQQ", "RQQQQ", "QQQQQ",
            Font = AnswerFont.SymbolsFont)]
        ColoredSwitchesInitialPosition,
        [SouvenirQuestion("What was the position of the switches when the LEDs came on in {0}?", "Colored Switches", 6, "RRRRR", "QRRRR", "RQRRR", "QQRRR", "RRQRR", "QRQRR", "RQQRR", "QQQRR", "RRRQR", "QRRQR", "RQRQR", "QQRQR", "RRQQR", "QRQQR", "RQQQR", "QQQQR", "RRRRQ", "QRRRQ", "RQRRQ", "QQRRQ", "RRQRQ", "QRQRQ", "RQQRQ", "QQQRQ", "RRRQQ", "QRRQQ", "RQRQQ", "QQRQQ", "RRQQQ", "QRQQQ", "RQQQQ", "QQQQQ",
            Font = AnswerFont.SymbolsFont)]
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

        [SouvenirQuestion("Which button was the submit button in {0}?", "Double-Oh", 6, "↕", "⇕", "↔", "⇔", "◆")]
        DoubleOhSubmitButton,

        [SouvenirQuestion("What was the last pair of letters in {0}?", "Fast Math", 6, "AA", "AB", "AC", "AD", "AE", "AG", "AK", "AN", "AP", "AS", "AT", "AX", "AZ", "BA", "BB", "BC", "BD", "BE", "BG", "BK", "BN", "BP", "BS", "BT", "BX", "BZ", "CA", "CB", "CC", "CD", "CE", "CG", "CK", "CN", "CP", "CS", "CT", "CX", "CZ", "DA", "DB", "DC", "DD", "DE", "DG", "DK", "DN", "DP", "DS", "DT", "DX", "DZ", "EA", "EB", "EC", "ED", "EE", "EG", "EK", "EN", "EP", "ES", "ET", "EX", "EZ", "GA", "GB", "GC", "GD", "GE", "GG", "GK", "GN", "GP", "GS", "GT", "GX", "GZ", "KA", "KB", "KC", "KD", "KE", "KG", "KK", "KN", "KP", "KS", "KT", "KX", "KZ", "NA", "NB", "NC", "ND", "NE", "NG", "NK", "NN", "NP", "NS", "NT", "NX", "NZ", "PA", "PB", "PC", "PD", "PE", "PG", "PK", "PN", "PP", "PS", "PT", "PX", "PZ", "SA", "SB", "SC", "SD", "SE", "SG", "SK", "SN", "SP", "SS", "ST", "SX", "SZ", "TA", "TB", "TC", "TD", "TE", "TG", "TK", "TN", "TP", "TS", "TT", "TX", "TZ", "XA", "XB", "XC", "XD", "XE", "XG", "XK", "XN", "XP", "XS", "XT", "XX", "XZ", "ZA", "ZB", "ZC", "ZD", "ZE", "ZG", "ZK", "ZN", "ZP", "ZS", "ZT", "ZX", "ZZ")]
        FastMathLastLetters,

        [SouvenirQuestion("What was the last digit of your first query’s result in {0}?", "Functions", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        FunctionsLastDigit,

        [SouvenirQuestion("What were the numbers on {0}?", "Gamepad", 6, null, AddThe = true, ExampleAnswers = new[] { "01:03", "12:92", "84:72", "24:56" })]
        GamepadNumbers,

        [SouvenirQuestion("What was the starting location in {0}?", "Gridlock", 6, "A1", "B1", "C1", "D1", "A2", "B2", "C2", "D2", "A3", "B3", "C3", "D3", "A4", "B4", "C4", "D4")]
        GridLockStartingLocation,

        [SouvenirQuestion("What was the ending location in {0}?", "Gridlock", 6, "A1", "B1", "C1", "D1", "A2", "B2", "C2", "D2", "A3", "B3", "C3", "D3", "A4", "B4", "C4", "D4")]
        GridLockEndingLocation,

        [SouvenirQuestion("What was the starting color in {0}?", "Gridlock", 4, "Green", "Yellow", "Red", "Blue")]
        GridLockStartingColor,

        [SouvenirQuestion("What was the color of the pawn in {0}?", "Hexamaze", 6, "Red", "Yellow", "Green", "Cyan", "Blue", "Pink")]
        HexamazePawnColor,

        [SouvenirQuestion("Who was {1} in {0}?", "Human Resources", 6, "Rebecca", "Damian", "Jean", "Mike", "River", "Samuel", "Yoshi", "Caleb", "Ashley", "Tim", "Eliott", "Ursula", "Silas", "Noah", "Quinn", "Dylan",
            ExampleExtraFormatArguments = new[] { "an employee that was not fired", "an applicant that was not hired" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HumanResourcesEmployees,

        [SouvenirQuestion("Which was a descriptor shown in {1} in {0}?", "Human Resources", 4, "Intellectual", "Deviser", "Confidant", "Helper", "Auditor", "Innovator", "Defender", "Chameleon", "Director", "Designer", "Educator", "Advocate", "Manager", "Showman", "Contributor", "Entertainer",
            ExampleExtraFormatArguments = new[] { "red", "green" }, ExampleExtraFormatArgumentGroupSize = 1)]
        HumanResourcesDescriptors,

        [SouvenirQuestion("Which of the first three stages of {0} had the {1} symbol {2}?", "Hunting", 4, "none", "first", "second", "first two", "third", "first & third", "second & third", "all three",
            ExampleExtraFormatArguments = new[] { "column", "first", "row", "first", "column", "second", "row", "second", "column", "third", "row", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        HuntingColumnsRows,

        [SouvenirQuestion("Which one of these flavours {1} to the {2} customer in {0}?", "Ice Cream", 4, "Tutti Frutti", "Rocky Road", "Raspb. Ripple", "Double Choc.", "Double Str.", "Cookies & Cr.", "Neapolitan", "Mint Ch. Chip", "The Classic", "Vanilla",
            ExampleExtraFormatArguments = new[] { "was on offer, but not sold,", "first", "was not on offer", "first", "was on offer, but not sold,", "second", "was not on offer", "second", "was on offer, but not sold,", "third", "was not on offer", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        IceCreamFlavour,

        [SouvenirQuestion("Who was the {1} customer in {0}?", "Ice Cream", 6, "Mike", "Tim", "Tom", "Dave", "Adam", "Cheryl", "Sean", "Ashley", "Jessica", "Taylor", "Simon", "Sally", "Jade", "Sam", "Gary", "Victor", "George", "Jacob", "Pat", "Bob",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        IceCreamCustomer,

        [SouvenirQuestion("Which square (column, then row; A1 is top-left) was {1} in {0}?", "Kudosudoku", 6, "A1", "A2", "A3", "A4", "B1", "B2", "B3", "B4", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4",
            ExampleExtraFormatArguments = new[] { "pre-filled", "not pre-filled" }, ExampleExtraFormatArgumentGroupSize = 1)]
        KudosudokuPrefilled,

        [SouvenirQuestion("What was the correct code you entered in {0}?", "Listening", 6, null,
            ExampleAnswers = new[] { "&&&**", "&$#$&", "$#$*&", "#$$**", "$#$#*", "**$*#", "#$$&*", "##*$*", "$#*$&", "**#**", "#&&*#", "&#**&", "$&**#", "&#$$#", "$&&**", "#&$##", "&*$*$", "&$$&*", "#&&&&", "**$$$", "*&*&&", "*#&*&", "**###", "&&$&*", "&$**&", "#$#&$", "&#&&#", "$$*$*", "$&#$$", "&**$$", "$&&*&", "&$&##", "#&$*&", "$*$**", "*#$&&", "###&$", "*$$&$", "$*&##", "#&$&&", "$&$$*", "*$*$*" })]
        Listening,

        [SouvenirQuestion("What was the initial color of the {1} button in the {2} stage of {0}?", "Logical Buttons", 4, "Red", "Blue", "Green", "Yellow", "Purple", "White", "Orange", "Cyan", "Grey",
            ExampleExtraFormatArguments = new[] { "top", "first", "bottom-left", "first", "bottom-right", "first", "top", "second", "bottom-left", "second", "bottom-right", "second", "top", "third", "bottom-left", "third", "bottom-right", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        LogicalButtonsColor,
        [SouvenirQuestion("What was the initial label on the {1} button in the {2} stage of {0}?", "Logical Buttons", 4, "Logic", "Color", "Label", "Button", "Wrong", "Boom", "No", "Wait", "Hmmm",
            ExampleExtraFormatArguments = new[] { "top", "first", "bottom-left", "first", "bottom-right", "first", "top", "second", "bottom-left", "second", "bottom-right", "second", "top", "third", "bottom-left", "third", "bottom-right", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        LogicalButtonsLabel,
        [SouvenirQuestion("What was the initial operator in the {1} stage of {0}?", "Logical Buttons", 6, "AND", "OR", "XOR", "NAND", "NOR", "XNOR",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LogicalButtonsOperator,

        [SouvenirQuestion("What was {1} in {0}?", "Logic Gates", 6, "AND", "OR", "XOR", "NAND", "NOR", "XNOR",
            ExampleExtraFormatArguments = new[] { "gate A", "gate B", "gate C", "gate D", "gate E", "gate F", "gate G", "the duplicated gate" }, ExampleExtraFormatArgumentGroupSize = 1)]
        LogicGatesGates,

        [SouvenirQuestion("What was the first word in the name of the {1} {2} station in the last successful run on {0}?", "London Underground", 4, null, AddThe = true,
            ExampleExtraFormatArguments = new[] { "first", "departure", "second", "departure", "first", "destination", "second", "destination" }, ExampleExtraFormatArgumentGroupSize = 2)]
        LondonUndergroundStations,

        [SouvenirQuestion("Who was a player, but not the Godfather, in the last stage of {0}?", "Mafia", 6, "Rob", "Tim", "Mary", "Briane", "Hunter", "Macy", "John", "Will", "Lacy", "Claire", "Kenny", "Rick", "Walter", "Bonnie", "Luke", "Bill", "Sarah", "Larry", "Kate", "Stacy", "Diane", "Mac", "Jim", "Clyde", "Tommy", "Lenny", "Molly", "Benny", "Phil", "Bob", "Gary", "Ted", "Kim", "Nate", "Cher", "Ron", "Thomas", "Sam", "Duke", "Jack", "Ed", "Ronny", "Terry", "Claira", "Nick", "Cob", "Ash", "Don", "Jerry", "Simon")]
        MafiaPlayers,

        [SouvenirQuestion("What bearing was signalled in {0}?", "Maritime Flags", 6, "355", "109", "250", "308", "260", "356", "236", "28", "346", "78", "265", "357", "129", "23", "170", "121", "259", "316", "12", "105", "297", "107", "76", "190", "258", "332", "29", "99", "73", "165", "293", "43", "191", "279", "202", "193", "119", "294", "10", "194", "141", "1", "295", "217", "241", "94", "219", "177", "162", "214", "123", "228", "246", "306", "290", "163", "130", "198", "138", "324", "301", "225", "140", "20", "229", "27", "42", "13", "333", "147", "309", "327", "331", "289", "134", "158", "231", "238", "139", "351", "201", "143", "4", "137", "287", "60", "350", "62", "98", "11", "86", "243", "178", "133", "49", "337", "358", "89", "68", "172", "195", "323", "187", "262", "67", "15", "148", "118", "239", "145", "330", "156", "174", "227", "230", "204", "300", "292", "9", "210", "237", "354", "100", "30", "233", "74", "85", "253", "335", "153", "281", "164", "82", "221", "36", "325", "261", "161", "343", "182", "154", "65", "51", "205", "52", "298", "44", "189", "87", "282", "53", "111", "91", "285", "103", "69", "126", "110", "61", "71", "353", "215", "188", "166", "66", "41", "92", "277", "235", "155", "122", "283", "197", "116", "95", "81", "302", "276", "169", "180", "186", "34", "171", "340", "2", "106", "284", "5", "348", "244", "209", "157", "47", "310", "6", "97", "179", "252", "266", "31", "347", "249", "271", "77", "125", "75", "254", "113", "38", "334", "311", "159", "274", "135", "339", "149", "317", "37", "70", "108", "90", "183", "257", "315", "269", "329", "146", "35", "14", "167", "17", "26", "286", "39", "115", "33", "207", "93", "345", "79", "319", "291", "206", "58", "117", "22", "245", "220", "275", "25", "299", "247", "142", "45", "84", "196", "83", "50", "199", "268", "181", "19", "234", "273", "263", "132", "226", "59", "359", "57", "213", "314", "3", "242", "185", "203", "124", "342", "307", "255", "18", "54", "63", "270", "175", "305", "338", "267", "114", "251", "102", "150", "313", "127", "21", "173", "322", "46", "349", "131", "303", "318", "326", "55", "321", "212", "278", "7", "341", "223", "151", "211", "222", "218", "101")]
        MaritimeFlagsBearing,

        [SouvenirQuestion("Which callsign was signalled in {0}?", "Maritime Flags", 4, "1stmate", "2ndmate", "3rdmate", "abandon", "admiral", "advance", "aground", "allides", "anchors", "athwart", "azimuth", "bailers", "ballast", "barrack", "beached", "beacons", "beamend", "beamsea", "bearing", "beating", "belayed", "bermuda", "bobstay", "boilers", "bollard", "bonnets", "boomkin", "bounder", "bowline", "brailed", "breadth", "bridges", "brigged", "bringto", "bulwark", "bumboat", "bumpkin", "burthen", "caboose", "capsize", "capstan", "captain", "caravel", "careens", "carrack", "carrier", "catboat", "cathead", "chained", "channel", "charley", "charter", "citadel", "cleared", "cleated", "clinker", "clipper", "coaming", "coasted", "consort", "convoys", "corinth", "cotchel", "counter", "cranzes", "crewing", "cringle", "crojack", "cruiser", "cutters", "dandies", "deadrun", "debunks", "derrick", "dipping", "disrate", "dogvane", "doldrum", "dolphin", "draught", "drifter", "drogues", "drydock", "dunnage", "dunsels", "earings", "echelon", "embayed", "ensigns", "escorts", "fairway", "falkusa", "fantail", "fardage", "fathoms", "fenders", "ferries", "fitting", "flanked", "flaring", "flattop", "flemish", "floated", "floored", "flotsam", "folding", "follows", "forcing", "forward", "foulies", "founder", "framing", "freight", "frigate", "funnels", "furling", "galleon", "galleys", "galliot", "gangway", "garbled", "general", "georges", "ghosted", "ginpole", "giveway", "gondola", "graving", "gripies", "grounds", "growler", "guineas", "gundeck", "gunport", "gunwale", "halyard", "hammock", "hampers", "hangars", "harbors", "harbour", "hauling", "hawsers", "heading", "headsea", "heaving", "herring", "hogging", "holiday", "huffler", "inboard", "inirons", "inshore", "instays", "inwater", "inwayof", "jackies", "jacktar", "jennies", "jetties", "jiggers", "joggles", "jollies", "juryrig", "keelson", "kellets", "kicking", "killick", "kitchen", "lanyard", "laydays", "lazaret", "leehelm", "leeside", "leeward", "liberty", "lighter", "lizards", "loading", "lockers", "lofting", "lolling", "lookout", "lubbers", "luffing", "luggers", "lugsail", "maewest", "manowar", "marconi", "mariner", "matelot", "mizzens", "mooring", "mousing", "narrows", "nippers", "officer", "offpier", "oilskin", "oldsalt", "onboard", "oreboat", "outhaul", "outward", "painter", "panting", "parcels", "parleys", "parrels", "passage", "pelagic", "pendant", "pennant", "pickets", "pinnace", "pintles", "pirates", "pivoted", "pursers", "pursued", "quarter", "quaying", "rabbets", "ratline", "reduced", "reefers", "repairs", "rigging", "ripraps", "rompers", "rowlock", "rudders", "ruffles", "rummage", "sagging", "sailors", "salties", "salvors", "sampans", "sampson", "sculled", "scupper", "scuttle", "seacock", "sealing", "seekers", "serving", "sextant", "shelter", "shipped", "shiprig", "sickbay", "skipper", "skysail", "slinged", "slipway", "snagged", "snotter", "spliced", "splices", "sponson", "sponsor", "springs", "squares", "stackie", "standon", "starter", "station", "steamer", "steered", "steeves", "steward", "stopper", "stovein", "stowage", "strikes", "sunfish", "swimmie", "systems", "tacking", "thwarts", "tinclad", "tompion", "tonnage", "topmast", "topsail", "torpedo", "tossers", "trading", "traffic", "tramper", "transom", "trawler", "trenail", "trennel", "trimmer", "trooper", "trunnel", "tugboat", "turntwo", "unships", "upbound", "vessels", "voicing", "voyager", "weather", "whalers", "wharves", "whelkie", "whistle", "winches", "windage", "working", "yardarm")]
        MaritimeFlagsCallsign,

        [SouvenirQuestion("Which creature was displayed {1}in {0}?", "Monsplode, Fight!", 4, "Caadarim", "Buhar", "Melbor", "Lanaluff", "Bob", "Mountoise", "Aluga", "Nibs", "Zapra", "Zenlad", "Vellarim", "Ukkens", "Lugirit", "Flaurim", "Myrchat", "Clondar", "Gloorim", "Docsplode", "Magmy", "Pouse", "Asteran", "Violan", "Percy", "Cutie Pie",
            ExampleExtraFormatArguments = new[] { "", "first ", "second ", "third " }, ExampleExtraFormatArgumentGroupSize = 1)]
        MonsplodeFightCreature,

        [SouvenirQuestion("Which one of these moves {1} selectable {2}in {0}?", "Monsplode, Fight!", 4, "Tic", "Tac", "Toe", "Hollow Gaze", "Splash", "Heavy Rain", "Fountain", "Candle", "Torchlight", "Flame Spear", "Tangle", "Grass Blade", "Ivy Spikes", "Spectre", "Boo", "Battery Power", "Zap", "Double Zap", "Shock", "High Voltage", "Dark Portal", "Last Word", "Void", "Boom", "Fiery Soul", "Stretch", "Shrink", "Appearify", "Sendify", "Freak Out", "Glyph", "Bug Spray", "Bedrock", "Earthquake", "Cave In", "Toxic Waste", "Venom Fang", "Countdown", "Finale", "Sidestep",
            ExampleExtraFormatArguments = new[] { "was", "", "was", "for the first creature ", "was", "for the second creature ", "was not", "", "was not", "for the first creature ", "was not", "for the second creature " }, ExampleExtraFormatArgumentGroupSize = 2)]
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

        [SouvenirQuestion("What was the word shown as Morse code in {0}?", "Morse-A-Maze", 6, ExampleAnswers = new[] { "couch", "strobe", "smoke", "assay", "monkey", "glass", "starts", "strode", "office", "essays", "couple", "bosses" })]
        MorseAMazeMorseCodeWord,

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
            ExampleExtraFormatArguments = new[] { "a suspect, but not the murderer,", "not a suspect" }, ExampleExtraFormatArgumentGroupSize = 1)]
        MurderSuspect,

        [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", 4, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner",
            ExampleExtraFormatArguments = new[] { "a potential weapon, but not the murder weapon,", "not a potential weapon" }, ExampleExtraFormatArgumentGroupSize = 1)]
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

        [SouvenirQuestion("At which position in the selection bar on the left, counting from the {1}, was the highlight in {0}?", "Pattern Cube", 6, "first", "second", "third", "4th", "5th",
            ExampleExtraFormatArguments = new[] { "top", "bottom" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PatternCubeHighlightPosition,

        [SouvenirQuestion("Which peg was {1} in the solution to {0}?", "Perspective Pegs", 4, "top", "top right", "bottom right", "bottom left", "top left",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PerspectivePegsSolution,

        [SouvenirQuestion("What was the starting position in {0}?", "Polyhedral Maze", 6, null, ExampleAnswers = new[] { "1", "16", "24", "47", "89" })]
        PolyhedralMazeStartPosition,

        [SouvenirQuestion("What was the {1} digit in the {2} slot in {0}?", "Quintuples", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        QuintuplesNumbers,

        [SouvenirQuestion("What color was the {1} digit in the {2} slot in {0}?", "Quintuples", 4, "red", "blue", "orange", "green", "pink",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "second", "first", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        QuintuplesColors,

        [SouvenirQuestion("How many numbers were {1} in {0}?", "Quintuples", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25",
            ExampleExtraFormatArguments = new[] { "red", "blue", "orange", "green", "pink" }, ExampleExtraFormatArgumentGroupSize = 1)]
        QuintuplesColorCounts,

        [SouvenirQuestion("What was the last color in {0}?", "Rhythms", 4, "Blue", "Red", "Green", "Yellow")]
        RhythmsColor,

        [SouvenirQuestion("What were the first and second words in the {1} phrase in {0}?", "Sea Shells", 4, "she sells", "she shells", "sea shells", "sea sells",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells1,

        [SouvenirQuestion("What were the third and fourth words in the {1} phrase in {0}?", "Sea Shells", 4, "sea shells", "she shells", "sea sells", "she sells",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells2,

        [SouvenirQuestion("What was the end of the {1} phrase in {0}?", "Sea Shells", 4, "sea shore", "she sore", "she sure", "seesaw",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells3,

        [SouvenirQuestion("What was the initial shape in {0}?", "Shape Shift", 4, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", Font = AnswerFont.SymbolsFont)]
        ShapeShiftInitialShape,

        [SouvenirQuestion("What was the {1} slot in the {2} stage in {0}?", "Silly Slots", 4, "red bomb", "red cherry", "red coin", "red grape", "green bomb", "green cherry", "green coin", "green grape", "blue bomb", "blue cherry", "blue coin", "blue grape",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third", "third", "first", "third", "second", "third", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SillySlots,

        [SouvenirQuestion("What were the call samples {1} of {0}?", "Simon Samples", 6, "KKSS", "KKSH", "KSSH", "KHSS", "KHSH", "KHSO", "KHOH", "KOSH", "KOSO", "SKSK", "SHHS",
            ExampleExtraFormatArguments = new[] { "played in the first stage", "added in the second stage", "added in the third stage" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSamplesSamples,

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
            }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonScreamsRule,

        [SouvenirQuestion("What was the {1} received letter in {0}?", "Simon Sends", 6, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            ExampleExtraFormatArguments = new[] { "red", "green", "blue" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSendsReceivedLetters,

        [SouvenirQuestion("Which key’s color flashed {1} in the {2} stage of {0}?", "Simon Sings", 6, "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third", "third", "first", "third", "second", "third", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonSingsFlashing,

        [SouvenirQuestion("Which color was the bubble that flashed {1} in {0}?", "Simon Speaks", 6, "black", "blue", "green", "cyan", "red", "purple", "yellow", "white", "gray",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSpeaksColors,
        [SouvenirQuestion("Which word was in the bubble that flashed {1} in {0}?", "Simon Speaks", 6, "black", "sort", "zwart", "nigra", "musta", "noir", "schwarz", "fekete", "nero", "blue", "blå", "blauw", "blua", "sininen", "bleu", "blau", "kék", "blu", "green", "grøn", "groen", "verda", "vihreä", "vert", "grün", "zöld", "verde", "cyan", "turkis", "turkoois", "turkisa", "turkoosi", "turquoise", "türkis", "türkiz", "turchese", "red", "rød", "rood", "ruĝa", "punainen", "rouge", "rot", "piros", "rosso", "purple", "lilla", "purper", "purpura", "purppura", "pourpre", "lila", "bíbor", "porpora", "yellow", "gul", "geel", "flava", "keltainen", "jaune", "gelb", "sárga", "giallo", "white", "hvid", "wit", "blanka", "valkoinen", "blanc", "weiß", "fehér", "bianco", "gray", "grå", "grijs", "griza", "harmaa", "gris", "grau", "szürke", "grigio",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSpeaksWords,
        [SouvenirQuestion("Which bubble flashed {1} in {0}?", "Simon Speaks", 6, "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonSpeaksPositions,

        [SouvenirQuestion("Which {1} in the {2} stage in {0}?", "Simon States", 4, "Red", "Yellow", "Green", "Blue", "Red, Yellow", "Red, Green", "Red, Blue", "Yellow, Green", "Yellow, Blue", "Green, Blue", "all 4", "none",
            ExampleExtraFormatArguments = new[] { "color(s) flashed", "first", "color(s) didn’t flash", "first", "color(s) flashed", "second", "color(s) didn’t flash", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStatesDisplay,

        [SouvenirQuestion("What were the {1}original numbers in {0}?", "Skewed Slots", 6, null, ExampleAnswers = new[] { "123", "847", "000", "245", "961", "253", "858" },
            ExampleExtraFormatArguments = new[] { "", "first ", "second ", "third " }, ExampleExtraFormatArgumentGroupSize = 1)]
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

        [SouvenirQuestion("What was the {1} picture on {0}?", "Sonic The Hedgehog", 4, "Annoyed Sonic", "Ballhog", "Blue Lamppost", "Burrobot", "Buzz Bomber", "Crab Meat", "Dead Sonic", "Drowned Sonic", "Falling Sonic", "Moto Bug", "Red Lamppost", "Red Spring", "Standing Sonic", "Switch", "Yellow Spring",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SonicTheHedgehogPictures,

        [SouvenirQuestion("Which sound was played by the {1} screen on {0}?", "Sonic The Hedgehog", 4, "Boss Theme", "Breathe", "Continue", "Drown", "Emerald", "Extra Life", "Final Zone", "Invincibility", "Jump", "Lamppost", "Marble Zone", "Bumper", "Skid", "Spikes", "Spin", "Spring",
            ExampleExtraFormatArguments = new[] { "Running Boots", "Invincibility", "Extra Life", "Rings" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SonicTheHedgehogSounds,

        [SouvenirQuestion("What was the initial position of the switches in {0}?", "Switches", 6, "RRRRR", "QRRRR", "RQRRR", "QQRRR", "RRQRR", "QRQRR", "RQQRR", "QQQRR", "RRRQR", "QRRQR", "RQRQR", "QQRQR", "RRQQR", "QRQQR", "RQQQR", "QQQQR", "RRRRQ", "QRRRQ", "RQRRQ", "QQRRQ", "RRQRQ", "QRQRQ", "RQQRQ", "QQQRQ", "RRRQQ", "QRRQQ", "RQRQQ", "QQRQQ", "RRQQQ", "QRQQQ", "RQQQQ", "QQQQQ",
            Font = AnswerFont.SymbolsFont)]
        SwitchesInitialPosition,

        [SouvenirQuestion("Which number was displayed on {0}?", "Synonyms", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
        SynonymsNumber,

        [SouvenirQuestion("What was the received word in {0}?", "Tap Code", 4, ExampleAnswers = new[] { "child", "style", "shake", "alive", "axion", "wreck", "cause", "pupil", "cheat", "watch" })]
        TapCodeReceivedWord,

        [SouvenirQuestion("What was the initial color of the {1} button in the {2} stage of {0}?", "Ten-Button Color Code", 4, "red", "green", "blue",
            ExampleExtraFormatArguments = new[] { "first", "first", "second", "first", "first", "second", "second", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        TenButtonColorCodeInitialColors,

        [SouvenirQuestion("What was on the {1} button at the start of {0}?", "Tic-Tac-Toe", 6, "1", "2", "3", "4", "5", "6", "7", "8", "9", "O", "X",
            ExampleExtraFormatArguments = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TicTacToeInitialState,

        [SouvenirQuestion("What was the {1} city in {0}?", "Timezone", 4, "Alofi", "Papeete", "Unalaska", "Whitehorse", "Denver", "Managua", "Quito", "Manaus", "Buenos Aires", "Sao Paulo", "Praia", "Edinburgh", "Berlin", "Bujumbura", "Moscow", "Tbilisi", "Lahore", "Omsk", "Bangkok", "Beijing", "Tokyo", "Brisbane", "Sydney", "Tarawa",
            ExampleExtraFormatArguments = new[] { "departure", "destination" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TimezoneCities,

        [SouvenirQuestion("What was the {1} correct query response from {0}?", "Two Bits", 6, "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99",
            ExampleExtraFormatArguments = new[] { "first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TwoBitsResponse,

        [SouvenirQuestion("What was the {1} color in reading order used in the first stage of {0}?", "Uncolored Squares", 6, "White", "Red", "Blue", "Green", "Yellow", "Magenta",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        UncoloredSquaresFirstStage,

        [SouvenirQuestion("What was the color of the {1} dial when you solved {0}?", "Wire", 6, "blue", "green", "grey", "orange", "purple", "red",
            ExampleExtraFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        WireDialColors,

        [SouvenirQuestion("What was the displayed number when you solved {0}?", "Wire", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ExampleExtraFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        WireDisplayedNumber,

        [SouvenirQuestion("What was the desired color in the {1} successful round on {0}?", "Visual Impairment", 4, "Blue", "Green", "Red", "White",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        VisualImpairmentColors,

        [SouvenirQuestion("What was the initial roll on {0}?", "Yahtzee", 4, "Yahtzee", "large straight", "small straight", "four of a kind", "full house", "three of a kind", "two pairs", "pair")]
        YahtzeeInitialRoll,
    }

    enum AnswerFont
    {
        Default,
        SymbolsFont
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
        public AnswerFont Font { get; set; }

        public SouvenirQuestionAttribute(string questionText, string moduleName, int numAnswers, params string[] allAnswers)
        {
            QuestionText = questionText;
            ModuleName = moduleName;
            NumAnswers = numAnswers;
            AllAnswers = allAnswers == null || allAnswers.Length == 0 ? null : allAnswers;
            Font = AnswerFont.Default;
        }
    }

    sealed class QandA
    {
        public string QuestionText { get; private set; }
        public string[] Answers { get; private set; }
        public int CorrectIndex { get; private set; }
        public Font Font { get; private set; }
        public Texture FontTexture { get; private set; }
        public QandA(string question, string[] answers, int correct, Font font, Texture fontTexture)
        {
            QuestionText = question;
            Answers = answers;
            CorrectIndex = correct;
            Font = font;
            FontTexture = fontTexture;
        }
        public string DebugString { get { return string.Format("{0} — {1}", QuestionText, Answers.Select((a, ix) => string.Format(ix == CorrectIndex ? "[_{0}_]" : "{0}", a)).JoinString(" | ")); } }
    }
}
