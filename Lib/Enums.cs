namespace Souvenir
{
    public enum AnswerType
    {
        // These values must match the indexes in SouvenirModule.Fonts/SouvenirModule.FontTextures
        Default = 0,
        SymbolsFont = 1,    // used by 3D Tunnels, Shape Shift, Switches and Colored Switches
        TurtleRobotFont = 2,
        TicTacToeFont = 3,
        UnownFont = 4,
        SugarSkullsFont = 5,
        AsciiMazeFont = 6, // also used by Forget This
        PianoKeysFont = 7,
        JapaneseFont = 8,   // for the Japanese translation
        SnowflakesFont = 9,
        SixtyFourFont = 10,
        CrypticCycleBoozleglyphFont = 11,
        AMessageFont = 12,

        Sprites = -1,     // Special value for answers that use sprites
        DynamicFont = -2, // Special value to indicate that the module handler will obtain the font from the client module
        Audio = -3        // Special value for answers that are audio clips
    }

    public enum AnswerLayout
    {
        OneColumn3Answers,
        OneColumn4Answers,
        TwoColumns2Answers,
        TwoColumns4Answers,
        ThreeColumns3Answers,
        ThreeColumns6Answers,
    }
}
