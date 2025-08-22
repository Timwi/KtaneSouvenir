using static Souvenir.AnswerLayout;

namespace Souvenir;

public enum Question
{
    [SouvenirQuestion("What was the display in the {1} stage on {0}?", ".--/---/.--.", ThreeColumns6Answers, ExampleAnswers = new[] { "COULD", "SMALL", "BELOW", "LARGE", "STUDY", "FIRST" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    MorseWoFDisplays,

    [SouvenirQuestion("What was the initially displayed number in {0}?", "0", TwoColumns4Answers)]
    [AnswerGenerator.Integers(100000000, 999999999)]
    _0Number,

    [SouvenirQuestion("What was the {1} word shown in {0}?", "1000 Words", ThreeColumns6Answers,
        ExampleAnswers = new[] { "Baken", "Ghost", "Tolts", "Oyers", "Sweel", "Rangy", "Noses", "Chapt", "Phuts", "Pingo", "Hylas", "Podia", "Vizor" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    _1000WordsWords,

    [SouvenirQuestion("What was the {1} displayed letter in {0}?", "100 Levels of Defusal", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    _100LevelsOfDefusalLetters,

    [SouvenirQuestion("Who was the opponent in {0}?", "1, 2, 3 Game", ThreeColumns6Answers, AddThe = true, Type = AnswerType.Sprites)]
    _123GameProfile,
    [SouvenirQuestion("Who was the opponent in {0}?", "1, 2, 3 Game", ThreeColumns6Answers, "Changyeop", "Eunji", "Gura", "Jinho", "Jungmoon", "Junseok", "Kyungran", "Minseo", "Minsoo", "Poong", "Sangmin", "Sunggyu", "Yuram", AddThe = true)]
    _123GameName,

    [SouvenirQuestion("What was {1} in {0}?", "1D Chess", ThreeColumns6Answers, "B a→c", "B a→e", "B a→g", "B a→i", "B a→k", "B b→d", "B b→f", "B b→h", "B b→j", "B c→a", "B c→e", "B c→g", "B c→i", "B c→k", "B d→b", "B d→f", "B d→h", "B d→j", "B e→a", "B e→c", "B e→g", "B e→i", "B e→k", "B f→b", "B f→d", "B f→h", "B f→j", "B g→a", "B g→c", "B g→e", "B g→i", "B g→k", "B h→b", "B h→d", "B h→f", "B h→j", "B i→a", "B i→c", "B i→e", "B i→g", "B i→k", "B j→b", "B j→d", "B j→f", "B j→h", "B k→a", "B k→c", "B k→e", "B k→g", "B k→i", "K a→b", "K b→a", "K b→c", "K c→b", "K c→d", "K d→c", "K d→e", "K e→d", "K e→f", "K f→e", "K f→g", "K g→f", "K g→h", "K h→g", "K h→i", "K i→h", "K i→j", "K j→i", "K j→k", "K k→j", "N a→c", "N b→d", "N c→a", "N c→e", "N d→b", "N d→f", "N e→c", "N e→g", "N f→d", "N f→h", "N g→e", "N g→i", "N h→f", "N h→j", "N i→g", "N i→k", "N j→h", "N k→i", "P a→b", "P a→c", "P b→a", "P b→c", "P b→d", "P c→a", "P c→b", "P c→d", "P c→e", "P d→b", "P d→c", "P d→e", "P d→f", "P e→c", "P e→d", "P e→f", "P e→g", "P f→d", "P f→e", "P f→g", "P f→h", "P g→e", "P g→f", "P g→h", "P g→i", "P h→f", "P h→g", "P h→i", "P h→j", "P i→g", "P i→h", "P i→j", "P i→k", "P j→h", "P j→i", "P j→k", "P k→i", "P k→j", "Q a→b", "Q a→c", "Q a→d", "Q a→e", "Q a→f", "Q a→g", "Q a→h", "Q a→i", "Q a→j", "Q a→k", "Q b→a", "Q b→c", "Q b→d", "Q b→e", "Q b→f", "Q b→g", "Q b→h", "Q b→i", "Q b→j", "Q b→k", "Q c→a", "Q c→b", "Q c→d", "Q c→e", "Q c→f", "Q c→g", "Q c→h", "Q c→i", "Q c→j", "Q c→k", "Q d→a", "Q d→b", "Q d→c", "Q d→e", "Q d→f", "Q d→g", "Q d→h", "Q d→i", "Q d→j", "Q d→k", "Q e→a", "Q e→b", "Q e→c", "Q e→d", "Q e→f", "Q e→g", "Q e→h", "Q e→i", "Q e→j", "Q e→k", "Q f→a", "Q f→b", "Q f→c", "Q f→d", "Q f→e", "Q f→g", "Q f→h", "Q f→i", "Q f→j", "Q f→k", "Q g→a", "Q g→b", "Q g→c", "Q g→d", "Q g→e", "Q g→f", "Q g→h", "Q g→i", "Q g→j", "Q g→k", "Q h→a", "Q h→b", "Q h→c", "Q h→d", "Q h→e", "Q h→f", "Q h→g", "Q h→i", "Q h→j", "Q h→k", "Q i→a", "Q i→b", "Q i→c", "Q i→d", "Q i→e", "Q i→f", "Q i→g", "Q i→h", "Q i→j", "Q i→k", "Q j→a", "Q j→b", "Q j→c", "Q j→d", "Q j→e", "Q j→f", "Q j→g", "Q j→h", "Q j→i", "Q j→k", "Q k→a", "Q k→b", "Q k→c", "Q k→d", "Q k→e", "Q k→f", "Q k→g", "Q k→h", "Q k→i", "Q k→j", "R a→b", "R a→d", "R a→f", "R a→h", "R a→j", "R b→a", "R b→c", "R b→e", "R b→g", "R b→i", "R b→k", "R c→b", "R c→d", "R c→f", "R c→h", "R c→j", "R d→a", "R d→c", "R d→e", "R d→g", "R d→i", "R d→k", "R e→b", "R e→d", "R e→f", "R e→h", "R e→j", "R f→a", "R f→c", "R f→e", "R f→g", "R f→i", "R f→k", "R g→b", "R g→d", "R g→f", "R g→h", "R g→j", "R h→a", "R h→c", "R h→e", "R h→g", "R h→i", "R h→k", "R i→b", "R i→d", "R i→f", "R i→h", "R i→j", "R j→a", "R j→c", "R j→e", "R j→g", "R j→i", "R j→k", "R k→b", "R k→d", "R k→f", "R k→h", "R k→j",
        ExampleFormatArguments = new[] { "your first move", "Rustmate’s first move", "your second move", "Rustmate’s second move", "your third move", "Rustmate’s third move", "your fourth move", "Rustmate’s fourth move", "your fifth move", "Rustmate’s fifth move", "your sixth move", "Rustmate’s sixth move", "your seventh move", "Rustmate’s seventh move", "your eighth move", "Rustmate’s eighth move", }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    _1DChessMoves,

    [SouvenirQuestion("What was the displayed number in {0}?", "21", ThreeColumns6Answers, Type = AnswerType.SixtyFourFont, ExampleAnswers = new[] { "A0A3", "K1I1", "3000", "83F1", "ABCD", "1234" })]
    _21DisplayedNumber,

    [SouvenirQuestion("What were the markings in {0}?", "3D Maze", ThreeColumns6Answers, "ABC", "ABD", "ABH", "ACD", "ACH", "ADH", "BCD", "BCH", "BDH", "CDH")]
    _3DMazeMarkings,
    [SouvenirQuestion("What was the cardinal direction in {0}?", "3D Maze", TwoColumns4Answers, "North", "South", "West", "East", TranslateAnswers = true)]
    _3DMazeBearing,

    [SouvenirQuestion("What was the received word in {0}?", "3D Tap Code", ThreeColumns6Answers,
        ExampleAnswers = new[] { "Aback", "Backs", "Habit", "Oasis", "Unzip", "Vogue" })]
    _3DTapCodeWord,

    [SouvenirQuestion("What was the {1} goal node in {0}?", "3D Tunnels", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings("a-z.")]
    _3DTunnelsTargetNode,

    [SouvenirQuestion("What was the initial state of the LEDs in {0} (in reading order)?", "3 LEDs", TwoColumns4Answers, "off/off/off", "off/off/on", "off/on/off", "off/on/on", "on/off/off", "on/off/on", "on/on/off", "on/on/on", TranslateAnswers = true)]
    _3LEDsInitialState,

    [SouvenirQuestion("What number was initially displayed in {0}?", "3N+1", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 100)]
    _3NPlus1,

    [SouvenirQuestion("What was the displayed number in {0}?", "64", ThreeColumns6Answers, Type = AnswerType.SixtyFourFont, ExampleAnswers = new[] { "A0A3", "bbda", "30", "h3X1", "ABCD", "1234" })]
    _64DisplayedNumber, // Use the font from the module because o and 0 are almost identical in the default font.

    [SouvenirQuestion("What was the {1} channel’s initial value in {0}?", "7", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(-9, 9)]
    _7InitialValues,
    [SouvenirQuestion("What LED color was shown in stage {1} of {0}?", "7", TwoColumns4Answers, "red", "blue", "green", "white",
        ExampleFormatArguments = new[] { "0", "1", "2", "3" }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    _7LedColors,

    [SouvenirQuestion("What was the number of ball {1} in {0}?", "9-Ball", ThreeColumns6Answers, ExampleAnswers = new[] { "2", "3", "4", "5", "6", "7" },
        ExampleFormatArguments = new[] { "A", "B", "C", "D", "E", "F", "G" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(2, 8)]
    _9BallLetters,
    [SouvenirQuestion("What was the letter of ball {1} in {0}?", "9-Ball", ThreeColumns6Answers, ExampleAnswers = new[] { "A", "B", "C", "D", "E", "F" },
        ExampleFormatArguments = new[] { "2", "3", "4", "5", "6", "7", "8" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-G")]
    _9BallNumbers,

    [SouvenirQuestion("What was the {1} character displayed on {0}?", "Abyss", ThreeColumns6Answers, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(1, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")]
    AbyssSeed,

    [SouvenirQuestion("What was the background color on the {1} stage in {0}?", "Accumulation", ThreeColumns6Answers, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    AccumulationBackgroundColor,
    [SouvenirQuestion("What was the border color in {0}?", "Accumulation", ThreeColumns6Answers, "Blue", "Brown", "Green", "Grey", "Lime", "Orange", "Pink", "Red", "White", "Yellow", TranslateAnswers = true)]
    AccumulationBorderColor,

    [SouvenirQuestion("Which item was the {1} correct item you used in {0}?", "Adventure Game", TwoColumns4Answers, "Broadsword", "Caber", "Nasty knife", "Longbow", "Magic orb", "Grimoire", "Balloon", "Battery", "Bellows", "Cheat code", "Crystal ball", "Feather", "Hard drive", "Lamp", "Moonstone", "Potion", "Small dog", "Stepladder", "Sunstone", "Symbol", "Ticket", "Trophy",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    AdventureGameCorrectItem,

    [SouvenirQuestion("What enemy were you fighting in {0}?", "Adventure Game", TwoColumns4Answers, "Dragon", "Demon", "Eagle", "Goblin", "Troll", "Wizard", "Golem", "Lizard")]
    AdventureGameEnemy,

    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Affine Cycle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    AffineCycleDialDirections,
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", "Affine Cycle", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    AffineCycleDialLabels,

    [SouvenirQuestion("Who was the {1} mercenary displayed in {0}?", "Alcoholic Rampage", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "AlcoholicRampageSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    AlcoholicRampageMercenaries,

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

    [SouvenirQuestion("Which position was the {1} position in {0}?", "Algorithmia", ThreeColumns6Answers, Type = AnswerType.Sprites, ExampleFormatArguments = new[] { "starting", "goal" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Grid(4, 4)]
    AlgorithmiaPositions,
    [SouvenirQuestion("What was the color of the colored bulb in {0}?", "Algorithmia", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Yellow", "Magenta")]
    AlgorithmiaColor,
    [SouvenirQuestion("Which number was present in the seed in {0}?", "Algorithmia", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99)]
    AlgorithmiaSeed,

    [SouvenirQuestion("What was the letter displayed in the {1} stage of {0}?", "Alphabetical Ruling", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(1, 'A', 'Z')]
    AlphabeticalRulingLetter,
    [SouvenirQuestion("What was the number displayed in the {1} stage of {0}?", "Alphabetical Ruling", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 9)]
    AlphabeticalRulingNumber,

    [SouvenirQuestion("Which of these numbers was on one of the buttons in the {1} stage of {0}?", "Alphabet Numbers", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 32)]
    AlphabetNumbersDisplayedNumbers,

    [SouvenirQuestion("What was the {1} letter shown during the cycle in {0}?", "Alphabet Tiles", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    AlphabetTilesCycle,
    [SouvenirQuestion("What was the missing letter in {0}?", "Alphabet Tiles", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z")]
    AlphabetTilesMissingLetter,

    [SouvenirQuestion("What character was displayed on the {1} screen on the {2} in {0}?", "Alpha-Bits", ThreeColumns6Answers, TranslateFormatArgs = new[] { false, true },
        Type = AnswerType.DynamicFont, ExampleFormatArguments = new[] { QandA.Ordinal, "left", QandA.Ordinal, "right" }, ExampleFormatArgumentGroupSize = 2)]
    [AnswerGenerator.Strings("0-9A-V")]
    AlphaBitsDisplayedCharacters,

    [SouvenirQuestion("What was the initial message in {0}?", "A Message", TwoColumns4Answers, Type = AnswerType.AMessageFont, FontSize = 560, CharacterSize = 0.125f)]
    [AnswerGenerator.AMessage]
    AMessageAMessage,

    [SouvenirQuestion("Which ride was available in {0}?", "Amusement Parks", AnswerLayout.OneColumn4Answers, "Carousel", "Drop Tower", "Enterprise", "Ferris Wheel", "Ghost Train", "Inverted Coaster", "Junior Coaster", "Launched Coaster", "Log Flume", "Omnimover", "Pirate Ship", "River Rapids", "Safari", "Star Flyer", "Top Spin", "Tourbillon", "Vintage Cars", "Walkthrough", "Wooden Coaster")]
    AmusementParksRides,

    [SouvenirQuestion("What letter was shown by the raised buttons on the {1} stage on {0}?", "Ángel Hernández", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    AngelHernandezMainLetter,

    [SouvenirQuestion("What was the maximum weapon damage of the attack phase in {0}?", "Arena", ThreeColumns6Answers, AddThe = true)]
    [AnswerGenerator.Integers(1, 99)]
    ArenaDamage,
    [SouvenirQuestion("Which enemy was present in the defend phase of {0}?", "Arena", TwoColumns4Answers, "Bat", "Snake", "Spider", "Cobra", "Scorpion", "Mole", "Creeper", "Goblin", "Golem", "Robo-Mouse", "Skeleton", "Undead Guard", "The Reaper", "The Mole’s Dad", AddThe = true)]
    ArenaEnemies,
    [SouvenirQuestion("Which was a number present in the grab phase of {0}?", "Arena", ThreeColumns6Answers, AddThe = true)]
    [AnswerGenerator.Integers(10, 99)]
    ArenaNumbers,

    [SouvenirQuestion("What was the symbol on the submit button in {0}?", "Arithmelogic", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "ArithmelogicSprites")]
    ArithmelogicSubmit,
    [SouvenirQuestion("Which number was selectable, but not the solution, in the {1} screen on {0}?", "Arithmelogic", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "left", "middle", "right" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(10, 40)]
    ArithmelogicNumbers,

    [SouvenirQuestion("What was the {1} character displayed on {0}?", "ASCII Maze", ThreeColumns6Answers, "NUL", "SOH", "STX", "ETX", "EOT", "ENQ", "ACK", "BEL", "BS", "HT", "LF", "VT", "FF", "CR", "SO", "SI", "DLE", "DC1", "DC2", "DC3", "DC4", "NAK", "SYN", "ETB", "CAN", "EM", "SUB", "ESC", "FS", "GS", "RS", "US", "(space)", "!", "\"", "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", "\\", "]", "^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~", "DEL", "Ç", "ü", "é", "â", "ä", "à", "å", "ç", "ê", "ë", "è", "ï", "î", "ì", "Ä", "Å", "É", "æ", "Æ", "ô", "ö", "ò", "û", "ù", "ÿ", "Ö", "Ü", "ø", "£", "Ø", "×", "ƒ", "á", "í", "ó", "ú", "ñ", "Ñ", "ª", "º", "¿", "®", "¬", "½", "¼", "¡", "«", "»", "░", "▒", "▓", "│", "┤", "Á", "Â", "À", "©", "╣", "║", "╗", "╝", "¢", "¥", "┐", "└", "┴", "┬", "├", "─", "┼", "ã", "Ã", "╚", "╔", "╩", "╦", "╠", "═", "╬", "¤", "ð", "Ð", "Ê", "Ë", "È", "ı", "Í", "Î", "Ï", "┘", "┌", "█", "▄", "¦", "Ì", "▀", "Ó", "ß", "Ô", "Ò", "õ", "Õ", "µ", "þ", "Þ", "Ú", "Û", "Ù", "ý", "Ý", "¯", "´", "\u2261", "±", "‗", "¾", "¶", "§", "÷", "¸", "°", "¨", "·", "¹", "³", "²", "■", "nbsp",
        Type = AnswerType.AsciiMazeFont, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ASCIIMazeCharacters,

    [SouvenirQuestion("Which of these was an index color in {0}?", "A Square", ThreeColumns6Answers, "Orange", "Pink", "Cyan", "Yellow", "Lavender", "Brown", "Tan", "Blue", "Jade", "Indigo", "White")]
    ASquareIndexColors,
    [SouvenirQuestion("Which color was submitted {1} in {0}?", "A Square", ThreeColumns6Answers, "Orange", "Pink", "Cyan", "Yellow", "Lavender", "Brown", "Tan", "Blue", "Jade", "Indigo", "White",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ASquareCorrectColors,

    [SouvenirQuestion("What was signaled in {0}?", "Audio Morse", OneColumn4Answers, Type = AnswerType.Audio, ForeignAudioID = Sounds.Generated)]
    AudioMorseSound,

    [SouvenirQuestion("What was T in {0}?", "Azure Button", ThreeColumns6Answers, AddThe = true, Type = AnswerType.Sprites, SpriteFieldName = "AzureButtonSprites")]
    AzureButtonT,
    [SouvenirQuestion("Which of these cards was shown in Stage 1, but not T, in {0}?", "Azure Button", ThreeColumns6Answers, AddThe = true, Type = AnswerType.Sprites, SpriteFieldName = "AzureButtonSprites")]
    AzureButtonNotT,
    [SouvenirQuestion("What was M in {0}?", "Azure Button", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", AddThe = true)]
    AzureButtonM,
    [SouvenirQuestion("What was the {1} direction in the decoy arrow in {0}?", "Azure Button", TwoColumns4Answers, "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west",
        AddThe = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    AzureButtonDecoyArrowDirection,
    [SouvenirQuestion("What was the {1} direction in the {2} non-decoy arrow in {0}?", "Azure Button", TwoColumns4Answers, "north", "north-east", "east", "south-east", "south", "south-west", "west", "north-west",
        AddThe = true, ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    AzureButtonNonDecoyArrowDirection,

    [SouvenirQuestion("Which menu item was present in {0}?", "Bakery", OneColumn4Answers,
        ExampleAnswers = new[] { "Butter slab", "Sugar cookie", "Applie pie", "Tea biscuit", "Tuile", "Sprinkles Cookie" })]
    BakeryItems,

    [SouvenirQuestion("What color was the {1} correct button in {0}?", "Bamboozled Again", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BamboozledAgainButtonColor,
    [SouvenirQuestion("What was the text on the {1} correct button in {0}?", "Bamboozled Again", TwoColumns4Answers, "THE LETTER", "ONE LETTER", "THE COLOUR", "ONE COLOUR", "THE PHRASE", "ONE PHRASE", "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "QUEBEC", "TANGO", "WHISKEY", "VICTOR", "YANKEE", "ECHO ECHO", "E THEN E", "ALPHA PAPA", "PAPA ALPHA", "PAPHA ALPA", "T GOLF", "TANGOLF", "WHISKEE", "WHISKY", "CHARLIE C", "C CHARLIE", "YANGO", "DELTA NEXT", "CUEBEQ", "MILO", "KI LO", "HI-LO", "VVICTOR", "VICTORR", "LIME BRAVO", "BLUE BRAVO", "G IN JADE", "G IN ROSE", "BLUE IN RED", "YES BUT NO", "COLOUR", "MESSAGE", "CIPHER", "BUTTON", "TWO BUTTONS", "SIX BUTTONS", "I GIVE UP", "ONE ELEVEN", "ONE ONE ONE", "THREE ONES", "WHAT?", "THIS?", "THAT?", "BLUE!", "ECHO!", "BLANK", "BLANK?!", "NOTHING", "YELLOW TEXT", "BLACK TEXT?", "QUOTE V", "END QUOTE", "\"QUOTE K\"", "IN RED", "ORANGE", "IN YELLOW", "LIME", "IN GREEN", "JADE", "IN CYAN", "AZURE", "IN BLUE", "VIOLET", "IN MAGENTA", "ROSE",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BamboozledAgainButtonText,
    [SouvenirQuestion("What was the {1} decrypted text on the display in {0}?", "Bamboozled Again", TwoColumns4Answers, "THE LETTER", "ONE LETTER", "THE COLOUR", "ONE COLOUR", "THE PHRASE", "ONE PHRASE",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BamboozledAgainDisplayTexts1,
    [SouvenirQuestion("What was the {1} decrypted text on the display in {0}?", "Bamboozled Again", TwoColumns4Answers, "ALPHA", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "QUEBEC", "TANGO", "WHISKEY", "VICTOR", "YANKEE", "ECHO ECHO", "E THEN E", "ALPHA PAPA", "PAPA ALPHA", "PAPHA ALPA", "T GOLF", "TANGOLF", "WHISKEE", "WHISKY", "CHARLIE C", "C CHARLIE", "YANGO", "DELTA NEXT", "CUEBEQ", "MILO", "KI LO", "HI-LO", "VVICTOR", "VICTORR", "LIME BRAVO", "BLUE BRAVO", "G IN JADE", "G IN ROSE", "BLUE IN RED", "YES BUT NO", "COLOUR", "MESSAGE", "CIPHER", "BUTTON", "TWO BUTTONS", "SIX BUTTONS", "I GIVE UP", "ONE ELEVEN", "ONE ONE ONE", "THREE ONES", "WHAT?", "THIS?", "THAT?", "BLUE!", "ECHO!", "BLANK", "BLANK?!", "NOTHING", "YELLOW TEXT", "BLACK TEXT?", "QUOTE V", "END QUOTE", "\"QUOTE K\"", "IN RED", "ORANGE", "IN YELLOW", "LIME", "IN GREEN", "JADE", "IN CYAN", "AZURE", "IN BLUE", "VIOLET", "IN MAGENTA", "ROSE",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BamboozledAgainDisplayTexts2,
    [SouvenirQuestion("What color was the {1} text on the display in {0}?", "Bamboozled Again", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BamboozledAgainDisplayColor,

    [SouvenirQuestion("What color was the button in the {1} stage of {0}?", "Bamboozling Button", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", "Black", TranslateAnswers = true,
      ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BamboozlingButtonColor,
    [SouvenirQuestion("What was the {2} label on the button in the {1} stage of {0}?", "Bamboozling Button", TwoColumns4Answers, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE", TranslateFormatArgs = new[] { false, true },
      ExampleFormatArguments = new[] { QandA.Ordinal, "top", QandA.Ordinal, "bottom" }, ExampleFormatArgumentGroupSize = 2)]
    BamboozlingButtonLabel,
    [SouvenirQuestion("What was the {2} display in the {1} stage of {0}?", "Bamboozling Button", TwoColumns4Answers, "A LETTER", "A WORD", "THE LETTER", "THE WORD", "1 LETTER", "1 WORD", "ONE LETTER", "ONE WORD", "B", "C", "D", "E", "G", "K", "N", "P", "Q", "T", "V", "W", "Y", "BRAVO", "CHARLIE", "DELTA", "ECHO", "GOLF", "KILO", "NOVEMBER", "PAPA", "QUEBEC", "TANGO", "VICTOR", "WHISKEY", "YANKEE", "COLOUR", "RED", "ORANGE", "YELLOW", "LIME", "GREEN", "JADE", "CYAN", "AZURE", "BLUE", "VIOLET", "MAGENTA", "ROSE", "IN RED", "IN YELLOW", "IN GREEN", "IN CYAN", "IN BLUE", "IN MAGENTA", "QUOTE", "END QUOTE",
      ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    BamboozlingButtonDisplay,
    [SouvenirQuestion("What was the color of the {2} display in the {1} stage of {0}?", "Bamboozling Button", TwoColumns4Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Jade", "Cyan", "Azure", "Blue", "Violet", "Magenta", "Rose", "White", "Grey", TranslateAnswers = true,
      ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    BamboozlingButtonDisplayColor,

    [SouvenirQuestion("What was the category of {0}?", "Bar Charts", OneColumn4Answers, null, ExampleAnswers = new[] { "Non-Percussion Instruments", "European Capital Cities", "Cast of Star Trek: TOS", "Percussion Instruments", "Zodiac Signs", "20th Century Composers" })]
    BarChartsCategory,
    [SouvenirQuestion("What was the color of the {1} bar in {0}?", "Bar Charts", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    BarChartsColor,
    [SouvenirQuestion("What was the position of the {1} bar in {0}?", "Bar Charts", TwoColumns4Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "shortest", "second shortest", "second tallest", "tallest" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Ordinal(1, 4)]
    BarChartsHeight,
    [SouvenirQuestion("What was the label of the {1} bar in {0}?", "Bar Charts", TwoColumns4Answers, null, ExampleAnswers = new[] { "Glockenspiel", "C.Discharge", "Shakespeare", "Sagittarius", "Malted Milk", "Venting Gas" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BarChartsLabel,
    [SouvenirQuestion("What was the unit of {0}?", "Bar Charts", ThreeColumns6Answers, "Popularity", "Frequency", "Responses", "Occurrences", "Density", "Magnitude")]
    BarChartsUnit,

    [SouvenirQuestion("What was the screen number in {0}?", "Barcode Cipher", OneColumn4Answers)]
    [AnswerGenerator.Integers(0, 999999, "000000")]
    BarcodeCipherScreenNumber,
    [SouvenirQuestion("What was the edgework represented by the {1} barcode in {0}?", "Barcode Cipher", OneColumn4Answers, "SERIAL NUMBER", "BATTERIES", "BATTERY HOLDERS", "PORTS", "PORT PLATES", "LIT INDICATORS", "UNLIT INDICATORS", "INDICATORS",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    BarcodeCipherBarcodeEdgework,
    [SouvenirQuestion("What was the answer for the {1} barcode in {0}?", "Barcode Cipher", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BarcodeCipherBarcodeAnswers,

    [SouvenirQuestion("Which ingredient was in the {1} position on {0}?", "Bartending", TwoColumns4Answers, "Adelhyde", "Flanergide", "Bronson Extract", "Karmotrine", "Powdered Delta",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    BartendingIngredients,

    [SouvenirQuestion("What was this bean in {0}?", "Beans", OneColumn4Answers, "Wobbly Orange", "Wobbly Yellow", "Wobbly Green", "Not Wobbly Orange", "Not Wobbly Yellow", "Not Wobbly Green",
        UsesQuestionSprite = true, TranslateAnswers = true)]
    BeansColors,

    [SouvenirQuestion("What was sprout {1} in {0}?", "Bean Sprouts", TwoColumns4Answers,
        "Raw", "Cooked", "Burnt", "Fake", TranslateAnswers = true,
        ExampleFormatArgumentGroupSize = 1,
        ExampleFormatArguments = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" })]
    BeanSproutsColors,
    [SouvenirQuestion("What bean was on sprout {1} in {0}?", "Bean Sprouts", TwoColumns4Answers,
        "Left", "Right", "None", "Both", TranslateAnswers = true,
        ExampleFormatArgumentGroupSize = 1,
        ExampleFormatArguments = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" })]
    BeanSproutsBeans,

    [SouvenirQuestion("What was the bean in {0}?", "Big Bean", OneColumn4Answers, "Wobbly Orange", "Wobbly Yellow", "Wobbly Green", "Not Wobbly Orange", "Not Wobbly Yellow", "Not Wobbly Green", TranslateAnswers = true)]
    BigBeanColor,

    [SouvenirQuestion("What color was {1} in the solution to {0}?", "Big Circle", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BigCircleColors,

    [SouvenirQuestion("At which numeric value did you cut the correct wire in {0}?", "Binary LEDs", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 31)]
    BinaryLEDsValue,

    [SouvenirQuestion("What was the {1} initial number in {0}?", "Binary Shift", ThreeColumns6Answers, ExampleAnswers = new[] { "13", "14", "34", "46", "53", "64", "67", "77", "82", "96" },
        ExampleFormatArguments = new[] { "top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    BinaryShiftInitialNumber,
    [SouvenirQuestion("What number was selected at stage {1} in {0}?", "Binary Shift", ThreeColumns6Answers, "top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "0", "1", "2" }, ExampleFormatArgumentGroupSize = 1)]
    BinaryShiftSelectedNumberPossition,
    [SouvenirQuestion("What number was not selected at stage {1} in {0}?", "Binary Shift", ThreeColumns6Answers, "top-left", "top-middle", "top-right", "left-middle", "center", "right-middle", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "0", "1", "2" }, ExampleFormatArgumentGroupSize = 1)]
    BinaryShiftNotSelectedNumberPossition,

    [SouvenirQuestion("What word was displayed in {0}?", "Binary", ThreeColumns6Answers, "ah", "at", "am", "as", "an", "be", "by", "go", "if", "in", "is", "it", "mu", "nu", "no", "nu", "of", "pi", "to", "up", "us", "we", "xi", "ace", "aim", "air", "bed", "bob", "but", "buy", "can", "cat", "chi", "cut", "day", "die", "dog", "dot", "eat", "eye", "for", "fly", "get", "gut", "had", "hat", "hot", "ice", "lie", "lit", "mad", "map", "may", "new", "not", "now", "one", "pay", "phi", "pie", "psi", "red", "rho", "sad", "say", "sea", "see", "set", "six", "sky", "tau", "the", "too", "two", "why", "win", "yes", "zoo", "alfa", "beta", "blue", "chat", "cyan", "demo", "door", "east", "easy", "each", "edit", "fail", "fall", "fire", "five", "four", "game", "golf", "grid", "hard", "hate", "help", "hold", "iota", "kilo", "lima", "lime", "list", "lock", "lost", "stop", "test", "time", "tree", "type", "west", "wire", "wood", "xray", "yell", "zero", "zeta", "zulu", "abort", "about", "alpha", "black", "bravo", "clock", "close", "could", "crash", "delta", "digit", "eight", "gamma", "glass", "green", "guess", "hotel", "india", "kappa", "later", "least", "lemon", "month", "morse", "north", "omega", "oscar", "panic", "press", "romeo", "seven", "sigma", "smash", "south", "tango", "timer", "voice", "while", "white", "world", "worry", "would", "binary", "defuse", "disarm", "expert", "finish", "forget", "lambda", "manual", "module", "number", "orange", "period", "purple", "quebec", "should", "sierra", "source", "strike", "submit", "twitch", "victor", "violet", "window", "yellow", "yankee", "charlie", "epsilon", "explode", "foxtrot", "juliett", "measure", "mission", "omicron", "subject", "uniform", "upsilon", "whiskey", "detonate", "notsolve", "november")]
    BinaryWord,

    [SouvenirQuestion("How many pixels were {1} in the {2} quadrant in {0}?", "Bitmaps", ThreeColumns6Answers, TranslateFormatArgs = new[] { true, true },
        ExampleFormatArguments = new[] { "white", "top left", "white", "top right", "white", "bottom left", "white", "bottom right", "black", "top left", "black", "top right", "black", "bottom left", "black", "bottom right" }, ExampleFormatArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 16)]
    Bitmaps,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Black Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    BlackCipherScreen,

    [SouvenirQuestion("What roll did the module claim in the {1} stage of {0}?", "Blindfolded Yahtzee", TwoColumns4Answers, "Yahtzee", "Large Straight", "Small Straight", "Full House", "Four of a Kind", "Chance", "Three of a Kind", "1s", "2s", "3s", "4s", "5s", "6s",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    BlindfoldedYahtzeeClaim,

    [SouvenirQuestion("What color was the {1} button in {0}?", "Blind Maze", TwoColumns4Answers, "Red", "Green", "Blue", "Gray", "Yellow", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "north", "east", "west", "south" }, ExampleFormatArgumentGroupSize = 1)]
    BlindMazeColors,
    [SouvenirQuestion("Which maze did you solve {0} on?", "Blind Maze", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    BlindMazeMaze,

    [SouvenirQuestion("What song was flashed in {0}?", "Blinking Notes", OneColumn4Answers, "New Super Mario Bros. - Castle Theme", "Better Call Saul Intro", "Franz Schubert - Serenade", "Keep Talking and Nobody Explodes OST - SMILEYFACE", "Plants Vs. Zombies OST - Watery Graves (Horde)", "Cass Elliot - Make Your Own Kind Of Music", "Michael Jackson - Earth Song", "Maon Kurosaki - DEAD OR LIE", "La Marseillaise (French National Anthem)", "Dave James & Keith Beauvais - Class Act", "Rhythm Heaven Fever OST - Exhibition Match", "Lost OST - Hollywood And Vines", "TLoZ: A Link To The Past - Hyrule Castle", "TLoZ: Spirit Tracks OST - Realm Overworld", "Jamiroquai - Virtual Insanity", "Mii Channel Theme")]
    BlinkingNotesSong,

    [SouvenirQuestion("How many times did the LED flash in {0}?", "Blinkstop", ThreeColumns6Answers, "30", "33", "37", "39", "42", "44", "47", "51", "55", "59")]
    BlinkstopNumberOfFlashes,
    [SouvenirQuestion("Which color did the LED flash the fewest times in {0}?", "Blinkstop", TwoColumns4Answers, "Purple", "Cyan", "Yellow", "Multicolor", TranslateAnswers = true)]
    BlinkstopFewestFlashedColor,

    [SouvenirQuestion("What was the last letter pressed on {0}?", "Blockbusters", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    BlockbustersLastLetter,

    [SouvenirQuestion("What were the characters on the screen in {0}?", "Blue Arrows", ThreeColumns6Answers, "CA", "C1", "CB", "C8", "CF", "C4", "CE", "C6", "3A", "31", "3B", "38", "3F", "34", "3E", "36", "GA", "G1", "GB", "G8", "GF", "G4", "GE", "G6", "7A", "71", "7B", "78", "7F", "74", "7E", "76", "DA", "D1", "DB", "D8", "DF", "D4", "DE", "D6", "5A", "51", "5B", "58", "5F", "54", "5E", "56", "HA", "H1", "HB", "H8", "HF", "H4", "HE", "H6", "2A", "21", "2B", "28", "2F", "24", "2E", "26")]
    BlueArrowsInitialCharacters,

    [SouvenirQuestion("What was D in {0}?", "Blue Button", TwoColumns4Answers, AddThe = true)]
    [AnswerGenerator.Integers(1, 4)]
    BlueButtonD,
    [SouvenirQuestion("What was {1} in {0}?", "Blue Button", TwoColumns4Answers, AddThe = true,
        ExampleFormatArguments = new[] { "E", "F", "G", "H" }, ExampleFormatArgumentGroupSize = 1)]
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
    [SouvenirQuestion("What was Q in {0}?", "Blue Button", ThreeColumns6Answers, "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow", TranslateAnswers = true, AddThe = true)]
    BlueButtonQ,
    [SouvenirQuestion("What was X in {0}?", "Blue Button", TwoColumns4Answers, AddThe = true)]
    [AnswerGenerator.Integers(1, 5)]
    BlueButtonX,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Blue Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "ANCHOR", "ATTAIN", "DECIDE", "JAILOR", "LIGHTS", "OFFERS", "POETIC", "UNISON", "VECTOR", "VISION" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    BlueCipherScreen,

    [SouvenirQuestion("What was the {1} indicator label in {0}?", "Bob Barks", ThreeColumns6Answers, "BOB", "CAR", "CLR", "IND", "FRK", "FRQ", "MSA", "NSA", "SIG", "SND", "TRN", "BUB", "DOG", "ETC", "KEY", TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "top left", "top right", "bottom left", "bottom right" }, ExampleFormatArgumentGroupSize = 1)]
    BobBarksIndicators,
    [SouvenirQuestion("Which button flashed {1} in sequence in {0}?", "Bob Barks", TwoColumns4Answers, "top left", "top right", "bottom left", "bottom right", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BobBarksPositions,

    [SouvenirQuestion("What letter was initially visible on {0}?", "Boggle", ThreeColumns6Answers, ExampleAnswers = new[] { "A", "E", "G", "M", "T", "W" })]
    BoggleLetters,

    [SouvenirQuestion("What was the license number in {0}?", "Bomb Diffusal", TwoColumns4Answers, ExampleAnswers = new[] { "A4BIK5", "HI391D", "ZX98O1", "12K9PL" })]
    BombDiffusalLicenseNumber,

    [SouvenirQuestion("Which phrase was shown on {0}?", "Bone Apple Tea", OneColumn4Answers, "Bone Apple Tea", "Seizure Salad", "Hey to break it to ya", "This is oak ward", "Clea Shay", "It's in tents", "Bench watch", "You're an armature", "Man hat in", "Try all and era", "Million Air", "Die of beaties", "Rush and roulette", "Night and shining armour", "What a nice jester", "In some near", "This is my master peace", "I'm in a colder sac", "Cereal killer", "I come here off ten", "Slide of ham", "Test lah", "Refreshing campaign", "I'm being more pacific", "God blast you", "BC soft wear", "Sense in humor", "The three must of tears", "Third da men chin", "Prang mantas", "Hammy downs", "Yum, a case idea", "Dandy long legs", "Can't merge, little lone drive", "My guest is", "Sink", "You lake it", "Emit da feet")]
    BoneAppleTeaPhrase,

    [SouvenirQuestion("Which word was shown on {0}?", "Boob Tube", OneColumn4Answers, "Shittah", "Dik-Dik", "Aktashite", "Tetheradick", "Sack-Butt", "Nobber", "Knobstick", "Jerkinhead", "Haboob", "Fanny-Blower", "Assapanick", "Fuksheet", "Clatterfart", "Humpenscrump", "Cock-Bell", "Slagger", "Pakapoo", "Wankapin", "Lobcocked", "Poonga", "Sexagesm", "Tit-Bore", "Pershitte", "Invagination", "Bumfiddler", "Nestle-Cock", "Gullgroper", "Boob Tube", "Boobyalla", "Dreamhole")]
    BoobTubeWord,

    [SouvenirQuestion("Who said the {1} quote in {0}?", "Book of Mario", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "BookOfMarioSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BookOfMarioPictures,
    [SouvenirQuestion("What did {1} say in the {2} stage of {0}?", "Book of Mario", OneColumn4Answers, ExampleAnswers = new[] { "Dark Koopatrol. These people just blow hard...", "I came, Mario! You finna", "Absolutely, I came! Got it!", "Well, I’m so desperate, so you better save me…" },
        ExampleFormatArguments = new[] { "Goombell", QandA.Ordinal, "Prince Peach", QandA.Ordinal, "God Browser", QandA.Ordinal, "Mr.Krump", QandA.Ordinal, "Mario", QandA.Ordinal, "Flavio", QandA.Ordinal, "Quiz Thwomb", QandA.Ordinal, "Carbon", QandA.Ordinal, "Belda", QandA.Ordinal, "Make", QandA.Ordinal, "Yoshi Kid", QandA.Ordinal, "Bob", QandA.Ordinal, "Prosecutor Grubba", QandA.Ordinal },
        ExampleFormatArgumentGroupSize = 2)]
    BookOfMarioQuotes,

    [SouvenirQuestion("Which operator did you submit in the {1} stage of {0}?", "Boolean Wires", TwoColumns4Answers, "OR", "XOR", "AND", "NAND", "NOR", ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BooleanWiresEnteredOperators,

    [SouvenirQuestion("What was rule {1} in {0}?", "Boomtar the Great", ThreeColumns6Answers,
        ExampleFormatArgumentGroupSize = 1, ExampleFormatArguments = new[] { "one", "two" }, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(1, 6)]
    BoomtarTheGreatRules,

    [SouvenirQuestion("What was the {1} key’s border color when it was pressed in {0}?", "Bordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    BorderedKeysBorderColor,
    [SouvenirQuestion("What was the digit displayed when the {1} key was pressed in {0}?", "Bordered Keys", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    BorderedKeysDigit,
    [SouvenirQuestion("What was the {1} key’s key color when it was pressed in {0}?", "Bordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    BorderedKeysKeyColor,
    [SouvenirQuestion("What was the {1} key’s label when it was pressed in {0}?", "Bordered Keys", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    BorderedKeysLabel,
    [SouvenirQuestion("What was the {1} key’s label color when it was pressed in {0}?", "Bordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    BorderedKeysLabelColor,

    [SouvenirQuestion("What tweet was shown in {0}?", "Bottom Gear", OneColumn4Answers,
        "Today on bottom gear I drive a silent electric ca…", "*show budget does not exceed 23¥", "good evening ladies and gents today, our todayz s…", "today we will be reviewing one of a kin vehicle t…", "helo mate we are going to asda do  uwant sanythij…", "hello i am stug i go quikk noom", "oy luv you posh dickead oy 'ave cum bak gimme a s…", "hammon you tiny man where is the lambo chevy?", "gon ei crashed it into James car", "hammond you sodding tic tac this was my laborghin…", "call 999 my fokin cah is beaning on Fire mate", "ham ond i have crack additcion i am die", "Jeremy I have to write divorce papers today I don…", "we do not hav petroleum hmalet", "Tody on medium gear, wat happens when taste exhoo…", "K, I'll have a wiff.", "Ery nice.", "No Jeremia, car gas bad for helf.", "Shut mouth hammock.", "cock", "Shut up jams", "th Esped is a lot !", "weed", "car", "feet")]
    BottomGearTweet,

    [SouvenirQuestion("Which {1} appeared on {0}?", "Boxing", TwoColumns4Answers, ExampleAnswers = new[] { "Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander" },
        ExampleFormatArguments = new[] { "contestant’s first name", "contestant’s last name", "substitute’s first name", "substitute’s last name" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    BoxingNames,
    [SouvenirQuestion("What was the {1} of the contestant with strength rating {2} on {0}?", "Boxing", TwoColumns4Answers, ExampleAnswers = new[] { "Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander" }, TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "first name", "0", "first name", "1", "first name", "2", "last name", "0", "last name", "1", "last name", "2", "substitute’s first name", "0", "substitute’s first name", "1", "substitute’s first name", "2", "substitute’s last name", "0", "substitute’s last name", "1", "substitute’s last name", "2" }, ExampleFormatArgumentGroupSize = 2)]
    BoxingContestantByStrength,
    [SouvenirQuestion("What was {1}’s strength rating on {0}?", "Boxing", ThreeColumns6Answers, "0", "1", "2", "3", "4",
        ExampleFormatArguments = new[] { "Muhammad", "Mike", "Floyd", "Joe", "George", "Manny", "Sugar Ray", "Evander" }, ExampleFormatArgumentGroupSize = 1)]
    BoxingStrengthByContestant,

    [SouvenirQuestion("What was the {1} pattern in {0}?", "Braille", ThreeColumns6Answers, Type = AnswerType.Sprites, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Circles(2, 3, 20, 20, SuppressEmpty = true)]
    BraillePattern,

    [SouvenirQuestion("Which color appeared on the egg in {0}?", "Breakfast Egg", TwoColumns4Answers, "Crimson", "Orange", "Pink", "Beige", "Cyan", "Lime", "Petrol", TranslateAnswers = true)]
    BreakfastEggColor,

    [SouvenirQuestion("What was the {1} correct button you pressed in {0}?", "Broken Buttons", ThreeColumns6Answers, "bomb", "blast", "boom", "burst", "wire", "button", "module", "light", "led", "switch", "RJ-45", "DVI-D", "RCA", "PS/2", "serial", "port", "row", "column", "one", "two", "three", "four", "five", "six", "seven", "eight", "size", "this", "that", "other", "submit", "abort", "drop", "thing", "blank", "broken", "too", "to", "yes", "see", "sea", "c", "wait", "word", "bob", "no", "not", "first", "hold", "late", "fail",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    BrokenButtons,

    [SouvenirQuestion("What was the displayed chord in {0}?", "Broken Guitar Chords", ThreeColumns6Answers, ExampleAnswers = new[] { "C", "Dm", "F#sus", "Gm7", "A9", "Eadd9" })]
    BrokenGuitarChordsDisplayedChord,
    [SouvenirQuestion("In which position, from left to right, was the broken string in {0}?", "Broken Guitar Chords", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 6)]
    BrokenGuitarChordsMutedString,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Brown Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "AROUND", "JUKING", "OCELOT", "PARDON", "SCHOOL", "SOCCER", "SPRING", "TIMING", "VALVES", "VORTEX" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    BrownCipherScreen,

    [SouvenirQuestion("What was the color of the middle contact point in {0}?", "Brush Strokes", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Lime", "Green", "Cyan", "Sky", "Blue", "Purple", "Magenta", "Brown", "White", "Gray", "Black", "Pink", TranslateAnswers = true)]
    BrushStrokesMiddleColor,

    [SouvenirQuestion("Was the bulb initially lit in {0}?", "Bulb", TwoColumns2Answers, "Yes", "No", AddThe = true)]
    BulbInitialState,

    [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Burger Alarm", ThreeColumns6Answers, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    BurgerAlarmDigits,
    [SouvenirQuestion("What was the {1} order number in {0}?", "Burger Alarm", ThreeColumns6Answers, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 99, "00")]
    BurgerAlarmOrderNumbers,

    [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Burglar Alarm", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    BurglarAlarmDigits,

    [SouvenirQuestion("What color did the light glow in {0}?", "Button", TwoColumns4Answers, "red", "blue", "yellow", "white", AddThe = true, TranslateAnswers = true)]
    ButtonLightColor,

    [SouvenirQuestion("How many {1} buttons were there on {0}?", "Buttonage", ThreeColumns6Answers, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "orange", "blue", "pink", "white", "black", "white-bordered", "pink-bordered", "gray-bordered", "red-bordered", "“P”", "special" })]
    [AnswerGenerator.Integers(0, 64)]
    ButtonageButtons,

    [SouvenirQuestion("How many of the buttons in {0} were {1}?", "Button Sequence", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "blue", "yellow", "white" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 12)]
    ButtonSequencesColorOccurrences,

    [SouvenirQuestion("What color was the LED in the {1} stage of {0}?", "Cacti’s Conundrum", TwoColumns4Answers, "Blue", "Lime", "Orange", "Red",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    CactisConundrumColor,

    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Caesar Cycle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    CaesarCycleDialDirections,
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", "Caesar Cycle", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    CaesarCycleDialLabels,

    [SouvenirQuestion("What text was on the top display in the {1} stage of {0}?", "Caesar Psycho", ThreeColumns6Answers,
    ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("5*A-Z")]
    CaesarPsychoScreenTexts,
    [SouvenirQuestion("What color was the text on the top display in the second stage of {0}?", "Caesar Psycho", ThreeColumns6Answers, "white", "red", "magenta", "yellow", "green", "cyan", "violet")]
    CaesarPsychoScreenColor,

    [SouvenirQuestion("What was the LED color in {0}?", "Calendar", TwoColumns4Answers, "Green", "Yellow", "Red", "Blue", TranslateAnswers = true)]
    CalendarLedColor,

    [SouvenirQuestion("What color was this cell initially in {0}?", "CA-RPS", TwoColumns4Answers, "Red", "Green", "Blue", "Black", UsesQuestionSprite = true, TranslateAnswers = true)]
    CARPSCell,

    [SouvenirQuestion("What color was the {1} button in {0}?", "Cartinese", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue",
        ExampleFormatArguments = new[] { "up", "right", "down", "left" }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true, TranslateFormatArgs = new[] { true })]
    CartineseButtonColors,
    [SouvenirQuestion("What lyric was played by the {1} button in {0}?", "Cartinese", TwoColumns4Answers, "Aingobodirou", "Dongifubounan", "Ayofumylu", "Dimycamilayw", "Dogosemiu", "Bitgosemiu", "Iwittyluyu", "Herolideca", "Anseweke", "Likwoveke", "Omeygah", "Dediamnatifney",
        ExampleFormatArguments = new[] { "up", "right", "down", "left" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    CartineseLyrics,

    [SouvenirQuestion("What was the colour of the {1} panel in {0}?", "Catchphrase", ThreeColumns6Answers, "Red", "Green", "Blue", "Orange", "Purple", "Yellow",
        ExampleFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true, TranslateFormatArgs = new[] { true })]
    CatchphraseColour,

    [SouvenirQuestion("What was the {1} submitted answer in {0}?", "Challenge & Contact", TwoColumns4Answers, ExampleAnswers = new[] { "Accumulation", "Coffeebucks", "Perplexing", "Zoo", "Sunstone", "Bob" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ChallengeAndContactAnswers,

    [SouvenirQuestion("What was the {1} character in {0}?", "Character Codes", ThreeColumns6Answers, ExampleAnswers = new[] { "♥", "♣", "•", "☑", "☣", "Ϣ" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    CharacterCodesCharacter,

    [SouvenirQuestion("Which letter was present but not submitted on the left slider of {0}?", "Character Shift", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z")]
    CharacterShiftLetters,
    [SouvenirQuestion("Which digit was present but not submitted on the right slider of {0}?", "Character Shift", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("0-9")]
    CharacterShiftDigits,

    [SouvenirQuestion("Who was displayed in the {1} slot in the {2} stage of {0}?", "Character Slots", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CharacterSlotsSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    CharacterSlotsDisplayedCharacters,

    [SouvenirQuestion("What was {1} in {0}?", "Cheap Checkout", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "the paid amount", "the first paid amount", "the second paid amount" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(5, 50, "$0\".00\"")]
    CheapCheckoutPaid,

    [SouvenirQuestion("What was the cryptocurrency of {0}?", "Cheat Checkout", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    CheatCheckoutCurrency,
    [SouvenirQuestion("What was the hack method for the {1} hack of {0}?", "Cheat Checkout", TwoColumns4Answers, "DSA", "W", "CI", "XSS", "BFA",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    CheatCheckoutHack,
    [SouvenirQuestion("What was the site for the {1} hack of {0}?", "Cheat Checkout", OneColumn4Answers,
        ExampleAnswers = new[] { "medicalsite.co", "checkout.kt", "collection.com", "ktane.timwi.de", "cartoon.com", "galaxydeliver.com" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    CheatCheckoutSite,

    [SouvenirQuestion("Which bird {1} present in {0}?", "Cheep Checkout", OneColumn4Answers, "Auklet", "Bluebird", "Chickadee", "Dove", "Egret", "Finch", "Godwit", "Hummingbird", "Ibis", "Jay", "Kinglet", "Loon", "Magpie", "Nuthatch", "Oriole", "Pipit", "Quail", "Raven", "Shrike", "Thrush", "Umbrellabird", "Vireo", "Warbler", "Xantus’s Hummingbird", "Yellowlegs", "Zigzag Heron", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "was", "was not" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    CheepCheckoutBirds,

    [SouvenirQuestion("What was the {1} coordinate in {0}?", "Chess", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("a-f", "1-6")]
    ChessCoordinate,

    [SouvenirQuestion("What color was the {1} LED in {0}?", "Chinese Counting", TwoColumns4Answers, "White", "Red", "Green", "Orange", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
      ExampleFormatArguments = new[] { "left", "right" }, ExampleFormatArgumentGroupSize = 1)]
    ChineseCountingLED,

    [SouvenirQuestion("Which equation was used in {0}?", "Chinese Remainder Theorem", ThreeColumns6Answers)]
    [AnswerGenerator.ChineseRemainderTheorem]
    ChineseRemainderTheoremEquations,

    [SouvenirQuestion("Which note was part of the given chord in {0}?", "Chord Qualities", ThreeColumns6Answers, "A", "A♯", "B", "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯")]
    ChordQualitiesNotes,

    [SouvenirQuestion("Which arrow was shown in {0}?", "↻↺", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "ClockCounterSprites")]
    ClockCounterArrows,

    [SouvenirQuestion("What was the displayed number in {0}?", "Code", ThreeColumns6Answers, null, AddThe = true)]
    [AnswerGenerator.Integers(999, 9999)]
    CodeDisplayNumber,

    [SouvenirQuestion("Which of these words was submitted in {0}?", "Codenames", TwoColumns4Answers, ExampleAnswers = new[] { "Hyperborean", "Weenus", "Melody", "King" })]
    CodenamesAnswers,

    [SouvenirQuestion("What was the {1} movement in {0}?", "Coffee Beans", TwoColumns4Answers, "Horizontal", "Vertical", "Diagonal", "Nothing",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    CoffeeBeansMovements,

    [SouvenirQuestion("What was the last served coffee in {0}?", "Coffeebucks", OneColumn4Answers, "Twix Frappuccino", "The Blue Drink", "Matcha & Espresso Fusion", "Caramel Snickerdoodle Macchiato", "Liquid Cocaine", "S’mores Hot Chocolate", "The Pink Drink", "Grasshopper Frappuccino")]
    CoffeebucksCoffee,

    [SouvenirQuestion("Which coin was flipped in {0}?", "Coinage", ThreeColumns6Answers, ExampleAnswers = new[] { "e4", "h5", "d4", "h4", "c4", "h3", "c3", "g2", "f3", "h1", "f7" })]
    CoinageFlip,

    [SouvenirQuestion("What was {1}’s number in {0}?", "Color Addition", ThreeColumns6Answers, ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Strings(3, "0123456789")]
    ColorAdditionNumbers,

    [SouvenirQuestion("What color was this dot in {0}?", "Color Braille", ThreeColumns6Answers, "Black", "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow", "White", TranslateAnswers = true, UsesQuestionSprite = true)]
    ColorBrailleColor,

    [SouvenirQuestion("What was the {1}-stage indicator pattern in {0}?", "Color Decoding", TwoColumns4Answers, "Checkered", "Horizontal", "Vertical", "Solid", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ColorDecodingIndicatorPattern,
    [SouvenirQuestion("Which color {1} in the {2}-stage indicator pattern in {0}?", "Color Decoding", TwoColumns4Answers, "Green", "Purple", "Red", "Blue", "Yellow", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "appeared", QandA.Ordinal, "did not appear", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    ColorDecodingIndicatorColors,

    [SouvenirQuestion("What was the displayed word in {0}?", "Colored Keys", ThreeColumns6Answers, "red", "blue", "green", "yellow", "purple", "white", TranslateAnswers = true)]
    ColoredKeysDisplayWord,
    [SouvenirQuestion("What was the displayed word’s color in {0}?", "Colored Keys", ThreeColumns6Answers, "red", "blue", "green", "yellow", "purple", "white", TranslateAnswers = true)]
    ColoredKeysDisplayWordColor,
    [SouvenirQuestion("What was the color of the {1} key in {0}?", "Colored Keys", ThreeColumns6Answers, "red", "blue", "green", "yellow", "purple", "white",
        ExampleFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true, TranslateFormatArgs = new[] { true })]
    ColoredKeysKeyColor,
    [SouvenirQuestion("What letter was on the {1} key in {0}?", "Colored Keys", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
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
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ColorMorseColor,

    [SouvenirQuestion("What character was flashed by the {1} LED in {0}?", "Color Morse", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0-9A-Z")]
    ColorMorseCharacter,

    [SouvenirQuestion("What color was the {1} LED in {0}?", "Color One Two", TwoColumns4Answers, "Red", "Blue", "Green", "Yellow", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "left", "right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    ColorOneTwoColor,

    [SouvenirQuestion("How many buttons were {1} in {0}?", "Colors Maximization", ThreeColumns6Answers, ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(0, 11)]
    ColorsMaximizationColorCount,

    [SouvenirQuestion("What was the colour of this {1} in the {2} stage of {0}?", "Coloured Cubes", ThreeColumns6Answers, "Black", "Indigo", "Blue", "Forest", "Teal", "Azure", "Green", "Jade", "Cyan", "Maroon", "Plum", "Violet", "Olive", "Grey", "Maya", "Lime", "Mint", "Aqua", "Red", "Rose", "Magenta", "Orange", "Salmon", "Pink", "Yellow", "Cream", "White",
        ExampleFormatArguments = new[] { "cube", QandA.Ordinal, "stage light", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2, UsesQuestionSprite = true, TranslateAnswers = true, TranslateFormatArgs = new[] { true, false })]
    ColouredCubesColours,

    [SouvenirQuestion("What was the {1} colour flashed on the cylinder in {0}?", "Coloured Cylinder", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "Magenta", "White", "Black", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ColouredCylinderColours,

    [SouvenirQuestion("What was the color of the last word in the sequence in {0}?", "Colour Flash", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Magenta", "White", TranslateAnswers = true)]
    ColourFlashLastColor,

    [SouvenirQuestion("What number began here in {0}?", "Concentration", ThreeColumns6Answers, UsesQuestionSprite = true,
        TranslatableStrings = new[] { "the Concentration which began with {1} in the {0} position (in reading order)" })]
    [AnswerGenerator.Integers(1, 15)]
    ConcentrationStartingDigit,

    [SouvenirQuestion("What was the color of this button in {0}?", "Conditional Buttons", ThreeColumns6Answers, "black", "blue", "dark green", "light green", "orange", "pink", "purple", "red", "white", "yellow", UsesQuestionSprite = true, TranslateAnswers = true)]
    ConditionalButtonsColors,

    [SouvenirQuestion("What number was initially displayed on this screen in {0}?", "Connected Monitors", ThreeColumns6Answers, UsesQuestionSprite = true)]
    [AnswerGenerator.Integers(0, 99)]
    ConnectedMonitorsNumber,
    [SouvenirQuestion("What colour was the indicator on this screen in {0}?", "Connected Monitors", ThreeColumns6Answers, "Red", "Orange", "Green", "Blue", "Purple", "White", UsesQuestionSprite = true, TranslateAnswers = true)]
    ConnectedMonitorsSingleIndicator,
    [SouvenirQuestion("What colour was the {1} indicator on this screen in {0}?", "Connected Monitors", ThreeColumns6Answers, "Red", "Orange", "Green", "Blue", "Purple", "White",
        UsesQuestionSprite = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    ConnectedMonitorsOrdinalIndicator,

    [SouvenirQuestion("What pair of numbers was present in {0}?", "Connection Check", ThreeColumns6Answers, TranslatableStrings = new[] { "the Connection Check with no {0}’s", "the Connection Check with one {0}", "the Connection Check with two {0}’s", "the Connection Check with three {0}’s", "the Connection Check with four {0}’s" })]
    ConnectionCheckNumbers,

    [SouvenirQuestion("What was the solution you selected first in {0}?", "Coordinates", OneColumn4Answers, ExampleAnswers = new[] { "[4,7]", "C4", "<0, 2>", "3, 1", "(6,2)", "B-1", "“1, 0”", "4/3", "[12]", "#23", "四十七" })]
    CoordinatesFirstSolution,
    [SouvenirQuestion("What was the grid size in {0}?", "Coordinates", OneColumn4Answers, "9", "15", "25", "21", "35", "49", "(9)", "(15)", "(21)", "(25)", "(35)", "(49)", "3 by 3", "4 by 3", "5 by 3", "6 by 3", "7 by 3", "3 by 4", "4 by 4", "5 by 4", "6 by 4", "7 by 4", "3 by 5", "4 by 5", "5 by 5", "6 by 5", "7 by 5", "3 by 6", "4 by 6", "5 by 6", "6 by 6", "7 by 6", "3 by 7", "4 by 7", "5 by 7", "6 by 7", "7 by 7", "9*3", "12*4", "15*5", "18*6", "21*7", "12*3", "16*4", "20*5", "24*6", "28*7", "15*3", "20*4", "25*5", "30*6", "35*7", "18*3", "24*4", "30*5", "36*6", "42*7", "21*3", "28*4", "35*5", "42*6", "49*7", "9 : 3", "12 : 3", "15 : 3", "18 : 3", "21 : 3", "12 : 4", "16 : 4", "20 : 4", "24 : 4", "28 : 4", "15 : 5", "20 : 5", "25 : 5", "30 : 5", "35 : 5", "18 : 6", "24 : 6", "30 : 6", "36 : 6", "42 : 6", "21 : 7", "28 : 7", "35 : 7", "42 : 7", "49 : 7", "3×3", "3×4", "3×5", "3×6", "3×7", "4×3", "4×4", "4×5", "4×6", "4×7", "5×3", "5×4", "5×5", "5×6", "5×7", "6×3", "6×4", "6×5", "6×6", "6×7", "7×3", "7×4", "7×5", "7×6", "7×7")]
    CoordinatesSize,

    [SouvenirQuestion("What was the label of the starting coordinate in {0}?", "Coordination", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-F", "1-6")]
    CoordinationLabel,
    [SouvenirQuestion("Where was the starting coordinate in {0}?", "Coordination", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(6, 6)]
    CoordinationPosition,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Coral Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    CoralCipherScreen,

    [SouvenirQuestion("What was the color of the {1} corner in {0}?", "Corners", TwoColumns4Answers, "red", "green", "blue", "yellow", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "top-left", "top-right", "bottom-right", "bottom-left" }, ExampleFormatArgumentGroupSize = 1)]
    CornersColors,
    [SouvenirQuestion("How many corners in {0} were {1}?", "Corners", ThreeColumns6Answers, "0", "1", "2", "3", "4", TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue", "yellow" }, ExampleFormatArgumentGroupSize = 1)]
    CornersColorCount,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Cornflower Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    CornflowerCipherScreen,

    [SouvenirQuestion("What was the number initially shown in {0}?", "Cosmic", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9999)]
    CosmicNumber,

    [SouvenirQuestion("What was the {1} ingredient shown in {0}?", "Crazy Hamburger", ThreeColumns6Answers, "Bread", "Cheese", "Grass", "Meat", "Oil", "Peppers",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    CrazyHamburgerIngredient,

    [SouvenirQuestion("What was the {1} location in {0}?", "Crazy Maze", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "starting", "goal" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Strings("A-Z", "A-Z")]
    CrazyMazeStartOrGoal,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Cream Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    CreamCipherScreen,

    [SouvenirQuestion("What were the weather conditions on the {1} day in {0}?", "Creation", TwoColumns4Answers, "Clear", "Heat Wave", "Meteor Shower", "Rain", "Windy",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    CreationWeather,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Crimson Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    CrimsonCipherScreen,

    [SouvenirQuestion("What was the color in {0}?", "Critters", TwoColumns4Answers, "Yellow", "Pink", "Blue", "White", TranslateAnswers = true)]
    CrittersColor,

    [SouvenirQuestion("What was the displayed word in {0}?", "Cruel Binary", TwoColumns4Answers, ExampleAnswers = new[] { "LEAST", "YELLOW", "SIERRA", "WHITE" })]
    CruelBinaryDisplayedWord,

    [SouvenirQuestion("Which of these characters appeared in the {1} stage of {0}?", "Cruel Keypads", ThreeColumns6Answers, "ㄹ", "ㅁ", "ㅂ", "ㄱ", "ㄲ", "ㄷ", "ㅈ", "ㅉ", "ㅟ", "ㅋ", "ㅌ", "ㅍ", "ㅃ", "ㅅ", "ㅆ", "ㅇ", "ㅢ", "ㄴ", "ㄸ", ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    CruelKeypadsDisplayedSymbols,
    [SouvenirQuestion("What was the color of the bar in the {1} stage of {0}?", "Cruel Keypads", ThreeColumns6Answers, "Red", "Blue", "Yellow", "Green", "Magenta", "White", ExampleFormatArguments = new[] { QandA.Ordinal }, TranslateAnswers = true, ExampleFormatArgumentGroupSize = 1)]
    CruelKeypadsColors,

    [SouvenirQuestion("Which cell was pre-filled at the start of {0}?", "cRule", TwoColumns4Answers, Type = AnswerType.Sprites, AddThe = true)]
    CRulePrefilled,
    [SouvenirQuestion("Which symbol pair was here in {0}?", "cRule", ThreeColumns6Answers, "♤♤", "♤♧", "♤♢", "♤♡", "♧♤", "♧♧", "♧♢", "♧♡", "♢♤", "♢♧", "♢♢", "♢♡", "♡♤", "♡♧", "♡♢", "♡♡", AddThe = true, UsesQuestionSprite = true)]
    CRuleSymbolPair,
    [SouvenirQuestion("Which symbol pair was present on {0}?", "cRule", ThreeColumns6Answers, "♤♤", "♤♧", "♤♢", "♤♡", "♧♤", "♧♧", "♧♢", "♧♡", "♢♤", "♢♧", "♢♢", "♢♡", "♡♤", "♡♧", "♡♢", "♡♡", AddThe = true)]
    CRuleSymbolPairPresent,
    [SouvenirQuestion("Where was {1} in {0}?", "cRule", ThreeColumns6Answers, Type = AnswerType.Sprites, AddThe = true,
        ExampleFormatArguments = new[] { "♤♤", "♤♧", "♤♢", "♤♡", "♧♤", "♧♧", "♧♢", "♧♡", "♢♤", "♢♧", "♢♢", "♢♡", "♡♤", "♡♧", "♡♢", "♡♡" }, ExampleFormatArgumentGroupSize = 1)]
    CRuleSymbolPairCell,

    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Cryptic Cycle", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleCrypticSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    CrypticCycleDialDirections,
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", "Cryptic Cycle", ThreeColumns6Answers, Type = AnswerType.CrypticCycleBoozleglyphFont,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!\"£$%^&*()[]{}<>")]
    CrypticCycleDialLabels,

    [SouvenirQuestion("What was the label of the {1} key in {0}?", "Cryptic Keypad", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Strings("A-Z")]
    CrypticKeypadLabels,
    [SouvenirQuestion("Which cardinal direction was the {1} key rotated to in {0}?", "Cryptic Keypad", TwoColumns4Answers, "North", "East", "South", "West", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    CrypticKeypadRotations,

    [SouvenirQuestion("What was the {1} cube rotation in {0}?", "Cube", TwoColumns4Answers, "rotate cw", "tip left", "tip backwards", "rotate ccw", "tip right", "tip forwards",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, AddThe = true, TranslateAnswers = true)]
    CubeRotations,

    [SouvenirQuestion("What was the first digit of the initially displayed number in {0}?", "Cursed Double-Oh", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    CursedDoubleOhInitialPosition,

    [SouvenirQuestion("Who was the {1} customer in {0}?", "Customer Identification", OneColumn4Answers, "Akari", "Alberto", "Allan", "Amy", "Austin", "Bertha", "Big Pauly", "Boomer", "Boopsy & Bill", "Brody", "Bruna Romano", "C.J. Friskins", "Cameo", "Captain Cori", "Carlo Romano", "Cecilia", "Cherissa", "Chester", "Chuck", "Clair", "Cletus", "Clover", "Connor", "Cooper", "Crystal", "Daniela", "Deano", "Didar", "Doan", "Drakson", "Duke Gotcha", "Edna", "Elle", "Ember", "Emmlette", "Evelyn", "Fernanda", "Foodini", "Franco", "Georgito", "Gino Romano", "Greg", "Gremmie", "Hacky Zak", "Hank", "Hope", "Hugo", "Iggy", "Indigo", "Ivy", "James", "Janana", "Johnny", "Jojo", "Joy", "Julep", "Kahuna", "Kaleb", "Kasey O", "Kayla", "Kenji", "Kenton", "Kingsley", "Koilee", "LePete", "Liezel", "Lisa", "Little Edoardo", "Maggie", "Mandi", "Marty", "Mary", "Matt", "Mayor Mallow", "Mesa", "Mindy", "Mitch", "Moe", "Mousse", "Mr. Bombolony", "Nevada", "Nick", "Ninjoy", "Nye", "Okalani", "Olga", "Olivia", "Pally", "Papa Louie", "Peggy", "Penny", "Perri", "Petrona", "Pinch Hitwell", "Professor Fitz", "Prudence", "Quinn", "Radlynn", "Rhonda", "Rico", "Ripley", "Rita", "Robby", "Rollie", "Roy", "Rudy", "Santa", "Sarge Fan", "Sasha", "Scarlett", "Scooter", "Shannon", "Sienna", "Simone", "Skip", "Skyler", "Sprinks The Clown", "Steven", "Sue", "Taylor", "The Dynamoe", "Timm", "Tohru", "Tony", "Trishna", "Utah", "Vicky", "Vincent", "Wally", "Wendy", "Whiff", "Whippa", "Willow", "Wylan B", "Xandra", "Xolo", "Yippy", "Yui", "Zoe",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    CustomerIdentificationCustomer,

    [SouvenirQuestion("Where was the button at the {1} stage in {0}?", "Cyan Button", TwoColumns4Answers, "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, AddThe = true, TranslateAnswers = true)]
    CyanButtonPositions,

    [SouvenirQuestion("Which region did you depart from in {0}?", "DACH Maze", OneColumn4Answers, "Burgenland, A", "Carinthia, A", "Lower Austria, A", "North Tyrol, A", "Upper Austria, A", "East Tyrol, A", "Salzburg, A", "Styria, A", "Vorarlberg, A", "Vienna, A", "Aargau, CH", "Appenzell Inner Rhodes, CH", "Appenzell Outer Rhodes, CH", "Basel Country, CH", "Bern, CH", "Basel City, CH", "Fribourg, CH", "Geneva, CH", "Glarus, CH", "Grisons, CH", "Jura, CH", "Luzern, CH", "Nidwalden, CH", "Neuchâtel, CH", "Obwalden, CH", "Schaffhausen, CH", "St. Gallen, CH", "Solothurn, CH", "Schwyz, CH", "Thurgau, CH", "Ticino, CH", "Uri, CH", "Vaud, CH", "Valais, CH", "Zug, CH", "Zürich, CH", "Brandenburg, D", "Berlin, D", "Baden-Württemberg, D", "Bavaria, D", "Bremen, D", "Hesse, D", "Hamburg, D", "Mecklenburg-Vorpommern, D", "Lower Saxony, D", "North Rhine-Westphalia, D", "Rhineland-Palatinate, D", "Schleswig-Holstein, D", "Saarland, D", "Saxony, D", "Saxony-Anhalt, D", "Thuringia, D", "Liechtenstein", TranslateAnswers = true)]
    DACHMazeOrigin,

    [SouvenirQuestion("What was the shape generated in {0}?", "Deaf Alley", ThreeColumns6Answers, ExampleAnswers = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "d", "e", "f", "g", "h", "i", "j", "k", "m", "n", "p", "q", "r", "t", "u", "y", "1", "2", "3", "4", "6", "7", "8", "9", "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "[", "]", "{", "}", ":", ";", "“", "‘", "<", ",", ">", ".", "?", "/", "\\" })]
    DeafAlleyShape,

    [SouvenirQuestion("What deck did the first card of {0} belong to?", "Deck of Many Things", TwoColumns4Answers, "Standard", "Metropolitan", "Maritime", "Arctic", "Tropical", "Oasis", "Celestial", AddThe = true)]
    DeckOfManyThingsFirstCard,

    [SouvenirQuestion("What was the starting {1} defining color in {0}?", "Decolored Squares", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "column", "row" }, ExampleFormatArgumentGroupSize = 1)]
    DecoloredSquaresStartingPos,

    [SouvenirQuestion("What was the {1} of the {2} goal in {0}?", "Decolour Flash", ThreeColumns6Answers, "Blue", "Green", "Red", "Magenta", "Yellow", "White", ExampleFormatArguments = new[] { "colour", QandA.Ordinal, "word", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2, TranslateAnswers = true, TranslateFormatArgs = new[] { true, false })]
    DecolourFlashGoal,

    [SouvenirQuestion("What number was initially shown on display {1} in {0}?", "Denial Displays", ThreeColumns6Answers, ExampleAnswers = new[] { "1", "22", "333", "4", "55", "666", "7", "88", "999" },
        ExampleFormatArguments = new[] { "A", "B", "C", "D", "E" }, ExampleFormatArgumentGroupSize = 1)]
    DenialDisplaysDisplays,

    [SouvenirQuestion("What was the {1} display in {0}?", "DetoNATO", TwoColumns4Answers, ExampleAnswers = new[] { "Ozzy Osbourne", "Jouleliette", "Flockstrot", "Joulelette", "Jouleliett", "Uniqueform" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    DetoNATODisplay,

    [SouvenirQuestion("What was the {1} egg’s {2} rotation in {0}?", "Devilish Eggs", TwoColumns4Answers, "W90CW", "W180CW", "W270CW", "W360CW", "W90CCW", "W180CCW", "W270CCW", "W360CCW", "T90CW", "T180CW", "T270CW", "T360CW", "T90CCW", "T180CCW", "T270CCW", "T360CCW", TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "top", QandA.Ordinal, "bottom", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    DevilishEggsRotations,
    [SouvenirQuestion("What was the {1} digit in the string of numbers on {0}?", "Devilish Eggs", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    DevilishEggsNumbers,
    [SouvenirQuestion("What was the {1} letter in the string of letters on {0}?", "Devilish Eggs", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    DevilishEggsLetters,

    [SouvenirQuestion("What dialtones were heard in {0}?", "Dialtones", OneColumn4Answers, Type = AnswerType.Audio, ForeignAudioID = Sounds.Generated)]
    DialtonesDialtones,

    [SouvenirQuestion("What was the number on the {1} button in {0}?", "Digisibility", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 9)]
    DigisibilityDisplayedNumber,

    [SouvenirQuestion("What was the initial number in {0}?", "Digit String", TwoColumns4Answers)]
    [AnswerGenerator.Strings("1-9", "6*0-9", "1-9")]
    DigitStringInitialNumber,

    [SouvenirQuestion("Which of these was a visible character in {0}?", "Dimension Disruption", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z0-9")]
    DimensionDisruptionVisibleLetters,

    [SouvenirQuestion("How many times did you press the button in the {1} stage of {0}?", "Directional Button", TwoColumns4Answers, "1", "2", "3", "4",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    DirectionalButtonButtonCount,

    [SouvenirQuestion("What was {1}’s remembered position in {0}?", "Discolored Squares", ThreeColumns6Answers, Type = AnswerType.Sprites, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "Blue", "Red", "Yellow", "Green", "Magenta" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(4, 4)]
    DiscoloredSquaresRememberedPositions,

    [SouvenirQuestion("What was the missing information for the {1} key in {0}?", "Disordered Keys", OneColumn3Answers, "Key color", "Label color", "Label",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    DisorderedKeysMissingInfo,
    [SouvenirQuestion("What was the revealed key color for the {1} key in {0}?", "Disordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    DisorderedKeysRevealedKeyColor,
    [SouvenirQuestion("What was the revealed label for the {1} key in {0}?", "Disordered Keys", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    DisorderedKeysRevealedLabel,
    [SouvenirQuestion("What was the revealed label color for the {1} key in {0}?", "Disordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    DisorderedKeysRevealedLabelColor,
    [SouvenirQuestion("What was the unrevealed key color for the {1} key in {0}?", "Disordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    DisorderedKeysUnrevealedKeyColor,
    [SouvenirQuestion("What was the unrevealed label for the {1} key in {0}?", "Disordered Keys", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    DisorderedKeysUnrevealedKeyLabel,
    [SouvenirQuestion("What was the unrevealed label color for the {1} key in {0}?", "Disordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    DisorderedKeysUnrevealedLabelColor,

    [SouvenirQuestion("What color was {1} while pressing it in {0}?", "Divided Squares", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Black", "White",
        ExampleFormatArguments = new[] { "the square", "the correct square" }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true, TranslateFormatArgs = new[] { true })]
    DividedSquaresColor,

    [SouvenirQuestion("What was the {1} stage’s number in {0}?", "Divisible Numbers", ThreeColumns6Answers, null,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9999)]
    DivisibleNumbersNumbers,

    [SouvenirQuestion("What jingle played in {0}?", "Doofenshmirtz Evil Inc.", OneColumn4Answers, Type = AnswerType.Audio, ForeignAudioID = "doofenshmirtzEvilIncModule", AudioSizeMultiplier = 8)]
    DoofenshmirtzEvilIncJingles,
    [SouvenirQuestion("Which image was shown in {0}?", "Doofenshmirtz Evil Inc.", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    DoofenshmirtzEvilIncInators,

    [SouvenirQuestion("What was the starting position in {0}?", "Double Arrows", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 81, "00")]
    DoubleArrowsStart,
    [SouvenirQuestion("Which {1} arrow moved {2} in the grid in {0}?", "Double Arrows", TwoColumns4Answers, "Up", "Right", "Left", "Down",
        ExampleFormatArguments = new[] { "inner", "up", "outer", "up", "inner", "down", "outer", "down", "inner", "left", "outer", "left", "inner", "right", "outer", "right" }, TranslateAnswers = true, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, true })]
    DoubleArrowsArrow,
    [SouvenirQuestion("Which direction in the grid did the {1} arrow move in {0}?", "Double Arrows", TwoColumns4Answers, "Up", "Right", "Left", "Down",
        ExampleFormatArguments = new[] { "inner up", "inner down", "inner left", "inner right", "outer up", "outer down", "outer left", "outer right" }, TranslateAnswers = true, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    DoubleArrowsMovement,

    [SouvenirQuestion("What was the screen color on the {1} stage of {0}?", "Double Color", ThreeColumns6Answers, "Green", "Blue", "Red", "Pink", "Yellow", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    DoubleColorColors,

    [SouvenirQuestion("What was the digit on the {1} display in {0}?", "Double Digits", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
        ExampleFormatArguments = new[] { "left", "right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    DoubleDigitsDisplays,

    [SouvenirQuestion("What was the starting key number in {0}?", "Double Expert", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(30, 69)]
    DoubleExpertStartingKeyNumber,
    [SouvenirQuestion("What was the word you submitted in {0}?", "Double Expert", ThreeColumns6Answers, ExampleAnswers = new[] { "Echo", "November", "Rodeo", "Words", "Victor", "Zulu" })]
    DoubleExpertSubmittedWord,

    [SouvenirQuestion("What clip was played in {0}?", "Double Listening", ThreeColumns6Answers, Type = AnswerType.Audio, AudioFieldName = "ListeningAudio")]
    DoubleListeningSounds,

    [SouvenirQuestion("Which button was the submit button in {0}?", "Double-Oh", ThreeColumns6Answers, "↕", "⇕", "↔", "⇔", "◆")]
    DoubleOhSubmitButton,

    [SouvenirQuestion("What color was the {1} screen in the {2} stage of {0}?", "Double Screen", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "top", QandA.Ordinal, "bottom", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    DoubleScreenColors,

    [SouvenirQuestion("Which of these symptoms was listed on {0}?", "Dr. Doctor", TwoColumns4Answers, "Bloating", "Chills", "Cold Hands", "Constipation", "Cough", "Diarrhea", "Disappearance of the Ears", "Dizziness", "Excessive Crying", "Fatigue", "Fever", "Foot swelling", "Gas", "Hallucination", "Headache", "Loss of Smell", "Muscle Cramp", "Nausea", "Numbness", "Shortness of Breath", "Sleepiness", "Thirstiness", "Throat irritation")]
    DrDoctorSymptoms,
    [SouvenirQuestion("Which of these diseases was listed on {0}, but not the one treated?", "Dr. Doctor", TwoColumns4Answers, "Alztimer’s", "Braintenance", "Color allergy", "Detonession", "Emojilepsy", "Foot and Morse", "Gout of Life", "HRV", "Indicitis", "Jaundry", "Keypad stones", "Legomania", "Microcontusion", "Narcolization", "OCd", "Piekinson’s", "Quackgrounds", "Royal Flu", "Seizure Siphor", "Tetrinus", "Urinary LEDs", "Verticode", "Widgeting", "XMAs", "Yes-no infection", "Zooties", "Chronic Talk", "Jukepox", "Neurolysis", "Perspective Loss", "Orientitis", "Huntington’s disease")]
    DrDoctorDiseases,

    [SouvenirQuestion("What was the decrypted word in {0}?", "Dreamcipher", OneColumn4Answers, ExampleAnswers = new[] { "asparagus", "demonstration", "fossilizing", "foursquare", "grinning", "jumpiness", "pasteboard", "prosecution", "sarcastic", "transition" })]
    DreamcipherWord,

    [SouvenirQuestion("What was the color of the curtain in {0}?", "Duck", TwoColumns4Answers, "blue", "yellow", "green", "orange", "red", AddThe = true, TranslateAnswers = true)]
    DuckCurtainColor,

    [SouvenirQuestion("Which player {1} present in {0}?", "Dumb Waiters", OneColumn4Answers, ExampleAnswers = new[] { "Arceus", "Danny7007", "EpicToast", "eXish", "Fang", "Makebao", "MCD573", "Mr. Peanut", "Mythers", "Xmaster" },
        ExampleFormatArguments = new[] { "was", "was not" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    DumbWaitersPlayerAvailable,

    [SouvenirQuestion("What was the background in {0}?", "Earthbound", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    EarthboundBackground,
    [SouvenirQuestion("Which monster was displayed in {0}?", "Earthbound", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    EarthboundMonster,

    [SouvenirQuestion("What word was asked to be spelled in {0}?", "eeB gnillepS", TwoColumns4Answers, ExampleAnswers = new[] { "odontalgia", "precocious", "privilege", "prospicience" })]
    eeBgnillepSWord,

    [SouvenirQuestion("What was the last digit on the small display in {0}?", "Eight", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    EightLastSmallDisplayDigit,
    [SouvenirQuestion("What was the position of the last broken digit in {0}?", "Eight", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 8)]
    EightLastBrokenDigitPosition,
    [SouvenirQuestion("What were the last resulting digits in {0}?", "Eight", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(50, 99)]
    EightLastResultingDigits,
    [SouvenirQuestion("What was the last displayed number in {0}?", "Eight", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(10, 99)]
    EightLastDisplayedNumber,

    [SouvenirQuestion("What was the {1} rune shown on {0}?", "Elder Futhark", TwoColumns4Answers, "Algiz", "Ansuz", "Berkana", "Dagaz", "Ehwaz", "Eihwaz", "Fehu", "Gebo", "Hagalaz", "Isa", "Jera", "Kenaz", "Laguz", "Mannaz", "Nauthiz", "Othila", "Perthro", "Raido", "Sowulo", "Teiwaz", "Thurisaz", "Uruz", "Wunjo",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ElderFutharkRunes,

    [SouvenirQuestion("What was the {1} emoji in {0}?", "Emoji", ThreeColumns6Answers, Type = AnswerType.Sprites,
        ExampleFormatArguments = new[] { "left", "right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    EmojiEmoji,

    [SouvenirQuestion("What was the {1} keyword in {0}?", "ƎNA Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    EnaCipherKeywordAnswer,
    [SouvenirQuestion("What was the transposition key in {0}?", "ƎNA Cipher", TwoColumns4Answers)]
    [AnswerGenerator.Strings(6, "123456")]
    EnaCipherExtAnswer,
    [SouvenirQuestion("What was the encrypted word in {0}?", "ƎNA Cipher", TwoColumns4Answers)]
    [AnswerGenerator.Strings(6, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
    EnaCipherEncryptedAnswer,

    [SouvenirQuestion("Which of these numbers appeared on a die in the {1} stage of {0}?", "Encrypted Dice", TwoColumns4Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    EncryptedDice,

    [SouvenirQuestion("Which shape was the {1} operand in {0}?", "Encrypted Equations", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "EncryptedEquationsSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    EncryptedEquationsShapes,

    [SouvenirQuestion("What method of encryption was used by {0}?", "Encrypted Hangman", OneColumn4Answers, "Caesar Cipher", "Atbash Cipher", "Rot-13 Cipher", "Affine Cipher", "Modern Cipher", "Vigenère Cipher", "Playfair Cipher", TranslateAnswers = true)]
    EncryptedHangmanEncryptionMethod,
    [SouvenirQuestion("What module name was encrypted by {0}?", "Encrypted Hangman", OneColumn4Answers, ExampleAnswers = new[] { "Anagrams", "Word Scramble", "Two Bits", "Switches", "Lights Out", "Emoji Math", "Math", "Semaphore", "Piano Keys", "Colour Flash" })]
    EncryptedHangmanModule,

    [SouvenirQuestion("Which symbol on {0} was spinning {1}?", "Encrypted Maze", ThreeColumns6Answers, "f", "H", "$", "l", "B", "N", "g", "I", "%", "m", "C", "O", "h", "J", "&", "n", "D", "P", "i", "K", "'", "o", "E", "Q", "j", "L", "(", "p", "F", "R",
        Type = AnswerType.DynamicFont, ExampleFormatArguments = new[] { "clockwise", "counter-clockwise" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    EncryptedMazeSymbols,

    [SouvenirQuestion("What was the {1} on {0}?", "Encrypted Morse", TwoColumns4Answers, ExampleAnswers = new[] { "Detonate", "Ready Now", "Please No", "Cheesecake" },
        ExampleFormatArguments = new[] { "received call", "sent response" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    EncryptedMorseCallResponse,

    [SouvenirQuestion("What was the first encoding used in {0}?", "Encryption Bingo", OneColumn4Answers, "Morse Code", "Tap Code", "Maritime Flags", "Semaphore", "Pigpen", "Lombax", "Braille", "Wingdings", "Zoni", "Galatic Alphabet", "Arrow", "Listening", "Regular Number", "Chinese Number", "Cube Symbols", "Runes", "New York Point", "Fontana", "ASCII Hex Code", TranslateAnswers = true)]
    EncryptionBingoEncoding,

    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Enigma Cycle", ThreeColumns3Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleThreeSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    EnigmaCycleDialDirectionsThree,
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Enigma Cycle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleTwelveSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    EnigmaCycleDialDirectionsTwelve,
    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Enigma Cycle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    EnigmaCycleDialDirectionsEight,
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", "Enigma Cycle", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    EnigmaCycleDialLabels,

    [SouvenirQuestion("What was the displayed quote on {0}?", "English Entries", OneColumn4Answers, ExampleAnswers = new[] { "Let’s go shopping!", "We wear our shoes in the house.", "(Kevin starts to clap)", "What is Namsu doing?" })]
    EnglishEntriesDisplay,

    [SouvenirQuestion("What was the {1} digit in the {2} number shown in {0}?", "Entry Number Four", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    EntryNumberFourDigits,

    [SouvenirQuestion("What was the {1} digit in the {2} number shown in {0}?", "Entry Number One", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    EntryNumberOneDigits,

    [SouvenirQuestion("What word was asked to be spelled in {0}?", "Épelle-moi Ça", TwoColumns4Answers, ExampleAnswers = new[] { "abasourdi", "aberrant", "abrasive", "acatalectique", "accueil", "acrobatie", "aligot", "amphigourique", "analgésiante", "antipasti" })]
    ÉpelleMoiÇaWord,

    [SouvenirQuestion("What was the displayed symbol in {0}?", "Equations X", ThreeColumns6Answers, "H(T)", "P", "\u03C7", "\u03C9", "Z(T)", "\u03C4", "\u03BC", "\u03B1", "K")]
    EquationsXSymbols,

    [SouvenirQuestion("What was the active error code in {0}?", "Error Codes", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 101, 1, "X2")]
    ErrorCodesActiveError,

    [SouvenirQuestion("What was the beat for the {1} arrow from the bottom in {0}?", "Etterna", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
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

    [SouvenirQuestion("What pitch did the {1} faerie sing in {0}?", "Faerie Fires", ThreeColumns6Answers, Type = AnswerType.Audio, AudioFieldName = "FaerieFiresAudio", AudioSizeMultiplier = 8,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    FaerieFiresPitchOrdinal,
    [SouvenirQuestion("What pitch did the {1} faerie sing in {0}?", "Faerie Fires", ThreeColumns6Answers, Type = AnswerType.Audio, AudioFieldName = "FaerieFiresAudio", AudioSizeMultiplier = 8,
        ExampleFormatArguments = new[] { "red", "green", "blue", "yellow", "cyan", "magenta" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    FaerieFiresPitchColor,
    [SouvenirQuestion("What color was the {1} faerie in {0}?", "Faerie Fires", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "Cyan", "Magenta",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    FaerieFiresColor,

    [SouvenirQuestion("What was the last pair of letters in {0}?", "Fast Math", ThreeColumns6Answers, ExampleAnswers = new[] { "CT", "DK", "SA", "SG", "SX", "TX", "TZ", "XP", "XX", "ZB" })]
    FastMathLastLetters,

    [SouvenirQuestion("What was the last displayed message in {0}?", "Fast Playfair Cipher", ThreeColumns6Answers, ExampleAnswers = new[] { "CT", "DK", "SA", "SG", "SH", "TG", "TZ", "FP", "JA", "ZB" })]
    [AnswerGenerator.Strings("A-WYZ")]
    [AnswerGenerator.Strings("A-WYZ", "A-WYZ")]
    FastPlayfairCipherLastMessage,

    [SouvenirQuestion("Which button referred to the {1} button in reading order in {0}?", "Faulty Buttons", ThreeColumns6Answers, Type = AnswerType.Sprites, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(4, 4)]
    FaultyButtonsReferredToThisButton,
    [SouvenirQuestion("Which button did the {1} button in reading order refer to in {0}?", "Faulty Buttons", ThreeColumns6Answers, Type = AnswerType.Sprites, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(4, 4)]
    FaultyButtonsThisButtonReferredTo,

    [SouvenirQuestion("What was the exit coordinate in {0}?", "Faulty RGB Maze", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-G", "1-7")]
    FaultyRGBMazeExit,
    [SouvenirQuestion("Where was the {1} key in {0}?", "Faulty RGB Maze", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-G", "1-7")]
    FaultyRGBMazeKeys,
    [SouvenirQuestion("Which maze number was the {1} maze in {0}?", "Faulty RGB Maze", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0-9a-f")]
    FaultyRGBMazeNumber,

    [SouvenirQuestion("What was the day displayed in the {1} stage of {0}?", "Find The Date", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 31)]
    FindTheDateDay,
    [SouvenirQuestion("What was the month displayed in the {1} stage of {0}?", "Find The Date", TwoColumns4Answers, "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    FindTheDateMonth,
    [SouvenirQuestion("What was the year displayed in the {1} stage of {0}?", "Find The Date", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 2899, "000")]
    FindTheDateYear,

    [SouvenirQuestion("Which of these words was on the display in {0}?", "Five Letter Words", ThreeColumns6Answers, ExampleAnswers = new[] { "ABAFF", "MAYOR", "PANUS", "FRIZE", "NIRIS", "TEJON" })]
    FiveLetterWordsDisplayedWords,

    [SouvenirQuestion("What was the {1} digit on the {2} display of {0}?", "FizzBuzz", ThreeColumns6Answers, ExampleFormatArguments = new[] { QandA.Ordinal, "top", QandA.Ordinal, "middle", QandA.Ordinal, "bottom" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { false, true })]
    [AnswerGenerator.Integers(0, 9)]
    FizzBuzzDisplayedNumbers,

    [SouvenirQuestion("What was the displayed number in {0}?", "Flags", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 7)]
    FlagsDisplayedNumber,
    [SouvenirQuestion("What was the main country flag in {0}?", "Flags", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "FlagsSprites")]
    FlagsMainCountry,
    [SouvenirQuestion("Which of these country flags was shown, but not the main country flag, in {0}?", "Flags", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "FlagsSprites")]
    FlagsCountries,

    [SouvenirQuestion("What number was displayed on {0}?", "Flashing Arrows", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99)]
    FlashingArrowsDisplayedValue,
    [SouvenirQuestion("What color flashed {1} black on the relevant arrow in {0}?", "Flashing Arrows", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White",
        ExampleFormatArguments = new[] { "before", "after" }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true, TranslateFormatArgs = new[] { true })]
    FlashingArrowsReferredArrow,

    [SouvenirQuestion("How many times did the {1} LED flash {2} on {0}?", "Flashing Lights", ThreeColumns6Answers, TranslateFormatArgs = new[] { true, true },
        ExampleFormatArguments = new[] { "top", "cyan", "top", "green", "top", "red", "top", "purple", "top", "orange", "bottom", "cyan", "bottom", "green", "bottom", "red", "bottom", "purple", "bottom", "orange" }, ExampleFormatArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 12)]
    FlashingLightsLEDFrequency,

    [SouvenirQuestion("Which module’s flavor text was shown in {0}?", "Flavor Text", OneColumn4Answers, ExampleAnswers = new[] { "Totally Accurate Minecraft Simulator", "Rock-Paper-Scissors-Lizard-Spock", "The Octadecayotton", "Power Button" })]
    FlavorTextModule,

    [SouvenirQuestion("Which module’s flavor text was shown in the {1} stage of {0}?", "Flavor Text EX", OneColumn4Answers, ExampleAnswers = new[] { "Totally Accurate Minecraft Simulator", "Rock-Paper-Scissors-Lizard-Spock", "The Octadecayotton", "Power Button" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    FlavorTextEXModule,

    [SouvenirQuestion("Which fly was present, but not in the solution in {0}?", "Flyswatting", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    FlyswattingUnpressed,

    [SouvenirQuestion("What was the {1} flashing direction in {0}?", "Follow Me", TwoColumns4Answers, "Up", "Down", "Left", "Right", ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    FollowMeDisplayedPath,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Forest Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    ForestCipherScreen,

    [SouvenirQuestion("What colors were the cylinders during the {1} stage of {0}?", "Forget Any Color", OneColumn4Answers,
        ExampleAnswers = new[] { "Orange, Yellow, Green", "Yellow, Cyan, Purple", "Green, Purple, Orange", "Green, Blue, Purple" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1,
        TranslatableStrings = new[] { "{0}, {1}, {2}", "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "White", "L", "M", "R",
            "the Forget Any Color which used figure {0} in the {1} stage",
            "the Forget Any Color whose cylinders in the {1} stage were {0}" })]
    ForgetAnyColorCylinder,
    [SouvenirQuestion("Which figure was used during the {1} stage of {0}?", "Forget Any Color", ThreeColumns6Answers,
        ExampleAnswers = new[] { "LLLMR", "LMMMR", "LMRRR", "LMMRR", "LLMRR", "LLMMR" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ForgetAnyColorSequence,

    [SouvenirQuestion("What was the {1} displayed digit in the first stage of {0}?", "Forget Everything", ThreeColumns6Answers, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1,
        TranslatableStrings = new[] { "the Forget Everything whose {0} displayed digit in that stage was {1}" })]
    [AnswerGenerator.Integers(0, 9)]
    ForgetEverythingStageOneDisplay,

    [SouvenirQuestion("What number was in the {1} position of the initial puzzle in {0}?", "Forget Me", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 8)]
    ForgetMeInitialState,

    [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", "Forget Me Not", ThreeColumns6Answers, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1,
        TranslatableStrings = new[] { "the Forget Me Not which displayed a {0} in the {1} stage" })]
    [AnswerGenerator.Integers(0, 9)]
    ForgetMeNotDisplayedDigits,

    [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Forget Me Now", ThreeColumns6Answers, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    ForgetMeNowDisplayedDigits,

    [SouvenirQuestion("What was played in the {1} stage of {0}?", "Forget Our Voices", ThreeColumns6Answers, Type = AnswerType.Audio, ForeignAudioID = "forgetOurVoices", AudioSizeMultiplier = 3, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1,
        TranslatableStrings = new[] { "the Forget Our Voices which played a {0} in {1}’s voice in the {2} stage", "Umbra Moruka", "Dicey", "MásQuéÉlite", "Obvious", "1254", "Dbros1000", "Bomberjack", "Danielstigman", "Depresso", "ktane1", "OEGamer", "jTIS", "Krispy", "Grunkle Squeaky", "Arceus", "ScopingLandscape", "Emik", "GhostSalt", "Short_c1rcuit", "Eltrick", "Axodeau", "Asew", "Cooldoom", "Piissii", "CrazyCaleb" })]
    ForgetOurVoicesVoice,

    [SouvenirQuestion("What was the {1} digit of the answer in {0}?", "Forget’s Ultimate Showdown", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    ForgetsUltimateShowdownAnswer,
    [SouvenirQuestion("What was the {1} digit of the initial number in {0}?", "Forget’s Ultimate Showdown", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    ForgetsUltimateShowdownInitial,
    [SouvenirQuestion("What was the {1} digit of the bottom number in {0}?", "Forget’s Ultimate Showdown", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    ForgetsUltimateShowdownBottom,
    [SouvenirQuestion("What was the {1} method used in {0}?", "Forget’s Ultimate Showdown", OneColumn4Answers, "Forget Me Not", "Simon’s Stages", "Forget Me Later", "Forget Infinity", "A>N<D", "Forget Me Now", "Forget Everything", "Forget Us Not", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ForgetsUltimateShowdownMethod,

    [SouvenirQuestion("What number was on the gear during stage {1} of {0}?", "Forget The Colors", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleFormatArgumentGroupSize = 1,
        TranslatableStrings = new[] {
            "the Forget The Colors whose gear number was {0} in stage {1}",
            "the Forget The Colors which had {0} on its large display in stage {1}",
            "the Forget The Colors whose received sine number in stage {1} ended with a {0}",
            "the Forget The Colors whose gear color was {0} in stage {1}",
            "the Forget The Colors whose rule color was {0} in stage {1}" })]
    [AnswerGenerator.Integers(0, 9)]
    ForgetTheColorsGearNumber,
    [SouvenirQuestion("What number was on the large display during stage {1} of {0}?", "Forget The Colors", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 990)]
    ForgetTheColorsLargeDisplay,
    [SouvenirQuestion("What was the last decimal in the sine number received during stage {1} of {0}?", "Forget The Colors", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    ForgetTheColorsSineNumber,
    [SouvenirQuestion("What color was the gear during stage {1} of {0}?", "Forget The Colors", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleFormatArgumentGroupSize = 1)]
    ForgetTheColorsGearColor,
    [SouvenirQuestion("Which edgework-based rule was applied to the sum of nixies and gear during stage {1} of {0}?", "Forget The Colors", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Pink", "Maroon", "White", "Gray", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }, ExampleFormatArgumentGroupSize = 1)]
    ForgetTheColorsRuleColor,

    [SouvenirQuestion("What color was the LED in the {1} stage of {0}?", "Forget This", ThreeColumns6Answers, "Cyan", "Magenta", "Yellow", "Black", "White", "Green", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1,
        TranslatableStrings = new[] { "the Forget This whose LED was {0} in the {1} stage", "the Forget This which displayed {0} in the {1} stage" })]
    ForgetThisColors,
    [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", "Forget This", ThreeColumns6Answers, Type = AnswerType.AsciiMazeFont, // Use this font to make 0 and O distinguishable from each other.
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0-9A-Z")]
    ForgetThisDigits,

    [SouvenirQuestion("Which module name was used for stage {1} in {0}?", "Forget Us Not", OneColumn4Answers, ExampleAnswers = new[] { "Souvenir", "The Button", "The Needlessly Complicated Button", "8", "Eight", "Zero, Zero" },
        ExampleFormatArguments = new[] { "1", "2", "3", "4", "5" }, ExampleFormatArgumentGroupSize = 1, TranslatableStrings = new[] { "the Forget Us Not in which {0} was used for stage {1}" })]
    ForgetUsNotStage,

    [SouvenirQuestion("What was the player token in {0}?", "Free Parking", ThreeColumns6Answers, "Dog", "Wheelbarrow", "Cat", "Iron", "Top Hat", "Car", "Battleship", TranslateAnswers = true)]
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

    [SouvenirQuestion("What color flashed {1} in {0}?", "Fuse Box", TwoColumns4Answers, Type = AnswerType.Sprites, AddThe = true, IsEntireQuestionSprite = true, SpriteFieldName = "FuseBoxColorSprites",
        ExampleFormatArgumentGroupSize = 1, ExampleFormatArguments = new[] { QandA.Ordinal })]
    FuseBoxFlashes,
    [SouvenirQuestion("What arrow was shown {1} in {0}?", "Fuse Box", TwoColumns4Answers, Type = AnswerType.Sprites, AddThe = true, IsEntireQuestionSprite = true, SpriteFieldName = "FuseBoxArrowSprites",
        ExampleFormatArgumentGroupSize = 1, ExampleFormatArguments = new[] { QandA.Ordinal })]
    FuseBoxArrows,

    [SouvenirQuestion("What was your current weapon in {0}?", "Gadgetron Vendor", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "GadgetronVendorIconSprites")]
    GadgetronVendorCurrentWeapon,
    [SouvenirQuestion("What was the weapon up for sale in {0}?", "Gadgetron Vendor", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "GadgetronVendorWeaponSprites")]
    GadgetronVendorWeaponForSale,

    [SouvenirQuestion("Which of these was a color combination that occurred in {0}?", "Game of Life Cruel", TwoColumns4Answers,
        ExampleAnswers = new[] { "Red/Orange", "Orange/Yellow", "Yellow/Green", "Green/Blue" })]
    GameOfLifeCruelColors,

    [SouvenirQuestion("What were the numbers on {0}?", "Gamepad", ThreeColumns6Answers, AddThe = true)]
    [AnswerGenerator.Strings("2*0-9", ":", "2*0-9")]
    GamepadNumbers,

    [SouvenirQuestion("How many puzzle pieces did {0} have?", "Garfield Kart", TwoColumns4Answers, "0", "1", "2", "3")]
    GarfieldKartPuzzleCount,
    [SouvenirQuestion("What was the track in {0}?", "Garfield Kart", OneColumn4Answers,
        ExampleAnswers = new[] { "Play Misty for Me", "Sneak-A-Peak", "Blazing Oasis", "Pastacosi Factory", "Mysterious Temple", "Prohibited Site" })]
    GarfieldKartTrack,

    [SouvenirQuestion("Which faction did {1} claim to be in {0}?", "Garnet Thief", TwoColumns4Answers, "Mafia", "Cartel", "Beggar", "Police", AddThe = true,
        ExampleFormatArguments = new[] { "Jungmoon", "Yeonseung", "Jinho", "Dongmin", "Kyunghoon", "Kyungran", "Yoohyun", "Junseok", "Sangmin", "Yohwan", "Yoonsun", "Hyunmin", "Junghyun" }, ExampleFormatArgumentGroupSize = 1)]
    GarnetThiefClaim,

    [SouvenirQuestion("Where was {1} in {0}?", "Ghost Movement", ThreeColumns6Answers, "A1", "B1", "C1", "D1", "E1", "F1", "G1", "H1", "I1", "J1", "K1", "L1", "O1", "P1", "Q1", "R1", "S1", "T1", "U1", "V1", "W1", "X1", "Y1", "Z1", "A2", "F2", "L2", "O2", "U2", "Z2", "A3", "F3", "L3", "O3", "U3", "Z3", "A4", "F4", "L4", "O4", "U4", "Z4", "A5", "B5", "C5", "D5", "E5", "F5", "G5", "H5", "I5", "J5", "K5", "L5", "M5", "N5", "O5", "P5", "Q5", "R5", "S5", "T5", "U5", "V5", "W5", "X5", "Y5", "Z5", "A6", "F6", "I6", "R6", "U6", "Z6", "A7", "F7", "I7", "R7", "U7", "Z7", "A8", "B8", "C8", "D8", "E8", "F8", "I8", "J8", "K8", "L8", "O8", "P8", "Q8", "R8", "U8", "V8", "W8", "X8", "Y8", "Z8", "F9", "L9", "O9", "U9", "F10", "L10", "O10", "U10", "F11", "I11", "J11", "K11", "L11", "M11", "N11", "O11", "P11", "Q11", "R11", "U11", "F12", "I12", "R12", "U12", "F13", "I13", "R13", "U13", "4514", "A14", "B14", "C14", "D14", "E14", "F14", "G14", "H14", "I14", "R14", "S14", "T14", "U14", "V14", "W14", "X14", "Y14", "Z14", "a14", "F15", "I15", "R15", "U15", "F16", "I16", "R16", "U16", "F17", "I17", "J17", "K17", "L17", "M17", "N17", "O17", "P17", "Q17", "R17", "U17", "F18", "I18", "R18", "U18", "F19", "I19", "R19", "U19", "A20", "B20", "C20", "D20", "E20", "F20", "G20", "H20", "I20", "J20", "K20", "L20", "O20", "P20", "Q20", "R20", "S20", "T20", "U20", "V20", "W20", "X20", "Y20", "Z20", "A21", "F21", "L21", "O21", "U21", "Z21", "A22", "F22", "L22", "O22", "U22", "Z22", "A23", "B23", "C23", "F23", "G23", "H23", "I23", "J23", "K23", "L23", "M23", "N23", "O23", "P23", "Q23", "R23", "S23", "T23", "U23", "X23", "Y23", "Z23", "C24", "F24", "I24", "R24", "U24", "X24", "C25", "F25", "I25", "R25", "U25", "X25", "A26", "B26", "C26", "D26", "E26", "F26", "I26", "J26", "K26", "L26", "O26", "P26", "Q26", "R26", "U26", "V26", "W26", "X26", "Y26", "Z26", "A27", "L27", "O27", "Z27", "A28", "L28", "O28", "Z28", "A29", "B29", "C29", "D29", "E29", "F29", "G29", "H29", "I29", "J29", "K29", "L29", "M29", "N29", "O29", "P29", "Q29", "R29", "S29", "T29", "U29", "V29", "W29", "X29", "Y29", "Z29",
        ExampleFormatArguments = new[] { "Inky", "Blinky", "Pinky", "Clyde", "Pac-Man" }, ExampleFormatArgumentGroupSize = 1)]
    GhostMovementPosition,

    [SouvenirQuestion("What was the language sung in {0}?", "Girlfriend", TwoColumns4Answers, "English", "French", "German", "Italian", "Japanese", "Mandarin", "Portuguese", "Spanish")]
    GirlfriendLanguage,

    [SouvenirQuestion("What was the cycling bit sequence in {0}?", "Glitched Button", OneColumn4Answers, AddThe = true)]
    [AnswerGenerator.Strings(12, "01")]
    GlitchedButtonSequence,

    [SouvenirQuestion("What number was flashed by the {1} LED in {0}?", "Goofy’s Game", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "left", "right", "center" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(0, 9)]
    GoofysGameNumber,

    [SouvenirQuestion("Which key was part of the {1} set in {0}?", "Grand Piano", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-G", "1-7")]
    [AnswerGenerator.Strings("AB", "0")]
    [AnswerGenerator.Strings("C", "8")]
    [AnswerGenerator.Strings("CDFGA", "♯", "1-7")]
    [AnswerGenerator.Strings("A", "♯", "0")]
    GrandPianoKey,
    [SouvenirQuestion("Which key was the fifth set in {0}?", "Grand Piano", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("DEGAB", "♭", "1-7")]
    [AnswerGenerator.Strings("B", "♭", "0")]
    GrandPianoFinalKey,

    [SouvenirQuestion("What was the {1} coordinate on the display in {0}?", "Gray Button", ThreeColumns6Answers, AddThe = true,
        ExampleFormatArguments = new[] { "horizontal", "vertical" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(0, 9)]
    GrayButtonCoordinates,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Gray Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "ASSUME", "EMBRYO", "GAMBIT", "LAMENT", "LEARNT", "NEBULA", "NEEDED", "OBJECT", "PHOTON", "QUARRY" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    GrayCipherScreen,

    [SouvenirQuestion("What was the {1} color in {0}?", "Great Void", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "Cyan", "White", AddThe = true, TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    GreatVoidColor,
    [SouvenirQuestion("What was the {1} digit in {0}?", "Great Void", ThreeColumns6Answers, AddThe = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    GreatVoidDigit,

    [SouvenirQuestion("What was the last number on the display on {0}?", "Green Arrows", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99, "00")]
    GreenArrowsLastScreen,

    [SouvenirQuestion("What was the word submitted in {0}?", "Green Button", ThreeColumns6Answers, AddThe = true, ExampleAnswers = new[] { "model", "vigor", "pedal", "relic", "lemon", "spoke", "brick", "berry", "equal", "loopy", "trash", "learn", "amuse", "valve", "bench", "igloo", "maybe", "fluid", "truck", "torch" })]
    GreenButtonWord,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Green Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "BARBER", "ELIXIR", "HARDLY", "JACKED", "LAMEST", "OCTAVE", "UMPIRE", "UNVEIL", "WAFFLE", "ZONING" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    GreenCipherScreen,

    [SouvenirQuestion("What was the starting location in {0}?", "Gridlock", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    GridLockStartingLocation,
    [SouvenirQuestion("What was the ending location in {0}?", "Gridlock", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    GridLockEndingLocation,
    [SouvenirQuestion("What was the starting color in {0}?", "Gridlock", TwoColumns4Answers, "Green", "Yellow", "Red", "Blue", TranslateAnswers = true)]
    GridLockStartingColor,

    [SouvenirQuestion("What was the first item shown in {0}?", "Grocery Store", TwoColumns4Answers, ExampleAnswers = new[] { "Cheese", "Coffee", "Flour", "Glass Cleaner", "Pepper", "Salt", "Soup", "Steak", "Toilet Paper", "Turkey" })]
    GroceryStoreFirstItem,

    [SouvenirQuestion("What was the gryphon’s name in {0}?", "Gryphons", ThreeColumns6Answers, "Gabe", "Gabriel", "Gad", "Gael", "Gage", "Gaia", "Galena", "Galina", "Gallo", "Gallagher", "Ganymede", "Ganzorig", "Garen", "Gareth", "Garland", "Garnett", "Garret", "Garrick", "Gary", "Gaspar", "Gaston", "Gauthier", "Gavin", "Gaz", "Geena", "Geff", "Geffrey", "Gela", "Geltrude", "Gene", "Geneva", "Genevieve", "Geno", "Gentius", "Geoff", "George", "Georgio", "Georgius", "Gerald", "Geraldo", "Gerda", "Gerel", "Gergana", "Gerhardt", "Gerhart", "Gerry", "Gertrude", "Gervais", "Gervaise", "Ghada", "Ghadir", "Ghassan", "Ghjulia", "Gia", "Giada", "Giampaolo", "Giampiero", "Giancarlo", "Giana", "Gianna", "Gideon", "Gidon", "Gilbert", "Gilberta", "Gino", "Giorgio", "Giovanni", "Giove", "Girish", "Girisha", "Gisela", "Giselle", "Gittel", "Gizella", "Gjorgji", "Gladys", "Glauco", "Glaukos", "Glen", "Glenn", "Godfrey", "Godfried", "Gojko", "Gol", "Golda", "Gona", "Gonzalo", "Gordie", "Gordy", "Goretti", "Gosia", "Gosse", "Gotzon", "Gotzone", "Gowri", "Gozzo", "Grace", "Gracia", "Griffith", "Gwynnyth")]
    GryphonsName,
    [SouvenirQuestion("What was the gryphon’s age in {0}?", "Gryphons", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(23, 34)]
    GryphonsAge,

    [SouvenirQuestion("Did {1} flash “YES” in {0}?", "Guess Who?", TwoColumns2Answers, "Yes", "No")]
    GuessWhoColors,

    [SouvenirQuestion("What color was the {1} LED in {0}?", "Gyromaze", TwoColumns4Answers, "Red", "Blue", "Green", "Yellow",
        ExampleFormatArguments = new[] { "top", "bottom" }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true, TranslateFormatArgs = new[] { true })]
    GyromazeLEDColor,

    [SouvenirQuestion("What was the transmitted letter in {0}?", "h", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z")]
    HLetter,

    [SouvenirQuestion("Which fruit were there five of in {0}?", "Halli Galli", TwoColumns4Answers, "Strawberries", "Melons", "Lemons", "Raspberries", "Bananas", TranslateAnswers = true)]
    HalliGalliFruit,
    [SouvenirQuestion("What were the relevant counts in {0}?", "Halli Galli", TwoColumns4Answers, "5", "1 4", "2 3", "1 1 3", "1 2 2")]
    HalliGalliCounts,

    [SouvenirQuestion("What was the given number in {0}?", "Hereditary Base Notation", TwoColumns4Answers, ExampleAnswers = new[] { "12", "33", "46", "112", "356" })]
    HereditaryBaseNotationInitialNumber,

    [SouvenirQuestion("What label was printed on {0}?", "Hexabutton", ThreeColumns6Answers, "Jump", "Boom", "Claim", "Button", "Hold", "Blue", AddThe = true)]
    HexabuttonLabel,

    [SouvenirQuestion("What was the color of the pawn in {0}?", "Hexamaze", ThreeColumns6Answers, "Red", "Yellow", "Green", "Cyan", "Blue", "Pink", TranslateAnswers = true)]
    HexamazePawnColor,

    [SouvenirQuestion("What was the {1} shape for the {2} display in {0}?", "hexOrbits", TwoColumns4Answers, "Square", "Pentagon", "Hexagon", "Heptagon",
        ExampleFormatArguments = new[] { "fast", QandA.Ordinal, "slow", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    HexOrbitsShape,

    [SouvenirQuestion("What were the deciphered letters in {0}?", "hexOS", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("2* A-Z")]
    HexOSCipher,
    [SouvenirQuestion("What was the deciphered phrase in {0}?", "hexOS", ThreeColumns6Answers, ExampleAnswers = new[] { "a maze", "someda", "but i ", "they h", "shorn o", "more s", "if onl", "grew b" })]
    HexOSOctCipher,
    [SouvenirQuestion("What was the {1} 3-digit number cycled by the screen in {0}?", "hexOS", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 999, "000")]
    HexOSScreen,
    [SouvenirQuestion("What were the rhythm values in {0}?", "hexOS", ThreeColumns6Answers, ExampleAnswers = new[] { "0001", "0012", "0123", "1230", "2300", "3000" })]
    HexOSSum,

    [SouvenirQuestion("What time was shown when the clock struck {1} on {0}?", "Hickory Dickory Dock", ThreeColumns6Answers, ExampleFormatArguments = new[] { "1:00", "2:00", "3:00", "4:00", "5:00", "6:00", "7:00", "8:00", "9:00", "10:00", "11:00", "12:00" }, ExampleFormatArgumentGroupSize = 1,
        TranslatableStrings = new[] { "the Hickory Dickory Dock which showed {0}:{1:00} when it struck {2}" })]
    [AnswerGenerator.HickoryDickoryDock]
    HickoryDickoryDockTime,

    [SouvenirQuestion("What was the color of the main LED in {0}?", "Hidden Colors", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Magenta", "White", TranslateAnswers = true)]
    HiddenColorsLED,

    [SouvenirQuestion("What was displayed on {0}?", "Hidden Value", TwoColumns4Answers, AddThe = true, ExampleAnswers = new[] { "Red 1", "Green 2", "White 3", "Yellow 4", "Magenta 5", "Cyan 6", "Purple 7" },
        TranslatableStrings = new[] { "Red", "Green", "White", "Yellow", "Magenta", "Cyan", "Purple", "{0} {1}" })]
    HiddenValueDisplay,

    [SouvenirQuestion("What was the position of the player in {0}?", "High Score", TwoColumns4Answers, "1st", "2nd", "3rd", "4th", "5th", AddThe = true)]
    HighScorePosition,
    [SouvenirQuestion("What was the score of the player in {0}?", "High Score", TwoColumns4Answers, AddThe = true)]
    [AnswerGenerator.Integers(1750, 999990, 10)]
    HighScoreScore,

    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Hill Cycle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleFiveSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    HillCycleDialDirections,
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", "Hill Cycle", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    HillCycleDialLabels,

    [SouvenirQuestion("Which of these hinges was initially {1} {0}?", "Hinges", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "HingesSprites",
        ExampleFormatArguments = new[] { "present on", "absent from" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    HingesInitialHinges,

    [SouvenirQuestion("Which House was {1} solved\u00a0for in {0}?", "Hogwarts", TwoColumns4Answers, "Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "Binary Puzzle", "Zoni", "Rock-Paper- Scissors-L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards" }, ExampleFormatArgumentGroupSize = 1)]
    HogwartsHouse,
    [SouvenirQuestion("Which module was solved\u00a0for {1} in {0}?", "Hogwarts", OneColumn4Answers, ExampleAnswers = new[] { "Binary Puzzle", "Zoni", "Rock-Paper-Scissors-L.-Sp.", "Modules Against Humanity", "Monsplode Trading Cards" },
        ExampleFormatArguments = new[] { "Gryffindor", "Hufflepuff", "Slytherin", "Ravenclaw" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    HogwartsModule,

    [SouvenirQuestion("What was the name of the {1} shadow shown in {0}?", "Hold Ups", OneColumn4Answers, "Mandrake", "Silky", "Koropokguru", "Nue", "Jack Frost", "Leanan Sidhe", "Hua Po", "Orthrus", "Lamia", "Bicorn", "Kelpie", "Apsaras", "Makami", "Nekomata", "Sandman", "Naga", "Agathion", "Berith", "Mokoi", "Inugami", "High Pixie", "Yaksini", "Anzu", "Take-Minakata", "Thoth", "Isis", "Incubis", "Onmoraki", "Koppa-Tengu", "Orobas", "Rakshasa", "Pixie", "Angel", "Jack O' Lantern", "Succubus", "Andras",
    ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    HoldUpsShadows,

    [SouvenirQuestion("What was the {1} displayed phrase in {0}?", "Homophones", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1,
        ExampleAnswers = new[] { "i", "C", "L", "1", "sees", "leemer", "aye-aye", "One" })]
    HomophonesDisplayedPhrases,

    [SouvenirQuestion("In what position was the button pressed on the {1} stage of {0}?", "Horrible Memory", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    HorribleMemoryPositions,
    [SouvenirQuestion("What was the label of the button pressed on the {1} stage of {0}?", "Horrible Memory", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    HorribleMemoryLabels,
    [SouvenirQuestion("What color was the button pressed on the {1} stage of {0}?", "Horrible Memory", ThreeColumns6Answers, "blue", "green", "red", "orange", "purple", "pink", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    HorribleMemoryColors,

    [SouvenirQuestion("Which was a descriptor shown in {1} in {0}?", "Human Resources", TwoColumns4Answers, "Intellectual", "Deviser", "Confidant", "Helper", "Auditor", "Innovator", "Defender", "Chameleon", "Director", "Designer", "Educator", "Advocate", "Manager", "Showman", "Contributor", "Entertainer",
        ExampleFormatArguments = new[] { "red", "green" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    HumanResourcesDescriptors,
    [SouvenirQuestion("Who was {1} in {0}?", "Human Resources", ThreeColumns6Answers, "Rebecca", "Damian", "Jean", "Mike", "River", "Samuel", "Yoshi", "Caleb", "Ashley", "Tim", "Eliott", "Ursula", "Silas", "Noah", "Quinn", "Dylan",
        ExampleFormatArguments = new[] { "fired", "hired" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    HumanResourcesHiredFired,

    [SouvenirQuestion("Which of the first three stages of {0} had the {1} symbol {2}?", "Hunting", TwoColumns4Answers, "none", "first", "second", "first two", "third", "first & third", "second & third", "all three", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "column", QandA.Ordinal, "row", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    HuntingColumnsRows,

    [SouvenirQuestion("What was the {1} rotation in {0}?", "Hypercube", ThreeColumns6Answers, "XY", "YX", "XZ", "ZX", "XW", "WX", "YZ", "ZY", "YW", "WY", "ZW", "WZ", AddThe = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    HypercubeRotations,

    [SouvenirQuestion("What was the rotation for the {1} stage in {0}?", "HyperForget", ThreeColumns6Answers, "XY", "XZ", "XW", "YX", "YZ", "YW", "ZX", "ZY", "ZW", "WX", "WY", "WZ",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslatableStrings = new[] { "the HyperForget whose rotation in the {1} stage was {0}" })]
    HyperForgetRotations,

    [SouvenirQuestion("What was the {1} character of the hyperlink in {0}?", "Hyperlink", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "_", "-", AddThe = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    HyperlinkCharacters,
    [SouvenirQuestion("Which module was referenced on {0}?", "Hyperlink", OneColumn4Answers, "3D Maze", "Adjacent Letters", "Adventure Game", "Alphabet", "Anagrams", "Answering Questions", "Astrology", "Backgrounds", "Battleship", "Big Circle", "Bitmaps", "Blind Alley", "Blind Maze", "Braille", "Broken Buttons", "Burglar Alarm", "Button Sequence", "Caesar Cipher", "Capacitor Discharge", "Cheap Checkout", "Chess", "Chord Qualities", "Color Flash", "Colored Squares", "Colored Switches", "Combination Lock", "Complicated Buttons", "Complicated Wires", "Connection Check", "Cooking", "Coordinates", "Crazy Talk", "Creation", "Cryptography", "Double-Oh", "Emoji Math", "English Test", "European Travel", "Faulty Backgrounds", "Filibuster", "Follow The Leader", "Foreign Exchange Rates", "Forget Me Not", "Friendship", "Game Of Life Cruel", "Game Of Life Simple", "Hexamaze", "HTTP Response", "Hunting", "Ice Cream", "Keypad", "Knob", "Laundry", "Letter Keys", "Light Cycle", "Lights Out", "Listening", "Logic", "Math", "Maze", "Memory", "Microcontroller", "Module Against Humanity", "Monsplode Trading Cards", "Monsplode, Fight!", "Morse Code", "Morsematics", "Mortal Kombat", "Motion Sense", "Mouse In The Maze", "Murder", "Mystic Square", "Neutralization", "Number Pad", "Orientation Cube", "Password", "Perspective Pegs", "Piano Keys", "Plumbing", "Probing", "Resistors", "Rock-Paper-Scissors-Lizard-Spock", "Rotary Phone", "Round Keypad", "Safety Safe", "Sea Shells", "Semaphore", "Shape Shift", "Silly Slots", "Simon Says", "Simon Screams", "Simon States", "Skewed Slots", "Souvenir", "Square Button", "Switches", "Symbolic Coordinates", "Symbolic Password", "Tetris", "Text Field", "The Bulb", "The Button", "The Clock", "The Gamepad", "The iPhone", "The Moon", "The Stopwatch", "The Sun", "The Swan", "Third Base", "Tic-Tac-Toe", "Turn The Key", "Turn The Keys", "Two Bits", "Venting Gas", "Who’s On First", "Who’s That Monsplode", "Wire Placement", "Wire Sequence", "Wires", "Word Scramble", "Word Search", "Zoo", AddThe = true)]
    HyperlinkAnswer,

    [SouvenirQuestion("Which one of these flavours {1} to the {2} customer in {0}?", "Ice Cream", OneColumn4Answers, "Tutti Frutti", "Rocky Road", "Raspberry Ripple", "Double Chocolate", "Double Strawberry", "Cookies & Cream", "Neapolitan", "Mint Chocolate Chip", "The Classic", "Vanilla", TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "was on offer, but not sold,", QandA.Ordinal, "was not on offer", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    IceCreamFlavour,
    [SouvenirQuestion("Who was the {1} customer in {0}?", "Ice Cream", ThreeColumns6Answers, "Mike", "Tim", "Tom", "Dave", "Adam", "Cheryl", "Sean", "Ashley", "Jessica", "Taylor", "Simon", "Sally", "Jade", "Sam", "Gary", "Victor", "George", "Jacob", "Pat", "Bob",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    IceCreamCustomer,

    [SouvenirQuestion("What was the {1} shape used in {0}?", "Identification Crisis", TwoColumns4Answers, "Circle", "Square", "Diamond", "Heart", "Star", "Triangle", "Pentagon", "Hexagon",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    IdentificationCrisisShape,
    [SouvenirQuestion("What was the {1} identification module used in {0}?", "Identification Crisis", OneColumn4Answers, "Morse Identification", "Boozleglyph Identification", "Plant Identification", "Pickup Identification", "Emotiguy Identification", "Ars Goetia Identification", "Mii Identification", "Customer identification", "Spongebob Birthday Identification", "VTuber Identification", TranslateAnswers = true,
       ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    IdentificationCrisisDataset,

    [SouvenirQuestion("Which hair color {1} listed in {0}?", "Identity Parade", TwoColumns4Answers, "Black", "Blonde", "Brown", "Grey", "Red", "White",
        ExampleFormatArguments = new[] { "was", "was not" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    IdentityParadeHairColors,
    [SouvenirQuestion("Which build {1} listed in {0}?", "Identity Parade", TwoColumns4Answers, "Fat", "Hunched", "Muscular", "Short", "Slim", "Tall",
        ExampleFormatArguments = new[] { "was", "was not" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    IdentityParadeBuilds,
    [SouvenirQuestion("Which attire {1} listed in {0}?", "Identity Parade", TwoColumns4Answers, "Blazer", "Hoodie", "Jumper", "Suit", "T-shirt", "Tank top",
        ExampleFormatArguments = new[] { "was", "was not" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    IdentityParadeAttires,

    [SouvenirQuestion("Which module was {0} pretending to be?", "Impostor", OneColumn4Answers, ExampleAnswers = new[] { "Ice Cream", "Microcontroller", "Sea Shells", "Combination Lock" }, AddThe = true)]
    ImpostorDisguise,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Indigo Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "BEAVER", "INDENT", "LONELY", "PILLAR", "REFUGE", "RIPPED", "STOLEN", "TUMBLE", "WHIMSY", "WYVERN" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
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
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    InterpunctDisplay,

    [SouvenirQuestion("What sound played in {0}?", "IPA", TwoColumns4Answers,
        Type = AnswerType.Audio, ForeignAudioID = "ipa", AudioSizeMultiplier = 4)]
    IpaSound,

    [SouvenirQuestion("What was the {1} PIN digit in {0}?", "iPhone", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, AddThe = true)]
    [AnswerGenerator.Integers(0, 9)]
    iPhoneDigits,

    [SouvenirQuestion("Which symbol was on the first correctly pulled block in {0}?", "Jenga", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "JengaSprites")]
    JengaFirstBlock,

    [SouvenirQuestion("What number was wheel {1} in {0}?", "Jewel Vault", TwoColumns4Answers,
        ExampleFormatArguments = new[] { "A", "B", "C", "D" }, ExampleFormatArgumentGroupSize = 1, AddThe = true)]
    [AnswerGenerator.Integers(1, 4)]
    JewelVaultWheels,

    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Jumble Cycle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    JumbleCycleDialDirections,
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", "Jumble Cycle", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    JumbleCycleDialLabels,

    [SouvenirQuestion("What was the color of this square in {0}?", "Juxtacolored Squares", ThreeColumns6Answers, "Red", "Blue", "Yellow", "Green", "Magenta", "Orange", "Cyan", "Purple", "Chestnut", "Brown", "Mauve", "Azure", "Jade", "Forest", "Gray", "Black", TranslateAnswers = true, UsesQuestionSprite = true)]
    JuxtacoloredSquaresColorsByPosition,
    [SouvenirQuestion("Which square was {1} in {0}?", "Juxtacolored Squares", ThreeColumns6Answers, Type = AnswerType.Sprites,
        ExampleFormatArguments = new[] { "red", "blue", "yellow", "green", "magenta", "orange", "cyan", "purple", "chestnut", "brown", "mauve", "azure", "jade", "forest", "gray", "black" },
        ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Grid(4, 4)]
    JuxtacoloredSquaresPositionsByColor,

    [SouvenirQuestion("What was the displayed word in the {1} stage of {0}?", "Kanji", TwoColumns4Answers, Type = AnswerType.JapaneseFont, ExampleAnswers = new[] { "ばくはつ", "でんき", "でんしゃ", "でんわ" }, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    KanjiDisplayedWords,

    [SouvenirQuestion("What was a food item displayed in {0}?", "Kanye Encounter", TwoColumns4Answers,
        "Onion", "Corn", "big MIOLK", "Yam", "Corn Cube", "Egg", "Eggchips", "hamger", "Tyler the Creator", "Onionade", "Soup", "jeb", AddThe = true)]
    KanyeEncounterFoods,

    [SouvenirQuestion("What was the {1} phrase in {0}?", "KayMazey Talk", OneColumn4Answers, "Big Khungus", "Kaleb", "Kielo", "Anonymous K-oala", "K in morse", "K in binary", "Kay", "Kayyy", "Invisible K", "Ktane", "Kayyyyyyyy", "Thousand", "Kelly Green", "O.K.", "Passive aggressive K", "Khan as in Genghis Khan", "Krazy Kalk Kaleb", "Kelvin", "Potassium", "1011", "OK Ayy Why", "Kilogram", "Kœ", "OKAI", "1,000", "Kaio", "Okae", "Oscar Kil", "Kaye", "Passive aggressive Kilo", "Cae", "K.I.A", "Keylow", "Kilo in Semaphore", "K", "-.-", "Cai", "Kae in 1’s and 0’s", "Key", "Kayyyy", "Kylo", "Kayyyyy", "Oh K", "κ", "Kappaκ", "Oh Kay", "Kombu Green", "K.", "KU Crimson", "Kayyyyyy", "K with y’s", "Alright", "Khan Academy", "Kay in binary", "Kayyyyyyy", "Oh Kae", "OK", "Cay", "Kae", "Kobe, Sienna", "Kum", "OKay", "Kpurple Arrows", "Kilo", "Southwest by North", "OK Simpson", "Keylo", "Kayy", "Keelo", "Ktay Krazy Ktay Kool", "K with a period", "K in Semaphore", "Okay", "Kay Tane", "Southwest dot North", "Kaaba", "Kappa, as in the Greek Letter", "Ketane", "Kapa", "Keytane", "Kæ", "Ketamine", "Khan", "Kilow",
        ExampleFormatArguments = new[] { "starting", "ending" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    KayMazeyTalkPhrase,

    [SouvenirQuestion("Which number was displayed on the {1} button, but not part of the answer on {0}?", "Keypad Combinations", ThreeColumns6Answers,
       ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    KeypadCombinationWrongNumbers,

    [SouvenirQuestion("What was the position of the LED in {0}?", "Keypad Magnified", TwoColumns4Answers, "Top-left", "Top-right", "Bottom-left", "Bottom-right", TranslateAnswers = true)]
    KeypadMagnifiedLED,

    [SouvenirQuestion("Which of these cells was yellow in {0}?", "Keypad Maze", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(6, 6)]
    KeypadMazeYellow,

    [SouvenirQuestion("What was this key’s label on the {1} panel in {0}?", "Keypad Sequence", ThreeColumns6Answers, Type = AnswerType.Sprites, UsesQuestionSprite = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    KeypadSequenceLabels,

    [SouvenirQuestion("What were the first four letters on the display in {0}?", "Keywords", ThreeColumns6Answers, ExampleAnswers = new[] { "abvo", "pola", "drea", "buew", "utre", "oidy" })]
    KeywordsDisplayedKey,

    [SouvenirQuestion("What was the first module to set off {0}?", "Klaxon", TwoColumns4Answers, AddThe = true,
        ExampleAnswers = new[] { "3D Maze", "Adjacent Letters", "Adventure Game", "Alphabet", "Anagrams", "Astrology", "Battleship", "Binary LEDs", "Bitmaps", "Bitwise Operations", "Blind Alley", "Boolean Venn Diagram", "Broken Buttons", "The Bulb", "Caesar Cipher", "Cheap Checkout", "Chess", "Chord Qualities", "The Clock", "Color Math", "Colored Squares", "Colour Flash", "Combination Lock", "Complicated Buttons", "Connection Check", "Coordinates", "Crazy Talk", "Creation", "Cryptography", "Double-Oh", "Emoji Math", "English Test", "Fast Math", "FizzBuzz", "Follow the Leader", "Foreign Exchange Rates", "Friendship", "The Gamepad", "Hexamaze", "Ice Cream", "Laundry", "LED Encryption", "Letter Keys", "Light Cycle", "Listening", "Logic", "Microcontroller", "Minesweeper", "Modules Against Humanity", "Monsplode, Fight!", "Morsematics", "Mouse In The Maze", "Murder", "Mystic Square", "Neutralization", "Number Pad", "Only Connect", "Orientation Cube", "Perspective Pegs", "Piano Keys", "Plumbing", "Point of Order", "Probing", "Resistors", "Rhythms", "Rock-Paper-Scissors-Lizard-Spock", "Round Keypad", "Rubik's Cube", "Safety Safe", "The Screw", "Sea Shells", "Semaphore", "Shape Shift", "Silly Slots", "Simon Screams", "Simon States", "Skewed Slots", "Square Button", "Switches", "Symbolic Password", "Text Field", "Third Base", "Tic Tac Toe", "Two Bits", "Web Design", "Wire Placement", "Word Scramble", "Word Search", "Yahtzee", "Zoo" })]
    KlaxonFirstModule,

    [SouvenirQuestion("Which way was the arrow pointing in {0}?", "Know Your Way", TwoColumns4Answers, "Up", "Down", "Left", "Right", TranslateAnswers = true)]
    KnowYourWayArrow,
    [SouvenirQuestion("Which LED was green in {0}?", "Know Your Way", TwoColumns4Answers, "Top", "Bottom", "Right", "Left", TranslateAnswers = true)]
    KnowYourWayLed,

    [SouvenirQuestion("What color was the {1} button’s LED in {0}?", "Kooky Keypad", OneColumn4Answers, "Crimson", "Red", "Coral", "Orange", "Lemon Chiffon", "Medium Spring Green", "Deep Sea Green", "Cadet Blue", "Slate Blue", "Dark Magenta", "Unlit",
        ExampleFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true }, TranslateAnswers = true)]
    KookyKeypadColor,

    [SouvenirQuestion("Which square was {1} in {0}?", "Kudosudoku", ThreeColumns6Answers, Type = AnswerType.Sprites, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "pre-filled", "not pre-filled" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(4, 4)]
    KudosudokuPrefilled,

    [SouvenirQuestion("Which particles were present for the {1} stage of {0}?", "Kugelblitz", ThreeColumns6Answers, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, ExampleAnswers = new[] { "None", "RGB", "RYV", "ROYGBIV", "YIV", "O" },
        TranslatableStrings = new[] { "the {0} Kugelblitz", "black", "red", "orange", "yellow", "green", "blue", "indigo", "violet", "the Kugelblitz linked with no other Kugelblitzes", "the {0} Kugelblitz linked with one other Kugelblitz", "the {0} Kugelblitz linked with two other Kugelblitzes", "the {0} Kugelblitz linked with three other Kugelblitzes", "the {0} Kugelblitz linked with four other Kugelblitzes", "the {0} Kugelblitz linked with five other Kugelblitzes", "the {0} Kugelblitz linked with six other Kugelblitzes", "the {0} Kugelblitz linked with seven other Kugelblitzes", "R", "O", "Y", "G", "B", "I", "V", "{0}{1}{2}{3}{4}{5}{6}", "None" })]
    KugelblitzBlackOrangeYellowIndigoViolet,
    [SouvenirQuestion("What were the particles’ values for the {1} stage of {0}?", "Kugelblitz", OneColumn4Answers, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, ExampleAnswers = new[] { "R=0, O=0, Y=0, G=0, B=0, I=0, V=0", "R=1, O=0, Y=2, G=3, B=4, I=1, V=6", "R=1, O=0, Y=1, G=1, B=1, I=1, V=0", "R=6, O=5, Y=2, G=4, B=3, I=1, V=2" },
        TranslatableStrings = new[] { "R={0}, O={1}, Y={2}, G={3}, B={4}, I={5}, V={6}" })]
    KugelblitzRedGreenBlue,

    [SouvenirQuestion("What was Kuro’s mood in {0}?", "Kuro", TwoColumns4Answers, "Angry", "Happy", "Neutral", "Curious", "Devious")]
    KuroMood,

    [SouvenirQuestion("Where was one of the portals in layer {1} in {0}?", "Labyrinth", ThreeColumns6Answers, AddThe = true, Type = AnswerType.Sprites, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "1 (Red)", "2 (Orange)", "3 (Yellow)", "4 (Green)", "5 (Blue)" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(6, 7)]
    LabyrinthPortalLocations,
    [SouvenirQuestion("In which layer was this portal in {0}?", "Labyrinth", TwoColumns4Answers, "1 (Red)", "2 (Orange)", "3 (Yellow)", "4 (Green)", "5 (Blue)", TranslateAnswers = true, AddThe = true,
        UsesQuestionSprite = true)]
    LabyrinthPortalStage,

    [SouvenirQuestion("Which light was on in {0}?", "Ladder Lottery", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "LadderLotterySprites")]
    LadderLotteryLightOn,

    [SouvenirQuestion("Which color was present on the second ladder in {0}?", "Ladders", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Cyan", "Purple", "Gray", TranslateAnswers = true)]
    LaddersStage2Colors,
    [SouvenirQuestion("What color was missing on the third ladder in {0}?", "Ladders", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Cyan", "Purple", "Gray", TranslateAnswers = true)]
    LaddersStage3Missing,

    [SouvenirQuestion("Which of these squares was initially {1} in {0}?", "Langton’s Anteater", ThreeColumns6Answers, Type = AnswerType.Sprites,
        ExampleFormatArguments = new[] { "black", "white" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Grid(5, 5)]
    LangtonsAnteaterInitialState,

    [SouvenirQuestion("What was the number on the {1} hatch on {0}?", "Lasers", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(1, 9)]
    LasersHatches,

    [SouvenirQuestion("What was the correct letter you pressed in the {1} stage of {0}?", "LED Encryption", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    LEDEncryptionPressedLetters,

    [SouvenirQuestion("How many LEDs were unlit in {0}?", "LED Grid", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    LEDGridNumBlack,

    [SouvenirQuestion("What color was {1} in {0}?", "LED Math", TwoColumns4Answers, "Red", "Blue", "Yellow", "Green", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "LED A", "LED B", "the operator LED" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    LEDMathLights,

    [SouvenirQuestion("What was the initial color of the changed LED in {0}?", "LEDs", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Black", "White", TranslateAnswers = true)]
    LEDsOriginalColor,

    [SouvenirQuestion("What were the dimensions of the {1} piece in {0}?", "LEGOs", ThreeColumns6Answers, "2×2", "3×1", "3×2", "4×1", "4×2", TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue", "cyan", "magenta", "yellow" }, ExampleFormatArgumentGroupSize = 1)]
    LEGOsPieceDimensions,

    [SouvenirQuestion("What was the letter on the {1} display in {0}?", "Letter Math", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "left", "right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Strings("A-Z")]
    LetterMathDisplay,

    [SouvenirQuestion("What was the color of the {1} bulb in {0}?", "Light Bulbs", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Cyan", "Magenta", TranslateAnswers = true, ExampleFormatArguments = new[] { "left", "right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    LightBulbsColors,

    [SouvenirQuestion("What was the {1} function in {0}?", "Linq", ThreeColumns6Answers, "First", "Last", "Min", "Max", "Distinct", "Skip", "SkipLast", "Take", "TakeLast", "ElementAt", "Except", "Intersect", "Concat", "Append", "Prepend",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslatableStrings = new[] { "the Linq whose {0} function was {1}" })]
    LinqFunction,

    [SouvenirQuestion("Which year was displayed on {0}?", "Lion’s Share", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16")]
    LionsShareYear,
    [SouvenirQuestion("Which lion was present but removed in {0}?", "Lion’s Share", TwoColumns4Answers, ExampleAnswers = new[] { "Taka", "Mufasa", "Uru", "Ahadi", "Zama", "Mohatu", "Kion", "Kiara", "Kopa", "Kovu", "Vitani", "Nuka", "Mheetu", "Zira", "Nala", "Simba", "Sarabi", "Sarafina" })]
    LionsShareRemovedLions,

    [SouvenirQuestion("What clip was played in {0}?", "Listening", ThreeColumns6Answers, AudioFieldName = "ListeningAudio", Type = AnswerType.Audio)]
    ListeningSound,

    [SouvenirQuestion("Which letter was in this position in {0}?", "Literal Maze", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    LiteralMazeLetter,

    [SouvenirQuestion("What was the color of the {1} button in the {2} stage of {0}?", "Logical Buttons", TwoColumns4Answers, "Red", "Blue", "Green", "Yellow", "Purple", "White", "Orange", "Cyan", "Grey", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "top", QandA.Ordinal, "bottom-left", QandA.Ordinal, "bottom-right", QandA.Ordinal, }, ExampleFormatArgumentGroupSize = 2)]
    LogicalButtonsColor,
    [SouvenirQuestion("What was the label on the {1} button in the {2} stage of {0}?", "Logical Buttons", TwoColumns4Answers, "Logic", "Color", "Label", "Button", "Wrong", "Boom", "No", "Wait", "Hmmm", TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "top", QandA.Ordinal, "bottom-left", QandA.Ordinal, "bottom-right", QandA.Ordinal, }, ExampleFormatArgumentGroupSize = 2)]
    LogicalButtonsLabel,
    [SouvenirQuestion("What was the final operator in the {1} stage of {0}?", "Logical Buttons", ThreeColumns6Answers, "AND", "OR", "XOR", "NAND", "NOR", "XNOR",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    LogicalButtonsOperator,

    [SouvenirQuestion("What was {1} in {0}?", "Logic Gates", ThreeColumns6Answers, "AND", "OR", "XOR", "NAND", "NOR", "XNOR", TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "gate A", "gate B", "gate C", "gate D", "gate E", "gate F", "gate G", "the duplicated gate" }, ExampleFormatArgumentGroupSize = 1)]
    LogicGatesGates,

    [SouvenirQuestion("What was the {1} letter on the button in {0}?", "Lombax Cubes", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-Z")]
    LombaxCubesLetters,

    [SouvenirQuestion("Where did the {1} journey on {0} {2}?", "London Underground", OneColumn4Answers, AddThe = true, ExampleAnswers = new[] { "Great Portland Street", "High Street Kensington", "King's Cross St. Pancras", "Mornington Crescent", "Shepherd's Bush Market", "Tottenham Court Road", "Walthamstow Central", "White City/Wood Lane" }, TranslateFormatArgs = new[] { false, true },
        ExampleFormatArguments = new[] { QandA.Ordinal, "depart from", QandA.Ordinal, "arrive to" }, ExampleFormatArgumentGroupSize = 2)]
    LondonUndergroundStations,

    [SouvenirQuestion("What was the word on the top display on {0}?", "Long Words", ThreeColumns6Answers, ExampleAnswers = new[] { "ABOARD", "ABRUPT", "SAFEST", "LAMBDA", "NARROW", "ECHOES", "VALVES", "YONDER", "ZIGGED", "UNBIND" })]
    LongWordsWord,

    [SouvenirQuestion("What was on the display in the {1} stage of {0}?", "Mad Memory", ThreeColumns6Answers, "1", "2", "3", "4", "01", "02", "03", "04", "ONE", "TWO", "THREE", "FOUR", "WON", "TOO", "TREE", "FOR", ExampleFormatArguments = new[] { "first", "second", "third", "4th" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    MadMemoryDisplays,

    [SouvenirQuestion("Who was a player, but not the Godfather, in {0}?", "Mafia", ThreeColumns6Answers, "Rob", "Tim", "Mary", "Briane", "Hunter", "Macy", "John", "Will", "Lacy", "Claire", "Kenny", "Rick", "Walter", "Bonnie", "Luke", "Bill", "Sarah", "Larry", "Kate", "Stacy", "Diane", "Mac", "Jim", "Clyde", "Tommy", "Lenny", "Molly", "Benny", "Phil", "Bob", "Gary", "Ted", "Kim", "Nate", "Cher", "Ron", "Thomas", "Sam", "Duke", "Jack", "Ed", "Ronny", "Terry", "Claira", "Nick", "Cob", "Ash", "Don", "Jerry", "Simon")]
    MafiaPlayers,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Magenta Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    MagentaCipherScreen,

    [SouvenirQuestion("Which tile was part of the {1} matched pair in {0}?", "Mahjong", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "MahjongSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    MahjongMatches,
    [SouvenirQuestion("Which tile was shown in the bottom-left of {0}?", "Mahjong", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "MahjongSprites")]
    MahjongCountingTile,

    [SouvenirQuestion("Which color did the bubble not display in {0}?", "Main Page", TwoColumns4Answers, "Blue", "Green", "Red", "Yellow", TranslateAnswers = true)]
    MainPageBubbleColors,
    [SouvenirQuestion("Which main page did the {1} button’s effect come from in {0}?", "Main Page", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "toons", "games", "characters", "downloads", "store", "email" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(1, 26)]
    MainPageButtonEffectOrigin,
    [SouvenirQuestion("Which of the following messages did the bubble {1} in {0}?", "Main Page", OneColumn4Answers, "play a game", "latest toon", "latest merch", "new strong bad email", "new sbemail a comin", "email soon", "new toon soon", "new cartoon!", "hey, a new toon!!", "more biz cas fri", "biz cas fri", "new biz cas fri!", "short shorts!", "new short shortly", "new short!",
        ExampleFormatArguments = new[] { "display", "not display" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    MainPageBubbleMessages,
    [SouvenirQuestion("Which main page did {1} come from in {0}?", "Main Page", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "Homestar", "the background" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(1, 27)]
    MainPageHomestarBackgroundOrigin,

    [SouvenirQuestion("What color was the text on the {1} button in {0}?", "M&Ms", ThreeColumns6Answers, "red", "green", "orange", "blue", "yellow", "brown", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    MandMsColors,
    [SouvenirQuestion("What was the text on the {1} button in {0}?", "M&Ms", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(5, 'M', 'N')]
    MandMsLabels,

    [SouvenirQuestion("What color was the text on the {1} button in {0}?", "M&Ns", ThreeColumns6Answers, "red", "green", "orange", "blue", "yellow", "brown", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    MandNsColors,
    [SouvenirQuestion("What was the text on the correct button in {0}?", "M&Ns", ThreeColumns6Answers)]
    [AnswerGenerator.Strings(5, 'M', 'N')]
    MandNsLabel,

    [SouvenirQuestion("What bearing was signalled in {0}?", "Maritime Flags", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 359)]
    MaritimeFlagsBearing,
    [SouvenirQuestion("Which callsign was signalled in {0}?", "Maritime Flags", TwoColumns4Answers, "1stmate", "2ndmate", "3rdmate", "abandon", "admiral", "advance", "aground", "allides", "anchors", "athwart", "azimuth", "bailers", "ballast", "barrack", "beached", "beacons", "beamend", "beamsea", "bearing", "beating", "belayed", "bermuda", "bobstay", "boilers", "bollard", "bonnets", "boomkin", "bounder", "bowline", "brailed", "breadth", "bridges", "brigged", "bringto", "bulwark", "bumboat", "bumpkin", "burthen", "caboose", "capsize", "capstan", "captain", "caravel", "careens", "carrack", "carrier", "catboat", "cathead", "chained", "channel", "charley", "charter", "citadel", "cleared", "cleated", "clinker", "clipper", "coaming", "coasted", "consort", "convoys", "corinth", "cotchel", "counter", "cranzes", "crewing", "cringle", "crojack", "cruiser", "cutters", "dandies", "deadrun", "debunks", "derrick", "dipping", "disrate", "dogvane", "doldrum", "dolphin", "draught", "drifter", "drogues", "drydock", "dunnage", "dunsels", "earings", "echelon", "embayed", "ensigns", "escorts", "fairway", "falkusa", "fantail", "fardage", "fathoms", "fenders", "ferries", "fitting", "flanked", "flaring", "flattop", "flemish", "floated", "floored", "flotsam", "folding", "follows", "forcing", "forward", "foulies", "founder", "framing", "freight", "frigate", "funnels", "furling", "galleon", "galleys", "galliot", "gangway", "garbled", "general", "georges", "ghosted", "ginpole", "giveway", "gondola", "graving", "gripies", "grounds", "growler", "guineas", "gundeck", "gunport", "gunwale", "halyard", "hammock", "hampers", "hangars", "harbors", "harbour", "hauling", "hawsers", "heading", "headsea", "heaving", "herring", "hogging", "holiday", "huffler", "inboard", "inirons", "inshore", "instays", "inwater", "inwayof", "jackies", "jacktar", "jennies", "jetties", "jiggers", "joggles", "jollies", "juryrig", "keelson", "kellets", "kicking", "killick", "kitchen", "lanyard", "laydays", "lazaret", "leehelm", "leeside", "leeward", "liberty", "lighter", "lizards", "loading", "lockers", "lofting", "lolling", "lookout", "lubbers", "luffing", "luggers", "lugsail", "maewest", "manowar", "marconi", "mariner", "matelot", "mizzens", "mooring", "mousing", "narrows", "nippers", "officer", "offpier", "oilskin", "oldsalt", "onboard", "oreboat", "outhaul", "outward", "painter", "panting", "parcels", "parleys", "parrels", "passage", "pelagic", "pendant", "pennant", "pickets", "pinnace", "pintles", "pirates", "pivoted", "pursers", "pursued", "quarter", "quaying", "rabbets", "ratline", "reduced", "reefers", "repairs", "rigging", "ripraps", "rompers", "rowlock", "rudders", "ruffles", "rummage", "sagging", "sailors", "salties", "salvors", "sampans", "sampson", "sculled", "scupper", "scuttle", "seacock", "sealing", "seekers", "serving", "sextant", "shelter", "shipped", "shiprig", "sickbay", "skipper", "skysail", "slinged", "slipway", "snagged", "snotter", "spliced", "splices", "sponson", "sponsor", "springs", "squares", "stackie", "standon", "starter", "station", "steamer", "steered", "steeves", "steward", "stopper", "stovein", "stowage", "strikes", "sunfish", "swimmie", "systems", "tacking", "thwarts", "tinclad", "tompion", "tonnage", "topmast", "topsail", "torpedo", "tossers", "trading", "traffic", "tramper", "transom", "trawler", "trenail", "trennel", "trimmer", "trooper", "trunnel", "tugboat", "turntwo", "unships", "upbound", "vessels", "voicing", "voyager", "weather", "whalers", "wharves", "whelkie", "whistle", "winches", "windage", "working", "yardarm")]
    MaritimeFlagsCallsign,

    [SouvenirQuestion("In which position was the dummy in {0}?", "Maritime Semaphore", ThreeColumns6Answers)]
    [AnswerGenerator.Ordinal(6)]
    MaritimeSemaphoreDummy,
    [SouvenirQuestion("Which letter was shown by the {2} in the {1} position in {0}?", "Maritime Semaphore", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal, "left flag", QandA.Ordinal, "right flag", QandA.Ordinal, "semaphore" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { false, true })]
    [AnswerGenerator.Strings('A', 'Z')]
    MaritimeSemaphoreLetter,

    [SouvenirQuestion("What was A in {0}?", "Maroon Button", ThreeColumns6Answers, AddThe = true, Type = AnswerType.Sprites, SpriteFieldName = "MaroonButtonSprites")]
    MaroonButtonA,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Maroon Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "AMBUSH", "BANZAI", "BIGGER", "GAMBLE", "KETOSE", "OCULUS", "SCRAMS", "SENSOR", "YEANED", "YOUTHS" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    MaroonCipherScreen,

    [SouvenirQuestion("What was the answer in {0}?", "Mashematics", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99)]
    MashematicsAnswer,
    [SouvenirQuestion("What was the {1} number in the equation on {0}?", "Mashematics", ThreeColumns6Answers, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 99)]
    MashematicsCalculation,

    [SouvenirQuestion("Which song was played in {0}?", "Master Tapes", OneColumn4Answers, "Redemption Song", "Do You Want To Know A Secret", "La Bamba", "Rock-A-Hula Baby", "Pickney Gal", "Dogs", "Young Americans", "Duvet", "Shadows Of Lost Days")]
    MasterTapesPlayedSong,

    [SouvenirQuestion("Which planet was present in the {1} stage of {0}?", "Match Refereeing", TwoColumns4Answers, Type = AnswerType.Sprites,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    MatchRefereeingPlanet,

    [SouvenirQuestion("What was the color of this tile before the shuffle on {0}?", "Math ’em", TwoColumns4Answers, "White", "Bronze", "Silver", "Gold", TranslateAnswers = true, UsesQuestionSprite = true)]
    MathEmColor,
    [SouvenirQuestion("What was the design on this tile before the shuffle on {0}?", "Math ’em", ThreeColumns6Answers, UsesQuestionSprite = true, Type = AnswerType.Sprites, SpriteFieldName = "MathEmSprites")]
    MathEmLabel,

    [SouvenirQuestion("Which word was part of the latest access code in {0}?", "Matrix", TwoColumns4Answers, "Twins", "Neo", "Seraph", "Cypher", "Persephone", "Tank", "Dozer", "Mouse", "Switch", "Architect", "Smith", "Merovingian", "Morpheus", "Niobe", "Bane", "Oracle", "Keymaker", "Link", "Trinity", "Apoc", AddThe = true)]
    MatrixAccessCode,
    [SouvenirQuestion("What was the glitched word in {0}?", "Matrix", TwoColumns4Answers, "headjack", "phone", "dystopia", "control", "paradise", "utopia", "version", "nebuchadnezzar", "zion", "fight", "utopia", "mind", "squiddy", "guns", "trace", "spoon", "machine", "red", "white", "paradise", "metacortex", "flint", "nova", "white", "rabbit", "follow", "matrix", "free", "neural", "mind", "fight", "free", "nova", "blue", "fields", "choice", "battery", "program", "flint", "headjack", "kungfu", "choi", "red", "blue", "pill", "jump", "program", "agent", "sentient", "squiddy", "dystopia", "rabbit", "jump", "code", "mirror", "cookie", "human", "pill", "follow", "version", "sentinel", "machine", "prison", "human", "fields", "battery", "code", "training", "guns", "hel", "elevator", "sentinel", "choi", "matrix", "nebuchadnezzar", "control", "metacortex", "sentient", "unplug", "hardwire", "trainman", "spoon", "cookie", "elevator", "hardwire", "choice", "trace", "mirror", "unplug", "interface", "prison", "kungfu", "interface", "neural", "trainman", "hel", "agent", "training", "zion", "phone", AddThe = true)]
    MatrixGlitchWord,

    [SouvenirQuestion("In which {1} was the starting position in {0}, counting from the {2}?", "Maze", ThreeColumns6Answers, TranslateFormatArgs = new[] { true, true },
        ExampleFormatArguments = new[] { "column", "left", "row", "top" }, ExampleFormatArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(1, 6)]
    MazeStartingPosition,

    [SouvenirQuestion("What was the color of the starting face in {0}?", "Maze³", ThreeColumns6Answers, "Red", "Blue", "Yellow", "Green", "Magenta", "Orange", TranslateAnswers = true)]
    Maze3StartingFace,

    [SouvenirQuestion("What was the seed of the maze in {0}?", "Maze Identification", ThreeColumns6Answers, ExampleAnswers = new[] { "1234", "1111", "2222", "3333", "4444", "4321" })]
    [AnswerGenerator.Strings("4*1-4")]
    MazeIdentificationSeed,
    [SouvenirQuestion("What was the function of button {1} in {0}?", "Maze Identification", OneColumn4Answers, new[] { "Forwards", "Clockwise", "Backwards", "Counter-clockwise" }, ExampleAnswers = new[] { "forwards", "clockwise", "backwards", "counter-clockwise" }, TranslateAnswers = true,
        ExampleFormatArguments = new[] { "1", "2", "3", "4" }, ExampleFormatArgumentGroupSize = 1)]
    MazeIdentificationNum,
    [SouvenirQuestion("Which button {1} in {0}?", "Maze Identification", TwoColumns4Answers, new[] { "1", "2", "3", "4" }, ExampleAnswers = new[] { "1", "2", "3", "4" },
        ExampleFormatArguments = new[] { "moved you forwards", "turned you clockwise", "moved you backwards", "turned you counter-clockwise" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    MazeIdentificationFunc,

    [SouvenirQuestion("Which was the {1} value in {0}?", "Mazematics", ThreeColumns6Answers, ExampleAnswers = new[] { "30", "42", "51" },
        ExampleFormatArguments = new[] { "initial", "goal" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    MazematicsValue,

    [SouvenirQuestion("What was the starting position on {0}?", "Maze Scrambler", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    MazeScramblerStart,
    [SouvenirQuestion("What was the goal on {0}?", "Maze Scrambler", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    MazeScramblerGoal,
    [SouvenirQuestion("Which of these positions was a maze marking on {0}?", "Maze Scrambler", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    MazeScramblerIndicators,

    [SouvenirQuestion("How many walls surrounded this cell in {0}?", "Mazeseeker", TwoColumns4Answers, "0", "1", "2", "3", UsesQuestionSprite = true)]
    MazeseekerCell,
    [SouvenirQuestion("Where was the start in {0}?", "Mazeseeker", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(6, 6)]
    MazeseekerStart,
    [SouvenirQuestion("Where was the goal in {0}?", "Mazeseeker", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(6, 6)]
    MazeseekerGoal,

    [SouvenirQuestion("Where was the {1} position in {0}?", "Maze Swap", ThreeColumns6Answers, Type = AnswerType.Sprites,
        ExampleFormatArguments = new[] { "starting", "goal" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Grid(6, 6)]
    MazeSwapPosition,

    [SouvenirQuestion("Which master was shown in {0}?", "Mega Man 2", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    MegaMan2Master,
    [SouvenirQuestion("Which weapon was shown in {0}?", "Mega Man 2", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    MegaMan2Weapon,

    [SouvenirQuestion("Which part was in slot #{1} at the start of {0}?", "Melody Sequencer", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "1", "2" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 8)]
    MelodySequencerSlots,
    [SouvenirQuestion("Which slot contained part #{1} at the start of {0}?", "Melody Sequencer", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "1", "2" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 8)]
    MelodySequencerParts,

    [SouvenirQuestion("What was the {1} correct symbol pressed in {0}?", "Memorable Buttons", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "J", "K", "L", "P", "Q",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, Type = AnswerType.DynamicFont)]
    MemorableButtonsSymbols,

    [SouvenirQuestion("What was the displayed number in the {1} stage of {0}?", "Memory", TwoColumns4Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 4)]
    MemoryDisplay,
    [SouvenirQuestion("In what position was the button that you pressed in the {1} stage of {0}?", "Memory", TwoColumns4Answers, Type = AnswerType.Sprites,
        SpriteFieldName = "MemorySprites", ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    MemoryPosition,
    [SouvenirQuestion("What was the label of the button that you pressed in the {1} stage of {0}?", "Memory", TwoColumns4Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 4)]
    MemoryLabel,

    [SouvenirQuestion("What was the digit displayed in the {1} stage of {0}?", "Memory Wires", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    MemoryWiresDisplayedDigits,
    [SouvenirQuestion("What was the colour of wire {1} in {0}?", "Memory Wires", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", "Black",
        ExampleFormatArguments = new[] { "1", "2", "3", "4", "29", "30" }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    MemoryWiresWireColours,

    [SouvenirQuestion("What was the extracted letter in {0}?", "Metamorse", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-Z")]
    MetamorseExtractedLetter,

    [SouvenirQuestion("What was the final answer in {0}?", "Metapuzzle", TwoColumns4Answers,
        ExampleAnswers = new[] { "GIBBONS", "GIRAFFE", "MISUSED", "RUSHING", "DUSTMAN", "STATICS" })]
    MetapuzzleAnswer,

    [SouvenirQuestion("What was the name of starting station in {0}?", "Minsk Metro", OneColumn4Answers, ExampleAnswers = new[] { "Uručča", "Kamiennaja Horka", "Park Čaluskincaŭ", "Płošča Jakuba Kołasa" })]
    MinskMetroStation,

    [SouvenirQuestion("What was the second word written by the original ghost in {0}?", "Mirror", TwoColumns4Answers, ExampleAnswers = new[] { "ALPACA", "BUBBLE", "COWBOY", "DIESEL", "EULOGY", "FUSION", "GASKET", "HOODIE", "ICEBOX", "JOYPOP" })]
    MirrorWord,

    [SouvenirQuestion("Where was the SpongeBob Bar on {0}?", "Mister Softee", ThreeColumns6Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-middle", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    MisterSofteeSpongebobPosition,
    [SouvenirQuestion("Which treat was present on {0}?", "Mister Softee", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "MisterSofteeSprites")]
    MisterSofteeTreatsPresent,

    [SouvenirQuestion("What was the position of the submit button in {0}?", "Mixometer", TwoColumns4Answers)]
    [AnswerGenerator.Ordinal(5)]
    MixometerSubmitButton,

    [SouvenirQuestion("What was the decrypted word of the {1} stage in {0}?", "Modern Cipher", ThreeColumns6Answers, "Absent", "Abstract", "Abysmal", "Accident", "Activate", "Adjacent", "Afraid", "Agenda", "Agony", "Alchemy", "Alcohol", "Alive", "Allergic", "Allergy", "Alpha", "Alphabet", "Already", "Amethyst", "Amnesty", "Amperage", "Ancient", "Animals", "Animate", "Anthrax", "Anxious", "Aquarium", "Aquarius", "Arcade", "Arrange", "Arrow", "Artefact", "Asterisk", "Atrophy", "Audio", "Author", "Avoid", "Awesome", "Balance", "Banana", "Bandit", "Bankrupt", "Basket", "Battle", "Bazaar", "Beard", "Beauty", "Beaver", "Becoming", "Beetle", "Beseech", "Between", "Bicycle", "Bigger", "Biggest", "Biology", "Birthday", "Bistro", "Bites", "Blight", "Blockade", "Blubber", "Bomb", "Bonobo", "Books", "Bottle", "Brazil", "Brief", "Broccoli", "Broken", "Brother", "Bubble", "Budget", "Bulkhead", "Bumper", "Bunny", "Button", "Bytes", "Cables", "Caliber", "Campaign", "Canada", "Canister", "Caption", "Caution", "Cavity", "Chalk", "Chamber", "Chamfer", "Champion", "Changes", "Chicken", "Children", "Chlorine", "Chord", "Chronic", "Church", "Cinnamon", "Civic", "Cleric", "Clock", "Cocoon", "Combat", "Combine", "Comedy", "Comics", "Comma", "Command", "Comment", "Compost", "Computer", "Condom", "Conflict", "Consider", "Contour", "Control", "Corrupt", "Costume", "Criminal", "Crunch", "Cryptic", "Cuboid", "Cypher", "Daddy", "Dancer", "Dancing", "Daughter", "Dead", "Decapod", "Decay", "Decoy", "Defeat", "Defuser", "Degree", "Delay", "Demigod", "Dentist", "Desert", "Design", "Desire", "Dessert", "Detail", "Develop", "Device", "Diamond", "Dictate", "Diffuse", "Dilemma", "Dingy", "Dinosaur", "Disease", "Disgust", "Document", "Doubled", "Doubt", "Downbeat", "Dragon", "Drawer", "Dream", "Drink", "Drunken", "Dungeon", "Dynasty", "Dyslexia", "Eclipse", "Eldritch", "Email", "Emulator", "Encrypt", "England", "Enlist", "Enough", "Ensure", "Equality", "Equation", "Eruption", "Eternity", "Euphoria", "Exact", "Exclaim", "Exhaust", "Expert", "Expertly", "Explain", "Explodes", "Fabric", "Factory", "Faded", "Faint", "Fair", "False", "Falter", "Famous", "Fantasy", "Farm", "Father", "Faucet", "Faulty", "Fearsome", "Feast", "February", "Feint", "Festival", "Fiction", "Fighter", "Figure", "Finish", "Fireman", "Firework", "First", "Fixture", "Flagrant", "Flagship", "Flamingo", "Flesh", "Flipper", "Fluorine", "Flush", "Foreign", "Forensic", "Fractal", "Fragrant", "France", "Frantic", "Freak", "Friction", "Friday", "Friendly", "Frighten", "Furor", "Fused", "Garage", "Genes", "Genetic", "Genius", "Gentle", "Glacier", "Glitch", "Goat", "Golden", "Granular", "Graphics", "Graphite", "Grateful", "Gridlock", "Ground", "Guitar", "Gumption", "Halogen", "Harmony", "Hawk", "Headache", "Heard", "Hedgehog", "Heinous", "Herd", "Heretic", "Hexagon", "Hiccup", "Highway", "Holiday", "Home", "Homesick", "Honest", "Horror", "Horse", "House", "Huge", "Humanity", "Hungry", "Hydrogen", "Hysteria", "Imagine", "Industry", "Infamous", "Inside", "Integral", "Interest", "Ironclad", "Issue", "Italic", "Italy", "Itch", "Jaundice", "Jeans", "Jeopardy", "Joyful", "Joystick", "Juice", "Juncture", "Jungle", "Junkyard", "Justice", "Keep", "Keyboard", "Kilobyte", "Kilogram", "Kingdom", "Kitchen", "Kitten", "Knife", "Krypton", "Ladylike", "Language", "Large", "Laughter", "Launch", "Leaders", "Learn", "Leave", "Leopard", "Level", "Liberal", "Liberty", "Lifeboat", "Ligament", "Light", "Liquid", "Listen", "Little", "Lobster", "Logical", "Love", "Lucky", "Lulled", "Lunatic", "Lurks", "Machine", "Madam", "Magnetic", "Manager", "Manual", "Marina", "Marine", "Martian", "Master", "Matrix", "Measure", "Meaty", "Meddle", "Medical", "Mental", "Menu", "Meow", "Merchant", "Message", "Messes", "Metal", "Method", "Mettle", "Militant", "Minim", "Minimum", "Miracle", "Mirror", "Misjudge", "Misplace", "Misses", "Mistake", "Mixture", "Mnemonic", "Mobile", "Modern", "Modest", "Module", "Moist", "Money", "Morning", "Most", "Mother", "Movies", "Multiple", "Munch", "Musical", "Mustache", "Mystery", "Mystic", "Mystique", "Mythic", "Narcotic", "Nasty", "Nature", "Navigate", "Network", "Neutral", "Nobelium", "Nobody", "Noise", "Notice", "Noun", "Nuclear", "Numeral", "Nutrient", "Nymph", "Obelisk", "Obstacle", "Obvious", "Octopus", "Offset", "Omega", "Opaque", "Opinion", "Orange", "Organic", "Ouch", "Outbreak", "Outdo", "Overcast", "Overlaps", "Package", "Padlock", "Pancake", "Panda", "Panic", "Paper", "Papers", "Parent", "Park", "Particle", "Passive", "Patented", "Pathetic", "Patient", "Peace", "Peasant", "Penalty", "Pencil", "Penguin", "Perfect", "Person", "Persuade", "Perusing", "Phone", "Physical", "Piano", "Picture", "Piglet", "Pilfer", "Pillage", "Pinch", "Pirate", "Pitcher", "Pizza", "Plane", "Planet", "Platonic", "Player", "Please", "Plucky", "Plunder", "Plurals", "Pocket", "Police", "Portrait", "Potato", "Potently", "Pounce", "Poverty", "Practice", "Predict", "Prefect", "Premium", "Present", "Prince", "Printer", "Prison", "Profit", "Promise", "Prophet", "Protein", "Province", "Psalm", "Psychic", "Puddle", "Punchbag", "Pungent", "Punish", "Purchase", "Quagmire", "Qualify", "Quantify", "Quantize", "Quarter", "Querying", "Queue", "Quiche", "Quick", "Rabbit", "Racoon", "Radar", "Radical", "Rainbow", "Random", "Rattle", "Ravenous", "Reason", "Rebuke", "Refine", "Regular", "Reindeer", "Request", "Resort", "Respect", "Retire", "Revolt", "Reward", "Rhapsody", "Rhenium", "Rhodium", "Rhomboid", "Rhyme", "Rhythm", "Ridicule", "Roadwork", "Roar", "Roast", "Room", "Rooster", "Roster", "Rotor", "Rotunda", "Royal", "Ruler", "Rural", "Sailor", "Sainted", "Sales", "Sally", "Satisfy", "Saunter", "Scale", "Scandal", "Schedule", "School", "Science", "Scratch", "Screen", "Sensible", "Separate", "Serious", "Several", "Shampoo", "Shares", "Shelter", "Shift", "Ship", "Shirt", "Shiver", "Shorten", "Showcase", "Shuffle", "Silent", "Similar", "Sister", "Sixth", "Sixty", "Skater", "Skyward", "Slander", "Slayer", "Sleek", "Slipper", "Smart", "Smeared", "Soccer", "Society", "Source", "Spain", "Spare", "Spark", "Spatula", "Speaker", "Special", "Spectate", "Spectrum", "Spicy", "Spinach", "Spiral", "Splendid", "Splinter", "Sprayed", "Spread", "Spring", "Squadron", "Squander", "Squash", "Squib", "Squid", "Squish", "Stake", "Stalking", "Steak", "Steam", "Sticker", "Stinky", "Stocking", "Stone", "Store", "Stormy", "Strange", "Strike", "Stutter", "Subway", "Suffer", "Supreme", "Surf", "Surplus", "Survey", "Switch", "Symbol", "System", "Systemic", "Table", "Tadpole", "Talking", "Tangle", "Tank", "Tapeworm", "Target", "Tarot", "Teach", "Teamwork", "Terminal", "Terminus", "Terror", "Testify", "Their", "There", "Thick", "Thief", "Think", "Throat", "Through", "Thunder", "Thyme", "Ticket", "Time", "Toaster", "Tomato", "Tone", "Torque", "Tortoise", "Touchy", "Toupe", "Tower", "Transfix", "Transit", "Trash", "Trauma", "Treason", "Treasure", "Trick", "Tripod", "Trouble", "Truck", "Trumpet", "Turtle", "Twinkle", "Ugly", "Ultra", "Umbrella", "Underway", "Unique", "Unknown", "Unsteady", "Untoward", "Unwashed", "Upgrade", "Urban", "Used", "Useless", "Utopia", "Vacuum", "Vampire", "Vanish", "Vanquish", "Various", "Vast", "Velocity", "Vendor", "Verb", "Verbatim", "Verdict", "Vexation", "Vicious", "Victim", "Victory", "Video", "View", "Viking", "Village", "Violent", "Violin", "Virulent", "Visceral", "Vision", "Volatile", "Voltage", "Vortex", "Vulgar", "Warden", "Warlock", "Warning", "Wealth", "Weapon", "Wedding", "Weight", "Whack", "Wharf", "What", "When", "Whisk", "Whistle", "Wicked", "Window", "Winter", "Witness", "Wizard", "Wrench", "Wretch", "Wrinkle", "Writer", "Xanthous", "Yacht", "Yarn", "Yawn", "Yeah", "Yearlong", "Yearn", "Yeoman", "Yodel", "Yoga", "Yonder", "Youngest", "Yourself", "Zealot", "Zebra", "Zenith", "Zither", "Zodiac", "Zombie",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ModernCipherWord,

    [SouvenirQuestion("Which sound did the {1} button play in {0}?", "Module Listening", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue", "yellow" }, ExampleFormatArgumentGroupSize = 1, Type = AnswerType.Audio, ForeignAudioID = "moduleListening")]
    ModuleListeningButtonAudio,
    [SouvenirQuestion("Which sound played in {0}?", "Module Listening", ThreeColumns6Answers, Type = AnswerType.Audio, ForeignAudioID = "moduleListening")]
    ModuleListeningAnyAudio,

    [SouvenirQuestion("What was the goal location in {0}?", "Module Maneuvers", ThreeColumns6Answers, ExampleAnswers = new[] { "0, 0", "1, 0", "2, -1", "-2, 0", "3, 3", "12, -15" }, TranslatableStrings = new[] { "{0}, {1}" })]
    ModuleManeuversGoal,

    [SouvenirQuestion("Which of the following was the starting icon for {0}?", "Module Maze", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    ModuleMazeStartingIcon,

    [SouvenirQuestion("What was the {1} module shown in {0}?", "Module Movements", TwoColumns4Answers, "3D Tunnels", "Alchemy", "Braille", "Button Sequence", "Chord Qualities", "Crackbox", "Functions", "Hunting", "Kudosudoku", "Logic Gates", "Morse-A-Maze", "Pattern Cube", "Planets", "Quintuples", "Schlag den Bomb", "Shapes And Bombs", "Simon Samples", "Simon States", "Symbol Cycle", "Turtle Robot", "Wavetapping", "The Wire", "Yahtzee",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ModuleMovementsDisplay,

    [SouvenirQuestion("What were the first and second words in the {1} phrase in {0}?", "Money Game", TwoColumns4Answers, "she sells", "she shells", "sea shells", "sea sells",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    MoneyGame1,
    [SouvenirQuestion("What were the third and fourth words in the {1} phrase in {0}?", "Money Game", TwoColumns4Answers, "sea shells", "she shells", "sea sells", "she sells",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    MoneyGame2,
    [SouvenirQuestion("What was the end of the {1} phrase in {0}?", "Money Game", TwoColumns4Answers, "sea shore", "she sore", "she sure", "seesaw", "seizure", "shell sea", "steep store", "sheer sort", "speed spore", "sieve horn", "steel sword",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    MoneyGame3,

    [SouvenirQuestion("Which creature was displayed in {0}?", "Monsplode, Fight!", TwoColumns4Answers, "Caadarim", "Buhar", "Melbor", "Lanaluff", "Bob", "Mountoise", "Aluga", "Nibs", "Zapra", "Zenlad", "Vellarim", "Ukkens", "Lugirit", "Flaurim", "Myrchat", "Clondar", "Gloorim", "Docsplode", "Magmy", "Pouse", "Asteran", "Violan", "Percy", "Cutie Pie")]
    MonsplodeFightCreature,
    [SouvenirQuestion("Which one of these moves {1} selectable in {0}?", "Monsplode, Fight!", TwoColumns4Answers, "Tic", "Tac", "Toe", "Hollow Gaze", "Splash", "Heavy Rain", "Fountain", "Candle", "Torchlight", "Flame Spear", "Tangle", "Grass Blade", "Ivy Spikes", "Spectre", "Boo", "Battery Power", "Zap", "Double Zap", "Shock", "High Voltage", "Dark Portal", "Last Word", "Void", "Boom", "Fiery Soul", "Stretch", "Shrink", "Appearify", "Sendify", "Freak Out", "Glyph", "Bug Spray", "Bedrock", "Earthquake", "Cave In", "Toxic Waste", "Venom Fang", "Countdown", "Finale", "Sidestep",
        ExampleFormatArguments = new[] { "was", "was not" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    MonsplodeFightMove,

    [SouvenirQuestion("What was the {1} before the last action in {0}?", "Monsplode Trading Cards", TwoColumns4Answers, "Aluga", "Asteran", "Bob", "Buhar", "Caadarim", "Clondar", "Cutie Pie", "Docsplode", "Flaurim", "Gloorim", "Lanaluff", "Lugirit", "Magmy", "Melbor", "Mountoise", "Myrchat", "Nibs", "Percy", "Pouse", "Ukkens", "Vellarim", "Violan", "Zapra", "Zenlad", "Aluga, The Fighter", "Bob, The Ancestor", "Buhar, The Protector", "Melbor, The Web Bug",
        ExampleFormatArguments = new[] { "first card in your hand", "second card in your hand", "third card in your hand", "card on offer" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    MonsplodeTradingCardsCards,
    [SouvenirQuestion("What was the print version of the {1} before the last action in {0}?", "Monsplode Trading Cards", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "first card in your hand", "second card in your hand", "third card in your hand", "card on offer" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Strings("A-J", "1-9")]
    MonsplodeTradingCardsPrintVersions,

    [SouvenirQuestion("What was the {1} set in clockwise order in {0}?", "Moon", TwoColumns4Answers, "south", "south-west", "west", "north-west", "north", "north-east", "east", "south-east", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "first initially lit", "second initially lit", "third initially lit", "fourth initially lit", "first initially unlit", "second initially unlit", "third initially unlit", "fourth initially unlit" },
        ExampleFormatArgumentGroupSize = 1, AddThe = true, TranslateFormatArgs = new[] { true })]
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
    [SouvenirQuestion("What was the word shown as Morse code in {0}?", "Morse-A-Maze", ThreeColumns6Answers,
        ExampleAnswers = new[] { "couch", "strobe", "smoke", "assay", "monkey", "glass", "starts", "strode", "office", "essays", "couple", "bosses" })]
    MorseAMazeMorseCodeWord,

    [SouvenirQuestion("What was the character flashed by the {1} button in {0}?", "Morse Buttons", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-Z0-9")]
    MorseButtonsButtonLabel,
    [SouvenirQuestion("What was the color flashed by the {1} button in {0}?", "Morse Buttons", ThreeColumns6Answers, "red", "blue", "green", "yellow", "orange", "purple", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    MorseButtonsButtonColor,

    [SouvenirQuestion("What was the {1} received letter in {0}?", "Morsematics", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    MorsematicsReceivedLetters,

    [SouvenirQuestion("What were the LEDs in the {1} row in {0} (1\u00a0=\u00a0on, 0\u00a0=\u00a0off)?", "Morse War", ThreeColumns6Answers, "1100", "1010", "1001", "0110", "0101", "0011", TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "bottom", "middle", "top" }, ExampleFormatArgumentGroupSize = 1)]
    MorseWarLeds,
    [SouvenirQuestion("What code was transmitted in {0}?", "Morse War", ThreeColumns6Answers, "ABR", "RBS", "SVR", "ZUX", "ZAQ", "MOI", "OPA", "VZQ", "XRP", "OLL", "AIR", "RHG", "MJN", "VTT", "XZS", "SUN")]
    MorseWarCode,

    [SouvenirQuestion("What color was the torus in {0}?", "Mouse in the Maze", TwoColumns4Answers, "white", "green", "blue", "yellow", TranslateAnswers = true)]
    MouseInTheMazeTorus,
    [SouvenirQuestion("Which color sphere was the goal in {0}?", "Mouse in the Maze", TwoColumns4Answers, "white", "green", "blue", "yellow", TranslateAnswers = true)]
    MouseInTheMazeSphere,

    [SouvenirQuestion("What was the {1} obtained digit in {0}?", "M-Seq", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 9)]
    MSeqObtained,
    [SouvenirQuestion("What was the final number from the iteration process in {0}?", "M-Seq", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(25, 225)]
    MSeqSubmitted,

    [SouvenirQuestion("Which vowel was missing in {0}?", "\uE001Mssngv Wls\uE002", ThreeColumns6Answers, "A", "E", "I", "O", "U", TranslatableStrings = new[] { "AEIOU" })]
    MssngvWlsMssNgvwL,

    [SouvenirQuestion("What color was the {1} LED on the {2} row when the tiny LED was {3} in {0}?", "Multicolored Switches", TwoColumns4Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white", TranslateAnswers = true, TranslateFormatArgs = new[] { false, true, true },
        ExampleFormatArguments = new[] { QandA.Ordinal, "top", "lit", QandA.Ordinal, "bottom", "lit", QandA.Ordinal, "top", "unlit", QandA.Ordinal, "bottom", "unlit" }, ExampleFormatArgumentGroupSize = 3)]
    MulticoloredSwitchesLedColor,

    [SouvenirQuestion("Where was the body found in {0}?", "Murder", TwoColumns4Answers, "Dining Room", "Study", "Kitchen", "Lounge", "Billiard Room", "Conservatory", "Ballroom", "Hall", "Library", TranslateAnswers = true)]
    MurderBodyFound,
    [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", TwoColumns4Answers, "Miss Scarlett", "Professor Plum", "Mrs Peacock", "Reverend Green", "Colonel Mustard", "Mrs White", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "a suspect but not the murderer", "not a suspect" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    MurderSuspect,
    [SouvenirQuestion("Which of these was {1} in {0}?", "Murder", TwoColumns4Answers, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "a potential weapon but not the murder weapon", "not a potential weapon" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    MurderWeapon,

    [SouvenirQuestion("Which module was the first requested to be solved by {0}?", "Mystery Module", OneColumn4Answers, ExampleAnswers = new[] { "Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox", "Rock-Paper-Scissors-L.-Sp." })]
    MysteryModuleFirstKey,
    [SouvenirQuestion("Which module was hidden by {0}?", "Mystery Module", OneColumn4Answers, ExampleAnswers = new[] { "Probing", "Kudosudoku", "Ten-Button Color Code", "The Jukebox" })]
    MysteryModuleHiddenModule,

    [SouvenirQuestion("Where was the skull in {0}?", "Mystic Square", TwoColumns4Answers, "top left", "top middle", "top right", "middle left", "center", "middle right", "bottom left", "bottom middle", "bottom right", TranslateAnswers = true)]
    MysticSquareSkull,

    [SouvenirQuestion("What was the {1} index in {0}?", "Name Codes", TwoColumns4Answers, "2", "3", "4", "5", TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "left", "right" }, ExampleFormatArgumentGroupSize = 1)]
    NameCodesIndices,

    [SouvenirQuestion("What was the label of the first button in {0}?", "Naming Conventions", TwoColumns4Answers, "Class", "Constructor", "Method", "Argument", "Local", "Constant", "Field", "Property", "Delegate", "Enum")]
    NamingConventionsObject,

    [SouvenirQuestion("What was the label of the correct button in {0}?", "N&Ms", ThreeColumns6Answers)]
    [AnswerGenerator.Strings(5, 'M', 'N')]
    NandMsAnswer,

    [SouvenirQuestion("Which label was present in the {1} stage of {0}?", "N&Ns", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(5, 'M', 'N')]
    NandNsLabel,
    [SouvenirQuestion("Which color was missing in the third stage of {0}?", "N&Ns", ThreeColumns6Answers, "Red", "Green", "Orange", "Blue", "Yellow", "Brown", TranslateAnswers = true)]
    NandNsColor,

    [SouvenirQuestion("What was the color of the maze in {0}?", "Navigation Determination", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", TranslateAnswers = true)]
    NavigationDeterminationColor,
    [SouvenirQuestion("What was the label of the maze in {0}?", "Navigation Determination", TwoColumns4Answers, "A", "B", "C", "D")]
    NavigationDeterminationLabel,

    [SouvenirQuestion("What was the initial middle digit in {0}?", "Navinums", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 9)]
    NavinumsMiddleDigit,
    [SouvenirQuestion("What was the {1} directional button pressed in {0}?", "Navinums", TwoColumns4Answers, "up", "left", "right", "down", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NavinumsDirectionalButtons,

    [SouvenirQuestion("Which Greek letter appeared on {0} (case-sensitive)?", "Navy Button", ThreeColumns6Answers, "Α", "Β", "Γ", "Δ", "Ε", "Ζ", "Η", "Θ", "Ι", "Κ", "Λ", "Μ", "Ν", "Ξ", "Ο", "Π", "Ρ", "Σ", "Τ", "Υ", "Φ", "Χ", "Ψ", "Ω", "α", "β", "γ", "δ", "ε", "ζ", "η", "θ", "ι", "κ", "λ", "μ", "ν", "ξ", "ο", "π", "ρ", "σ", "τ", "υ", "φ", "χ", "ψ", "ω", AddThe = true)]
    NavyButtonGreekLetters,
    [SouvenirQuestion("What was the {1} of the given in {0} (0-indexed)?", "Navy Button", ThreeColumns6Answers, "0", "1", "2", "3", AddThe = true,
        ExampleFormatArguments = new[] { "column", "row", "value" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    NavyButtonGiven,

    [SouvenirQuestion("What was the chapter number of the {1} page in {0}?", "Necronomicon", ThreeColumns6Answers, ExampleAnswers = new[] { "1", "24", "36" }, AddThe = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NecronomiconChapters,

    [SouvenirQuestion("In base 10, what was the value submitted in {0}?", "Negativity", ThreeColumns6Answers, ExampleAnswers = new[] { "0", "9990", "-9990", "-1234", "5678", "-90" })]
    NegativitySubmittedValue,
    [SouvenirQuestion("Excluding 0s, what was the submitted balanced ternary in {0}?", "Negativity", TwoColumns4Answers, ExampleAnswers = new[] { "+-", "-++", "++-+-", "++++-", "-----", "+-----++++" })]
    NegativitySubmittedTernary,

    [SouvenirQuestion("Which star was displayed in {0}?", "Neptune", OneColumn4Answers, ExampleAnswers = new[] { "Bob-omb Battlefield #1", "Whomp's Fortress #2", "Jolly Roger Bay #3", "Bowser in the Sky" })]
    NeptuneStar,

    [SouvenirQuestion("What was the acid’s color in {0}?", "Neutralization", TwoColumns4Answers, "Yellow", "Green", "Red", "Blue", TranslateAnswers = true)]
    NeutralizationColor,
    [SouvenirQuestion("What was the acid’s volume in {0}?", "Neutralization", TwoColumns4Answers, "5", "10", "15", "20")]
    NeutralizationVolume,

    [SouvenirQuestion("What color was the first wire in {0}?", "Next In Line", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Black", "White", "Gray", TranslateAnswers = true)]
    NextInLineFirstWire,

    [SouvenirQuestion("Which button flashed in the {1} stage in {0}?", "❖", TwoColumns4Answers, IsEntireQuestionSprite = true, Type = AnswerType.Sprites, SpriteFieldName = "NonverbalSimonSprites", ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NonverbalSimonFlashes,

    [SouvenirQuestion("What was the position of the square you initially pressed in {0}?", "Not Colored Squares", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    NotColoredSquaresInitialPosition,

    [SouvenirQuestion("What was the encrypted word in {0}?", "Not Colored Switches", ThreeColumns6Answers, ExampleAnswers = new[] { "Adjust", "Anchor", "Bowtie", "Button", "Cipher", "Corner" })]
    NotColoredSwitchesWord,

    [SouvenirQuestion("What was {1} in the displayed word sequence in {0}?", "Not Colour Flash", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "White",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NotColourFlashInitialWord,
    [SouvenirQuestion("What was {1} in the displayed colour sequence in {0}?", "Not Colour Flash", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "White", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NotColourFlashInitialColour,

    [SouvenirQuestion("What symbol flashed on the {1} button in {0}?", "Not Connection Check", ThreeColumns6Answers, "+", "-", ".", ":", "/", "_", "=", ",",
        ExampleFormatArguments = new[] { "top left", "top right", "bottom left", "bottom right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    NotConnectionCheckFlashes,
    [SouvenirQuestion("What was the value of the {1} button in {0}?", "Not Connection Check", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
        ExampleFormatArguments = new[] { "top left", "top right", "bottom left", "bottom right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    NotConnectionCheckValues,

    [SouvenirQuestion("Which coordinate was part of the square in {0}?", "Not Coordinates", OneColumn4Answers, ExampleAnswers = new[] { "[4,7]", "C4", "<0, 2>", "3, 1", "(6,2)", "B-1", "“1, 0”", "4/3", "[12]", "#23", "四十七" })]
    NotCoordinatesSquareCoords,

    [SouvenirQuestion("What was the {1} displayed position in the second stage of {0}?", "Not Double-Oh", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(2, 'A', 'H')]
    NotDoubleOhPosition,

    [SouvenirQuestion("What color flashed {1} in the final sequence in {0}?", "Not Keypad", ThreeColumns6Answers, "red", "orange", "yellow", "green", "cyan", "blue", "purple", "magenta", "pink", "brown", "grey", "white", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NotKeypadColor,
    [SouvenirQuestion("Which symbol was on the button that flashed {1} in the final sequence in {0}?", "Not Keypad", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "KeypadSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NotKeypadSymbol,

    [SouvenirQuestion("What was the starting distance in {0}?", "Not Maze", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 9)]
    NotMazeStartingDistance,

    [SouvenirQuestion("What was the {1} correct word you submitted in {0}?", "Not Morse Code", ThreeColumns6Answers, ExampleAnswers = new[] { "shelf", "pounds", "sister", "beef", "yeast", "drive" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NotMorseCodeWord,

    [SouvenirQuestion("What was the transmitted word on {0}?", "Not Morsematics", ThreeColumns6Answers, ExampleAnswers = new[] { "ABORT", "AFTER", "AGONY", "ALIGN", "AMONG", "AMBER", "ANGST", "AZURE", "BAKER", "BAYOU", "BEACH", "BLACK", "BOGUS", "BOXES", "BRASH", "BUDGE", "CABLE", "CAULK", "CHIEF", "CLOVE", "CODEX", "CRAZE", "CRISP", "CRUEL" })]
    NotMorsematicsWord,

    [SouvenirQuestion("What room was {1} in initially on {0}?", "Not Murder", TwoColumns4Answers, "Ballroom", "Billiard Room", "Conservatory", "Dining Room", "Hall", "Kitchen", "Library", "Lounge", "Study", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White", }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true },
        TranslatableStrings = new[] { "the Not Murder where he initially held the {0}", "the Not Murder where she initially held the {0}", "the Not Murder where he started in the {0}", "the Not Murder where she started in the {0}", "the Not Murder where he was present", "the Not Murder where she was present" })]
    NotMurderRoom,
    [SouvenirQuestion("What weapon did {1} possess initially on {0}?", "Not Murder", TwoColumns4Answers, "Candlestick", "Dagger", "Lead Pipe", "Revolver", "Rope", "Spanner", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "Miss Scarlett", "Colonel Mustard", "Reverend Green", "Mrs Peacock", "Professor Plum", "Mrs White", }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    NotMurderWeapon,

    [SouvenirQuestion("Which of these numbers {1} at the {2} stage of {0}?", "Not Number Pad", TwoColumns4Answers, TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "flashed", QandA.Ordinal, "did not flash", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    NotNumberPadFlashes,

    [SouvenirQuestion("Which letter was missing from {0}?", "Not Password", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    NotPasswordLetter,

    [SouvenirQuestion("What was the position of the {1} flashing peg on {0}?", "Not Perspective Pegs", ThreeColumns6Answers, "top", "top-right", "bottom-right", "bottom-left", "top-left",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    NotPerspectivePegsPosition,
    [SouvenirQuestion("From what perspective did the {1} peg flash on {0}?", "Not Perspective Pegs", ThreeColumns6Answers, "top", "top-right", "bottom-right", "bottom-left", "top-left",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    NotPerspectivePegsPerspective,
    [SouvenirQuestion("What was the color of the {1} flashing peg on {0}?", "Not Perspective Pegs", ThreeColumns6Answers, "blue", "green", "purple", "red", "yellow",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    NotPerspectivePegsColor,

    [SouvenirQuestion("What was the first displayed symbol on {0}?", "Not Piano Keys", TwoColumns4Answers, "b", "n", "#", "", Type = AnswerType.PianoKeysFont)]
    NotPianoKeysFirstSymbol, //There are only 4 possibilities for the first symbol.
    [SouvenirQuestion("What was the second displayed symbol on {0}?", "Not Piano Keys", ThreeColumns6Answers, "c", "C", "^", "v", ">", "", "%", "\"", "*", Type = AnswerType.PianoKeysFont)]
    NotPianoKeysSecondSymbol, //There are only 9 possibilities for the 2nd and 3rd symbols
    [SouvenirQuestion("What was the third displayed symbol on {0}?", "Not Piano Keys", ThreeColumns6Answers, "U", "T", "m", "w", "", "B", "x", "", "", Type = AnswerType.PianoKeysFont)]
    NotPianoKeysThirdSymbol,

    [SouvenirQuestion("What was the starting number in {0}?", "Not Red Arrows", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(10, 99, 1, "00")]
    NotRedArrowsStart,

    [SouvenirQuestion("Which maze was used in {0}?", "Not Simaze", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", TranslateAnswers = true)]
    NotSimazeMaze,
    [SouvenirQuestion("What was the starting position in {0}?", "Not Simaze", TwoColumns4Answers, "(red, red)", "(red, orange)", "(red, yellow)", "(red, green)", "(red, blue)", "(red, purple)", "(orange, red)", "(orange, orange)", "(orange, yellow)", "(orange, green)", "(orange, blue)", "(orange, purple)", "(yellow, red)", "(yellow, orange)", "(yellow, yellow)", "(yellow, green)", "(yellow, blue)", "(yellow, purple)", "(green, red)", "(green, orange)", "(green, yellow)", "(green, green)", "(green, blue)", "(green, purple)", "(blue, red)", "(blue, orange)", "(blue, yellow)", "(blue, green)", "(blue, blue)", "(blue, purple)", "(purple, red)", "(purple, orange)", "(purple, yellow)", "(purple, green)", "(purple, blue)", "(purple, purple)", TranslateAnswers = true)]
    NotSimazeStart,
    [SouvenirQuestion("What was the goal position in {0}?", "Not Simaze", TwoColumns4Answers, "(red, red)", "(red, orange)", "(red, yellow)", "(red, green)", "(red, blue)", "(red, purple)", "(orange, red)", "(orange, orange)", "(orange, yellow)", "(orange, green)", "(orange, blue)", "(orange, purple)", "(yellow, red)", "(yellow, orange)", "(yellow, yellow)", "(yellow, green)", "(yellow, blue)", "(yellow, purple)", "(green, red)", "(green, orange)", "(green, yellow)", "(green, green)", "(green, blue)", "(green, purple)", "(blue, red)", "(blue, orange)", "(blue, yellow)", "(blue, green)", "(blue, blue)", "(blue, purple)", "(purple, red)", "(purple, orange)", "(purple, yellow)", "(purple, green)", "(purple, blue)", "(purple, purple)", TranslateAnswers = true)]
    NotSimazeGoal,

    [SouvenirQuestion("Which letter was pressed in the first stage of {0}?", "Not Text Field", TwoColumns4Answers)]
    [AnswerGenerator.Strings('A', 'F')]
    NotTextFieldInitialPresses,
    [SouvenirQuestion("Which letter appeared 9 times at the start of {0}?", "Not Text Field", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'F')]
    NotTextFieldBackgroundLetter,

    [SouvenirQuestion("What word flashed on {0}?", "Not The Bulb", OneColumn4Answers, ExampleAnswers = new[] { "Amplitude", "Boulevard", "Chemistry", "Duplicate", "Eightfold", "Filaments", "Goldsmith", "Harlequin", "Injectors", "Juxtapose", "Kilohertz", "Labyrinth", "Moustache", "Neighbour", "Obscurity", "Penumbral", "Quicksand", "Rhapsodic", "Squawking", "Triglyphs", "Universal", "Vexations", "Whizbangs", "Xenoglyph", "Yardstick", "Zigamorph" })]
    NotTheBulbWord,
    [SouvenirQuestion("What color was the bulb on {0}?", "Not The Bulb", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "Purple", "White", TranslateAnswers = true)]
    NotTheBulbColor,
    [SouvenirQuestion("What was the material of the screw cap on {0}?", "Not The Bulb", ThreeColumns6Answers, "Copper", "Silver", "Gold", "Plastic", "Carbon Fibre", "Ceramic", TranslateAnswers = true)]
    NotTheBulbScrewCap,

    [SouvenirQuestion("What colors did the light glow in {0}?", "Not the Button", ThreeColumns6Answers, "white", "red", "yellow", "green", "blue", "white/red", "white/yellow", "white/green", "white/blue", "red/yellow", "red/green", "red/blue", "yellow/green", "yellow/blue", "green/blue", TranslateAnswers = true)]
    NotTheButtonLightColor,

    [SouvenirQuestion("What color did the background flash in {0}?", "Not The Plunger Button", TwoColumns4Answers, "Black", "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", "White", TranslateAnswers = true)]
    NotThePlungerButtonBackground,

    [SouvenirQuestion("What was the initial position in {0}?", "Not the Screw", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(6, 4)]
    NotTheScrewInitialPosition,

    [SouvenirQuestion("In which position was the button you pressed in the {1} stage on {0}?", "Not Who’s on First", TwoColumns4Answers, "top left", "top right", "middle left", "middle right", "bottom left", "bottom right", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NotWhosOnFirstPressedPosition,
    [SouvenirQuestion("What was the label on the button you pressed in the {1} stage on {0}?", "Not Who’s on First", ThreeColumns6Answers, "BLANK", "DONE", "FIRST", "HOLD", "LEFT", "LIKE", "MIDDLE", "NEXT", "NO", "NOTHING", "OKAY", "PRESS", "READY", "RIGHT", "SURE", "U", "UH HUH", "UH UH", "UHHH", "UR", "WAIT", "WHAT", "WHAT?", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NotWhosOnFirstPressedLabel,
    [SouvenirQuestion("In which position was the reference button in the {1} stage on {0}?", "Not Who’s on First", TwoColumns4Answers, "top left", "top right", "middle left", "middle right", "bottom left", "bottom right", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NotWhosOnFirstReferencePosition,
    [SouvenirQuestion("What was the label on the reference button in the {1} stage on {0}?", "Not Who’s on First", ThreeColumns6Answers, "BLANK", "DONE", "FIRST", "HOLD", "LEFT", "LIKE", "MIDDLE", "NEXT", "NO", "NOTHING", "OKAY", "PRESS", "READY", "RIGHT", "SURE", "U", "UH HUH", "UH UH", "UHHH", "UR", "WAIT", "WHAT", "WHAT?", "YES", "YOU", "YOU ARE", "YOU'RE", "YOUR",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    NotWhosOnFirstReferenceLabel,
    [SouvenirQuestion("What was the calculated number in the second stage on {0}?", "Not Who’s on First", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 60)]
    NotWhosOnFirstSum,

    [SouvenirQuestion("Which of these consonants was missing in {0}?", "Not Word Search", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z")]
    NotWordSearchMissing,
    [SouvenirQuestion("What was the first correctly pressed letter in {0}?", "Not Word Search", ThreeColumns6Answers, "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Y", "Z")]
    NotWordSearchFirstPress,

    [SouvenirQuestion("Which sector value {1} present on {0}?", "Not X01", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
        ExampleFormatArguments = new[] { "was", "was not" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    NotX01SectorValues,

    [SouvenirQuestion("What table were we in in {0} (numbered 1–8 in reading order in the manual)?", "Not X-Ray", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8")]
    NotXRayTable,
    [SouvenirQuestion("What direction was button {1} in {0}?", "Not X-Ray", ThreeColumns6Answers, "Up", "Right", "Down", "Left", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "1", "2", "3", "4" }, ExampleFormatArgumentGroupSize = 1)]
    NotXRayDirections,
    [SouvenirQuestion("Which button went {1} in {0}?", "Not X-Ray", ThreeColumns6Answers, "1", "2", "3", "4", TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "up", "right", "down", "left" }, ExampleFormatArgumentGroupSize = 1)]
    NotXRayButtons,
    [SouvenirQuestion("What was the scanner color in {0}?", "Not X-Ray", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", TranslateAnswers = true)]
    NotXRayScannerColor,

    [SouvenirQuestion("Which number was correctly pressed on {0}?", "Numbered Buttons", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 100)]
    NumberedButtonsButtons,

    [SouvenirQuestion("What was the maximum number in {0}?", "Number Game", TwoColumns4Answers, AddThe = true)]
    [AnswerGenerator.Integers(10000000, 99999999)]
    NumberGameMaximum,

    [SouvenirQuestion("What two-digit number was given in {0}?", "Numbers", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99, "00")]
    NumbersTwoDigit,

    [SouvenirQuestion("What was the color of the number on {0}?", "Numpath", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", TranslateAnswers = true)]
    NumpathColor,
    [SouvenirQuestion("What was the number displayed on {0}?", "Numpath", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 9)]
    NumpathDigit,

    [SouvenirQuestion("Which of these was a contestant on {0}?", "Object Shows", TwoColumns4Answers, ExampleAnswers = new[] { "Battleship", "Big Circle", "Jack O’ Lantern", "Lego", "Moon", "Radio", "Combination Lock", "Cookie Jar", "Fidget Spinner" })]
    ObjectShowsContestants,

    [SouvenirQuestion("What was the starting sphere in {0}?", "The Octadecayotton", OneColumn4Answers, ExampleAnswers = new[] { "--+", "-+-+-++-+", "-++-+--+-", "+++-+-++-", "--++-++-+-++" })]
    OctadecayottonSphere,
    [SouvenirQuestion("What was one of the subrotations in the {1} rotation in {0}?", "The Octadecayotton", OneColumn4Answers, ExampleAnswers = new[] { "-X", "+Y-Z", "+U+V+W", "-R+S+T-O", "+P-Q-X+Y-Z" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    OctadecayottonRotations,

    [SouvenirQuestion("What was the button you pressed in the {1} stage of {0}?", "Odd One Out", TwoColumns4Answers, "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    OddOneOutButton,

    [SouvenirQuestion("Which of these keys played at an incorrect pitch in {0}?", "Off Keys", ThreeColumns6Answers, "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B")]
    OffKeysIncorrectPitch,
    [SouvenirQuestion("Which of these runes was displayed in {0}?", "Off Keys", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "OffKeysSprites")]
    OffKeysRunes,

    [SouvenirQuestion("What was the {1} of the numbers shown in {0}?", "Old AI", TwoColumns4Answers, "1", "2", "3", "4", "5",
        ExampleFormatArguments = new[] { "group", "sub-group" }, TranslateFormatArgs = new[] { true }, ExampleFormatArgumentGroupSize = 1)]
    OldAIGroup,

    [SouvenirQuestion("What was the initial color of the status light in {0}?", "Old Fogey", ThreeColumns6Answers, "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", "White", TranslateAnswers = true)]
    OldFogeyStartingColor,

    [SouvenirQuestion("What was the starting article in {0}?", "One Links To All", OneColumn4Answers,
         ExampleAnswers = new[] { "Waves (Jade Warrior album)", "Himali Siriwardena", "Campbell Pass", "1973 Northern Ireland Assembly election", "Bravo Airways", "Adolph Hoffmann", "Australian cyclists at the Tour de France", "Lebanese Canadians", "Albert Richard Pritchard", "Mary A. Lehman" })]
    OneLinksToAllStart,
    [SouvenirQuestion("What was the ending article in {0}?", "One Links To All", OneColumn4Answers,
         ExampleAnswers = new[] { "Bob Kitterman", "Johannes Nevala", "Alfred Patfield", "Dublin Bay South (Dáil constituency)", "The Monkees Present", "Finding Me", "Sibora", "Operator (linguistics)", "2022 Iowa Senate election", "Ab Dang Sar, Savadkuh" })]
    OneLinksToAllEnd,

    [SouvenirQuestion("Which Egyptian hieroglyph was in the {1} in {0}?", "Only Connect", TwoColumns4Answers, "Two Reeds", "Lion", "Twisted Flax", "Horned Viper", "Water", "Eye of Horus", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "top left", "top middle", "top right", "bottom left", "bottom middle", "bottom right" }, ExampleFormatArgumentGroupSize = 1)]
    OnlyConnectHieroglyphs,

    [SouvenirQuestion("What was the {1} arrow on the display of the {2} stage of {0}?", "Orange Arrows", TwoColumns4Answers, "Up", "Right", "Down", "Left", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    OrangeArrowsSequences,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Orange Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "FORMAL", "FREEZE", "GLANCE", "JACKED", "JAMMED", "JAMMER", "NECTAR", "NEEDED", "QUEENS", "UTOPIA" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    OrangeCipherScreen,

    [SouvenirQuestion("What color was this key in the {1} stage of {0}?", "Ordered Keys", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta", TranslateAnswers = true, UsesQuestionSprite = true,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    OrderedKeysColors,
    [SouvenirQuestion("What was the label of this key in the {1} stage of {0}?", "Ordered Keys", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", UsesQuestionSprite = true,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    OrderedKeysLabels,
    [SouvenirQuestion("What color was the label of this key in the {1} stage of {0}?", "Ordered Keys", ThreeColumns6Answers, "Red", "Blue", "Green", "Yellow", "Cyan", "Magenta", TranslateAnswers = true, UsesQuestionSprite = true,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    OrderedKeysLabelColors,

    [SouvenirQuestion("What was the order ID in the {1} order of {0}?", "Order Picking", ThreeColumns6Answers, ExampleAnswers = new[] { "3141", "7946", "6905", "6408", "5030", "2803", "6918", "6642", "4645", "4356", "2868", "1887" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1000, 9999)]
    OrderPickingOrder,
    [SouvenirQuestion("What was the product ID in the {1} order of {0}?", "Order Picking", ThreeColumns6Answers, ExampleAnswers = new[] { "3141", "7946", "6905", "6408", "5030", "2803", "6918", "6642", "4645", "4356", "2868", "1887" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1000, 9999)]
    OrderPickingProduct,
    [SouvenirQuestion("What was the pallet in the {1} order of {0}?", "Order Picking", ThreeColumns6Answers, "CHEP", "SIPPL", "SLPR", "EWHITE", "ECHEP", "ESIPPL", "ESLPR",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    OrderPickingPallet,

    [SouvenirQuestion("What was the observer’s initial position in {0}?", "Orientation Cube", TwoColumns4Answers, "front", "left", "back", "right", TranslateAnswers = true)]
    OrientationCubeInitialObserverPosition,

    [SouvenirQuestion("What was the observer’s initial position in {0}?", "Orientation Hypercube", TwoColumns4Answers, "front", "left", "back", "right", TranslateAnswers = true)]
    OrientationHypercubeInitialObserverPosition,
    [SouvenirQuestion("What was the initial colour of the {1} face in {0}?", "Orientation Hypercube", ThreeColumns6Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white",
         ExampleFormatArguments = new[] { "right", "left", "top", "bottom", "back", "front", "zag", "zig" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true }, TranslateAnswers = true)]
    OrientationHypercubeInitialFaceColour,

    [SouvenirQuestion("What was {1}’s {2} digit from the right in {0}?", "Palindromes", ThreeColumns6Answers, TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "X", QandA.Ordinal, "Y", QandA.Ordinal, "Z", QandA.Ordinal, "the screen", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    PalindromesNumbers,

    [SouvenirQuestion("What was the {1} digit in the order number on {0}?", "Papa’s Pizzeria", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 7)]
    PapasPizzeriaDigit,
    [SouvenirQuestion("What was the letter in the order number on {0}?", "Papa’s Pizzeria", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("ACQBJMSD")]
    PapasPizzeriaLetter,

    [SouvenirQuestion("What was shown on the display on {0}?", "Parity", ThreeColumns6Answers, ExampleAnswers = new[] { "A1", "B2", "C3", "D4", "E5", "F6" })]
    ParityDisplay,

    [SouvenirQuestion("What was the LED color in the {1} stage of {0}?", "Partial Derivatives", ThreeColumns6Answers, "blue", "green", "orange", "purple", "red", "yellow", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PartialDerivativesLedColors,
    [SouvenirQuestion("What was the {1} term in {0}?", "Partial Derivatives", TwoColumns4Answers,
        ExampleAnswers = new[] { "−5x⁴z³", "8x⁴z⁴", "4xy³z²", "−3x⁴z", "3x⁵y⁵z³" }, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PartialDerivativesTerms,

    [SouvenirQuestion("What was the passport expiration year of the {1} inspected passenger in {0}?", "Passport Control", ThreeColumns6Answers, ExampleAnswers = new[] { "1931", "1956", "1977", "1980", "1991", "2000", "2004", "2019", "2047" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PassportControlPassenger,

    [SouvenirQuestion("What was the 2FAST™ value when you solved {0}?", "Password Destroyer", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(100100, 999999)]
    PasswordDestroyerTwoFactorV2,

    [SouvenirQuestion("Which symbol was highlighted in {0}?", "Pattern Cube", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "PatternCubeSprites")]
    PatternCubeHighlightedSymbol,

    [SouvenirQuestion("What was the base colour in {0}?", "Pentabutton", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "White", AddThe = true, TranslateAnswers = true)]
    PentabuttonBaseColor,

    [SouvenirQuestion("What word was on the display in the {1} stage of {0}?", "Periodic Words", OneColumn4Answers, ExampleAnswers = new[] { "ATTACKERS", "BUY", "SUPERPOSITION", "WHO" }, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PeriodicWordsDisplayedWords,

    [SouvenirQuestion("What was the {1} color in the initial sequence in {0}?", "Perspective Pegs", ThreeColumns6Answers, "red", "yellow", "green", "blue", "purple", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PerspectivePegsColorSequence,

    [SouvenirQuestion("What was the offset in {0}?", "Phosphorescence", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 419)]
    PhosphorescenceOffset,
    [SouvenirQuestion("What was the {1} button press in {0}?", "Phosphorescence", ThreeColumns6Answers, new[] { "Azure", "Blue", "Crimson", "Diamond", "Emerald", "Fuchsia", "Green", "Hazel", "Ice", "Jade", "Kiwi", "Lime", "Magenta", "Navy", "Orange", "Purple", "Quartz", "Red", "Salmon", "Tan", "Ube", "Vibe", "White", "Xotic", "Yellow", "Zen" }, TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PhosphorescenceButtonPresses,

    [SouvenirQuestion("What pickup was shown in the {1} stage of {0}?", "Pickup Identification", ThreeColumns6Answers, Type = AnswerType.Sprites,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PickupIdentificationItem,

    [SouvenirQuestion("What was the code in {0}?", "Pictionary", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("0-579", "0-68", "0-7", "0-68")]
    PictionaryCode,

    [SouvenirQuestion("What was the {1} digit of the displayed number in {0}?", "Pie", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    PieDigits,

    [SouvenirQuestion("What number was not displayed in {0}?", "Pie Flash", TwoColumns4Answers, ExampleAnswers = new[] { "31415", "62643", "28410", "93105" })]
    PieFlashDigits,

    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Pigpen Cycle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PigpenCycleDialDirections,
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", "Pigpen Cycle", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    PigpenCycleDialLabels,

    [SouvenirQuestion("Which distance occurred in {0}?", "Pinpoint", ThreeColumns6Answers)]
    [AnswerGenerator.Concatenate(typeof(AnswerGenerator.Integers), new object[] { 0, 99 }, typeof(AnswerGenerator.Strings), new object[] { new string[] { ".", "0-9", "0-9", "0-9" } })]
    PinpointDistances,
    [SouvenirQuestion("Which point occurred in {0}?", "Pinpoint", ThreeColumns6Answers)]
    [AnswerGenerator.Concatenate(typeof(AnswerGenerator.Strings), new object[] { new string[] { "A-J" } }, typeof(AnswerGenerator.Integers), new object[] { 1, 10 })]
    PinpointPoints,

    [SouvenirQuestion("What was the {1} word in {0}?", "Pink Button", TwoColumns4Answers, "BLK", "RED", "GRN", "YLW", "BLU", "MGT", "CYN", "WHT",
        AddThe = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PinkButtonWords,
    [SouvenirQuestion("What was the {1} color in {0}?", "Pink Button", TwoColumns4Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white", TranslateAnswers = true,
        AddThe = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PinkButtonColors,

    [SouvenirQuestion("What was the keyword in {0}?", "Pixel Cipher", ThreeColumns6Answers, "HEART", "HAPPY", "HOUSE", "ARROW", "ARMOR", "ACORN", "CROSS", "CHORD", "CLOCK", "DONUT", "DELTA", "DUCKY", "EQUAL", "EMOJI", "EDGES", "LIBRA", "LUCKY", "LUNAR", "MEDAL", "MOVIE", "MUSIC", "PANDA", "PEARL", "PIANO", "PIXEL")]
    PixelCipherKeyword,

    [SouvenirQuestion("What was the first half of the first phrase in {0}?", "Placeholder Talk", TwoColumns4Answers, ExampleAnswers = new[] { "", "IS IN THE", "IS THE", "IS IN UH", "IS", "IS AT", "IS INN", "IS THE IN", "IN IS", "IS IN.", "IS IN", "THE", "FIRST-", "IN", "UH IS IN", "AT", "LAST-", "UH", "KEYBORD", "A" })]
    PlaceholderTalkFirstPhrase,
    [SouvenirQuestion("What was the last half of the first phrase in {0}?", "Placeholder Talk", TwoColumns4Answers, ExampleAnswers = new[] { "", "FIRST POS.", "SECOND POS.", "THIRD POS.", "FOURTH POS.", "FIFTH POS.", "MILLIONTH POS.", "BILLIONTH POS.", "LAST POS.", "AN ANSWER" })]
    PlaceholderTalkOrdinal,

    [SouvenirQuestion("What was the character listed on the information display in {0}?", "Placement Roulette", TwoColumns4Answers, "Baby Mario", "Baby Luigi", "Baby Peach", "Baby Daisy", "Toad", "Toadette", "Koopa Troopa", "Dry Bones", "Mario", "Luigi", "Peach", "Daisy", "Yoshi", "Birdo", "Diddy Kong", "Bowser Jr.", "Mii", "Wario", "Waluigi", "Donkey Kong", "Bowser", "King Boo", "Rosalina", "Funky Kong", "Dry Bowser")]
    PlacementRouletteChar,
    [SouvenirQuestion("What was the track listed on the information display in {0}?", "Placement Roulette", OneColumn4Answers, "Luigi Circuit", "Moo Moo Meadows", "Mushroom Gorge", "Toad's Factory", "Mario Circuit", "Coconut Mall", "DK Snowboard Cross", "Wario's Gold Mine", "Daisy Circuit", "Koopa Cape", "Maple Treeway", "Grumble Volcano", "Dry Dry Ruins", "Moonview Highway", "Bowser's Castle", "Rainbow Road", "GCN Peach Beach", "DS Yoshi Falls", "SNES Ghost Valley 2", "N64 Mario Raceway", "N64 Sherbet Land", "GBA Shy Guy Beach", "DS Delfino Square", "GCN Waluigi Stadium", "DS Desert Hills", "GBA Bowser Castle 3", "N64 DK's Jungle Parkway", "GCN Mario Circuit", "SNES Mario Circuit 3", "DS Peach Gardens", "GCN DK Mountain", "N64 Bowser's Castle")]
    PlacementRouletteTrack,
    [SouvenirQuestion("What was the vehicle listed on the information display in {0}?", "Placement Roulette", OneColumn4Answers, "Standard Kart S", "Baby Booster", "Concerto", "Cheep Charger", "Rally Romper", "Blue Falcon", "Standard Bike S", "Bullet Bike", "Nanobike", "Quacker", "Magikruiser", "Bubble Bike", "Standard Kart M", "Nostalgia 1", "Wild Wing", "Turbo Blooper", "Royal Racer", "B Dasher Mk. 2", "Standard Bike M", "Mach Bike", "Bon Bon", "Rapide", "Nitrocycle", "Dolphin Dasher", "Standard Kart L", "Offroader", "Flame Flyer", "Piranha Prowler", "Jetsetter", "Honeycoupe", "Standard Bike L", "Bowser Bike", "Wario Bike", "Twinkle Star", "Torpedo", "Phantom")]
    PlacementRouletteVehicle,

    [SouvenirQuestion("What was the planet shown in {0}?", "Planets", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "PlanetsSprites")]
    PlanetsPlanet,
    [SouvenirQuestion("What was the color of the {1} strip (from the top) in {0}?", "Planets", ThreeColumns6Answers, "Aqua", "Blue", "Green", "Lime", "Orange", "Red", "Yellow", "White", "Off", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PlanetsStrips,

    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Playfair Cycle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleFiveSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PlayfairCycleDialDirections,
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", "Playfair Cycle", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    PlayfairCycleDialLabels,

    [SouvenirQuestion("What was the {1} correct answer you pressed in {0}?", "Poetry", TwoColumns4Answers, "clarity", "flow", "fatigue", "hollow", "energy", "sunshine", "ocean", "reflection", "identity", "black", "crowd", "heart", "weather", "words", "past", "solitary", "relax", "dance", "weightless", "morality", "gaze", "failure", "bunny", "lovely", "romance", "future", "focus", "search", "cookies", "compassion", "creation", "patience",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PoetryAnswers,

    [SouvenirQuestion("What color flashed {1} in {0}?", "Pointless Machines", TwoColumns4Answers, "White", "Purple", "Red", "Blue", "Yellow",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    PointlessMachinesFlashes,

    [SouvenirQuestion("Which polygon was present on {0}?", "Polygons", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "PolygonsSprites")]
    PolygonsPolygon,

    [SouvenirQuestion("What was the starting position in {0}?", "Polyhedral Maze", ThreeColumns6Answers,
        TranslatableStrings = new[] { "the 4-truncated deltoidal icositetrahedral Polyhedral Maze", "the chamfered dodecahedral Polyhedral Maze", "the chamfered icosahedral Polyhedral Maze", "the deltoidal hexecontahedral Polyhedral Maze", "the disdyakis dodecahedral Polyhedral Maze", "the joined snub cubic Polyhedral Maze", "the joined rhombicuboctahedral Polyhedral Maze", "the pentagonal hexecontahedral Polyhedral Maze", "the orthokis propello cubic Polyhedral Maze", "the pentakis dodecahedral Polyhedral Maze", "the rectified rhombicuboctahedral Polyhedral Maze", "the triakis icosahedral Polyhedral Maze", "the rhombicosidodecahedral Polyhedral Maze", "the canonical rectified snub cubic Polyhedral Maze" })]
    [AnswerGenerator.Integers(0, 61)]
    PolyhedralMazeStartPosition,

    [SouvenirQuestion("What was the number shown in {0}?", "Prime Encryption", ThreeColumns6Answers, ExampleAnswers = new[] { "1147", "1271", "1333", "1457", "1643", "1829" })]
    PrimeEncryptionDisplayedValue,

    [SouvenirQuestion("Which cell did the prisoner start in in {0}?", "Prison Break", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 15)]
    PrisonBreakPrisoner,
    [SouvenirQuestion("Where did you start in {0}?", "Prison Break", ThreeColumns6Answers)]
    [AnswerGenerator.Concatenate(typeof(AnswerGenerator.Strings), new object[] { 'A', 'L' }, typeof(AnswerGenerator.Integers), new object[] { 1, 12 })]
    PrisonBreakDefuser,

    [SouvenirQuestion("What was the missing frequency in the {1} wire in {0}?", "Probing", TwoColumns4Answers, "10Hz", "22Hz", "50Hz", "60Hz", TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red-white", "yellow-black", "green", "gray", "yellow-red", "red-blue" }, ExampleFormatArgumentGroupSize = 1)]
    ProbingFrequencies,

    [SouvenirQuestion("What was the initial seed in {0}?", "Procedural Maze", TwoColumns4Answers)]
    [AnswerGenerator.Strings("6*0-1")]
    ProceduralMazeInitialSeed,

    [SouvenirQuestion("What was the displayed number in {0}?", "...?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99, "00")]
    PunctuationMarksDisplayedNumber,

    [SouvenirQuestion("What was the target word on {0}?", "Purple Arrows", ThreeColumns6Answers, ExampleAnswers = new[] { "Thesis", "Immune", "Agency", "Height", "Active", "Bother", "Viable" })]
    PurpleArrowsFinish,

    [SouvenirQuestion("What was the {1} number in the cyclic sequence on {0}?", "Purple Button", ThreeColumns6Answers, AddThe = true, ExampleAnswers = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PurpleButtonNumbers,

    [SouvenirQuestion("What was the {1} puzzle number in {0}?", "Puzzle Identification", ThreeColumns6Answers, ExampleAnswers = new[] { "001", "002", "003", "004", "005", "006" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 170, 1, "000")]
    PuzzleIdentificationNum,
    [SouvenirQuestion("What game was the {1} puzzle in {0} from?", "Puzzle Identification", OneColumn4Answers, "Professor Layton and the Curious Village", "Professor Layton and Pandora's Box", "Professor Layton and the Lost Future", "Professor Layton and the Spectre's Call", "Professor Layton and the Miracle Mask", "Professor Layton and the Azran Legacy", "Layton's Mystery Journey: Katrielle and the Millionaire's Conspiracy", "Professor Layton vs. Phoenix Wright: Ace Attorney",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    PuzzleIdentificationGame,
    [SouvenirQuestion("What was the {1} puzzle in {0}?", "Puzzle Identification", OneColumn4Answers, ExampleAnswers = new[] { "Where's the Village?", "Dr Schrader's Map", "A Party Crasher", "A Secret Message" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    PuzzleIdentificationName,

    [SouvenirQuestion("What letter was displayed on the {1} hexabutton when submitting in {0}?", "Puzzling Hexabuttons", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "top-left", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Strings('A', 'F')]
    PuzzlingHexabuttonsLetter,

    [SouvenirQuestion("What was the {1} question asked in {0}?", "Q & A", ThreeColumns6Answers, "WHAT", "WHEN", "WHERE", "WHO", "HOW", "WHY",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    QnAQuestions,

    [SouvenirQuestion("What was on the {1} button of the {2} stage in {0}?", "Quadrants", ThreeColumns6Answers, "1", "2", "3", "4", "+", "-",
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    QuadrantsButtons,

    [SouvenirQuestion("Which word was used in {0}?", "Quantum Passwords", ThreeColumns6Answers, "Argue", "Blaze", "Cajun", "Depth", "Endow", "Foyer", "Gimpy", "Heavy", "Index", "Joker", "Kylix", "Lambs", "Mercy", "Nifty", "Omens", "Pupil", "Risky", "Stoic", "Taboo", "Unbox", "Viced", "Waltz", "Xerus", "Yuzus", "Zilch")]
    QuantumPasswordsWord,

    [SouvenirQuestion("Which number was shown in {0}?", "Quantum Ternary Converter", TwoColumns4Answers)]
    [AnswerGenerator.Integers(-265720, -9842)]
    [AnswerGenerator.Integers(9842, 265720)]
    QuantumTernaryConverterNumber,

    [SouvenirQuestion("What was the {1} sequence’s answer in {0}?", "Quaver", OneColumn4Answers, ExampleAnswers = new[] { "4", "10", "87", "320", "3, 3, 2, 3", "87, 85, 82, 84" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    QuaverArrows,

    [SouvenirQuestion("Which of these symbols was part of the flashing sequence in {0}?", "Question Mark", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "QuestionMarkSprites")]
    QuestionMarkFlashedSymbols,

    [SouvenirQuestion("What was the {1} color in the primary sequence in {0}?", "Quick Arithmetic", ThreeColumns6Answers, "red", "blue", "green", "yellow", "white", "black", "orange", "pink", "purple", "cyan", "brown", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    QuickArithmeticColors,
    [SouvenirQuestion("What was the {1} digit in the {2} sequence in {0}?", "Quick Arithmetic", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal, "primary", QandA.Ordinal, "secondary" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { false, true })]
    [AnswerGenerator.Integers(0, 9)]
    QuickArithmeticPrimSecDigits,

    [SouvenirQuestion("What was the {1} digit in the {2} slot in {0}?", "Quintuples", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    [AnswerGenerator.Integers(0, 9)]
    QuintuplesNumbers,
    [SouvenirQuestion("What color was the {1} digit in the {2} slot in {0}?", "Quintuples", TwoColumns4Answers, "red", "blue", "orange", "green", "pink", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    QuintuplesColors,
    [SouvenirQuestion("How many numbers were {1} in {0}?", "Quintuples", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "blue", "orange", "green", "pink" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 25)]
    QuintuplesColorCounts,

    [SouvenirQuestion("What number was shown on {0}?", "Quiplash", ThreeColumns6Answers, "101", "102", "103", "107", "109", "127", "128", "130", "148", "149", "15", "154", "160", "162", "18", "181", "183", "189", "190", "191", "194", "196", "200", "204", "209", "21", "215", "220", "23", "243", "244", "246", "249", "250", "252", "253", "255", "257", "260", "263", "267", "268", "27", "271", "275", "277", "28", "288", "296", "309", "313", "316", "32", "323", "324", "326", "336", "340", "341", "348", "351", "352", "353", "359", "36", "360", "369", "370", "372", "373", "374", "382", "386", "391", "40", "407", "412", "415", "42", "43", "431", "441", "444", "445", "50", "507", "510", "515", "525", "526", "527", "53", "530", "532", "533", "536", "538", "54", "540", "542", "545", "547", "548", "551", "557", "562", "565", "566", "57", "572", "574", "58", "581", "582", "62", "63", "64", "68001", "68005", "68014", "68016", "68024", "68029", "68030", "68031", "68033", "68037", "68042", "68043", "68045", "68053", "68056", "68072", "68074", "68076", "68081", "68087", "68092", "68097", "68103", "68108", "68112", "68121", "68127", "68131", "68135", "68136", "68137", "68174", "68182", "68185", "68214", "68227", "68233", "68274", "68278", "68295", "68298", "68310", "68329", "68339", "68369", "68374", "68375", "68405", "68407", "68419", "68428", "68430", "68434", "68436", "68452", "68456", "68458", "68459", "68470", "68475", "68478", "68481", "68482", "68487", "68492", "68494", "68496", "68497", "68507", "68510", "68517", "68519", "68523", "68528", "68531", "68532", "68534", "68540", "68541", "68542", "68543", "68544", "68545", "68546", "68552", "68555", "68556", "68557", "68559", "68561", "68562", "68564", "68565", "68568", "68570", "68571", "68574", "68575", "68579", "68580", "68581", "68583", "68584", "68585", "68590", "68592", "68593", "68595", "68597", "68603", "68605", "68607", "68610", "68615", "68618", "68619", "68622", "68624", "68627", "68629", "68630", "68636", "68638", "68639", "68641", "68643", "68645", "68653", "68655", "68656", "68659", "68661", "68663", "68666", "68668", "68677", "68710", "68714", "68739", "68756", "68762", "68798", "68799", "68805", "68812", "68818", "68819", "68827", "68829", "68835", "68836", "68838", "68842", "68843", "68846", "68865", "68882", "68883", "68884", "68887", "68891", "68896", "68899", "68914", "68917", "68923", "68926", "68928", "68930", "68932", "68934", "68943", "68954", "68955", "68965", "68973", "68974", "68975", "68995", "68996", "68999", "69000", "69003", "69004", "69006", "69008", "69012", "69014", "69017", "69019", "69023", "69024", "69026", "69034", "69035", "69038", "69044", "69055", "69073", "69076", "69087", "69175", "69176", "69177", "69180", "69229", "69241", "69245", "69248", "69308", "69319", "69322", "69323", "69324", "69328", "69329", "69331", "69336", "69340", "69352", "69356", "69360", "69362", "69378", "69379", "69383", "69391", "69393", "69398", "69401", "69408", "69411", "69432", "69433", "69448", "69449", "69454", "69458", "69459", "69467", "69477", "69478", "69506", "69508", "69509", "69516", "69517", "69523", "69525", "69798", "69800", "69804", "69812", "69829", "69836", "69851", "69855", "69857", "69865", "69866", "69868", "69872", "69873", "69876", "69878", "69885", "69891", "69900", "69903", "69905", "69906", "69909", "69913", "69921", "69923", "69926", "69928", "69929", "69932", "69945", "69947", "69968", "69971", "69973", "69977", "69979", "69981", "70", "70004", "70005", "70007", "70014", "70018", "70029", "70031", "70068", "70080", "70109", "70156", "70161", "70166", "70168", "70244", "70560", "70723", "71100", "71136", "71231", "71329", "72", "75", "81", "87", "92")]
    QuiplashNumber,

    [SouvenirQuestion("What was the number initially on the display in {0}?", "Quiz Buzz", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(6, 74)]
    QuizBuzzStartingNumber,

    [SouvenirQuestion("What tile did you place {1} in {0}?", "Qwirkle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "QwirkleSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    QwirkleTilesPlaced,

    [SouvenirQuestion("How many jewels were in the starting common pool in {0}?", "Raiding Temples", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 10)]
    RaidingTemplesStartingCommonPool,

    [SouvenirQuestion("What was the {1} car in {0}?", "Railway Cargo Loading", TwoColumns4Answers, Type = AnswerType.Sprites,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    RailwayCargoLoadingCars,
    [SouvenirQuestion("Which freight table rule {1} in {0}?", "Railway Cargo Loading", OneColumn4Answers, "Over 150 lumber/75 logs", "Over 100 sheet metal", "Over 250 crude oil", "Over 400 mail", "Over 30 livestock", "Over 600 milk/water/resin", "Over 100 liquid fuel", "Over 700 industrial gas", "Over 150 food", "Over 100 coal", "Over 500 loose bulk (excl. coal)", "Over 7 large objects", "Over 5 automobiles", "Over 700 industrial gas",
        ExampleFormatArguments = new[] { "was met", "wasn’t met" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    RailwayCargoLoadingFreightTableRules,

    [SouvenirQuestion("What was the displayed number in {0}?", "Rainbow Arrows", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 99, "00")]
    RainbowArrowsNumber,

    [SouvenirQuestion("What was the color of the {1} LED in {0}?", "Recolored Switches", TwoColumns4Answers, "red", "green", "blue", "cyan", "orange", "purple", "white", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    RecoloredSwitchesLedColors,

    [SouvenirQuestion("Which of these words appeared, but was not the password, in {0}?", "Recursive Password", ThreeColumns6Answers, ExampleAnswers = new[] { "Abyss", "Ingot", "Nonce", "Whelk", "Obeys", "Lobed" })]
    RecursivePasswordNonPasswordWords,
    [SouvenirQuestion("What was the password in {0}?", "Recursive Password", ThreeColumns6Answers, ExampleAnswers = new[] { "Abyss", "Ingot", "Nonce", "Whelk", "Obeys", "Lobed" })]
    RecursivePasswordPassword,

    [SouvenirQuestion("What was the starting number in {0}?", "Red Arrows", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    RedArrowsStartNumber,

    [SouvenirQuestion("What was the word before “SUBMIT” in {0}?", "Red Button’t", TwoColumns4Answers, AddThe = false,
        ExampleAnswers = new[] { "ABACUS", "BABBLE", "CABLES", "DABBLE", "EAGLES", "FABLED", "HABITS", "IAMBIC" })]
    RedButtontWord,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Red Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "EATING", "GOBLET", "INCOME", "INSIDE", "MARKED", "POWDER", "STRING", "WIZARD", "WOBBLE", "YELLOW" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    RedCipherScreen,

    [SouvenirQuestion("What was the first color flashed by {0}?", "Red Herring", TwoColumns4Answers, "Green", "Blue", "Purple", "Orange")]
    RedHerringFirstFlash,

    [SouvenirQuestion("Which condition was the solving condition in {0}?", "Reformed Role Reversal", ThreeColumns6Answers, "second", "third", "4th", "5th", "6th", "7th", "8th", TranslateAnswers = true)]
    ReformedRoleReversalCondition,
    [SouvenirQuestion("What color was the {1} wire in {0}?", "Reformed Role Reversal", ThreeColumns6Answers, "Navy", "Lapis", "Blue", "Sky", "Teal", "Plum", "Violet", "Purple", "Magenta", "Lavender", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ReformedRoleReversalWire,

    [SouvenirQuestion("Which calculation was used for the {1} stage of {0}?", "ReGret-B Filtering", ThreeColumns6Answers, "+", "×", "÷", "⊻", "∧", "∨",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ReGretBFilteringOperator,

    [SouvenirQuestion("What was the displayed digit that corresponded to the solution phrase in {0}?", "Regular Crazy Talk", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    RegularCrazyTalkDigit,
    [SouvenirQuestion("What was the embellishment of the solution phrase in {0}?", "Regular Crazy Talk", OneColumn4Answers, "[PHRASE]", "It says: [PHRASE]", "Quote: [PHRASE] End quote", "“[PHRASE]”", "It says: “[PHRASE]”", "“It says: [PHRASE]”", TranslateAnswers = true)]
    RegularCrazyTalkModifier,

    [SouvenirQuestion("Which key was the pivot in the {1} stage of {0}?", "Reordered Keys", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "OrderedKeysSprites")]
    ReorderedKeysPivot,
    [SouvenirQuestion("What color was this key in the {1} stage of {0}?", "Reordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
        UsesQuestionSprite = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    ReorderedKeysKeyColor,
    [SouvenirQuestion("What color was the label of this key in the {1} stage of {0}?", "Reordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow",
        UsesQuestionSprite = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    ReorderedKeysLabelColor,
    [SouvenirQuestion("What was the label of this key in the {1} stage of {0}?", "Reordered Keys", ThreeColumns6Answers,
        UsesQuestionSprite = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    ReorderedKeysLabel,

    [SouvenirQuestion("Which one of these houses was on offer, but not chosen by Bob in {0}?", "Retirement", TwoColumns4Answers, ExampleAnswers = new[] { "Hotham Place", "Homestead", "Riverwell", "Lodge Park" })]
    RetirementHouses,

    [SouvenirQuestion("What was the {1} character in the {2} message of {0}?", "Reverse Morse", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    [AnswerGenerator.Strings("A-Z0-9")]
    ReverseMorseCharacters,

    [SouvenirQuestion("What character was used in the {1} round of {0}?", "Reverse Polish Notation", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-G0-9")]
    ReversePolishNotationCharacter,

    [SouvenirQuestion("What was the exit coordinate in {0}?", "RGB Maze", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("A-H", "1-8")]
    RGBMazeExit,
    [SouvenirQuestion("Where was the {1} key in {0}?", "RGB Maze", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-H", "1-8")]
    RGBMazeKeys,
    [SouvenirQuestion("Which maze number was the {1} maze in {0}?", "RGB Maze", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    RGBMazeNumber,

    [SouvenirQuestion("What was the color of the {1} LED in {0}?", "RGB Sequences", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Cyan", "Yellow", "White", TranslateAnswers = true,
    ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    RGBSequencesDisplay,

    [SouvenirQuestion("What was the color in {0}?", "Rhythms", TwoColumns4Answers, "Blue", "Red", "Green", "Yellow", TranslateAnswers = true)]
    RhythmsColor,

    [SouvenirQuestion("Which bit had a tap in {0} (the output after shifting is at bit 0)?", "RNG Crystal", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 23)]
    RNGCrystalTaps,

    [SouvenirQuestion("Where was the empty cell in {0}?", "Robo-Scanner", ThreeColumns6Answers, "A1", "A2", "A3", "A4", "A5", "B1", "B2", "B3", "B4", "B5", "C1", "C2", "C4", "C5", "D1", "D2", "D3", "D4", "D5", "E1", "E2", "E3", "E4", "E5")]
    RoboScannerEmptyCell,

    [SouvenirQuestion("What was the color of the {1} robot in {0}?", "Robot Programming", TwoColumns4Answers, "Blue", "Green", "Red", "Yellow",
    ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    RobotProgrammingColor,
    [SouvenirQuestion("What was the shape of the {1} robot in {0}?", "Robot Programming", TwoColumns4Answers, "Triangle", "Square", "Hexagon", "Circle",
    ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    RobotProgrammingShape,

    [SouvenirQuestion("What four-digit number was given in {0}?", "Roger", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9999, "0000")]
    RogerSeed,

    [SouvenirQuestion("What was the number corresponding to the correct condition in {0}?", "Role Reversal", ThreeColumns6Answers, "2", "3", "4", "5", "6", "7", "8")]
    RoleReversalNumber,
    [SouvenirQuestion("How many {1} wires were there in {0}?", "Role Reversal", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7",
        ExampleFormatArguments = new[] { "warm-colored", "cold-colored", "primary-colored", "secondary-colored" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    RoleReversalWires,

    [SouvenirQuestion("Which round did the {1} team {2} in {0}?", "RPS Judging", ThreeColumns6Answers, ExampleAnswers = new[] { "first", "second", "third", "fourth", "fifth", "sixth" },
        ExampleFormatArguments = new[] { "red", "win", "blue", "win", "red", "lose", "blue", "lose" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, true },
        TranslatableStrings = new[] { "the RPS Judging where the {0} team {1} the {2} round", "won", "lost", "the RPS Judging with a draw in the {0} round" })]
    RPSJudgingWinner,
    [SouvenirQuestion("Which round was a draw in {0}?", "RPS Judging", ThreeColumns6Answers, ExampleAnswers = new[] { "first", "second", "third", "fourth", "fifth", "sixth" })]
    RPSJudgingDraw,

    [SouvenirQuestion("What was the rule number in {0}?", "Rule", ThreeColumns6Answers, AddThe = true)]
    [AnswerGenerator.Integers(0, 15)]
    RuleNumber,

    [SouvenirQuestion("What was the {1} coordinate of the {2} vertex in {0}?", "Rule of Three", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "X", "red", "Y", "yellow", "Z", "blue" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { false, true })]
    [AnswerGenerator.Integers(-13, 13)]
    RuleOfThreeCoordinates,
    [SouvenirQuestion("What was the position of the {1} sphere on the {2} axis in the {3} cycle in {0}?", "Rule of Three", TwoColumns4Answers, "-", "0", "+", TranslateFormatArgs = new[] { true, false, false },
        ExampleFormatArguments = new[] { "red", "X", QandA.Ordinal, "yellow", "Y", QandA.Ordinal, "blue", "Z", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 3)]
    RuleOfThreeCycles,

    [SouvenirQuestion("What was the digit displayed on the {1} diamond in {0}?", "Safety Square", TwoColumns4Answers, ExampleFormatArguments = new[] { "red", "yellow", "blue" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(0, 4)]
    SafetySquareDigits,
    [SouvenirQuestion("What was the special rule displayed on the white diamond in {0}?", "Safety Square", OneColumn4Answers, "No special rule", "Reacts with water", "Simple asphyxiant", "Oxidizer", TranslateAnswers = true)]
    SafetySquareSpecialRule,

    [SouvenirQuestion("Where was {1} in {0}?", "Samsung", ThreeColumns6Answers, "TL", "TM", "TR", "ML", "MM", "MR", "BL", "BM", "BR", AddThe = true, TranslateAnswers = true,
        ExampleFormatArguments = new[] { "Duolingo", "Google Maps", "Kindle", "Google Authenticator", "Photomath", "Spotify", "Google Arts & Culture", "Discord" }, ExampleFormatArgumentGroupSize = 1)]
    SamsungAppPositions,

    [SouvenirQuestion("Where was the goal in {0}?", "Saturn", ThreeColumns6Answers)]
    [AnswerGenerator.Concatenate(typeof(AnswerGenerator.Strings), new object[] { new string[] { "0-9", " " } }, typeof(AnswerGenerator.Integers), new object[] { 0, 63 })]
    SaturnGoal,

    [SouvenirQuestion("What was the displayed song for stage {1} (hexadecimal) of {0}?", "Sbemail Songs", OneColumn4Answers, ExampleAnswers = new[] { "Oh, who is the guy that…", "I'm gonna check my email all of the time…", "Checkin' my email, checkin' my email…", "I check the email once…", "Checkin' emails is like the best thing I do.", "I check, you check, we all check…", "I am going to check my email.", "I remember the time when I checked my email.", "I've carefully set aside this time…", "I'm totally checking my email…" },
        ExampleFormatArguments = new[] { "01", "02" }, ExampleFormatArgumentGroupSize = 1,
        TranslatableStrings = new[] { "the Sbemail Songs which displayed ‘{0}’ in stage {1} (hexadecimal)" })]
    SbemailSongsSongs,

    [SouvenirQuestion("Which tile was correctly submitted in the first stage of {0}?", "Scavenger Hunt", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    ScavengerHuntKeySquare,
    [SouvenirQuestion("Which of these tiles was {1} in the first stage of {0}?", "Scavenger Hunt", ThreeColumns6Answers, Type = AnswerType.Sprites, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1)]
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

    [SouvenirQuestion("What was the {1} encrypted word in {0}?", "Scramboozled Eggain", ThreeColumns6Answers,
        ExampleAnswers = new[] { "Basted", "Boiled", "Boxing", "Carton", "Dumpty", "French" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ScramboozledEggainWord,

    [SouvenirQuestion("What was the submitted data type of the variable in {0}?", "Scripting", TwoColumns4Answers, "int", "bool", "float", "char")]
    ScriptingVariableDataType,

    [SouvenirQuestion("What was the modified property of the first display in {0}?", "Scrutiny Squares", OneColumn4Answers, "Word", "Color around word", "Color of background", "Color of word", TranslateAnswers = true)]
    ScrutinySquaresFirstDifference,

    [SouvenirQuestion("What were the first and second words in the {1} phrase in {0}?", "Sea Shells", TwoColumns4Answers, "she sells", "she shells", "sea shells", "sea sells",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SeaShells1,
    [SouvenirQuestion("What were the third and fourth words in the {1} phrase in {0}?", "Sea Shells", TwoColumns4Answers, "sea shells", "she shells", "sea sells", "she sells",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SeaShells2,
    [SouvenirQuestion("What was the end of the {1} phrase in {0}?", "Sea Shells", TwoColumns4Answers, "sea shore", "she sore", "she sure", "seesaw",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SeaShells3,

    [SouvenirQuestion("What was the {1} letter involved in the starting value in {0}?", "Semamorse", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
        ExampleFormatArguments = new[] { "Morse", "semaphore" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    SemamorseLetters,
    [SouvenirQuestion("What was the color of the display involved in the starting value in {0}?", "Semamorse", TwoColumns4Answers, "red", "green", "cyan", "indigo", "pink", TranslateAnswers = true)]
    SemamorseColor,

    [SouvenirQuestion("What sequence was used in {0}?", "Sequencyclopedia", TwoColumns4Answers, ExampleAnswers = new[] { "A000001", "A069420", "A111111" }, AddThe = true)]
    [AnswerGenerator.Integers(0, 1000000, "'A'000000")]
    SequencyclopediaSequence,

    [SouvenirQuestion("What equation was shown in the {1} stage of {0}?", "S.E.T. Theory", OneColumn4Answers,
        ExampleAnswers = new[] { "(A ∩ B)", "(A ∪ B)", "(!B ∆ !A)", "(B ∩ !A)", "(!(C − B) ∪ !A)", "((B ∩ A) − C)", "(!(B ∪ A) ∆ (C ∩ !B))", "((A − !C) ∩ !(B ∪ !C))" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SetTheoryEquations,

    [SouvenirQuestion("What was the initial letter in {0}?", "Shapes And Bombs", ThreeColumns6Answers, "A", "B", "D", "E", "G", "I", "K", "L", "N", "O", "P", "S", "T", "X", "Y")]
    ShapesAndBombsInitialLetter,

    [SouvenirQuestion("What was the initial shape in {0}?", "Shape Shift", TwoColumns4Answers, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings('A', 'P')]
    ShapeShiftInitialShape,

    [SouvenirQuestion("What color was the {1} marker in {0}?", "Shifted Maze", TwoColumns4Answers, "White", "Blue", "Yellow", "Magenta", "Green", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    ShiftedMazeColors,

    [SouvenirQuestion("What was the seed in {0}?", "Shifting Maze", TwoColumns4Answers)]
    [AnswerGenerator.Strings(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/")]
    ShiftingMazeSeed,

    [SouvenirQuestion("What was the displayed piece in {0}?", "Shogi Identification", TwoColumns4Answers, "Go-Between", "Pawn", "Side Mover", "Vertical Mover", "Bishop", "Rook", "Dragon Horse", "Dragon King", "Lance", "Reverse Chariot", "Blind Tiger", "Ferocious Leopard", "Copper General", "Silver General", "Gold General", "Drunk Elephant", "Kirin", "Phoenix", "Queen", "Flying Stag", "Flying Ox", "Free Boar", "Whale", "White Horse", "King", "Prince", "Horned Falcon", "Soaring Eagle", "Lion", TranslateAnswers = true)]
    ShogiIdentificationPiece,

    [SouvenirQuestion("What was the deciphered word in {0}?", "Sign Language", TwoColumns4Answers,
        "PHALANX", "DIGITAL", "ACHIRAL", "DEAFENS", "LISTENS", "EXPLAIN", "SPEAKER", "TURTLES", "QUOTING", "MISTAKE", "REALIZE", "HELPERS", "HEARING", "STROKES", "OVERJOY", "ROYALTY", "EARDRUM", "COCHLEA", "AUDIBLE", "KABOOMS", "REFUGEE", "SWINGER", "BALANCE", "LIQUIDS", "VOYAGED")]
    SignLanguageWord,

    [SouvenirQuestion("What was the {1} slot in the {2} stage in {0}?", "Silly Slots", TwoColumns4Answers, "red bomb", "red cherry", "red coin", "red grape", "green bomb", "green cherry", "green coin", "green grape", "blue bomb", "blue cherry", "blue coin", "blue grape", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SillySlots,

    [SouvenirQuestion("What was the message type in {0}?", "Silo Authorization", TwoColumns4Answers, "Red-Alpha", "Yellow-Alpha", "Green-Alpha")]
    SiloAuthorizationMessageType,
    [SouvenirQuestion("What was the {1} part of the encrypted message in {0}?", "Silo Authorization", ThreeColumns6Answers, ExampleAnswers = new[] { "A1B2", "BC84", "QW47", "B420", "AFS2", "FUN9" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings(4, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")]
    SiloAuthorizationEncryptedMessage,
    [SouvenirQuestion("What was the received authentication code in {0}?", "Silo Authorization", ThreeColumns6Answers, ExampleAnswers = new[] { "1234", "5678", "1357", "2468", "0001", "9999" })]
    [AnswerGenerator.Integers(0, 9999, "0000")]
    SiloAuthorizationAuthCode,

    [SouvenirQuestion("What color flashed {1} in the final sequence of {0}?", "Simon Said", TwoColumns4Answers, "Red", "Green", "Blue", "Yellow", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonSaidFlashes,

    [SouvenirQuestion("What were the call samples {1} of {0}?", "Simon Samples", TwoColumns4Answers, AudioFieldName = "SimonSamplesAudio", Type = AnswerType.Audio,
        TranslateFormatArgs = new[] { true }, ExampleFormatArguments = new[] { "played in the first stage", "added in the second stage", "added in the third stage" }, ExampleFormatArgumentGroupSize = 1)]
    SimonSamplesSamples,

    [SouvenirQuestion("What color flashed {1} in the final sequence in {0}?", "Simon Says", TwoColumns4Answers, "red", "yellow", "green", "blue", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonSaysFlash,

    [SouvenirQuestion("What color flashed {1} in {0}?", "Simon Scrambles", TwoColumns4Answers, "Red", "Green", "Blue", "Yellow", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonScramblesColors,

    [SouvenirQuestion("Which color flashed {1} in the final sequence in {0}?", "Simon Screams", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonScreamsFlashing,
    [SouvenirQuestion("In which stage(s) of {0} was “{1}” the applicable rule?", "Simon Screams", TwoColumns4Answers, "first", "second", "third", "first and second", "first and third", "second and third", "all of them",
        TranslateAnswers = true, TranslateFormatArgs = new[] { true }, ExampleFormatArguments = new[] {
            "a color flashed, then a color two away, then the first again",
            "a color flashed, then a color two away, then the one opposite that",
            "a color flashed, then a color two away, then the one opposite the first",
            "a color flashed, then an adjacent color, then the first again",
            "a color flashed, then another color, then the first",
            "a color flashed, then one adjacent, then the one opposite that",
            "a color flashed, then one adjacent, then the one opposite the first",
            "a color flashed, then the one opposite, then one adjacent to that",
            "a color flashed, then the one opposite, then one adjacent to the first",
            "a color flashed, then the one opposite, then the first again",
            "every color flashed at least once",
            "exactly one color flashed exactly twice",
            "exactly one color flashed more than once",
            "exactly two colors flashed exactly twice",
            "exactly two colors flashed more than once",
            "no color flashed exactly twice",
            "no color flashed more than once",
            "no two adjacent colors flashed in clockwise order",
            "no two adjacent colors flashed in counter-clockwise order",
            "no two colors two apart flashed in clockwise order",
            "no two colors two apart flashed in counter-clockwise order",
            "the colors flashing first and last are adjacent",
            "the colors flashing first and last are different and not adjacent",
            "the colors flashing first and last are the same",
            "the number of distinct colors that flashed is even",
            "the number of distinct colors that flashed is odd",
            "there are at least three colors that didn’t flash",
            "there are exactly two colors that didn’t flash",
            "there are two colors adjacent to each other that didn’t flash",
            "there are two colors opposite each other that didn’t flash",
            "there are two colors two away from each other that didn’t flash",
            "there is exactly one color that didn’t flash",
            "three adjacent colors did not flash",
            "three adjacent colors flashed in clockwise order",
            "three adjacent colors flashed in counter-clockwise order",
            "three colors, each two apart, flashed in clockwise order",
            "three colors, each two apart, flashed in counter-clockwise order",
            "two adjacent colors flashed in clockwise order",
            "two adjacent colors flashed in counter-clockwise order",
            "two colors two apart flashed in clockwise order",
            "two colors two apart flashed in counter-clockwise order"
        }, ExampleFormatArgumentGroupSize = 1)]
    SimonScreamsRuleSimple,
    [SouvenirQuestion("In which stage(s) of {0} was “{1} flashed out of {2}, {3}, and {4}” the applicable rule?", "Simon Screams", TwoColumns4Answers,
        "first", "second", "third", "first and second", "first and third", "second and third", "all of them",
        TranslateAnswers = true, TranslateFormatArgs = new[] { true, true, true, true },
        ExampleFormatArguments = new[] {
            "at most one color", "Red", "Orange", "Yellow",
            "at least two colors", "Green", "Blue", "Purple"
        }, ExampleFormatArgumentGroupSize = 4)]
    SimonScreamsRuleComplex,

    [SouvenirQuestion("Which color flashed {1} in the {2} stage of {0}?", "Simon Selects", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Magenta", "Cyan", TranslateAnswers = true,
         ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SimonSelectsOrder,

    [SouvenirQuestion("What was the {1} received letter in {0}?", "Simon Sends", ThreeColumns6Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    SimonSendsReceivedLetters,

    [SouvenirQuestion("Who flashed {1} in course {2} of {0}?", "Simon Serves", ThreeColumns6Answers, "Riley", "Brandon", "Gabriel", "Veronica", "Wendy", "Kayle",
        ExampleFormatArguments = new[] { QandA.Ordinal, "1", QandA.Ordinal, "2", QandA.Ordinal, "3" }, ExampleFormatArgumentGroupSize = 2)]
    SimonServesFlash,
    [SouvenirQuestion("Which item was not served in course {1} of {0}?", "Simon Serves", OneColumn4Answers, "Baked Batterys", "Bamboozling Waffles", "Big Boom Tortellini", "Blast Shrimps", "Blastwave Compote", "Bomb Brûlée", "Boolean Waffles", "Boom Lager Beer", "Caesar Salad", "Centurion Wings", "Colored Spare Ribs", "Cruelo Juice", "Defuse Juice", "Defuse au Chocolat", "Deto Bull", "Edgework Toast", "Forget Cocktail", "Forghetti Bombognese", "Indicator Tar Tar", "Morse Soup", "NATO Shrimps", "Not Ice Cream", "Omelette au Bombage", "Simon’s Special Mix", "Solve Cake", "Status Light Rolls", "Strike Pie", "Tasha’s Drink", "Ticking Timecakes", "Veggie Blast Plate", "Wire Shake", "Wire Spaghetti",
        ExampleFormatArguments = new[] { "1", "2", "3" }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    SimonServesFood,

    [SouvenirQuestion("What was the shape submitted at the end of {0}?", "Simon Shapes", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "SimonShapesSprites")]
    SimonShapesSubmittedShape,

    [SouvenirQuestion("Which letter flashed on the {1} button in {0}?", "Simon Shouts", ThreeColumns6Answers, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "top", "left", "right", "bottom" }, ExampleFormatArgumentGroupSize = 1)]
    SimonShoutsFlashingLetter,

    [SouvenirQuestion("How many spaces clockwise from the arrow was the {1} flash in the final sequence in {0}?", "Simon Shrieks", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonShrieksFlashingButton,

    [SouvenirQuestion("What was the {1} flash of the {2} stage of {0}?", "Simon Shuffles", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Purple", "Magenta", "White",
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SimonShufflesFlashes,

    [SouvenirQuestion("What shape was the {1} arrow in {0}?", "Simon Signals", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = nameof(SouvenirModule.SimonSignalsSprites),
        ExampleFormatArguments = new[] { "red", "green", "blue", "gray" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    SimonSignalsColorToShape,
    [SouvenirQuestion("How many directions did the {1} arrow in {0} have?", "Simon Signals", TwoColumns4Answers, "3", "4", "5", "6",
        ExampleFormatArguments = new[] { "red", "green", "blue", "gray" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    SimonSignalsColorToRotations,
    [SouvenirQuestion("What color was the arrow with this shape in {0}?", "Simon Signals", TwoColumns4Answers, "red", "green", "blue", "gray", UsesQuestionSprite = true, TranslateAnswers = true)]
    SimonSignalsShapeToColor,
    [SouvenirQuestion("How many directions did the arrow with this shape have in {0}?", "Simon Signals", TwoColumns4Answers, "3", "4", "5", "6", UsesQuestionSprite = true)]
    SimonSignalsShapeToRotations,
    [SouvenirQuestion("What color was the arrow with {1} possible directions in {0}?", "Simon Signals", TwoColumns4Answers, "red", "green", "blue", "gray", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "3", "4", "5", "6" }, ExampleFormatArgumentGroupSize = 1)]
    SimonSignalsRotationsToColor,
    [SouvenirQuestion("What shape was the arrow with {1} possible directions in {0}?", "Simon Signals", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = nameof(SouvenirModule.SimonSignalsSprites),
        ExampleFormatArguments = new[] { "3", "4", "5", "6" }, ExampleFormatArgumentGroupSize = 1)]
    SimonSignalsRotationsToShape,

    [SouvenirQuestion("What was the {1} flash in the final sequence in {0}?", "Simon Simons", ThreeColumns6Answers, "TR", "TY", "TG", "TB", "LR", "LY", "LG", "LB", "RR", "RY", "RG", "RB", "BR", "BY", "BG", "BB",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonSimonsFlashingColors,

    [SouvenirQuestion("Which key’s color flashed {1} in the {2} stage of {0}?", "Simon Sings", ThreeColumns6Answers, "C", "C♯", "D", "D♯", "E", "F", "F♯", "G", "G♯", "A", "A♯", "B",
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SimonSingsFlashing,

    [SouvenirQuestion("What sound did the {1} button press make {0}?", "Simon Smiles", TwoColumns4Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1,
        Type = AnswerType.Audio, AudioFieldName = "SimonSmilesAudio", AudioSizeMultiplier = 6)]
    SimonSmilesSounds,

    [SouvenirQuestion("What was the color of the {1} flash in {0}?", "Simon Smothers", ThreeColumns6Answers, "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    SimonSmothersColors,
    [SouvenirQuestion("What was the direction of the {1} flash in {0}?", "Simon Smothers", TwoColumns4Answers, "Up", "Down", "Left", "Right", ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    SimonSmothersDirections,

    [SouvenirQuestion("Which sample button sounded {1} in the final sequence in {0}?", "Simon Sounds", TwoColumns4Answers, "red", "blue", "yellow", "green", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonSoundsFlashingColors,

    [SouvenirQuestion("Which bubble flashed first in {0}?", "Simon Speaks", TwoColumns4Answers, "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right", TranslateAnswers = true)]
    SimonSpeaksPositions,
    [SouvenirQuestion("Which bubble flashed second in {0}?", "Simon Speaks", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "SimonSpeaksSprites")]
    SimonSpeaksShapes,
    [SouvenirQuestion("Which language was the bubble that flashed third in {0} in?", "Simon Speaks", TwoColumns4Answers, "English", "Danish", "Dutch", "Esperanto", "Finnish", "French", "German", "Hungarian", "Italian")]
    SimonSpeaksLanguages,
    [SouvenirQuestion("Which word was in the bubble that flashed fourth in {0}?", "Simon Speaks", ThreeColumns6Answers, "black", "sort", "zwart", "nigra", "musta", "noir", "schwarz", "fekete", "nero", "blue", "blå", "blauw", "blua", "sininen", "bleu", "blau", "kék", "blu", "green", "grøn", "groen", "verda", "vihreä", "vert", "grün", "zöld", "verde", "cyan", "turkis", "turkoois", "turkisa", "turkoosi", "turquoise", "türkis", "türkiz", "turchese", "red", "rød", "rood", "ruĝa", "punainen", "rouge", "rot", "piros", "rosso", "purple", "lilla", "purper", "purpura", "purppura", "pourpre", "lila", "bíbor", "porpora", "yellow", "gul", "geel", "flava", "keltainen", "jaune", "gelb", "sárga", "giallo", "white", "hvid", "wit", "blanka", "valkoinen", "blanc", "weiß", "fehér", "bianco", "gray", "grå", "grijs", "griza", "harmaa", "gris", "grau", "szürke", "grigio")]
    SimonSpeaksWords,
    [SouvenirQuestion("What color was the bubble that flashed fifth in {0}?", "Simon Speaks", ThreeColumns6Answers, "black", "blue", "green", "cyan", "red", "purple", "yellow", "white", "gray", TranslateAnswers = true)]
    SimonSpeaksColors,

    [SouvenirQuestion("Which color flashed {1} in {0}?", "Simon’s Star", ThreeColumns6Answers, "red", "yellow", "green", "blue", "purple", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonsStarColors,

    [SouvenirQuestion("Which color flashed in the {1} stage of {0}?", "Simon Stacks", TwoColumns4Answers, "Red", "Green", "Blue", "Yellow", TranslateAnswers = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonStacksColors,

    [SouvenirQuestion("Which color flashed {1} in the {2} stage in {0}?", "Simon Stages", ThreeColumns6Answers, "red", "blue", "yellow", "orange", "magenta", "green", "pink", "lime", "cyan", "white", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SimonStagesFlashes,
    [SouvenirQuestion("What color was the indicator in the {1} stage in {0}?", "Simon Stages", ThreeColumns6Answers, "red", "blue", "yellow", "orange", "magenta", "green", "pink", "lime", "cyan", "white", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonStagesIndicator,

    [SouvenirQuestion("Which {1} in the {2} stage in {0}?", "Simon States", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue", "Red, Yellow", "Red, Green", "Red, Blue", "Yellow, Green", "Yellow, Blue", "Green, Blue", "all 4", "none", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "color(s) flashed", QandA.Ordinal, "color(s) didn’t flash", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SimonStatesDisplay,

    [SouvenirQuestion("Which color flashed {1} in the output sequence in {0}?", "Simon Stops", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Violet", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SimonStopsColors,

    [SouvenirQuestion("Which color {1} {2} in the final sequence of {0}?", "Simon Stores", TwoColumns4Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "flashed", QandA.Ordinal, "was among the colors flashed", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SimonStoresColors,

    [SouvenirQuestion("What color was the button at this position in {0}?", "Simon Subdivides", TwoColumns4Answers, new[] { "Blue", "Green", "Red", "Violet" }, TranslateAnswers = true,
        UsesQuestionSprite = true)]
    SimonSubdividesButton,

    [SouvenirQuestion("What was the {1} topic in {0}?", "Simon Supports", TwoColumns4Answers, "Boss", "Cruel", "Faulty", "Lookalike", "Puzzle", "Simon", "Time-Based", "Translated",
        ExampleFormatArguments = new[] { QandA.Ordinal }, TranslateAnswers = true, ExampleFormatArgumentGroupSize = 1)]
    SimonSupportsTopics,

    [SouvenirQuestion("Where was {1} in {0}?", "Simon Swizzles", ThreeColumns6Answers, Type = AnswerType.Sprites, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true },
       ExampleFormatArguments = new[] { "OFF", "ON" })]
    [AnswerGenerator.Grid(4, 4)]
    SimonSwizzlesButton,
    [SouvenirQuestion("What was the hidden number in {0}?", "Simon Swizzles", ThreeColumns6Answers)]
    [AnswerGenerator.Strings("6*01")]
    SimonSwizzlesNumber,

    [SouvenirQuestion("What were the flashes in the {1} stage of {0}?", "Simply Simon", TwoColumns4Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("RBYG")]
    [AnswerGenerator.Strings("2*RBYG")]
    [AnswerGenerator.Strings("3*RBYG")]
    [AnswerGenerator.Strings("4*RBYG")]
    SimplySimonFlash,

    [SouvenirQuestion("What color flashed {1} on the {2} Simon in {0}?", "Simultaneous Simons", TwoColumns4Answers, "Blue", "Yellow", "Red", "Green", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SimultaneousSimonsFlash,

    [SouvenirQuestion("What were the original numbers in {0}?", "Skewed Slots", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 999, "000")]
    SkewedSlotsOriginalNumbers,

    [SouvenirQuestion("What color was this gem in {0}?", "Skewers", ThreeColumns6Answers, "Black", "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", "White", UsesQuestionSprite = true, TranslateAnswers = true)]
    SkewersColor,

    [SouvenirQuestion("Which race was selectable, but not the solution, in {0}?", "Skyrim", TwoColumns4Answers, "Nord", "Khajiit", "Breton", "Argonian", "Dunmer", "Altmer", "Redguard", "Orc", "Imperial", TranslateAnswers = true)]
    SkyrimRace,
    [SouvenirQuestion("Which weapon was selectable, but not the solution, in {0}?", "Skyrim", TwoColumns4Answers, "Axe of Whiterun", "Dawnbreaker", "Windshear", "Blade of Woe", "Firiniel’s End", "Bow of Hunt", "Volendrung", "Chillrend", "Mace of Molag Bal", TranslateAnswers = true)]
    SkyrimWeapon,
    [SouvenirQuestion("Which enemy was selectable, but not the solution, in {0}?", "Skyrim", TwoColumns4Answers, "Alduin", "Blood Dragon", "Cave Bear", "Dragon Priest", "Draugr", "Draugr Overlord", "Frost Troll", "Frostbite Spider", "Mudcrab", TranslateAnswers = true)]
    SkyrimEnemy,
    [SouvenirQuestion("Which city was selectable, but not the solution, in {0}?", "Skyrim", TwoColumns4Answers, "Dawnstar", "Ivarstead", "Markarth", "Riverwood", "Rorikstead", "Solitude", "Whiterun", "Windhelm", "Winterhold", TranslateAnswers = true)]
    SkyrimCity,
    [SouvenirQuestion("Which dragon shout was selectable, but not the solution, in {0}?", "Skyrim", TwoColumns4Answers, "Disarm", "Dismay", "Dragonrend", "Fire Breath", "Ice Form", "Kyne’s Peace", "Slow Time", "Unrelenting Force", "Whirlwind Sprint", TranslateAnswers = true)]
    SkyrimDragonShout,

    [SouvenirQuestion("What was the last triplet of letters in {0}?", "Slow Math", ThreeColumns6Answers, ExampleAnswers = new[] { "ABC", "DEG", "KNP", "STX", "ZAB", "CDE", "GKN", "PST", "XZA", "BCD" })]
    [AnswerGenerator.Strings(3, "ABCDEGKNPSTXZ")]
    SlowMathLastLetters,

    [SouvenirQuestion("How much did the sequence shift by in {0}?", "Small Circle", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8")]
    SmallCircleShift,
    [SouvenirQuestion("Which wedge made the different noise in the beginning of {0}?", "Small Circle", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black", TranslateAnswers = true)]
    SmallCircleWedge,
    [SouvenirQuestion("Which color was {1} in the solution to {0}?", "Small Circle", TwoColumns4Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Magenta", "White", "Black", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SmallCircleSolution,

    [SouvenirQuestion("What was on the display in the {1} stage of {0}?", "Small Talk", TwoColumns4Answers, ExampleAnswers = new[] { "TOP", "NAH", "INDIA", "UNIFORM" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SmallTalkDisplays,

    [SouvenirQuestion("In what category was {1} for {0}?", "Smash, Marry, Kill", TwoColumns4Answers, "SMASH", "MARRY", "KILL",
            ExampleFormatArguments = new[] { "The Button", "Maze", "Memory", "Morse Code", "Password", "Simon Says", "Who’s on First", "Wires", "Wire Sequence" }, ExampleFormatArgumentGroupSize = 1)]
    SmashMarryKillCategory,
    [SouvenirQuestion("Which module was in the {1} category for {0}?", "Smash, Marry, Kill", OneColumn4Answers,
            ExampleAnswers = new[] { "The Button", "Maze", "Memory", "Morse Code", "Password", "Simon Says", "Who’s on First", "Wires", "Wire Sequence" },
            ExampleFormatArguments = new[] { "SMASH", "MARRY", "KILL" }, ExampleFormatArgumentGroupSize = 1)]
    SmashMarryKillModule,

    [SouvenirQuestion("How many red balls were there at the start of {0}?", "Snooker", ThreeColumns3Answers, "8", "9", "10")]
    SnookerReds,

    [SouvenirQuestion("Which snowflake was on the {1} button of {0}?", "Snowflakes", ThreeColumns6Answers, Type = AnswerType.SnowflakesFont, FontSize = 400, CharacterSize = 0.2f, ExampleFormatArguments = new[] { "top", "right", "bottom", "left" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Strings("A-Za-z")]
    SnowflakesDisplayedSnowflakes,

    [SouvenirQuestion("Which sound was played but not featured in the chosen zone in {0}?", "Sonic & Knuckles", OneColumn4Answers, Type = AnswerType.Audio, ForeignAudioID = "sonicKnuckles")]
    SonicKnucklesSounds,
    [SouvenirQuestion("Which badnik was shown in {0}?", "Sonic & Knuckles", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "SonicKnucklesBadniksSprites")]
    SonicKnucklesBadnik,
    [SouvenirQuestion("Which monitor was shown in {0}?", "Sonic & Knuckles", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "SonicKnucklesMonitorsSprites")]
    SonicKnucklesMonitor,

    [SouvenirQuestion("What was the {1} picture on {0}?", "Sonic The Hedgehog", TwoColumns4Answers, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, Type = AnswerType.Sprites, SpriteFieldName = "SonicTheHedgehogSprites")]
    SonicTheHedgehogPictures,
    [SouvenirQuestion("Which sound was played by the {1} screen on {0}?", "Sonic The Hedgehog", TwoColumns4Answers, Type = AnswerType.Audio, AudioFieldName = "SonicTheHedgehogAudio", AudioSizeMultiplier = 4,
        ExampleFormatArguments = new[] { "Running Boots", "Invincibility", "Extra Life", "Rings" }, TranslateFormatArgs = new[] { true }, ExampleFormatArgumentGroupSize = 1)]
    SonicTheHedgehogSounds,

    [SouvenirQuestion("What positions were the last swap used to solve {0}?", "Sorting", ThreeColumns6Answers, "1 & 2", "1 & 3", "1 & 4", "1 & 5", "2 & 3", "2 & 4", "2 & 5", "3 & 4", "3 & 5", "4 & 5")]
    SortingLastSwap,

    [SouvenirQuestion("What was the first module asked about in the other Souvenir on this bomb?", "Souvenir", OneColumn4Answers,
        ExampleAnswers = new[] { "Probing", "Microcontroller", "Third Base", "Kudosudoku", "Quintuples", "3D Tunnels", "Uncolored Squares", "Pattern Cube", "Synonyms", "The Moon", "Human Resources", "Algebra" })]
    SouvenirFirstQuestion,

    [SouvenirQuestion("What was the maximum tax amount per vessel in {0}?", "Space Traders", ThreeColumns6Answers, "0 GCr", "1 GCr", "2 GCr", "3 GCr", "4 GCr", "5 GCr")]
    SpaceTradersMaxTax,

    [SouvenirQuestion("What word was asked to be spelled in {0}?", "Spelling Bee", TwoColumns4Answers, ExampleAnswers = new[] { "allocation", "auxiliary", "cloying", "connoisseur", "controversial", "deceit", "garrulous", "malachite", "perambulate", "sedge" })]
    SpellingBeeWord,

    [SouvenirQuestion("What was the {1} flashed color in {0}?", "Sphere", ThreeColumns6Answers, "red", "blue", "green", "orange", "pink", "purple", "grey", "white", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, AddThe = true)]
    SphereColors,

    [SouvenirQuestion("What bag was initially colored in {0}?", "Splitting The Loot", ThreeColumns6Answers, ExampleAnswers = new[] { "A5", "E6", "19", "82" })]
    SplittingTheLootColoredBag,

    [SouvenirQuestion("Who was the {1} child displayed in {0}?", "Spongebob Birthday Identification", OneColumn4Answers, "Abela", "Aiden", "Allen", "Amber", "Apollo Yuojan", "Ashley", "Bobby", "Brayden", "Brendon", "Brent", "Bryce", "Caoimhe", "Carl Pobie", "Carlos Paolo", "Carson", "Chester Paul", "Christopher", "Cristian James Glavez", "Cyan Miguel", "Danny", "Dave", "Davian", "Donn Jeff Velionix Fijo", "Drew Justin", "Ethan", "Fabio", "Frame Baby", "Gabriel Felix", "Grayson", "Hayden", "Jacob", "Jaden", "Jake", "James", "Jayden", "Jeremiah", "Jon JonJon Eric Cabebe Jr.", "Juan Carlos", "Julian", "Junely Delos Reyes Jr.", "Kate Venus Valadores", "Ken Ivan", "Kenny Lee", "King Monic", "Kurt", "Landon", "Logan", "Lukas", "Makenly", "Mason", "Max", "Melvern Ryann", "Michael", "Miguel", "Myles A. Williams", "Neftali Xyler S. Ilao", "Noah", "Patrick", "Raymond", "Rhojus", "Sam Daniel", "Seth Laurence", "Shik", "Simon", "Sony Boy", "Spanky", "Spencer", "Stacey", "Steve Jr.", "Xander Chio E. Ceniza",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SpongebobBirthdayIdentificationChildren,

    [SouvenirQuestion("What was the color of the {1} lit LED in {0}?", "Stability", TwoColumns4Answers, "Red", "Yellow", "Blue",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    StabilityLedColors,
    [SouvenirQuestion("What was the identification number in {0}?", "Stability", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9999, "0000")]
    StabilityIdNumber,

    [SouvenirQuestion("What was the {1} time signature in {0}?", "Stable Time Signatures", ThreeColumns6Answers, "1/1", "2/1", "3/1", "4/1", "5/1", "6/1", "7/1", "8/1", "9/1", "1/2", "2/2", "3/2", "4/2", "5/2", "6/2", "7/2", "8/2", "9/2", "1/4", "2/4", "3/4", "4/4", "5/4", "6/4", "7/4", "8/4", "9/4", "1/8", "2/8", "3/8", "4/8", "5/8", "6/8", "7/8", "8/8", "9/8",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    StableTimeSignaturesSignatures,

    [SouvenirQuestion("Which of these is the length of a sequence in {0}?", "Stacked Sequences", TwoColumns4Answers, ExampleAnswers = new[] { "3", "4", "5", "6" })]
    [AnswerGenerator.Integers(3, 9)]
    StackedSequences,

    [SouvenirQuestion("What was the digit in the center of {0}?", "Stars", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    StarsCenter,

    [SouvenirQuestion("Which star was present on {0}?", "Starstruck", ThreeColumns6Answers, Type = AnswerType.DynamicFont, FontSize = 432, CharacterSize = 1 / 7f)]
    [AnswerGenerator.Strings("a-zA-Z0-9!@#$%^&*()=+_,./<>?;:[]\\{}|-")]
    StarstruckStar,

    [SouvenirQuestion("What was the element shown in {0}?", "State of Aggregation", ThreeColumns6Answers, "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Al", "Si", "P", "S", "Cl", "Ar", "K", "Ca", "Sc", "Ti", "V", "Cr", "Mn", "Fe", "Co", "Ni", "Cu", "Zn", "Ga", "Ge", "As", "Se", "Br", "Kr", "Rb", "Sr", "Y", "Zr", "Nb", "Mo", "Tc", "Ru", "Rh", "Pd", "Ag", "Cd", "In", "Sn", "Sb", "Te", "I", "Xe", "Cs", "Ba", "La", "Ce", "Pr", "Nd", "Pm", "Sm", "Eu", "Gd", "Tb", "Dy", "Ho", "Er", "Tm", "Yb", "Lu", "Hf", "Ta", "W", "Re", "Os", "Ir", "Pt", "Au", "Hg", "Tl", "Pb", "Bi", "Po", "At", "Rn", "Fr", "Ra", "Ac", "Th", "Pa", "U", "Np", "Pu", "Am", "Cm", "Bk", "Cf", "Es", "Fm", "Md", "No", "Lr", "Rf", "Db", "Sg", "Bh", "Hs", "Mt", "Ds", "Rg", "Cn", "Nh", "Fl", "Mc", "Lv", "Ts", "Og")]
    StateOfAggregationElement,

    [SouvenirQuestion("What was the {1} letter in {0}?", "Stellar", ThreeColumns6Answers, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
        ExampleFormatArguments = new[] { "Morse code", "tap code", "Braille" }, TranslateFormatArgs = new[] { true }, ExampleFormatArgumentGroupSize = 1)]
    StellarLetters,

    [SouvenirQuestion("What was the {1} submitted word in {0}?", "Stroop’s Test", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Magenta", "White",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    StroopsTestWord,
    [SouvenirQuestion("What was the {1} submitted word’s color in {0}?", "Stroop’s Test", ThreeColumns6Answers, "Red", "Yellow", "Green", "Blue", "Magenta", "White",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    StroopsTestColor,

    [SouvenirQuestion("What was the value of the {1} arrow in {0}?", "Stupid Slots", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "top-left", "top-middle", "top-right", "bottom-left", "bottom-middle", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(-30, 30)]
    StupidSlotsValues,

    [SouvenirQuestion("What was a substitution word in {0}?", "Subbly Jubbly", TwoColumns4Answers, ExampleAnswers = new[] { "AMOGUS", "BOINKY", "CRINGE", "DUMPY", "EUPHEMISM", "FORTNITE" })]
    SubblyJubblySubstitutions,

    [SouvenirQuestion("How many subscribers does {1} have in {0}?", "Subscribe to Pewdiepie", TwoColumns4Answers,
        ExampleFormatArguments = new[] { "PewDiePie", "T-Series" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(10000000, 99999999)]
    SubscribeToPewdiepieSubCount,

    [SouvenirQuestion("Which bread did the customer ask for in {0}?", "Subway", OneColumn4Answers, "WHITE", "MULTIGRAIN", "GLUTEN FREE", "WHOLE WHEAT")]
    SubwayBread,
    [SouvenirQuestion("Which of these was not asked for in {0}?", "Subway", OneColumn4Answers, "TUNA", "CHICKEN", "TURKEY", "HAM", "PASTRAMI", "MYSTERY MEAT", "AMERICAN", "MOZZARELLA", "PROVOLONE", "SWISS", "CHEDDAR", "TOAST THE BREAD", "OLIVES", "LETTUCE", "PICKLES", "ONIONS", "TOMATOES", "JALAPENOS", "KETCHUP", "MAYONNAISE", "RANCH", "SALT", "PEPPER", "VINEGAR")]
    SubwayItems,

    [SouvenirQuestion("What skull was shown on the {1} square in {0}?", "Sugar Skulls", ThreeColumns6Answers, "A", "C", "E", "G", "I", "K", "M", "O", "P", "R", "T", "V", "X", "Z", "b", "d", "f", "h", "j", "l", "n", "p", "r", "t", "v", "x", "z", TranslateFormatArgs = new[] { true },
        Type = AnswerType.SugarSkullsFont, FontSize = 432, CharacterSize = 1 / 6f, ExampleFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1)]
    SugarSkullsSkull,
    [SouvenirQuestion("Which skull {1} present in {0}?", "Sugar Skulls", ThreeColumns6Answers, "A", "C", "E", "G", "I", "K", "M", "O", "P", "R", "T", "V", "X", "Z", "b", "d", "f", "h", "j", "l", "n", "p", "r", "t", "v", "x", "z",
        Type = AnswerType.SugarSkullsFont, FontSize = 432, CharacterSize = 1 / 6f, ExampleFormatArguments = new[] { "was", "was not" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    SugarSkullsAvailability,

    [SouvenirQuestion("What was the colour of this cell in {0}?", "Suits And Colours", TwoColumns4Answers, "yellow", "green", "orange", "red", UsesQuestionSprite = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SuitsAndColourColour,
    [SouvenirQuestion("What was the suit of this cell in {0}?", "Suits And Colours", TwoColumns4Answers, "spades", "diamonds", "clubs", "hearts", UsesQuestionSprite = true,
    ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    SuitsAndColourSuit,

    [SouvenirQuestion("What was the displayed word in {0}?", "Superparsing", ThreeColumns6Answers, "ROOT", "WARM", "THEM", "MAKE", "TIES", "LOTS", "RUGS", "SUCH", "STEM", "DIET", "RENT", "AQUA", "LOWS", "PATH", "HULL", "DRAW", "SLOT", "BRAD", "EGGS", "MEMO", "WIFE", "SHED", "FIRM", "SKIP", "DARK", "REAR", "EDGE", "LOCK", "BOWL", "DEAD", "ZONE", "GOOD", "FLAT", "GREW", "CRAP", "PUBS", "PEST", "ORAL", "FORD", "TRIM", "HIGH", "OKAY", "FLOW", "HITS", "JACK", "RUBY", "REEF", "TANK", "PUTS", "TRIP", "POST", "PAIN", "READ", "GEAR", "THEN", "FATE", "ARMY", "MINS", "DREW", "WIND", "FAME", "FUNK", "SOLD", "CREW", "WIFI", "CITE", "DAMN", "FARM", "NOSE", "HOOD", "CORN", "FAIR", "ADAM", "TIDE", "DISH", "GOAL", "DUMP", "GUAM", "GETS", "SOUP", "SYNC", "LAWN", "ROPE", "CLAN", "MESA", "BALL", "LORD", "EACH", "REEL", "TIER", "BYTE", "LIFT", "TUNE", "HEAR", "MALL", "HELL", "CITY", "MENS", "HOSE", "PIKE", "GOES", "PALM", "MILD", "JURY", "JUAN", "SUCK", "NAVY", "POOR", "LETS", "BOOK", "ACRE", "INFO", "MESS", "JUMP", "LAWS", "BEST", "BOFA", "PURE", "BUCK", "TAIL", "LEGS", "BIND", "HOST", "EYES", "MILF", "HANG", "VERY", "OVAL", "WELL", "LENS", "OOPS", "DIRT", "RAIN", "ONTO", "VICE", "HOME", "DUTY", "VOLT", "GAIN", "BEAR", "DAVE", "GEEK", "SINK", "BASS", "DESK", "POLE", "LACK", "NINE", "KNEW", "DUCK", "BLOW", "ZOOM", "SLOW", "SPOT", "MEGA", "RAID", "BEND", "DIVE", "MOMS", "VOID", "HOLE", "TIME", "ENDS", "THEY", "SNAP", "LOGS", "FROG", "CAST", "YEAH", "PAYS", "SITE", "EVIL", "SWAP", "OILS", "GENE", "BOAT", "FELT", "RUNS", "PAGE", "FOLD", "PAIR", "PINS", "WATT", "PETS", "RIDE", "IRON", "GRIP", "STUD", "HORN", "AIDS", "AIDS", "SOUL", "FLAG", "FREE", "WILL", "CLIP", "SPAN", "NAIL", "RARE", "NUTS", "JOKE", "BARS", "PADS", "MOON", "THAT", "THIS", "LOUD", "BAGS", "JAZZ", "WITH", "RATS", "FEAR", "WEAR", "DUAL", "MINE", "SEAT", "BIAS", "FOAM", "POEM", "CAPE", "BIDS", "ASKS", "TOLL", "BEAN", "WHOM", "SWIM", "BIRD", "DOGS", "RISE", "LADY", "MATH", "FANS", "CUPS", "ZERO", "NULL", "KNEE", "ROLL", "PREP", "FROM", "HAVE", "CENT", "POND", "LIPS", "WIRE", "MORE", "TAPE", "SPAM", "HIDE", "ACID", "BATH", "BARE", "HUNG", "RIPE", "FAIL", "KEPT", "JEEP", "PRAY", "WARS", "POPE", "RELY", "BETA", "DRAG", "FONT", "ARMS", "TIRE", "APPS", "TILE", "BEAT", "BOOB", "WAIT", "JUST", "KEYS", "EXIT", "LIST", "NAME", "ROSE", "WAVE", "FUCK", "MESH", "YEAR", "SOFT", "ACTS", "HOLY", "WALK", "FUEL", "ROWS", "OVER", "CATS", "DATE", "FIND", "KITS", "HARM", "THAN", "BOMB", "BACK", "ARCH", "UGLY", "COIN", "PICK", "FAKE", "SOIL", "CURE", "LACE", "INTO", "COLD", "MOST", "WORK", "SEES", "LAST", "FOLK", "TRIO", "CUBE", "DENY", "SHOT", "LUNG", "QUIT", "DATA", "USED", "NEXT", "EARS", "MISS", "FIST", "TASK", "DEAN", "GODS", "TALE", "MART", "WILD", "HEAT", "BOND", "TAXI", "MASS", "FOOT", "MERE", "POLL", "BULL", "BENT", "SNOW", "SAGE", "TRAY", "CAMP", "ONLY", "SLIM", "ISLE", "WHEN", "MATE", "DIED", "INCH", "CHEF", "LANE", "FILL", "NUKE", "BOOM", "CALM", "PEAK", "BUZZ", "GONE", "WHAT", "FORT", "EURO", "NEWS", "BELL", "SALT", "DICK", "FORK", "TIED", "HERE", "CAVE", "BIKE", "DIES", "DROP", "RICH", "WERE", "TRAP", "FOOL", "OVEN", "SOME", "LOSE", "SEEK", "LOAD", "DEER", "TAGS", "LIKE", "COCK", "JAIL", "GUYS", "BEEN", "PISS", "ONES", "GAVE", "ALSO", "TITS", "WOOL", "RANK", "HELP", "EYED", "SEEM", "TONS", "NOON", "COOK", "PLUG", "GRAB", "HIRE", "AGES", "PINK", "HAWK", "SHOW", "EVEN", "MIND", "LOST", "TOUR", "SIZE", "CODE", "MENU", "HELD", "TEND", "GULF", "ADDS", "PINE", "LIME", "AIMS", "PULL", "HOPE", "EASY", "DECK", "BLUE", "FINE", "MOLD", "AXIS", "HERB", "ALOT", "ROLE", "SOAP", "WISH", "IDLE", "LONG", "CAME", "TUBE", "STAR", "DRUG", "COPY", "YANG", "KING", "PICS", "MINT", "OPEN", "CASH", "REID", "BUSY", "NICK", "TONE", "PLOT", "HEEL", "FLEX", "MEAN", "HUNT", "TURN", "LINK", "SEAL", "MUCH", "SIGN", "SOON", "HAND", "TINY", "RICK", "COVE", "WING", "WASH", "FILE", "NECK", "PORN", "SEEN", "SPIN", "STOP", "PORT", "KEEP", "HOUR", "BUSH", "CLUB", "EASE", "RUSH", "TEXT", "CHAR", "DOLL", "RATE", "MALE", "ORGY", "ORGY", "ORGY", "ORGY", "FUND", "DOCK", "TABS", "MODS", "DUMB", "MODE", "DICE", "ROAD", "GIFT", "WEED", "TOOK", "SELL", "OWEN", "BITE", "CHIP", "DOOM", "WIKI", "TOOL", "IDEA", "BODY", "PERU", "EAST", "MYTH", "SONG", "FALL", "LATE", "HARD", "BARN", "FARE", "BUTT", "PUNK", "WANT", "PACE", "DUKE", "MOVE", "GUNS", "BELT", "BALD", "HUGE", "KIND", "HERO", "LOGO", "WAGE", "BOLT", "TAKE", "WENT", "DAWN", "FOUR", "CARL", "COAT", "SPAS", "HINT", "LEAD", "SHOP", "PORK", "NICE", "GRID", "BUYS", "BAND", "ECHO", "SENT", "STAY", "TECH", "GRAD", "LEAF", "PEER", "MEET", "OURS", "BOTH", "LEVY", "PAST", "UNIT", "FACT", "EDIT", "AUTO", "PLUS", "UPON", "FIVE", "BRAS", "FAST", "SONS", "GURU", "GOLD", "KISS", "PIPE", "RISK", "CUBA", "TOWN", "MEAT", "SILK", "BOLD", "ARTS", "BANK", "SCAN", "ZINC", "LIES", "IDOL", "FEEL", "STAN", "TIPS", "CUTE", "GAPS", "WISE", "WORD", "ODDS", "BILL", "WARD", "SEED", "LAID", "DOWN", "NEST", "CLAY", "WEAK", "TEAR", "EVER", "CARE", "TALK", "PALE", "MARK", "SOLE", "BULK", "ROCK", "ROOF", "AGED", "SAYS", "LAND", "NEON", "DONE", "GALE", "GAME", "KIDS", "NUDE", "OWNS", "CHAT", "FIRE", "CORK", "MILL", "CUTS", "PENS", "MARS", "SAME", "HEAD", "GATE", "ONCE", "MATS", "SLIP", "DEAF", "AWAY", "CELL", "SELF", "WORM", "LOSS", "HASH", "LAZY", "PILL", "CASE", "FRED", "WOOD", "BABY", "META", "NEAR", "BASE", "DOME", "COPE", "FACE", "FILM", "CARB", "PUMP", "TERM", "CAPS", "ANNE", "SONY", "WIDE", "TELL", "LOAN", "LABS", "KEEN", "CARS", "BANG", "ALEX", "RYAN", "PEAS", "GIRL", "ABLE", "GREY", "TOYS", "GOLF", "WOLF", "URLS", "BEDS", "SORT", "BONE", "BLAN", "MIME", "NONE", "MEAL", "MOSS", "HURT", "LAKE", "KICK", "FITS", "EXAM", "SURE", "HALF", "STEP", "BUGS", "BURN", "SUIT", "QUAD", "LAMP", "CAGE", "SHOE", "DASH", "BOYS", "OAKS", "KNIT", "REST", "FORM", "MAIL", "DEEP", "PLAN", "WAYS", "LION", "LOOP", "CROP", "SICK", "CAKE", "TOLD", "RICE", "TALL", "DIAL", "FOOD", "CULT", "RULE", "DEAR", "GORE", "VOTE", "SKIN", "CARD", "HATE", "LAMB", "FELL", "MUST", "WALL", "THOU", "TREE", "THUS", "RAGE", "BEER", "FLUX", "COAL", "REAL", "PING", "WINE", "SEAS", "ITEM", "YARD", "COST", "LUCK", "MASK", "POOL", "FULL", "SANS", "SURF", "YARN", "MILK", "HOOK", "LEAN", "LIFE", "ASIA", "KNOW", "POET", "USES", "WEST", "RING", "MARY", "PASS", "ROOM", "GARY", "FEES", "SAKE", "BOSS", "EPIC", "FOUL", "TEEN", "JOIN", "ARAB", "CORD", "ROME", "JADE", "VARY", "HATS", "DUDE", "NOTE", "LIVE", "JUNK", "DOOR", "WEEK", "SOLO", "FEET", "ALTO", "DOSE", "SEXY", "NORM", "PART", "LEFT", "BEEF", "TEAM", "DAYS", "LOOK", "BOOT", "SALE", "WARE", "PUSH", "COOL", "GANG", "JOBS", "MANY", "WRAP", "FEED", "MOOD", "BLOG", "QUIZ", "LESS", "GLAD", "SOFA", "NEED", "VAST", "SAFE", "JOHN", "WAKE", "WINS", "TWIN", "GOAT", "GLOW", "HALO", "GAYS", "FEAT", "DONT", "USER", "GRAY", "SAID", "SHIT", "SAIL", "TYPE", "TILL", "DRUM", "CALL", "REED", "JETS", "SING", "DUST", "YOGA", "VIDS", "SETS", "MICE", "GIVE", "TEMP", "SIDE", "BITS", "PARK", "SEND", "HOLD", "DEAL", "CORE", "MAIN", "DEBT", "MAPS", "HILL", "SHIP", "THIN", "CART", "DOES", "SAND", "DEMO", "NOVA", "DEPT", "RAYS", "HACK", "PAID", "COME", "RACK", "SEMI", "ANAL", "JULY", "SEAN", "MADE", "FISH", "SHUT", "CONF", "HAIR", "EARN", "LOVE", "TEST", "PLAY", "PACK", "BORN", "INTL", "LITE", "ACNE", "TOPS", "RACE", "LIBS", "HALL", "BEAM", "LINE", "CAFE", "WALT", "UNDO", "SAVE", "ANTI", "POSE", "TENT")]
    SuperparsingDisplayed,

    [SouvenirQuestion("Which security protocol was installed in {0}?", "SUSadmin", TwoColumns4Answers, "ByteDefender", "Kasperovich", "Awast", "MedicWeb", "Disco", "MOD32")]
    SUSadminSecurity,

    [SouvenirQuestion("What color was the {1} LED on the {2} flip of {0}?", "Switch", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", TranslateAnswers = true, TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "top", QandA.Ordinal, "bottom", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2, AddThe = true)]
    SwitchInitialColor,

    [SouvenirQuestion("What was the initial position of the switches in {0}?", "Switches", ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings(5, 'Q', 'R')]
    SwitchesInitialPosition,

    [SouvenirQuestion("What was the seed in {0}?", "Switching Maze", TwoColumns4Answers)]
    [AnswerGenerator.Strings(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/")]
    SwitchingMazeSeed,
    [SouvenirQuestion("What was the starting maze color in {0}?", "Switching Maze", ThreeColumns6Answers, "Blue", "Cyan", "Magenta", "Orange", "Red", "White", TranslateAnswers = true)]
    SwitchingMazeColor,

    [SouvenirQuestion("How many symbols were cycling on the {1} screen in {0}?", "Symbol Cycle", TwoColumns4Answers, "2", "3", "4", "5", TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "left", "right" }, ExampleFormatArgumentGroupSize = 1)]
    SymbolCycleSymbolCounts,

    [SouvenirQuestion("What was the {1} symbol in the {2} stage of {0}?", "Symbolic Coordinates", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "SymbolicCoordinatesSprites", TranslateFormatArgs = new[] { true, false },
        ExampleFormatArguments = new[] { "left", QandA.Ordinal, "middle", QandA.Ordinal, "right", QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SymbolicCoordinateSymbols,

    [SouvenirQuestion("Which button flashed {1} in the final sequence of {0}?", "Symbolic Tasha", ThreeColumns6Answers, "Top", "Right", "Bottom", "Left", "Pink", "Green", "Yellow", "Blue",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    SymbolicTashaFlashes,
    [SouvenirQuestion("Which symbol was on the {1} button in {0}?", "Symbolic Tasha", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "SymbolicTashaSprites",
        ExampleFormatArguments = new[] { "top", "right", "bottom", "left", "blue", "green", "yellow", "pink" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    SymbolicTashaSymbols,

    [SouvenirQuestion("What color flashed {1} in the {2} stage of {0}?", "Synapse Says", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue",
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SynapseSaysFlashes,
    [SouvenirQuestion("What color was in the {1} position of the {2} stage of {0}?", "Synapse Says", TwoColumns4Answers, "Red", "Yellow", "Green", "Blue",
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    SynapseSaysPositions,
    [SouvenirQuestion("What number was displayed in the {1} stage of {0}?", "Synapse Says", TwoColumns4Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 4)]
    SynapseSaysDisplays,

    [SouvenirQuestion("What was displayed on the screen in the {1} stage of {0}?", "SYNC-125 [3]", TwoColumns4Answers, Type = AnswerType.DynamicFont, ExampleAnswers = new[] { "İ'ms'", "ăĠ'n'", "kğ'i", "kĞ'p'", "ăut'", "ăġ'r", "ăġ'm", "ărs", "kğp'", "kğk" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    Sync125_3Word,

    [SouvenirQuestion("Which number was displayed on {0}?", "Synonyms", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    SynonymsNumber,

    [SouvenirQuestion("What error code did you fix in {0}?", "Sysadmin", ThreeColumns6Answers, ExampleAnswers = new[] { "391M", "4HZZ", "56OW", "6RO0", "6WMJ", "8V94", "CYB6", "HR71", "PT68", "X8IZ" })]
    SysadminFixedErrorCodes,

    [SouvenirQuestion("Which card was {1} in the swap in {0}?", "TAC", TwoColumns4Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "backwards 3", "backwards 4", "backwards 5", "single-step 6", "single-step 7", "8 or discard", "9 or discard", "10 or discard", "Warrior", "Trickster",
        ExampleFormatArguments = new[] { "given away", "received" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true }, TranslateAnswers = true)]
    TACSwappedCard,
    [SouvenirQuestion("Which card was in your hand in {0}?", "TAC", TwoColumns4Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "backwards 3", "backwards 4", "backwards 5", "single-step 6", "single-step 7", "8 or discard", "9 or discard", "10 or discard", "Warrior", "Trickster",
        TranslateAnswers = true)]
    TACHeldCard,

    [SouvenirQuestion("What was the received word in {0}?", "Tap Code", TwoColumns4Answers, ExampleAnswers = new[] { "child", "style", "shake", "alive", "axion", "wreck", "cause", "pupil", "cheat", "watch" })]
    TapCodeReceivedWord,

    [SouvenirQuestion("What was the {1} flashed color in {0}?", "Tasha Squeals", TwoColumns4Answers, "Pink", "Green", "Yellow", "Blue", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    TashaSquealsColors,

    [SouvenirQuestion("Where was the starting position in {0}?", "Tasque Managing", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "TasqueManagingSprites")]
    TasqueManagingStartingPos,

    [SouvenirQuestion("Which ingredient was displayed {1}, from left to right, in {0}?", "Tea Set", ThreeColumns6Answers, AddThe = true, Type = AnswerType.Sprites, SpriteFieldName = "TeaSetSprites", ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    TeaSetDisplayedIngredients,

    [SouvenirQuestion("What was the {1} displayed digit in {0}?", "Technical Keypad", ThreeColumns6Answers, IsEntireQuestionSprite = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    TechnicalKeypadDisplayedDigits,

    [SouvenirQuestion("What was the initial color of the {1} button in the {2} stage of {0}?", "Ten-Button Color Code", ThreeColumns3Answers, "red", "green", "blue", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    TenButtonColorCodeInitialColors,

    [SouvenirQuestion("What was the {1} split in {0}?", "Tenpins", OneColumn4Answers, "Goal Posts", "Cincinnati", "Woolworth Store", "Lily", "3-7 Split", "Cocked Hat", "4-7-10 Split", "Big Four", "Greek Church", "Big Five", "Big Six", "HOW", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1)]
    TenpinsSplits,

    [SouvenirQuestion("What colour triangle pulsed {1} in {0}?", "Tetriamonds", ThreeColumns6Answers, "orange", "lime", "jade", "azure", "violet", "rose", "grey", TranslateAnswers = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
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
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ThirdBaseDisplay,

    [SouvenirQuestion("Which sound was used in {0}?", "Thirty Dollar Module", AnswerLayout.ThreeColumns6Answers, Type = AnswerType.Audio, ForeignAudioID = "ThirtyDollarModule", AudioSizeMultiplier = 5)]
    ThirtyDollarModuleSounds,

    [SouvenirQuestion("What was on the {1} button at the start of {0}?", "Tic Tac Toe", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "O", "X", Type = AnswerType.TicTacToeFont,
        ExampleFormatArguments = new[] { "top-left", "top-middle", "top-right", "middle-left", "middle-center", "middle-right", "bottom-left", "bottom-middle", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    TicTacToeInitialState,

    [SouvenirQuestion("What was the {1} time signature in {0}?", "Time Signatures", ThreeColumns6Answers, "1/1", "2/1", "3/1", "4/1", "5/1", "6/1", "7/1", "8/1", "9/1", "1/2", "2/2", "3/2", "4/2", "5/2", "6/2", "7/2", "8/2", "9/2", "1/4", "2/4", "3/4", "4/4", "5/4", "6/4", "7/4", "8/4", "9/4", "1/8", "2/8", "3/8", "4/8", "5/8", "6/8", "7/8", "8/8", "9/8",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    TimeSignaturesSignatures,

    [SouvenirQuestion("What was the {1} city in {0}?", "Timezone", TwoColumns4Answers, "Alofi", "Papeete", "Unalaska", "Whitehorse", "Denver", "Managua", "Quito", "Manaus", "Buenos Aires", "Sao Paulo", "Praia", "Edinburgh", "Berlin", "Bujumbura", "Moscow", "Tbilisi", "Lahore", "Omsk", "Bangkok", "Beijing", "Tokyo", "Brisbane", "Sydney", "Tarawa",
        ExampleFormatArguments = new[] { "departure", "destination" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    TimezoneCities,

    [SouvenirQuestion("Which of these squares was safe in row {1} in {0}?", "Tip Toe", ThreeColumns6Answers, ExampleFormatArguments = new[] { "9", "10" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    TipToeSafeSquares,

    [SouvenirQuestion("What was the word initially shown in {0}?", "Topsy Turvy", ThreeColumns6Answers, "Topsy", "Robot", "Cloud", "Round", "Quilt", "Found", "Plaid", "Curve", "Water", "Ovals", "Verse", "Sandy", "Frown", "Windy", "Curse", "Ghost")]
    TopsyTurvyWord,

    [SouvenirQuestion("What was the transmitted word in {0}?", "Touch Transmission", ThreeColumns6Answers, "that", "this", "not", "your", "all", "new", "was", "can", "has", "but", "one", "may", "what", "which", "their", "use", "any", "there", "see", "his", "here", "web", "get", "been", "were", "these", "its", "than", "find", "top", "had", "list", "just", "over", "year", "day", "into", "two", "used", "last", "most", "buy", "post", "add", "such", "best", "where", "info", "high", "very", "read", "sex", "need", "user", "set", "map", "know", "way", "part", "real", "must", "line", "did", "send", "using", "forum", "even", "being", "much", "link", "open", "south", "both", "power", "care", "down", "him", "without", "think", "big", "law", "shop", "old", "main", "man", "card", "job", "teen", "too", "join", "west", "team", "box", "gay", "start", "air", "yes", "hot", "cost", "march", "say", "going", "test", "cart", "staff", "things", "tax", "got", "let", "park", "act", "key", "few", "age", "hard", "pay", "four", "offer", "easy", "fax", "china", "yet", "areas", "sun", "enter", "share", "run", "net", "term", "put", "try", "god", "head", "least", "log", "cars", "fun", "arts", "lot", "ask", "beach", "past", "due", "ever", "ago", "cheap", "mark", "bad", "edit", "fast", "often", "though", "town", "step", "shows", "enough", "death", "brand", "oil", "bit", "near", "stuff", "doing", "stay", "mean", "force", "cash", "bay", "seen", "stop", "dog", "mind", "lost", "tour", "menu", "wish", "lower", "fine", "hour", "gas", "six", "bush", "sat", "zip", "bid", "kind", "sent", "shown", "lead", "went", "idea", "deal", "forms", "feed", "cut", "earth", "ship", "kit", "boy", "wine", "stars", "owner", "son", "bring", "grand", "van", "skin", "pop", "rest", "hit", "fish", "eye", "string", "youth", "fee", "rent", "dark", "aid", "host", "hands", "fat", "saw", "dead", "farm", "showing", "hear", "fan", "former", "cat", "die", "flow", "path", "pet", "guy", "cup", "army", "gear", "forest", "ending", "wind", "bob", "fit", "pain", "cum", "edge", "ice", "pink", "shot", "bus", "heat", "nor", "bug", "soft", "theme", "rich", "touch", "chain", "died", "reach", "lab", "snow", "owned", "chart", "gene", "ends", "cast", "soul", "ended", "dining", "mix", "fix", "ray", "bear", "gain", "dry", "blow", "shared", "cent", "forced", "zero", "bath", "sharing", "won", "wear", "mom", "rare", "bars", "seat", "aim", "rings", "tip", "mine", "whom", "math", "fly", "fear", "standing", "wars", "hey", "beat", "arms", "sky", "toy", "slow", "hip", "nine", "grow", "dot", "rain", "yeah", "cap", "peak", "raw", "sharp", "wet", "ram", "fox", "mesh", "dean", "pub", "hop", "mouth", "gun", "lens", "warm", "rear", "showed", "mens", "bowl", "kid", "pan", "dish", "eating", "vary", "arab", "bands", "push", "tower", "sum", "shower", "dear", "vat", "beer", "sir", "earn", "twin", "spy", "chip", "sit", "echo", "fig", "stands", "teach", "tab", "beds", "aged", "seed", "peer", "meat", "inner", "leg", "tiny", "gap", "rob", "mining", "jet", "mad", "shoe", "joy", "ran", "seal", "ill", "lay", "wings", "bet", "throw", "dad", "pat", "yard", "pour", "dust", "kings", "tie", "ward", "roof", "beast", "rush", "wins", "ghost", "toe", "shit", "ease", "arena", "lands", "armed", "pine", "tend", "candy", "finger", "tough", "lie", "chest", "weak", "leaf", "pad", "rod", "sad", "meal", "pot", "mars", "theft", "swing", "mint", "spin", "wash", "jam", "hero", "ion", "peru", "singer", "aging", "reed", "ban", "vast", "odd", "beam", "shut", "inform", "cry", "zoo", "arrow", "rough", "outer", "steam", "ace", "sue", "eggs", "mins", "stem", "opt", "rap", "charm", "soup", "cod", "singing", "gel", "doug", "mart", "coin", "harm", "deer", "pal", "oven", "cheat", "gym", "tan", "pie", "tied", "bingo", "cedar", "stud", "bend", "dam", "chad", "dying", "bench", "tub", "inns", "easter", "landing", "bean", "wheat", "bee", "loud", "bare", "pit", "ton", "lying", "handed", "sink", "pins", "handy", "rid", "rip", "lip", "sap", "forming", "eyed", "ought", "aye", "forty", "rows", "ears", "fist", "mere", "dig", "caring", "deny", "rim", "tier", "andrea", "pig", "lit", "duo", "fog", "fur", "rug", "ham", "sheer", "bind", "lows", "pest", "sofa", "tent", "dare", "wax", "nut", "lean", "bye", "strand", "dash", "lap", "steal", "ant", "gem", "heath", "yeast", "myth", "gig", "weed", "hint", "barn", "fare", "herb", "ate", "mud", "shark", "shine", "dip", "hash", "lined", "pens", "lid", "deaf", "keen", "peas", "owns", "hay", "zinc", "tear", "nest", "cop", "dim", "stan", "sip", "feat", "glow", "ware", "foul", "seas", "forge", "pod", "ours", "wit", "yarn", "mug", "marsh", "bent", "hat")]
    TouchTransmissionWord,
    [SouvenirQuestion("In what order was the Braille read in {0}?", "Touch Transmission", OneColumn4Answers, "Standard Braille Order", "Individual Reading Order", "Merged Reading Order", "Chinese Reading Order", TranslateAnswers = true)]
    TouchTransmissionOrder,

    [SouvenirQuestion("What was the {1} received message in {0}?", "Transmitted Morse", TwoColumns4Answers, "BOMBS", "SHORT", "UNDERSTOOD", "W1RES", "SOS", "MANUAL", "STRIKED", "WEREDEAD", "GOTASOUV", "EXPLOSION", "EXPERT", "RIP", "LISTEN", "DETONATE", "ROGER", "WELOSTBRO", "AMIDEAF", "KEYPAD", "DEFUSER", "NUCLEARWEAPONS", "KAPPA", "DELTA", "PI3", "SMOKE", "SENDHELP", "LOST", "SWAN", "NOMNOM", "BLUE", "BOOM", "CANCEL", "DEFUSED", "BROKEN", "MEMORY", "R6S8T", "TRANSMISSION", "UMWHAT", "GREEN", "EQUATIONSX", "RED", "ENERGY", "JESTER", "CONTACT", "LONG",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    TransmittedMorseMessage,

    [SouvenirQuestion("What colour triangle pulsed {1} in {0}?", "Triamonds", ThreeColumns6Answers, "black", "red", "green", "yellow", "blue", "magenta", "cyan", "white", ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    TriamondsPulsingColours,

    [SouvenirQuestion("What was the {1} name in {0}?", "Tribal Council", TwoColumns4Answers, "Louise", "Mark", "Hannah", "Adam", "Harvey", "Maria", "Jonathan", "Carolyn", "Stacy", "Bob",
        ExampleFormatArguments = new[] { "northeast", "southwest" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    TribalCouncilName,

    [SouvenirQuestion("Which of these was one of the passwords in {0}?", "Triple Term", ThreeColumns6Answers, ExampleAnswers = new[] { "Three", "Every", "These", "Would", "Where", "First", "Still", "Plant", "Small", })]
    TripleTermPasswords,

    [SouvenirQuestion("What was the {1} line you commented out in {0}?", "Turtle Robot", TwoColumns4Answers, ExampleAnswers = new[] { "LT 90", "FD 1", "RT 180 2", "LT 90 2", "RT 180", "FD 6", "RT 90 2" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, Type = AnswerType.TurtleRobotFont)]
    TurtleRobotCodeLines,

    [SouvenirQuestion("What was the {1} correct query response from {0}?", "Two Bits", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 99, "00")]
    TwoBitsResponse,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Ultimate Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "ACCESS", "EMPIRE", "EXPEND", "INDUCE", "LOCATE", "MELODY", "SPIRIT", "STOLEN", "VESSEL", "WIGGLE" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    UltimateCipherScreen,

    [SouvenirQuestion("Which direction was the {1} dial pointing in {0}?", "Ultimate Cycle", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "CycleModuleEightSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    UltimateCycleDialDirections,
    [SouvenirQuestion("What letter was written on the {1} dial in {0}?", "Ultimate Cycle", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("1*A-Z")]
    UltimateCycleDialLabels,

    [SouvenirQuestion("What was the {1} rotation in {0}?", "Ultracube", ThreeColumns6Answers, "XY", "YX", "XZ", "ZX", "XW", "WX", "XV", "VX", "YZ", "ZY", "YW", "WY", "YV", "VY", "ZW", "WZ", "ZV", "VZ", "WV", "VW", AddThe = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    UltracubeRotations,

    [SouvenirQuestion("What was the {1} rotation in the {2} stage of {0}?", "UltraStores", ThreeColumns6Answers, ExampleAnswers = new[] { "UZ", "VU", "WV", "YU", "YW", "YX" },
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    UltraStoresSingleRotation,
    [SouvenirQuestion("What was the {1} rotation in the {2} stage of {0}?", "UltraStores", TwoColumns4Answers, ExampleAnswers = new[] { "(XU, VY, WZ)", "(XY, VZ, UW)", "(XZ, YV, WU)", "(YX, UZ, VW)" },
        ExampleFormatArguments = new[] { QandA.Ordinal, QandA.Ordinal }, ExampleFormatArgumentGroupSize = 2)]
    UltraStoresMultiRotation,

    [SouvenirQuestion("What was the {1} color in reading order used in the first stage of {0}?", "Uncolored Squares", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    UncoloredSquaresFirstStage,

    [SouvenirQuestion("What was the initial state of the switches in {0}?", "Uncolored Switches", ThreeColumns6Answers, Type = AnswerType.SymbolsFont)]
    [AnswerGenerator.Strings(5, 'Q', 'R')]
    UncoloredSwitchesInitialState,
    [SouvenirQuestion("What color was the {1} LED in reading order in {0}?", "Uncolored Switches", TwoColumns4Answers, "red", "green", "blue", "turquoise", "orange", "purple", "white", "black", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    UncoloredSwitchesLedColors,

    [SouvenirQuestion("What was the {1} in the {2} position of the {3} sequence of {0}?", "Uncolour Flash", ThreeColumns6Answers, "Red", "Green", "Blue", "Yellow", "White", "Magenta",
        ExampleFormatArguments = new[] { "word", QandA.Ordinal, "“YES”", "colour of the word", QandA.Ordinal, "“NO”" }, ExampleFormatArgumentGroupSize = 3)]
    UncolourFlashDisplays,

    [SouvenirQuestion("What was the {1} received instruction in {0}?", "Unfair Cipher", ThreeColumns6Answers, "PCR", "PCG", "PCB", "SUB", "MIT", "CHK", "PRN", "BOB", "REP", "EAT", "STR", "IKE",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    UnfairCipherInstructions,

    [SouvenirQuestion("What was the {1} decrypted instruction in {0}?", "Unfair’s Revenge", ThreeColumns6Answers, "PCR", "PCG", "PCB", "SCC", "SCM", "SCY", "SUB", "MIT", "CHK", "PRN", "BOB", "REP", "EAT", "STR", "IKE", "SIG", "PVP", "NXP", "PVS", "NXS", "OPP",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    UnfairsRevengeInstructions,

    [SouvenirQuestion("What was the {1} submitted code in {0}?", "Unicode", ThreeColumns6Answers, "00A7", "00B6", "0126", "04D4", "017F", "01F6", "01F7", "2042", "037C", "03C2", "040B", "20AA", "042E", "0460", "046C", "20B0", "222F", "222B", "2569", "04EC", "260A", "04A6", "2626", "FB21", "0428", "03A9", "0583", "2592", "254B", "2318", "2234", "2205", "2104", "04A8", "2605", "019B", "03EA", "062A", "067C", "063A", "06BA", "00FE", "0194", "0239",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    UnicodeSortedAnswer,

    [SouvenirQuestion("What was the initial card in {0}?", "UNO!", OneColumn4Answers, "Red 0", "Red 1", "Red 2", "Red 3", "Red 4", "Red 5", "Red 6", "Red 7", "Red 8", "Red 9", "Red +2", "Red Skip", "Red Reverse", "Green 0", "Green 1", "Green 2", "Green 3", "Green 4", "Green 5", "Green 6", "Green 7", "Green 8", "Green 9", "Green +2", "Green Skip", "Green Reverse", "Yellow 0", "Yellow 1", "Yellow 2", "Yellow 3", "Yellow 4", "Yellow 5", "Yellow 6", "Yellow 7", "Yellow 8", "Yellow 9", "Yellow +2", "Yellow Skip", "Yellow Reverse", "Blue 0", "Blue 1", "Blue 2", "Blue 3", "Blue 4", "Blue 5", "Blue 6", "Blue 7", "Blue 8", "Blue 9", "Blue +2", "Blue Skip", "Blue Reverse", "+4", "Wild", TranslateAnswers = true)]
    UnoInitialCard,

    [SouvenirQuestion("What color was this key in the {1} stage of {0}?", "Unordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    UnorderedKeysKeyColor,
    [SouvenirQuestion("What color was the label of this key in the {1} stage of {0}?", "Unordered Keys", ThreeColumns6Answers, "Red", "Green", "Blue", "Cyan", "Magenta", "Yellow", UsesQuestionSprite = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    UnorderedKeysLabelColor,
    [SouvenirQuestion("What was the label of this key in the {1} stage of {0}?", "Unordered Keys", ThreeColumns6Answers, UsesQuestionSprite = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(1, 6)]
    UnorderedKeysLabel,

    [SouvenirQuestion("What was the {1} submitted letter in {0}?", "Unown Cipher", ThreeColumns6Answers,
        Type = AnswerType.UnownFont, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings('A', 'Z')]
    UnownCipherAnswers,

    [SouvenirQuestion("What was the color of this square in {0}?", "Unpleasant Squares", TwoColumns4Answers, "Red", "Yellow", "Jade", "Azure", "Violet", TranslateAnswers = true, UsesQuestionSprite = true)]
    UnpleasantSquaresColor,

    [SouvenirQuestion("What was the text on {0}?", "Updog", ThreeColumns6Answers, "dog", "DOG", "dawg", "DAWG", "doge", "DOGE", "dag", "DAG", "dogg", "DOGG", "dage", "DAGE")]
    UpdogWord,
    [SouvenirQuestion("What was the {1} color in the sequence on {0}?", "Updog", ThreeColumns6Answers, "Red", "Yellow", "Orange", "Green", "Blue", "Purple", TranslateFormatArgs = new[] { true }, TranslateAnswers = true,
        ExampleFormatArguments = new[] { "first", "last" }, ExampleFormatArgumentGroupSize = 1)]
    UpdogColor,

    [SouvenirQuestion("Which state was displayed in {0}?", "USA Cycle", TwoColumns4Answers,
        Type = AnswerType.Sprites, SpriteFieldName = "USACycleSprites")]
    USACycleDisplayed,

    [SouvenirQuestion("Which state did you depart from in {0}?", "USA Maze", TwoColumns4Answers, "Alaska", "Alabama", "Arkansas", "Arizona", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming")]
    USAMazeOrigin,

    [SouvenirQuestion("Which word {1} shown in {0}?", "V", OneColumn4Answers,
    ExampleFormatArguments = new[] { "was", "was not" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true }, ExampleAnswers = new[] { "Vacant", "Valorous", "Volition", "Vermin", "Vanity", "Visage", "Voracious", "Veers", "Vengeance", "Violation", "Vigilant", "Veteran", "Vanguarding", "Villain" })]
    VWords,

    [SouvenirQuestion("What was the initial state of {0}?", "Valves", TwoColumns4Answers, Type = AnswerType.Sprites, SpriteFieldName = "ValvesSprites")]
    ValvesInitialState,

    [SouvenirQuestion("What was the initially pressed color on {0}?", "Varicolored Squares", ThreeColumns6Answers, "White", "Red", "Blue", "Green", "Yellow", "Magenta", TranslateAnswers = true)]
    VaricoloredSquaresInitialColor,

    [SouvenirQuestion("What was the word of the {1} goal in {0}?", "Varicolour Flash", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "White",
        TranslateAnswers = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    VaricolourFlashWords,
    [SouvenirQuestion("What was the color of the {1} goal in {0}?", "Varicolour Flash", ThreeColumns6Answers, "Red", "Green", "Blue", "Magenta", "Yellow", "White",
        TranslateAnswers = true, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    VaricolourFlashColors,

    [SouvenirQuestion("What color was the LED flashing in {0}?", "Variety", TwoColumns4Answers, "Red", "Yellow", "Blue", "White", "Black", TranslateAnswers = true,
        TranslatableStrings = new[] { "the Variety that has one", "the Variety that has {0}", "a knob", "a colored knob", "a white knob", "a red knob", "a black knob", "a blue knob", "a yellow knob", "a keypad", "a white keypad", "a red keypad", "a yellow keypad", "a blue keypad", "a slider", "a horizontal slider", "a vertical slider", "an LED", "a digit display", "a wire", "a black wire", "a blue wire", "a red wire", "a yellow wire", "a white wire", "a button", "a red button", "a yellow button", "a blue button", "a white button", "a letter display", "a Braille display", "a key-in-lock", "a switch", "a red switch", "a yellow switch", "a blue switch", "a white switch", "a timer", "an ascending timer", "a descending timer", "a die", "a light-on-dark die", "a dark-on-light die", "a bulb", "a red bulb", "a yellow bulb", "a maze", "a 3×3 maze", "a 3×4 maze", "a 4×3 maze", "a 4×4 maze" })]
    VarietyLED,
    [SouvenirQuestion("What digit was displayed but not the answer for the digit display in {0}?", "Variety", ThreeColumns6Answers, "1", "2", "3", "4", "5", "6", "7", "8", "9", "0")]
    VarietyDigitDisplay,
    [SouvenirQuestion("What word could be formed but was not the answer for the letter display in {0}?", "Variety", ThreeColumns6Answers, "ACE", "ACT", "AID", "AIM", "AIR", "ALE", "ALL", "AND", "ANT", "APT", "ARM", "ART", "AWE", "AYE", "BAD", "BAG", "BAR", "BAT", "BAY", "BED", "BEE", "BEG", "BET", "BID", "BIG", "BIT", "BIZ", "BOB", "BOW", "BOY", "BUT", "BUY", "BYE", "CAN", "CAP", "CAR", "CAT", "COP", "COT", "COW", "CUE", "CUP", "CUT", "DAD", "DAM", "DAY", "DIE", "DIG", "DIM", "DIP", "DOG", "DOT", "DRY", "DUE", "DUG", "DUO", "DYE", "EAR", "EAT", "FAN", "FAQ", "FAR", "FAT", "FAX", "FED", "FEE", "FEN", "FEW", "FIN", "FIT", "FIX", "FLY", "FOG", "FOR", "FRK", "FRQ", "FRY", "FUN", "FUR", "GET", "GIG", "GIN", "GUM", "GUT", "GUY", "HAM", "HAT", "HAY", "HEN", "HER", "HEY", "HIM", "HIP", "HIT", "HOP", "HOT", "HOW", "HUT", "ILK", "ILL", "IND", "INK", "IRK", "JAM", "JAR", "JAW", "JOB", "JOY", "KID", "KIN", "KIT", "LAD", "LAP", "LAW", "LAY", "LEG", "LET", "LID", "LIE", "LIP", "LIT", "LOG", "LOO", "LOT", "LOW", "LUA", "LUG", "MAD", "MAN", "MAP", "MAT", "MAX", "MAY", "MIC", "MID", "MIX", "MOB", "MOD", "MUD", "MUG", "MUM", "NET", "NEW", "NIL", "NLL", "NOD", "NOR", "NOT", "NOW", "NUN", "NUT", "OIL", "OPT", "OUR", "OUT", "OWE", "OWL", "PAD", "PAN", "PAR", "PAT", "PAY", "PEG", "PEN", "PER", "PET", "PIE", "PIG", "PIN", "PIT", "POP", "POT", "POW", "PUB", "PUT", "QUA", "QUE", "QUO", "RAG", "RAM", "RAT", "RAW", "RED", "RGB", "RIB", "RID", "RIG", "RIM", "ROB", "ROD", "ROT", "ROW", "RUB", "RUG", "RUM", "RUN", "SAD", "SAW", "SAY", "SEA", "SEE", "SET", "SHE", "SHY", "SIC", "SIG", "SIN", "SIR", "SIT", "SIX", "SLY", "SND", "SUE", "SUM", "SUN", "TAG", "TAP", "TAX", "TEA", "TEE", "TEN", "TGB", "THY", "TIE", "TIN", "TIP", "TOE", "TOO", "TOP", "TOY", "TRN", "TRY", "TUB", "VAT", "VET", "WAR", "WAX", "WAY", "WEE", "WET", "WHY", "WIG", "WIN", "WIT", "WIZ", "WRY", "YEN", "YET", "ZAG", "ZIG")]
    VarietyLetterDisplay,
    [SouvenirQuestion("What was the maximum display for the {1}timer in {0}?", "Variety", ThreeColumns6Answers, "1 1", "2 1", "4 1", "6 1", "1 2", "2 2", "4 2", "1 4", "2 4",
        ExampleFormatArguments = new[] { "", "ascending ", "descending " }, TranslateFormatArgs = new[] { true }, ExampleFormatArgumentGroupSize = 1)]
    VarietyTimer,
    [SouvenirQuestion("What was n for the {1}knob in {0}?", "Variety", TwoColumns4Answers,
        ExampleFormatArguments = new[] { "", "colored ", "red ", "black ", "blue ", "yellow " }, TranslateFormatArgs = new[] { true }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(3, 6)]
    VarietyColoredKnob,
    [SouvenirQuestion("What was n for the {1}bulb in {0}?", "Variety", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { "", "red ", "yellow " }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    [AnswerGenerator.Integers(5, 13)]
    VarietyBulb,

    [SouvenirQuestion("What was the word in {0}?", "Vcrcs", TwoColumns4Answers, "destiny", "control", "refresh", "grouped", "wedging", "summary", "kitchen", "teacher", "concern", "section", "similar", "western", "dropper", "checker", "xeroses", "sunrise", "abolish", "harvest", "protest", "shallow", "plotted", "deafens", "colored", "aroused", "unsling", "holiday", "dictate", "dribble", "retreat", "episode", "crashed", "crazily", "silvers", "usurped", "witcher", "jealous", "village", "wizards", "prosper", "recycle", "pounced", "nonfood", "imblaze", "dryable", "swiftly", "mention", "rubbish", "realize", "collect", "surgeon", "gearbox", "schnozz", "passion", "freshen", "society", "passive", "archive", "shelter", "harmful", "freedom", "papayas", "thwarts", "railway", "teapots", "ravines", "density", "provide", "diagram", "lighter", "general", "upriver", "editors", "mingled", "ransoms", "prairie", "balance", "applied", "history", "calorie", "realism", "liquids", "validly", "varying", "wickers", "isolate", "falsify", "painter", "mixture", "bedroom", "dilemma", "skylike", "ranging", "simplex", "gallied", "missile", "posture", "highway", "prevent", "bracket", "project")]
    VcrcsWord,

    [SouvenirQuestion("What was the color of the {1} vector in {0}?", "Vectors", ThreeColumns6Answers, "Red", "Orange", "Yellow", "Green", "Blue", "Purple", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
    ExampleFormatArguments = new[] { "first", "second", "third", "only" }, ExampleFormatArgumentGroupSize = 1)]
    VectorsColors,

    [SouvenirQuestion("What was the {1} flagpole color on {0}?", "Vexillology", ThreeColumns6Answers, "Red", "Orange", "Green", "Yellow", "Blue", "Aqua", "White", "Black", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    VexillologyColors,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Violet Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "DISMAY", "FRIDGE", "GALLON", "JAMMER", "KIDNEY", "RITUAL", "TRIPOD", "VIKING", "YEANED", "ZIPPER" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    VioletCipherScreen,

    [SouvenirQuestion("What was the desired color in the {1} stage on {0}?", "Visual Impairment", TwoColumns4Answers, "Blue", "Green", "Red", "White", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    VisualImpairmentColors,

    [SouvenirQuestion("Which of these cells was part of the cube’s path in {0}?", "Walking Cube", ThreeColumns6Answers, Type = AnswerType.Sprites)]
    [AnswerGenerator.Grid(4, 4)]
    WalkingCubePath,

    [SouvenirQuestion("What was the displayed sign in {0}?", "Warning Signs", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "WarningSignsSprites")]
    WarningSignsDisplayedSign,

    [SouvenirQuestion("What was the location displayed in {0}?", "WASD", ThreeColumns6Answers, "Bank", "Grocery", "School", "Gym", "Home", "Mall", "Cafe", "Park", "Office")]
    WasdDisplayedLocation,

    [SouvenirQuestion("How many brush strokes were heard in {0}?", "Watching Paint Dry", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(3, 8)]
    WatchingPaintDryStrokeCount,

    [SouvenirQuestion("What was the color on the {1} stage in {0}?", "Wavetapping", TwoColumns4Answers, "Red", "Orange", "Orange-Yellow", "Chartreuse", "Lime", "Green", "Seafoam Green", "Cyan-Green", "Turquoise", "Dark Blue", "Indigo", "Purple", "Purple-Magenta", "Magenta", "Pink", "Gray", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WavetappingColors,
    [SouvenirQuestion("What was the correct pattern on the {1} stage in {0}?", "Wavetapping", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "WavetappingSprites",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WavetappingPatterns,

    [SouvenirQuestion("Who did you eliminate in {0}?", "Weakest Link", OneColumn4Answers, AddThe = true, ExampleAnswers = new[] { "Annie", "Albert", "Josephine", "Frederick" })]
    WeakestLinkElimination,
    [SouvenirQuestion("Who made it to the Money Phase with you in {0}?", "Weakest Link", OneColumn4Answers, AddThe = true, ExampleAnswers = new[] { "Annie", "Albert", "Josephine", "Frederick" })]
    WeakestLinkMoneyPhaseName,
    [SouvenirQuestion("What ratio did {1} get in the Question Phase in {0}?", "Weakest Link", OneColumn4Answers, AddThe = true,
        ExampleFormatArguments = new[] { "Annie", "Albert", "Josephine", "Frederick" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("0-5", "/", "56")]
    WeakestLinkRatio,
    [SouvenirQuestion("What was {1}’s skill in {0}?", "Weakest Link", OneColumn4Answers, "Geography", "Language", "Wildlife", "Biology", "Maths", "KTANE", "History", "Other", AddThe = true, ExampleAnswers = new[] { "KTANE", "Geography", "Language", "Wildlife" },
        ExampleFormatArguments = new[] { "Annie", "Albert", "Josephine", "Frederick" }, ExampleFormatArgumentGroupSize = 1)]
    WeakestLinkSkill,

    [SouvenirQuestion("What was the display text in the {1} stage of {0}?", "What’s on Second", ThreeColumns6Answers, "got it", "says", "display", "leed", "their", "blank", "right", "reed", "hold", "they are", "louder", "lead", "repeat", "ready", "none", "led", "ur", "you’re", "no", "you", "nothing", "middle", "done", "empty", "your", "hold on", "like", "read", "wait", "left", "press", "what?", "uh uh", "they’re", "uhhh", "c", "error", "you are", "next", "yes", "u", "sure", "okay", "what", "cee", "first", "see", "uh huh", "there", "red",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WhatsOnSecondDisplayText,
    [SouvenirQuestion("What was the display text color in the {1} stage of {0}?", "What’s on Second", ThreeColumns6Answers, "Blue", "Cyan", "Green", "Magenta", "Red", "Yellow", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WhatsOnSecondDisplayColor,

    [SouvenirQuestion("What was the {1} non-white arrow in {0}?", "White Arrows", TwoColumns4Answers, ExampleAnswers = new[] { "Blue Up", "Red Right", "Yellow Down", "Green Left", "Purple Up", "Orange Right", "Cyan Down", "Teal Left" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslatableStrings = new[] { "Blue", "Red", "Yellow", "Green", "Purple", "Orange", "Cyan", "Teal", "Up", "Right", "Down", "Left", "{0} {1}" })]
    WhiteArrowsArrows,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "White Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "ATTEND", "BREATH", "CRUNCH", "EFFECT", "JAILED", "JUMPER", "PLASMA", "UPROAR", "VERTEX", "VIEWED" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    WhiteCipherScreen,

    [SouvenirQuestion("What was the display in the {1} stage on {0}?", "WhoOF", ThreeColumns6Answers, "FIRST", "OKAY", "C", "BLANK", "YOU", "READ", "YOUR", "UR", "YES", "LED", "THEIR", "RED", "HIRE", "THERE", "THEY", "THING", "CEE", "LEED", "NO", "HOLD", "PLAY", "LEAD", "HARE", "HERE", " ", "REED", "SAYS", "SEE",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WhoOFDisplay,

    [SouvenirQuestion("What was the display in the {1} stage on {0}?", "Who’s on First", ThreeColumns6Answers, "", "BLANK", "C", "CEE", "DISPLAY", "FIRST", "HOLD ON", "LEAD", "LED", "LEED", "NO", "NOTHING", "OK", "OKAY", "READ", "RED", "REED", "SAY", "SAYS", "SEE", "THEIR", "THERE", "THEY ARE", "THEY’RE", "U", "UR", "YES", "YOU", "YOU ARE", "YOU’RE", "YOUR",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WhosOnFirstDisplay,

    [SouvenirQuestion("What was the display in the first phase of the {1} stage on {0}?", "Who’s on Gas", ThreeColumns6Answers, ExampleAnswers = new[] { "DISPLAY", "PRESS", "PRESSED", "LAST", "START", "ONE" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WhosOnGasDisplay,

    [SouvenirQuestion("What word was transmitted in the {1} stage on {0}?", "Who’s on Morse", ThreeColumns6Answers, "SHELL", "HALLS", "SLICK", "TRICK", "BOXES", "LEAKS", "STROBE", "BISTRO", "FLICK", "BOMBS", "BREAK", "BRICK", "STEAK", "STING", "VECTOR", "BEATS", "CURSE", "NICE", "VERB", "NEARLY", "CREEK", "TRIBE", "CYBER", "CINEMA", "KOALA", "WATER", "WHISK", "MATTER", "KEYS", "STUCK",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WhosOnMorseTransmitDisplay,

    [SouvenirQuestion("What was the color of the {1} dial in {0}?", "Wire", ThreeColumns6Answers, "blue", "green", "grey", "orange", "purple", "red", TranslateAnswers = true, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "top", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, AddThe = true)]
    WireDialColors,
    [SouvenirQuestion("What was the displayed number in {0}?", "Wire", ThreeColumns6Answers, AddThe = true)]
    [AnswerGenerator.Integers(0, 9)]
    WireDisplayedNumber,

    [SouvenirQuestion("What color was the {1} display from the left in {0}?", "Wire Ordering", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", "white", "black", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WireOrderingDisplayColor,
    [SouvenirQuestion("What number was on the {1} display from the left in {0}?", "Wire Ordering", TwoColumns4Answers, "1", "2", "3", "4",
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WireOrderingDisplayNumber,
    [SouvenirQuestion("What color was the {1} wire from the left in {0}?", "Wire Ordering", ThreeColumns6Answers, "red", "orange", "yellow", "green", "blue", "purple", "white", "black", TranslateAnswers = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    WireOrderingWireColor,

    [SouvenirQuestion("How many {1} wires were there in {0}?", "Wire Sequence", TwoColumns4Answers, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "blue", "black" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Integers(0, 9)]
    WireSequenceColorCount,

    [SouvenirQuestion("Which of these was {1} on {0}?", "Wolf, Goat, and Cabbage", ThreeColumns6Answers, "Cat", "Wolf", "Rabbit", "Berry", "Fish", "Dog", "Duck", "Goat", "Fox", "Grass", "Rice", "Mouse", "Bear", "Cabbage", "Chicken", "Goose", "Corn", "Carrot", "Horse", "Earthworm", "Kiwi", "Seeds",
        ExampleFormatArguments = new[] { "present", "not present" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    WolfGoatAndCabbageAnimals,
    [SouvenirQuestion("What was the boat size in {0}?", "Wolf, Goat, and Cabbage", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    WolfGoatAndCabbageBoatSize,

    [SouvenirQuestion("What was the displayed number on {0}?", "Word Count", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(1, 1000)]
    WordCountNumber,

    [SouvenirQuestion("What was the label shown in {0}?", "Working Title", OneColumn4Answers, "foo", "foobar", "quuz", "garply", "plugh", "wibble", "flob", "fuga", "toto", "tutu", "eggs", "alice", "lorem ipsum", "widget", "eek", "bat", "haystack", "blarg", "kalaa", "sub", "momo", "change this", "hi", "thing", "xyz", "bar", "qux", "corge", "waldo", "xyzzy", "wobble", "hoge", "hogera", "tata", "spam", "raboof", "bob", "do stuff", "bla", "moof", "shme", "beekeeper", "dothestuff", "mum", "temp", "var", "placeholder", "hello", "stuff", "text", "baz", "quux", "grault", "fred", "thud", "wubble", "piyo", "hogehoge", "titi", "ham", "fruit", "john doe", "data", "gadget", "gleep", "needle", "blah", "grault", "puppu", "test", "change", "null", "hey", "something", "abc")]
    WorkingTitleLabel,

    [SouvenirQuestion("What was the number in {0}?", "Wumbo", OneColumn4Answers, ExampleAnswers = new[] { "30030", "813244863240810000", "0", "376639725", "27081081027000", "901800900" })]
    [AnswerGenerator.Wumbo]
    WumboNumber,

    [SouvenirQuestion("What was the color of the {1} flash in {0}?", "Xenocryst", ThreeColumns6Answers, ExampleAnswers = new[] { "Red", "Orange", "Yellow", "Green", "Blue", "Indigo" },
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, AddThe = true)]
    Xenocryst,

    [SouvenirQuestion("What was the {1} displayed letter (in reading order) in {0}?", "XmORse Code", ThreeColumns6Answers,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-Z")]
    XmORseCodeDisplayedLetters,
    [SouvenirQuestion("What word did you decrypt in {0}?", "XmORse Code", ThreeColumns6Answers, "ADMIT", "AWARD", "BANJO", "BRAVO", "CHILL", "CYCLE", "DECOR", "DISCO", "EERIE", "ERUPT", "FEWER", "FUZZY", "GERMS", "GUSTO", "HAULT", "HEXED", "ICHOR", "INFER", "JEWEL", "KTANE", "LADLE", "LYRIC", "MANGO", "MUTED", "NERDS", "NIXIE", "OOZED", "OXIDE", "PARTY", "PURSE", "QUEST", "RETRO", "ROUGH", "SCOWL", "SIXTH", "THANK", "TWINE", "UNBOX", "USHER", "VIBES", "VOICE", "WHIZZ", "WRUNG", "XENON", "YOLKS", "ZILCH")]
    XmORseCodeWord,

    [SouvenirQuestion("What song was played on {0}?", "xobekuJ ehT", OneColumn4Answers, ExampleAnswers = new[] { "Gimme Gimme Gimme", "Take On Me", "Barbie Girl", "Do I Wanna Know" })]
    XobekuJehTSong,

    [SouvenirQuestion("Which symbol was scanned in {0}?", "X-Ring", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "XRingSprites")]
    XRingSymbol,

    [SouvenirQuestion("Which shape was scanned by {0}?", "XY-Ray", ThreeColumns6Answers, Type = AnswerType.Sprites, SpriteFieldName = "XYRaySprites")]
    XYRayShapes,

    [SouvenirQuestion("What was the initial roll on {0}?", "Yahtzee", TwoColumns4Answers, "Yahtzee", "large straight", "small straight", "four of a kind", "full house", "three of a kind", "two pairs", "pair", TranslateAnswers = true)]
    YahtzeeInitialRoll,

    [SouvenirQuestion("What was the starting row letter in {0}?", "Yellow Arrows", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    YellowArrowsStartingRow,

    [SouvenirQuestion("What was the {1} color in {0}?", "Yellow Button", TwoColumns4Answers, "Red", "Yellow", "Green", "Cyan", "Blue", "Magenta", AddThe = true,
        ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1, TranslateAnswers = true)]
    YellowButtonColors,

    [SouvenirQuestion("What was the filename in {0}?", "Yellow Button’t", TwoColumns4Answers, ExampleAnswers = new[] { "ABACUS.JPG", "BABBLE.MP4", "CABLES.MP3", "DABBLE.CS", "EAGLES.EXE", "FABLED.ISO" })]
    YellowButtontFilename,

    [SouvenirQuestion("What was on the {1} screen on page {2} in {0}?", "Yellow Cipher", TwoColumns4Answers, ExampleAnswers = new[] { "ALTHOUGH", "BUSINESS", "CHILDREN", "DIRECTOR", "EXCHANGE", "FUNCTION", "GUIDANCE", "HOSPITAL", "INDUSTRY", "JUNCTION", "KEYBOARD", "LANGUAGE", "MATERIAL", "NUMEROUS", "OFFERING", "POSSIBLE", "QUESTION", "RESEARCH", "SOFTWARE", "TOGETHER", "ULTIMATE", "VALUABLE", "WIRELESS", "XENOLITH", "YOURSELF", "ZUCCHINI" },
        ExampleFormatArguments = new[] { "top", "1", "middle", "1", "bottom", "1", "top", "2", "middle", "2", "bottom", "2" }, ExampleFormatArgumentGroupSize = 2, TranslateFormatArgs = new[] { true, false })]
    YellowCipherScreen,

    [SouvenirQuestion("What color was the {1} star in {0}?", "Zero, Zero", TwoColumns4Answers, "black", "blue", "green", "cyan", "red", "magenta", "yellow", "white", TranslateAnswers = true,
        ExampleFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    ZeroZeroStarColors,
    [SouvenirQuestion("How many points were on the {1} star in {0}?", "Zero, Zero", ThreeColumns6Answers, "2", "3", "4", "5", "6", "7", "8",
        ExampleFormatArguments = new[] { "top-left", "top-right", "bottom-left", "bottom-right" }, ExampleFormatArgumentGroupSize = 1, TranslateFormatArgs = new[] { true })]
    ZeroZeroStarPoints,
    [SouvenirQuestion("Where was the {1} square in {0}?", "Zero, Zero", ThreeColumns6Answers, Type = AnswerType.Sprites, TranslateFormatArgs = new[] { true },
        ExampleFormatArguments = new[] { "red", "green", "blue" }, ExampleFormatArgumentGroupSize = 1)]
    [AnswerGenerator.Grid(6, 6)]
    ZeroZeroSquares,

    [SouvenirQuestion("What was the {1} word in {0}?", "Zoni", OneColumn4Answers, ExampleAnswers = new[] { "angel", "thing", "dance", "heavy", "quote", "radio" },
        Type = AnswerType.DynamicFont, ExampleFormatArguments = new[] { QandA.Ordinal }, ExampleFormatArgumentGroupSize = 1)]
    ZoniWords
}
