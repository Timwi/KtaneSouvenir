using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPuzzlingHexabuttons
{
    [SouvenirQuestion("What letter was displayed on the {1} hexabutton when submitting in {0}?", ThreeColumns6Answers, Arguments = ["top-left", "top-right", "middle-left", "center", "middle-right", "bottom-left", "bottom-right"], ArgumentGroupSize = 1, TranslateArguments = [true])]
    [AnswerGenerator.Strings('A', 'F')]
    Letter
}

public partial class SouvenirModule
{
    [SouvenirHandler("puzzlingHexabuttons", "Puzzling Hexabuttons", typeof(SPuzzlingHexabuttons), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessPuzzlingHexabuttons(ModuleData module)
    {
        yield return WaitForSolve;

        var comp = GetComponent(module, "puzzlingHexabuttons");
        var texts = GetArrayField<TextMesh>(comp, "buttonText", true).Get(expectedLength: 7);
        var center = texts[6].text[0];
        if (center is < 'A' or > 'F')
            throw new AbandonModuleException($"Center button label ({center}) was not in \"ABCDEF\"");
        var outer = GetArrayField<char>(comp, "solution").Get(expectedLength: 6, validator: v => v is < 'A' or > 'F' ? "Expected character in “ABCDEF”" : null);

        var formats = new[] { "top-left", "top-right", "middle-left", "middle-right", "bottom-left", "bottom-right", "center" };
        var source = outer.Concat(new[] { center });
        for (var i = 0; i < source.Length; i++)
            yield return question(SPuzzlingHexabuttons.Letter, args: [formats[i]]).Answers(source[i].ToString());

        yield return null; // Allow other Souvenirs to grab the text

        foreach (var text in texts)
            text.text = "";
    }
}