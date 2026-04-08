namespace Souvenir;

public enum AnswerType
{
    // These values must match the indexes in SouvenirModule.Fonts/SouvenirModule.FontTextures
    Default = 0,
    SymbolsFont = 1,    // used by 3D Tunnels, Shape Shift, Switches and Colored Switches
    TicTacToeFont = 2,
    UnownFont = 3,
    SugarSkullsFont = 4,
    AsciiMazeFont = 5, // also used by Forget This
    PianoKeysFont = 6,
    JapaneseFont = 7,   // for the Japanese translation
    SnowflakesFont = 8,
    SixtyFourFont = 9,
    CrypticCycleBoozleglyphFont = 10,
    AMessageFont = 11,
    FourDTunnelsFont = 12,
    CubeFont = 13,

    DynamicFont = -1, // Special value to indicate that the module handler will obtain the font from the client module
    Sprites = -2,     // Special value for answers that use sprites
    Audio = -3        // Special value for answers that are audio clips
}
