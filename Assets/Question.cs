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

        [SouvenirQuestion("Which item was the {1} correct item you used in {0}?", "Adventure Game", 4, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        AdventureGameCorrectItem,

        [SouvenirQuestion("What was the first equation in {0}?", "Algebra", 4, "a=3z", "a=5+y", "a=6-x", "a=7x", "a=8y", "a=9+z", "a=x/2", "a=x+1", "a=y/4", "a=y-2", "a=z/10", "a=z-7")]
        AlgebraEquation1,

        [SouvenirQuestion("What was the second equation in {0}?", "Algebra", 4, "b=(2x/10)-y", "b=(7x)y", "b=(x+y)-(z/2)", "b=(y/2)-z", "b=(zy)-(2x)", "b=(z-y)/2", "b=2(z+7)", "b=2z+7", "b=xy-(2+x)", "b=xyz")]
        AlgebraEquation2,

        [SouvenirQuestion("What color was {1} in the solution to {0}?", "Big Circle", 6, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BigCircleColors,

        [SouvenirQuestion("At which numeric value did you cut the correct wire in {0}?", "Binary LEDs", 6, null, ExampleAnswers = new[] { "1", "12", "25", "31" })]
        BinaryLEDsValue,

        [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", "Bitmaps", 6, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16",
            ExampleExtraFormatArguments = new[] { "white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right" }, ExampleExtraFormatArgumentGroupSize = 2)]
        Bitmaps,

        [SouvenirQuestion("What was the final solution word in {0}?", "Braille", 6, "acting", "dating", "heading", "meaning", "server", "aiming", "dealer", "hearing", "miners", "shaking", "artist", "eating", "heating", "nearer", "sought", "asking", "eighth", "higher", "parish", "staying", "bearing", "farmer", "insist", "parker", "strands", "beating", "farming", "lasted", "parking", "strings", "beings", "faster", "laying", "paying", "teaching", "binding", "father", "leader", "powers", "tended", "bought", "finding", "leading", "pushed", "tender", "boxing", "finest", "leaned", "pushing", "testing", "breach", "finish", "leaning", "rather", "throwing", "breast", "flying", "leaving", "reaching", "towers", "breath", "foster", "linking", "reader", "vested", "breathe", "fought", "listed", "reading", "warned", "bringing", "gaining", "listen", "resting", "warning", "brings", "gather", "living", "riding", "weaker", "carers", "gazing", "making", "rushed", "wealth", "carter", "gender", "marked", "rushing", "winner", "charter", "growing", "marking", "saying", "winning", "crying", "headed", "master", "served", "winter")]
        BrailleWord,

        [SouvenirQuestion("What was the {1} correct button you pressed in {0}?", "Broken Buttons", 6, "bomb", "blast", "boom", "burst", "wire", "button", "module", "light", "led", "switch", "RJ-45", "DVI-D", "RCA", "PS/2", "serial", "port", "row", "column", "one", "two", "three", "four", "five", "six", "seven", "eight", "size", "this", "that", "other", "submit", "abort", "drop", "thing", "blank", "broken", "too", "to", "yes", "see", "sea", "c", "wait", "word", "bob", "no", "not", "first", "hold", "late", "fail",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleExtraFormatArgumentGroupSize = 1)]
        BrokenButtons,

        [SouvenirQuestion("What was the {1}paid amount in {0}?", "Cheap Checkout", 6, null, ExampleAnswers = new[] { "$11.00", "$12.00", "$14.00", "$11.00", "$25.00", "$24.00" },
            ExampleExtraFormatArguments = new[] { "", "first ", "second " }, ExampleExtraFormatArgumentGroupSize = 1)]
        CheapCheckoutPaid,

        [SouvenirQuestion("¿Qué fue el monto pagado {1}del cliente en {2}?", "Supermercado Salvaje", 6, null, ExampleAnswers = new[] { "$11.00", "$12.00", "$14.00", "$11.00", "$25.00", "$24.00" },
            ExampleExtraFormatArguments = new[] { "", "inicial ", "segundo " }, ExampleExtraFormatArgumentGroupSize = 1)]
        CheapCheckoutPaidSupermercadoSalvaje,

        [SouvenirQuestion("What was the {1} coordinate in {0}?", "Chess", 6, "a1", "a2", "a3", "a4", "a5", "a6", "b1", "b2", "b3", "b4", "b5", "b6", "c1", "c2", "c3", "c4", "c5", "c6", "d1", "d2", "d3", "d4", "d5", "d6", "e1", "e2", "e3", "e4", "e5", "e6", "f1", "f2", "f3", "f4", "f5", "f6",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        ChessCoordinate,

        [SouvenirQuestion("What was the first color group in {0}?", "Colored Squares", 6, "White", "Red", "Blue", "Green", "Yellow", "Magenta")]
        ColoredSquares,

        [SouvenirQuestion("What was the solution you selected first in {0}?", "Coordinates", 6, null, ExampleAnswers = new[] { "[4,7]", "C4", "<0, 2>", "3, 1", "(6,2)", "B-1", "“1, 0”", "4/3", "[12]", "#23", "四十七" })]
        CoordinatesFirstSolution,

        [SouvenirQuestion("What was the grid size in {0}?", "Coordinates", 6, "9", "15", "25", "21", "35", "49", "(9)", "(15)", "(21)", "(25)", "(35)", "(49)", "3 by 3", "4 by 3", "5 by 3", "6 by 3", "7 by 3", "3 by 4", "4 by 4", "5 by 4", "6 by 4", "7 by 4", "3 by 5", "4 by 5", "5 by 5", "6 by 5", "7 by 5", "3 by 6", "4 by 6", "5 by 6", "6 by 6", "7 by 6", "3 by 7", "4 by 7", "5 by 7", "6 by 7", "7 by 7", "9*3", "12*4", "15*5", "18*6", "21*7", "12*3", "16*4", "20*5", "24*6", "28*7", "15*3", "20*4", "25*5", "30*6", "35*7", "18*3", "24*4", "30*5", "36*6", "42*7", "21*3", "28*4", "35*5", "42*6", "49*7", "9 : 3", "12 : 3", "15 : 3", "18 : 3", "21 : 3", "12 : 4", "16 : 4", "20 : 4", "24 : 4", "28 : 4", "15 : 5", "20 : 5", "25 : 5", "30 : 5", "35 : 5", "18 : 6", "24 : 6", "30 : 6", "36 : 6", "42 : 6", "21 : 7", "28 : 7", "35 : 7", "42 : 7", "49 : 7", "3×3", "3×4", "3×5", "3×6", "3×7", "4×3", "4×4", "4×5", "4×6", "4×7", "5×3", "5×4", "5×5", "5×6", "5×7", "6×3", "6×4", "6×5", "6×6", "6×7", "7×3", "7×4", "7×5", "7×6", "7×7")]
        CoordinatesSize,

        [SouvenirQuestion("What were the weather conditions on the {1} day in {0}?", "Creation", 4, "Clear", "Heat Wave", "Meteor Shower", "Rain", "Windy",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "4th", "5th" }, ExampleExtraFormatArgumentGroupSize = 1, AddThe = true)]
        Creation,//

        [SouvenirQuestion("Which button was the submit button in {0}?", "Double-Oh", 6, "↕", "⇕", "↔", "⇔", "◆")]
        DoubleOhSubmitButton,

        [SouvenirQuestion("What was the last pair of letters in {0}?", "Fast Math", 6, "AA", "AB", "AC", "AD", "AE", "AG", "AK", "AN", "AP", "AS", "AT", "AX", "AZ", "BA", "BB", "BC", "BD", "BE", "BG", "BK", "BN", "BP", "BS", "BT", "BX", "BZ", "CA", "CB", "CC", "CD", "CE", "CG", "CK", "CN", "CP", "CS", "CT", "CX", "CZ", "DA", "DB", "DC", "DD", "DE", "DG", "DK", "DN", "DP", "DS", "DT", "DX", "DZ", "EA", "EB", "EC", "ED", "EE", "EG", "EK", "EN", "EP", "ES", "ET", "EX", "EZ", "GA", "GB", "GC", "GD", "GE", "GG", "GK", "GN", "GP", "GS", "GT", "GX", "GZ", "KA", "KB", "KC", "KD", "KE", "KG", "KK", "KN", "KP", "KS", "KT", "KX", "KZ", "NA", "NB", "NC", "ND", "NE", "NG", "NK", "NN", "NP", "NS", "NT", "NX", "NZ", "PA", "PB", "PC", "PD", "PE", "PG", "PK", "PN", "PP", "PS", "PT", "PX", "PZ", "SA", "SB", "SC", "SD", "SE", "SG", "SK", "SN", "SP", "SS", "ST", "SX", "SZ", "TA", "TB", "TC", "TD", "TE", "TG", "TK", "TN", "TP", "TS", "TT", "TX", "TZ", "XA", "XB", "XC", "XD", "XE", "XG", "XK", "XN", "XP", "XS", "XT", "XX", "XZ", "ZA", "ZB", "ZC", "ZD", "ZE", "ZG", "ZK", "ZN", "ZP", "ZS", "ZT", "ZX", "ZZ")]
        FastMathLastLetters,

        [SouvenirQuestion("What was the starting location in {0}?", "Gridlock", 6, "A1", "B1", "C1", "D1", "A2", "B2", "C2", "D2", "A3", "B3", "C3", "D3", "A4", "B4", "C4", "D4")]
        GridLockStartingLocation,

        [SouvenirQuestion("What was the ending location in {0}?", "Gridlock", 6, "A1", "B1", "C1", "D1", "A2", "B2", "C2", "D2", "A3", "B3", "C3", "D3", "A4", "B4", "C4", "D4")]
        GridLockEndingLocation,

        [SouvenirQuestion("What was the starting color in {0}?", "Gridlock", 4, "Green", "Yellow", "Red", "Blue")]
        GridLockStartingColor,

        [SouvenirQuestion("What was the color of the pawn in {0}?", "Hexamaze", 6, "Red", "Yellow", "Green", "Cyan", "Blue", "Pink")]
        HexamazePawnColor,

        [SouvenirQuestion("Which of the first three stages of {0} had the {1} symbol {2}?", "Hunting", 4, "none", "first", "second", "first two", "third", "first & third", "second & third", "all three")]
        HuntingColumnsRows,

        [SouvenirQuestion("Which one of these flavours {1} to the {2} customer in {0}?", "Ice Cream", 4, "Tutti Frutti", "Rocky Road", "Raspb. Ripple", "Double Choc.", "Double Str.", "Cookies & Cr.", "Neapolitan", "Mint Ch. Chip", "The Classic", "Vanilla",
            ExampleExtraFormatArguments = new[] { "was on offer, but not sold,", "first", "was not on offer", "first", "was on offer, but not sold,", "second", "was not on offer", "second", "was on offer, but not sold,", "third", "was not on offer", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        IceCreamFlavour,

        [SouvenirQuestion("Who was the {1} customer in {0}?", "Ice Cream", 6, "Mike", "Tim", "Tom", "Dave", "Adam", "Cheryl", "Sean", "Ashley", "Jessica", "Taylor", "Simon", "Sally", "Jade", "Sam", "Gary", "Victor", "George", "Jacob", "Pat", "Bob",
            ExampleExtraFormatArguments = new[] { "first", "second" }, ExampleExtraFormatArgumentGroupSize = 1)]
        IceCreamCustomer,

        [SouvenirQuestion("What was the correct code you entered in {0}?", "Listening", 6, null,
            ExampleAnswers = new[] { "&&&**", "&$#$&", "$#$*&", "#$$**", "$#$#*", "**$*#", "#$$&*", "##*$*", "$#*$&", "**#**", "#&&*#", "&#**&", "$&**#", "&#$$#", "$&&**", "#&$##", "&*$*$", "&$$&*", "#&&&&", "**$$$", "*&*&&", "*#&*&", "**###", "&&$&*", "&$**&", "#$#&$", "&#&&#", "$$*$*", "$&#$$", "&**$$", "$&&*&", "&$&##", "#&$*&", "$*$**", "*#$&&", "###&$", "*$$&$", "$*&##", "#&$&&", "$&$$*", "*$*$*" })]
        Listening,

        [SouvenirQuestion("Who was a player, but not the Godfather, in {0}?", "Mafia", 6, "Rob", "Tim", "Mary", "Briane", "Hunter", "Macy", "John", "Will", "Lacy", "Claire", "Kenny", "Rick", "Walter", "Bonnie", "Luke", "Ed", "Sarah", "Larry", "Kate", "Stacy", "Diane", "Mac", "Jim", "Ron", "Tommy", "Lenny", "Molly", "Benny", "Phil", "Bob", "Gary", "Ted", "Kim", "Nate", "Cher", "Wally", "Thomas", "Sam", "Duke", "Jack", "Bill", "Ronny", "Terry", "Claira", "Nick", "Cob", "Ash", "Don", "Jerry", "Simon")]
        MafiaPlayers,

        [SouvenirQuestion("Which creature was displayed {1}in {0}?", "Monsplode, Fight!", 4, "Caadarim", "Buhar", "Melbor", "Lanaluff", "Bob", "Mountoise", "Aluga", "Nibs", "Zapra", "Zenlad", "Vellarim", "Ukkens", "Lugirit", "Flaurim", "Myrchat", "Clondar", "Gloorim", "Docsplode", "Magmy", "Pouse", "Asteran", "Violan", "Percy", "Cutie Pie",
            ExampleExtraFormatArguments = new[] { "", "first ", "second ", "third " }, ExampleExtraFormatArgumentGroupSize = 1)]
        MonsplodeFightCreature,

        [SouvenirQuestion("Which one of these moves {1} selectable {2}in {0}?", "Monsplode, Fight!", 4, "Tic", "Tac", "Toe", "Hollow Gaze", "Splash", "Heavy Rain", "Fountain", "Candle", "Torchlight", "Flame Spear", "Tangle", "Grass Blade", "Ivy Spikes", "Spectre", "Boo", "Battery Power", "Zap", "Double Zap", "Shock", "High Voltage", "Dark Portal", "Last Word", "Void", "Boom", "Fiery Soul", "Stretch", "Shrink", "Appearify", "Sendify", "Freak Out", "Glyph", "Bug Spray", "Bedrock", "Earthquake", "Cave In", "Toxic Waste", "Venom Fang", "Countdown", "Finale", "Sidestep",
            ExampleExtraFormatArguments = new[] { "was", "", "was", "for the first creature ", "was", "for the second creature ", "was not", "", "was not", "for the first creature ", "was not", "for the second creature " }, ExampleExtraFormatArgumentGroupSize = 2)]
        MonsplodeFightMove,

        [SouvenirQuestion("What was the starting location in {0}?", "Morse-A-Maze", 6, "A1", "A2", "A3", "A4", "A5", "A6", "B1", "B2", "B3", "B4", "B5", "B6", "C1", "C2", "C3", "C4", "C5", "C6", "D1", "D2", "D3", "D4", "D5", "D6", "E1", "E2", "E3", "E4", "E5", "E6", "F1", "F2", "F3", "F4", "F5", "F6")]
        MorseAMazeStartingCoordinate,

        [SouvenirQuestion("What was the ending location in {0}?", "Morse-A-Maze", 6, "A1", "A2", "A3", "A4", "A5", "A6", "B1", "B2", "B3", "B4", "B5", "B6", "C1", "C2", "C3", "C4", "C5", "C6", "D1", "D2", "D3", "D4", "D5", "D6", "E1", "E2", "E3", "E4", "E5", "E6", "F1", "F2", "F3", "F4", "F5", "F6")]
        MorseAMazeEndingCoordinate,

        [SouvenirQuestion("What was the word shown as Morse code in {0}?", "Morse-A-Maze", 6, "Thank you BOB", "shell", "leaks", "strike", "alien3", "bistro", "tango", "timer", "boxes", "trick", "penguin", "sting", "elias", "ktane", "manual", "zulu", "november", "kaboom", "unicorn", "quebec", "bashly", "slick", "vector", "flick", "timwi", "strobe", "bombs", "bravo", "laundry", "brick", "kitty", "halls", "steak", "break", "beats")]
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

        [SouvenirQuestion("Which peg was {1} in the solution to {0}?", "Perspective Pegs", 4, "top", "top right", "bottom right", "bottom left", "top left",
            ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        PerspectivePegsSolution,

        [SouvenirQuestion("What were the first and second words in the {1} phrase in {0}?", "Sea Shells", 4, "she sells", "she shells", "sea shells", "sea sells", ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells1,

        [SouvenirQuestion("What were the third and fourth words in the {1} phrase in {0}?", "Sea Shells", 4, "sea shells", "she shells", "sea sells", "she sells", ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells2,

        [SouvenirQuestion("What was the end of the {1} phrase in {0}?", "Sea Shells", 4, "sea shore", "she sore", "she sure", "seesaw", ExampleExtraFormatArguments = new[] { "first", "second", "third" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SeaShells3,

        [SouvenirQuestion("What was the {1} slot in the {2} stage in {0}?", "Silly Slots", 4, "red bomb", "red cherry", "red coin", "red grape", "green bomb", "green cherry", "green coin", "green grape", "blue bomb", "blue cherry", "blue coin", "blue grape",
            ExampleExtraFormatArguments = new[] { "first", "first", "first", "second", "first", "third", "second", "first", "second", "second", "second", "third", "third", "first", "third", "second", "third", "third" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SillySlots,

        [SouvenirQuestion("Which color flashed {1} in the final sequence in {0}?", "Simon Screams", 6, "Red", "Orange", "Yellow", "Green", "Blue", "Purple",
            ExampleExtraFormatArguments = new[] { "first", "second", "third", "fourth", "fifth", "sixth" }, ExampleExtraFormatArgumentGroupSize = 1)]
        SimonScreamsFlashing,

        [SouvenirQuestion("Which {1} of {0} had {2}?", "Simon Screams", 4, null, ExampleAnswers = new[] { "first", "second", "third", "first and second", "first and third", "second and third", "all of them" },
            ExampleExtraFormatArguments = new[] {
                "stage", "three adjacent colors flashing in clockwise order",
                "stage", "a color flashing, then an adjacent color, then the first again",
                "stage", "at most one color flashing out of red, yellow, and blue",
                "stage", "two colors opposite each other that didn’t flash",
                "stage", "two (but not three) adjacent colors flashing in clockwise order",
                "stages", "three adjacent colors flashing in clockwise order",
                "stages", "a color flashing, then an adjacent color, then the first again",
                "stages", "at most one color flashing out of red, yellow, and blue",
                "stages", "two colors opposite each other that didn’t flash",
                "stages", "two (but not three) adjacent colors flashing in clockwise order"
            }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonScreamsRule,

        [SouvenirQuestion("Which {1} in the {2} stage in {0}?", "Simon States", 4, "Red", "Yellow", "Green", "Blue", "Red, Yellow", "Red, Green", "Red, Blue", "Yellow, Green", "Yellow, Blue", "Green, Blue", "all 4", "none",
            ExampleExtraFormatArguments = new[] { "color(s) flashed", "first", "color(s) didn’t flash", "first", "color(s) flashed", "second", "color(s) didn’t flash", "second" }, ExampleExtraFormatArgumentGroupSize = 2)]
        SimonStatesDisplay,

        [SouvenirQuestion("What were the {1}original numbers in {0}?", "Skewed Slots", 6, null, ExampleAnswers = new[] { "123", "847", "000", "245", "961", "253", "858" },
            ExampleExtraFormatArguments = new[] { "", "first ", "second ", "third " }, ExampleExtraFormatArgumentGroupSize = 1)]
        SkewedSlotsOriginalNumbers,

        [SouvenirQuestion("What were the correct button presses in {0}?", "Bulb", 6, "OOO", "OOI", "OIO", "OII", "IOO", "IOI", "IIO", "III", AddThe = true)]
        TheBulbButtonPresses,

        [SouvenirQuestion("What were the numbers on {0}?", "Gamepad", 6, null, AddThe = true, ExampleAnswers = new[] { "01:03", "12:92", "84:72", "24:56" })]
        TheGamepadNumbers,

        [SouvenirQuestion("What was the {1} query response from {0}?", "Two Bits", 6, "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99",
            ExampleExtraFormatArguments = new[] { "first" }, ExampleExtraFormatArgumentGroupSize = 1)]
        TwoBitsResponse,//
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
        public QandA(string question, string[] answers, int correct)
        {
            QuestionText = question;
            Answers = answers;
            CorrectIndex = correct;
        }
        public string DebugString { get { return string.Format("{0} — {1}", QuestionText, Answers.Select((a, ix) => string.Format(ix == CorrectIndex ? "[_{0}_]" : "{0}", a)).JoinString(" | ")); } }
    }
}
