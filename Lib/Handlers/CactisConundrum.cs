using System.Collections.Generic;
using System.Linq;
using Souvenir;

using static Souvenir.AnswerLayout;

public enum SCactisConundrum
{
    [SouvenirQuestion("What color was the LED in the {1} stage of {0}?", TwoColumns4Answers, "Blue", "Lime", "Orange", "Red", Arguments = [QandA.Ordinal], ArgumentGroupSize = 1, TranslateAnswers = true)]
    Color
}

public partial class SouvenirModule
{
    [SouvenirHandler("CactusPConundrum", "Cacti’s Conundrum", typeof(SCactisConundrum), "Anonymous")]
    private IEnumerator<SouvenirInstruction> ProcessCactisConundrum(ModuleData module)
    {
        var colors = new int[3];
        var comp = GetComponent(module, "conundramScript");
        var fldStage = GetIntField(comp, "Stage");
        var fldColor = GetIntField(comp, "ColLedState");

        while (!module.IsSolved)
        {
            var stage = fldStage.Get(min: 0, max: 3);
            if (stage == 3) yield break;
            colors[stage] = fldColor.Get(min: 2, max: 5);
            yield return null;
        }

        addQuestions(module, colors.Select((c, i) =>
            makeQuestion(Question.CactisConundrumColor, module,
                formatArgs: new[] { Ordinal(i + 1) },
                correctAnswers: new[] { Question.CactisConundrumColor.GetAnswers()[c - 2] })));
    }
}