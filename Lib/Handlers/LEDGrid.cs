using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SLEDGrid
{
    [SouvenirQuestion("How many LEDs were unlit in {0}?", ThreeColumns6Answers)]
    [AnswerGenerator.Integers(0, 9)]
    NumBlack
}

public partial class SouvenirModule
{
    [SouvenirHandler("ledGrid", "LED Grid", typeof(SLEDGrid), "Hawker")]
    private IEnumerator<SouvenirInstruction> ProcessLEDGrid(ModuleData module)
    {
        var comp = GetComponent(module, "ledGridScript");
        yield return null;

        var lights = GetListField<Renderer>(comp, "lights", isPublic: true).Get(expectedLength: 9);
        var colors = GetListField<Texture>(comp, "colours", isPublic: true).Get(expectedLength: 11);
        var blackColor = colors.FirstOrDefault(c => c.name == "black")
            ?? throw new AbandonModuleException("Abandoning LED Grid because ‘colours’ does not contain a texture named ‘black’.");
        var numBlack = lights.Count(l => l.material.mainTexture.name == "black");

        yield return WaitForSolve;

        // Change all of the LEDs to black
        foreach (var light in lights)
            light.material.mainTexture = blackColor;

        yield return question(SLEDGrid.NumBlack).Answers(numBlack.ToString());
    }
}