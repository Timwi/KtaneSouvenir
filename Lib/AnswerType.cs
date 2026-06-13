namespace Souvenir;

public enum AnswerType
{
    // These values must match the indexes in SouvenirModule.Fonts/SouvenirModule.FontTextures
    DefaultFont = 0,

    // Special symbols in the Symbol font:
    // 0–9 : ; < = > ? @ (U+0030–U+0040): card ranks 10, A, 2–9, J, Q, K; suits ♠ ♥ ♣ ♦ — Thirty One
    // upper-case A–P (U+0041–U+0050) — Shape Shift
    // upper-case Q/R (U+0051–U+0052): up/down switches — Switches, Colored Switches, Uncolored Switches
    // lower-case a–z (U+0061–U+007A) and . (U+002E) — 3D Tunnels
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

    DynamicFont = -1, // Special value to indicate that the module handler will obtain the font from the client module
    Sprites = -2,     // Special value for answers that use sprites
    Audio = -3        // Special value for answers that are audio clips
}
