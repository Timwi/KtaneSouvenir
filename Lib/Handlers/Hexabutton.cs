using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SHexabutton
{
    [Question("What was {1} of {0} when it was held?", ThreeColumns6Answers, "blue", "cyan", "gray", "green", "magenta", "purple", "white", TranslateArguments = [true], Arguments = ["the color", "the flickering color"], ArgumentGroupSize = 1)]
    Color,

    [Question("What Morse Code letter was transmitted by {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Strings('A', 'Z')]
    Letter
}

public partial class SouvenirModule
{
    [Handler("hexabutton", "Hexabutton", typeof(SHexabutton), "Espik", AddThe = true)]
    [ManualQuestion("What color was the button when held?")]
    [ManualQuestion("What Morse Code letter was transmitted?")]
    private IEnumerator<SouvenirInstruction> ProcessHexabutton(ModuleData module)
    {
        var comp = GetComponent(module, "hexabuttonScript");

        var colors = GetArrayField<string>(comp, "stripColors").Get();

        yield return WaitForSolve;

        var isTap = GetField<bool>(comp, "answerIsTap").Get();

        if (isTap)
            yield return legitimatelyNoQuestion(module, "The button was meant to be tapped.");

        var flashType = GetIntField(comp, "typeOfLight").Get();
        var flashColor = GetIntField(comp, "lightColor").Get();
        var flashLetter = GetIntField(comp, "letter").Get();

        if (flashType == 2)
            yield return question(SHexabutton.Letter).Answers(((char) ('A' + flashLetter)).ToString());

        else
            yield return question(SHexabutton.Color, args: [flashType == 1 ? "the flickering color" : "the color"]).Answers(colors[flashColor]);
    }
}
