using System;
using System.Collections.Generic;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SBlackHexabuttons
{
    [Question("What was the character transmitted by the {1} button in {0}?", ThreeColumns6Answers, TranslateArguments = [true], Arguments = ["top-left", "top-right", "middle-left", "middle-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")]
    QCharacters,

    [Discriminator("the Black Hexabuttons where the {0} button transmitted the character {1}", TranslateArguments = [true, false], Arguments = ["top-left", "B", "top-right", "L", "middle-left", "K", "middle-right", "8", "bottom-left", "5", "bottom-right", "4"], ArgumentGroupSize = 2)]
    DCharacters
}

public partial class SouvenirModule
{
    [Handler("blackHexabuttons", "Black Hexabuttons", typeof(SBlackHexabuttons), "Infusion")]
    [ManualQuestion("What were the characters transmitted by each of the outer buttons?")]
    private IEnumerator<SouvenirInstruction> ProcessBlackHexabuttons(ModuleData module)
    {
        var comp = GetComponent(module, "blackHexabuttons");
        var transmissions = GetArrayField<string>(comp, "blackMorse").Get(expectedLength: 6);
        var positions = new string[6] { "top-left", "top-right", "middle-left", "middle-right", "bottom-left", "bottom-right" };
        var morse = new string[36] { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--..", "-----", ".----", "..---", "...--", "....-", ".....", "-....", "--...", "---..", "----." };

        yield return WaitForSolve;

        for (var i = 0; i < 6; i++)
        {
            var answer = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"[Array.IndexOf(morse, transmissions[i].ToString())].ToString();
            yield return new Discriminator(SBlackHexabuttons.DCharacters, $"character{i}", transmissions[i], args: [positions[i], answer]);
            yield return question(SBlackHexabuttons.QCharacters, args: [positions[i]]).AvoidDiscriminators($"character{i}").Answers(answer);
        }
    }
}
