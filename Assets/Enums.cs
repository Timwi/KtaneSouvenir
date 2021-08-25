﻿namespace Souvenir
{
    enum AnswerType
    {
        // These values must match the indexes in SouvenirModule.Fonts/SouvenirModule.FontTextures
        Default = 0,
        SymbolsFont = 1,    // used by 3D Tunnels, Shape Shift, Switches and Colored Switches
        TurtleRobotFont = 2,
        TicTacToeFont = 3,  // also used by The Bulb
        UnownFont = 4,
        SugarSkullsFont = 5,
        TernaryTilesFont = 6,

        Sprites = -1,           // Special value that doesn’t translate to a font (used for answers that use sprites)
        DynamicFont = -2    // Special value to indicate that the module handler will obtain the font from the client module
    }

    enum AnswerLayout
    {
        TwoColumns4Answers = 0,
        ThreeColumns6Answers = 1,
        OneColumn4Answers = 2
    }
}
