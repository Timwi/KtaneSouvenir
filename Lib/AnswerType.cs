namespace Souvenir;

public enum AnswerType
{
    // These values must match the indexes in SouvenirModule.Fonts/SouvenirModule.FontTextures
    DefaultFont = 0,

    // Special symbols in the Symbol font:
    //          0   1   2   3   4   5   6   7   8   9   A   B   C   D   E   F
    //        ┌───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┐
    // U+002x │   │   │   │   │   │   │   │   │   │   │   │   │   │   │███│   │ ███ = 3D Tunnels
    //        ├───┴───┴───┴───┴───┴───┴───┴───┴───┴───┴───┴───┴───┼───┴───┴───┤
    // U+003x │░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░│▒▒▒▒▒▒▒▒▒▒▒│ ░░░ = Thirty One card ranks
    //        ├───┬───────────────────────────────────────────────┴───────────┤ ▒▒▒ = Thirty One card suits
    // U+004x │▒▒▒│▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓│ ▓▓▓ = Shape Shift
    //        ├───┼───────┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┬───┤
    // U+005x │▓▓▓│░░░░░░░│   │   │   │   │   │   │   │   │   │   │   │   │   │ ░░░ = Switches, Colored Switches, Uncolored Switches
    //        ├───┼───────┴───┴───┴───┴───┴───┴───┴───┴───┴───┴───┴───┴───┴───┤
    // U+006x │   │███████████████████████████████████████████████████████████│ ███ = 3D Tunnels
    //        ├───┴───────────────────────────────────────┬───┬───┬───┬───┬───┤
    // U+007x │███████████████████████████████████████████│   │   │   │   │   │
    //        └───────────────────────────────────────────┴───┴───┴───┴───┴───┘
    SymbolsFont = 1,

    TicTacToeFont = 2,
    SugarSkullsFont = 3,
    AsciiMazeFont = 4, // used by ASCII Maze and Forget This
    PianoKeysFont = 5,
    JapaneseFont = 6,   // used by the Japanese translation and by Kanji
    SnowflakesFont = 7,
    SixtyFourFont = 8,
    BoozleglyphFont = 9,
    AMessageFont = 10,
    FourDTunnelsFont = 11,
    CubeFont = 12,
    GreatVoidFont = 13,
    ElderRuneFont = 14,
    LombaxFont = 15,

    DynamicFont = -1, // Special value to indicate that the module handler will obtain the font from the client module
    Sprites = -2,     // Special value for answers that use sprites
    Audio = -3        // Special value for answers that are audio clips
}
