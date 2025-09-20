using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMorseWoF
{
    [SouvenirQuestion("What was the display in the {1} stage on {0}?", ThreeColumns6Answers, ExampleAnswers = ["COULD", "SMALL", "BELOW", "LARGE", "STUDY", "FIRST"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Displays
}

public partial class SouvenirModule
{
    [SouvenirHandler("morseWoF", ".--/---/..-.", typeof(SMorseWoF), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessMorseWoF(ModuleData module)
    {
        var comp = GetComponent(module, "MWoFScript");
        var wordList = new string[] { "COULD", "SMALL", "BELOW", "LARGE", "STUDY", "FIRST", "RIGHT", "THINK", "PLANT", "SOUND", "SIXTY", "BROWN", "VIRUS", "BUSHY", "FUNGI", "OPTED", "YOUNG", "ICHOR", "QUILL", "WRONG", "ZILCH", "JERKY", "BANJO", "PUNCH", "IVORY", "COQUI", "TOPAZ", "JAUNT", "NUDGE", "MAJOR" };
        var fldStage = GetIntField(comp, "stage");
        var display = GetArrayField<TextMesh>(comp, "displays", isPublic: true).Get(expectedLength: 7)[6];

        var displays = new string[4];
        while (module.Unsolved)
        {
            if (wordList.Contains(display.text))
                displays[fldStage.Get()] = display.text;
            yield return null;
        }
        for (var st = 0; st < 3; st++)
            yield return question(SMorseWoF.Displays, args: [Ordinal(st + 1)]).Answers(displays[st], preferredWrong: wordList);
    }
}
