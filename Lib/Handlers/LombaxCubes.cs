using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLombaxCubes
{
    [SouvenirQuestion("What was the {1} letter on the button in {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-Z")]
    Letters
}

public partial class SouvenirModule
{
    [SouvenirHandler("lgndLombaxCubes", "Lombax Cubes", typeof(SLombaxCubes), "Marksam")]
    private IEnumerator<SouvenirInstruction> ProcessLombaxCubes(ModuleData module)
    {
        var comp = GetComponent(module, "LombaxCubesScript");
        var fldLetter1 = GetField<TextMesh>(comp, "buttonLetter1", isPublic: true);
        var fldLetter2 = GetField<TextMesh>(comp, "buttonLetter2", isPublic: true);

        yield return WaitForSolve;

        yield return question(SLombaxCubes.Letters, args: ["first"]).Answers(fldLetter1.Get().text);
        yield return question(SLombaxCubes.Letters, args: ["second"]).Answers(fldLetter2.Get().text);
    }
}