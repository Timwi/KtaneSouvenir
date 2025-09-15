using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSmallTalk
{
    [SouvenirQuestion("What was on the display in the {1} stage of {0}?", TwoColumns4Answers, ExampleAnswers = ["TOP", "NAH", "INDIA", "UNIFORM"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Displays
}

public partial class SouvenirModule
{
    [SouvenirHandler("SmallTalk", "Small Talk", typeof(SSmallTalk), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessSmallTalk(ModuleData module)
    {
        var comp = GetComponent(module, "SmallTalk");
        var possibleDisplays = new string[] { "TOP", "I", "OK", "YEAH", "APOSTROPHE", "1ST", "NAH", "SEA", "BOTTOM", "'", "CEA", "HOLD UP", "YUP", "EMPTY", "WHO'S ON", "INDIA", "CHARLIE", "UNIFORM", "MT", "ME", "NOPE", "WRONG", "EYE", "WHOSE ON" };
        var stageComp = GetIntField(comp, "Stage");
        var textMeshComp = GetField<TextMesh>(comp, "TopWordTM", isPublic: true);
        var displays = new string[3];
        while (module.Unsolved)
        {
            var stage = stageComp.Get();
            var dispText = textMeshComp.Get().text;
            if (dispText != "")
                displays[stage] = dispText;
            yield return null;
        }
        for (var st = 0; st < 2; st++)
            yield return question(SSmallTalk.Displays, args: [Ordinal(st + 1)]).Answers(displays[st], preferredWrong: possibleDisplays);
    }
}