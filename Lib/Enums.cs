﻿namespace Souvenir
{
    public enum AnswerType
    {
        // These values must match the indexes in SouvenirModule.Fonts/SouvenirModule.FontTextures
        Default = 0,
        SymbolsFont = 1,    // used by 3D Tunnels, Shape Shift, Switches and Colored Switches
        TurtleRobotFont = 2,
        TicTacToeFont = 3,  // also used by The Bulb
        UnownFont = 4,
        SugarSkullsFont = 5,
        AsciiMazeFont = 6, // also used by Forget This
        PianoKeysFont = 7,
        JapaneseFont = 8,   // for the Japanese translation
        SnowflakesFont = 9,
        SixtyFourFont = 10,

        Sprites = -1,     // Special value for answers that use sprites
        DynamicFont = -2, // Special value to indicate that the module handler will obtain the font from the client module
        Audio = -3        // Special value for answers that are audio clips
    }

    public enum AnswerLayout
    {
        TwoColumns4Answers = 0,
        ThreeColumns6Answers = 1,
        OneColumn4Answers = 2
    }
}
