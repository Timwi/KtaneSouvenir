using System;
using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SCaesarPsycho
{
    [SouvenirQuestion("What text was on the top display in the {1} stage of {0}?", ThreeColumns6Answers, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    [AnswerGenerator.Strings("5*A-Z")]
    ScreenTexts,

    [SouvenirQuestion("What color was the text on the top display in the second stage of {0}?", ThreeColumns6Answers, "white", "red", "magenta", "yellow", "green", "cyan", "violet")]
    ScreenColor
}

public partial class SouvenirModule
{
    [SouvenirHandler("caesarPsycho", "Caesar Psycho", typeof(SCaesarPsycho), "Quinn Wuest")]
    private IEnumerator<SouvenirInstruction> ProcessCaesarPsycho(ModuleData module)
    {
        var comp = GetComponent(module, "CaesarPsychoScript");
        var stage = GetIntField(comp, "stage");
        var cols = GetArrayField<Color>(comp, "cols").Get();
        var dletters = GetArrayField<TextMesh>(comp, "dletters", isPublic: true);

        var texts = new string[2];
        var c = -1;
        var colorNames = new string[] { "white", "red", "magenta", "yellow", "green", "cyan", "violet" };
        yield return WaitForActivate;
        while (stage.Get() == 0)
        {
            texts[0] = dletters.Get().Take(5).Select(i => i.text).JoinString();
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log($"<Souvenir #{_moduleId}> CPSY " + texts[0]);
        var tmc = new Color(0, 0, 0);
        while (stage.Get() == 1)
        {
            texts[1] = dletters.Get().Take(5).Select(i => i.text).JoinString();
            var dColor = dletters.Get()[0].color;
            var r = dColor.r;
            r = (int) (r * 10) / 10f;
            tmc = new Color(r, dColor.g, dColor.b);
            c = Array.IndexOf(cols, tmc);
            yield return new WaitForSeconds(.1f);
        }
        Debug.Log($"<Souvenir #{_moduleId}> CPSY " + texts[1]);
        Debug.Log($"<Souvenir #{_moduleId}> CPSY COLORS: " + cols.JoinString());
        Debug.Log($"<Souvenir #{_moduleId}> CPSY TMC: " + tmc);
        Debug.Log($"<Souvenir #{_moduleId}> CPSY " + c);

        yield return WaitForSolve;
        for (var st = 0; st < 2; st++)
            yield return question(SCaesarPsycho.ScreenTexts, args: [Ordinal(st + 1)]).Answers(texts[st]);
        yield return question(SCaesarPsycho.ScreenColor).Answers(colorNames[c], preferredWrong: colorNames);
    }
}
