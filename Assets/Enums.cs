namespace Souvenir
{
    enum AnswerType
    {
        // These values must match the indexes in SouvenirModule.Fonts/SouvenirModule.FontTextures
        Default = 0,
        SymbolsFont = 1,    // used by 3D Tunnels, Shape Shift, Switches and Colored Switches
        TurtleRobotFont = 2,
        TicTacToeFont = 3,  // also used by The Bulb
        UnownFont = 4,

        // Special value that doesn’t translate to a font
        Sprites = -1
    }

    enum AnswerLayout
    {
        TwoColumns4Answers = 0,
        TwoColumns6Answers = 1,
        OneColumn4Answers = 2
    }

}
