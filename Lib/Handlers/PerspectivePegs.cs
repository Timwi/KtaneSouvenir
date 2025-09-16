using System.Collections.Generic;
using System.Linq;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SPerspectivePegs
{
    [SouvenirQuestion("What was the {1} color in the initial sequence in {0}?", ThreeColumns6Answers, "red", "yellow", "green", "blue", "purple", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    ColorSequence
}

public partial class SouvenirModule
{
    [SouvenirHandler("spwizPerspectivePegs", "Perspective Pegs", typeof(SPerspectivePegs), "Andrio Celos")]
    private IEnumerator<SouvenirInstruction> ProcessPerspectivePegs(ModuleData module)
    {
        var comp = GetComponent(module, "PerspectivePegsModule");
        yield return WaitForSolve;

        var serialNumber = Bomb.GetSerialNumber();

        var keyNumber = 0;
        var prevChar = '\0';
        foreach (var letter in serialNumber)
        {
            if (!char.IsLetter(letter))
                continue;
            if (prevChar == 0)
                prevChar = letter;
            else
            {
                keyNumber += Math.Abs(letter - prevChar);
                prevChar = '\0';
            }
        }
        var colorNames = new[] { "red", "yellow", "green", "blue", "purple" };
        var keyColour = (keyNumber % 10) switch
        {
            0 or 3 => "ColourRed",
            4 or 9 => "ColourYellow",
            1 or 7 => "ColourGreen",
            5 or 8 => "ColourBlue",
            2 or 6 => "ColourPurple",
            _ => throw new AbandonModuleException("Invalid keyNumber % 10."),
        };
        var colourMeshes = GetField<MeshRenderer[,]>(comp, "ColourMeshes").Get();
        var pegIndex = Enumerable.Range(0, 5).IndexOf(px => Enumerable.Range(0, 5).Count(i => colourMeshes[px, i].sharedMaterial.name.StartsWith(keyColour)) >= 3);
        if (pegIndex == -1)
            throw new AbandonModuleException($"The key peg couldn't be found (the key colour was {keyColour}).");
        var source = Enumerable.Range(0, 5)
                    .Select(i => (pegIndex + i) % 5)
                    .Select(n => colorNames.First(cn => colourMeshes[n, n].sharedMaterial.name.Substring(6).StartsWith(cn, StringComparison.InvariantCultureIgnoreCase)));

        for (var ix = 0; ix < source.Length; ix++)
            yield return question(SPerspectivePegs.ColorSequence, args: [Ordinal(ix + 1)]).Answers(source[ix]);
    }
}