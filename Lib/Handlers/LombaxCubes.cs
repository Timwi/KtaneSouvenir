using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLombaxCubes
{
    [Question("What was the {1} letter on the button in {0}?", ThreeColumns6Answers, Type = AnswerType.DynamicFont, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("A-Z")]
    Letters
}

public partial class SouvenirModule
{
    [Handler("lgndLombaxCubes", "Lombax Cubes", typeof(SLombaxCubes), "Marksam")]
    [ManualQuestion("What were the letters on the button?")]
    private IEnumerator<SouvenirInstruction> ProcessLombaxCubes(ModuleData module)
    {
        var comp = GetComponent(module, "LombaxCubesScript");
        var fldLetter1 = GetField<TextMesh>(comp, "buttonLetter1", isPublic: true).Get();
        var fldLetter2 = GetField<TextMesh>(comp, "buttonLetter2", isPublic: true).Get();

        var gotStrike = true;
        module.Module.OnStrike += delegate { gotStrike = true; return false; };

        var letters = new string[2];
        while (!module.IsSolved)
        {
            if (gotStrike)
            {
                letters[0] = fldLetter1.text;
                letters[1] = fldLetter2.text;
            }
            yield return null;
        }

        var info = new TextAnswerInfo(fldLetter1.font, fldLetter1.GetComponent<MeshRenderer>().sharedMaterial.mainTexture);

        yield return question(SLombaxCubes.Letters, args: [Ordinal(1)]).Answers(letters[0], info: info);
        yield return question(SLombaxCubes.Letters, args: [Ordinal(2)]).Answers(letters[1], info: info);
    }
}
