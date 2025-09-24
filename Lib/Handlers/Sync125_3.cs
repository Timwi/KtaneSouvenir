using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SSync125_3
{
    [SouvenirQuestion("What was displayed on the screen in the {1} stage of {0}?", TwoColumns4Answers, Type = AnswerType.DynamicFont, ExampleAnswers = ["İ'ms'", "ăĠ'n'", "kğ'i", "kĞ'p'", "ăut'", "ăġ'r", "ăġ'm", "ărs", "kğp'", "kğk"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("sync125_3", "SYNC-125 [3]", typeof(SSync125_3), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessSync125_3(ModuleData module)
    {
        var comp = GetComponent(module, "sync125_3");
        var fldTextId = GetIntField(comp, "textId");
        var fldStage = GetIntField(comp, "stage");
        var words = GetArrayField<string>(comp, "words").Get();
        var screenText = GetField<TextMesh>(comp, "screenText", isPublic: true).Get();
        var submitButton = GetField<KMSelectable>(comp, "submitButton", isPublic: true).Get();

        var textIds = new int[4];

        while (!_isActivated)
            yield return null;

        var oldInteract = submitButton.OnInteract;
        submitButton.OnInteract = () =>
        {
            textIds[fldStage.Get(0, 3)] = fldTextId.Get();
            return oldInteract();
        };

        yield return WaitForSolve;

        var info = new TextAnswerInfo(font: screenText.font, fontTexture: screenText.GetComponent<MeshRenderer>().sharedMaterial.mainTexture);
        for (var stage = 0; stage < 3; stage++)
            yield return question(SSync125_3.Word, args: [(stage + 1).ToString()]).Answers(words[textIds[stage]], preferredWrong: words, info: info);
    }
}
