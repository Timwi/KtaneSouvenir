using System.Collections.Generic;
using Souvenir;
using UnityEngine;

using static Souvenir.AnswerLayout;

public enum SShiftingMaze
{
    [SouvenirQuestion("What was the seed in {0}?", TwoColumns4Answers)]
    [AnswerGenerator.Strings(8, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/")]
    Seed
}

public partial class SouvenirModule
{
    [SouvenirHandler("MazeShifting", "Shifting Maze", typeof(SShiftingMaze), "BigCrunch22")]
    private IEnumerator<SouvenirInstruction> ProcessShiftingMaze(ModuleData module)
    {
        var comp = GetComponent(module, "ShiftingMazeScript");
        var seedTextMesh = GetField<TextMesh>(comp, "Seedling", isPublic: true).Get();
        var seed = seedTextMesh.text;

        var hadStrike = false;
        module.Module.OnStrike += delegate { hadStrike = true; return false; };

        while (module.Unsolved)
        {
            if (hadStrike)
            {
                seed = seedTextMesh.text;
                hadStrike = false;
            }
            yield return null;
        }

        var seedSplit = Regex.Replace(seed, " ", "").Split(':');
        yield return question(SShiftingMaze.Seed).Answers(seedSplit[1]);
    }
}