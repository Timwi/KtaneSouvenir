using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SMirror
{
    [SouvenirQuestion("What was the second word written by the original ghost in {0}?", TwoColumns4Answers, ExampleAnswers = ["ALPACA", "BUBBLE", "COWBOY", "DIESEL", "EULOGY", "FUSION", "GASKET", "HOODIE", "ICEBOX", "JOYPOP"])]
    Word
}

public partial class SouvenirModule
{
    [SouvenirHandler("mirror", "Mirror", typeof(SMirror), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessMirror(ModuleData module)
    {
        var comp = GetComponent(module, "mirror");
        var fldModuleReady = GetField<bool>(comp, "moduleReady");
        var candidateWords =
            GetStaticField<string[]>(comp.GetType(), "table1").Get().Concat(
            GetStaticField<string[]>(comp.GetType(), "table2").Get().Concat(
            GetStaticField<string[]>(comp.GetType(), "table3").Get())).ToArray();

        while (!fldModuleReady.Get())
            yield return null;

        var position = GetIntField(comp, "fontPosition").Get(min: 0, max: 2);
        var texts = GetArrayField<TextMesh>(comp, "mirrorTexts", isPublic: true).Get(expectedLength: 3);

        yield return WaitForSolve;

        yield return question(SMirror.Word).Answers(texts[position].text, preferredWrong: candidateWords);
    }
}