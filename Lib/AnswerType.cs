namespace Souvenir;

public enum AnswerType
{
    // These values must match the indexes in SouvenirModule.Fonts/SouvenirModule.FontTextures
    DefaultFont = 0,
    SymbolsFont = 1,    // used by 3D Tunnels, Shape Shift, Switches and Colored Switches
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
