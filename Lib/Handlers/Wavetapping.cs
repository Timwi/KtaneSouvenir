using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWavetapping
{
    [SouvenirQuestion("What was the color on the {1} stage in {0}?", TwoColumns4Answers, "Red", "Orange", "Orange-Yellow", "Chartreuse", "Lime", "Green", "Seafoam Green", "Cyan-Green", "Turquoise", "Dark Blue", "Indigo", "Purple", "Purple-Magenta", "Magenta", "Pink", "Grey", TranslateAnswers = true, Arguments = [QandA.Ordinal], ArgumentGroupSize = 1)]
    Colors
}

public partial class SouvenirModule
{
    [SouvenirHandler("Wavetapping", "Wavetapping", typeof(SWavetapping), "KingSlendy")]
    [SouvenirManualQuestion("What was the color in the first two stages?")]
    private IEnumerator<SouvenirInstruction> ProcessWavetapping(ModuleData module)
    {
        var comp = GetComponent(module, "scr_wavetapping");

        yield return WaitForSolve;

        var stageColors = GetArrayField<int>(comp, "stageColors").Get(expectedLength: 3);
        var usedColors = GetArrayField<int>(comp, "usedColors").Get(expectedLength: 8);
        var colorNames = GetArrayField<string>(comp, "colorNames").Get(expectedLength: 16);

        var usedColorNames = usedColors.Select(i => colorNames[i]).ToArray();
        
        for (var stage = 0; stage < 2; stage++)
            yield return question(SWavetapping.Colors, args: [Ordinal(stage + 1)]).Answers(colorNames[stageColors[stage]], preferredWrong: usedColorNames);
    }
}
