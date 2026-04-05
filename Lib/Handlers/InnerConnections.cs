using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SInnerConnections
{
    [Question("What color was the LED in {0}?", ThreeColumns6Answers, "Black", "Blue", "Red", "White", "Yellow", "Green", TranslateAnswers = true)]
    LED,

    [Question("What was the digit flashed in Morse in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
    Morse
}

public partial class SouvenirModule
{
    [Handler("InnerConnectionsModule", "Inner Connections", typeof(SInnerConnections), "Brawlboxgaming")]
    [ManualQuestion("What color was the LED?")]
    [ManualQuestion("What was the digit flashed in Morse?")]
    private IEnumerator<SouvenirInstruction> ProcessInnerConnections(ModuleData module)
    {
        var comp = GetComponent(module, "InnerConnectionsScript");
        var morseNumber = GetField<int>(comp, "morseNumber").Get();
        var rndLEDColour = GetField<int>(comp, "rndLEDColour").Get();
        yield return WaitForSolve;

        var colourList = new[] { "Black", "Blue", "Red", "White", "Yellow" };

        yield return question(SInnerConnections.LED).Answers(colourList[rndLEDColour]);
        yield return question(SInnerConnections.Morse).Answers(morseNumber.ToString());
    }
}