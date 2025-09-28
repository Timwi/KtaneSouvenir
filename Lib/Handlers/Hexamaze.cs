using System.Collections.Generic;
using Souvenir;
using UnityEngine;
using static Souvenir.AnswerLayout;

public enum SHexamaze
{
    [SouvenirQuestion("What was the color of the pawn in {0}?", ThreeColumns6Answers, "Red", "Yellow", "Green", "Cyan", "Blue", "Pink", TranslateAnswers = true)]
    PawnColor,

    [SouvenirDiscriminator("the Hexamaze that {0} a {1} marking on it", Arguments = ["has", "triangle", "has", "circle", "doesn’t have", "hexagon"], ArgumentGroupSize = 2, TranslateArguments = [true, true])]
    Discriminator
}

public partial class SouvenirModule
{
    [SouvenirHandler("HexamazeModule", "Hexamaze", typeof(SHexamaze), "Timwi")]
    private IEnumerator<SouvenirInstruction> ProcessHexamaze(ModuleData module)
    {
        var comp = GetComponent(module, "HexamazeModule");
        yield return WaitForSolve;
        yield return question(SHexamaze.PawnColor).Answers(new[] { "Red", "Yellow", "Green", "Cyan", "Blue", "Pink" }[GetIntField(comp, "_pawnColor").Get(0, 5)]);

        var playfield = GetField<GameObject>(comp, "Playfield", isPublic: true).Get().transform;

        var hasTriangle = false;
        var hasCircle = false;
        var hasHexagon = false;

        for (var i = 0; i < playfield.childCount; i++)
            if (playfield.GetChild(i) is { } child
                    && child.name.StartsWith("Marking")
                    && child.GetComponent<MeshRenderer>() is { sharedMaterial.mainTexture: { } tex })
                switch (tex.name)
                {
                    case "None": break;
                    case "Circle": hasCircle = true; break;
                    case "Hexagon": hasHexagon = true; break;
                    case string f when f.StartsWith("Triangle"): hasTriangle = true; break;
                    default: throw new AbandonModuleException($"Unrecognized symbol texture: {tex.name}");
                }

        yield return new Discriminator(SHexamaze.Discriminator, "triangle", hasTriangle, args: [hasTriangle ? "has" : "doesn’t have", "triangle"]);
        yield return new Discriminator(SHexamaze.Discriminator, "circle", hasCircle, args: [hasCircle ? "has" : "doesn’t have", "circle"]);
        yield return new Discriminator(SHexamaze.Discriminator, "hexagon", hasHexagon, args: [hasHexagon ? "has" : "doesn’t have", "hexagon"]);
    }
}
