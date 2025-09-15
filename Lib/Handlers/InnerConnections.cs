using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SInnerConnections
{
    [SouvenirQuestion("What color was the LED in {0}?", ThreeColumns6Answers, "Black", "Blue", "Red", "White", "Yellow", "Green", TranslateAnswers = true)]
    LED,

    [SouvenirQuestion("What was the digit flashed in Morse in {0}?", ThreeColumns6Answers, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9")]
    Morse
}

public partial class SouvenirModule
{
    [SouvenirHandler("InnerConnectionsModule", "Inner Connections", typeof(SInnerConnections), "Brawlboxgaming")]
    private IEnumerator<SouvenirInstruction> ProcessInnerConnections(ModuleData module)
    {
        var comp = GetComponent(module, "InnerConnectionsScript");
        var morseNumber = GetField<int>(comp, "morseNumber").Get();
        var rndLEDColour = GetField<int>(comp, "rndLEDColour").Get();
        yield return WaitForSolve;

        var colourList = new[] { "Black", "Blue", "Red", "White", "Yellow" };

        addQuestions(module,
            makeQuestion(Question.InnerConnectionsLED, module, correctAnswers: new[] { colourList[rndLEDColour] }),
            makeQuestion(Question.InnerConnectionsMorse, module, correctAnswers: new[] { morseNumber.ToString() }));
    }
}