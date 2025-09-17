using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SWhiteArrows
{
    [SouvenirQuestion("What was the {1} non-white arrow in {0}?", TwoColumns4Answers, ExampleAnswers = ["Blue Up", "Red Right", "Yellow Down", "Green Left", "Purple Up", "Orange Right", "Cyan Down", "Teal Left"], Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslatableStrings = ["Blue", "Red", "Yellow", "Green", "Purple", "Orange", "Cyan", "Teal", "Up", "Right", "Down", "Left", "{0} {1}"])]
    Arrows
}

public partial class SouvenirModule
{
    [SouvenirHandler("WhiteArrows", "White Arrows", typeof(SWhiteArrows), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessWhiteArrows(ModuleData module)
    {
        var comp = GetComponent(module, "WhiteArrowsScript");
        var fldStage = GetIntField(comp, "Stage");
        var fldArrow = GetArrayField<int>(comp, "NumberAssist");

        var arrows = new int[7][];

        while (module.Unsolved)
        {
            var stage = fldStage.Get(min: 0, max: 7);
            if (stage is not 7)
            {
                arrows[stage] = fldArrow.Get(expectedLength: 2, validator: v => v is < 0 or > 7 ? "Out of range [0, 7]" : null).ToArray();
                if (arrows[stage][0] is > 3)
                    throw new AbandonModuleException($"Arrow out of range [0, 3] (stage {stage}, arrows {arrows[stage].Stringify()})");
            }
            yield return null;
        }

        if (arrows.Any(a => a is null))
            throw new AbandonModuleException($"A stage was somehow missed: {arrows.Stringify()}");

        var colors = new[] { "Blue", "Red", "Yellow", "Green", "Purple", "Orange", "Cyan", "Teal" };
        var directions = new[] { "Up", "Right", "Down", "Left" };

        string format(int dir, int col) => string.Format(
            TranslateQuestionString(SWhiteArrows.Arrows, "{0} {1}"), 
            TranslateQuestionString(SWhiteArrows.Arrows, colors[col]), 
            TranslateQuestionString(SWhiteArrows.Arrows, directions[dir]));

        var all = (from d in Enumerable.Range(0, 4) from c in Enumerable.Range(0, 8) select format(d, c)).ToArray();

        for (var i = 0; i < arrows.Length; i++)
            yield return question(SWhiteArrows.Arrows, args: [Ordinal(i + 1)]).Answers(format(arrows[i][0], arrows[i][1]), all: all);
    }
}
